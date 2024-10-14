using UnityEngine;
using TMPro;

public class DamageNumber : MonoBehaviour
{
    public float duration = 1f;
    public float floatSpeed = 0.3f;
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

    public void SetValue(int damage,string element)
    {
         if (text != null)
        {
             Color textColor;

            switch (element.ToLower())
            {
            case "fire":
                textColor = Color.red;  // Set to red for "fire"
                break;
            case "water":
                textColor = Color.blue; // Set to blue for "water"
                break;
            case "grass":
                textColor = Color.green; // Set to green for "earth"
                break;
            case "rock":
                textColor = new Color(0.545f, 0.271f, 0.075f);
                break;
            case "light":
                textColor = Color.yellow; // Set to cyan for "wind"
                break;
            case "dark":
                textColor = Color.black; // Set to cyan for "wind"
                break;
            case "ice":
                textColor = Color.cyan; // Set to cyan for "wind"
                break;
            case "electric":
                textColor = new Color(0.5f, 0f, 0.5f); // Set to cyan for "wind"
                break;                  
            default:
                textColor = Color.white; // Default color if no match
                break;
            }

            // Apply the color and set the damage text
            text.color = textColor;
            // Update the text to display the damage value
            text.text ="-"+damage.ToString();
        }
        else
        {
            Debug.LogError("TextMeshProUGUI component is missing!");
        }
    }
}
