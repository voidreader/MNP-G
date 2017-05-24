using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;


#region enums

public enum NekoAppearSize {
    Mini,
    Small,
    Normal // 보통사이즈 
}

public enum NekoMedal {
    none,
    bronze,
    silver,
    gold
}



public enum TipType {
    FisrtStage,
    BombTip,
    SpecialAttackTip,
    CookieTip,
    StoneTip,
    FishGrillTip,
    MoveMissionTip,
    AllPuzzleItem, // Items Only
    WakeUpTip,
    NekoService,
    HistoryTip,


    ItemNoMiss,
    ItemBomb150,
    ItemPowerUp,
    ItemStartFever,

    PassiveGameTime,
    PassiveFeverTime,
    PassivePower,
    Wanted,
    MissionResult,
    PuzzlePlay,
    
    AllPassive,
    AllPuzzleItemAbility, // Passive & Buff
    
    AllMission,
    NekoLevelup,
    Bingo
}

public enum UnlockType {
    PassiveUnlock,
    ItemUnlock,
    RankingUnlock,
    WantedUnlock,
    MissionUnlock
}

public enum SocialType {
    twiiter,
    FB
}

public enum MissionType {
    Day,
    Week
}

#region MissionState, FishType, 뽑기 분류

public enum MissionState {
    NotComplete, // 완료되지 않음 
    WaitingComplete, // 보상받기 전단계
    Complete // 완료
}

public enum FishType {
    Chub,
    Tuna,
    Salmon
}

public enum GatchaProductType {
    Free,
    Special,
    Fish
}



#endregion

public enum PopMessageType {
    CommingSoon,
	GatchaConfirm,
	HeartFull, // 하트가 가득차서 더이상 받을 수 없음!
	HeartZero,  // 하트가 없어서 게임을 진행할 수 없음 
	GoldShortage, // 코인이 부족합니다.
	GoldPurchase, // 골드 구매 
    AdsCool,
    BingoLock,
    ChangeLang,

    NeedGoldPurchase, // 골드 구매가 필요하다!
	ShortageGoldForItem, // Item 장착을 위한 골드가 부족합니다. 
	ShortageGemForGatcha, // 가챠를 위한 젬이 부족해 
	CouponSucceed,
	CouponFail,
	CouponUsed,
	CouponOutOf,
	CouponInput,
	HeartPurchase,
	HeartPurChaseButFull,
	AdShortage,
	NeedGradeUp, // 등급을 올려야..
	GoldAdd, // 골드 얻음 
	GemAdd, // 보석 얻음
    
	HeartAdd, // 하트 얻음
    MailHeartAdd,
	NeedLevelUp, // 레벨 업필요 
    MaxPassiveLevel, // 패시브 레벨 만땅.
	
	FacebookFriendNeed,
	AlreadySentHeart, // 하트 이미 보냄 
    FacebookLinkLost, // 페이스북 링크 잃음 

    PowerUpgrade,
    PowerUpgradeWithGem,

    Logout, // 로그아웃


	EquipNekoNeed,

    AlreadStartPackage,
	RateForGem,
	SalmonAdd, // 연어 획득 
	TunaAdd, // 참치 획득
	ChubAdd, // 고등어 획득

	BandSalmonAdd, // 연어 획득 (뽑기)
	BandTunaAdd, // 참치 획득
	BandChubAdd, // 고등어 획득
    NickNameChanged,
    ApplicationQuit,

	GameCenterConnectInfo,

	NekoGiftGoldAdd, //보은 코인 획득 
	NekoGiftGemAdd, //보은 보석 획득


    AdsNotEnable,

    NoFunc, // 없는 기능
    ConfirmFeed, // 생선주기 확인 
    NekoGiftWithAdsConfirm, //네코의 보은 확인창
    NoRemainAds,
    NoRemainFreeGatcha,
    
    NoFish,
    ConfirmSpecialGatchaTen, // 10회 유료 가챠 체크 
    ConfirmFishGatchaOne,  // 생선 1회 체크 
    ConfirmFishGatchaTen, // 생선 1회 체크 
    CantGrowNeko, // 네코를 성장시킬 수 없음
    NekoGonnaMaxGrade, // 네코의 다음등급이 맥스 
    ReLogin, // 리로그인 체크 
    NekoAdd, // 네코 추가
    NeedWatchGatchaMovie,  // 광고를 봐야합니다. 
    ReadyToFreeGatcha, // 무료 뽑기 가능 

    UploadComplete,
    UploadFail,

    InviteComplete, // 초대 완료

    AlreadyIssued, // 코드 이미 발급
    WrongCode, // 잘못된 코드 
    UsedCode, // 기 사용 코드 
    ExpiredCode, // 만료 코드 

    SameDeviceCode, // 동일 디바이스에서 데이터 이전 
    CompleteDataTranfer,

