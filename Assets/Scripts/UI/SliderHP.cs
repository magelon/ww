using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderHP : MonoBehaviour
{
    private Slider sl;
    public Enime e;
    public float fullHP;
    void Start()
    {
        sl = GetComponent<Slider>();
        fullHP = e.health;
    }

    void Update()
    {
        sl.value = e.health / fullHP;
    }
}
