using System.Collections;
using Unity.Mathematics;
using UnityEngine;

namespace Pikamoon.Controller
{
    public class Swimming : State
    {
        [Header("Water Detection")]
        public LayerMask waterLayer;          // The layer that represents water in the scene
        public float chestDisToFeet;          // The height difference between chest and feet for depth checking
        public float UpForce;                 // Upward floating force to keep player buoyant

        [Header("Climb Out Settings")]
        [Space]
        public float SwimmingMaxLedgeClimbHeight = 2f;   // Max ledge height that can be climbed
        public float LedgeDetectDistance = 1f;           // How far ahead to detect climbable ledge
        public float LedgeCheckRadius = 0.4f;            // Size of detection sphere
        public float SurfaceOffset = 0.8f;               // Offset to place player above ledge top

        bool _inWater;                        // Whether player is inside water volume
        bool _isNormalSwim;                   // Whether player is swimming at normal speed
        bool _isSwimming;                     // Whether player is currently swimming
        float _verticalForce;                 // Vertical force applied for floating

        public bool IsInWater => _inWater;    // Public getter for water status

        public Transform ClimbSphere1;
        public Transform ClimbSphere2;
        public Transform ClimbSphere3;


        Coroutine endClimbRoutine;

        [Header("Multiplayer Setup (Optional)")]
        public PlayerSetupForMultiplayer MP_Setup;
        bool isClimbingOut;

        // ---------------------------------------------------------
        // Setup and Initialization
        // ---------------------------------------------------------
        public override void Initialize()
        {
            base.Initialize();
            playerInput.onSprint_Down += EnableFastSwim;
            playerInput.onSprint_Up += DisbleFastSwim; 

            playerInput.onJump_Down += TryClimbOut;
        }

        public override void Initialize(Transform Root)
        {
            base.Initialize(Root);
            playerInput.onSprint_Down += EnableFastSwim;
            playerInput.onSprint_Up += DisbleFastSwim;

            playerInput.onJump_Down += TryClimbOut;
        }

        public override StateType GetStateType() => StateType.Swimming;

        // ---------------------------------------------------------
        // Update is called once per frame
        // ---------------------------------------------------------
        private void Update()
        {
            // If this is a multiplayer player and not owned by the local user, skip logic
            if (MP_Setup != null && !MP_Setup.isMinePlayer)
                return;

            // Check if the player is touching water
            _inWater = Physics.CheckSphere(this.transform.position, 0.5f, waterLayer);

            // If not in water, skip further swimming logic
            if (!_inWater || isClimbingOut)
                return;

            // Check if player is deep enough to swim
            if (IsInEnoughDeepToSwim())
            {
                // Enter swimming mode if not already swimming
                if (!_isSwimming)
                    OnStart();


                HandleSwimmingSpeed();     // Adjust swimming speed based on movement & sprint
                MovementAndRotationHandler(); // Move and rotate player while swimming
                HandleAnimation();         // Sync animator with movement
                CheckPlayerFloating();     // Manage buoyancy force
            }
            else
            {
                // Exit swimming mode if player is too shallow
                if (_isSwimming)
                    OnEnd();
            }


        }

        // ---------------------------------------------------------
        // Buoyancy and Depth Check
        // ---------------------------------------------------------
        void CheckPlayerFloating()
        {
            // This ray goes down from above player to find water surface
            if (Physics.Raycast(this.transform.position + (Vector3.up * 100), Vector3.down, out RaycastHit hit, 120, waterLayer))
            {
                float dist = Vector3.Distance(hit.point, this.transform.position);

                // If player’s body is below chest depth, apply upward force to float
                if (dist > chestDisToFeet)
                    _verticalForce = UpForce;
                else
                    _verticalForce = 0;
            }
        }

        bool IsInEnoughDeepToSwim()
        {
            // Check whether player’s shoulders are under water
            Vector3 shoulderPosition = transform.position + Vector3.up * chestDisToFeet;
            return Physics.CheckSphere(shoulderPosition, 0.1f, waterLayer);
        }