    //Ranking Reward 추가 메세지
    RankRewardCoinAdd,
    RankRewardChubAdd,
    RankRewardTunaAdd,
    RankRewardSalmonAdd,
    RankRewardNekoAdd,
    BingoNeed,
    BingoSelect,
    BingoRetry,
    BingoStart,

    ExistsCantReadMail, // 일괄메일 읽기에서 읽지 못한 메일이 있음.
    Lock, // 잠금상태의 버튼
    BoostItemLock,
    UseTicket,
    AddFreeTicket,
    AddRareTicket,
    
    GetFacebookLinkReward,
    GetUserEventReward,
	NotImplemented,
    LineInvited,
    ThemeLock,
    RescueRewarded,
    WakeUpGemAdd,
    WakeUpCoinAdd,

    PostCompleted,
    RuntimePermissionDenied,

    GemShortage // 보석 부족, 상점 가세요 
}



// 게임 상태 
public enum GameState {
	Ready,
	Play,
	End,
	Stop,
}



// 블럭 상태 
public enum BlockState {
	None, 
	Inactive,
	Idle, 
	Dead, // 매치되서 Dead. 
	ItemBlockDead, // 아이템 블록이 Dead 
	Item,
	DeadFromItem, // 아이템에 의해 블록이 Dead 
	NoneFromItem,
    Stone,
    StrongStone,
    FishGrill,
    FireworkCap,
    Firework
    
}


public enum NekoSkillType {
    score_passive
, coin_passive
, time_passive
, fevertime_passive
, power_passive
, yellowblock_score_passive
, blueblock_score_passive
, redblock_score_passive
, yellowblock_appear_passive
, blueblock_appear_passive
, redblock_appear_passive
, bomb_appear_passive
, nekoskill_appear_passive
, userexp_passive
, random_bomb_active
, combo_maintain_active
, fever_raise_active
, time_active
, yellow_bomb_active
, blue_bomb_active
, red_bomb_active
, black_bomb_active
}

#endregion

// 블럭 좌표 값 
public struct BlockPos {
	public int x;
	public int y;
}



public struct NekoDamageInfo {
	public int nekoID;
    public int nekoStar;
	public int damage;
}

public struct SKUInfo {
	public string sku;
	public string price;
}




// 네코 데이터 
public struct NekoData {
    public int skillid1;
    public float skillvalue1;
    public string skillname1;

    public int skillid2;
    public float skillvalue2;
    public string skillname2;

    public int skillid3;
    public float skillvalue3;
    public string skillname3;

    public int skillid4;
    public float skillvalue4;
    public string skillname4;

    public int nameID;
    public int infoID;

    public List<string> listSkillInfo;

    public int skillCount;
}

public enum BombType {
    Black,
    Yellow,
    Blue,
    Red,
    Random
}



// Enemy Figure State 
public enum NekoState {
	Ready,
	Idle,
	Hit,
	BigHit,
	Attack,
	Dead
}


/// <summary>
/// 네코 타격 타입 
/// </summary>
public enum NekoHitType {
	MyNekoHit,
	Hit,
	BigHit
}


public enum AdsType {
	HeartAds,
	GatchaAds,
    NekoGiftAds
	
}




public static class PuzzleConstBox {


	public static bool isInitialize = false;



    // 객체 
    //public static InGameCtrl inGameCtrl = null;

    // 패키지
    public static readonly string packageHoney600 = "honey_600";
    public static readonly string packageSummer600 = "summer_600";
    public static readonly string packageMoon600 = "moon_600";
    public static readonly string packageMusic600 = "music_600";
    public static readonly string packageBoots600 = "boots_600";
    public static readonly string packageMaple600 = "maple_600";
    public static readonly string packageSecret600 = "secret_600";

    public static readonly string packageSanta600 = "santa_600";
    public static readonly string packageFish600 = "fish_600";
    public static readonly string packageSpecial = "mn_pkg_02";


    public static List<int> listNekoSpawnOrder = new List<int> (); // 네코 Spawn Order 

    /* 가격정보 */
    public static readonly int originalBoostItemPrice = 1000;
    public static readonly int originalBoostStartFever = 3000; // 스타트 피버
    public static readonly int originalSingleGatchaPrice = 300;
    public static readonly int originalMultiGatchaPrice = 2700; // 2700원으로 변경.
    public static readonly int originalSingleFishingPrice = 1000;
    public static readonly int originalMultiFishingPrice = 10000;

    public static readonly int origianlFreeCraneCount = 3;

    public static List<int> listCoinShopOriginalPrices = new List<int>();


    // 스테이지
    public static List<Vector3> listCurrentStagePos = new List<Vector3>();

    public static List<Vector3> listTheme8RocketStartPos = new List<Vector3>(); // 테마 8, 로켓 발사위치 
    

