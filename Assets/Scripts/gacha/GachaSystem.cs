using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;
using System.IO;

[System.Serializable]
public class Attributes2
{
    public int hp;
    public int atk;
    public int mess;
    public float speed;
    public float energy;
    public int force;
    public int rate;
    public string element;
    public string art;
}

[System.Serializable]
public class Item2
{
    public string itemsName;
    public Attributes2 attributes;
}

[System.Serializable]
public class ItemsData2
{
    public List<Item2> items;
}

public class GachaSystem : MonoBehaviour
{
    private static System.Random random = new System.Random();
    private int resultCounter = 0;

    public TextAsset jsonFile;
    public TextAsset jsonFile2;
    private List<Item2> loadedItems;
    private List<Item2> simplifiedItemListRate3;
    private List<Item2> simplifiedItemListRate5;
    public GachaMenu gachamenu;

    public string GetGachaResult()
    {
        if(gachamenu.page==0){
            if(jsonFile != null){

            string jsonText = jsonFile.text;
            ItemsData2 itemsData = JsonUtility.FromJson<ItemsData2>(jsonText);
            loadedItems = itemsData.items;
           
            simplifiedItemListRate3 = new List<Item2>();
            simplifiedItemListRate5 = new List<Item2>();
            //Extract only itemsName and rate
            foreach (Item2 item in loadedItems)
            {
                Item2 item2gacha = new Item2
                {
                    itemsName = item.itemsName,
                    attributes = new Attributes2
                    {
                        rate = item.attributes.rate
                    }
                };

                 if (item2gacha.attributes.rate == 3)
                {
                    simplifiedItemListRate3.Add(item2gacha);
                }
                else if (item2gacha.attributes.rate == 5)
                {
                    simplifiedItemListRate5.Add(item2gacha);
                }
            }
        }
        else
        {
            Debug.LogError("No JSON file assigned.");
        }
        }else{
            if(jsonFile2 != null){
            string jsonText = jsonFile2.text;
            ItemsData2 itemsData = JsonUtility.FromJson<ItemsData2>(jsonText);
            loadedItems = itemsData.items;
            simplifiedItemListRate3 = new List<Item2>();
            simplifiedItemListRate5 = new List<Item2>();
            //Extract only itemsName and rate
            foreach (Item2 item in loadedItems)
            {
                Item2 item2gacha = new Item2
                {
                    itemsName = item.itemsName,
                    attributes = new Attributes2
                    {
                        rate = item.attributes.rate
                    }
                };
                 if (item2gacha.attributes.rate == 3)
                {
                    simplifiedItemListRate3.Add(item2gacha);
                }
                else if (item2gacha.attributes.rate == 5)
                {
                    simplifiedItemListRate5.Add(item2gacha);
                }
            }
        }
        else
        {
            Debug.LogError("No JSON file assigned.");
        }
        }
        
        double randomNumber = random.NextDouble(); // Generate a random number between 0 and 1
        resultCounter = PlayerPrefs.GetInt("ResultCounter", 0);
        Debug.Log(resultCounter);

        if((randomNumber < 0.000014 || (resultCounter % 60 == 0 && resultCounter != 0)))
        {
            resultCounter++;
            PlayerPrefs.SetInt("ResultCounter", resultCounter);
            // Get a random index within the range of the list
            int randomIndex = random.Next(0, simplifiedItemListRate5.Count);

            // Get the random item
            Item2 randomItem = simplifiedItemListRate5[randomIndex];

            // Print the result to the console
            Debug.Log("Random Item from Rate 5: " + randomItem.itemsName);
            SaveGachaResult(randomItem.itemsName);
            return randomItem.itemsName;
        }
        else
        {
            // 99.986% chance for 0, 1, 2, 3, 4, 5
            resultCounter++;
            PlayerPrefs.SetInt("ResultCounter", resultCounter);

            // Get a random index within the range of the list
            int randomIndex2 = random.Next(0, simplifiedItemListRate3.Count);

            // Get the random item
            Item2 randomItem2 = simplifiedItemListRate3[randomIndex2];

            // Print the result to the console
            Debug.Log("Random Item from Rate 3: " + randomItem2.itemsName);
            SaveGachaResult(randomItem2.itemsName);
            return randomItem2.itemsName;
        }
    }

    void SaveGachaResult(string result)
    {
    // Format the result with date and time for better tracking
    string formattedResult = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " - " + result;

    // Use resultCounter as the key to store the result in PlayerPrefs
    string key = "GachaResult_" + resultCounter;
    PlayerPrefs.SetString(key, formattedResult);

    // Save the result in PlayerPrefs
    PlayerPrefs.Save();

    // Log the result for debugging
    Debug.Log("Saved Gacha Result: " + formattedResult + " with key: " + key);
    }
}
