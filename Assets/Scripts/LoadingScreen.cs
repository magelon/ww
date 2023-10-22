using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    public GameObject loadingImg;
    public Slider sl;
    public string loadingScene;
    void Start()
    {
        //AsyncOperation operation= SceneManager.LoadSceneAsync("level1ForWTS");
        //StartCoroutine(LoadAsynchronously());
    }

    public void changeScene(string lv)
    {
        StartCoroutine(LoadAsynchronously(lv));
    }

    IEnumerator LoadAsynchronously(string lv)
    {
        loadingImg.SetActive(true);
        AsyncOperation operation = SceneManager.LoadSceneAsync(lv);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            sl.value = progress;
            //Debug.Log(progress);
            yield return null;
        }
    }

}
