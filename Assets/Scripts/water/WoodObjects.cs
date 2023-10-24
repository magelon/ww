using UnityEngine;

public class FloatingObject : MonoBehaviour
{
    public float floatHeight;
    public float liftForce;
    public float damping;
    public LayerMask waterLayer;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component not found. Make sure it's attached to the GameObject.");
        }
    }

    private void FixedUpdate()
    {
        if (rb == null)
            return;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity, waterLayer);

        if (hit.collider != null)
        {
            Debug.DrawRay(transform.position, Vector2.down * hit.distance, Color.yellow);

            float distance = Mathf.Abs(hit.point.y - transform.position.y);
            float heightError = floatHeight - distance;

            float force = liftForce * heightError - rb.velocity.y * damping;
            rb.AddForce(Vector2.up * force);
        }
    }
}
