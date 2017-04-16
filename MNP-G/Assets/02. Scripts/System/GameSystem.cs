using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using IgaworksUnityAOS;
using DG.Tweening;
using CodeStage.AntiCheat.ObscuredTypes;
using UnityEngine.Advertisements;

using Google2u;

public partial class GameSystem : MonoBehaviour {


    static GameSystem _instance = null;
    public bool isInitialize = false;
    
    public bool IgawInitialized = false;
    private bool _isLiveOpsInit = false;


    private bool _isEnterLobbyCompleted = false; // 첫 로비 진입이 완료 되었는지 체크 
    private bool _isOnGameResult = false; // 게임 결과창을 띄워야 하는지? 

    private bool _isConnectedServer = false; // 첫 인증 완.
    [SerializeField] bool _isAdminUser = false; // Admin 유저 


    [SerializeField] int _playStage = 0;

    GooglePurchaseTemplate _currentPurchase;

    // 안드로이드 구글 현재 유저.
    GooglePlayerTemplate _currentPlayer = null;


    // 데이터 이전 
    string _dataCode = null;
    long _dataCodeExpiredTime = 0;



    [SerializeField] string _deviceID = null; // 디바이스 아이디  
    [SerializeField] string _userID = null; // 사용자 아이디 
    [SerializeField] string _userName = ""; // 사용자 이름 
    [SerializeField] string _platform = "android"; // ios or android
    [SerializeField] string _facebookid = string.Empty; // 페이스북 ID
    
    [SerializeField] int _gatchaCount = 1;


    [SerializeField] int _lastActiveHour = 0;
    [SerializeField] int _preLastActiveHour = 0;
    [SerializeField] int _lastActiveDay = 0;
    [SerializeField] int _preLastActiveDay = 0;

    private bool _iosAuthed = false; // ios 게임센터 연동 여부
    private int _fishGatchaCount = 0;


    // 튜토리얼 
    TextAsset _text;
    private int tutorialStage = -1; // 로비 튜토리얼 사용 용도 
    [SerializeField] private int _localTutorialStep = 0; // 디바이스에 저장되는 튜토리얼 단계
    public int _explainTextIndex = 3131; // 3106번 부터 시작 


    // System 기본 값 
    private readonly Vector3 _baseScale = new Vector3(1, 1, 1);
    private readonly Vector3 _coinDestPos = new Vector3(-3f, 5.9f, 0);

    // 옵션값 
    bool _optionSoundPlay = true; // 사운드 플레이 여부 
    bool _optionBGMPlay = true; // BGM 사운드 플레이 여부 
    bool _optionHeartPush = true; // 하트 푸시 여부
    bool _optionFreeCranePush = true; // 프리크레인 푸시 여부
    bool _optionRemotePush = true; // 

    [SerializeField]
    bool _optionPuzzleTip = false; // 퍼즐 가이드 표시 0.5초 여부  (false가 초보자, true는 숙련자)

    

    // 시간값 (유니티 애즈)
    AdsType CurrentAdsType;


    private long _possibleAdsTimeTick;
    private System.DateTime _possibleAdsTime;
    private System.TimeSpan _remainAdsTimeSpan;

    [SerializeField]
    bool _recordUse = false; // 사용자 실적 정보 사용 여부 

    DateTime _nextCoinHotTime;
    DateTime _nextFreeCraneResetTime;
    TimeSpan _remainNextCoinHotTime;

    #region WakeUp Time
    TimeSpan _remainWakeUpTime;
    DateTime _dtRemainWakeUpTime;
    long _remainWakeUpTimeTick;

    bool _isRequestingWakeUp = false;
    #endregion

    private bool _isWaitingForAds = false;
    private readonly long _fixAddTick = 621355968000000000; // javascript와 C#의 타임 갭을 없애주는 변수 
    private long _timeGapBetweenServerClient;

    // Block Generate Setting
    private int _height, _width;
    private float _blockStartX, _blockStartY; // 0,0 위치  블록의 최초 생성 위치 
    private int _blockTypeCount; // 일반 블록의 종류 
    private int _fillBlockCount; // 스테이지에 채워지는 블록의 Max 개수
    private int _activeBlockCount; 
    private float _blockScaleValue; // 생성 블록의 크기 값 
    private Vector3 _blockScale; // 생성 블록 Vector3 크기 값 


    // Balancing Data
    private ObscuredFloat _blockAttackPower; // 블록 한개의 공격력 
    private ObscuredFloat _ingamePlayTime; // 인게임 플레이 타임 
    private ObscuredInt _inGameComboIndex = 15; // 노멀에서 피버로 전환되는 콤보 카운트 
    private ObscuredFloat _comboKeepTime; // 콤보 유지 시간 
    private ObscuredFloat _intervalMinusComboTime; // 콤보 감소 시간
    private ObscuredFloat _minumumComboKeepTime; // 콤보 최소 유지 시간
    private ObscuredFloat _feverPlayTime;   // 피버 유지 시간 





    // Google Drive Download 
    MNP_Neko _NekoInfo = MNP_Neko.Instance; // 네코 기준 정보 
    MNP_NekoSkill _NekoSkill = MNP_NekoSkill.Instance; // 네코 스킬 정보 
    MNP_NekoSkillValue _SkillValue = MNP_NekoSkillValue.Instance; // 네코 스킬 값
    
      


    // Load Scene
    private AsyncOperation asyncOp;
    private bool isLoadGame = false;

    


    #region Neko Atlas 

    // UI Sprite Atlas
    [SerializeField] UIAtlas globalCatCollection1;
    [SerializeField] UIAtlas globalCatCollection2;

 	public UIAtlas comAtlas;

    public UIFont NormalStarFont;
    public UIFont OrangeStarFont;

    #endregion



    // 게임 획득 정보 
    #region 네코 그룹 
    bool _equipGroup1Neko = false; // 네코 그룹 장착 여부 
    bool _equipGroup2Neko = false;
    bool _equipGroup3Neko = false;
    bool _equipGroup4Neko = false;
    bool _equipGroup5Neko = false;
    bool _equipGroup6Neko = false;
    bool _equipGroup7Neko = false;
    bool _equipGroup8Neko = false;
    bool _equipGroup9Neko = false;
    bool _equipGroup10Neko = false;
    bool _equipGroup11Neko = false;
    bool _equipGroup12Neko = false;
    bool _equipGroup13Neko = false;
    bool _equipGroup14Neko = false;
    bool _equipGroup15Neko = false;
    bool _equipGroup16Neko = false;
    bool _equipGroup17Neko = false;
    bool _equipGroup18Neko = false;
    bool _equipGroup19Neko = false;
    bool _equipGroup20Neko = false;
    bool _equipGroup21Neko = false;
    bool _equipGroup22Neko = false;
    bool _equipGroup23Neko = false;
    bool _equipGroup24Neko = false;
    bool _equipGroup25Neko = false;
    bool _equipGroup26Neko = false;
    bool _equipGroup27Neko = false;
    bool _equipGroup28Neko = false;
    bool _equipGroup29Neko = false;
    bool _equipGroup30Neko = false;
    bool _equipGroup31Neko = false;

    bool _equipLevel10Neko = false;
	bool _equipLevel20Neko = false;
    bool _equipLevel30Neko = false;
	bool _equipLevel50Neko = false; // over level 50 Neko
	bool _equipRank5Neko = false; // rank5 neko 

    #endregion
    
    float _userNekoBadgeBonus = 0;

    [SerializeField] int _matchedRedBlock = 0; // 매치한 각 블록의 수 
    [SerializeField] int _matchedBlueBlock = 0;
    [SerializeField] int _matchedYellowBlock = 0;
    [SerializeField] int _ingameSpecialAttackCount = 0; // 인게임 스페셜 어택 사용 횟수
    [SerializeField] int _ingameBombCount = 0; // 인게임 폭탄 사용 횟수 
    [SerializeField] int _ingameBlueBombCount = 0;
	[SerializeField] int _ingameYellowBombCount = 0;
	[SerializeField] int _ingameRedBombCount = 0;
	[SerializeField] int _ingameBlackBombCount = 0;
    [SerializeField] int _ingameMatchThreeCount = 0; // 한번에 3개 매치 
    [SerializeField] int _ingameMatchFourCount = 0; // 한번에 3개 매치 
    [SerializeField] int _ingameBlockCount = 0;
    [SerializeField] int _ingameRemainCookie = 0; // 남은 쿠키 블록 
    [SerializeField] int _ingameFishGrill = 0; // 생선굽기 카운트 

    [SerializeField] int _ingameBoardClearCount = 0;
    [SerializeField] int _ingameBoardPerfectClearCount = 0;

    bool _ingameDiamondPlay = false;

    [SerializeField] bool _inGameBoostItemUSE = false;

    [SerializeField] int _ingameMissCount = 0; // 미스 카운트 
    bool _ingameContinue10secUse = false;

    private ObscuredInt _inGameRescueNeko = 0;
    private ObscuredInt _inGameDestroyUFO = 0;
    private ObscuredInt _inGameMaxCombo; // 게임내 최대 콤보
    private ObscuredInt _inGameTotalCombo; // 게임내 콤보의 총합 
    private ObscuredInt _inGameScore;
    private ObscuredInt _inGamePlusScore;
    private ObscuredInt _inGameUserLevelBonusScore;
    private ObscuredInt _inGameBadgeBonusScore;

    [SerializeField] private bool _inGameStageClear = false;
    [SerializeField] bool _inGameStageUp = false;
    
    private int _inGameStageMissionCount = 0;

    [SerializeField] private ObscuredInt _inGameDamage;
    [SerializeField] private ObscuredFloat _floatInGameDamage; // float 계산 용도 
    [SerializeField] private ObscuredInt _inGameDiamond;
    [SerializeField] private ObscuredInt _inGameCoin;
    [SerializeField] private ObscuredInt _inGameTotalCoin;
    private ObscuredInt _inGameStage;
    private ObscuredInt _inGameTotalScore;
    

    private ObscuredInt _inGameChub; // 고등어 
    private ObscuredInt _inGameTicket; // 인게임 티켓
    private ObscuredInt _inGameTuna; // 참치 
    private ObscuredInt _inGameSalmon; // 연어

    [SerializeField] private float _inGamePauseTime;

    // 인게임 내 Passive skill 합산 
    [SerializeField] private float _nekoPowerPlus = 0; // 네코파워증가 
    [SerializeField] private float _nekoCoinPercent = 0; // 코인획득량 퍼센트
    [SerializeField] private float _nekoScorePercent = 0; // 점수획득량 퍼센트
    [SerializeField] private float _nekoGameTimePlus = 0; // 게임시간 증가

    [SerializeField] int _nekoBombAppearBonus = 0; // 폭탄게이지 감소 보너스 
    [SerializeField] int _nekoSkillInvokeBonus = 0; //네코 스킬 발동 감소 보너스
    [SerializeField] int _nekoStartBombCount = 0; // 시작 폭탄 개수 





    [SerializeField] PlayerOwnNekoCtrl _selectNeko = null; // 방금 선택한 고양이 정보.
    [SerializeField] int _previousSelectNekoID = -1; // 이전 선택한 고양이의 ID 

    

    // 사용자 정보 
    [SerializeField] int _userCurrentStage;
    [SerializeField] private ObscuredInt _userGold; // 사용자 골드 
    [SerializeField] private ObscuredInt _userGem; // 사용자 보석

    // 생선 
    [SerializeField] private ObscuredInt _userChub;
    [SerializeField] private ObscuredInt _userTuna;
    [SerializeField] private ObscuredInt _userSalmon;

    [SerializeField] private ObscuredInt _adsPoint;
    [SerializeField] int _exchangeGoldIndex = -1;



    [SerializeField] int _userBestScore;
    [SerializeField] int _userPreBestScore = -1;

    [SerializeField] private long _userNekorewardtime; // 네코의 보은 

    private DateTime _dtNextNekoRewardTime; // 다음 네코의 보은 시간
    private TimeSpan _remainNekoRewardTimeSpan; // 다음 네코의 보은 시간 계산 용도 

    
    [SerializeField] private bool _isHotTime = false;

    [SerializeField]
    int _userPowerLevel; //플레이어 파워 레벨


    [SerializeField] List<int> _listEquipNekoID = null;
    [SerializeField] List<int> _listEquipItemID = null; // 착용한 아이템 
    [SerializeField] List<int> _listPreEquipItemID = null; // 이전 게임에서 사용한 아이템 

    public List<NekoDamageInfo> ListNekoDamageInfo = null;


    

    // Fish 먹이주기. 
    [SerializeField] ObscuredInt _feedChub;
    [SerializeField] ObscuredInt _feedTuna;
    [SerializeField] ObscuredInt _feedSalmon;

    

    [SerializeField] int _remainfreegacha; // 프리가챠 제한 
    [SerializeField] int _remainstartfever; //시작피버 제한 
    int _remainheartcharge; // 하트 충전 제한
    [SerializeField] int _remainnekogift; // 프리미엄 네코의 선물 제한 



    int _updateBestScoreCount; // 한주간 베스트 스코어 갱신횟수 
    int _firstWeekConnectDay; // 매주 첫번째 플레이 갱신일자. 
    bool _firstWeekPlay; // 매주 첫번째 평가를 하였는지? 


    #region 가격정보 
    int _checkedBoostItemsPrice = 0;
    int _boostShieldPrice;
    int _boostBombPrice;
    int _boostCriticalPrice;
    int _boostStartFeverPrice;


    int _specialSingleGatchaPrice;
    int _specialMultiGatchaPrice;

    int _specialSingleFishingPrice;
    int _specialMultiFishingPrice;


    [SerializeField] List<int> _listCoinShopPrice;

    #endregion

    // 사운드 정보
    private AudioClip _lobbyBGM;
	private AudioClip _gatchaBGM;

	// 빌링
	private string _currentSKU = "";
		
	private int _gapMoney;
	private int _dividedGapMoney;


	private int _nekodbkey;
	public PlayerOwnNekoCtrl CurrentSelectNeko;

	// 네코의 보은 시간이 되어 대상 네코 정보를 갖고 있는지???
	// 초기화는 true로 해주고, 로비에서 고양이가 소환될때, false로  변경된다. 
	[SerializeField] bool _isNekoRewardReady = true;

    [SerializeField]
    int _adsID = 0;

	int _tempInt;

	// 게임내에서 사용되는 파티클 
	public ParticleSystem particleUseHeart; // 하트 소모 
	public ParticleSystem particleItemEquip; // 아이템 장착
	public ParticleSystem particleNekoRewardGet; // 네코의 보은 사용

	public ParticleSystem particleBombBoard; // 인게임 폭탄 
    public ParticleSystem particleCartoonFight;
    public ParticleSystem particleDeathGhost;

    readonly string _endBBCode = "[-]";
    readonly string _yellowBBCode = "[FFF55A]";

    public SystemLanguage GameLanguage;


	public static GameSystem Instance {

		get {
			if(_instance == null) {
				_instance = FindObjectOfType(typeof(GameSystem)) as GameSystem;

				if(_instance == null) {
					Debug.Log("Game System Init Error");
					return null;
				}
			}

			return _instance;
		}


	}

	void Awake() {

		//Debug.Log ("!! Awake in GameSystem :: " + Application.persistentDataPath);

        LoadLocalTutorialStep(); // 디바이스 튜토리얼 스텝 조회 

        // 사운드 Clip 로드
        LoadSoundResources ();
        LoadOptionSetting(); // 사운드 옵션 정보 조회 
        LoadPuzzleTipOption();

		//LoadPushOption (); // 푸시 옵션 정보 조회 

        DontDestroyOnLoad(this.gameObject);

	}

	void Start() {

        


        /* 가챠 배너 */
        _fileFishSmallBanner = Application.persistentDataPath + "/" + "small_fish_gatcha.png";
        _fileFreeSmallBanner = Application.persistentDataPath + "/" + "small_free_gatcha.png";
        _fileSpecialSmallBanner = Application.persistentDataPath + "/" + "small_special_gatcha.png";

        // 패키지 배너 
        _filePackageSmallBanner = Application.persistentDataPath + "/" + "package_small_banner"; // 뒤에 번호와 png가 붙는다.

        // 공지사항 배너 
        _fileNoticeSmallBanner = Application.persistentDataPath + "/" + "notice_small_banner"; // 뒤에 번호와 png가 붙는다.

        // 데이터 버전 정보 조회 
        LoadDataVersion();



        LoadPackageInfo(); // 패키지 정보 불러오기

        InitOS(); // 운영체제별 초기화 

        Initialize(); // 게임 시스템 초기화 

#if UNITY_ANDROID

        //IGAW 공통 모듈 연동 초기화
        InitAndroidIGWA();

#elif UNITY_IOS

		//IGAW 공통 모듈 연동 초기화 (ios)
		InitIosIGAW(); 

#endif


    }

    #region GameSystem 초기화 부분 

    /// <summary>
    /// Game System 초기화
    /// </summary>
    public void Initialize() {
        Debug.Log("▶ Game System Init");

        isInitialize = true;

        // 블록 생성 변수 세팅 
        SetBlockGenerateSetting();

        // 밸런싱 값 (시간, 데미지 등) 세팅 
        SetBalanceValue();


        // 인게임 획득정보 초기화 
        InitInGameAccquireInfo();

        // 사용자 준비창 정보 
        LoadEquipNekoInfo();

        // 사용자 정보, 밸런싱 값 조합으로 실제 게임에 사용될 값 수정 
        SetIngameValue();


    }

    /// <summary>
    /// 밸런싱 값 세팅 
    /// </summary>
    private void SetBalanceValue() {
        _ingamePlayTime = 60;
        _feverPlayTime = 6;

        _comboKeepTime = 3;
        _minumumComboKeepTime = 1.5f;
        _intervalMinusComboTime = 0.25f;



        _blockAttackPower = 5;
    }


    // 인게임 획득 정보 초기화 
    public void InitInGameAccquireInfo() {

        _equipGroup1Neko = false;
        _equipGroup2Neko = false;
        _equipGroup3Neko = false;
        _equipGroup4Neko = false;
        _equipGroup5Neko = false;
        _equipGroup6Neko = false;
        _equipGroup7Neko = false;
        _equipGroup8Neko = false;
		_equipGroup9Neko = false;
		_equipGroup10Neko = false;
		_equipGroup11Neko = false;
		_equipGroup12Neko = false;
		_equipGroup13Neko = false;
		_equipGroup14Neko = false;
		_equipGroup15Neko = false;
		_equipGroup16Neko = false;
		_equipGroup17Neko = false;
		_equipGroup18Neko = false;
		_equipGroup19Neko = false;
		_equipGroup20Neko = false;
		_equipGroup21Neko = false;
		_equipGroup22Neko = false;
		_equipGroup23Neko = false;
		_equipGroup24Neko = false;
		_equipGroup25Neko = false;
        _equipGroup26Neko = false;
        _equipGroup27Neko = false;
        _equipGroup28Neko = false;
        _equipGroup29Neko = false;
        _equipGroup30Neko = false;
        _equipGroup31Neko = false;


        _equipLevel10Neko = false;
		_equipLevel20Neko = false;
        _equipLevel30Neko = false;
        _equipLevel50Neko = false;
		_equipRank5Neko = false;

        _matchedBlueBlock = 0;
        _matchedRedBlock = 0;
        _matchedYellowBlock = 0;
        _ingameBombCount = 0;
        _ingameSpecialAttackCount = 0;
        _ingameBombCount = 0;
        _ingameBlueBombCount = 0;
		_ingameYellowBombCount = 0;
		_ingameRedBombCount = 0;
		_ingameBlackBombCount = 0;
        _ingameMatchThreeCount = 0;
        _ingameMatchFourCount = 0;
        _ingameBlockCount = 0;
        IngameRemainCookie = 0;
        IngameBoardClearCount = 0;
        IngameBoardPerfectClearCount = 0;
        IngameFishGrill = 0;
        
        _ingameMissCount = 0;
        _ingameContinue10secUse = false;

        _inGameMaxCombo = 0;
        _inGameTotalCombo = 0;
        _inGameScore = 0;
        _inGamePlusScore = 0;
        InGameUserLevelBonusScore = 0;
        InGameBadgeBonusScore = 0;
        _inGameDamage = 0;
        _floatInGameDamage = 0;
        _inGameDiamond = 0;
        _inGameCoin = 0;
        _inGameTotalCoin = 0;
        _inGameStage = 0;
        _inGameTotalScore = 0;
        _inGamePauseTime = 0;
        

        _inGameChub = 0;
        InGameTicket = 0;
        _inGameTuna = 0;
        _inGameSalmon = 0;

        _inGameRescueNeko = 0;
        _inGameDestroyUFO = 0;
        InGameStageMissionCount = 0;
        InGameBoostItemUSE = false;

        _inGameStageClear = false;
        InGameStageUp = false;
        

        _ingameMissCount = 0;
        
        _nekoPowerPlus = 0; // 네코파워증가 
        _nekoCoinPercent = 0; // 코인획득량 퍼센트
        _nekoScorePercent = 0; // 점수획득량 퍼센트
        _nekoStartBombCount = 0;
        _nekoGameTimePlus = 0; // 게임시간 증가
        _nekoSkillInvokeBonus = 0;
        _nekoBombAppearBonus = 0;
        

        _checkedBoostItemsPrice = 0;


    }

    /// <summary>
    /// 밸런싱 값 세팅 
    /// </summary>
    private void SetIngameValue() {


        //_feverPlayTime = _feverPlayTime + (_abilityFeverTimeLevel - 1) * _intervalFeverTime;
        _blockAttackPower = _userPowerLevel * 5;
        //_ingamePlayTime = _ingamePlayTime + (_abilityPlayTimeLevel - 1) * _intervalPlayTime;

        Debug.Log("▶▶▶ SetInGameValue _blockAttackPower :: " + _blockAttackPower);

    }


