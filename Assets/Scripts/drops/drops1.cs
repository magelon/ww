using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drops1 : MonoBehaviour
{
    private static System.Random random = new System.Random();
    private Enime e;
    private Inventory1 i;
    public List<GameObject> dropsList;
    private bool droponce;
    // Start is called before the first frame update
    void Start()
    {
        e=GetComponent<Enime>();
        i=GetComponent<Inventory1>();
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
            double randomNumber = random.NextDouble();
            if(randomNumber < 0.0014){
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
}
