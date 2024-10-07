using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DailyBouns : MonoBehaviour
{
    string date;
    int dayCount;
    public RectTransform panel;
    private int bonus;


    void Start()
    {
        //PlayerPrefs.DeleteAll();PlayerPrefs is a class that stores Player preferences between game sessions. It can store string, float and integer values into the user’s platform registry.
        //Unity stores PlayerPrefs in a local registry, without encryption. Do not use PlayerPrefs data to store sensitive data.
        dayCount = PlayerPrefs.GetInt("dayCount", 0);
        bonus = panel.childCount;
        date = DateTime.Now.ToString().Substring(0, 9);
        Debug.Log(date);

        

        if (!PlayerPrefs.HasKey(date))
        {
            PlayerPrefs.SetInt(date, 1);
            dayCount++;
            PlayerPrefs.SetInt("dayCount", dayCount);
            //get daily Bouns
            for (int i = 0; i < dayCount; i++)
            {
                if (i < dayCount - 1)
                {
                   panel.GetChild(i).GetChild(1).gameObject.SetActive(true);
                }

                if (i == dayCount - 1)
                {
                    panel.GetChild(i).GetChild(0).gameObject.SetActive(true);
                    //add 120 to prefs
                }
            }

            if (dayCount == bonus)
            {
                //clean up
                PlayerPrefs.SetInt("dayCount", 0);
            }
        }
        else
        {
            for (int i = 0; i < dayCount; i++)
            {
                panel.GetChild(i).GetChild(1).gameObject.SetActive(true);
            }

        }

    }
}