    /// <summary>
    /// 사운드 리소스 로딩 
    /// </summary>
    private void LoadSoundResources() {
        _lobbyBGM = Resources.Load("Sound/OutGame/LobbyBGM") as AudioClip;
        _gatchaBGM = Resources.Load("Sound/OutGame/GatchaBGM") as AudioClip;

        SoundConstBox.acGatchaResult = Resources.Load("Sound/OutGame/Gatcha/GatchaResult") as AudioClip;

        SoundConstBox.acResultFormBGM = Resources.Load("RebuildSound/Result/acResultBGM") as AudioClip;
        SoundConstBox.acBestScore = Resources.Load("RebuildSound/Result/acBestScore") as AudioClip;
        SoundConstBox.acScoring = Resources.Load("RebuildSound/Result/acScoring") as AudioClip;
        SoundConstBox.acResultBottle = Resources.Load("RebuildSound/Result/acResultBottle") as AudioClip;
        SoundConstBox.acUserLevelUp = Resources.Load("RebuildSound/Result/acUserLevelUp") as AudioClip;


        // Enemy Neko 관련
        SoundConstBox.acEnemyHit = Resources.Load("RebuildSound/InGame/Enemy/acEnemyHit") as AudioClip;
        SoundConstBox.acEnemyNekoKill = Resources.Load("RebuildSound/InGame/Enemy/acEnemyKill") as AudioClip;
        
        SoundConstBox.acEnemySingleBigHit = Resources.Load("RebuildSound/InGame/Enemy/acEnemySingleBigHit2") as AudioClip;

        // Player Neko 관련
        SoundConstBox.acPlayerSingleAttackVoice = Resources.Load("RebuildSound/InGame/PlayerNeko/acPlayerSingleAttackVoice") as AudioClip;
        SoundConstBox.acPlayerSkillAttackHit = Resources.Load("RebuildSound/InGame/PlayerNeko/acPlayerSkillAttackHit") as AudioClip;
        SoundConstBox.acPlayerSkillSingleFull = Resources.Load("RebuildSound/InGame/PlayerNeko/acPlayerSkillSingleFull") as AudioClip;
        SoundConstBox.acPlayerSkillTripleFull = Resources.Load("RebuildSound/InGame/PlayerNeko/acPlayerSkillTripleFull") as AudioClip;
        SoundConstBox.acPlayerTripleAttackVoice = Resources.Load("RebuildSound/InGame/PlayerNeko/acPlayerTripleAttackVoice") as AudioClip;
        SoundConstBox.acPlayerTripleAttackWhip = Resources.Load("RebuildSound/InGame/PlayerNeko/acPlayerTripleAttackWhip") as AudioClip;

        

        // 성장
        SoundConstBox.acNekoLevelUp = Resources.Load("RebuildSound/OutGame/acNekoLevelUp") as AudioClip;
        SoundConstBox.acNekoFeed = Resources.Load("RebuildSound/OutGame/acNekoFeed") as AudioClip;

        // 타이틀 
        SoundConstBox.acTitleBGM = Resources.Load("RebuildSound/Title/acTitleBGM") as AudioClip;

        // Voice 
        SoundConstBox.acReady = Resources.Load("RebuildSound/InGame/Voice/acReady") as AudioClip;
        SoundConstBox.acGo = Resources.Load("RebuildSound/InGame/Voice/acGo") as AudioClip;
        SoundConstBox.acTimeout = Resources.Load("RebuildSound/InGame/Voice/acTimeout") as AudioClip;

        // BGM
        SoundConstBox.acIngameBGM = Resources.Load("RebuildSound/InGame/InGameBGM") as AudioClip;
        
        SoundConstBox.acFeverBGM = Resources.Load("RebuildSound/InGame/FeverBGM2") as AudioClip;
        SoundConstBox.acFeverVocie = Resources.Load("RebuildSound/InGame/FeverVoice") as AudioClip;
        SoundConstBox.acFishGatchaBGM = Resources.Load("RebuildSound/Gatcha/acFishGatchaBGM") as AudioClip;
        


        SoundConstBox.acCap = Resources.Load("RebuildSound/Result/acCap") as AudioClip;

        SoundConstBox.acTick = Resources.Load("RebuildSound/InGame/acTick") as AudioClip;
        

        //GeneralUI/SoundBtnPossitive
        SoundConstBox.acUIPossitive = Resources.Load("RebuildSound/GeneralUI/SoundBtnPossitive") as AudioClip;
        SoundConstBox.acUINegative = Resources.Load("RebuildSound/GeneralUI/SoundBtnNegative") as AudioClip;

        /*
        SoundConstBox.acVoiceCinematic1 = Resources.Load("RebuildSound/Gatcha/acVoiceCinematic1") as AudioClip;
        SoundConstBox.acVoiceCinematic2 = Resources.Load("RebuildSound/Gatcha/acVoiceCinematic2") as AudioClip;
        SoundConstBox.acAppearPic = Resources.Load("RebuildSound/Gatcha/acAppearPic") as AudioClip;
        SoundConstBox.acVoiceFusion = Resources.Load("RebuildSound/Gatcha/acVoiceFusion") as AudioClip;

        SoundConstBox.acFusionFirst = Resources.Load("RebuildSound/Gatcha/acFusionFirst") as AudioClip;
        SoundConstBox.acFusionSecond = Resources.Load("RebuildSound/Gatcha/acFusionSecond") as AudioClip;
        SoundConstBox.acFusionThird = Resources.Load("RebuildSound/Gatcha/acFusionThird") as AudioClip;
        */
        //NekoEvolution
        SoundConstBox.acNekoEvolBGM = Resources.Load("RebuildSound/NekoEvolution/acNekoEvolBGM") as AudioClip;
        SoundConstBox.acNekoJump = Resources.Load("RebuildSound/NekoEvolution/acNekoJump") as AudioClip;
        SoundConstBox.acShrinkNeko = Resources.Load("RebuildSound/NekoEvolution/acShrinkNeko") as AudioClip;
        SoundConstBox.acStarJump = Resources.Load("RebuildSound/NekoEvolution/acStarJump") as AudioClip;

        SoundConstBox.acCoinAbsorb = Resources.Load("RebuildSound/InGame/Effect/acCoinAbsorb") as AudioClip;
        SoundConstBox.acEquipItemPing = Resources.Load("RebuildSound/Result/SoundResultNekoPing1") as AudioClip;
        

        SoundConstBox.acPing = new AudioClip[3];
        SoundConstBox.acPing[0] = Resources.Load("RebuildSound/Result/SoundResultNekoPing1") as AudioClip;
        SoundConstBox.acPing[1] = Resources.Load("RebuildSound/Result/SoundResultNekoPing2") as AudioClip;
        SoundConstBox.acPing[2] = Resources.Load("RebuildSound/Result/SoundResultNekoPing3") as AudioClip;

        //D:\GitWorkSpace\DuoFunProject\MitchirinekoPuzzle\Assets\Resources\RebuildSound\Result
    }

    /// <summary>
    /// Inits the android IGW.
    /// </summary>
    private void InitAndroidIGWA() {

        //Debug.Log("InitAndroidIGWA");
        IgaworksUnityPluginAOS.InitPlugin ();
        //IgaworksUnityPluginAOS.Common.startApplication (); // 네이티브 SDK 초기화 
        /*
        if (!Application.isEditor) {
            AndroidJavaClass jc = new AndroidJavaClass("com.igaworks.unity.IgawUnityUtilityAOS");
            string IMEI = jc.CallStatic<string>("getAndroidId");
            IgaworksUnityPluginAOS.Common.startApplication(IMEI);
        }
        */

        IgawInitialized = true;
        //Debug.Log("InitAndroidIGWA End");
    }

	/// <summary>
	/// Inits the ios IGAWorks
	/// </summary>
	private void InitIosIGAW() {
		IgaworksCorePluginIOS.IgaworksCoreWithAppKey("963747472", "3163a38efa664c1f");
		IgaworksCorePluginIOS.SetLogLevel(IgaworksCorePluginIOS.IgaworksCoreLogDebug);
		IgaworksCorePluginIOS.SetCallbackHandler("GameSystem");
		IgaworksCorePluginIOS.SetIgaworksCoreDelegate();

        IgawInitialized = true;


        ISN_LocalNotificationsController.Instance.RequestNotificationPermissions();
    }

    /// <summary>
    /// OS 별 초기화 추가 로직 
    /// </summary>
    private void InitOS() {
        #if UNITY_IOS
        GameCenterManager.OnAchievementsLoaded += OnAchievementsLoadedIOS;
        GameCenterManager.OnLeadrboardInfoLoaded += OnLeadrboardInfoLoaded;
        #endif
    }


    #endregion


    #region Save Load Game Data Version 
    bool _checkedMasterVersion = false;
    /// <summary>
    ///  버전 체크 
    ///  최초의 서버접속 Post2GameDataVersion 에서 각 연동 데이터의 버전 정보를 가져온 후, 로컬의 버전과 비교한다. 
    /// </summary>
    private void CheckDataVersion() {

        
        //Debug.Log("▶▶▶▶▶ CheckDataVersion _version :: " + _version.ToString());
        //Debug.Log("▶▶▶▶▶ Master version  :: " + _gameVesionJSON["master_version"].AsFloat.ToString());

        /* 마스터 버전 처리 */

        // 빌드버전이 마스터 버전보다 높은 경우 Test Server로 연동된다. 
        if ( _gameVesionJSON == null || (float.Parse(_version) > _gameVesionJSON["master_version"].AsFloat)) {

            //WWWHelper.Instance.SetConnectServerURL(true);

            Debug.Log("★★★★★Connect to Test Server");

            // 최초 1회 체크 후, 다시 Connect Server를 호출, 테스트 서버로만 접속하도록 처리한다.
            if (!_checkedMasterVersion) {
                WWWHelper.Instance.SetConnectServerURL(false); // 테스트 서버로 설정 
                ConnectServer(); // 재호출. 

                _checkedMasterVersion = true; // 두번 실행되지 않도록 FLAG 처리 

                return;
            }

            // 테스트 서버로 연결이 된다면, 데이터의 version들을 모두 -1로 처리한다. (매번 refresh) 
            /*
            ENV_VERSION = -1;
            COINSHOP_VERSION = -1;
            RANKREWARD_VERSION = -1;
            ATTENDANCE_VERSION = -1;
            //MISSION_DAILY_VERSION = -1;
            //MISSION_WEEKLY_VERSION = -1;

            GATCHA_BANNER_VERSION = -1;
            PACKAGE_BANNER_VERSION = -1;
            //NOTICE_BANNER_VERSION = -1;
            
            */
        }
        else {
            Debug.Log("★★★★★Connect to Live Server");
            WWWHelper.Instance.SetConnectServerURL(true); // 라이브 서버로 설정 
        }



        // 요청 데이터 리스트 초기화 
        RequestGameData.Clear();

        /* 버전의 수치가 다르거나, 로컬의 데이터가 없으면 다운 받도록 처리한다. */

        // 환경정보 
        if(ENV_VERSION != _gameVesionJSON[ENV_DATA].AsInt) {
            RequestGameData.Add(ENV_DATA);
        }

        if (COINSHOP_VERSION != _gameVesionJSON[COINSHOP_DATA].AsInt) {
            RequestGameData.Add(COINSHOP_DATA);
        }

        // 출석정보 
        if (ATTENDANCE_VERSION != _gameVesionJSON[ATTENDANCE_DATA].AsInt) {
            RequestGameData.Add(ATTENDANCE_DATA);
        }

        /* 1.045 부터 미션만 예외로 버전이 낮은 경우만 업데이트 한다. */
        if (MISSION_DAILY_VERSION != _gameVesionJSON[MISSION_DAILY_DATA].AsInt) {
            RequestGameData.Add(MISSION_DAILY_DATA);
        }

        if (MISSION_WEEKLY_VERSION != _gameVesionJSON[MISSION_WEEKLY_DATA].AsInt) {
            RequestGameData.Add(MISSION_WEEKLY_DATA);
        }

        if (RANKREWARD_VERSION != _gameVesionJSON[RANKREWARD_DATA].AsInt) {
            RequestGameData.Add(RANKREWARD_DATA);
        }

        if(BINGO_VERSION != _gameVesionJSON[BINGO_DATA].AsInt) {
            RequestGameData.Add(BINGO_DATA);
        }

        if (BINGO_GROUP_VERSION != _gameVesionJSON[BINGO_GROUP_DATA].AsInt) {
            RequestGameData.Add(BINGO_GROUP_DATA);
        }

        if (USER_PASSIVE_VERSION != _gameVesionJSON[USER_PASSIVE_DATA].AsInt) {
            RequestGameData.Add(USER_PASSIVE_DATA);
        }

        if (STAGE_DETAIL_VERSION != _gameVesionJSON[STAGE_DETAIL_DATA].AsInt) {
            RequestGameData.Add(STAGE_DETAIL_DATA);
        }

        if (STAGE_MASTER_VERSION != _gameVesionJSON[STAGE_MASTER_DATA].AsInt) {
            RequestGameData.Add(STAGE_MASTER_DATA);
        }



        #region 배너 버전

        if (GATCHA_BANNER_VERSION != _gameVesionJSON[GATCHA_BANNER_DATA].AsInt) {
            RequestGameData.Add(GATCHA_BANNER_DATA);
        }

        if (PACKAGE_BANNER_VERSION != _gameVesionJSON[PACKAGE_BANNER_DATA].AsInt) {
            RequestGameData.Add(PACKAGE_BANNER_DATA);
        }

        if (NOTICE_BANNER_VERSION != _gameVesionJSON[NOTICE_BANNER_DATA].AsInt) {
            RequestGameData.Add(NOTICE_BANNER_DATA);
        }

        #endregion


        /* 추가 데이터 체크 체크 */

        if (!RequestGameData.Contains(ENV_DATA) && EnvInitJSON == null)
            RequestGameData.Add(ENV_DATA);

        if (!RequestGameData.Contains(COINSHOP_DATA) && CoinShopInitJSON == null)
            RequestGameData.Add(COINSHOP_DATA);

        if (!RequestGameData.Contains(ATTENDANCE_DATA) && _attendanceInitJSON == null)
            RequestGameData.Add(ATTENDANCE_DATA);

        if (!RequestGameData.Contains(RANKREWARD_DATA) && RankRewardInitJSON == null)
            RequestGameData.Add(RANKREWARD_DATA);

        if (!RequestGameData.Contains(MISSION_DAILY_DATA) && _missionDayInitJSON == null)
            RequestGameData.Add(MISSION_DAILY_DATA);

        if (!RequestGameData.Contains(MISSION_WEEKLY_DATA) && _missionWeekInitJSON == null)
            RequestGameData.Add(MISSION_WEEKLY_DATA);

        if (!RequestGameData.Contains(BINGO_DATA) && BingoInitJSON == null)
            RequestGameData.Add(BINGO_DATA);

        if (!RequestGameData.Contains(BINGO_GROUP_DATA) && BingoGroupJSON == null)
            RequestGameData.Add(BINGO_GROUP_DATA);

        if (!RequestGameData.Contains(USER_PASSIVE_DATA) && UserPassivePriceJSON == null)
            RequestGameData.Add(USER_PASSIVE_DATA);

        if (!RequestGameData.Contains(STAGE_DETAIL_DATA) && StageDetailJSON == null)
            RequestGameData.Add(STAGE_DETAIL_DATA);

        if (!RequestGameData.Contains(STAGE_MASTER_DATA) && StageMasterJSON == null)
            RequestGameData.Add(STAGE_MASTER_DATA);

        if (!RequestGameData.Contains(GATCHA_BANNER_DATA) && _gatchaBannerInitJSON == null)
            RequestGameData.Add(GATCHA_BANNER_DATA);

        if (!RequestGameData.Contains(PACKAGE_BANNER_DATA) && _packageBannerInitJSON == null)
            RequestGameData.Add(PACKAGE_BANNER_DATA);

        if (!RequestGameData.Contains(NOTICE_BANNER_DATA) && _noticeBannerInitJSON == null)
            RequestGameData.Add(NOTICE_BANNER_DATA);

        // 새로운 배너정보 불러오지 않았을때, 기존의 로컬 배너 이미지 불러오기 
        if (!RequestGameData.Contains(GATCHA_BANNER_DATA)) {
            LoadGatchaImages();
        }

        if(!RequestGameData.Contains(NOTICE_BANNER_DATA)) {
            LoadLocalNoticeImages();
        }

        if (!RequestGameData.Contains(PACKAGE_BANNER_DATA)) {
            LoadLocalPackageImages();
        }

        // 로컬 이미지 불러오는데 문제가 있다면 무조건 다운로드로 처리
        if(_isLoadingImageException) {

            Debug.Log("▶▶▶▶▶▶ _isLoadingImageException is true");

            if (!RequestGameData.Contains(GATCHA_BANNER_DATA))
                RequestGameData.Add(GATCHA_BANNER_DATA);

            if (!RequestGameData.Contains(NOTICE_BANNER_DATA))
                RequestGameData.Add(NOTICE_BANNER_DATA);

            if (!RequestGameData.Contains(PACKAGE_BANNER_DATA))
                RequestGameData.Add(PACKAGE_BANNER_DATA);
        }

        /* RequestGameData 리스트의 Count에 따라서 분기 GameSystem.Http.OnFinishedGameDataVersion */


        // 하나라도 데이터 요청이 있으면 게임 데이터 요청 처리 
        if (_requestGameData.Count > 0)
            Post2GameData();
        else
            Post2FindAccount(); // 바로 로그인 처리 

    }

    /// <summary>
    /// 서버와 데이터 연동 후 로컬로 저장 처리 
    /// </summary>
    private void SaveDataVersion() {
        
        // 버전 정보 가져오기.
        ENV_VERSION = _gameVesionJSON[ENV_DATA].AsInt;
        COINSHOP_VERSION = _gameVesionJSON[COINSHOP_DATA].AsInt;
        ATTENDANCE_VERSION = _gameVesionJSON[ATTENDANCE_DATA].AsInt;
        NEKOSKILL_VERSION = _gameVesionJSON[NEKOSKILL_DATA].AsInt;
        RANKREWARD_VERSION = _gameVesionJSON[RANKREWARD_DATA].AsInt;
        NEKOBEAD_VERSION = _gameVesionJSON[NEKOBEAD_DATA].AsInt;


        // 배너 정보
        GATCHA_BANNER_VERSION = _gameVesionJSON[GATCHA_BANNER_DATA].AsInt;
        PACKAGE_BANNER_VERSION = _gameVesionJSON[PACKAGE_BANNER_DATA].AsInt;
        NOTICE_BANNER_VERSION = _gameVesionJSON[NOTICE_BANNER_DATA].AsInt;

        // 미션 정보 
        MISSION_DAILY_VERSION = _gameVesionJSON[MISSION_DAILY_DATA].AsInt;
        MISSION_WEEKLY_VERSION = _gameVesionJSON[MISSION_WEEKLY_DATA].AsInt;
        BINGO_VERSION = _gameVesionJSON[BINGO_DATA].AsInt;
        BINGO_GROUP_VERSION = _gameVesionJSON[BINGO_GROUP_DATA].AsInt;
        USER_PASSIVE_VERSION = _gameVesionJSON[USER_PASSIVE_DATA].AsInt;

        STAGE_DETAIL_VERSION = _gameVesionJSON[STAGE_DETAIL_DATA].AsInt;
        STAGE_MASTER_VERSION = _gameVesionJSON[STAGE_MASTER_DATA].AsInt;

        // 버전 정보 저장 
        ES2.Save<int>(ENV_VERSION, ENV_DATA); // 환경정보 
        ES2.Save<int>(COINSHOP_VERSION, COINSHOP_DATA); // 코인샵 
        ES2.Save<int>(ATTENDANCE_VERSION, ATTENDANCE_DATA);        // 출석체크 
        ES2.Save<int>(NEKOSKILL_VERSION, NEKOSKILL_DATA);          // 네코 스킬 밸런스
        ES2.Save<int>(RANKREWARD_VERSION, RANKREWARD_DATA); // 랭킹 보상 
        ES2.Save<int>(NEKOBEAD_VERSION, NEKOBEAD_DATA); //  네코 경험치  
        ES2.Save<int>(GATCHA_BANNER_VERSION, GATCHA_BANNER_DATA); // 가챠 배너
        ES2.Save<int>(PACKAGE_BANNER_VERSION, PACKAGE_BANNER_DATA); // 패키지 
        ES2.Save<int>(NOTICE_BANNER_VERSION, NOTICE_BANNER_DATA); // 공지 


        // 미션 
        ES2.Save<int>(MISSION_DAILY_VERSION, MISSION_DAILY_DATA);
        ES2.Save<int>(MISSION_WEEKLY_VERSION, MISSION_WEEKLY_DATA);
        ES2.Save<int>(BINGO_VERSION, BINGO_DATA);
        ES2.Save<int>(BINGO_GROUP_VERSION, BINGO_GROUP_DATA);

        // 패시브 스킬 
        ES2.Save<int>(USER_PASSIVE_VERSION, USER_PASSIVE_DATA);

        ES2.Save<int>(STAGE_DETAIL_VERSION, STAGE_DETAIL_DATA);
        ES2.Save<int>(STAGE_MASTER_VERSION, STAGE_MASTER_DATA);

        // JSON 저장 
        ES2.Save<string>(EnvInitJSON.ToString(), ENV_DATA + "-JSON");
        ES2.Save<string>(CoinShopInitJSON.ToString(), COINSHOP_DATA + "-JSON");
        ES2.Save<string>(_attendanceInitJSON.ToString(), ATTENDANCE_DATA + "-JSON");
        ES2.Save<string>(RankRewardInitJSON.ToString(), RANKREWARD_DATA + "-JSON");

        ES2.Save<string>(_missionDayInitJSON.ToString(), MISSION_DAILY_DATA + "-JSON");
        ES2.Save<string>(_missionWeekInitJSON.ToString(), MISSION_WEEKLY_DATA + "-JSON");
        ES2.Save<string>(BingoInitJSON.ToString(), BINGO_DATA + "-JSON");

        if(UserPassivePriceJSON != null)
            ES2.Save<string>(UserPassivePriceJSON.ToString(), USER_PASSIVE_DATA + "-JSON");

        if (StageDetailJSON != null)
            ES2.Save<string>(StageDetailJSON.ToString(), STAGE_DETAIL_DATA + "-JSON");

        if (StageMasterJSON != null)
            ES2.Save<string>(StageMasterJSON.ToString(), STAGE_MASTER_DATA + "-JSON");

        if (BingoGroupJSON != null) {
            ES2.Save<string>(BingoGroupJSON.ToString(), BINGO_GROUP_DATA + JSON_DATA);

            //PlayerPrefs.SetString(BINGO_GROUP_DATA + JSON_DATA, BingoGroupJSON.ToString());
            //PlayerPrefs.Save();
        }




        ES2.Save<string>(_gatchaBannerInitJSON.ToString(), GATCHA_BANNER_DATA + "-JSON");
        ES2.Save<string>(_packageBannerInitJSON.ToString(), PACKAGE_BANNER_DATA + "-JSON");
        ES2.Save<string>(_noticeBannerInitJSON.ToString(), NOTICE_BANNER_DATA + "-JSON");



        // CDN 이미지 다운로드 
        if (RequestGameData.Contains(GATCHA_BANNER_DATA)) {
            DownloadGatchaImages();
        }

        if(RequestGameData.Contains(NOTICE_BANNER_DATA)) {
            DownloadNoticeImages();
        } 

        if (RequestGameData.Contains(PACKAGE_BANNER_DATA)) {
            DownloadPackageImages();
        }


        SetPriceInfo(EnvInitJSON);
        SetCoinShopInfo(CoinShopInitJSON);

    }

