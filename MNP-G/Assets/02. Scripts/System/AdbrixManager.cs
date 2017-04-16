using UnityEngine;
using System.Collections;
using IgaworksUnityAOS;

/* Adbrix 처리 */

public class AdbrixManager : MonoBehaviour {

	static AdbrixManager _instance = null;

	// 일반 게임 활동 
	public readonly string GAME_CLEAR = "gameClear"; // 인게임 클리어 
	public readonly string GAME_START = "gameStart"; // 일반 게임 시작   


    public readonly string VIEW_2NDREWARD_MOVIE = "viewMovieFor2ndReward"; // 네코 보은 동영상 광고 시청
	public readonly string VIEW_NEKO_MOVIE = "viewMovieForNeko"; // 네코 뽑기 동영상 광고 시청 
	public readonly string VIEW_HEART_MOVIE = "viewMovieForHeart"; // 하트 동영상 광고 시청 
	public readonly string ITEM_MOVIE = "itemMovie"; // 아이템 동영상 광고 시청 

	public readonly string OPEN_ADPOPCORN = "openAdPopCorn"; // 애드팝콘 오픈
	public readonly string USE_ADPOPCORN = "useAdPopCorn"; // 애드팝콘 참여 

	// 구매 활동 
	public readonly string BUY_GEM = "buyGem"; // 보석 구매
	public readonly string BUY_GOLD = "buyGold"; // 골드 구매
	public readonly string BUY_HEART = "buyHeart"; // 하트 구매

	// 가챠 
	public readonly string GATCHA = "gatcha"; // 뽑기 

	// 업그레이드 
	public readonly string NEKO_UPGRADE = "nekoUpgrade"; // 사용자 고양이 업그레이드
	public readonly string GAMETIME_UPGRADE = "gameTimeUpgrade"; // 게임시간 업그레이드 
	public readonly string FEVERTIME_UPGRADE = "feverTimeUpgrade"; // 피버타임 업그레이드 
	public readonly string POWER_UPGRADE = "powerUpgrade"; // 파워 업그레이드

	// 아이템 사용 
	public readonly string ITEM_USE = "boostItemUse";
	public readonly string ITEM1 = "Shield"; // 실드 아이템 사용
	public readonly string ITEM2 = "FeverItem"; // 피버 아이템 게이지 사용 
	public readonly string ITEM3 = "Critical"; // 크리티컬 
	public readonly string ITEM4 = "StartFever"; // 시작시 피버 아이템 사용


	// New User Funnel
	public readonly string CONNECT_SERVER = "connectSuccess";
	public readonly string TUTORIAL_STEP1 = "tutorialStep1";
	public readonly string TUTORIAL_STEP2 = "tutorialStep2";
	public readonly string TUTORIAL_STEP3 = "tutorialStep3";

	public readonly string TUTORIAL_DONE = "tutorialComplete";
	public readonly string NEW_GAME_START = "firstGameStart";
    public readonly string FIRST_REAL_PUZZLE_START = "realPuzzleStart";



    #region Adbrix 버튼 사용 추출

    //public readonly string BUTTON_COINSHOP = "btnCoinShop";
    //public readonly string BUTTON_GEMSHOP = "btnGemShop";
    //public readonly string BUTTON_HEARTSHOP = "btnGemShop";

    public readonly string BUTTON_MISSION = "btnMission";
    public readonly string BUTTON_MAIL = "btnMail";
    public readonly string BUTTON_COLLECTION = "btnCollection";
    public readonly string BUTTON_RANKING = "btnRanking";

    public readonly string BUTTON_NEKOGROWTH = "btnNekoGrowth";
    public readonly string BUTTON_FRIEND = "btnFriend";
    public readonly string BUTTON_OPTION = "btnOption";
    public readonly string BUTTON_PUZZLE = "btnPuzzle";
    public readonly string BUTTON_GACHJA = "btnGacha";

    public readonly string BUTTON_NEWFIVENEKO = "btnEventNekoFiveNeko";

    #endregion


    #region AppsFlyer 변수 

    public readonly string AF_DIA_GACHA_1 = "dia_gacha_1";
    public readonly string AF_DIA_GACHA_10 = "dia_gacha_10";

