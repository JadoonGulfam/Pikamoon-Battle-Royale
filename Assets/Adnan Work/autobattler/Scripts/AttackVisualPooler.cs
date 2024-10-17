using System.Collections.Generic;
using UnityEngine;
public class AttackVisualPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        [Tooltip("Unique identifier for the pool.")]
        public string tag;

        [Tooltip("Prefab to be instantiated and pooled.")]
        public GameObject prefab;

        [Tooltip("Initial size of the pool.")]
        public int size;
    }

    [Header("Pools Configuration")]
    [Tooltip("List of pools to manage different attack visuals.")]
    public List<Pool> pools;
    private Dictionary<string, Queue<GameObject>> poolDictionary;

    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject GetPooledObject(string tag)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning($"Pool with tag {tag} doesn't exist.");
            return null;
        }

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        objectToSpawn.SetActive(true);
        poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }
}
