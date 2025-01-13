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
        int Anim_isAimingHash;
        int Anim_ThrowHash;
        int Anim_isWalkRunHash;
        int Anim_SecondaryStateHash;
        int Anim_XVal;
        int Anim_YVal;

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
        void Start()
        {
            Initialize();

            isActiveWeaponInHand = true;

            Anim_ThrowHash = Animator.StringToHash("Shoot");
            Anim_isAimingHash = Animator.StringToHash("isAiming");
            Anim_SecondaryStateHash = Animator.StringToHash("SecondaryState");
            Anim_isWalkRunHash = Animator.StringToHash("isWalkRun");
            Anim_XVal = Animator.StringToHash("XVal");
            Anim_YVal = Animator.StringToHash("YVal");


            if (Controller.ActiveWeapon.Data.Type == WeaponType.Ranged)
            {
                //Initialize(Controller.GetWeaponAs<RangedWeaponSO>());
            }

            CamTransform = Controller._cameraController._camera.transform;

            //reticle.DOFade(0, 0);

            playerInput.onAttack2_Down += StartAim;
            playerInput.onAttack2_Up   += CancelAim;

            playerInput.onAttack1_Down += DecideToAttackOrCatch;

        }

        public void ActivateWithWeapon()
        {

        }
        
        public override void Initialize()
        {
            base.Initialize();

            if (Controller.ActiveWeapon.Data.Type != WeaponType.Throwable)
                return;

            ActiveWeapon = Controller.ActiveWeapon.Prefab as ThrowableWeapon;


            DefaultPos = ActiveWeapon.transform.localPosition;
            DefaultRot = ActiveWeapon.transform.localRotation;

        }

        private void Update()
        {
            if (Controller.ActiveWeapon.Data.Type != WeaponType.Throwable)
                return;

            AimRigging();

            if (!Controller.IsInAttack)
                return;


            if (!Controller.IsInAttack || Controller.ActiveWeapon.Data.Type != WeaponType.Throwable)
                return;

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

                Ray ray = Controller._cameraController._camera.ScreenPointToRay(screenCenterPoint);

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
            if (Controller.ActiveWeapon.Data.Type != WeaponType.Throwable || !isActiveWeaponInHand || !isActiveWeaponInHand)
                return;

            ReferencesHolder.Instance._CameraController.ChangeAimZoom(true);

            AC.PAnimator.SetLayerWeight(2, 1);

            _isAiming = true;
            
            AC.PAnimator.SetBool(Anim_isWalkRunHash, true);
            AC.PAnimator.SetBool(Anim_isAimingHash, true);

            //if (inAttack)
            //{
            //}

            Controller.IsInAttack = true;
        }

        public void CancelAim()
        {
            if (Controller.ActiveWeapon.Data.Type != WeaponType.Throwable)
                return;

            ReferencesHolder.Instance._CameraController.ChangeAimZoom(false);

            _isAiming = false;

            AC.PAnimator.SetBool(Anim_isAimingHash, false);

            //AC.PAnimator.SetLayerWeight(2, 0);

            AC.PAnimator.SetFloat(Anim_XVal, 0);
         
            Controller.IsInAttack = false;
        }

        public void DecideToAttackOrCatch()
        {
            if (Controller.ActiveWeapon.Data.Type != WeaponType.Throwable)
                return;


            inAttack = true;

            if (isActiveWeaponInHand)
            {
                AC.PAnimator.SetLayerWeight(2, 1);
                AC.PAnimator.SetTrigger(Anim_ThrowHash);
                
                Controller.IsInAttack = true;

            }
            else
            {
                AC.PAnimator.SetInteger(Anim_SecondaryStateHash,1);
                ActiveWeapon.CallItBack(curvePoint);
            }

            Controller.IsInAttack = true;    
        }

        public void ThrowFromAimPoint()
        {
            isActiveWeaponInHand = false;

            Vector2 screenCenterPoint = new Vector2(Screen.width / 2, Screen.height / 2);

            Ray ray = ReferencesHolder.Instance._CameraController._camera.ScreenPointToRay(screenCenterPoint);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 999f, AimableMask))
            {
                ActiveWeapon.Throw(this,HoldingPoint,hit.point);
                //DebugTransform.transform.position = hit.point;
            }

            if (!_isAiming)
            {
                AC.PAnimator.SetBool(Anim_isAimingHash, false);
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

        void MoveDuringAim()
        {
            Vector3 direction = Controller.GetDirectionAccordingToCameraWhenMoving();

            AC.PAnimator.SetFloat(Anim_XVal, playerInput.Horizontal);
            AC.PAnimator.SetFloat(Anim_YVal, playerInput.Vertical);

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

        public void WeaponCatchSuccessfully()
        {
            isActiveWeaponInHand = true;

            ActiveWeapon.transform.parent = HoldingPoint;

            ActiveWeapon.transform.localPosition = DefaultPos;
            ActiveWeapon.transform.localRotation = DefaultRot;
            
            AC.PAnimator.SetInteger(Anim_SecondaryStateHash, 2);

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
            playerInput.onAttack2_Down -= StartAim;
            playerInput.onAttack2_Up -= CancelAim;
            playerInput.onAttack1_Down -= DecideToAttackOrCatch;
        }
    }
}