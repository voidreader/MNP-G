using UnityEngine;
using System.Collections;

public class OptionCtrl : MonoBehaviour {

	[SerializeField] OptionSubPopCtrl _subPopUp;
    [SerializeField] OptionSoundPushCtrl _settingList;

    [SerializeField] CouponInputCtrl _couponInput;

    [SerializeField] UILabel lblID;
	[SerializeField] UILabel lblName;




	[SerializeField] UILabel lblVer;


	private int _id; // 유저 dbkey 
	private string _name; // 유저명 


    // 플랫폼별 버튼 (구글플레이스토어 , 앱스토어 게임센터) 
	[SerializeField] UIButton _btnPlatformStore; 
    [SerializeField] UIButton _btnFB;
    [SerializeField] UIButton _btnLB; // 리더보드 
    [SerializeField] UIButton _btnAchievement; //업적 
	[SerializeField] GameObject _pushSetup;

    [SerializeField] AttendanceCtrl _attendance;

    [SerializeField]
    GameObject _btnRecord;



    GPConnectionState _preGPConnectionState;


    readonly string _playstoreOff = "google-play-off";
    readonly string _gamecenterOff = "ios-game-center-off";
    readonly string _playstoreOn = "google-play-on";
    readonly string _gamecenterOn = "ios-game-center-on";

    readonly string _gpAchievement = "google-results";
    readonly string _gpLB = "google-reader";
    readonly string _gcAchievement = "google-results";
    readonly string _gcLB = "ios-results";

    readonly string _fbOn = "option_btn_facebook";
    readonly string _fbOff = "facebook-g";

    public GPConnectionState PreGPConnectionState {
        get {
            return _preGPConnectionState;
        }

        set {
            _preGPConnectionState = value;
        }
    }

    // Sprite





    void OnEnable() {
		RefreshFBGoogleState ();
        SetUserInfo();

    }






    void Start() {
		if (WWWHelper.Instance != null) {
			_id = WWWHelper.Instance.UserDBKey;
			_name = "";
		}

		// 사용자 정보 초기화 
		lblName.text = _name;
		lblID.text = _id.ToString ();



		SetUserInfo ();
		lblVer.text = "Ver " + GameSystem.Instance.GameVersion;
	}

	private void SetUserInfo() {
		_id = WWWHelper.Instance.UserDBKey;
		_name = GameSystem.Instance.UserName;


		lblID.text = "Neko-00-" + _id.ToString ();
		lblName.text = GameSystem.Instance.UserName;

        // _btnRecord.SetActive(GameSystem.Instance.RecordUse);
	}




    public void RefreshFBGoogleState() {
        //_btnPlatformStore.gameObject.SetActive(false);
        Debug.Log("RefreshFBGoogleState");

        if (Facebook.Unity.FB.IsLoggedIn) { // 페이스북 연결
            Debug.Log("FB Login Done");
            _btnFB.normalSprite = _fbOn;
        }
        else {
            Debug.Log("FB Logout Done");
            _btnFB.normalSprite = _fbOff;
        }

#if UNITY_ANDROID

        _btnPlatformStore.gameObject.SetActive(true);
        if (GameSystem.Instance.CurrentPlayer != null) { // 플레이 스토어 연결 되어있음. 

            
            _btnPlatformStore.normalSprite = _playstoreOn;
            _btnAchievement.gameObject.SetActive(true);
            _btnLB.gameObject.SetActive(true);
            _btnLB.normalSprite = _gpLB;
            _btnAchievement.normalSprite = _gpAchievement;

            
        }
        else {
            _btnPlatformStore.normalSprite = _playstoreOff;
            _btnAchievement.gameObject.SetActive(false);
            _btnLB.gameObject.SetActive(false);
        }

#elif UNITY_IOS

        _btnPlatformStore.gameObject.SetActive(true);
		if(GameCenterManager.IsPlayerAuthenticated) {
            
			_btnPlatformStore.normalSprite = _gamecenterOn;
            _btnAchievement.gameObject.SetActive(true);
            _btnLB.gameObject.SetActive(true);
            _btnLB.normalSprite = _gcLB;
            _btnAchievement.normalSprite = _gcAchievement;

		} else {
			_btnPlatformStore.normalSprite = _gamecenterOff;
            _btnAchievement.gameObject.SetActive(false);
            _btnLB.gameObject.SetActive(false);
		}

		// ios disable 
        // 쿠폰, 로컬 푸시 제거 
		_pushSetup.SetActive(false);

#endif



    }

