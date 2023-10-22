
// using UnityEngine;
// using GooglePlayGames;
// using GooglePlayGames.BasicApi;
// using System.Threading.Tasks;
// using Firebase;
// using Firebase.Auth;
// using Firebase.Database;
// using Firebase.Unity.Editor;
// using UnityEngine.UI;

//public class SignInPlay : MonoBehaviour
//{
//    DatabaseReference reference;
   
//    public Text t;
//    public GameObject signButton;
//    public GameObject start;

//    void Start()
//    {
//        //FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        
//        // Set this before calling into the realtime database.
//        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://war3-371f1.firebaseio.com/");

//        // Get the root reference location of the database.
//        reference = FirebaseDatabase.DefaultInstance.RootReference;
        
//    PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
//    // enables saving game progress.
//    .EnableSavedGames()
//    .RequestEmail()

//    .RequestServerAuthCode(false)
//    /// You need to request the Email before asking for it
//    .AddOauthScope("email")

//    .RequestIdToken()
//    .Build();


//        PlayGamesPlatform.InitializeInstance(config);
//        PlayGamesPlatform.Activate();
        
//        /*
//        Social.localUser.Authenticate((bool success) => { t.text = "in"; });

//            if (PlayerPrefs.HasKey("signed"))
//        {
//            signButton.SetActive(false);
//            start.SetActive(true);
//        }**/
//    }

//    private void writeNewUser(string userId, string name, string email,string time)
//    {
        
//        User user = new User(name, email,time);
//        string json = JsonUtility.ToJson(user);

//        reference.Child("users").Child(userId).SetRawJsonValueAsync(json);
//    }

//    public void SignInWithGP()
//    {
//        Social.localUser.Authenticate((bool success) => {
//           if (success)
//           {
//                /*
//                string authCode = PlayGamesPlatform.Instance.GetServerAuthCode();

//                Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
//                Firebase.Auth.Credential credential =
//                    Firebase.Auth.PlayGamesAuthProvider.GetCredential(authCode);
//                auth.SignInWithCredentialAsync(credential).ContinueWith(task => {
//                    if (task.IsCanceled)
//                    {
//                        Debug.LogError("SignInWithCredentialAsync was canceled.");
//                        return;
//                    }
//                    if (task.IsFaulted)
//                    {
//                        Debug.LogError("SignInWithCredentialAsync encountered an error: " + task.Exception);
//                        return;
//                    }

//                    Firebase.Auth.FirebaseUser newUser = task.Result;
//                    Debug.LogFormat("User signed in successfully: {0} ({1})",
//                        newUser.DisplayName, newUser.UserId);
//                });
//                **/
//                t.text = "welcome "+ Social.localUser.userName;
//                PlayerPrefs.SetInt("signed", 1);
//                signButton.SetActive(false);
//                start.SetActive(true);
//                writeNewUser(Social.localUser.id, Social.localUser.userName, ((PlayGamesLocalUser)Social.localUser).Email,System.DateTime.Now.ToString());    
//                }
//          });
//        }

//}
