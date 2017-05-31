using UnityEngine;
using System.Collections;
using IgaworksUnityAOS;

public partial class GameSystem : MonoBehaviour {

	[SerializeField] string _version;
	[SerializeField] int _tutorialComplete = 0;

    string _javaAndroidID = string.Empty;
    

	private PackageAppInfo packageInfo;


	private string debugMsg = string.Empty;
	private AndroidMessage systemMsg = null;
    
    private string googleMarketURL = "market://details?id=";
    private string iOSMarketURL = string.Empty;



    public readonly string LEADERBOARD_ID = "CgkIgcy5lKECEAIQAA";

	public readonly string ACHIEVEMENT_ID1 = "CgkIgcy5lKECEAIQAQ"; // 고양이 10마리 
    public readonly string ACHIEVEMENT_ID2 = "CgkIgcy5lKECEAIQAg"; // 30마리
	public readonly string ACHIEVEMENT_ID3 = "CgkIgcy5lKECEAIQAw"; // 50마리
	public readonly string ACHIEVEMENT_ID4 = "CgkIgcy5lKECEAIQBA"; // 70마리
	public readonly string ACHIEVEMENT_ID5 = "CgkIgcy5lKECEAIQBQ"; // 100마리

    public readonly string IOS_ACHIEVEMENT_ID1 = "mnp.collection.10"; // 고양이 10마리 
    public readonly string IOS_ACHIEVEMENT_ID2 = "mnp.collection.30"; // 30마리
    public readonly string IOS_ACHIEVEMENT_ID3 = "mnp.collection.50"; // 50마리
    public readonly string IOS_ACHIEVEMENT_ID4 = "mnp.collection.70"; // 70마리
    public readonly string IOS_ACHIEVEMENT_ID5 = "mnp.collection.100"; // 100마리

    private int _previousHeartNotificationID = -1;




    #region 업적, 리더보드 

    #region 공통 메소드

    public void OpenAchievement() {

		#if UNITY_IOS

		GameCenterManager.ShowAchievements();

		#elif UNITY_ANDROID

		GooglePlayManager.Instance.ShowAchievementsUI ();
		#endif
	}

	public void OpenLeaderBoard() {


		#if UNITY_IOS
		GameCenterManager.ShowLeaderboards ();

		#elif UNITY_ANDROID

		GooglePlayManager.Instance.ShowLeaderBoardsUI ();

		#endif

	}

    public void SubmitLeaderBoardScore(int pScore) {

#if UNITY_ANDROID
        SubmitGoogleLeaderboard(pScore);
#endif

#if UNITY_IOS

		Debug.Log("SubmitLeaderBoardScore long pScore:: " + pScore.ToString());
		GameCenterManager.OnScoreSubmitted += OnScoreSumittedIOS;
		GameCenterManager.ReportScore (pScore, "mnp.collection",0);
#endif
    }

    public void SubmitLeaderBoardScore(long pScore) {

#if UNITY_ANDROID
        //SubmitGoogleLeaderboard(pScore);
#endif

#if UNITY_IOS
		GameCenterManager.OnScoreSubmitted += OnScoreSumittedIOS;
		GameCenterManager.ReportScore (pScore, "mnp.collection",0);
#endif



    }


    public void CheckAchievements() {

#if UNITY_ANDROID

        CheckPlayStoreAchievement();

#endif

#if UNITY_IOS
		CheckIosAchievement ();
#endif

    }

    #endregion


    #region 구글 플레이스토어 리더보드, 업적 




	private void HandleOnAchievementsProgress (GK_AchievementProgressResult result) {
		if(result.IsSucceeded) {
			GK_AchievementTemplate tpl = result.Achievement;
			Debug.Log (tpl.Id + ":  " + tpl.Progress.ToString());
			//IOSNativePopUpManager.showMessage ("Achievement", tpl.Id + " / " + tpl.Description + " : " + tpl.Progress.ToString ());
		}

		GameCenterManager.OnAchievementsProgress -= HandleOnAchievementsProgress;
	}


