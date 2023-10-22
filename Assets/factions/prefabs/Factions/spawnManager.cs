using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class spawnManager : MonoBehaviour
{
    public Button[] SpawnButtonGroup;
    private List<string> equipList;
    public GameObject intro;
    //lv1 unlock lv2 unlock lv3 unlock 0,1,2
    //public int requimentLv;

    void Start()
    {
        //PlayerPrefs.DeleteAll();
        //currentLv =(SceneManager.GetActiveScene().name.Substring(4, 1));

        int count= SpawnButtonGroup.Length;
        equipList = new List<string>();
        int ecount = 0;
        for (int i = 0; i < count; i++)
        {
            if (PlayerPrefs.HasKey("currentEquip" + i))
            {
                equipList.Add(PlayerPrefs.GetString("currentEquip" + i));
                ecount++;
            }
        }


        for (int i = 0; i < count; i++)
        {
            if (i<ecount)
            {
                SpawnButtonGroup[i].transform.Find("Image").GetComponent<Image>().sprite = 
                Resources.Load<Sprite>("sumPrefabs/itemImgs/" + equipList[i]);

            }
            else
            {
                SpawnButtonGroup[i].transform.Find("Image").GetComponent<Image>().sprite = 
                Resources.Load<Sprite>("sumPrefabs/itemImgs/" + equipList[0]);
                //SpawnButtonGroup[i].transform.Find("Image").GetComponent<Image>().sprite = false;
            }
        }

        //check level is passed or not and given things
        if (GameData.getInstance().levelPassed < 4)
        {
            GameData.getInstance().levelPassed = PlayerPrefs.GetInt("levelPassed", 0);
            //Debug.Log("current passed level = " + GameData.getInstance().levelPassed);
            if (GameData.getInstance().levelPassed == 0)
            {
                //unlock item0
                PlayerPrefs.SetInt("item_0", 1);
                GameManager.getInstance().updateItemLock();
                SpawnButtonGroup[0].transform.Find("Image").GetComponent<Image>().sprite =
                Resources.Load<Sprite>("sumPrefabs/itemImgs/item0");
                PlayerPrefs.SetString("currentEquip0","item0");
                if (intro)
                {
                    intro.SetActive(true);
                }

            }
            if (GameData.getInstance().levelPassed == 2)
            {
                PlayerPrefs.SetInt("item_1", 1);
                GameManager.getInstance().updateItemLock();
                SpawnButtonGroup[0].transform.Find("Image").GetComponent<Image>().sprite =
                Resources.Load<Sprite>("sumPrefabs/itemImgs/item0");
                SpawnButtonGroup[1].transform.Find("Image").GetComponent<Image>().sprite =
                Resources.Load<Sprite>("sumPrefabs/itemImgs/item1");
                PlayerPrefs.SetString("currentEquip1", "item1");
                if (intro)
                {
                    intro.SetActive(true);
                }

            }
            if (GameData.getInstance().levelPassed == 3)
            {
                PlayerPrefs.SetInt("item_2", 1);
                GameManager.getInstance().updateItemLock();
                SpawnButtonGroup[0].transform.Find("Image").GetComponent<Image>().sprite =
                Resources.Load<Sprite>("sumPrefabs/itemImgs/item0");
                SpawnButtonGroup[1].transform.Find("Image").GetComponent<Image>().sprite =
                Resources.Load<Sprite>("sumPrefabs/itemImgs/item1");
                SpawnButtonGroup[2].transform.Find("Image").GetComponent<Image>().sprite =
                Resources.Load<Sprite>("sumPrefabs/itemImgs/item2");
                PlayerPrefs.SetString("currentEquip2", "item2");
                if (intro)
                {
                    intro.SetActive(true);
                }
            }
        }

    }

}