    /// <summary>
    /// 공지 배너 정보 저장 
    /// </summary>
    private void SaveNoticeBannerJSON() {
        ES2.Save<string>(_noticeBannerInitJSON.ToString(), NOTICE_BANNER_DATA + "-JSON");
    }

    /// <summary>
    /// 서버 데이터 연동 정보의 버전을 로드 (GameSystem Start 에서 실행)
    /// </summary>
    private void LoadDataVersion() {


        if(ES2.Exists(ENV_DATA)) {
            ENV_VERSION = ES2.Load<int>(ENV_DATA);
        }
        else {
            ENV_VERSION = -1;
        }


        if (ES2.Exists(COINSHOP_DATA)) {
            COINSHOP_VERSION = ES2.Load<int>(COINSHOP_DATA);
        }
        else {
            COINSHOP_VERSION = -1;
        }


        // 출석체크 
        if (ES2.Exists(ATTENDANCE_DATA)) {
            ATTENDANCE_VERSION = ES2.Load<int>(ATTENDANCE_DATA);
        }
        else {
            ATTENDANCE_VERSION = -1;
        }


        // 네코 스킬
        if (ES2.Exists(NEKOSKILL_DATA)) {
            NEKOSKILL_VERSION = ES2.Load<int>(NEKOSKILL_DATA);
        }
        else {
            NEKOSKILL_VERSION = -1;
        }

        // 랭킹 보상
        if (ES2.Exists(RANKREWARD_DATA)) {
            RANKREWARD_VERSION = ES2.Load<int>(RANKREWARD_DATA);
        }
        else {
            RANKREWARD_VERSION = -1;
        }

        // 네코 경험치 
        if (ES2.Exists(NEKOBEAD_DATA)) {
            NEKOBEAD_VERSION = ES2.Load<int>(NEKOBEAD_DATA);
        }
        else {
            NEKOBEAD_VERSION = -1;
        }


        // 유저 패시브 업그레이드 가격 
        if (ES2.Exists(USER_PASSIVE_DATA)) {
            USER_PASSIVE_VERSION = ES2.Load<int>(USER_PASSIVE_DATA);
        }
        else {
            USER_PASSIVE_VERSION = -1;
        }


        if (ES2.Exists(STAGE_DETAIL_DATA)) {
            STAGE_DETAIL_VERSION = ES2.Load<int>(STAGE_DETAIL_DATA);
        }
        else {
            STAGE_DETAIL_VERSION = -1;
        }

        if (ES2.Exists(STAGE_MASTER_DATA)) {
            STAGE_MASTER_VERSION = ES2.Load<int>(STAGE_MASTER_DATA);
        }
        else {
            STAGE_MASTER_VERSION = -1;
        }



        if (ES2.Exists(GATCHA_BANNER_DATA)) {
            GATCHA_BANNER_VERSION = ES2.Load<int>(GATCHA_BANNER_DATA);
        }
        else {
            GATCHA_BANNER_VERSION = -1;
        }

        if (ES2.Exists(PACKAGE_BANNER_DATA)) {
            PACKAGE_BANNER_VERSION = ES2.Load<int>(PACKAGE_BANNER_DATA);
        }
        else {
            PACKAGE_BANNER_VERSION = -1;
        }

        if (ES2.Exists(NOTICE_BANNER_DATA)) {
            NOTICE_BANNER_VERSION = ES2.Load<int>(NOTICE_BANNER_DATA);
        }
        else {
            NOTICE_BANNER_VERSION = -1;
        }


        // 미션
        if (ES2.Exists(MISSION_DAILY_DATA)) {
            MISSION_DAILY_VERSION = ES2.Load<int>(MISSION_DAILY_DATA);
        }
        else {
            MISSION_DAILY_VERSION = -1;
        }

        if (ES2.Exists(MISSION_WEEKLY_DATA)) {
            MISSION_WEEKLY_VERSION = ES2.Load<int>(MISSION_WEEKLY_DATA);
        }
        else {
            MISSION_WEEKLY_VERSION = -1;
        }

        if(ES2.Exists(BINGO_DATA)) {
            BINGO_VERSION = ES2.Load<int>(BINGO_DATA);
        }
        else {
            BINGO_VERSION = -1;
        }

        if (ES2.Exists(BINGO_GROUP_DATA)) {
            BINGO_GROUP_VERSION = ES2.Load<int>(BINGO_GROUP_DATA);
        }
        else {
            BINGO_GROUP_VERSION = -1;
        }


        /* JSON 정보 로드 */
        if (ES2.Exists(ENV_DATA + JSON_DATA)) {

            if (string.IsNullOrEmpty(ES2.Load<string>(ENV_DATA + JSON_DATA))) {
                EnvInitJSON = null;
            }
            else {
                EnvInitJSON = JSON.Parse(ES2.Load<string>(ENV_DATA + JSON_DATA));
            }

        }
        else {
            EnvInitJSON = null;
        }


        if (ES2.Exists(COINSHOP_DATA + JSON_DATA)) {

            if (string.IsNullOrEmpty(ES2.Load<string>(COINSHOP_DATA + JSON_DATA))) {
                CoinShopInitJSON = null;
            }
            else {
                CoinShopInitJSON = JSON.Parse(ES2.Load<string>(COINSHOP_DATA + JSON_DATA));
            }

        }
        else {
            CoinShopInitJSON = null;
        }



        if (ES2.Exists(ATTENDANCE_DATA + JSON_DATA)) {

            if (string.IsNullOrEmpty(ES2.Load<string>(ATTENDANCE_DATA + JSON_DATA))) {
                _attendanceInitJSON = null;
            }
            else {
                _attendanceInitJSON = JSON.Parse(ES2.Load<string>(ATTENDANCE_DATA + JSON_DATA));
            }
            
        } else {
            _attendanceInitJSON = null;
        }


        // 랭킹 정보 
        if (ES2.Exists(RANKREWARD_DATA + JSON_DATA)) {

            if (string.IsNullOrEmpty(ES2.Load<string>(RANKREWARD_DATA + JSON_DATA))) {
                RankRewardInitJSON = null;
            }
            else {
                RankRewardInitJSON = JSON.Parse(ES2.Load<string>(RANKREWARD_DATA + JSON_DATA));
            }
            
        }
        else {
            RankRewardInitJSON = null;
        }

        // 유저 패시브
        if (ES2.Exists(USER_PASSIVE_DATA + JSON_DATA)) {

            if (string.IsNullOrEmpty(ES2.Load<string>(USER_PASSIVE_DATA + JSON_DATA))) {
                UserPassivePriceJSON = null;
            }
            else {
                UserPassivePriceJSON = JSON.Parse(ES2.Load<string>(USER_PASSIVE_DATA + JSON_DATA));
            }

        }
        else {
            UserPassivePriceJSON = null;
        }



        if (ES2.Exists(MISSION_DAILY_DATA + JSON_DATA)) {

            if (string.IsNullOrEmpty(ES2.Load<string>(MISSION_DAILY_DATA + JSON_DATA))) {
                _missionDayInitJSON = null;
            }
            else {
                _missionDayInitJSON = JSON.Parse(ES2.Load<string>(MISSION_DAILY_DATA + JSON_DATA));
                _missionDayInitJSON = InitMissionProgress(_missionDayInitJSON); // InitJson의 progress, current 초기화
            }
        }
        else {
            _missionDayInitJSON = null;
        }

        if (ES2.Exists(MISSION_WEEKLY_DATA + JSON_DATA)) {


            if (string.IsNullOrEmpty(ES2.Load<string>(MISSION_WEEKLY_DATA + JSON_DATA))) {
                _missionWeekInitJSON = null;
            }
            else {
                _missionWeekInitJSON = JSON.Parse(ES2.Load<string>(MISSION_WEEKLY_DATA + JSON_DATA));
                _missionWeekInitJSON = InitMissionProgress(_missionWeekInitJSON); // InitJson의 progress, current 초기화
            }
        }
        else {
            _missionWeekInitJSON = null;
        }


        // 빙고 정보 로드 
        if(ES2.Exists(BINGO_DATA + JSON_DATA)) {

            if(string.IsNullOrEmpty(ES2.Load<string>(BINGO_DATA + JSON_DATA))) {
                BingoInitJSON = null;
            }
            else {
                BingoInitJSON = JSON.Parse(ES2.Load<string>(BINGO_DATA + JSON_DATA));
                //BingoInitJSON = InitBingoProgress(BingoInitJSON);
                //BingoJSON = BingoInitJSON; // 저장 후 SetUserInfo에서 진행사항을 저장 
            }


            if(ES2.Exists(BINGO_PROGRESS)) {
                if (!string.IsNullOrEmpty(ES2.Load<string>(BINGO_PROGRESS))) {
                    BingoJSON = JSON.Parse(ES2.Load<string>(BINGO_PROGRESS));
                }
                else {
                    BingoJSON = BingoInitJSON;
                }
            }
            else {
                BingoJSON = BingoInitJSON;
            }


        }
        else {
            BingoInitJSON = null;
        }

        // 빙고 고양이 그룹 정보 로드
        if(ES2.Exists(BINGO_GROUP_DATA + JSON_DATA)) {
            if (string.IsNullOrEmpty(ES2.Load<string>(BINGO_GROUP_DATA + JSON_DATA))) {
                BingoGroupJSON = null;
            }
            else {
                BingoGroupJSON = JSON.Parse(ES2.Load<string>(BINGO_GROUP_DATA + JSON_DATA));
            }
        }
        else {
            BingoGroupJSON = null;
        }

        /*
        if(PlayerPrefs.HasKey(BINGO_GROUP_DATA + JSON_DATA)) {
            if(string.IsNullOrEmpty(PlayerPrefs.GetString(BINGO_GROUP_DATA + JSON_DATA))) {
                BingoGroupJSON = null;
            }
            else {
                BingoGroupJSON = JSON.Parse(PlayerPrefs.GetString(BINGO_GROUP_DATA + JSON_DATA));
            }
        }
        else {
            BingoGroupJSON = null;
        }
        */


        #region 배너s 

        // 가챠 배너 
        if (ES2.Exists(GATCHA_BANNER_DATA + JSON_DATA)) {


            if (string.IsNullOrEmpty(ES2.Load<string>(GATCHA_BANNER_DATA + JSON_DATA))) {
                _gatchaBannerInitJSON = null;
            }

            GatchaBannerInitJSON = JSON.Parse(ES2.Load<string>(GATCHA_BANNER_DATA + JSON_DATA));
            
        }
        else {
            _gatchaBannerInitJSON = null;
        }


        if (ES2.Exists(PACKAGE_BANNER_DATA + JSON_DATA)) {


            if (string.IsNullOrEmpty(ES2.Load<string>(PACKAGE_BANNER_DATA + JSON_DATA))) {
                _packageBannerInitJSON = null;
            }

            PackageBannerInitJSON = JSON.Parse(ES2.Load<string>(PACKAGE_BANNER_DATA + JSON_DATA));

        }
        else {
            _packageBannerInitJSON = null;
        }


        if (ES2.Exists(NOTICE_BANNER_DATA + JSON_DATA)) {


            if (string.IsNullOrEmpty(ES2.Load<string>(NOTICE_BANNER_DATA + JSON_DATA))) {
                _noticeBannerInitJSON = null;
            }
            NoticeBannerInitJSON = JSON.Parse(ES2.Load<string>(NOTICE_BANNER_DATA + JSON_DATA));

        }
        else {
            _noticeBannerInitJSON = null;
        }

        #endregion


        // 가겨정보 세팅 
        SetPriceInfo(EnvInitJSON);
        SetCoinShopInfo(CoinShopInitJSON);
    }

    /// <summary>
    /// 로컬이 필요한 데이터 버전을 저장 
    /// </summary>
    public void InitLocalDataVersion() {

        GATCHA_BANNER_VERSION = 1;
        PACKAGE_BANNER_VERSION = 1;
        NOTICE_BANNER_VERSION = 1;

        ES2.Save<int>(GATCHA_BANNER_VERSION, GATCHA_BANNER_DATA); // 가챠 배너
        ES2.Save<int>(PACKAGE_BANNER_VERSION, PACKAGE_BANNER_DATA); // 패키지 
        ES2.Save<int>(NOTICE_BANNER_VERSION, NOTICE_BANNER_DATA); // 공지 
    }


    /// <summary>
    /// 게임내 가격정보 세팅 
    /// </summary>
    /// <param name="pNode"></param>
    private void SetPriceInfo(JSONNode pNode) {

        // null 인 경우는 기본 가격으로 세팅 
        if (pNode == null) {
            BoostBombPrice = PuzzleConstBox.originalBoostItemPrice;
            BoostShieldPrice = PuzzleConstBox.originalBoostItemPrice;
            BoostCriticalPrice = PuzzleConstBox.originalBoostItemPrice;
            BoostStartFeverPrice = PuzzleConstBox.originalBoostStartFever;

            SpecialSingleGatchaPrice = PuzzleConstBox.originalSingleGatchaPrice;
            SpecialMultiGatchaPrice = PuzzleConstBox.originalMultiGatchaPrice;

            SpecialSingleFishingPrice = PuzzleConstBox.originalSingleFishingPrice;
            SpecialMultiFishingPrice = PuzzleConstBox.originalMultiFishingPrice;
            RecordUse = false;
        }
        else {
            BoostBombPrice = pNode["boostbombvalue"].AsInt;
            BoostShieldPrice = pNode["boostshieldvalue"].AsInt;
            BoostCriticalPrice = pNode["boostcriticalvalue"].AsInt;
            BoostStartFeverPrice = pNode["booststartfevervalue"].AsInt;

            SpecialSingleGatchaPrice = pNode["singlespecialnekogem"].AsInt;
            SpecialMultiGatchaPrice = pNode["tenspecialnekogem"].AsInt;

            SpecialSingleFishingPrice = pNode["singlefishcoin"].AsInt;
            SpecialMultiFishingPrice = pNode["tenfishcoin"].AsInt;

            RecordUse = pNode["recorduse"].AsBool;
        }

    }

    private void SetCoinShopInfo(JSONNode pNode) {

        _listCoinShopPrice = new List<int>();

        if(pNode == null || pNode.Count !=5) {
            Debug.Log("★★★★ Load Coin Shop  Error!");
            InitCoinShopInfo();
            return;
        }

        for(int i=0; i<pNode.Count; i++) {
            _listCoinShopPrice.Add(pNode[i]["coin"].AsInt);
        }

    }

    private void InitCoinShopInfo() {
        _listCoinShopPrice = new List<int>();

        for(int i=0; i < PuzzleConstBox.listCoinShopOriginalPrices.Count; i++) {
            _listCoinShopPrice.Add(PuzzleConstBox.listCoinShopOriginalPrices[i]);
        }
    }

    #endregion


    #region Save Load Option 

    public void InitCurrnetDayOfYear() {
        ES2.Save<int>(-1, "checkday");
    }

    public void SaveCurrentDayOfYear() {
        if(_dtSyncTime != null) {
            ES2.Save<int>(_dtSyncTime.DayOfYear, "checkday");
        }
        else {
            ES2.Save<int>(System.DateTime.Now.DayOfYear, "checkday");
            
        }
    }

    public int LoadCurrentDayOfYear() {
        if(ES2.Exists("checkday")) {
            return ES2.Load<int>("checkday");
        } else {
            return -1;
        }
    }

    /// <summary>
    /// 등급 정렬 여부 세이브 
    /// </summary>
    /// <param name="pFlag">If set to <c>true</c> p flag.</param>
    public void SaveGradeOrder(bool pFlag) {
		ES2.Save<bool> (pFlag, "NekoGradeOrder");
	}

	public bool LoadGradeOrder() {
		if (ES2.Exists ("NekoGradeOrder")) {
			return ES2.Load<bool> ("NekoGradeOrder");
		}
		else 
			return false;
	}

	private void SaveRate(bool pFlag) {
		ES2.Save<bool> (pFlag, "Rated");
		ES2.Save<string> (_version, "RatedVersion");
	}

	public bool GetRate() {
		// 평가 했는지 여부 
		if (ES2.Exists ("Rated")) {
			return ES2.Load<bool> ("Rated");
		}
		else 
			return false;
	}

	public void SaveEffectSoundOption(bool pFlag) {
		_optionSoundPlay = pFlag;
		ES2.Save (pFlag, "OptionEffectSound");

		SetSoundVolumn ();
	}
	public void SaveBGMSoundOption(bool pFlag) {
		_optionBGMPlay = pFlag; // true가 재생 
		ES2.Save (pFlag, "OptionBGMSound");

		SetSoundVolumn ();
	}

    public void SavePuzzleTipOption(bool pFlag) {
        _optionPuzzleTip = pFlag;
        SaveESvalueBool(PuzzleConstBox.PF_PuzzleTipOption, pFlag);
    }

	/// <summary>
	/// 하트 Full 푸시 옵션 
	/// </summary>
	/// <param name="pFlag">If set to <c>true</c> p flag.</param>
	public void SaveHeartPushOption(bool pFlag) {
        _optionHeartPush = pFlag;
        SaveESvalueBool(PuzzleConstBox.PF_HeartPushUse, pFlag);
	}

    public void SaveFreeCranePushOption(bool pFlag) {
        _optionFreeCranePush = pFlag;
        SaveESvalueBool(PuzzleConstBox.PF_FreeCranePushUse, pFlag);
    }

    public void SaveRemotePushOption(bool pFlag) {
        _optionRemotePush = pFlag;
        SaveESvalueBool(PuzzleConstBox.PF_RemotePushUse, pFlag);

        // Remote Push 설정 
#if UNITY_ANDROID
        if (IsLiveOpsInit)
            IgaworksUnityPluginAOS.LiveOps.enableService(_optionRemotePush);
#elif UNITY_IOS
        if(IsLiveOpsInit)
            LiveOpsPluginIOS.LiveOpsSetRemotePushEnable(_optionRemotePush);
#endif
    }




    #region 월요일 첫플레이 정보 처리 

    public void CheckWeekFirstConnect() {
        
        // 갱신처리 
        if(_dtSyncTime.DayOfYear >= LoadFisrtWeekConnectDay()) {

            // 다음 갱신일자 처리 
            if (_dtSyncTime.DayOfWeek == DayOfWeek.Monday) {
                _firstWeekConnectDay = _dtSyncTime.DayOfYear + 7;
            }
            else if (_dtSyncTime.DayOfWeek == DayOfWeek.Tuesday) {
                _firstWeekConnectDay = _dtSyncTime.DayOfYear + 6;
            }
            else if (_dtSyncTime.DayOfWeek == DayOfWeek.Wednesday) {
                _firstWeekConnectDay = _dtSyncTime.DayOfYear + 5;
            }
            else if (_dtSyncTime.DayOfWeek == DayOfWeek.Thursday) {
                _firstWeekConnectDay = _dtSyncTime.DayOfYear + 4;
            }
            else if (_dtSyncTime.DayOfWeek == DayOfWeek.Friday) {
                _firstWeekConnectDay = _dtSyncTime.DayOfYear + 3;
            }
            else if (_dtSyncTime.DayOfWeek == DayOfWeek.Saturday) {
                _firstWeekConnectDay = _dtSyncTime.DayOfYear + 2;
            }
            else if (_dtSyncTime.DayOfWeek == DayOfWeek.Sunday) {
                _firstWeekConnectDay = _dtSyncTime.DayOfYear + 1;
            }

            // Save.
            ES2.Save<int>(_firstWeekConnectDay, "FirstWeekConnectDay");

            // 기타 항목들 역시 갱신한다. 
            UpdateBestScoreCount = 0;
            _firstWeekPlay = false;

            ES2.Save<int>(UpdateBestScoreCount, "UpdatedBestScoreCount");
            ES2.Save<bool>(_firstWeekPlay, "FirstWeekPlay");
        }
        else { // 미 갱신 

            // 정보 조회 
            LoadUpdatedBestScoreCount();
            LoadFirstWeekPlay();
            
        }

    }

