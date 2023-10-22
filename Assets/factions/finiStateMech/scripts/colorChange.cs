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
        if (en.f == Factions.yellow)
        {
            if (m_SpriteRenderer.color == Color.white)
            {
                m_SpriteRenderer.color = Color.yellow;
            }
        }
        else if (en.f == Factions.red)
        {
            if (m_SpriteRenderer.color == Color.white)
            {
                m_SpriteRenderer.color = Color.red;
            }
        }

    }
}
