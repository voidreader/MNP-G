using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using PathologicalGames;
using UnityEngine.Advertisements;
using DG.Tweening;
using SimpleJSON;
using Google2u;


public partial class LobbyCtrl : MonoBehaviour {

    static LobbyCtrl _instance = null;
    

    #region Action

    // 로비 로딩 완료
    public static event Action OnCompleteInitLobby = delegate { };

    #endregion

    [SerializeField] Camera _mainCamera;
    [SerializeField] private bool _onResultShow = false;
    [SerializeField] bool _isWaitingCheck = false;


    private Ray ray;
    private RaycastHit hit;
    private LayerMask mask;


    UIButton[] _arrButtons;
    [SerializeField] NekoAppearCtrl _gatchaNekoAppear;
    [SerializeField] GSceneTipCtrl _sceneTip;


    // 현재 팝업된 화면 
    // 팝업이 발생할때마다, 갱신되며 뒤로가기 버튼을 통해 사라진다. 
    [SerializeField] Stack<LobbyCommonUICtrl> _stackPopup = new Stack<LobbyCommonUICtrl>();


    
    private bool _isAdsNekoGift = false;
    private bool isTouchLock = false;

    #region Panel, 팝업창 모음 
    [SerializeField] GameObject topPanel;
    
    [SerializeField] GameObject LobbyPanel;
    [SerializeField] GameObject StagePanel;

    


    [SerializeField] GameObject objResultForm; 

    // 준비화면 
    [SerializeField]
    private ReadyGroupCtrl objReadyGroup; // Ready Group
    [SerializeField]
    private GameObject objPopupUpgrade; // 업그레이드 팝업창 

    [SerializeField]
    GameObject _objSimplePopup; // 공용 메세지 팝업 
    [SerializeField]
    SimplePopupCtrl _objUpperPopup; // 상위 레이어 공용 메세지 팝업 

    #endregion

    [SerializeField] NekoTicketWindowCtrl _nekoticketList;
    

    [SerializeField] GameObject gatchaResultSet;
    [SerializeField] GameObject gatchaNekoSet;
    [SerializeField] Transform[] arrGatchaNekoAppear;


    // 객체 모음
    
    [SerializeField] UISprite spNekoRewardTop; // 고양이의 보은 선물상자 (Top)
    

    [SerializeField] BigPopupCtrl bigPopup; // 빅사이즈 팝업 컨트롤


    #region 상단 객체 
    
    CoinBarCtrl _coinBar;
    GemBarCtrl _gemBar;
    HeartBarCtrl _heartBar;
    //AdsPointBarCtrl _adsPointBar;

    #endregion

    GameObject _lobbyLowerButtons; // 로비 하단 버튼들 
    [SerializeField] TalkativeCatCtrl _talkingCat;
    [SerializeField] NewNekoEventCtrl _newNekoEventButton;
    [SerializeField] FreeCraneIconCtrl _freeCraneIconButton;

    [SerializeField] GameObject objWaitingRequest = null;

    // 핫타임 마크 
    [SerializeField] GameObject _objHotTimeMark = null;



    [SerializeField] NekoEvolutionCtrl objNekoEvolution = null;
    [SerializeField] GameObject objFishFeed = null;
    [SerializeField] GameObject objNekoLevelup = null;
    [SerializeField] GameObject objNekoLevelUpConfirmWindow = null;

    [SerializeField] private GameObject objSelectNeko = null;
        


    [SerializeField] GameObject _objBingoFocusMark; // 빙고 포커스 마크 
    [SerializeField] GameObject _bingoMaster;

    [SerializeField]
    GatchaConfirmCtrl _gatchaConfirmCtrl;


    Vector3 _nekoSelectScrollViewPos = new Vector3(-220, -90, 0);

    [SerializeField] NekoBonusInfoCtrl _nekoBonusInfo;

    #region Unlock 관련 오브젝트 

    [SerializeField] SparkLightCtrl _lockSpark;
    [SerializeField] UIButton _btnMission;
    [SerializeField] UIButton _btnWanted ;
    [SerializeField] UIButton _btnBingo;

    [SerializeField] GTipCtrl _gameTip;  // 게임 팁

    bool _isUnlockingBingo = false;

    #endregion

    #region Notice Declare

    List<NoticeSmallBannerCtrl> _listNoticeSmallBanner = new List<NoticeSmallBannerCtrl>();

    #endregion

    #region 캐릭터 뽑기 오브젝트

    [SerializeField] GameObject _objGatchaScreen;
    [SerializeField] GameObject _gatchaBargainMark;

    #endregion

    #region 스크린 캡쳐 관련 변수 

    private byte[] _imageByte; // 스크린샷을 Byte로 저장 
    private Texture2D _tex; // 텍스쳐 


    #endregion

    #region 튜토리얼 관련 변수

    [SerializeField] GameObject _explain; // 설명 객체 
    [SerializeField] TutorialHandCtrl _tutorialHand; // 설명 손 
    [SerializeField] bool _isWaitingTab = false;
    private bool _clearTutorialEvent = false; // 튜토리얼 클리어 이벤트 완료시 호출 

    #endregion

    public static LobbyCtrl Instance {
        get {
            if (_instance == null) {
                _instance = FindObjectOfType(typeof(LobbyCtrl)) as LobbyCtrl;

                if (_instance == null) {
                   // Debug.Log("LobbyCtrl Init Error");
                    return null;
                }
            }

            return _instance;
        }

    }

    #region 1. 초기화 (Awake, Start)

    /// <summary>
    /// 로비 UI 객체 할당 처리 
    /// </summary>
    private void SetLobbyUI() {
        
        _coinBar = GameObject.FindGameObjectWithTag("CoinBar").GetComponent<CoinBarCtrl>();
        _gemBar = GameObject.FindGameObjectWithTag("GemBar").GetComponent<GemBarCtrl>();
        _heartBar = GameObject.FindGameObjectWithTag("HeartBar").GetComponent<HeartBarCtrl>();
        //_adsPointBar = GameObject.FindGameObjectWithTag("AdsBar").GetComponent<AdsPointBarCtrl>();

        _lobbyLowerButtons = GameObject.FindGameObjectWithTag("RollupButton");

        spNekoRewardTop.GetComponent<UIButton>().enabled = false;

    }

    void Awake() {

        ScissorCtrl.Instance.UpdateResolution();

        SetLobbyUI();
    }


    // Use this for initialization
    void Start() {

        //GoogleAdmobMgr.Instance.BottomBannerView.Show();
        GoogleAdmobMgr.Instance.RequestBanner(); // 애드몹 배너요청 

        // 빌링 초기화 
        GameSystem.Instance.InitBilling();
        GameSystem.Instance.SetSoundVolumn(); // 사운드 볼륨 제어 
        GameSystem.Instance.LoadPushOption();

        

        if (GameSystem.Instance.IsOnGameResult) { // 게임 결과 오픈 


            OnResultShow = true;
            UpdateTopInformation();

            GameResultManager.Instance.BeginShowingResult();
        } else { // 로비 오픈 
            InitializeLobby();
        }


    }


    /// <summary>
    /// 로비 초기화 
    /// </summary>
    public void InitializeLobby(bool pPassLogic = false) {


        OnResultShow = false;

        // 할인 정보 체크 
        CheckGatchaBargain();

        // Lock 정보 세팅 
        SetLockState();



        // 스테이지 초기화
        StageMasterCtrl.Instance.InitializeStageMaster();



        // 고양이의 보은 오브젝트 초기화
        InitNekoRewardObjects();


        _talkingCat.Talk();
        NewNekoEventButton.Change();
        //_freeCraneIconButton.Play();

        // 아이템 클리어 
        GameSystem.Instance.ListEquipItemID.Clear();

        // 상단 정보 갱신
        UpdateTopInformation();

        // PoolManager 관련 
        SetSpawningObject();

        // 하트 시간 체크 
        UpdateHeartTime();


        // 튜토리얼 진행 
        if (GameSystem.Instance.TutorialStage == 0 || GameSystem.Instance.TutorialStage == 10) {
            // 튜토리얼 
            CheckLobbyTutorial();
            return;
        }


        bgmSrc.Play();



        GameSystem.Instance.Post2CheckNewMail(); // 메일 조회 
        GameSystem.Instance.Post2RequestAdsRemainSimple(); // 광고 회수 리로드
        GameSystem.Instance.CheckMissionDay();

        UpdateMissionNew(); // 미션 

        //CheckBingoFocusMark(); // 빙고 강조 표시체크

		CheckNewClearBingo(); // New Bingo Check

        // 불꽃놀이 
        //StartCoroutine(Fireworking());

        // 후반부는 패스 한다. true 일경우.
        if (pPassLogic)
            return;

        // 레벨 10 달성 보상, 페이스북 연동 보상 처리 
        if (!string.IsNullOrEmpty(GameSystem.Instance.FacebookID) && !GameSystem.Instance.UserJSON["facebooklinkget"].AsBool) {

            _isWaitingCheck = true;
            GameSystem.Instance.Post2UserEventReward(OpenUserEventReward);
        }

        // 후반부 체크 시작 
        StartCoroutine(CheckingFirstLobbyEnter());
        
    }



    private void CheckBingoFocusMark() {
        // 빙고 팁의 해제 여부 확인 
        if (!GameSystem.Instance.LoadESvalueBool(PuzzleConstBox.ES_UnlockBingoTip)) {
            // 해제되지 않았으면 강조표시를 하지 않는다.
            _objBingoFocusMark.SetActive(false);
        }
        else { // 팁을 해제했을때, 도전중인 빙고 여부에 따라서 강조 표시 

            if (GameSystem.Instance.UserJSON["currentbingoid"].AsInt >= 0) {
                _objBingoFocusMark.SetActive(false);
            }
            else { // 팁을 해제했으나, 도전중인 빙고를 선택하지 않음 
                _objBingoFocusMark.SetActive(true);
            }
        }
    }


    #region 유저 특정 조건 달성시 팝업 오픈 
    /// <summary>
    /// 유저 특정 조건 달성시 팝업 오픈 
    /// </summary>
    private void OpenUserEventReward(SimpleJSON.JSONNode pNode) {
        StartCoroutine(OpeningUserEventReward(pNode));
    }

    IEnumerator OpeningUserEventReward(SimpleJSON.JSONNode pNode) {
        JSONNode rewards = pNode["data"];

        yield return new WaitForSeconds(0.1f);

        while(_objSimplePopup.activeSelf) {
            yield return new WaitForSeconds(0.1f);
        }

        // 페이스북 연동 
        if (rewards["getfacebooklink"].AsBool) {
            JSONNode rewardNode;

            for (int i = 0; i < rewards["rewardlist"].Count; i++) {
                if ("getfacebooklink".Equals(rewards["rewardlist"][i]["rewardid"].Value)) {
                    rewardNode = rewards["rewardlist"][i];
                    OpenInfoPopUp(PopMessageType.GetUserEventReward, rewardNode.ToString());
                    GameSystem.Instance.UserJSON["facebooklinkget"].AsBool = true;
                    GameSystem.Instance.UserDataJSON["data"]["facebooklinkget"].AsBool = true;
                }
            }
        }

        yield return new WaitForSeconds(0.1f);


        while (_objSimplePopup.activeSelf) {
            yield return new WaitForSeconds(0.1f);
        }

        _isWaitingCheck = false;

    }
    #endregion


