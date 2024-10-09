using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;


public class StartSplash : MonoBehaviour {

	// Use this for initialization
	void Start () {
                //PlayerPrefs.DeleteAll();
                if(!PlayerPrefs.HasKey("firsttime")){
                        PlayerPrefs.SetInt("firsttime",1);
                        Transform dialogTransform = transform.Find("dialog");
				if (dialogTransform != null)
				{
				// Get the GameObject of the child
    			        GameObject dialog = dialogTransform.gameObject;
    			        // Disable the dialog GameObject
    			        dialog.SetActive(true);
				}
                }else{
                        string date = DateTime.Now.ToString().Substring(0, 9);
                        if (PlayerPrefs.GetInt(date) == 1)
                        {
                        SceneManager.LoadScene("MainMenu");
                        }else{
                        SceneManager.LoadScene("DailyBonus");
                        }
                }
                
        //		MadLevelProfile.Reset ();
        //		bool istrial = UnityPluginForWindowsPhone.Class1.SwitchTrialMode();//comment this when on real version.
	}
	

}
