using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class foodstandsystem : MonoBehaviour
{
    //public List<Recipe> recipes;  // List of possible recipes
    public List<GameObject> go;

    private string[] stringArray = new string[5]; // Array to hold two strings
    private int currentIndex = 0; // Tracks which index to add a new string to

    // Method to add a new string to the array
    public void AddString(string newString)
    {
        // Add the new string to the current index
        stringArray[currentIndex] = newString;
        currentIndex++;

        // If the index exceeds 1 (0 and 1 are the valid indices), reset it to 0
        if (currentIndex > 4)
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
            //Debug.Log("Element " + i + ": " + stringArray[i]);
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

