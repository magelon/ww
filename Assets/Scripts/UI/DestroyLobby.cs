using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyLobby : MonoBehaviour
{
    GameObject lobby;   
    void Start()
    {
        lobby = GameObject.Find("LobbyManager");
        Destroy(lobby);
    }

}
