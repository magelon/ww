using UnityEngine;

public class Example : MonoBehaviour
{
    public NumberPopup numberPopup;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Example trigger
        {
            numberPopup.ShowPopup(100, Camera.main.WorldToScreenPoint(transform.position));
        }
    }
}
