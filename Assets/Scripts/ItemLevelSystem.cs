using UnityEngine;
using UnityEngine.UI;

public class ItemLevelSystem : MonoBehaviour
{
    private int experience;
    public Text itemLevelText; // Reference to the UI Text component

    [System.Serializable]
    public class ItemLevelData
    {
        public string itemName;  // Name of the item
        public int level;        // Current level of the item
        public int experienceRequired;   // Experience needed for the item's next level
    }

    public ItemLevelData[] items;

    void Start()
    {
        LoadData();
        InitializeDefaultItems(); // Initialize default items
    }

    // Initialize default items from item0 to item9
    void InitializeDefaultItems()
    {
        if (items.Length == 0)
        {
            items = new ItemLevelData[10]; // Create an array with 10 slots

            for (int i = 0; i < items.Length; i++)
            {
                items[i] = new ItemLevelData
                {
                    itemName = "item" + i,
                    level = 1,  // You can set the default level here
                    experienceRequired = 100,  // You can set the default experience requirement here
                };
            }

            SaveData(); // Save the default items to PlayerPrefs
        }
    }

    public void LevelUp()
    {
        // Get the item name from the GameObject's name
    string itemName = gameObject.name;

    // Find the specific item you want to level up based on its name
    ItemLevelData itemToLevelUp = null;

    foreach (var item in items)
    {
        if (item.itemName == itemName)
        {
            itemToLevelUp = item;
            break;
        }
    }

    if (itemToLevelUp != null)
    {
        if (experience >= itemToLevelUp.experienceRequired)
        {
        itemToLevelUp.level++;
        experience -= itemToLevelUp.experienceRequired;
        itemToLevelUp.experienceRequired = CalculateExperienceRequiredForNextLevel(itemToLevelUp);
        SaveData();
        Debug.Log("Level Up! " + itemToLevelUp.itemName + " New Level: " + itemToLevelUp.level);
        }else
        {
            Debug.LogError("Not enough experience to level up " + itemToLevelUp.itemName);
        }
        UpdateItemLevelText(itemName);
    }
    else
    {
        Debug.LogError("Item not found: " + itemName);
    }

    }

    public void UpdateItemLevelText(string itemName)
    {
    ItemLevelData itemToUpdate = null;

    foreach (var item in items)
    {
        if (item.itemName == itemName)
        {
            itemToUpdate = item;
            break;
        }
    }

    if (itemToUpdate != null)
    {
        itemLevelText.text = "Level: " + itemToUpdate.level.ToString();
    }
    else
    {
        itemLevelText.text = "Item not found";
    }
    }


    private int CalculateExperienceRequiredForNextLevel(ItemLevelData item)
    {
        return item.experienceRequired * 2; // Adjust as needed
    }

    private void SaveData()
    {
        PlayerPrefs.SetInt("totalXp", experience);

        foreach (ItemLevelData item in items)
        {
            PlayerPrefs.SetInt(GetPlayerPrefsKey(item.itemName, "Level"), item.level);
            PlayerPrefs.SetInt(GetPlayerPrefsKey(item.itemName, "ExperienceRequired"), item.experienceRequired);
        }

        PlayerPrefs.Save();
    }

    private void LoadData()
    {
        string itemName = gameObject.name;
        UpdateItemLevelText(itemName);
        if (PlayerPrefs.HasKey("totalXp"))
        {
            experience = PlayerPrefs.GetInt("totalXp");
        }

        foreach (ItemLevelData item in items)
        {
            if (PlayerPrefs.HasKey(GetPlayerPrefsKey(item.itemName, "Level")))
            {
                item.level = PlayerPrefs.GetInt(GetPlayerPrefsKey(item.itemName, "Level"));
            }

            if (PlayerPrefs.HasKey(GetPlayerPrefsKey(item.itemName, "ExperienceRequired")))
            {
                item.experienceRequired = PlayerPrefs.GetInt(GetPlayerPrefsKey(item.itemName, "ExperienceRequired"));
            }
        }
    }

    private string GetPlayerPrefsKey(string itemName, string dataType)
    {
        return itemName + "_" + dataType;
    }
}