        // ---------------------------------------------------------
        // Speed and Movement Handling
        // ---------------------------------------------------------
        void HandleSwimmingSpeed()
        {
            if (playerInput.isMoving)
            {
                Controller.ChangeSpeed(
                    _isNormalSwim ? Controller.PlayerData.SwimmingNormalSpeed : Controller.PlayerData.SwimmingFastSpeed,
                    _isNormalSwim ? 1f : 2f
                );
            }
            else
            {
                _isNormalSwim = true;
                Controller.ChangeSpeed(0f, 0f);
            }
        }

        void HandleAnimation()
        {
            // Update animation speed parameter
            AC.PAnimator.SetFloat(AC.Parameters.Speed.Hash, Controller.AnimSpeed);
        }

        void MovementAndRotationHandler()
        {
            // Always set YVal (vertical blend) to 1 while swimming
            AC.PAnimator.SetFloat(AC.Parameters.YVal.Hash, 1);

            // Determine swimming direction based on camera orientation
            Vector3 direction = Controller.GetDirectionAccordingToCameraWhenMoving();

            // Smoothly rotate towards that direction
            Controller.RotatePlayerTowardDirection(direction, Controller.TurnSmoothTime);

            // Combine directional and vertical movement
            Vector3 finalMove = new Vector3(direction.x * Controller.Speed, _verticalForce, direction.z * Controller.Speed);

            // Apply final movement to player
            Controller.Move(finalMove);
        }

        // ---------------------------------------------------------
        // Speed Mode Controls (Normal / Fast Swim)
        // ---------------------------------------------------------
        void ToggleFastSwim()
        {
            _isNormalSwim = !_isNormalSwim;
            Controller.ChangeSpeed(
                _isNormalSwim ? Controller.PlayerData.SwimmingNormalSpeed : Controller.PlayerData.SwimmingFastSpeed,
                _isNormalSwim ? 1f : 2f
            );
        }

        void EnableFastSwim()
        {
            if (Controller.CurrentPlayerState != StateType.Swimming)
                return;
            _isNormalSwim = false;
        }

        void DisbleFastSwim()
        {
            if (Controller.CurrentPlayerState != StateType.Swimming)
                return;
            _isNormalSwim = true;
        }

        // ---------------------------------------------------------
        // State Transitions
        // ---------------------------------------------------------
        public override void OnEnd()
        {
            Controller.CurrentPlayerState = StateType.Locomtion;
            _isSwimming = false;
            Controller.IgnoreGravity = false;
            Controller.IsSwimming = false;
            isClimbingOut = false;

            // Disable swimming animation
            AC.PAnimator.SetBool(AC.Parameters.isSwim.Hash, false);
        }

        public override void OnStart()
        {
            isClimbingOut = false;
            Controller.CurrentPlayerState = StateType.Swimming;
            Controller.IgnoreGravity = true;
            Controller.IsSwimming = true;

            _verticalForce = 0;
            _isNormalSwim = true;
            _isSwimming = true;

            // Enable swimming animation
            AC.PAnimator.SetBool(AC.Parameters.isSwim.Hash, true);
        }

        public override void OnUpdate() { }

        private void OnDisable()
        {
            playerInput.onSprint_Down -= EnableFastSwim;
            playerInput.onSprint_Up -= DisbleFastSwim;

            playerInput.onJump_Down -= TryClimbOut;
        }


        // ---------------------------------------------------------
        // --- NEW FEATURE: CLIMB OUT OF WATER ---------------------
        // ---------------------------------------------------------

        void TryClimbOut()
        {
            // Check for climb-out input while swimming
            if (_isSwimming)
            {
                if (CanClimbOut(out Vector3 targetClimbPos))
                {
                    StartCoroutine(PerformClimbOut(targetClimbPos));
                    return;
                }
            }
        }


