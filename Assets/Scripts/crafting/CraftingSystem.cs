using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingSystem : MonoBehaviour
{
    public List<Recipe> recipes;  // List of possible recipes
    public List<GameObject> go;
    //public GameObject sell;
    public GameObject result;
    public Inventory inv;

    private string[] stringArray = new string[2]; // Array to hold two strings
    private int currentIndex = 0; // Tracks which index to add a new string to

    // Method to add a new string to the array
    public void AddString(string newString)
    {
        //Image imm=sell.GetComponent<Image>();
        //imm.sprite = Resources.Load<Sprite>("sumPrefabs/goodImgs/" + newString);
        // Add the new string to the current index
        stringArray[currentIndex] = newString;
        currentIndex++;

        // If the index exceeds 1 (0 and 1 are the valid indices), reset it to 0
        if (currentIndex > 1)
        {
            currentIndex = 0;
        }

        Debug.Log("Updated array: [" + stringArray[0] + ", " + stringArray[1] + "]");
    }

    public void PrintAllElements()
    {
        //Debug.Log("Array elements:");
        for (int i = 0; i < stringArray.Length; i++)
        {
            Image im=go[i].GetComponent<Image>();
            im.sprite = Resources.Load<Sprite>("sumPrefabs/goodImgs/" + stringArray[i]);
            Debug.Log("Element " + i + ": " + stringArray[i]);
        }
        if(stringArray[1]!=null){
            CraftPreview();
        }
    }

    void Start()
    {
        //PlayerPrefs.DeleteAll();
        // Define a few recipes
        Recipe recipe1 = new Recipe("apple", "banana", "apple");
        Recipe recipe2 = new Recipe("apple", "apple", "apple");

        // Add the recipes to the list
        recipes.Add(recipe1);
        recipes.Add(recipe2);

        Debug.Log("Recipes list populated with " + recipes.Count + " recipes.");
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

    public void CraftPreview(){
            if(stringArray!=null){
            Craft(stringArray[0],stringArray[1]);
        }
    }

    public void OnCraftButtonClick(){
        //check item in result slot
        //add items to prefab
        //remove items in prefab
        //remove button images
        if(stringArray[1]!=null){
            inv.LoadInventory();
            inv.AddItem(result.GetComponent<Image>().sprite.name);
             for (int i = 0; i < stringArray.Length; i++)
            {
                inv.RemoveItem(stringArray[i]);
                go[i].GetComponent<Image>().sprite=null;
            }
            result.GetComponent<Image>().sprite=null; 
        }
        inv.LoadInventory();

    }

    // Craft function that takes two items
    public void Craft(string input1, string input2)
    {
        // Loop through all recipes to find a match
        foreach (Recipe recipe in recipes)
        {
            if (recipe.Matches(input1, input2))
            {
                Debug.Log("Crafting successful: " + recipe.result);
                
                Image im=result.GetComponent<Image>();
                im.sprite = Resources.Load<Sprite>("sumPrefabs/goodImgs/" + recipe.result);
                return;
            }
        }

        // No matching recipe
        Debug.Log("No recipe matches these items!");
        Image imf=result.GetComponent<Image>();
        imf.sprite = Resources.Load<Sprite>("sumPrefabs/goodImgs/" + "failed");
        
    }
}

[System.Serializable]
public class Recipe
{
    public string item1;
    public string item2;
    public string result;

    // Constructor
    public Recipe(string item1, string item2, string result)
    {
        this.item1 = item1;
        this.item2 = item2;
        this.result = result;
    }

    // Check if the given items match this recipe
    public bool Matches(string input1, string input2)
    {
        return (input1 == item1 && input2 == item2) || (input1 == item2 && input2 == item1);
    }
}
