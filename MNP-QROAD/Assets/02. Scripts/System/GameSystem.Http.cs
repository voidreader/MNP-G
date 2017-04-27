using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using BestHTTP;
using BestHTTP.Decompression.Zlib;
using System.IO;
//using BestHTTP.Decompression.Zlib;
//using IgaworksUnityAOS;

    
public partial class GameSystem : MonoBehaviour {

    /// <summary> 
    /// 로그인 순서
    /// request_findaccount → request_login
    /// </summary>

    // 통신 
    private long _originalSyncTime = 0;
    private long _syncTime = 0;
    private DateTime _dtSyncTime;
    string _debugSyncTime;
    private int _addHeartTakeTime = 15;

    long _nowTimeTick; // 현재 디바이스 시간의 Tick 

    // 통신 (하트 부분)
    int _heartCount; // 게임 하트 
    int _preHeartCount;
    private long _lastHeartTakeTime; // 마지막에 하트를 수령한 시간.
    string _debugLastHeartTakeTime;
    private long _nextHeartTakeTime; // 하트 수령 예정 시간 
    private DateTime _dtNextHeartTakeTime = new DateTime(); // 하트 수령 예정 시간 DateTime type
    private TimeSpan _remainHeartTakeTimeSpan; // 하트 수령 시간 계산용도 

    bool _hasNewMail = false; // 새로운 메일 유무
    string _debugUserNekoData;

    // JSON
    private JSONNode _resultForm;
    private JSONNode _bankData; // 재화 정보 
    
    private JSONNode _userNekoJSON; // 사용자 소유 고양이 정보 

    private JSONNode _gatchaData; // 가챠 결과 정보 
    private JSONNode _rescueHistory; // 구출 내역 
    private JSONNode _ingameResultData; // 인게임 결과 
    private JSONNode _mailData; // 메일 박스 
    private JSONNode _billJSON; // 인앱빌링 
    private JSONNode _rankJSON; // 랭크 
    private JSONNode _takeHeartJSON; // 하트 
    private JSONNode _userDataJSON;
    private JSONNode _userJSON = null; // 유저 정보 
    private JSONNode _userStageJSON = null; // 사용자 스테이지 JSON
    JSONNode _userThemeJSON = null; // 사용자 테마 진행도 JSON

    private JSONNode _targetNekoRewardJSON; // 네코의 보은 대상 정보 
    private JSONNode _nekoRewardJSON; // 네코의 보은 사용 JSON 
    
    private JSONNode _eventJSON;
    private JSONNode _fishGachaJSON;
    private JSONNode _syncTimeData;
    private JSONNode _fbLinkJSON;
    private JSONNode _userMissionJSON = null; // 사용자 미션 정보


    #region 환경정보 JSON Node 

    private JSONNode _gameVesionJSON = null; // 게임내 데이터 버전 정보 

    private JSONNode _envInitJSON = null; // 환경정보
    private JSONNode _attendanceInitJSON = null; // 출석체크 보상 정보 
    private JSONNode _rankRewardInitJSON = null;

    // 배너정보
    private JSONNode _gatchaBannerInitJSON = null;
    private JSONNode _packageBannerInitJSON = null;
    private JSONNode _noticeBannerInitJSON = null;
    private JSONNode _tempNoticeBannerInitJSON = null;

    private JSONNode _coinShopInitJSON = null; // 코인 샵 
    private JSONNode _trespassRewardJSON = null;

    private JSONNode _userPassivePriceJSON = null;

    private JSONNode _stageDetailJSON = null;
    private JSONNode _stageMasterJSON = null;

    #endregion


    private JSONNode _preFusionNekoJSON; // 퓨전 이전의 네코 정보 (단일 개체)




    private int _preFusionNekoStar;
    private int _preFusionNekoBead;

    JSONNode _viewAdsJSON; // Ads JSON 
    JSONNode _rankRewardJSON;
    JSONNode _attendanceJSON = null;

    private bool _isFreeGatcha = false;

    private DateTime _resetSendHeartTime;
    private DateTime _calcLocaleTime;

    private readonly string _jData = "data";

    List<JSONNode> _listUserNeko = new List<JSONNode>();
    public HeartRequestCtrl CurrentHeartRequest;

    private bool _isRequesting = false;

    private bool _isLoadingImageException = false; // 이미지 로컬 로딩시 오류 여부 

    // 저장될 예정의 배너 버전 
    int _preSavedNoticeBannerVersion = -1;
    int _preSavedGachaBannerVersion = -1;
    int _preSavedPackageBannerVersion = -1;

    #region Waiting Screen


    private void OnOffWaitingRequestInGame(bool isOn) {

        if (InGameCtrl.Instance == null)
            return;

        if (isOn) {
            InGameCtrl.Instance.OnWaitingRequest();
        } else {
            InGameCtrl.Instance.OffWaitingRequest();
        }
    }

    public void OnOffWaitingRequestInLobby(bool isOn) {

        IsRequesting = isOn;

        if (LobbyCtrl.Instance != null) {
            if (isOn) {
                LobbyCtrl.Instance.OnWaitingRequest();
            } else {
                LobbyCtrl.Instance.OffWaitingRequest();
            }
        }
    }




    #endregion

    #region static Event Action 

    MailColumnCtrl _readMailColumn;
    public static event Action OnMailReadAction = delegate { };
    public static event Action<Transform> OnMailAllReadAction = delegate { };

    public static event Action<bool> OnCheckPremiumNekoGift = delegate { };
    public static event Action OnSyncTimeAction = delegate { };

    public static event Action<bool> OnCompleteMorePlay = delegate { };

    public static event Action OnCompleteTrespassReward = delegate { };
    public static event Action<JSONNode> OnCompleteUserEventReward = delegate { };
    public static event Action<JSONNode> OnCompleteSelectBingo = delegate { }; // 도전 빙고 선택 
    public static event Action OnCompleteAroundRankList = delegate { };
    public static event Action<JSONNode> OnCompleteReadNekoTicketList = delegate { };
    public static event Action OnCompleteUserMission = delegate { };
    public static event Action OnCompleteRewardRescue = delegate { };
    

    #endregion

    #region Post2 Set BestHTTP


    private bool CheckRequestState(HTTPRequest request) {

        Debug.Log("CheckRequestState :: " + request.State.ToString());
        
        if (request.State == HTTPRequestStates.ConnectionTimedOut || request.State == HTTPRequestStates.TimedOut
            || request.State == HTTPRequestStates.Error) {
               

            if(!string.IsNullOrEmpty(request.Exception.Message)) {
                Debug.Log("Request Exception :: " + request.Exception.Message);
            }

            OnOffWaitingRequestInLobby(false);
            SetSystemMessage("timeout-connect");
            return false;
        }

        return true;

    }


    #region 환경정보 조회 

    /// <summary>
    /// 게임 버전 정보 조회 
    /// </summary>
    public void Post2GameDataVersion() {
        WWWHelper.Instance.Post2("request_dataversion", OnFinishedGameDataVersion);
    }

    private void OnFinishedGameDataVersion(HTTPRequest request, HTTPResponse response) {

        if (!CheckRequestState(request))
            return;


        Debug.Log(">>> OnFinishedGameDataVersion :: " + response.DataAsText);
        JSONNode result = JSON.Parse(response.DataAsText);

        if (CheckCommonServerError(result))
            return;


        _resultForm = result;

        if(result["result"].AsInt != 0) {
            GameSystem.Instance.SetSystemMessage(result["error"].Value);
            return;
        }

        _gameVesionJSON = result["data"];

        CheckDataVersion(); // Check Data Version에서 다음 패킷 전송




    }





    /// <summary>
    /// 게임 데이터 조회
    /// </summary>
    public void Post2GameData() {
        WWWHelper.Instance.Post2("request_gamedata", OnFinishedGameData);
    }

    private void OnFinishedGameData(HTTPRequest request, HTTPResponse response) {
        if (!CheckRequestState(request))
            return;


        Debug.Log(">>> OnFinishedGameData :: " + response.DataAsText);
        JSONNode result = JSON.Parse(response.DataAsText);

        if (CheckCommonServerError(result))
            return;


        if (response.DataAsText.Contains("env")) {
            EnvInitJSON = result["data"]["env"]["data"];
        }

        if (response.DataAsText.Contains("coin_shop")) {
            CoinShopInitJSON = result["data"]["coin_shop"]["data"];
        }

        if (response.DataAsText.Contains("attendance")) {
            _attendanceInitJSON = result["data"]["attendance"]["data"];
        }

        if (response.DataAsText.Contains("rank")) {
            _rankRewardInitJSON = result["data"]["rank"]["data"];
        }

        if (response.DataAsText.Contains("user_passive")) {
            _userPassivePriceJSON = result["data"]["user_passive"]["data"];
        }


        /* 스테이지 */
        if (response.DataAsText.Contains("stage_base")) {
            StageDetailJSON = result["data"]["stage_base"]["data"];
        }

        if (response.DataAsText.Contains("stage_master")) {
            StageMasterJSON = result["data"]["stage_master"]["data"];
        }

        #region 일일 미션, 주간 미션, 빙고 

        // 일일 미션 정보 설정 
        if (response.DataAsText.Contains("mission_daily")) {

            // 일일 미션의 초기화 정보 설정 
            MissionDayInitJSON = result["data"]["mission_daily"]["data"];

            // 미션 데이터가 갱신된 경우, Progress 역시 초기화 
            //ES2.Save<string>(MissionDayInitJSON.ToString(), "mission_day_progress");

            // 기존 미션이 초기화 되지 않도록 처리 
            RefreshMissionDayProgress();
            
        }


        // 주간 미션 정보 설정 
        if (response.DataAsText.Contains("mission_weekly")) {
            MissionWeekInitJSON = result["data"]["mission_weekly"]["data"];

            // 미션 데이터가 갱신된 경우, Progress 역시 초기화 
            //ES2.Save<string>(MissionWeekInitJSON.ToString(), "mission_week_progress");

            // 기존의 주간 미션이 초기화 되지 않도록 처리 
            RefreshMissionWeekProgress();
        }

        if(response.DataAsText.Contains("bingo")) { // 새 데이터 갱신 시, 진행사항 초기화
            BingoInitJSON = result["data"]["bingo"]["data"];
            //BingoInitJSON = InitBingoProgress(BingoInitJSON);
            UpdateBingoBaseInfo();
            //Debug.Log("BingoInitJSON :: " + BingoInitJSON.ToString());
        }

        if (response.DataAsText.Contains("neko_group")) {
            BingoGroupJSON = result["data"]["neko_group"]["data"];
        }

        #endregion


        if (response.DataAsText.Contains(GATCHA_BANNER_DATA)) {
            GatchaBannerInitJSON = result["data"][GATCHA_BANNER_DATA]["data"];
        }

        if (response.DataAsText.Contains(PACKAGE_BANNER_DATA)) {
            PackageBannerInitJSON = result["data"][PACKAGE_BANNER_DATA]["data"];
        }

        if (response.DataAsText.Contains(NOTICE_BANNER_DATA)) {

            // new 체크를 위해 temp에 담는다. 
            _tempNoticeBannerInitJSON = result["data"][NOTICE_BANNER_DATA]["data"];

            if (NoticeBannerInitJSON != null) { // 기존에 저장했던 알림 정보가 있는 경우

                // 첫 조회시, new 처리 
                for (int i = 0; i < _tempNoticeBannerInitJSON.Count; i++) {

                    _tempNoticeBannerInitJSON[i]["new"].AsBool = true; // 일단 new를 주고 시작한다. 

                    for (int j=0; j<_noticeBannerInitJSON.Count;j++) {

                        if(_tempNoticeBannerInitJSON[i]["bannertext"].Value.Equals(_noticeBannerInitJSON[j]["bannertext"].Value)) {
                            _tempNoticeBannerInitJSON[i]["new"].AsBool = false; // 동일한 내용이 있다면 new 가 아니다. 
                            break;
                        }
                    } // end of j for
                } // end of i for
            }
            else { // null 의 첫 조회 인 경우 
                // 첫 조회시, new 처리 
                for (int i = 0; i < _tempNoticeBannerInitJSON.Count; i++) {
                    _tempNoticeBannerInitJSON[i]["new"].AsBool = true;
                }
            }

            //NoticeBannerInitJSON = result["data"][NOTICE_BANNER_DATA]["data"];
            NoticeBannerInitJSON = _tempNoticeBannerInitJSON;


        }



        
        //Debug.Log("_missionDayInitJSON Count :: " + _missionDayInitJSON.Count);
        //Debug.Log("_missionWeekInitJSON Count :: " + _missionWeekInitJSON.Count);

        // 불러온 데이터 정보 로컬에 저장 
        SaveDataVersion();

        // 로그인 과정 진행 
        Post2FindAccount();
    }





    #endregion


    #region 계정 정보 조회 , 로그인 

    /// <summary>
    /// 계정 Find
    /// </summary>
    public void Post2FindAccount() {
        WWWHelper.Instance.Post2("request_findaccount", OnFinishedRequestFindAccount);
    }

    void OnFinishedRequestFindAccount(HTTPRequest request, HTTPResponse response) {
        if (!CheckRequestState(request))
            return;

        Debug.Log(">>> OnFinishedRequestFindAccount :: " + response.DataAsText);

        JSONNode accountData = JSON.Parse(response.DataAsText);

        if (CheckCommonServerError(accountData)) {
            return;
        }

        accountData = accountData[_jData];

        WWWHelper.Instance.UserDBKey = accountData["userdbkey"].AsInt;
        WWWHelper.Instance.Root = accountData["root"].AsInt;

        Post2RequestLogin();

    }


    #endregion


    #region 게임 서버 접속 GameConnect


    /// <summary>
    /// 게임 서버 접속 
    /// </summary>
    public void ConnectServer() {
        //Post2EnvData();

        Post2GameDataVersion();
    }




    /// <summary>
    /// 로그인 처리 
    /// </summary>
    public void Post2RequestLogin() {
        WWWHelper.Instance.Post2("request_login", OnFinishedRequestLogin);
    }

    private void OnFinishedRequestLogin(HTTPRequest request, HTTPResponse response) {
        if (!CheckRequestState(request))
            return;

        Debug.Log(">>> OnFinishedRequestLogin :: " + response.DataAsText);

        JSONNode loginData = JSON.Parse(response.DataAsText);

        if (CheckCommonServerError(loginData)) {
            return;
        }

        if (loginData["result"].AsInt == 0) {

            // block 여부 체크 
            if (loginData["data"]["block"].AsInt == 1) {
                GameSystem.Instance.SetBlockMessage();
                return;
            }

            WWWHelper.Instance.HttpToken = loginData["data"]["token"].Value;
            // GameSystem.Instance.FacebookID = loginData["data"]["facebookid"].Value;

            DataCode = loginData[_jData]["code"].Value;
            DataCodeExpiredTime = loginData[_jData]["codelimittime"].AsLong;
        }

        // 유저 정보 호출 
        TitleCtrl.Instance.SetLoadingMessage("3400");
        WWWHelper.Instance.Post2("request_userdata", OnFinishedUserData);

    }

    /// <summary>
    /// 사용자 세션 갱신 
    /// </summary>
    public void Post2UserData() {

        Debug.Log("▶ Reload UserData");
        WWWHelper.Instance.Post2("request_userdata", OnFinishedUserDataInGame);
    }

    /// <summary>
    /// 유저 데이터 정보 조회
    /// </summary>
    /// <param name="request"></param>
    /// <param name="response"></param>
    private void OnFinishedUserDataInGame(HTTPRequest request, HTTPResponse response) {

        if (!CheckRequestState(request))
            return;

        Debug.Log(">>> OnFinishedUserDataInGame :: " + response.DataAsText);
        UserDataJSON = JSON.Parse(response.DataAsText);

        if (CheckCommonServerError(UserDataJSON))
            return;

        
        UserJSON = UserDataJSON["data"];

    }


    /// <summary>
    /// 유저 데이터 정보 조회
    /// </summary>
    /// <param name="request"></param>
    /// <param name="response"></param>
    private void OnFinishedUserData(HTTPRequest request, HTTPResponse response) {

        if (!CheckRequestState(request))
            return;

        Debug.Log(">>> OnFinishedUserData :: " + response.DataAsText);
        GameSystem.Instance.UserDataJSON = JSON.Parse(response.DataAsText);

        if (CheckCommonServerError(UserDataJSON))
            return;

        // 빙고 추가 처리 2016.08
        UserJSON = UserDataJSON["data"];
        SetUserBingoProgress(); // GameSystem.Mission


        // 기존 사용자 명과 이름이 같으면 bankdata로 넘어간다.
        if (GameSystem.Instance.UserName.Equals(GameSystem.Instance.UserDataJSON["data"]["nickname"].Value)) {

            TitleCtrl.Instance.SetLoadingMessage("3400");

            WWWHelper.Instance.Post2("request_bankdata", OnFinishedBankData);
            //WWWHelper.Instance.Post("request_nickname", OnFinishedNickName);

        }
        else {
            TitleCtrl.Instance.SetLoadingMessage("3400");
            WWWHelper.Instance.Post2("request_nickname", OnFinishedNickName);
        }

    }

    /// <summary>
    /// 닉네임 요청 Single 
    /// </summary>
    public void Post2RequestNickName(string pNewNickName) {
        //WWWHelper.Instance.Post2("request_nickname", OnFinishedNickNameOnly);
        WWWHelper.Instance.Post2WithString("request_nickname", OnFinishedNickNameOnly, pNewNickName);
    }

    /// <summary>
    /// NickName 통신
    /// </summary>
    /// <param name="request"></param>
    /// <param name="response"></param>
    private void OnFinishedNickNameOnly(HTTPRequest request, HTTPResponse response) {
        if (!CheckRequestState(request))
            return;

        Debug.Log("OnFinishedNickNameOnly :: " + response.DataAsText);
        JSONNode result = JSON.Parse(response.DataAsText);

        if (CheckCommonServerError(result))
            return;


        SimpleNickCtrl simpleNick;

        if (LobbyCtrl.Instance != null) {
            simpleNick = FindObjectOfType(typeof(SimpleNickCtrl)) as SimpleNickCtrl;

            // 성공/실패 처리 
            if (result["result"].AsInt == 0) {

                GameSystem.Instance.UserName = WWWHelper.Instance.RequestedNickName;
                GameSystem.Instance.UserDataJSON["data"]["nickname"] = GameSystem.Instance.UserName;
                simpleNick.SendMessage("CloseSelf");
                LobbyCtrl.Instance.OpenInfoPopUp(PopMessageType.NickNameChanged);

            }
            else {
                simpleNick.SendMessage("OnCompleteFalse");
            }
            
        }

        if(TitleCtrl.Instance != null) {

            Debug.Log("Nick Title Send ");

            FirstNickCtrl firstNickCtrl = FindObjectOfType(typeof(FirstNickCtrl)) as FirstNickCtrl;
            if (result["result"].AsInt == 0) {
                Debug.Log("#1");


                GameSystem.Instance.UserName = WWWHelper.Instance.RequestedNickName;
                GameSystem.Instance.UserDataJSON["data"]["nickname"] = GameSystem.Instance.UserName;
                firstNickCtrl.SendMessage("OnCompleteNickName");

            }
            else {
                Debug.Log("#2");
                firstNickCtrl.SendMessage("OnCompleteFalse");
            }
        }

    }


    /// <summary>
    /// NickName 통신
    /// </summary>
    /// <param name="request"></param>
    /// <param name="response"></param>
    private void OnFinishedNickName(HTTPRequest request, HTTPResponse response) {
        if (!CheckRequestState(request))
            return;

        GameSystem.Instance.UserDataJSON["data"]["nickname"] = GameSystem.Instance.UserName;

        TitleCtrl.Instance.SetLoadingMessage("3400");
        WWWHelper.Instance.Post2("request_bankdata", OnFinishedBankData);
    }

    /// <summary>
    /// 사용자 재화 정보 통신완료
    /// </summary>
    /// <param name="request"></param>
    /// <param name="response"></param>
    private void OnFinishedBankData(HTTPRequest request, HTTPResponse response) {
        if (!CheckRequestState(request))
            return;

        Debug.Log("OnFinishedBankData :: " + response.DataAsText);

        GameSystem.Instance.BankData = JSON.Parse(response.DataAsText);
        TitleCtrl.Instance.SetLoadingMessage("3400");


        WWWHelper.Instance.Post2("request_nekodata", OnFinishedNekoData);
    }

    /// <summary>
    /// 사용자 네코 정보 통신완료
    /// </summary>
    /// <param name="request"></param>
    /// <param name="response"></param>
    private void OnFinishedNekoData(HTTPRequest request, HTTPResponse response) {
        if (!CheckRequestState(request))
            return;

        JSONNode resultNode = JSON.Parse(response.DataAsText);
        GameSystem.Instance.UserNeko = resultNode[_jData]["nekodatas"];

        TitleCtrl.Instance.SetLoadingMessage("3400");
        WWWHelper.Instance.Post2("request_synctime", OnFinishedSyncTime);
        //request_synctime
    }


