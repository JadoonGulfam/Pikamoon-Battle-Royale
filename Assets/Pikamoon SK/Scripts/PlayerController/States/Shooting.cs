using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using static UnityEditorInternal.ReorderableList;

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
        [SerializeField] Transform DebugTransform;
        [SerializeField] RectTransform DebugUITransform;

        bool _isAiming;
        bool _isInAttack;
        bool AllowFire;
        int bulletIndex;
        float riggingVal;
        float FireRate;
        Coroutine cancelAimRoutine;

        public override void Initialize()
        {
            base.Initialize();
            
            playerInput.onAttack1_Down += PlayFireAnimation;
            playerInput.onAttack2_Down += StartAim;
            playerInput.onAttack2_Up += CancelAim;

            AllowFire = true;
        }

        private void Update()
        {
            if (!Controller.MP_Setup.isMinePlayer)
                return;


            if (Controller.ActiveWeapon.Data.Type != WeaponType.Ranged)
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
            

        //public override void Initialize()
        //{
        //    if (Controller.ActiveWeapon.Data.Type != WeaponType.Ranged)
        //        return;

        //    AssignWeapon();
        //}

        void AssignWeapon()
        {
            bulletIndex = 0;
            if(Controller.ActiveWeapon.Prefab is RangedWeapon)
            {
                ActiveWeapon = Controller.ActiveWeapon.Prefab as RangedWeapon;

                FireRate = ActiveWeapon.GetFireRate();
            }
        }


        public void ActivateWeapon(Weapon _weapon)
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

        void HandleSpeed()
        {
            if(playerInput.isMoving)
            {
                Controller.ChangeSpeed(Controller.PlayerData.WalkSpeed,0.2f);
            }
            else
            {
                Controller.ChangeSpeed(0,0);
            }
        }

        void HandleAnimation()
        {
            AC.PAnimator.SetFloat(AC.Parameters.Speed.Hash, Controller.AnimSpeed);
            if (Controller.IsGrounded && !Controller.IsInAttack)
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

            AC.PAnimator.SetBool(AC.Parameters.isWalkRun.Hash, true);
            AC.PAnimator.SetBool(AC.Parameters.isAiming.Hash, true);
        }
        void CancelAim()
        {
            if (Controller.ActiveWeapon.Data.Type != WeaponType.Ranged)
                return;

            ReferencesHolder.Instance._cameraController.ChangeCam(Cam.Default);
            ReferencesHolder.Instance._cameraController.ChangeAimZoom(false);

            _isAiming = false;

            AC.PAnimator.SetBool(AC.Parameters.isAiming.Hash, false);
            AC.PAnimator.SetFloat(AC.Parameters.XVal.Hash, 0);

            Controller.IsInAttack = false;

            LookTowardCameraForward = false;
        }

        IEnumerator CancelAimAndAttack()
        {
            float waitTime = _isInAttack ? CancelAimAfterSeconds : 0f;

            yield return new WaitForSeconds(waitTime);

            LookTowardCameraForward = false;

            if(_isAiming)
                ReferencesHolder.Instance._cameraController.ChangeAimZoom(false);
            ReferencesHolder.Instance._cameraController.ChangeCam(Cam.Default);

            //AR_LockedOnTargetAimer.weight = 0;
            AC.PAnimator.SetBool(AC.Parameters.isAiming.Hash, false);
            Controller.IsInAttack = false;
            _isInAttack = false;

            yield return new WaitForSeconds (CancelAttackAfterSeconds);

            AC.PAnimator.SetLayerWeight(1, 0);
            AC.PAnimator.SetBool(AC.Parameters.inCombat.Hash, false);
        }
        
        
        public void PlayFireAnimation()
        {
            if (!AllowFire || Controller.ActiveWeapon.Data.Type != WeaponType.Ranged)
                return;


            _isInAttack = true;

            //AR_LockedOnTargetAimer.weight = 1;

            Controller.IsInAttack = true;

            ReferencesHolder.Instance._cameraController.ChangeCam(Cam.Aim);

            AC.PAnimator.SetLayerWeight(1, 1);
            AC.PAnimator.SetBool(AC.Parameters.isAiming.Hash, true);
            AC.PAnimator.SetBool(AC.Parameters.isWalkRun.Hash, true);
            AC.PAnimator.SetTrigger(AC.Parameters.Shoot.Hash);

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

            Ray ray = Controller._cameraController._camera.ScreenPointToRay(screenCenterPoint);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 999f, AimableMask))
            {
                if(ActiveWeapon == null)
                {
                    AssignWeapon();
                }
                else
                {
                    ActiveWeapon.ShootBullet(hit.point);
                    DebugTransform.transform.position = hit.point;
                }
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
            if (!Controller.MP_Setup.isMinePlayer)
                return;

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