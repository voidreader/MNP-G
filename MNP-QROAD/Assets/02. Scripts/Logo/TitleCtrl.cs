using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using SimpleJSON;
using BestHTTP;
using Facebook.Unity;
using IgaworksUnityAOS;


public class TitleCtrl : MonoBehaviour {

	static TitleCtrl _instance = null;


    [SerializeField] AndroidPermissionCheckerCtrl _aosPermissionChecker;
    [SerializeField] FirstNickCtrl _firstNickCtrl;

	[SerializeField] bool isCompletedLoading = false;
    bool _isStartBtnClicked = false;
    
    



	[SerializeField] UILabel _lblLoading;
    [SerializeField] UILabel _lblVersion;

	// 사운드
	[SerializeField] AudioSource bgmSource = null;

	// 팝업창 관련 
	[SerializeField] GameObject objExitPopup; // Exit 팝업 
	[SerializeField] GameObject _currentPopup; // 현재 띄워져 있는 팝업 




    [SerializeField]
    GameObject _touchScreen;

    [SerializeField]
    Transform _tapStart;


    // 페이스북 로그인(WEB)
    [SerializeField]
    GameObject _btnFBLogin;

	private bool isTouchLock = false;
	public bool isTitleLoginCompleted = false; // 타이틀 로그인 완료 여부 .
	public string currentLoadMessage = "";


    bool _isLoadingLobbyScene = false;
    int _loadingTextIndex = 0;
    string _postLoadingMsg;


    public static TitleCtrl Instance {
		get {
			if(_instance == null) {
				_instance = FindObjectOfType(typeof(TitleCtrl)) as TitleCtrl;
				
				if(_instance == null) {
					Debug.Log("TitleCtrl Init Error");
					return null;
				}
			}
			
			return _instance;
		}
		
		
	}

    #region Awake, Start, Update 

    void Awake() {
        ScissorCtrl.Instance.UpdateResolution();
    }

	// Use this for initialization
	void Start () {
        _lblVersion.text = "Ver " + GameSystem.Instance.GameVersion.ToString();

        _isLoadingLobbyScene = false;
        _currentPopup = null; // 팝업 제어 
        _touchScreen.SetActive(false);
        _tapStart.gameObject.SetActive(false);


        if(Application.isEditor) {
            ConnectGame(); 
            return;
        }


#if UNITY_ANDROID
        CheckAndroidRuntimePermission();
        return;
#else
         ConnectGame();
#endif





    }




	void Update() {

		if (Input.GetKeyDown (KeyCode.Escape)) {

			// 현재 뜬 팝업이 아무것도 없을시에는 exit 팝업이 뜬다. 
			if(_currentPopup == null) {
				objExitPopup.SetActive(true);
				_currentPopup = objExitPopup;
			} else { // 현재 팝업이 있는 경우. 
				_currentPopup.SetActive(false);
				_currentPopup = null;
			}
        
			/*
			if(objExitPopup.activeSelf) {
				objExitPopup.SetActive(false);
			} else {
				objExitPopup.SetActive(true);
			}
			*/
		}


	}


    private void KillTweening() {
        StopCoroutine(BlinkTapText());
        _tapStart.transform.DOKill();
        _tapStart.transform.localScale = GameSystem.Instance.BaseScale;
    }


    /// <summary>
    /// 안드로이드 런타임 권한 체크 
    /// </summary>
    void CheckAndroidRuntimePermission() {
    #if UNITY_ANDROID
        // 안드로이드 권한 체크 추가 (2017.05)
        // SDK 레벨이 23이상이고, 권한을 획득하지 않은 상태일때 오픈한다. 

        if (GetSDKLevel() >= 23) {

            Debug.Log("★ Android Permission Check READ_EXTERNAL_STORAGE :: " + PermissionsManager.IsPermissionGranted(AN_Permission.READ_EXTERNAL_STORAGE));
            Debug.Log("★ Android Permission Check WRITE_EXTERNAL_STORAGE :: " + PermissionsManager.IsPermissionGranted(AN_Permission.WRITE_EXTERNAL_STORAGE));

            if (!PermissionsManager.IsPermissionGranted(AN_Permission.READ_EXTERNAL_STORAGE)
                || !PermissionsManager.IsPermissionGranted(AN_Permission.WRITE_EXTERNAL_STORAGE)) {

                // 더이상 팝업하지 않게 했는지 체크
                if(PermissionsManager.ShouldShowRequestPermission(AN_Permission.READ_EXTERNAL_STORAGE) 
                    || PermissionsManager.ShouldShowRequestPermission(AN_Permission.WRITE_EXTERNAL_STORAGE)) {

                    Debug.Log("★★★ ShouldShowRequestPermission");
                    // 팝업 띄우기
                }

                _aosPermissionChecker.OpenChecker(OnCompleteAndroidRuntimePermissionCheck);
                //_isStartBtnClicked = false;
                return;

            }
        }


    #endif
    }


