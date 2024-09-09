using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drops : MonoBehaviour
{

    private Enime e;
    private Inventory i;
    public GameObject drop;
    private bool droponce;
    // Start is called before the first frame update
    void Start()
    {
        e=GetComponent<Enime>();
        i=GetComponent<Inventory>();
        if(drop){
            poolManager.instance.CreatePool(drop, 10);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(e.health<=0 && !droponce){
            droponce=true;
            poolManager.instance.ReuseObject(drop, this.gameObject.transform.position, Quaternion.identity);
            i.LoadInventory();
            //Debug.Log(drop.name);
            i.AddItem(drop.name);
        }
    }
}
