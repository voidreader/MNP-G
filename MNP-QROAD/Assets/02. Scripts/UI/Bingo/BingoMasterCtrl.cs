using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using DG.Tweening;
using Facebook.Unity;

public class BingoMasterCtrl : MonoBehaviour {


	#region instance 
	static BingoMasterCtrl _instance = null;

	public static BingoMasterCtrl Instance {
		get {
			if(_instance == null) {
				_instance = FindObjectOfType(typeof(BingoMasterCtrl)) as BingoMasterCtrl;

				if(_instance == null) {
					Debug.Log("BingoMasterCtrl Init Error");
					return null;
				}
			}

			return _instance;
		}
	}

    public BingoColumnCtrl SelectedCol {
        get {
            return _selectedCol;
        }

        set {
            _selectedCol = value;
        }
    }

    public string FillState {
        get {
            return _fillState;
        }
    }

    public string EmptyState {
        get {
            return _emptyState;
        }
    }

    public GameObject BingoMasterUI {
        get {
            return _bingoMasterUI;
        }

        set {
            _bingoMasterUI = value;
        }
    }

    public AudioClip AcBlink {
        get {
            return _acBlink;
        }

        set {
            _acBlink = value;
        }
    }

    public AudioClip AcColumnOpen {
        get {
            return _acColumnOpen;
        }

        set {
            _acColumnOpen = value;
        }
    }

    public AudioClip AcPossitiveUI {
        get {
            return _acPossitiveUI;
        }

        set {
            _acPossitiveUI = value;
        }
    }

    public AudioClip AcAllClearStart {
        get {
            return _acAllClearStart;
        }

        set {
            _acAllClearStart = value;
        }
    }

    public int JsonIndx {
        get {
            return _jsonIndx;
        }

        set {
            _preJsonIndx = _jsonIndx;
            _jsonIndx = value;
        }
    }

    #endregion

    [SerializeField]
    bool _isInit = false;

    // 빙고 마스터 정보 
    [SerializeField] int _currentBingoID;
    [SerializeField] int _bingoID;
    
    [SerializeField] string _bingoColor; // 빙고 메인 칼라 
    [SerializeField] string _bingoDifficulty; // 빙고 난이도 
    [SerializeField] string _bingoRewardNeko; // 빙고 보상 네코 
    [SerializeField] string _bingoColorNekoSprite; // 빙고 칼라네코 스프라이트
    [SerializeField] string _bingoShadowNekoSprite; // 빙고 섀도우네코 스프라이트
    [SerializeField] int _bingoDifficultyStar;

    [SerializeField] int _jsonIndx; // JSON 인덱스 
    int _preJsonIndx;
    [SerializeField] bool _isCurrentBingo = false;
    [SerializeField] bool _isBingoProgressed = false;
    

    [SerializeField]
    float _playIntervalTime = 0.2f;

	[SerializeField] JSONNode _bingoMasterNode;
    [SerializeField] GameObject _bingoMasterUI;
    [SerializeField] Transform _bingoBase; 
    [SerializeField] GameObject _bingoRows; // 빙고 컬럼 집합 
    [SerializeField] Transform _bingoNekoColor; // 컬러 네코 
    [SerializeField] GameObject _bingoNekoShadow; // 그림자 네코 
    [SerializeField] GameObject _bingoNekoAura;
    [SerializeField] GameObject _bingoRainbowTicket;
    [SerializeField] GameObject _bingoFinalGem;

    [SerializeField] GameObject _bingoNekoStarLabel;
    [SerializeField] GameObject _receiveInfo;

    [SerializeField] GameObject _btnQ;

    [SerializeField] GameObject _btnBingoSelect; // 빙고 선택 버튼 
    [SerializeField] GameObject _btnBingoRetry; // 빙고 리트라이 버튼 

    
    
    [SerializeField] UILabel _lblBingoInfo; // 빙고 안내 레이블 
    [SerializeField] UILabel _lblBottomDifficulty; // 하단 난이도 
    [SerializeField] GameObject[] _arrDifficultyStar = new GameObject[5];

    [SerializeField] GameObject _leftArrow; // 왼쪽 화살표
    [SerializeField] GameObject _rightArrow; // 오른쪽 화살표 

	

	[SerializeField] BingoColumnCtrl[] _arrBingoCols;
	[SerializeField] BingoColumnCtrl _selectedCol;

    [SerializeField] BingoMissionInfoCtrl _bingoMissionInfo;
	[SerializeField] BingoMissionResultCtrl _bingoMissionResult;


	[SerializeField] GameObject _btnClose;
	[SerializeField] List<JSONNode> _clearedMissions = new List<JSONNode>();
    [SerializeField] List<JSONNode> _clearedLines = new List<JSONNode>();


    // 라인 클리어시 표시되는 이미지 텍스트 
    [SerializeField] Transform _bingoImageText;
    [SerializeField] Transform _bingoComplete;
    
   
    [SerializeField] GameObject _screenLock;
    [SerializeField] GameObject _allClearMark;

    [SerializeField] UITweener _bgAlphaTweener;

    [SerializeField] GameObject _socialButtons;

    [SerializeField]
    int _currentClearLineCount = 0;

    [SerializeField]
    bool isClearLastLine;

    string _infoReward = "[b]こんぷりーとで\n[n]をげっと！[-]"; // 일반 빙고 
    string _infoReward2 = "[b]こんぷりーとで[n]げっと！[-]"; // 이벤트 빙고 레인보우 티켓 용도 

    string _infoDifficulty = "[b]なんいど：[n][-]";
    string _infoCurrentBingo = "こんぷりーとで[n]をげっと！";
    string _infoCurrentBingo2 = "こんぷりーとで[n]げっと！";

    readonly string END_CODE = "[-]";

    #region Colors


    Color _greenShadow = new Color(0, 0.38f, 0.14f, 0.6f);
    Color _blueShadow = new Color(0.004f, 0.48f, 0.66f, 0.6f);
    Color _redShadow = new Color(0.94f, 0.37f, 0.37f, 0.6f);

    Color _purpleShadow = new Color(0.23f, 0.12f, 0.294f, 0.6f);
    Color _orangeShadow = new Color(0.79f, 0.454f, 0.11f, 0.6f);
    Color _blackShadow = new Color(0.1f, 0.1f, 0.1f, 0.6f);
    Color _africotShadow = new Color(0, 0, 0, 0.6f);

    #endregion


    #region 이펙트 애니메이션 관련 변수 

    [SerializeField]
    GameObject _whiteScreen; 
    [SerializeField] SparkLightCtrl[] _arrFireworks;
    [SerializeField] SparkLightCtrl[] _arrSplashes;

    private Vector3[] _arrLineClearFireworkPos = new Vector3[] { new Vector3(220, 460, 0), new Vector3(-10,240, 0), new Vector3(-125, -220, 0), new Vector3(-210, -500, 0), new Vector3(205, -340, 0) };
    private Vector3[] _arrLineClearSplashPos = new Vector3[] { new Vector3(200, -340, 0), new Vector3(100, 210, 0), new Vector3(-85, -120, 0), new Vector3(-240, -470, 0), new Vector3(270, 25, 0), new Vector3(-240, 150, 0) };

    #endregion

    #region Audio Source, Clip
    [SerializeField] AudioClip _acBingoVoice;
    [SerializeField] AudioClip _acNewBingoBaseAppear;
    [SerializeField] AudioClip _acBingoBaseAppear;
    [SerializeField] AudioClip _acColumnOpen;
    [SerializeField] AudioClip _acBlink;
    [SerializeField] AudioClip _acBingoBaseDisappear;
    [SerializeField] AudioClip _acAllClear;
    [SerializeField] AudioClip _acStarOn;
    [SerializeField] AudioClip _acFirework;
    [SerializeField] AudioClip _acPossitiveUI;
    [SerializeField] AudioClip _acAllClearStart;

    [SerializeField] AudioSource _audio;
    #endregion

    #region Sprite  & Name 