    public static string spriteClearStage = "stage-clear-wing";

    public static string spriteBronzeClear = "stage-clear-wing-bronz";
    public static string spriteSilverClear = "stage-clear-wing-silver";
    public static string spriteGoldClear = "stage-clear-wing";

    public static string spriteDiaClear = "stage-clear-wing-dia";
    public static string spriteDiaClearPrefix = "stage-clear-wing-dia-fx";

    public static List<string> listTheme7Candy = new List<string>();

    public static Color colorBronzeClear = new Color(0.66f, 0.37f, 0.23f);
    public static Color colorSilverClear = new Color(0.49f, 0.49f, 0.49f);
    public static Color colorGoldClear = new Color(0.74f, 0.52f, 0.05f);
    public static Color colorDiaClear = new Color(0.19f, 0.447f, 0.576f);


    /* Easy Save 관련 변수 */

    public static string ES_NoticeCheckDay = "checkday"; // 공지사항 오늘은 보지않음 체크 
    public static string ES_NekoGradeOrder = "NekoGradeOrder"; // 네코 정렬 여부 
    public static string ES_MissionDay = "mission_day";  // 미션일자 
    public static string ES_WeeklyMissionRefreshDay = "weeklymission_refreshday"; // 주간미션 갱신일자 
    
    public static string ES_GameStartCount = "GameStartCount"; // 퍼즐게임을 시작한 횟수
    

    public static string ES_UnlockMission = "UnlockMission"; // 미션 unlock 여부 
    public static string ES_UnlockItem = "UnlockItem"; // 아이템 unlock 여부 
    public static string ES_UnlockPassive = "UnlockPassive"; // 패시브 언락
    public static string ES_UnlockWanted = "UnlockWanted"; // 도감 언락
    public static string ES_UnlockRanking = "UnlockRanking"; // 랭킹 언락 (랭킹은 잠금 표시를 하지 않는다.)
    public static string ES_UnlockNekoService = "UnlockNekoService";
    public static string ES_UnlockNekoLevelup = "UnlockNekoLevelUp";
    public static string ES_UnlockBingoTip = "UnlockBingoTip"; // 빙고 팁 

    public static string ES_StageCookieTip = "stage_cookie_tip";
    public static string ES_StageStoneTip = "stage_stone_tip";
    public static string ES_StageMoveTip = "stage_move_tip";
    public static string ES_StageGrillTip = "stage_grill_tip";

    public static string ES_Language = "game_language";

    public static string ES_EventTalkCatTouchCount = "EventTalkCatTouchCount2";


    public static string ES_NotRenewBestScore = "NotRenewBestScore";

    /* 난입 이벤트 변수 */
    // Yaki, Rabbit, Drum, Harp
    public static string ES_EventSunBurnCatch = "Event212Catch";
    public static string ES_EventSunBurnStep = "Event212Step";
    public static string ES_EventWaterMelonCatch = "Event210Catch";
    public static string ES_EventWaterMelonStep = "Event210Step";

    
    // PF
    public static string PF_HeartPushUse = "OptionHeartPushUse";
    public static string PF_FreeCranePushUse = "OptionFreeCranePushUse";
    public static string PF_RemotePushUse = "OptionRemotePushUse";

    public static string PF_LoginDayOfYear = "PF_LoginDayOfYear"; // 로그인한 일자
    
    public static string PF_PuzzleTipOption = "PF_PuzzleTipOption";


    public readonly static string packet_hottime = "hottime";

    public readonly static string spriteNormalStarSprite = "lvup_ico_star";
    public readonly static string spriteOrangeStarSprite = "red-star";


    public readonly static string spriteFrameNekoYellow = "lvup_frm_neko_yellow";
    public readonly static string spriteFrameNekoGreen = "lvup_frm_neko_green";


    public readonly static string spriteBigBronzeBadge = "badge-bronze";
    public readonly static string spriteBigSilverBadge = "badge-silver";
    public readonly static string spriteBigGoldBadge = "badge-gold";
    public readonly static string spriteSmallBronzeBadge = "badge-bronze-bronze";
    public readonly static string spriteSmallSilverBadge = "badge-silver-mini";
    public readonly static string spriteSmallGoldBadge = "badge-gold-mini";

    public static string spriteLockIcon = "ico-top-key";
    public static string spriteLockBotIcon = "main_btn_bot_key";
    public static string spriteLockBig = "lock-big";
    public static string spriteLockCoin = "lock-coin";
    public static string spriteMissionBtn = "main_btn_mission";
    public static string spriteWantedBtn = "main_btn_wanted";
    public static string spriteBingoBtn = "main_btn_bot_bingo";

    public readonly static string spriteStageMissionColor = "mission-ico";
    public readonly static string spriteStageMissionNoColor = "mission-ico-fail";

