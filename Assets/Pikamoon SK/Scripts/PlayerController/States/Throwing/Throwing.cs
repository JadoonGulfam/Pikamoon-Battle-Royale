using UnityEngine;
using UnityEngine.Animations.Rigging;


namespace Pikamoon.Controller
{
    public class Throwing : State
    {
        [Header("Public References")]
        public ThrowableWeapon ActiveWeapon;
        public Transform HoldingPoint;
        public Transform curvePoint;
        [SerializeField] LayerMask AimableMask;

        [Space]
        [Header("Animation Rigging")]

        [SerializeField] Rig AR_LockedOnTargetAimer;
        [SerializeField] Transform AR_LookTarget;
        [SerializeField] float AR_LookSpeed;


        [Space]
        [Header("Animation Variables")]

        [Space]
        [Header("Bools")]
        public bool walking = true;
        public bool aiming = false;
        public bool hasWeapon = true;
        public bool pulling = false;
        
        [Space]
        [SerializeField] float AdditionalAimRotation;

        [Space]
        [Header("UI")]
        //public Image reticle;

        private Rigidbody weaponRb;
        private float returnTime;

        private Vector3 origLocPos;
        private Vector3 origLocRot;
        private Vector3 pullPosition;


        bool isActiveWeaponInHand;
        bool _isAiming;

        bool inAttack;
        float riggingVal;

        Vector3 DefaultPos;
        Quaternion DefaultRot;
        Transform CamTransform;

        public override void Initialize()
        {
            base.Initialize();

            playerInput.onAttack2_Down += StartAim;
            playerInput.onAttack2_Up += CancelAim;

            playerInput.onAttack1_Down += DecideToAttackOrCatch;


            CamTransform = Controller.cameraController._camera.transform;

            if (Controller.ActiveWeapon.Data.Type != WeaponType.Throwable)
                return;

            AssignWeapon();

        }
        public override void Initialize(Transform Root)
        {
            base.Initialize(Root);

            playerInput.onAttack2_Down += StartAim;
            playerInput.onAttack2_Up += CancelAim;

            playerInput.onAttack1_Down += DecideToAttackOrCatch;


            CamTransform = Controller.cameraController._camera.transform;

            if (Controller.ActiveWeapon.Data.Type != WeaponType.Throwable)
                return;

            AssignWeapon();

        }
        public override StateType GetStateType()
        {
            return StateType.Throwing;
        }
        public void ActivateWeapon( Weapon _weapon)
        {
            ActiveWeapon = _weapon as ThrowableWeapon; 
            
            DefaultPos = ActiveWeapon.transform.localPosition;
            DefaultRot = ActiveWeapon.transform.localRotation;

            isActiveWeaponInHand = true;
        }

        void AssignWeapon()
        {
            ActiveWeapon = Controller.ActiveWeapon.Prefab as ThrowableWeapon;

            DefaultPos = ActiveWeapon.transform.localPosition;
            DefaultRot = ActiveWeapon.transform.localRotation;

            isActiveWeaponInHand = true;
        }


        public PlayerSetupForMultiplayer MP_Setup;
        private void Update()
        {
            //if (Controller.MP_Setup != null && !Controller.MP_Setup.isMinePlayer)
            //    return;

            if (MP_Setup != null && !MP_Setup.isMinePlayer)
                return;


            if (Controller.ActiveWeapon.Data.Type != WeaponType.Throwable)
            {
                ActiveWeapon = null;
                return;
            }
            AimRigging();

            if (!Controller.IsInAttack)
                return;

            HandleAnimation();
            MoveDuringAim();
            RotatePlayerTowardsCamFor();
        }
        
        void AimRigging()
        {

            if (_isAiming)
            {
                if (riggingVal < 1)
                    riggingVal += Time.deltaTime * AR_LookSpeed;


                AR_LookTarget.position = HoldingPoint.position;
                Vector2 screenCenterPoint = new Vector2(Screen.width / 2, Screen.height / 2);

                Ray ray = Controller.cameraController._camera.ScreenPointToRay(screenCenterPoint);

                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 999f, AimableMask))
                {
                    AR_LookTarget.position = hit.point;
                }
            }
            else
            {
                if (riggingVal > 0)
                    riggingVal -= Time.deltaTime * AR_LookSpeed;
            }

