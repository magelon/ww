using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEnhance : MonoBehaviour
{  
    private Enime en;
    private TankAI ta;
    private int atk;
    private int hp;
    private int level;
    private string name;
    // Start is called before the first frame update
    void Start()
    {
       en=GetComponent<Enime>();
       ta=GetComponent<TankAI>();
       atk=(int)ta.damage;
       hp=en.health;
       name=gameObject.name;
       name=RemoveCloneSuffix(name);
       level=PlayerPrefs.GetInt(name+"Level",1);
       Debug.Log(name);
       //ta.damage=(float)atk+level;
       if(en.f== Factions.yellow){
           en.health=hp*level;
           ta.damage=atk*level;
        }
    }

    public static string RemoveCloneSuffix(string input)
    {
        // Check if the input string contains "(Clone)"
        int cloneIndex = input.IndexOf("(Clone)");
        if (cloneIndex != -1)
        {
            // Remove "(Clone)" and any leading/trailing spaces
            string result = input.Substring(0, cloneIndex).Trim();
            return result;
        }
        else
        {
            // Return the original string if "(Clone)" is not found
            return input;
        }
    }
}
