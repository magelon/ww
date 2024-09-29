using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

[System.Serializable]
public class FooddetailData
{
    public int price;
    public int rate;
    public string art;
}

[System.Serializable]
public class FoodData
{
    public string foodName;
    public FooddetailData foodsdetail;
}

[System.Serializable]
public class FoodsData
{
    public List<FoodData> foods;
}

public class foodstandsystem : MonoBehaviour
{
    public TextAsset jsonDataFile;
    public List<GameObject> go;
    public FoodsData foodsData;
    public Inventory inv;
    public InventoryUI invui;

    private List<string> stringList = new List<string>(); 

     void Start()
    { 
        if (jsonDataFile != null)
        {
            foodsData = JsonUtility.FromJson<FoodsData>(jsonDataFile.text);
           
        }
    }

    //void Updated(){PrintAllElements();}

    // Method to add a new string to the array
    public void AddString(string newString)
    {
        if (stringList.Count >= 5) // Limit to 5 elements
        {
            stringList.RemoveAt(0); // Remove the oldest element
        }
        stringList.Add(newString); // Add the new string

        Debug.Log("Updated list: [" + string.Join(", ", stringList) + "]");
    }

    public void PrintAllElements()
    {
        for (int i = 0; i < 5; i++)
    {
    if (i < go.Count) // Ensure we do not exceed the number of GameObjects
    {
        Image im = go[i].GetComponent<Image>();

        // Clear the sprite first
        im.sprite = null;

        if (i < stringList.Count) // Check if there's a corresponding string in stringList
        {
            // Attempt to load the sprite
            Sprite loadedSprite = Resources.Load<Sprite>("sumPrefabs/goodImgs/" + stringList[i]);

            if (loadedSprite != null)
            {
                im.sprite = loadedSprite; // Set the sprite if found
            }
            else
            {
                Debug.LogWarning("Sprite not found for: " + stringList[i]);
            }
        }
        else
        {
            Debug.LogWarning("No string available for slot " + i);
        }
    }
    }

    }

    public void itemsold()
    {
        List<int> indicesToRemove = new List<int>(); 
        for (int i = 0; i < go.Count && i < stringList.Count; i++)
        {
            Image imageComponent = go[i].GetComponent<Image>();

            if (imageComponent != null && imageComponent.sprite != null)
            {
                // Get the name of the sprite currently in the Image component
                string imageNamee = imageComponent.sprite.name;

                Debug.Log("Image name: " + imageNamee);

                // Find the corresponding FoodData by matching the imageName
                FoodData soldFood = foodsData.foods.Find(food => food.foodName == imageNamee);

                if (soldFood != null)
                {
                    Debug.Log("Food sold: " + soldFood.foodName + ", Price: " + soldFood.foodsdetail.price);
                    int co=PlayerPrefs.GetInt("coin");
                    co+=soldFood.foodsdetail.price;
                    //Debug.Log(co);
                    PlayerPrefs.SetInt("coin",co);
                    inv.RemoveItem(soldFood.foodName);
                    if(invui){
                        invui.UpdateUI();
                    }
                    imageComponent.sprite=null;
                    indicesToRemove.Add(i);
                }
                else
                {
                    Debug.LogError("Food not found for image: " + imageNamee);
                }
            }
            else
            {
                Debug.LogWarning("No sprite found for GameObject at index: " + i);
            }
            for (int j = indicesToRemove.Count - 1; j >= 0; j--)
            {
                int index = indicesToRemove[j];
                if (index < stringList.Count) // Ensure index is within range
            {
                stringList.RemoveAt(index);
            }
            }
            stringList.Sort();
            PrintAllElements();
            Debug.Log("Sorted stringList: [" + string.Join(", ", stringList) + "]");
        }
    }

    public void OnButtonClick(Button clickedButton)
    {
         Image buttonImage = clickedButton.GetComponent<Image>();

        if (buttonImage != null && buttonImage.sprite != null)
        {
            // Get the name of the sprite used in the button's Image component
            string imageName = buttonImage.sprite.name;
            AddString(imageName);
            PrintAllElements();
        }
        else
        {
            Debug.Log("No image found on the button.");
        }
    }

    
}

