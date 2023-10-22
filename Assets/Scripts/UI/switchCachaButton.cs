using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class switchCachaButton : MonoBehaviour
{
    public Button gachaButton;
    public Text gachaText;
    public Button gachaButtonX;
    public PanelAskSkip pas;
   

    public RectTransform gachaButtonTransform;
    public RectTransform gachaButtonXTransform;

    private bool switchBool=false;

    public void switchButton(){
        if (switchBool == false)
        {
            switchBool = true;
            gachaButtonXTransform.SetAsLastSibling();
            gachaButton.interactable = false;
            gachaButtonX.interactable = true;
            gachaText.text = "Recurit 10  \n Button";
            pas.x = true;
        }
        else if (switchBool == true)
        {
            switchBool = false;
            gachaButtonTransform.SetAsLastSibling();
            gachaButton.interactable = true;
            gachaButtonX.interactable = false;
            gachaText.text = "Recurit   \n Button";
            pas.x = false;
        }

    }
}
