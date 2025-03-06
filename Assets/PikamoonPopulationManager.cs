using System.Collections.Generic;
using UnityEngine;



public class PikamoonPopulationManager : MonoBehaviour
{
    [System.Serializable]
    public struct Pikas
    {
        public GameObject Pikam;
        public bool isActive;
    }

    [System.Serializable]
    public struct PikaPool
    {
        public string PikaName;
        public Pikas[] Pikam;
    }



    [Header("Nearby Pikamoon Feature")]
    public PikaPool[] pikas;  // 3 types of Pikas

    public Transform player; // Assign player reference in the inspector
    public int InnerRange = 10;
    public int OuterRange = 50;
    public int HideDistance = 100;
    public int MaxPikasPerType = 10; // Ensures equal count per type

    private Dictionary<string, List<GameObject>> activePikas = new Dictionary<string, List<GameObject>>();

    void Start()
    {
        InitializePikas();
    }

    void Update()
    {
        ManagePikas();
    }

    // Initializes the Pikas and distributes them equally
    void InitializePikas()
    {
        foreach (var pool in pikas)
        {
            activePikas[pool.PikaName] = new List<GameObject>(); // Create separate list for each type

            int count = 0;
            foreach (var pika in pool.Pikam)
            {
                if (count >= MaxPikasPerType) break; // Limit to MaxPikasPerType

                if (pika.Pikam != null)
                {
                    pika.Pikam.SetActive(false);
                    activePikas[pool.PikaName].Add(pika.Pikam);
                    RepositionPika(pika.Pikam, pool.PikaName);
                    count++;
                }
            }
        }
    }

    // Checks for Pikas exceeding HideDistance and repositions them
    void ManagePikas()
    {
        foreach (var pool in pikas)
        {
            List<GameObject> pikaList = activePikas[pool.PikaName];

            for (int i = 0; i < pikaList.Count; i++)
            {
                if (pikaList[i] == null) continue;

                float distance = Vector3.Distance(player.position, pikaList[i].transform.position);

                if (distance > HideDistance)
                {
                    pikaList[i].SetActive(false);
                    RepositionPika(pikaList[i], pool.PikaName);
                }
            }
        }
    }

    // Repositions a specific Pika ensuring balanced distribution
    void RepositionPika(GameObject pika, string pikaType)
    {
        Vector3 newPosition = GetRandomPosition();
        pika.transform.position = newPosition;
        pika.SetActive(true);
    }

    // Gets a new random position within the Inner and Outer Range
    Vector3 GetRandomPosition()
    {
        Vector3 randomOffset = Random.insideUnitSphere * OuterRange;
        randomOffset.y = 0; // Keep it on the X-Z plane
        Vector3 newPos = player.position + randomOffset;

        if (Vector3.Distance(newPos, player.position) < InnerRange)
        {
            newPos += randomOffset.normalized * InnerRange;
        }

        newPos.y = GetTerrainHeight(newPos);
        return newPos;
    }

    // Adjusts the Pika position to match the terrain height
    float GetTerrainHeight(Vector3 position)
    {
        RaycastHit hit;
        if (Physics.Raycast(new Vector3(position.x, 100, position.z), Vector3.down, out hit, Mathf.Infinity))
        {
            return hit.point.y;
        }
        return position.y; // Default to same height if no terrain is detected
    }
}
