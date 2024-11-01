using Unity.Cinemachine;
using System.Collections;
using UnityEngine;
public class CameraScrollZoom : MonoBehaviour
{
    public float minDistance = 4f;
    public float maxDistance = 15f;
    public float sensitivity = 10f;

    private CinemachineFreeLook setDistance;
    IEnumerator Start()
    {
        yield return new WaitForSeconds(1.0f);
        setDistance = GetComponent<CinemachineFreeLook>();
    }

    void Update()
    {
        if (setDistance != null)
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel") * sensitivity;
            for (int i = 0; i < setDistance.m_Orbits.Length; i++)
            {
                setDistance.m_Orbits[i].m_Radius = Mathf.Clamp(setDistance.m_Orbits[i].m_Radius - scroll, minDistance, maxDistance);
            }
        }
    }
}