    private void OnFinishedSyncTime(HTTPRequest request, HTTPResponse response) {
        if (!CheckRequestState(request))
            return;

        GameSystem.Instance.SyncTimeData = JSON.Parse(response.DataAsText);

        // 출첵 호출 
        // 최초 사용자 로그인시는 출첵을 하지 않음
        if (GameSystem.Instance.TutorialComplete != 0) {

            TitleCtrl.Instance.SetLoadingMessage("3400");

            WWWHelper.Instance.Post2("request_attendance", OnFinishedLoginAttend);

            return;
        }

		if (TitleCtrl.Instance != null) {
			Debug.Log ("OnFinishedSyncTime OnCompleteGameConnect");
			TitleCtrl.Instance.OnCompleteGameConnect ();
		}
		else {
			Debug.Log ("OnFinishedSyncTime OnCompleteGameConnect");
			GameSystem.Instance.LoadLobbyScene ();
		}
    }


    private void OnFinishedLoginAttend(HTTPRequest request, HTTPResponse response) {

        if (!CheckRequestState(request))
            return;

        Debug.Log("OnFinishedLoginAttend :: " + response.DataAsText);
             
        GameSystem.Instance.AttendanceJSON = JSON.Parse(response.DataAsText);

		if (TitleCtrl.Instance != null) {
			Debug.Log ("OnFinishedLoginAttend OnCompleteGameConnect");
			TitleCtrl.Instance.OnCompleteGameConnect ();
		}
		else {
			Debug.Log ("OnFinishedLoginAttend OnCompleteGameConnect");
			GameSystem.Instance.LoadLobbyScene ();
		}

        if (AttendanceJSON["result"].AsInt == 0)
            CheckLoginAfterAttendance();

    }


    /// <summary>
    /// 출석체크 보상을 받았을때 함께 처리. (빙고 관련 처리)
    /// </summary>
    private void CheckLoginAfterAttendance() {
        CheckLobbyBingoQuest(60); // 출석 보상을 받을 때는 무조건 1씩 증가.
    }

    #endregion

    #region 미션 진척도 업데이트 

    public void Post2UpdateMissionProgress(JSONNode pNode) {
        WWWHelper.Instance.Post2WithJSON("request_updatemissionprogress", OnFinishedUpdateMissionProgress, pNode);
    }

    private void OnFinishedUpdateMissionProgress(HTTPRequest request, HTTPResponse response) {


        if (!CheckRequestState(request))
            return;

        JSONNode result = JSON.Parse(response.DataAsText);

        Debug.Log(" ▶ OnFinishedUpdateMissionProgress result :: " + response.DataAsText);


        if (CheckCommonServerError(result)) {
            return;
        }

        if (result["result"].AsInt != 0) {
            SetSystemMessage(result["error"].Value);
            return;
        }
    }

    #endregion

    #region 미션 진척도 조회 

    public void Post2UserMission(Action pAction) {

        OnCompleteUserMission = delegate { };
        OnCompleteUserMission += pAction;

        OnOffWaitingRequestInLobby(true);
        UserMissionJSON = null;
        WWWHelper.Instance.Post2("request_usermission", OnFinishedUserMission);
    }

    private void OnFinishedUserMission(HTTPRequest request, HTTPResponse response) {
        if (!CheckRequestState(request))
            return;

        OnOffWaitingRequestInLobby(false);
        Debug.Log(" >>> OnFinishedUserMission :: " + response.DataAsText);

        UserMissionJSON = JSON.Parse(response.DataAsText);

        // re-login 체크 
        if (CheckCommonServerError(UserMissionJSON)) {
            return;
        }


        UserMissionJSON = UserMissionJSON[_jData]["missions"];

        //Debug.Log("★★ UserMissionJSON :: " + UserMissionJSON.ToString());
        //Debug.Log("★★ MissionDayJSON :: " + MissionDayJSON.ToString());


        // 미션정보, 기존 JSON과 연계
        for (int i=0; i<UserMissionJSON.Count; i++) {

            if (UserMissionJSON[i]["missiontype"].Value.Equals("daily")) {

                for (int j = 0; j < MissionDayJSON.Count; j++) {


                    //Debug.Log("tid :: " + MissionDayJSON[j]["tid"].AsInt);
                    //Debug.Log("missionid :: " + UserMissionJSON[i]["missionid"].AsInt);

                    if(MissionDayJSON[j]["tid"].AsInt == UserMissionJSON[i]["missionid"].AsInt && UserMissionJSON[i]["current"].AsInt >= 0) {
                        Debug.Log("★ MissionID Match");

                        if(MissionDayJSON[j]["tid"].AsInt == 13) {
                            Debug.Log("★ MissionID Match 13 ");
                        }

                        MissionDayJSON[j]["progress"].AsInt = UserMissionJSON[i]["progress"].AsInt;
                        MissionDayJSON[j]["current"].AsInt = UserMissionJSON[i]["current"].AsInt;
                        Debug.Log("★★ MissionDayJSON :: " + MissionDayJSON.ToString());
                    }

                    /*
                    if (MissionDayJSON[j]["tid"].Value == UserMissionJSON[i]["missionid"].Value && UserMissionJSON[i]["current"].AsInt >= 0) {
                        Debug.Log("★ MissionID Match2");
                    }

                    // 동일 ID, current가 0이상일때 덮어쓴다. 
                    //if (MissionDayJSON[j]["tid"].AsInt == UserMissionJSON[i]["missionid"].AsInt && UserMissionJSON[i]["current"].AsInt >= 0) {
                    if (MissionDayJSON[j]["tid"].AsInt == UserMissionJSON[i]["missionid"].AsInt ) {

                        MissionDayJSON[j]["progress"].AsInt = UserMissionJSON[i]["progress"].AsInt;
                        MissionDayJSON[j]["current"].AsInt = UserMissionJSON[i]["current"].AsInt;
                    }
                    */
                } // end of daily j for 

            }
            else if (UserMissionJSON[i]["missiontype"].Value.Equals("weekly")) {

                for (int j = 0; j < MissionWeekJSON.Count; j++) {

                    // 동일 ID, current가 0이상일때 덮어쓴다. 
                    if (MissionWeekJSON[j]["tid"].AsInt == UserMissionJSON[i]["missionid"].AsInt && UserMissionJSON[i]["current"].AsInt >= 0) {

                        MissionWeekJSON[j]["progress"].AsInt = UserMissionJSON[i]["progress"].AsInt;
                        MissionWeekJSON[j]["current"].AsInt = UserMissionJSON[i]["current"].AsInt;
                    }
                } // end of weekly j for 

            }
        }

        SaveMissionProgress();
        OnCompleteUserMission();
    }

    #endregion

    #region 빙고 진척도 업데이트 

    public void Post2UpdateBingoProgress(JSONNode pNode) {
        WWWHelper.Instance.Post2WithJSON("request_updatebingoprogress", OnFinishedUpdateBingoProgress, pNode);
    }

    private void OnFinishedUpdateBingoProgress(HTTPRequest request, HTTPResponse response) {


        //OnOffWaitingRequestInLobby(false);


        if (!CheckRequestState(request))
            return;

        JSONNode result = JSON.Parse(response.DataAsText);

        Debug.Log(" ▶ OnFinishedUpdateBingoProgress result :: " + response.DataAsText);


        if (CheckCommonServerError(result)) {
            return;
        }

        if (result["result"].AsInt != 0) {
            SetSystemMessage(result["error"].Value);
            return;
        }


    }


    #endregion


    #region 난입 이벤트 보상 처리, 유저 이벤트 보상 처리 

    // 유저 이벤트 보상 정보 조회 
    public void Post2UserEventReward(Action<JSONNode> pAction) {
        OnOffWaitingRequestInLobby(true);
        OnCompleteUserEventReward += pAction;

        WWWHelper.Instance.Post2("request_eventreward", OnFinishedUserEventReward);
    }

    private void OnFinishedUserEventReward(HTTPRequest request, HTTPResponse response) {


        OnOffWaitingRequestInLobby(false);


        if (!CheckRequestState(request))
            return;

        JSONNode result = JSON.Parse(response.DataAsText);

        Debug.Log(" ▶ OnFinishedUserEventReward result :: " + response.DataAsText);


        if (CheckCommonServerError(result)) {
            return;
        }

        if (result["result"].AsInt != 0) {
            SetSystemMessage(result["error"].Value);
            return;
        }

        OnCompleteUserEventReward(result);
        OnCompleteUserEventReward = delegate { };
    }


    public void Post2Trespass(string pParam, Action pAction) {
        OnOffWaitingRequestInLobby(true);

        OnCompleteTrespassReward += pAction;

        WWWHelper.Instance.Post2WithString("request_rewardtrespass", OnFinishedTrespassReward, pParam);
    }

    private void OnFinishedTrespassReward(HTTPRequest request, HTTPResponse response) {


        OnOffWaitingRequestInLobby(false);


        if (!CheckRequestState(request))
            return;

        JSONNode result = JSON.Parse(response.DataAsText);

        Debug.Log(" ▶ OnFinishedTrespassReward result :: " + response.DataAsText);


        if (CheckCommonServerError(result)) {
            return;
        }

        

        if (result["result"].AsInt != 0) {
            SetSystemMessage(result["error"].Value);
            return;
        }

        OnCompleteTrespassReward();
        OnCompleteTrespassReward = delegate { };
    }

    #endregion

    #region 게임 내 잠금 해제 

    public void Post2Unlock(string pColumn) {
         WWWHelper.Instance.Post2WithString("request_updateunlock", OnFinishedUpdateUnlock, pColumn);
    }


    private void OnFinishedUpdateUnlock(HTTPRequest request, HTTPResponse response) {

        if (!CheckRequestState(request))
            return;

        JSONNode result = JSON.Parse(response.DataAsText);

        Debug.Log(" ▶ OnFinishedUpdateUnlock result :: " + response.DataAsText);


        if (CheckCommonServerError(result)) {
            return;
        }



        if (result["result"].AsInt != 0) {
            SetSystemMessage(result["error"].Value);
            return;
        }

        // 성공적으로 Update 한 경우 
        Debug.Log(" ▶ OnFinishedUpdateUnlock Tag :: " + request.Tag.ToString());

        if(request.Tag.ToString() == "mission_unlock") {
            GameSystem.Instance.SaveESvalueBool(PuzzleConstBox.ES_UnlockMission, true);
        }
        else if(request.Tag.ToString() == "item_unlock") {
            GameSystem.Instance.SaveESvalueBool(PuzzleConstBox.ES_UnlockItem, true);
            ReadyGroupCtrl.Instance.CheckReadyUnlock();
        }
        else if (request.Tag.ToString() == "passive_unlock") {
            GameSystem.Instance.SaveESvalueBool(PuzzleConstBox.ES_UnlockPassive, true);
            ReadyGroupCtrl.Instance.CheckReadyUnlock();
        }
        else if (request.Tag.ToString() == "wanted_unlock") {
            GameSystem.Instance.SaveESvalueBool(PuzzleConstBox.ES_UnlockWanted, true);
        }
        else if (request.Tag.ToString() == "ranking_unlock") {
            GameSystem.Instance.SaveESvalueBool(PuzzleConstBox.ES_UnlockRanking, true);
        }
        else if (request.Tag.ToString() == "nekoservice_tip") {
            GameSystem.Instance.SaveESvalueBool(PuzzleConstBox.ES_UnlockNekoService, true);
            LobbyCtrl.Instance.CheckProceedUnlock();
        }
        else if (request.Tag.ToString() == "nekolevelup_tip") {
            GameSystem.Instance.SaveESvalueBool(PuzzleConstBox.ES_UnlockNekoLevelup, true);
        }
        else if (request.Tag.ToString() == "bingo_tip") {
            SaveESvalueBool(PuzzleConstBox.ES_UnlockBingoTip, true);
            LobbyCtrl.Instance.SendMessage("CheckBingoFocusMark");
        }
        else if (request.Tag.ToString() == "bingo_mission_tip") {
            UserJSON["bingo_mission_tip"].AsInt = 0;
        }
        else {
            UserJSON[request.Tag.ToString()].AsInt = 0;
        }

    }

    #endregion

    #region 서버와 시간 동기화 request_synctime 

    public void Post2SyncTime(Action pAction) {
        OnSyncTimeAction += pAction;
        WWWHelper.Instance.Post2("request_synctime", OnFinishedSingleSyncTime);
    }


    private void OnFinishedSingleSyncTime(HTTPRequest request, HTTPResponse response) {

        if (!CheckRequestState(request))
            return;

        GameSystem.Instance.SyncTimeData = JSON.Parse(response.DataAsText);


        if (CheckCommonServerError(SyncTimeData)) {
            return;
        }


        OnSyncTimeAction();
        OnSyncTimeAction = delegate { };
    }


    #endregion

    #region Neko Ticket List

    /// <summary>
    /// 
    /// </summary>
    /// <param name="pMailDBKey"></param>
    /// <param name="pCallback"></param>
    public void Post2NekoTicketList(int pMailDBKey, Action<JSONNode> pCallback) {
        OnCompleteReadNekoTicketList = delegate { };
        OnOffWaitingRequestInLobby(true);
        
        OnCompleteReadNekoTicketList += pCallback;
        WWWHelper.Instance.Post2("request_nekoticketlist", OnFinishedNekoTicketList);

    }


    private void OnFinishedNekoTicketList(HTTPRequest request, HTTPResponse response) {
        if (!CheckRequestState(request))
            return;

        OnOffWaitingRequestInLobby(false);
        Debug.Log(" >>> OnFinishedNekoTicketList :: " + response.DataAsText);

        JSONNode result = JSON.Parse(response.DataAsText);

        // re-login 체크 
        if (CheckCommonServerError(result)) {
            return;
        }

        // 조회한 티켓으로 교환 가능한 고양이 리스트를 전달.
        OnCompleteReadNekoTicketList(result[_jData]["list"]);

    }

    #endregion

    #region Mission Reward

    public void Post2MissionReward(MissionType pType, int pID, MissionColumnCtrl pMission) {
        OnOffWaitingRequestInLobby(true);

        WWWHelper.Instance.SendMission = pMission;
        WWWHelper.Instance.SendMissionID = pID;

        if (pType == MissionType.Day) {
            WWWHelper.Instance.SendMissionType = "daily";
        } else if (pType == MissionType.Week) {
            WWWHelper.Instance.SendMissionType = "weekly";
        }


        WWWHelper.Instance.Post2("request_missionreward", OnFinishedMissionReward);
    }




    private void OnFinishedMissionReward(HTTPRequest request, HTTPResponse response) {
        if (!CheckRequestState(request))
            return;

        OnOffWaitingRequestInLobby(false);
        Debug.Log(" >>> OnFinishedMissionReward :: " + response.DataAsText);

        JSONNode result = JSON.Parse(response.DataAsText);

        // re-login 체크 
        if(CheckCommonServerError(result)) {
            return;
        }
        

        if (result["result"].AsInt != 0) {
            Debug.Log(result["error"].Value);
            if (result["error"].Value.Contains("Already complete mission")) {
                //WWWHelper.Instance.SendMission.SetCompleteMission();
            }
            else if (result["error"].Value.Contains("Can not find mission reward data")) {
            }

            return;
        }

        // 받았다고 처리 
        switch(result["data"]["rewardtype"].Value) {
            case "gem":
                //LobbyCtrl.Instance.OpenInfoPopUp(PopMessageType.GemAdd, result["data"]["rewardvalue"]);
                UserGem += result["data"]["rewardvalue"].AsInt;
                break;
            case "coin":
                //LobbyCtrl.Instance.OpenInfoPopUp(PopMessageType.GoldAdd, result["data"]["rewardvalue"]);
                UserGold += result["data"]["rewardvalue"].AsInt;
                break;
            case "chub":
                //LobbyCtrl.Instance.OpenInfoPopUp(PopMessageType.BandChubAdd, result["data"]["rewardvalue"]);
                UserChub += result["data"]["rewardvalue"].AsInt;
                break;
            case "tuna":
                //LobbyCtrl.Instance.OpenInfoPopUp(PopMessageType.BandTunaAdd, result["data"]["rewardvalue"]);
                UserTuna += result["data"]["rewardvalue"].AsInt;
                break;
            case "salmon":
                //LobbyCtrl.Instance.OpenInfoPopUp(PopMessageType.BandSalmonAdd, result["data"]["rewardvalue"]);
                UserSalmon += result["data"]["rewardvalue"].AsInt;
                break;
            case "freeticket":
                //LobbyCtrl.Instance.OpenInfoPopUp(PopMessageType.AddFreeTicket, result["data"]["rewardvalue"]);
                break;
            case "rareticket":
                //LobbyCtrl.Instance.OpenInfoPopUp(PopMessageType.AddRareTicket, result["data"]["rewardvalue"]);
                break;
			/*
			case "rainbowticket":
				LobbyCtrl.Instance.OpenInfoPopUp(PopMessageType.AddRareTicket, result["data"]["rewardvalue"]);
				break;
			*/

        }

        WWWHelper.Instance.SendMission.SetCompleteMission();

    }

    #endregion

    #region 인앱결제 소비 처리 request_purchase

    public void Post2Consume() {
		OnOffWaitingRequestInLobby (true);
		WWWHelper.Instance.Post2 ("request_purchase", OnFinishedConsume);
	}
	
	private void OnFinishedConsume(HTTPRequest request, HTTPResponse response) {
        if (!CheckRequestState(request))
            return;

        OnOffWaitingRequestInLobby (false);
		Debug.Log (" >>> OnFinishedConsume :: " + response.DataAsText);
		
		JSONNode result = JSON.Parse (response.DataAsText);

        // re-login 체크 
        if (CheckCommonServerError(result)) {
            return;
        }

        if (result ["result"].AsInt != 0) {
			SetSystemMessage("fail request purchase");
			return;
		}

		#if UNITY_ANDROID
		// 완료처리 
		OnCompleteServerConsume (_currentPurchase, result);

		#elif UNITY_IOS

		OnCompleteIOSPurchase(_currentSKU, result);

		#endif
	}

	#endregion

	#region payload 검증 request_verifypayload

	public void Post2VerifyPayload() {
		OnOffWaitingRequestInLobby (true);
		WWWHelper.Instance.Post2 ("request_verifypayload", OnFinishedVerifyPayload);
	}
	
	private void OnFinishedVerifyPayload(HTTPRequest request, HTTPResponse response) {
        if (!CheckRequestState(request))
            return;


        OnOffWaitingRequestInLobby (false);
		Debug.Log (" >>> OnFinishedVerifyPayload :: " + response.DataAsText);
		
		JSONNode result = JSON.Parse (response.DataAsText);

		if (result ["result"].AsInt != 0) {
			SetSystemMessage("결제 검증 과정에서 오류가 발생했습니다. 고객센터로 문의해주세요.");
			return;
		}

		#if UNITY_ANDROID
		// 검증 완료 되면 소비 Consume
		InAppConsume (_currentPurchase);
		#endif
	}

	#endregion

	#region 인앱 payload 발급 요청 request_payload

	public void Post2ReqPayload() {
		OnOffWaitingRequestInLobby (true);
		WWWHelper.Instance.Post2 ("request_payload", OnFinishedReqPayload);
	}
	
	private void OnFinishedReqPayload(HTTPRequest request, HTTPResponse response) {
        if (!CheckRequestState(request))
            return;

        OnOffWaitingRequestInLobby (false);
		Debug.Log (" >>> OnFinishedReqPayload :: " + response.DataAsText);
		
		JSONNode result = JSON.Parse (response.DataAsText);

        // re-login 체크 
        if (CheckCommonServerError(result)) {
            return;
        }

        if (result ["result"].AsInt != 0) {
			// 이미 패키지를 구매한 경우 
			LobbyCtrl.Instance.OpenInfoPopUp(PopMessageType.AlreadStartPackage);
			return;
		}

		_payload = result[_jData].Value;

		#if UNITY_ANDROID
		InAppPurchase ();

		#elif UNITY_IOS

		InAppIOSPurchase();


		#endif
	}

	#endregion

	#region 광고보고 보상받기 request_viewads

	public void Post2ViewAds() {
		OnOffWaitingRequestInLobby (true);
		WWWHelper.Instance.Post2 ("request_viewads", OnFinishedViewAds);
	}
	
	private void OnFinishedViewAds(HTTPRequest request, HTTPResponse response) {
        if (!CheckRequestState(request))
            return;

        OnOffWaitingRequestInLobby (false);
		Debug.Log (" >>> OnFinishedViewAds :: " + response.DataAsText);
		
		_viewAdsJSON = JSON.Parse (response.DataAsText);

        // re-login 체크 
        if (CheckCommonServerError(_viewAdsJSON)) {
            return;
        }

        Invoke("OnRequestViewAds", 1f);
		
	}

	#endregion

	#region 코인 구매 request_exchangegold

	public void Post2ExchangeGold(int pIndex) {
		OnOffWaitingRequestInLobby (true);
		WWWHelper.Instance.Post2 ("request_exchangegold", OnFinishedExchangeGold, pIndex);
	}
	
