using UnityEngine;
using TMPro;
using System.IO;
using System.Collections.Generic;

public class ItemDisplay : MonoBehaviour
{
    public TextAsset jsonFile;
    public TextMeshProUGUI textDisplay;
    private List<Item2> loadedItems;
    
    void Start()
    {
        if(jsonFile != null){

            string jsonText = jsonFile.text;
            ItemsData2 itemsData = JsonUtility.FromJson<ItemsData2>(jsonText);
            loadedItems = itemsData.items;
            DisplayItems();
        }
    }

 
    void DisplayItems()
    {
        string displayText = "";

        foreach (Item2 item in loadedItems)
        {
            displayText += $"Character Name: {item.itemsName}, Rate: {item.attributes.rate}\n";
        }

        textDisplay.text = displayText;
    }
}


