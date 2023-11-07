using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colorChange : MonoBehaviour
{
    SpriteRenderer m_SpriteRenderer;
    Enime en;
    // Start is called before the first frame update
    void Start()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        en = GetComponent<Enime>();
    }

    // Update is called once per frame
    void Update()
    {
       if (en.f == Factions.red)
        {
            if (m_SpriteRenderer.color == Color.white)
            {
                Color randomColor = GetRandomColor();
                m_SpriteRenderer.color = randomColor;
            }
        }

    }

    private Color GetRandomColor()
    {
        // Generate random values for red, green, and blue between 0 and 1
        float randomRed = Random.Range(0f, 1f);
        float randomGreen = Random.Range(0f, 1f);
        float randomBlue = Random.Range(0f, 1f);

        // Create a random color using the generated values
        return new Color(randomRed, randomGreen, randomBlue);
    }
}
