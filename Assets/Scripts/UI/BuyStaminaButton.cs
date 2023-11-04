using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyStaminaButton : MonoBehaviour
{
    public StaminaSystem ss;
    public Text txtCoin;
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
            txtCoin.text = GameData.getInstance ().coin.ToString();
            PlayerPrefs.SetInt("CurrentStamina", 100);
            Debug.Log(PlayerPrefs.GetInt("CurrentStamina"));
            ss.UpdateSlider();
        }
            
    }
}
