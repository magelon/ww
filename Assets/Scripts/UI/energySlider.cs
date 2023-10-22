using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class energySlider : MonoBehaviour
{
    public Slider sl;
    public Text slt;
    public float energy;

    void Start()
    {
        energy = 0;

    }


    void Update()
    {
        if (energy < 1)
        {
            energy += 0.002f;
            Mathf.Clamp(energy, 0, 1);
        }
        sl.value = energy;
        slt.text = (int)(energy * 100) + "/100" + " Energy";
    }
}
