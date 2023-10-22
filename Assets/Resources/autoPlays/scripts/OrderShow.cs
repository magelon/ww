using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderShow : MonoBehaviour
{
    public GameObject[] foes;
    public int index;

    

    void Start()
    {
        //PlayerPrefs.DeleteAll();
        index = 0;
        for (int i = 0; i < foes.Length; i++)
        {
            if (i == index)
            {
                foes[i].SetActive(true);
            }
        }
    }

    private void Update()
    {
        if (index < 0)
        {
            index = 0;
        }
        if (index > foes.Length-1)
        {
            index = foes.Length-1;
        }
    }

    public void UpdateIndex()
    {
        for (int i = 0; i < foes.Length; i++)
        {
            if (i == index)
            {
                foes[i].SetActive(true);
            }
            else
            {
                foes[i].SetActive(false);
            }
        }
    }

    public void HideImage(int n)
    {
        for (int i = 0; i < foes.Length; i++)
        {
            if (i == n)
            {
                Color tmp = foes[1].GetComponent<SpriteRenderer>().color;
                tmp.r = 0;
                tmp.g = 0;
                tmp.b = 0;
                foes[i].GetComponent<SpriteRenderer>().color = tmp;
            }
        }
    }

}
