using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class poolManager : MonoBehaviour
{
    Dictionary<int, Queue<GameObject>> poolDictionary =
        new Dictionary<int, Queue<GameObject>>();

    static poolManager _instance;
    public  static poolManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<poolManager>();
            }
            return _instance;
        }
    }

    public void CreatePool(GameObject prefab,int poolSize)
    {
        if (prefab != null)
        {
            int poolKey = prefab.GetInstanceID();
            if (!poolDictionary.ContainsKey(poolKey))
            {
                poolDictionary.Add(poolKey, new Queue<GameObject>());
                for (int i = 0; i < poolSize; i++)
                {
                    try
                    {
                        GameObject newObject = Instantiate(prefab) as GameObject;
                        newObject.SetActive(false);
                        poolDictionary[poolKey].Enqueue(newObject);
                    }
                    catch (Exception e)
                    {
                        Debug.Log(e);
                    }

                }
            }
        }

    }
    public GameObject ReuseObject(GameObject prefab,Vector3 position,Quaternion rotation)
    {
        int poolKey = prefab.GetInstanceID();
        if (poolDictionary.ContainsKey(poolKey))
        {
            GameObject objectToReuse = poolDictionary[poolKey].Dequeue();
            poolDictionary[poolKey].Enqueue(objectToReuse);
            objectToReuse.SetActive(false);
            objectToReuse.SetActive(true);
            objectToReuse.transform.position = position;
            objectToReuse.transform.rotation = rotation;
            return objectToReuse;
        }
        else
        {
            return null;
        }

    }
}
