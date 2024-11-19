using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Pikamoon.Controller
{
    public class Shooting : State
    {
        RangedWeaponSO ActiveWeapon;

        public Transform FirePoint;

        [Space]
        [Header("Aim")]
        [SerializeField] Rig AR_LockedOnTargetAimer;
        [SerializeField] Transform AimLookTarget;
        [SerializeField] LayerMask AimableMask;
        [SerializeField] float MaxAimRange;
        [SerializeField] bool LookTowardCameraForward;
        [SerializeField] float CancelAimAfterSeconds;
        [SerializeField] float CancelAttackAfterSeconds;

        [Space]
        [Header("Bullet")]
        public int MaxPoolSize;
        public List<Bullet> bulletsPool;

        [Space]
        [SerializeField] Transform DebugTransform;
        [SerializeField] RectTransform DebugUITransform;
        [SerializeField] float Value;

        bool AllowFire;
        int bulletIndex;
        Camera cam;
        Coroutine cancelAimRoutine;

        private void Start()
        {
            Initialize();

            if(Controller.activeWeapon.Type == WeaponType.Ranged)
            {
                Initialize(Controller.GetWeaponAs<RangedWeaponSO>());
            }

            playerInput.onAttack1_Down += PlayFireAnimation;
            playerInput.onAttack2_Down += StartAim;
            playerInput.onAttack2_Up += CancelAim;


            AllowFire = true;
        }

        private void Update()
        {
            if (!Controller.IsInAttack || Controller.activeWeapon.Type != WeaponType.Ranged)
                return;

            MoveDuringAim();
            RotatePlayerTowardsCamFor();
        }

        public void Initialize(RangedWeaponSO rangedWeapon)
        {
            bulletIndex = 0;
            ActiveWeapon = rangedWeapon;
            MakePool(ActiveWeapon.Bullet);

            cam = ReferencesHolder.Instance._CameraController.camera;
        }

        void RotatePlayerTowardsCamFor()
        {
            if(LookTowardCameraForward)
            {
                Controller.RotatePlayerTowardsCameraForwardDirectionDuringAim(10,Value);
            }
        }

        void StartAim()
        {
            if(Controller.activeWeapon.Type == WeaponType.Ranged)
            {
                ReferencesHolder.Instance._CameraController.ChangeAimZoom(true);
            }
        }
        void CancelAim()
        {
            if (Controller.activeWeapon.Type == WeaponType.Ranged)
            {
                ReferencesHolder.Instance._CameraController.ChangeAimZoom(false);
            }
        }


        IEnumerator CancelAimAndAttack()
        {
            yield return new WaitForSeconds(CancelAimAfterSeconds);

            AR_LockedOnTargetAimer.weight = 0;
            Controller.Anim.SetBool("isAiming", false);
            ReferencesHolder.Instance._CameraController.ChangeCam(Cam.Default);
            Controller.IsInAttack = false;

            yield return new WaitForSeconds (CancelAttackAfterSeconds);

            Controller.Anim.SetBool("inCombat", false);
            Controller.Anim.SetLayerWeight(1, 0);
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


        void EndAttack()
        {

        }

        public void PlayFireAnimation()
        {
            if (!AllowFire || Controller.activeWeapon.Type != WeaponType.Ranged)
                return;


            AR_LockedOnTargetAimer.weight = 1;

            Controller.IsInAttack = true;

            ReferencesHolder.Instance._CameraController.ChangeCam(Cam.Aim);

            Controller.Anim.SetLayerWeight(1, 1);
            Controller.Anim.SetBool("isAiming", true);
            Controller.Anim.SetBool("isWalkRun", true);
            Controller.Anim.SetTrigger("Shoot");
            
            LookTowardCameraForward = true;

            StartCoroutine(RegulateFire());

            if(cancelAimRoutine != null)
                StopCoroutine(cancelAimRoutine);
            cancelAimRoutine = StartCoroutine(CancelAimAndAttack());
        }

        public void ShootArrow()
        {
            GetBulletIndex();
            Vector2 screenCenterPoint = new Vector2(Screen.width / 2, Screen.height / 2);
            DebugUITransform.position = screenCenterPoint;

            Ray ray = cam.ScreenPointToRay(screenCenterPoint);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 999f, AimableMask))
            {
                bulletsPool[bulletIndex].Shoot(FirePoint.position, hit.point, ActiveWeapon.BulletSpeed, ActiveWeapon.BulletDamage);
                DebugTransform.transform.position = hit.point;
            }

        }

        IEnumerator RegulateFire()
        {
            AllowFire = false;

            yield return new WaitForSeconds(ActiveWeapon.DelayInNextFire);
            AllowFire = true;
        }

        void GetBulletIndex()
        {
            bulletIndex++;
            if(bulletIndex == bulletsPool.Count)
            {
                bulletIndex = 0;
            }
        }

        void MakePool(Bullet bullet)
        {
            bulletsPool.Clear();
            bulletIndex = 0;

            for (int i = 0; i < MaxPoolSize; ++i)
            {
                var _bllt = Instantiate(bullet,null);
                
                _bllt.transform.parent = null; 

                _bllt.Initialize(this,i);

                bulletsPool.Add(_bllt);
            }
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