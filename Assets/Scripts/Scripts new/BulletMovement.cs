using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [Header("Bullet Properties")]
    public float speed = 20f; // Speed of the bullet
    public float lifetime = 5f; // Time before the bullet is destroyed
    private void Awake()
    {
        print("awiak");
    }
    private void OnEnable()
    {
        print("onenable");
    }
    void Start()
    {
        print("start");
        // Destroy the bullet after a certain lifetime to avoid cluttering the scene
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // Move the bullet forward based on its local transform
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
