using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DispayDialogs : MonoBehaviour
{
    public Dialog[] dias;
    private Queue<string> lines;
    //private Dialog currentDia;
    private int index;
    public Text text;
    private Image image;

    public GameObject[] characters;

    void Start()
    {
        index = 0;
        text.text = dias[index].who + ": " + dias[index].lines;
        Invoke("StopTime", 2f);

        for (int i = 0; i < characters.Length; i++)
        {
            if (characters[i].name != dias[0].who)
            {
                image = characters[i].GetComponent<Image>();
                Color col = image.color;
                col.a = 0.5f;
                image.color = col;
            }
        }
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
    }

    private void StopTime()
    {

        Time.timeScale = 0;
    }

    public void ButtonHandler(GameObject g)
    {
        if (g.name == "next")
        {
            index++;
            //terminate point
            if (index >= dias.Length)
            {
                this.gameObject.SetActive(false);
            }
            else
            {
                //going on dialogs
                text.text = dias[index].who + ": " + dias[index].lines;
                for (int i = 0; i < characters.Length; i++)
                {
                    //high light the character
                    if (characters[i].name == dias[index].who)
                    {
                        image = characters[i].GetComponent<Image>();
                        Color col = image.color;
                        col.a = 1;
                        image.color = col;
                    }
                    else //dehighlight the character
                    {
                        image = characters[i].GetComponent<Image>();
                        Color col = image.color;
                        col.a = 0.5f;
                        image.color = col;
                    }
                }
            }

        }
    }

}