    /// <summary>
    /// 첫 로비 진입시 체크 로직 (출석체크, 공지사항, 랭킹)
    /// </summary>
    /// <returns></returns>
    IEnumerator CheckingFirstLobbyEnter() {

        yield return new WaitForSeconds(0.2f);

        // 이전 체크가 끝날때까지 대기 
        while (_isWaitingCheck) {
            yield return new WaitForSeconds(0.1f);
        }


        CheckAttendance();



    }




    private void CheckLastRankReward() {
        // 랭킹보상이 없으면 끝.
        if (!GameSystem.Instance.UserDataJSON["data"]["lastweekreward"].AsBool) {
            return;
        }

        // 팝업창 오픈한다. 
        RewardLastRank();
    }

    /// <summary>
    /// PoolManager 관련 초기화 
    /// </summary>
    private void SetSpawningObject() {
        SetReadyCharacterList(); // 캐릭터 리스트 
        SetFriendList(); // 친구에게 하트보내기 
        SetNoticeList(); // 공지사항 리스트
    }
    

    /// <summary>
    /// Lobby 화면의 Lock 처리 
    /// </summary>
    private void SetLockState() {
        // 미션과 도감 

        // 미션이 Unlock 되어있지 않은 경우 
        if(!GameSystem.Instance.CheckStateMissionUnlock()) {
            _btnMission.normalSprite = PuzzleConstBox.spriteLockIcon;
        }
        else {
            _btnMission.normalSprite = PuzzleConstBox.spriteMissionBtn;
        }

        // 도감 Unlock 상태 체크 
        if(!GameSystem.Instance.CheckStateWantedUnlock()) {
            _btnWanted.normalSprite = PuzzleConstBox.spriteLockIcon;
        }
        else {
            _btnWanted.normalSprite = PuzzleConstBox.spriteWantedBtn;
        }


        // 빙고 Unlock체크 
        if (!GameSystem.Instance.CheckBingoUnlockProceed()) {
            _btnBingo.normalSprite = PuzzleConstBox.spriteLockBotIcon;
        }
        else {
            _btnBingo.normalSprite = PuzzleConstBox.spriteBingoBtn;
        }
    }


    /// <summary>
    /// 가챠 바겐 마크 체크 
    /// </summary>
    private void CheckGatchaBargain() {
        if ((PuzzleConstBox.originalMultiGatchaPrice > GameSystem.Instance.SpecialMultiGatchaPrice)
            || (PuzzleConstBox.originalMultiFishingPrice > GameSystem.Instance.SpecialMultiFishingPrice)) {

            _gatchaBargainMark.SetActive(true);
        }
        else {

            _gatchaBargainMark.SetActive(false);
        }
    }

    /// <summary>
    /// 출석 체크 
    /// </summary>
    private void CheckAttendance() {
        //Debug.Log("▶ CheckAttendance # 1");


        // 튜토리얼이면 진행되면 안된다. 
        if (GameSystem.Instance.TutorialStage >= 10)
            return;

        if (GameSystem.Instance.AttendanceJSON == null)
            return;

        //Debug.Log("▶ CheckAttendance # 2");

        // result 0인 경우에만 출석 체크 
        if (GameSystem.Instance.AttendanceJSON["result"].AsInt == 0) {
            Debug.Log("▶ CheckAttendance # 3");
            WindowManagerCtrl.Instance.OpenAttendance();
            GameSystem.Instance.AttendanceJSON["result"].AsInt = 1;
        } else {
            // 출석체크를 이미 한 경우 공지사항 체크로 이동
            CheckNotice();
        }

    }

    private void CheckNotice() {

        //Debug.Log("▶ CheckNotice");

        CheckLastRankReward(); // 랭킹 보상 창 오픈 


        // 최초 로비 진입시에만 호출 
        if (GameSystem.Instance.IsEnterLobbyCompleted) {
            CheckProceedUnlock(); // Unlock 처리 진행 
            return;
        }

        // 오늘만 보기를 체크한 경우를 확인한다
        if (GameSystem.Instance.LoadCurrentDayOfYear() == GameSystem.Instance.DtSyncTime.DayOfYear) {
            GameSystem.Instance.IsEnterLobbyCompleted = true;
            //Debug.Log("▶▶▶ Not Today any more (Notice) ");
            CheckProceedUnlock(); // Unlock 처리 진행 
            return;
        }


        WindowManagerCtrl.Instance.OpenNoticeList();

        //한번이라도 CheckNotice가 호출된 경우에는 사용작자 로비로 진입을 했다는것으로 판단한다. 
        GameSystem.Instance.IsEnterLobbyCompleted = true;

        CheckProceedUnlock(); // Unlock 처리 진행 
    }



    #endregion

    #region Unlock, Tip 처리 



    /// <summary>
    /// Unlock 진행 여부 처리 
    /// </summary>
    public void CheckProceedUnlock() {
        StartCoroutine(CheckingProceedUnlock());
    }


    IEnumerator CheckingProceedUnlock() {

        yield return new WaitForSeconds(0.5f); // 지연 시간 0.5초 

        // 공지사항 창이 열려있으면 대기
        while (WindowManagerCtrl.Instance.ObjNoticeList.gameObject.activeSelf) {
            yield return new WaitForSeconds(0.1f);
        }



        // 준비 화면의 Unlock은 LobbyCtrl.OpenReady에서 처리한다. 
        if (objReadyGroup.gameObject.activeSelf) {
            yield break;

        }

        Debug.Log("★★★★ Ready Closed ");

        // 잠금 해제는 한번에 하나씩만 실행된다.
        // 로비 화면의 잠금은 한번에 두개가 풀릴 수 있으니, Lock 완료시점에 한번 더 호출한다. 
        /*
        if(GameSystem.Instance.CheckBingoUnlockProceed()) {
            StartCoroutine(UnlockingBingo());
            yield break;
        }
        */

        if (GameSystem.Instance.CheckNekoServiceUnlockProceed()) {
            StartCoroutine(UnlockingNekoService());
            yield break;
        }
            

        if (GameSystem.Instance.CheckMissionUnlockProceed()) {
            StartCoroutine(UnlockingMission());
            yield break;
        }

        if(GameSystem.Instance.CheckWantedUnlockProceed()) {
            StartCoroutine(UnlockingWanted());
            yield break;
        }


        if(GameSystem.Instance.CheckRankingUnlockProceed()) {
            StartCoroutine(UnlockingRanking());
            yield break;
        }
    }




    public void CheckUnlockNekoLevelUpTip() {

        Debug.Log("★CheckUnlockNekoLevelUpTip Start");

        // 결과창이 없을때는 띄우지 않음 
        if (!objResultForm.activeSelf)
            return;

        if (GameSystem.Instance.CheckNekoLevelUpUnlockProceed()) {

            GameTip.SetGameTip(TipType.NekoLevelup);
            GameSystem.Instance.Post2Unlock("nekolevelup_tip");
        }
    }


    /// <summary>
    /// 네코 서비스 팁 표시 (잠금해제가 아니다)
    /// </summary>
    /// <returns></returns>
    IEnumerator UnlockingNekoService() {
        // Wanted 화면이 열린 후 팁 화면 오픈 
        GameTip.SetGameTip(TipType.NekoService);
        while (GameTip.gameObject.activeSelf) {
            yield return new WaitForSeconds(0.02f);
        }

        GameSystem.Instance.Post2Unlock("nekoservice_tip");
    }


    /// <summary>
    ///  랭킹 잠금 해제 처리 
    /// </summary>
    /// <returns></returns>
    IEnumerator UnlockingRanking() {
        DisableAllButton();


        GameSystem.Instance._explainTextIndex = 2520; //


        

        SetExplain(false); // 랭킹 설명 1
        while (_isWaitingTab) {
            yield return null;
        }

        SetExplain(false); // 랭킹 설명 2
        while (_isWaitingTab) {
            yield return null;
        }


        //랭킹 창 오픈까지 기다린다. 
        _tutorialHand.SetEnable(new Vector3(290, 460, 0)); // 손가락 등장
        ReturnEnableSomeButton("ButtonRanking");

        while (!bigPopup.gameObject.activeSelf) {
            yield return new WaitForSeconds(0.02f);
        }

        _tutorialHand.SetDisable();


        yield return new WaitForSeconds(0.2f);
        DisableAllButton();



        SetExplain(false); // 랭킹 설명 3
        while (_isWaitingTab) {
            yield return null;
        }

        SetExplain(false); // 랭킹 설명 4
        while (_isWaitingTab) {
            yield return null;
        }

        SetExplain(false); // 랭킹 설명 5
        while (_isWaitingTab) {
            yield return null;
        }

        EnableAllButton(); // 창이 열리면 모든 버튼 활성화 처리 


        // Unlock 완료 처리 
        //GameSystem.Instance.SaveESvalueBool(PuzzleConstBox.ES_UnlockRanking, true);
        GameSystem.Instance.Post2Unlock("ranking_unlock");

        // Wanted 창이 닫히길 기다렸다가 잠금 해제 확인을 실행한다.
        while (bigPopup.gameObject.activeSelf) {
            yield return new WaitForSeconds(0.02f);
        }

        Debug.Log("▶ Firt Ranking Closed. CheckProceedUnlock Again");

        CheckProceedUnlock();

    }

