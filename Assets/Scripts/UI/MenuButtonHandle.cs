using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MenuButtonHandle : MonoBehaviour
{
    public GameObject loadingImg;
    public Slider sl;


    public void LoadScense(GameObject g)
    {
        switch (g.name)
        {
            case "HomeButton":
                Time.timeScale = 1;
                GameManager.getInstance().playSfx("click");
                ChangeScene("MainMenu");
                break;
            case "BarracksButton":
                Time.timeScale = 1;
                GameManager.getInstance().playSfx("click");
                ChangeScene("TroopsMenu");
                break;
            case "RecuritButton":
                Time.timeScale = 1;
                GameManager.getInstance().playSfx("click");
                ChangeScene("GachaMenu");
                break;
            case "LevelButton":
                Time.timeScale = 1;
                GameManager.getInstance().playSfx("click");
                ChangeScene("LevelMenu");
                break;
            case "TroopsIndexButton":
                Time.timeScale = 1;
                GameManager.getInstance().playSfx("click");
                ChangeScene("Index");
                break;
            case "ArenaButton":
                Time.timeScale = 1;
                GameManager.getInstance().playSfx("click");
                ChangeScene("Lobby");
                break;
            case "MoreGameButton":
                GameManager.getInstance().playSfx("click");
                if (Application.platform == RuntimePlatform.WP8Player)
                {
                }
                else
                {

                #if (UNITY_IPHONE || UNITY_ANDROID)
                    Application.OpenURL("https://play.google.com/store/apps/details?id=com.lon.crossing");
                #endif
                }
                break;
        }
    }

    private void ChangeScene(string s)
    {
        StartCoroutine(LoadAsynchronously(s));
    }

    IEnumerator LoadAsynchronously(string s)
    {
        loadingImg.SetActive(true);
        AsyncOperation operation = SceneManager.LoadSceneAsync(s);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            sl.value = progress;
            yield return null;
        }
    }
}
