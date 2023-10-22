using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//for test 
public class mouseSpawner : MonoBehaviour
{
    private Vector3 po;
    public GameObject white;
    public GameObject yellow;

    private void Start()
    {
        poolManager.instance.CreatePool(white, 5);
        poolManager.instance.CreatePool(yellow, 5);
    }

    void Update()
    {
        po = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetButtonDown("Fire1"))
        {
            //spawn white
            poolManager.instance.ReuseObject(white, new Vector3(po.x, po.y, 0.05f), Quaternion.identity);
            //Instantiate(white, new Vector3(po.x,po.y,0.05f), Quaternion.identity);
        }
        if (Input.GetButtonDown("Fire2"))
        {
            //yellow
            poolManager.instance.ReuseObject(yellow, new Vector3(po.x, po.y, 0.05f), Quaternion.identity);
            //Instantiate(yellow, new Vector3(po.x, po.y, 0.05f), Quaternion.identity);
        }
    }
}
