﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class clickHand : MonoBehaviour
{

    void OnMouseDown()
    {
        SceneManager.LoadScene("Index");
    }

}
