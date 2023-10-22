using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpBar : MonoBehaviour
{
    int xp;
    Text t;
    void Start()
    {
        xp = PlayerPrefs.GetInt("totalXp");
        t = GetComponent<Text>();
        t.text = "Exp: " + xp;
    }

}
