using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CallmenuButton : MonoBehaviour
{
    public Transform panel;
    public GameObject CloseButton;

    public void openMenuGroup()
    {
        Time.timeScale = 0;
        ATween.MoveTo(panel.gameObject, 
            ATween.Hash("ignoretimescale", true, "islocal", true, "y", 0,
            "time", 1f, "easeType", "easeOutExpo", "oncomplete",
            "OnShowCompleted", "oncompletetarget", this.gameObject));
        CloseButton.SetActive(true);
    }

    public void closeMenuGroup()
    {
        Time.timeScale = 1;
        ATween.MoveTo(panel.gameObject,
          ATween.Hash("ignoretimescale", true, "islocal", true, "y", 1280,
          "time", 1f, "easeType", "easeOutExpo", "oncomplete",
          "OnShowCompleted", "oncompletetarget", this.gameObject));
        CloseButton.SetActive(false);
    }

}
