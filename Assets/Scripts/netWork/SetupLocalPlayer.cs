// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.Networking;

// public class SetupLocalPlayer : NetworkBehaviour
// {
//     [SyncVar]
//     public string pname ="player";
//     [SyncVar]
//     public Color playerColor = Color.black;

//     public GameObject l;
//     public GameObject r;

//     //public GameObject g;
//     public GameObject go;

//     private void Start()
//     {
//         if (isLocalPlayer)
//         {
//             if (playerColor == Color.yellow)
//             {
//                 r.SetActive(true);
//             }
//             if (playerColor == Color.red)
//             {
//                 l.SetActive(true);
//             }
//         }
       
//     }

//     [Command]
//     public void CmdFire()
//     {
//         //GameObject go = Instantiate(g, Vector3.zero, Quaternion.identity);
//         //NetworkServer.Spawn(g);
//     }

//     [Command]
//     public void CmdSpawn(string s)
//     {
//         Debug.Log(s);
//         string pre = s.Substring(0, 1);
//         string item = s.Substring(1);
//         go= Resources.Load<GameObject>("netWorkprefabs/"+item+pre);
//         if (pre == "r")
//         {
//             GameObject goo = Instantiate(go, new Vector3(-1.4f, 0, 0), Quaternion.identity);
//             NetworkServer.SpawnWithClientAuthority(goo, base.connectionToClient);
//         }
//         if (pre == "y")
//         {
//             GameObject goo = Instantiate(go, new Vector3(1.4f, 0, 0), Quaternion.identity);
//             NetworkServer.SpawnWithClientAuthority(goo, base.connectionToClient);
//         }

        
//     }
// }