        // Check if there’s a climbable ledge in front of player
        bool CanClimbOut(out Vector3 climbPoint)
        {
            climbPoint = Vector3.zero;

            // Start from chest level forward
            Vector3 chestPos = Controller.transform.position + Vector3.up * chestDisToFeet;

            ClimbSphere1.transform.position = chestPos;

            if (Physics.Raycast(chestPos, Controller.transform.forward, out RaycastHit forwardHit, LedgeDetectDistance, Controller.groundLayer))
            {
                ////// From the wall hit, shoot a ray downward from above to find ledge top
                ////Vector3 topCheckStart = forwardHit.point + Vector3.up * SwimmingMaxLedgeClimbHeight;

               // Vector3 origin = forwardHit.point + Vector3.up * 10;

                Vector3 origin = forwardHit.point + Vector3.up * 10f + Controller.transform.forward * .75f;

                ClimbSphere2.transform.position = forwardHit.point;

                if (Physics.BoxCast(origin, new Vector3(.1f,0.01f,0.1f), Vector3.down, out RaycastHit topHit, quaternion.identity, 20, Controller.groundLayer))
                {
                    //ClimbSphere.transform.position = topHit.point;
                    //ClimbSphere.transform.position = topHit.point;

                    if (Vector3.Distance(forwardHit.point, topHit.point) < SwimmingMaxLedgeClimbHeight)
                    {

                        ClimbSphere3.transform.position = topHit.point;
                        climbPoint = topHit.point;
                        return true;
                    }

                }
            }
            return false;
        }

        // Coroutine handles climb animation and movement
        IEnumerator PerformClimbOut(Vector3 climbTarget)
        {
            isClimbingOut = true;

            // Determine the facing direction towards climb point
            Vector3 lookDir = (climbTarget - Controller.transform.position);
            lookDir.y = 0;
            if (lookDir != Vector3.zero)
                Controller.transform.rotation = Quaternion.LookRotation(lookDir);

            // Trigger climb-out animation (61 = climb animation state)
            AC.PAnimator.SetInteger(AC.Parameters.SecondaryState.Hash, 61);

            // Initial position before climbing
            Vector3 startPos = Controller.transform.position;

            // Define how far player should stay away from the ledge when done climbing
            float ledgeBackwardOffset = SurfaceOffset; // tweak between 0.3f–0.6f depending on player size

            // Find a point a little *behind* the ledge (away from wall)
            Vector3 ledgeAwayDir = -Controller.transform.forward * ledgeBackwardOffset;

            // Adjust hold position (slightly below climb top for animation alignment)
            Vector3 LedgeHoldPosition = climbTarget;
            LedgeHoldPosition.y = climbTarget.y - 2.808f;
            LedgeHoldPosition += ledgeAwayDir; // push back from wall

            // Smooth climb transition
            float t = 0f;
            while (t < 1f)
            {
                t += Time.deltaTime * 3f; // climb speed
                Controller.transform.position = Vector3.Lerp(startPos, LedgeHoldPosition, t);
                yield return null;
            }


            Controller.IsRootMotionEnabled = true;
            // Trigger climb animation (make sure Animator has “ClimbOut” trigger)
            AC.PAnimator.SetInteger(AC.Parameters.SecondaryState.Hash, 62);

            if (endClimbRoutine != null)
                StopCoroutine(endClimbRoutine);

            endClimbRoutine = StartCoroutine(ForceExitSwimmingClimb());
        }

        IEnumerator ForceExitSwimmingClimb()
        {
            yield return new WaitForSeconds(2);

            EndSwimmingThroughClimb();
        }

        public void EndSwimmingThroughClimb()
        {
            if (endClimbRoutine != null)
                StopCoroutine(endClimbRoutine);

            Controller.IsRootMotionEnabled = false;
            Controller.InAir = true;
            OnEnd();
        }


    }
}