    private int LoadFisrtWeekConnectDay() {
        if (ES2.Exists("FirstWeekConnectDay"))
            _firstWeekConnectDay = ES2.Load<int>("FirstWeekConnectDay");
        else
            _firstWeekConnectDay = -1;


        return _firstWeekConnectDay;

    }

    private int LoadUpdatedBestScoreCount() {
        if (ES2.Exists("UpdatedBestScoreCount"))
            UpdateBestScoreCount = ES2.Load<int>("UpdatedBestScoreCount");
        else
            UpdateBestScoreCount = 0;


        return UpdateBestScoreCount;
    }


    private bool LoadFirstWeekPlay() {
        if (ES2.Exists("FirstWeekPlay"))
            _firstWeekPlay = ES2.Load<bool>("FirstWeekPlay");
        else
            _firstWeekPlay = false;


        return _firstWeekPlay;
    }


    #endregion


	/// <summary>
	/// 푸시 옵션 조회 
	/// </summary>
	public void LoadPushOption() {



		// 하트 Full Push
		if (ES2.Exists (PuzzleConstBox.PF_HeartPushUse))
            _optionHeartPush = ES2.Load<bool> (PuzzleConstBox.PF_HeartPushUse);
		else
            _optionHeartPush = true;


        if (ES2.Exists(PuzzleConstBox.PF_FreeCranePushUse))
            _optionFreeCranePush = ES2.Load<bool>(PuzzleConstBox.PF_FreeCranePushUse);
        else
            _optionFreeCranePush = true;

        if (ES2.Exists(PuzzleConstBox.PF_RemotePushUse))
            _optionRemotePush = ES2.Load<bool>(PuzzleConstBox.PF_RemotePushUse);
        else
            _optionRemotePush = true;


        // Remote Push 설정 
#if UNITY_ANDROID

        if(IsLiveOpsInit)
            IgaworksUnityPluginAOS.LiveOps.enableService(_optionRemotePush);
#elif UNITY_IOS
        if(IsLiveOpsInit)
            LiveOpsPluginIOS.LiveOpsSetRemotePushEnable(_optionRemotePush);
#endif


    }



    /// <summary>
    /// 옵션 정보 조회 
    /// </summary>
	private void LoadOptionSetting() {
		if (ES2.Exists ("OptionEffectSound"))
			_optionSoundPlay = ES2.Load<bool> ("OptionEffectSound");
		else 
			_optionSoundPlay = true;

		if (ES2.Exists ("OptionBGMSound"))
			_optionBGMPlay = ES2.Load<bool> ("OptionBGMSound");
		else 
			_optionBGMPlay = true; // Default는 true 


        if (ES2.Exists(PuzzleConstBox.ES_Language)) {
            GameLanguage = ES2.Load<SystemLanguage>(PuzzleConstBox.ES_Language);
        }
        else {

            if (Application.systemLanguage == SystemLanguage.Korean)
                GameLanguage = SystemLanguage.Korean;
            else
                GameLanguage = SystemLanguage.English; // 한국어 아니면 다 영어로 통일 

            // 언어 정보 저장 
            ES2.Save<SystemLanguage>(GameLanguage, PuzzleConstBox.ES_Language);
        }
	}

    public void LoadPuzzleTipOption() {


        if(ES2.Exists(PuzzleConstBox.PF_PuzzleTipOption)) {
            _optionPuzzleTip = ES2.Load<bool>(PuzzleConstBox.PF_PuzzleTipOption);
        }
        else {
            _optionPuzzleTip = false;
        }
    }

    /// <summary>
    /// 디바이스에 저장된 튜토리얼 스텝 조회 
    /// </summary>
    private void LoadLocalTutorialStep() {

        // 임시 초기화
        SaveLocalTutorialStep(0);

        if(ES2.Exists("LocalTutorialStep")) {
            _localTutorialStep = ES2.Load<int>("LocalTutorialStep");
        } else {
            _localTutorialStep = 0; // 없으면 0.
        }
    }

    /// <summary>
    /// 튜토리얼 스텝 저장 
    /// </summary>
    /// <param name="pStep"></param>
    public void SaveLocalTutorialStep(int pStep) {
        _localTutorialStep = pStep;

        ES2.Save<int>(_localTutorialStep, "LocalTutorialStep");
    }


	/// <summary>
	/// 씬 사운드 제어 
	/// </summary>
	public void SetSoundVolumn() {

		// Audio Source 처리 
		AudioSource[] arrSource = GameObject.FindObjectsOfType <AudioSource> ();

		for (int i=0; i<arrSource.Length; i++) {
			// BGM
			if("BGMSource".Equals(arrSource[i].name) && _optionBGMPlay) {
				arrSource[i].mute = false;

			} else if ("BGMSource".Equals(arrSource[i].name) && !_optionBGMPlay) {
				arrSource[i].mute = true;

			} else if(!"BGMSource".Equals(arrSource[i].name) && _optionSoundPlay) {
				arrSource[i].mute = false;

			} else if (!"BGMSource".Equals(arrSource[i].name) && !_optionSoundPlay) {
				arrSource[i].mute = true;

			}
		} // Audio Source 처리 끝
	
		// UI 사운드 처리 
		UIRoot uiRoot = GameObject.FindObjectOfType<UIRoot> ();
		UIPlaySound[] arrPlaySound = uiRoot.GetComponentsInChildren<UIPlaySound> (true);

		for (int i=0; i<arrPlaySound.Length; i++) {
			if(_optionSoundPlay) 
				arrPlaySound[i].volume = 1;
			else
				arrPlaySound[i].volume = 0;
		}
	}



#endregion








    
    private void EndIgaWorkSession() {

    }

    private void ResumeIgaWorkSession() {
#if UNITY_ANDROID


        if(IsLiveOpsInit)
            IgaworksUnityPluginAOS.LiveOps.resume();

#endif
    }


    private void OnApplicationQuit () {


        //SetHeartFullLocalNotification();

        EndIgaWorkSession();

        

        //SetHotTimeLocalNotification();

    }
		 

	/// <summary>
	/// Raises the application pause event.
	/// </summary>
	/// <param name="pauseStatus">If set to <c>true</c> pause status.</param>
	private void OnApplicationPause(bool pauseStatus) {


		Debug.Log ("!!! OnApplicationPause pauseStatus :: " + pauseStatus);

		if (pauseStatus) {  // Pause 

            // 비활성화된 시간을 체크 
            LastActiveHour = DateTime.Now.Hour;
            LastActiveDay = DateTime.Now.DayOfYear;

            // Pause 시에 PushNotification 사용 
            SetHeartFullLocalNotification();
            //SetFreeCraneResetNotification();
            //SetHotTimeLocalNotification();

            EndIgaWorkSession();

           

        } else { // Resume 

            

            CancelAllLocalNotification(); // 모든 로컬 푸시 취소 

            ResumeIgaWorkSession();

            //Screen.orientation = ScreenOrientation.Portrait;
            if(Screen.orientation != ScreenOrientation.Portrait) {
                Screen.orientation = ScreenOrientation.Portrait;
            }

            if(LobbyCtrl.Instance != null) {
                //LobbyCtrl.Instance.CheckBGM();
            }

            // 마지막 비활성화 시간과 일자를 체크하여 Session 갱신처리
            if(LastActiveDay != 0 && LastActiveDay != DateTime.Now.DayOfYear) {
                Post2UserData();
            }
            else if (LastActiveHour != 0 && LastActiveHour < 9 && DateTime.Now.Hour >= 9) {
                Post2UserData();
            }
            else if (LastActiveHour != 0 && LastActiveHour < 21 && DateTime.Now.Hour >= 21) {
                Post2UserData();
            }




        }

		if (InUICtrl.Instance != null) {
			if(pauseStatus) {
				InUICtrl.Instance.PopPause();
			}
		}

	}









	/// <summary>
	/// 다음 광고 가능 시간을 저장.
	/// </summary>
	/// <param name="pTick">P tick.</param>
	public void SaveNextAdsTime(long pTick) {
		ES2.Save<long> (pTick, "PossibleAdsTick");
		CheckUnityAdsTime ();
	}

	/// <summary>
	/// Checks the unity ads time.
	/// 유니티 광고를 볼 수 있는 시간을 체크한다. 
	/// 로비씬을 불러올때마다 실행 필요. 
	/// </summary>
	/// <returns><c>true</c>, if unity ads time was checked, <c>false</c> otherwise.</returns>
	public bool CheckUnityAdsTime() {

		if (ES2.Exists ("PossibleAdsTick")) {
			_possibleAdsTimeTick = ES2.Load<long> ("PossibleAdsTick");
		} else {
			_possibleAdsTimeTick = 0;
		}

		// 
		if (_possibleAdsTimeTick == 0) {
			_isWaitingForAds = false;
			return _isWaitingForAds;
		}

		// 시간 계산 
		_possibleAdsTime = new System.DateTime (_possibleAdsTimeTick);

		// 이미 10분이 지났음. 
		if ((_possibleAdsTime - System.DateTime.Now).Ticks < 0) {
			_isWaitingForAds = false;
			return _isWaitingForAds;
		}

		_isWaitingForAds = true;
		return _isWaitingForAds;


		//_remainAdsTimeSpan
	}


    /// <summary>
    /// 남은 프리크레인 리셋 시간 체크
    /// </summary>
    /// <returns></returns>
    private int GetFreeCraneResetTime() {
        int nextResetHour = 0;

        _nextFreeCraneResetTime = DateTime.Now.AddTicks(_timeGapBetweenServerClient);

        if (_nextFreeCraneResetTime.Hour < 9)
            nextResetHour = 9;
        else if (_nextFreeCraneResetTime.Hour >= 9 && _nextFreeCraneResetTime.Hour < 21)
            nextResetHour = 21;
        else if (_nextFreeCraneResetTime.Hour >= 21)
            nextResetHour = 33; // 내일 오전 9시로 설정 
        else
            return -1;

        // 00시 00분으로 만들고, nextResetHour을 더함. 
        _nextFreeCraneResetTime = _nextFreeCraneResetTime.AddHours(_nextFreeCraneResetTime.Hour * -1);
        _nextFreeCraneResetTime = _nextFreeCraneResetTime.AddMinutes(_nextFreeCraneResetTime.Minute * -1);

        _nextFreeCraneResetTime = _nextFreeCraneResetTime.AddHours(nextResetHour);

        // 예정시간을 초단위로 구한다.
        _remainNextCoinHotTime = _nextFreeCraneResetTime - DateTime.Now.AddTicks(_timeGapBetweenServerClient);

        Debug.Log("★GetFreeCraneResetTime :: " + _remainNextCoinHotTime.TotalSeconds);

        return Mathf.RoundToInt((float)_remainNextCoinHotTime.TotalSeconds);


    }


    /// <summary>
    /// 네코의 선물 사용 가능 여부 체크 
    /// </summary>
    /// <returns></returns>
    public bool GetNekoRewardPossible() {
        // 9월 5일 업데이트에서 방식의 변경 (쿨타임이 없어진다.)
        if (UserNeko.Count < 1) {
            return false;
        }

        if (_remainnekogift > 0)
            return true;


        return false;
    }


	/// <summary>
	/// 다음 네코의 보은 시간 체크 
	/// </summary>
	/// <returns>The remain neko reward time tick.</returns>
	private string GetRemainNekoRewardTimeTick() {

        // 디바이스의 시간이 흐른만큼을 더한다. 
        _remainNekoRewardTimeSpan = _dtNextNekoRewardTime - DateTime.Now.AddTicks(_timeGapBetweenServerClient);

        // 고양이가 하나도 없는 경우
        if (UserNeko.Count < 1) {
            return string.Empty;
        }

        if (_isNekoRewardReady) {
            return string.Empty;
        }

        if (_remainnekogift <= 0)
            return string.Empty;



        if (_remainNekoRewardTimeSpan.Ticks <= 0 && !_isNekoRewardReady) {

            Debug.Log("▷▷ GetRemainNekoRewardTimeTick");



            // request_nekorewarddbkey 호출 
            //GameSystem.Instance.Post2NekoRewardDBKey();
            //GameSystem.Instance.Post2NekoGift();

            // 로비에게 고양이의 보은이 준비되었음을 알림. 
            LobbyCtrl.Instance.OnReadyNekoGift();

            // 빈 문자열 리턴 
            return string.Empty;
        }

        return string.Format("{0:D2}:{1:D2}", _remainNekoRewardTimeSpan.Minutes, _remainNekoRewardTimeSpan.Seconds);
    }


    /// <summary>
    /// 다음 깨우는 시간 체크 
    /// </summary>
    /// <returns></returns>
    public TimeSpan GetRemainWakeUpTimeTick() {

        // 이미 잠든 상태라면, null로 처리 
        if (IsRequestingWakeUp)
            return TimeSpan.Zero;

        _remainWakeUpTime = _dtRemainWakeUpTime - DateTime.Now.AddTicks(_timeGapBetweenServerClient);

        
        if(_remainWakeUpTime.Ticks <= 0) {
            IsRequestingWakeUp = true;
            return TimeSpan.Zero;
        }

        return _remainWakeUpTime;

    }


	/// <summary>
	/// 다음 하트 수령 시간 체크 
	/// </summary>
	/// <returns>The remain take heart time tick.</returns>
	public string GetRemainTakeHeartTimeTick() {

		if (_heartCount >= 5)
			return string.Empty;

		// 시간의 흐름을 체크한다. 
		// syncTime에서 디바이스의 시간이 흐른만큼을 더한다. 
		_remainHeartTakeTimeSpan = _dtNextHeartTakeTime - DateTime.Now.AddTicks(_timeGapBetweenServerClient);

		if (_remainHeartTakeTimeSpan.Ticks <= 0) {

			if(!IsRequesting)
				Post2TakeHeart();
			//OnAddHeart();

			return string.Empty;
		}


		return string.Format ("{0:D2}:{1:D2}", _remainHeartTakeTimeSpan.Minutes, _remainHeartTakeTimeSpan.Seconds);
	}


    /// <summary>
    /// 하트가 모두 충전될때가지 남은 시간(초) 
    /// </summary>
    /// <returns></returns>
    private int GetHeartFullChargeSeconds() {

        int returnValue = 0;

        if (_heartCount >= 5)
            return 0;

        _remainHeartTakeTimeSpan = _dtNextHeartTakeTime - DateTime.Now.AddTicks(_timeGapBetweenServerClient);

        returnValue = Mathf.RoundToInt((float)_remainHeartTakeTimeSpan.TotalSeconds);

        if (_heartCount == 4 && returnValue < 30)
            return 0;

        // 총 시간을 구한다.
        returnValue = returnValue + ((5 - HeartCount - 1) * 900);

        return returnValue;

    }

    private int GetNoConnectTime(int pDay) {

        System.TimeSpan _tempSpan = DateTime.Now.AddDays(pDay) - DateTime.Now;

        Debug.Log("GetNoConnectTime :: " + pDay + " :: " + _tempSpan.Seconds);

        return _tempSpan.Seconds;
    }


    /// <summary>
    /// 당일 다음 핫타임 시간 체크 
    /// </summary>
    /// <returns></returns>
    private int GetNextHotTimeSeconds() {

        if (DateTime.Now.AddTicks(_timeGapBetweenServerClient).Hour > 21)
            return 0;


        // 00시 00분으로 변경 후, 12시, 9시를 체크한다. 
        _nextCoinHotTime = DateTime.Now.AddTicks(_timeGapBetweenServerClient);

        Debug.Log("▶▶ GetNextHotTimeSeconds Current Time :: " + _nextCoinHotTime.Month + " / " + _nextCoinHotTime.Day + " : " + _nextCoinHotTime.Hour + " : " + _nextCoinHotTime.Minute);

        // 00시 00분으로 변경 한다. 
        _nextCoinHotTime = _nextCoinHotTime.AddHours(_nextCoinHotTime.Hour * -1);
        _nextCoinHotTime = _nextCoinHotTime.AddMinutes(_nextCoinHotTime.Minute * -1);

        if (_dtSyncTime.Hour < 12)
            _nextCoinHotTime = _nextCoinHotTime.AddHours(12);
        else if (_dtSyncTime.Hour > 13 && _dtSyncTime.Hour < 21) {
            _nextCoinHotTime = _nextCoinHotTime.AddHours(21);
        }
        else {
            return 0;
        }

        // 예정시간 - 현재 시간을 체크. 
        _remainNextCoinHotTime = _nextCoinHotTime - DateTime.Now.AddTicks(_timeGapBetweenServerClient);

        return Mathf.RoundToInt((float)_remainNextCoinHotTime.TotalSeconds);

    }



	/// <summary>
	/// 남은 시간 조회 
	/// </summary>
	/// <returns>The remain time tick.</returns>
	public string GetRemainTimeTick() {
		_remainAdsTimeSpan = _possibleAdsTime - System.DateTime.Now;

		// 0가 되는 순간부터 
		if (_remainAdsTimeSpan.Ticks <= 0) {
			SaveNextAdsTime(0);
			return string.Empty;
		}

		return string.Format ("{0:D2}:{1:D2}", _remainAdsTimeSpan.Minutes, _remainAdsTimeSpan.Seconds);
	}


#region Equip Neko 

    public void CheckEquipNekoInfo() {
        if(_listEquipNekoID.Count < 3) {
            ClearEquipNekoInfo();
        }
    }


    public void ClearEquipNekoInfo() {
        for(int i=0; i<3; i++) {
            _listEquipNekoID[i] = -1;
        }

        ES2.Save(_listEquipNekoID, "EquipNekoPos");
    }

	public void SaveEquipNekoInfo(int pIndex, int pID) {

		_listEquipNekoID [pIndex] = pID;

		//ES2.Save<List<int>> (_listEquipNekoID, "EquipNeko");
		ES2.Save (_listEquipNekoID, "EquipNekoPos");
	}

	public void LoadEquipNekoInfo() {


		_listEquipNekoID = new List<int>();

		for( int i=0; i<3; i++) {
			_listEquipNekoID.Add(-1);
		}
		
		if (ES2.Exists ("EquipNekoPos")) {
			_listEquipNekoID = ES2.LoadList<int>("EquipNekoPos");
		}


        

		if (_listEquipNekoID.Count < 2) {
			_listEquipNekoID.Clear();
			for( int i=0; i<3; i++) {
				_listEquipNekoID.Add(-1);
			}
		}

        
    }

#endregion






    #region 상단 정보 업데이트 

    /// <summary>
    /// 상단정보 갱신 
    /// </summary>
    public void UpdateTopInfomation() {
		if (LobbyCtrl.Instance != null) {
			LobbyCtrl.Instance.UpdateTopInformation();
		}
		
	}

#endregion



	/// <summary>
	/// 블록 생성 변수 세팅 
	/// </summary>
	private void SetBlockGenerateSetting() {
		_height = 9; // 세로 컬럼
		_width = 9; // 가로 컬럼 

		_fillBlockCount = 25;
		_blockTypeCount = 3;

		_blockStartX = -3.1f;
		_blockStartY = 1.47f;

		_blockScaleValue = 1;
		_blockScale = new Vector3 (_blockScaleValue, _blockScaleValue, 1);
	}





    public UIAtlas GetNekoUIAtlas(int pCollectionIndex) {

        //Debug.Log("GetNekoUIAtlas :: " + pAtlas);
            
        switch(pCollectionIndex) {
            case 0:
                return globalCatCollection1;
            case 1:
                return globalCatCollection2;
            default:
                return null;
        }


    }


	public void LoadLobbyScene() {
		Fader.Instance.FadeIn (0.5f).LoadLevel ("SceneLobby").FadeOut (3f);
	}

	
	public void LoadTitleScene() {
		Fader.Instance.FadeIn (0.2f).LoadLevel ("SceneTitle").FadeOut (1f);
	}

    /// <summary>
    /// 타이틀 씬 로드 (초기화 처리 포함 
    /// </summary>
    public void LoadTitleSceneWithInitialize() {
        DeleteAllLocalData();
        //Application.Quit(); // 종료로 변경. 2016.07
        ExitGame();
    }

    /// <summary>
    /// 게임 종료 
    /// </summary>
    public void ExitGame() {
        SetHeartFullLocalNotification(); // Local Notification을 OnApplicationQuit에서 실행하면 게임 종료 후 Crash 가 발생한다.
        // SetFreeCraneResetNotification();
        Invoke("Quit", 0.5f);
    }

    private void Quit() {
        Application.Quit();
    }



	public void UnLockUIInput() {
		if(GameObject.FindGameObjectWithTag ("UILockScreen") != null)
			GameObject.FindGameObjectWithTag ("UILockScreen").SetActive (false);
	}


	public void ModifyGold(int pGold) {

		UserGold = UserGold + pGold; // 계산된 값 미리 할당. 

		UpdateTopInfomation ();

	}


	/// <summary>
	/// 안드로이드 팝업창 dialog (강제종료 처리)
	/// 안드로이드 ver.
	/// </summary>
	/// <param name="result">Result.</param>
	private void OnMessageCloseExitGame(AndroidDialogResult result) {
	}


	public void ClearFeedFish() {
		_feedChub = 0;
		_feedTuna = 0;
		_feedSalmon = 0;
	}







