using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Attributes
{
    public int hp;
    public int atk;
    public int mess;
    public float speed;
    public int energy;
    public int force;
}

[System.Serializable]
public class Item
{
    public string itemName;
    public Attributes attributes;
}

[System.Serializable]
public class ItemsData
{
    public List<Item> items;
}

public class ItemAttributeLoader : MonoBehaviour
{
    public TextAsset jsonFile; // Reference to your JSON file
    public List<Item> loadedItems;

    private Enime en;
    private TankAI ta;

    void Start()
    {
       

        if (jsonFile != null)
        {
            string jsonText = jsonFile.text;
            ItemsData itemsData = JsonUtility.FromJson<ItemsData>(jsonText);
            loadedItems = itemsData.items;

             // Find the item with the same name as the GameObject
            string objectName = gameObject.name;
            Item item = loadedItems.Find(i => i.itemName == objectName);

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

    private void ApplyAttributesToGameObject(Attributes attributes)
    {
        en=GetComponent<Enime>();
        ta=GetComponent<TankAI>();
        ta.damage=attributes.atk;
        en.health=attributes.hp;
        en.energy=(float)attributes.energy;
        en.speed=(float)attributes.speed;
        en.knockBackForce=(float)attributes.force;
    }
}