	private void OnFinishedExchangeGold(HTTPRequest request, HTTPResponse response) {
        if (!CheckRequestState(request))
            return;

        OnOffWaitingRequestInLobby (false);
		Debug.Log (" >>> OnFinishedExchangeGold :: " + response.DataAsText);
		
		JSONNode result = JSON.Parse (response.DataAsText);

        // re-login 체크 
        if (CheckCommonServerError(result)) {
            return;
        }


        if (result ["result"].AsInt != 0) {

			// 보석이 충분하지 않다!
			if (result ["error"].Value == "Not enought gem" && LobbyCtrl.Instance != null) { 
				LobbyCtrl.Instance.OpenUpperInfoPopUp(PopMessageType.GemShortage);
			}

			return;
		}

		AdbrixManager.Instance.SendAdbrixInAppActivity (AdbrixManager.Instance.BUY_GOLD);
        



        UserGold = result[_jData]["gold"].AsInt;
		UserGem = result[_jData]["gem"].AsInt;

        // 준비창에서 선택된 아이템의 코인값 차감 
        UserGold = UserGold - LobbyCtrl.Instance.GetSelectedEquipItemCoinValue();

        UpdateTopInfomation();
		
		// 팝업창 호출
		//LobbyCtrl.Instance.OpenCommonPopup(PopMessageType.GoldPurchase);
        LobbyCtrl.Instance.OpenInfoPopUp(PopMessageType.GoldPurchase);

        
        GameSystem.Instance.CheckLobbyBingoQuest(51);// 빙고 젬 사용 퀘스트 
        //GameSystem.Instance.CheckLobbyBingoQuest(64); // 코인 구매 퀘스트 


    }

    #endregion

    #region 페이스북 하트 보내기 request_sendheart , 친구초대 request_facebookinvite

    public void Post2InviteFriend(int pFriendCount) {
        OnOffWaitingRequestInLobby(true);

        WWWHelper.Instance.Post2("request_facebookinvite", OnFinishedInvite, pFriendCount);
    }

    private void OnFinishedInvite(HTTPRequest request, HTTPResponse response) {
        if (!CheckRequestState(request))
            return;


        OnOffWaitingRequestInLobby(false);

        Debug.Log(" >>> OnFinishedInvite :: " + response.DataAsText);
        JSONNode result = JSON.Parse(response.DataAsText);

        // re-login 체크 
        if (CheckCommonServerError(result)) {
            return;
        }

        LobbyCtrl.Instance.OpenUpperInfoPopUp(PopMessageType.InviteComplete);

        Post2CheckNewMail(); // 메일 체크 

    }


    public void Post2SendHeart(string pFacebookID) {
		OnOffWaitingRequestInLobby (true);
		WWWHelper.Instance.Post2WithString ("request_sendheart", OnFinishedSendHeart, pFacebookID);
	}
	
	private void OnFinishedSendHeart(HTTPRequest request, HTTPResponse response) {
        if (!CheckRequestState(request))
            return;

        OnOffWaitingRequestInLobby (false);
		Debug.Log (" >>> OnFinishedSendHeart :: " + response.DataAsText);
		JSONNode result = JSON.Parse (response.DataAsText);

        // re-login 체크 
        if (CheckCommonServerError(result)) {
            return;
        }

        if (result["result"].AsInt != 0) {

            if(result["error"].Value.Contains("Today already send heart")) {
                LobbyCtrl.Instance.OpenUpperInfoPopUp(PopMessageType.AlreadySentHeart);
            } else {
                
                LobbyCtrl.Instance.OpenUpperInfoPopUp(PopMessageType.FacebookLinkLost);
                _heartFriendFacebookID = null;
                return; // 이것만 리턴처리 .
                // 링크를 잃어버린 친구라도 하트를 보낸것처럼 처리한다.
            }

		}
	
		// 하트 보낸 정보를 담는다.
		_listSendHeartHistory.Add(_heartFriendFacebookID);

		// 히스토리 정보 로컬에 저장 
		SaveHeartSentHistory();
		_heartFriendFacebookID = null;
		Post2CheckNewMail (); // 메일 체크 

		CurrentHeartRequest.Receiving ();

	}

    #endregion

    #region 사용자 파워 패시브 업그레이드 request_powerupgrade

    /// <summary>
    /// 코인으로 파워 업그레이드 
    /// </summary>
    /// <param name="pPrice"></param>
    public void Post2PowerUpgrade(int pNextLevel, int pPrice, string pPriceType) {

        JSONNode node = JSON.Parse("{}");
        node["nextlevel"].AsInt = pNextLevel;
        node["price"].AsInt = pPrice;
        node["pricetype"] = pPriceType;


        OnOffWaitingRequestInLobby (true);
        WWWHelper.Instance.Post2WithJSON("request_powerupgrade", OnFinishedPassiveUpgrade, node);
	}




    private void OnFinishedPassiveUpgrade(HTTPRequest request, HTTPResponse response) {
        if (!CheckRequestState(request))
            return;

        OnOffWaitingRequestInLobby (false);
		Debug.Log (" >>> OnFinishedPassiveUpgrade :: " + response.DataAsText);
		
		JSONNode result = JSON.Parse (response.DataAsText);

        



        if (result ["result"].AsInt != 0) {
            //Not enough gem

            if ("Not enough coin".Equals(result["error"].Value)) {
                LobbyCtrl.Instance.OpenUpperInfoPopUp(PopMessageType.GoldShortage);
            }
            else if ("Not enough coin".Equals(result["error"].Value)) {
                LobbyCtrl.Instance.OpenUpperInfoPopUp(PopMessageType.GemShortage);
            }
            return;
		}


        // 코인 소모 빙고 적용
        if(result[_jData]["resultgold"].AsInt < UserGold) {
            CheckLobbyBingoQuest(50, UserGold - result[_jData]["resultgold"].AsInt, false); // 소모된 값을 전송
        }


		// result 처리 
		UserGold = result[_jData]["resultgold"].AsInt;
        UserGem = result[_jData]["resultgem"].AsInt;

        UserPowerLevel = result[_jData]["powerlevel"].AsInt;

        // 준비창에서 선택된 아이템의 코인값 차감 
        UserGold = UserGold - LobbyCtrl.Instance.GetSelectedEquipItemCoinValue();
    
		
		// 상단 정보 갱신
		UpdateTopInfomation();


        // 준비창 업데이트 
        //PopUpgradeCtrl passiveUpgrade = FindObjectOfType(typeof(PopUpgradeCtrl)) as PopUpgradeCtrl;

        ReadyGroupCtrl.Instance.RefreshPower();
    }


    #endregion

    #region 메일 읽기 request_mailread


    public void Post2AllMailRead(int pMailDBKey, MailColumnCtrl pMail, Action<Transform> pOnMailRead) {
        OnOffWaitingRequestInLobby(true);

        OnMailAllReadAction += pOnMailRead;
        ReadMailColumn = pMail;

        WWWHelper.Instance.Post2("request_mailread", OnFinishedAllMailRead, pMailDBKey);
    }

    private void OnFinishedAllMailRead(HTTPRequest request, HTTPResponse response) {
        if (!CheckRequestState(request))
            return;

        OnOffWaitingRequestInLobby(false);
        Debug.Log(" >>> OnFinishedMailRead :: " + response.DataAsText);

        JSONNode result = JSON.Parse(response.DataAsText);

        // re-login 체크 
        if (CheckCommonServerError(result)) {
            return;
        }

        // 0이 아닌 경우에 대한 체크 
        if (result["result"].AsInt == 0) {
            OnCompleteMailRead(result[_jData], true);
        }
        else {
            ReadMailColumn.MailCheckFlag = true; // 체크 했음을 처리 
            LobbyCtrl.Instance.HasCantReadMail = true;
        }

        LobbyCtrl.Instance.OffWaitingRequest();


        // 콜백 처리 
        OnMailAllReadAction(ReadMailColumn.transform);
        OnMailAllReadAction = delegate { };

    }




    /// <summary>
    /// 단일 메일 읽기.
    /// </summary>
    public void Post2MailRead(int pMailDBKey, MailColumnCtrl pMail, Action pOnMailRead) {
		OnOffWaitingRequestInLobby (true);

        OnMailReadAction += pOnMailRead;

        ReadMailColumn = pMail;
		WWWHelper.Instance.Post2 ("request_mailread", OnFinishedMailRead, pMailDBKey);
	}
	
	private void OnFinishedMailRead(HTTPRequest request, HTTPResponse response) {
        if (!CheckRequestState(request)) {
            OnOffWaitingRequestInLobby(false);
            return;
        }
            

        
		Debug.Log (" >>> OnFinishedMailRead :: " + response.DataAsText);
		
		JSONNode result = JSON.Parse (response.DataAsText);

        // re-login 체크 
        if (CheckCommonServerError(result)) {
            OnOffWaitingRequestInLobby(false);
            return;
        }

        if (result ["result"].AsInt != 0) {

            OnOffWaitingRequestInLobby(false);

            if (LobbyCtrl.Instance != null) {
				LobbyCtrl.Instance.OpenInfoPopUp(PopMessageType.HeartFull);
				LobbyCtrl.Instance.OffWaitingRequest ();
			}

			return;
		}

        OnCompleteMailRead(result[_jData]);


        // 메일 리스트 쪽에서의 처리 Callback 
        OnMailReadAction();
        OnMailReadAction = delegate { };

    }


    /// <summary>
    /// 메일 읽기 후 팝업 
    /// </summary>
    /// <param name="pNode"></param>
    private void OnCompleteMailRead(JSONNode pNode, bool pAllRead = false) {

        PopMessageType sendType;

        int resultValue = 0;

        if(pNode["resulttype"].Value.Contains("heart")) {

            sendType = PopMessageType.HeartAdd;

            HeartCount = pNode["resultvalue"].AsInt;


            if (pAllRead)
                LobbyCtrl.Instance.OpenInfoPopUpDuplicate(sendType, pNode["addvalue"].ToString());
            else 
                LobbyCtrl.Instance.OpenInfoPopUp(sendType, pNode["addvalue"].ToString());

        }
        else if(pNode["resulttype"].Value.Contains("gem")) {

            sendType = PopMessageType.GemAdd;


            resultValue = pNode["resultvalue"].AsInt - UserGem;
            UserGem = pNode["resultvalue"].AsInt;

            if (pAllRead)
                LobbyCtrl.Instance.OpenInfoPopUpDuplicate(sendType, resultValue.ToString()); // 팝업 띄우기 
            else
                LobbyCtrl.Instance.OpenInfoPopUp(sendType, resultValue.ToString()); // 팝업 띄우기 

            

        }
        else if (pNode["resulttype"].Value.Contains("coin") || pNode["resulttype"].Value.Contains("gold")) {


            sendType = PopMessageType.GoldAdd;

            // MailRead에서 랭킹보상은 다른 메세지로 변경
            if (ReadMailColumn.GetComponent<MailColumnCtrl>().Mailtype == 3) {
                sendType = PopMessageType.RankRewardCoinAdd;
            }

            resultValue = pNode["resultvalue"].AsInt - UserGold;
            UserGold = pNode["resultvalue"].AsInt;


            if (pAllRead)
                LobbyCtrl.Instance.OpenInfoPopUpDuplicate(sendType, resultValue.ToString());
            else
                LobbyCtrl.Instance.OpenInfoPopUp(sendType, resultValue.ToString());


            
        }
        else if (pNode["resulttype"].Value.Contains("chub")) {

            sendType = PopMessageType.ChubAdd;

            // MailRead에서 랭킹보상은 다른 메세지로 변경
            if (ReadMailColumn.GetComponent<MailColumnCtrl>().Mailtype == 3) {
                sendType = PopMessageType.RankRewardChubAdd;
            }

            if (pAllRead)
                LobbyCtrl.Instance.OpenInfoPopUpDuplicate(sendType, (pNode["resultvalue"].AsInt - UserChub).ToString());
            else
                LobbyCtrl.Instance.OpenInfoPopUp(sendType, (pNode["resultvalue"].AsInt - UserChub).ToString());

            UserChub = pNode["resultvalue"].AsInt;

        }
        else if (pNode["resulttype"].Value.Contains("tuna")) {

            sendType = PopMessageType.TunaAdd;

            // MailRead에서 랭킹보상은 다른 메세지로 변경
            if (ReadMailColumn.GetComponent<MailColumnCtrl>().Mailtype == 3) {
                sendType = PopMessageType.RankRewardTunaAdd;
            }


            if (pAllRead)
                LobbyCtrl.Instance.OpenInfoPopUpDuplicate(sendType, (pNode["resultvalue"].AsInt - UserTuna).ToString());
            else
                LobbyCtrl.Instance.OpenInfoPopUp(sendType, (pNode["resultvalue"].AsInt - UserTuna).ToString());


            UserTuna = pNode["resultvalue"].AsInt;

        }
        else if (pNode["resulttype"].Value.Contains("salmon")) {

            sendType = PopMessageType.SalmonAdd;

            // MailRead에서 랭킹보상은 다른 메세지로 변경
            if (ReadMailColumn.GetComponent<MailColumnCtrl>().Mailtype == 3) {
                sendType = PopMessageType.RankRewardSalmonAdd;
            }

            if (pAllRead)
                LobbyCtrl.Instance.OpenInfoPopUpDuplicate(sendType, (pNode["resultvalue"].AsInt - UserSalmon).ToString());
            else
                LobbyCtrl.Instance.OpenInfoPopUp(sendType, (pNode["resultvalue"].AsInt - UserSalmon).ToString());

            UserSalmon = pNode["resultvalue"].AsInt;

        }
        else if (pNode["resulttype"].Value.Contains("neko")) {

            sendType = PopMessageType.NekoAdd;

            // MailRead에서 랭킹보상은 다른 메세지로 변경
            if (ReadMailColumn.GetComponent<MailColumnCtrl>().Mailtype == 3) {
                sendType = PopMessageType.RankRewardNekoAdd;
                
            }

            // 네코 획득팝업 오픈 
            JSONNode nekoNode = pNode["resultvalue"];
            

            if (pAllRead)
                LobbyCtrl.Instance.OpenInfoPopUpDuplicate(sendType, nekoNode.ToString());
            else
                LobbyCtrl.Instance.OpenInfoPopUp(sendType, nekoNode.ToString());

        }
        else if(pNode["resulttype"].Value.Contains("ticket")) { // 티켓 처리 
            
            // 생선처리 
            UserChub = pNode["resultvalue"]["resultchub"].AsInt;
            UserTuna = pNode["resultvalue"]["resulttuna"].AsInt;
            UserSalmon = pNode["resultvalue"]["resultsalmon"].AsInt;

            // rainbow ticket 과 free/rare ticket의 분기 


            if(pNode["resulttype"].Value.Contains("rainbowticket") && pNode["resultvalue"]["nekolist"][0]["isFusion"].AsInt == 1) {
                // 다른 팝업창을 띄우도록 요청 
                sendType = PopMessageType.NekoAdd;

                JSONNode nekoNode = pNode["resultvalue"]["nekolist"][0];
                LobbyCtrl.Instance.OpenUpperInfoPopUp(sendType, nekoNode.ToString());

                return;
            }

            
            // 뽑기 결과창 오픈
            
            LobbyCtrl.Instance.SendMessage("DisableUIForGatcha");
            
            LobbyCtrl.Instance.ObjGatchaScreen.gameObject.SetActive(true);

            
            LobbyCtrl.Instance.ObjGatchaScreen.GetComponent<NekoGatchaCtrl>().SetTicketNeko(pNode["resultvalue"]["nekolist"][0]);
        }


    }


    #endregion

    #region 깨우기 Wakeup

    public void Post2WakeUp(string pWakeUpTime) {
        OnOffWaitingRequestInLobby(true);

        WWWHelper.Instance.Post2WithString("request_wakeup", OnFinishedWakeUp, pWakeUpTime);

    }

    private void OnFinishedWakeUp(HTTPRequest request, HTTPResponse response) {
        if (!CheckRequestState(request))
            return;


        OnOffWaitingRequestInLobby(false);
        Debug.Log(" >>> OnFinishedWakeUp :: " + response.DataAsText);

        JSONNode result = JSON.Parse(response.DataAsText);

        // re-login 체크 
        if (CheckCommonServerError(result)) {
            return;
        }

        result = result[_jData];


        _remainWakeUpTimeTick = ConvertServerTimeTick(result["wakeuptime"].AsLong); // 시간 갱신 
        _dtRemainWakeUpTime = new DateTime(_remainWakeUpTimeTick).AddMinutes(30); // 30분 추가 

        UserJSON["wakeuptime"].AsLong = result["wakeuptime"].AsLong;

        // 재화 처리 
        if ("gem".Equals(result["gifttype"].Value)) {
            UserGem = result["resultgem"].AsInt;
            LobbyCtrl.Instance.OpenInfoPopUp(PopMessageType.WakeUpGemAdd, result["giftvalue"].Value);
        }
        else if ("coin".Equals(result["gifttype"].Value)) {
            UserGold = result["resultcoin"].AsInt;
            LobbyCtrl.Instance.OpenInfoPopUp(PopMessageType.WakeUpCoinAdd, result["giftvalue"].Value);
        }

        BottleManager.Instance.WakeUpCallback();
        IsRequestingWakeUp = false;
        
        UpdateTopInfomation();
    }

    #endregion

    #region 구출 내역  request_rescuerewardupdate

    public void Post2Wanted(int pThemeID) {
		OnOffWaitingRequestInLobby (true);
		WWWHelper.Instance.Post2 ("request_rescuerewardupdate", OnFinishedRescueRewardUpdate, pThemeID);
	}
	
	private void OnFinishedRescueRewardUpdate(HTTPRequest request, HTTPResponse response) {
        if (!CheckRequestState(request))
            return;


        OnOffWaitingRequestInLobby (false);
		Debug.Log (" >>> request_rescuerewardupdate :: " + response.DataAsText);
		
		JSONNode result = JSON.Parse (response.DataAsText);

        // re-login 체크 
        if (CheckCommonServerError(result)) {
            return;
        }


		UpdateTopInfomation();

		

	}

	#endregion

	#region 고양이의 보은 request_nekoreward

	public void Post2NekoReward(int pNekoDBKey) {
		OnOffWaitingRequestInLobby (true);
		WWWHelper.Instance.Post2 ("request_nekoreward", OnFinishedNekoReward, pNekoDBKey);
	}
	
	private void OnFinishedNekoReward(HTTPRequest request, HTTPResponse response) {
        if (!CheckRequestState(request))
            return;

        OnOffWaitingRequestInLobby (false);
		Debug.Log (" >>> OnFinishedNekoReward :: " + response.DataAsText);
		
		_nekoRewardJSON = JSON.Parse (response.DataAsText);


        // re-login 체크 
        if (CheckCommonServerError(_nekoRewardJSON)) {
            return;
        }

        if (_nekoRewardJSON ["result"].AsInt != 0) {
			return;
		} 

		
		/// 고양이의 보은 시간 갱신
		_userNekorewardtime = ConvertServerTimeTick(_nekoRewardJSON[_jData]["nekorewardtime"].AsLong);
		_dtNextNekoRewardTime = new System.DateTime(_userNekorewardtime); // DateTime으로 변환 
		_isNekoRewardReady = false; // 리워드 플래그 변경 
		//Debug.Log("!! _dtNextNekoRewardTime " + _dtNextNekoRewardTime.Year + "/" + _dtNextNekoRewardTime.Month + "/" + _dtNextNekoRewardTime.Day + "/" + _dtNextNekoRewardTime.Hour + "/" + _dtNextNekoRewardTime.Minute);
		
		if(_nekoRewardJSON[_jData]["rewardtype"].Value != "" && _nekoRewardJSON[_jData]["rewardtype"].Value != null) {
			
			
			if("gem".Equals(_nekoRewardJSON[_jData]["rewardtype"].Value)) {
				
				UserGem += _nekoRewardJSON[_jData]["value"].AsInt;
				LobbyCtrl.Instance.OpenInfoPopUp(PopMessageType.NekoGiftGemAdd, _nekoRewardJSON[_jData]["value"].Value);
				
				
			} else if("coin".Equals(_nekoRewardJSON[_jData]["rewardtype"].Value) ||"gold".Equals(_nekoRewardJSON[_jData]["rewardtype"].Value) ) {
				
				UserGold += _nekoRewardJSON[_jData]["value"].AsInt;
				LobbyCtrl.Instance.OpenInfoPopUp(PopMessageType.NekoGiftGoldAdd, _nekoRewardJSON[_jData]["value"].Value);
				
			}
			
			UpdateTopInfomation();
		}
		
	}

	#endregion

	#region 하트 보석으로 구매 request_heartcharge

	public void Post2HeartCharge() {
		OnOffWaitingRequestInLobby (true);
		WWWHelper.Instance.Post2 ("request_heartcharge", OnFinishedHeartCharge);
	}
	
