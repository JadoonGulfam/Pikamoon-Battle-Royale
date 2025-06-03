using System.Collections;
using UnityEngine;

namespace Pikamoon.Controller
{
    public class Bullet : MonoBehaviour
    {
        //Shooting shooter;
        Weapon RootWeapon;

        [SerializeField] int IndexInManager;
        [Space]
        [SerializeField] Rigidbody rigidBody;
        [SerializeField] Collider _collider;
        [SerializeField] float disableAfter;

        [Space]
        [Header("Particles")]
        [SerializeField] ParticleSystem MuzzleFlash;
        [SerializeField] ParticleSystem ProjectileFlash;
        [SerializeField] ParticleSystem HitParticle;

        public Transform ActionCamParent;

        Coroutine bulletRoutine;

        IDamageable damageable;
        float damage;
        float speed;
        bool isShooted;

        private void OnEnable()
        {
            HitParticle.transform.parent = transform;
            HitParticle.transform.localPosition = Vector3.zero;
            HitParticle.transform.localRotation = Quaternion.identity;

        }



        private void OnDisable()
        {
            //HitParticle.gameObject?.SetActive(false);
            //MuzzleFlash.gameObject?.SetActive(false);
            //ProjectileFlash.gameObject?.SetActive(false);
        }

        //public void Initialize(Shooting _shooter, int Index)
        //{
        //    this.transform.localPosition = Vector3.zero;

        //    shooter = _shooter;
        //    IndexInManager = Index;

        //    if (MuzzleFlash)
        //    {
        //        MuzzleFlash.transform.localPosition = Vector3.zero;
        //        MuzzleFlash.transform.localRotation = Quaternion.identity;

        //        HitParticle.gameObject.SetActive(false);
        //    }

        //    HitParticle?.gameObject.SetActive(false);

        //    ProjectileFlash?.gameObject.SetActive(false);

        //}

        public void Initialize(Weapon _shooter, int Index)
        {
            //this.transform.localPosition = Vector3.zero;
            RootWeapon = _shooter;

            _collider.enabled = false;

            RootWeapon = _shooter;
            IndexInManager = Index;

            if (MuzzleFlash)
            {
                MuzzleFlash.transform.localPosition = Vector3.zero;
                MuzzleFlash.transform.localRotation = Quaternion.identity;

                HitParticle.gameObject.SetActive(false);
            }

            HitParticle?.gameObject.SetActive(false);

            ProjectileFlash?.gameObject.SetActive(false);

            this.gameObject.SetActive(false);
        }

        public void Shoot(Vector3 spawnpoint, Vector3 AimPosition, float _speed, float Damage)
        {
            gameObject.SetActive(false);
            transform.position = spawnpoint;
            rigidBody.isKinematic = true;
            

            damage = Damage;
            speed = _speed;

            transform.rotation = Quaternion.LookRotation((AimPosition - spawnpoint).normalized,Vector3.up);
            //transform.LookAt((AimPosition - spawnpoint).normalized,transform.up);

            if(HitParticle)
            {
                HitParticle.gameObject.SetActive(false);

                HitParticle.transform.localPosition = Vector3.zero;
                HitParticle.transform.localRotation = Quaternion.identity;
            }

            if (MuzzleFlash)
            {
                MuzzleFlash.transform.localPosition = Vector3.zero;
                MuzzleFlash.transform.localRotation = Quaternion.identity;

                MuzzleFlash.gameObject.SetActive(true);
            }

            if (ProjectileFlash)
            {
                ProjectileFlash.gameObject.SetActive(true);
            }

            rigidBody.isKinematic = false;

            this.gameObject.SetActive(_speed > 0);
            rigidBody.linearVelocity = transform.forward * speed;
            _collider.enabled = true;

            if(bulletRoutine != null)
                StopCoroutine(bulletRoutine);
            bulletRoutine = StartCoroutine(Disabler());

        }

        IEnumerator Disabler()
        {
            yield return new WaitForSeconds(disableAfter);
            this.gameObject.SetActive(false);
        }

        //private void OnCollisionEnter(Collision collision)
        //{
        //    ProjectileFlash?.gameObject.SetActive(false);

        //    if (HitParticle)
        //    {
        //        HitParticle.transform.parent = null;
        //        HitParticle.gameObject.SetActive(true);
        //    }


        //    damageable = collision.gameObject.GetComponent<IDamageable>();
        //    if (damageable != null)
        //    {
        //        damageable.OnDamage(damage);
        //    }
            
        //    rigidBody.velocity = Vector3.zero;
        //    rigidBody.isKinematic = true;

        //    transform.position = transform.position + transform.forward.normalized;

        //    collider.enabled = false;
        //}

        private void OnTriggerEnter(Collider other)
        {
            //ProjectileFlash?.gameObject.SetActive(false);


            if (HitParticle)
            {
                HitParticle.transform.parent = null;
                HitParticle.gameObject.SetActive(true);
            }

            RootWeapon.Holder._cameraController.DisableBulletActionCam();

            damageable = other.gameObject.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.OnDamage(damage,this.transform);
            }

            rigidBody.linearVelocity = Vector3.zero;
            rigidBody.isKinematic = true;

            transform.position = transform.position + transform.forward.normalized;

            _collider.enabled = false;
        }

        private void OnDestroy()
        {
            if (bulletRoutine != null)
                StopCoroutine(bulletRoutine);
        }
    }
}