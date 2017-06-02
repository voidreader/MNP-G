using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
//using IgaworksUnityAOS;

public partial class GameSystem : MonoBehaviour {

	
    // billing 
    private bool _isBillInit = false;
    private string _payload = null;
    private bool isPassedPayload = false;
    private string _billKey = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAjFg2eer9JhGItWdeyZ4logq7RE7hAMuCIz1AUH2Mw/hQrIP9JEpZ+5a0b7UQA5fCCU/KtdeJZ0dGt6ro+khj0Vkwc3Gfq1naVn4eXCth3HIPfE/M183RfkCk7vqJjshaBWlnZcH3H/pEZB2+rd5CC3mee6p+eZHsXiKzRLV572DWV8/VMBc2oMdwPa6HumqLbjOMZ0CP/xGojPfde6EIqCCEXDjKlOc8l3ZILJl6zP8GEDiEl5+da98wSUxl1s7bBe9uoeC5Jb6k+DabkFroFB0dPv+KbnTraNIVcjnObth+TD5Zge+8iIx7PuUbD9okzDjcHkHtxRiRo8uVzZ7iXQIDAQAB";

	private string _recieptIOS;


    // 600엔 패키지
    public readonly string _startPackProductID_ios = "mn_pkg_01";
    public readonly string _600Pack2_ios = "mn_pkg_01_2";
    public readonly string _600Pack3_ios = "mn_pkg_01_3";
    public readonly string _600Pack4_ios = "mn_pkg_01_4";
    public readonly string _600Pack5_ios = "mn_pkg_01_5";
    public readonly string _600Pack6_ios = "mn_pkg_01_6";
    public readonly string _600Pack7_ios = "mn_pkg_01_7";
    public readonly string _600Pack8_ios = "mn_pkg_01_8";
    public readonly string _600Pack9_ios = "mn_pkg_01_9";

    public readonly string _startPackProductID_android = "mn_pkg_01";
    public readonly string _600Pack2_android = "mn_pkg_01_2";
    public readonly string _600Pack3_android = "mn_pkg_01_3";
    public readonly string _600Pack4_android = "mn_pkg_01_4";
    public readonly string _600Pack5_android = "mn_pkg_01_5";
    public readonly string _600Pack6_android = "mn_pkg_01_6";
    public readonly string _600Pack7_android = "mn_pkg_01_7";
    public readonly string _600Pack8_android = "mn_pkg_01_8";
    public readonly string _600Pack9_android = "mn_pkg_01_9";

    public readonly string _special_package = "mn_pkg_02";


    /// <summary>
    /// 구글 인앱 빌링 모듈 초기화 
    /// </summary>
    public void InitBilling() {

		Debug.Log (">>> Init Billing Start");

        // 초기화 했으면 return;
        if (_isBillInit) {

            // 상점 화면 세팅 
            LobbyCtrl.Instance.SkuMaster.SetSKUs();

            return;
        }


		#if UNITY_ANDROID
		    InitPlayServiceBilling();
		#endif

		#if UNITY_IOS
		InitIOSBilling();
		#endif


    }


    /// <summary>
    /// 일반 결제 
    /// </summary>
    /// <param name="sku"></param>
	public void PurchaseInApp(string sku) {

        WWWHelper.Instance.CurrentPackage = null;

		#if UNITY_ANDROID
		Purchase(sku);

		#elif UNITY_IOS

		BuyItemIOS(sku);

		#endif
	}



    /// <summary>
    /// 패키지 상품 구매 
    /// </summary>
    /// <param name="sku"></param>
    /// <param name="packageName"></param>
    public void PurchaseInAppPackage(string sku, string packageName) {

        Debug.Log("★★★★ PurchaseInAppPackage");

        WWWHelper.Instance.CurrentPackage = packageName;
        WWWHelper.Instance.CurrentSKU = sku;


#if UNITY_IOS
        BuyPackageIOS(packageName);

#elif UNITY_ANDROID

        BuyPackageAndroid(packageName);

#endif
    }