    // 빙고 Unlock
    IEnumerator UnlockingBingo() {

        _isUnlockingBingo = true; // unlocking 이 진행중

        /*

        DisableAllButton();

        GameSystem.Instance._explainTextIndex = 3180;

        SetExplain(true); // 설명 1
        while (_isWaitingTab) {
            yield return null;
        }


        // Spark 이펙트 처리
        _lockSpark.Play(_btnBingo.transform.localPosition);
        PlayUnlock(); // 사운드 
        yield return new WaitForSeconds(0.3f);

        // 버튼 변경 
        _btnBingo.normalSprite = PuzzleConstBox.spriteBingoBtn;
        _btnBingo.transform.DOScale(1.1f, 0.3f).SetLoops(-1, LoopType.Yoyo);

        _tutorialHand.SetEnable(new Vector3(-260, -590, 0));

        ReturnEnableSomeButton("ButtonBingo");

        

        // 팁 화면 오픈을 기다린다. 
        while (!GameTip.gameObject.activeSelf) {
            yield return new WaitForSeconds(0.02f);
        }

        _tutorialHand.SetDisable();

        DisableAllButton();

        SetExplain(true); // 설명 2
        while (_isWaitingTab) {
            yield return null;
        }

        EnableAllButton();

        // ReturnEnableSomeButton("Confirm");

        // 팁 화면 종료를 기다린다.
        while (GameTip.gameObject.activeSelf) {
            yield return new WaitForSeconds(0.02f);
        }

        DisableAllButton();

        SetExplain(true); // 설명 3
        while (_isWaitingTab) {
            yield return null;
        }

        _isUnlockingBingo = false; // 이제 팁을 띄울 필요가 없음 

        // 빙고 화면 강제 오픈
        OpenBingo();

        // 빙고 화면 오픈을 기다린다. 
        while (!_bingoMaster.activeSelf) {
            yield return new WaitForSeconds(0.02f);
        }

        yield return new WaitForSeconds(0.6f);


        DisableAllButton();


        SetExplain(false); // 설명 4
        while (_isWaitingTab) {
            yield return null;
        }

        DisableAllButton();


        // 강제 선택 
        ReturnEnableSomeButton("btnBingoSelect");
        ReturnEnableSomeButton("btnBingoRetry");

        _tutorialHand.SetEnable(new Vector3(15, -280, 0));

        // 메세지 창 오픈을 기다린다.
        while(!_objUpperPopup.gameObject.activeSelf) {
            yield return new WaitForSeconds(0.02f);
        }
        _tutorialHand.SetDisable();

        yield return new WaitForSeconds(0.2f);

        DisableAllButton();

        ReturnEnableSomeButton("btnYes");
        _tutorialHand.SetEnable(new Vector3(100, -160, 0));

        // 메세지 창  종료를 기다린다.
        while (_objUpperPopup.gameObject.activeSelf) {
            yield return new WaitForSeconds(0.01f);
        }
        _tutorialHand.SetDisable();

        EnableAllButton();

        */

        // 빙고 팁 완료 
        GameSystem.Instance.Post2Unlock("bingo_tip");


        // 빙고 창이 닫히길 기다렸다가 잠금 해제 확인을 실행한다.
        while (_bingoMaster.activeSelf) {
            yield return new WaitForSeconds(0.02f);
        }

        Debug.Log("▶ First Bingo Closed. CheckProceedUnlock Again");

        _btnBingo.transform.DOKill();
        _btnBingo.transform.localScale = GameSystem.Instance.BaseScale;

        CheckProceedUnlock();

        

    }

    public IEnumerator UnlockingFirstBingoMission() {
        Debug.Log("▲UnlockingFirstBingoMission");

        GameSystem.Instance.Post2Unlock("bingo_mission_tip");

        DisableAllButton();


        GameSystem.Instance._explainTextIndex = 3184;

        SetExplain(false); // 설명1
        while (_isWaitingTab) {
            yield return null;
        }

        SetExplain(false); // 설명2
        while (_isWaitingTab) {
            yield return null;
        }

        SetExplain(false); // 설명3
        while (_isWaitingTab) {
            yield return null;
        }

        EnableAllButton();

    }

    /// <summary>
    /// 도감 잠금 해제 처리 
    /// </summary>
    /// <returns></returns>
    IEnumerator UnlockingWanted() {
        DisableAllButton();


        //GameSystem.Instance._explainTextIndex = 2510; //
        GameSystem.Instance._explainTextIndex = 2516;

        SetExplain(false); // Wanted 설명 1
        while (_isWaitingTab) {
            yield return null;
        }


        // Spark 이펙트 처리
        _lockSpark.Play(_btnWanted.transform.localPosition);
        PlayUnlock(); // 사운드 
        yield return new WaitForSeconds(0.3f);

        // 버튼 변경 
        _btnWanted.normalSprite = PuzzleConstBox.spriteWantedBtn;
        _btnWanted.transform.DOScale(1.1f, 0.3f).SetLoops(-1, LoopType.Yoyo);

        _tutorialHand.SetEnable(new Vector3(200, 490, 0));

        ReturnEnableSomeButton("ButtonWanted");

        // 화면 오픈을 기다린다. 
        while (!WindowManagerCtrl.Instance.CollectionMaster.gameObject.activeSelf) {
            yield return new WaitForSeconds(0.02f);
        }
       
        _tutorialHand.SetDisable();


        yield return new WaitForSeconds(0.2f);
        DisableAllButton();


        // 버튼 원상복구 처리 
        _btnWanted.transform.DOKill();
        _btnWanted.transform.localScale = GameSystem.Instance.BaseScale;


        // Wanted 화면이 열린 후 팁 화면 오픈 
        /*
        GameTip.SetGameTip(TipType.Wanted);
        while (GameTip.gameObject.activeSelf) {
            yield return new WaitForSeconds(0.02f);
        }
        */



        SetExplain(false); // Wanted 설명 2
        while (_isWaitingTab) {
            yield return null;
        }

        SetExplain(false); // Wanted 설명 3
        while (_isWaitingTab) {
            yield return null;
        }

        SetExplain(false); // Wanted 설명 4
        while (_isWaitingTab) {
            yield return null;
        }



        EnableAllButton(); // 모든 버튼 활성화 처리 



        // Unlock 완료 처리 
        //GameSystem.Instance.SaveESvalueBool(PuzzleConstBox.ES_UnlockWanted, true);
        GameSystem.Instance.Post2Unlock("wanted_unlock");

        // Wanted 창이 닫히길 기다렸다가 잠금 해제 확인을 실행한다.
        while (bigPopup.gameObject.activeSelf) {
            yield return new WaitForSeconds(0.02f);
        }

        Debug.Log("▶ Firt Wanted Closed. CheckProceedUnlock Again");

        CheckProceedUnlock();


    }


    /// <summary>
    /// Passive Unlock 진행 여부 체크 
    /// </summary>
    public void CheckPassiveUnlockProceed() {

        Debug.Log("▶ CheckPassiveUnlockProceed");

        if (GameSystem.Instance.CheckPassiveUnlockProceed()) {
            StartCoroutine(UnlockingPassive());
        }
    }

    IEnumerator UnlockingPassive() {

        // 레디창이 완전히 열릴때까지 대기. 
        while (!objReadyGroup.gameObject.activeSelf) {
            yield return null;
        }

        DisableAllButton(); // 버튼 비활성 처리 

        //yield return new WaitForSeconds(0.2f);

        // 설명 시작
        GameSystem.Instance._explainTextIndex = 2505; //

        SetExplain(true); // 패시브 설명 1
        while (_isWaitingTab) {
            yield return null;
        }

        //Unlock 처리
        /*
        for (int i = 0; i < arrPlayerAbilityCtrl.Length; i++) {
            arrPlayerAbilityCtrl[i].UnlockPassive();
        }
        */

        PlayUnlock(); // 사운드 

        yield return new WaitForSeconds(1f);


        GameTip.SetGameTip(TipType.AllPassive);
        while (GameTip.gameObject.activeSelf) {
            yield return new WaitForSeconds(0.02f);
        }

        SetExplain(true); // 패시브 설명 2
        while (_isWaitingTab) {
            yield return null;
        }

        SetExplain(true); // 패시브 설명 3
        while (_isWaitingTab) {
            yield return null;
        }

        EnableAllButton();

        // Unlock 완료 처리 
        //GameSystem.Instance.SaveESvalueBool(PuzzleConstBox.ES_UnlockPassive, true);
        GameSystem.Instance.Post2Unlock("passive_unlock");

    }




    /// <summary>
    /// Item Unlock 진행 여부 체크 
    /// </summary>
    public void CheckItemUnlockProceed() {
        if(GameSystem.Instance.CheckItemUnlockProceed()) {
            StartCoroutine(UnlockingItem());
        }
    }


    IEnumerator UnlockingItem() {

        // 레디창이 완전히 열릴때까지 대기. 
        while(!objReadyGroup.gameObject.activeSelf) {
            yield return null;
        }

        DisableAllButton();

        GameSystem.Instance._explainTextIndex = 2512; //


        SetExplain(false); // 아이템 설명 1
        while (_isWaitingTab) {
            yield return null;
        }

        SetExplain(false); // 아이템 설명 2
        while (_isWaitingTab) {
            yield return null;
        }

        // Unlock 처리 
        ReadyGroupCtrl.Instance.UnlockItems();
        PlayUnlock(); // 사운드 

        yield return new WaitForSeconds(1f);


        // Tip 화면 호출
        GameTip.SetGameTip(TipType.AllPuzzleItem); 
        while (GameTip.gameObject.activeSelf) {
            yield return new WaitForSeconds(0.02f);
        }

        SetExplain(false); // 아이템 설명 3
        while (_isWaitingTab) {
            yield return null;
        }


        SetExplain(false); // 아이템 설명 4
        while (_isWaitingTab) {
            yield return null;
        }

        EnableAllButton();

        // Unlock 완료 처리 
        //GameSystem.Instance.SaveESvalueBool(PuzzleConstBox.ES_UnlockItem, true);
        GameSystem.Instance.Post2Unlock("item_unlock");

    }


    /// <summary>
    /// 미션 잠금해제 처리 
    /// </summary>
    /// <returns></returns>
    IEnumerator UnlockingMission() {

        Debug.Log(">>>>> UnlockingMission Start");
        _btnMission.normalSprite = PuzzleConstBox.spriteLockIcon;

        DisableAllButton();

        GameSystem.Instance._explainTextIndex = 2500; //

        SetExplain(true); // 미션에 대한 설명을 시작한다. 
        while (_isWaitingTab) {
            yield return null;
        }

        // Spark 이펙트 처리
        _lockSpark.Play(_btnMission.transform.localPosition);
        PlayUnlock(); // 사운드 
        yield return new WaitForSeconds(0.3f);

        // 버튼 변경 
        _btnMission.normalSprite = PuzzleConstBox.spriteMissionBtn;
        _btnMission.transform.DOScale(1.1f, 0.3f).SetLoops(-1, LoopType.Yoyo);

        UpdateMissionNew();


        _tutorialHand.SetEnable(new Vector3(30, 460, 0));

        SetExplain(true); // 이곳에서 미션을 달성하고.. 어쩌고 
        while (_isWaitingTab) {
            yield return null;
        }

        ReturnEnableSomeButton("ButtonMission");

        // 팝업 대기 
        while(!bigPopup.gameObject.activeSelf) {
            yield return new WaitForSeconds(0.02f);
        }

        _tutorialHand.SetDisable();

        yield return new WaitForSeconds(0.2f);
        DisableAllButton();

        // 버튼 원상복구 처리 
        _btnMission.transform.DOKill();
        _btnMission.transform.localScale = GameSystem.Instance.BaseScale;


        SetExplain(true); // 미션 설명 3
        while (_isWaitingTab) {
            yield return null;
        }

        // Tip 화면 호출
        GameTip.SetGameTip(TipType.MissionResult);
        while (GameTip.gameObject.activeSelf) {
            yield return new WaitForSeconds(0.02f);
        }

        SetExplain(true); // 미션 설명 4
        while (_isWaitingTab) {
            yield return null;
        }

        EnableAllButton(); // 창이 열리면 모든 버튼 활성화 처리 



        // Unlock 완료 처리 
        //GameSystem.Instance.SaveESvalueBool(PuzzleConstBox.ES_UnlockMission, true);
        GameSystem.Instance.Post2Unlock("mission_unlock");


        // 미션창이 닫히길 기다렸다가 미션 잠금 해제 확인을 실행한다.
        while (bigPopup.gameObject.activeSelf) {
            yield return new WaitForSeconds(0.02f);
        }

        Debug.Log("▶ Firt Mission Closed. CheckProceedUnlock Again");

        CheckProceedUnlock();


    }

