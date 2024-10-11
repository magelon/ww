using UnityEngine;
using UnityEngine.UI;

public class deActivateObjectOnClick : MonoBehaviour
{
    public GameObject targetObject;  // Reference to the GameObject to activate

    private Button button;  // Reference to the Button component

    void Start()
    {
        // Get the Button component attached to the same GameObject
        button = GetComponent<Button>();

        // Check if the button and the targetObject are assigned
        if (button != null && targetObject != null)
        {
            // Add a listener to the button to call ActivateObject when clicked
            button.onClick.AddListener(ActivateObject);
        }
    }

    // Function to activate the GameObject
    void ActivateObject()
    {
        // Check if the object is not active, and then activate it
        if (!targetObject.activeSelf)
        {
            
            Debug.Log("GameObject activated!");
        }
        else
        {
            targetObject.SetActive(false);
            Debug.Log("GameObject is already active.");
        }
    }
}
