using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;
using Unity.Cinemachine;


namespace Pikamoon.Controller
{
    public class Throwing : State
    {
        private Rigidbody weaponRb;
        private float returnTime;

        private Vector3 origLocPos;
        private Vector3 origLocRot;
        private Vector3 pullPosition;
        [Space]
        [SerializeField] float AdditionalAimRotation;

        [Header("Public References")]
        public ThrowableWeapon ActiveWeapon;
        public Transform HoldingPoint;
        public Transform curvePoint;
        [SerializeField] LayerMask AimableMask;

        [Space]
        [Header("Animation Variables")]
        int Anim_isAimingHash;
        int Anim_ThrowHash;
        int Anim_SecondaryStateHash;
        int Anim_InCombatHash;

        [Space]
        [Header("Bools")]
        public bool walking = true;
        public bool aiming = false;
        public bool hasWeapon = true;
        public bool pulling = false;

        [Space]
        [Header("UI")]
        //public Image reticle;
        bool isActiveWeaponInHand;
        bool isAimButtonHeld;

        bool inAttack;

        Vector3 DefaultPos;
        Quaternion DefaultRot;

        void Start()
        {
            base.Initialize();
            isActiveWeaponInHand = true;
            Anim_ThrowHash = Animator.StringToHash("Shoot");
            Anim_isAimingHash = Animator.StringToHash("isAiming");
            Anim_SecondaryStateHash = Animator.StringToHash("SecondaryState");

            if (Controller.activeWeapon.Type == WeaponType.Ranged)
            {
                //Initialize(Controller.GetWeaponAs<RangedWeaponSO>());
            }

            DefaultPos = ActiveWeapon.transform.localPosition;
            DefaultRot = ActiveWeapon.transform.localRotation;
            //reticle.DOFade(0, 0);

            playerInput.onAttack2_Down += StartAim;
            playerInput.onAttack2_Up   += CancelAim;

            playerInput.onAttack1_Down += DecideToAttackOrCatch;

        }
        private void Update()
        {
            if (!Controller.IsInAttack || Controller.activeWeapon.Type != WeaponType.Throwable)
                return;

            MoveDuringAim();
            RotatePlayerTowardsCamFor();
        }

        public void StartAim()
        {
            if (Controller.activeWeapon.Type != WeaponType.Throwable || !isActiveWeaponInHand || !isActiveWeaponInHand)
                return;

            ReferencesHolder.Instance._CameraController.ChangeAimZoom(true);

            Controller.Anim.SetLayerWeight(2, 1);

            isAimButtonHeld = true;
            
            Controller.Anim.SetBool("isWalkRun", true);
            Controller.Anim.SetBool(Anim_isAimingHash, true);

            //if (inAttack)
            //{
            //}

            Controller.IsInAttack = true;
        }

        public void CancelAim()
        {
            if (Controller.activeWeapon.Type != WeaponType.Throwable)
                return;

            ReferencesHolder.Instance._CameraController.ChangeAimZoom(false);

            isAimButtonHeld = false;

            Controller.Anim.SetBool(Anim_isAimingHash, false);

            //Controller.Anim.SetLayerWeight(2, 0);

            Controller.Anim.SetFloat("XVal", 0);
         
            Controller.IsInAttack = false;
        }

        public void DecideToAttackOrCatch()
        {
            if (Controller.activeWeapon.Type != WeaponType.Throwable)
                return;

            inAttack = true;

            if (isActiveWeaponInHand)
            {
                Controller.Anim.SetLayerWeight(2, 1);
                
                Controller.IsInAttack = true;

                Controller.Anim.SetTrigger(Anim_ThrowHash);
            }
            else
            {
                Controller.Anim.SetInteger(Anim_SecondaryStateHash,1);
                ActiveWeapon.CallItBack(curvePoint);
            }

            Controller.IsInAttack = true;    
        }

        public void ThrowFromAimPoint()
        {
            isActiveWeaponInHand = false;

            Vector2 screenCenterPoint = new Vector2(Screen.width / 2, Screen.height / 2);

            Ray ray = ReferencesHolder.Instance._CameraController.camera.ScreenPointToRay(screenCenterPoint);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 999f, AimableMask))
            {
                ActiveWeapon.Throw(this,HoldingPoint,hit.point);
                //DebugTransform.transform.position = hit.point;
            }

            if (!isAimButtonHeld)
            {
                Controller.Anim.SetBool(Anim_isAimingHash, false);
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

            Controller.Anim.SetFloat("XVal", playerInput.Horizontal);
            Controller.Anim.SetFloat("YVal", playerInput.Vertical);

            // Always apply vertical velocity (for gravity or jumping)
            Vector3 finalMove = new Vector3(direction.x * Controller.Speed, playerInput.JumpVelocity, direction.z * Controller.Speed);

            // Move the character based on calculated velocity and speed
            Controller.Move(finalMove);
        }
        void RotatePlayerTowardsCamFor()
        {
            Vector3 forward = Controller._camera.transform.forward + (Controller._camera.transform.right * AdditionalAimRotation);
            forward.y = 0f;

            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(forward), Time.deltaTime * 10);
        }

        public void WeaponCatchSuccessfully()
        {
            isActiveWeaponInHand = true;

            ActiveWeapon.transform.parent = HoldingPoint;

            ActiveWeapon.transform.localPosition = DefaultPos;
            ActiveWeapon.transform.localRotation = DefaultRot;
            
            Controller.Anim.SetInteger(Anim_SecondaryStateHash, 2);

            //Controller.Anim.SetLayerWeight(2, 0);
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
        }
    }
}