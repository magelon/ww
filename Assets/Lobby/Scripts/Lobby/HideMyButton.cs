using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideMyButton : MonoBehaviour
{
    public GameObject myHome;
    public void hideMyButton()
    {
        myHome.SetActive(false);
    }
}
