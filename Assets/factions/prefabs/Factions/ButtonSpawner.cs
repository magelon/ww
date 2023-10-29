using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    private string imgName;
    
    private void Start()
    {
        Invoke("laterStart", 2f);
        text.text = energyCost*100+"";
    }

    void laterStart()
    {
        
            im =transform.Find("Image").GetComponent<Image>();
            if (im.sprite)
            {
                imgName = im.sprite.name;
                white = Resources.Load<GameObject>("sumPrefabs/" + imgName);
                energyCost = white.GetComponent<Enime>().energy;
                text.text = energyCost * 100 + "";
                poolManager.instance.CreatePool(white, 15);
            }
        
    }

    void Update()
    {
        startTime += Time.deltaTime;
        if (im != null)
        {
            im.fillAmount = startTime / spawnTime;
        }

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
