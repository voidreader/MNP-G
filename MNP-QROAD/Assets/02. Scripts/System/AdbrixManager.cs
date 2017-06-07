using UnityEngine;
using System.Collections;
using IgaworksUnityAOS;

/* Adbrix 처리 */

public class AdbrixManager : MonoBehaviour {

	static AdbrixManager _instance = null;

	// 일반 게임 활동 
	public readonly string GAME_END = "GameEnd"; // 인게임 클리어 
	public readonly string GAME_START = "GameStart"; // 일반 게임 시작   

    public readonly string FACEBOOK_LOGIN = "FacebookLogin";



	// 구매 활동 
	public readonly string BUY_IAP = "BuyIAP"; // 인앱결제 
	public readonly string BUY_GOLD = "BuyGold"; // 골드 구매


    public readonly string BUY_HEART_GEM = "BuyHeartWithGem"; // 젬으로 하트 구매
    public readonly string BUY_HEART_ADS = "BuyHeartWithAd"; // 광고보고 하트 충전 


    // 크레인
    public readonly string SPECIAL_CRANE = "SpecialCrane"; // 스페셜 크레인 
    public readonly string FREE_CRANE = "FreeCrane"; // 광고 보고 프리 크레인 

    // 업그레이드 
    public readonly string NEKO_LEVELUP = "NekoLevelup"; // 사용자 고양이 업그레이드
	

	// 아이템 사용 
	public readonly string ITEM_USE = "BoostItemUse";
	public readonly string ITEM1 = "BoostPlayTime"; // 실드 아이템 사용
	public readonly string ITEM2 = "BoostBomb"; // 피버 아이템 게이지 사용 
	public readonly string ITEM3 = "BoostSkill"; // 크리티컬 


    public readonly string NEKO_FEED = "NekoFeed"; // 고양이 물고기 먹이기 
    public readonly string POWER_UPGRADE = "PowerUpgrade"; // 파워 업그레이드
    public readonly string WAKE_UP = "WakeUp"; // 깨우기

    public readonly string NEKO_BONUS_ADS = "MitchiriBonusAds";
    public readonly string NEKO_BONUS_NOADS = "MitchiriBonusNoAds";

    #region New User Funnel

    public readonly string APP_EXCUTE = "AppExcuted";
    public readonly string LOGIN_GP = "GoogleLogin";
    public readonly string TAB_START = "TabStart";

    public readonly string INPUT_FIRSTNAME = "InputFirstName";

	public readonly string TUTORIAL_STEP1 = "TutorialStep1";
	public readonly string TUTORIAL_STEP2 = "TutorialStep2";
	public readonly string TUTORIAL_STEP3 = "TutorialStep3";
	public readonly string TUTORIAL_DONE = "CompletedTutorial";

	public readonly string START_STAGE2 = "StartStage2";
    public readonly string START_STAGE3 = "StartStage3";
    public readonly string START_STAGE4 = "StartStage4";
    public readonly string START_STAGE5 = "StartStage5";
    #endregion



    public static AdbrixManager Instance {
		get {
			if(_instance == null) {
				_instance = FindObjectOfType(typeof(AdbrixManager)) as AdbrixManager;
				
				if(_instance == null) {
					Debug.Log("AdbrixManager Init Error");
					return null;
				}
			}
			
			return _instance;
		}
		
		
	}


	void Awake() {
		DontDestroyOnLoad (this.gameObject);
	}



	/// <summary>
	/// Sends the adbrix new user funnel.
	/// </summary>
	/// <param name="pAct">P act.</param>
	public void SendAdbrixNewUserFunnel(string pAct) {

        
#if UNITY_ANDROID

        IgaworksUnityPluginAOS.Adbrix.firstTimeExperience(pAct);

#elif UNITY_IOS

        AdBrixPluginIOS.FirstTimeExperience(pAct);

#endif
        

    }


    /// <summary>
    /// In Adpp Purchasing
    /// </summary>
    /// <param name="pSKU">P SK.</param>
    public void SendAdbrixInAppPurchasing(GooglePurchaseTemplate pPurchase) {
        Debug.Log(">>> SendAdbrixInAppPurchasing :: " + pPurchase.SKU);

        //if (GameSystem.Instance.IsAdminUser)
        //return;


        //IgaworksUnityPluginAOS.Adbrix.purchase()


        if (WWWHelper.Instance.Url == EnvManagerCtrl.Instance.testServerURL)
            return;


#if UNITY_ANDROID

        //IgaworksUnityPluginAOS.Adbrix.buy(pSKU);
        //IgaworksUnityPluginAOS.Adbrix.purchase()

        foreach (GoogleProductTemplate tpl in AndroidInAppPurchaseManager.Client.Inventory.Products) {
            if(tpl.SKU == pPurchase.SKU) {

                Debug.Log(">>> Send Adbrix Purchase API");
                IgaworksUnityPluginAOS.Adbrix.purchase(pPurchase.OrderId, pPurchase.SKU, pPurchase.SKU, tpl.Price, 1, tpl.PriceCurrencyCode, "IAP");
                break;
            }
        }


#elif UNITY_IOS

        // AdBrixPluginIOS.Buy(pSKU);



#endif

    }


    /// <summary>
    /// In App Activity
    /// </summary>
    /// <param name="pAct">P act.</param>
    public void SendAdbrixInAppActivity(string pAct) {
        
#if UNITY_ANDROID

        IgaworksUnityPluginAOS.Adbrix.retention(pAct);

#elif UNITY_IOS

        AdBrixPluginIOS.Retention(pAct);

#endif

        

    }

    public void SendAdbrixInAppActivity(string pAct, string pParam) {

        
#if UNITY_ANDROID
        IgaworksUnityPluginAOS.Adbrix.retention(pAct, pParam);

#elif UNITY_IOS

        AdBrixPluginIOS.RetentionWithParam(pAct, pParam);

#endif
        

    }





}