	/// <summary>
	/// 소유한 고양이 정보 체크 
	/// </summary>
	/// <returns><c>true</c>, if user neko data was checked, <c>false</c> otherwise.</returns>
	/// <param name="pNekoID">P neko I.</param>
	public bool CheckUserNekoData(int pNekoID) {

        for (int i = 0; i < UserNeko.Count; i++) {

            if (UserNeko[i]["tid"].AsInt == pNekoID)
                return true;

        }

        return false;
    }

	/// <summary>
	/// JSON index 찾기 
	/// </summary>
	/// <returns>The user neko data.</returns>
	/// <param name="pNekoID">P neko I.</param>
	public int FindUserNekoData(int pNekoID) {
        for (int i = 0; i < UserNeko.Count; i++) {

            if (UserNeko[i]["tid"].AsInt == pNekoID) {
                return i;
            }
        }

        return -1;
    }

	/// <summary>
	/// JSON index 찾기 by nekodbkey
	/// </summary>
	/// <returns>The user neko data.</returns>
	/// <param name="pNekoID">P neko I.</param>
	public int FindUserNekoDataByDBkey(int pDBkey) {
        for (int i = 0; i < UserNeko.Count; i++) {

            if (UserNeko[i]["dbkey"].AsInt == pDBkey) {
                return i;
            }
        }

        return -1;
    }

	/// <summary>
	/// Finds the neko star by neko ID.
	/// </summary>
	/// <returns>The neko star by neko I.</returns>
	/// <param name="pNekoID">P neko I.</param>
	public int FindNekoStarByNekoID(int pNekoID) {
        for (int i = 0; i < UserNeko.Count; i++) {

            if (UserNeko[i]["tid"].AsInt == pNekoID) {
                return UserNeko[i]["star"].AsInt;
            }
        }

        return -1;
    }

	public int GetUserNekoCount() {
        return UserNeko.Count;
    }



	/// <summary>
	/// 새로운 네코 정보 추가 
	/// </summary>
	/// <param name="pNode">P node.</param>
	public void AddNewNeko(JSONNode pNode) {

        Debug.Log("★★ AddNewNeko :: " + pNode.ToString());

        UserNeko.Add(pNode);
        // UserNeko = _userNekoJSON;

        // List에 추가 
        InitSortUserNeko();

        BottleManager.Instance.SpawnSingleNeko(pNode);

        LobbyCtrl.Instance.EnableNewNekoAddedSign();
    }

	/// <summary>
	/// 새로운 네코 정보 업데이트(퓨전)
	/// </summary>
	/// <param name="pNode">P node.</param>
	public void UpdateNewNeko(JSONNode pNode) {

        int findIndex = FindUserNekoData(pNode["tid"].AsInt);

        if (findIndex < 0) {
            Debug.Log("▶ Can't Find Neko ");
            return;
        }

        // 변경사항 업데이트 (높은 경우에만 )

        if (UserNeko[findIndex]["star"].AsInt < pNode["star"].AsInt)
            UserNeko[findIndex]["star"].AsInt = pNode["star"].AsInt;

        if (UserNeko[findIndex]["bead"].AsInt < pNode["bead"].AsInt)
            UserNeko[findIndex]["bead"].AsInt = pNode["bead"].AsInt;


        // List에 추가 
        InitSortUserNeko();

        //Debug.Log ("▶ UpdateNewNeko :: " + _userNekoData.ToString ());

    }

    /// <summary>
    /// Adds the purchased package.
    /// </summary>
    /// <param name="sku">Sku.</param>
    public void AddPurchasedPackage(string sku) {
		Debug.Log ("▶ AddPurchasedPackage before count :: " + _userDataJSON [_jData] ["packagelist"].Count);
		_userDataJSON [_jData] ["packagelist"] [-1] = sku;

		Debug.Log ("▶ AddPurchasedPackage after count :: " + _userDataJSON [_jData] ["packagelist"].Count);

        //WindowManagerCtrl.Instance.CheckPackage ();
        WindowManagerCtrl.Instance.RefreshPackageAdopted();
        
	}

	public void DetectCheat() {
		AndroidMessage.Create ("확인", "프로그램 불법 침입 시도가 감지되어 게임을 강제 종료합니다.");
		Application.Quit ();
	}



    /// <summary>
    /// 로컬라이즈 텍스트 조회 
    /// </summary>
    /// <param name="pID"></param>
    /// <returns></returns>
    public string GetLocalizeText(MNP_Localize.rowIds pID) {
        return MNP_Localize.Instance.Rows[(int)pID].GetStringData(GameLanguage.ToString());
    }

