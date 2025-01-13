using DG.Tweening;
using UnityEngine;

namespace Pikamoon.Controller
{
    public class ThrowableWeapon : Weapon
    {
        [Space]
        [Header("Core Class Value")]

        public Rigidbody rb;
        public float rotationSpeed;

        [Space]
        [Header("Holder Data")]
        public Throwing player;
        public Transform CurveT;

        [Header("VFX")]
        public ParticleSystem ThrowFlash_PSys;
        public ParticleSystem Catch_PSys;
        public ParticleSystem Glow_PSys;
        public ParticleSystem TrailPSys;
        public TrailRenderer TrailTRen;

        bool isReturning;
        bool activated;

        Vector3 _distantPoint;
        Transform _curvePoint;
        Transform _throwingOrigin;

        float returnTime;

        Throwing throwing;
        IDamageable damageable;

        ThrowableWeaponDataSO tWeaponData;
        private void Start()
        {

            tWeaponData = GetWeaponDataAs<ThrowableWeaponDataSO>();

            returnTime = 0;
            activated = isReturning = false;
        }

        void Update()
        {
            if (activated)
            {
                transform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime, Space.Self);
            }


            if (isReturning)
            {
                if (returnTime < 1)
                {
                    this.transform.position = GetQuadraticCurvePoint(returnTime, _distantPoint, _curvePoint.position, _throwingOrigin.position);
                    returnTime += Time.deltaTime * 1.5f;
                }
                else
                {
                    ReturnedSuccessfully();
                }
            }

        }



        public void Throw(Throwing _thrower,Transform defaultHoldingPos,Vector3 TargetPos)
        {
            isReturning = false;
            activated = true;

            returnTime = 0;

            throwing = _thrower;
            _throwingOrigin = defaultHoldingPos;

            transform.rotation = Quaternion.LookRotation((TargetPos - _throwingOrigin.position).normalized, Vector3.up);

            rb.isKinematic = false;
            rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

            transform.parent = null;
            //transform.eulerAngles = new Vector3(0, -90 + transform.eulerAngles.y, 0);
            //transform.transform.position += transform.right / 5;

            rb.AddForce(transform.forward * tWeaponData.Power + transform.up * 2, ForceMode.Impulse);

            //Trail
            TrailTRen.emitting = true;
            TrailPSys.Play();
        }

        public void CallItBack(Transform CurvePoint)
        {
            _curvePoint = CurvePoint;
            _distantPoint = this.transform.position;
            activated = true;

            rb.Sleep();
            rb.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
            rb.isKinematic = true;
            transform.DORotate(new Vector3(-90, -90, 0), .2f).SetEase(Ease.InOutSine);
            transform.DOBlendableLocalRotateBy(Vector3.right * 90, .5f);
            isReturning = true;
        }

        public void ReturnedSuccessfully()
        {
            isReturning = false;
            activated = false;

            throwing.WeaponCatchSuccessfully();
        }

        public Vector3 GetQuadraticCurvePoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
        {
            float u = 1 - t;
            float tt = t * t;
            float uu = u * u;
            return (uu * p0) + (2 * u * t * p1) + (tt * p2);
        }

        private void OnTriggerEnter(Collider other)
        {
            //if (collision.gameObject.layer == 11)
            //{
            activated = false;
            GetComponent<Rigidbody>().Sleep();
            GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
            GetComponent<Rigidbody>().isKinematic = true;

            damageable = other.gameObject.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.OnDamage(tWeaponData.Damage, this.transform);
            }

            //}
        }


        public override void OnPicked()
        {

        }

        public override void OnPicked(Transform Picker)
        {
        }

        public override void Drop()
        {
        }

        public override WeaponInfo GetWeaponInfo()
        {
            WeaponInfo info = new WeaponInfo();

            info.Prefab = this;
            info.Data = tWeaponData;

            return info;
        }
    }
}