using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slashonce : MonoBehaviour
{
     public float duration = 1f;
    
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, duration);  
    }

}
