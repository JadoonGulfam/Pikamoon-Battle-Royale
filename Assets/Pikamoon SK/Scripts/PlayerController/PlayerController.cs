using Pikamoon.UI;
using UnityEngine;

namespace Pikamoon.Controller
{
    [System.Serializable]
    public struct WeaponInfo
    {
        public Weapon Prefab;
        public WeaponDataSO Data;
        public bool isEnabled;
    }

    [System.Serializable]
    public struct HoldingPoint
    {
        public WeaponHoldingPointType Type;
        public Transform Point;
    }

    [System.Serializable]
    public struct RestingPoint
    {
        public WeaponRestingPointType Type;
        public Transform Point;
    }

    public enum StateType
    {
        Locomtion,
        Crouch,
        Slide,
        Air,
        Combat,
        Throwing,
        Shooting,
        Swimming,
        Battle,
        Capture
    }


    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        public PlayerData PlayerData;
        public StateType CurrentPlayerState;

        public PlayerSetupForMultiplayer MP_Setup;

        [Header("References")]
        public Transform Head; 
        [SerializeField] State[] states;
        
        [Header("Weapon")]
        [Space]
        public HoldingPoint[] holdingPoints;
        public RestingPoint[] restingPoints;
        [HideInInspector] public WeaponInfo ActiveWeapon;

        //public T GetWeaponAs<T>() where T : Weapon
        //{
        //    return ActiveWeapon as T; // Tries to cast the currentWeapon to the specified type
        //}

        [Space]
        [Header("Rotation Setting")]
        public float TurnSmoothTime;


        [Header("Grounded Settings")]
        [Space]
        [SerializeField] Vector3 crouchColliderOffset;
        [SerializeField] Vector3 groundCheckColliderScale;
        public LayerMask groundLayer;
        


        [HideInInspector] public CameraController _cameraController;
        [HideInInspector] public PlayerInput input;
        [HideInInspector] public InventoryController inventory;
        [HideInInspector] public AnimatorController AC;
        [HideInInspector] public HealthController HC;
        [HideInInspector] public HitBehaviour HitBehaviour;
       // public PlayerSetupForMultiplayer MP_Setup;

        Shooting _shooting;
        Throwing _throwing;
        Combat _combat;

        CharacterController characterController;

        float defaultHeight;
        float defaultRadius;
        Vector3 defaultCenter;


        float moveSpeedLerper;
        float animSpeedLerper;



        [SerializeField] bool isRootMotionEnabled;
        public bool IsRootMotionEnabled
        {
            get { return isRootMotionEnabled; }
            set { isRootMotionEnabled = value; }
            
        }
        
        [SerializeField] bool canExitCrouch;
        public bool CanExitCrouch
        {
            get
            {
                if (CurrentPlayerState == StateType.Crouch && Physics.CheckBox(this.transform.position + crouchColliderOffset, new Vector3(.5f, 1, .5f), Quaternion.identity, groundLayer))
                    return true;
                else
                    return false;
            }
        }

        public bool IsInAttack;
        public bool IsSwimming;
        public bool IgnoreGravity;

        float speed;
        public float Speed
        {
            get
            {
                return speed;
            }
        }
        

        float animSpeed;
        public float AnimSpeed
        {
            get
            {
                return animSpeed;
            }
        }

        [SerializeField] bool isGrounded;
        public bool IsGrounded
        {
            get
            {
                return isGrounded;
                //return Physics.CheckBox(this.transform.position + (Vector3.down * (groundCheckColliderScale.y / 2)), groundCheckColliderScale, Quaternion.identity, groundLayer);
                //return characterController.isGrounded;
            }
        }

        [SerializeField] bool cameraOrbitStatus;
        public bool CameraOrbitStatus
        {
            get
            {
                return cameraOrbitStatus;
            }
            set
            {
                cameraOrbitStatus = value;
            }
        }