    /// <summary>
    /// Android 패키지 구매 시작 
    /// </summary>
    /// <param name="pPackageName"></param>
    private void BuyPackageAndroid(string pPackageName) {

        Debug.Log("★★★★ BuyPackageAndroid");

        // 내부 사용용도의 패키지 명 변경처리.
        if (pPackageName.Equals(PuzzleConstBox.packageHoney600)) {
            WWWHelper.Instance.PackageSKU = "mn_pkg_01";
        }
        else if (pPackageName.Equals(PuzzleConstBox.packageSummer600)) {
            WWWHelper.Instance.PackageSKU = "mn_pkg_01_2";
        }
        else if (pPackageName.Equals(PuzzleConstBox.packageMoon600)) {
            WWWHelper.Instance.PackageSKU = "mn_pkg_01_3";
        }
        else if (pPackageName.Equals(PuzzleConstBox.packageMusic600)) {
            WWWHelper.Instance.PackageSKU = "mn_pkg_01_4";
        }
        else if (pPackageName.Equals(PuzzleConstBox.packageBoots600)) {
            WWWHelper.Instance.PackageSKU = "mn_pkg_01_5";
        }
        else if (pPackageName.Equals(PuzzleConstBox.packageMaple600)) {
            WWWHelper.Instance.PackageSKU = "mn_pkg_01_6";
        }
        else if (pPackageName.Equals(PuzzleConstBox.packageSecret600)) {
            WWWHelper.Instance.PackageSKU = "mn_pkg_01_7";
        }
        else if (pPackageName.Equals(PuzzleConstBox.packageSanta600)) {
            WWWHelper.Instance.PackageSKU = "mn_pkg_01_8";
        }
        else if (pPackageName.Equals(PuzzleConstBox.packageFish600)) {
            WWWHelper.Instance.PackageSKU = "mn_pkg_01_9";
        }
        else if (pPackageName.Equals(PuzzleConstBox.packageSpecial)) {
            WWWHelper.Instance.PackageSKU = "mn_pkg_02";
        }

        _currentSKU = WWWHelper.Instance.CurrentSKU; // currentSKU는 실제 스토어 등록ID를 사용

        GameSystem.Instance.Post2ReqPayload();
    }

#region ios app store

    /// <summary>
    /// iOS 패키지 구매 시작 
    /// </summary>
    /// <param name="pPackageName"></param>
    private void BuyPackageIOS(string pPackageName) {

        Debug.Log("★★★★ BuyPackageAndroid");

        // 내부 사용용도의 패키지 명 변경처리.
        if (pPackageName.Equals(PuzzleConstBox.packageHoney600)) {
            WWWHelper.Instance.PackageSKU = "mn_pkg_01";
        }
        else if(pPackageName.Equals(PuzzleConstBox.packageSummer600)) {
            WWWHelper.Instance.PackageSKU = "mn_pkg_01_2";
        }
        else if (pPackageName.Equals(PuzzleConstBox.packageMoon600)) {
            WWWHelper.Instance.PackageSKU = "mn_pkg_01_3";
        }
        else if (pPackageName.Equals(PuzzleConstBox.packageMusic600)) {
            WWWHelper.Instance.PackageSKU = "mn_pkg_01_4";
        }
        else if (pPackageName.Equals(PuzzleConstBox.packageBoots600)) {
            WWWHelper.Instance.PackageSKU = "mn_pkg_01_5";
        }
        else if (pPackageName.Equals(PuzzleConstBox.packageMaple600)) {
            WWWHelper.Instance.PackageSKU = "mn_pkg_01_6";
        }
        else if (pPackageName.Equals(PuzzleConstBox.packageSecret600)) {
            WWWHelper.Instance.PackageSKU = "mn_pkg_01_7";
        }
        else if (pPackageName.Equals(PuzzleConstBox.packageSanta600)) {
            WWWHelper.Instance.PackageSKU = "mn_pkg_01_8";
        }
        else if (pPackageName.Equals(PuzzleConstBox.packageFish600)) {
            WWWHelper.Instance.PackageSKU = "mn_pkg_01_9";
        }
        else if (pPackageName.Equals(PuzzleConstBox.packageSpecial)) {
            WWWHelper.Instance.PackageSKU = "mn_pkg_02";
        }


        Debug.Log("★★★★ BuyPackageAndroid PackageSKU :: " + WWWHelper.Instance.PackageSKU);

        _currentSKU = WWWHelper.Instance.CurrentSKU; // currentSKU는 실제 스토어 등록ID를 사용
        GameSystem.Instance.Post2ReqPayload();

    }

