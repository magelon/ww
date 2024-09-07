using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class summer : MonoBehaviour
{
    // The GameObject to spawn
    public GameObject objectToSpawn;
    
    // The interval between spawns
    public float spawnInterval = 5f;
    
    // The spawn position (optional)
    public Transform spawnPoint;

    private GameObject white;

    private string objectName;
    // Start is called before the first frame update
    void Start()
    {
        // Start the spawn coroutine
        objectName = GetGameObjectName(objectToSpawn);
        StartCoroutine(SpawnObject());
    }

    IEnumerator SpawnObject()
    {
        // Infinite loop to keep spawning objects
        while (true)
        {
            // Wait for the specified interval
            yield return new WaitForSeconds(spawnInterval);

            // Spawn the object at the specified position, or at the spawner's position if none is given
            if (spawnPoint != null)
            {
                white = Resources.Load<GameObject>("rightPrefabs/" + objectName);
                white.GetComponent<Enime>().f=Factions.yellow;
                poolManager.instance.CreatePool(white, 15);
                poolManager.instance.ReuseObject(white, 
                new Vector3(spawnPoint.position.x, spawnPoint.position.y + 0.2f, 0), Quaternion.identity);
            }
            else
            {
                white = Resources.Load<GameObject>("rightPrefabs/" + objectName);
                white.GetComponent<Enime>().f=Factions.yellow;
                poolManager.instance.CreatePool(white, 15);
                poolManager.instance.ReuseObject(white, 
                new Vector3(transform.position.x, transform.position.y + 0.2f, 0), Quaternion.identity);
            }
        }
    }

    // Function to get the name of the GameObject
    public string GetGameObjectName(GameObject obj)
    {
        return obj.name;
    }
}