    public readonly string AF_FISH_GACHA_1 = "fishgacha_1";
    public readonly string AF_FISH_GACHA_10 = "fishgacha_10";

    public readonly string AF_FREE_GACHA = "free_gacha";
    public readonly string AF_FREE_HEART_CHARGE = "free_heart_charge"; // 광고 보고 하트 충전
    public readonly string AF_DIA_HEART_CHARGE = "dia_heart_charge"; // 다이아로 하트 충전


    public readonly string AF_BUFF_MISS= "buff_miss";
    public readonly string AF_BUFF_BOMB_GAUGE = "buff_bomb_gauge";
    public readonly string AF_BUFF_CRITICAL = "buff_critical";

    public readonly string AF_BUY_PACK = "buy_pack"; // 스타트 팩 구매

    public readonly string AF_BUY_COIN_30000 = "buy_coin_30000";
    public readonly string AF_BUY_COIN_53000 = "buy_coin_53000";
    public readonly string AF_BUY_COIN_110000 = "buy_coin_110000";
    public readonly string AF_BUY_COIN_340000 = "buy_coin_340000";
    public readonly string AF_BUY_COIN_580000 = "buy_coin_580000";



    public readonly string AF_FREE_FEVER = "free_fever";
    public readonly string AF_FREE_GIFT1 = "free_gift1";
    public readonly string AF_FREE_GIFT2 = "free_gift2";




    public readonly string AF_MEMBERNAME_TRY= "membername_try"; // 플레이어 이름 입력이 화면에 표시
    public readonly string AF_MEMBERNAME_SUCC = "membername_success　"; // 플레이어 이름 입력 성공

    public readonly string AF_TUTORIAL_START = "tutorial_start"; // 플레이어 이름 입력이 화면에 표시
    public readonly string AF_TUTORIAL_COMPLETE = "tutorial_complete"; // 플레이어 이름 입력이 화면에 표시

    public readonly string AF_PLAYER_LEVEL_1 = "player_level_1";
    public readonly string AF_PLAYER_LEVEL_2 = "player_level_2";
    public readonly string AF_PLAYER_LEVEL_3 = "player_level_3";
    public readonly string AF_PLAYER_LEVEL_4 = "player_level_4";
    public readonly string AF_PLAYER_LEVEL_5 = "player_level_5";
    public readonly string AF_PLAYER_LEVEL_6 = "player_level_6";
    public readonly string AF_PLAYER_LEVEL_7 = "player_level_7";
    public readonly string AF_PLAYER_LEVEL_8 = "player_level_8";
    public readonly string AF_PLAYER_LEVEL_9 = "player_level_9";
    public readonly string AF_PLAYER_LEVEL_10 = "player_level_10";
    public readonly string AF_PLAYER_LEVEL_15 = "player_level_15";
    public readonly string AF_PLAYER_LEVEL_20 = "player_level_20";
    public readonly string AF_PLAYER_LEVEL_25 = "player_level_25";
    public readonly string AF_PLAYER_LEVEL_30 = "player_level_30";
    public readonly string AF_PLAYER_LEVEL_35 = "player_level_35";
    public readonly string AF_PLAYER_LEVEL_40 = "player_level_40";
    public readonly string AF_PLAYER_LEVEL_45 = "player_level_45";
    public readonly string AF_PLAYER_LEVEL_50 = "player_level_50";



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
    public void SendAdbrixInAppPurchasing(string pSKU) {
        Debug.Log(">>> SendAdbrixInAppPurchasing :: " + pSKU);

        if (GameSystem.Instance.IsAdminUser)
            return;


#if UNITY_ANDROID

        IgaworksUnityPluginAOS.Adbrix.buy(pSKU);
        //IgaworksUnityPluginAOS.Adbrix.purchase()

        foreach (GoogleProductTemplate tpl in AndroidInAppPurchaseManager.Client.Inventory.Products) {
            if(tpl.SKU == pSKU) {
                
                //Debug.Log(">>> SendAdbrixInAppPurchasing Match :: " + tpl.PriceCurrencyCode + "/" +tpl.Price.ToString());
                //SendAppsFlyerPurchaseEvent(tpl.PriceCurrencyCode, tpl.Price.ToString());
                break;
            }
        }


#elif UNITY_IOS

        AdBrixPluginIOS.Buy(pSKU);



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