            AR_LockedOnTargetAimer.weight = riggingVal;
        }



        public void StartAim()
        {
            if (Controller.ActiveWeapon.Data.Type != WeaponType.Throwable || Controller.IsSwimming)// || !isActiveWeaponInHand)
                return;

            ReferencesHolder.Instance._cameraController.ChangeAimZoom(true);

            AC.PAnimator.SetLayerWeight(2, 1);

            _isAiming = true;

            AC.PAnimator.SetBool(AC.Parameters.isWalkRun.Hash, true);
            AC.PAnimator.SetBool(AC.Parameters.isAiming.Hash, true);

            //if (inAttack)
            //{
            //}

            Controller.IsInAttack = true;
        }

        public void CancelAim()
        {
            if (Controller.ActiveWeapon.Data.Type != WeaponType.Throwable)
                return;

            ReferencesHolder.Instance._cameraController.ChangeAimZoom(false);

            _isAiming = false;

            AC.PAnimator.SetBool(AC.Parameters.isAiming.Hash, false);

            AC.PAnimator.SetLayerWeight(2, 0);

            AC.PAnimator.SetFloat(AC.Parameters.XVal.Hash, 0);
         
            Controller.IsInAttack = false;
        }

        public void DecideToAttackOrCatch()
        {
            if (Controller.ActiveWeapon.Data.Type != WeaponType.Throwable || Controller.IsSwimming)
                return;

            inAttack = true;

            if (ActiveWeapon == null)
            {
                AssignWeapon();
            }

            if (isActiveWeaponInHand)
            {
                AC.PAnimator.SetLayerWeight(2, 1);
                AC.PAnimator.SetTrigger(AC.Parameters.Shoot.Hash);
                
                Controller.IsInAttack = true;
            }
            else
            {
                AC.PAnimator.SetInteger(AC.Parameters.SecondaryState.Hash, 1);
                ActiveWeapon.CallItBack(curvePoint);
            }

            //Controller.IsInAttack = true;    
        }

        public void ThrowFromAimPoint()
        {
            isActiveWeaponInHand = false;

            Vector2 screenCenterPoint = new Vector2(Screen.width / 2, Screen.height / 2);

            Ray ray = ReferencesHolder.Instance._cameraController._camera.ScreenPointToRay(screenCenterPoint);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 999f, AimableMask))
            {
                if (ActiveWeapon == null)
                {
                    AssignWeapon();
                }
                ActiveWeapon.Throw(this,HoldingPoint,hit.point);
            }

            if (!_isAiming)
            {
                AC.PAnimator.SetBool(AC.Parameters.isAiming.Hash, false);
            }
            inAttack = false;
            CancelAim();

        }

        public Vector3 GetQuadraticCurvePoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
        {
            float u = 1 - t;
            float tt = t * t;
            float uu = u * u;
            return (uu * p0) + (2 * u * t * p1) + (tt * p2);
        }

        void HandleAnimation()
        {
            AC.PAnimator.SetFloat(AC.Parameters.Speed.Hash, Controller.AnimSpeed);
            if (Controller.IsGrounded)// && !Controller.IsInAttack)
            {
                AC.PAnimator.SetBool(AC.Parameters.isWalkRun.Hash, playerInput.isMoving);
            }
        }

        void MoveDuringAim()
        {

            HandleSpeed();


            Vector3 direction = Controller.GetDirectionAccordingToCameraWhenMoving();

            AC.PAnimator.SetFloat(AC.Parameters.XVal.Hash, playerInput.Horizontal);
            AC.PAnimator.SetFloat(AC.Parameters.YVal.Hash, playerInput.Vertical);

            // Always apply vertical velocity (for gravity or jumping)
            Vector3 finalMove = new Vector3(direction.x * Controller.Speed, playerInput.JumpVelocity, direction.z * Controller.Speed);

            // Move the character based on calculated velocity and speed
            Controller.Move(finalMove);
        }
        void RotatePlayerTowardsCamFor()
        {

            Vector3 forward = CamTransform.forward + (CamTransform.right * AdditionalAimRotation);
            forward.y = 0f;

            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(forward), Time.deltaTime * 10);
        }


        void HandleSpeed()
        {
            if (playerInput.isMoving)
            {
                Controller.ChangeSpeed(Controller.PlayerData.WalkSpeed, 0.2f);
            }
            else
            {
                Controller.ChangeSpeed(0, 0);
            }
        }

        public void WeaponCatchSuccessfully()
        {
            isActiveWeaponInHand = true;

            if (ActiveWeapon == null)
            {
                AssignWeapon();
            }
            ActiveWeapon.transform.parent = HoldingPoint;

            ActiveWeapon.transform.localPosition = DefaultPos;
            ActiveWeapon.transform.localRotation = DefaultRot;
            
            AC.PAnimator.SetInteger(AC.Parameters.SecondaryState.Hash, 2);

            //AC.PAnimator.SetLayerWeight(2, 0);
        }

        public override void OnEnd()
        {
        }

        public override void OnUpdate()
        {
        }

        public override void OnStart()
        {
        }

        private void OnDestroy()
        {
            //playerInput.onAttack2_Down -= StartAim;
            //playerInput.onAttack2_Up -= CancelAim;
            //playerInput.onAttack1_Down -= DecideToAttackOrCatch;
        }
    }
}