        [SerializeField] bool inAir;
        public bool InAir
        {
            get
            {
                return inAir;
            }
            set
            {
                inAir = value;
            }
        }



        public Vector3 Velocity
        {
            get 
            {
                return characterController.velocity;
            }
        }

        private void Start()
        {
            characterController = this.GetComponent<CharacterController>();
            inventory = GetComponent<InventoryController>();
            HC = GetComponent<HealthController>();

        }
        public void Inititalize(PlayerInput _input, CameraController _camera, UIManagerSK _uiManager)
        {
            input = _input;
            _cameraController = _camera;

            CameraOrbitStatus = true;

            characterController = this.GetComponent<CharacterController>();
            inventory = GetComponent<InventoryController>();
            HC = GetComponent<HealthController>();


            IgnoreGravity = false;

            defaultHeight = characterController.height;
            defaultRadius = characterController.radius;
            defaultCenter = characterController.center;

            foreach (var state in states)
            {
                state.Initialize(this.transform);


                switch(state.GetStateType())
                {
                    case StateType.Combat:
                        _combat = state.GetComponent<Combat>();
                        
                        break;
                    case StateType.Throwing:
                        _throwing = state.GetComponent<Throwing>();
                        break;
                    case StateType.Shooting:
                        _shooting = state.GetComponent<Shooting>();
                        break;
                }
            }


            //_combat = this.GetComponent<Combat>();
            //_throwing = this.GetComponent<Throwing>();
            //_shooting = this.GetComponent<Shooting>();


            inventory.Initialize(_uiManager, this);
            HC.Initialize(_uiManager);

        }


        private void Update()
        {

            if(MP_Setup == null)
            {
                AdjustSpeed();
                IsGroundedCheck();
                CameraOrbit();
            }
            else
            {
                if(MP_Setup.isMinePlayer)
                {
                    AdjustSpeed();
                    IsGroundedCheck();
                    CameraOrbit();
                }
            }
        }

        public void ToggleCursor(bool flag)
        {
            Cursor.visible = flag;
            Cursor.lockState = !flag ? CursorLockMode.None : CursorLockMode.Confined;
        }

        #region Weapon Portion
        public void ActivateWeapon(WeaponInfo weapon)
        {
            ActiveWeapon = weapon;

            //ActiveWeapon.Prefab.gameObject.SetActive(true);

            if(ActiveWeapon.Prefab != null)
            {
                ActiveWeapon.Prefab.transform.parent = holdingPoints[(int)weapon.Data.HoldingPointType].Point;
                ActiveWeapon.Prefab.transform.localPosition = Vector3.zero;
                ActiveWeapon.Prefab.transform.localRotation = Quaternion.identity;
            }

            if(weapon.Data.Type == WeaponType.None)
            {
                _combat.ActivatingFistNoWeapon(weapon.Data);
            }

            else if (weapon.Data.Type == WeaponType.Melee)
            {
                _combat.ActivateWeapon(weapon.Prefab);
            }

            else if (weapon.Data.Type == WeaponType.Ranged)
            {
                _shooting.ActivateWeapon(weapon.Prefab);
            }
            
            else if (weapon.Data.Type == WeaponType.Throwable)
            {
                _throwing.ActivateWeapon(weapon.Prefab);
            }
        }
        public Transform GetRestingPoint(WeaponRestingPointType type)
        {
            return restingPoints[(int)type].Point;
        }
        public Transform GetRestingPoint(WeaponInfo weapon)
        {
            return restingPoints[(int)weapon.Data.HoldingPointType].Point;
        }
        #endregion


        #region Camera Portion

        public Vector3 GetDirectionAccordingToCameraWhenMoving()
        {
            Vector3 direction = transform.forward;

            if (input.isMoving)
            {
                Vector3 CamForward = _cameraController._camera.transform.forward.normalized;
                Vector3 CamRight = _cameraController._camera.transform.right.normalized;

                CamForward.y = 0;
                CamRight.y = 0;

                direction = (CamForward * input.Vertical + CamRight * input.Horizontal).normalized;
            }

            return direction;
        }