    [SerializeField]
    UISprite _spMissionBoard;

    [SerializeField]
    UISprite _spMainBoard;

    [SerializeField]
    UISprite _spColorNeko;

    [SerializeField]
    UISprite _spShadowNeko;

    [SerializeField]
    UISprite _spMissionClear;


    readonly string BOTTOM_DIFFICULTY = "난이도";

    readonly string SPRITE_GREEN_BOARD = "bingo-card-base-no1";
    readonly string SPRITE_BLUE_BOARD = "bingo-card-base-no2";
    readonly string SPRITE_RED_BOARD = "bingo-card-base-no3";
    readonly string SPRITE_PURPLE_BORAD = "bingo-card-base-no5";
    readonly string SPRITE_ORAGNE_BOARD = "bingo-card-base-no4";
    readonly string SPRITE_BLACK_BOARD = "bingo-card-base-no6";
    readonly string SPRITE_AFRICOT_BOARD = "bingo-card-base-no1";


    readonly string SPRITE_GREEN_CLEAR = "clear-base";
    readonly string SPRITE_BLUE_CLEAR = "clear-bbase";
    readonly string SPRITE_RED_CLEAR = "clear-rbase";
    readonly string SPRITE_PURPLE_CLEAR = "clear-vbase";
    readonly string SPRITE_ORANGE_CLEAR = "clear-obase";
    readonly string SPRITE_BLACK_CLEAR = "clear-blbase";
    readonly string SPRITE_AFRICOT_CLEAR = "clear-base";

    readonly string SPRITE_GREEN_MISSION_INFO_BOARD = "mission-pop";
    readonly string SPRITE_BLUE_MISSION_INFO_BOARD = "mission-bpop";
    readonly string SPRITE_RED_MISSION_INFO_BOARD = "mission-rpop";
    readonly string SPRITE_PURPLE_MISSION_INFO_BOARD = "mission-vpop";
    readonly string SPRITE_ORANGE_MISSION_INFO_BOARD = "mission-opop";
    readonly string SPRITE_BLACK_MISSION_INFO_BOARD = "mission-blpop";
    readonly string SPRITE_AFRICOT_MISSION_INFO_BOARD = "mission-pop";




    #endregion


    #region Font
    public UIFont _boldGreenFont;
    public UIFont _greenFont;
    public UIFont _boldBlueFont;
    public UIFont _blueFont;
    public UIFont _boldRedFont;
    public UIFont _redFont;

    public UIFont _boldPurpleFont;
    public UIFont _purpleFont;

    public UIFont _boldOrangeFont;
    public UIFont _orangeFont;

    public UIFont _boldBlackFont;
    public UIFont _blackFont;

    public UIFont _boldAfricotFont;
    public UIFont _AfricotFont;

    #endregion

    // JSON 구조상 0~24 는 컬럼, 25~36은 Line 
    int _lineStartIndex = 25; // 라인은 25번째 행부터 시작. 
    int _calLineIndex;
    int _tempLineIndex;

    readonly string _fillState = "fill";
    readonly string _emptyState = "empty";
    bool _checkExistsFillColumn = false;

    JSONNode tempNode;

