using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance { get; private set; }

    // Dictionary to hold multiple queues for different prefabs
    private Dictionary<string, Queue<GameObject>> poolDictionary;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        poolDictionary = new Dictionary<string, Queue<GameObject>>();
    }

    public void CreatePool(GameObject prefab, int poolSize)
    {
        string poolKey = prefab.name;

        if (!poolDictionary.ContainsKey(poolKey))
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < poolSize; i++)
            {
                GameObject obj = Instantiate(prefab, transform);
                obj.name = prefab.name; // Keep name clean for keys
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            poolDictionary.Add(poolKey, objectPool);
        }
    }

    public GameObject GetObject(GameObject prefab)
    {
        string poolKey = prefab.name;

        if (poolDictionary.ContainsKey(poolKey) && poolDictionary[poolKey].Count > 0)
        {
            GameObject obj = poolDictionary[poolKey].Dequeue();
            poolDictionary[poolKey].Enqueue(obj); // Put it back at the end of the line
            
            // Only return it if it's currently inactive. Otherwise, instantiate a new one to prevent stealing.
            if (!obj.activeInHierarchy)
            {
                return obj;
            }
        }

        // Fallback if pool is too small
        GameObject newObj = Instantiate(prefab, transform);
        newObj.name = prefab.name;
        poolDictionary[poolKey].Enqueue(newObj);
        return newObj;
    }
}