    public static Vector3 initVector = new Vector3(1,1,1);
	public static Vector3 minBlockScale = new Vector3 (0.2f, 0.2f, 1);


    // Tags 
    public static readonly string tagFoot = "Foot";
    public static readonly string tagBigFoot = "BigFoot";
    public static readonly string tagFireworkBolt = "FireworkBolt";
    public static readonly string tagEnemyNeko = "EnemyNeko";
    public static readonly string tagCloneEnemyNeko = "CloneEnemyNeko";


    // Neko Skill
    public static List<string> listNekoSkillType = new List<string>(); // 네코 스킬 종류
    public static List<string> listNekoSkillSprite = new List<string>(); // 네코 스킬 스프라이트 
    


    public static List<string> listBlockSprite = new List<string> (); // 블록 Sprite List
    public static List<string> listShockBlockSprite = new List<string>(); // 블록 Sprite List
    public static List<string> listMissBlockSprite = new List<string>(); // 블록 Sprite List
    
    public static List<string> listDestroyBlockClip = new List<string>(); // 블록 Sprite List
    public static List<string> listDestroyBlockClip2 = new List<string>(); // 블록 Sprite List
    public static List<string> listDestroyBlockClip3 = new List<string>(); // 블록 Sprite List

    
	public static List<string> listItemBlockSprite = new List<string> (); // 아이템 블록 Sprite List

    public static List<string> listNekoBarFullSprite = new List<string>(); // 네코 게이지 Full Sprite

	public static List<string> listEnemyNekoAttackDrop = new List<string> ();
    public static string timeoverBlockSprite = "bl-game-over";

	public static List<string> listFruitClip = new List<string>();  // 
	
	public static List<string> listFragmentNekoHitClip = new List<string> ();
	public static List<string> listMovingStarClip = new List<string> (); // Moving Star
	public static List<string> listEquipItemClip = new List<string>(); // 장착 아이템 Clip
    public static List<string> listColoredEquipItemClip = new List<string>(); // 장착 아이템 Clip

    public static List<string> listPlayerAbilityClip = new List<string>(); // 플레이어 어빌리티  Clip
	public static List<string> listGatchaResultLightClip = new List<string> (); // 가챠 결과창 Light Clip
	public static List<string> listGoldPurchaseConfirmSprite = new List<string>(); // 골드 구매 확인창 Sprite 용도 
	public static List<string> listEquipBoostItemColorSprite = new List<string>(); // 장착 아이템 sprite (인게임)
	public static List<string> listEquipBoostItemDarkSprite = new List<string>(); // 장착 아이템 sprite (인게임)
	
	

	public static readonly string clipBlockDestroy = "BlockDestroy";
	public static readonly string clipItemBlockDestroy = "ItemBlockDestroy";

	public static readonly string clipCoinFX = "ClipCoinFx";
	public static readonly string clipCoinRotate = "ClipCoinRotate";


	//public static List<string> listBGCloudClip = new List<string>();
	public static List<string> listItemHeadClip = new List<string>();

	public static readonly string clipDustWhite = "DustWhite";
	public static readonly string clipDustBlack = "DustBlack";
	
	public static readonly string clipBreakIceBlock = "BreakIceBlock";
	public static readonly string clipMyNekoHitBG = "MyNekoHit";
	public static readonly string clipMyNekoTripleHitBG = "MyNekoTripleHit";
	public static readonly string clipMyNekoHitSunEffect = "MyNekoHitSunEffect";

    public static readonly string spriteCageBackFull = "cap-standard-base";
    public static readonly string spriteCageFrontFull = "cap-standard-top";
    public static readonly string spriteCageBackHalf = "cap-damage-base";
    public static readonly string spriteCageFrontHalf = "cap-damage-top";

    // 게임 내 블록 타일설정 
    public static readonly string spriteTile1 = "001-standard-tile-1";
    public static readonly string spriteTile2 = "001-standard-tile-2";
    public static readonly string spriteTouchTile = "002-touch-tile";

    public static readonly string spriteCookieTile = "003-ck-tile-standard";
    public static readonly string spriteTouchCookieTile = "003-ck-tile-touch";

    public static readonly string spriteX2 = "x2";
    public static readonly string spriteX3 = "x3";




    public static readonly string clipCookie = "ClipCookieTile";
    public static readonly string clipCookieBreak = "ClipCookieTileBreak";

    public static readonly string clipStoneBreak = "ClipStoneDestroy";
    public static readonly string clipStrongStone = "ClipStrongStone";
    public static readonly string clipStone = "ClipStone";
    public static readonly string spriteStrongStone = "004-stone-block-1";
    public static readonly string spriteStone = "004-stone-block-2";



