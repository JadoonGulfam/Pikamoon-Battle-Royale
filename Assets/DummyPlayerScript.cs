using UnityEngine;

public class DummyPlayerScript : MonoBehaviour
{
    public Transform player;
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
        if (other.gameObject.tag == "Pikamoon")
        {
           // Debug.Log("hua");
            other.gameObject.GetComponent<PikamoonAi>().TakeDamage(10f, player); // Reduce Pikamoon's health
        }
    }
    //[SerializeField] private LayerMask Pikamoon; // Assign this in the Inspector

    //private void OnTriggerEnter(Collider other)
    //{
    //    Debug.Log("1");
    //    Debug.Log(IsInLayerMask(other.gameObject, Pikamoon));
    //    if (IsInLayerMask(other.gameObject, Pikamoon)) // Check if the collided object is in the Pikamoon layer
    //    {
    //        Debug.Log("2");
    //        PikamoonAi pikamoon = other.gameObject.GetComponent<PikamoonAi>();
    //        if (pikamoon != null)
    //        {
    //            pikamoon.TakeDamage(10f); // Reduce Pikamoon's health
    //        }
    //    }
    //}
    //bool IsInLayerMask(GameObject obj, LayerMask layerMask)
    //{
    //    Debug.Log(layerMask.value + "123" + obj.layer);
    //    return ((1 << obj.layer) & layerMask.value) != 0;
    //}
}
