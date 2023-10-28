using UnityEngine;
using System;

public class GachaSystem : MonoBehaviour
{
    private static System.Random random = new System.Random();

    public int GetGachaResult()
    {
        double randomNumber = random.NextDouble(); // Generate a random number between 0 and 1

        if (randomNumber < 0.000014)
        {
            // 0.0014 (0.14%) chance for 6, 7, and 9
            return UnityEngine.Random.Range(7, 10);
        }
        else
        {
            // 99.986% chance for 0, 1, 2, 3, 4, 5, and 8
            return UnityEngine.Random.Range(0, 6);
        }
    }
}