	private void OnFinishedHeartCharge(HTTPRequest request, HTTPResponse response) {
        if (!CheckRequestState(request))
            return;

        OnOffWaitingRequestInLobby (false);
		Debug.Log (" >>> OnFinishedHeartCharge :: " + response.DataAsText);
		
		JSONNode result = JSON.Parse (response.DataAsText);

        // re-login 체크 
        if (CheckCommonServerError(result)) {
            return;
        }

        if (result ["result"].AsInt != 0) {

			if (result ["error"].Value.IndexOf ("enough") >= 0) {
				// 결과창, 로비에서 둘다 사용 
				if(LobbyCtrl.Instance != null) 
					LobbyCtrl.Instance.OpenUpperInfoPopUp(PopMessageType.GemShortage);
				
			}

			return;

		} else {

			AdbrixManager.Instance.SendAdbrixInAppActivity (AdbrixManager.Instance.BUY_HEART);
            //AdbrixManager.Instance.SendAppsFlyerEvent(AdbrixManager.Instance.AF_DIA_HEART_CHARGE);

			UserGold = result[_jData]["gold"].AsInt;
			UserGem = result[_jData]["gem"].AsInt;
			HeartCount = result[_jData]["heart"]["value"].AsInt;
			
			if(LobbyCtrl.Instance != null) {
				UpdateTopInfomation();
				LobbyCtrl.Instance.OpenInfoPopUp(PopMessageType.HeartPurchase);
				
			}


            //HeartShopCtrl heartShop = FindObjectOfType(typeof(HeartShopCtrl)) as HeartShopCtrl;
            //heartShop.UpdateHearts();


            // 빙고 젬 사용 퀘스트 
            GameSystem.Instance.CheckLobbyBingoQuest(51);
        }
		
	}

	#endregion

	#region 페이스북 연동 request_facebooklink

	public void Post2FBLink() {
		OnOffWaitingRequestInLobby (true);
        _fbLinkJSON = null;

        WWWHelper.Instance.Post2 ("request_facebooklink", OnFinishedFBLink);
	}
	
	private void OnFinishedFBLink(HTTPRequest request, HTTPResponse response) {
        if (!CheckRequestState(request))
            return;

        OnOffWaitingRequestInLobby (false);
		Debug.Log (" >>> OnFinishedFBLink :: " + response.DataAsText);
		

        _fbLinkJSON = JSON.Parse(response.DataAsText);


        // re-login 체크 
        if (CheckCommonServerError(_fbLinkJSON)) {
            return;
        }

        _facebookid = _fbLinkJSON[_jData]["facebookid"];
        _fbLinkJSON = _fbLinkJSON[_jData];
    }


	#endregion

	#region 쿠폰 사용 request_usecoupon

	public void Post2UseCoupon() {
		OnOffWaitingRequestInLobby (true);
		WWWHelper.Instance.Post2 ("request_usecoupon", OnFinishedUseCoupon);
	}
	
	private void OnFinishedUseCoupon(HTTPRequest request, HTTPResponse response) {
        if (!CheckRequestState(request))
            return;

        OnOffWaitingRequestInLobby (false);
		Debug.Log (" >>> OnFinishedUseCoupon :: " + response.DataAsText);
		
		JSONNode result = JSON.Parse (response.DataAsText);

        // re-login 체크 
        if (CheckCommonServerError(result)) {
            return;
        }

        if (result ["result"].AsInt != 0) {
			// 쿠폰 사용 기간이 지났습니다.
			if (result ["error"].Value.IndexOf ("coupon days") >= 0) {
				LobbyCtrl.Instance.OpenInfoPopUp(PopMessageType.CouponOutOf);
			} else if (result ["error"].Value.IndexOf ("already use") >= 0) { // 이미 사용됨 
				LobbyCtrl.Instance.OpenInfoPopUp(PopMessageType.CouponUsed);
			} else if (result ["error"].Value.IndexOf ("Invalid coupon") >= 0) { // 잘못된 쿠폰번호 
				LobbyCtrl.Instance.OpenInfoPopUp(PopMessageType.CouponFail);
			}

			return;

		} else {
			Post2CheckNewMail(); // 새로운 메일 체크 
			LobbyCtrl.Instance.OpenInfoPopUp(PopMessageType.CouponSucceed); // 쿠폰 사용 완료 
		}
	}

	#endregion

	#region 광고보고 하트받기 request_heartads

	/// <summary>
	/// request_itemads
	/// </summary>
	public void Post2HeartAds() {
		OnOffWaitingRequestInLobby (true);
		WWWHelper.Instance.Post2 ("request_heartads", OnFinishedHeartAds);
	}
	
	private void OnFinishedHeartAds(HTTPRequest request, HTTPResponse response) {
        if (!CheckRequestState(request))
            return;

        OnOffWaitingRequestInLobby (false);

		Debug.Log (" >>> OnFinishedHeartAds :: " + response.DataAsText);
		
		JSONNode result = JSON.Parse (response.DataAsText);

        // re-login 체크 
        if (CheckCommonServerError(result)) {
            return;
        }

        OnCompleteRequestTakeHeart(result);


        HeartShopCtrl heartShop = FindObjectOfType<HeartShopCtrl>();
        heartShop.UpdateHearts();
		
		
		
	}
    #endregion

    #region 구출 내역 조회 request_rescuehistory, 구출 보상 지급 request_rescuerewardupdate


    public void Post2RescueReward(int pThemeID, Action pCallback) {

        OnCompleteRewardRescue = delegate { };
        OnCompleteRewardRescue = pCallback;

        OnOffWaitingRequestInLobby(true);
        WWWHelper.Instance.Post2("request_rescuerewardupdate", OnFinishedRescueReward, pThemeID);
    }

    private void OnFinishedRescueReward(HTTPRequest request, HTTPResponse response) {
        if (!CheckRequestState(request))
            return;

        OnOffWaitingRequestInLobby(false);
        Debug.Log(" >>> OnFinishedRescueReward :: " + response.DataAsText);

        JSONNode resultNode = JSON.Parse(response.DataAsText);

        // re-login 체크 
        if (CheckCommonServerError(resultNode)) {
            return;
        }

        OnCompleteRewardRescue(); // callback

        resultNode = resultNode[_jData];

        // RescueHistory 업데이트
        for(int i=0; i<RescueHistory.Count;i++) {
            if (RescueHistory[i]["themeid"].AsInt == resultNode["updatethemeid"].AsInt) {
                RescueHistory[i]["takepay"].AsInt = 1;
                
                return;
            }
        }

    }


    /// <summary>
    /// Post2WinNekoList
    /// </summary>
    public void Post2RescueHistory() {
		OnOffWaitingRequestInLobby (true);
		WWWHelper.Instance.Post2 ("request_rescuehistory", OnFinishedRescueHistory);
	}
	
	private void OnFinishedRescueHistory(HTTPRequest request, HTTPResponse response) {
        if (!CheckRequestState(request))
            return;

        OnOffWaitingRequestInLobby (false);
		Debug.Log (" >>> OnFinishedRescueHistory :: " + response.DataAsText);
		
		RescueHistory = JSON.Parse (response.DataAsText);

        // re-login 체크 
        if (CheckCommonServerError(RescueHistory)) {
            return;
        }

        RescueHistory = RescueHistory[_jData]["list"];

        WindowManagerCtrl.Instance.OpenCollectionMasterCallback();

	}

    #endregion

    #region 출석 체크 request_attendance (튜토리얼 완료 후 호출한다.)
    public void Post2Attendance() {
		OnOffWaitingRequestInLobby (true);
		WWWHelper.Instance.Post2("request_attendance", OnFinishedAttend);
	}

	private void OnFinishedAttend(HTTPRequest request, HTTPResponse response) {

        if (!CheckRequestState(request))
            return;

        OnOffWaitingRequestInLobby (false);

		Debug.Log (" >>> OnFinishedAttend :: " + response.DataAsText);
		GameSystem.Instance.AttendanceJSON = JSON.Parse(response.DataAsText);


        // re-login 체크 
        if (CheckCommonServerError(GameSystem.Instance.AttendanceJSON)) {
            return;
        }

        // 로비 새로 불러온다. 
        GameSystem.Instance.Initialize();
        GameSystem.Instance.LoadLobbyScene();

        //빙고 체크
        if (AttendanceJSON["result"].AsInt == 0)
            CheckLoginAfterAttendance();

        
    }	

	#endregion

	#region 랭킹 보상  request_rankreward

	public void Post2ScoreRankReward() {
		OnOffWaitingRequestInLobby (true);
		WWWHelper.Instance.Post2 ("request_rankreward", OnFinishedScoreRankReward);
	}
	
	private void OnFinishedScoreRankReward(HTTPRequest request, HTTPResponse response) {
        if (!CheckRequestState(request))
            return;

        OnOffWaitingRequestInLobby (false);
		Debug.Log (" >>> OnFinishedScoreRankReward :: " + response.DataAsText);

        GameSystem.Instance.UserDataJSON["data"]["lastweekreward"].AsBool = false;

        _rankRewardJSON = JSON.Parse (response.DataAsText);

        // re-login 체크 
        if (CheckCommonServerError(_rankRewardJSON)) {
            return;
        }

        if (_rankRewardJSON["result"].AsInt != 0) {
            return;
        }

	
		LobbyCtrl.Instance.OpenRankReward();
		
	}




	#endregion

	#region 페이스북 네코 공유 request_facebookshare
	/// <summary>
	/// request_itemads
	/// </summary>
	public void Post2FacebookShare() {
		OnOffWaitingRequestInLobby (true);
		WWWHelper.Instance.Post2 ("request_facebookshare", OnFinishedFacebookShare);
	}
	
	private void OnFinishedFacebookShare(HTTPRequest request, HTTPResponse response) {
        if (!CheckRequestState(request))
            return;
        OnOffWaitingRequestInLobby (false);
		Debug.Log (" >>> OnFinishedFacebookShare :: " + response.DataAsText);
		
		// 메일 체크 
		Post2CheckNewMail ();
		
		
		
	}
	#endregion

	#region 평가하기 보상 request_rating
	/// <summary>
	/// request_itemads
	/// </summary>
	public void Post2Rating() {
		OnOffWaitingRequestInLobby (true);
		WWWHelper.Instance.Post2 ("request_rating", OnFinishedRating);
	}
	
	private void OnFinishedRating(HTTPRequest request, HTTPResponse response) {
        if (!CheckRequestState(request))
            return;


        OnOffWaitingRequestInLobby (false);

		// 메일 체크 
		Post2CheckNewMail ();
	}

	#endregion

	#region 캐릭터 뽑기 광고 보기 request_gachaads

	public void Post2GatchaAds() {
		
		OnOffWaitingRequestInLobby (true);
		WWWHelper.Instance.Post2 ("request_gachaads", OnFinishedGatchaAds);
	}
	
	private void OnFinishedGatchaAds(HTTPRequest request, HTTPResponse response) {
        if (!CheckRequestState(request))
            return;

        OnOffWaitingRequestInLobby (false);

		JSONNode result = JSON.Parse (response.DataAsText);

        // re-login 체크 
        if (CheckCommonServerError(result)) {
            return;
        }

        if (result[_jData]["gachaads"].Value != null && result[_jData]["gachaads"].Value != "") {
			_userDataJSON[_jData]["gachaads"].AsInt = result[_jData]["gachaads"].AsInt;
		}
	}

    #endregion

    #region 데이터 이전 코드 발급/입력

    public void Post2CodeInput(string pCode) {
        OnOffWaitingRequestInLobby(true);
        WWWHelper.Instance.Post2WithString("request_datatransfer", OnFinishedCodeInput, pCode);
    }

    private void OnFinishedCodeInput(HTTPRequest request, HTTPResponse response) {

        if (!CheckRequestState(request))
            return;

        OnOffWaitingRequestInLobby(false);
        Debug.Log(">>>> OnFinishedCodeInput :: " + response.DataAsText);
        JSONNode result = JSON.Parse(response.DataAsText);

        // re-login 체크 
        if (CheckCommonServerError(result)) {
            return;
        }

        if (result["result"].AsInt != 0) {

            if (result["error"].Value.Contains("Can not find transfer code")) {
                LobbyCtrl.Instance.OpenInfoPopUp(PopMessageType.WrongCode);
            }
            else if (result["error"].Value.Contains("Same dbkey. Can not data transfer")) {
                LobbyCtrl.Instance.OpenInfoPopUp(PopMessageType.SameDeviceCode);
            }
            else if (result["error"].Value.Contains("Already using transfer code")) {
                LobbyCtrl.Instance.OpenInfoPopUp(PopMessageType.UsedCode);
            }
            else if (result["error"].Value.Contains("Transfer code limit time over")) {
                LobbyCtrl.Instance.OpenInfoPopUp(PopMessageType.ExpiredCode);
            }


            return;
        }

        LobbyCtrl.Instance.OpenInfoPopUp(PopMessageType.CompleteDataTranfer);


    }


    public void Post2CodeIssue() {
        OnOffWaitingRequestInLobby(true);
        WWWHelper.Instance.Post2("request_transfercode", OnFinishedCodeIssue);

    }

    private void OnFinishedCodeIssue(HTTPRequest request, HTTPResponse response) {
        //Post2CodeIssue

        if (!CheckRequestState(request))
            return;

        OnOffWaitingRequestInLobby(false);
        Debug.Log(">>>> OnFinishedCodeIssue :: " + response.DataAsText);
        JSONNode result = JSON.Parse(response.DataAsText);

        // re-login 체크 
        if (CheckCommonServerError(result)) {
            return;
        }

        if (result["result"].AsInt != 0) {
            // 코드 발급이 불가 (이전에 이미 받았다.)
            LobbyCtrl.Instance.OpenInfoPopUp(PopMessageType.AlreadyIssued);
            return;
        }
        else {
            DataCode = result[_jData]["code"].Value;
            DataCodeExpiredTime = result[_jData]["codelimittime"].AsLong;
        }

        // 팝업창 띄우기 
        LobbyCtrl.Instance.OpenCodeInfo(result[_jData]["code"].Value, result[_jData]["codelimittime"].AsLong, false);

    }

    #endregion


    #region 유저 실적(레코드 조회)

    public void Post2UserRecord(int pUserDBKEY) {

        // 사용여부 체크 
        if (!GameSystem.Instance.RecordUse)
            return; 

        OnOffWaitingRequestInLobby(true);
        WWWHelper.Instance.Post2("request_userrecord", OnFinishedUserRecord, pUserDBKEY);
    }

    private void OnFinishedUserRecord(HTTPRequest request, HTTPResponse response) {
        if (!CheckRequestState(request))
            return;

        OnOffWaitingRequestInLobby(false);

        Debug.Log(">>>> OnFinishedUserRecord :: " + response.DataAsText);

        JSONNode result = JSON.Parse(response.DataAsText);

        // re-login 체크 
        if (CheckCommonServerError(result)) {
            return;
        }

        if (result["result"].AsInt != 0) {
            return;
        }

        result = result["data"];

        WindowManagerCtrl.Instance.OpenRecord(result);
    }

    #endregion

    #region 생선 뽑기 request_fishgacha

    public void Post2FishGatcha(int pCount) {
		
		OnOffWaitingRequestInLobby (true);
		WWWHelper.Instance.Post2 ("request_fishgacha", OnFinishedFishGatcha, pCount);
	}
	
	private void OnFinishedFishGatcha(HTTPRequest request, HTTPResponse response) {
        if (!CheckRequestState(request))
            return;

        OnOffWaitingRequestInLobby (false);

		Debug.Log (">>>> OnFinishedFishGatcha :: " + response.DataAsText);

		_fishGachaJSON = JSON.Parse (response.DataAsText);

        // re-login 체크 
        if (CheckCommonServerError(_fishGachaJSON)) {
            return;
        }

        if (_fishGachaJSON["result"].AsInt != 0) {
            Debug.Log(_fishGachaJSON["error"].Value); // 코인 부족팝업 호출
            LobbyCtrl.Instance.OpenUpperInfoPopUp(PopMessageType.GoldShortage);
            return;
        }

        _fishGachaJSON = _fishGachaJSON["data"];

        
        // 코인 소모 빙고 미션 
        if(_fishGachaJSON["resultcoin"].AsInt < UserGold) {
            CheckLobbyBingoQuest(50, UserGold - _fishGachaJSON["resultcoin"].AsInt, false); // 소모된 값을 전송
        }

        UserGold = _fishGachaJSON["resultcoin"].AsInt;
        UserChub = _fishGachaJSON["resultchub"].AsInt;
        UserTuna = _fishGachaJSON["resulttuna"].AsInt;
        UserSalmon = _fishGachaJSON["resultsalmon"].AsInt; 
        
        UpdateTopInfomation();

        GatchaConfirmCtrl gatchaConfirm = FindObjectOfType(typeof(GatchaConfirmCtrl)) as GatchaConfirmCtrl;
        gatchaConfirm.OpenFishGatcha();


        // Bingo Progress
        CheckLobbyBingoQuest(53); // 낚시

    }

    #endregion

    #region ads 횟수 정보 조회


    /*
    public void Post2RequestAdsRemainAll() {

        OnOffWaitingRequestInLobby(true);
        WWWHelper.Instance.Post2("request_adsremaincount", OnFinishedAdsRemainAll);
    }


    private void OnFinishedAdsRemainAll(HTTPRequest request, HTTPResponse response) {
        if (!CheckRequestState(request))
            return;

        OnOffWaitingRequestInLobby(false);

        Debug.Log(">>>> OnFinishedAdsRemainAll :: " + response.DataAsText);

        JSONNode result = JSON.Parse(response.DataAsText);

        // re-login 체크 
        if (CheckCommonServerError(result)) {
            return;
        }

        OnCompleteAdsRemainCount(result);
    }
    */



    public void Post2RequestAdsRemainSimple() {

        //OnOffWaitingRequestInLobby(true);
        WWWHelper.Instance.Post2("request_adsremaincount", OnFinishedAdsRemainSimple);
    }

    private void OnFinishedAdsRemainSimple(HTTPRequest request, HTTPResponse response) {

        if (request.State == HTTPRequestStates.ConnectionTimedOut || request.State == HTTPRequestStates.TimedOut
                || request.State == HTTPRequestStates.Error) {
            return;
        }

        /*
        if (!CheckRequestState(request))
            return;

        OnOffWaitingRequestInLobby(false);
        */

        Debug.Log(">>>> OnFinishedAdsRemainSimple :: " + response.DataAsText);

        JSONNode result = JSON.Parse(response.DataAsText);

        if (result["result"].AsInt != 0)
            return;

        OnCompleteAdsRemainCount(result);
    }



    public void Post2RequestAdsRemainFreeGatcha() {

        OnOffWaitingRequestInLobby(true);
        WWWHelper.Instance.Post2("request_adsremaincount", OnFinishedAdsRemainFreeGatcha);
    }



    public void Post2RequestAdsRemainFillHeart() {

        OnOffWaitingRequestInLobby(true);
        WWWHelper.Instance.Post2("request_adsremaincount", OnFinishedAdsRemainFillHeart);
    }

    public void Post2RequestAdsRemainNekoGift(Action<bool> pAction) {

        OnCheckPremiumNekoGift = delegate { };
        OnCheckPremiumNekoGift += pAction;

        OnOffWaitingRequestInLobby(true);
        WWWHelper.Instance.Post2("request_adsremaincount", OnFinishedAdsRemainNekoGift);
    }


    private void OnFinishedAdsRemainNekoGift(HTTPRequest request, HTTPResponse response) {
        if (!CheckRequestState(request))
            return;

        OnOffWaitingRequestInLobby(false);

        Debug.Log(">>>> OnFinishedAdsRemainNekoGift :: " + response.DataAsText);

        JSONNode result = JSON.Parse(response.DataAsText);

        // re-login 체크 
        if (CheckCommonServerError(result)) {
            return;
        }

        OnCompleteAdsRemainCount(result);

        Debug.Log("▶ Remainnekogift ::" + Remainnekogift);
        
        if (Remainnekogift > 0) {
            // 광고 본다. 
            OnCheckPremiumNekoGift(true);
        }
        else {

            
            OnCheckPremiumNekoGift(false);

        }

        OnCheckPremiumNekoGift = delegate { };


    }


    private void OnFinishedAdsRemainFreeGatcha(HTTPRequest request, HTTPResponse response) {
        if (!CheckRequestState(request))
            return;

        OnOffWaitingRequestInLobby(false);

        Debug.Log(">>>> OnFinishedAdsRemainFreeGatcha :: " + response.DataAsText);

        JSONNode result = JSON.Parse(response.DataAsText);

        // re-login 체크 
        if (CheckCommonServerError(result)) {
            return;
        }

        OnCompleteAdsRemainCount(result);


        // 남은 횟수에 따라서 분기 
        if (Remainfreegacha > 0) {
            LobbyCtrl.Instance.OpenInfoPopUp(PopMessageType.ReadyToFreeGatcha); // Free Crane을 할까요 라고 물어본다. 
        }
        else {
            Debug.Log("All Consumed Free Gatcha Ads");

            LobbyCtrl.Instance.OpenInfoPopUp(PopMessageType.NoRemainFreeGatcha); // 남은 Free Crane이 없다. 
        }

    }


