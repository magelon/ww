using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
//unite or items in team
public class GachClick : MonoBehaviour
{
    //a queue of equipted items
    public  static List<string> equiptedL;
    //queue of the item Buttons
    public  static Queue<GameObject> itemB;

    //private GameObject removeItem;
    //public loadUnitsUI luui;
    public GameObject[] buttonGroup;



    void Start()
    {
        //PlayerPrefs.DeleteAll();
        //PlayerPrefs.SetInt("coin", 10000);
        equiptedL = new List<string>();
        itemB = new Queue<GameObject>();
        //removeItem = new GameObject();

        /*
        for (int i = 0; i < 3; i++)
        {
            if (PlayerPrefs.HasKey("currentEquip" + i))
            {
                if (this.gameObject.name == PlayerPrefs.GetString("currentEquip" + i))
                {
                    equiptedL.Add(PlayerPrefs.GetString("currentEquip" + i));
                    itemB.Enqueue(GameObject.Find(PlayerPrefs.GetString("currentEquip" + i)));
                    this.transform.Find("tick").GetComponent<Image>().enabled = true;
                }

            }
            //tmp 3 equip slots

        }

        Debug.Log(equiptedL.Count+" "+itemB.Count);
        **/
        Invoke("loadEquip", 1);
    }

    private void Update()
    {
        InvokeRepeating("refresh", 2f, 2f);
    }

    private void refresh()
    {
        
            int count = buttonGroup.Length;
            int ecount = equiptedL.Count;
            for (int i = 0; i < count; i++)
            {
                if (i < ecount)
                {
                    buttonGroup[i].GetComponent<Image>().sprite =
                   Resources.Load<Sprite>("sumPrefabs/itemImgs/" + equiptedL[i]);
                }
                else
                {
                    buttonGroup[i].GetComponent<Image>().sprite =
                         Resources.Load<Sprite>("sumPrefabs/itemImgs/null");
                }
            }
        
       
    }

    private void loadEquip()
    {
        for (int i = 0; i < 3; i++)
        {
            if (PlayerPrefs.HasKey("currentEquip" + i))
            {
               if(equiptedL.Contains(PlayerPrefs.GetString("currentEquip" + i)) != true)
                {
                    equiptedL.Add(PlayerPrefs.GetString("currentEquip" + i));
                    //init view
                    buttonGroup[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("sumPrefabs/itemImgs/"
                    + PlayerPrefs.GetString("currentEquip" + i));

                    itemB.Enqueue(GameObject.Find(PlayerPrefs.GetString("currentEquip" + i)));
                }

                if (this.gameObject.name == PlayerPrefs.GetString("currentEquip" + i))
                {
                   
                    this.transform.Find("tick").GetComponent<Image>().enabled = true;
                }

            }
            //tmp 3 equip slots

        }
        Debug.Log(equiptedL.Count + " " + itemB.Count);
    }

    public void OnClick(GameObject g)
    {
        // Debug.Log(equiptedL.Count);
        //int slotCount = 0;

        if (g.transform.Find("lock").GetComponent<Image>().enabled == false)
        {
            //check tick is on off
            if (g.transform.Find("tick").GetComponent<Image>().enabled == false)
            {
                //check equipt limited items
                if (equiptedL == null|| equiptedL.Count < 3)
                {
                    //put item name into queue
                    equiptedL.Add(g.name);
                    itemB.Enqueue(g);

                    //tick it up
                    g.transform.Find("tick").GetComponent<Image>().enabled = true;
                    
                    for (int i = 0; i < equiptedL.Count; i++)
                    {
                        PlayerPrefs.SetString("currentEquip" + i, equiptedL[i]);
                    }
                }
                else if (equiptedL.Count == 3)
                {

                    GameObject removeItem = itemB.Dequeue();
                    //Debug.Log(removeItem.name + "removed");
                    if(removeItem){
                        removeItem.transform.Find("tick").GetComponent<Image>().enabled = false;
                        equiptedL.Remove(removeItem.name);
                    }
                    

                    //put item name into queue
                    itemB.Enqueue(g);
                    //tick it up
                    g.transform.Find("tick").GetComponent<Image>().enabled = true;

                    equiptedL.Add(g.name);

                    for (int i = 0; i < equiptedL.Count; i++)
                    {

                        PlayerPrefs.SetString("currentEquip" + i, equiptedL[i]);
                    }
                }

            }
            //the tick is true disable it 
            else
            {
                //set tick off and remove item form playerprefs and list and queue
                itemB = new Queue<GameObject>();
                List<string> tmp = new List<string>();
                g.transform.Find("tick").GetComponent<Image>().enabled = false;
                for (int i = 0; i < equiptedL.Count; i++)
                {
                    if (equiptedL[i] == g.name)
                    {
                        //SetString
                        PlayerPrefs.DeleteKey("currentEquip" + i);

                    }
                    else
                    {
                        tmp.Add(GameObject.Find(equiptedL[i]).name);
                        itemB.Enqueue(GameObject.Find(equiptedL[i]));
                    }

                }
                equiptedL = tmp;
            }



        }

    }

}
