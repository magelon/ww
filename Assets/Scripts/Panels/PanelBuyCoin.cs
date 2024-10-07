using UnityEngine;
using System.Collections;
using UnityEngine.Purchasing;
// using SmartLocalization;
using UnityEngine.UI;
public class PanelBuyCoin : MonoBehaviour, IStoreListener {

		// Use this for initialization
		GameObject scrollpanel;
		string lang = "";
		public GameObject btnBuyCoinClose;
		public GameObject panelBuyAlert;
        public GameObject spinButton;
		GameObject panel;
 		private static IStoreController storeController;
    	private static IExtensionProvider storeExtensionProvider;

    	public string productId = "com.lon.ww.coins300";
		public string productId2 = "com.lon.ww.coins600";
		public string productId3 = "com.lon.ww.coins1000";
		public string productId4 = "com.lon.ww.coins1500";

		void Start () {
				lang = "en";
				 if (storeController == null)
        	{
            	InitializePurchasing();
        	}
		}

    	public void InitializePurchasing()
    	{
        if (IsInitialized()) return;

        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        // Add products you want to offer
        builder.AddProduct(productId, ProductType.Consumable);
		builder.AddProduct(productId2, ProductType.Consumable);
		builder.AddProduct(productId3, ProductType.Consumable);
		builder.AddProduct(productId4, ProductType.Consumable);

        UnityPurchasing.Initialize(this, builder);
    	}

    	private bool IsInitialized() => storeController != null && storeExtensionProvider != null;

    	public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    	{
        storeController = controller;
        storeExtensionProvider = extensions;
    	}

    	public void OnInitializeFailed(InitializationFailureReason error)
        {
            Debug.Log(error);
        }

    	public void OnInitializeFailed(InitializationFailureReason error, string? message = null)
    	{
        var errorMessage = $"Purchasing failed to initialize. Reason: {error}.";

        if (message != null)
        {
            errorMessage += $" More details: {message}";
        }

        Debug.Log(errorMessage);
    	}

    	public void BuyProduct(string id)
    	{
        if (IsInitialized())
        {
            Product product = storeController.products.WithID(id);

            if (product != null && product.availableToPurchase)
            {
                storeController.InitiatePurchase(product);
            }
        }
    	}

     	public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    	{
        Debug.Log($"Purchase Failed: {failureReason}");
    	}

    	public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    	{
    	Debug.Log("Purchase successful!");
		Product purchasedProduct = args.purchasedProduct;
		switch(purchasedProduct.definition.id){
			case "com.lon.ww.coins300":
			grantCoins(300);
			break;
			case "com.lon.ww.coins600":
			grantCoins(1600);
			break;
			case "com.lon.ww.coins1000":
			grantCoins(3500);
			break;
			case "com.lon.ww.coins1500":
			grantCoins(15000);
			break;
			default:
            Debug.Log($"Unknown product purchased: {purchasedProduct.definition.id}");
            break;
		}
        return PurchaseProcessingResult.Complete;
    	}

		public void showMe(){
				panel = transform.Find ("panel").gameObject;	
				initView ();
				GameData.getInstance ().lockGame (true);
				ATween.MoveTo (panel, ATween.Hash ("ignoretimescale", true, "islocal", true, "y", 40, "time", 1f, "easeType", "easeOutExpo", "oncomplete", "OnShowCompleted", "oncompletetarget", this.gameObject));

		}

		void initView(){


				panel.transform.Find ("title").GetComponent<Text> ().text = Localization.Instance.GetString ("titleShop");
				panel.transform.Find ("btnClose").GetComponentInChildren<Text> ().text = Localization.Instance.GetString ("btnClose");

				for (int i = 1; i<5; i++) {
						GameObject trow = GameObject.Find ("row" + i);
						trow.transform.Find("lbDetail").GetComponent<Text>().text = Localization.Instance.GetString("price"+(i)+"tip");
						trow.transform.Find("lbPrice").GetComponent<Text>().text = Localization.Instance.GetString("price"+(i));
				}
		}


		IEnumerator hideWait(){
				yield return new WaitForSeconds (30);
				panelBuyAlert.SetActive(false);
		}


		public void OnClick2(GameObject g){
				switch (g.name) {
				case "btnBuyCoin":
						GameManager.getInstance().playSfx("select");

						int tindex = int.Parse(g.transform.parent.name.Substring(3,1));
						print(tindex);
						string tindexst=tindex.ToString();
						switch (tindexst){
							case "1":
							BuyProduct(productId);
							break;
							case "2":
							BuyProduct(productId2);
							
							break;
							case "3":
							BuyProduct(productId3);
							
							break;
							case "4":
							BuyProduct(productId4);
							break;
							
						}
                //add coin and dispaly
                //GameData.getInstance().coin += 60;
                //PlayerPrefs.SetInt("coin", GameData.getInstance().coin);
                //GameData.getInstance().main.txtCoin.text = GameData.getInstance().coin.ToString();
                //GameManager.getInstance().buy(tindex);
                break;
				case "btnClose":

						panel = transform.Find ("panel").gameObject;	
						ATween.MoveTo (panel.gameObject, ATween.Hash ("ignoretimescale",true,"islocal", true, "y", 600, "time", 1f, "easeType", "easeOutExpo", "oncomplete", "OnHideCompleted", "oncompletetarget", this.gameObject,"oncompleteparams", "buyClose"));
						break;
				}
		}

		private void grantCoins(int amount)
		{
    	// Add the coins to the user's account
    	PlayerPrefs.SetInt("coin", PlayerPrefs.GetInt("coin", 0) + amount);
		GameData.getInstance().coin = PlayerPrefs.GetInt ("coin");
		GameData.getInstance().main.txtCoin.text = GameData.getInstance().coin.ToString();
    	Debug.Log($"Granted {amount} coins. Total coins: {PlayerPrefs.GetInt("coin")}");
		}

		void OnHideCompleted(string str){
				switch (str) {
				case "buyClose":
						gameObject.SetActive (false);
						GameData.getInstance ().lockGame (false);
                        if (spinButton)
                        {
                            spinButton.SetActive(true);
                        }
                       
						break;
				}

		}
}
