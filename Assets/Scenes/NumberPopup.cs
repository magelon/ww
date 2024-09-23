using UnityEngine;
using TMPro; // Import TextMeshPro namespace
using System.Collections;

public class NumberPopup : MonoBehaviour
{
    public TMP_Text popupText; // Change to TMP_Text
    public float displayTime = 1f;
    public Vector3 offset = new Vector3(0, 1, 0);
    public float fadeDuration = 0.5f;

    public void ShowPopup(int number, Vector3 position)
    {
        popupText.text = number.ToString();
        transform.position = position + offset;
        gameObject.SetActive(true);
        StartCoroutine(PopupCoroutine());
    }

    private IEnumerator PopupCoroutine()
    {
        // Fade in
        float elapsedTime = 0f;
        Color textColor = popupText.color;
        textColor.a = 0;
        popupText.color = textColor;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            textColor.a = Mathf.Clamp01(elapsedTime / fadeDuration);
            popupText.color = textColor;
            yield return null;
        }

        // Wait for display time
        yield return new WaitForSeconds(displayTime);

        // Fade out
        elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            textColor.a = Mathf.Clamp01(1 - (elapsedTime / fadeDuration));
            popupText.color = textColor;
            yield return null;
        }

        gameObject.SetActive(false);
    }
    }
