using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
  public float parallaxEffect = 0.5f; // Adjust this value to control the movement speed

    private Transform mainCameraTransform;
    private Vector3 lastCameraPosition;

    void Start()
    {
        mainCameraTransform = Camera.main.transform;
        lastCameraPosition = mainCameraTransform.position;
    }

    void Update()
    {
        Vector3 cameraDelta = mainCameraTransform.position - lastCameraPosition;
        transform.position += new Vector3(cameraDelta.x * parallaxEffect, cameraDelta.y * parallaxEffect, 0);
        lastCameraPosition = mainCameraTransform.position;
    }
}
