using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveSinup : MonoBehaviour
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
        transform.Translate( Mathf.Sin(Time.time)*delta*speed*0.3f, Time.deltaTime*speed*5, 0, Space.World);

    }
}
