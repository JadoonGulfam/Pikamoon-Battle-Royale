using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AquariusMax.Demo {
public class Portal : MonoBehaviour {



    [SerializeField]
    public Transform PortalEndPoint;

    public void OnTriggerEnter(Collider other)
    {
		other.gameObject.transform.eulerAngles = new Vector3 (
			other.gameObject.transform.eulerAngles.x,
			other.gameObject.transform.eulerAngles.y + 180,
			other.gameObject.transform.eulerAngles.z);
        other.gameObject.transform.position = PortalEndPoint.position;
	//	Debug.Log("Rotation X: " + other.gameObject.transform.rotation.x + "Rotation Y: " + other.gameObject.transform.rotation.y + "Rotation Z: " + other.gameObject.transform.rotation.z);
//		other.gameObject.transform.Rotate (0, 180, 0, Space.World);
		//Debug.Log("Rotation X: " + other.gameObject.transform.rotation.x + "Rotation Y: " + other.gameObject.transform.rotation.y + "Rotation Z: " + other.gameObject.transform.rotation.z);
    }

}
}