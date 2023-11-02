using UnityEngine;
using UnityEngine.UI;

public class ItemLevelSystem : MonoBehaviour
{
    private int experience;
    public Text itemLevelText; // Reference to the UI Text component
    private string itemName;

    public int level;        // Current level of the item
    public int experienceRequired;   // Experience needed for the item's next level

    void Start()
    {
        itemName = gameObject.name;
        LoadData();
    }

    public void LevelUp()
    {

    if (itemName != null)
    {
        if (experience >= level*100)
        {
            level++;
            experience -= level*100;
        SaveData();
        Debug.Log("Level Up! " + itemName + " New Level: " + level);
        }else
        {
            Debug.LogError("Not enough experience to level up " + itemName);
        }
        UpdateItemLevelText();
    }
    else
    {
        Debug.LogError("Item not found: " + itemName);
    }

    }

    public void UpdateItemLevelText()
    {

    if (itemName != null)
    {
        itemLevelText.text = "Level: " + level.ToString();
    }
    else
    {
        itemLevelText.text = "Item not found";
    }
    }

    private void SaveData()
    {
        PlayerPrefs.SetInt("totalXp", experience);
        PlayerPrefs.SetInt(itemName+"Level",level);
        PlayerPrefs.Save();
    }

    private void LoadData()
    { 
        if (PlayerPrefs.HasKey("totalXp"))
        {
            experience = PlayerPrefs.GetInt("totalXp");
        }

        if(PlayerPrefs.HasKey(itemName+"Level")){
            itemLevelText.text = "Level: " + PlayerPrefs.GetInt(itemName+"Level").ToString();
            level=PlayerPrefs.GetInt(itemName+"Level");
            experienceRequired=100*level;
        }else{
            PlayerPrefs.SetInt(itemName+"Level",1);
        }
    }

}
