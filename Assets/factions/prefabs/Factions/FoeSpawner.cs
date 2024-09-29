using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoeSpawner : MonoBehaviour
{
    public Transform po;
    public GameObject yellow;
    public int timebetweenWave;

    public Image illstrate; // For UI Image, use this (optional)
    public float fadeDuration = 2f; // Duration of the fade-out in seconds
    private float startAlpha = 1f; // Initial alpha, set to fully opaque
    private Vector3 startScale = new Vector3(4f, 4f, 1f); // Initial scale (2x size)
    private Vector3 endScale = new Vector3(1f, 1f, 1f); // Final scale (1x size)

    private Sprite originalSprite;
    private Vector3 originalScale;
    private Color originalColor;

    public GameObject foespawnchick;

    void Start()
    {
        poolManager.instance.CreatePool(yellow, 20);
        InvokeRepeating("spawn", 3, timebetweenWave);
        if (illstrate != null)
        {
            originalSprite = illstrate.sprite;
            originalColor = illstrate.color;
            originalScale = illstrate.transform.localScale;
        }
    }

    public void spawn()
    {
        yellow.layer = LayerMask.NameToLayer("Default");
        poolManager.instance.ReuseObject(yellow, new Vector3 (po.position.x,po.position.y,0), Quaternion.identity);
        if (illstrate != null){
        ResetUIImage();
        illstrate.sprite=Resources.Load<Sprite>("sumPrefabs/illistrate/" + yellow.name);
        if (foespawnchick.activeInHierarchy){
        StartCoroutine(FadeOutAndShrinkUIImage());
        }
        }
                
    }

    private void ResetUIImage()
    {
        illstrate.color = new Color(originalColor.r, originalColor.g, originalColor.b, startAlpha); // Reset alpha
        illstrate.transform.localScale = startScale;  // Reset scale to the larger size
    }

    IEnumerator FadeOutAndShrinkUIImage()
    {
        Color color = illstrate.color;
        float elapsedTime = 0f;

        // Set initial alpha and scale
        color.a = startAlpha;
        illstrate.color = color;
        illstrate.transform.localScale = startScale;

        // Loop over time to fade alpha down to 0 and scale down to 1
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / fadeDuration;

            // Update alpha
            color.a = Mathf.Lerp(startAlpha, 0f, progress);
            illstrate.color = color;

            // Update scale
            illstrate.transform.localScale = Vector3.Lerp(startScale, endScale, progress);

            yield return null;
        }

        // Ensure it is fully transparent and at the final scale at the end
        color.a = 0f;
        illstrate.color = color;
        illstrate.transform.localScale = endScale;
    }

}