    #endregion


    #region 튜토리얼

    #region JP 튜토리얼 부분 

    private void CheckLobbyTutorial() {


        Debug.Log("▶ CheckLobbyTutorial Start");


        // 튜토리얼 스테이지 값이 '0' 이면 첫 튜토리얼 대상자. 
        if (GameSystem.Instance.TutorialStage != 0 && GameSystem.Instance.TutorialStage != 10) {
            Debug.Log("Not Tutorial Player");
            return;
        }

        //인게임 튜토리얼이 완료 안되어있다면 캐릭터 장착 리스트 클리어. 
        if (GameSystem.Instance.LocalTutorialStep < 4) {
            GameSystem.Instance.ClearEquipNekoInfo();
        }

        /* LocalTutorialStep
        -가챠 통신이 완료되면 1
        - 가챠 화면이 닫히면 2
        - 인게임 튜토리얼이 시작되면 3
        - 인게임 튜토리얼이 완료되면 4
        - 고양이 레벨 업그레이드가 완료되면 5
        - 고양이 생선 먹이기가 완료되면 6
        - 튜토리얼 완전 종료 후 7
        */
        if (GameSystem.Instance.LocalTutorialStep < 4) {
            StartCoroutine(TeachingStep1());
        } else {
            // 첫 인게임을 완료한 경우
            StartCoroutine(TeachingStep2());
        }

    }

    #region 첫 게임 종료 후 튜토리얼 (성장)
    IEnumerator TeachingStep2() {

        spNekoRewardTop.gameObject.SetActive(false);
        DisableAllButton();

        // 튜토리얼이 아직 끝나지 않은 사용자 임에도, 생선이나 코인이 부족하다면, 중간에 끊긴 사용자라 여기고 튜토리얼 완료 처리 
        if (GameSystem.Instance.UserTuna < 1 || GameSystem.Instance.UserGold < 1000) {

            GameSystem.Instance._explainTextIndex = 3153; // 마지막 멘트 

            SetExplain(false); // 이제 기본적인 튜토리얼이 모두 끝났어요.
            while (_isWaitingTab) {
                yield return null;
            }
            GameSystem.Instance.SaveLocalTutorialStep(6);
            GameSystem.Instance.Post2TutorialComplete();
            yield break;
        }

        bgmSrc.Play();

        AdbrixManager.Instance.SendAdbrixNewUserFunnel(AdbrixManager.Instance.TUTORIAL_STEP3);

        Debug.Log(">>>>> TeachingStep2 Start");
        GameSystem.Instance._explainTextIndex = 3146;

        Debug.Log(">>>>> First Grow up");

        SetExplain(false);
        while (_isWaitingTab) {
            yield return null;
        }

        // 네코 성장 선택
        _tutorialHand.SetEnable(new Vector3(-300, -520, 0));
        EnableSomeButton("ButtonUp");

        // 장착 화면 뜰때까지 대기. 
        while (!objSelectNeko.activeSelf) {
            yield return null;
        }

        _tutorialHand.SetDisable();

        yield return new WaitForSeconds(0.5f);
        DisableAllButton();

        Debug.Log("Grow Window Open");

        SetExplain(false); // 성장시킬 네코를 선택하고, 네코 성장 버튼을 터치해주세요.

        while (_isWaitingTab) {
            yield return null;
        }



        _tutorialHand.SetEnable(new Vector3(-240, -100, 0));
        SetFirstNekoEnableInTutorial();

        // 네코가 선택될때까지 대기
        NekoSelectBigPopCtrl nekoSelect = objSelectNeko.GetComponent<NekoSelectBigPopCtrl>();
        while (nekoSelect.Neko == null) {
            yield return null;
        }

        DisableAllButton();
        EnableSomeButton("btnUpgrade");

        // 손처리
        _tutorialHand.SetDisable();
        _tutorialHand.SetEnable(new Vector3(220, 80, 0));

        // 네코 레벨업 확인창 오픈 
        while(!objNekoLevelUpConfirmWindow.activeSelf) {
            yield return null;
        }

        DisableAllButton();
        EnableSomeButton("btnLevel1");
        _tutorialHand.SetDisable();
        _tutorialHand.SetEnable(new Vector3(140, -260, 0));



        // 네코 레벨업 창 
        while (!objNekoLevelup.activeSelf) {
            yield return null;
        }

        _tutorialHand.SetDisable();

        DisableAllButton();
        // 네코레벨이 오르면 공격력이 상승해요.
        SetExplain(false);
        while (_isWaitingTab) {
            yield return null;
        }

        Debug.Log("Enable btnLevelUp");
        EnableSomeButton("btnLevelUp");
        _tutorialHand.SetEnable(new Vector3(140, -260, 0));

        // 네코 레벨 업그레이드가 완료되면 4 에서 5로 변경된다. 
        while (GameSystem.Instance.LocalTutorialStep < 5) {
            yield return null;
        }

        _tutorialHand.SetDisable();
        DisableAllButton();

        SetExplain(false); // 이번엔 네코 등급을 올려볼까요?
        while (_isWaitingTab) {
            yield return null;
        }

        SetExplain(false); // 생선은 .. 얻을 수 있어요.
        while (_isWaitingTab) {
            yield return null;
        }

        // 이 부분에서. 보유한 고양이(첫번째)가 더이상 성장시킬 수 없는 경우(max_grade) 통과 시켜야 한다. 
        if (GameSystem.Instance.SelectNeko.IsMaxGrade) {
            GameSystem.Instance._explainTextIndex = 3170;

            SetExplain(false); // 현재 선택한 네코는 최고 등급이라 더이상 성장 시킬 수 없다. 
            while (_isWaitingTab) {
                yield return null;
            }


            SetExplain(false); // 다른 네코를 얻게되면 생선주기 화면을 통해 네코를 성장시켜 보아요.
            while (_isWaitingTab) {
                yield return null;
            }

            // 끝냄. 

            GameSystem.Instance.LocalTutorialStep = 6;

        }
        else {

            // 생선창 버튼에 손 표시 
            Debug.Log("Enable btnFish");
            EnableSomeButton("btnFish");
            _tutorialHand.SetDisable();
            _tutorialHand.SetEnable(new Vector3(-240, 80, 0));

            // 생선창 오픈을 기다린다. 
            while (!objFishFeed.activeSelf) {
                yield return null;
            }
            _tutorialHand.SetDisable();

            yield return new WaitForSeconds(0.2f);
            

            //네코 등급을 올리기 위해 생선을 먹여볼까요? 하단의 참치를 터치해보세요.
            SetExplain(false); // 생선은 .. 얻을 수 있어요.
            while (_isWaitingTab) {
                yield return null;
            }

            // 손표시 (참치 컬럼 표시)
            _tutorialHand.SetEnable(new Vector3(-155, -270, 0));
            //EnableSomeButton("btnFeed");


            // 생선먹이기가 완료되면 TutorialStep이 5에서 6으로 변경
            while (GameSystem.Instance.LocalTutorialStep < 6) {
                yield return null;
            }

            //objFishFeed
            _tutorialHand.SetDisable();
        }

        EnableAllButton();



        Debug.Log("Closing Stack");


        // 띄워진 모든 팝업을 제거한다. 
        /*
        for (int i=0; i < 3; i++) {
            CloseStack();
            yield return new WaitForSeconds(0.5f);
        }
        */


        Debug.Log("End teaching 2");

        GameSystem.Instance._explainTextIndex = 3153; // 마지막 멘트 

        SetExplain(false); // 이제 기본적인 튜토리얼이 모두 끝났어요.
        while (_isWaitingTab) {
            yield return null;
        }

        GameSystem.Instance.Post2TutorialComplete();

    }

    #endregion

    #region 첫번째 인게임까지의 튜토리얼 처리  

    /// <summary>
    /// 캐릭터 뽑기로 강제 이동 
    /// </summary>
    /// <returns></returns>
    IEnumerator TeachingStep1() {

        bgmSrc.Play();
        Debug.Log(">>>>> TeachingStep1 Start");

        
        AdbrixManager.Instance.SendAdbrixNewUserFunnel(AdbrixManager.Instance.TUTORIAL_STEP1);

        Transform btn;

        spNekoRewardTop.gameObject.SetActive(false);

        // 보석이 300개 이상인 경우, 네코 0마리에만. (중간에 종료된 사용자를 대비하기 위함.)
        if (GameSystem.Instance.UserGem >= 300 && GameSystem.Instance.GetUserNekoCount() == 0) {

            GameSystem.Instance._explainTextIndex = 3137; // 3137부터 시작


            // 모든 버튼의 기능을 제거
            DisableAllButton();

            SetExplain(true); // 밋치리네코팝에 오신걸 환영해요. 
            while (_isWaitingTab) {
                yield return null;
            }

            SetExplain(true); // 네코 친구를 데리러 가보죠. 
            while (_isWaitingTab) {
                yield return null;
            }

            SetExplain(true); // 하단 뽑기 버튼을.. 
            while (_isWaitingTab) {
                yield return null;
            }

            btn = ReturnEnableSomeButton("ButtonAdopt"); // 뽑기 버튼 활성화 
            _tutorialHand.SetEnable(new Vector3(-180, -530, 0));


            // Crane 창 오픈까지 대기 
            while (!WindowManagerCtrl.Instance.ObjGatchaConfirm.gameObject.activeSelf) {
                yield return null;
            }

            _tutorialHand.SetDisable();
            WindowManagerCtrl.Instance.ObjGatchaConfirm.GetComponentInChildren<UIScrollView>().enabled = false;

            Debug.Log("Open Gatcha Confirm");


            DisableAllButton();
            SetExplain(false); //스페셜 네코 1회 뽑기를 터치해볼까요?
            while (_isWaitingTab) {
                yield return null;
            }

            btn = ReturnEnableSomeButton("btnSpecialOne"); // 스페셜 1회 뽑기 버튼 활성화 
            _tutorialHand.SetEnable(new Vector3(-90, -340, 0)); // 손가락 세팅.


            // 뽑기 화면이 열릴때까지. 대기.
            while (GameSystem.Instance.LocalTutorialStep < 1) {
                yield return new WaitForSeconds(0.2f);
            }

            _tutorialHand.SetDisable();
            // 가챠화면의 input을 Lock.
            _objGatchaScreen.GetComponent<NekoGatchaCtrl>().InputLock = true;

            SetExplain(true); // 레버를 조작하여 뽑기를.. 어쩌고. 
            while (_isWaitingTab) {
                yield return null;
            }

            _objGatchaScreen.GetComponent<NekoGatchaCtrl>().InputLock = false;

            // 뽑기 완료까지 대기. 
            while (GameSystem.Instance.LocalTutorialStep < 2) {
                yield return new WaitForSeconds(0.5f);
            }

            Debug.Log("Gatcha Completed ");
        }
        else {

            // 조건에 충족하지 않으면 이미 뽑았다 여기고 다음으로 진행.
            GameSystem.Instance._explainTextIndex = 3142; // 3142부터 시작
            if (GameSystem.Instance.LocalTutorialStep < 2)
                GameSystem.Instance.SaveLocalTutorialStep(2); // 2로 저장. (가챠 종료)
        }


        yield return new WaitForSeconds(1);

        Debug.Log("CurrentStage");
        DisableAllButton();

        SetExplain(true); // 준비창으로 이동
        while (_isWaitingTab) {
            yield return null;
        }

        // 준비창으로 안내 처리 
        _tutorialHand.SetEnable(new Vector3(-310, -70, 0));

        EnableSomeButton("CurrentStage"); // CurrentStage 오픈 
        if(GameSystem.Instance.UserCurrentStage > 1) { // 이미 1스테이지를 클리어한 경우 추가. 
            EnableSomeButton("ClearStageObject");
        }
        

        Debug.Log("CurrentStage #2");
        


        while (!objReadyGroup.gameObject.activeSelf) {
            yield return null;
        }

        DisableAllButton();
        _tutorialHand.SetDisable();

        Debug.Log("Touch Ready ");

        SetExplain(false); // 네코 친구를 3마리까지... 
        while (_isWaitingTab) {
            yield return null;
        }

        ReturnEnableSomeButton("EquipNekoPrefab1");
        _tutorialHand.SetEnable(new Vector3(-220, 300, 0));
        pnlSelectiveNekoScrollView.gameObject.GetComponent<UIScrollView>().enabled = false;


        // 장착 화면 뜰때까지 대기. 
        while (!objSelectNeko.activeSelf) {
            yield return null;
        }

        _tutorialHand.SetDisable();

        yield return new WaitForSeconds(0.5f);
        DisableAllButton();

        SetExplain(false); // 방금 사귄 네코 친구를 장착해주세요.
        while (_isWaitingTab) {
            yield return null;
        }

        _tutorialHand.SetEnable(new Vector3(-250, -100, 0)); // 첫번째 네코에게 손가락. 
        SetFirstNekoEnableInTutorial();


        NekoSelectBigPopCtrl nekoSelect = objSelectNeko.GetComponent<NekoSelectBigPopCtrl>();
        // 네코가 선택될때까지 대기.
        while (nekoSelect.Neko == null) {
            yield return null;
        }
        _tutorialHand.SetDisable();

        // 네코 선택 후 장착 처리 
        DisableAllButton();
        EnableSomeButton("btnEquip");
        _tutorialHand.SetEnable(new Vector3(15, -530, 0));

        yield return null;
    }

