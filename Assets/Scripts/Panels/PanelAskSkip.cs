using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PanelAskSkip : MonoBehaviour {

// Use this for initialization
//dfLabel lb_tipLeft,lb_skipins;
//gacha button script here


		public Transform panel;
		public GameObject panelNotEnough;
		public GameObject panelDisplayTip;
		public GameObject panelBuyCoin;

        public GameObject gachaButton;
        public GameObject gachaButtonX;
        public GameObject GachaGroup;
        public bool x;
        
        public GameObject spritePrefab; // Assign your Image prefab in the Inspector
        private GameObject instantiatedSprite;
        private SpriteRenderer spriteRenderer;
        public float fadeDuration = 2f;
   
        private void spawngodness()
        {
        // Instantiate the sprite object
        instantiatedSprite = Instantiate(spritePrefab, new Vector3(0, 0, 0), Quaternion.identity);

        // Get the SpriteRenderer component
        spriteRenderer = instantiatedSprite.GetComponent<SpriteRenderer>();
        
        if (spriteRenderer != null)
        {
            // Set initial alpha to 0 (fully transparent)
            //Color color = spriteRenderer.color;
            //color.a = 1;
            //spriteRenderer.color = color;
            
            // Start the fade-in process
            //StartCoroutine(FadeInAndDestroyRoutine());
        }
        else
        {
            Debug.LogError("No SpriteRenderer component found on the prefab.");
        }
        }

        IEnumerator FadeInAndDestroyRoutine()
        {
        float elapsedTime = 0f;

        // Fade in over the duration
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;

            // Calculate new alpha
            float newAlpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            Color color = spriteRenderer.color;
            color.a = newAlpha;
            spriteRenderer.color = color;

            yield return null; // Wait for the next frame
        }

        // Ensure the alpha is set to 1 (fully visible) at the end of the fade-in
        Color finalColor = spriteRenderer.color;
        finalColor.a = 0f;
        spriteRenderer.color = finalColor;

        // Destroy the sprite after fade-in
        Destroy(instantiatedSprite);
        }

	    public void showMe(){
			initView ();

				if (GameData.getInstance ().coin >= 60) {
                        
						panelNotEnough.SetActive (false);
						ATween.MoveTo (panel.gameObject, ATween.Hash ("ignoretimescale",true,"islocal", true, "y", 40, "time", 1f, "easeType", "easeOutExpo", "oncomplete", "OnShowCompleted", "oncompletetarget", this.gameObject));

						//disable some UI;

				} else {
                     if (GachaGroup)
                        {
                            GachaGroup.SetActive(false);
                        }
          
						panelNotEnough.SetActive (true);

						panelNotEnough.GetComponent<PanelNotEnough> ().showMe ();
					    
						gameObject.SetActive (false);

				}
				GameData.getInstance ().lockGame (true);
	}

    public void showMeX()
    {
        initViewX();

        if (GameData.getInstance().coin >= 1200)
        {
            panelNotEnough.SetActive (false);
			ATween.MoveTo (panel.gameObject, ATween.Hash ("ignoretimescale",true,"islocal", true, "y", 40, "time", 1f, "easeType", "easeOutExpo", "oncomplete", "OnShowCompleted", "oncompletetarget", this.gameObject));
        }
        else
        {
            if (GachaGroup)
            {
                GachaGroup.SetActive(false);
            }

            panelNotEnough.SetActive(true);

            panelNotEnough.GetComponent<PanelNotEnough>().showMeX();

            gameObject.SetActive(false);

        }
        GameData.getInstance().lockGame(true);
    }

    void dispalySpinResult()
    {
        panelDisplayTip.SetActive(true);
        
        panelDisplayTip.GetComponent<PanelDisplayTip>().showMe();
        //disable spin button
        //GachaGroup.SetActive(false);
    }

    void dispalySpinResultX()
    {
        
        panelDisplayTip.SetActive(true);
        panelDisplayTip.GetComponent<PanelDisplayTip>().showMeX();
        //disable spin button
        //GachaGroup.SetActive(false);
    }

    public void OnClick(GameObject g)
    {
        if (x)
        {
            switch (g.name)
            {
                case "btnCancel":
                    ATween.MoveTo(panel.gameObject, ATween.Hash("ignoretimescale", true, "islocal", true, "y", 460, "time", 1f, "easeType", "easeOutExpo", "oncomplete", "OnHideCompleted", "oncompletetarget", this.gameObject));

                    break;
                case "btnYes":
                    if (SceneManager.GetActiveScene().name == "GachaMenu" && GameData.getInstance().coin >= 1200)
                    {
                        GameData.getInstance().coin -= 1200;
                        PlayerPrefs.SetInt("coin", GameData.getInstance().coin);
                        ATween.MoveTo(panel.gameObject, ATween.Hash("ignoretimescale", true, "islocal", true, "y", 460, "time", 1f, "easeType", "easeOutExpo", "oncomplete", "OnHideCompleted", "oncompletetarget", this.gameObject));
                        GameData.getInstance().main.txtCoin.text = GameData.getInstance().coin.ToString();
                        //gacha animation
                        if (gachaButtonX)
                        {
                            spawngodness();
                            //GameManager.getInstance().playSfx("win");
                            //get result from it
                            Invoke("dispalySpinResultX", 2f);
                        }

                    }
                    else
                    {
                        if (GachaGroup)
                        {
                            GachaGroup.SetActive(false);
                        }

                        panelNotEnough.SetActive(true);

                        panelNotEnough.GetComponent<PanelNotEnough>().showMe();

                        gameObject.SetActive(false);

                    }
                    if (SceneManager.GetActiveScene().name != "GachaMenu")
                    {
                        GameData.getInstance().coin -= 60;
                        PlayerPrefs.SetInt("coin", GameData.getInstance().coin);
                        ATween.MoveTo(panel.gameObject, ATween.Hash("ignoretimescale", true, "islocal", true, "y", 460, "time", 1f, "easeType", "easeOutExpo", "oncomplete", "OnHideCompleted", "oncompletetarget", this.gameObject));
                        GameData.getInstance().main.txtCoin.text = GameData.getInstance().coin.ToString();
                        GameData.getInstance().main.nextLevelSkip();
                    }
                    break;

            }
        }
        else
        {
        // Add event handler code here
        switch (g.name)
        {
            case "btnCancel":
                ATween.MoveTo(panel.gameObject, ATween.Hash("ignoretimescale", true, "islocal", true, "y", 460, "time", 1f, "easeType", "easeOutExpo", "oncomplete", "OnHideCompleted", "oncompletetarget", this.gameObject));

                break;
            case "btnYes":
                if (SceneManager.GetActiveScene().name == "GachaMenu" && GameData.getInstance().coin >= 120)
                {
                    GameData.getInstance().coin -= 120;
                    PlayerPrefs.SetInt("coin", GameData.getInstance().coin);
                    ATween.MoveTo(panel.gameObject, ATween.Hash("ignoretimescale", true, "islocal", true, "y", 460, "time", 1f, "easeType", "easeOutExpo", "oncomplete", "OnHideCompleted", "oncompletetarget", this.gameObject));
                    GameData.getInstance().main.txtCoin.text = GameData.getInstance().coin.ToString();
                    //gacha animation
                    if (gachaButton)
                    {
                        spawngodness();
                        //add gacha animation here
                        //GameManager.getInstance().playSfx("win");
                        //get result from it
                        Invoke("dispalySpinResult", 2f);
                    }

                }
                else
                {
                    if (GachaGroup)
                    {
                       GachaGroup.SetActive(false);
                    }

                    panelNotEnough.SetActive(true);

                    panelNotEnough.GetComponent<PanelNotEnough>().showMe();

                    gameObject.SetActive(false);

                }
                if (SceneManager.GetActiveScene().name != "GachaMenu")
                {
                    GameData.getInstance().coin -= 60;
                    PlayerPrefs.SetInt("coin", GameData.getInstance().coin);
                    ATween.MoveTo(panel.gameObject, ATween.Hash("ignoretimescale", true, "islocal", true, "y", 460, "time", 1f, "easeType", "easeOutExpo", "oncomplete", "OnHideCompleted", "oncompletetarget", this.gameObject));
                    GameData.getInstance().main.txtCoin.text = GameData.getInstance().coin.ToString();
                    GameData.getInstance().main.nextLevelSkip();
                }
                break;

        }
    }
		
	}
		void initView(){

        if (SceneManager.GetActiveScene().name == "GachaMenu")
        {
            panel.transform.Find("skiptitle").GetComponent<Text>().text = "Spin to recruite new troops!";
            panel.transform.Find("skiptip").GetComponent<Text>().text = "One Spin cost 120";
        }
        else
        {
				panel.transform.Find ("skiptitle").GetComponent<Text> ().text = Localization.Instance.GetString ("askSkipTitle");
				panel.transform.Find ("skiptip").GetComponent<Text> ().text = Localization.Instance.GetString ("askSkipHit");
				panel.transform.Find ("btnYes").GetComponentInChildren<Text> ().text = Localization.Instance.GetString ("btnYes");
				panel.transform.Find ("btnCancel").GetComponentInChildren<Text> ().text = Localization.Instance.GetString ("btnCancel");
        }
    }

    void initViewX()
    {

        if (SceneManager.GetActiveScene().name == "GachaMenu")
        {
            panel.transform.Find("skiptitle").GetComponent<Text>().text = "Spin to recruite 10 new troops!";
            panel.transform.Find("skiptip").GetComponent<Text>().text = "One Spin cost 1200";
        }
        else
        {
            panel.transform.Find("skiptitle").GetComponent<Text>().text = Localization.Instance.GetString("askSkipTitle");
            panel.transform.Find("skiptip").GetComponent<Text>().text = Localization.Instance.GetString("askSkipHit");
            panel.transform.Find("btnYes").GetComponentInChildren<Text>().text = Localization.Instance.GetString("btnYes");
            panel.transform.Find("btnCancel").GetComponentInChildren<Text>().text = Localization.Instance.GetString("btnCancel");
        }
    }



    void OnHideCompleted(){
				gameObject.SetActive (false);
				GameData.getInstance ().lockGame (false);
		}
}
