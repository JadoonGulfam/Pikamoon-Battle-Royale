using UnityEngine;

namespace Pikamoon.Controller
{
    public class Bullet : MonoBehaviour
    {
        ShootingManager shooter;
        [SerializeField] int IndexInManager;
        [Space]
        [SerializeField] Rigidbody rigidBody;
        [SerializeField] Collider collider;

        [Space]
        [Header("Particles")]
        [SerializeField] ParticleSystem MuzzleFlash;
        [SerializeField] ParticleSystem ProjectileFlash;
        [SerializeField] ParticleSystem HitParticle;

        IDamageable damageable;
        float damage;
        float speed;

        private void OnEnable()
        {
            HitParticle.transform.localPosition = Vector3.zero;
            HitParticle.transform.localRotation = Quaternion.identity;
        }



        private void OnDisable()
        {
            //HitParticle.gameObject?.SetActive(false);
            //MuzzleFlash.gameObject?.SetActive(false);
            //ProjectileFlash.gameObject?.SetActive(false);
        }

        public void Initialize(ShootingManager _shooter, int Index)
        {
            shooter = _shooter;
            IndexInManager = Index;

            if (MuzzleFlash)
            {
                MuzzleFlash.transform.localPosition = Vector3.zero;
                MuzzleFlash.transform.localRotation = Quaternion.identity;

                HitParticle.gameObject.SetActive(false);
            }

            HitParticle?.gameObject.SetActive(false);
            
            ProjectileFlash?.gameObject.SetActive(false);

        }

        public void Shoot(Vector3 spawnpoint, Vector3 AimPosition, float _speed, float Damage)
        {
            this.gameObject.SetActive(false);
            rigidBody.velocity = Vector3.zero;
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
            rigidBody.velocity = transform.forward * speed;
            collider.enabled = true;
        }

        private void OnCollisionEnter(Collision collision)
        {
            //ProjectileFlash?.gameObject.SetActive(false);

            //if (HitParticle)
            //{
            //    HitParticle.transform.parent = null;
            //    HitParticle.gameObject.SetActive(true);
            //}
            
            
            //damageable = collision.gameObject.GetComponent<IDamageable>();
            //if (damageable != null)
            //{
            //    damageable.OnDamage(damage);
            //}


            //collider.enabled = false;
        }
    }
}