    /// <summary>
    /// 게임센터, 구글플레이 로그인, 로그아웃
    /// </summary>
    public void LogoutGoogle() {

        

#if UNITY_ANDROID
        if (GameSystem.Instance.CurrentPlayer != null) { // 낫널일때만 
            GooglePlayConnection.ActionPlayerDisconnected += GooglePlayConnection_ActionPlayerDisconnected;
            //LobbyCtrl.Instance.OpenInfoPopUp(PopMessageType.Logout);
            GooglePlayConnection.Instance.Disconnect();

        } else {
            GooglePlayConnection.ActionConnectionResultReceived += ActionConnectionResultReceived;
            GooglePlayConnection.Instance.Connect();
        }


#elif UNITY_IOS

		if(!GameCenterManager.IsPlayerAuthenticated) {
            //GameSystem.Instance.ConnectGameCenterFromOption();
			LobbyCtrl.Instance.OpenInfoPopUp(PopMessageType.GameCenterConnectInfo);
        }

#endif


    }


    private void ActionConnectionResultReceived(GooglePlayConnectionResult result) {

        Debug.Log("!! ActionConnectionResultReceived ");
        GooglePlayConnection.ActionConnectionResultReceived -= ActionConnectionResultReceived; // 이벤트에서 제거 한다. 

        if (result.IsSuccess) {
            Debug.Log("Connected!");
            GameSystem.Instance.CurrentPlayer = GooglePlayManager.Instance.player;

            // 리더보드 로드 
            GooglePlayManager.ActionLeaderboardsLoaded += OnLeaderBoardsLoaded;
            GooglePlayManager.Instance.LoadLeaderBoards();

            // 업적 로드 
            GooglePlayManager.ActionAchievementsLoaded += OnAchievementsLoaded;
            GooglePlayManager.Instance.LoadAchievements();

        }
        else {
            Debug.Log("Cnnection failed with code: " + result.code.ToString());
        }

        RefreshFBGoogleState();


    }




    /// <summary>
    /// 리더보드 조회 완료 
    /// </summary>
    /// <param name="result">Result.</param>
    private void OnLeaderBoardsLoaded(GooglePlayResult result) {

        GooglePlayManager.ActionLeaderboardsLoaded -= OnLeaderBoardsLoaded;
        if (result.IsSucceeded) {
            if (GooglePlayManager.Instance.GetLeaderBoard(GameSystem.Instance.LEADERBOARD_ID) == null) {
                //Debug.Log("Leader boards loaded" + GameSystem.Instance.LEADERBOARD_ID + " not found in leader boards list");
                return;
            }

            //GPLeaderBoard leaderboard = GooglePlayManager.Instance.GetLeaderBoard(GameSystem.Instance.LEADERBOARD_ID);
            //long score = leaderboard.GetCurrentPlayerScore(GPBoardTimeSpan.ALL_TIME, GPCollectionType.GLOBAL).LongScore;

            //Debug.Log(GameSystem.Instance.LEADERBOARD_ID + "  score" +  score.ToString());
        }
        else {
            //AndroidMessage.Create("Leader-Boards Loaded error: ", result.message);
        }
    }


