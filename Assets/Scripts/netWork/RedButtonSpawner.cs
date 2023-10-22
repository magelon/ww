using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//for test 
public class RedButtonSpawner : MonoBehaviour
{
    private Transform po;
    public GameObject red;
    public GameObject yellow;
    //public GameObject yellow;
    public float spawnTime=1f;
    public float startTime;
    private Image im;
    public Text text;
    public float energyCost;
    public energySlider sl;

    private string imgName;
    public GameObject bullet;
    public GameObject bulletClient;

    //public SetupLocalPlayer slp;

    private void Start()
    {
        Invoke("laterStart", 2f);
        text.text = energyCost*100+"";
        po = GameObject.Find("baseL").transform;
    }

    void laterStart()
    {
        im =transform.Find("Image").GetComponent<Image>();
        if (im.sprite)
        {
            imgName = im.sprite.name;
            red = Resources.Load<GameObject>("netWorkprefabs/" + imgName+"r");
            red.GetComponent<Enime>().f = Factions.red;
            energyCost = red.GetComponent<Enime>().energy;
            text.text = energyCost * 100 + "";
         
            poolManager.instance.CreatePool(red, 15);
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
       
        //slp.CmdSpawn("r"+name);
    }

  
}
