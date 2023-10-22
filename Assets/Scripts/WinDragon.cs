using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinDragon : MonoBehaviour
{
    private Enime e;
    private bool win;
    private void Start()
    {
        e = GetComponent<Enime>();
    }
    void Update()
    {
        if (e.health <= 0&&!win)
        {
            win = true;
            GameData.getInstance().main.gameWin();
            //Destroy(this.gameObject);
        }
        
    }
}