    void OnCompleteAndroidRuntimePermissionCheck() {
        StartCoroutine(DelayStart());
    }

    IEnumerator DelayStart() {
        yield return new WaitForSeconds(2);

        ConnectGame();
    }

    #endregion





    /// <summary>
    /// 게임 시작
    /// </summary>
    public void StartGame() {
        _isStartBtnClicked = true;

        if (string.IsNullOrEmpty(GameSystem.Instance.UserName)) {

            Debug.Log("Nick Open");

            _firstNickCtrl.gameObject.SetActive(true);

            KillTweening();
            _tapStart.transform.DOScale(0, 0.3f).SetEase(Ease.OutBack);

            // 닉네임을 처음 띄울때 미션관련 로컬 데이터 삭제
            GameSystem.Instance.DeleteMissionData();

            return;
       }


        Debug.Log("Starting");

        KillTweening();
        _tapStart.transform.DOScale(0, 0.3f).SetEase(Ease.OutBack);

        // 중복 호출을 막는다. 
        if (_isLoadingLobbyScene)
            return;

        _isLoadingLobbyScene = true;

        GameSystem.Instance.CheckWeekFirstConnect();
        GameSystem.Instance.LoadLobbyScene(); // 로비 진입 
    }


    #region 팝업 제어 
    public void ExitGame() {
		Application.Quit ();
	}

    
	public void ClosePopup() {
		_currentPopup.SetActive (false);
		_currentPopup = null;
	}

    #endregion


    /// <summary>
    /// 로딩 메세지 처리 
    /// </summary>
    /// <param name="pMessage">P message.</param>
    public void SetLoadingMessage(string pMessage) {
        //_lblLoading.text = pMessage;

        switch(_loadingTextIndex) {
            case 0:
                _postLoadingMsg = string.Empty;
                break;
            case 1:
                _postLoadingMsg = ".";
                break;
            case 2:
                _postLoadingMsg = "..";
                break;
            case 3:
                _postLoadingMsg = "...";
                break;
        }

        _lblLoading.text = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3040) + _postLoadingMsg;

        _loadingTextIndex++;