    /// <summary>
    /// ios 아이템 구매 시작 
    /// </summary>
    /// <param name="sku">Sku.</param>
    private void BuyItemIOS(string sku) {

		Debug.Log (">>> BuyItemIOS :: " + sku);

		if (!_isBillInit) {
			Debug.Log("!! Billing is not yet inited");
            IOSNativePopUpManager.showMessage(GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3233), GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L4241));
            return;
		}


		WWWHelper.Instance.CurrentSKU = sku;
		_currentSKU = sku;
		GameSystem.Instance.Post2ReqPayload ();

		//IOSInAppPurchaseManager.Instance.BuyProduct (sku);
	}

    /// <summary>
    /// 구매 팝업 시작 iOS
    /// </summary>
	private void InAppIOSPurchase() {

        Debug.Log (">>> InAppIOSPurchase sku :: "  + _currentSKU);
		IOSInAppPurchaseManager.Instance.BuyProduct (_currentSKU);


        /* 구매 창을 띄운 후 _currentSKU Package 따라 변경 */
        if (!string.IsNullOrEmpty(WWWHelper.Instance.CurrentPackage)) {
            _currentSKU = WWWHelper.Instance.PackageSKU;
        }


        Debug.Log(">>> InAppIOSPurchase package Check :: " + _currentSKU);
    }



	public void InitIOSBilling() {

		Debug.Log (">>> InitIOSBilling");

		if(!_isBillInit) {

            //You do not have to add products by code if you already did it in seetings guid
            //Windows -> IOS Native -> Edit Settings
            //Billing tab.

            IOSInAppPurchaseManager.Instance.AddProductId("t0");
            IOSInAppPurchaseManager.Instance.AddProductId("g1");
			IOSInAppPurchaseManager.Instance.AddProductId("g2");
			IOSInAppPurchaseManager.Instance.AddProductId("g3");
			IOSInAppPurchaseManager.Instance.AddProductId("g4");
			IOSInAppPurchaseManager.Instance.AddProductId("g5");
			IOSInAppPurchaseManager.Instance.AddProductId("g0");
			IOSInAppPurchaseManager.Instance.AddProductId("g7");
            IOSInAppPurchaseManager.Instance.AddProductId("g8");
            IOSInAppPurchaseManager.Instance.AddProductId("special_g");
            


            //Event Use Examples
            IOSInAppPurchaseManager.OnVerificationComplete += HandleOnVerificationComplete;
			IOSInAppPurchaseManager.OnStoreKitInitComplete += OnStoreKitInitComplete;


			IOSInAppPurchaseManager.OnTransactionComplete += OnTransactionComplete;
			IOSInAppPurchaseManager.OnRestoreComplete += OnRestoreComplete;


			

		} 

		IOSInAppPurchaseManager.Instance.LoadStore();


	}

	private void OnStoreKitInitComplete(SA.Common.Models.Result result) {


		Debug.Log (">>> OnStoreKitInitComplete");

		if(result.IsSucceeded) {

			int avaliableProductsCount = 0;
			foreach(IOSProductTemplate tpl in IOSInAppPurchaseManager.Instance.Products) {
				string debug = "";

				debug += "id : " + tpl.Id;
				debug += "\nprice : " +tpl.Price;
				debug += "\nlocalizedPrice : " +tpl.LocalizedPrice;
				Debug.Log("debug : " + debug);

				/*
				Debug.Log("title : " + tpl.Title);
				Debug.Log("description : " + tpl.Description);
				Debug.Log("price : " + tpl.Price);
				Debug.Log("localizedPrice : " + tpl.LocalizedPrice);
				Debug.Log("currencySymbol : " + tpl.CurrencySymbol);
				Debug.Log("currencyCode : " + tpl.CurrencyCode);
				Debug.Log("-------------");
				*/

				if(tpl.IsAvaliable) {
					avaliableProductsCount++;
				}
			}

			//IOSNativePopUpManager.showMessage("StoreKit Init Succeeded", "Available products count: " + avaliableProductsCount);
			Debug.Log("StoreKit Init Succeeded Available products count: " + avaliableProductsCount);
			LobbyCtrl.Instance.SkuMaster.SetSKUs ();

            _isBillInit = true;

        } else {
			IOSNativePopUpManager.showMessage("StoreKit Init Failed",  "Error code: " + result.Error.Code + "\n" + "Error description:" + result.Error.Message);
		}
	}


	private void HandleOnVerificationComplete (IOSStoreKitVerificationResponse response) {
        //IOSNativePopUpManager.showMessage("Verification", "Transaction verification status: " + response.status.ToString());

        Debug.Log("HandleOnVerificationComplete receipt: " + response.receipt);
        Debug.Log("ORIGINAL JSON: " + response.originalJSON);
        
        if (response.status == 0) {
			
			Debug.Log ("Transaction is valid");
            // 일부 특별상품 중복 consuming 방지 
            



            if (_currentSKU == _startPackProductID_ios || _currentSKU == _startPackProductID_android
                || _currentSKU == _600Pack2_ios || _currentSKU == _600Pack2_android
                || _currentSKU == _600Pack3_ios || _currentSKU == _600Pack3_android
                || _currentSKU == _600Pack4_ios || _currentSKU == _600Pack4_android
                || _currentSKU == _600Pack5_ios || _currentSKU == _600Pack5_android
                || _currentSKU == _600Pack6_ios || _currentSKU == _600Pack6_android
                || _currentSKU == _600Pack7_ios || _currentSKU == _600Pack7_android
                || _currentSKU == _600Pack8_ios || _currentSKU == _600Pack8_android
                || _currentSKU == _600Pack9_ios || _currentSKU == _600Pack9_android
                || _currentSKU == _special_package) {

				for (int i = 0; i < _userDataJSON ["data"] ["packagelist"].Count; i++) {
					if (_userDataJSON ["data"] ["packagelist"].Value == _currentSKU) {
						Debug.Log ("▶ Double Consuming Checked ");
						return;
					}
				}
			}

			_recieptIOS = response.receipt;

			Post2Consume ();

		} else {
			// if fail
			IOSInAppPurchaseManager.OnVerificationComplete -= HandleOnVerificationComplete;
			IOSInAppPurchaseManager.OnVerificationComplete += HandleSandBoxOnVerificationComplete;
			IOSInAppPurchaseManager.Instance.VerifyLastPurchase (IOSInAppPurchaseManager.SANDBOX_VERIFICATION_SERVER);

			//IOSNativePopUpManager.showMessage ("Error", "올바르지 않은 결제 프로세스 입니다.");
			return;
		}

	}

	private void HandleSandBoxOnVerificationComplete (IOSStoreKitVerificationResponse response) {
		//IOSNativePopUpManager.showMessage("Verification", "Transaction verification status: " + response.status.ToString());

		Debug.Log("HandleSandBoxOnVerificationComplete receipt: " + response.receipt);
		Debug.Log("ORIGINAL JSON: " + response.originalJSON);

		// restore.
		IOSInAppPurchaseManager.OnVerificationComplete -= HandleSandBoxOnVerificationComplete;
		IOSInAppPurchaseManager.OnVerificationComplete += HandleOnVerificationComplete;


		if (response.status == 0) {

			Debug.Log ("Transaction is valid");
            // 일부 특별상품 중복 consuming 방지
            if (_currentSKU == _startPackProductID_ios || _currentSKU == _startPackProductID_android
                || _currentSKU == _600Pack2_ios || _currentSKU == _600Pack2_android
                || _currentSKU == _600Pack3_ios || _currentSKU == _600Pack3_android
                || _currentSKU == _600Pack4_ios || _currentSKU == _600Pack4_android
                || _currentSKU == _600Pack5_ios || _currentSKU == _600Pack5_android
                || _currentSKU == _600Pack6_ios || _currentSKU == _600Pack6_android
                || _currentSKU == _600Pack7_ios || _currentSKU == _600Pack7_android
                || _currentSKU == _600Pack8_ios || _currentSKU == _600Pack8_android
                || _currentSKU == _600Pack9_ios || _currentSKU == _600Pack9_android
                || _currentSKU == _special_package) {

                for (int i = 0; i < _userDataJSON ["data"] ["packagelist"].Count; i++) {
					if (_userDataJSON ["data"] ["packagelist"].Value == _currentSKU) {
						Debug.Log ("▶ Double Consuming Checked ");
						return;
					}
				}
			}

			_recieptIOS = response.receipt;

			Post2Consume ();

		} else {
            //IOSNativePopUpManager.showMessage ("Error", "正しくない決済プロセスです。");
            IOSNativePopUpManager.showMessage("Error", GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3401));
            return;
		}

	}



    /// <summary>
    /// 복원 (현재 상품중 복원할 일이 없음)
    /// </summary>
    /// <param name="res"></param>
	private void OnRestoreComplete (IOSStoreKitRestoreResult res) {
		
		if(res.IsSucceeded) {
			IOSNativePopUpManager.showMessage("Success", "Restore Compleated");
		} else {
			IOSNativePopUpManager.showMessage("Error: " + res.Error.Code, res.Error.Message);
		}
	}	


    /// <summary>
    /// 구매 통신 완료. 
    /// </summary>
    /// <param name="result"></param>
	private void OnTransactionComplete (IOSStoreKitResult result) {
        Debug.Log("OnTransactionComplete: " + result.ProductIdentifier);
        Debug.Log("OnTransactionComplete recipt: " + result.Receipt);
        //Debug.Log("OnTransactionComplete Receipt: " + result.Receipt);
        //Debug.Log("OnTransactionComplete: state: " + result.State);
        //Debug.Log("OnTransactionComplete: TransactionIdentifier: " + result.TransactionIdentifier);

        switch (result.State) {
		case InAppPurchaseState.Purchased:
		case InAppPurchaseState.Restored:
			//Our product been succsesly purchased or restored
			//So we need to provide content to our user depends on productIdentifier
			//UnlockProducts(result.ProductIdentifier);
			//IOSInAppPurchaseManager.Instance.VerifyLastPurchase (IOSInAppPurchaseManager.SANDBOX_VERIFICATION_SERVER);
			IOSInAppPurchaseManager.Instance.VerifyLastPurchase (IOSInAppPurchaseManager.APPLE_VERIFICATION_SERVER);
			break;
		case InAppPurchaseState.Deferred:
			//iOS 8 introduces Ask to Buy, which lets parents approve any purchases initiated by children
			//You should update your UI to reflect this deferred state, and expect another Transaction Complete  to be called again with a new transaction state 
			//reflecting the parent’s decision or after the transaction times out. Avoid blocking your UI or gameplay while waiting for the transaction to be updated.
			break;
		case InAppPurchaseState.Failed:
			//Our purchase flow is failed.
			//We can unlock intrefase and repor user that the purchase is failed. 
			Debug.Log("Transaction failed with error, code: " + result.Error.Code);
			Debug.Log("Transaction failed with error, description: " + result.Error.Message);


			break;
		}

		if (result.State == InAppPurchaseState.Purchased) {
			IOSNativePopUpManager.showMessage (GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3233), GetLocalizeText("4321"));
		} else if (result.State == InAppPurchaseState.Failed) {
			IOSNativePopUpManager.showMessage (GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3233), GetLocalizeText("4327"));
		}

	}

	private void UnlockProducts(string productIdentifier) {

		Debug.Log (">> Unlock Product :: " + productIdentifier);

        /*
		switch(productIdentifier) {
		
		case SMALL_PACK:
			//code for adding small game money amount here
			break;
		case NC_PACK:
			//code for unlocking cool item here
			break;

		}
        */
	}


	/// <summary>
	/// ios 인앱 결제 완료
	/// </summary>
	/// <param name="_sku">Sku.</param>
	/// <param name="pNode">P node.</param>
	private void OnCompleteIOSPurchase(string _sku, JSONNode pNode) {
		Debug.Log("!!! OnCompleteIOSPurchase sku :: " + _sku);
        Debug.Log("!!! OnCompleteIOSPurchase pNode :: " + pNode.ToString());

        string sku = _sku;

        if(!string.IsNullOrEmpty(WWWHelper.Instance.CurrentPackage)) {

            Debug.Log("!!! PackageItem! sku :: " + sku);
            Debug.Log("!!! PackageItem! pNode :: " + pNode.ToString());

            sku = WWWHelper.Instance.PackageSKU;
        }

        // Adbrix 테스트 사용자는 제외
        /*
        if (!WWWHelper.Instance.IsTestMode) {
			AdbrixManager.Instance.SendAdbrixInAppActivity(AdbrixManager.Instance.BUY_GEM);
			AdbrixManager.Instance.SendAdbrixInAppPurchasing(sku);
		}
        */


        // 재화 실제 적용 
        if (sku == _startPackProductID_ios
            || sku == _600Pack2_ios
            || sku == _600Pack3_ios
            || sku == _600Pack4_ios
            || sku == _600Pack5_ios
            || sku == _600Pack6_ios
            || sku == _600Pack7_ios
            || sku == _600Pack8_ios
            || sku == _600Pack9_ios
            || sku == _special_package) { // 패키지 처리 

            //AdbrixManager.Instance.SendAppsFlyerEvent(AdbrixManager.Instance.AF_BUY_PACK);

            UserGold += pNode[_jData]["valuegold"].AsInt;
			UserGem += pNode[_jData]["valuegem"].AsInt;


            if (pNode[_jData]["resultheart"].AsInt > 0) {
                HeartCount = pNode[_jData]["resultheart"].AsInt;
                LobbyCtrl.Instance.PlayNekoRewardGet();
            }

            if (pNode[_jData]["resultsalmon"].AsInt > 0)
                UserSalmon = pNode[_jData]["resultsalmon"].AsInt;



            for (int i = 0; i < pNode[_jData]["nekodatas"].Count; i++) {
                GameSystem.Instance.AddNewNeko(pNode[_jData]["nekodatas"][i]);
            }


            AddPurchasedPackage(sku);

            if (WindowManagerCtrl.Instance != null)
                WindowManagerCtrl.Instance.CheckSpecialPackageExists();
			


			UpdateTopInfomation();
		}
		else {

			// 상단 정보 체크 
			if (pNode["data"]["value"].AsInt > 0) {
				UserGem += pNode["data"]["value"].AsInt;
				UpdateTopInfomation(); // 상단정보 갱신 
			}

		}


	}

