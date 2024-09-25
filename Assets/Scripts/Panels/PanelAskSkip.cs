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

            panelNotEnough.SetActive(false);
            ATween.MoveTo(panel.gameObject, ATween.Hash("ignoretimescale", true, "islocal", true, "y", 40, "time", 1f, "easeType", "easeOutExpo", "oncomplete", "OnShowCompleted", "oncompletetarget", this.gameObject));

            //disable some UI;

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
                            if(gachaButtonX.GetComponent<Animator>()){
                            gachaButtonX.GetComponent<Animator>().SetTrigger("spin");
                            }
                            
                            //GameManager.getInstance().playSfx("win");
                            //get result from it
                            Invoke("dispalySpinResultX", 1f);
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
                        //gachaButton.GetComponent<Animator>().SetTrigger("spin");
                        //GameManager.getInstance().playSfx("win");
                        //get result from it
                        Invoke("dispalySpinResult", 1f);
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