    public static readonly string clipHorizontalLine = "ClipHorizontalLine";
    public static readonly string clipVerticalLine = "ClipVerticalLine";
    public static readonly string clipBigHorizontalLine = "ClipBigHorizontalLine";
    public static readonly string clipBigVerticalLine = "ClipBigVerticalLine";

    public static readonly string clipCurveLine = "ClipCurveLine";
    public static readonly string clipBigCurveLine = "ClipBigCurveLine";

    public static readonly string clipTLine = "ClipTLine";
    public static readonly string clipBigTLine = "ClipBigTLine";

    public static readonly string clipBlockFirework = "ClipBlockFirework";
    public static readonly string clipBlockFireworkCap = "ClipBlockFireworkCap";
    public static readonly string clipBlockFireworkBase = "ClipBlockFireworkBase";

    #region 게임 내 재화 
    public static readonly string spriteUIHeartMark = "main_ico_heart";
	public static readonly string spriteUIDiaMark = "top-dia";
    public static readonly string spriteUICoinMark = "top-coin";
    public static readonly string spriteUIGoldMark = "main_ico_coin";
	public static readonly string spriteUIGemMark = "i-zam2";
    public static readonly string spriteUIChubMark = "i-k";
    public static readonly string spriteUITunaMark = "i-t";
    public static readonly string spriteUISalmonMark = "i-ss";
    public static readonly string spriteUIFreeTicket = "freeticket";
    public static readonly string spriteUIRareTicket = "rareticket";
    public static readonly string spriteUIRainbowTicket = "rainbowticket";
    #endregion

    #region Box
    public static readonly string spriteBoxBlue = "frm_profile_box_blue";
    public static readonly string spriteBoxGreen = "frm_profile_box_green";
    public static readonly string spriteBoxYellow = "frm_profile_box_yellow";
    public static readonly string spriteBoxPink = "frm_profile_box_pink";
    #endregion



    // Pool
    public static readonly string myNekoPool = "MyNekos";
	public static readonly string objectPool = "Objects";
	public static readonly string lobbyParticlePool = "LobbyParticle";
    public static readonly string leafAppearPool = "LeafAppear";
    public static readonly string leafBasePool = "Leaf";
    public static readonly string nekoTicketExchangePool = "NekoTicket";
    public static readonly string inGameUIPool = "UI";
    public static readonly string worldMapPool = "WorldMapPool";
    public static readonly string lobbyCharacterPool = "CharacterPool";
    public static readonly string stagePool = "Stages";

    public static readonly string prefabMapEpisode = "MapEpisode";

    //public static readonly string prefabSmokePuffParticle = "CFXM3_Hit_SmokePuff";
    public static readonly string prefabBlockDestroyParticle = "BlockDestroyBluePrefab";
	public static readonly string prefabComboStarParticle = "ComboStarParticle";
	public static readonly string prefabCoinParticle = "CoinParticle";
	public static readonly string prefabMissText = "BlockMissText";
	public static readonly string prefabFootFrag = "FootPrefab";
	public static readonly string prefabDust = "DustPrefab";
	public static readonly string prefabMyNeko = "MyNekoPrefab";
    public static readonly string prefabBlockColorScore = "BlockColorScoreText";
	
	public static readonly string prefabMiniFoot = "MiniFootPrefab";
	public static readonly string prefabFragmentHit = "FragmentHitPrefab";
	
	public static readonly string prefabBonusGemPrefab = "BonusGemPrefab";
	public static readonly string prefabBlockDestroyEffect = "BlockDestroyEffectPrefab";
    
    
    public static readonly string prefabLeafAppear = "LeafAppear";
    public static readonly string prefabLeafBase = "LeafBase";

    public static readonly string prefabNekoTicketExchange = "TicketExchange";
    public static readonly string prefabUpperBlockEffect = "UpperBlockEffectPrefab";
    public static readonly string prefabFireworkBolt = "FireworkBolt";

    



    public static readonly string spriteChecked = "c-in";
    public static readonly string spriteUnchecked = "c-none";
    public static readonly string spriteRedTab = "common_btn_tap_red";
    public static readonly string spriteGrayTab = "common_btn_tap_gray";


    

    public static List<Vector3> listBombBoard = new List<Vector3>(); // 폭탄 이펙트 위치 지정
	

	// Color
	public static Color colorShadow = new Color(16, 120, 167);

	// Balance 
	public static float minFootSpeed = 0.5f;
	public static float maxFootSpeed = 0.7f;


    #region Collection 파트

    public static readonly string poolCollection = "CollectionPool";
    public static readonly string prefabCollection = "CollectionPrefab";

    public static readonly string spriteSmallCollectionSeat = "collection-st-base";
    public static readonly string spriteBigCollectionSeat = "collection-boss-base";

