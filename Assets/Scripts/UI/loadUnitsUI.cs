using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//end up nothing used here
public class loadUnitsUI : MonoBehaviour
{
    public List<string> equiptedL;
    public Queue<GameObject> itemButton;

    public static loadUnitsUI instance;
    public static loadUnitsUI getInstance()
    {
        if (instance == null)
        {
            instance = new loadUnitsUI();
        }
        return instance;
    }

    void Start()
    {
        equiptedL = new List<string>();
        
        itemButton = new Queue<GameObject>();
        //removeItem = new GameObject();

        for (int i = 0; i < 3; i++)
        {
            if (PlayerPrefs.HasKey("currentEquip" + i))
            {
                
                    equiptedL.Add(PlayerPrefs.GetString("currentEquip" + i));
                    itemButton.Enqueue(GameObject.Find(PlayerPrefs.GetString("currentEquip" + i)));
                    //this.transform.Find("tick").GetComponent<Image>().enabled = true;

            }
            //tmp 3 equip slots

        }

       
        for (int i = 0; i < 3; i++)
        {
            if (PlayerPrefs.HasKey("currentEquip" + i))
            {
                Debug.Log("CurrentEquipedCard"+PlayerPrefs.GetString("currentEquip" + i));
            }
 
        }
       
    }

  

}
