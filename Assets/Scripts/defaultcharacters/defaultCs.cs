using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class defaultCs : MonoBehaviour
{
    List<string> stringList = new List<string> {"yb", "hcz", "rm"};
    public  static List<string> equiptedL;
    // Start is called before the first frame update
    void Start()
    {
        equiptedL = new List<string>();
        //PlayerPrefs.DeleteAll();
        //Loop through the collection
         
        foreach (string fruit in stringList)
        {
            if (!PlayerPrefs.HasKey(fruit))
            {
                equiptedL.Add(fruit);
                PlayerPrefs.SetInt(fruit, 1);
                PlayerPrefs.SetInt(fruit+"Level",1);
                GameManager.getInstance().init();
            }
        }

        for (int i = 0; i < equiptedL.Count; i++)
        {
        PlayerPrefs.SetString("currentEquip" + i, equiptedL[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