    private void OnFinishedAdsRemainFillHeart(HTTPRequest request, HTTPResponse response) {
        if (!CheckRequestState(request))
            return;


        OnOffWaitingRequestInLobby(false);

        Debug.Log(">>>> OnFinishedAdsRemainFillHeart :: " + response.DataAsText);

        JSONNode result = JSON.Parse(response.DataAsText);

        // re-login 체크 
        if (CheckCommonServerError(result)) {
            return;
        }

        OnCompleteAdsRemainCount(result);

        if (Remainheartcharge > 0) {
            // 광고 본다. 
            
            GameSystem.Instance.ShowAd(AdsType.HeartAds);
        }
        else {
            Debug.Log("All Consumed Fill Heart Ads");

            if (LobbyCtrl.Instance != null) {
                LobbyCtrl.Instance.OpenInfoPopUp(PopMessageType.NoRemainAds);
            } 
        }

    }





    /// <summary>
    /// 광고 카운팅 후처리
    /// </summary>
    /// <param name="pNode"></param>
    private void OnCompleteAdsRemainCount(JSONNode pNode) {
        Remainfreegacha = pNode["data"]["remainfreegacha"].AsInt;
        Remainstartfever = pNode["data"]["remainstartfever"].AsInt;
        Remainheartcharge = pNode["data"]["remainheartcharge"].AsInt;
        Remainnekogift = pNode["data"]["remainnekogift"].AsInt;


        if(Remainnekogift > 0 && LobbyCtrl.Instance != null) {
            LobbyCtrl.Instance.OnReadyNekoGift();
        }
        else if(Remainnekogift <= 0 && LobbyCtrl.Instance != null) {
            LobbyCtrl.Instance.SendMessage("DisableNekoGift");
        }


        // 프리 크레인 횟수에 따라 홈 아이콘 활성/비활성화
        if(Remainfreegacha > 0) {
            LobbyCtrl.Instance.SetFreeCraneIcon(true);
        }
        else {
            LobbyCtrl.Instance.SetFreeCraneIcon(false);
        }
        
    }

    #endregion

    #region 빙고 처리 

    public void Post2ClearBingo() {
        // 화면에 lock을 걸지 않는다.
        WWWHelper.Instance.Post2("request_clearbingo", OnFinishedClearBingo);
    }

    private void OnFinishedClearBingo(HTTPRequest request, HTTPResponse response) {
    }

    public void Post2SelectBingo(int pBingoID, Action<JSONNode> pAction) {

        OnOffWaitingRequestInLobby(true);

        OnCompleteSelectBingo += pAction;
        WWWHelper.Instance.Post2("request_updateuserbingo", OnFinishedSelectBingo, pBingoID);
    }

    private void OnFinishedSelectBingo(HTTPRequest request, HTTPResponse response) {

        if (!CheckRequestState(request))
            return;


        OnOffWaitingRequestInLobby(false);

        JSONNode result = JSON.Parse(response.DataAsText);

        // re-login 체크 
        if (CheckCommonServerError(result)) {
            return;
        }

        Debug.Log(response.DataAsText);


        // 실패 처리 
        if (result["result"].AsInt != 0) {
            SetSystemMessage(result["error"].Value);
            return;
        }

        result = result["data"];

        OnCompleteSelectBingo(result);
        OnCompleteSelectBingo = delegate { };

        LobbyCtrl.Instance.SendMessage("CheckBingoFocusMark");

    }


    #endregion

    #region 밋치리네코 보너스, 공유 보너스

    public void Post2GetShareBonus() {
        OnOffWaitingRequestInLobby(true);
        WWWHelper.Instance.Post2("request_getsharegift", OnFinishedGetShareBonus);
    }

    private void OnFinishedGetShareBonus(HTTPRequest request, HTTPResponse response) {
        if (!CheckRequestState(request))
            return;


        OnOffWaitingRequestInLobby(false);

        Debug.Log(" >>> OnFinishedGetShareBonus :: " + response.DataAsText);

        JSONNode result = JSON.Parse(response.DataAsText);

        // re-login 체크 
        if (CheckCommonServerError(result)) {
            return;
        }


        if (result["result"].AsInt == 0) {
            //OnCompleteNekoRewardDBkey(result);
            UserGold = result["data"]["resultcoin"].AsInt;
            UserGem = result["data"]["resultgem"].AsInt;
            UserSalmon = result["data"]["resultsalmon"].AsInt;


            LobbyCtrl.Instance.PlayNekoRewardGet();

            WindowManagerCtrl.Instance.OpenShareBonusResult(result["data"]["gifttype"].Value, result["data"]["giftvalue"].AsInt);

        }
        else {
            LobbyCtrl.Instance.OpenInfoPopUp(PopMessageType.PostCompleted);
        }
    }


    /// <summary>
    /// 공유 보너스를 획득할 수 있는지 체크 
    /// </summary>
    public void Post2CheckShareBonus() {


        OnOffWaitingRequestInLobby(true);
        WWWHelper.Instance.Post2("request_checksharegift", OnFinishedCheckShareBonus);
    }

    private void OnFinishedCheckShareBonus(HTTPRequest request, HTTPResponse response) {
        if (!CheckRequestState(request))
            return;

        OnOffWaitingRequestInLobby(false);

        Debug.Log(" >>> OnFinishedCheckShareBonus :: " + response.DataAsText);


        JSONNode result = JSON.Parse(response.DataAsText);

        // re-login 체크 
        if (CheckCommonServerError(result)) {
            return;
        }

        result = result[_jData];

        if(result["possible"].AsInt > 0) {
            WindowManagerCtrl.Instance.OpenMainShareWithBonus();
        }
        else {
            WindowManagerCtrl.Instance.OpenMainShare();
        }


        
    }


    public void Post2NekoGift(int pParam) {
        OnOffWaitingRequestInLobby(true);
        WWWHelper.Instance.Post2("request_nekogift", OnFinishedNekoGift, pParam);
    }

    private void OnFinishedNekoGift(HTTPRequest request, HTTPResponse response) {
        if (!CheckRequestState(request))
            return;


        OnOffWaitingRequestInLobby(false);

        Debug.Log(" >>> OnFinishedNekoGift :: " + response.DataAsText);
        // >>> OnFinishedNekoGift :: {"result":1,"error":"Can not neko gift take time","data":{"curtime":1462111922864,"nekogifttime":1462112259000}}
        // {"result":0,"error":"","data":{"isads":false,"nextgifttime":1462112549648,"resultcoin":30248,"resultgem":31731,"resultchub":29,"giftcoin":500,"giftgem":10,"giftchub":1}}
        
        JSONNode result = JSON.Parse(response.DataAsText);

        // re-login 체크 
        if (CheckCommonServerError(result)) {
            return;
        }

        IsNekoRewardReady = false;
        LobbyCtrl.Instance.OffReadyNekoGift();


        

        if (result["result"].AsInt == 0) {
            //OnCompleteNekoRewardDBkey(result);
            UserGold = result["data"]["resultcoin"].AsInt;
            UserGem = result["data"]["resultgem"].AsInt;
            UserChub = result["data"]["resultchub"].AsInt;
            UserTuna = result["data"]["resulttuna"].AsInt;
            UserSalmon = result["data"]["resultsalmon"].AsInt;

            _userNekorewardtime = ConvertServerTimeTick(result[_jData]["nextgifttime"].AsLong);
            _dtNextNekoRewardTime = new System.DateTime(_userNekorewardtime); // DateTime으로 변환 
            
            LobbyCtrl.Instance.PlayNekoRewardGet();

            WindowManagerCtrl.Instance.OpenNekoGiftResult(result["data"]["gifttype"].Value, result["data"]["giftvalue"].AsInt);

            // Mission Progress
            CheckMissionProgress(MissionType.Day, 7, 1);
            CheckMissionProgress(MissionType.Week, 7, 1);

            
            CheckLobbyBingoQuest(59); // 밋치리보너스 빙고 체크 

            _remainnekogift = result["data"]["remainnekogift"].AsInt;

            // 남은 수량이 없으면 로비에서 보여지지 않도록 처리 
            if(_remainnekogift <= 0 && LobbyCtrl.Instance != null) {
                LobbyCtrl.Instance.SendMessage("DisableNekoGift");
            }
            else {
                LobbyCtrl.Instance.OnReadyNekoGift();
            }

        }
        else {
            Debug.Log("Fail NekoGift");
            _isNekoRewardReady = false;

            if(LobbyCtrl.Instance != null) 
                LobbyCtrl.Instance.OffReadyNekoGift();

            _userNekorewardtime = ConvertServerTimeTick(result[_jData]["nextgifttime"].AsLong);
            _dtNextNekoRewardTime = new System.DateTime(_userNekorewardtime); // DateTime으로 변환 

        }
        
    }


    #endregion


    #region 사용자 Gem 정보 조회

    public void Post2UserGemInfo() {
        OnOffWaitingRequestInLobby(true);
        WWWHelper.Instance.Post2("request_gemdata", OnFinishedUserGemInfo);
    }

    private void OnFinishedUserGemInfo(HTTPRequest request, HTTPResponse response) {

        if (!CheckRequestState(request))
            return;


        OnOffWaitingRequestInLobby(false);

        JSONNode result = JSON.Parse(response.DataAsText);

        // re-login 체크 
        if (CheckCommonServerError(result)) {
            return;
        }

        Debug.Log(response.DataAsText);


        // 실패 처리 
        if (result["result"].AsInt != 0) {
            SetSystemMessage(result["error"].Value);
            return;
        }

        
        result = result["data"];

        // Lobby 창에서만 사용, 팝업창 오픈 
        WindowManagerCtrl.Instance.OpenUserGemInfo(result);

        
    }

    #endregion

    #region 메인 네코 설정


    public void Post2MainNeko() {
        OnOffWaitingRequestInLobby(true);
        WWWHelper.Instance.Post2("request_mainneko", OnFinishedMainNeko);
    }

    private void OnFinishedMainNeko(HTTPRequest request, HTTPResponse response) {

        if (!CheckRequestState(request))
            return;


        OnOffWaitingRequestInLobby(false);

        JSONNode result = JSON.Parse(response.DataAsText);
        Debug.Log("OnFinishedMainNeko :: " + response.DataAsText);

        // re-login 체크 
        if (CheckCommonServerError(result)) {
            return;
        }

        // 실패 처리 
        if (result["result"].AsInt != 0) {
            //SetSystemMessage("네코 생선 주기가 실패하였어요. (" + result["error"].Value + ")");
            return;
        }

        // 대상 네코 정보 업데이트 
        int nekoIndex = FindUserNekoDataByDBkey(UpgradeNekoDBKey);

        if (nekoIndex < 0) {
            Debug.Log("▶ can't find neko");
            return;
        }


        UserDataJSON[_jData]["mainneko"].AsInt = UserNeko[nekoIndex]["tid"].AsInt;
        UserDataJSON[_jData]["mainnekograde"].AsInt = UserNeko[nekoIndex]["star"].AsInt;

        // Refresh
        NekoSelectBigPopCtrl nekogrow = null;
        nekogrow = FindObjectOfType(typeof(NekoSelectBigPopCtrl)) as NekoSelectBigPopCtrl;

        if (nekogrow != null)
            nekogrow.SetCurrentNeko(SelectNeko);
    }

    #endregion


    #region 네코 생선 주기 

    public void Post2NekoFeedFish() {
		OnOffWaitingRequestInLobby (true);
		WWWHelper.Instance.Post2 ("request_eatfish", OnFinishedNekoFeedFish);
	}

	private void OnFinishedNekoFeedFish(HTTPRequest request, HTTPResponse response) {
        bool isRankedUp = false; // 랭크업 여부 

        // 랭크업 (진화) 관련 변수 
        int nekoID, previousGrade, currentGrade;

        if (!CheckRequestState(request))
            return;



        OnOffWaitingRequestInLobby (false);

		JSONNode result = JSON.Parse (response.DataAsText);

        // re-login 체크 
        if (CheckCommonServerError(result)) {
            return;
        }

        Debug.Log("OnFinishedNekoFeedFish :: " + response.DataAsText);

		// 실패 처리 
		if (result ["result"].AsInt != 0) {
			SetSystemMessage("あげれるおさかながありません");
			return;
		}

		// 대상 네코 정보 업데이트 
		int nekoIndex = FindUserNekoDataByDBkey(result[_jData]["nekodbkey"].AsInt);
		
		if(nekoIndex < 0) {
			Debug.Log("▶ Not Found request_eatfish");
		}

        // 진화시에 Mission Progress
        if (UserNeko[nekoIndex]["star"].AsInt < result[_jData]["nekostar"].AsInt) {

            Debug.Log("★★★ Neko Rank up!");

            isRankedUp = true;


            nekoID = UserNeko[nekoIndex]["tid"].AsInt;
            previousGrade = UserNeko[nekoIndex]["star"].AsInt;
            currentGrade = result[_jData]["nekostar"].AsInt;

            // 진화화면 오픈 
            LobbyCtrl.Instance.OpenNekoEvolution(nekoID, previousGrade, currentGrade);


            // 스킬 업데이트 
            UserNeko[nekoIndex]["skillid1"].AsInt = result[_jData]["neko"]["skillid1"].AsInt;
            UserNeko[nekoIndex]["skillid2"].AsInt = result[_jData]["neko"]["skillid2"].AsInt;
            UserNeko[nekoIndex]["skillid3"].AsInt = result[_jData]["neko"]["skillid3"].AsInt;
            UserNeko[nekoIndex]["skillid4"].AsInt = result[_jData]["neko"]["skillid4"].AsInt;

            UserNeko[nekoIndex]["skillvalue1"].AsInt = result[_jData]["neko"]["skillvalue1"].AsInt;
            UserNeko[nekoIndex]["skillvalue2"].AsInt = result[_jData]["neko"]["skillvalue2"].AsInt;
            UserNeko[nekoIndex]["skillvalue3"].AsInt = result[_jData]["neko"]["skillvalue3"].AsInt;
            UserNeko[nekoIndex]["skillvalue4"].AsInt = result[_jData]["neko"]["skillvalue4"].AsInt;

        }

        UserNeko[nekoIndex]["star"].AsInt = result[_jData]["nekostar"].AsInt;
        UserNeko[nekoIndex]["bead"].AsInt = result[_jData]["nekobead"].AsInt;

        _userChub = result[_jData]["resultchub"].AsInt;
		_userTuna = result[_jData]["resulttuna"].AsInt;
		_userSalmon = result[_jData]["resultsalmon"].AsInt;

        // 튜토리얼 처리 생선 먹이기.  (5에서 6처리 ) 
        if (GameSystem.Instance.LocalTutorialStep == 5) {
            GameSystem.Instance.SaveLocalTutorialStep(6);
        }

        // 추가 처리 (네코 창)
        //PopupNekoInfoCtrl _popupNekoInfo = FindObjectOfType(typeof(PopupNekoInfoCtrl)) as PopupNekoInfoCtrl;
		
		//CurrentSelectNeko.Bead = result[_jData]["nekobead"].AsInt;
		//CurrentSelectNeko.Grade = result[_jData]["nekostar"].AsInt;
        CurrentSelectNeko.UpdateInfo();




        // NekoFeed 창 업데이트 
        //NekoFeedCtrl _nekoFeed = FindObjectOfType(typeof(NekoFeedCtrl)) as NekoFeedCtrl;
        //_nekoFeed.RefreshNekoInfo();
        NekoSelectBigPopCtrl popupNeko = FindObjectOfType(typeof(NekoSelectBigPopCtrl)) as NekoSelectBigPopCtrl;
        popupNeko.OnCompleteFeedFish(!isRankedUp);



        // Mission Progress 
        int feedCount = GameSystem.Instance.FeedChub + GameSystem.Instance.FeedTuna + GameSystem.Instance.FeedSalmon;
        



        // 피쉬 정보 초기화 
        GameSystem.Instance.InitFishFeed();

    }

	#endregion

	#region 네코 레벨 업그레이드 request_nekoupgrade
	public void Post2NekoUpgrade(int pNextLevel) {
		OnOffWaitingRequestInLobby (true);

		WWWHelper.Instance.Post2 ("request_nekoupgrade", OnFinishedNekoUpgrade, pNextLevel);
	}

	private void OnFinishedNekoUpgrade(HTTPRequest request, HTTPResponse response) {

        int upLevel = 0;

        if (!CheckRequestState(request))
            return;

        OnOffWaitingRequestInLobby (false);


        Debug.Log(response.DataAsText);

		JSONNode result = JSON.Parse (response.DataAsText);

        // re-login 체크 
        if (CheckCommonServerError(result)) {
            return;
        }

        // 네코 팝업창 찾기. 
        NekoUpCtrl popupNekoUp = FindObjectOfType(typeof(NekoUpCtrl)) as NekoUpCtrl;
        popupNekoUp.OnCompleteUpgrade();

        // 업그레이드 실패 처리 
        if (result ["result"].AsInt != 0) {
			if ("level is max".Equals (result ["error"].Value)) {
				// 레벨 만땅 
				LobbyCtrl.Instance.OpenInfoPopUp(PopMessageType.NeedGradeUp);
			} else if ("Not enough gold".Equals (result ["error"].Value)) {
				// 돈 부족 
				LobbyCtrl.Instance.OpenUpperInfoPopUp (PopMessageType.GoldShortage);
			}

			return;
		}

        // 코인 소비 Bingo 
        if(result[_jData]["resultgold"].AsInt < UserGold) {
            CheckLobbyBingoQuest(50, UserGold - result[_jData]["resultgold"].AsInt, false); // 소모된 값을 전송
        }




        // 재화정보, 네코 정보 업데이트 
        UserGold = result [_jData] ["resultgold"].AsInt;

		int tmp = FindUserNekoDataByDBkey(result[_jData]["nekodbkey"].AsInt);
		if(tmp>=0) {
            Debug.Log("Update _userNekoData");
            upLevel = result[_jData]["nekolevel"].AsInt - UserNeko[tmp]["level"].AsInt;
            UserNeko[tmp]["level"].AsInt = result[_jData]["nekolevel"].AsInt;  // level 처리
        }

		// Adbrix 
		AdbrixManager.Instance.SendAdbrixInAppActivity (AdbrixManager.Instance.NEKO_UPGRADE);

		// 상단 업데이트 
		UpdateTopInfomation ();

		// 네코 업그레이드 창은 닫는다.
        popupNekoUp.SendMessage("CloseSelf");

        // 성장 화면 업데이트 
        NekoSelectBigPopCtrl popupNeko = FindObjectOfType(typeof(NekoSelectBigPopCtrl)) as NekoSelectBigPopCtrl;
        popupNeko.OnCompleteUpgrade();
        
        if(ReadyGroupCtrl.Instance != null) {
            ReadyGroupCtrl.Instance.UpdateEquipNeko();
        }


        // Bingo Progress
        //CheckLobbyBingoQuest(17, upLevel);

        // 뱃지 빙고 체크 
        LobbyCtrl.Instance.SpecialBingoCheck();


    }
    #endregion

    #region Image Download 

    #region 가챠 배너 다운로드 

    /// <summary>
    /// 가챠 배너 이미지 다운로드 
    /// </summary>
    public void DownloadGatchaImages() {
        Debug.Log("▶▶▶▶▶▶▶ DownloadGatchaImages");

        _preSavedGachaBannerVersion = GATCHA_BANNER_VERSION; // 다운로드 시작전에 변수로 저장 

        //bigbannerurl, smallbannerurl
        for (int i=0; i<GatchaBannerInitJSON.Count; i++) {
            
            var request = new HTTPRequest(new Uri(GatchaBannerInitJSON[i]["smallbannerurl"].Value), GatchaImageDownloaded);
            request.ConnectTimeout = TimeSpan.FromSeconds(30);
            request.Timeout = TimeSpan.FromSeconds(60);
            request.Send();
        }
    }

    /// <summary>
    /// 모든 가챠이미지 다운로드 Callback
    /// </summary>
    /// <param name="req"></param>
    /// <param name="resp"></param>
    private void GatchaImageDownloaded(HTTPRequest req, HTTPResponse resp) {

        //Debug.Log(">>>>>>>>>> GatchaImageDownloaded Callback");


        if (req.State != HTTPRequestStates.Finished) {
            Debug.Log("Download Fail");
            // 다운로드 실패시, 재 다운로드 실행 처리 
            req.ConnectTimeout = TimeSpan.FromSeconds(10);
            req.Timeout = TimeSpan.FromSeconds(60);
            req.Send();

            // 실패시에는 버전 정보를 초기화 시킨다.
            GATCHA_BANNER_VERSION = 0;
            ES2.Save<int>(GATCHA_BANNER_VERSION, GATCHA_BANNER_DATA);

            return;
        }

        // 다운로드에 성공했다면, version을 저장한다. 
        GATCHA_BANNER_VERSION = _preSavedGachaBannerVersion;
        ES2.Save<int>(GATCHA_BANNER_VERSION, GATCHA_BANNER_DATA);

        Texture2D tex = new Texture2D(100, 100);
        tex.LoadImage(resp.Data);

        byte[] bytes = tex.EncodeToPNG();
        string type = GetGatchaImageType(req.Uri.ToString());

        if(type == "fish") {
            System.IO.File.WriteAllBytes(Application.persistentDataPath + "/" + "small_fish_gatcha.png", bytes);
            LoadLocalFishBanner(tex);
            //FishSmallBanner = tex;
        } 
        else if (type == "free") {
            System.IO.File.WriteAllBytes(Application.persistentDataPath + "/" + "small_free_gatcha.png", bytes);
            LoadLocalFreeBanner(tex);
            //FreeSmallBanner = tex;

        }
        else if (type == "special") {
            System.IO.File.WriteAllBytes(Application.persistentDataPath + "/" + "small_special_gatcha.png", bytes);
            LoadLocalSpecialBanner(tex);
            //SpecialSmallBanner = tex;
        }
    }

