using UnityEngine;
using UnityEngine.Events;

public class InputDetector : MonoBehaviour
{
    public UnityEvent onTap; // Define an event to invoke when a tap occurs
    private int tapCount = 0;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            tapCount++;
            Debug.Log("Tap Count: " + tapCount);

            if (tapCount >= 10)
            {
                // Invoke the event when tapped 10 times
                if (onTap != null)
                {
                    onTap.Invoke();
                }
                PlayerPrefs.SetInt("coin", PlayerPrefs.GetInt("coin") + 1200);
                // You can add event listeners to handle the action in the Unity Inspector.
                tapCount = 0; // Reset the count if you want to track more taps
            }
        }
    }
}