    public static readonly string spriteBaseNekoSeat = "collection-g-base";
    public static readonly string spriteBaseBossSeat = "collection-g-boss-base";
    public static readonly string spriteBaseNekoFigure = "collection-g-base-neko";
    public static readonly string spriteBaseBossFigure = "collection-g-boss-base-neko";

    #endregion


    //기본값
    public static Vector3 baseScale = new Vector3(1,1,1);
    public static Vector3 smallScale = new Vector3(0.6f, 0.6f, 1);
    public static readonly Vector3 upperFreeCranePos = new Vector3(288, 675, 0);
    public static readonly Vector3 lowerFreeCranePos = new Vector3(288, 570, 0);

    public static List<string> listFallingLeafPrefix = new List<string>();


    // Bingo Column
    public static readonly string spriteGreenBingoSelect = "bingo-block-tap";
    public static readonly string spriteGreenBingoEmpty = "bingo-block-st";
    public static readonly string spriteGreenBingoFill = "bingo-block-clear";

    public static readonly string spriteBlueBingoSelect = "bingo-bblock-tap";
    public static readonly string spriteBlueBingoEmpty = "bingo-bblock-st";
    public static readonly string spriteBlueBingoFill = "bingo-bblock-clear";

    public static readonly string spriteRedBingoSelect = "bingo-rblock-tap";
    public static readonly string spriteRedBingoEmpty = "bingo-rblock-st";
    public static readonly string spriteRedBingoFill = "bingo-rblock-clear";

    public static readonly string spriteOrangeBingoSelect = "bingo-oblock-tap";
    public static readonly string spriteOrangeBingoEmpty = "bingo-oblock-st";
    public static readonly string spriteOrangeBingoFill = "bingo-oblock-clear";

    public static readonly string spritePurpleBingoSelect = "bingo-vblock-tap";
    public static readonly string spritePurpleBingoEmpty = "bingo-vblock-st";
    public static readonly string spritePurpleBingoFill = "bingo-vblock-clear";


    public static readonly string spriteBlackBingoSelect = "bingo-blblock-tap";
    public static readonly string spriteBlackBingoEmpty = "bingo-blblock-st";
    public static readonly string spriteBlackBingoFill = "bingo-blblock-clear";


    public static readonly string spriteAfricotBingoSelect = "bingo-block-tap";
    public static readonly string spriteAfricotBingoEmpty = "bingo-block-st";
    public static readonly string spriteAfricotBingoFill = "bingo-block-clear";

    // Fish Grill
    public static readonly string orderFirstGrill = "OrderFirstGrill";
    public static readonly string orderSecondGrill = "OrderSecondGrill";
    public static readonly string orderLastGrill = "OrderLastGrill ";
    public static readonly string clipFirstFishGrill = "ClipFirstFishGrill";
    public static readonly string clipSecondFishGrill = "ClipSecondFishGrill";
    public static readonly string clipFishGrillTile = "ClipFishGrillTile";
    public static readonly string clipFishGrillIdleEffect = "ClipGrillIdleEffect";
    public static readonly string clipFishGrillSmokeEffect = "ClipGrillSmoke";


    public static readonly string spriteGrillTile = "fish-grl-base";
    public static readonly string spriteOriginGrill = "fish-a";
    public static readonly string spriteFirstGrill = "fish-b";
    public static readonly string spriteSecondGrill = "fish-c-clear";
    public static readonly string spriteLastGrill = "fish-c-clear-lol";

    // Movement
    public static readonly string prefabMoveTile = "MoveTile";
    public static readonly string prefabMoveingCat = "MovingCat";

    
    public static readonly string clipMoveStart = "ClipMoveStart";
    public static readonly string clipMoveEnd = "ClipMoveEnd";
    public static readonly string clipMoveTile = "ClipMoveTile";
    public static readonly string clipMoveTilePush = "ClipMoveTilePush";

