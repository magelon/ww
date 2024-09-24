using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gachagrop : MonoBehaviour
{
    public static List<string> equiptedL;
    public GameObject[] buttonGroup;
    public int maxEquippedItems = 3; 
    public Image im;

    // Start is called before the first frame update
    void Start()
    {
        equiptedL = new List<string>();

        for (int i = 0; i < maxEquippedItems; i++)
        {
            string equipKey = "currentEquip" + i;

            if (PlayerPrefs.HasKey(equipKey))
            {
                string equippedItem = PlayerPrefs.GetString(equipKey);

                if (!equiptedL.Contains(equippedItem))
                {
                    equiptedL.Add(equippedItem);

                    // Check if buttonGroup[i] is assigned
                    if (buttonGroup[i] == null)
                    {
                        Debug.LogError("buttonGroup[" + i + "] is not assigned in the Inspector.");
                        continue;
                    }

                    // Initialize the button with the item image
                    Sprite itemSprite = Resources.Load<Sprite>("sumPrefabs/itemImgs/" + equippedItem);

                    // Check if the sprite is valid
                    if (itemSprite != null)
                    {
                        Image buttonImage = buttonGroup[i].GetComponent<Image>();
                        if (buttonImage != null)
                        {
                            buttonImage.sprite = itemSprite;
                        }
                        else
                        {
                            Debug.LogError("No Image component found on buttonGroup[" + i + "]");
                        }
                    }
                    else
                    {
                        Debug.LogWarning("Sprite not found for item: " + equippedItem);
                    }

                    // Check if the GameObject has a Button component
                    Button buttonComponent = buttonGroup[i].GetComponent<Button>();
                    if (buttonComponent != null)
                    {
                        GameObject currentButton = buttonGroup[i];
                        // Assign the button click event and pass the button GameObject
                        buttonComponent.onClick.AddListener(() => OnButtonClick(currentButton));
                    }
                    else
                    {
                        Debug.LogError("No Button component found on buttonGroup[" + i + "]");
                    }
                }
            }
        }
    }

    // Function to be called when the button is clicked
    void OnButtonClick(GameObject button)
    {
        // Get the button's Image component
        Image buttonImage = button.GetComponent<Image>();

        // Retrieve the sprite's name
        string spriteName = buttonImage.sprite != null ? buttonImage.sprite.name : "No Sprite";

        im.sprite=Resources.Load<Sprite>("sumPrefabs/illistrate/"+spriteName);
    }

}
