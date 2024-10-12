using UnityEngine;
using UnityEngine.UI; // Required for the Image component
using System.Collections;

public class SelfDestruct : MonoBehaviour
{
    public float fadeInDuration = 1f;   // Time to fade in
    public float fadeOutDuration = 1f;  // Time to fade out
    private Image image;                // Reference to the Image component

    void Start()
    {
        // Automatically find the Image component on the child object
        image = GetComponentInChildren<Image>();

        if (image != null)
        {
            // Start the fade-in and fade-out effect
            StartCoroutine(FadeInAndOutAndDestroy());
        }
        else
        {
            Debug.LogError("No Image component found on child object!");
        }
    }

    IEnumerator FadeInAndOutAndDestroy()
    {
        Color originalColor = image.color; // Store the original color of the image

        // FADE IN
        float elapsedTime = 0f;
        while (elapsedTime < fadeInDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = elapsedTime / fadeInDuration;  // Gradually increase alpha
            image.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        // Ensure alpha is fully 1 at the end of the fade-in
        image.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1f);

        // FADE OUT
        elapsedTime = 0f;
        while (elapsedTime < fadeOutDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = 1 - (elapsedTime / fadeOutDuration); // Gradually decrease alpha
            image.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        // Ensure alpha is set to 0 at the end of the fade-out
        image.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);

        // Destroy the GameObject after both fades are complete
        Destroy(this.gameObject);
    }
}