    #region Each Gatcha Banner LocalLoad

    /// <summary>
    /// 로컬에 저장된 fish banner 조회 
    /// </summary>
    /// <param name="bytes"></param>
    private void LoadLocalFishBanner(Texture2D pTex = null) {
        if(!File.Exists(_fileFishSmallBanner) && pTex != null) {
            FishSmallBanner = pTex;
            return;
        }

        byte[] bytes;
        //Debug.Log("◆◆◆◆◆◆◆ LoadGatchaImages #1 :: " + _fileFishSmallBanner);
        bytes = File.ReadAllBytes(_fileFishSmallBanner);
        FishSmallBanner = new Texture2D(0, 0);
        FishSmallBanner.LoadImage(bytes);
    }


    private void LoadLocalFreeBanner(Texture2D pTex = null) {
        if (!File.Exists(_fileFreeSmallBanner) && pTex != null) {
            FreeSmallBanner = pTex;
            return;
        }

        byte[] bytes;
        //Debug.Log("◆◆◆◆◆◆◆ LoadGatchaImages #1 :: " + _fileFishSmallBanner);
        bytes = File.ReadAllBytes(_fileFreeSmallBanner);
        FreeSmallBanner = new Texture2D(0, 0);
        FreeSmallBanner.LoadImage(bytes);
    }


    private void LoadLocalSpecialBanner(Texture2D pTex = null) {
        if (!File.Exists(_fileSpecialSmallBanner) && pTex != null) {
            SpecialSmallBanner = pTex;
            return;
        }

        byte[] bytes;
        //Debug.Log("◆◆◆◆◆◆◆ LoadGatchaImages #1 :: " + _fileFishSmallBanner);
        bytes = File.ReadAllBytes(_fileSpecialSmallBanner);
        SpecialSmallBanner = new Texture2D(0, 0);
        SpecialSmallBanner.LoadImage(bytes);
    }

    #endregion

    /// <summary>
    /// 로컬에 저장된 가챠이미지 정보 조회 
    /// 없는 이미지가 있다면, 다운로드를 진행해야 한다. 
    /// </summary>
    private void LoadGatchaImages() {

        Debug.Log("◆◆◆◆◆◆◆ LoadGatchaImages");

        // 
        if (!File.Exists(_fileFishSmallBanner) ||  !File.Exists(_fileFreeSmallBanner)
            || !File.Exists(_fileSpecialSmallBanner)) {

            Debug.Log("◆◆◆◆◆◆◆ Missing one of Gatcha Banner");

            _isLoadingImageException = true;
            return;
        }


        Debug.Log("◆◆◆◆◆◆◆ LoadGatchaImages All files Exist");


        try {
            byte[] bytes;
            //Debug.Log("◆◆◆◆◆◆◆ LoadGatchaImages #1 :: " + _fileFishSmallBanner);
             bytes = File.ReadAllBytes(_fileFishSmallBanner);
            FishSmallBanner = new Texture2D(0, 0);
            FishSmallBanner.LoadImage(bytes);

            bytes = File.ReadAllBytes(_fileFreeSmallBanner);
            FreeSmallBanner = new Texture2D(0, 0);
            FreeSmallBanner.LoadImage(bytes);
            
            bytes = File.ReadAllBytes(_fileSpecialSmallBanner);
            SpecialSmallBanner = new Texture2D(0, 0);
            SpecialSmallBanner.LoadImage(bytes);

        }
        catch(Exception e) {
            Debug.Log("◆◆◆◆◆◆◆ LoadGatchaImages Exception : " + e.StackTrace);
            _isLoadingImageException = true;

        }


    }

    #endregion


    #region 공지 배너 다운로드

    private void DownloadNoticeImages() {


        //Debug.Log("▶▶▶▶▶▶▶ DownloadNoticeImages");
        _preSavedNoticeBannerVersion = NOTICE_BANNER_VERSION; // 다운로드 시작전에 변수로 저장 

        // 개수만큼 new 
        ArrNoticeSmallTextures = new Texture2D[NoticeBannerInitJSON.Count];

        for (int i=0; i<NoticeBannerInitJSON.Count; i++) {
            var request = new HTTPRequest(new Uri(NoticeBannerInitJSON[i]["smallbannerurl"].Value), NoticeImageDownloaded);
            request.Tag = i;
            request.ConnectTimeout = TimeSpan.FromSeconds(30);
            request.Timeout = TimeSpan.FromSeconds(60);
            request.Send();
        }
    }

    private void NoticeImageDownloaded(HTTPRequest req, HTTPResponse resp) {

        if (req.State != HTTPRequestStates.Finished) {
            Debug.Log("Notice Image Download Fail");
            // 다운로드 실패시, 재 다운로드 실행 처리 
            req.ConnectTimeout = TimeSpan.FromSeconds(10);
            req.Timeout = TimeSpan.FromSeconds(60);
            req.Send();

            // 실패시에는 버전 정보를 초기화 시킨다.
            NOTICE_BANNER_VERSION = 0;
            ES2.Save<int>(NOTICE_BANNER_VERSION, NOTICE_BANNER_DATA);

            return;
        }


        // 다운로드에 성공했다면, version을 저장한다. 
        NOTICE_BANNER_VERSION = _preSavedNoticeBannerVersion;
        ES2.Save<int>(NOTICE_BANNER_VERSION, NOTICE_BANNER_DATA);


        Texture2D tex = new Texture2D(0, 0);
        tex.LoadImage(resp.Data);

        byte[] bytes = tex.EncodeToPNG();

        int index = (int)req.Tag;

        System.IO.File.WriteAllBytes(_fileNoticeSmallBanner + index.ToString() + ".png", bytes);
        LoadLocalEachNoticeImage(index, tex);
        //ArrNoticeSmallTextures[index] = tex;
    }

    /// <summary>
    /// 개별 로컬 공지배너 이미지 로드 
    /// </summary>
    private void LoadLocalEachNoticeImage(int pIndex, Texture2D pTex = null) {
        if(!File.Exists(_fileNoticeSmallBanner + pIndex.ToString() + ".png") && pTex != null) {
            ArrNoticeSmallTextures[pIndex] = pTex;
            return;
        }

        byte[] bytes = File.ReadAllBytes(_fileNoticeSmallBanner + pIndex.ToString() + ".png");
        ArrNoticeSmallTextures[pIndex] = new Texture2D(0, 0);
        ArrNoticeSmallTextures[pIndex].LoadImage(bytes);
    }

    /// <summary>
    /// 모든 로컬 공지 배너 이미지 로드 
    /// </summary>
    private void LoadLocalNoticeImages() {

        Debug.Log("▶▶▶▶▶▶▶ LoadLocalNoticeImages");

        byte[] bytes;

        try {

            if(NoticeBannerInitJSON == null) {
                _isLoadingImageException = true;
                return;
            }

            ArrNoticeSmallTextures = new Texture2D[NoticeBannerInitJSON.Count];

            for (int i = 0; i < NoticeBannerInitJSON.Count; i++) {
                if (!File.Exists(_fileNoticeSmallBanner + i.ToString() + ".png"))  {

                    Debug.Log("◆◆◆◆◆◆◆ Missing one of Notice Banner");
                    _isLoadingImageException = true;
                    return;
                }

                Debug.Log("Notice Small Banner File :: " + _fileNoticeSmallBanner + i.ToString() + ".png");

                bytes = File.ReadAllBytes(_fileNoticeSmallBanner + i.ToString() + ".png");
                ArrNoticeSmallTextures[i] = new Texture2D(0, 0);
                ArrNoticeSmallTextures[i].LoadImage(bytes);
            }
        } 
        catch(Exception e) {
            Debug.Log("◆◆◆◆◆◆◆ LoadLocalNoticeImages Exception e : " +  e.StackTrace);

            _isLoadingImageException = true;
        }

    }

    #endregion

    #region 패키지 배너 다운로드

    private void DownloadPackageImages() {
        Debug.Log("▶▶▶▶▶▶▶ DownloadPackageImages");
        _preSavedPackageBannerVersion = PACKAGE_BANNER_VERSION; // 다운로드 시작전에 변수로 저장 

        ArrPackageSmallTextures = new Texture2D[PackageBannerInitJSON.Count];

        for (int i = 0; i < PackageBannerInitJSON.Count; i++) {
            var request = new HTTPRequest(new Uri(PackageBannerInitJSON[i]["smallbannerurl"].Value), PackageImageDownloaded);
            request.Tag = i;
            request.ConnectTimeout = TimeSpan.FromSeconds(30);
            request.Timeout = TimeSpan.FromSeconds(60);
            request.Send();
        }
    }


    private void PackageImageDownloaded(HTTPRequest req, HTTPResponse resp) {

        if (req.State != HTTPRequestStates.Finished) {
            Debug.Log("Package Image Download Fail");
            req.ConnectTimeout = TimeSpan.FromSeconds(10);
            req.Timeout = TimeSpan.FromSeconds(60);
            req.Send();


            // 실패시에는 버전 정보를 초기화 시킨다.
            PACKAGE_BANNER_VERSION = 0;
            ES2.Save<int>(PACKAGE_BANNER_VERSION, PACKAGE_BANNER_DATA);

            return;
        }



        // 다운로드에 성공했다면, version을 저장한다. 
        PACKAGE_BANNER_VERSION = _preSavedPackageBannerVersion;
        ES2.Save<int>(PACKAGE_BANNER_VERSION, PACKAGE_BANNER_DATA);

        Texture2D tex = new Texture2D(0, 0);
        tex.LoadImage(resp.Data);

        byte[] bytes = tex.EncodeToPNG();

        int index = (int)req.Tag;

        System.IO.File.WriteAllBytes(_filePackageSmallBanner + index.ToString() + ".png", bytes);
        EachLocalPackageImage(index, tex);
        //ArrPackageSmallTextures[index] = tex;

    }


    /// <summary>
    /// 개별 패키지 이미지 조회
    /// </summary>
    /// <param name="pIndex"></param>
    /// <param name="pTex"></param>
    private void EachLocalPackageImage(int pIndex, Texture2D pTex = null) {

        // 파일이 존재하지 않는 경우 
        if (!File.Exists(_filePackageSmallBanner + pIndex.ToString() + ".png") && pTex != null) {
            ArrPackageSmallTextures[pIndex] = pTex;
            return;
        }

        byte[] bytes = File.ReadAllBytes(_filePackageSmallBanner + pIndex.ToString() + ".png");
        ArrPackageSmallTextures[pIndex] = new Texture2D(0, 0);
        ArrPackageSmallTextures[pIndex].LoadImage(bytes);
    }


    /// <summary>
    /// 모든 로컬 패키지 배너 이미지 조회
    /// </summary>
    private void LoadLocalPackageImages() {

        Debug.Log("▶▶▶▶▶▶▶ LoadLocalPackageImages");

        byte[] bytes;

        try {

            if (PackageBannerInitJSON == null) {
                _isLoadingImageException = true;
                return;
            }

            ArrPackageSmallTextures = new Texture2D[PackageBannerInitJSON.Count];

            for (int i = 0; i < PackageBannerInitJSON.Count; i++) {
                if (!File.Exists(_filePackageSmallBanner + i.ToString() + ".png")) {

                    Debug.Log("◆◆◆◆◆◆◆ Missing one of Package Banner");
                    _isLoadingImageException = true;
                    return;
                }

                bytes = File.ReadAllBytes(_filePackageSmallBanner + i.ToString() + ".png");
                ArrPackageSmallTextures[i] = new Texture2D(0, 0);
                ArrPackageSmallTextures[i].LoadImage(bytes);


            }
        }
        catch (Exception e) {
            Debug.Log("◆◆◆◆◆◆◆ LoadLocalPackageImages Exception : " + e.StackTrace );
            
        }

    }

    #endregion

    #endregion


    #region Check New Mail request_checknewmail


    public void Post2LineInviteReward() {
        WWWHelper.Instance.Post2("request_lineinvite", OnFinishedLineInviteReward);
    }

    private void OnFinishedLineInviteReward(HTTPRequest request, HTTPResponse response) {
        if (!CheckRequestState(request))
            return;

        Debug.Log(" >>> OnFinishedLineInviteReward :: " + response.DataAsText);

        JSONNode result = JSON.Parse(response.DataAsText);

        // re-login 체크 
        if (CheckCommonServerError(result)) {
            return;
        }

        // 결과 에 따라 메세지 팝업 
        if(result["result"].AsInt == 0) {
            //LobbyCtrl.Instance.OpenUpperInfoPopUp(PopMessageType.LineInvited);
            WindowManagerCtrl.Instance.OpenLineInviteResult(result[_jData]);
        }
    }




    public void Post2CheckNewMailUnder() {
        WWWHelper.Instance.Post2("request_checknewmail", OnFinishedCheckNewMail);
    }

    public void Post2CheckNewMail() {
		OnOffWaitingRequestInLobby (true);
		WWWHelper.Instance.Post2 ("request_checknewmail", OnFinishedCheckNewMail);
	}

	private void OnFinishedCheckNewMail(HTTPRequest request, HTTPResponse response) {


        if (!CheckRequestState(request))
            return;

        OnOffWaitingRequestInLobby (false);

		Debug.Log (" >>> OnFinishedCheckNewMail :: " + response.DataAsText);

		JSONNode result = JSON.Parse (response.DataAsText);

        // re-login 체크 
        if (CheckCommonServerError(result)) {
            return;
        }

        if ("y".Equals(result[_jData]["newmail"].Value)) {
			HasNewMail = true;
		} else {
			HasNewMail = false;
		}
		
		LobbyCtrl.Instance.UpdateMailBoxNew();

	}

	#endregion

	#region 메일함 리스트 request_maillist

	public void Post2MailList() {
		OnOffWaitingRequestInLobby (true);
		WWWHelper.Instance.Post2 ("request_maillist", OnFinishedMailList);
	}
	
	private void OnFinishedMailList(HTTPRequest request, HTTPResponse response) {
        if (!CheckRequestState(request))
            return;

        OnOffWaitingRequestInLobby (false);
		
		Debug.Log (" >>> OnFinishedMailList :: " + response.DataAsText);
		
		_mailData = JSON.Parse (response.DataAsText);

        // re-login 체크 
        if (CheckCommonServerError(_mailData)) {
            return;
        }

        // 메일함 리스트 표현 
        LobbyCtrl.Instance.SpawnMailBoxColumns ();
		
	}
    #endregion

    #region 랭킹 리스트 request_ranking

    public void Post2AroundRankList(Action pCallback) {
        OnOffWaitingRequestInLobby(true);

        OnCompleteAroundRankList = delegate { };
        OnCompleteAroundRankList += pCallback;
        WWWHelper.Instance.Post2("request_aroundranking", OnFinishedAroundRankList);
    }

    private void OnFinishedAroundRankList(HTTPRequest request, HTTPResponse response) {
        if (!CheckRequestState(request))
            return;

        //OnOffWaitingRequestInLobby (false);

        Debug.Log(" >>> OnFinishedAroundRankList :: " + response.DataAsText);

        _rankJSON = JSON.Parse(response.DataAsText);

        // re-login 체크 
        if (CheckCommonServerError(_rankJSON)) {
            return;
        }

        OnCompleteAroundRankList();

    }


    public void Post2RankList() {
		OnOffWaitingRequestInLobby (true);
		WWWHelper.Instance.Post2 ("request_ranking", OnFinishedRankList);
	}



    private void OnFinishedRankList(HTTPRequest request, HTTPResponse response) {
        if (!CheckRequestState(request))
            return;

        //OnOffWaitingRequestInLobby (false);

        Debug.Log (" >>> OnFinishedRankList :: " + response.DataAsText);
		
		_rankJSON = JSON.Parse (response.DataAsText);

        // re-login 체크 
        if (CheckCommonServerError(_rankJSON)) {
            return;
        }

        // 랭킹 리스트 
        LobbyCtrl.Instance.SpawnRankData ();
		
	}





	#endregion

	#region 튜토리얼 캐릭터 뽑기, 튜토리얼 완료 request_gacha, request_tutorialcomplete

	public void Post2TutorialGatcha() {
		OnOffWaitingRequestInLobby (true);
		WWWHelper.Instance.Post2 ("request_gacha", OnFinishedTutorialGatcha);
	}
	
	
	private void OnFinishedTutorialGatcha(HTTPRequest request, HTTPResponse response) {
        if (!CheckRequestState(request))
            return;

        OnOffWaitingRequestInLobby (false);
		
		Debug.Log (" >>> OnFinishedTutorialGatcha :: " + response.DataAsText);
		
		_gatchaData = JSON.Parse (response.DataAsText);
		
		OnCompletePostGatcha (_gatchaData);

		Post2TutorialComplete ();
	}

	public void Post2TutorialComplete() {
		OnOffWaitingRequestInLobby (true);
		WWWHelper.Instance.Post2 ("request_tutorialcomplete", OnFinishedTutorialComplete);
	}
	
	
	private void OnFinishedTutorialComplete(HTTPRequest request, HTTPResponse response) {
        if (!CheckRequestState(request))
            return;

        OnOffWaitingRequestInLobby (false);
		
		Debug.Log (" >>> OnFinishedTutorialComplete :: " + response.DataAsText);
		
		_eventJSON = JSON.Parse (response.DataAsText);
        // re-login 체크 
        if (CheckCommonServerError(_eventJSON)) {
            return;
        }

        // 튜토리얼 완료 통신 후 0보다 큰 값이 들어오면 변수 세팅 . 
        if (_eventJSON[_jData]["tutorialcomplete"].AsInt > 0) {
			Debug.Log("!!! OnHttpRequest request_tutorialcomplete #2");
			_tutorialComplete = _eventJSON[_jData]["tutorialcomplete"].AsInt;
            tutorialStage = -1;
            

        }
		
		// Adbrix
		AdbrixManager.Instance.SendAdbrixNewUserFunnel(AdbrixManager.Instance.TUTORIAL_DONE);
        



        if (GameSystem.Instance.LocalTutorialStep == 6) {
            GameSystem.Instance.SaveLocalTutorialStep(7);


            // 튜토리얼 완료했으면, 출석체크 정보를 불러온다.
            Post2Attendance();
        }

        Post2Unlock("unlock_user"); // Unlock User 대상 처리 
        StageMasterCtrl.Instance.MoveTheme(1);
            	
	}

    


	#endregion 

	#region 캐릭터 뽑기 request_gacha

	public void Post2EventGatcha(int pEventNo) {
		OnOffWaitingRequestInLobby (true);
		WWWHelper.Instance.Post2 ("request_gacha", OnFinishedGatcha, pEventNo);
	}

	public void Post2Gatcha() {
		OnOffWaitingRequestInLobby (true);
		WWWHelper.Instance.Post2 ("request_gacha", OnFinishedGatcha);
	}
	
	
	private void OnFinishedGatcha(HTTPRequest request, HTTPResponse response) {
        if (!CheckRequestState(request))
            return;

        OnOffWaitingRequestInLobby (false);
		
		Debug.Log (" >>> OnFinishedGatcha :: " + response.DataAsText);
		
		_gatchaData = JSON.Parse (response.DataAsText);

        // re-login 체크 
        if (CheckCommonServerError(_gatchaData)) {
            return;
        }

        // 캐릭터 뽑기 실패. 
        if (_gatchaData ["result"].AsInt == 1) {

            if (_gatchaData["error"].ToString().IndexOf("not enough") >= 0) {
                LobbyCtrl.Instance.OpenInfoPopUp(PopMessageType.GemShortage, LobbyCtrl.Instance.OpenJewelShop);
                return;
            }
            else {
                //AndroidMessage.Create("확인", _gatchaData["error"].Value);
                return;
            }
        }

        if(_isFreeGatcha) {
            WWWHelper.Instance.AdsID++;
        }

		OnCompletePostGatcha (_gatchaData);
		
	}

