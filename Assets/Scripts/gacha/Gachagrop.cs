using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gachagrop : MonoBehaviour
{
    public  static List<string> equiptedL;
    public GameObject[] buttonGroup;
    // Start is called before the first frame update
    void Start()
    {
        equiptedL = new List<string>();
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
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