	private void OnScoreSumittedIOS(GK_LeaderboardResult result) {

		GameCenterManager.OnScoreSubmitted -= OnScoreSumittedIOS;


		if (result.IsSucceeded) {
			Debug.Log ("OnScoreSumitted Succeeded");
			//IOSNativePopUpManager.showMessage ("Confirm", "Score Sumitted");
		} else {
			Debug.Log ("OnScoreSumitted Failed :: " + result.Error.Code + " / " + result.Error.Message);
			//IOSNativePopUpManager.showMessage ("Confirm", "Score Submit Fail :: "+ result.Error.Code + " / " + result.Error.Description);
		}
	}


	/// <summary>
	/// Checks the ios achievement.
	/// </summary>
	/// <param name="pType">P type.</param>
	private void CheckIosAchievement() {

        if (GameCenterManager.Achievements == null || GameCenterManager.Achievements.Count == 0) {
            return;
        }

        if (GameCenterManager.GetAchievementProgress(IOS_ACHIEVEMENT_ID1) < 100 && GetUserNekoCount() >= 10) {
            UnLockIosAchievement(IOS_ACHIEVEMENT_ID1);
        }

        if (GameCenterManager.GetAchievementProgress(IOS_ACHIEVEMENT_ID2) < 100 && GetUserNekoCount() >= 30) {
            UnLockIosAchievement(IOS_ACHIEVEMENT_ID2);
        }

        if (GameCenterManager.GetAchievementProgress(IOS_ACHIEVEMENT_ID3) < 100 && GetUserNekoCount() >= 50) {
            UnLockIosAchievement(IOS_ACHIEVEMENT_ID3);
        }

        if (GameCenterManager.GetAchievementProgress(IOS_ACHIEVEMENT_ID4) < 100 && GetUserNekoCount() >= 70) {
            UnLockIosAchievement(IOS_ACHIEVEMENT_ID4);
        }

        if (GameCenterManager.GetAchievementProgress(IOS_ACHIEVEMENT_ID5) < 100 && GetUserNekoCount() >= 100) {
            UnLockIosAchievement(IOS_ACHIEVEMENT_ID5);
        }

    }

	private void UnLockIosAchievement(string id) {
		GameCenterManager.OnAchievementsProgress += HandleOnAchievementsProgress;
		GameCenterManager.SubmitAchievement (100, id, true);
	}

    



#if UNITY_ANDROID
    /// <summary>
    /// 리더보드 스코어 제출 
    /// </summary>
    /// <param name="pScore">P score.</param>
    private void SubmitGoogleLeaderboard(int pScore) {
		GooglePlayManager.Instance.SubmitScoreById (LEADERBOARD_ID, pScore);
	}

	/// <summary>
	/// 구글 플레이 스토어 업적 해제 
	/// </summary>
	/// <param name="pAchievementID">P achievement I.</param>
	private void UnLocPlayStorekAchievements(string pAchievementID) {
		GooglePlayManager.Instance.UnlockAchievementById (pAchievementID);
	}

    private void CheckPlayStoreAchievement() {

        if (CurrentPlayer == null)
            return;

        try {


            if (GooglePlayManager.Instance.Achievements == null || GooglePlayManager.Instance.Achievements.Count == 0)
                return;


            if (GooglePlayManager.Instance.GetAchievement(ACHIEVEMENT_ID1).State != GPAchievementState.STATE_UNLOCKED && GetUserNekoCount() >= 10) {
                UnLocPlayStorekAchievements(ACHIEVEMENT_ID1);
            }

            if (GooglePlayManager.Instance.GetAchievement(ACHIEVEMENT_ID2).State != GPAchievementState.STATE_UNLOCKED && GetUserNekoCount() >= 30) {
                UnLocPlayStorekAchievements(ACHIEVEMENT_ID2);
            }

            if (GooglePlayManager.Instance.GetAchievement(ACHIEVEMENT_ID3).State != GPAchievementState.STATE_UNLOCKED && GetUserNekoCount() >= 50) {
                UnLocPlayStorekAchievements(ACHIEVEMENT_ID3);
            }

            if (GooglePlayManager.Instance.GetAchievement(ACHIEVEMENT_ID4).State != GPAchievementState.STATE_UNLOCKED && GetUserNekoCount() >= 70) {
                UnLocPlayStorekAchievements(ACHIEVEMENT_ID4);
            }

            if (GooglePlayManager.Instance.GetAchievement(ACHIEVEMENT_ID5).State != GPAchievementState.STATE_UNLOCKED && GetUserNekoCount() >= 100) {
                UnLocPlayStorekAchievements(ACHIEVEMENT_ID5);
            }
        }
        catch (System.Exception e) {
            Debug.Log("★ e ::" + e.StackTrace);
        }

    }




#endif

