using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAttributeLoader : MonoBehaviour
{
    public TextAsset jsonFile; // Reference to your JSON file
    public List<Item> loadedItems;

    private Enime en;
    private TankAI ta;

    void Awake()
    {
        if (jsonFile != null)
        {
            string jsonText = jsonFile.text;
            ItemsData itemsData = JsonUtility.FromJson<ItemsData>(jsonText);
            loadedItems = itemsData.items;

            // Find the item with the same name as the GameObject
            string objectName = gameObject.name;
            string dataName=RemoveCloneSuffix(objectName);
            Item item = loadedItems.Find(i => i.itemsName == dataName);

            if (item != null)
            {
                // Apply attributes to the GameObject
                ApplyAttributesToGameObject(item.attributes);
            }
            else
            {
                Debug.LogWarning("Item not found for the GameObject: " + objectName);
            }
        }
        else
        {
            Debug.LogError("JSON file is not assigned.");
        }
    }

    public static string RemoveCloneSuffix(string input)
    {
        // Check if the input string contains "(Clone)"
        int cloneIndex = input.IndexOf("(Clone)");
        if (cloneIndex != -1)
        {
            // Remove "(Clone)" and any leading/trailing spaces
            string result = input.Substring(0, cloneIndex).Trim();
            return result;
        }
        else
        {
            // Return the original string if "(Clone)" is not found
            return input;
        }
    }

    private void ApplyAttributesToGameObject(Attributes attributes)
    {
        en=GetComponent<Enime>();
        ta=GetComponent<TankAI>();
        ta.element=attributes.element;
        en.Eelement=attributes.element;
        ta.heal=attributes.atk;
        ta.damage=attributes.atk;
        en.health=attributes.hp;
        en.energy=attributes.energy;
        en.speed=attributes.speed;
        en.knockBackForce=(float)attributes.force;
    }
}

