using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyStaminaButton : MonoBehaviour
{
    public StaminaSystem ss;
    private int currentStamina;
    // Start is called before the first frame update
    void Start()
    {
        currentStamina = PlayerPrefs.GetInt("CurrentStamina",100);
    }

    public void buyStamina(){
        if(currentStamina<20){
            Debug.Log("buy stamina");
            int coin = PlayerPrefs.GetInt("coin");
            PlayerPrefs.SetInt("coin", coin-20);
            PlayerPrefs.SetInt("CurrentStamina", 100);
            ss.UpdateSlider();
        }
            
    }
}