    public void TeachingReady() {
        SetExplain(true);
        _tutorialHand.SetEnable(new Vector3(150, -400, 0));
    }
    #endregion


    private void EnableAllButton() {
        _arrButtons = GameObject.FindObjectsOfType<UIButton>();

        for (int i = 0; i < _arrButtons.Length; i++) {

            // 네코 서비스 관련 추가 처리 
            if(_arrButtons[i].name == "spGift") {

                // 상태에 따라서 설정 
                if (GameSystem.Instance.IsNekoRewardReady) {
                    _arrButtons[i].enabled = true;
                    continue;
                }
                else {
                    _arrButtons[i].enabled = false;
                    continue;
                }
            }

            // 일반버튼들.
            _arrButtons[i].enabled = true;
        }
    }

    public void DisableAllButton() {

        _arrButtons = GameObject.FindObjectsOfType<UIButton>();

        for (int i = 0; i < _arrButtons.Length; i++) {
            _arrButtons[i].enabled = false;
        }
    }

    public void EnableSomeButton(string pName) {

        for (int i = 0; i < _arrButtons.Length; i++) {

            if (_arrButtons[i].name == pName) {
                _arrButtons[i].enabled = true;

            }
        }
    }


    public Transform ReturnEnableSomeButton(string pName) {

        for (int i = 0; i < _arrButtons.Length; i++) {

            if (_arrButtons[i].name == pName) {
                _arrButtons[i].enabled = true;
                return _arrButtons[i].transform;
            }
        }

        return null;
    }


    #endregion



    /// <summary>
    /// 설명 세팅
    /// </summary>
    public void SetExplain(bool pUpperFlag) {

        _isWaitingTab = true; // 기다리기 시작 

        _explain.SetActive(true);
        _explain.GetComponent<TutorialExplainCtrl>().SetLobbyExplainText(GameSystem.Instance._explainTextIndex++, pUpperFlag);

    }

    #endregion

    #region Update

    // Update is called once per frame
    void Update() {

        // 테스트 코드. 강제 발생
        if(Input.GetKeyDown(KeyCode.A)) {
            //StartCoroutine(UnlockingMission());
            //OpenBingo();
        }


        // 하트 시간 체크
        if (Time.frameCount % 20 == 0) {
            UpdateHeartTime();
        }

        // 게임결과 보여주는 중에는 동작하지 않음.
        if (OnResultShow) {
            //Debug.Log("OnResultShow is true");
            return;
        }


        // 네코 진화 화면이 떠있는중에는 동작하지 않음 
        if (objNekoEvolution.gameObject.activeSelf) {
            //Debug.Log("NekoEvolution is Opened");
            return;
        }

        // 빙고 화면이 떠있는중에 동작하지 않음
        if (BingoMasterCtrl.Instance.BingoMasterUI.activeSelf)
            return;
        


        // Escape 동작 제어  
        // Lobby , Ready 관련된 화면은 Stack으로 저장(Push)되고 종료시 Pop 된다. 
        if (Input.GetKeyDown(KeyCode.Escape)) {

            if (GameSystem.Instance.TutorialComplete != 2)
                return;


            // 서버 통신중에는 return 시킨다. 
            if (GetWaingRequestEnable())
                return;

            CheckEscapeControl();
        }



        // 고양이의 보은(네코의 보은) 체크 
        /*
        if (Time.frameCount % 30 == 0 && !GameSystem.Instance.IsNekoRewardReady) {
            UpdateNekoRewardTime();
        }
        */

        // UI Stack 체크 (팝업창이 떠있으면 고양이 모이기 동작하지 않음)
        if (StackPopup.Count > 0 || _objGatchaScreen.activeSelf || WindowManagerCtrl.Instance.FishGatcha.gameObject.activeSelf) {
            StopAssemble();
            return;
        }

        if (Input.GetMouseButtonDown(0)) {
            ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            mask = 1 << LayerMask.NameToLayer("LobbyNeko");

            if (Physics.Raycast(ray, out hit, 10f, mask)) {
                //hit.transform.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-600, 600), Random.Range(-600, 600) , 0));
                hit.transform.GetComponent<BottleNekoCtrl>().HitLobbyNeko();
                

            }
        }


    }

    #endregion

    #region 네코의 보은 관련 처리 


