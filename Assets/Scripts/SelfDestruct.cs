using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    public float destroyAfterSeconds = 2f; // Time before the object is destroyed

    void Start()
    {
        // Destroy the GameObject after `destroyAfterSeconds`
        Destroy(gameObject, destroyAfterSeconds);
    }
}
