using UnityEngine;

public class ThrowableWeapon : MonoBehaviour
{
    public bool activated;
    public Rigidbody rb;
    public float rotationSpeed;

    [Space]
    [Header("PlayerRelatedData")]
    public Transform playerWeaponHolderT;
    public Transform CurveT;

    [Space]
    [Header("VFX")]
    public ParticleSystem StartPSys;
    public ParticleSystem HitPSys;
    public ParticleSystem PSys;
    public ParticleSystem TrailPSys;
    public TrailRenderer TrailTRen;


    void Update()
    {
        if (activated)
        {
            transform.localEulerAngles += Vector3.forward * rotationSpeed * Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 11)
        {
            print(collision.gameObject.name);
            GetComponent<Rigidbody>().Sleep();
            GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
            GetComponent<Rigidbody>().isKinematic = true;
            activated = false;
        }
    }

    public void Throw()
    {

    }

    public void Return()
    {

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

    }
}
