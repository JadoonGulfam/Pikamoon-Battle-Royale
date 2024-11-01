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

        [Space]
        [Header("Bullet")]
        public List<Bullet> bulletsPool;
        public int MaxPoolSize;

        [Space]
        [SerializeField] Transform DebugTransform;
        [SerializeField] RectTransform DebugUITransform;

        bool AllowFire;
        int bulletIndex;
        Camera cam;
        PlayerController playerController;
        PlayerInput playerinput;

        private void Start()
        {
            playerController = GetComponent<PlayerController>();
            playerinput = ReferencesHolder.Instance._playerInput;
           
            if(playerController.activeWeapon.Type == WeaponType.Ranged)
            {
                Initialize(playerController.GetWeaponAs<RangedWeaponSO>());
            }

            playerinput.onAttack1_Clicked += Fire;
            playerinput.onAttack2_Clicked += ToggleAim;
            AllowFire = true;
        }

        public void Initialize(RangedWeaponSO rangedWeapon)
        {
            bulletIndex = 0;
            ActiveWeapon = rangedWeapon;
            MakePool(ActiveWeapon.Bullet);

            cam = ReferencesHolder.Instance._Camera;
        }

        void ToggleAim()
        {
            if (playerController.activeWeapon.Type == WeaponType.Melee)
                return;
        }
        

        public void Fire()
        {
            if (!AllowFire || playerController.activeWeapon.Type == WeaponType.Melee)
                return;

            Vector2 screenCenterPoint = new Vector2 (Screen.width/2, Screen.height/2);  
            DebugUITransform.position = screenCenterPoint;

            Ray ray = cam.ScreenPointToRay(screenCenterPoint);
            
            RaycastHit hit;
            if(Physics.Raycast(ray,out hit, 999f, AimableMask))
            {
                bulletsPool[bulletIndex].Shoot(FirePoint.position, hit.point, ActiveWeapon.BulletSpeed, ActiveWeapon.BulletDamage);
                DebugTransform.transform.position = hit.point;
            }



            StartCoroutine(RegulateFire());
        }

        IEnumerator RegulateFire()
        {
            AllowFire = false;

            GetBulletIndex();
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
            playerinput.onAttack1_Clicked -= Fire;
            playerinput.onAttack2_Clicked -= ToggleAim;
        }
    }
}