    #endregion




    #endregion

    #region 패키지 정보 조회 

    /// <summary>
    /// 패키지 정보 조회
    /// </summary>
    private void LoadPackageInfo() {

        Debug.Log(">>>  LoadPackageInfo");

        if (Application.isEditor)
            return;


        #if UNITY_ANDROID
		

        AndroidAppInfoLoader.ActionPacakgeInfoLoaded += OnPackageInfoLoaded;
		AndroidAppInfoLoader.Instance.LoadPackageInfo ();

        AndroidNativeUtility.OnAndroidIdLoaded += OnAndroidIDLoaded;
        AndroidNativeUtility.Instance.LoadAndroidId();

#elif UNITY_IOS

		ShowDevoceInfo();

#endif

    }

    #region package Info 

#if UNITY_ANDROID


	private void OnPackageInfoLoaded(PackageAppInfo PacakgeInfo) {
		AndroidAppInfoLoader.ActionPacakgeInfoLoaded -= OnPackageInfoLoaded;
		packageInfo = PacakgeInfo;

		debugMsg += AndroidAppInfoLoader.Instance.PacakgeInfo.packageName + "\n";
		debugMsg += AndroidAppInfoLoader.Instance.PacakgeInfo.versionCode + "\n";
		debugMsg += AndroidAppInfoLoader.Instance.PacakgeInfo.versionName + "\n";

        //AndroidAppInfoLoader.Instance.PacakgeInfo.

        Debug.Log("OnPackageInfoLoaded :: " + debugMsg);

        googleMarketURL = googleMarketURL + AndroidAppInfoLoader.Instance.PacakgeInfo.packageName;
    }

    private void OnAndroidIDLoaded(string id) {
        AndroidNativeUtility.OnAndroidIdLoaded -= OnAndroidIDLoaded;
        DeviceID = id;

        Debug.Log("OnAndroidIDLoaded :: " + DeviceID);
        JavaAndroidID = GetDeviceIDByJava();

    }

    /// <summary>
    /// Gets the device ID
    /// </summary>
    /// <returns>The device I.</returns>
    private string GetDeviceIDByJava() {
        try {
            AndroidJavaClass up = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = up.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaObject contentResolver = currentActivity.Call<AndroidJavaObject>("getContentResolver");
            AndroidJavaClass secure = new AndroidJavaClass("android.provider.Settings$Secure");
            return secure.CallStatic<string>("getString", contentResolver, "android_id");
        }
        catch (AndroidJavaException e) {

            Debug.Log(e.StackTrace);

            return "Exception 1";

        }
        catch (UnityException e) {
            Debug.Log(e.StackTrace);
            return "Exception 2";

        }


    }


#endif

#if UNITY_IOS

    // iOS 디바이스 정보 
    public ISN_Device iOS_Device;

