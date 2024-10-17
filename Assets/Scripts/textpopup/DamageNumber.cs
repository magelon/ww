using UnityEngine;
using TMPro;

public class DamageNumber : MonoBehaviour
{
    public float duration = 3f;
    public float floatSpeed = 0.03f;
    public float fallSpeed = 0.5f; // speed at which the number falls
    public float popUpDuration = 1.5f; // how long the number pops up before falling
    public Vector3 offset = new Vector3(0,(float)0.1, 0);
    public float randomRange = 0.05f; // range for random popping
    public AnimationCurve sizeCurve; // use an animation curve to control size based on time
    [SerializeField] private TextMeshProUGUI text;
    
    private float elapsedTime = 0f;
    private Vector3 randomOffset;

    void Start()
    {
        Destroy(gameObject, duration);
        randomOffset = new Vector3(
            Random.Range(-randomRange, randomRange),
            Random.Range(-randomRange, randomRange),
            0
        );
    }

    void Update()
    {
         elapsedTime += Time.deltaTime;

        // Movement logic: Pop up first, then fall down
        if (elapsedTime < popUpDuration)
        {
            // Pop-up phase
            transform.position += (offset + randomOffset) * floatSpeed * Time.deltaTime;
        }
        else
        {
            // Falling phase
            transform.position -= Vector3.up * fallSpeed * Time.deltaTime;
        }

        // Apply shrinking effect over time using the animation curve
        float normalizedTime = elapsedTime / duration;
        float scale = sizeCurve.Evaluate(normalizedTime); // assuming curve goes from 1 to 0
        transform.localScale = new Vector3(scale, scale, scale);

        // Optional: Fade the color (alpha) over time for a fade-out effect
        Color currentColor = text.color;
        currentColor.a = Mathf.Lerp(1f, 0f, normalizedTime);
        text.color = currentColor;
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
            case "steam":
                textColor = new Color(1f, 0.5f, 0.75f); // reaction
                break;
            case "conductivity":
                textColor = new Color(0f, 1f, 0.5f); // reaction
                break;
            case "burn":
                textColor = new Color(1f, 0.5f, 0f); // reaction
                break;
            case "overload":
                textColor = new Color(1f, 0.4f, 0.2f); // reaction
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
