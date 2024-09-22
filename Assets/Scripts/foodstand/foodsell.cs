using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class foodsell : MonoBehaviour
{
    public foodstandsystem fs;
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Print the name of the object that enters the trigger
        Debug.Log("Object Entered Trigger: " + other.gameObject.name);
        if(fs){
            fs.itemsold();
        }
    }

}
