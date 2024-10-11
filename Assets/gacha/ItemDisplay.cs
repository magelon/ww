using UnityEngine;
using TMPro;
using System.IO;

public class ItemDisplay : MonoBehaviour
{
    public string jsonFilePath;
    public TextMeshProUGUI textDisplay;

    void Start()
    {
        #if UNITY_EDITOR
            jsonFilePath = Path.Combine(Application.dataPath, "Scenes/GameLevel/cutLevels/levels/dataCharactors.json");
        #else
            jsonFilePath = Path.Combine(Application.streamingAssetsPath, "Scenes/GameLevel/cutLevels/levels/dataCharactors.json");
        #endif

        string jsonData = ReadJsonFile(jsonFilePath);

        if (!string.IsNullOrEmpty(jsonData))
        {
            ItemList itemList = JsonUtility.FromJson<ItemList>(jsonData);
            DisplayItems(itemList);
        }
        else
        {
            Debug.LogError("JSON data is empty or null.");
        }
    }

    private string ReadJsonFile(string filePath)
    {
        string jsonData = "{}";
        
        if (Application.platform == RuntimePlatform.Android)
        {
            UnityEngine.Networking.UnityWebRequest request = UnityEngine.Networking.UnityWebRequest.Get(filePath);
            request.SendWebRequest();

            while (!request.isDone) { }

            if (request.result == UnityEngine.Networking.UnityWebRequest.Result.Success)
            {
                jsonData = request.downloadHandler.text;
            }
            else
            {
                Debug.LogError($"Failed to load JSON file at path: {filePath}. Error: {request.error}");
            }
        }
        else
        {
            if (File.Exists(filePath))
            {
                jsonData = File.ReadAllText(filePath);
            }
            else
            {
                Debug.LogError($"JSON file not found at path: {filePath}");
            }
        }

        return jsonData;
    }

    void DisplayItems(ItemList itemList)
    {
        string displayText = "";

        foreach (Item item in itemList.items)
        {
            displayText += $"Character Name: {item.itemsName}, Rate: {item.attributes.rate}\n";
        }

        textDisplay.text = displayText;
    }
}

[System.Serializable]
public class ItemList
{
    public Item[] items;
}