    public void SetFreeCraneIcon(bool pEnable) {

        Debug.Log("▶▶ SetFreeCraneIcon :: " + pEnable);


        if(pEnable) {
            /*
            if(spNekoRewardTop.gameObject.activeSelf) { // 네코 선물의 활성화 여부에 따라 위치 조정 
                _freeCraneIconButton.transform.localPosition = PuzzleConstBox.lowerFreeCranePos;
            }
            else {
                _freeCraneIconButton.transform.localPosition = PuzzleConstBox.upperFreeCranePos;
            }
            */

            _freeCraneIconButton.Play();
        }
        else {
            //_freeCraneIconButton.StopAllCoroutines();
            _freeCraneIconButton.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 네코의 선물 얻기.
    /// </summary>
    public void GetNekoGift() {

        // 프리미엄 네코 서비스 100% 확률로 실행
        GameSystem.Instance.Post2RequestAdsRemainNekoGift(GetPremiumNekoGift);

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="pPremium"></param>
    private void GetPremiumNekoGift(bool pPremium) {

        Debug.Log("GetPremiumNekoGift:: " + pPremium.ToString());


        // false가 떨어지면, 횟수가 없는 것이므로 선물상자를 감춘다.
        if(!pPremium) {
            DisableNekoGift();
            return;
        }

        // 안내창 오픈
        _nekoBonusInfo.SetNekoBonusInfo();

        // 프리미엄 
        /*
        if (pPremium) {
            OpenInfoPopUp(PopMessageType.NekoGiftWithAdsConfirm);
            _isAdsNekoGift = true;
        }
        else {
            DisableNekoGift();
        }
        */
    }

    private void GetFreeNekoGift() {
        GameSystem.Instance.Post2NekoGift(0);
        //AdbrixManager.Instance.SendAppsFlyerEvent(AdbrixManager.Instance.AF_FREE_GIFT1);

        _isAdsNekoGift = false;
    }


    /// <summary>
    /// NekoGiftWithAdsConfirm 의 콜백
    /// </summary>
    private void GetAdsNekoGift() {
        ShowNekoGiftAd();
    }

    public void DelayedGetNekoFreeGift() {
        OffReadyNekoGift();
        Invoke("DelayingFreeGift", 0.5f);
        //DelayingFreeGift();
    }

    private void DelayingFreeGift() {
        GameSystem.Instance.Post2NekoGift(0);
		//AdbrixManager.Instance.SendAppsFlyerEvent(AdbrixManager.Instance.AF_FREE_GIFT1);
    }





    /// <summary>
    /// 네코의 보은이 준비되었음.  
    /// </summary>
    public void OnReadyNekoGift() {
        GameSystem.Instance.IsNekoRewardReady = true;
        spNekoRewardTop.gameObject.SetActive(true);
        spNekoRewardTop.GetComponent<UIButton>().enabled = true;
        spNekoRewardTop.transform.DOScale(1.3f, 0.5f).SetLoops(-1, LoopType.Yoyo);
    }

    /// <summary>
    /// 네코의 보은 Off(2016.09.05 업데이트 이전 방식)
    /// </summary>
    public void OffReadyNekoGift() {
        spNekoRewardTop.transform.DOKill();
        spNekoRewardTop.transform.localScale = GameSystem.Instance.BaseScale;
        spNekoRewardTop.GetComponent<UIButton>().enabled = false;
    }
    

    /// <summary>
    /// 네코 선물 상자 비활성화 (2016.09.05 Update)
    /// </summary>
    private void DisableNekoGift() {
        spNekoRewardTop.gameObject.SetActive(false);

        
    }


    /// <summary>
    /// 네코의 보은 받음!
    /// </summary>
    public void OnCompleteRewardedNeko() {


        // 상단 상자 처리 
        spNekoRewardTop.transform.DOScale(1, 0.5f).SetEase(Ease.InOutElastic);



    }

    private void InitNekoRewardObjects() {
        spNekoRewardTop.transform.localScale = GameSystem.Instance.BaseScale;
    }


    #endregion

    #region 상단정보 체크






    /// <summary>
    /// 하트 시간 레이블 업데이트 
    /// </summary>
    private void UpdateHeartTime() {
        //lblHeartTime.text = GameSystem.Instance.GetRemainTakeHeartTimeTick ();
        _heartBar.UpdateHeartTime();

    }

    /// <summary>
    /// 고양이의 보은 레이블 업데이트 
    /// </summary>
    private void UpdateNekoRewardTime() {

        //lblNekoRewardTime.text = GameSystem.Instance.GetRemainNekoRewardTimeTick();
    }


    /// <summary>
    /// 하트 업데이트 
    /// </summary>
    public void UpdateHearts() {

        _heartBar.UpdateHearts(GameSystem.Instance.HeartCount);
    }

    public void UpdateAdsPoint() {
        //_adsPointBar.SetAdsPoint(GameSystem.Instance.AdsPoint);
    }

    public void UpdateTopInformation() {

        // 각 상단정보 업데이트 
        
        UpdateHearts();
        UpdateMoney();

        UpdateMailBoxNew();
    }

    /// <summary>
    /// 상단 코인, 젬 바 처리 
    /// </summary>
    public void UpdateMoney() {
        // 부스트 아이템을 체크한 경우에는 처리 
        _coinBar.SetCoinBar(GameSystem.Instance.UserGold - GameSystem.Instance.CheckedBoostItemsPrice);
        _gemBar.SetGemBar(GameSystem.Instance.UserGem);
    }



    #endregion

    #region 팝업 Stack, 메세지 창 제어
    public void PushPopup(LobbyCommonUICtrl pPopup) {
        StackPopup.Push(pPopup);
    }

    // 단순 Pop
    public void PopPopup() {

        if (StackPopup.Count > 0)
           StackPopup.Pop();


        if (StackPopup.Count == 0) {
            Debug.Log("★ PopPopup");
            CheckNewClearBingo();
        }
    }

    public void CloseStack() {
        if (StackPopup.Count > 0)
            StackPopup.Pop().Close();


        if (StackPopup.Count == 0) {
            Debug.Log("★ CloseStack");
            CheckNewClearBingo();
        }

    }

    /// <summary>
    /// 클리어한 빙고 체크 
    /// </summary>
    private void CheckNewClearBingo() {

        if (WindowManagerCtrl.Instance.FishGatcha.gameObject.activeSelf)
            return;

		if (_objGatchaScreen.activeSelf)
			return;

		if (BingoMasterCtrl.Instance.BingoMasterUI.gameObject.activeSelf)
			return;

        if(BingoMasterCtrl.Instance.CheckExistsUncheckedFill()) {
            OpenBingo();
        }
    }

	/// <summary>
	/// Checks the user level bingo.
	/// </summary>
	public void CheckUserLevelBingo() {


        // Event Bingo에 대한 체크 추가 
        SpecialBingoCheck();

        Invoke ("CheckNewClearBingo", 0.5f);
	}

    /// <summary>
    /// 특수 빙고 체크 
    /// </summary>
    public void SpecialBingoCheck() {

        // 뱃지 정보 
        JSONNode badgeinfo = GameSystem.Instance.GetNekoMedal();

        // BingoID 7보다 작은 곳에서는 사용하지 않음 
        if (GameSystem.Instance.UserJSON["currentbingoid"].AsInt < 7) {
            return;
        }

        // BingoID 7 클리어체크
        if (GameSystem.Instance.UserJSON["currentbingoid"].AsInt == 8) {
            if(GameSystem.Instance.CheckBingoClear(7)) {
                GameSystem.Instance.CheckLobbyBingoQuest(81, 1, true);
            }
        }

        // BingoID 8 클리어 체크 
        if (GameSystem.Instance.UserJSON["currentbingoid"].AsInt == 9) {
            if (GameSystem.Instance.CheckBingoClear(8)) {
                GameSystem.Instance.CheckLobbyBingoQuest(87, 1, true);
            }
        }

        // BingoID 9 클리어 체크 
        if (GameSystem.Instance.UserJSON["currentbingoid"].AsInt == 10) {
            if (GameSystem.Instance.CheckBingoClear(9)) {
                GameSystem.Instance.CheckLobbyBingoQuest(92, 1, true);
            }
        }

        // Silver Badge 체크
        GameSystem.Instance.CheckLobbyBingoQuest(86, badgeinfo["silver"].AsInt, true);


        // Gold Badge 체크 
        GameSystem.Instance.CheckLobbyBingoQuest(93, badgeinfo["gold"].AsInt, true);

    }



    private void PlayUINegative() {
        effectSrc.PlayOneShot(SoundConstBox.acUINegative);
    }

    private void CheckEscapeControl() {
        if (StackPopup.Count > 0) {
            CloseStack();
        }
        else if (StackPopup.Count == 0 && _objGatchaScreen.activeSelf) {

            return; // 아무것도 하지 않음 


        }
        else {
            // Exit 팝업 발생 
            PopupExit();
        }
    }

    /// <summary>
    /// Popups the exit.
    /// </summary>
    private void PopupExit() {
        OpenInfoPopUp(PopMessageType.ApplicationQuit);
    }


    #region 상위 레이어 팝업 

    /// <summary>
    /// 일반 안내 팝업 오픈 
    /// </summary>
    /// <param name="pType"></param>
    public void OpenUpperInfoPopUp(PopMessageType pType) {

        Debug.Log("▶ OpenUpperInfoPopUp MessageType :: " + pType.ToString());

        _objUpperPopup.gameObject.SetActive(true);

        // 콜백 연계
        if (pType == PopMessageType.GoldPurchase) { // 골드 구매 
            _objUpperPopup.SetInfoMessage(pType, CloseGoldPurchaseConfirm);
        }
        else if (pType == PopMessageType.HeartZero) { // 하트가 부족함
            _objUpperPopup.SetInfoMessage(pType, OpenHeartShop);
        }

        else if (pType == PopMessageType.ShortageGemForGatcha || pType == PopMessageType.GemShortage) { // 젬 부족!
            _objUpperPopup.SetInfoMessage(pType, OpenJewelShop);
        }
        else if (pType == PopMessageType.GoldShortage || pType == PopMessageType.NeedGoldPurchase || pType == PopMessageType.ShortageGoldForItem) { // 코인 부족!
            _objUpperPopup.SetInfoMessage(pType, OpenGoldShop);
        }
        else if (pType == PopMessageType.ApplicationQuit) {
            _objUpperPopup.SetInfoMessage(pType, ExitGame);
        }
        else if (pType == PopMessageType.Logout) {
            _objUpperPopup.SetInfoMessage(pType, LogoutGame);
        }
        else if (pType == PopMessageType.RateForGem) {
            _objUpperPopup.SetInfoMessage(pType, RewardRate);
        }
        else if (pType == PopMessageType.NekoGiftWithAdsConfirm) {
            _objUpperPopup.SetInfoMessage(pType, GetAdsNekoGift);
        }
        else if (pType == PopMessageType.ConfirmFishGatchaOne) {
            _objUpperPopup.SetInfoMessage(pType, FishingSingle);
        }
        else if (pType == PopMessageType.ConfirmFishGatchaTen) {
            _objUpperPopup.SetInfoMessage(pType, FishingMulti);
        }
        else if (pType == PopMessageType.ReLogin) { // 타이틀로 이동 처리
            _objUpperPopup.SetInfoMessage(pType, GameSystem.Instance.LoadTitleScene);
        }
        else if (pType == PopMessageType.CompleteDataTranfer) { // 타이틀로 이동 처리(초기화 포함)
            _objUpperPopup.SetInfoMessage(pType, GameSystem.Instance.LoadTitleSceneWithInitialize);
        }
        else if (pType == PopMessageType.NickNameChanged) {
            _objUpperPopup.SetInfoMessage(pType, RefreshOptionUserInfo);
        }
        else if (pType == PopMessageType.ReadyToFreeGatcha) {
            _objUpperPopup.SetInfoMessage(pType, _gatchaConfirmCtrl.ShowFreeCraneAd);
        }
        else {
            _objUpperPopup.SetInfoMessage(pType, null);
        }


    }

    /// <summary>
    /// 안내 팝업 오픈(변수 추가)
    /// </summary>
    /// <param name="pType"></param>
    /// <param name="pValue"></param>
    public void OpenUpperInfoPopUp(PopMessageType pType, string pValue) {

        Debug.Log("▶ OpenUpperInfoPopUp Confirm MessageType  :: " + pType.ToString());


        _objUpperPopup.gameObject.SetActive(true);

        // 생선가게 
        if (pType == PopMessageType.BandChubAdd || pType == PopMessageType.BandSalmonAdd || pType == PopMessageType.BandTunaAdd) {
            _objUpperPopup.SetConfirmMessage(pType, pValue, null);
        }

        else if (pType == PopMessageType.ShortageGemForGatcha || pType == PopMessageType.GemShortage) { // 젬 부족!
            _objUpperPopup.SetConfirmMessage(pType, pValue, OpenJewelShop);
        }
        else if (pType == PopMessageType.GoldShortage || pType == PopMessageType.NeedGoldPurchase || pType == PopMessageType.ShortageGoldForItem) { // 코인 부족!
            _objUpperPopup.SetConfirmMessage(pType, pValue, OpenGoldShop);
        }

        else {
            _objUpperPopup.SetConfirmMessage(pType, pValue, UpdateTopInformation);
        }
    }

    #endregion

    /// <summary>
    /// 메세지창 중복 띄우기
    /// </summary>
    /// <param name="pType"></param>
    /// <param name="pValue"></param>
    public void OpenInfoPopUpDuplicate(PopMessageType pType, string pValue) {


        _objSimplePopup.SetActive(true);

        if (pType == PopMessageType.HeartAdd || pType == PopMessageType.GemAdd || pType == PopMessageType.GoldAdd
            || pType == PopMessageType.ChubAdd || pType == PopMessageType.TunaAdd || pType == PopMessageType.SalmonAdd
            || pType == PopMessageType.NekoAdd) {

            // 상단 업데이트 먼저 실시. 
            UpdateTopInformation();

        }

        _objSimplePopup.GetComponent<SimplePopupCtrl>().SetConfirmMessage(pType, pValue, null);


    }


    /// <summary>
    /// 일반 안내 팝업 오픈 
    /// </summary>
    /// <param name="pType"></param>
    public void OpenInfoPopUp(PopMessageType pType) {

        Debug.Log("▶ MessageType :: " + pType.ToString());

        if(_jewelShop.activeSelf || _goldShop.activeSelf) {
            OpenUpperInfoPopUp(pType);
            return;
        }


        _objSimplePopup.SetActive(true);

        // 콜백 연계
        if (pType == PopMessageType.GoldPurchase) { // 골드 구매 
            _objSimplePopup.GetComponent<SimplePopupCtrl>().SetInfoMessage(pType, CloseGoldPurchaseConfirm);
        }
        else if (pType == PopMessageType.HeartZero) { // 하트가 부족함
            _objSimplePopup.GetComponent<SimplePopupCtrl>().SetInfoMessage(pType, OpenHeartShop);
        }
        else if (pType == PopMessageType.ShortageGemForGatcha || pType == PopMessageType.GemShortage) { // 젬 부족!
            _objSimplePopup.GetComponent<SimplePopupCtrl>().SetInfoMessage(pType, OpenJewelShop);
        }
        else if (pType == PopMessageType.GoldShortage || pType == PopMessageType.NeedGoldPurchase || pType == PopMessageType.ShortageGoldForItem) { // 코인 부족!
            _objSimplePopup.GetComponent<SimplePopupCtrl>().SetInfoMessage(pType, OpenGoldShop);
        }
        else if (pType == PopMessageType.ApplicationQuit) {
            _objSimplePopup.GetComponent<SimplePopupCtrl>().SetInfoMessage(pType, ExitGame);
        }
        else if (pType == PopMessageType.Logout) {
            _objSimplePopup.GetComponent<SimplePopupCtrl>().SetInfoMessage(pType, LogoutGame);
        }
        else if (pType == PopMessageType.RateForGem) {
            _objSimplePopup.GetComponent<SimplePopupCtrl>().SetInfoMessage(pType, RewardRate);
        }
        else if (pType == PopMessageType.NekoGiftWithAdsConfirm) {
            _objSimplePopup.GetComponent<SimplePopupCtrl>().SetInfoMessage(pType, GetAdsNekoGift);
        }
        else if (pType == PopMessageType.ConfirmFishGatchaOne) {
            _objSimplePopup.GetComponent<SimplePopupCtrl>().SetInfoMessage(pType, FishingSingle);
        }
        else if (pType == PopMessageType.ConfirmFishGatchaTen) {
            _objSimplePopup.GetComponent<SimplePopupCtrl>().SetInfoMessage(pType, FishingMulti);
        }
        else if (pType == PopMessageType.ReLogin) { // 타이틀로 이동 처리
            _objSimplePopup.GetComponent<SimplePopupCtrl>().SetInfoMessage(pType, GameSystem.Instance.LoadTitleScene);
        }
        else if (pType == PopMessageType.CompleteDataTranfer) {  // 초기화와 함께 타이틀 이동처리
            _objSimplePopup.GetComponent<SimplePopupCtrl>().SetInfoMessage(pType, GameSystem.Instance.LoadTitleSceneWithInitialize);
        }
        else if(pType == PopMessageType.NickNameChanged) {
            _objSimplePopup.GetComponent<SimplePopupCtrl>().SetInfoMessage(pType, RefreshOptionUserInfo);
        }
        else if (pType == PopMessageType.ReadyToFreeGatcha) {
            _objSimplePopup.GetComponent<SimplePopupCtrl>().SetInfoMessage(pType, _gatchaConfirmCtrl.ShowFreeCraneAd);
        }
        else {
            _objSimplePopup.GetComponent<SimplePopupCtrl>().SetInfoMessage(pType, null);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="pType"></param>
    /// <param name="pCallback"></param>
    public void OpenInfoPopUp(PopMessageType pType, Action pCallback) {

        _objSimplePopup.GetComponent<SimplePopupCtrl>().SetInfoMessage(pType, pCallback);

    }

    public void OpenUpperInfoPopUp(PopMessageType pType, Action pCallback) {

        _objUpperPopup.GetComponent<SimplePopupCtrl>().SetInfoMessage(pType, pCallback);

    }



    /// <summary>
    /// 안내 팝업 오픈(변수 추가)
    /// </summary>
    /// <param name="pType"></param>
    /// <param name="pValue"></param>
    public void OpenInfoPopUp(PopMessageType pType, string pValue) {

        Debug.Log("▶ Confirm MessageType  :: " + pType.ToString());

        if (_jewelShop.activeSelf || _goldShop.activeSelf) {
            OpenUpperInfoPopUp(pType, pValue);
            return;
        }


        _objSimplePopup.SetActive(true);

        // 생선가게 
        if (pType == PopMessageType.BandChubAdd || pType == PopMessageType.BandSalmonAdd || pType == PopMessageType.BandTunaAdd) {
            _objSimplePopup.GetComponent<SimplePopupCtrl>().SetConfirmMessage(pType, pValue, null);
        } 

        else if (pType == PopMessageType.ShortageGemForGatcha || pType == PopMessageType.GemShortage) { // 젬 부족!
            _objSimplePopup.GetComponent<SimplePopupCtrl>().SetConfirmMessage(pType, pValue, OpenJewelShop);
        }
        else if (pType == PopMessageType.GoldShortage || pType == PopMessageType.NeedGoldPurchase || pType == PopMessageType.ShortageGoldForItem) { // 코인 부족!
            _objSimplePopup.GetComponent<SimplePopupCtrl>().SetConfirmMessage(pType, pValue, OpenGoldShop);
        }

        else {
            _objSimplePopup.GetComponent<SimplePopupCtrl>().SetConfirmMessage(pType, pValue, UpdateTopInformation);
        }
    }



    /// <summary>
    /// 메세지 창 닫힘 Callback  
    /// </summary>
    /// <param name="pType">P type.</param>
    public void OnConfirmCompletedSimplePopup(PopMessageType pType) {

        Debug.Log(">>> OnConfirmCompletedSimplePopup type :: " + pType.ToString());

        _objSimplePopup.SendMessage("CloseSelf");
    }



    /// <summary>
    /// 평가하기 (보상)
    /// </summary>
    private void RewardRate() {

        // 패킷 전송
        #if UNITY_ANDROID
        //GameSystem.Instance.Post2Rating();
        #endif

        GameSystem.Instance.OpenGoogleMarketURL();
    }


    /// <summary>
    /// 로그아웃
    /// </summary>
    private void LogoutGame() {
        #if UNITY_ANDROID
        GooglePlayConnection.Instance.Disconnect();
        #elif UNITY_IOS

        #endif
        // 타이틀로 이동
        Fader.Instance.FadeIn(0.5f).LoadLevel("SceneTitle").FadeOut(1f);
    }

    /// <summary>
    /// 옵션 유저정보 
    /// </summary>
    private void RefreshOptionUserInfo() {
        //SetUserInfo
        if (_objOptionGroup.gameObject.activeSelf) {
            _objOptionGroup.gameObject.SendMessage("SetUserInfo");
        }
    }




    private void ShowNekoGiftAd() {
        GameSystem.Instance.CheckAd(AdsType.NekoGiftAds);
        
    }

    /// <summary>
    /// 프리 뽑기 화면 오픈 
    /// </summary>
    public void OpenFreeCrane() {
        _gatchaConfirmCtrl.OpenFreeCraneScreen();
    }

    /// <summary>
    /// 게임 종료
    /// </summary>
    private void ExitGame() {
        GameSystem.Instance.ExitGame();
    }


    /// <summary>
    /// 생선뽑기 창 관련 
    /// </summary>
    private void FishingSingle() {
        GameSystem.Instance.Post2FishGatcha(1);
    }

    private void FishingMulti() {
        GameSystem.Instance.Post2FishGatcha(10);
    }

    /// <summary>
    /// 티켓리스트 오픈 
    /// </summary>
    /// <param name="pNode"></param>
    public void OpenNekoTicketList(JSONNode pNode, int pMailKey, MailColumnCtrl pMail, Action pCallback) {
        _nekoticketList.SetNekoTicketExchangeView(pNode, pMailKey, pMail, pCallback);
    }



    




    #endregion

    

    #region 기타 기능



    /// <summary>
    /// 핫타임 표시 기능 
    /// </summary>
    /// <param name="pFlag"></param>
    public void SetHotTimeMark(bool pFlag) {
        //_objHotTimeMark.SetActive(pFlag);
    }


    public void OpenBingo() {

		Debug.Log ("▶▶  LobbyCtrl OpenBingo currentbingoid :: " + GameSystem.Instance.UserJSON["currentbingoid"].AsInt);

        if(_btnBingo.normalSprite == PuzzleConstBox.spriteLockBotIcon) {
            LobbyCtrl.Instance.OpenInfoPopUp(PopMessageType.BingoLock);
            return;
        }


        BingoMasterCtrl.Instance.OpenBingo();




        // 2016.11.08 업데이트에서 변경
        /*
        if (_isUnlockingBingo)
            GameTip.SetGameTip(TipType.Bingo);
        else
            BingoMasterCtrl.Instance.OpenBingo();
        */
        


        /*
        // 빙고 팁이 해제가 안되어있으면 팁부터 띄운다. 
        if(!GameSystem.Instance.LoadESvalueBool(PuzzleConstBox.ES_UnlockBingoTip)) {
            // 빙고 팁 화면 오픈
            GameTip.SetGameTip(TipType.Bingo); // 팁화면에서 Bingo를 실행한다. 

            // Unlock 
            GameSystem.Instance.Post2Unlock("bingo_tip");
            return;
        }
        else {
            // 도전 빙고를 선택했는지 여부에 따른 분기 
            if(GameSystem.Instance.UserJSON["currentbingoid"].AsInt >= 0) {
				Debug.Log ("LobbyCtrl OpenBingo #1");
                BingoMasterCtrl.Instance.OpenBingo(); // 도전 중이면 그냥 빙고 열림 
            }
            else { // 도전중이지 않으면 메세지 창 오픈 
				//PopPopup
				Debug.Log ("LobbyCtrl OpenBingo #2");
				//CloseStack();
				OpenUpperInfoPopUp(PopMessageType.BingoNeed, BingoMasterCtrl.Instance.OpenBingo);
            }
        }
        */

        //BingoMasterCtrl.Instance.OpenBingo(GameSystem.Instance.BingoJSON);

    }

    




    /// <summary>
    /// 준비창에서 선택된 아이템들의 코인 값.
    /// </summary>
    /// <returns></returns>
    public int GetSelectedEquipItemCoinValue() {
        if (objReadyGroup.gameObject.activeSelf)
            return objReadyGroup.GetSelectedEquipItemCoinValue();
        else
            return 0;
    }

    public void OpenNekoEvolution(int pNekoID, int pPreviousGrade, int pCurrentGrade) {
        objNekoEvolution.SetNekoEvolution(pNekoID, pPreviousGrade, pCurrentGrade);
    }

    public void OnWaitingRequest() {
		objWaitingRequest.SetActive (true);
	}

	public void OffWaitingRequest() {
		objWaitingRequest.SetActive (false);
	}


    public bool GetWaingRequestEnable() {

        Debug.Log("GetWaingRequestEnable :: " + objWaitingRequest.activeSelf.ToString());


        return objWaitingRequest.activeSelf;
    }

    #endregion

	#region Option

	public void OpenOption() {
        bigPopup.gameObject.SetActive(true);
        bigPopup.SetOption();
    }



	#endregion

	#region 캐릭터 뽑기

	/// <summary>
	/// 가챠창 진입. 
	/// </summary>
	public void OpenGatchaConfirm() {

		// 튜토리얼 진행시에는 창을 물어보지 않음. 
		if (GameSystem.Instance.TutorialStage >= 10) {

			Debug.Log ("!! Tutorial! Gatcha!");

			OpenGatchaScreen(false);
			return;
		}

		// 새로운 팝업으로 대체 
		// 무료 뽑기(광고), 유료 뽑기 안내 팝업
		WindowManagerCtrl.Instance.OpenGatchaConfirm ();

    }
	

    /// <summary>
    /// 튜토리얼 가챠 화면 오픈 
    /// </summary>
    public void OpenTutorialGatchaScreen() {

        GameSystem.Instance.IsFreeGatcha = false;
        GameSystem.Instance.Post2Gatcha();
    }

	/// <summary>
	/// 가챠 화면 오픈 
	/// </summary>
	public void OpenGatchaScreen(bool pIsFree) {


        //ClearLobbyNeko();

        // 튜토리얼 여부에 따른 분기 
        if (GameSystem.Instance.TutorialStage >= 10) { // 튜토리얼인 경우 다른 Post
			GameSystem.Instance.Post2TutorialGatcha();
		} else {
			GameSystem.Instance.Post2Gatcha();
		}
	}


    /// <summary>
    /// 캐릭터 뽑기 통신 완료 후 화면 오픈 
    /// </summary>
    public void OnCompletePostGatchaRequest() {

        Debug.Log("OnCompletePostGatchaRequest");

        // 튜토리얼 처리 (JP) 2016.04
        if (GameSystem.Instance.TutorialStage == 0 && GameSystem.Instance.LocalTutorialStep < 1) {
            ClearBottleNeko();
            GameSystem.Instance.GatchaCount = 1;
            GameSystem.Instance.SaveLocalTutorialStep(1); // 1로 변경
        }

        ClearBottleNeko();
        // 일부 UI 비활성화 
        DisableUIForGatcha();

        // 현재 로비 룸 Index에 따른 위치를 조정 한다. 
        //_objGatchaScreen.transform.position = new Vector3(GameSystem.Instance.SavedLobbyIndex * 7.2f, 0, 0);
        _objGatchaScreen.gameObject.SetActive(true);

        // InitializeGatcha에 정보 전달. 
        if (GameSystem.Instance.GatchaCount == 1)
            _objGatchaScreen.GetComponent<NekoGatchaCtrl>().InitializeGatcha(true);
        else
            _objGatchaScreen.GetComponent<NekoGatchaCtrl>().InitializeGatcha(false);


        // 빙고 처리 (무료 크레인 이용)
        if (!GameSystem.Instance.IsFreeGatcha) {
            GameSystem.Instance.CheckLobbyBingoQuest(51); // 보석 사용
            GameSystem.Instance.CheckLobbyBingoQuest(52); // 스페셜 크레인 
        }

        // 튜토리얼 처리 
        /*
		if(GameSystem.Instance.TutorialStage == 10) 
			GameSystem.Instance.TutorialStage = 11; 
        */
    }


    private void ClearBottleNeko() {

        BottleManager.Instance.HideNBottleeko();


        for (int i = 0; i < arrGatchaNekoAppear.Length; i++) {
            arrGatchaNekoAppear[i].localPosition = new Vector3(800, 0, 0);
        }
        gatchaNekoSet.SetActive(true);
    }




	/// <summary>
	/// Opens the event gatcha.
	/// </summary>
	/// <param name="pEventNo">P event no.</param>
	public void OpenEventGatcha(int pEventNo) {
		GameSystem.Instance.Post2EventGatcha(pEventNo);
	}









    /// <summary>
    ///  가챠 종료
    /// </summary>
    public void CloseGatchaScreen() {

        Debug.Log(">>> CloseGatchaScreen");

        EnableUIAfterGatcha();
        gatchaNekoSet.SetActive(false);

        if (_objGatchaScreen.GetComponent<NekoGatchaCtrl>().IsTicketGacha) {
            GameSystem.Instance.UpdateSingleNekoData(_objGatchaScreen.GetComponent<NekoGatchaCtrl>().TicketNode);
            CheckBGM();
        }
        else {
            GameSystem.Instance.UpdateNekoAfterGatcha(GameSystem.Instance.GatchaData);
            PlayLobbyBGM();
        }

        // 스크린 클로즈. 
        _objGatchaScreen.gameObject.SetActive(false);

        Debug.Log(">>> CloseGatchaScreen _objGatchaScreen.gameObject.SetActive(false)");


        // 튜토리얼 처리
        if (GameSystem.Instance.LocalTutorialStep == 1)
            GameSystem.Instance.SaveLocalTutorialStep(2);

        Debug.Log(">>> CloseGatchaScreen ShowBottleNeko");
        BottleManager.Instance.ShowBottleNeko();


        Debug.Log(">>> CloseGatchaScreen CheckNewClearBingo");
        CheckNewClearBingo();


        try {

            /* 리더보드 제출 고양이를 모은 개수  */
#if UNITY_ANDROID
            GameSystem.Instance.SubmitLeaderBoardScore(GameSystem.Instance.GetUserNekoCount());
#elif UNITY_IOS
			GameSystem.Instance.SubmitLeaderBoardScore((long)GameSystem.Instance.GetUserNekoCount());
#endif

            // 업적 체크
            Debug.Log(">>> CloseGatchaScreen CheckAchievements");
            GameSystem.Instance.CheckAchievements();
        }
        catch(System.Exception e) {
            Debug.Log("★ e ::" + e.StackTrace);
        }
        





    }

    /// <summary>
    /// 가챠 화면 등장시 UI Disable
    /// </summary>
    private void DisableUIForGatcha() {
		topPanel.SetActive (false);
		LobbyPanel.SetActive (false);
        StagePanel.SetActive(false);
		
	
	}

    private void DisableTopLobbyUI() {
        topPanel.SetActive(false);
        LobbyPanel.SetActive(false);
        StagePanel.SetActive(false);
    }

    private void EnableTopLobbyUI() {
        topPanel.SetActive(true);
        LobbyPanel.SetActive(true);
        StagePanel.SetActive(true);
    } 

	
	private void EnableUIAfterGatcha() {
		
		
		topPanel.SetActive (true);
		LobbyPanel.SetActive (true);
        StagePanel.SetActive(true);

        gatchaResultSet.SetActive(false);
		
	}


    #endregion

    #region SceneTip

    public void OpenDelayedSceneTip() {
        Invoke("OpenSceneTip", 0.5f);
    }

    void OpenSceneTip() {
        _sceneTip.SetSceneTip();
    }

    public void CloseSceneTip() {
        _sceneTip.CloseSceneTip();
    }

    #endregion

    #region 레디 창 관련 기능

    /// <summary>
    /// 스테이지 레디 화면 오픈 
    /// </summary>
    /// <param name="pStage"></param>
    public void OpenReady(int pStage) {
        

        objReadyGroup.gameObject.SetActive(true);
        objReadyGroup.SetStageGame(pStage);

        /*
        for (int i = 0; i < 3; i++) {
            arrEquipNeko[i].SetNekoInfo(GameSystem.Instance.ListEquipNekoID[i]);
        }

        // 튜토리얼 처리
        if (GameSystem.Instance.LocalTutorialStep == 2) {
            DisableAllButton();

            // 첫번째 칸 활성화처리
            arrEquipNeko[0].GetComponent<UIButton>().enabled = true;

            Debug.Log("First Equip Neko Enable");
        }

        */


        //CheckReadyUnlock();
    }







    #endregion

    #region Notice List 세팅

    /// <summary>
    /// 공지사항 리스트 초기화 
    /// </summary>
    private void SetNoticeList() {

        ListNoticeSmallBanner.Clear();

        NoticeSmallBannerCtrl noticeSmallBanner;

        for(int i=0; i<GameSystem.Instance.NoticeBannerInitJSON.Count;i++) {
            noticeSmallBanner = PoolManager.Pools["SmallNoticePool"].Spawn("NoticeSmallBanner").GetComponent<NoticeSmallBannerCtrl>();
            ListNoticeSmallBanner.Add(noticeSmallBanner);
        }

        // 비활성화 처리 
        ClearNoticeSmallBanner();
    }

    private void ClearNoticeSmallBanner() {
        for (int i = 0; i < ListNoticeSmallBanner.Count; i++) {
            ListNoticeSmallBanner[i].gameObject.SetActive(false);
        }

    }


    #endregion

    #region 파티클 시스템 사용 

    /// <summary>
    /// 하트 사용시 파티클 효과
    /// </summary>
    /// <param name="pos">Position.</param>
    public void ParticleUseHeart(Vector3 pPos) {
		PoolManager.Pools [PuzzleConstBox.lobbyParticlePool].Spawn (GameSystem.Instance.particleUseHeart, pPos, Quaternion.identity);
	}

	public void ParticleEquipItem(Vector3 pos) {
		PoolManager.Pools [PuzzleConstBox.lobbyParticlePool].Spawn (GameSystem.Instance.particleItemEquip, pos, Quaternion.identity);
	}

	public void ParticleNekoRewardGet(Vector3 pos) {
		PoolManager.Pools [PuzzleConstBox.lobbyParticlePool].Spawn (GameSystem.Instance.particleNekoRewardGet, pos, Quaternion.identity);
		PlayNekoRewardGet (); // 사운드 
	}
	
	#endregion

	#region Properties



	public bool IsWaitingTab {
		get {
			return this._isWaitingTab;
		}
		set {
			this._isWaitingTab = value;
		}
		
		
	}

    public NekoAppearCtrl GatchaNekoAppear {
        get {
            return _gatchaNekoAppear;
        }

        set {
            _gatchaNekoAppear = value;
        }
    }

    public TutorialHandCtrl TutorialHand {
        get {
            return _tutorialHand;
        }

        set {
            _tutorialHand = value;
        }
    }

    public List<NoticeSmallBannerCtrl> ListNoticeSmallBanner {
        get {
            return _listNoticeSmallBanner;
        }

        set {
            _listNoticeSmallBanner = value;
        }
    }

    public bool OnResultShow {
        get {
            return _onResultShow;
        }

        set {
            _onResultShow = value;
        }
    }

    public GTipCtrl GameTip {
        get {
            return _gameTip;
        }

        set {
            _gameTip = value;
        }
    }

    public GameObject ObjGatchaScreen {
        get {
            return _objGatchaScreen;
        }

        set {
            _objGatchaScreen = value;
        }
    }

    public BigPopupCtrl BigPopup {
        get {
            return bigPopup;
        }

        set {
            bigPopup = value;
        }
    }

    public Stack<LobbyCommonUICtrl> StackPopup {
        get {
            return _stackPopup;
        }

        set {
            _stackPopup = value;
        }
    }

    public GameObject GatchaResultSet {
        get {
            return gatchaResultSet;
        }

        set {
            gatchaResultSet = value;
        }
    }

    public NewNekoEventCtrl NewNekoEventButton {
        get {
            return _newNekoEventButton;
        }

        set {
            _newNekoEventButton = value;
        }
    }

    public GameObject ObjHotTimeMark {
        get {
            return _objHotTimeMark;
        }

        set {
            _objHotTimeMark = value;
        }
    }

    public bool IsTouchLock {
        get {
            return isTouchLock;
        }

        set {
            isTouchLock = value;
        }
    }





    #endregion

}
