using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttributeInfor : MonoBehaviour
{
    public string Troopsname;
    public int health;
    public float energyCost;
    public float speed;
    public string rate;
    public int damage;
    private bool hide;
    public string artist;

    public Text t;
    public Text h;
    public Text c;
    public Text s;
    public Text r;
    public Text d;
    public Text a;

    void OnEnable()
    {
        hide = false;
        if (t)
        {
            t.text = "Name: " + Troopsname;
            h.text = "HP: " + health;
            c.text = "EnergyCost: " + energyCost;
            s.text = "Speed: " + speed;
            r.text = "Rarity: " + rate;
            d.text = "Damage: " + damage;
            if (artist!="")
            {
                a.text = "Artist: " + artist;
            }
            else
            {
                a.text = "Artist: lon";
            }
            
        }
    }

    public void HiddenInfo()
    {
        t.text = "Name:??? ";
        h.text = "HP: ??";
        c.text = "EnergyCost:??? ";
        s.text = "Speed: ??";
        r.text = "Rarity: ??";
        d.text = "Damage: ??";
    }
    private void Update()
    {
        if (GetComponent<SpriteRenderer>().color.r == 0&&!hide)
        {
            hide = true;
            HiddenInfo();
        }
    }
}
