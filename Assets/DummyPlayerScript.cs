using UnityEngine;

public class DummyPlayerScript : MonoBehaviour
{
    //void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Bullet")
    //    {
    //        Debug.Log("hua");
    //        collision.gameObject.GetComponent<PikamoonAi>().TakeDamage(20f); // Reduce Pikamoon's health
    //    }
    //}
    private void OnTriggerEnter(Collider other)
    {
        //print(other.gameObject.name);
        if (other.gameObject.tag == "Bullet")
        {
           // Debug.Log("hua");
            other.gameObject.GetComponent<PikamoonAi>().TakeDamage(10f); // Reduce Pikamoon's health
        }
    }
}
