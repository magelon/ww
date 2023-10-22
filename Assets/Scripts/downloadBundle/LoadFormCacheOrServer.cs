// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.Networking;
//using Firebase;
//using Firebase.Auth;
//using Firebase.Database;
//using Firebase.Unity.Editor;

//public class LoadFormCacheOrServer : MonoBehaviour
//{
    // string assetBundleName = "newbundle";
    // public AssetBundle bundle;
    // private string currentBundleVersion;
    // string localVersion;
    // //DatabaseReference reference;
    // bool Startupdate;

    //void Start()
    //{
        
        // Set these values before calling into the realtime database.
        //FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://war3-371f1.firebaseio.com/");
        // Get the root reference location of the database.
        //reference = FirebaseDatabase.DefaultInstance.RootReference;

        //FirebaseDatabase.DefaultInstance
      //.GetReference("bundleVersion")
      //.GetValueAsync().ContinueWith(task => {
          //if (task.IsFaulted)
          //{
              // Handle the error...
          //}
          //else if (task.IsCompleted)
          //{
              //DataSnapshot snapshot = task.Result;
             
              //Debug.Log(snapshot.Value);
               //currentBundleVersion = snapshot.Value.ToString();
              //Debug.Log("cur: "+ currentBundleVersion);
              // Do something with snapshot...

          //}
      //});

    //}

    // private void Update()
    // {
    //     if (!Startupdate && currentBundleVersion != null)
    //     {
    //         Startupdate = true;
    //         if (PlayerPrefs.HasKey("bundle"))
    //         {
    //             localVersion = PlayerPrefs.GetString("bundle");
    //             Debug.Log("local " + localVersion);
    //             Debug.Log("current " + currentBundleVersion);

    //             if (localVersion != currentBundleVersion)
    //             {
    //                 StartCoroutine("StartDownload");

    //             }
    //         }
    //         else
    //         {
    //             PlayerPrefs.SetString("bundle", currentBundleVersion);
    //             StartCoroutine("StartDownload");
    //         }
    //     }
    // }

    // IEnumerator StartDownload()
    // {   
    //     string uri = "http://192.168.1.65:8080/newbundle";
    //     UnityWebRequest request =
    //                UnityWebRequestAssetBundle.GetAssetBundle(uri, 0);
    //     while (request.downloadProgress < 1)
    //     {
           
    //         Debug.Log(string.Format("Progress - {0}%. from {1}", request.downloadProgress * 100, request.url));
    //         yield return new WaitForSeconds(.1f);
    //     }
    //     yield return request.Send();
    //     bundle = DownloadHandlerAssetBundle.GetContent(request);
    //     if (bundle != null)
    //     {
    //         Debug.Log(bundle.name);
    //        // GameObject au = bundle.LoadAsset<GameObject>("Dragon");
    //        // Instantiate(au);
    //     }

    //     /*
    //     if (PlayerPrefs.HasKey("bundle"))
    //     {
    //         localVersion=PlayerPrefs.GetInt("bundle");
    //         if (localVersion != currentBundleVersion)
    //         {
    //             PlayerPrefs.SetInt("bundle", currentBundleVersion);
    //             //string uri = "file:///" + Application.dataPath + "/AssetBundles/" + assetBundleName;
               
    //             UnityWebRequest request =
    //                 UnityWebRequestAssetBundle.GetAssetBundle(uri, 0);
    //             yield return request.Send();
    //             AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(request);
    //             if (bundle != null)
    //             {
    //               Debug.Log(bundle.name);
    //               bundle.LoadAsset<GameObject>("bgmusic");
    //             }

    //         }
    //     }
    //     else
    //     {
    //         PlayerPrefs.SetInt("bundle", currentBundleVersion);

    //         UnityWebRequest request =
    //             UnityWebRequestAssetBundle.GetAssetBundle(uri, 0);
    //         yield return request.Send();
    //         AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(request);
    //         if (bundle != null)
    //         {
    //             Debug.Log(bundle.name);
    //             bundle.LoadAsset<GameObject>("bgmusic");
    //         }
    //     }
    //     **/
    // }
//}
