using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class ItemLevelSystem : MonoBehaviour
{
    private int experience;
    public Text itemLevelText; // Reference to the UI Text component
    public ExpBar expbar;
    private string itemName;

    public int level;        // Current level of the item
    public int experienceRequired;   // Experience needed for the item's next level

    public Text name;
	public Text health;
	public Text atk;
	public Text energy;
	public Text speed;
	public Text rate;
	public Text art;

    public TextAsset jsonFile;
    private List<Item2> loadedItems;
	private List<Item2> simplifiedItemList;

    void Start()
    {
        itemName = gameObject.name;
        if(jsonFile != null){
        string jsonText = jsonFile.text;
        ItemsData2 itemsData = JsonUtility.FromJson<ItemsData2>(jsonText);
        loadedItems = itemsData.items;
        
        	}
        	else
        	{
            	Debug.LogError("No JSON file assigned.");
        	}

        if (!PlayerPrefs.HasKey(itemName))
            {
				name.text="name: ???";
				health.text="hp: ???";
				atk.text="attack: ???";
				energy.text="cost: ???";
				speed.text="speed: ???";
				rate.text="rate: ???";
				art.text="art: ???";
               
            }
            else
            {
                Item2 item = loadedItems.Find(i => i.itemsName == itemName);

				name.text="name: "+item.itemsName;
				health.text="hp: "+item.attributes.hp.ToString();
				atk.text="attack: "+item.attributes.atk.ToString();
				energy.text="cost: "+item.attributes.energy.ToString();
				speed.text="speed: "+item.attributes.speed.ToString();
				rate.text="rate: "+item.attributes.rate.ToString();
				art.text="art: "+item.attributes.art;
				
            }

        LoadData();
    }

    public void LevelUp()
    {

    if (itemName != null)
    {
        if (experience >= level*1000)
        {
            level++;
            experience -= level*1000;
        SaveData();
        Debug.Log("Level Up! " + itemName + " New Level: " + level);
        }else
        {
            Debug.Log("Not enough experience to level up " + itemName);
        }
        UpdateItemLevelText();
    }
    else
    {
        Debug.Log("Item not found: " + itemName);
    }
    
        expbar.GetComponent<ExpBar>().xpUpdate();

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
            experienceRequired=1000*level;
        }else{
            PlayerPrefs.SetInt(itemName+"Level",1);
        }
    }

}
