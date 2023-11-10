using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Attributes
{
    public int hp;
    public int atk;
    public int mess;
    public float speed;
    public float energy;
    public int force;
}

[System.Serializable]
public class Item
{
    public string itemsName;
    public Attributes attributes;
}

[System.Serializable]
public class ItemsData
{
    public List<Item> items;
}

//button spawners and energy generate
public class ButtonSpawner : MonoBehaviour
{
    public Transform po;
    public GameObject white;
    //public GameObject yellow;
    public float spawnTime=1f;
    public float startTime;
    private Image im;
    public Text text;
    public float energyCost;
    private bool loaded;

    public TextAsset jsonFile;

    private string imgName;

    public List<Item> loadedItems;
    
    private void Start()
    {
        loaded=false;
        //Invoke("laterStart", 2f);
        //text.text = energyCost*100+"";
    }

    void laterStart()
    {
        if(!loaded){
            im =transform.Find("Image").GetComponent<Image>();
            if (im.sprite && jsonFile != null)
            {
                loaded=true;
                
                string jsonText = jsonFile.text;
                ItemsData itemsData = JsonUtility.FromJson<ItemsData>(jsonText);

                imgName = im.sprite.name;
                loadedItems = itemsData.items;
                Item item = loadedItems.Find(i => i.itemsName == imgName);
                Debug.Log(imgName);

                if(item!=null){
                    energyCost = item.attributes.energy;
                    text.text = energyCost * 100 + "";
                    white = Resources.Load<GameObject>("sumPrefabs/" + imgName);
                    white.GetComponent<Enime>().f=Factions.yellow;
                    poolManager.instance.CreatePool(white, 15);
                }
                
            }
        }
    }

    void Update()
    {
        startTime += Time.deltaTime;
        if (im != null)
        {
            im.fillAmount = startTime / spawnTime;
        }
        laterStart();
    }

    public void spawn()
    {
        if (startTime > spawnTime)
        {
            GameManager.getInstance().playSfx("buttonSpawn");
            if (GameData.getInstance().energy > energyCost)
            {
                startTime = 0;
                
                poolManager.instance.ReuseObject(white, 
                new Vector3(po.position.x, po.position.y + 0.2f, 0), Quaternion.identity);
                GameData.getInstance().energy -= energyCost;
            }
        }
    }
}