    /// <summary>
    /// 업적 조회 완료 
    /// </summary>
    /// <param name="result">Result.</param>
    private void OnAchievementsLoaded(GooglePlayResult result) {
        GooglePlayManager.ActionAchievementsLoaded -= OnAchievementsLoaded;

        if (result.IsSucceeded) {
        }
        else {
        }

    }


    /// <summary>
    /// 구글 플레이 disconnect 
    /// </summary>
    private void GooglePlayConnection_ActionPlayerDisconnected() {

        Debug.Log("Disconnect Google Play Service");
        

        GooglePlayConnection.ActionPlayerDisconnected -= GooglePlayConnection_ActionPlayerDisconnected;
        GameSystem.Instance.CurrentPlayer = null;

        RefreshFBGoogleState();
    }





    public void OpenSettingList() {
        _settingList.SetSetting();
    }

	/// <summary>
	/// 공지사항 Open
	/// </summary>
	public void OpenNotification() {
		Application.OpenURL ("https://game.nanoo.so/mitchirinekopop/forum?code=87eb9175146cce931b6c910ce31a8b16");
	}

	public void OpenEvent() {
		Application.OpenURL ("https://game.nanoo.so/mitchirinekopop/forum?code=112551f7f5b3c859dd9123fd4ea43310");
	}

	public void OpenGuide() {
		Application.OpenURL ("https://game.nanoo.so/mitchirinekopop/forum?code=82f08b144e3b0daba9a3313a433fa5d8");
	}

	public void OpenService() {
		Application.OpenURL ("https://game.nanoo.so/mitchirinekopop/forum?code=538eaceb469d24570207033e341a1c17");
	}

	public void OpenQA() {
		Application.OpenURL ("https://game.nanoo.so/mitchirinekopop/customer/inquiry");
	
	}

    public void OpenFBPage() {
        Application.OpenURL("https://www.facebook.com/mitchirinekopop.kr/");
    }

	public void OpenRate() {
		LobbyCtrl.Instance.OpenInfoPopUp (PopMessageType.RateForGem);
	}


    public void OpenUserGemInfo() {
        GameSystem.Instance.Post2UserGemInfo();
    }

    public void LoginTwitter() {
        //GameSystem.Instance.StartLogin();
    }


    /// <summary>
	/// Opens the google leaderboard.
	/// </summary>
	public void OpenGoogleLeaderboard() {

        GameSystem.Instance.OpenLeaderBoard();
    }

    /// <summary>
    /// Opens the google leaderboard.
    /// </summary>
    public void OpenGoogleAchievement() {
        GameSystem.Instance.OpenAchievement();
    }

    /// <summary>
    /// 닉네임 설정 팝업 오픈 
    /// </summary>
    public void OpenNickName() {
        WindowManagerCtrl.Instance.OpenSimpleNickInfo();
    }

    public void OpenNoticeList() {
        WindowManagerCtrl.Instance.OpenNoticeList();
    }

    public void OpenCodeIssue() {
        WindowManagerCtrl.Instance.OpenCodeIssue();
    }

    public void OpenCodeInput() {
        LobbyCtrl.Instance.OpenCodeInput();
    }



    public void OpenFAQ() {
        
        Application.OpenURL(GameSystem.Instance.UrlFAQ);
    }

    public void OpenQuestion() {
        // Application.OpenURL("https://mnpop.x-legend.co.jp/sp/contact/");
        //Application.OpenURL("https://game.nanoo.so/mitchirinekopop/customer/inquiry");

        Application.OpenURL(GameSystem.Instance.UrlFAQ);

    }

    public void OpenInviteWindow() {
        WindowManagerCtrl.Instance.OpenInviteWindow();
    }


    /// <summary>
    /// 본인 실적 조회 
    /// </summary>
    public void OpenUserRecord() {
        GameSystem.Instance.Post2UserRecord(WWWHelper.Instance.UserDBKey);
            
    }