        if (_loadingTextIndex > 3)
            _loadingTextIndex = 0;

    }

	/// <summary>
	/// 접속 버튼 처리 
	/// </summary>
	/// <returns>The tap text.</returns>
	IEnumerator BlinkTapText() {

		//로딩 메세지 제거 
		_lblLoading.gameObject.SetActive (false);

        _tapStart.gameObject.SetActive(true);
        _tapStart.DOScale(1, 0.5f).SetEase(Ease.InOutBounce);


        yield return new WaitForSeconds(1.5f);
        

		// 깜빡깜빡 
		while (!_isStartBtnClicked) {

            _tapStart.DOShakeScale(0.2f, 0.5f, 10, 20).OnComplete(OnCompletePunchScale);
            yield return new WaitForSeconds(Random.Range(1.5f, 3));

		}
        
	}

    private void OnCompletePunchScale() {
        _tapStart.localScale = new Vector3(1, 1, 1);
    }



    /// <summary>
    /// Editor에서 연결 
    /// </summary>
    private void ConnectEditor() {
        Debug.Log("▣▣▣ Unity Editor ");
        //5d0ec7c04f4b2e8f
        //GameSystem.Instance.UserID = "g18154655887972306480";
        //GameSystem.Instance.DeviceID = "b624c8f9c0ef03ab";
        GameSystem.Instance.UserID = "developer6";
        GameSystem.Instance.DeviceID = "developer6";
        //GameSystem.Instance.UserName = "W35 Room";
        GameSystem.Instance.Platform = "android";

        ConnectMNP();
    }


    /// <summary>
    /// 게임 로그은 시작 
    /// </summary>
	private void ConnectGame() {


        // 로컬 저장 데이터 로드 
        GameSystem.Instance.InitExternalGameData();


#if UNITY_EDITOR || UNITY_STANDALONE_WIN
        ConnectEditor();

#elif UNITY_IOS

        
		ConnectiOS();


#elif UNITY_WEBGL

        InitCanvasFB();

        
#elif UNITY_ANDROID

        ConnectAndroid();

#endif

    }



    #region iOS,  GameCener 접속

    /// <summary>
    /// iOS 접속 시작 
    /// </summary>
    void ConnectiOS() {
        GameSystem.Instance.Platform = "ios";
        GameSystem.Instance.UserID = GameSystem.Instance.DeviceID;

        if(GameCenterManager.IsInitialized) {
            ConnectMNP();
        }
        else {
            GameCenterManager.OnAuthFinished += OnIOSAuthFinished;
            GameCenterManager.Init();
        }
    }


    private void OnIOSAuthFinished(SA.Common.Models.Result res) {
		Debug.Log (">> OnIOS Auth Finished");

		

		if (res.IsSucceeded) {
            // 정상적으로 로그인이 된 경우에는 델리게이트 제거 
            GameCenterManager.OnAuthFinished -= OnIOSAuthFinished;
            GameSystem.Instance.IosAuthed = true;

			//IOSNativePopUpManager.showMessage("Player Authed ", "ID: " + GameCenterManager.Player.Id + "\n" + "Alias: " + GameCenterManager.Player.Alias);

			GameCenterManager.LoadLeaderboardInfo("mnp.collection");

            GameSystem.Instance.UserID = GameCenterManager.Player.Id;


        } else {
			// System message
			Debug.Log (">>>> Auth Failed");

            
            GameSystem.Instance.IosAuthed = false;
            GameSystem.Instance.UserID = GameSystem.Instance.DeviceID;

        }

        // Game Server Connect
        ConnectMNP();
	}

    #endregion

    #region 안드로이드, 구글 플레이 서비스 접속


    /// <summary>
    /// 안드로이드 게임 로그인 
    /// </summary>
    private void ConnectAndroid() {

        Debug.Log("▣▣▣ Unity Android ");
        GameSystem.Instance.Platform = "android";

        if (GooglePlayConnection.State == GPConnectionState.STATE_CONNECTED) { // 이미 접속되어 있는 상태에서는 구글플레이에 또 로그인하지 않음. 바로 서버 접속 시작 
            GameSystem.Instance.CurrentPlayer = GooglePlayManager.Instance.player;
            GameSystem.Instance.UserID = GooglePlayManager.Instance.player.playerId;
            ConnectMNP(); 
        }
        else {
            GooglePlayConnection.ActionConnectionResultReceived += ActionConnectionResultReceived; // 구글플레이 접속 
            GooglePlayConnection.Instance.Connect();
        }

    }

    /// <summary>
    /// 구글 플레이 접속 
    /// </summary>
    /// <param name="result"></param>
    private void ActionConnectionResultReceived(GooglePlayConnectionResult result) {
		
		Debug.Log ("!! ActionConnectionResultReceived ");
        GooglePlayConnection.ActionConnectionResultReceived -= ActionConnectionResultReceived; // 이벤트에서 제거 한다. 

        if (result.IsSuccess) {
			Debug.Log("Connected!");
			GameSystem.Instance.CurrentPlayer = GooglePlayManager.Instance.player;
            GameSystem.Instance.UserID = GameSystem.Instance.CurrentPlayer.playerId;

			// 리더보드 로드 
			GooglePlayManager.ActionLeaderboardsLoaded += OnLeaderBoardsLoaded;
			GooglePlayManager.Instance.LoadLeaderBoards ();

			// 업적 로드 
			GooglePlayManager.ActionAchievementsLoaded += OnAchievementsLoaded;
			GooglePlayManager.Instance.LoadAchievements ();

        } else {
			Debug.Log("Cnnection failed with code: " + result.code.ToString());
            GameSystem.Instance.UserID = GameSystem.Instance.DeviceID;
            
        }

        ConnectMNP(); // MNP 서버 접속 시작 

    }



    /// <summary>
    /// 리더보드 조회 완료 
    /// </summary>
    /// <param name="result">Result.</param>
    private void OnLeaderBoardsLoaded(GooglePlayResult result) {

		GooglePlayManager.ActionLeaderboardsLoaded -= OnLeaderBoardsLoaded;
		if(result.IsSucceeded) {
			if( GooglePlayManager.Instance.GetLeaderBoard(GameSystem.Instance.LEADERBOARD_ID) == null) {
				//Debug.Log("Leader boards loaded" + GameSystem.Instance.LEADERBOARD_ID + " not found in leader boards list");
				return;
			}
			
			//GPLeaderBoard leaderboard = GooglePlayManager.Instance.GetLeaderBoard(GameSystem.Instance.LEADERBOARD_ID);
			//long score = leaderboard.GetCurrentPlayerScore(GPBoardTimeSpan.ALL_TIME, GPCollectionType.GLOBAL).LongScore;
			
			//Debug.Log(GameSystem.Instance.LEADERBOARD_ID + "  score" +  score.ToString());
		} else {
			//AndroidMessage.Create("Leader-Boards Loaded error: ", result.message);
		}
	}


	/// <summary>
	/// 업적 조회 완료 
	/// </summary>
	/// <param name="result">Result.</param>
	private void OnAchievementsLoaded(GooglePlayResult result) {
		GooglePlayManager.ActionAchievementsLoaded -= OnAchievementsLoaded;

		if(result.IsSucceeded) {
		} else {
		}
		
	}


    #endregion


    #region 게임 서버 접속 



    /// <summary>
    /// 밋치리네코 게임 서버 접속 시작 
    /// </summary>
    public void ConnectMNP() {
        if (!bgmSource.isPlaying) {
            bgmSource.Play(); // BGM 플레이 
        }

        _lblLoading.gameObject.SetActive(true);
        SetLoadingMessage(GameSystem.Instance.GetLocalizeText("3400"));

        GameSystem.Instance.ConnectServer();

        GameSystem.Instance.SetLongTimeDisconnectPush();
    }

    /// <summary>
    /// 서버와의 접속 완료 
    /// </summary>
    public void OnCompleteGameConnect() {

		// Complete Server Connect 
		GameSystem.Instance.IsConnectedServer = true;

        // 기타 툴 (페이스북, 오퍼월 초기화)
        InitLiveOps();

        // 접속 완료
        isCompletedLoading = true;
        StartCoroutine(BlinkTapText());

        _touchScreen.SetActive(true);

    }




    /// <summary>
    /// LiveOps 초기화 
    /// </summary>
	void InitLiveOps() {

        // 모든 과정 종료 되었으면 페이스북 초기화 
        //MNPFacebookCtrl.Instance.InitFB();

        Debug.Log (">>> InitLiveOps >>>>>>>>");

        /* LiveOps 초기화 */

        if (Application.isEditor)
            return;

#if UNITY_ANDROID
        InitLiveOpsAOS();
#elif UNITY_IOS
        InitLiveOpsIOS();
#endif

    }


    private void InitLiveOpsAOS() {


        Debug.Log("▶▶▶▶ InitLiveOpsAOS :: " + WWWHelper.Instance.UserDBKey.ToString());

        //유저식별값 설정
        IgaworksUnityPluginAOS.Common.setUserId(WWWHelper.Instance.UserDBKey.ToString());

        Debug.Log("▶▶▶▶ InitLiveOpsAOS Set UserID");

        //라이브옵스 초기화
        IgaworksUnityPluginAOS.LiveOps.initialize();
        //IgaworksUnityPluginAOS.LiveOps.setNotificationIconStyle("push_small_icon", "app_icon", "ffffffff");

        Debug.Log("▶▶▶▶ InitLiveOpsAOS Inited");


        GameSystem.Instance.IsLiveOpsInit = true;
        //GameSystem.Instance.IsLiveOpsInit = false;



    }

    private void InitLiveOpsIOS() {
        IgaworksCorePluginIOS.SetUserId(WWWHelper.Instance.UserDBKey.ToString());
		LiveOpsPluginIOS.LiveOpsInitPush ();

        GameSystem.Instance.IsLiveOpsInit = true;

    }

    #endregion

    #region FB Canvas (WEB)

    private void InitCanvasFB() {

        Debug.Log(">>> InitCanvasFB");

        if (!FB.IsInitialized)
            MNPFacebookCtrl.Instance.InitFB();

    }

    


    public void OnCanvasInit() {

        Debug.Log(">> OnCanvasInit");

        if(FB.IsLoggedIn) {
            // 버튼 제거 
            _btnFBLogin.SetActive(false);
            GameSystem.Instance.UserID = MNPFacebookCtrl.Instance.UserId;
            GameSystem.Instance.DeviceID = MNPFacebookCtrl.Instance.UserId;
            GameSystem.Instance.Platform = "facebook";

            Debug.Log(">> Connect Server");
            GameSystem.Instance.ConnectServer();

        } 
        else {
            _btnFBLogin.SetActive(true);
        }

    }


    /// <summary>
    /// Logins the facebook.
    /// </summary>
    public void LoginFacebook() {
        MNPFacebookCtrl.Instance.LoginFB();
    }


    #endregion


    #region Permission

#if UNITY_ANDROID

    public int GetSDKLevel() {

        int sdkLevel = 0;

        try {
            var clazz = AndroidJNI.FindClass("android.os.Build$VERSION");
            var fieldID = AndroidJNI.GetStaticFieldID(clazz, "SDK_INT", "I");
            sdkLevel = AndroidJNI.GetStaticIntField(clazz, fieldID);
        }
        catch (System.Exception e) {
            Debug.Log("★★★★ GetSDKLevel Exception ");

        }

        Debug.Log("★★★★ GetSDKLevel :: " + sdkLevel);

        return sdkLevel;
    }
#endif 

#endregion


}
