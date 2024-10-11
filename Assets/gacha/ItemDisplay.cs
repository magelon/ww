using UnityEngine;
using TMPro;
using System.IO;

public class ItemDisplay : MonoBehaviour
{
    public string jsonFilePath = "Assets/Resources/items.json"; // Public variable to specify full path to JSON file
    public TextMeshProUGUI textDisplay; // Reference to the TextMeshProUGUI component

    void Start()
    {
        // Read the JSON data from the file
        string jsonData = ReadJsonFile(jsonFilePath);

        // Parse the JSON data
        ItemList itemList = JsonUtility.FromJson<ItemList>(jsonData);

        // Display item names and rates in the TextMeshProUGUI
        DisplayItems(itemList);
    }

    // Function to read the JSON file
    private string ReadJsonFile(string filePath)
    {
        // Read the file content
        if (File.Exists(filePath))
        {
            return File.ReadAllText(filePath);
        }
        else
        {
            Debug.LogError($"JSON file not found at path: {filePath}");
            return "{}"; // Return empty JSON object if file not found
        }
    }

    void DisplayItems(ItemList itemList)
    {
        string displayText = ""; // String to accumulate display text

        foreach (Item item in itemList.items)
        {
            // Append item name and rate to the display text
            displayText += $"Character Name: {item.itemsName}, Rate: {item.attributes.rate}\n";
        }

        // Set the accumulated text to the TextMeshProUGUI component
        textDisplay.text = displayText;
    }
}



[System.Serializable]
public class ItemList
{
    public Item[] items; // Array of items
}
