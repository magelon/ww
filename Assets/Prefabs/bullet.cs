﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public Factions f;
    public float damage;
    public string element;
    public LayerMask foeLayer;

    private void OnEnable()
    {
        damage = 1;
    }

    private void Update()
    {
        Collider2D[] enimes = Physics2D.
              OverlapCircleAll(this.transform.position, 0.01f, foeLayer);
        if (Physics2D.OverlapCircle(this.transform.position, 0.01f, foeLayer))
        {
            for (int i = 0; i < enimes.Length; i++)
            {
                if (enimes[i].gameObject.GetComponent<Enime>()) {
                    if (enimes[i].gameObject.GetComponent<Enime>().f
                   != f)
                    {
                        if (enimes[i].gameObject.GetComponent<Enime>())
                        {
                            if (enimes[i].gameObject.name.Substring(0, 5) != "Drago" && enimes[i].gameObject.name.Substring(4, 1) != "6")
                            {
                                //GameManager.getInstance().playSfx("pounch");
                            }
                            enimes[i].gameObject.GetComponent<Enime>().damage((int)damage,element);
                            enimes[i].gameObject.GetComponent<TankAI>().clostDistance = 9999;
                            enimes[i].gameObject.GetComponent<TankAI>().GetClosest();
                            this.gameObject.SetActive(false);
                        }
                    }
                }
               
            }
        }

    }

}
