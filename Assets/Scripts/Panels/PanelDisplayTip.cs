using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//gacha result script
public class PanelDisplayTip : MonoBehaviour {

    
    // Use this for initialization
    string tipContextType = "";
    public Text xpText;
	private	Transform panel;
    public int dispalyX=0;
	bool locker;
    public GachaSystem gs;

	public void close(){
			ATween.MoveTo (panel.gameObject, ATween.Hash ("ignoretimescale",true,"islocal", true, "y", 500, "time", 1f, "easeType", "easeOutExpo", "oncomplete", "OnHideCompleted", "oncompletetarget", this.gameObject));
            if (dispalyX>0)
            {
                dispalyX--;
                showMeX();
            }
		}

	public void showMe(){
        //GetComponent<dfPanel> ().IsVisible = isVis;
		//show tip freely during level if once displayed.
				
        //HandleSpinButton();
                
                panel = transform.Find ("panel");

				initView ();

				GameData.getInstance().cLvShowedTip = true;
		
				ATween.MoveTo (panel.gameObject, ATween.Hash ("ignoretimescale",true,"islocal", true, "y", 40, "time", 1f, "easeType", "easeOutExpo","oncomplete", "OnShowCompleted", "oncompletetarget", this.gameObject));
		
				GameData.getInstance ().lockGame (true);

	}

    public void showMeX()
    {
        //GetComponent<dfPanel> ().IsVisible = isVis;
        //show tip freely during level if once displayed.

        panel = transform.Find("panel");

        initView();

        GameData.getInstance().cLvShowedTip = true;

        ATween.MoveTo(panel.gameObject, ATween.Hash("ignoretimescale", true, "islocal", true, "y", 40, "time", 1f, "easeType", "easeOutExpo", "oncomplete", "OnShowCompleted", "oncompletetarget", this.gameObject));

        GameData.getInstance().lockGame(true);

    }

    //gacha
    string SpinResult()
    {
        //Debug.Log(GameData.totalItem);
        string result=gs.GetGachaResult();
        Debug.Log(result);
        return result;
        //Random.Range(0, GameData.totalItem);
    }

    void initView()
    {
        if (SceneManager.GetActiveScene().name == "GachaMenu")
        {
            panel.transform.Find("txtDisplayTipTitle").GetComponent<Text>().text = "Result";
            string result = SpinResult();
            panel.transform.Find("txtDisplayTipContent").GetComponent<Text>().text = result+" ";
            panel.transform.Find("resultIllstrate").GetComponent<Image>().sprite=Resources.Load<Sprite>("sumPrefabs/illistrate/" +result);
            panel.transform.Find("resultImg").GetComponent<Image>().sprite = Resources.Load<Sprite>("sumPrefabs/itemImgs/" +result);
            if (PlayerPrefs.HasKey(result))
            {
                int xp = PlayerPrefs.GetInt("totalXp");
                xp += 500;
                PlayerPrefs.SetInt("totalXp", xp);
                if (xpText)
                {
                    xpText.text = "Exp: " + xp;
                }
               
            }
            else
            {
                PlayerPrefs.SetInt(result, 1);
                PlayerPrefs.SetInt(result+"Level",1);
                GameManager.getInstance().init();
            }
           
        }
        else { 
        panel.transform.Find("txtDisplayTipTitle").GetComponent<Text>().text = Localization.Instance.GetString("tipTitle");
        panel.transform.Find("txtDisplayTipContent").GetComponent<Text>().text = Localization.Instance.GetString("level" + GameData.getInstance().cLevel + "tip");
        panel.transform.Find("btnClose").GetComponentInChildren<Text>().text = Localization.Instance.GetString("btnClose");
            }	
		}

		void OnHideCompleted(){
				gameObject.SetActive (false);
				GameData.getInstance ().lockGame (false);
		}

		void OnShowCompleted(){
				GameData.getInstance ().lockGame (true);
		}

}
