using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drops : MonoBehaviour
{

    private Enime e;
    private Inventory i;
    public List<GameObject> dropsList;
    private bool droponce;
    // Start is called before the first frame update
    void Start()
    {
        e=GetComponent<Enime>();
        i=GetComponent<Inventory>();
        if(poolManager.instance != null && dropsList != null && dropsList.Count > 0){
            foreach (var drop in dropsList)
            {
                poolManager.instance.CreatePool(drop, 10);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(e != null && e.health <= 0 && !droponce){
            droponce=true;
             // Randomly select a drop from the list
            int randomIndex = Random.Range(0, dropsList.Count);
            GameObject randomDrop = dropsList[randomIndex];
            // Reuse the randomly selected object
            poolManager.instance.ReuseObject(randomDrop, this.gameObject.transform.position, Quaternion.identity);

            i.LoadInventory();
            //Debug.Log(drop.name);
            i.AddItem(randomDrop.name);
        }
    }
}
