using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class followSliderHP : MonoBehaviour
{
    private Slider sl;
    public Enime e;
    public float fullHP;

    public Transform target;       // The game object that the slider will follow (e.g., player or enemy)
    public Camera mainCamera;      // The main camera used to convert world positions to screen positions
    public Slider slider;          // The UI slider

    private RectTransform canvasRectTransform;  // The canvas' RectTransform
    private RectTransform sliderRectTransform; 

    void Start()
    {
        mainCamera=Camera.main;
        sl = GetComponent<Slider>();
        fullHP = e.health;
        sliderRectTransform = slider.GetComponent<RectTransform>();
        canvasRectTransform = slider.GetComponentInParent<Canvas>().GetComponent<RectTransform>();
    }

    void Update()
    {
        sl.value = e.health / fullHP;
        Vector3 screenPos = mainCamera.WorldToScreenPoint(target.position);

        // If the target is behind the camera, hide the slider
        if (screenPos.z < 0)
        {
            slider.gameObject.SetActive(false);  // Hide slider if the target is behind the camera
        }
        else
        {
            slider.gameObject.SetActive(true);   // Show slider if the target is visible

            // Convert screen position to canvas space
            Vector2 anchoredPos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, screenPos, mainCamera, out anchoredPos);

            // Move the slider to follow the game object
            sliderRectTransform.anchoredPosition = anchoredPos;
        }
    }
   
}
