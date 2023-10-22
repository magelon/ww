using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hatScript : MonoBehaviour
{
    private SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetString("currentEquip") != null)
        {
            sr.sprite = Resources.Load<Sprite>("hats/"+ PlayerPrefs.GetString("currentEquip"));
        }
    }
}
