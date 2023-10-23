using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dailytomainmenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Auto_change_scenes", 2f);

    }

    void Auto_change_scenes()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
