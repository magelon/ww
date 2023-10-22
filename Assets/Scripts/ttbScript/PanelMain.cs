﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
////using DG.Tweening;
using UnityEngine.SceneManagement;
public class PanelMain : MonoBehaviour {

		// game UI elements
		public Text btnStart,btnMore,btnReview;
		//public GameObject titleCN,titleEN;
		public Toggle toggleMusic,toggleSFX;
		public Image mask;
        
		// Use this for initialization
		void Start () {
				GameManager.getInstance ().init ();

				fadeOut ();


				toggleMusic.isOn = GameData.getInstance ().isSoundOn == 1 ? true : false;//0 is on
				toggleSFX.isOn = GameData.getInstance ().isSfxOn == 1 ? true : false;


				Localization.Instance.SetLanguage (GameData.getInstance().GetSystemLaguage());

				initView();
		}

		// Update is called once per frame
		void Update () {
			
		}
		public GameObject panelShop,panelFade;
		/// <summary>
		/// process kind of click events
		/// </summary>
		/// <param name="g">The green component.</param>
		public void OnClick(GameObject g){
				switch (g.name) {
				case "btnStart":
						GameManager.getInstance ().playSfx ("click");
						fadeIn ("LevelMenu");
						break;
				case "btnMore":
						GameManager.getInstance().playSfx("click");
						if (Application.platform == RuntimePlatform.WP8Player) {
						} else {

								#if (UNITY_IPHONE || UNITY_ANDROID)
								Application.OpenURL ("https://play.google.com/store/apps/details?id=com.lon.crossing"); 
								#endif
						}
						break;
				case "btnReview":
						GameManager.getInstance().playSfx("click");
						Application.OpenURL("https://discord.com/invite/bnFuFbzMaD");
                if (Application.platform == RuntimePlatform.WP8Player)
                {
                }
                else
                {

#if (UNITY_IPHONE || UNITY_ANDROID)
                    Application.OpenURL("https://guojindong.blogspot.com/2019/04/miniwarsimulator-privacy-policy.html");
#endif
                }
                break;
            case "btnShop":
						GameManager.getInstance().playSfx("click");
						panelShop.SetActive(true);
						break;
				case "btnGC":
						GameManager.getInstance().playSfx("click");
						GameManager.getInstance().ShowLeaderboard();
						break;
                case "Shop":
                GameManager.getInstance().playSfx("click");
                SceneManager.LoadScene("GachaMenu");
            
                break;
                case "pvp":
                GameManager.getInstance().playSfx("click");
                SceneManager.LoadScene("Lobby");
                break;
        }
		}


		void initView(){
        if (GameObject.Find("btnStart"))
        {
            GameObject.Find("btnStart").GetComponentInChildren<Text>().text = Localization.Instance.GetString("btnStart");
        }
				
			//	GameObject.Find ("btnMore").GetComponentInChildren<Text> ().text = Localization.Instance.GetString ("btnMore");
			//	GameObject.Find ("btnReview").GetComponentInChildren<Text> ().text = Localization.Instance.GetString ("btnReview");
		}

		/// <summary>
		/// process toggle button(music and sound effect buttons)
		/// </summary>
		/// <param name="toggle">Toggle.</param>
		public void OnToggle(Toggle toggle){
				switch (toggle.gameObject.name) {
				case "ToggleMusic":
						GameManager.getInstance().playSfx("click");
						GameData.getInstance().isSoundOn = toggle.isOn ? 1 : 0;

						if(toggle.isOn){
								GameManager.getInstance().stopBGMusic();
						}else{
								GameManager.getInstance().playMusic("bgmusic");
						}
						PlayerPrefs.SetInt("sound",GameData.getInstance().isSoundOn);

						break;
				case "ToggleSfx":
						GameManager.getInstance().playSfx("click");
						GameData.getInstance().isSfxOn = toggle.isOn ? 1 : 0;
						if(toggle.isOn){
								GameManager.getInstance().stopAllSFX();
						}

						PlayerPrefs.SetInt("sfx",GameData.getInstance().isSfxOn);
						break;
				}
		}


		void fadeOut(){
				mask.gameObject.SetActive (true);
				mask.color = Color.black;

				ATween.ValueTo (mask.gameObject, ATween.Hash ("from", 1, "to", 0, "time", 1, "onupdate", "OnUpdateTween", "onupdatetarget", this.gameObject, "oncomplete", "fadeOutOver","oncompletetarget",this.gameObject));

		}

		void fadeIn(string sceneName){
				if (mask.IsActive())
						return;
				mask.gameObject.SetActive (true);
				mask.color = new Color(0,0,0,0);

				ATween.ValueTo (mask.gameObject, ATween.Hash ("from", 0, "to", 1, "time", 1, "onupdate", "OnUpdateTween", "onupdatetarget", this.gameObject, "oncomplete", "fadeInOver", "oncompleteparams", sceneName,"oncompletetarget",this.gameObject));

		}


		void fadeInOver(string sceneName){
				SceneManager.LoadScene(sceneName);
		}

		void fadeOutOver(){
				mask.gameObject.SetActive (false);
		}

		/// <summary>
		/// tween update event
		/// </summary>
		/// <param name="value">Value.</param>
		void OnUpdateTween(float value)

		{

				mask.color = new Color(0,0,0,value);
		}
}
