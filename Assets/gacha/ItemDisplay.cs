using UnityEngine;
using TMPro;
using System.IO;
using System.Collections.Generic;

public class ItemDisplay : MonoBehaviour
{
    public TextAsset jsonFile;
    public TextAsset jsonFile2;
    public TextMeshProUGUI textDisplay;
    private List<Item2> loadedItems;
    public GachaMenu gm;

    private string jsonText2;
    private string jsonText;
    private ItemsData2 itemsData2;
    private ItemsData2 itemsData;

    void Start(){
        if(jsonFile2 != null){
             jsonText2 = jsonFile2.text;
             itemsData2 = JsonUtility.FromJson<ItemsData2>(jsonText2);
            }
        if(jsonFile != null){
             jsonText = jsonFile.text;
             itemsData = JsonUtility.FromJson<ItemsData2>(jsonText);
        }   
    }

    void Update()
    {  
        if(gm.page!=0){ 
            loadedItems = itemsData2.items;
            DisplayItems();
        }
        else{
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