	private void ShowDevoceInfo() {
		iOS_Device = ISN_Device.CurrentDevice;
		debugMsg = "";
		debugMsg += "Device Info Name: "  + iOS_Device.Name + "\n";
		debugMsg += "System Name: " + iOS_Device.SystemName + "\n";
		debugMsg += "Model: " + iOS_Device.Model + "\n";
		debugMsg += "Localized Model: " + iOS_Device.LocalizedModel + "\n";
		debugMsg += "System Version: " + iOS_Device.SystemVersion + "\n";
		debugMsg += "Major System Version: " + iOS_Device.MajorSystemVersion + "\n";
		debugMsg += "User Interface Idiom: " + iOS_Device.InterfaceIdiom + "\n";
		debugMsg += "GUID in Base64: " + iOS_Device.GUID.Base64String ;

		Debug.Log("ShowDevoceInfo :: " + debugMsg);

        DeviceID = iOS_Device.GUID.Base64String;
	}

#endif



    #endregion

    #endregion

    #region Native 메세지 시스템 

    /// <summary>
    /// 중요한 시스템 단순 메세지를 띄워주는데 사용된다. 
    /// </summary>
    /// <param name="pMsg">P message.</param>
    public void SetSystemMessage(string pMsg) {

#if UNITY_ANDROID || UNITY_EDITOR
        SetAndroidSystemMessage(pMsg);

#elif UNITY_IOS

		SetIosSystemMessage(pMsg);

#endif
    }

    public void SetSystemMessage(string pTitle, string pMsg, string pType) {
#if UNITY_ANDROID || UNITY_EDITOR

        SetAndroidSystemMessage(pTitle, pMsg, pType);

#elif UNITY_IOS

        SetiOSSystemMessage(pTitle, pMsg, pType);
#endif
    }