    public string GetLocalizeText(string pID) {
        return MNP_Localize.Instance.GetRow("L" + pID).GetStringData(GameLanguage.ToString());
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="localizeID"></param>
    /// <returns></returns>
    public string GetLocalizeText(int localizeID) {

        return MNP_Localize.Instance.GetRow("L" + localizeID.ToString()).GetStringData(Application.systemLanguage.ToString());

    }

    /// <summary>
    /// 칼라 로컬라이즈 
    /// </summary>
    /// <param name="localizeID"></param>
    /// <param name="pCode"></param>
    /// <returns></returns>
    public string GetColoredLocalizeText(string localizeID, string pCode) {
        if (GameLanguage == SystemLanguage.Korean) {
            return pCode + GetLocalizeText(localizeID) + _endBBCode;
        }
        else {
            return pCode + GetLocalizeText(localizeID) + _endBBCode;
        }
    }

	// 정렬 초기화 
	private void InitSortUserNeko() {
        _listUserNeko.Clear();

        //Debug.Log("InitSortUserNeko Count :: " + _userNekoData[_jData]["nekodatas"].Count);

        for (int i = 0; i < UserNeko.Count; i++) {
            _listUserNeko.Add(UserNeko[i]);
        }
    }


	public void SortUserNekoByBead() {
		InitSortUserNeko (); 
		_listUserNeko.Sort (delegate(JSONNode node1, JSONNode node2) { return node2["bead"].AsInt.CompareTo(node1["bead"].AsInt);});
	}

	public void SortUserNekoByGet() {
		InitSortUserNeko ();
        _listUserNeko.Sort(delegate (JSONNode node1, JSONNode node2) { return node1["bead"].AsInt.CompareTo(node2["bead"].AsInt); });

        //_listUserNeko.Sort (delegate(JSONNode node1, JSONNode node2) { return node1["dbkey"].AsInt.CompareTo(node2["dbkey"].AsInt);});
    }


    







    /// <summary>
    /// 네코의 스킬 아이콘 세팅 
    /// </summary>
    /// <param name="pArrSprite"></param>
    /// <param name="pNekoID"></param>
    /// <param name="pNekoStar"></param>
    public void SetNekoSkillIcon(UISprite[] pArrSprite, int pNekoID, int pNekoStar) {
        
        string id = "Skill" +  (pNekoID.ToString() + pNekoStar.ToString()).ToString(); //  id는 네코 ID + 등급 

        //Debug.Log("▶ SetNekoSkillIcon id :: " + id);

        // 초기화
        for (int i = 0; i < pArrSprite.Length; i++) {
            pArrSprite[i].gameObject.SetActive(false);
        }


        // 첫번째 skill 컬림이 None이면 없음 처리  
        if (_NekoSkill.GetRow(id)._skill == "none") {
            return;
        }

        // 첫번째 컬럼 처리 
        SetSkillIcon(pArrSprite[0], _NekoSkill.GetRow(id)._skill);

        // 두번째 컬림 
        if(!string.IsNullOrEmpty(_NekoSkill.GetRow(id)._skill2)) {
            SetSkillIcon(pArrSprite[1], _NekoSkill.GetRow(id)._skill2);
        }


        // 세번째 컬럼 
        if (!string.IsNullOrEmpty(_NekoSkill.GetRow(id)._skill3)) {
            SetSkillIcon(pArrSprite[2], _NekoSkill.GetRow(id)._skill3);
        }
    }

    /// <summary>
    /// 대상 스프라이트에 스킬 타입별 아이콘 처리 
    /// </summary>
    /// <param name="pSprite"></param>
    /// <param name="pType"></param>
    public void SetSkillIcon(UISprite pSprite, string pType) {
        pSprite.gameObject.SetActive(true);

        switch (pType) {
            case "score_passive":
                pSprite.spriteName = PuzzleConstBox.listNekoSkillSprite[0];
                break;
            case "coin_passive":
                pSprite.spriteName = PuzzleConstBox.listNekoSkillSprite[1];
                break;
            case "time_passive":
                pSprite.spriteName = PuzzleConstBox.listNekoSkillSprite[2];
                break;
            case "fevertime_passive":
                pSprite.spriteName = PuzzleConstBox.listNekoSkillSprite[3];
                break;
            case "power_passive":
                pSprite.spriteName = PuzzleConstBox.listNekoSkillSprite[4];
                break;
            case "yellowblock_score_passive":
                pSprite.spriteName = PuzzleConstBox.listNekoSkillSprite[5];
                break;
            case "blueblock_score_passive":
                pSprite.spriteName = PuzzleConstBox.listNekoSkillSprite[6];
                break;
            case "redblock_score_passive":
                pSprite.spriteName = PuzzleConstBox.listNekoSkillSprite[7];
                break;
            case "yellowblock_appear_passive":
                pSprite.spriteName = PuzzleConstBox.listNekoSkillSprite[8];
                break;
            case "blueblock_appear_passive":
                pSprite.spriteName = PuzzleConstBox.listNekoSkillSprite[9];
                break;
            case "redblock_appear_passive":
                pSprite.spriteName = PuzzleConstBox.listNekoSkillSprite[10];
                break;
            case "bomb_appear_passive":
                pSprite.spriteName = PuzzleConstBox.listNekoSkillSprite[11];
                break;

            case "nekoskill_appear_passive":
                pSprite.spriteName = PuzzleConstBox.listNekoSkillSprite[12];
                break;
            case "userexp_passive":
                pSprite.spriteName = PuzzleConstBox.listNekoSkillSprite[13];
                break;
            case "random_bomb_active":
                pSprite.spriteName = PuzzleConstBox.listNekoSkillSprite[14];
                break;
            case "combo_maintain_active":
                pSprite.spriteName = PuzzleConstBox.listNekoSkillSprite[15];
                break;
            case "fever_raise_active":
                pSprite.spriteName = PuzzleConstBox.listNekoSkillSprite[16];
                break;
            case "time_active":
                pSprite.spriteName = PuzzleConstBox.listNekoSkillSprite[17];
                break;
            case "yellow_bomb_active":
                pSprite.spriteName = PuzzleConstBox.listNekoSkillSprite[18];
                break;
            case "blue_bomb_active":
                pSprite.spriteName = PuzzleConstBox.listNekoSkillSprite[19];
                break;
            case "red_bomb_active":
                pSprite.spriteName = PuzzleConstBox.listNekoSkillSprite[20];
                break;
            case "black_bomb_active":
                pSprite.spriteName = PuzzleConstBox.listNekoSkillSprite[21];
                break;


        }
    }


    public void InitFishFeed() {
		_feedChub = 0;
		_feedTuna = 0;
		_feedSalmon = 0;

	}

	public string GetNumberToString(int pNum) {

        if (pNum < 1000)
            return pNum.ToString();
        else
            return pNum.ToString("#,###");
        
	}



    /// <summary>
    /// 고양이 스프라이트 이름 
    /// </summary>
    /// <param name="pID"></param>
    /// <param name="pGrade"></param>
    /// <returns></returns>
    public string GetNekoSpriteName(int pID, int pGrade) {

        
        // 5성일때 변화 
        if ("Y".Equals(NekoInfo.Rows[GetNekoRowID(pID)]._five_grade_change) && pGrade == 5) {
            return NekoInfo.Rows[GetNekoRowID(pID)]._five_main_sprite;
        }
        else {
            return NekoInfo.Rows[GetNekoRowID(pID)]._main_sprite;
        }
        
    }

    /// <summary>
    /// 고양이 외형 세팅 
    /// </summary>
    /// <param name="pSprite"></param>
    /// <param name="pID"></param>
    /// <param name="pGrade"></param>
    public void SetNekoSprite(UISprite pSprite, int pID, int pGrade) {
        // 외형 세팅 
        pSprite.atlas = GameSystem.Instance.GetNekoUIAtlas(NekoInfo.Rows[GetNekoRowID(pID)]._collection_index);
            

        // 5성일때 변화 
        if ("Y".Equals(NekoInfo.Rows[GetNekoRowID(pID)]._five_grade_change) && pGrade == 5) {
            //five_main_sprite
            
            pSprite.atlas = GameSystem.Instance.GetNekoUIAtlas(NekoInfo.Rows[GetNekoRowID(int.Parse(NekoInfo.Rows[GetNekoRowID(pID)]._five_main_id))]._collection_index);
            pSprite.spriteName = NekoInfo.Rows[GetNekoRowID(pID)]._five_main_sprite;
        }
        else {

            //main_prefix
            pSprite.spriteName = NekoInfo.Rows[GetNekoRowID(pID)]._main_sprite;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="pSprite"></param>
    /// <param name="pID"></param>
    public void SetNekoSpriteByID(UISprite pSprite, int pID) {
        pSprite.atlas = GameSystem.Instance.GetNekoUIAtlas(NekoInfo.Rows[GetNekoRowID(pID)]._collection_index);
        pSprite.spriteName = NekoInfo.Rows[GetNekoRowID(pID)]._main_sprite;
        pSprite.MakePixelPerfect();
    }


    public void SetNekoMiniSpriteByID(UISprite pSprite, int pID) {
        //pSprite.atlas = GameSystem.Instance.GetNekoUIAtlas(NekoInfo.Rows[GetNekoRowID(pID)]._collection_index);
        pSprite.spriteName = NekoInfo.Rows[GetNekoRowID(pID)]._mini_head;
    }


    public string GetNekoName(int pID, int pGrade) {


        if(pGrade == 5)
           return GameSystem.Instance.GetLocalizeText(NekoInfo.Rows[GetNekoRowID(pID)]._five_NameLocalID);
        else
            return GameSystem.Instance.GetLocalizeText(NekoInfo.Rows[GetNekoRowID(pID)]._NameLocalID);
    }

    /// <summary>
    /// 네코 설명 조회 
    /// </summary>
    /// <param name="pID"></param>
    /// <param name="pGrade"></param>
    /// <returns></returns>
    public string GetNekoDetail(int pID, int pGrade) {

        
        

        if (pGrade == 5)
            return GameSystem.Instance.GetLocalizeText(NekoInfo.Rows[GetNekoRowID(pID)]._five_InfoLocalID);
        else
            return GameSystem.Instance.GetLocalizeText(NekoInfo.Rows[GetNekoRowID(pID)]._InfoLocalID);
    }

    /// <summary>
    /// 사용자가 가지고 있는 네코 관련 메달 (Lv, Rank Max) 조회 
    /// </summary>
    /// <returns></returns>
    public JSONNode GetNekoMedal() {
        JSONNode result = JSON.Parse("{}");
        
        int max_grade;

        result["bronze"].AsInt = 0;
        result["silver"].AsInt = 0;
        result["gold"].AsInt = 0;

        for (int i = 0; i < UserNeko.Count; i++) {

            max_grade = int.Parse(NekoInfo.Rows[GetNekoRowID(UserNeko[i]["tid"].AsInt)]._max_grade);

            if (UserNeko[i]["star"].AsInt == 3
                && UserNeko[i]["level"].AsInt >= 40
                && max_grade == 3) {
                result["bronze"].AsInt = result["bronze"].AsInt + 1;
            }
            else if (UserNeko[i]["star"].AsInt == 4
                && UserNeko[i]["level"].AsInt >= 45
                && max_grade == 4) {
                result["silver"].AsInt = result["silver"].AsInt + 1;
            }
            else if (UserNeko[i]["star"].AsInt == 5
                && UserNeko[i]["level"].AsInt >= 50
                && max_grade == 5) {
                result["gold"].AsInt = result["gold"].AsInt + 1;
            }
        }

        return result;
    }

    /// <summary>
    /// 네코의 등급과 레벨로 메달 형태 체크 
    /// </summary>
    /// <param name="pStar"></param>
    /// <param name="pLevel"></param>
    /// <returns></returns>
    public NekoMedal GetNekoMedalType(int pNekoID, int pStar, int pLevel) {
        
        if (pStar == 3 && pLevel >= 40 && int.Parse(NekoInfo.Rows[pNekoID]._max_grade) == 3)
            return NekoMedal.bronze;
        else if (pStar == 4 && pLevel >= 45 && int.Parse(NekoInfo.Rows[pNekoID]._max_grade) == 4)
            return NekoMedal.silver;
        else if (pStar == 5 && pLevel >= 50 && int.Parse(NekoInfo.Rows[pNekoID]._max_grade) == 5)
            return NekoMedal.gold;
        else
            return NekoMedal.none;

    }

    

    /// <summary>
    /// 미니 네코 세팅 
    /// </summary>
    /// <param name="pSprite"></param>
    /// <param name="pID"></param>
    /// <param name="pGrade"></param>
    public void SetNekoMiniSprite(UISprite pSprite, int pID, int pGrade) {
        // 미니네코의 아틀라스는 같다. 
        // 5성일때 변화 
        
        if ("Y".Equals(NekoInfo.Rows[GetNekoRowID(pID)]._five_grade_change) && pGrade == 5) {
            pSprite.spriteName = NekoInfo.Rows[GetNekoRowID(pID)]._five_mini_head;
        }
        else {

            pSprite.spriteName = NekoInfo.Rows[GetNekoRowID(pID)]._mini_head;
        }

    }

    public void SetFishSprite(UISprite pSprite, string pFishType) {
        if(pFishType == "chub") {
            pSprite.spriteName = "fish-go-b";
        } else if(pFishType == "tuna") {
            pSprite.spriteName = "fish-tuna-b";
        } else if (pFishType == "salmon") {
            pSprite.spriteName = "fish-salmon-b";
        }
    }

    public void SetFishSprite(UISprite pSprite, FishType pType) {
        if (pType == FishType.Chub) {
            pSprite.spriteName = "fish-go-b";
        }
        else if (pType == FishType.Tuna) {
            pSprite.spriteName = "fish-tuna-b";
        }
        else if (pType == FishType.Salmon) {
            pSprite.spriteName = "fish-salmon-b";
        }
    }

    /// <summary>
    /// 네코 데이터 검증 및 제거 
    /// </summary>
    public void CheckClearUserNeko() {
        for (int i = UserNeko.Count - 1; i >= 0; i--) {
            if (UserNeko[i]["star"].AsInt == 0)
                UserNeko.Remove(i);
        }
    }

    #region 광고 처리 




    /// <summary>
    /// 체크 광고 통과 후 Callback 에서 ShowAd 
    /// </summary>
    /// <param name="pType"></param>
    public void CheckAd(AdsType pType) {

        if (pType == AdsType.HeartAds) {
            GameSystem.Instance.Post2RequestAdsRemainFillHeart();
            return;
        }

        
        else if (pType == AdsType.GatchaAds) {
            GameSystem.Instance.Post2RequestAdsRemainFreeGatcha();
            //ShowAd(AdsType.GatchaAds);
            return;
        }
        
        ShowAd(AdsType.NekoGiftAds);
    }

    /// <summary>
    /// 광고 재생 처리 
    /// </summary>
    /// <param name="pType"></param>
    public void ShowAd(AdsType pType) {


        int randomNumber = -1;

        // AdColony와 UnityAds 번갈아 송출
        if(Application.internetReachability == NetworkReachability.NotReachable) {
            Debug.Log("Network is not enable");
        }

        Debug.Log("▶▶▶ ShowAd UnityAds::" + Advertisement.IsReady().ToString() + " / AdColony::" + AdColonyMgr.Instance.IsVideoAvailable().ToString());
        //AdColonyMgr.Instance.ShowAd(pType);
        

        // 둘다 모두 소진 된 경우 
        
        if(!Advertisement.IsReady() && !AdColonyMgr.Instance.IsVideoAvailable()) {
            LobbyCtrl.Instance.OpenInfoPopUp(PopMessageType.AdShortage); //광고 소진 팝업
            return;
        }

        if (Advertisement.IsReady() && !AdColonyMgr.Instance.IsVideoAvailable()) {
            UnityAdsMgr.Instance.ShowAd(pType);
            return;
        }
        else if (!Advertisement.IsReady() && AdColonyMgr.Instance.IsVideoAvailable()) {
            AdColonyMgr.Instance.ShowAd(pType);
            return;
        }
        else if (Advertisement.IsReady() && AdColonyMgr.Instance.IsVideoAvailable()) { // 둘다 사용가능한 경우 랜덤 플레이 


            randomNumber = UnityEngine.Random.Range(0, 2);

            if(randomNumber == 0)
                UnityAdsMgr.Instance.ShowAd(pType);
            else
                AdColonyMgr.Instance.ShowAd(pType);
            return;
        }

        

    }


    /// <summary>
    /// Adfurikun Callback
    /// </summary>
    /// <param name="vars"></param>
    /*
    void MovieRewardCallback(ArrayList vars) {
        int stateName = (int)vars[0];
        string appID = (string)vars[1];
        string adnetworkKey = (string)vars[2];

        AdfurikunMovieRewardUtility.ADF_MovieStatus state = (AdfurikunMovieRewardUtility.ADF_MovieStatus)stateName; switch (state) {
            case AdfurikunMovieRewardUtility.ADF_MovieStatus.PrepareSuccess:
                Debug.Log("★ Adfurikun MovieRewardCallback :: PrepareSuccess");
                //"준비완료"  
                break;
            case AdfurikunMovieRewardUtility.ADF_MovieStatus.StartPlaying:
                Debug.Log("★ Adfurikun MovieRewardCallback :: StartPlaying");
                //"재생시작"  
                break;
            case AdfurikunMovieRewardUtility.ADF_MovieStatus.FinishedPlaying:
                Debug.Log("★ Adfurikun MovieRewardCallback :: FinishedPlaying");
                //"재생완료"  

                OnAdvertisementFinished(CurrentAdsType);

                break;
            case AdfurikunMovieRewardUtility.ADF_MovieStatus.FailedPlaying:
                Debug.Log("★ Adfurikun MovieRewardCallback :: FailedPlaying");
                //"재생실패"  
                break;
            case AdfurikunMovieRewardUtility.ADF_MovieStatus.AdClose:
                Debug.Log("★ Adfurikun MovieRewardCallback :: AdClose");
                //"동영상을 닫음"  
                break;

            case AdfurikunMovieRewardUtility.ADF_MovieStatus.NotPrepared:
                Debug.Log("★ Adfurikun MovieRewardCallback :: NotPrepared");
                LobbyCtrl.Instance.OpenInfoPopUp(PopMessageType.AdShortage); //광고 소진 팝업
                break;
        
            default:
                return;
        }
    }
    */


    /// <summary>
    /// 동영상 광고 플레이 완료
    /// </summary>
    /// <param name="pType"></param>
    public void OnAdvertisementFinished(AdsType pType) {

        Debug.Log("OnAdvertisementFinished :: " + pType.ToString());
        

        if (pType == AdsType.GatchaAds) { // 뽑기 광고 

            //Post2GatchaAds();
            // 뽑기 광고 시청 후 , Free Crane 화면을 오픈.
            LobbyCtrl.Instance.OpenFreeCrane();
            AdbrixManager.Instance.SendAdbrixInAppActivity(AdbrixManager.Instance.VIEW_NEKO_MOVIE);

            // 미션 체크 
            CheckMissionProgress(MissionType.Day, 13, 1);
            CheckMissionProgress(MissionType.Week, 13, 1);

        }
        else if (pType == AdsType.HeartAds) { // 하트 1개 얻기 용도 

            Post2HeartAds();
            AdbrixManager.Instance.SendAdbrixInAppActivity(AdbrixManager.Instance.VIEW_HEART_MOVIE);

        }
        else if (pType == AdsType.NekoGiftAds) {
            //AdbrixManager.Instance.SendAppsFlyerEvent(AdbrixManager.Instance.AF_FREE_GIFT2);
            Post2NekoGift(1);
        }

        if(LobbyCtrl.Instance != null) {
            LobbyCtrl.Instance.CheckBGM();
        }

    }


#endregion



#region ios gamecenter login


    /// <summary>
    /// ios 업적 로드 완료 
    /// </summary>
    /// <param name="result"></param>
    private void OnAchievementsLoadedIOS(SA.Common.Models.Result result) {
        if (result.IsSucceeded) {
            Debug.Log("Achievemnts was loaded from IOS Game Center");

            foreach (GK_AchievementTemplate tpl in GameCenterManager.Achievements) {
                Debug.Log(tpl.Id + ":  " + tpl.Progress);
            }
        }
    }


    /// <summary>
    /// ios 리더보드 로드 완료  
    /// </summary>
    /// <param name="result"></param>
    private void OnLeadrboardInfoLoaded(GK_LeaderboardResult result) {

        Debug.Log(">>> OnLeadrboardInfoLoaded");
        GameCenterManager.OnLeadrboardInfoLoaded -= OnLeadrboardInfoLoaded;

        if (result.IsSucceeded) {
            GK_Score score = result.Leaderboard.GetCurrentPlayerScore(GK_TimeSpan.ALL_TIME, GK_CollectionType.GLOBAL);
            //IOSNativePopUpManager.showMessage("Leaderboard " + score.LeaderboardId, "Score: " + score.LongScore + "\n" + "Rank:" + score.Rank);

            Debug.Log("double score representation: " + score.DecimalFloat_2);
            Debug.Log("long score representation: " + score.LongScore);
        }
    }


    #endregion

    #region 사용자 레벨에 따른 event 추적 

    private void TrackUserLevel() {
        /*
        if (_userPreLevel < _userlevel && _userlevel == 2)
            AdbrixManager.Instance.SendAppsFlyerEvent(AdbrixManager.Instance.AF_PLAYER_LEVEL_2);
        else if (_userPreLevel < _userlevel && _userlevel == 3)
            AdbrixManager.Instance.SendAppsFlyerEvent(AdbrixManager.Instance.AF_PLAYER_LEVEL_3);
        else if (_userPreLevel < _userlevel && _userlevel == 4)
            AdbrixManager.Instance.SendAppsFlyerEvent(AdbrixManager.Instance.AF_PLAYER_LEVEL_4);
        else if (_userPreLevel < _userlevel && _userlevel == 5)
            AdbrixManager.Instance.SendAppsFlyerEvent(AdbrixManager.Instance.AF_PLAYER_LEVEL_5);
        else if (_userPreLevel < _userlevel && _userlevel == 6)
            AdbrixManager.Instance.SendAppsFlyerEvent(AdbrixManager.Instance.AF_PLAYER_LEVEL_6);
        else if (_userPreLevel < _userlevel && _userlevel == 7)
            AdbrixManager.Instance.SendAppsFlyerEvent(AdbrixManager.Instance.AF_PLAYER_LEVEL_7);
        else if (_userPreLevel < _userlevel && _userlevel == 8)
            AdbrixManager.Instance.SendAppsFlyerEvent(AdbrixManager.Instance.AF_PLAYER_LEVEL_8);
        else if (_userPreLevel < _userlevel && _userlevel == 9)
            AdbrixManager.Instance.SendAppsFlyerEvent(AdbrixManager.Instance.AF_PLAYER_LEVEL_9);
        else if (_userPreLevel < _userlevel && _userlevel == 10)
            AdbrixManager.Instance.SendAppsFlyerEvent(AdbrixManager.Instance.AF_PLAYER_LEVEL_10);
        else if (_userPreLevel < _userlevel && _userlevel == 15)
            AdbrixManager.Instance.SendAppsFlyerEvent(AdbrixManager.Instance.AF_PLAYER_LEVEL_15);
        else if (_userPreLevel < _userlevel && _userlevel == 20)
            AdbrixManager.Instance.SendAppsFlyerEvent(AdbrixManager.Instance.AF_PLAYER_LEVEL_20);
        else if (_userPreLevel < _userlevel && _userlevel == 25)
            AdbrixManager.Instance.SendAppsFlyerEvent(AdbrixManager.Instance.AF_PLAYER_LEVEL_25);
        else if (_userPreLevel < _userlevel && _userlevel == 30)
            AdbrixManager.Instance.SendAppsFlyerEvent(AdbrixManager.Instance.AF_PLAYER_LEVEL_30);
        else if (_userPreLevel < _userlevel && _userlevel == 35)
            AdbrixManager.Instance.SendAppsFlyerEvent(AdbrixManager.Instance.AF_PLAYER_LEVEL_35);
        else if (_userPreLevel < _userlevel && _userlevel == 40)
            AdbrixManager.Instance.SendAppsFlyerEvent(AdbrixManager.Instance.AF_PLAYER_LEVEL_40);
        else if (_userPreLevel < _userlevel && _userlevel == 45)
            AdbrixManager.Instance.SendAppsFlyerEvent(AdbrixManager.Instance.AF_PLAYER_LEVEL_45);
        else if (_userPreLevel < _userlevel && _userlevel == 50)
            AdbrixManager.Instance.SendAppsFlyerEvent(AdbrixManager.Instance.AF_PLAYER_LEVEL_50);
            */
    }

    #endregion


    /// <summary>
    /// 가챠 이미지의 타입 구하기 
    /// </summary>
    /// <param name="pURL"></param>
    /// <returns></returns>
    private string GetGatchaImageType(string pURL) {
        for(int i=0; i<_gatchaBannerInitJSON.Count; i++) {
            if(GatchaBannerInitJSON[i]["smallbannerurl"].Value == pURL || GatchaBannerInitJSON[i]["bigbannerurl"].Value == pURL) {
                return GatchaBannerInitJSON[i]["type"].Value;
            }
        }


        return null;
    }


    public void DeleteMissionData() {
        if (ES2.Exists("mission_day_progress"))
            ES2.Delete("mission_day_progress");

        if (ES2.Exists("mission_week_progress"))
            ES2.Delete("mission_week_progress");

        if(ES2.Exists(BINGO_PROGRESS)) {
            ES2.Delete(BINGO_PROGRESS);
        }

        if (ES2.Exists(PuzzleConstBox.ES_NekoGradeOrder)) {
            ES2.Delete(PuzzleConstBox.ES_NekoGradeOrder);
        }


        if (ES2.Exists(PuzzleConstBox.ES_NoticeCheckDay)) {
            ES2.Delete(PuzzleConstBox.ES_NoticeCheckDay);
        }


        if (ES2.Exists(PuzzleConstBox.ES_MissionDay)) {
            ES2.Delete(PuzzleConstBox.ES_MissionDay);
        }

        if (ES2.Exists(PuzzleConstBox.ES_WeeklyMissionRefreshDay)) {
            ES2.Delete(PuzzleConstBox.ES_WeeklyMissionRefreshDay);
        }


        if (ES2.Exists(PuzzleConstBox.ES_EventWaterMelonCatch))
            ES2.Delete(PuzzleConstBox.ES_EventWaterMelonCatch);

        if (ES2.Exists(PuzzleConstBox.ES_EventSunBurnCatch))
            ES2.Delete(PuzzleConstBox.ES_EventSunBurnCatch);

        if (ES2.Exists(PuzzleConstBox.ES_GameStartCount))
            ES2.Delete(PuzzleConstBox.ES_GameStartCount);

        if (ES2.Exists(PuzzleConstBox.ES_NotRenewBestScore))
            ES2.Delete(PuzzleConstBox.ES_NotRenewBestScore);



    }

    /// <summary>
    /// 로컬 데이터 삭제 처리 
    /// </summary>
    public void DeleteAllLocalData() {
        if (ES2.Exists("mission_day_progress"))
            ES2.Delete("mission_day_progress");

        if (ES2.Exists("mission_week_progress"))
            ES2.Delete("mission_week_progress");

        if (ES2.Exists(BINGO_PROGRESS)) {
            ES2.Delete(BINGO_PROGRESS);
        }



        if (ES2.Exists(PuzzleConstBox.ES_NekoGradeOrder)) {
            ES2.Delete(PuzzleConstBox.ES_NekoGradeOrder);
        }
        if (ES2.Exists(PuzzleConstBox.ES_NoticeCheckDay)) {
            ES2.Delete(PuzzleConstBox.ES_NoticeCheckDay);
        }


        if (ES2.Exists(PuzzleConstBox.ES_MissionDay)) {
            ES2.Delete(PuzzleConstBox.ES_MissionDay);
        }

        if (ES2.Exists(PuzzleConstBox.ES_WeeklyMissionRefreshDay)) {
            ES2.Delete(PuzzleConstBox.ES_WeeklyMissionRefreshDay);
        }


        if (ES2.Exists(PuzzleConstBox.ES_EventWaterMelonCatch))
            ES2.Delete(PuzzleConstBox.ES_EventWaterMelonCatch);

        if (ES2.Exists(PuzzleConstBox.ES_EventSunBurnCatch))
            ES2.Delete(PuzzleConstBox.ES_EventSunBurnCatch);


        if (ES2.Exists(PuzzleConstBox.ES_GameStartCount))
            ES2.Delete(PuzzleConstBox.ES_GameStartCount);

        if (ES2.Exists(PuzzleConstBox.ES_NotRenewBestScore))
            ES2.Delete(PuzzleConstBox.ES_NotRenewBestScore);

    }


    /// <summary>
    /// 네코 업그레이드 비용 
    /// </summary>
    /// <param name="pNekoLevel"></param>
    /// <returns></returns>
    public int GetNekoUpgradeCost(int pNekoLevel) {


        return MNP_NekoUpgradeCost.Instance.Rows[pNekoLevel - 1]._cost;

        

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="pStar"></param>
    /// <returns></returns>
    public string GetNekoGradeText(int pStar) {

        string returnValue = string.Empty;

        for(int i =0; i< pStar; i++) {
            returnValue += "*";
        }

        return returnValue;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="pNekoID"></param>
    /// <param name="pStar"></param>
    /// <returns></returns>
    public bool GetMatchNekoData(int pNekoID, int pStar) {

        for (int i = 0; i < UserNeko.Count; i++) {


            if (UserNeko[i]["tid"].AsInt == pNekoID
                && UserNeko[i]["star"].AsInt == pStar) {
                return true;
            }

        }

        return false;

    }

    /// <summary>
    /// 현재 로컬 시간 구하기 
    /// </summary>
    /// <returns></returns>
    public DateTime GetCurrentRealTime() {
        return DateTime.Now.AddTicks(_timeGapBetweenServerClient);
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="pNekoID"></param>
    /// <returns></returns>
    public JSONNode GetNekoNodeByID(int pNekoID) {


        for (int i = 0; i < UserNeko.Count; i++) {

            if (UserNeko[i]["tid"].AsInt == pNekoID)
                return UserNeko[i];

        }

        return null;
    }



    /// <summary>
    /// 장착 고양이의 레벨 얻기 
    /// </summary>
    /// <param name="pNekoID"></param>
    /// <returns></returns>
    private int GetNekoLevel(int pNekoID) {
        JSONNode neko = GetNekoNodeByID(pNekoID);

        if (neko == null)
            return 0;

        return neko["level"].AsInt;
    }

	private int GetNekoGrade(int pNekoID) {
		JSONNode neko = GetNekoNodeByID(pNekoID);

		if (neko == null)
			return 0;

		return neko["star"].AsInt;
	}

    /// <summary>
    /// 인게임상의 네코 파워 조회 
    /// </summary>
    /// <returns></returns>
    public float GetNekoInGamePower(int pNekoID) {

        float power = 100;
        JSONNode neko = GetNekoNodeByID(pNekoID);

        if (neko == null)
            return 0;

        

        switch(neko["star"].AsInt) {
            case 1:
                power = 100;
                break;
            case 2:
                power = 150;
                break;
            case 3:
                power = 250;
                break;
            case 4:
                power = 400;
                break;
            case 5:
                power = 600;
                break;

        }

        power += (neko["level"].AsInt - 1) * 30;


        // 패시브 능력 처리 
        if (GameSystem.Instance.NekoPowerPlus > 0) {
            power = power + (power * GameSystem.Instance.NekoPowerPlus / 100);
        }

        return power;
    }

    /// <summary>
    /// 장착 고양이의 그룹 세팅 
    /// </summary>
    private void SetEquipNekoGroup() {

        Debug.Log("▶▶ SetEquipNekoGroup ::" + _listEquipNekoID.Count);


        if (_listEquipNekoID.Count == 0)
            return;


        #region

        if(WWWHelper.Instance.UserDBKey == 257 && IsAdminUser) {
            _equipGroup1Neko = true;
            _equipGroup2Neko = true;
            _equipGroup3Neko = true;
            _equipGroup4Neko = true;
            _equipGroup5Neko = true;
            _equipGroup6Neko = true;
            _equipGroup7Neko = true;
            _equipGroup8Neko = true;
            _equipGroup9Neko = true;
            _equipGroup10Neko = true;
            _equipGroup11Neko = true;
            _equipGroup12Neko = true;
            _equipGroup13Neko = true;
            _equipGroup14Neko = true;
            _equipGroup15Neko = true;
            _equipGroup16Neko = true;
            _equipGroup17Neko = true;
            _equipGroup18Neko = true;
            _equipGroup19Neko = true;
            _equipGroup20Neko = true;
            _equipGroup21Neko = true;
            _equipGroup22Neko = true;
            _equipGroup23Neko = true;
            _equipGroup24Neko = true;
            _equipGroup25Neko = true;
            _equipGroup26Neko = true;
            _equipGroup27Neko = true;
            _equipGroup28Neko = true;
            _equipGroup29Neko = true;
            _equipGroup30Neko = true;
            _equipGroup31Neko = true;


            _equipLevel10Neko = true;
            _equipLevel20Neko = true;
            _equipLevel30Neko = true;
            _equipLevel50Neko = true;
            _equipRank5Neko = true;
        }

        #endregion




        for (int i=0; i < _listEquipNekoID.Count; i++ ) {

			_tempInt = _listEquipNekoID [i];
			// 5성 check
			if (FindNekoStarByNekoID (_tempInt) == 5) {

                

                _tempInt = int.Parse(NekoInfo.Rows[_tempInt]._five_main_id); // five main id
			}


            // 각 그룹별 체크 1~25
			if (!EquipGroup1Neko && _listBingoGroup1.Contains(_tempInt)) {
                _equipGroup1Neko = true;
            }

			if(!EquipGroup2Neko && _listBingoGroup2.Contains(_tempInt)) {
                _equipGroup2Neko = true;
            }

			if (!EquipGroup3Neko && _listBingoGroup3.Contains(_tempInt)) {
                _equipGroup3Neko = true;
            }

			if (!EquipGroup4Neko && _listBingoGroup4.Contains(_tempInt)) {
                _equipGroup4Neko = true;
            }

			if (!EquipGroup5Neko && _listBingoGroup5.Contains(_tempInt)) {
                _equipGroup5Neko = true;
            }

			if (!EquipGroup6Neko && _listBingoGroup6.Contains(_tempInt)) {
                _equipGroup6Neko = true;
            }

			if (!EquipGroup7Neko && _listBingoGroup7.Contains(_tempInt)) {
                _equipGroup7Neko = true;
            }

			if (!EquipGroup8Neko && _listBingoGroup8.Contains(_tempInt)) {
                _equipGroup8Neko = true;
            }

            if (!EquipGroup9Neko && _listBingoGroup9.Contains(_tempInt)) {
                _equipGroup9Neko = true;
            }
            if (!EquipGroup10Neko && _listBingoGroup10.Contains(_tempInt)) {
                _equipGroup10Neko = true;
            }
            if (!EquipGroup11Neko && _listBingoGroup11.Contains(_tempInt)) {
                _equipGroup11Neko = true;
            }
            if (!EquipGroup12Neko && _listBingoGroup12.Contains(_tempInt)) {
                _equipGroup12Neko = true;
            }
            if (!EquipGroup13Neko && _listBingoGroup13.Contains(_tempInt)) {
                EquipGroup13Neko = true;
            }
            if (!EquipGroup14Neko && _listBingoGroup14.Contains(_tempInt)) {
                EquipGroup14Neko = true;
            }
            if (!EquipGroup15Neko && _listBingoGroup15.Contains(_tempInt)) {
                EquipGroup15Neko = true;
            }
            if (!EquipGroup16Neko && _listBingoGroup16.Contains(_tempInt)) {
                EquipGroup16Neko = true;
            }
            if (!EquipGroup17Neko && _listBingoGroup17.Contains(_tempInt)) {
                EquipGroup17Neko = true;
            }
            if (!EquipGroup18Neko && _listBingoGroup18.Contains(_tempInt)) {
                EquipGroup18Neko = true;
            }
            if (!EquipGroup19Neko && _listBingoGroup19.Contains(_tempInt)) {
                EquipGroup19Neko = true;
            }
            if (!EquipGroup20Neko && _listBingoGroup20.Contains(_tempInt)) {
                EquipGroup20Neko = true;
            }
            if (!EquipGroup21Neko && _listBingoGroup21.Contains(_tempInt)) {
                EquipGroup21Neko = true;
            }
            if (!EquipGroup22Neko && _listBingoGroup22.Contains(_tempInt)) {
                EquipGroup22Neko = true;
            }
            if (!EquipGroup23Neko && _listBingoGroup23.Contains(_tempInt)) {
                EquipGroup23Neko = true;
            }
            if (!EquipGroup24Neko && _listBingoGroup24.Contains(_tempInt)) {
                EquipGroup24Neko = true;
            }
            if (!EquipGroup25Neko && _listBingoGroup25.Contains(_tempInt)) {
                EquipGroup25Neko = true;
            }

            if (!EquipGroup26Neko && _listBingoGroup26.Contains(_tempInt)) {
                EquipGroup26Neko = true;
            }


            if (!EquipGroup27Neko && _listBingoGroup27.Contains(_tempInt)) {
                EquipGroup27Neko = true;
            }

            if (!EquipGroup28Neko && _listBingoGroup28.Contains(_tempInt)) {
                EquipGroup28Neko = true;
            }

            if (!EquipGroup29Neko && _listBingoGroup29.Contains(_tempInt)) {
                EquipGroup29Neko = true;
            }

            if (!EquipGroup30Neko && _listBingoGroup30.Contains(_tempInt)) {
                EquipGroup30Neko = true;
            }

            if (!EquipGroup31Neko && _listBingoGroup31.Contains(_tempInt)) {
                EquipGroup31Neko = true;
            }



            // Neko Level
            if (!EquipLevel10Neko  &&  GetNekoLevel(_listEquipNekoID[i]) >= 10) {
                EquipLevel10Neko = true;
            }

			if (!EquipLevel20Neko  &&  GetNekoLevel(_listEquipNekoID[i]) >= 20) {
				EquipLevel20Neko = true;
			}

            if (!EquipLevel30Neko && GetNekoLevel(_listEquipNekoID[i]) >= 30) {
                EquipLevel30Neko = true;
            }

            if (!EquipLevel50Neko  &&  GetNekoLevel(_listEquipNekoID[i]) >= 50) {
				EquipLevel50Neko = true;
			}

			// Grade 
			if (!EquipRank5Neko && GetNekoGrade (_listEquipNekoID [i]) >= 5) {
				EquipRank5Neko = true;
			}

        }
    }


    #region ES2 value 로드 & 세이브 

    public int LoadESvalueInt(string pID) {
        if (ES2.Exists(pID))
            return ES2.Load<int>(pID);
        else
            return 0;
    }

    public bool LoadESvalueBool(string pID) {
        if (ES2.Exists(pID)) {
            //Debug.Log("★★★ " + pID + " : " + ES2.Load<bool>(pID));
            return ES2.Load<bool>(pID);
        }
        else {
            //Debug.Log("★★★ " + pID + " : false");
            return false;
        }
    }

    public string LoadESvalueString(string pID) {
        if (ES2.Exists(pID))
            return ES2.Load<string>(pID);
        else
            return null;
    }


    public void SaveESvalueInt(string pID, int pValue) {
        ES2.Save<int>(pValue, pID);
    }

    public void SaveESvalueBool(string pID, bool pValue) {
        ES2.Save<bool>(pValue, pID);
    }

    public void SaveESvalueString(string pID, string pValue) {
        ES2.Save<string>(pValue, pID);
    }


    #endregion


    #region G2U RowID 

    /// <summary>
    /// 
    /// </summary>
    /// <param name="pNekoID"></param>
    /// <returns></returns>
    public int GetNekoRowID(int pNekoID) {

        switch(pNekoID) {

            case 0: return (int)MNP_Neko.rowIds.Neko0;
            case 1: return (int)MNP_Neko.rowIds.Neko1;
            case 2: return (int)MNP_Neko.rowIds.Neko2;
            case 3: return (int)MNP_Neko.rowIds.Neko3;
            case 4: return (int)MNP_Neko.rowIds.Neko4;
            case 5: return (int)MNP_Neko.rowIds.Neko5;
            case 6: return (int)MNP_Neko.rowIds.Neko6;
            case 7: return (int)MNP_Neko.rowIds.Neko7;
            case 8: return (int)MNP_Neko.rowIds.Neko8;
            case 9: return (int)MNP_Neko.rowIds.Neko9;
            case 10: return (int)MNP_Neko.rowIds.Neko10;
            case 11: return (int)MNP_Neko.rowIds.Neko11;
            case 12: return (int)MNP_Neko.rowIds.Neko12;
            case 13: return (int)MNP_Neko.rowIds.Neko13;
            case 14: return (int)MNP_Neko.rowIds.Neko14;
            case 15: return (int)MNP_Neko.rowIds.Neko15;
            case 16: return (int)MNP_Neko.rowIds.Neko16;
            case 17: return (int)MNP_Neko.rowIds.Neko17;
            case 18: return (int)MNP_Neko.rowIds.Neko18;
            case 19: return (int)MNP_Neko.rowIds.Neko19;
            case 20: return (int)MNP_Neko.rowIds.Neko20;
            case 21: return (int)MNP_Neko.rowIds.Neko21;
            case 22: return (int)MNP_Neko.rowIds.Neko22;
            case 23: return (int)MNP_Neko.rowIds.Neko23;
            case 24: return (int)MNP_Neko.rowIds.Neko24;
            case 25: return (int)MNP_Neko.rowIds.Neko25;
            case 26: return (int)MNP_Neko.rowIds.Neko26;
            case 27: return (int)MNP_Neko.rowIds.Neko27;
            case 28: return (int)MNP_Neko.rowIds.Neko28;
            case 29: return (int)MNP_Neko.rowIds.Neko29;
            case 30: return (int)MNP_Neko.rowIds.Neko30;
            case 31: return (int)MNP_Neko.rowIds.Neko31;
            case 32: return (int)MNP_Neko.rowIds.Neko32;
            case 33: return (int)MNP_Neko.rowIds.Neko33;
            case 34: return (int)MNP_Neko.rowIds.Neko34;
            case 35: return (int)MNP_Neko.rowIds.Neko35;
            case 36: return (int)MNP_Neko.rowIds.Neko36;
            case 37: return (int)MNP_Neko.rowIds.Neko37;
            case 38: return (int)MNP_Neko.rowIds.Neko38;
            case 39: return (int)MNP_Neko.rowIds.Neko39;
            case 40: return (int)MNP_Neko.rowIds.Neko40;
            case 41: return (int)MNP_Neko.rowIds.Neko41;
            case 42: return (int)MNP_Neko.rowIds.Neko42;
            case 43: return (int)MNP_Neko.rowIds.Neko43;
            case 44: return (int)MNP_Neko.rowIds.Neko44;
            case 45: return (int)MNP_Neko.rowIds.Neko45;
            case 46: return (int)MNP_Neko.rowIds.Neko46;
            case 47: return (int)MNP_Neko.rowIds.Neko47;
            case 48: return (int)MNP_Neko.rowIds.Neko48;
            case 49: return (int)MNP_Neko.rowIds.Neko49;
            case 50: return (int)MNP_Neko.rowIds.Neko50;
            case 51: return (int)MNP_Neko.rowIds.Neko51;
            case 52: return (int)MNP_Neko.rowIds.Neko52;
            case 53: return (int)MNP_Neko.rowIds.Neko53;
            case 54: return (int)MNP_Neko.rowIds.Neko54;
            case 55: return (int)MNP_Neko.rowIds.Neko55;
            case 56: return (int)MNP_Neko.rowIds.Neko56;
            case 57: return (int)MNP_Neko.rowIds.Neko57;
            case 58: return (int)MNP_Neko.rowIds.Neko58;
            case 59: return (int)MNP_Neko.rowIds.Neko59;
            case 60: return (int)MNP_Neko.rowIds.Neko60;
            case 61: return (int)MNP_Neko.rowIds.Neko61;
            case 62: return (int)MNP_Neko.rowIds.Neko62;
            case 63: return (int)MNP_Neko.rowIds.Neko63;
            case 64: return (int)MNP_Neko.rowIds.Neko64;
            case 65: return (int)MNP_Neko.rowIds.Neko65;
            case 66: return (int)MNP_Neko.rowIds.Neko66;
            case 67: return (int)MNP_Neko.rowIds.Neko67;
            case 68: return (int)MNP_Neko.rowIds.Neko68;
            case 69: return (int)MNP_Neko.rowIds.Neko69;
            case 70: return (int)MNP_Neko.rowIds.Neko70;
            case 71: return (int)MNP_Neko.rowIds.Neko71;
            case 72: return (int)MNP_Neko.rowIds.Neko72;
            case 73: return (int)MNP_Neko.rowIds.Neko73;
            case 74: return (int)MNP_Neko.rowIds.Neko74;
            case 75: return (int)MNP_Neko.rowIds.Neko75;
            case 76: return (int)MNP_Neko.rowIds.Neko76;
            case 77: return (int)MNP_Neko.rowIds.Neko77;
            case 78: return (int)MNP_Neko.rowIds.Neko78;
            case 79: return (int)MNP_Neko.rowIds.Neko79;
            case 80: return (int)MNP_Neko.rowIds.Neko80;
            case 81: return (int)MNP_Neko.rowIds.Neko81;
            case 82: return (int)MNP_Neko.rowIds.Neko82;
            case 83: return (int)MNP_Neko.rowIds.Neko83;
            case 84: return (int)MNP_Neko.rowIds.Neko84;
            case 85: return (int)MNP_Neko.rowIds.Neko85;
            case 86: return (int)MNP_Neko.rowIds.Neko86;
            case 87: return (int)MNP_Neko.rowIds.Neko87;
            case 88: return (int)MNP_Neko.rowIds.Neko88;
            case 89: return (int)MNP_Neko.rowIds.Neko89;
            case 90: return (int)MNP_Neko.rowIds.Neko90;
            case 91: return (int)MNP_Neko.rowIds.Neko91;
            case 92: return (int)MNP_Neko.rowIds.Neko92;
            case 93: return (int)MNP_Neko.rowIds.Neko93;
            case 94: return (int)MNP_Neko.rowIds.Neko94;
            case 95: return (int)MNP_Neko.rowIds.Neko95;
            case 96: return (int)MNP_Neko.rowIds.Neko96;
            case 97: return (int)MNP_Neko.rowIds.Neko97;
            case 98: return (int)MNP_Neko.rowIds.Neko98;
            case 99: return (int)MNP_Neko.rowIds.Neko99;
            case 100: return (int)MNP_Neko.rowIds.Neko100;
            case 101: return (int)MNP_Neko.rowIds.Neko101;
            case 102: return (int)MNP_Neko.rowIds.Neko102;
            case 103: return (int)MNP_Neko.rowIds.Neko103;
            case 104: return (int)MNP_Neko.rowIds.Neko104;
            case 105: return (int)MNP_Neko.rowIds.Neko105;
            case 106: return (int)MNP_Neko.rowIds.Neko106;
            case 107: return (int)MNP_Neko.rowIds.Neko107;
            case 108: return (int)MNP_Neko.rowIds.Neko108;
            case 109: return (int)MNP_Neko.rowIds.Neko109;
            case 110: return (int)MNP_Neko.rowIds.Neko110;
            case 111: return (int)MNP_Neko.rowIds.Neko111;
            case 112: return (int)MNP_Neko.rowIds.Neko112;
            case 113: return (int)MNP_Neko.rowIds.Neko113;
            case 114: return (int)MNP_Neko.rowIds.Neko114;
            case 115: return (int)MNP_Neko.rowIds.Neko115;
            case 116: return (int)MNP_Neko.rowIds.Neko116;
            case 117: return (int)MNP_Neko.rowIds.Neko117;
            case 118: return (int)MNP_Neko.rowIds.Neko118;
            case 119: return (int)MNP_Neko.rowIds.Neko119;
            case 120: return (int)MNP_Neko.rowIds.Neko120;
            case 121: return (int)MNP_Neko.rowIds.Neko121;
            case 122: return (int)MNP_Neko.rowIds.Neko122;
            case 123: return (int)MNP_Neko.rowIds.Neko123;
            case 124: return (int)MNP_Neko.rowIds.Neko124;
            case 125: return (int)MNP_Neko.rowIds.Neko125;
            case 126: return (int)MNP_Neko.rowIds.Neko126;
            case 127: return (int)MNP_Neko.rowIds.Neko127;
            case 128: return (int)MNP_Neko.rowIds.Neko128;
            case 129: return (int)MNP_Neko.rowIds.Neko129;
            case 130: return (int)MNP_Neko.rowIds.Neko130;
            case 131: return (int)MNP_Neko.rowIds.Neko131;
            case 132: return (int)MNP_Neko.rowIds.Neko132;
            case 133: return (int)MNP_Neko.rowIds.Neko133;
            case 134: return (int)MNP_Neko.rowIds.Neko134;
            case 135: return (int)MNP_Neko.rowIds.Neko135;
            case 136: return (int)MNP_Neko.rowIds.Neko136;
            case 137: return (int)MNP_Neko.rowIds.Neko137;
            case 138: return (int)MNP_Neko.rowIds.Neko138;
            case 139: return (int)MNP_Neko.rowIds.Neko139;
            case 140: return (int)MNP_Neko.rowIds.Neko140;
            case 141: return (int)MNP_Neko.rowIds.Neko141;
            case 142: return (int)MNP_Neko.rowIds.Neko142;
            case 143: return (int)MNP_Neko.rowIds.Neko143;
            case 144: return (int)MNP_Neko.rowIds.Neko144;
            case 145: return (int)MNP_Neko.rowIds.Neko145;
            case 146: return (int)MNP_Neko.rowIds.Neko146;
            case 147: return (int)MNP_Neko.rowIds.Neko147;
            case 148: return (int)MNP_Neko.rowIds.Neko148;
            case 149: return (int)MNP_Neko.rowIds.Neko149;
            case 150: return (int)MNP_Neko.rowIds.Neko150;
            case 151: return (int)MNP_Neko.rowIds.Neko151;
            case 152: return (int)MNP_Neko.rowIds.Neko152;
            case 153: return (int)MNP_Neko.rowIds.Neko153;
            case 154: return (int)MNP_Neko.rowIds.Neko154;
            case 155: return (int)MNP_Neko.rowIds.Neko155;
            case 156: return (int)MNP_Neko.rowIds.Neko156;
            case 157: return (int)MNP_Neko.rowIds.Neko157;
            case 158: return (int)MNP_Neko.rowIds.Neko158;
            case 159: return (int)MNP_Neko.rowIds.Neko159;
            case 160: return (int)MNP_Neko.rowIds.Neko160;
            case 161: return (int)MNP_Neko.rowIds.Neko161;
            case 162: return (int)MNP_Neko.rowIds.Neko162;
            case 163: return (int)MNP_Neko.rowIds.Neko163;
            case 164: return (int)MNP_Neko.rowIds.Neko164;
            case 165: return (int)MNP_Neko.rowIds.Neko165;
            case 166: return (int)MNP_Neko.rowIds.Neko166;
            case 167: return (int)MNP_Neko.rowIds.Neko167;
            case 168: return (int)MNP_Neko.rowIds.Neko168;
            case 169: return (int)MNP_Neko.rowIds.Neko169;
            case 170: return (int)MNP_Neko.rowIds.Neko170;
            case 171: return (int)MNP_Neko.rowIds.Neko171;
            case 172: return (int)MNP_Neko.rowIds.Neko172;
            case 173: return (int)MNP_Neko.rowIds.Neko173;
            case 174: return (int)MNP_Neko.rowIds.Neko174;
            case 175: return (int)MNP_Neko.rowIds.Neko175;
            case 176: return (int)MNP_Neko.rowIds.Neko176;
            case 177: return (int)MNP_Neko.rowIds.Neko177;
            case 178: return (int)MNP_Neko.rowIds.Neko178;
            case 179: return (int)MNP_Neko.rowIds.Neko179;
            case 180: return (int)MNP_Neko.rowIds.Neko180;
            case 181: return (int)MNP_Neko.rowIds.Neko181;
            case 182: return (int)MNP_Neko.rowIds.Neko182;
            case 183: return (int)MNP_Neko.rowIds.Neko183;
            case 184: return (int)MNP_Neko.rowIds.Neko184;
            case 185: return (int)MNP_Neko.rowIds.Neko185;
            case 186: return (int)MNP_Neko.rowIds.Neko186;
            case 187: return (int)MNP_Neko.rowIds.Neko187;
            case 188: return (int)MNP_Neko.rowIds.Neko188;
            case 189: return (int)MNP_Neko.rowIds.Neko189;
            case 190: return (int)MNP_Neko.rowIds.Neko190;
            case 191: return (int)MNP_Neko.rowIds.Neko191;
            case 192: return (int)MNP_Neko.rowIds.Neko192;
            case 193: return (int)MNP_Neko.rowIds.Neko193;
            case 194: return (int)MNP_Neko.rowIds.Neko194;
            case 195: return (int)MNP_Neko.rowIds.Neko195;
            case 196: return (int)MNP_Neko.rowIds.Neko196;
            case 197: return (int)MNP_Neko.rowIds.Neko197;
            case 198: return (int)MNP_Neko.rowIds.Neko198;
            case 199: return (int)MNP_Neko.rowIds.Neko199;
            case 200: return (int)MNP_Neko.rowIds.Neko200;
            case 201: return (int)MNP_Neko.rowIds.Neko201;
            case 202: return (int)MNP_Neko.rowIds.Neko202;
            case 203: return (int)MNP_Neko.rowIds.Neko203;
            case 204: return (int)MNP_Neko.rowIds.Neko204;
            case 205: return (int)MNP_Neko.rowIds.Neko205;
            case 206: return (int)MNP_Neko.rowIds.Neko206;
            case 207: return (int)MNP_Neko.rowIds.Neko207;
            case 208: return (int)MNP_Neko.rowIds.Neko208;
            case 209: return (int)MNP_Neko.rowIds.Neko209;
            case 210: return (int)MNP_Neko.rowIds.Neko210;
            case 211: return (int)MNP_Neko.rowIds.Neko211;
            case 212: return (int)MNP_Neko.rowIds.Neko212;
            case 213: return (int)MNP_Neko.rowIds.Neko213;
            case 214: return (int)MNP_Neko.rowIds.Neko214;
            case 215: return (int)MNP_Neko.rowIds.Neko215;
            case 216: return (int)MNP_Neko.rowIds.Neko216;
            case 217: return (int)MNP_Neko.rowIds.Neko217;
            case 218: return (int)MNP_Neko.rowIds.Neko218;
            case 219: return (int)MNP_Neko.rowIds.Neko219;
            case 220: return (int)MNP_Neko.rowIds.Neko220;
            case 221: return (int)MNP_Neko.rowIds.Neko221;
            case 222: return (int)MNP_Neko.rowIds.Neko222;


            default:
                return (int)MNP_Neko.rowIds.Neko0;
        }

    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="pID"></param>
    /// <returns></returns>
    public int GetLocalRowID(int pID) {
        switch (pID) {
            default: return (int)MNP_Localize.rowIds.L1000;
        }
    }

    #endregion



    #region Properties 


	public int Height {
		get {
			return this._height;
		}
	}

	public int Width {
		get {
			return this._width;
		}
	}

	public float BlockStartX {
		get {
			return this._blockStartX;
		}
	}

	public float BlockStartY {
		get {
			return this._blockStartY;
		}
	}

	public float BlockScaleValue {
		get {
			return this._blockScaleValue;
		}
	}

	public Vector3 BlockScale {
		get {
			return this._blockScale;
		}
	}

	public int FillBlockCount {
		get {
			return this._fillBlockCount;
		}

        set {
            this._fillBlockCount = value;
        }
	}

	public int BlockTypeCount {
		get {
			return this._blockTypeCount;
		}
	}

	public Vector3 BaseScale {
		get {
			return this._baseScale;
		}
	}

	public float ComboKeepTime {
		get {
			return this._comboKeepTime;
		}
	}
	public float IntervalMinusComboTime {
		get {
			return this._intervalMinusComboTime;
		}
	}
	public float MinumumComboKeepTime {
		get {
			return this._minumumComboKeepTime;
		}
	}
	public float FeverPlayTime {
		get {
			return this._feverPlayTime;
		}
	}

	public float InGamePlayTime {
		get {
			return this._ingamePlayTime;
		}
	}





	public int InGameMaxCombo {
		get {
			return this._inGameMaxCombo;
		} 
		set {
			this._inGameMaxCombo = value;
		}
	}

	public int InGameScore {
		get {
			return this._inGameScore;
		} 
		set {
			this._inGameScore = value;
		}
	}

	public int InGamePlusScore {
		get {
			return this._inGamePlusScore;
		} 
		set {
			this._inGamePlusScore = value;
		}
	}

	public int InGameDamage {
		get {
			return this._inGameDamage;
		} 
		set {
			this._inGameDamage = value;
		}
	}

	public int InGameChub {
		get {
			return this._inGameChub;
		} set {
			this._inGameChub = value;
		}
	}

	public int InGameTuna {
		get {
			return this._inGameTuna;
		} set {
			this._inGameTuna = value;
		}
	}

	public int InGameSalmon {
		get {
			return this._inGameSalmon;
		} set {
			this._inGameSalmon = value;
		}
	}


	public float FloatInGameDamage {
		get {
			return this._floatInGameDamage;
		} 
		set {
			this._floatInGameDamage = value;
		}
	}

	public int InGameDiamond {
		get {
			return this._inGameDiamond;
		} 
		set {
			this._inGameDiamond = value;
		}
	}

	public int InGameCoin {
		get {
			return this._inGameCoin;
		} 
		set {
			this._inGameCoin = value;
		}
	}


	public int InGameTotalCoin {
		get {
			return this._inGameTotalCoin;
		} 
		set {
			this._inGameTotalCoin = value;
		}
	}

	public int InGameStage {
		get {
			return this._inGameStage;
		} 
		set {
			this._inGameStage = value;
		}

	}

	public float BlockAttackPower {
		get {
			return this._blockAttackPower;
		}

        set {
            this._blockAttackPower = value;
        }
    }

	public Vector3 CoinDestPos {
		get {
			return this._coinDestPos;
		}
	}


	public int UserGold {
		get {
			return this._userGold;
		} 
		set {
			this._userGold = value;
		}
	}


	public bool IsWaitingForAds {
		get { 
			return this._isWaitingForAds;
		}
	}


	public GooglePlayerTemplate CurrentPlayer {
		get {
			return this._currentPlayer;
		}
		set {
			this._currentPlayer = value;
		}
	}
		 
	public AudioClip LobbyBGM {
		get {
			return this._lobbyBGM;
		}
	}

	public AudioClip GatchaBGM {
		get {
			return this._gatchaBGM;
		}
	}

	public bool OptionSoundPlay {
		get {
			return this._optionSoundPlay;
		}
	}

	public bool OptionBGMPlay {
		get {
			return this._optionBGMPlay;
		}
	}

	public List<int> ListEquipNekoID {
		get {
			return this._listEquipNekoID;
		} 
		set {
			this._listEquipNekoID = value;
		}
	}

	public string DeviceID {
		get {
			return this._deviceID;
		}
		set {
			this._deviceID = value;
		}
	}

	public string UserID {
		get {
			return this._userID;
		}
		set {
			this._userID = value;
		}
	}

	public string UserName {
		get {
			return this._userName;
		}
		set {
			this._userName = value;
		}
	}









	








	public JSONNode GatchaData {
		get {
			return this._gatchaData;
		}
	}



	public JSONNode MailData {
		get {
			return this._mailData;
		}
	}

	public JSONNode BillJSON {
		get {
			return this._billJSON;
		}
	}


	public JSONNode EventJSON {
		get {
			return this._eventJSON;
		}
	}




	public long SyncTime {
		get {
			return this._syncTime;
		}
	}

	public int UpgradeNekoDBKey {
		get {
			return this._nekodbkey;
		}
		set{ 
			this._nekodbkey = value;
		}
	}




	public List<int> ListEquipItemID {
		get {
			return this._listEquipItemID;
		}
	}

	public List<int> ListPreEquipItemID {
		get {
			return this._listPreEquipItemID;
		}
	}
	
	public int InGameTotalScore {
		get {
			return this._inGameTotalScore;
		}
		set {
			this._inGameTotalScore = value;
		}
	}

	public float InGamePauseTime {
		get {
			return this._inGamePauseTime;
		}
		set {
			this._inGamePauseTime = value;
		}
	}

	public long LastHeartTakeTime {
		get {
			return this._lastHeartTakeTime;
		}
		set {
			this._lastHeartTakeTime = value;
		}
	}

	public long NextHeartTakeTime {
		get {
			return this._nextHeartTakeTime;
		}
		set {
			this._nextHeartTakeTime = value;
		}
	}

	public int HeartCount {
		get {
			return this._heartCount;
		}
		set {
			this._heartCount = value;
		}
	}

	public long FixAddTick {
		get {
			return this._fixAddTick;
		}

	}

	public long TimeGapBetweenServerClient {
		get {
			return this._timeGapBetweenServerClient;
		}
	}

	public int UserGem {
		get {
			return this._userGem;
		}
		set {
			this._userGem = value;
		}
	}



	public JSONNode RankJSON  {
		get {
			return this._rankJSON;
		}
	}

	public bool HasNewMail {
		get {
			return this._hasNewMail;
		}
		set {
			this._hasNewMail = value;
		}
		
	}

	public bool IsBillInit {
		get {
			return this._isBillInit;
		}
	}

	public bool IsNekoRewardReady {
		get {
			return this._isNekoRewardReady;
		}
		set {
			this._isNekoRewardReady = value;
		}
	}

	public bool IsEnterLobbyCompleted {
		get {
			return this._isEnterLobbyCompleted;
		}
		set {
			this._isEnterLobbyCompleted = value;
		}
	}

	public bool OptionHeartPushUse {
		get {
			return this._optionHeartPush;
		}
		set {
			this._optionHeartPush = value;
		}
	}



	public float NekoPowerPlus {
		get {
			return this._nekoPowerPlus;
		}
		set {
			this._nekoPowerPlus = value;
		}
	}

	public float NekoCoinPercent {
		get {
			return this._nekoCoinPercent;
		}
		set {
			this._nekoCoinPercent = value;
		}
	}

	public float NekoScorePercent {
		get {
			return this._nekoScorePercent;
		}
		set {
			this._nekoScorePercent = value;
		}
	}



	public float NekoGameTimePlus {
		get {
			return this._nekoGameTimePlus;
		}
		set {
			this._nekoGameTimePlus = value;
		}
	}

	public string FacebookID {
		get {

            if (string.IsNullOrEmpty(_facebookid))
                return "";


			return this._facebookid;
		}
		set {
			this._facebookid = value;
		}
	}






	public int UserChub {
		get {
			return _userChub;
		} 
		set {
			this._userChub = value;

            if (_userChub < 0)
                _userChub = 0;
		}
	}

	public int UserTuna {
		get {
			return _userTuna;
		} 
		set {
			this._userTuna = value;
            if (_userTuna < 0)
                _userTuna = 0;
        }
	}

	public int UserSalmon {
		get {
			return _userSalmon;
		} 
		set {
			this._userSalmon = value;
            if (_userSalmon < 0)
                _userSalmon = 0;
        }
	}

	public int AdsPoint {
		get {
			return _adsPoint;
		} 
		set {
			this._adsPoint = value;
		}
	}

	public int FeedChub {
		get {
			return _feedChub;
		} set {
			this._feedChub = value;
		}
	}

	public int FeedTuna {
		get {
			return _feedTuna;
		} set {
			this._feedTuna = value;
		}
	}

	public int FeedSalmon {
		get {
			return _feedSalmon;
		} set {
			this._feedSalmon = value;
		}
	}





	public string Platform {
		get {
			return this._platform;
		}
		set {
			this._platform = value;
		}
	
	}

    public bool IosAuthed {
        get {
            return _iosAuthed;
        }

        set {
            _iosAuthed = value;
        }
    }

    public int LocalTutorialStep {
        get {
            return _localTutorialStep;
        }

        set {
            _localTutorialStep = value;
        }
    }

    public int TutorialStage {
        get {
            return tutorialStage;
        }

        set {
            tutorialStage = value;
        }
    }

    public int GatchaCount {
        get {
            return _gatchaCount;
        }

        set {
            _gatchaCount = value;
        }
    }

    public int Remainfreegacha {
        get {
            return _remainfreegacha;
        }

        set {
            _remainfreegacha = value;
        }
    }

    public int Remainheartcharge {
        get {
            return _remainheartcharge;
        }

        set {
            _remainheartcharge = value;
        }
    }

    public int Remainstartfever {
        get {
            return _remainstartfever;
        }

        set {
            _remainstartfever = value;
        }
    }

    public PlayerOwnNekoCtrl SelectNeko {
        get {
            return _selectNeko;
        }

        set {
            _selectNeko = value;
        }
    }

	public bool IsConnectedServer {
		get {
			return _isConnectedServer;
		}
		set {
			_isConnectedServer = value;
		}
	}






    public ObscuredInt InGameComboIndex {
        get {
            return _inGameComboIndex;
        }

        set {
            _inGameComboIndex = value;
        }
    }

    public int ExchangeGoldIndex {
        get {
            return _exchangeGoldIndex;
        }

        set {
            _exchangeGoldIndex = value;
        }
    }

    public int FishGatchaCount {
        get {
            return _fishGatchaCount;
        }

        set {
            _fishGatchaCount = value;
        }
    }




    public int PreviousSelectNekoID {
        get {
            return _previousSelectNekoID;
        }

        set {
            _previousSelectNekoID = value;
        }
    }

    public int UserBestScore {
        get {
            return _userBestScore;
        }

        set {

            if(_userPreBestScore == -1) {
                this._userPreBestScore = value;
                this._userBestScore = value;
            } else {
                this._userPreBestScore = this._userBestScore;
                this._userBestScore = value;
            }
        }
    }

    public int UserPreBestScore {
        get {
            return _userPreBestScore;
        }

        set {
            _userPreBestScore = value;
        }
    }

    public List<string> RequestGameData {
        get {
            return _requestGameData;
        }

        set {
            _requestGameData = value;
        }
    }



    public bool IsOnGameResult {
        get {
            return _isOnGameResult;
        }

        set {
            _isOnGameResult = value;
        }
    }



    public bool FirstWeekPlay {
        get {
            return _firstWeekPlay;
        }

        set {
            _firstWeekPlay = value;
            ES2.Save<bool>(_firstWeekPlay, "FirstWeekPlay");
        }
    }

    public int UpdateBestScoreCount {
        get {
            return _updateBestScoreCount;
        }

        set {
            _updateBestScoreCount = value;
            ES2.Save<int>(_updateBestScoreCount, "UpdatedBestScoreCount");
        }
    }

    public string DataCode {
        get {
            return _dataCode;
        }

        set {
            _dataCode = value;
        }
    }

    public long DataCodeExpiredTime {
        get {
            return _dataCodeExpiredTime;
        }

        set {
            _dataCodeExpiredTime = value;
        }
    }



    public ObscuredInt InGameTotalCombo {
        get {
            return _inGameTotalCombo;
        }

        set {
            _inGameTotalCombo = value;
        }
    }

    public int Remainnekogift {
        get {
            return _remainnekogift;
        }

        set {
            _remainnekogift = value;
        }
    }

    public int BoostShieldPrice {
        get {
            return _boostShieldPrice;
        }

        set {
            _boostShieldPrice = value;
        }
    }

    public int BoostBombPrice {
        get {
            return _boostBombPrice;
        }

        set {
            _boostBombPrice = value;
        }
    }

    public int BoostCriticalPrice {
        get {
            return _boostCriticalPrice;
        }

        set {
            _boostCriticalPrice = value;
        }
    }

    public int SpecialSingleGatchaPrice {
        get {
            return _specialSingleGatchaPrice;
        }

        set {
            _specialSingleGatchaPrice = value;
        }
    }

    public int SpecialMultiGatchaPrice {
        get {
            return _specialMultiGatchaPrice;
        }

        set {
            _specialMultiGatchaPrice = value;
        }
    }

    public int SpecialSingleFishingPrice {
        get {
            return _specialSingleFishingPrice;
        }

        set {
            _specialSingleFishingPrice = value;
        }
    }

    public int SpecialMultiFishingPrice {
        get {
            return _specialMultiFishingPrice;
        }

        set {
            _specialMultiFishingPrice = value;
        }
    }

    public List<int> ListCoinShopPrice {
        get {
            return _listCoinShopPrice;
        }

        set {
            _listCoinShopPrice = value;
        }
    }



    public int AdsID {
        get {
            return _adsID;
        }

        set {
            _adsID = value;
        }
    }

    public ObscuredInt InGameTicket {
        get {
            return _inGameTicket;
        }

        set {
            _inGameTicket = value;
        }
    }

    public bool OptionFreeCranePush {
        get {
            return _optionFreeCranePush;
        }

        set {
            _optionFreeCranePush = value;
        }
    }

    public bool OptionRemotePush {
        get {
            return _optionRemotePush;
        }

        set {
            _optionRemotePush = value;
        }
    }

    public bool IsLiveOpsInit {
        get {
            return _isLiveOpsInit;
        }

        set {
            _isLiveOpsInit = value;
        }
    }

    public bool EquipGroup1Neko {
        get {
            return _equipGroup1Neko;
        }

        set {
            _equipGroup1Neko = value;
        }
    }

    public bool EquipGroup2Neko {
        get {
            return _equipGroup2Neko;
        }

        set {
            _equipGroup2Neko = value;
        }
    }

    public bool EquipGroup3Neko {
        get {
            return _equipGroup3Neko;
        }

        set {
            _equipGroup3Neko = value;
        }
    }

    public bool EquipGroup4Neko {
        get {
            return _equipGroup4Neko;
        }

        set {
            _equipGroup4Neko = value;
        }
    }

    public int MatchedRedBlock {
        get {
            return _matchedRedBlock;
        }

        set {
            _matchedRedBlock = value;
        }
    }

    public int MatchedBlueBlock {
        get {
            return _matchedBlueBlock;
        }

        set {
            _matchedBlueBlock = value;
        }
    }

    public int MatchedYellowBlock {
        get {
            return _matchedYellowBlock;
        }

        set {
            _matchedYellowBlock = value;
        }
    }

    public int IngameSpecialAttackCount {
        get {
            return _ingameSpecialAttackCount;
        }

        set {
            _ingameSpecialAttackCount = value;
        }
    }

    public int IngameBombCount {
        get {
            return _ingameBombCount;
        }

        set {
            _ingameBombCount = value;
        }
    }

	public bool EquipLevel50Neko {
		get {
			return _equipLevel50Neko;
		}

		set {
			_equipLevel50Neko = value;
		}
	}

	public bool EquipRank5Neko {
		get {
			return _equipRank5Neko;
		}

		set {
			_equipRank5Neko = value;
		}
	}

	public bool EquipLevel20Neko {
		get {
			return _equipLevel20Neko;
		}

		set {
			_equipLevel20Neko = value;
		}
	}

    public bool EquipLevel10Neko {
        get {
            return _equipLevel10Neko;
        }

        set {
            _equipLevel10Neko = value;
        }
    }

    public int IngameBlueBombCount {
        get {
            return _ingameBlueBombCount;
        }

        set {
            _ingameBlueBombCount = value;
        }
    }

	public int IngameYellowBombCount {
		get {
			return _ingameYellowBombCount;
		}

		set {
			_ingameYellowBombCount = value;
		}
	}

	public int IngameRedBombCount {
		get {
			return _ingameRedBombCount;
		}

		set {
			_ingameRedBombCount = value;
		}
	}

	public int IngameBlackBombCount {
		get {
			return _ingameBlackBombCount;
		}

		set {
			_ingameBlackBombCount = value;
		}
	}

    public int IngameMatchThreeCount {
        get {
            return _ingameMatchThreeCount;
        }

        set {
            _ingameMatchThreeCount = value;
        }
    }

    public int IngameBlockCount {
        get {
            return _ingameBlockCount;
        }

        set {
            _ingameBlockCount = value;
        }
    }


    public bool EquipGroup5Neko {
        get {
            return _equipGroup5Neko;
        }

        set {
            _equipGroup5Neko = value;
        }
    }

    public bool EquipGroup6Neko {
        get {
            return _equipGroup6Neko;
        }

        set {
            _equipGroup6Neko = value;
        }
    }

    public bool EquipGroup7Neko {
        get {
            return _equipGroup7Neko;
        }

        set {
            _equipGroup7Neko = value;
        }
    }

    public bool EquipGroup8Neko {
        get {
            return _equipGroup8Neko;
        }

        set {
            _equipGroup8Neko = value;
        }
    }

    public bool IsAdminUser {
        get {
            return _isAdminUser;
        }

        set {
            _isAdminUser = value;
        }
    }

    public int BoostStartFeverPrice {
        get {
            return _boostStartFeverPrice;
        }

        set {
            _boostStartFeverPrice = value;
        }
    }

    public int CheckedBoostItemsPrice {
        get {
            return _checkedBoostItemsPrice;
        }

        set {
            _checkedBoostItemsPrice = value;
        }
    }

    public bool EquipGroup9Neko {
        get {
            return _equipGroup9Neko;
        }

        set {
            _equipGroup9Neko = value;
        }
    }

    public bool EquipGroup10Neko {
        get {
            return _equipGroup10Neko;
        }

        set {
            _equipGroup10Neko = value;
        }
    }

    public bool EquipGroup11Neko {
        get {
            return _equipGroup11Neko;
        }

        set {
            _equipGroup11Neko = value;
        }
    }

    public bool EquipGroup12Neko {
        get {
            return _equipGroup12Neko;
        }

        set {
            _equipGroup12Neko = value;
        }
    }

    public bool EquipGroup13Neko {
        get {
            return _equipGroup13Neko;
        }

        set {
            _equipGroup13Neko = value;
        }
    }

    public bool EquipGroup14Neko {
        get {
            return _equipGroup14Neko;
        }

        set {
            _equipGroup14Neko = value;
        }
    }

    public bool EquipGroup15Neko {
        get {
            return _equipGroup15Neko;
        }

        set {
            _equipGroup15Neko = value;
        }
    }

    public bool EquipGroup16Neko {
        get {
            return _equipGroup16Neko;
        }

        set {
            _equipGroup16Neko = value;
        }
    }

    public bool EquipGroup17Neko {
        get {
            return _equipGroup17Neko;
        }

        set {
            _equipGroup17Neko = value;
        }
    }

    public bool EquipGroup18Neko {
        get {
            return _equipGroup18Neko;
        }

        set {
            _equipGroup18Neko = value;
        }
    }

    public bool EquipGroup19Neko {
        get {
            return _equipGroup19Neko;
        }

        set {
            _equipGroup19Neko = value;
        }
    }

    public bool EquipGroup20Neko {
        get {
            return _equipGroup20Neko;
        }

        set {
            _equipGroup20Neko = value;
        }
    }

    public bool EquipGroup21Neko {
        get {
            return _equipGroup21Neko;
        }

        set {
            _equipGroup21Neko = value;
        }
    }

    public bool EquipGroup22Neko {
        get {
            return _equipGroup22Neko;
        }

        set {
            _equipGroup22Neko = value;
        }
    }

    public bool EquipGroup23Neko {
        get {
            return _equipGroup23Neko;
        }

        set {
            _equipGroup23Neko = value;
        }
    }

    public bool EquipGroup24Neko {
        get {
            return _equipGroup24Neko;
        }

        set {
            _equipGroup24Neko = value;
        }
    }

    public bool EquipGroup25Neko {
        get {
            return _equipGroup25Neko;
        }

        set {
            _equipGroup25Neko = value;
        }
    }

    public bool IngameContinue10secUse {
        get {
            return _ingameContinue10secUse;
        }

        set {
            _ingameContinue10secUse = value;
        }
    }

    public int IngameMissCount {
        get {
            return _ingameMissCount;
        }

        set {
            _ingameMissCount = value;
        }
    }

    public bool OptionPuzzleTip {
        get {
            return _optionPuzzleTip;
        }

        set {
            _optionPuzzleTip = value;
        }
    }

    public bool IsHotTime {
        get {
            return _isHotTime;
        }

        set {
            _isHotTime = value;
        }
    }

    public int LastActiveHour {
        get {
            return _lastActiveHour;
        }

        set {
            _preLastActiveHour = _lastActiveHour;
            _lastActiveHour = value;
        }
    }

    public int LastActiveDay {
        get {
            return _lastActiveDay;
        }

        set {

            _preLastActiveDay = _lastActiveDay;
            _lastActiveDay = value;
        }
    }

    public float UserNekoBadgeBonus {
        get {
            return _userNekoBadgeBonus;
        }

        set {
            _userNekoBadgeBonus = value;
        }
    }

    public bool EquipGroup26Neko {
        get {
            return _equipGroup26Neko;
        }

        set {
            _equipGroup26Neko = value;
        }
    }

    public int IngameMatchFourCount {
        get {
            return _ingameMatchFourCount;
        }

        set {
            _ingameMatchFourCount = value;
        }
    }

    public ObscuredInt InGameUserLevelBonusScore {
        get {
            return _inGameUserLevelBonusScore;
        }

        set {
            _inGameUserLevelBonusScore = value;
        }
    }

    public ObscuredInt InGameBadgeBonusScore {
        get {
            return _inGameBadgeBonusScore;
        }

        set {
            _inGameBadgeBonusScore = value;
        }
    }

    public bool RecordUse {
        get {
            return _recordUse;
        }

        set {
            _recordUse = value;
        }
    }

    public bool EquipGroup27Neko {
        get {
            return _equipGroup27Neko;
        }

        set {
            _equipGroup27Neko = value;
        }
    }

    public bool EquipGroup28Neko {
        get {
            return _equipGroup28Neko;
        }

        set {
            _equipGroup28Neko = value;
        }
    }

    public bool EquipGroup29Neko {
        get {
            return _equipGroup29Neko;
        }

        set {
            _equipGroup29Neko = value;
        }
    }

    public bool EquipGroup30Neko {
        get {
            return _equipGroup30Neko;
        }

        set {
            _equipGroup30Neko = value;
        }
    }

    public bool EquipGroup31Neko {
        get {
            return _equipGroup31Neko;
        }

        set {
            _equipGroup31Neko = value;
        }
    }

    public bool EquipLevel30Neko {
        get {
            return _equipLevel30Neko;
        }

        set {
            _equipLevel30Neko = value;
        }
    }

    public MNP_Neko NekoInfo {
        get {
            return _NekoInfo;
        }

        set {
            _NekoInfo = value;
        }
    }

    public int PlayStage {
        get {
            return _playStage;
        }

        set {
            _playStage = value;
        }
    }

    public ObscuredInt InGameRescueNeko {
        get {
            return _inGameRescueNeko;
        }

        set {
            _inGameRescueNeko = value;
        }
    }

    public ObscuredInt InGameDestroyUFO {
        get {
            return _inGameDestroyUFO;
        }

        set {
            _inGameDestroyUFO = value;
        }
    }

    public bool InGameStageClear {
        get {
            return _inGameStageClear;
        }

        set {
            _inGameStageClear = value;
        }
    }



    public int InGameStageMissionCount {
        get {
            return _inGameStageMissionCount;
        }

        set {
            _inGameStageMissionCount = value;
        }
    }

    public bool InGameBoostItemUSE {
        get {
            return _inGameBoostItemUSE;
        }

        set {
            _inGameBoostItemUSE = value;
        }
    }

    public int UserPowerLevel {
        get {
            return _userPowerLevel;
        }

        set {
            _userPowerLevel = value;
            BlockAttackPower = _userPowerLevel * 5;
        }
    }

    public int ActiveBlockCount {
        get {
            return _activeBlockCount;
        }

        set {
            _activeBlockCount = value;
        }
    }

    public int IngameRemainCookie {
        get {
            return _ingameRemainCookie;
        }

        set {
            _ingameRemainCookie = value;
        }
    }

    public int NekoBombAppearBonus {
        get {
            return _nekoBombAppearBonus;
        }

        set {
            _nekoBombAppearBonus = value;
        }
    }

    public int NekoSkillInvokeBonus {
        get {
            return _nekoSkillInvokeBonus;
        }

        set {
            _nekoSkillInvokeBonus = value;
        }
    }

    public int NekoStartBombCount {
        get {
            return _nekoStartBombCount;
        }

        set {
            _nekoStartBombCount = value;
        }
    }

    public int UserCurrentStage {
        get {
            return _userCurrentStage;
        }

        set {
            _userCurrentStage = value;
        }
    }

    public bool IngameDiamondPlay {
        get {
            return _ingameDiamondPlay;
        }

        set {
            _ingameDiamondPlay = value;
        }
    }

    public bool InGameStageUp {
        get {
            return _inGameStageUp;
        }

        set {
            _inGameStageUp = value;
        }
    }

    public int IngameBoardClearCount {
        get {
            return _ingameBoardClearCount;
        }

        set {
            _ingameBoardClearCount = value;
        }
    }

    public int IngameBoardPerfectClearCount {
        get {
            return _ingameBoardPerfectClearCount;
        }

        set {
            _ingameBoardPerfectClearCount = value;
        }
    }

    public int IngameFishGrill {
        get {
            return _ingameFishGrill;
        }

        set {
            _ingameFishGrill = value;
        }
    }

    public bool IsRequestingWakeUp {
        get {
            return _isRequestingWakeUp;
        }

        set {
            _isRequestingWakeUp = value;
        }
    }


    #endregion


}
