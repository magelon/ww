using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

[System.Serializable]
public class ItemData
{
    public string item1;
    public int wave1;
    public string item2;
    public int wave2;
    public string item3;
    public int wave3;
}

[System.Serializable]
public class LevelData
{
    public string levelName;
    public ItemData items;
}

[System.Serializable]
public class LevelsData
{
    public List<LevelData> levels;
}

public class SpawnInstansi : MonoBehaviour
{
    public GameObject spawnButton;
    public GameObject spawnManagerT;
    public TextAsset jsonDataFile;
    // Reference to the JSON file in Unity's Assets folder.
    // Start is called before the first frame update
    void Start()
    { 
        if (jsonDataFile != null)
        {
            LevelsData levelsData = JsonUtility.FromJson<LevelsData>(jsonDataFile.text);

            foreach (LevelData level in levelsData.levels)
            {
                Debug.Log("Level Name: " + level.levelName);
                // Get the name of the currently active scene
                string currentSceneName = SceneManager.GetActiveScene().name;
                if(level.levelName==currentSceneName){
                    ItemData item= level.items;
                    
                    // Load the GameObject from the Resources folder
                    GameObject loadedObject1 = Resources.Load<GameObject>("leftPrefabs/"+item.item1);
                    spawnButton.GetComponent<FoeSpawner>().yellow=loadedObject1;
                    spawnButton.GetComponent<FoeSpawner>().timebetweenWave=item.wave1;

                    
                    // Load the GameObject from the Resources folder
                    GameObject loadedObject2 = Resources.Load<GameObject>("leftPrefabs/"+item.item2);
                    GameObject tbtn = Instantiate (spawnButton, Vector3.zero, Quaternion.identity) as GameObject;
                    tbtn.transform.SetParent(spawnManagerT.transform);
                    tbtn.GetComponent<FoeSpawner>().yellow=loadedObject2;
                    tbtn.GetComponent<FoeSpawner>().timebetweenWave=item.wave2;

                    
                    // Load the GameObject from the Resources folder
                    GameObject loadedObject3 = Resources.Load<GameObject>("leftPrefabs/"+item.item3);
                    GameObject tbtn2 = Instantiate (spawnButton, Vector3.zero, Quaternion.identity) as GameObject;
                    tbtn2.transform.SetParent(spawnManagerT.transform);
                    tbtn2.GetComponent<FoeSpawner>().yellow=loadedObject3;
                    tbtn2.GetComponent<FoeSpawner>().timebetweenWave=item.wave3;
                }
                
            }
        }
        else
        {
            Debug.LogError("JSON data file is missing or not assigned.");
        } 
    }

   

}