	/// <summary>
	/// Post Gatcha 후 세팅 
	/// </summary>
	/// <param name="pNode">P node.</param>
	private void OnCompletePostGatcha(JSONNode pNode) {
		
		// 재화처리
		if (pNode [_jData] ["resultgem"].Value != null && pNode [_jData] ["resultgem"].Value != "") {
			UserGem = pNode [_jData] ["resultgem"].AsInt; // 유저 젬 
		}
		
		if(pNode [_jData] ["gachaads"].Value != null && pNode [_jData] ["gachaads"].Value != "")
			_userDataJSON [_jData] ["gachaads"].AsInt = pNode [_jData] ["gachaads"].AsInt;

        if (pNode[_jData]["resultchub"].Value != null && pNode[_jData]["resultchub"].Value != "")
            UserChub = pNode[_jData]["resultchub"].AsInt;

        if (pNode[_jData]["resulttuna"].Value != null && pNode[_jData]["resulttuna"].Value != "")
            UserTuna = pNode[_jData]["resulttuna"].AsInt;

        if (pNode[_jData]["resultsalmon"].Value != null && pNode[_jData]["resultsalmon"].Value != "")
            UserSalmon = pNode[_jData]["resultsalmon"].AsInt;


        
        if(!String.IsNullOrEmpty(pNode[_jData]["remainfreegacha"].Value)) {
            Remainfreegacha = pNode[_jData]["remainfreegacha"].AsInt;
            if (Remainfreegacha <= 0) {
                LobbyCtrl.Instance.SetFreeCraneIcon(false);
            }
            else {
                LobbyCtrl.Instance.SetFreeCraneIcon(true);
            }
        }
        


	
		UpdateTopInfomation ();




		// Track Event
		AdbrixManager.Instance.SendAdbrixInAppActivity (AdbrixManager.Instance.GATCHA);



		if (GameSystem.Instance.IsFreeGatcha) {
			//AdbrixManager.Instance.SendAppsFlyerEvent (AdbrixManager.Instance.AF_FREE_GACHA);
		}
        else {
            if(tutorialStage != 0) {
                // Mission Progress
                CheckMissionProgress(MissionType.Week, 9, 1);
            }
        }


        LobbyCtrl.Instance.OnCompletePostGatchaRequest ();
		

	}


    /// <summary>
    /// 가챠 후 고양이 정보 업데이트 
    /// </summary>
    /// <param name="pNode"></param>
    public void UpdateNekoAfterGatcha(JSONNode pNode) {

        Debug.Log("UpdateNekoAfterGatcha #1");

        if (pNode == null)
            return;

        Debug.Log("UpdateNekoAfterGatcha #2");

        for(int i=0; i<pNode[_jData]["resultlist"].Count; i++) {
            if (pNode[_jData]["resultlist"][i]["isFusion"].AsInt == 0) { // 새로운 고양이 
                AddNewNeko(pNode[_jData]["resultlist"][i]);
            }
            else if (pNode[_jData]["resultlist"][i]["isFusion"].AsInt == 1) { // 퓨전 
                UpdateNewNeko(pNode[_jData]["resultlist"][i]);
            }
        }
    }

    public void UpdateSingleNekoData(JSONNode pNode) {
        Debug.Log("UpdateSingleNekoData pNode :: " + pNode.ToString());

        if(pNode["isFusion"].AsInt == 0) {
            Debug.Log("UpdateSingleNekoData add Neko");
            AddNewNeko(pNode);
         
        }
        else {
            Debug.Log("UpdateSingleNekoData update neko");
            UpdateNewNeko(pNode);
        }

    }

	#endregion





	#region 하트 받기 request_takeheart
	public void Post2TakeHeart() {
		OnOffWaitingRequestInLobby (true);
		WWWHelper.Instance.Post2 ("request_takeheart", OnFinishedTakeHeart);
	}

	private void OnFinishedTakeHeart(HTTPRequest request, HTTPResponse response) {
        if (!CheckRequestState(request))
            return;

        _takeHeartJSON = JSON.Parse (response.DataAsText);
		OnCompleteRequestTakeHeart (_takeHeartJSON);
		OnOffWaitingRequestInLobby (false);

	}
    #endregion

    #region 테마 클리어 보상 요청

    public void Post2RequestThemeClearPay(int pThemeID) {
        
        
        WWWHelper.Instance.Post2("request_takethemepay", OnFinishedRequestThemeClearPay, pThemeID);
    }


    private void OnFinishedRequestThemeClearPay(HTTPRequest request, HTTPResponse response) {
        if (!CheckRequestState(request))
            return;

        
        Debug.Log(">>>> OnFinishedRequestThemeClearPay :: " + response.DataAsText);
        JSONNode result = JSON.Parse(response.DataAsText);

        // re-login 체크 
        if (CheckCommonServerError(result)) {
            return;
        }

        result = result[_jData];
        UserThemeJSON = result["theme"]; // 사용자 Theme 진척도 갱신
    }


    #endregion


    #region Game Start

    /// <summary>
    /// Post2s the game start.
    /// </summary>
    public void Post2GameStart() {

        // 하트 체크 추가
        if (HeartCount <= 0) {
            OpenMessageWithCallBack(PopMessageType.HeartZero);
            return;
        }


        OnOffWaitingRequestInLobby (true);
		WWWHelper.Instance.Post2 ("request_synctime", OnFinishedGameStartSyncTime);

        Fader.Instance.FadeIn(0.5f);
        LobbyCtrl.Instance.OpenDelayedSceneTip();
        GSceneTipCtrl.OnCompletePostStart += DoGameStart;


    }

	private void OnFinishedGameStartSyncTime(HTTPRequest request, HTTPResponse response) {
        if (!CheckRequestState(request))
            return;

        _resultForm = JSON.Parse (response.DataAsText);
        

        // re-login 체크 
        if (CheckCommonServerError(_resultForm)) {
            return;
        }

        GameSystem.Instance.SyncTimeData = _resultForm;

        // 게임 시작 
        WWWHelper.Instance.Post2("request_gamestart", OnFinishedGameStart);

    }



	private void OnFinishedGameStart(HTTPRequest request, HTTPResponse response) {
        if (!CheckRequestState(request))
            return;

        _resultForm = JSON.Parse (response.DataAsText);

        Debug.Log("▶▶ OnFinishedGameStart :: " + response.DataAsText);

        // re-login 체크 
        if (CheckCommonServerError(_resultForm)) {
            return;
        }

        // 씬 로딩 
        OnCompleteGameStart (_resultForm);

        // 게임 시작 횟수 증가 처리 
        SaveESvalueInt(PuzzleConstBox.ES_GameStartCount, LoadESvalueInt(PuzzleConstBox.ES_GameStartCount) + 1);

		// Time check
		LastActiveHour = DateTime.Now.Hour;
		LastActiveDay = DateTime.Now.DayOfYear;

	}

	/// <summary>
	/// GameStart
	/// </summary>
	/// <param name="pNode">P node.</param>
	private void OnCompleteGameStart(JSONNode pNode) {
		
		if (pNode ["result"].AsInt != 0) {
			
			OnOffWaitingRequestInLobby (false);
			
			if(pNode["error"].Value == "Heart is zero") {
                OpenMessageWithCallBack(PopMessageType.HeartZero);
			} else if(pNode["error"].Value == "item ads is not enable") {
				OpenMessage(PopMessageType.AdsNotEnable);
                GameSystem.Instance.UserDataJSON["data"]["itemads"].AsBool = false;
			} else {
				GameSystem.Instance.SetSystemMessage(pNode["error"].Value);
			}

            // SceneTip 비활성화
            // 다시 게임화면으로 돌아온다
            LobbyCtrl.Instance.CloseSceneTip();
            return;
		}

        // 레디창의 Starting을 설정
        if(ReadyGroupCtrl.Instance != null) {
            ReadyGroupCtrl.Instance.OnStartingGame = true;
        }

        BlockAttackPower = _userPowerLevel * 5; // 블록파워 
        IngameDiamondPlay = pNode[_jData]["diamondplay"].AsBool; // 다이아 레벨 플레이 여부 
	
		
		// 하트 처리 
		_heartCount = pNode[_jData]["value"].AsInt;
		
		// (자바스크립트 , C# Conver)
		_lastHeartTakeTime = ConvertServerTimeTick (pNode [_jData] ["lastTakeTime"].AsLong);
		_debugLastHeartTakeTime = _lastHeartTakeTime.ToString();
		
		// 다음 수령 시간 계산 
		_dtNextHeartTakeTime = new DateTime(_lastHeartTakeTime).AddMinutes(_addHeartTakeTime);
		
		
		
		
		_nextHeartTakeTime = _dtNextHeartTakeTime.Ticks;
		
		if (LobbyCtrl.Instance != null) {
			LobbyCtrl.Instance.UpdateHearts ();
		}


        // Coin 추가 처리 (Bingo) 게임 시작시 코인을 사용한 경우 
        if(pNode[_jData]["gold"].AsInt < UserGold) {
            CheckLobbyBingoQuest(50, UserGold - pNode[_jData]["gold"].AsInt, false);
        }
		
		
		// Adbrix
		for (int i=0; i<pNode[_jData]["useitem"].Count; i++) {
			
			if(i >= 4) 
				break;
			
			if(pNode[_jData]["useitem"][i].AsInt == 0) {
				AdbrixManager.Instance.SendAdbrixInAppActivity (AdbrixManager.Instance.ITEM_USE, AdbrixManager.Instance.ITEM1);
                //AdbrixManager.Instance.SendAppsFlyerEvent(AdbrixManager.Instance.AF_BUFF_MISS);
            } else if(pNode[_jData]["useitem"][i].AsInt == 1) {
				AdbrixManager.Instance.SendAdbrixInAppActivity (AdbrixManager.Instance.ITEM_USE, AdbrixManager.Instance.ITEM2);
                //AdbrixManager.Instance.SendAppsFlyerEvent(AdbrixManager.Instance.AF_BUFF_BOMB_GAUGE);
            } else if(pNode[_jData]["useitem"][i].AsInt == 2) {
				AdbrixManager.Instance.SendAdbrixInAppActivity (AdbrixManager.Instance.ITEM_USE, AdbrixManager.Instance.ITEM3);
                //AdbrixManager.Instance.SendAppsFlyerEvent(AdbrixManager.Instance.AF_BUFF_CRITICAL);
            } else if(pNode[_jData]["useitem"][i].AsInt == 3) {
				AdbrixManager.Instance.SendAdbrixInAppActivity (AdbrixManager.Instance.ITEM_USE, AdbrixManager.Instance.ITEM4);
                CheckMissionProgress(MissionType.Day, 1, 1);
            }
		}

        


        //DoGameStart();

    }

    /// <summary>
    ///  실제 인게임 시작 처리 
    /// </summary>
    public void DoGameStart() {
        // Adbrix 
        AdbrixManager.Instance.SendAdbrixInAppActivity(AdbrixManager.Instance.GAME_START);


        // New user Check
        if (TutorialComplete == 0) {
            AdbrixManager.Instance.SendAdbrixNewUserFunnel(AdbrixManager.Instance.NEW_GAME_START);
            
        }


        
        if(TutorialComplete > 0 && UserCurrentStage == 1) {
            AdbrixManager.Instance.SendAdbrixNewUserFunnel(AdbrixManager.Instance.FIRST_REAL_PUZZLE_START);
            
        }

        // 획득정보 초기화
        InitInGameAccquireInfo();
        SetEquipNekoGroup(); // 장착 네코의 그룹화 



        // 튜토리얼 처리 
        if (GameSystem.Instance.LocalTutorialStep <= 2) {

            Debug.Log("Load Tutorial");
            GameSystem.Instance.SaveLocalTutorialStep(3);
            //GameSystem.Instance.LoadTutorialScene();
            Fader.Instance.FadeIn(1f).LoadLevel("SceneInGame").FadeOut(1f);
            return;
        }

        // 일반 게임일때, Fade 좀 더 빨리.
        Fader.Instance.FadeIn(0.5f).LoadLevel("SceneInGame").FadeOut(1f);
    }

	#endregion 


    /// <summary>
    /// 10초 더 플레이!
    /// </summary>
    public void Post2MorePlay(Action<bool> pAction) {
        OnOffWaitingRequestInGame(true);
        OnCompleteMorePlay += pAction;
        WWWHelper.Instance.Post2("request_continue10sec", OnFinishedMorePlay);
    }


    private void OnFinishedMorePlay(HTTPRequest request, HTTPResponse response) {
        if (!CheckRequestState(request))
            return;

        OnOffWaitingRequestInGame(false);
        Debug.Log(">>>> OnFinishedMorePlay :: " + response.DataAsText);
        JSONNode result = JSON.Parse(response.DataAsText);

        // re-login 체크 
        if (CheckCommonServerError(result)) {
            return;
        }

        if (result["result"].AsInt != 0) {
            // 코드 발급이 불가 (이전에 이미 받았다.)
            OnCompleteMorePlay(false);
        }
        else {
            _ingameContinue10secUse = true; // 인게임에서 10초 더 사용.
            OnCompleteMorePlay(true);
            UserGem = result[_jData]["resultgem"].AsInt; // Gem 갱신처리 
        }

        OnCompleteMorePlay = delegate { };

    }




    /// <summary>
    /// Post2s the game result.
    /// </summary>
    public void Post2GameResult() {
		OnOffWaitingRequestInGame (true);
		WWWHelper.Instance.Post2 ("request_gameresult", OnFinishedGameResult);
	}

	/// <summary>
	/// Raises the complete game result event.
	/// </summary>
	/// <param name="pNode">P node.</param>
	private void OnCompleteGameResult(JSONNode pNode) {

		UserGold = pNode["gold"].AsInt;
		UserGem = pNode["gem"].AsInt;
		_userDataJSON[_jData]["highscore"].AsInt = pNode["highscore"].AsInt;
        UserBestScore = pNode["highscore"].AsInt;

        _userChub = pNode["chub"].AsInt;
		_userTuna = pNode["tuna"].AsInt;
		_userSalmon = pNode["salmon"].AsInt;

        #region 게임 결과 스테이지 처리 


        // 스테이지 클리어 등급이 더 높아졌을때만, 처리하도록 한다.
        UserStageJSON["laststage"].AsInt = pNode["laststage"].AsInt; // 마지막으로 플레이한 스테이지 
        UserStageJSON["currentstage"].AsInt = pNode["currentstage"].AsInt; // 현재 스테이지
        UserStageJSON["prestate"].AsInt = pNode["prestate"].AsInt; // 플레이한 스테이지의 이전 상태
        UserStageJSON["laststate"].AsInt = pNode["laststate"].AsInt;

        UserCurrentStage = UserStageJSON["currentstage"].AsInt;



        // 스테이지 달성도 체크 
        if (pNode["clear"].AsBool) {

            if (pNode["laststate"].AsInt <= UserStageJSON["stagelist"][pNode["laststage"].AsInt - 1]["state"].AsInt)
                InGameStageUp = false; // 달성도에 진척이 없음. 
            else
                InGameStageUp = true;
        }

        // 방금 플레이한 스테이지의 상태값 변경 


        #endregion


        //20151231 하트 체크 추가 
        PreHeartCount = _heartCount;
		_heartCount = pNode["heart"]["value"].AsInt;
		
		// (자바스크립트 , C# Conver)
		_lastHeartTakeTime = ConvertServerTimeTick(pNode["heart"]["lastTakeTime"].AsLong);
		_debugLastHeartTakeTime = _lastHeartTakeTime.ToString();
		
		// 다음 수령 시간 계산 
		_dtNextHeartTakeTime = new DateTime(_lastHeartTakeTime).AddMinutes(_addHeartTakeTime);
		_nextHeartTakeTime = _dtNextHeartTakeTime.Ticks;
		


        // Adbrix 
        AdbrixManager.Instance.SendAdbrixInAppActivity(AdbrixManager.Instance.GAME_CLEAR); // 게임종료 

        // Mission Progress
        CheckMissionProgress(MissionType.Day, 12, 1); // 퍼즐 수행
        CheckMissionProgress(MissionType.Week, 12, 1); // 퍼즐 수행
        CheckMissionProgress(MissionType.Day, 2, GameSystem.Instance.InGameTotalCoin); // 코인 획득
        //CheckMissionProgress(MissionType.Day, 3, GameSystem.Instance.InGameTotalScore); // 스코어 획득
        CheckMissionProgress(MissionType.Week, 2, GameSystem.Instance.InGameTotalCoin); // 코인 획득
        //CheckMissionProgress(MissionType.Week, 3, GameSystem.Instance.InGameTotalScore); // 스코어 획득

        CheckMissionProgress(MissionType.Day, 4, GameSystem.Instance.InGameTotalCombo); // 콤보
        CheckMissionProgress(MissionType.Week, 4, GameSystem.Instance.InGameTotalCombo); // 콤보

        //CheckMissionProgress(MissionType.Week, 10, GameSystem.Instance.ListIngameWinNekoInfo.Count); // 스테이지 클리어 

        OnFinishedGameResultEventHandle();



        // 마지막 비활성화 시간, 혹은 게임 시작 시간과 일자를 체크하여 Session 갱신처리
        if (LastActiveDay != 0 && LastActiveDay != DateTime.Now.DayOfYear) {
            Post2UserData();
        }
        else if (LastActiveHour != 0 && LastActiveHour < 9 && DateTime.Now.Hour >= 9) {
            Post2UserData();
        }
        else if (LastActiveHour != 0 && LastActiveHour < 21 && DateTime.Now.Hour >= 21) {
            Post2UserData();
        }
    }

    /// <summary>
    /// 마지막으로 플레이했던 스테이지의 상태값을 업데이트
    /// 스테이지 마스터에서 호출
    /// </summary>
    public void UpdateLastPlayStage() {
        Debug.Log("★ UpdateLastPlayStage Called :: " + InGameStageUp);

        // 등급이 올랐을때만 갱신처리.
        if (InGameStageUp)
            UserStageJSON["stagelist"][IngameResultData["laststage"].AsInt - 1]["state"].AsInt = IngameResultData["laststate"].AsInt;
    }

    /// <summary>
    /// Raises the finished game result event.
    /// </summary>
    /// <param name="request">Request.</param>
    /// <param name="response">Response.</param>
    private void OnFinishedGameResult(HTTPRequest request, HTTPResponse response) {

        if (!CheckRequestState(request))
            return;

        Debug.Log (">>> OnFinishedGameResult :: " + response.DataAsText);

		IngameResultData = JSON.Parse (response.DataAsText);

        // re-login 체크 
        if (CheckCommonServerError(IngameResultData)) {
            return;
        }

        IngameResultData = IngameResultData[_jData];

        OnCompleteGameResult (IngameResultData);

        // 게임 결과 창 오픈 
        //Fader.Instance.FadeIn (0.5f).LoadLevel ("SceneResult").FadeOut (1f);
        GameSystem.Instance.IsOnGameResult = true; // 로비에서 결과창을 띄운다.
        Fader.Instance.FadeIn(0.5f).LoadLevel("SceneLobby").FadeOut(1f);

        OnOffWaitingRequestInGame (false);
	}


    /// <summary>
    /// 이벤트 관련 추가 로직 
    /// </summary>
    private void OnFinishedGameResultEventHandle() {


        if (!UserJSON["trespassuse"].AsBool) {
            return;
        }

        //int watermelonCatch = 0;
        //int sunburnCatch = 0;



        /*
        for (int i =0; i<_listIngameWinNekoInfo.Count;i++) {
            if (_listIngameWinNekoInfo[i] == 210)
                watermelonCatch++;
            else if (_listIngameWinNekoInfo[i] == 212)
                sunburnCatch++;
        }


        Debug.Log("★★★OnFinishedGameResultEventHandle Drum / Harp :: " + watermelonCatch + " / " + sunburnCatch);

        // 저장 
        SaveESvalueInt(PuzzleConstBox.ES_EventWaterMelonCatch, LoadESvalueInt(PuzzleConstBox.ES_EventWaterMelonCatch) + watermelonCatch);
        SaveESvalueInt(PuzzleConstBox.ES_EventSunBurnCatch, LoadESvalueInt(PuzzleConstBox.ES_EventSunBurnCatch) + sunburnCatch);
        */
    }

	#endregion


	

	private void OnCompleteNekoReward2nd(JSONNode pNode) {

		Debug.Log (">>>>>>>> OnCompleteNekoReward2nd ");

		

		// adspoint 처리
		if (pNode [_jData] ["resultadspoint"].AsInt > 0) {
			_adsPoint = pNode [_jData] ["resultadspoint"].AsInt;
			UpdateTopInfomation();
		}

		if (pNode [_jData] ["addchub"].AsInt > 0) {
			_userChub += pNode [_jData] ["addchub"].AsInt;
			LobbyCtrl.Instance.OpenInfoPopUp(PopMessageType.ChubAdd, pNode [_jData] ["addchub"].AsInt.ToString());
		}


		/*
		if (pNode [_jData] ["resultchub"].AsInt > 0) {
			gap = pNode [_jData] ["resultchub"].AsInt - _userChub;
			_userChub = pNode [_jData] ["resultchub"].AsInt;

			LobbyCtrl.Instance.OpenConfirmPopup(PopMessageType.ChubAdd, gap.ToString());
			return;
		} else if (pNode [_jData] ["resulttuna"].AsInt > 0) {
			gap = pNode [_jData] ["resulttuna"].AsInt - _userTuna;
			_userTuna = pNode [_jData] ["resulttuna"].AsInt;
			
			LobbyCtrl.Instance.OpenConfirmPopup(PopMessageType.TunaAdd, gap.ToString());
			return;
		} else if (pNode [_jData] ["resultsalmon"].AsInt > 0) {
			gap = pNode [_jData] ["resultsalmon"].AsInt - _userSalmon;
			_userSalmon = pNode [_jData] ["resultsalmon"].AsInt;
			
			LobbyCtrl.Instance.OpenConfirmPopup(PopMessageType.SalmonAdd, gap.ToString());
			return;
		}
		*/
	}




