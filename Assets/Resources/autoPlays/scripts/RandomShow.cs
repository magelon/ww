using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomShow : MonoBehaviour
{
    public GameObject[] foes;
    private int randomN;
    
    void Start()
    {
        randomN = Random.Range(0, foes.Length-1);
        for (int i = 0; i < foes.Length; i++)
        {
            if (i == randomN)
            {
                foes[i].SetActive(true);
            }
        }
    }

}