    /// <summary>
    /// ios 시스템 메세지 팝업 호출 
    /// </summary>
    /// <param name="pMsg"></param>
    private void SetIosSystemMessage(string pMsg) {
        if (pMsg.IndexOf("login failed") >= 0) {
            IOSMessage msg = IOSMessage.Create("확인", "안전한 게임 플레이를 위해 게임센터에 접속해 주세요. 게임센터에 접속하지 않으면 게임 이용이 불가능합니다. ");
            msg.OnComplete += OnGameCenterLoginFailMessageClose;
        }
        else if (pMsg.IndexOf("invalid version") >= 0) { // 버전맞지 않음 

            IOSMessage msg = IOSMessage.Create(GameSystem.Instance.GetLocalizeText("4200"), GameSystem.Instance.GetLocalizeText("4201"));
            msg.OnComplete += OnSystemVersionIOSClose;

        }
        else if (pMsg.IndexOf("server inspection") >= 0) { // 서버 점검 처리 

            /*
            IOSMessage msg = IOSMessage.Create(GameSystem.Instance.GetLocalizeText("3233")
                , GameSystem.Instance.GetLocalizeText("3485") + "[" + _resultForm["data"]["starttime"].Value + "] ~ [" + _resultForm["data"]["endtime"].Value + "]");
                */

            IOSMessage msg = IOSMessage.Create(GameSystem.Instance.GetLocalizeText("3233")
                , GameSystem.Instance.GetLocalizeText("3485") + _resultForm["data"]["msg"].Value); 
            //msg = GameSystem.Instance.GetLocalizeText("3485") + _resultForm["data"]["msg"].Value;

            msg.OnComplete += OnSystemUnderInspectioniOS;

        }
        else if (pMsg.IndexOf("fail request purchase") >= 0) {

            IOSMessage msg = IOSMessage.Create(GameSystem.Instance.GetLocalizeText("3489")
                , GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3488));

        }
        else if (pMsg.IndexOf("re-login") >= 0) {
            IOSMessage msg = IOSMessage.Create(GameSystem.Instance.GetLocalizeText("3233"), GameSystem.Instance.GetLocalizeText("3054")); // 메세지 가공
            msg.OnComplete += OnSystemReLoginMessageCloseiOS;
        }
        else if (pMsg.IndexOf("duplication access") >= 0) { // 중복로그인
            IOSMessage msg = IOSMessage.Create(GameSystem.Instance.GetLocalizeText("3233"), GameSystem.Instance.GetLocalizeText("3550")); // 메세지 가공
            msg.OnComplete += OnSystemReLoginMessageCloseiOS;
        }
        else if (pMsg.IndexOf("timeout-connect") >= 0) {
            //IOSMessage msg = IOSMessage.Create(GameSystem.Instance.GetLocalizeText("3233"), GameSystem.Instance.GetLocalizeText("3484")); // 네트워크 환경
            CreateIOS_Dialog(pMsg); //
        }



    }

    private void OnSystemUnderInspectioniOS() {
        Application.Quit();
    }

    private void OnGameCenterLoginFailMessageClose() {
        Application.Quit();
    }




    /// <summary>
    /// 
    /// </summary>
    /// <param name="pTitle"></param>
    /// <param name="pMsg"></param>
    /// <param name="pType"></param>
    private void SetiOSSystemMessage(string pTitle, string pMsg, string pType) {
        IOSMessage msg = IOSMessage.Create(pTitle, pMsg);
        msg.OnComplete += OnGameCenterLoginFailMessageClose;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="pTitle"></param>
    /// <param name="pMsg"></param>
    /// <param name="pType"></param>
    private void SetAndroidSystemMessage(string pTitle, string pMsg, string pType) {
        AndroidMessage msg = AndroidMessage.Create(pTitle, pMsg);
        if (pType == "quit") {
            msg.ActionComplete += OnSystemUnderInspection;
        }
    }

    /// <summary>
    /// 안드로이드 시스템 메세지 
    /// </summary>
    /// <param name="pMsg"></param>
    private void SetAndroidSystemMessage(string pMsg) {

        Debug.Log(">>> SetAndroidSystemMessage");

        // message에 따라서 갈린다
        if (pMsg.IndexOf("invalid version") >= 0) { // 버전맞지 않음 
            systemMsg = AndroidMessage.Create(GameSystem.Instance.GetLocalizeText("4200"), GameSystem.Instance.GetLocalizeText("4201"));
            systemMsg.ActionComplete += OnSystemVersionMessageClose;
        }
        else if (pMsg.IndexOf("re-login") >= 0) { // 리 로그인 
            systemMsg = AndroidMessage.Create(GameSystem.Instance.GetLocalizeText("3233"), GameSystem.Instance.GetLocalizeText("3054")); // 메세지 가공
            systemMsg.ActionComplete += OnSystemReLoginMessageClose;
        }
        else if (pMsg.IndexOf("duplication access") >= 0) { // 중복로그인
            systemMsg = AndroidMessage.Create(GameSystem.Instance.GetLocalizeText("3233"), GameSystem.Instance.GetLocalizeText("3550")); // 메세지 가공
            systemMsg.ActionComplete += OnSystemReLoginMessageClose;
        }
        else if (pMsg.IndexOf("server inspection") >= 0) {
            string msg;
            // msg = GameSystem.Instance.GetLocalizeText("3485") + "[" + _resultForm["data"]["starttime"].Value + "] ~ [" + _resultForm["data"]["endtime"].Value + "]";
            msg = GameSystem.Instance.GetLocalizeText("3485") + _resultForm["data"]["msg"].Value;
            systemMsg = AndroidMessage.Create(GameSystem.Instance.GetLocalizeText("3233"), msg); // 메세지 가공
            systemMsg.ActionComplete += OnSystemUnderInspection;
        }
        else if (pMsg.IndexOf("fail request purchase") >= 0) {
            systemMsg = AndroidMessage.Create(GameSystem.Instance.GetLocalizeText("3489")
                , GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3488));
        }
        else if (pMsg.IndexOf("timeout-connect") >= 0) {
            //systemMsg = AndroidMessage.Create(GameSystem.Instance.GetLocalizeText("3233"), GameSystem.Instance.GetLocalizeText("3484")); // 메세지 가공
            CreateAOS_Dialog(pMsg);
        }

        else {
            systemMsg = AndroidMessage.Create(GameSystem.Instance.GetLocalizeText("3233"), pMsg);
        }
    }

    public void SetBlockMessage() {

#if UNITY_ANDROID || UNITY_EDITOR

        systemMsg = AndroidMessage.Create(GameSystem.Instance.GetLocalizeText("3233"), GameSystem.Instance.GetLocalizeText("3482")); // 메세지 가공
        systemMsg.ActionComplete += OnSystemReLoginMessageClose;

#elif UNITY_IOS

        IOSMessage msg = IOSMessage.Create(GameSystem.Instance.GetLocalizeText("3233"), GameSystem.Instance.GetLocalizeText("3482")); // 메세지 가공
        msg.OnComplete += OnSystemReLoginMessageCloseiOS;

#endif



    }


    #region Dialog 처리 

    private void CreateIOS_Dialog(string pMsg) {
        if (pMsg.Contains("timeout-connect")) {
            IOSDialog dialog = IOSDialog.Create(GameSystem.Instance.GetLocalizeText("3233"), GameSystem.Instance.GetLocalizeText("3484")
                , GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L4240)
                , GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3403));
            dialog.OnComplete += Dialog_OnCompleteTimeoutConnect;
        }
    }

    private void Dialog_OnCompleteTimeoutConnect(IOSDialogResult obj) {
        switch (obj) {
            case IOSDialogResult.YES:
                WWWHelper.Instance.PostPreviousRequest();
                break;
            case IOSDialogResult.NO:
                Application.Quit();
                break;
        }
    }


    /// <summary>
    /// Android 
    /// </summary>
    /// <param name="pMsg"></param>
    private void CreateAOS_Dialog(string pMsg) {

        if (pMsg.Contains("timeout-connect")) {
            AndroidDialog dialog = AndroidDialog.Create(GameSystem.Instance.GetLocalizeText("3233"), GameSystem.Instance.GetLocalizeText("3484")
                , GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L4240)
                , GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3403));
            dialog.ActionComplete += Dialog_ActionCompleteTimeoutConnect;
        }

    }

    private void Dialog_ActionCompleteTimeoutConnect(AndroidDialogResult obj) {
        switch (obj) {
            case AndroidDialogResult.YES:
                WWWHelper.Instance.PostPreviousRequest();
                break;

            case AndroidDialogResult.NO:
                //Application.Quit();
                ExitGame();
                break;

            default:
                //Application.Quit();
                ExitGame();
                break;
        }
    }

    #endregion

    private void OnSystemMessageClose(AndroidDialogResult result) {
        //systemMsg.ActionComplete -= OnSystemMessageClose;
    }

    /// <summary>
    /// 버전 관련 메세지일때 호출 
    /// </summary>
    /// <param name="result">Result.</param>
    private void OnSystemVersionMessageClose(AndroidDialogResult result) {
        systemMsg.ActionComplete -= OnSystemVersionMessageClose;

        Debug.Log("Application.OpenURL");

        Application.OpenURL(googleMarketURL);  // 마켓으로 보내버리기 
        //Application.Quit();
        ExitGame();



    }

    /// <summary>
    /// 버전이 맞지 않아 스토어로 보내기 
    /// </summary>
    private void OnSystemVersionIOSClose() {
        //IOSNativeUtility.RedirectToAppStoreRatingPage();
        // Application.OpenURL(iosMarketURL);  // 마켓으로 보내버리기 
        IOSNativeUtility.RedirectToAppStoreRatingPage();
        Application.Quit();
    }

    public void OpenGoogleMarketURL() {

#if UNITY_ANDROID
        Application.OpenURL(googleMarketURL);  // 마켓으로 보내버리기 
        

#elif UNITY_IOS

        IOSNativeUtility.RedirectToAppStoreRatingPage();
        //Application.OpenURL(iosMarketURL);  // 마켓으로 보내버리기 
#endif
    }

    /// <summary>
    /// relogin 처리 
    /// </summary>
    /// <param name="result">Result.</param>
    private void OnSystemReLoginMessageClose(AndroidDialogResult result) {
        systemMsg.ActionComplete -= OnSystemReLoginMessageClose;
        Debug.Log("OnSystemReLoginMessageClose");
        LoadTitleScene();
    }

    private void OnSystemReLoginMessageCloseiOS() {
        Debug.Log("OnSystemReLoginMessageClose");
        LoadTitleScene();
    }


    private void OnSystemUnderInspection(AndroidDialogResult result) {
        systemMsg.ActionComplete -= OnSystemUnderInspection;
        //Application.Quit();
        ExitGame();
    }




    /// <summary>
    /// Opens the rate pop up.
    /// </summary>
    public void OpenRatePopUp() {


#if UNITY_ANDROID

        OpenRatePopUpAndroid();
#elif UNITY_IOS

        OpenRatePopUpIOS();

#endif

    }

    private void OpenRatePopUpAndroid() {
        string rateText = GameSystem.Instance.GetLocalizeText("3486");

        // yes later no 
        
        AndroidRateUsPopUp rate = AndroidRateUsPopUp.Create(GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3490)
            , rateText, googleMarketURL
            , GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3491)
            , GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3492)
            , GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3493));
        rate.ActionComplete += OnRatePopUpClose;
    }

    private void OpenRatePopUpIOS() {
        string rateText = GameSystem.Instance.GetLocalizeText("3486");

        IOSRateUsPopUp rate = IOSRateUsPopUp.Create(GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3490)
            , rateText
            , GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3491)
            , GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3492)
            , GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3493));
        rate.OnComplete += onRatePopUpCloseIOS;
    }

    private void onRatePopUpCloseIOS(IOSDialogResult result) {
        switch (result) {
            case IOSDialogResult.RATED:
                Debug.Log("Rate button pressed");
                IOSNativeUtility.RedirectToAppStoreRatingPage();
                SaveRate(true);
                break;
            case IOSDialogResult.REMIND:
                Debug.Log("Remind button pressed");
                SaveRate(false);
                break;
            case IOSDialogResult.DECLINED:
                Debug.Log("Decline button pressed");
                SaveRate(true);
                break;

        }

        //IOSNativePopUpManager.showMessage("Result", result.ToString() + " button pressed");
    }

    /// <summary>
    /// Raises the rate pop up close event.
    /// </summary>
    /// <param name="result">Result.</param>
    private void OnRatePopUpClose(AndroidDialogResult result) {
        switch (result) {
            case AndroidDialogResult.RATED:
                Debug.Log("Rate button pressed");
                SaveRate(true);
                break;
            case AndroidDialogResult.REMIND:
                Debug.Log("Remind button pressed");
                SaveRate(false);
                break;
            case AndroidDialogResult.DECLINED:
                Debug.Log("Decline button pressed");
                SaveRate(true);
                break;

        }
    }

    #endregion


    #region Local Push Notification



    /// <summary>
    /// 장기 미접속자에게 푸시 
    /// </summary>
    public void SetLongTimeDisconnectPush() {

        if (Application.isEditor)
            return;

        SetAndroidLocalNotification(GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3483), GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3494), 86400); // 하루
        SetAndroidLocalNotification(GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3483), GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3495), 259200); // 3일
        SetAndroidLocalNotification(GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3483), GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3496), 604800); // 7일
    }


    /// <summary>
    /// 프리크레인 리셋 시간 푸시 
    /// </summary>
    public void SetFreeCraneResetNotification() {

        

        if (Application.isEditor)
            return;

        if (!_optionFreeCranePush)
            return;

        // 프리크레인을 한번도 안했으면, 리턴 
        if (Remainfreegacha >= 3)
            return; 
        

        if (GetFreeCraneResetTime() <= 60)
            return;


        

#if UNITY_ANDROID
        SetAndroidLocalNotification(GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3483), GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3497), GetFreeCraneResetTime());

