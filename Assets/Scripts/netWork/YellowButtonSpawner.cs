using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//for test 
public class YellowButtonSpawner : MonoBehaviour
{
    private Transform po;
    public GameObject yellow;
    public GameObject red;
    //public GameObject yellow;
    public float spawnTime=1f;
    public float startTime;
    private Image im;
    public Text text;
    public float energyCost;
    public energySlider sl;
    public GameObject bullet;
    public GameObject bulletClient;

    // public SetupLocalPlayer slp;

    private string imgName;
    
    private void Start()
    {
        Invoke("laterStart", 1f);
        text.text = energyCost*100+"";
        po = GameObject.Find("baseR").transform;
    }

    void laterStart()
    {
        im =transform.Find("Image").GetComponent<Image>();
        if (im.sprite)
        {
            imgName = im.sprite.name;
            yellow = Resources.Load<GameObject>("netWorkprefabs/" + imgName+"y");
            yellow.GetComponent<Enime>().f = Factions.yellow;
            energyCost = yellow.GetComponent<Enime>().energy;
            text.text = energyCost * 100 + "";
           
            
            poolManager.instance.CreatePool(yellow, 15);
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
            if (sl.energy > energyCost)
            {
                startTime = 0;

                spawnWithslp(imgName);
                

                sl.energy -= energyCost;
            }
        }
    }

    void spawnWithslp(string name)
    {
       // GameObject go = Instantiate(yellow, po.position, Quaternion.identity);
      //  go.GetComponent<Enime>().f = Factions.yellow;
       // slp.g = go;
        //slp.CmdFire();
        //slp.CmdSpawn("y"+name);
    }


}
