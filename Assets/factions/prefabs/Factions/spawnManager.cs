using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class spawnManager : MonoBehaviour
{
    public Button[] SpawnButtonGroup;
    private List<string> equipList;
    //public GameObject intro;
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
            else{
                equipList.Add("item0");

            }
        }


        // for (int i = 0; i < count; i++)
        // {
        //     if (i<ecount)
        //     {
        //         SpawnButtonGroup[i].transform.Find("Image").GetComponent<Image>().sprite = 
        //         Resources.Load<Sprite>("sumPrefabs/itemImgs/" + equipList[i]);

        //     }
        //     else
        //     {
        //         SpawnButtonGroup[i].transform.Find("Image").GetComponent<Image>().sprite = 
        //         Resources.Load<Sprite>("sumPrefabs/itemImgs/" + equipList[0]);
        //         //SpawnButtonGroup[i].transform.Find("Image").GetComponent<Image>().sprite = false;
        //     }
        // }

        //generated with chat gpt add items from local to the scene
            for (int i = 0; i < count; i++)
            {
                Image buttonImage = SpawnButtonGroup[i].transform.Find("Image").GetComponent<Image>();
                if (i < ecount)
                {
                    string spritePath = "sumPrefabs/itemImgs/" + equipList[i];
                    Sprite sprite = Resources.Load<Sprite>(spritePath);
                    
                    if (sprite != null)
                    {
                        buttonImage.sprite = sprite;
                    }
                    else
                    {
                        Debug.LogWarning("Sprite not found: " + spritePath);
                        // Optionally, set a default sprite or handle the error in another way.
                    }
                }
                else
                {
                    if (equipList.Count > 0)
                    {
                        buttonImage.sprite = Resources.Load<Sprite>("sumPrefabs/itemImgs/" + equipList[0]);
                    }
                    else
                    {
                        // Handle the case where equipList is empty.
                        Debug.LogWarning("equipList is empty");
                        // Optionally, set a default sprite or handle the error in another way.
                    }
                }
            }


        //check level is passed or not and given things
        // if (GameData.getInstance().levelPassed < 4)
        // {
        //     GameData.getInstance().levelPassed = PlayerPrefs.GetInt("levelPassed", 0);
        //     //Debug.Log("current passed level = " + GameData.getInstance().levelPassed);
        //     if (GameData.getInstance().levelPassed == 0)
        //     {
        //         //unlock item0
        //         PlayerPrefs.SetInt("item_0", 1);
        //         GameManager.getInstance().updateItemLock();
        //         SpawnButtonGroup[0].transform.Find("Image").GetComponent<Image>().sprite =
        //         Resources.Load<Sprite>("sumPrefabs/itemImgs/item0");
        //         PlayerPrefs.SetString("currentEquip0","item0");
        //         // if (intro)
        //         // {
        //         //     intro.SetActive(true);
        //         // }

        //     }
        //     if (GameData.getInstance().levelPassed == 2)
        //     {
        //         PlayerPrefs.SetInt("item_1", 1);
        //         GameManager.getInstance().updateItemLock();
        //         SpawnButtonGroup[0].transform.Find("Image").GetComponent<Image>().sprite =
        //         Resources.Load<Sprite>("sumPrefabs/itemImgs/item0");
        //         SpawnButtonGroup[1].transform.Find("Image").GetComponent<Image>().sprite =
        //         Resources.Load<Sprite>("sumPrefabs/itemImgs/item1");
        //         PlayerPrefs.SetString("currentEquip1", "item1");
        //         // if (intro)
        //         // {
        //         //     intro.SetActive(true);
        //         // }

        //     }
        //     if (GameData.getInstance().levelPassed == 3)
        //     {
        //         PlayerPrefs.SetInt("item_2", 1);
        //         GameManager.getInstance().updateItemLock();
        //         SpawnButtonGroup[0].transform.Find("Image").GetComponent<Image>().sprite =
        //         Resources.Load<Sprite>("sumPrefabs/itemImgs/item0");
        //         SpawnButtonGroup[1].transform.Find("Image").GetComponent<Image>().sprite =
        //         Resources.Load<Sprite>("sumPrefabs/itemImgs/item1");
        //         SpawnButtonGroup[2].transform.Find("Image").GetComponent<Image>().sprite =
        //         Resources.Load<Sprite>("sumPrefabs/itemImgs/item2");
        //         PlayerPrefs.SetString("currentEquip2", "item2");
        //         // if (intro)
        //         // {
        //         //     intro.SetActive(true);
        //         // }
        //     }
        // }

    }

}