	/// <summary>
	/// Raises the request view ads event.
	/// </summary>
	/// <param name="pNode">P node.</param>
	private void OnRequestViewAds() {
		
		if (_viewAdsJSON [_jData] ["rewardtype"].Value == "heart") {
			// 새로운 메일이 있다고 체크 
			_hasNewMail = true;
			LobbyCtrl.Instance.OpenInfoPopUp(PopMessageType.HeartAdd, delegate { });
			
		} else if (_viewAdsJSON [_jData] ["rewardtype"].Value == "gem") {
			UserGem = _viewAdsJSON [_jData] ["gem"].AsInt;
			
			// "value" 값 만큼이 증가되었다고 알린다. 
			LobbyCtrl.Instance.OpenInfoPopUp(PopMessageType.GemAdd, _viewAdsJSON[_jData]["value"].AsInt.ToString());
		} else if (_viewAdsJSON [_jData] ["rewardtype"].Value == "gold") {
			UserGold = _viewAdsJSON [_jData] ["gold"].AsInt;
			LobbyCtrl.Instance.OpenInfoPopUp(PopMessageType.GoldAdd, _viewAdsJSON[_jData]["value"].AsInt.ToString());
		}
		UpdateTopInfomation ();
	}






	
	/// <summary>
	/// 하트 refresh 용도 
	/// </summary>
	/// <param name="pNode">P node.</param>
	private void RefreshHeart(JSONNode pNode) {
		
		if(pNode[_jData]["heart"]["value"].AsInt >= 0) {
			_heartCount = pNode[_jData]["heart"]["value"].AsInt;
			
			// (자바스크립트 , C# Conver)
			_lastHeartTakeTime =  ConvertServerTimeTick( pNode[_jData]["heart"]["lastTakeTime"].AsLong);

			
			// 다음 수령 시간 계산 
			_dtNextHeartTakeTime = new DateTime(_lastHeartTakeTime).AddMinutes(_addHeartTakeTime);
			
			
			_nextHeartTakeTime = _dtNextHeartTakeTime.Ticks;
			
		} 
		
		// 업데이트 
		UpdateTopInfomation();
	}
	
	

	
	
	
	/// <summary>
	/// Take heart OnComplete
	/// </summary>
	private void OnCompleteRequestTakeHeart(JSONNode pNode) {
		
		if(pNode["result"].AsInt == 1 && pNode[_jData]["curheart"]["value"].AsInt == 5)  {
			GameSystem.Instance.HeartCount = 5;
		} else if (pNode["result"].AsInt == 0) {
			
			_heartCount = pNode[_jData]["value"].AsInt;
			
			// (자바스크립트 , C# Conver)
			_lastHeartTakeTime = ConvertServerTimeTick(pNode[_jData]["lastTakeTime"].AsLong);
			_debugLastHeartTakeTime = _lastHeartTakeTime.ToString();
			
			// 다음 수령 시간 계산 
			_dtNextHeartTakeTime = new DateTime(_lastHeartTakeTime).AddMinutes(_addHeartTakeTime);
			


			
			_nextHeartTakeTime = _dtNextHeartTakeTime.Ticks;
		}
		
		if (LobbyCtrl.Instance != null) {
			LobbyCtrl.Instance.UpdateHearts ();
		}
	}

	/// <summary>
	/// 서버 통신 없이 하트 증가 처리 
	/// </summary>
	private void OnAddHeart() {
		_heartCount++;

		_dtNextHeartTakeTime = _dtNextHeartTakeTime.AddMinutes (_addHeartTakeTime);
		_nextHeartTakeTime = _dtNextHeartTakeTime.Ticks;


		if (LobbyCtrl.Instance != null) {
			LobbyCtrl.Instance.UpdateHearts ();
		} 
	}


	private void OpenMessage(PopMessageType pType) {
		if (LobbyCtrl.Instance != null) {
			LobbyCtrl.Instance.OpenInfoPopUp(pType);
		} 
	}

    private void OpenMessageWithCallBack(PopMessageType pType) {


        if (LobbyCtrl.Instance != null) {
            LobbyCtrl.Instance.OpenInfoPopUp(pType);
        }
        
    }



	/// <summary>
	/// 유저 정보 세팅 
	/// </summary>
	public void SetUserInfo() {
		
		Debug.Log(">>>> SetUserInfo");

        UserName = _userDataJSON[_jData]["nickname"].Value;
        UserPowerLevel = _userDataJSON[_jData]["powerlevel"].AsInt;

        // admin 처리
        if(_userDataJSON[_jData]["admin"].AsInt == 1) {
            IsAdminUser = true;
        }
        else {
            IsAdminUser = false;
        }
		
		// 레벨 정보 세팅

        UserBestScore = _userDataJSON[_jData]["highscore"].AsInt;
        _tutorialComplete = _userDataJSON[_jData]["tutorialcomplete"].AsInt;

        Remainfreegacha = _userDataJSON[_jData]["remaingachaads"].AsInt;
        Remainstartfever = _userDataJSON[_jData]["remainitemads"].AsInt;

        //tutorialcomplete
        if (_userDataJSON[_jData]["tutorialcomplete"].AsInt == 0) {
            TutorialStage = 0; //  튜토리얼 대상자
            _tutorialComplete = 0; // 튜토리얼 컴플릿트 변수 설정 
        }
		
		/// 유저 고양이의 보은 시간 
		_userNekorewardtime = ConvertServerTimeTick(_userDataJSON[_jData]["nekorewardtime"].AsLong);
		_dtNextNekoRewardTime = new System.DateTime(_userNekorewardtime); // DateTime으로 변환 

        // WakeUp 시간
        _remainWakeUpTimeTick = ConvertServerTimeTick(_userDataJSON[_jData]["wakeuptime"].AsLong);
        _dtRemainWakeUpTime = new DateTime(_remainWakeUpTimeTick).AddMinutes(30); // 30분 추가 

        //Debug.Log("▶▶ DtSyncTime #1 :: " + DtSyncTime.Month + " / " + DtSyncTime.Day + " : " + DtSyncTime.Hour + " : " + DtSyncTime.Minute);
        //Debug.Log("★★ WakeUP Time :: " + _dtRemainWakeUpTime.Month + " / " + _dtRemainWakeUpTime.Day + " : " + _dtRemainWakeUpTime.Hour + " : " + _dtRemainWakeUpTime.Minute);


        #region 난입 이벤트 정보 저장

        SaveESvalueInt(PuzzleConstBox.ES_EventSunBurnStep, _userDataJSON[_jData]["sunburnstep"].AsInt);
        SaveESvalueInt(PuzzleConstBox.ES_EventWaterMelonStep, _userDataJSON[_jData]["watermelonstep"].AsInt);

        if(_userDataJSON[_jData]["trespassuse"].AsBool) {
            _trespassRewardJSON = _userDataJSON[_jData]["trespass"];
        }


        #endregion


        #region Unlock 정보 저장 

        if (_userDataJSON[_jData]["mission_unlock"].AsInt == 0) 
            SaveESvalueBool(PuzzleConstBox.ES_UnlockMission, true);
        else
            SaveESvalueBool(PuzzleConstBox.ES_UnlockMission, false);

        if (_userDataJSON[_jData]["item_unlock"].AsInt == 0)
            SaveESvalueBool(PuzzleConstBox.ES_UnlockItem, true);
        else
            SaveESvalueBool(PuzzleConstBox.ES_UnlockItem, false);

        if (_userDataJSON[_jData]["passive_unlock"].AsInt == 0)
            SaveESvalueBool(PuzzleConstBox.ES_UnlockPassive, true);
        else
            SaveESvalueBool(PuzzleConstBox.ES_UnlockPassive, false);

        if (_userDataJSON[_jData]["wanted_unlock"].AsInt == 0)
            SaveESvalueBool(PuzzleConstBox.ES_UnlockWanted, true);
        else
            SaveESvalueBool(PuzzleConstBox.ES_UnlockWanted, false);

        if (_userDataJSON[_jData]["ranking_unlock"].AsInt == 0)
            SaveESvalueBool(PuzzleConstBox.ES_UnlockRanking, true);
        else
            SaveESvalueBool(PuzzleConstBox.ES_UnlockRanking, false);

        if (_userDataJSON[_jData]["nekoservice_tip"].AsInt == 0)
            SaveESvalueBool(PuzzleConstBox.ES_UnlockNekoService, true);
        else
            SaveESvalueBool(PuzzleConstBox.ES_UnlockNekoService, false);

        if (_userDataJSON[_jData]["nekolevelup_tip"].AsInt == 0)
            SaveESvalueBool(PuzzleConstBox.ES_UnlockNekoLevelup, true);
        else
            SaveESvalueBool(PuzzleConstBox.ES_UnlockNekoLevelup, false);

        if (_userDataJSON[_jData]["bingo_tip"].AsInt == 0)
            SaveESvalueBool(PuzzleConstBox.ES_UnlockBingoTip, true);
        else
            SaveESvalueBool(PuzzleConstBox.ES_UnlockBingoTip, false);

        #endregion


        #region 스테이지 정보 저장 

        UserStageJSON = _userDataJSON[_jData]["stage"]; // 스테이지 진행도 
        UserThemeJSON = _userDataJSON[_jData]["theme"]; // 테마 진행도 

        UserCurrentStage = UserStageJSON["currentstage"].AsInt; 
        
        #endregion

    }


    /// <summary>
    /// 재화정보 저장 
    /// </summary>
    private void SetBankInfo() {
		
		Debug.Log(">>>> SetBankInfo");
		
		UserGold = _bankData[_jData]["gold"].AsInt;
		UserGem = _bankData[_jData]["gem"].AsInt;
		_userChub = _bankData[_jData]["chub"].AsInt;
		_userTuna = _bankData[_jData]["tuna"].AsInt;
		_userSalmon = _bankData[_jData]["salmon"].AsInt;
		AdsPoint = _bankData[_jData]["adspoint"].AsInt;
		
		// 하트 refresh 
		RefreshHeart(_bankData);
		
	}
	
    /// <summary>
    /// 사용자 네코 정보 리스트 
    /// </summary>
	private void SetUserNekoInfo() {
        // List 구축 (정렬 용도)
        _listUserNeko.Clear();
        for (int i = 0; i < UserNeko.Count; i++) {
            _listUserNeko.Add(UserNeko[i]);
        }

        // 자동 메인네코 설정 처리 
        if (_listUserNeko.Count == 1 && UserDataJSON[_jData]["mainneko"].AsInt < 0) {
            // 메인 네코 설정 
            GameSystem.Instance.UpgradeNekoDBKey = UserNeko[0]["dbkey"].AsInt;
            Post2MainNeko();
        }
    }

	/// <summary>
	/// 싱크 타임 세팅 
	/// </summary>
	private void SetSyncTime() {
		
		Debug.Log(">>>> SetSyncTime");
		
		// 전달받은 서버시간을 C#에 맞게 변경처리
		_syncTime = _originalSyncTime;
		_syncTime = ConvertServerTimeTick(_syncTimeData[_jData].AsLong);
		
		DtSyncTime = new DateTime(_syncTime);
		_resetSendHeartTime = new DateTime(_syncTime);
		
		Debug.Log("▶▶ DtSyncTime #1 :: " + DtSyncTime.Month + " / " + DtSyncTime.Day + " : " + DtSyncTime.Hour + " : " + DtSyncTime.Minute);
        Debug.Log("▶▶ DateTime.Now #1 :: " + DateTime.Now.Month + " / " + DateTime.Now.Day + " : " + DateTime.Now.Hour + " : " + DateTime.Now.Minute);

        // 00시로 만들어버리고 하루를 더한다.
        _resetSendHeartTime = _resetSendHeartTime.AddHours(_resetSendHeartTime.Hour * -1);
		_resetSendHeartTime = _resetSendHeartTime.AddMinutes(_resetSendHeartTime.Minute * -1);
		_resetSendHeartTime = _resetSendHeartTime.AddDays(1);
		
		//Debug.Log("▶▶ _resetSendHeartTime #2 :: " + _resetSendHeartTime.Month + " / " + _resetSendHeartTime.Day + " : " + _resetSendHeartTime.Hour + " : " + _resetSendHeartTime.Minute);
		
		// 만들어진 리셋 시간을 저장. 
		SaveNextResetTime();
		
		_debugSyncTime = _syncTime.ToString(); // 이건 디버그용도 
		
		
		// 서버시간과 클라이언트 시간과의 차이를 구한다. 
		_nowTimeTick = DateTime.Now.Ticks;
		
		// 서버시간 - 클라시간으로 시간 갭을 구해서, 앞으로의 모든 시간계산 (DateTime.Now.Ticks + timeGap) 에 이용한다. 
		_timeGapBetweenServerClient = _syncTime - _nowTimeTick;
        //Debug.Log("_timeGapBetweenServerClient :: " + _timeGapBetweenServerClient);

        Debug.Log("▶▶ Adjust TimeGap #1 :: " + DateTime.Now.AddTicks(_timeGapBetweenServerClient).Month + " / " + DateTime.Now.AddTicks(_timeGapBetweenServerClient).Day + " : " + DateTime.Now.AddTicks(_timeGapBetweenServerClient).Hour + " : " + DateTime.Now.AddTicks(_timeGapBetweenServerClient).Minute);


    }

	private void SetAttendance() {
		Debug.Log(">>>> SetAttendance");
		
		if(_attendanceJSON["result"].AsInt == 0) {
			
		}
	}
	
	/// <summary>
	/// 자바스크립트의 타임 Tick 을 C#틱으로 변환 
	/// </summary>
	/// <returns>The server time tick.</returns>
	/// <param name="pServerTick">P server tick.</param>
	public long ConvertServerTimeTick(long pServerTick) {

		_calcLocaleTime = new DateTime ((pServerTick * 10000) + _fixAddTick);
		_calcLocaleTime = _calcLocaleTime.ToLocalTime ();

		return _calcLocaleTime.Ticks;

	}




    /// <summary>
    /// 서버 공통 오류 코드 처리 
    /// </summary>
    /// <param name="pNode"></param>
    /// <returns></returns>
    private bool CheckCommonServerError(JSONNode pNode) {

        // Hot Time 체크 로직
        IsHotTime = pNode[PuzzleConstBox.packet_hottime].AsBool;
        if(LobbyCtrl.Instance != null) {
            LobbyCtrl.Instance.SetHotTimeMark(IsHotTime);

        }

        // 서버 점검 
        if (pNode.ToString().Contains("server inspection")) {
            _resultForm = pNode; // 서버시간을 체크하기 위해 노드 할당.

            OnOffWaitingRequestInLobby(false);
            SetSystemMessage("server inspection");

            return true; // 다음 진행하지 않음 
        }

        if (pNode.ToString().Contains("re-login")) {

            OnOffWaitingRequestInLobby(false);

            if (LobbyCtrl.Instance != null) {

                LobbyCtrl.Instance.OpenInfoPopUp(PopMessageType.ReLogin);
                
            }
            else {
                SetSystemMessage("re-login");
            }

            return true;
        }

        if(pNode.ToString().Contains("invalid version")) {


            Debug.Log("invalid version!!");

            OnOffWaitingRequestInLobby(false);
            SetSystemMessage("invalid version");

            return true;
        }


        return false;
    }



	#region Properties 


	public JSONNode BankData {
		get {
			return _bankData;
		}
		set {
			_bankData = value;
			SetBankInfo();
		}
	}

	public JSONNode UserDataJSON {
		get {
			return this._userDataJSON;
		}
		set {
			this._userDataJSON = value;
			SetUserInfo(); // User Info 세팅 
		}
	}



	public JSONNode RankRewardJSON {
		get {
			return this._rankRewardJSON;
		}
	}

	public JSONNode AttendanceJSON {
		get {
			return this._attendanceJSON;
		}
		set {
			this._attendanceJSON = value;
			SetAttendance();
		}
	}

	public bool IsFreeGatcha {
		get {
			return this._isFreeGatcha;
		}
		set {
			this._isFreeGatcha = value;
		}
	}


	public DateTime DtSyncTime {
		get {
			return this._dtSyncTime;
		}
		set {
			this._dtSyncTime = value;
		}
	}

	public JSONNode PreFusionNekoJSON {
		get {
			return this._preFusionNekoJSON;
		}
	}

	public List<JSONNode> ListSortUserNeko {
		get {
			return this._listUserNeko;
		}
	}

	public int PreFusionNekoStar {
		get {
			return this._preFusionNekoStar;
		}
	}

	public int PreFusionNekoBead {
		get {
			return this._preFusionNekoBead;
		}
	}

	

	public JSONNode FishGachaJSON {
		get {
			return this._fishGachaJSON;
		}
	}



	public long OriginalSyncTime {
		get {
			return this._originalSyncTime;
		}
	}

	public JSONNode SyncTimeData
	{
		get
		{
			return _syncTimeData;
		}
		
		set
		{
			_syncTimeData = value;
			SetSyncTime();
		}
	}

    public JSONNode AttendanceInitJSON {
        get {
            return _attendanceInitJSON;
        }

        set {
            _attendanceInitJSON = value;
        }
    }

    public int PreHeartCount {
        get {
            return _preHeartCount;
        }

        set {
            _preHeartCount = value;
        }
    }

    public bool IsRequesting {
        get {
            return _isRequesting;
        }

        set {
            _isRequesting = value;
        }
    }

    public JSONNode GameVesionJSON {
        get {
            return _gameVesionJSON;
        }

        set {
            _gameVesionJSON = value;
        }
    }

    public JSONNode GatchaBannerInitJSON {
        get {
            return _gatchaBannerInitJSON;
        }

        set {
            _gatchaBannerInitJSON = value;
            
        }
    }

    public JSONNode PackageBannerInitJSON {
        get {
            return _packageBannerInitJSON;
        }

        set {
            _packageBannerInitJSON = value;
            
        }
    }

    public JSONNode NoticeBannerInitJSON {
        get {
            return _noticeBannerInitJSON;
        }

        set {
            _noticeBannerInitJSON = value;
            
        }
    }

    public JSONNode RankRewardInitJSON {
        get {
            return _rankRewardInitJSON;
        }

        set {
            _rankRewardInitJSON = value;
        }
    }

    public JSONNode EnvInitJSON {
        get {
            return _envInitJSON;
        }

        set {
            _envInitJSON = value;


        }
    }

    public JSONNode CoinShopInitJSON {
        get {
            return _coinShopInitJSON;
        }

        set {
            _coinShopInitJSON = value;
        }
    }

    public JSONNode UserJSON {
        get {
            return _userJSON;
        }

        set {
            _userJSON = value;
        }
    }

    public JSONNode IngameResultData {
        get {
            return _ingameResultData;
        }

        set {
            _ingameResultData = value;
        }
    }

    public JSONNode FbLinkJSON {
        get {
            return _fbLinkJSON;
        }

        set {
            _fbLinkJSON = value;
        }
    }

    public JSONNode TrespassRewardJSON {
        get {
            return _trespassRewardJSON;
        }

        set {
            _trespassRewardJSON = value;
        }
    }

    public JSONNode UserPassivePriceJSON {
        get {
            return _userPassivePriceJSON;
        }

        set {
            _userPassivePriceJSON = value;
        }
    }

    public MailColumnCtrl ReadMailColumn {
        get {
            return _readMailColumn;
        }

        set {
            _readMailColumn = value;
        }
    }

    public JSONNode UserMissionJSON {
        get {
            return _userMissionJSON;
        }

        set {
            _userMissionJSON = value;
        }
    }

    public JSONNode StageDetailJSON {
        get {
            return _stageDetailJSON;
        }

        set {
            _stageDetailJSON = value;
        }
    }

    public JSONNode StageMasterJSON {
        get {
            return _stageMasterJSON;
        }

        set {
            _stageMasterJSON = value;
        }
    }

    public JSONNode UserStageJSON {
        get {
            return _userStageJSON;
        }

        set {
            _userStageJSON = value;
        }
    }

    public JSONNode UserNeko {
        get {
            return UserNekoJSON;
        }

        set {
            UserNekoJSON = value;
            _debugUserNekoData = UserNekoJSON.ToString();
            SetUserNekoInfo();
        }
    }

    public JSONNode UserNekoJSON {
        get {
            return _userNekoJSON;
        }

        set {
            _userNekoJSON = value;
        }
    }

    public JSONNode RescueHistory {
        get {
            return _rescueHistory;
        }

        set {
            _rescueHistory = value;
        }
    }

    public JSONNode UserThemeJSON {
        get {
            return _userThemeJSON;
        }

        set {
            _userThemeJSON = value;
        }
    }




    #endregion



}
