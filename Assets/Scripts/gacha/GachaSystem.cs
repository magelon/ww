using UnityEngine;
using System;

public class GachaSystem : MonoBehaviour
{
    private static System.Random random = new System.Random();
    private int resultCounter = 0;

    public int GetGachaResult()
    {
        double randomNumber = random.NextDouble(); // Generate a random number between 0 and 1
        resultCounter = PlayerPrefs.GetInt("ResultCounter", 0);
        Debug.Log(resultCounter);
        if (randomNumber < 0.000014 || resultCounter >= 60)
        {
            // 0.0014 (0.14%) chance for 6, 7, 8 and 9 or when resultCounter reaches 100
            if (resultCounter >= 60)
            {
                resultCounter = 0; // Reset the counter after 100 results
                PlayerPrefs.SetInt("ResultCounter", resultCounter);
            }
            else
            {
                resultCounter++;
                PlayerPrefs.SetInt("ResultCounter", resultCounter);
            }
            return UnityEngine.Random.Range(7, 10);
        }
        else
        {
            // 99.986% chance for 0, 1, 2, 3, 4, 5
            resultCounter++;
            PlayerPrefs.SetInt("ResultCounter", resultCounter);
            return UnityEngine.Random.Range(0, 6);
        }
    }
}
