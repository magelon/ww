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
            case "InventoryButton":
                Time.timeScale = 1;
                GameManager.getInstance().playSfx("click");
                ChangeScene("Inventory");
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
        operation.allowSceneActivation = false; // Prevent auto scene activation

         float loadingTime = 0f;

        // Loop until the operation is done or 2 seconds have passed
        while (!operation.isDone)
        {
            // Increment the loading timer
            loadingTime += Time.deltaTime;

            // Calculate progress (0.9f is the threshold before the scene is activated)
            float progress = Mathf.Clamp01(operation.progress / 2f);
            sl.value = progress;

            // Ensure a minimum of 2 seconds before allowing scene activation
            if (operation.progress >= 0.9f && loadingTime >= 2f)
            {
                operation.allowSceneActivation = true; // Activate the scene
            }

            yield return null;
        }
    }
}