    void Start() {
        SetCurrentBingoID();
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.N)) {
            TestPlayAllClear();
        }

    }


    /// <summary>
    /// 빙고 화면 오픈 
    /// </summary>
    public void OpenBingo() {

        _bingoMasterUI.SetActive(true);

        SetBingoScreenLock(true);

        // 빙고 초기화 
        InitBingoMaster();


        // 진행중인 빙고 ID 정보 
        SetCurrentBingoID();


        // 빙고 외향 및 정보 설정 
        SetBingoBaseInfo();

        // 빙고 판 이동 처리 
        MoveBingoMasterUI();
    }

    /// <summary>
    /// 빙고 기본 정보 세팅 (색상, 텍스트 등)
    /// </summary>
    private void SetBingoBaseInfo() {

        string tempReward;
        tempReward = GetCurrentBingoReward();

        if (JsonIndx < 0 || JsonIndx >= _bingoMasterNode.Count) {
            Debug.Log("★★★_jsonIndx Error In SetBingoBaseInfo");
            return;
        }

        if( _currentBingoID == _bingoMasterNode[JsonIndx]["bingoid"].AsInt) {
            _isCurrentBingo = true;
        }
        else {
            _isCurrentBingo = false;
        }


        _bingoColor = _bingoMasterNode[JsonIndx]["color"];
        _bingoDifficulty = _bingoMasterNode[JsonIndx]["difficulty"];
        _bingoRewardNeko = _bingoMasterNode[JsonIndx]["reward"];
        _bingoDifficultyStar = _bingoMasterNode[JsonIndx]["difficultystar"].AsInt;

        _bingoColorNekoSprite = _bingoMasterNode[JsonIndx]["colorsprite"];
        _bingoShadowNekoSprite = _bingoMasterNode[JsonIndx]["shadowsprite"];
        _baseSnsText = _bingoMasterNode[JsonIndx]["snsmessage"];

        // 이벤트 도움 버튼 활성화
        if(_bingoMasterNode[JsonIndx]["event"].AsBool) {
            _btnQ.SetActive(true);
        }
        else {
            _btnQ.SetActive(false);
        }



        // 색상처리 
        switch (_bingoColor) {
            case "green":
                _spMainBoard.spriteName = SPRITE_GREEN_BOARD;
                _spMissionBoard.spriteName = SPRITE_GREEN_MISSION_INFO_BOARD;
                _spMissionClear.spriteName = SPRITE_GREEN_CLEAR;


                break;
            case "red":
                _spMainBoard.spriteName = SPRITE_RED_BOARD;
                _spMissionBoard.spriteName = SPRITE_RED_MISSION_INFO_BOARD;
                _spMissionClear.spriteName = SPRITE_RED_CLEAR;

                
                break;
            case "blue":
                _spMainBoard.spriteName = SPRITE_BLUE_BOARD;
                _spMissionBoard.spriteName = SPRITE_BLUE_MISSION_INFO_BOARD;
                _spMissionClear.spriteName = SPRITE_BLUE_CLEAR;

                break;

            case "purple":
                _spMainBoard.spriteName = SPRITE_PURPLE_BORAD;
                _spMissionBoard.spriteName = SPRITE_PURPLE_MISSION_INFO_BOARD;
                _spMissionClear.spriteName = SPRITE_PURPLE_CLEAR;

                

                break;

            case "orange":
                _spMainBoard.spriteName = SPRITE_ORAGNE_BOARD;
                _spMissionBoard.spriteName = SPRITE_ORANGE_MISSION_INFO_BOARD;
                _spMissionClear.spriteName = SPRITE_ORANGE_CLEAR;

                
                
                break;

            case "black":
                _spMainBoard.spriteName = SPRITE_BLACK_BOARD;
                _spMissionBoard.spriteName = SPRITE_BLACK_MISSION_INFO_BOARD;
                _spMissionClear.spriteName = SPRITE_BLACK_CLEAR;
                break;

            case "africot":
                _spMainBoard.spriteName = SPRITE_AFRICOT_BOARD;
                _spMissionBoard.spriteName = SPRITE_AFRICOT_MISSION_INFO_BOARD;
                _spMissionClear.spriteName = SPRITE_AFRICOT_CLEAR;
                break;
        }

        // Bottom, 상단 정보 세팅 
        SetBottomTextDifficulty();


        _spColorNeko.spriteName = _bingoColorNekoSprite;
        _spShadowNeko.spriteName = _bingoShadowNekoSprite;

        // 컬럼과 라인 배치 
        JSONNode bingodataNode = _bingoMasterNode[JsonIndx]["bingodata"];
        Debug.Log("★ bingodataNode :: " + bingodataNode.ToString());

        for (int i = 0; i < bingodataNode.Count; i++) {
            if (bingodataNode[i]["type"].Value.Equals("column"))
                _arrBingoCols[i].SetBingoCol(bingodataNode[i], _bingoColor);
        }

        // Line 처리
        for (int i = _lineStartIndex; i < bingodataNode.Count; i++) {

            if (bingodataNode[i]["type"].Value.Equals("column"))
                continue;

            if (bingodataNode[i]["state"].Value.Equals(FillState) && bingodataNode[i]["checked"].AsBool) {
                SetLineState(bingodataNode[i]["id"].AsInt - 1);
            }
        }


        // 진행중인 빙고 여부에 따른 추가 배치 
        Debug.Log("▶▶▶ SetBingoBaseInfo Check Current Value");

        for (int i = 0; i < bingodataNode.Count; i++) {
            if (bingodataNode[i]["current"].AsInt > 0 
                || bingodataNode[i]["state"].Value.Equals("fill")
                || bingodataNode[i]["state"] == "fill") {

                Debug.Log("★Progressed Bingo");

                // 하나라도 진행한 내용이 있으면 progress true 처리 
                _bingoMasterNode[JsonIndx]["progress"].AsBool = true;
                _isBingoProgressed = true;
                _bingoRows.SetActive(true);
                _lblBingoInfo.gameObject.SetActive(false);
                
                _lblBottomDifficulty.gameObject.SetActive(true);
                SetArrStar(_bingoDifficultyStar);
                break;
            }
        }

        // 모두 클리어한 경우에는 추가 처리 없음 
        Debug.Log("▶▶▶ SetBingoBaseInfo Check All Clear");

        if (!CheckExistsEmptyCol())
            return;

        // 현재 진행중인 빙고
        if (_currentBingoID == _bingoMasterNode[JsonIndx]["bingoid"].AsInt) {
            _bingoMasterNode[JsonIndx]["progress"].AsBool = true;
            _isBingoProgressed = true;
            _bingoRows.SetActive(true);

            DisableInformation();
            Debug.Log("▶▶▶ SetBingoBaseInfo Current!");
        }
        else {
            Debug.Log("▶▶▶ SetBingoBaseInfo Not Current!");
            //_lblBingoInfo.gameObject.SetActive(true);
            //_lblDiffcult.gameObject.SetActive(true);
        }

    }


    private void DisableInformation() {
        _btnBingoRetry.SetActive(false);
        _btnBingoSelect.SetActive(false);
        _lblBingoInfo.gameObject.SetActive(false);
        
        
    }

    /// <summary>
    /// Inits the bingo master.
    /// </summary>
    private void InitBingoMaster() {

        // 위치 초기화 및 활성,비활성화
        _bgAlphaTweener.ResetToBeginning();
        _bgAlphaTweener.PlayForward();
        

        //_bingoMasterNode = null;
		_bingoID = -1;
        _isBingoProgressed = false;
        _isCurrentBingo = false;

        _bingoMissionInfo.gameObject.SetActive(false);
		_btnClose.SetActive (false);
        _allClearMark.SetActive(false);

        _bingoImageText.localPosition = new Vector3(-700, 0, 0);
        _bingoComplete.localScale = GameSystem.Instance.BaseScale;
        _bingoComplete.gameObject.SetActive(false);

        _bingoBase.localPosition = Vector3.zero;

        // Color와 Shadow를 분리 
        _bingoNekoColor.localPosition = new Vector3(-5, -60, 0);
        _bingoNekoColor.gameObject.SetActive(false);
        _bingoNekoShadow.transform.localPosition = new Vector3(-5, -60, 0);
        _bingoNekoShadow.SetActive(false);

        _bingoNekoAura.SetActive(false); // 네코 아우라 
        _bingoNekoStarLabel.SetActive(false); // 네코 등급표시 
        _bingoRainbowTicket.SetActive(false); // 레인보우 티켓
        _bingoFinalGem.SetActive(false);

        _bingoRows.SetActive(false);
        

        _rightArrow.SetActive(false);
        _leftArrow.SetActive(false);
        _lblBingoInfo.gameObject.SetActive(false);
        
        _lblBottomDifficulty.gameObject.SetActive(false);
        SetArrStar(0);

        _receiveInfo.SetActive(false);
        _whiteScreen.SetActive(false);
        _socialButtons.SetActive(false);

        _checkExistsFillColumn = false;

        _btnBingoRetry.gameObject.SetActive(false);
        _btnBingoSelect.gameObject.SetActive(false);

        _btnQ.SetActive(false);

        // 사운드 처리 
        if (GameSystem.Instance.OptionSoundPlay)
            _audio.volume = 1;
        else
            _audio.volume = 0;

    }


    /// <summary>
    /// 하단 난이도 표시 추가 
    /// </summary>
    private void SetBottomTextDifficulty() {
        switch (_bingoColor) {
            case "green":
                
                _lblBottomDifficulty.text = "[004c1d]" + BOTTOM_DIFFICULTY + "[-]";
                _lblBottomDifficulty.effectColor = _greenShadow;

                break;
            case "red":
                _lblBottomDifficulty.text = "[600e03]" + BOTTOM_DIFFICULTY + "[-]";
                _lblBottomDifficulty.effectColor = _redShadow;
                break;
            case "blue":
                _lblBottomDifficulty.text = "[004766]" + BOTTOM_DIFFICULTY + "[-]";
                _lblBottomDifficulty.effectColor = _blueShadow;
                break;

            case "orange":
                _lblBottomDifficulty.text = "[913c04]" + BOTTOM_DIFFICULTY + "[-]";
                _lblBottomDifficulty.effectColor = _orangeShadow;
                break;


            case "purple":
                _lblBottomDifficulty.text = "[d2a0f0]" + BOTTOM_DIFFICULTY + "[-]";
                _lblBottomDifficulty.effectColor = _purpleShadow;
                break;

            case "black":

                _lblBottomDifficulty.text = "[d4d4d4]" + BOTTOM_DIFFICULTY + "[-]";
                _lblBottomDifficulty.effectColor = _blackShadow;
                break;


            case "africot":

                _lblBottomDifficulty.text = BOTTOM_DIFFICULTY;
                _lblBottomDifficulty.effectColor = _africotShadow;
                break;

        }

        _lblBottomDifficulty.gameObject.SetActive(true);
        
        SetArrStar(_bingoDifficultyStar);
    }

    /// <summary>
    /// 연출 후 Button 등장
    /// </summary>
	private void ShowButtons() {


		Debug.Log ("★ ShowButtons");

        SetBingoScreenLock(false);

        _btnClose.SetActive (true);


        if(JsonIndx + 1 < _bingoMasterNode.Count) {
            _rightArrow.SetActive(true);
        }

        if (JsonIndx > 0) {
            _leftArrow.SetActive(true);
        }

        // 진행중이지 않은 빙고에는 무조건 빙고 정보(난이도, 보상정보)를 표시 
        if(!_bingoMasterNode[JsonIndx]["progress"].AsBool) {
            SetBingoRewardAndDifficultyInfo();

            _btnBingoSelect.SetActive(true); // 빙고 선택 버튼 활성화 
        }
        else { // 진행이 된 빙고의 경우

            if (_isCurrentBingo) { // 현재 선택해서 도전중인 빙고 
                
            }
            else { // 이전에 했다가 중단한 빙고 

                SetBingoRewardAndDifficultyInfo();
                _btnBingoRetry.SetActive(true); // 재도전 버튼만 활성화
                _lblBingoInfo.gameObject.SetActive(false);
                

            }
        }

		if (_allClearMark.activeSelf) {
			Debug.Log ("★ ShowButtons, But Allclear");
			DisableInformation ();

		}
    }

    /// <summary>
    /// ShowButtons의 반대 
    /// </summary>
    private void HideButtons() {
        _btnClose.SetActive(false);
        _lblBingoInfo.gameObject.SetActive(false);
        

        _rightArrow.SetActive(false);
        _leftArrow.SetActive(false);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private string GetInfoReward() {
        if (_bingoRewardNeko.IndexOf("gem") >= 0 || _bingoRewardNeko.IndexOf("だいや") >= 0) { // "gem"이 올수도 있음. 
            return _infoReward;
        }
        else if (_bingoRewardNeko.IndexOf("超れあねこちけっと") >= 0) {
            return _infoReward2;
        }
        else {
            return _infoReward;
        }
    }

    /// <summary>
    /// 네코 제목 하단에 들어가는 보상 정보 문구 
    /// </summary>
    /// <returns></returns>
    private string GetCurrentBingoReward() {
        if (_bingoRewardNeko.IndexOf("gem") >= 0 || _bingoRewardNeko.IndexOf("だいや") >= 0) { // "gem"이 올수도 있음. 
            return _infoCurrentBingo;
        }
        else if (_bingoRewardNeko.IndexOf("超れあねこちけっと") >= 0) {
            return _infoCurrentBingo2;
        }
        else {
            return _infoCurrentBingo;
        }
    }


    /// <summary>
    /// BB code main RGB Color 
    /// </summary>
    private string GetBBCode() {
        if(_bingoColor == "green") {
            return "[004c1d]";
        }
        else if (_bingoColor == "red") {
            return "[600e03]";
        }
        else if (_bingoColor == "blue") {
            return "[004766]";
        }
        else if (_bingoColor == "black") {
            return "[d4d4d4]";
        }
        else if (_bingoColor == "africot") {
            return "[fff0b5]";
        }
        else if (_bingoColor == "orange") {
            return "[913c04]";
        }
        else if (_bingoColor == "purple") {
            return "[d2a0f0]";
        }
        else {
            return "[004c1d]";
        }

    }

    /// <summary>
    /// 레이블 그림자 색상 구하기 
    /// </summary>
    /// <returns></returns>
    private Color GetShadowEffectColor() {

        switch (_bingoColor) {
            case "green":
                return _greenShadow;
                
            case "red":
                return _redShadow;
                
            case "blue":
                return _blueShadow;
                

            case "orange":
                return _orangeShadow;
                


            case "purple":
                return _purpleShadow;
                

            case "black":
                return _blackShadow;

            case "africot":
                return _africotShadow;
                
            default:
                return _greenShadow;

        }
    }

    /// <summary>
    /// 빙고 보상 및 난이도 정보 설정 
    /// </summary>
    private void SetBingoRewardAndDifficultyInfo() {

        string tempReward, tempDifficulty;

        tempReward = GetInfoReward() ;
        tempDifficulty = _infoDifficulty;

        // 색상과 관련된 설정 시작 
        tempDifficulty = GetBBCode() + tempDifficulty + END_CODE;
        tempReward = GetBBCode() + tempReward + END_CODE;
        _lblBottomDifficulty.text = GetBBCode() + BOTTOM_DIFFICULTY + END_CODE;

        // 보상에 따른 문구 설정 
        if (_bingoRewardNeko.IndexOf("gem") >= 0 || _bingoRewardNeko.IndexOf("だいや") >= 0) { // "gem"이 올수도 있음. 
            _lblBingoInfo.text = tempReward.Replace("[n]", "[-] [f5bbb2]「" + "だいや" + "」[-]" + GetBBCode());
        }
        else if (_bingoRewardNeko.IndexOf("超れあねこちけっと") >= 0) {
            _lblBingoInfo.text = tempReward.Replace("[n]", "[-] [f5bbb2]「" + _bingoRewardNeko + "」[-]" + GetBBCode());
        }
        else {
            _lblBingoInfo.text = tempReward.Replace("[n]", "[-] [f5bbb2]「" + GameSystem.Instance.GetNekoName(int.Parse(_bingoRewardNeko), 3) + "」[-]" + GetBBCode());
        }

        
        // 문구 설정 끝 


        // Shadow Effect 설정 
        _lblBingoInfo.effectColor = GetShadowEffectColor();
        
        _lblBottomDifficulty.effectColor = GetShadowEffectColor();



        // Enable & Disable 
        _lblBingoInfo.gameObject.SetActive(true);
        

        _lblBottomDifficulty.gameObject.SetActive(true);
        

        SetArrStar(_bingoDifficultyStar);


    }


    /// <summary>
    /// 현재 빙고 ID 정보를 세팅 오픈때만 실행 
    /// </summary>
    public void SetCurrentBingoID() {
        _currentBingoID = GameSystem.Instance.UserJSON["currentbingoid"].AsInt;
        _bingoID = _currentBingoID;
        _bingoMasterNode = GameSystem.Instance.BingoJSON;



        // 진행중인 빙고가 있으면 진행중인 빙고 아이디로 세팅 
        if (_currentBingoID >= 0) {
            for (int i = 0; i < _bingoMasterNode.Count; i++) {
                if (_bingoMasterNode[i]["bingoid"].AsInt == _currentBingoID) {
                    JsonIndx = i;
                    _isCurrentBingo = true;
                }
            }
        }
        else {
            JsonIndx = 0;
            _bingoID = _bingoMasterNode[0]["bingoid"].AsInt;
            _isCurrentBingo = false;
        }
    }




    /// <summary>
    /// Opens the bingo. 단순 오픈
    /// </summary>
    /// <param name="pNode">P node.</param>
    public void OpenBingo(JSONNode pNode) {

        SetBingoScreenLock(true);

        InitBingoMaster();
		_bingoMasterNode = pNode;
        // pNode envdb.bingo data

        Debug.Log("▶▶OpenBingo :: " + _bingoMasterNode.ToString());
           

        _bingoID = pNode [0] ["bingoid"].AsInt;

		for (int i = 0; i < pNode.Count; i++) {
            if(pNode[i]["type"].Value.Equals("column")) 
                _arrBingoCols[i].SetBingoCol(pNode[i], null);
		}

        // Line 처리
        for(int i=_lineStartIndex; i<pNode.Count; i++) {

            if (pNode[i]["type"].Value.Equals("column"))
                continue;

            if (pNode[i]["state"].Value.Equals(FillState) && pNode[i]["checked"].AsBool) {
                SetLineState(pNode[i]["id"].AsInt - 1);
            }

        }

        // 빙고 판 이동 처리 
        MoveBingoMasterUI();
    }


    /// <summary>
    /// 빙고 등장 연출
    /// </summary>
    private void MoveBingoMasterUI() {

        _checkExistsFillColumn = CheckExistsUncheckedFill();
        BingoMasterUI.SetActive(true);

        _bingoBase.transform.DOKill();
        _bingoBase.transform.localPosition = new Vector3(0, -200, 0);
        _bingoBase.transform.DOLocalMoveY(0, 0.5f).OnComplete(OnCompleteBingoMasterUIMove);

        // 진행했던 빙고만 Color가 표시된다. 
        if(_bingoMasterNode[JsonIndx]["progress"].AsBool) {
            _bingoNekoColor.gameObject.SetActive(true);
            _bingoNekoColor.transform.DOKill();
            _bingoNekoColor.transform.localPosition = new Vector3(0, -250, 0);
            _bingoNekoColor.transform.DOLocalMoveY(-50, 0.5f);
        }
        else {
            _bingoNekoColor.transform.localPosition = new Vector3(0, -50, 0);
        }

        _bingoNekoShadow.gameObject.SetActive(true);
        _bingoNekoShadow.transform.DOKill();
        _bingoNekoShadow.transform.localPosition = new Vector3(0, -250, 0);
        _bingoNekoShadow.transform.DOLocalMoveY(-50, 0.5f);


        if (_checkExistsFillColumn)
            PlayAudio(_acNewBingoBaseAppear);
        else
            PlayAudio(_acBingoBaseAppear);


        //MoveBingoMasterUI → OnCompleteBingoMasterUIMove
    }

    private void OnCompleteBingoMasterUIMove() {
		Debug.Log ("★ OnCompleteBingoMasterUIMove");

        _isInit = true;

        // Clear 체크 
        if (_checkExistsFillColumn) {
			//Debug.Log ("★ OnCompleteBingoMasterUIMove #1");
            StartCoroutine(ClearingBingoColumns());
        }
        else {

			if(!CheckExistsEmptyCol()) {
				_allClearMark.SetActive(true);
             
            }

            Debug.Log("<<< No New Bingo");
            ShowButtons();


        }


		/*
        if(!_allClearMark && _isCurrentBingo) {
            _markCurrentBingo.gameObject.SetActive(true);
        }
        */

    }



    /// <summary>
    /// 빙고 클리어링. 컬럼 - 라인의 순서로 진행 
    /// 라인의 경우는 clear는 id를 따라가지만, 보상 정보는 따라가지 않는다. 
    /// </summary>
    /// <returns></returns>
	IEnumerator ClearingBingoColumns() {

        //Debug.Log("▶▶▶▶ ClearingBingoColumns");
        JSONNode bingodataNode = _bingoMasterNode[JsonIndx]["bingodata"];
        isClearLastLine = false;

        _clearedMissions.Clear();
        _clearedLines.Clear();

        SetClearLineCount(); // 달성 Line의 수를 확보 currentClearLineCount 

        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < bingodataNode.Count; i++) {

            if (bingodataNode[i]["type"].Value != "column") {
                
                continue;
            }

           // Debug.Log("▶▶ checked["+i+"] :: " + _bingoMasterNode[i]["checked"].AsBool);
            //Debug.Log("▶▶ state[" + i + "] :: " + _bingoMasterNode[i]["state"]);

            if (!bingodataNode[i]["checked"].AsBool && bingodataNode[i]["state"].Value.Equals(FillState)) {
                Debug.Log (">>> Clear Bingo Col i :: " + bingodataNode[i].ToString ());


                _bingoMissionInfo.ClearMission(bingodataNode[i], _bingoColor); // 미션용 Node 전달 

                _arrBingoCols[i].PlayClearMotion(); // 효과 처리 
                bingodataNode[i]["checked"].AsBool = true; // 체크 처리 

                _clearedMissions.Add(bingodataNode[i]); // List Added


                yield return new WaitForSeconds(0.8f); // 각 컬럼간 오픈 간격 
            }
        } // 컬럼 처리 종료 

        

        // 컬럼 처리가 완료된 시점에, 팁 화면 체크 
        if(GameSystem.Instance.CheckFirstBingoMissionUnlockProceed()) {
            Debug.Log("Bingo Mission Tip Unlock!");
            yield return StartCoroutine(LobbyCtrl.Instance.UnlockingFirstBingoMission());
        }




        // 라인 체크 시작 
        for (int i = _lineStartIndex; i < bingodataNode.Count; i++) {

            if (!bingodataNode[i]["type"].Value.Equals("line"))
                continue;

            if (bingodataNode[i]["state"].Value.Equals("fill")) // 이미 체크된 것은 패스 
                continue;

            if (bingodataNode[i]["state"].Value.Equals("empty")) {


                // 클리언 라인이 있으면 리스트에 Add.
                if (CheckLine(i)) {

                    Debug.Log(">> Checked Line OK i :: " + bingodataNode[i].ToString());
                    bingodataNode[i]["state"] = FillState;
                    bingodataNode[i]["checked"].AsBool = true; // 이펙트에 들어가게 되면 무조건 true 처리 
                    _clearedLines.Add(bingodataNode[i]); // 라인 효과를 위해서 추가 
                    //_clearedMissions.Add(_bingoMasterNode[i]); // 결과 처리를 위해서 추가 
                }

            }
        }



        // 세이브 처리 
        //GameSystem.Instance.BingoJSON = _bingoMasterNode;
        GameSystem.Instance.SaveBingoProgress();


        int startIndex = _lineStartIndex + _currentClearLineCount;

        //Debug.Log("Line Clear Go! #1 :: " + bingodataNode.Count);
        //Debug.Log("Line Clear Go! #1 :: " + startIndex);

        yield return new WaitForSeconds(0.5f);
        CloseMissionInfo(); // 미션 정보 제거 

        

        // 클리어된 라인의 클리어 효과 시작 
        for (int i = 0; i < _clearedLines.Count; i++) {

            StartCoroutine(PlayingLineClear(_clearedLines[i]["id"].AsInt - 1));
            yield return new WaitForSeconds(1.6f);


            // 빙고 효과 
            PlayLineBingoMotion();
            PlayAudio(_acBingoVoice); // 사운드 효과 
            yield return new WaitForSeconds(1.5f);

            // 빙고 텍스트, complete 제거 
            _bingoImageText.localPosition = new Vector3(-700, 0, 0);
            _bingoComplete.localScale = GameSystem.Instance.BaseScale;
            _bingoComplete.gameObject.SetActive(false);


            // 결과 창 오픈 
            // 마지막 보상은 띄우지 않고 추가 연출 처리 
            if(startIndex >= bingodataNode.Count -1) {
                isClearLastLine = true;
                break;
            }

            // 결과창 오픈 
            _bingoMissionResult.SetResultInfo(bingodataNode[startIndex]);

            // 결과창이 없어질때까지 대기
            while (_bingoMissionResult.gameObject.activeSelf) {
                yield return new WaitForSeconds(0.1f);
            } // waiting

            startIndex++;
        }




        
        if(isClearLastLine) {
            StartCoroutine(PlayingAllClear());
        }
        else {
            ShowButtons();
        }

	}


    /// <summary>
    /// 라인 클리어 후 , 빙고 효과 
    /// </summary>
    private void PlayLineBingoMotion() {
        _bingoImageText.DOLocalMoveX(0, 0.2f).SetEase(Ease.OutBounce).OnComplete(OnCompleteMoveBingoImageText);

        // PlayLineBingoMotion→OnCompleteMoveBingoImageText→OnCompleteShakeBingoImageText→PlayFirework
    }

    private void OnCompleteMoveBingoImageText() {
        _bingoImageText.DOShakeScale(0.2f, 1, 5, 30).OnComplete(OnCompleteShakeBingoImageText);
    }

    private void OnCompleteShakeBingoImageText() {
        // complete 등장 
        _bingoComplete.gameObject.SetActive(true);
        _bingoComplete.localScale = new Vector3(2, 2, 2);
        _bingoComplete.DOScale(1, 0.2f);

        

        PlayFirework();
    }

    private void PlayFirework() {
        StartCoroutine(PlayingFirework(0.2f));
    }

    IEnumerator PlayingFirework(float pInterval) {
        //yield return new WaitForSeconds(0.2f);
        for(int i =0; i< _arrLineClearFireworkPos.Length; i++) {
            _arrFireworks[i].Play(_arrLineClearFireworkPos[i]);
            PlayAudio(_acFirework);
            yield return new WaitForSeconds(pInterval);
        }
    }



	/// <summary>
	/// Clears the select.
	/// </summary>
    public void ClearSelect() {

        SelectedCol = null;

        for(int i =0; i<_arrBingoCols.Length; i++) {
            _arrBingoCols[i].UnselectCol();
        }
    }

    public void OpenMissionInfo(JSONNode pNode) {
        _bingoMissionInfo.SetMissionInfo(pNode, _bingoColor);
    }

    public void CloseMissionInfo() {
        _bingoMissionInfo.gameObject.SetActive(false);
    }

	public void CloseBingo() {

        GameSystem.Instance.Post2CheckNewMailUnder();
		BingoMasterUI.SetActive (false);

		// 
		LobbyCtrl.Instance.CheckUserLevelBingo();
	}



    // 연출처리 되지 않은 'fill' 상태의 Col, Line 체크 
    public bool CheckExistsUncheckedFill() {

        //Debug.Log("▷CheckExistsUncheckedFill :: " + GameSystem.Instance.BingoJSON.Count);
        JSONNode bingodataNode = _bingoMasterNode[JsonIndx]["bingodata"];
        for (int i = 0; i < bingodataNode.Count; i++) {

            // 변수로 비교는 value.equals로, 단순 스트링 비교는 == 로 처리

            if(!bingodataNode[i]["checked"].AsBool && bingodataNode[i]["state"].Value.Equals(FillState)) {
                Debug.Log("★★★ Exists Unchecked Fill!");
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// 아직 미달성된 col 이 있는지 체크 
    /// </summary>
    /// <returns></returns>
    public bool CheckExistsEmptyCol() {

        JSONNode bingodataNode = _bingoMasterNode[JsonIndx]["bingodata"];

        for (int i = 0; i < bingodataNode.Count; i++) {

            // 변수로 비교는 value.equals로, 단순 스트링 비교는 == 로 처리

            if(bingodataNode[i]["state"].Value.Equals(EmptyState)) {
                //Debug.Log("★★★ Exists Empty");
                return true;
            }

            if (!bingodataNode[i]["checked"].AsBool && bingodataNode[i]["state"].Value.Equals(FillState)) {
                //Debug.Log("★★★ Exists Unchecked Fill!");
                return true;
            }
        }

        return false;
    }




    /// <summary>
    /// 달성된 라인의 개수 
    /// </summary>
    /// <returns></returns>
    private void SetClearLineCount() {

        _currentClearLineCount = 0;

        JSONNode bingodataNode = _bingoMasterNode[JsonIndx]["bingodata"];

        for (int i = 0; i < bingodataNode.Count; i++) {

            // 변수로 비교는 value.equals로, 단순 스트링 비교는 == 로 처리

            if (!bingodataNode[i]["type"].Value.Equals("line")) {
                continue;
            }

            if(bingodataNode[i]["state"].Value.Equals(FillState)) {
                _currentClearLineCount++;
            }
        }

        Debug.Log("★★ SetClearLineCount :: " + _currentClearLineCount);
    }

    #region 빙고 All 클리어 모션 

    private void TestPlayAllClear() {
        StartCoroutine(PlayingAllClear());
    }

    IEnumerator PlayingAllClear() {

        HideButtons();

        // 올클리어 마크 등장
        _allClearMark.SetActive(true);
        _allClearMark.transform.localScale = new Vector3(2, 2, 2);
        PlayAudio(_acAllClear);
        _allClearMark.transform.DOScale(1, 0.2f);


        

        //blink 
        for (int i=0; i<3; i++) {
            _whiteScreen.SetActive(true);
            yield return new WaitForSeconds(0.05f);
            _whiteScreen.SetActive(false);
            yield return new WaitForSeconds(0.05f);
        }

        // 판 이동 
        _bingoBase.DOLocalMoveY(1300, 0.6f).SetEase(Ease.InBack).OnComplete(OnCompleteBingoBaseMove);
        PlayAudio(_acBingoBaseDisappear);

        // 판 올라가면서 Splash 이펙트 

        yield return new WaitForSeconds(0.1f);

        // Splashes
        for(int i=0; i<_arrSplashes.Length; i++) {
            _arrSplashes[i].Play(_arrLineClearSplashPos[i]);
            PlayAudio(_acBlink);
            yield return new WaitForSeconds(0.1f);
        }

        // → OnCompleteBingoBaseMove

    }

    private void OnCompleteBingoBaseMove() {
        // 네코 칼라 점프
        _bingoNekoColor.DOLocalJump(new Vector3(-5, 20, 0), 300, 1, 0.6f).OnComplete(OnCompleteBingoNekoJump);

        // Shadow는 제거
        _bingoNekoShadow.SetActive(false);

        // → OnCompleteBingoNekoJump
    }


    /// <summary>
    /// 올 클리어, 네코 점프 완료 
    /// </summary>
    private void OnCompleteBingoNekoJump() {
        

        // 오로라 표시 등장 
        _bingoNekoAura.SetActive(true);
        _bingoNekoAura.transform.DOKill();
        _bingoNekoAura.transform.localEulerAngles = Vector3.zero;
        _bingoNekoAura.transform.DOLocalRotate(new Vector3(0, 0, 360), 2, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);

        // 별 등장

        // 보상이 네코일때, 
        if (_bingoRewardNeko.IndexOf("だいや") < 0 && _bingoRewardNeko.IndexOf("gem") < 0 && _bingoRewardNeko.IndexOf("超れあねこちけっと") < 0) {
            _bingoNekoStarLabel.gameObject.SetActive(true);
            _bingoNekoStarLabel.transform.DOLocalJump(_bingoNekoStarLabel.transform.localPosition, 100, 1, 0.5f).OnComplete(OnCompleteNekoStarLabelMove);
        }

        // 특정 빙고만 레인보우티켓 증정 
        if(_bingoID == 4 || _bingoID == 10) {
            _bingoRainbowTicket.SetActive(true);
            _bingoRainbowTicket.transform.DOKill();
            _bingoRainbowTicket.transform.localScale = new Vector3(2, 2, 2);
            _bingoRainbowTicket.transform.DOScale(1, 0.5f).OnComplete(OnCompleteRainbowTicketShow);
            PlayAudio(_acBlink);
        }

        if(_bingoID == 7 || _bingoID == 8 || _bingoID == 9 || _bingoID == 10) {
            _bingoFinalGem.SetActive(true);
            _bingoFinalGem.transform.DOKill();
            _bingoFinalGem.transform.localScale = new Vector3(2, 2, 2);
            _bingoFinalGem.transform.DOScale(1, 0.5f).OnComplete(OnCompleteFinalGemShow);
            PlayAudio(_acBlink);
        }

        // → OnCompleteNekoStarLabelMove
    }


    private void OnCompleteFinalGemShow() {
        _bingoFinalGem.transform.DOScale(1.1f, 0.5f).SetLoops(-1, LoopType.Yoyo);

        if(_bingoRewardNeko.IndexOf("gem") >=0 || _bingoRewardNeko.IndexOf("だいや") >= 0 || _bingoRewardNeko.IndexOf("超れあねこちけっと") >= 0) {
            OnCompleteNekoStarLabelMove();
        }
    }

    private void OnCompleteRainbowTicketShow() {
        _bingoRainbowTicket.transform.DOScale(1.1f, 0.5f).SetLoops(-1, LoopType.Yoyo);
    }

    /// <summary>
    /// 올 클리어 등급 표시 완료 
    /// </summary>
    private void OnCompleteNekoStarLabelMove() {

        PlayAudio(_acStarOn);

        //_receiveInfo.SetActive(true);

        // 화면 Lock 해제 
        //SetBingoScreenLock(false);

        //ShowButtons();

        _socialButtons.SetActive(true);
        SetBingoScreenLock(false);

        PlayFirework();
    }


    public void SetBingoScreenLock(bool pLock) {
        _screenLock.SetActive(pLock);
    }


    #endregion

    

    #region 라인 체크 로직, 길어서 region 처리 

    IEnumerator PlayingLineClear(int pLineIndex) {
        _calLineIndex = pLineIndex - 25;

        Debug.Log("▶▶ PlayingLineClear :: " + _calLineIndex);


        if(_calLineIndex < 5) { // 가로줄 라인 
            _tempLineIndex = _calLineIndex;
            _tempLineIndex *= 5;

            for(int i = 0; i<5; i++) {
                _arrBingoCols[_tempLineIndex + i].PlayLineClearMotion();
                yield return new WaitForSeconds(_playIntervalTime);
            }
        }
        else if(_calLineIndex >= 5 && _calLineIndex < 10) { // 세로줄 라인 
            _tempLineIndex = _calLineIndex - 5;

            for(int i=0; i<5; i++) {
                _arrBingoCols[_tempLineIndex + (i * 5)].PlayLineClearMotion();
                yield return new WaitForSeconds(_playIntervalTime);
            }
        }
        else if(_calLineIndex == 10) {
            _arrBingoCols[0].PlayLineClearMotion();
            yield return new WaitForSeconds(_playIntervalTime);
            _arrBingoCols[6].PlayLineClearMotion();
            yield return new WaitForSeconds(_playIntervalTime);
            _arrBingoCols[12].PlayLineClearMotion();
            yield return new WaitForSeconds(_playIntervalTime);
            _arrBingoCols[18].PlayLineClearMotion();
            yield return new WaitForSeconds(_playIntervalTime);
            _arrBingoCols[24].PlayLineClearMotion();
            yield return new WaitForSeconds(_playIntervalTime);
        }
        else if (_calLineIndex == 11) {
            _arrBingoCols[4].PlayLineClearMotion();
            yield return new WaitForSeconds(_playIntervalTime);
            _arrBingoCols[8].PlayLineClearMotion();
            yield return new WaitForSeconds(_playIntervalTime);
            _arrBingoCols[12].PlayLineClearMotion();
            yield return new WaitForSeconds(_playIntervalTime);
            _arrBingoCols[16].PlayLineClearMotion();
            yield return new WaitForSeconds(_playIntervalTime);
            _arrBingoCols[20].PlayLineClearMotion();
            yield return new WaitForSeconds(_playIntervalTime);
        }
    }

    /// <summary>
    /// 각 Line별 Column 의 클리어 여부를 체크 
    /// </summary>
    public bool CheckLine(int pLineIndex) {


        tempNode = _bingoMasterNode[JsonIndx]["bingodata"];

        _calLineIndex = pLineIndex - 25; // 25를 뺀다. 

        //Debug.Log("★★ CheckLine :: " + _calLineIndex);


        if (_calLineIndex < 5) { // 가로줄 라인 체크 
            _tempLineIndex = _calLineIndex;
            _tempLineIndex *= 5;

            if(_tempLineIndex == 0) {
                /*
                Debug.Log("state _tempLineIndex[" + _tempLineIndex + "] state :: " + _bingoMasterNode[_tempLineIndex]["state"]);
                Debug.Log("state _tempLineIndex[" + _tempLineIndex +1 + "] state :: " + _bingoMasterNode[_tempLineIndex+1]["state"]);
                Debug.Log("state _tempLineIndex[" + _tempLineIndex +2 + "] state :: " + _bingoMasterNode[_tempLineIndex+2]["state"]);
                Debug.Log("state _tempLineIndex[" + _tempLineIndex +3 + "] state :: " + _bingoMasterNode[_tempLineIndex+3]["state"]);
                Debug.Log("state _tempLineIndex[" + _tempLineIndex +4 + "] state :: " + _bingoMasterNode[_tempLineIndex+4]["state"]);


                Debug.Log("state _tempLineIndex[" + _tempLineIndex + "] checked :: " + _bingoMasterNode[_tempLineIndex]["checked"]);
                Debug.Log("state _tempLineIndex[" + _tempLineIndex + 1 + "] checked :: " + _bingoMasterNode[_tempLineIndex + 1]["checked"]);
                Debug.Log("state _tempLineIndex[" + _tempLineIndex + 2 + "] checked :: " + _bingoMasterNode[_tempLineIndex + 2]["checked"]);
                Debug.Log("state _tempLineIndex[" + _tempLineIndex + 3 + "] checked :: " + _bingoMasterNode[_tempLineIndex + 3]["checked"]);
                Debug.Log("state _tempLineIndex[" + _tempLineIndex + 4 + "] checked :: " + _bingoMasterNode[_tempLineIndex + 4]["checked"]);
                */
            }

            
           

            
            if (tempNode[_tempLineIndex]["state"].Value.Equals(FillState)
                && tempNode[_tempLineIndex+1]["state"].Value.Equals(FillState)
                && tempNode[_tempLineIndex+2]["state"].Value.Equals(FillState)
                && tempNode[_tempLineIndex+3]["state"].Value.Equals(FillState)
                && tempNode[_tempLineIndex+4]["state"].Value.Equals(FillState)) {

                return true;
            }
            


        }
        else if (_calLineIndex >= 5 && _calLineIndex < 10) { // 세로줄 라인 
            _tempLineIndex = _calLineIndex - 5;

            if (tempNode[_tempLineIndex]["state"].Value.Equals(FillState) 
                && tempNode[_tempLineIndex + 5]["state"].Value.Equals(FillState)
                && tempNode[_tempLineIndex + 10]["state"].Value.Equals(FillState)
                && tempNode[_tempLineIndex + 15]["state"].Value.Equals(FillState)
                && tempNode[_tempLineIndex + 20]["state"].Value.Equals(FillState)) {

                return true;

            }
        }
        else if (_calLineIndex == 10) { // 왼쪽 대각선 라인 

            if (tempNode[0]["state"].Value.Equals(FillState)
                && tempNode[6]["state"].Value.Equals(FillState)
                && tempNode[12]["state"].Value.Equals(FillState)
                && tempNode[18]["state"].Value.Equals(FillState)
                && tempNode[24]["state"].Value.Equals(FillState)) {

                return true;

            }
        }
        else if (_calLineIndex == 11) { // 오른쪽 대각선 라인 

            if (tempNode[4]["state"].Value.Equals(FillState)
                && tempNode[8]["state"].Value.Equals(FillState)
                && tempNode[12]["state"].Value.Equals(FillState)
                && tempNode[16]["state"].Value.Equals(FillState)
                && tempNode[20]["state"].Value.Equals(FillState)) {

                return true;

            }
        }
           

        return false;
    }


    /// <summary>
    /// 각 라인 클리어상황에 다른 Column 설정 처리 
    /// </summary>
    /// <param name="pLineIndex"></param>
    private void SetLineState(int pLineIndex) {
        _calLineIndex = pLineIndex - 25;

        //Debug.Log("▶▶ SetLineState :: " + _calLineIndex);


        if (_calLineIndex < 5) { // 가로줄 라인 
            _tempLineIndex = _calLineIndex;
            _tempLineIndex *= 5;

            for (int i = 0; i < 5; i++) {
                _arrBingoCols[_tempLineIndex + i].SetClearState();
                
            }
        }
        else if (_calLineIndex >= 5 && _calLineIndex < 10) { // 세로줄 라인 
            _tempLineIndex = _calLineIndex - 5;

            for (int i = 0; i < 5; i++) {
                _arrBingoCols[_tempLineIndex + (i * 5)].SetClearState();
                
            }
        }
        else if (_calLineIndex == 10) {
            _arrBingoCols[0].SetClearState();
            
            _arrBingoCols[6].SetClearState();
            
            _arrBingoCols[12].SetClearState();
            
            _arrBingoCols[18].SetClearState();
            
            _arrBingoCols[24].SetClearState();
            
        }
        else if (_calLineIndex == 11) {
            _arrBingoCols[4].SetClearState();
            
            _arrBingoCols[8].SetClearState();
            
            _arrBingoCols[12].SetClearState();
            
            _arrBingoCols[16].SetClearState();
            
            _arrBingoCols[20].SetClearState();
            
        }
    }

    #endregion

    #region 버튼 Click 이벤트 

    /// <summary>
    /// 오른쪽 빙고 화살표 
    /// </summary>
    public void OnClickRightArrow() {
        JsonIndx++;
        SetBingoScreenLock(true);

        // 빙고 초기화 
        InitBingoMaster();

        // 빙고 외향 및 정보 설정 
        SetBingoBaseInfo();

        // 빙고 판 이동 처리 
        MoveBingoMasterUI();

    }

    /// <summary>
    /// 왼쪽 빙고 화살표 
    /// </summary>
    public void OnClickLeftArrow() {
        JsonIndx--;
        SetBingoScreenLock(true);

        // 빙고 초기화 
        InitBingoMaster();

        // 빙고 외향 및 정보 설정 
        SetBingoBaseInfo();

        // 빙고 판 이동 처리 
        MoveBingoMasterUI();
    }

    /// <summary>
    /// 빙고 도전 터치
    /// </summary>
    public void OnClickBingoSelect() {
        // 팝업창 오픈 
        LobbyCtrl.Instance.OpenUpperInfoPopUp(PopMessageType.BingoSelect, OnConfirmBingoSelect);
    }

    private void OnConfirmBingoSelect() {
        _bingoID = _bingoMasterNode[JsonIndx]["bingoid"].AsInt;
        RecoverProgress();
        GameSystem.Instance.Post2SelectBingo(_bingoID, OnCompleteBingoSelect);
    }

    /// <summary>
    /// 빙고 리트라이 터치 
    /// </summary>
    public void OnClickBingoRetry() {
        LobbyCtrl.Instance.OpenUpperInfoPopUp(PopMessageType.BingoRetry, OnConfirmBingoRetry);
    }

    private void OnConfirmBingoRetry() {
        _bingoID = _bingoMasterNode[JsonIndx]["bingoid"].AsInt;
        RecoverProgress();
        GameSystem.Instance.Post2SelectBingo(_bingoID, OnCompleteBingoSelect);
    }

    private void OnCompleteBingoSelect(JSONNode pNode) {
        GameSystem.Instance.UserJSON["currentbingoid"].AsInt = pNode["currentbingoid"].AsInt;

        OpenBingo();

    }

    /// <summary>
    /// 
    /// </summary>
    private void RecoverProgress() {

        Debug.Log("★RecoverProgress");

        JSONNode bingodataNode = _bingoMasterNode[_preJsonIndx]["bingodata"];
        _bingoMasterNode[_preJsonIndx]["progress"].AsBool = false;
        _isBingoProgressed = false;


        for (int i = 0; i < bingodataNode.Count; i++) {
            if (bingodataNode[i]["current"].AsInt > 0 || bingodataNode[i]["state"].Value.Equals(FillState)) {
                Debug.Log("★Progressed Bingo in RecoverProgress");
                // 하나라도 진행한 내용이 있으면 progress true 처리 
                _bingoMasterNode[_preJsonIndx]["progress"].AsBool = true;
                _isBingoProgressed = true;
                return;
            }
        }
    }

    #endregion



    public void PlayAudio(AudioClip pClip) {
        _audio.PlayOneShot(pClip);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="pValue"></param>
    private void SetArrStar(int pValue) {
        for(int i=0; i<_arrDifficultyStar.Length; i++) {
            _arrDifficultyStar[i].SetActive(false);
        }


        for(int i=0; i<pValue; i++) {
            _arrDifficultyStar[i].SetActive(true);
        }
    }


    #region 스크린 캡쳐 
    [SerializeField]
    GameObject objWaitingRequest = null;
    bool _isCapturing = false;
    [SerializeField]
    string _baseSnsText = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L4136);
	string _snstext = "";
    private Texture2D _tex; // 텍스쳐 

    public void FBCapture() {
        CaptureBingoScreen(SocialType.FB);
    }

    public void TWCapture() {
        CaptureBingoScreen(SocialType.twiiter);
    }

    public void CaptureBingoScreen(SocialType pType) {

        if (_isCapturing)
            return;

        _socialButtons.SetActive(false);

        // 캡쳐 시작
        StartCoroutine(TakeScreenShot(pType));

    }

    IEnumerator TakeScreenShot(SocialType pType) {
        _isCapturing = true;



        // snsText 수정
        if (_bingoRewardNeko.IndexOf("gem") >= 0 || _bingoRewardNeko.IndexOf("だいや") >= 0 || _bingoRewardNeko.IndexOf("超れあねこちけっと") >= 0) {
            _snstext = _baseSnsText.Replace("[n]", _bingoDifficulty);
        }
        else {
            _snstext = _baseSnsText.Replace("[n]", GameSystem.Instance.GetNekoName(int.Parse(_bingoRewardNeko), 3));
        }


            
            

        yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(0.1f);

        string type = string.Empty;

        _tex = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);

        yield return new WaitForEndOfFrame();

        _tex.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        _tex.Apply();

        if (pType == SocialType.FB) {
            type = "facebook.katana";
        }
        else if (pType == SocialType.twiiter) {
            type = "twi";
        }

        // Lock 객체 활성화
        this.objWaitingRequest.SetActive(true);

#if UNITY_ANDROID

        if (pType == SocialType.twiiter) { // 트위터 
            AndroidSocialGate.OnShareIntentCallback += HandleOnShareIntentCallback;
            AndroidSocialGate.StartShareIntent(_snstext, _snstext, _tex, type);
        }
        else { // FB
            PostFBImage();
            yield break;

        }

#elif UNITY_IOS
        if (pType == SocialType.twiiter) {
            IOSSocialManager.OnTwitterPostResult += OnTwiiterPostResult;
            IOSSocialManager.Instance.TwitterPost(_snstext, null, _tex);
        }
        else if (pType == SocialType.FB) {
            IOSSocialManager.OnFacebookPostResult += OnFacebookPostResult;
            IOSSocialManager.Instance.FacebookPost(_snstext, null, _tex);
        }

#endif


        _isCapturing = false;
        StartCoroutine(UnlockScreen());
    }


    IEnumerator UnlockScreen() {
        
        yield return new WaitForSeconds(2);
        _isCapturing = false;

        _socialButtons.SetActive(true);
        this.objWaitingRequest.SetActive(false);

    }


    public void PostFBImage() {

        // 로그인 되지 않았으면 로그인 처리 
        if (!FB.IsLoggedIn) {
            MNPFacebookCtrl.Instance.LoginFB();
            _isCapturing = false;
            this.objWaitingRequest.SetActive(false);
            StartCoroutine(UnlockScreen());
            return;

        }
        AndroidNotificationManager.Instance.ShowToastNotification("Facebook投稿を開始します。", 5);

        //MNPFacebookCtrl.OnPostingCompleteAction += OnCompletePosingFB;
        MNPFacebookCtrl.Instance.PostImage(_snstext, _snstext, _tex);

        // 올리는것처럼 보여준다. 
        StartCoroutine(UnlockScreen());

    }

    /// <summary>
    /// AndroidSocialGate.OnShareIntentCallback 콜백
    /// </summary>
    /// <param name="status"></param>
    /// <param name="package"></param>
    void HandleOnShareIntentCallback(bool status, string package) {
        AndroidSocialGate.OnShareIntentCallback -= HandleOnShareIntentCallback;
        Debug.Log("[HandleOnShareIntentCallback] " + status.ToString() + " " + package);

        this.objWaitingRequest.SetActive(false);

        if (status) {
            LobbyCtrl.Instance.OpenUpperInfoPopUp(PopMessageType.UploadComplete);
        }
        else {
            LobbyCtrl.Instance.OpenUpperInfoPopUp(PopMessageType.UploadFail);
        }

        

    }

    void OnTwiiterPostResult(SA.Common.Models.Result result) {

        this.objWaitingRequest.SetActive(false);

        Debug.Log("OnTwiiterPostResult");
        IOSSocialManager.OnTwitterPostResult -= OnTwiiterPostResult;

        if (result.IsSucceeded) {
            LobbyCtrl.Instance.OpenUpperInfoPopUp(PopMessageType.UploadComplete);
        }
        else {
            LobbyCtrl.Instance.OpenUpperInfoPopUp(PopMessageType.UploadFail);
        }

        


    }

    void OnFacebookPostResult(SA.Common.Models.Result result) {

        this.objWaitingRequest.SetActive(false);

        Debug.Log("OnFacebookPostResult");
        IOSSocialManager.OnFacebookPostResult -= OnFacebookPostResult;

        if (result.IsSucceeded) {
            LobbyCtrl.Instance.OpenUpperInfoPopUp(PopMessageType.UploadComplete);
        }
        else {
            LobbyCtrl.Instance.OpenUpperInfoPopUp(PopMessageType.UploadFail);
        }

        

    }

    #endregion

    /// <summary>
    /// 이벤트 페이지 오픈 
    /// </summary>
    public void OpenHelpPage() {
        for (int i = 0; i < GameSystem.Instance.NoticeBannerInitJSON.Count; i++) {
            if (GameSystem.Instance.NoticeBannerInitJSON[i]["action"].Value == "help") {
                //this.GetComponent<LobbyCommonUICtrl>().CloseSelf();
                WindowManagerCtrl.Instance.OpenNoticeDetail(i);
            }
        }
    }

}
