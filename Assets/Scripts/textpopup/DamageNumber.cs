using UnityEngine;
using TMPro;

public class DamageNumber : MonoBehaviour
{
    public float duration = 1f;
    public float floatSpeed = 1f;
    public Vector3 offset = new Vector3(0, 1, 0);
    [SerializeField] private TextMeshProUGUI text; 

    void Start()
    {
        Destroy(gameObject, duration);
    }

    void Update()
    {
        transform.position += offset * floatSpeed * Time.deltaTime;
    }

    public void SetValue(int damage)
    {
         if (text != null)
        {
            // Update the text to display the damage value
            text.text = damage.ToString();
        }
        else
        {
            Debug.LogError("TextMeshProUGUI component is missing!");
        }
    }
}
