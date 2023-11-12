using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoeSpawner : MonoBehaviour
{
    public Transform po;
    public GameObject yellow;
    public int timebetweenWave;
    void Start()
    {
        poolManager.instance.CreatePool(yellow, 20);
        InvokeRepeating("spawn", 3, timebetweenWave);
    }

    public void spawn()
    {
        yellow.layer = LayerMask.NameToLayer("Default");
        poolManager.instance.ReuseObject(yellow, new Vector3 (po.position.x,po.position.y,0), Quaternion.identity);
    }

}
