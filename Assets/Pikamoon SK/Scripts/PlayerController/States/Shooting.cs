using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Pikamoon.Controller
{
    public class Shooting : State
    {
        RangedWeapon ActiveWeapon;

        public Transform FirePoint;
        
        [Space]
        [Header("Animation Rigging")]

        [SerializeField] Rig AR_LockedOnTargetAimer;
        [SerializeField] Transform AR_LookTarget;
        [SerializeField] float AR_LookSpeed;
        
        [Space]
        [Header("Aim")]

        [SerializeField] LayerMask AimableMask;
        [SerializeField] float MaxAimRange;
        [Space]
        [SerializeField] bool LookTowardCameraForward;
        [SerializeField] float RotOffsetDuringAim;
        [Space]
        [SerializeField] float CancelAimAfterSeconds;
        [SerializeField] float CancelAttackAfterSeconds;

        [Space]
        [Header("Animation Variables")]
        int AnimParamHash_isAiming;
        int AnimParamHash_Throw;
        int AnimParamHash_SecondaryState;
        int AnimParamHash_InCombat;
        int AnimParamHash_isWalkRun;
        int AnimParamHash_XVal;
        int AnimParamHash_YVal;


        [Space]
        [SerializeField] Transform DebugTransform;
        [SerializeField] RectTransform DebugUITransform;

        bool _isAiming;
        bool _isInAttack;
        bool AllowFire;
        int bulletIndex;
        float riggingVal;
        float FireRate;
        Coroutine cancelAimRoutine;

        private void Start()
        {
            base.Initialize();
            

            AnimParamHash_Throw           =   Animator.StringToHash  (     "Shoot"           );
            AnimParamHash_isAiming        =   Animator.StringToHash  (     "isAiming"        );
            AnimParamHash_InCombat        =   Animator.StringToHash  (     "inCombat"        );
            AnimParamHash_SecondaryState  =   Animator.StringToHash  (     "SecondaryState"  );
            AnimParamHash_isWalkRun       =   Animator.StringToHash  (     "isWalkRun"       );
            AnimParamHash_XVal            =   Animator.StringToHash  (     "XVal"            );
            AnimParamHash_YVal            =   Animator.StringToHash  (     "YVal"            );



            playerInput.onAttack1_Down += PlayFireAnimation;
            playerInput.onAttack2_Down += StartAim;
            playerInput.onAttack2_Up += CancelAim;

            AllowFire = true;
        }

        private void Update()
        {
            if (Controller.ActiveWeapon.Data.Type != WeaponType.Ranged)
                return;

            AimRigging();

            if (!Controller.IsInAttack)
                return;

            MoveDuringAim();
            RotatePlayerTowardsCamFor();
        }

        public override void Initialize()
        {
            if (Controller.ActiveWeapon.Data.Type != WeaponType.Ranged)
                return;

            AssignWeapon();
        }

        void AssignWeapon()
        {
            bulletIndex = 0;

            ActiveWeapon = Controller.ActiveWeapon.Prefab as RangedWeapon;

            FireRate = ActiveWeapon.GetFireRate();
        }

        void AimRigging()
        {
            if (_isAiming)
            {
                if(riggingVal < 1)
                    riggingVal += Time.deltaTime * AR_LookSpeed;


                AR_LookTarget.position = FirePoint.position;
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

        void MoveDuringAim()
        {

            Vector3 direction = Controller.GetDirectionAccordingToCameraWhenMoving();


            AC.PAnimator.SetFloat(AnimParamHash_XVal, playerInput.Horizontal);
            AC.PAnimator.SetFloat(AnimParamHash_YVal, playerInput.Vertical);


            // Always apply vertical velocity (for gravity or jumping)
            Vector3 finalMove = new Vector3(direction.x * Controller.Speed, playerInput.JumpVelocity, direction.z * Controller.Speed);

            // Move the character based on calculated velocity and speed
            Controller.Move(finalMove);
        }

        void RotatePlayerTowardsCamFor()
        {
            if(LookTowardCameraForward)
            {
                Controller.RotatePlayerTowardsCameraForwardDirectionDuringAim(10,RotOffsetDuringAim);
            }
        }

        void StartAim()
        {
            if (Controller.ActiveWeapon.Data.Type != WeaponType.Ranged)
                return;

            Controller._cameraController.ChangeCam(Cam.Aim);
            Controller._cameraController.ChangeAimZoom(true);

            AC.PAnimator.SetLayerWeight(1, 1);

            Controller.IsInAttack = true;
            _isAiming = true;
            LookTowardCameraForward = true;

            AC.PAnimator.SetBool(AnimParamHash_isWalkRun, true);
            AC.PAnimator.SetBool(AnimParamHash_isAiming, true);
        }
        void CancelAim()
        {
            if (Controller.ActiveWeapon.Data.Type != WeaponType.Ranged)
                return;

            ReferencesHolder.Instance._CameraController.ChangeCam(Cam.Default);
            ReferencesHolder.Instance._CameraController.ChangeAimZoom(false);

            _isAiming = false;

            AC.PAnimator.SetBool(AnimParamHash_isAiming, false);
            AC.PAnimator.SetFloat(AnimParamHash_XVal, 0);

            Controller.IsInAttack = false;

            LookTowardCameraForward = false;
        }

        IEnumerator CancelAimAndAttack()
        {
            float waitTime = _isInAttack ? CancelAimAfterSeconds : 0f;

            yield return new WaitForSeconds(waitTime);

            LookTowardCameraForward = false;

            if(_isAiming)
                ReferencesHolder.Instance._CameraController.ChangeAimZoom(false);
            ReferencesHolder.Instance._CameraController.ChangeCam(Cam.Default);

            //AR_LockedOnTargetAimer.weight = 0;
            AC.PAnimator.SetBool(AnimParamHash_isAiming, false);
            Controller.IsInAttack = false;
            _isInAttack = false;

            yield return new WaitForSeconds (CancelAttackAfterSeconds);

            AC.PAnimator.SetLayerWeight(1, 0);
            AC.PAnimator.SetBool(AnimParamHash_InCombat, false);
        }
        
        
        public void PlayFireAnimation()
        {
            if (!AllowFire || Controller.ActiveWeapon.Data.Type != WeaponType.Ranged)
                return;


            _isInAttack = true;

            //AR_LockedOnTargetAimer.weight = 1;

            Controller.IsInAttack = true;

            ReferencesHolder.Instance._CameraController.ChangeCam(Cam.Aim);

            AC.PAnimator.SetLayerWeight(1, 1);
            AC.PAnimator.SetBool(AnimParamHash_isAiming, true);
            AC.PAnimator.SetBool(AnimParamHash_isWalkRun, true);
            AC.PAnimator.SetTrigger(AnimParamHash_Throw);

            LookTowardCameraForward = true;

            StartCoroutine(RegulateFire());

            if (cancelAimRoutine != null)
                StopCoroutine(cancelAimRoutine);

            if(!_isAiming)
                cancelAimRoutine = StartCoroutine(CancelAimAndAttack());
        }

        public void ShootArrow()
        {
            Vector2 screenCenterPoint = new Vector2(Screen.width / 2, Screen.height / 2);
            DebugUITransform.position = screenCenterPoint;

            Ray ray = Controller._cameraController._camera.ScreenPointToRay(screenCenterPoint);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 999f, AimableMask))
            {
                ActiveWeapon.ShootBullet(hit.point);
                DebugTransform.transform.position = hit.point;
            }
        }


        void EndAttack()
        {

        }

        IEnumerator RegulateFire()
        {
            AllowFire = false;

            yield return new WaitForSeconds(FireRate);
            AllowFire = true;
        }


        private void OnDestroy()
        {
            playerInput.onAttack1_Clicked -= PlayFireAnimation;
            playerInput.onAttack2_Down -= StartAim;
            playerInput.onAttack2_Up -= CancelAim;
        }

        public override void OnEnd()
        {
        }

        public override void OnStart()
        {
        }

        public override void OnUpdate()
        {
        }
    }
}