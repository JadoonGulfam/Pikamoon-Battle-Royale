using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pikamoon.Controller
{
    public class ShootingManager : MonoBehaviour
    {
        RangedWeaponSO ActiveWeapon;

        public Transform FirePoint;

        [Space]
        [Header("Aim")]
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
        PlayerController playerController;
        PlayerInput playerinput;

        Coroutine cancelAimRoutine;

        private void Start()
        {
            playerController = GetComponent<PlayerController>();
            playerinput = ReferencesHolder.Instance._playerInput;
           
            if(playerController.activeWeapon.Type == WeaponType.Ranged)
            {
                Initialize(playerController.GetWeaponAs<RangedWeaponSO>());
            }

            playerinput.onAttack1_Clicked += PlayFireAnimation;
            playerinput.onAttack2_Down += OnZoomedAim;
            playerinput.onAttack2_Up += OnZoomedAimCancel;


            AllowFire = true;
        }

        private void Update()
        {
            if (!playerController.IsInAttack)
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
                Vector3 forward = cam.transform.right + (cam.transform.forward * Value);
                forward.y = 0f;

                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(forward), Time.deltaTime * 10);
            }
        }

        void OnZoomedAim()
        {
            if(ActiveWeapon.Type == WeaponType.Ranged)
            {
                ReferencesHolder.Instance._CameraController.ChangeAimZoom(true);
            }
        }
        void OnZoomedAimCancel()
        {
            if (ActiveWeapon.Type == WeaponType.Ranged)
            {
                ReferencesHolder.Instance._CameraController.ChangeAimZoom(false);
            }
        }


        IEnumerator CancelAimAndAttack()
        {
            yield return new WaitForSeconds(CancelAimAfterSeconds);

            playerController.Anim.SetBool("isAiming", false);
            ReferencesHolder.Instance._CameraController.ChangeCam(Cam.Default);
            playerController.IsInAttack = false;

            yield return new WaitForSeconds (CancelAttackAfterSeconds);

            playerController.Anim.SetBool("inCombat", false);
            playerController.Anim.SetLayerWeight(1, 0);



        }


        void MoveDuringAim()
        {

            Vector3 direction = playerController.GetDirectionAccordingToCameraWhenMoving();
            

            playerController.Anim.SetFloat("XVal", playerinput.Horizontal);
            playerController.Anim.SetFloat("YVal", playerinput.Vertical);


            // Always apply vertical velocity (for gravity or jumping)
            Vector3 finalMove = new Vector3(direction.x * playerController.Speed, playerinput.JumpVelocity, direction.z * playerController.Speed);

            // Move the character based on calculated velocity and speed
            playerController.Move(finalMove);
        }


        void EndAttack()
        {

        }

        public void PlayFireAnimation()
        {
            if (!AllowFire || playerController.activeWeapon.Type == WeaponType.Melee)
                return;

            playerController.IsInAttack = true;

            ReferencesHolder.Instance._CameraController.ChangeCam(Cam.Aim);

            playerController.Anim.SetLayerWeight(1, 1);
            playerController.Anim.SetBool("isAiming", true);
            playerController.Anim.SetBool("isWalkRun", true);
            playerController.Anim.SetTrigger("Shoot");
            
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
            playerinput.onAttack1_Clicked -= PlayFireAnimation;
            playerinput.onAttack2_Down -= OnZoomedAim;
            playerinput.onAttack2_Up -= OnZoomedAimCancel;
        }
    }
}