#endregion

#region Google PlayService

#if UNITY_ANDROID





    /// <summary>
    /// 
    /// </summary>
    private void InitPlayServiceBilling() {

        Debug.Log("▶ InitPlayServiceBilling");

        /*
        AndroidInAppPurchaseManager.Client.AddProduct("pack");
        AndroidInAppPurchaseManager.Client.AddProduct("diamond_100");
        AndroidInAppPurchaseManager.Client.AddProduct("diamond_1500");
        AndroidInAppPurchaseManager.Client.AddProduct("diamond_3400");
        AndroidInAppPurchaseManager.Client.AddProduct("diamond_500");
        AndroidInAppPurchaseManager.Client.AddProduct("diamond_5500");
        AndroidInAppPurchaseManager.Client.AddProduct("diamond_8000");
        */
        


        //listening for purchase and consume events
        AndroidInAppPurchaseManager.ActionProductPurchased += OnProductPurchased;
        AndroidInAppPurchaseManager.ActionProductConsumed += OnProductConsumed;

        AndroidInAppPurchaseManager.ActionBillingSetupFinished += OnBillingConnected;

        
        //you may use loadStore function without parametr if you have filled base64EncodedPublicKey in plugin settings
        //AndroidInAppPurchaseManager.Instance.LoadStore(_billKey);
        AndroidInAppPurchaseManager.Client.Connect();
        //AndroidInAppPurchaseManager.Instance.Connect();
        //AndroidInAppPurchaseManager.Client.Connect(_billKey);

        //or You can pass base64EncodedPublicKey using scirption:
        //AndroidInAppPurchaseManager.instance.loadStore(YOU_BASE_64_KEY_HERE);
    }


    /// <summary>
    /// 빌링 모듈 초기화 완료 
    /// </summary>
    /// <param name="result">Result.</param>
    private void OnBillingConnected(BillingResult result) {
        AndroidInAppPurchaseManager.ActionBillingSetupFinished -= OnBillingConnected;


        if (result.IsSuccess) {
            //Store connection is Successful. Next we loading product and customer purchasing details
            AndroidInAppPurchaseManager.Client.RetrieveProducDetails();
            AndroidInAppPurchaseManager.ActionRetrieveProducsFinished += OnRetrieveProductsFinised;
        }

        //AndroidMessage.Create("Connection Responce", result.response.ToString() + " " + result.message);
        Debug.Log("Connection Responce: " + result.Response.ToString() + " " + result.Message);
    }


    /// <summary>
    /// 인벤토리 조회 완료
    /// </summary>
    /// <param name="result">Result.</param>
    private void OnRetrieveProductsFinised(BillingResult result) {
        AndroidInAppPurchaseManager.ActionRetrieveProducsFinished -= OnRetrieveProductsFinised;

        

        if (result.IsSuccess) {
            UpdateStoreData();
            _isBillInit = true;

            Debug.Log("▶▶▶ OnRetrieveProductsFinised is OK");

            foreach (GoogleProductTemplate tpl in AndroidInAppPurchaseManager.Client.Inventory.Products) {
                Debug.Log(tpl.SKU + "/ " + tpl.LocalizedPrice);
                Debug.Log(tpl.PriceCurrencyCode + " / " + tpl.Price);
                //Debug.Log(tpl.OriginalJson);
            }

        }
        else {
            
			Debug.Log ("OnRetrieveProductsFinised Connection Responce: " + result.Response.ToString() + " " + result.Message);
			//AndroidMessage.Create("Connection Responce", result.response.ToString() + " " + result.message);
			AndroidInAppPurchaseManager.ActionProductPurchased -= OnProductPurchased;  
			AndroidInAppPurchaseManager.ActionProductConsumed  -= OnProductConsumed;
			AndroidInAppPurchaseManager.ActionBillingSetupFinished -= OnBillingConnected;
            //AndroidInAppPurchaseManager.ActionRetrieveProducsFinished -= OnRetrieveProductsFinised;

            AndroidInAppPurchaseManager.Client.RetrieveProducDetails();


        }



    }

    /// <summary>
    /// 인벤토리 조회 완료 이후, 미처 소비되지 않은 재화에 대해 재소비 처리한다.
    /// </summary>
    private void UpdateStoreData() {

        Debug.Log("▶▶▶ UpdateStoreDate Not Cosumed Count :: " + AndroidInAppPurchaseManager.Client.Inventory.Purchases.Count);

        /*
        foreach (GoogleProductTemplate p in AndroidInAppPurchaseManager.instance.Inventory.Products) {
            Debug.Log("Loaded product: " + p.Title);
        }
        */

        
        LobbyCtrl.Instance.SkuMaster.SetSKUs();



        for (int i = 0; i < AndroidInAppPurchaseManager.Client.Inventory.Purchases.Count; i++) {
            InAppConsume(AndroidInAppPurchaseManager.Client.Inventory.Purchases[i]);
        }
    }


    /// <summary>
    /// Raises the product purchased event.
    /// </summary>
    /// <param name="result">Result.</param>
    private void OnProductPurchased(BillingResult result) {
        //AndroidInAppPurchaseManager.ActionProductPurchased -= OnProductPurchased;  

        Debug.Log("♤♤♤ Purchased Responce: " + result.Purchase.ToString() + " " + result.Message);
        //Debug.Log(result.purchase.originalJson);

        if (result.IsSuccess) {
            //AndroidMessage.Create ("Product Purchased", result.purchase.SKU+ "\n Full Response: " + result.purchase.originalJson);
            

            _currentSKU = result.Purchase.SKU;
            OnProcessingPurchasedProduct(result.Purchase);
        }
        else {
            Debug.Log(result.Response.ToString() + " " + result.Message);
            //AndroidMessage.Create("Product Purchase Failed", result.response.ToString() + " " + result.message);
        }


        //AndroidInAppPurchaseManager.ActionProductPurchased += OnProductPurchased;  

        
    }


    private void OnProductConsumed(BillingResult result) {

        Debug.Log("Cousume Responce: " + result.Response.ToString() + " " + result.Message);

        if (result.IsSuccess) {
            //AndroidMessage.Create ("Product Consumed", result.purchase.SKU + "\n Full Response: " + result.purchase.originalJson);
            OnProcessingConsumeProduct(result.Purchase);
        }
        else {
            AndroidMessage.Create("Product Cousume Failed", result.Response.ToString() + " " + result.Message);
        }


    }

    private void OnProcessingConsumeProduct(GooglePurchaseTemplate purchase) {

        //StartCoroutine (Consuming (purchase));
        Debug.Log("OnProcessingConsumeProduct Done");
    }

    private void OnProcessingPurchasedProduct(GooglePurchaseTemplate purchase) {

        _billJSON = JSON.Parse(purchase.OriginalJson);
        _currentPurchase = purchase;

        //payload 검증 
        Post2VerifyPayload();
    }


    /// <summary>
    /// 테스트 용도의 인앱 소비 
    /// </summary>
    /// <param name="pSKU"></param>
    private void InAppConsumeTest(string pSKU) {
        Debug.Log("!!! InAppConsumeTest ");

        string sku;

        sku = pSKU;
        //_currentPurchase = purchase;
        //sku = _currentPurchase.SKU;


        // 상품 지급요청 용도(서버)
        //_billJSON = JSON.Parse(purchase.OriginalJson);


        if (!string.IsNullOrEmpty(WWWHelper.Instance.CurrentPackage)) {

            Debug.Log("★ Package Product! :: " + WWWHelper.Instance.CurrentPackage);
            sku = _currentSKU;
        }

        // 일부 특별상품 중복 consuming 방지
        if (sku == _startPackProductID_android || sku == _startPackProductID_ios
            || sku == _600Pack2_android || sku == _600Pack2_ios
            || sku == _600Pack3_android || sku == _600Pack3_ios
            || sku == _600Pack4_android || sku == _600Pack4_ios
            || sku == _600Pack5_android || sku == _600Pack5_ios
            || sku == _600Pack6_android || sku == _600Pack6_ios
            || sku == _600Pack7_android || sku == _600Pack7_ios
            || sku == _600Pack8_android || sku == _600Pack8_ios
            || sku == _600Pack9_android || sku == _600Pack9_ios
            || sku == _special_package) {

            for (int i = 0; i < _userDataJSON["data"]["packagelist"].Count; i++) {
                if (_userDataJSON["data"]["packagelist"].Value == sku) {
                    Debug.Log("▶ Double Consuming Checked ");
                    return;
                }
            }

        }

        // 패킷 전송 
        Post2Consume();
    }

    /// <summary>
    /// 결제상품 서버,스토어 소비 처리 (결제 플로우 에서 호출)
    /// </summary>
    /// <param name="purchase">Purchase.</param>
    private void InAppConsume(GooglePurchaseTemplate purchase) {
        Debug.Log("!!! InAppConsume ");

        string sku;

        _currentPurchase = purchase;
        sku = _currentPurchase.SKU;


        // 상품 지급요청 용도(서버)
        _billJSON = JSON.Parse(purchase.OriginalJson);


        if (!string.IsNullOrEmpty(WWWHelper.Instance.CurrentPackage)) {
            sku = _currentSKU;
        }

        // 일부 특별상품 중복 consuming 방지
        if (sku == _startPackProductID_android || sku == _startPackProductID_ios
            || sku == _600Pack2_android || sku == _600Pack2_ios
            || sku == _600Pack3_android || sku == _600Pack3_ios
            || sku == _600Pack4_android || sku == _600Pack4_ios
            || sku == _600Pack5_android || sku == _600Pack5_ios
            || sku == _600Pack6_android || sku == _600Pack6_ios
            || sku == _600Pack7_android || sku == _600Pack7_ios
            || sku == _600Pack8_android || sku == _600Pack8_ios
            || sku == _600Pack9_android || sku == _600Pack9_ios
            || sku == _special_package) {

            for (int i = 0; i < _userDataJSON["data"]["packagelist"].Count; i++) {
                if (_userDataJSON["data"]["packagelist"].Value == sku) {
                    Debug.Log("▶ Double Consuming Checked ");
                    return;
                }
            }
        }

        // 패킷 전송 
        Post2Consume();
    }

    /// <summary>
    /// 안드로이드 패킷 전송 후 최종 처리 
    /// </summary>
    /// <param name="purchase">Purchase.</param>
    /// <param name="pNode">P node.</param>
    private void OnCompleteServerConsume(GooglePurchaseTemplate purchase, JSONNode pNode) {

        Debug.Log("!!! OnCompleteServerConsume OK");
        string sku = purchase.SKU;

        // 패키지의 경우 교체 처리 
        if (!string.IsNullOrEmpty(WWWHelper.Instance.CurrentPackage)) {

            sku = WWWHelper.Instance.PackageSKU;
            Debug.Log("!!! OnCompleteServerConsume  Package! :: " + sku);
        }


        // Adbrix 테스트 사용자는 제외
        if (!WWWHelper.Instance.IsTestMode) {
            AdbrixManager.Instance.SendAdbrixInAppActivity(AdbrixManager.Instance.BUY_IAP);
            AdbrixManager.Instance.SendAdbrixInAppPurchasing(purchase);
        }


        // 구글 소비 처리 (실제 sku만 사용한다.)
        PlayStoreConsume(purchase.SKU);


        // 재화 실제 적용 
        if (sku == _startPackProductID_android 
            || sku == _600Pack2_android 
            || sku == _600Pack3_android 
            || sku == _600Pack4_android
            || sku == _600Pack5_android
            || sku == _600Pack6_android
            || sku == _600Pack7_android
            || sku == _600Pack8_android
            || sku == _600Pack9_android
            || sku == _special_package) { // 패키지 처리 

            
            //AdbrixManager.Instance.SendAppsFlyerEvent(AdbrixManager.Instance.AF_BUY_PACK);

            UserGold += pNode[_jData]["valuegold"].AsInt;
            UserGem += pNode[_jData]["valuegem"].AsInt;

            if (pNode[_jData]["resultheart"].AsInt > 0) {
                HeartCount = pNode[_jData]["resultheart"].AsInt;
                LobbyCtrl.Instance.PlayNekoRewardGet();
            }

            if (pNode[_jData]["resultsalmon"].AsInt > 0)
                UserSalmon = pNode[_jData]["resultsalmon"].AsInt;

            // 고양이 적용
            for (int i = 0; i < pNode[_jData]["nekodatas"].Count; i++) {
                GameSystem.Instance.AddNewNeko(pNode[_jData]["nekodatas"][i]);
            }


            AddPurchasedPackage(sku);

            if (WindowManagerCtrl.Instance != null)
                WindowManagerCtrl.Instance.CheckSpecialPackageExists();
                

            UpdateTopInfomation();
        }
        else {

            // 상단 정보 체크 
            if (pNode["data"]["value"].AsInt > 0) {
                UserGem += pNode["data"]["value"].AsInt;
                UpdateTopInfomation(); // 상단정보 갱신 
            }

        }
    }



    

    /// <summary>
    /// 구매처리 
    /// </summary>
    /// <param name="SKU">SK.</param>
    public void Purchase(string SKU) {
        //AndroidInAppPurchaseManager.Instance.Purchase (SKU);
        // 초기화 여부 체크 
        if (!_isBillInit) {
            Debug.Log("!! Billing is not yet inited");

#if UNITY_ANDROID
            AndroidMessage.Create(GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3233), GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L4241));

