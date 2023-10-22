using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class moveUpfade : MonoBehaviour
{
    public float delta = 0.2f;
    public float speed = 0.2f;
    private Rigidbody2D rb2d;
    private Vector3 startPos;
    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
       
        startPos = transform.position;
    }

    void Update()
    {
        transform.Translate(0, Time.deltaTime*speed, 0, Space.World);
        //Color imageCol = new Color();
        //imageCol = GetComponent<SpriteRenderer>().color;
        //imageCol.a = Mathf.Lerp(1, 0, 2);
        //GetComponent<SpriteRenderer>().color = imageCol;
    }
}