    public static void Initialize() {

        //Debug.Log("▶ ConstBox Initialize");

        isInitialize = true;

        // 코인샵 가격 설정 
        listCoinShopOriginalPrices.Add(30000);
        listCoinShopOriginalPrices.Add(53000);
        listCoinShopOriginalPrices.Add(110000);
        listCoinShopOriginalPrices.Add(340000);
        listCoinShopOriginalPrices.Add(580000);

        #region 네코 스틸 타입, 스프라이트 설정
        listNekoSkillType.Add("score_passive");
        listNekoSkillType.Add("coin_passive");
        listNekoSkillType.Add("time_passive");
        listNekoSkillType.Add("fevertime_passive");
        listNekoSkillType.Add("power_passive");
        listNekoSkillType.Add("yellowblock_score_passive");
        listNekoSkillType.Add("blueblock_score_passive");
        listNekoSkillType.Add("redblock_score_passive");
        listNekoSkillType.Add("yellowblock_appear_passive");
        listNekoSkillType.Add("blueblock_appear_passive");
        listNekoSkillType.Add("redblock_appear_passive");
        listNekoSkillType.Add("bomb_appear_passive");
        listNekoSkillType.Add("nekoskill_appear_passive");
        listNekoSkillType.Add("userexp_passive");
        listNekoSkillType.Add("random_bomb_active");
        listNekoSkillType.Add("combo_maintain_active");
        listNekoSkillType.Add("fever_raise_active");
        listNekoSkillType.Add("time_active");
        listNekoSkillType.Add("yellow_bomb_active");
        listNekoSkillType.Add("blue_bomb_active");
        listNekoSkillType.Add("red_bomb_active");
        listNekoSkillType.Add("black_bomb_active");


        listNekoSkillSprite.Add("1-score-up");
        listNekoSkillSprite.Add("2-coin-up");
        listNekoSkillSprite.Add("3-playtime-up");
        listNekoSkillSprite.Add("4-fevertime-up");
        listNekoSkillSprite.Add("5-damage-up");
        listNekoSkillSprite.Add("7-yellow-block-up");
        listNekoSkillSprite.Add("8-blue-block-up");
        listNekoSkillSprite.Add("9-red-block-up");
        listNekoSkillSprite.Add("10-yellow-in");
        listNekoSkillSprite.Add("11-blue-in");
        listNekoSkillSprite.Add("12-red-in");
        listNekoSkillSprite.Add("13-bomb-up");
        listNekoSkillSprite.Add("14-neko-skill-up");
        listNekoSkillSprite.Add("15-exp-up");
        listNekoSkillSprite.Add("6-random-bomb-up");
        listNekoSkillSprite.Add("16-combo");
        listNekoSkillSprite.Add("17-fever-gauge-up");
        listNekoSkillSprite.Add("18-game-play-time-up");
        listNekoSkillSprite.Add("19-yello-bomb-in");
        listNekoSkillSprite.Add("20-blue-bomb-in");
        listNekoSkillSprite.Add("21-red-bomb-in");
        listNekoSkillSprite.Add("22-black-bomb-in");






        #endregion




        listBlockSprite.Add("bl-blue-2");
        listBlockSprite.Add ("bl-yellow-2");
        listBlockSprite.Add ("bl-red-2");

        listShockBlockSprite.Add("bl-blue-3");
        listShockBlockSprite.Add("bl-yellow-3");
        listShockBlockSprite.Add("bl-red-3");


        listMissBlockSprite.Add("bl-blue-4");
        listMissBlockSprite.Add("bl-yellow-4");
        listMissBlockSprite.Add("bl-red-4");

        #region Block Destroy 

        listDestroyBlockClip.Add("ClipBlockDestroyB");
        listDestroyBlockClip.Add("ClipBlockDestroyY");
        listDestroyBlockClip.Add("ClipBlockDestroyR");
        listDestroyBlockClip2.Add("ClipBlockDestroyB2");
        listDestroyBlockClip2.Add("ClipBlockDestroyY2");
        listDestroyBlockClip2.Add("ClipBlockDestroyR2");
        listDestroyBlockClip3.Add("ClipBlockDestroyB3");
        listDestroyBlockClip3.Add("ClipBlockDestroyY3");
        listDestroyBlockClip3.Add("ClipBlockDestroyR3");

        #endregion

		listItemBlockSprite.Add ("021-boom-black");
        listItemBlockSprite.Add("021-boom-blue");
        listItemBlockSprite.Add ("021-boom-yellow");
		listItemBlockSprite.Add ("021-boom-red");

        listNekoBarFullSprite.Add("010-cat-at-ani3");
        listNekoBarFullSprite.Add("010-cat-at-ani3-y");
        listNekoBarFullSprite.Add("010-cat-at-ani3-r");
        


        listItemHeadClip.Add ("021-boom-black-mini");
        listItemHeadClip.Add("021-boom-blue-mini");
        listItemHeadClip.Add ("021-boom-yellow-mini");
		listItemHeadClip.Add ("021-boom-red-mini");



        // 과일 Clip
		listFruitClip.Add("at-1");
        listFruitClip.Add("at-2");
        listFruitClip.Add("at-3");
        listFruitClip.Add("at-4");
        listFruitClip.Add("at-5");
        listFruitClip.Add("at-6");
        listFruitClip.Add("at-7");
        listFruitClip.Add("at-8");
        listFruitClip.Add("at-9");
        listFruitClip.Add("at-10");



		listBombBoard.Add (new Vector3 (2.1f, 1, 0));
		listBombBoard.Add (new Vector3 (0.12f, -1.6f, 0));
		listBombBoard.Add (new Vector3 (-2.2f, -4.37f, 0));
		listBombBoard.Add (new Vector3 (1.88f, -4.2f, 0));
		listBombBoard.Add (new Vector3 (-2.4f, 0.17f, 0));



		listFragmentNekoHitClip.Add ("008-star-fx1");
		listFragmentNekoHitClip.Add ("008-star-fx2");
		listFragmentNekoHitClip.Add ("008-star-fx3");

		listMovingStarClip.Add ("009-star-b-1");
		listMovingStarClip.Add ("009-star-b-2");
		listMovingStarClip.Add ("009-star-b-3");
		listMovingStarClip.Add ("010-star-s-1");
		listMovingStarClip.Add ("010-star-s-2");
		listMovingStarClip.Add ("010-star-s-3");


        /* 부스트 아이템 Icon */
        listEquipItemClip.Add("ready-5sec-g");
        listEquipItemClip.Add("ready-bomb-150-g");
        listEquipItemClip.Add("ready-neko-skill-g");
        listEquipItemClip.Add("CommingSoon");

        listColoredEquipItemClip.Add("ready-5sec");
        listColoredEquipItemClip.Add("ready-bomb-150");
        listColoredEquipItemClip.Add("ready-neko-skill");
        listColoredEquipItemClip.Add("ready-start-fever");


        listPlayerAbilityClip.Add ("ico_upgrade_time");
		listPlayerAbilityClip.Add ("ico_upgrade_fever");
		listPlayerAbilityClip.Add ("ico_upgrade_power");

		listGatchaResultLightClip.Add ("GatchaResultGreen");
		listGatchaResultLightClip.Add ("GatchaResultBlue");
		listGatchaResultLightClip.Add ("GatchaResultOrange");


		listEnemyNekoAttackDrop.Add ("018-neko-attack-ring-3");
		listEnemyNekoAttackDrop.Add ("018-neko-attack-ring-2");
		listEnemyNekoAttackDrop.Add ("018-neko-attack-ring-1");
	
		listGoldPurchaseConfirmSprite.Add ("shop_ico_coin_01");
		listGoldPurchaseConfirmSprite.Add ("shop_ico_coin_02");
		listGoldPurchaseConfirmSprite.Add ("shop_ico_coin_03");
		listGoldPurchaseConfirmSprite.Add ("shop_ico_coin_04");
		listGoldPurchaseConfirmSprite.Add ("shop_ico_coin_05");


		listEquipBoostItemColorSprite.Add ("024-ico_item_5sec");
		listEquipBoostItemColorSprite.Add ("024-ico_item_ico_item_bomb-in");
		listEquipBoostItemColorSprite.Add ("024-ico_item_neko-skill");
		listEquipBoostItemColorSprite.Add ("024-ico_item_start-in");

		listEquipBoostItemDarkSprite.Add ("024-ico_item_5sec-mono");
		listEquipBoostItemDarkSprite.Add ("024-ico_item_ico_item_bomb-mono");
		listEquipBoostItemDarkSprite.Add ("024-ico_item_neko-skill-mono");
		listEquipBoostItemDarkSprite.Add ("024-ico_item_start-mono");


        listFallingLeafPrefix.Add("d-red");
        listFallingLeafPrefix.Add("d-brown");
        listFallingLeafPrefix.Add("d-yellow");
        listFallingLeafPrefix.Add("d-pink");

        listCurrentStagePos.Add(new Vector3(-315, -50, 0));
        listCurrentStagePos.Add(new Vector3(-290, -180, 0));
        listCurrentStagePos.Add(new Vector3(-290, -320, 0));
        listCurrentStagePos.Add(new Vector3(-195, -435, 0));
        listCurrentStagePos.Add(new Vector3(-95, -320, 0));
        listCurrentStagePos.Add(new Vector3(-95, -165, 0));
        listCurrentStagePos.Add(new Vector3(0, -25, 0));
        listCurrentStagePos.Add(new Vector3(100, -165, 0));
        listCurrentStagePos.Add(new Vector3(100, -320, 0));
        listCurrentStagePos.Add(new Vector3(195, -435, 0));
        listCurrentStagePos.Add(new Vector3(291, -320, 0));
        listCurrentStagePos.Add(new Vector3(291, -184, 0));
        listCurrentStagePos.Add(new Vector3(315, -52, 0));


        

        listTheme7Candy.Add("dr-1");
        listTheme7Candy.Add("dr-2");
        listTheme7Candy.Add("dr-3");

        listTheme8RocketStartPos.Add(new Vector3(-320, 300, 0));
        listTheme8RocketStartPos.Add(new Vector3(320, 300, 0));
        listTheme8RocketStartPos.Add(new Vector3(-220, 440, 0));
        listTheme8RocketStartPos.Add(new Vector3(220, 440, 0));
        listTheme8RocketStartPos.Add(new Vector3(-115, 490, 0));
        listTheme8RocketStartPos.Add(new Vector3(115, 490, 0));
        

    }
}