#elif UNITY_IOS
            IOSNativePopUpManager.showMessage(GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3233), GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L4241));
#endif
            return;
        }

        Debug.Log("Android Purchase :: " + SKU);

        WWWHelper.Instance.CurrentSKU = SKU; // SKU 설정 
        GameSystem.Instance.Post2ReqPayload();
    }


    /// <summary>
    /// Android 인앱 구매창 오픈 
    /// </summary>
    private void InAppPurchase() {

        Debug.Log("Google InAppPurchase :: " + WWWHelper.Instance.CurrentSKU + " / " + _payload);

        //if(!WWWHelper.Instance.IsTestMode)
            AndroidInAppPurchaseManager.Client.Purchase(WWWHelper.Instance.CurrentSKU, _payload);


        /* 구매 창을 띄운 후 _currentSKU Package 따라 변경 */
        if (!string.IsNullOrEmpty(WWWHelper.Instance.CurrentPackage))
            _currentSKU = WWWHelper.Instance.PackageSKU;

        Debug.Log(">>> InAppPurchase package Check :: " + _currentSKU);

        /*
        if (WWWHelper.Instance.IsTestMode)
            InAppConsumeTest(_currentSKU);
        */
    }

    /// <summary>
    /// 인앱결제 소모 처리 
    /// 서버 재화 처리 요청 이후에 Consume을 처리한다. (20151126)
    /// </summary>
    /// <param name="SKU">SK.</param>
    public void PlayStoreConsume(string SKU) {

        Debug.Log("!! Consume SKU :: " + SKU);

        AndroidInAppPurchaseManager.Client.Consume(SKU);
    }

#endif

#endregion



#region Properties


	public string PayLoad {
		get {
			return this._payload;
		}
	}

	public string RecieptIOS {
		get {
			return this._recieptIOS;
		}
	}

    public string StartPackProductID_ios {
        get {
            return _startPackProductID_ios;
        }
    }

    public string StartPackProductID_android {
        get {
            return _startPackProductID_android;
        }
    }


    #endregion

}
