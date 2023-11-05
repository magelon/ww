using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gemFade : MonoBehaviour
{
     public float initialSpeed = 0.2f; // Initial speed (slow).
    public float finalSpeed = 5.0f;   // Final speed (fast).

    private Vector2 targetPosition;
    private float currentSpeed;

    void Start()
    { 
        // Calculate the target position at the top-left corner of the screen in 2D space.
        targetPosition = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0));

        // Initialize the current speed to the initial speed.
        currentSpeed = initialSpeed;
    }

    private void Update()
    {
       // Calculate the step based on currentSpeed and time.
        float step = currentSpeed * Time.deltaTime;

        // Move the GameObject towards the target position in 2D space.
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, step);

        // Gradually increase the speed as the GameObject gets closer to the target.
        float distanceToTarget = Vector2.Distance(transform.position, targetPosition);
        currentSpeed = Mathf.Lerp(initialSpeed, finalSpeed, 1 - (distanceToTarget / Vector2.Distance(Vector2.zero, targetPosition)));
    }
}
