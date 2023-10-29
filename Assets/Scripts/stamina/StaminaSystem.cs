using System;
using UnityEngine;
using UnityEngine.UI;

public class StaminaSystem : MonoBehaviour
{
    public int maxStamina = 100;
    public int staminaRegenerationRate = 1; // Stamina per minute
    public float timeToRegenerate = 60.0f; // Time to regenerate 1 stamina (in seconds)
    public Text staminaText; 

    private int currentStamina;
    private DateTime lastStaminaUpdateTime;

    private Slider sl;

    private void Start()
    {
        // Load saved stamina and last update time
        currentStamina = PlayerPrefs.GetInt("CurrentStamina",maxStamina);
        //Debug.Log(currentStamina);
        //UseStamina(20);
        sl = GetComponent<Slider>();

        UpdateSlider();

        long ticks = Convert.ToInt64(PlayerPrefs.GetString("LastStaminaUpdateTime", DateTime.Now.Ticks.ToString()));
        lastStaminaUpdateTime = new DateTime(ticks);

        // Calculate regenerated stamina since the last update
        TimeSpan timeSinceLastUpdate = DateTime.Now - lastStaminaUpdateTime;
        int regeneratedStamina = Mathf.FloorToInt((float)timeSinceLastUpdate.TotalSeconds / timeToRegenerate);

        // Update current stamina while respecting the maximum limit
        currentStamina = Mathf.Min(maxStamina, currentStamina + regeneratedStamina);

        // Save the current time as the last stamina update time
        lastStaminaUpdateTime = DateTime.Now;
        PlayerPrefs.SetString("LastStaminaUpdateTime", lastStaminaUpdateTime.Ticks.ToString());
        PlayerPrefs.SetInt("CurrentStamina", currentStamina);
    }

    public bool UseStamina(int amount)
    {
        if (currentStamina >= amount)
        {
            currentStamina -= amount;
            UpdateSlider();
            Debug.Log(currentStamina);
            // Update the last stamina update time
            lastStaminaUpdateTime = DateTime.Now;
            PlayerPrefs.SetString("LastStaminaUpdateTime", lastStaminaUpdateTime.Ticks.ToString());

            // Save the current stamina
            PlayerPrefs.SetInt("CurrentStamina", currentStamina);
            
            // Perform the action that consumes stamina
            return true;
        }
        else
        {
            // Not enough stamina, handle this as needed
            return false;
        }
    }

    private void Update()
    {
        // Check if it's time to regenerate stamina
        TimeSpan timeSinceLastUpdate = DateTime.Now - lastStaminaUpdateTime;
        int regeneratedStamina = Mathf.FloorToInt((float)timeSinceLastUpdate.TotalSeconds / timeToRegenerate);

        // Update current stamina while respecting the maximum limit
        currentStamina = Mathf.Min(maxStamina, currentStamina + regeneratedStamina);
        PlayerPrefs.SetInt("CurrentStamina", currentStamina);
        sl.value = (float)currentStamina / maxStamina;
    }

     private void UpdateSlider()
    {
        // Update the slider's value based on current stamina
        sl.value = (float)currentStamina / maxStamina;
         // Update the stamina text (optional)
        if (staminaText != null)
        {
            staminaText.text = "stamina: "+$"{currentStamina} / {maxStamina}";
        }
    }

}