    public void OpenCouponInput() {
        _couponInput.OpenCouponInput();
    }

#region 팁 화면 관련 제어


    public void OpenTipList() {
        
    }

    public void OpenLanguageList() {
        _subPopUp.OpenLanguageList();
    }

    public void OpenPuzzlePlayTips() {
        //LobbyCtrl.Instance.GameTip.SetGameTip(TipType.PuzzlePlay, 0);
    }

    public void OpenAllItemPassiveTips() {
        //LobbyCtrl.Instance.GameTip.SetGameTip(TipType.AllPuzzleItemAbility, 0);
    }

    public void OpenWantedTips() {
        //LobbyCtrl.Instance.GameTip.SetGameTip(TipType.Wanted, 0);
    }

    public void OpenNekoServiceTips() {
        LobbyCtrl.Instance.GameTip.SetGameTip(TipType.NekoService);
    }

    public void OpenMissionTips() {
        LobbyCtrl.Instance.GameTip.SetGameTip(TipType.AllMission);
    }

    public void OpenNekoLevelUp() {
        LobbyCtrl.Instance.GameTip.SetGameTip(TipType.NekoLevelup);
    }

    public void OpenBingoTip() {
        LobbyCtrl.Instance.GameTip.SetGameTip(TipType.Bingo);
    }

    public void OpenAttendanceCheck() {
        _attendance.OpenAttendanceCheck();
    }

#endregion



    /// <summary>
    /// 
    /// </summary>
    /// <param name="pause"></param>
    private void OnApplicationPause(bool pause) {
#if UNITY_ANDROID 
        if (pause) {



            GooglePlayConnection.ActionConnectionStateChanged += GooglePlayConnection_ActionConnectionStateChanged;
            PreGPConnectionState = GooglePlayConnection.State; // 현재 상태를 저장 

            if (PreGPConnectionState == GPConnectionState.STATE_DISCONNECTED)
                GameSystem.Instance.CurrentPlayer = null;
        }

#endif
            
    }

    #if UNITY_ANDROID
    public void GooglePlayConnection_ActionConnectionStateChanged(GPConnectionState obj) {

        Debug.Log(">>>> GooglePlayConnection_ActionConnectionStateChanged In");
        GooglePlayConnection.ActionConnectionStateChanged -= GooglePlayConnection_ActionConnectionStateChanged;


        StartCoroutine(DelayedGPConnectionCheck(obj));

    }
    #endif


    #if UNITY_ANDROID    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="obj"></param>
    IEnumerator DelayedGPConnectionCheck(GPConnectionState obj) {

        yield return new WaitForSeconds(0.2f);


        // 이전상태와 다르게 접속이 끊어진 경우
        // 끊어진 상태에서 끊어진 상태는 체크하지 않음.
        if (PreGPConnectionState != GPConnectionState.STATE_DISCONNECTED && obj == GPConnectionState.STATE_DISCONNECTED) {
            Debug.Log(">>>> Disconnected!!");
            LobbyCtrl.Instance.OpenInfoPopUp(PopMessageType.PlayerInfoModified);
            yield break;
        }

        // 연결상태이고 이전에도 연결상태일때. 
        if (obj == GPConnectionState.STATE_CONNECTED && PreGPConnectionState == GPConnectionState.STATE_CONNECTED) {

            // 현재 설정된 유저와 신규 유저의 ID 체크 
            Debug.Log(" Current GP ID :: " + GameSystem.Instance.CurrentPlayer.playerId);

            // New ID 
            Debug.Log(" New GP ID :: " + GooglePlayManager.Instance.player.playerId);

            if (GameSystem.Instance.CurrentPlayer.playerId != GooglePlayManager.Instance.player.playerId) {
                Debug.Log(">>>> Different ID !!");
                LobbyCtrl.Instance.OpenInfoPopUp(PopMessageType.PlayerInfoModified);
                yield break;
            }
        }

    }
    #endif
    
}