#elif UNITY_IOS
        SetiOSLocalNotification(GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3497), GetFreeCraneResetTime());

#endif
    }

    /// <summary>
    /// 하트 가득찼을때 알림
    /// </summary>
    public void SetHeartFullLocalNotification() {

        if (!_optionHeartPush) {
            return;
        }


        Debug.Log(">>>> SetHeartFullLocalNotification Sec :: " + GetHeartFullChargeSeconds());

        if (Application.isEditor)
            return;

        


#if UNITY_ANDROID

        if (GetHeartFullChargeSeconds() > 60) {
            SetAndroidLocalNotification(GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3483), GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3498), GetHeartFullChargeSeconds());
        }

        // Android에서 Application.Quit 에서 발생하는 문제로 게임 시작시점으로 옮긴다. (TitleCtrl)


#elif UNITY_IOS
        if (GetHeartFullChargeSeconds() > 30) {
            SetiOSLocalNotification(GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3498), GetHeartFullChargeSeconds());
        }
       

        SetiOSLocalNotification(GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3494), 86400);
        SetiOSLocalNotification(GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3495), 259200);
        SetiOSLocalNotification(GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3496), 604800);

#endif


    }

    private void SetHotTimeLocalNotification() {

        return;

        if (Application.isEditor)
            return;

#if UNITY_ANDROID
        SetAndroidLocalNotification(GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3483), "今すぐプレーするとコイン獲得が2倍​​！", GetNextHotTimeSeconds());

