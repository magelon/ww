using UnityEngine;
using UnityEngine.UI;

public class HideObjectsOnClick : MonoBehaviour
{
    public GameObject objectToHide1; // The first GameObject to hide
    public GameObject objectToHide2; // The second GameObject to hide (can be the button itself or another object)

    private Button button;
    public InventoryUI invui;
    public InventoryUI1 invui1;

    void Start()
    {
        // Get the Button component on this GameObject
        //button = GetComponent<Button>();

        // Add a listener to call HideObjects when the button is clicked
        //if (button != null)
        //{
            //button.onClick.AddListener(HideObjects);
        //}
    }

    public void HideObjects()
    {
        // Hide both objects
        if (objectToHide1 != null)
        {
            objectToHide1.SetActive(!objectToHide1.activeSelf);
            invui.UpdateUI();
        }

        if (objectToHide2 != null)
        {
            objectToHide2.SetActive(!objectToHide2.activeSelf);
        }
    }

    public void HideObjectsshowOne(){
         if (objectToHide2 != null)
        {
            objectToHide2.SetActive(!objectToHide2.activeSelf);
            invui.UpdateUI();
            invui1.UpdateUI();
        }
         if (objectToHide1 != null)
        {
            objectToHide1.SetActive(!objectToHide1.activeSelf);
            invui.UpdateUI();
            invui1.UpdateUI();
        }
    }
}