        public Vector3 GetDirectionAccordingToCameraIgnoringMoving()
        {
            Vector3 direction = transform.forward;

                Vector3 CamForward = _cameraController._camera.transform.forward.normalized;
                Vector3 CamRight = _cameraController._camera.transform.right.normalized;

                CamForward.y = 0;
                CamRight.y = 0;

            if (input.isMoving)
            {
                direction = (CamForward * input.Vertical + CamRight * input.Horizontal).normalized;
            }
            else
            {
                direction = (CamForward  + CamRight).normalized;
            }

            return direction;
        }

        public void RotatePlayerTowardDirection(Vector3 Direction, float Speed)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Direction), Time.deltaTime * Speed);
        }

        public void RotateTowardsCameraForwardDirection(float Speed)
        {
            Vector3 Dir = GetDirectionAccordingToCameraIgnoringMoving();

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Dir), Time.deltaTime * Speed);
        }

        public void RotatePlayerTowardsCameraForwardDirectionDuringAim(float Speed,float AdditionalVal)
        {
            Vector3 forward = _cameraController._camera.transform.right + (_cameraController._camera.transform.forward * AdditionalVal);
            forward.y = 0f;

            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(forward), Time.deltaTime * Speed);
        }

        public void CameraOrbit()
        {
            _cameraController.CameraOrbitStatus(cameraOrbitStatus);
            input.AllowInputFlagWhileUIEnabled = cameraOrbitStatus;
        }

        #endregion


        #region Movement Portion

        public void Move(Vector3 direction)
        {
            if (IsRootMotionEnabled)
                return;

            characterController.Move(direction * Time.deltaTime);
        }

        public void Move(float Speed)
        {

        }

        public void Move(Vector3 direction, float _speed)
        {
            if (IsRootMotionEnabled)
                return;

            characterController.Move(direction * _speed * Time.deltaTime);
        }
        
        public void RootMove(Vector3 direction)
        {
            characterController.Move(direction);
        }

        public void MoveTowards(Transform Target, float Speed)
        {

        }
        #endregion


        #region Character Controller Portion

        public void IsGroundedCheck()
        {
            isGrounded =  Physics.CheckBox(this.transform.position + (Vector3.down * (groundCheckColliderScale.y / 2)), groundCheckColliderScale, Quaternion.identity, groundLayer);
        }

        public void SetCharacterController(float height, float radius, Vector3 center)
        {
            if (characterController)
            {
                characterController.height = height;
                characterController.radius = radius;
                characterController.center = center;
            }
        }
        public void SetCharacterControllerDefault()
        {
            if (characterController)
            {
                characterController.height = defaultHeight;
                characterController.radius = defaultRadius;
                characterController.center = defaultCenter;
            }
        }

        #endregion


        public void ChangeState(StateType newState)
        {
            CurrentPlayerState = newState;
        }


        #region Speed Adjustment
        
        public void AdjustSpeed()
        {
            speed     = Mathf.Lerp(speed     , moveSpeedLerper, Time.deltaTime * PlayerData.Acceleration         );
            animSpeed = Mathf.Lerp(animSpeed , animSpeedLerper, Time.deltaTime * PlayerData.AnimationAcceleration);
        }
        
        public void ChangeSpeed(float MovementSpeed)
        {
            moveSpeedLerper = MovementSpeed;
        }

        public void ChangeSpeed(float MovementSpeed, float AnimationSpeed)
        {
            moveSpeedLerper = MovementSpeed;
            animSpeedLerper = AnimationSpeed;
        }

        public void ChangeMovementSpeed(float val)
        {

            moveSpeedLerper = val;
        }

        public void ChangeAnimationSpeed(float val)
        {
            animSpeedLerper = val;
        }
       

        #endregion
    }
}