#elif UNITY_IOS
            SetiOSLocalNotification("今すぐプレーするとコイン獲得が2倍​​！", GetNextHotTimeSeconds());

#endif
    }


    /// <summary>
    /// 안드로이드 로컬 푸시 
    /// </summary>
    /// <param name="pTitle"></param>
    /// <param name="pMsg"></param>
    /// <param name="pSec"></param>
    private void SetAndroidLocalNotification(string pTitle, string pMsg, int pSec) {

        // 30초 미만에서는 푸시를 날리지 않음. 
        if (pSec < 30) {
            Debug.Log("Under 30 seconds. No Push");
            return;
        }

        //Debug.Log("SetAndroidLocalNotification pSec :: " + pSec);

        AndroidNotificationBuilder builder = new AndroidNotificationBuilder(SA.Common.Util.IdFactory.NextId, pTitle, pMsg, pSec);
        AndroidNotificationManager.Instance.ScheduleLocalNotification(builder);
        //_previousHeartNotificationID = AndroidNotificationManager.Instance.ScheduleLocalNotification(pTitle, pMsg, pSec);

    }

    /// <summary>
    /// iOS 로컬 푸시 
    /// </summary>
    /// <param name="pMsg"></param>
    /// <param name="pSec"></param>
    private void SetiOSLocalNotification(string pMsg, int pSec) {


        // 30초 미만에서는 푸시를 날리지 않음. 
        if (pSec < 30) {
            Debug.Log("Under 30 seconds. No iOS Push");
            return;
        }

        Debug.Log("SetiOSLocalNotification pSec :: " + pSec);
        ISN_LocalNotificationsController.OnNotificationScheduleResult += OnNotificationScheduleResult;
        ISN_LocalNotification notification = new ISN_LocalNotification(System.DateTime.Now.AddSeconds(pSec), pMsg, false);
        notification.Schedule();

    }


    /// <summary>
    /// 
    /// </summary>
    public void CancelAllLocalNotification() {

        #if UNITY_ANDROID
            AndroidNotificationManager.Instance.CancelAllLocalNotifications();
        
        #elif UNITY_IOS
		ISN_LocalNotificationsController.Instance.CancelAllLocalNotifications();
        #endif


    }



    private void OnNotificationScheduleResult(SA.Common.Models.Result res) {
        ISN_LocalNotificationsController.OnNotificationScheduleResult -= OnNotificationScheduleResult;
      
    }


    #endregion


    public string MarketURL {
        get {

#if UNITY_IOS
            return iOSMarketURL;
#else
            return googleMarketURL;
#endif

        }
    }

    public string GameVersion {
		get {
			return this._version;
		}
	}

	public int TutorialComplete {
		get {
			return this._tutorialComplete;
		}
	}

    public string JavaAndroidID {
        get {
            return _javaAndroidID;
        }

        set {
            _javaAndroidID = value;
        }
    }
    /*
   public string IosMarketURL {
       get {
           return iosMarketURL;
       }

       set {
           iosMarketURL = value;
       }
   }
*/


}
