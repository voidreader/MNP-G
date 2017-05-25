using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PathologicalGames;
using CodeStage.AntiCheat.ObscuredTypes;
using SimpleJSON;
using DG.Tweening;

public partial class InGameCtrl : MonoBehaviour {



    static InGameCtrl _instance = null;

    [SerializeField] GameObject objWaitingRequest = null; // 통신용도 
    private AsyncOperation asyncOp;
    private bool isLoadGame = false;

    /* FSM 상태 머신 */
    public GameState currentState; // 현재 상태 
    public GameState preState; // 이전상태 

    private bool _isNewState; // 새 상태로 변경 되었는지 여부 
    private bool isPlaying = false;
    private bool isBonusTime = false; // 보너스 타임 진행 여부 
    private bool isFirstPlay = false;

    public bool updateLock = true;
    public bool timerLock = false;

    // 필드 상 빈공간 리스트 
    public List<BlockCtrl> listEmptyBlock = new List<BlockCtrl>();
    private List<UISpriteData> listHurryTimerSprites; // 


    // 필드 블록 관련 변수 
    //public int fieldHeight = 0;
    //public int fieldWidth = 0;
    public BlockCtrl[,] fieldBlocks; // StageGenerator에서 생성하면서 배열처리 
    List<BlockCtrl> _listFieldBlock = new List<BlockCtrl>();

    // Spawn 에 사용되는 변수
    private bool isSpawned = false;
    private string blockPrefab = "Block";
    private Transform spawnBlock; // 생성된 Spawn prefab
    private float localX; // 좌표
    private float localY;

    // 필드에 리스폰 되는 블록의 수 
    [SerializeField] int _fillBlockCount = 0;

    // 블록이 생성될 수 있는 유효 공간
    [SerializeField] int _validBlockSpaceCount = 0;


    // 장착 부스트 아이템 체크 
    bool _boostPlayTime = false; // 플레이 시간 증가 
    bool _boostBombCreate = false; // 폭탄 생성에 필요한 블록 감소 
    bool _boostSpecialAttack = false; // 스페셜 어택에 필요한 블록 감소 
    bool _boostFirework = false;
    

    public int _equipItemCount = 0;
    private bool _isDoingBackToMenu = false;
    bool _isRespawningBoard = false;

    [SerializeField] int _passiveSkillCount = 0;
    [SerializeField] bool _isShowingSkillAndIcon = true; // 게임 첫시작시 연출 중임을 알림.

    // 이벤트 정보
    [SerializeField] List<int> _listCurrentEvent;

    // 네코 패시브 정보
    [SerializeField] List<MyNekoPassiveCtrl> _listNekoPassive = new List<MyNekoPassiveCtrl>();
    private BlockCtrl pickPassiveBombBlock = null;
    [SerializeField] int _nekoTotalPower = 0; // 네코 파워 합계 
    [SerializeField] int _bombAppearBlockCount = 60;
    [SerializeField] int _skillInvokeBlockCount = 50;


    #region 맵 Type 세팅 

    [SerializeField] string _stageMapType = "standard";
    [SerializeField] int _colorCount = 3;


    // 특수 미션 여부  (InGameStageInfoCtrl.cs에서 할당)
    [SerializeField] bool _isCookieMission = false; // 쿠키 미션 
    [SerializeField] bool _isStoneMission = false; // 바위 미션 
    [SerializeField] bool _isFishMission = false; // 생선 튀기기 미션 
    [SerializeField] bool _isMoveMission = false; // 이동 미션

    [SerializeField] bool _isBossStage = false; // 보스 스테이지 
    [SerializeField] bool _isRescueStage = false; // 구출 스테이지 



    [SerializeField]
    List<MoveTileCtrl> _listMoveTiles = new List<MoveTileCtrl>(); // 무브 타일 
    List<BlockCtrl> _listMoveTilesBlock = new List<BlockCtrl>(); // 무브 타일 
    BlockCtrl _moveTileStartBlock = null; // 무브타일 시작지점의 블록 

    [SerializeField]
    MovingCatCtrl _currentMovingCat;

    // 무브 미션 
    int _moveMissionCount = 0;
    int _initMoveMissionGoal = 0;
    bool _isProcessingMove = false;
    [SerializeField]
    int _moveDefenceType = 0; 

    #endregion

    // 퍼즐 팁
    [SerializeField]
    GTipCtrl _puzzleTip;

    // Alert 
    [SerializeField]
    GameObject _alertMark
        ;
    [SerializeField]
    Transform _alertText;


    BlockCtrl _tempBlockCtrl;

    [SerializeField] GameObject _confirmExit;

    [SerializeField] float _naviTime = 3; // 미입력시 가이드 표시 타임 
    [SerializeField] TutorialExplainCtrl _explain;
    [SerializeField] bool _isWaitingExplain = false; // 설명창 대기 

    [SerializeField] ParticleSystem _firework9;
    [SerializeField] ParticleSystem _firework10;
    bool _isFireworking = false;

    public static InGameCtrl Instance {
        get {
            if (_instance == null) {
                _instance = FindObjectOfType(typeof(InGameCtrl)) as InGameCtrl;

                if (_instance == null) {
                    //Debug.Log("InGameCtrl Init Error");
                    return null;
                }
            }

            return _instance;
        }
    }



    void Awake() {

        // 시저링 
        ScissorCtrl.Instance.UpdateResolution();

        // 게임 상태 초기화 
        preState = GameState.Ready;
        currentState = GameState.Ready;

        //fieldWidth = PuzzleConstBox.width;
        //fieldHeight = PuzzleConstBox.height;
        fieldBlocks = new BlockCtrl[GameSystem.Instance.Height, GameSystem.Instance.Width];

        comboTime = GameSystem.Instance.ComboKeepTime;

        // 플레이 시간 설정 
        playTime = GameSystem.Instance.StageDetailJSON[GameSystem.Instance.PlayStage - 1]["time"].AsFloat;
        playTimeValue = (int)playTime;
        lblInGameTimer.text = playTimeValue.ToString();


        // 맵 형태 체크 
        _stageMapType = GameSystem.Instance.StageDetailJSON[GameSystem.Instance.PlayStage - 1]["map"];


    }

    // Use this for initialization
    void Start() {


        InitField();

        // 미션 정보 
        CheckStageMission();

        // 장착 부스트 체크 
        CheckEquipItem();

        // 네코 패시브 
        CheckNekoPassivePlus();

        // Equip Neko 처리 
        InitPlayerNekoBars();


        // 블록 파워 설정
        InitBlockPower();


        // Enemy Neko 소환 
        EnemyNekoManager.Instance.InitStageRescueNeko();

        // 아이템 블록 준비 
        InUICtrl.Instance.SetItemHead();

        // 가이드 표시시간 초기화 
        InitNaviTime();


        // 퍼즐 팁 체크 
        //CheckPuzzleTipUnlock();

        // 네코 뱃지 보너스 추가 
        GetUserNekoBadgesBonus();

        // 고양이 스킬에 따른 시작 폭탄 생성
        if(GameSystem.Instance.NekoStartBombCount > 0) {
            DropRandomBomb(GameSystem.Instance.NekoStartBombCount);
        }


    }


    void OnEnable() {
        StartCoroutine("FSMMain");
    }

    public void SetState(GameState pNewState) {
        _isNewState = true;

        preState = currentState;
        currentState = pNewState;
    }

    /// <summary>
    /// 인게임 매니저 상태머신 메인. 
    /// </summary>
    /// <returns>The main.</returns>
    IEnumerator FSMMain() {
        while (Application.isPlaying) {
            _isNewState = false;
            yield return StartCoroutine(currentState.ToString());
        }
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Z)) {
            InUICtrl.Instance.StartSeqSummonItem();
        } else if (Input.GetKeyDown(KeyCode.T)) {
            //this.SpawnTripleAttack (InUICtrl.Instance.arrMyNekoBarCtrl);
        } else if (Input.GetKeyDown(KeyCode.H)) {
            EnemyNekoManager.Instance.CurrentEnemyNeko.HitEnemyNeko(1000000, true);
        } else if (Input.GetKeyDown(KeyCode.P)) {
            playTime = 5;
        } else if (Input.GetKeyDown(KeyCode.Minus) || Input.GetKeyDown(KeyCode.KeypadMinus) || Input.GetKeyDown(KeyCode.DownArrow)) {

            Time.timeScale -= 0.1f;
            if (Time.timeScale < 0.1f) {
                Time.timeScale = 0.1f;
            }
        } else if (Input.GetKeyDown(KeyCode.Plus) || Input.GetKeyDown(KeyCode.KeypadPlus) || Input.GetKeyDown(KeyCode.UpArrow)) {
            Time.timeScale += 0.1f;
            if (Time.timeScale >= 1f) {
                Time.timeScale = 1f;
            }
        } else if (Input.GetKeyDown(KeyCode.N)) {

        }
        /*
        else if(Input.GetKeyDown(KeyCode.Alpha1)) {
			InUICtrl.Instance.FullMyNekoBar(0);
		} else if(Input.GetKeyDown(KeyCode.Alpha2)) {
			InUICtrl.Instance.FullMyNekoBar(1);
		} else if(Input.GetKeyDown(KeyCode.Alpha3)) {
			InUICtrl.Instance.FullMyNekoBar(2);
		}
        */

    }


    public bool GetIsPlaying() {
        return isPlaying;
    }


    /// <summary>
    /// 가이드 표시시간 초기화 
    /// </summary>
    private void InitNaviTime() {

        _naviTime = 3;

        /*
        if (!GameSystem.Instance.OptionPuzzleTip) {
            _naviTime = GameSystem.Instance.EnvInitJSON["puzzle_navi_time"].AsFloat;
        }
        else {
            _naviTime = 3;
        }
        */
    }

    /// <summary>
    /// 블록파워 초기화
    /// 기본 블록파워 10 + 패시브 레벨별 파워
    /// </summary>
    private void InitBlockPower() {

        Debug.Log("★★★ InitBlockPower BlockAttackPower ::" + GameSystem.Instance.BlockAttackPower);
        //Debug.Log("★★★ Init InGame Block Power :: " + GameSystem.Instance.BlockAttackPower);
    }


    #region 보너스 타임 

    /// <summary>
    /// 블록 페이스 변경처리 
    /// </summary>
    private void SetBlockFace() {
        for (int i = 0; i < GameSystem.Instance.Height; i++) {
            for (int j = 0; j < GameSystem.Instance.Width; j++) {

                fieldBlocks[i, j].SetGamePlayBlock();
            }
        }
    }

    private void SetTimeOverBlock() {
        for (int i = 0; i < GameSystem.Instance.Height; i++) {
            for (int j = 0; j < GameSystem.Instance.Width; j++) {

                fieldBlocks[i, j].SetTimeoverBlock();
            }
        }
    }


    public bool GetBonusTimePossible() {

        // 필드에 폭탄 블록이 있거나, 가득찬 플레이어 네코 바 게이지가 있는지 체크 
        for (int i = 0; i < GameSystem.Instance.Height; i++) {
            for (int j = 0; j < GameSystem.Instance.Width; j++) {
                if (fieldBlocks[i, j].currentState == BlockState.Item) {
                    return true;
                }
            }
        }



        return false;
    }

    /// <summary>
    /// 필드상 폭탄 블록 체크 
    /// </summary>
    /// <returns><c>true</c>, if bomb block check was gotten, <c>false</c> otherwise.</returns>
    public bool GetBombBlockCheck() {

        // 필드에 폭탄 블록이 있거나, 가득찬 플레이어 네코 바 게이지가 있는지 체크 
        for (int i = 0; i < GameSystem.Instance.Height; i++) {
            for (int j = 0; j < GameSystem.Instance.Width; j++) {
                if (fieldBlocks[i, j].currentState == BlockState.Item) {
                    return true;
                }
            }
        }

        return false;
    }

    public void StartBonusTime() {

        Debug.Log(">>>>>>>>> StartBonusTime");

        StartCoroutine(LoopingBonusTime());

    }

    IEnumerator LoopingBonusTime() {

        //isPlaying = true;
        isBonusTime = true; // 보너스 타임으로 변경 

        if (currentState != GameState.Play)
            this.SetState(GameState.Play);

        // BGM 정지.
        InSoundManager.Instance.StopBGM();
        InSoundManager.Instance.PlayBonusTime();

        // Combo 초기화 
        InUICtrl.Instance.ResetCombo();

        yield return new WaitForSeconds(1.5f);
        InUICtrl.Instance.OnCompleteBonusTimeScale();



        while (GetBonusTimePossible()) {


            SetBlockFace();

            // 폭탄 체크 시작
            while (GetBombBlockCheck()) {

                for (int i = 0; i < GameSystem.Instance.Height; i++) {
                    for (int j = 0; j < GameSystem.Instance.Width; j++) {

                        if (fieldBlocks[i, j].currentState == BlockState.Item) {
                            fieldBlocks[i, j].TouchBlock();
                            yield return new WaitForSeconds(1);
                        }
                    }
                }
            } // end of Bomb Check


            // 끝났으면 다시 체크한다. 끝날때까지 반복.
        }



        yield return new WaitForSeconds(2);
        InUICtrl.Instance.OnTimeOver();

    }


    #endregion

    #region 스테이지 생성
    /// <summary>
    /// 필드 블록 초기화 
    /// </summary>
    public void InitField() {
        if (isSpawned)
            PoolManager.Pools["Blocks"].DespawnAll();

        for (int i = 0; i < GameSystem.Instance.Height; i++) {
            for (int j = 0; j < GameSystem.Instance.Width; j++) {
                fieldBlocks[i, j] = null;
            }
        }

        listEmptyBlock.Clear(); // 빈공간 제어용도 
        _listFieldBlock.Clear();

        // 필드 Inactive 생성 
        for (int i = 0; i < GameSystem.Instance.Height; i++) {

            for (int j = 0; j < GameSystem.Instance.Width; j++) {

                localX = GameSystem.Instance.BlockStartX + j * 0.78f;
                localY = GameSystem.Instance.BlockStartY - i * 0.78f;

                //localX = GameSystem.Instance.BlockStartX + GameSystem.Instance.BlockScaleValue * j * 1f;
                //localY = GameSystem.Instance.BlockStartY - GameSystem.Instance.BlockScaleValue * i * 1f;

                spawnBlock = PoolManager.Pools["Blocks"].Spawn(blockPrefab, new Vector3(localX, localY, 0), Quaternion.identity);
                spawnBlock.transform.localScale = GameSystem.Instance.BlockScale;
                fieldBlocks[i, j] = spawnBlock.gameObject.GetComponent<BlockCtrl>();

                fieldBlocks[i, j].SetBlockPos(i, j); // 위치 지정 
                fieldBlocks[i, j].SetState(BlockState.Inactive); // 상태값 None 으로 초기화 

                _listFieldBlock.Add(fieldBlocks[i, j]);

            }
        } /// Inactive 필드 생성 루프 종료 


        // 맵 형태에 따른 None 설정 
        SetFieldNoneStateBlocks(_stageMapType);

    }


    /// <summary>
    /// 
    /// </summary>
    void SetValidBlockSpaceCount() {
        for (int i = 0; i < GameSystem.Instance.Height; i++) {
            for (int j = 0; j < GameSystem.Instance.Width; j++) {

                if (fieldBlocks[i, j].currentState == BlockState.None)
                    ValidBlockSpaceCount++;
            }
        }


        Debug.Log(">>>> SetValidBlockSpaceCount :: " + ValidBlockSpaceCount);
    }

    /// <summary>
    /// 리스폰 되는 블록 수 설정 (매번 스테이지 갱신시점 마다 처리)
    /// </summary>
    void SetFillBlockCount() {


        int tempFillBlockCount = ValidBlockSpaceCount / 2;

        int stone = 0;
        int fish = 0;
        int moving = 0;
        int firework = 0;

        // 필드에 출현한 특수블록 개수 만큼 제외해야한다. 
        // 바위, 생선, 무빙, 폭죽

        for (int i = 0; i < GameSystem.Instance.Height; i++) {
            for (int j = 0; j < GameSystem.Instance.Width; j++) {

                if (fieldBlocks[i, j].currentState == BlockState.Inactive)
                    continue;

                if (fieldBlocks[i, j].currentState == BlockState.FishGrill)
                    fish++;

                if (fieldBlocks[i, j].currentState == BlockState.StrongStone || fieldBlocks[i, j].currentState == BlockState.Stone)
                    stone++;

                moving = ListMoveTiles.Count;

                if (fieldBlocks[i, j].currentState == BlockState.Firework || fieldBlocks[i, j].currentState == BlockState.FireworkCap)
                    firework++;
            }
        }


        // 나누기 2한다. 
        stone = stone / 2;
        fish = fish / 2;
        moving = moving / 2;
        firework = firework / 2;

        tempFillBlockCount = tempFillBlockCount - (stone + fish + moving + firework);

        FillBlockCount = tempFillBlockCount;

        Debug.Log(">>>> SetFillBlockCount :: " + FillBlockCount);


    }

    /// <summary>
    /// 스테이지 생성
    /// </summary>
    IEnumerator GenerateStage() {


        Debug.Log("★★★ GenerateStage");

        int emptyIndx = -1;

        // 이동 미션의 경우 경로상의 블록은 ListEmptyBlock에서 제거 
        if (IsMoveMission) {
            for (int i = 0; i < ListMoveTilesBlock.Count; i++) {
                listEmptyBlock.Remove(ListMoveTilesBlock[i]);
            }
        }

        // 보스&구출 스테이지일때 폭죽 추가 
        if ( (IsBossStage || IsRescueStage ) && BoostFirework) {
            for (int i = 0; i < 3; i++) {
                emptyIndx = Random.Range(0, listEmptyBlock.Count);
                listEmptyBlock[emptyIndx].SetState(BlockState.FireworkCap);
            }
        }

        SetFillBlockCount(); // FillBlockCount 


        // Idle 생성 시작
        for (int i = 0; i < FillBlockCount; i++) {

            // listEmptyBlock에서 임의의 공간을 골라서 Idle 상태로 변경(listEmptyBlock은 Block Ctrl에서 제거
            emptyIndx = Random.Range(0, listEmptyBlock.Count);

            listEmptyBlock[emptyIndx].SetBlockType(Random.Range(0, GameSystem.Instance.BlockTypeCount)); // 랜덤 배치 
            //listEmptyBlock[emptyIndx].JumpBlock();
            listEmptyBlock[emptyIndx].SetState(BlockState.Idle);



            InSoundManager.Instance.PlayBlockAraise(); // 블록 생성음 

            yield return new WaitForSeconds(0.03f);
        }

        isSpawned = true;


        // 첫 생성시점에 매칭 체크 추가 2017.03
        if (!CheckStageMatch()) {
            //Debug.Log("♠♠♠ No Match In this Stage!");
            SetBoardClearResult();
            RemoveAllIdleBlocks();
            StartCoroutine(RespawnStageBlock());
        }
    }


    #region Map 형태에 따른 None 설정 

    /// <summary>
    /// 
    /// </summary>
    /// <param name="pMapType"></param>
    private void SetFieldNoneStateBlocks(string pMapType) {

        switch (pMapType) {
            case "7x7": // 7x7 기본 형태 
                InitMap7x7();
                break;

            case "9x9":
                InitMap9x9();
                break;

            case "Big Fat Cross":
                InitMapBigFatCross();
                break;
                
            case "Big Doughnut":
                InitMapBigDoughnut();
                break;

            case "Big Zigzag":
                InitMapBigZigzag();
                break;

            case "SmallType1":
                InitSmallType1();
                break;

            case "DiagonalType1":
                InitDiagonalType1();
                break;

            case "DiagonalType2":
                InitDiagonalType2();
                break;

            case "Rhombus":
                InitRhombus();
                break;

            case "DividedType1":
                InitDividedType1();
                break;

            case "Pyramid":
                InitPyramid();
                break;

            case "Narrow":
                InitNarrow();
                break;

            case "X":
                InitX();
                break;

            case "Hourglass":
                InitHourglass();
                break;

            case "Custom":
                InitCustomMap();
                break;

        }

        SetValidBlockSpaceCount();
        CheckSpecialTile(); // 특수 타일 구현 
        SetFillBlockCount(); // 리스폰 블록 개수 정하기 

    }

    #region 7x7

    /// <summary>
    /// 7x7 맵 설정 
    /// </summary>
    void InitMap7x7() {

        // 1~7 index 
        for (int i = 1; i < 8; i++) {

            for (int j = 1; j < 8; j++) {
                fieldBlocks[i, j].SetState(BlockState.None); // 상태값 None 으로 초기화 
            }
        }

        
       
    }
    #endregion

    #region SmallType1

    /// <summary>
    /// SmallType1 
    /// </summary>
    void InitSmallType1() {

        // 1~7 index 
        for (int i = 1; i < 8; i++) {

            for (int j = 1; j < 8; j++) {

                if (i == 1 && j == 1)
                    continue;

                if (i == 1 && j == 7)
                    continue;

                if (i == 7 && j == 1)
                    continue;

                if (i == 7 && j == 7)
                    continue;

                fieldBlocks[i, j].SetState(BlockState.None); // 상태값 None 으로 초기화 

            }
        }

        
    }

    #endregion

    #region 9x9
    /// <summary>
    /// 9x9 맵 설정 
    /// </summary>
    void InitMap9x9() {

        

        for (int i = 0; i < GameSystem.Instance.Height; i++) {

            for (int j = 0; j < GameSystem.Instance.Width; j++) {
                fieldBlocks[i, j].SetState(BlockState.None); // 상태값 None 으로 초기화 
            }
        }

        

    }
    #endregion

    #region Big Zigzag

    /// <summary>
    /// 지그재그 구멍 맵
    /// </summary>
    void InitMapBigZigzag() {
        

        for (int i = 0; i < 2; i++) {
            for (int j = 0; j < 7; j++) {
                fieldBlocks[i, j].SetState(BlockState.None);
        
            }
        }

        for (int i = 2; i < 4; i++) {
            for (int j = 0; j < 2; j++) {
                fieldBlocks[i, j].SetState(BlockState.None);
        
            }

            for (int j = 4; j < 9; j++) {
                fieldBlocks[i, j].SetState(BlockState.None);
        
            }
        }

        for (int i = 4; i < 5; i++) {
            for (int j = 0; j < 9; j++) {
                fieldBlocks[i, j].SetState(BlockState.None);
        
            }
        }



        for (int i = 5; i < 7; i++) {
            for (int j = 0; j < 5; j++) {
                fieldBlocks[i, j].SetState(BlockState.None);
        
            }

            for (int j = 7; j < 9; j++) {
                fieldBlocks[i, j].SetState(BlockState.None);
        
            }
        }

        for (int i = 7; i < 9; i++) {
            for (int j = 2; j < 9; j++) {
                fieldBlocks[i, j].SetState(BlockState.None);
        
            }

        }

        
    }

    #endregion

    #region Big Doughnut

    /// <summary>
    /// 도넛 맵 
    /// </summary>
    void InitMapBigDoughnut() {
        

        for (int i = 0; i < 3; i++) {
            for (int j = 0; j < 9; j++) {
                fieldBlocks[i, j].SetState(BlockState.None);
                
            }
        }

        for (int i = 3; i < 6; i++) {
            for (int j = 0; j < 3; j++) {
                fieldBlocks[i, j].SetState(BlockState.None);
                
            }
        }


        for (int i = 3; i < 6; i++) {
            for (int j = 6; j < 9; j++) {
                fieldBlocks[i, j].SetState(BlockState.None);
        
            }
        }



        for (int i = 6; i < 9; i++) {
            for (int j = 0; j < 9; j++) {
                fieldBlocks[i, j].SetState(BlockState.None);
        
            }
        }

        
    }
    #endregion

    #region Big Fat Cross
    /// <summary>
    /// 크고 뚱뚱한 십자가 맵 
    /// </summary>
    void InitMapBigFatCross() {

        
        // 2~6 index 
        for (int i = 2; i < 7; i++) {
            for (int j = 0; j < 9; j++) {
                fieldBlocks[i, j].SetState(BlockState.None);
        
            }
        }


        for (int i = 0; i < 2; i++) {
            for (int j = 2; j < 7; j++) {
                fieldBlocks[i, j].SetState(BlockState.None);
        
            }
        }

        for (int i = 7; i < 9; i++) {
            for (int j = 2; j < 7; j++) {
                fieldBlocks[i, j].SetState(BlockState.None);
        
            }
        }


        
    }

    #endregion


    #region DiagonalType1, 2
    void InitDiagonalType1() {

        _listFieldBlock[0].SetState(BlockState.None);
        _listFieldBlock[1].SetState(BlockState.None);
        _listFieldBlock[2].SetState(BlockState.None);
        _listFieldBlock[3].SetState(BlockState.None);
        _listFieldBlock[4].SetState(BlockState.None);
        _listFieldBlock[9].SetState(BlockState.None);
        _listFieldBlock[10].SetState(BlockState.None);
        _listFieldBlock[11].SetState(BlockState.None);
        _listFieldBlock[12].SetState(BlockState.None);
        _listFieldBlock[13].SetState(BlockState.None);
        _listFieldBlock[14].SetState(BlockState.None);
        _listFieldBlock[18].SetState(BlockState.None);
        _listFieldBlock[19].SetState(BlockState.None);
        _listFieldBlock[20].SetState(BlockState.None);
        _listFieldBlock[21].SetState(BlockState.None);
        _listFieldBlock[22].SetState(BlockState.None);
        _listFieldBlock[23].SetState(BlockState.None);
        _listFieldBlock[24].SetState(BlockState.None);
        _listFieldBlock[27].SetState(BlockState.None);
        _listFieldBlock[28].SetState(BlockState.None);
        _listFieldBlock[29].SetState(BlockState.None);
        _listFieldBlock[30].SetState(BlockState.None);
        _listFieldBlock[31].SetState(BlockState.None);
        _listFieldBlock[32].SetState(BlockState.None);
        _listFieldBlock[33].SetState(BlockState.None);
        _listFieldBlock[34].SetState(BlockState.None);
        _listFieldBlock[36].SetState(BlockState.None);
        _listFieldBlock[37].SetState(BlockState.None);
        _listFieldBlock[38].SetState(BlockState.None);
        _listFieldBlock[39].SetState(BlockState.None);
        _listFieldBlock[40].SetState(BlockState.None);
        _listFieldBlock[41].SetState(BlockState.None);
        _listFieldBlock[42].SetState(BlockState.None);
        _listFieldBlock[43].SetState(BlockState.None);
        _listFieldBlock[44].SetState(BlockState.None);
        _listFieldBlock[46].SetState(BlockState.None);
        _listFieldBlock[47].SetState(BlockState.None);
        _listFieldBlock[48].SetState(BlockState.None);
        _listFieldBlock[49].SetState(BlockState.None);
        _listFieldBlock[50].SetState(BlockState.None);
        _listFieldBlock[51].SetState(BlockState.None);
        _listFieldBlock[52].SetState(BlockState.None);
        _listFieldBlock[53].SetState(BlockState.None);
        _listFieldBlock[56].SetState(BlockState.None);
        _listFieldBlock[57].SetState(BlockState.None);
        _listFieldBlock[58].SetState(BlockState.None);
        _listFieldBlock[59].SetState(BlockState.None);
        _listFieldBlock[60].SetState(BlockState.None);
        _listFieldBlock[61].SetState(BlockState.None);
        _listFieldBlock[62].SetState(BlockState.None);
        _listFieldBlock[66].SetState(BlockState.None);
        _listFieldBlock[67].SetState(BlockState.None);
        _listFieldBlock[68].SetState(BlockState.None);
        _listFieldBlock[69].SetState(BlockState.None);
        _listFieldBlock[70].SetState(BlockState.None);
        _listFieldBlock[71].SetState(BlockState.None);
        _listFieldBlock[76].SetState(BlockState.None);
        _listFieldBlock[77].SetState(BlockState.None);
        _listFieldBlock[78].SetState(BlockState.None);
        _listFieldBlock[79].SetState(BlockState.None);
        _listFieldBlock[80].SetState(BlockState.None);

        


    }

    void InitDiagonalType2() {


        _listFieldBlock[4].SetState(BlockState.None);
        _listFieldBlock[5].SetState(BlockState.None);
        _listFieldBlock[6].SetState(BlockState.None);
        _listFieldBlock[7].SetState(BlockState.None);
        _listFieldBlock[8].SetState(BlockState.None);

        _listFieldBlock[12].SetState(BlockState.None);
        _listFieldBlock[13].SetState(BlockState.None);
        _listFieldBlock[14].SetState(BlockState.None);
        _listFieldBlock[15].SetState(BlockState.None);
        _listFieldBlock[16].SetState(BlockState.None);
        _listFieldBlock[17].SetState(BlockState.None);

        _listFieldBlock[20].SetState(BlockState.None);
        _listFieldBlock[21].SetState(BlockState.None);
        _listFieldBlock[22].SetState(BlockState.None);
        _listFieldBlock[23].SetState(BlockState.None);
        _listFieldBlock[24].SetState(BlockState.None);
        _listFieldBlock[25].SetState(BlockState.None);
        _listFieldBlock[26].SetState(BlockState.None);
        
        _listFieldBlock[28].SetState(BlockState.None);
        _listFieldBlock[29].SetState(BlockState.None);
        _listFieldBlock[30].SetState(BlockState.None);
        _listFieldBlock[31].SetState(BlockState.None);
        _listFieldBlock[32].SetState(BlockState.None);
        _listFieldBlock[33].SetState(BlockState.None);
        _listFieldBlock[34].SetState(BlockState.None);
        _listFieldBlock[35].SetState(BlockState.None);

        _listFieldBlock[36].SetState(BlockState.None);
        _listFieldBlock[37].SetState(BlockState.None);
        _listFieldBlock[38].SetState(BlockState.None);
        _listFieldBlock[39].SetState(BlockState.None);
        _listFieldBlock[40].SetState(BlockState.None);
        _listFieldBlock[41].SetState(BlockState.None);
        _listFieldBlock[42].SetState(BlockState.None);
        _listFieldBlock[43].SetState(BlockState.None);
        _listFieldBlock[44].SetState(BlockState.None);

        _listFieldBlock[45].SetState(BlockState.None);
        _listFieldBlock[46].SetState(BlockState.None);
        _listFieldBlock[47].SetState(BlockState.None);
        _listFieldBlock[48].SetState(BlockState.None);
        _listFieldBlock[49].SetState(BlockState.None);
        _listFieldBlock[50].SetState(BlockState.None);
        _listFieldBlock[51].SetState(BlockState.None);
        _listFieldBlock[52].SetState(BlockState.None);

        _listFieldBlock[54].SetState(BlockState.None);
        _listFieldBlock[55].SetState(BlockState.None);
        _listFieldBlock[56].SetState(BlockState.None);
        _listFieldBlock[57].SetState(BlockState.None);
        _listFieldBlock[58].SetState(BlockState.None);
        _listFieldBlock[59].SetState(BlockState.None);
        _listFieldBlock[60].SetState(BlockState.None);

        _listFieldBlock[63].SetState(BlockState.None);
        _listFieldBlock[64].SetState(BlockState.None);
        _listFieldBlock[65].SetState(BlockState.None);
        _listFieldBlock[66].SetState(BlockState.None);
        _listFieldBlock[67].SetState(BlockState.None);
        _listFieldBlock[68].SetState(BlockState.None);

        _listFieldBlock[72].SetState(BlockState.None);
        _listFieldBlock[73].SetState(BlockState.None);
        _listFieldBlock[74].SetState(BlockState.None);
        _listFieldBlock[75].SetState(BlockState.None);
        _listFieldBlock[76].SetState(BlockState.None);

        
    }
    #endregion

    #region Rhombus
    void InitRhombus() {

        _listFieldBlock[3].SetState(BlockState.None);
        _listFieldBlock[4].SetState(BlockState.None);
        _listFieldBlock[5].SetState(BlockState.None);

        _listFieldBlock[11].SetState(BlockState.None);
        _listFieldBlock[12].SetState(BlockState.None);
        _listFieldBlock[13].SetState(BlockState.None);
        _listFieldBlock[14].SetState(BlockState.None);
        _listFieldBlock[15].SetState(BlockState.None);

        _listFieldBlock[19].SetState(BlockState.None);
        _listFieldBlock[20].SetState(BlockState.None);
        _listFieldBlock[21].SetState(BlockState.None);
        _listFieldBlock[22].SetState(BlockState.None);
        _listFieldBlock[23].SetState(BlockState.None);
        _listFieldBlock[24].SetState(BlockState.None);
        _listFieldBlock[25].SetState(BlockState.None);

        _listFieldBlock[27].SetState(BlockState.None);
        _listFieldBlock[28].SetState(BlockState.None);
        _listFieldBlock[29].SetState(BlockState.None);
        _listFieldBlock[30].SetState(BlockState.None);
        _listFieldBlock[31].SetState(BlockState.None);
        _listFieldBlock[32].SetState(BlockState.None);
        _listFieldBlock[33].SetState(BlockState.None);
        _listFieldBlock[34].SetState(BlockState.None);
        _listFieldBlock[35].SetState(BlockState.None);

        _listFieldBlock[36].SetState(BlockState.None);
        _listFieldBlock[37].SetState(BlockState.None);
        _listFieldBlock[38].SetState(BlockState.None);
        _listFieldBlock[39].SetState(BlockState.None);
        _listFieldBlock[40].SetState(BlockState.None);
        _listFieldBlock[41].SetState(BlockState.None);
        _listFieldBlock[42].SetState(BlockState.None);
        _listFieldBlock[43].SetState(BlockState.None);
        _listFieldBlock[44].SetState(BlockState.None);

        _listFieldBlock[45].SetState(BlockState.None);
        _listFieldBlock[46].SetState(BlockState.None);
        _listFieldBlock[47].SetState(BlockState.None);
        _listFieldBlock[48].SetState(BlockState.None);
        _listFieldBlock[49].SetState(BlockState.None);
        _listFieldBlock[50].SetState(BlockState.None);
        _listFieldBlock[51].SetState(BlockState.None);
        _listFieldBlock[52].SetState(BlockState.None);
        _listFieldBlock[53].SetState(BlockState.None);

        _listFieldBlock[55].SetState(BlockState.None);
        _listFieldBlock[56].SetState(BlockState.None);
        _listFieldBlock[57].SetState(BlockState.None);
        _listFieldBlock[58].SetState(BlockState.None);
        _listFieldBlock[59].SetState(BlockState.None);
        _listFieldBlock[60].SetState(BlockState.None);
        _listFieldBlock[61].SetState(BlockState.None);

        _listFieldBlock[65].SetState(BlockState.None);
        _listFieldBlock[66].SetState(BlockState.None);
        _listFieldBlock[67].SetState(BlockState.None);
        _listFieldBlock[68].SetState(BlockState.None);
        _listFieldBlock[69].SetState(BlockState.None);

        _listFieldBlock[75].SetState(BlockState.None);
        _listFieldBlock[76].SetState(BlockState.None);
        _listFieldBlock[77].SetState(BlockState.None);

        
    }
    #endregion

    #region DividedType1
    void InitDividedType1() {
        _listFieldBlock[0].SetState(BlockState.None);
        _listFieldBlock[1].SetState(BlockState.None);
        _listFieldBlock[2].SetState(BlockState.None);
        _listFieldBlock[3].SetState(BlockState.None);
        _listFieldBlock[5].SetState(BlockState.None);
        _listFieldBlock[6].SetState(BlockState.None);
        _listFieldBlock[7].SetState(BlockState.None);
        _listFieldBlock[8].SetState(BlockState.None);

        _listFieldBlock[9].SetState(BlockState.None);
        _listFieldBlock[10].SetState(BlockState.None);
        _listFieldBlock[11].SetState(BlockState.None);
        _listFieldBlock[12].SetState(BlockState.None);
        _listFieldBlock[14].SetState(BlockState.None);
        _listFieldBlock[15].SetState(BlockState.None);
        _listFieldBlock[16].SetState(BlockState.None);
        _listFieldBlock[17].SetState(BlockState.None);

        _listFieldBlock[18].SetState(BlockState.None);
        _listFieldBlock[19].SetState(BlockState.None);
        _listFieldBlock[20].SetState(BlockState.None);
        _listFieldBlock[21].SetState(BlockState.None);
        _listFieldBlock[23].SetState(BlockState.None);
        _listFieldBlock[24].SetState(BlockState.None);
        _listFieldBlock[25].SetState(BlockState.None);
        _listFieldBlock[26].SetState(BlockState.None);

        _listFieldBlock[27].SetState(BlockState.None);
        _listFieldBlock[28].SetState(BlockState.None);
        _listFieldBlock[29].SetState(BlockState.None);
        _listFieldBlock[30].SetState(BlockState.None);
        _listFieldBlock[32].SetState(BlockState.None);
        _listFieldBlock[33].SetState(BlockState.None);
        _listFieldBlock[34].SetState(BlockState.None);
        _listFieldBlock[35].SetState(BlockState.None);

        _listFieldBlock[45].SetState(BlockState.None);
        _listFieldBlock[46].SetState(BlockState.None);
        _listFieldBlock[47].SetState(BlockState.None);
        _listFieldBlock[48].SetState(BlockState.None);
        _listFieldBlock[50].SetState(BlockState.None);
        _listFieldBlock[51].SetState(BlockState.None);
        _listFieldBlock[52].SetState(BlockState.None);
        _listFieldBlock[53].SetState(BlockState.None);

        _listFieldBlock[54].SetState(BlockState.None);
        _listFieldBlock[55].SetState(BlockState.None);
        _listFieldBlock[56].SetState(BlockState.None);
        _listFieldBlock[57].SetState(BlockState.None);
        _listFieldBlock[59].SetState(BlockState.None);
        _listFieldBlock[60].SetState(BlockState.None);
        _listFieldBlock[61].SetState(BlockState.None);
        _listFieldBlock[62].SetState(BlockState.None);

        _listFieldBlock[63].SetState(BlockState.None);
        _listFieldBlock[64].SetState(BlockState.None);
        _listFieldBlock[65].SetState(BlockState.None);
        _listFieldBlock[66].SetState(BlockState.None);
        _listFieldBlock[68].SetState(BlockState.None);
        _listFieldBlock[69].SetState(BlockState.None);
        _listFieldBlock[70].SetState(BlockState.None);
        _listFieldBlock[71].SetState(BlockState.None);

        _listFieldBlock[72].SetState(BlockState.None);
        _listFieldBlock[73].SetState(BlockState.None);
        _listFieldBlock[74].SetState(BlockState.None);
        _listFieldBlock[75].SetState(BlockState.None);
        _listFieldBlock[77].SetState(BlockState.None);
        _listFieldBlock[78].SetState(BlockState.None);
        _listFieldBlock[79].SetState(BlockState.None);
        _listFieldBlock[80].SetState(BlockState.None);

        
    }
    #endregion

    #region Pyramid
    void InitPyramid() {
        

        _listFieldBlock[22].SetState(BlockState.None);
        _listFieldBlock[30].SetState(BlockState.None);
        _listFieldBlock[31].SetState(BlockState.None);
        _listFieldBlock[32].SetState(BlockState.None);
        _listFieldBlock[38].SetState(BlockState.None);
        _listFieldBlock[39].SetState(BlockState.None);
        _listFieldBlock[40].SetState(BlockState.None);
        _listFieldBlock[41].SetState(BlockState.None);
        _listFieldBlock[42].SetState(BlockState.None);

        _listFieldBlock[46].SetState(BlockState.None);
        _listFieldBlock[47].SetState(BlockState.None);
        _listFieldBlock[48].SetState(BlockState.None);
        _listFieldBlock[49].SetState(BlockState.None);
        _listFieldBlock[50].SetState(BlockState.None);
        _listFieldBlock[51].SetState(BlockState.None);
        _listFieldBlock[52].SetState(BlockState.None);

        for(int i=54; i<81; i++) {
            _listFieldBlock[i].SetState(BlockState.None);
        }
    }

    #endregion

    #region Narrow
    void InitNarrow() {
        for(int i=0; i<9; i++) {
            for(int j=3;j<6;j++) {
                fieldBlocks[i, j].SetState(BlockState.None);
            }
        }
    }
    #endregion

    #region Big X
    void InitX() {
        

        for(int i=0; i<81; i++) {
            _listFieldBlock[i].SetState(BlockState.None);
        }

        _listFieldBlock[0].SetState(BlockState.Inactive);
        _listFieldBlock[10].SetState(BlockState.Inactive);
        _listFieldBlock[20].SetState(BlockState.Inactive);
        _listFieldBlock[30].SetState(BlockState.Inactive);
        _listFieldBlock[40].SetState(BlockState.Inactive);
        _listFieldBlock[50].SetState(BlockState.Inactive);
        _listFieldBlock[60].SetState(BlockState.Inactive);
        _listFieldBlock[70].SetState(BlockState.Inactive);
        _listFieldBlock[80].SetState(BlockState.Inactive);

        _listFieldBlock[8].SetState(BlockState.Inactive);
        _listFieldBlock[16].SetState(BlockState.Inactive);
        _listFieldBlock[24].SetState(BlockState.Inactive);
        _listFieldBlock[32].SetState(BlockState.Inactive);
        _listFieldBlock[48].SetState(BlockState.Inactive);
        _listFieldBlock[56].SetState(BlockState.Inactive);
        _listFieldBlock[64].SetState(BlockState.Inactive);
        _listFieldBlock[72].SetState(BlockState.Inactive);
    }

    #endregion

    #region Hourglass
    void InitHourglass() {
        

        _listFieldBlock[0].SetState(BlockState.None);
        _listFieldBlock[1].SetState(BlockState.None);
        _listFieldBlock[2].SetState(BlockState.None);
        _listFieldBlock[3].SetState(BlockState.None);
        _listFieldBlock[4].SetState(BlockState.None);
        _listFieldBlock[5].SetState(BlockState.None);
        _listFieldBlock[6].SetState(BlockState.None);
        _listFieldBlock[7].SetState(BlockState.None);
        _listFieldBlock[8].SetState(BlockState.None);

        _listFieldBlock[10].SetState(BlockState.None);
        _listFieldBlock[11].SetState(BlockState.None);
        _listFieldBlock[12].SetState(BlockState.None);
        _listFieldBlock[13].SetState(BlockState.None);
        _listFieldBlock[14].SetState(BlockState.None);
        _listFieldBlock[15].SetState(BlockState.None);
        _listFieldBlock[16].SetState(BlockState.None);

        _listFieldBlock[20].SetState(BlockState.None);
        _listFieldBlock[21].SetState(BlockState.None);
        _listFieldBlock[22].SetState(BlockState.None);
        _listFieldBlock[23].SetState(BlockState.None);
        _listFieldBlock[24].SetState(BlockState.None);

        _listFieldBlock[30].SetState(BlockState.None);
        _listFieldBlock[31].SetState(BlockState.None);
        _listFieldBlock[32].SetState(BlockState.None);

        _listFieldBlock[40].SetState(BlockState.None);

        _listFieldBlock[72].SetState(BlockState.None);
        _listFieldBlock[73].SetState(BlockState.None);
        _listFieldBlock[74].SetState(BlockState.None);
        _listFieldBlock[75].SetState(BlockState.None);
        _listFieldBlock[76].SetState(BlockState.None);
        _listFieldBlock[77].SetState(BlockState.None);
        _listFieldBlock[78].SetState(BlockState.None);
        _listFieldBlock[79].SetState(BlockState.None);
        _listFieldBlock[80].SetState(BlockState.None);

        _listFieldBlock[64].SetState(BlockState.None);
        _listFieldBlock[65].SetState(BlockState.None);
        _listFieldBlock[66].SetState(BlockState.None);
        _listFieldBlock[67].SetState(BlockState.None);
        _listFieldBlock[68].SetState(BlockState.None);
        _listFieldBlock[69].SetState(BlockState.None);
        _listFieldBlock[70].SetState(BlockState.None);

        _listFieldBlock[56].SetState(BlockState.None);
        _listFieldBlock[57].SetState(BlockState.None);
        _listFieldBlock[58].SetState(BlockState.None);
        _listFieldBlock[59].SetState(BlockState.None);
        _listFieldBlock[60].SetState(BlockState.None);

        _listFieldBlock[48].SetState(BlockState.None);
        _listFieldBlock[49].SetState(BlockState.None);
        _listFieldBlock[50].SetState(BlockState.None);
    }
    #endregion

    


    #region Custom Map
    void InitCustomMap() {
        int activeBlock = 0;
        JSONNode custompos = GameSystem.Instance.StageDetailJSON[GameSystem.Instance.PlayStage - 1]["tilepos"];

        for(int i=0; i<custompos.Count; i++) {
            _listFieldBlock[custompos[i].AsInt].SetState(BlockState.None);
            activeBlock++;
        }

    }
    #endregion



    /// <summary>
    /// 스테이지 스페셜 타일 체크 
    /// 스페셜 타일이 존재하면 필드에 구현 
    /// </summary>
    void CheckSpecialTile() {

        Debug.Log("★ CheckSpecialTile ★★★★★★");

        // 모든 타일을 쿠키로 변경 
        if (CheckCookieMission()) {
            SetCookieTile();
        }

        if (CheckStoneMission()) {
            SetStoneTile();
        } 

        if(CheckFishMission()) {
            SetFishTile();
        }

        if(CheckMoveMission()) {
            SetMovementTile();
        }
    }

    /// <summary>
    /// 이동 타일 세팅 
    /// </summary>
    void SetMovementTile() {
        Debug.Log("★★★★ SetMovementTile START");

        Transform movetile;
        JSONNode movepos = GameSystem.Instance.StageDetailJSON[GameSystem.Instance.PlayStage - 1]["movepos"];
        MoveDefenceType = GameSystem.Instance.StageDetailJSON[GameSystem.Instance.PlayStage - 1]["movedefence"].AsInt;


        _listMoveTiles.Clear();
        _listMoveTilesBlock.Clear();

        if (movepos.Count == 0 || movepos[0] == "N") {
            return;
        }

        // 대상 블록 자리에 생성
        for(int i =0; i<movepos.Count;i++) {
            // Spawn, Add
            movetile =  PoolManager.Pools["Blocks"].Spawn(PuzzleConstBox.prefabMoveTile, _listFieldBlock[movepos[i].AsInt].transform.position, Quaternion.identity);

            ListMoveTiles.Add(movetile.GetComponent<MoveTileCtrl>());
            ListMoveTilesBlock.Add(_listFieldBlock[movepos[i].AsInt]);

        }


        // 시작점과 종료지점을 체크 
        ListMoveTiles[0].SetStartSpot();
        ListMoveTiles[ListMoveTiles.Count - 1].SetLastSpot();
        
        // 시작, 종료 지점 Inactive 처리 
        ListMoveTilesBlock[0].SetState(BlockState.Inactive);
        _moveTileStartBlock = ListMoveTilesBlock[0]; // 클리어되었을때 사용한다.

        // Inactive 시키고, 제거 
        ListMoveTilesBlock.RemoveAt(0);



        SpawnMovingBlocks();

        SetMovingCatInStartPosition();
    }

    /// <summary>
    /// Fish Grill 세팅 
    /// </summary>
    void SetFishTile() {
        Debug.Log("★★★★ SetFishGrillTile START");

        JSONNode grillpos = GameSystem.Instance.StageDetailJSON[GameSystem.Instance.PlayStage - 1]["grillpos"];

        Debug.Log("★★★★ SetFishGrillTile grillpos :: " + grillpos.ToString());


        if (grillpos.Count == 0 || grillpos[0] == "N") {
            return;
        }


        InitFishGrillCount = grillpos.Count;
        TotalFishGrillCount = InitFishGrillCount;


        for (int i = 0; i < grillpos.Count; i++) {
            _listFieldBlock[grillpos[i].AsInt].SetFishGrill();
        }
    }

    /// <summary>
    ///바위 타일 세팅 
    /// </summary>
    void SetStoneTile() {

        Debug.Log("★★★★ SetStoneTile START");

        JSONNode rockpos = GameSystem.Instance.StageDetailJSON[GameSystem.Instance.PlayStage - 1]["rockpos"];

        Debug.Log("★★★★ SetStoneTile rockpos :: " + rockpos.ToString());


        if (rockpos.Count == 0 || rockpos[0] == "N") {
            return;
        }


        InitStoneBlockCount = rockpos.Count;


        for (int i=0; i<rockpos.Count;i++) {
            _listFieldBlock[rockpos[i].AsInt].SetState(BlockState.StrongStone);
        }
    }


    /// <summary>
    /// 모든 블록을 쿠키 타일화
    /// </summary>
    void SetCookieTile() {

        for (int i = 0; i < GameSystem.Instance.Height; i++) {
            for (int j = 0; j < GameSystem.Instance.Width; j++) {
                fieldBlocks[i, j].SetCookie();
            }
        }

    }


    /// <summary>
    /// 쿠키 미션 체크 
    /// </summary>
    /// <returns></returns>
    public bool CheckCookieMission() {

        if (GameSystem.Instance.StageDetailJSON[GameSystem.Instance.PlayStage - 1]["questid1"].AsInt == 12
            || GameSystem.Instance.StageDetailJSON[GameSystem.Instance.PlayStage - 1]["questid2"].AsInt == 12
            || GameSystem.Instance.StageDetailJSON[GameSystem.Instance.PlayStage - 1]["questid3"].AsInt == 12
            || GameSystem.Instance.StageDetailJSON[GameSystem.Instance.PlayStage - 1]["questid4"].AsInt == 12) {

            return true;

        }


        return false;


    }


    /// <summary>
    /// 생선굽기 미션 체크
    /// </summary>
    /// <returns></returns>
    public bool CheckFishMission() {

        if (GameSystem.Instance.StageDetailJSON[GameSystem.Instance.PlayStage - 1]["questid1"].AsInt == 22
            || GameSystem.Instance.StageDetailJSON[GameSystem.Instance.PlayStage - 1]["questid2"].AsInt == 22
            || GameSystem.Instance.StageDetailJSON[GameSystem.Instance.PlayStage - 1]["questid3"].AsInt == 22
            || GameSystem.Instance.StageDetailJSON[GameSystem.Instance.PlayStage - 1]["questid4"].AsInt == 22) {

            return true;

        }


        return false;
    }


    public bool CheckMoveMission() {
        if (GameSystem.Instance.StageDetailJSON[GameSystem.Instance.PlayStage - 1]["questid1"].AsInt == 23
            || GameSystem.Instance.StageDetailJSON[GameSystem.Instance.PlayStage - 1]["questid2"].AsInt == 23
            || GameSystem.Instance.StageDetailJSON[GameSystem.Instance.PlayStage - 1]["questid3"].AsInt == 23
            || GameSystem.Instance.StageDetailJSON[GameSystem.Instance.PlayStage - 1]["questid4"].AsInt == 23) {


            // 미션값 설정 
            if (GameSystem.Instance.StageDetailJSON[GameSystem.Instance.PlayStage - 1]["questid1"].AsInt == 23) {
                MoveMissionCount = GameSystem.Instance.StageDetailJSON[GameSystem.Instance.PlayStage - 1]["questvalue1"].AsInt;
            }
            else if (GameSystem.Instance.StageDetailJSON[GameSystem.Instance.PlayStage - 1]["questid2"].AsInt == 23) {
                MoveMissionCount = GameSystem.Instance.StageDetailJSON[GameSystem.Instance.PlayStage - 1]["questvalue2"].AsInt;
            }
            else if (GameSystem.Instance.StageDetailJSON[GameSystem.Instance.PlayStage - 1]["questid3"].AsInt == 23) {
                MoveMissionCount = GameSystem.Instance.StageDetailJSON[GameSystem.Instance.PlayStage - 1]["questvalue3"].AsInt;
            }
            else if (GameSystem.Instance.StageDetailJSON[GameSystem.Instance.PlayStage - 1]["questid4"].AsInt == 23) {
                MoveMissionCount = GameSystem.Instance.StageDetailJSON[GameSystem.Instance.PlayStage - 1]["questvalue4"].AsInt;
            }

            InitMoveMissionGoal = MoveMissionCount;

            return true;

        }
        return false;
    }



    /// <summary>
    /// 바위 미션 체크 
    /// </summary>
    /// <returns></returns>
    public bool CheckStoneMission() {

        if (GameSystem.Instance.StageDetailJSON[GameSystem.Instance.PlayStage - 1]["questid1"].AsInt == 15
            || GameSystem.Instance.StageDetailJSON[GameSystem.Instance.PlayStage - 1]["questid2"].AsInt == 15
            || GameSystem.Instance.StageDetailJSON[GameSystem.Instance.PlayStage - 1]["questid3"].AsInt == 15
            || GameSystem.Instance.StageDetailJSON[GameSystem.Instance.PlayStage - 1]["questid4"].AsInt == 15) {

            return true;

        }


        return false;
    }

    #endregion


    /// <summary>
    /// Respawns the stage block.
    /// </summary>
    IEnumerator RespawnStageBlock() {

        if (IsRespawningBoard)
            yield break;

        int emptyIndx = -1;
        IsRespawningBoard = true; // 중복 실행을 막는다. 

        yield return new WaitForSeconds(0.4f); // 각 블록의 효과 처리가 완료될때까지 기다려야한다. (0.2초)

        // 이동 미션의 경우 경로상의 블록은 ListEmptyBlock에서 제거 
        if(IsMoveMission) {
            for(int i=0; i<ListMoveTilesBlock.Count;i++) {
                listEmptyBlock.Remove(ListMoveTilesBlock[i]);
            }
        }

        SetFillBlockCount();

        // 모든 스테이지 블록이 None 상태 이어야 한다. 
        for (int i = 0; i < FillBlockCount; i++) {
            // listEmptyBlock에서 임의의 공간을 골라서 Idle 상태로 변경(listEmptyBlock은 Block Ctrl에서 제거
            emptyIndx = Random.Range(0, listEmptyBlock.Count);

            listEmptyBlock[emptyIndx].SetBlockType(Random.Range(0, GameSystem.Instance.BlockTypeCount)); // 랜덤 배치 
            listEmptyBlock[emptyIndx].SetState(BlockState.Idle);
        }

        IsRespawningBoard = false;

        // 첫 생성시점에 매칭 체크 추가 2017.03
        if (!CheckStageMatch()) {
            //Debug.Log("♠♠♠ No Match In this Stage!");
            SetBoardClearResult();
            RemoveAllIdleBlocks();
            StartCoroutine(RespawnStageBlock());
        }

    }

    /// <summary>
    /// 보드 클리어시 효과 및 결과 값 저장
    /// </summary>
    private void SetBoardClearResult() {

        // 남은 블록 여부에 따라서 BoardClear 값 증가 
        // 연출 처리 
        if(GetExistsIdleBlock()) {
            GameSystem.Instance.IngameBoardClearCount++;
            InUICtrl.Instance.ShowGreatText();
        }
        else {
            GameSystem.Instance.IngameBoardPerfectClearCount++;
            InUICtrl.Instance.ShowPerfectText();
        }
          

    }

    /// <summary>
    /// 피버 진입시 스테이지 블록 설정 
    /// </summary>
    private void SetFeverStageBlock() {

        // listEmptyBlock 의 수가 height * width - fillBlockCount 의 수로 맞춰야된다. 

        int emptyIndx = -1;
        int loopCount = listEmptyBlock.Count - (GameSystem.Instance.Height * GameSystem.Instance.Width - FillBlockCount);

        // loopCount 만큼 Idle 블록으로 처리 

        for (int i = 0; i < loopCount; i++) {
            // listEmptyBlock에서 임의의 공간을 골라서 Idle 상태로 변경(listEmptyBlock은 Block Ctrl에서 제거
            emptyIndx = Random.Range(0, listEmptyBlock.Count);

            listEmptyBlock[emptyIndx].SetBlockType(Random.Range(0, GameSystem.Instance.BlockTypeCount)); // 랜덤 배치 
            listEmptyBlock[emptyIndx].SetState(BlockState.Idle);
        }


    }

    #endregion



    /// <summary>
    /// 10초 경고 표시 시작 
    /// </summary>
    private void OnAlertStage() {
        InSoundManager.Instance.PlayAlertSound();

        _alertMark.SetActive(true);
        _alertText.DOScale(1.4f, 0.2f).SetLoops(-1, LoopType.Yoyo);

        /*
		spInGameTimerRingAlert.gameObject.SetActive (true);
		spInGameTimerRing.gameObject.SetActive (false);
		spInGameTimerFx.gameObject.SetActive (true);
        */
    }

    private void OffAlertStage() {
        _alertMark.SetActive(false);
        _alertText.transform.DOKill();
        _alertText.localScale = GameSystem.Instance.BaseScale;

        InSoundManager.Instance.StopAlertSound();
        /*
		spInGameTimerRingAlert.gameObject.SetActive (false);
		spInGameTimerRing.gameObject.SetActive (true);
		spInGameTimerFx.gameObject.SetActive (false);
        */
    }


    /// <summary>
    /// 피버가 한번 풀릴때마다 콤보시간이 감소된다. 
    /// </summary>
    public void MinusComboTime() {

        // 최소시간보다 적은 경우는 더이상 감소시키지 않는다.
        if (comboTime <= GameSystem.Instance.MinumumComboKeepTime)
            return;

        comboTime = comboTime - GameSystem.Instance.IntervalMinusComboTime;
    }

    /// <summary>
    /// 콤보시간 복원 
    /// </summary>
    public void RestoreComboTime() {
        comboTime = GameSystem.Instance.ComboKeepTime;
    }


    /// <summary>
    /// 게임 오버 후 처리 
    /// </summary>
    public void OnTimeOver() {
        ReadyResultScene(); // 점수 계산
        SetBingoQuestProgress(); // 빙고 진척도 처리 


        SetStageMissionResult(); // 스테이지 미션 처리 

        Debug.Log("★ GameSystem.Instance.Post2GameResult In InGameCtrl");

        // 서버 통신
        GameSystem.Instance.Post2GameResult();
    }

    /// <summary>
    /// 결과창으로 넘어가기전에 서버에게 보낼 점수 계산을 미리한다.
    /// </summary>
    public void ReadyResultScene() {


        GameSystem.Instance.IngameRemainCookie = GetRemainCookieBlocks();


        // 젬
        GameSystem.Instance.InGameDiamond = InUICtrl.Instance.gotGem;

        // 코인 
        GameSystem.Instance.InGameCoin = InUICtrl.Instance.GotCoin;

        // 코인 네코 패시브 적용
        GameSystem.Instance.InGameTotalCoin = GameSystem.Instance.InGameCoin + (int)(GameSystem.Instance.InGameCoin * GameSystem.Instance.NekoCoinPercent / 100);

        // 스코어
        GameSystem.Instance.InGameScore = InUICtrl.Instance.GameScore;

        // 스코어 네코 패시브 적용
        GameSystem.Instance.InGamePlusScore = Mathf.RoundToInt(InUICtrl.Instance.GameScore * GameSystem.Instance.NekoScorePercent / 100);

        // 총 데미지 
        GameSystem.Instance.InGameDamage = (int)GameSystem.Instance.FloatInGameDamage;


        // 인게임 토탈 점수를 계산
        // (스코어 + 네코 스코어 보너스 패시브 + MaxCombo * 1000)
        GameSystem.Instance.InGameTotalScore = GameSystem.Instance.InGameScore + GameSystem.Instance.InGamePlusScore;


        // 네코 뱃지 보너스 적용 
        GameSystem.Instance.InGameBadgeBonusScore = Mathf.RoundToInt(GameSystem.Instance.InGameTotalScore * GameSystem.Instance.UserNekoBadgeBonus / 100); // 100분율 계산 
        GameSystem.Instance.InGameTotalScore = GameSystem.Instance.InGameTotalScore + GameSystem.Instance.InGameBadgeBonusScore;

        // 최대 콤보 보너스 추가
        GameSystem.Instance.InGameTotalScore = GameSystem.Instance.InGameTotalScore + (GameSystem.Instance.InGameMaxCombo * 1000);


        Debug.Log("★★★ ReadyResultScene Final Score :: " + GameSystem.Instance.InGameTotalScore);
    }


    #region 스테이지 미션 클리어 
    /// <summary>
    /// 스테이지 결과 
    /// </summary>
    public void SetStageMissionResult() {

        int clearMissionCount = 0; // 클리어 미션의 개수.
        
        JSONNode currentStage = GameSystem.Instance.StageDetailJSON[GameSystem.Instance.PlayStage - 1]; // 스테이지 기준정보 
        JSONNode currentUserStage = GameSystem.Instance.UserStageJSON["stagelist"][GameSystem.Instance.PlayStage - 1]; // 사용자 스테이지 정보 

        // 사용자 스테이지정보 복사.
        // UserStageJSON 에는 직접적으로 데이터를 쓰지 않는다. 
        string clone = currentUserStage.ToString();
        GameSystem.Instance.CloneUserStageJSON = JSON.Parse(clone);

        

        GameSystem.Instance.InGameStageClear = true;
        
        // 다이아 레벨(4)까지 클리어 했으면 종료 
        if (currentUserStage["state"].AsInt >= 4)
            return;

        // 4가지 미션에 대한 진척도 체크 및 처리 

        SetStageMissionProgress(1, currentStage["questid1"].AsInt, GameSystem.Instance.CloneUserStageJSON, currentStage);
        SetStageMissionProgress(2, currentStage["questid2"].AsInt, GameSystem.Instance.CloneUserStageJSON, currentStage);
        SetStageMissionProgress(3, currentStage["questid3"].AsInt, GameSystem.Instance.CloneUserStageJSON, currentStage);

        // 4번째 미션은 다이아 레벨 플레이때만 처리 
        if(GameSystem.Instance.IngameDiamondPlay)
            SetStageMissionProgress(4, currentStage["questid4"].AsInt, GameSystem.Instance.CloneUserStageJSON, currentStage);


        // 결과 체크 및 서버 통신 변수 세팅  1~4번 미션 순차 체크
        // 클리어한 미션의 개수를 합산한다. 
        if (currentStage["questid1"].AsInt > 0 && currentStage["questvalue1"].AsInt <= GameSystem.Instance.CloneUserStageJSON["progress1"].AsInt) 
            clearMissionCount++;

        if (currentStage["questid2"].AsInt > 0 && currentStage["questvalue2"].AsInt <= GameSystem.Instance.CloneUserStageJSON["progress2"].AsInt)
            clearMissionCount++;

        if (currentStage["questid3"].AsInt > 0 && currentStage["questvalue3"].AsInt <= GameSystem.Instance.CloneUserStageJSON["progress3"].AsInt)
            clearMissionCount++;

        if (currentStage["questid4"].AsInt > 0 && currentStage["questvalue4"].AsInt <= GameSystem.Instance.CloneUserStageJSON["progress4"].AsInt)
            clearMissionCount++;


        if (clearMissionCount == 0) {
            GameSystem.Instance.InGameStageClear = false;
            GameSystem.Instance.CloneUserStageJSON["state"].AsInt = 0;
        }
        else {

            GameSystem.Instance.CloneUserStageJSON["state"].AsInt = clearMissionCount;

            // 더 높은 등급을 달성했을때만 udpate. 
            //if (GameSystem.Instance.CloneUserStageJSON["state"].AsInt < clearMissionCount)
                
        }

    }

    /// <summary>
    /// 각 스테이지 미션에 대한 달성 처리 
    /// </summary>
    /// <param name="pQuestIndex"></param>
    /// <param name="pQuestID"></param>
    /// <param name="pUserStage">복사된 사용자 스테이지 정보 </param>
    /// <param name="pCurrentStage">스테이지 기준 정보</param>
    private void SetStageMissionProgress(int pQuestIndex, int pQuestID, JSONNode pUserStage, JSONNode pCurrentStage) {

        if (pQuestID <= 0)
            return;

        int progress = 0; // 이번 게임에서의 달성 진척도 

        // 미션에 따른 처리 시작 
        switch (pQuestID) {
            case 1: // 스테이지를 도전해 보세요!
                progress = 1;
                break;

            case 2: // 목표점수 달성
                progress = GameSystem.Instance.InGameTotalScore;
                break;

            case 3: // 최대 콤보 
                progress = GameSystem.Instance.InGameMaxCombo;
                break;

            case 4: // 블록 깨뜨리기 
                progress = GameSystem.Instance.IngameBlockCount;
                break;

            case 5: // 스페셜 어택 
                progress = GameSystem.Instance.IngameSpecialAttackCount;
                break;

            case 6: // 폭탄 블록 
                progress = GameSystem.Instance.IngameBombCount;
                break;

            case 7: // 폭탄 블록 
                progress = GameSystem.Instance.IngameBombCount;
                break;

            case 8: // 코인 획득
                progress = GameSystem.Instance.InGameTotalCoin;
                break;

            case 9: // 생선 획득 
                progress = GameSystem.Instance.InGameChub;
                break;

            case 10: // 고양이 구출하기
                progress = GameSystem.Instance.InGameRescueNeko;
                break;

            case 11: // 보스를 이겨라 
                progress = GameSystem.Instance.InGameDestroyUFO;
                break;

            case 12: // 모든 쿠키를 제거하라

                if (GameSystem.Instance.IngameRemainCookie == 0)
                    progress = 100;
                else
                    progress = ValidBlockSpaceCount - GameSystem.Instance.IngameRemainCookie;

                // questvalue 강제로 수정 
                pCurrentStage["questvalue" + pQuestIndex.ToString()].AsInt = ValidBlockSpaceCount;

                break;

            case 13: // 부스트 아이템을 사용하라 
                if (GameSystem.Instance.InGameBoostItemUSE)
                    progress = 1;
                else
                    progress = 0;
                break;

            case 14: // 블록을 [n]번 완전 제거 하세요 
                progress = GameSystem.Instance.IngameBoardPerfectClearCount;
                break;

            case 15: // 모든 바위를 제거하세요
                progress = InitStoneBlockCount - GetStoneBlockCount();
                break;

            case 16: // 블록 3개 깨뜨리기 
                progress = GameSystem.Instance.IngameMatchThreeCount;
                
                break;

            case 17: // 블록 4개 깨뜨리기 
                
                progress = GameSystem.Instance.IngameMatchFourCount;
                break;

            case 18: // Miss를 n번 미만으로 하기 
                
                //progress = GameSystem.Instance.IngameMissCount;

                if (GameSystem.Instance.IngameMissCount < pCurrentStage["questvalue" + pQuestIndex.ToString()].AsInt)
                    progress = 100;
                else
                    progress = 0;

                break;

            case 19: // 블록 클리어 달성
                progress = GameSystem.Instance.IngameBoardClearCount;
                break;

            case 20: // n초 이내에 고양이 구출 
                if (PlaytimeFromZero > pCurrentStage["questvalue" + pQuestIndex.ToString()].AsInt) // 시간 초과 
                    progress = 0;
                else
                    progress = 100;

                // 실제로 고양이를 구출하지 않았으면 0으로 
                if (GameSystem.Instance.InGameRescueNeko <= 0)
                    progress = 0;
                
                break;

            case 21: // n초 이내에 나쁜 고양이 격퇴
                if (PlaytimeFromZero > pCurrentStage["questvalue" + pQuestIndex.ToString()].AsInt) // 시간 초과 
                    progress = 0;
                else
                    progress = 100;

                // 실제로 보스를 격퇴하지 않았으면 0
                if (GameSystem.Instance.InGameDestroyUFO <= 0)
                    progress = 0;

                break;


            case 22: // 생선 튀김 

                progress = InitFishGrillCount - TotalFishGrillCount;

                if (TotalFishGrillCount == 0)
                    progress = 100;

                break;


            case 23: // 이동 미션 
                progress = InitMoveMissionGoal - MoveMissionCount;

                break;



        }

        // 오버라이드 여부에 따른 최종 값 처리 
        pUserStage["progress" + pQuestIndex.ToString()].AsInt = progress;
    }

    #endregion 



    /// <summary>
    /// 빙고 진행상황 저장 
    /// </summary>
    public void SetBingoQuestProgress() {


        // 스테이지 13까지는 Lock
        if (GameSystem.Instance.UserCurrentStage < 14)
            return;

        int currentbingo = GameSystem.Instance.UserJSON["currentbingoid"].AsInt;

        Debug.Log("▶ InGameCtrl SetBingoQuestProgress :: " + currentbingo);

        GameSystem.Instance.ListClearedBingoCols.Clear();
        GameSystem.Instance.ListClearedBingoLines.Clear();


        #region BingoID 1 AFRICOT

        if (currentbingo == 1) {

            GameSystem.Instance.SetBingoQuestProgress(23, GameSystem.Instance.IngameMatchFourCount, 0);
            GameSystem.Instance.SetBingoQuestProgress(22, GameSystem.Instance.IngameMatchThreeCount, 0);

            GameSystem.Instance.SetBingoQuestProgress(11, GameSystem.Instance.MatchedRedBlock, 0, true);
            GameSystem.Instance.SetBingoQuestProgress(20, GameSystem.Instance.InGameMaxCombo, 0, true);
            GameSystem.Instance.SetBingoQuestProgress(15, GameSystem.Instance.IngameBombCount, 0);
            GameSystem.Instance.SetBingoQuestProgress(10, GameSystem.Instance.MatchedYellowBlock, 0, true);
            GameSystem.Instance.SetBingoQuestProgress(4, GameSystem.Instance.InGameTotalCoin, 0);
            GameSystem.Instance.SetBingoQuestProgress(12, GameSystem.Instance.MatchedBlueBlock, 0, true);
            GameSystem.Instance.SetBingoQuestProgress(17, GameSystem.Instance.IngameBlueBombCount, 0, true);
            GameSystem.Instance.SetBingoQuestProgress(5, GameSystem.Instance.InGameTotalCoin, 0, true);
            GameSystem.Instance.SetBingoQuestProgress(3, GameSystem.Instance.InGameTotalScore, 0, true);


            if (GameSystem.Instance.InGameBoostItemUSE) {
                GameSystem.Instance.SetBingoQuestProgress(21, 1, 0);
            }



            GameSystem.Instance.SetBingoQuestProgress(1, 1, 0);

            if (GameSystem.Instance.EquipGroup1Neko)
                GameSystem.Instance.SetBingoQuestProgress(28, 1, 1);

            if (GameSystem.Instance.EquipGroup8Neko)
                GameSystem.Instance.SetBingoQuestProgress(2, GameSystem.Instance.InGameTotalScore, 8);

            GameSystem.Instance.SetBingoQuestProgress(14, GameSystem.Instance.IngameBlockCount, 0);

            if (GameSystem.Instance.EquipGroup3Neko) {
                GameSystem.Instance.SetBingoQuestProgress(15, GameSystem.Instance.IngameBombCount, 3);
                GameSystem.Instance.SetBingoQuestProgress(19, GameSystem.Instance.InGameTotalCombo, 3);
            }

            GameSystem.Instance.SetBingoQuestProgress(26, GameSystem.Instance.IngameSpecialAttackCount, 0);

        }
        #endregion


    }


    /// <summary>
    /// 로비로 돌아가기 
    /// </summary>
	public void BackToMenu() {
        if (_isDoingBackToMenu)
            return;

        _isDoingBackToMenu = true;
        GameSystem.Instance.LoadLobbyScene();
    }


    public void ConfirmBackToMenu() {
        ConfirmExit.SetActive(true);
    }


    /// <summary>
    /// 스테이지 미션 체크
    /// </summary>
    private void CheckStageMission() {
        InUICtrl.Instance.ShowStageMission();
    }

    /// <summary>
    /// 아이템 체크 
    /// </summary>
    private void CheckEquipItem() {

        _boostPlayTime = false;
        _boostBombCreate = false;
        _boostSpecialAttack = false;
        _boostFirework = false;
          

        GameSystem.Instance.ListPreEquipItemID.Clear();

        for (int i = 0; i < GameSystem.Instance.ListEquipItemID.Count; i++) {
            if (GameSystem.Instance.ListEquipItemID[i] == 0) {
                _boostPlayTime = true;
                _equipItemCount++;
                GameSystem.Instance.ListPreEquipItemID.Add(0);



            } else if (GameSystem.Instance.ListEquipItemID[i] == 1) {
                _boostBombCreate = true;
                _equipItemCount++;
                GameSystem.Instance.ListPreEquipItemID.Add(1);
            } else if (GameSystem.Instance.ListEquipItemID[i] == 2) {
                _boostSpecialAttack = true;
                _equipItemCount++;
                GameSystem.Instance.ListPreEquipItemID.Add(2);
            } else if (GameSystem.Instance.ListEquipItemID[i] == 3) {
                _boostFirework = true;
                _equipItemCount++;
                GameSystem.Instance.ListPreEquipItemID.Add(3);
            }
        }

        if (_equipItemCount > 0) {
            GameSystem.Instance.InGameBoostItemUSE = true;
        }

        InUICtrl.Instance.ShowEquipItem();
        InUICtrl.Instance.InitEquipItem();





    }

    #region 장착된 네코의 스킬 정보 처리 

    /// <summary>
    /// 장착된 네코의 스킬 정보 처리 (패시브 )
    /// </summary>
    private void CheckNekoPassivePlus() {


        for (int i = 0; i < GameSystem.Instance.ListEquipNekoID.Count; i++) {
            _listNekoPassive[i].SetNekoPassive(GameSystem.Instance.ListEquipNekoID[i]);
        }

        // 스킬 합산 처리 
        for (int i = 0; i < _listNekoPassive.Count; i++) {

            if (_listNekoPassive[i].IsScorePlus) { // 스코어 상승 
                GameSystem.Instance.NekoScorePercent += _listNekoPassive[i].ScorePlus;
                PassiveSkillCount++;
            }

            if (_listNekoPassive[i].IsCoinPlus) { // 획득 코인 상승 
                GameSystem.Instance.NekoCoinPercent += _listNekoPassive[i].CoinPlus;
                PassiveSkillCount++;
            }

            if (_listNekoPassive[i].IsGameTimePlus) { // 게임 플레이 시간 상승 
                GameSystem.Instance.NekoGameTimePlus += _listNekoPassive[i].GameTimePlus;
                PassiveSkillCount++;
            }

            if (_listNekoPassive[i].IsPowerPlus) { // 파워 상승 
                GameSystem.Instance.NekoPowerPlus += _listNekoPassive[i].PowerPlus;
                PassiveSkillCount++;
            }


            if(_listNekoPassive[i].IsStartBomb) { // 시작 폭탄
                GameSystem.Instance.NekoStartBombCount += _listNekoPassive[i].StartBombCount;
                PassiveSkillCount++;
            }


            // 폭탄 아이템 게이지 상승(합산 처리 X)
            if (_listNekoPassive[i].IsBombAppearBonus) {
                GameSystem.Instance.NekoBombAppearBonus += _listNekoPassive[i].BombAppearPlus;
                PassiveSkillCount++;
            }

            // 네코 스킬 게이지 획득 상승(합산 처리 X)
            if (_listNekoPassive[i].IsSkillInvokeBonus) {
                GameSystem.Instance.NekoSkillInvokeBonus += _listNekoPassive[i].NekoSkillInvokePlus;
                PassiveSkillCount++;
            }

        }

        // 폭탄 게이지, 네코 스킬 게이지 최종 처리
        BombAppearBlockCount -= GameSystem.Instance.NekoBombAppearBonus;
        SkillInvokeBlockCount -= GameSystem.Instance.NekoSkillInvokeBonus;

        if (BoostBombCreate) 
            BombAppearBlockCount -= 5;

        if (BoostSpecialAttack)
            SkillInvokeBlockCount -= 5;

        UpdateGameTime();



    }

    #endregion

    #region Passive Bomb Drop

    /// <summary>
    /// 폭탄 드롭 개수 공식에 따른 Return
    /// </summary>
    /// <param name="pCount"></param>
    /// <returns></returns>
    private int GetBombDropCount(int pCount) {
        int bombCount = 0;
        int rollDice = 0;


        // 폭탄 max 갯수에 따른 확률 계산 
        if (pCount == 1) {
            bombCount = 1;

        }
        else {
            // 확률 계산 처리 
            rollDice = Random.Range(0, 100);

            if (pCount == 2) {
                if (rollDice < 90)
                    bombCount = 1;
                else
                    bombCount = 2;
            }
            else if (pCount == 3) {

                if (rollDice < 80)
                    bombCount = 1;
                else if (rollDice >= 80 && rollDice < 95)
                    bombCount = 2;
                else
                    bombCount = 3;

            }
            else {

                if (rollDice < 70)
                    bombCount = 1;
                else if (rollDice >= 70 && rollDice < 85)
                    bombCount = 2;
                else if (rollDice >= 85 && rollDice < 95)
                    bombCount = 3;
                else
                    bombCount = 4;

            }
        }

        return bombCount;

    }


    /// <summary>
    /// 지정된 개수만큼 폭탄블록을 생성한다
    /// </summary>
    /// <param name="pCount">P count.</param>
    public void DropPassiveBomb(int pCount, BombType pType) {

        int bombCount = 0; // 폭탄의 개수 
        int bombID = -1; // 폭탄의 종류 

        if (pType == BombType.Random) {

            bombID = Random.Range(0, 100);

            // 별폭탄 등장 확률 하향
            if (bombID < 10)
                bombID = 0;
            else {
                bombID = Random.Range(1, PuzzleConstBox.listItemBlockSprite.Count);
            }
           
        }
        else if (pType == BombType.Black)
            bombID = 0;
        else if (pType == BombType.Blue)
            bombID = 1;
        else if (pType == BombType.Yellow)
            bombID = 2;
        else if (pType == BombType.Red)
            bombID = 3;

        // 폭탄의 개수를 구함. 
        bombCount = GetBombDropCount(pCount);

        // 사운드 플레이 
        InSoundManager.Instance.PlayBombDrop();

        for (int i = 0; i < bombCount; i++) {
            pickPassiveBombBlock = GetEmptyBlock();
            pickPassiveBombBlock.SetItemBlock(bombID);
        }
    }


    /// <summary>
    /// 개수만큼 랜덤 폭탄 블록 드롭 
    /// </summary>
    /// <param name="pCount"></param>
    private void DropRandomBomb(int pCount) {
        for (int i = 0; i < pCount; i++) {
            pickPassiveBombBlock = GetEmptyBlock();
            pickPassiveBombBlock.SetRandomBombBlock();
        }
    }


    #endregion

    #region 미입력시간 체크, 네비게이터 사용 

    /// <summary>
    /// 미입력 시간 체크 
    /// </summary>
    private void CheckNoInputTime() {

        // 이미 네비게이터가 실행중이라면  
        if (IsOnNavigator)
            return;

        // 미입력시간 5초 
        if (InputCtrl.Instance.NoInputTime < _naviTime)
            return;


        // 네비게이터 쇼 
        OnNavigator();


    }

    /// <summary>
    /// 네비게이터 활성화 
    /// </summary>
    private void OnNavigator() {

        GetRandomMatchBlock();

        if (_listNaviTarget.Count == 0) {
            Debug.Log("★_listNaviTarget.Count : 0 ");
            InputCtrl.Instance.NoInputTime = 0; // 미입력 시간 초기화 
            return;
        }

        IsOnNavigator = true;

        //_spawnedNavigator = PoolManager.Pools[PuzzleConstBox.objectPool].Spawn(PuzzleConstBox.prefabBlockNavigator, _tempBlockCtrl.transform.position, Quaternion.identity);

        for (int i = 0; i < _listNaviTarget.Count; i++) {
            _listNaviTarget[i].SetNaviTarget();
        }

        for (int i = 0; i < _listNaviTouchPos.Count; i++) {
            _listNaviTouchPos[i].OnNaviTile();
        }

    }


    /// <summary>
    /// 네비게이터 비활성화
    /// </summary>
    private void OffNavigator() {

        if (!IsOnNavigator)
            return;


        for (int i = 0; i < _listNaviTarget.Count; i++) {
            _listNaviTarget[i].OffNavigator();
        }

        for (int i = 0; i < _listNaviTouchPos.Count; i++) {
            _listNaviTouchPos[i].OffNaviTile();
        }


        //PoolManager.Pools[PuzzleConstBox.objectPool].Despawn(_spawnedNavigator);

        IsOnNavigator = false;
    }

    #endregion


    /// <summary>
    /// 네코 뱃지 보너스 조회 
    /// </summary>
    private void GetUserNekoBadgesBonus() {
        GameSystem.Instance.UserNekoBadgeBonus = 0;


        JSONNode badgeinfo = GameSystem.Instance.GetNekoMedal();


        // 보너스 계산
        GameSystem.Instance.UserNekoBadgeBonus += (badgeinfo["bronze"].AsInt * 0.2f);
        GameSystem.Instance.UserNekoBadgeBonus += (badgeinfo["silver"].AsInt * 0.5f);
        GameSystem.Instance.UserNekoBadgeBonus += (badgeinfo["gold"].AsInt * 1.0f);


    }




    public void OnWaitingRequest() {
        objWaitingRequest.SetActive(true);
    }

    public void OffWaitingRequest() {
        objWaitingRequest.SetActive(false);
    }


    public void StopPlaying() {
        isPlaying = false;
    }


    #region 특수 스테이지 체크 (미션창이 뜨기전에 체크해야될 필요가 있을때 사용)

    public bool GetBossStage() {
        SimpleJSON.JSONNode stagenode = GameSystem.Instance.StageDetailJSON[GameSystem.Instance.PlayStage - 1];
        int theme = stagenode["theme"].AsInt;

        if (stagenode["questid1"].AsInt == 11 || stagenode["questid2"].AsInt == 11 || stagenode["questid3"].AsInt == 11 || stagenode["questid4"].AsInt == 11)
            return true;
        else
            return false;
    }

    #endregion


    #region 폭죽 터뜨리기 

    public void DoFirework() {
        if (IsFireworking)
            return;

        StartCoroutine(Fireworking());

    }

    IEnumerator Fireworking() {

        //int randomNumber = 0;
        IsFireworking = true;

        for (int i = 0; i < 4; i++) {
            //randomNumber = Random.Range(0, 2);


            PoolManager.Pools[PuzzleConstBox.objectPool].Spawn(_firework10, new Vector3(Random.Range(-6.0f, 6.0f), Random.Range(4.0f, 6.0f), 0), Quaternion.identity);
            /*
            if (randomNumber % 2 == 0)
                PoolManager.Pools[PuzzleConstBox.objectPool].Spawn(_firework9, new Vector3(Random.Range(-6.0f, 6.0f), Random.Range(4.0f, 6.0f), 0), Quaternion.identity);
            else
                PoolManager.Pools[PuzzleConstBox.objectPool].Spawn(_firework10, new Vector3(Random.Range(-6.0f, 6.0f), Random.Range(4.0f, 6.0f), 0), Quaternion.identity);
            */

            yield return new WaitForSeconds(0.2f);
        }

        IsFireworking = false;

    }

    #endregion

    #region Properties 

    public bool IsPlaying {
		get {
			return this.isPlaying;
		}
	}

	public List<MyNekoPassiveCtrl> ListNekoPassive {
		get {
			return this._listNekoPassive;
		}
	}

	public bool IsBonusTime {
		get {
			return this.isBonusTime;
		}
	}

    public int PassiveSkillCount {
        get {
            return _passiveSkillCount;
        }

        set {
            _passiveSkillCount = value;
        }
    }

    public bool IsShowingSkillAndIcon {
        get {
            return _isShowingSkillAndIcon;
        }

        set {
            _isShowingSkillAndIcon = value;
        }
    }

    public GameObject ConfirmExit {
        get {
            return _confirmExit;
        }

        set {
            _confirmExit = value;
        }
    }

    public int NekoTotalPower {
        get {
            return _nekoTotalPower;
        }

        set {
            _nekoTotalPower = value;
        }
    }



    public bool IsWaitingExplain {
        get {
            return _isWaitingExplain;
        }

        set {
            _isWaitingExplain = value;
        }
    }

    public bool IsCookieMission {
        get {
            return _isCookieMission;
        }

        set {
            _isCookieMission = value;
        }
    }

    public bool IsBossStage {
        get {
            return _isBossStage;
        }

        set {
            _isBossStage = value;
        }
    }

    public int SkillInvokeBlockCount {
        get {
            return _skillInvokeBlockCount;
        }

        set {
            _skillInvokeBlockCount = value;
        }
    }

    public int BombAppearBlockCount {
        get {
            return _bombAppearBlockCount;
        }

        set {
            _bombAppearBlockCount = value;
        }
    }

    public bool BoostPlayTime {
        get {
            return _boostPlayTime;
        }

        set {
            _boostPlayTime = value;
        }
    }

    public bool BoostBombCreate {
        get {
            return _boostBombCreate;
        }

        set {
            _boostBombCreate = value;
        }
    }

    public bool BoostSpecialAttack {
        get {
            return _boostSpecialAttack;
        }

        set {
            _boostSpecialAttack = value;
        }
    }

    public bool IsFishMission {
        get {
            return _isFishMission;
        }

        set {
            _isFishMission = value;
        }
    }

    public bool IsStoneMission {
        get {
            return _isStoneMission;
        }

        set {
            _isStoneMission = value;
        }
    }

    public bool IsRescueStage {
        get {
            return _isRescueStage;
        }

        set {
            _isRescueStage = value;
        }
    }

    public bool IsMoveMission {
        get {
            return _isMoveMission;
        }

        set {
            _isMoveMission = value;
        }
    }

    public List<MoveTileCtrl> ListMoveTiles {
        get {
            return _listMoveTiles;
        }

        set {
            _listMoveTiles = value;
        }
    }

    public List<BlockCtrl> ListMoveTilesBlock {
        get {
            return _listMoveTilesBlock;
        }

        set {
            _listMoveTilesBlock = value;
        }
    }

    public int MoveMissionCount {
        get {
            return _moveMissionCount;
        }

        set {
            _moveMissionCount = value;
        }
    }

    public bool IsProcessingMove {
        get {
            return _isProcessingMove;
        }

        set {
            _isProcessingMove = value;
        }
    }

    public int MoveDefenceType {
        get {
            return _moveDefenceType;
        }

        set {
            _moveDefenceType = value;
        }
    }

    public int InitMoveMissionGoal {
        get {
            return _initMoveMissionGoal;
        }

        set {
            _initMoveMissionGoal = value;
        }
    }

    public bool IsRespawningBoard {
        get {
            return _isRespawningBoard;
        }

        set {
            _isRespawningBoard = value;
        }
    }

    public bool IsFireworking {
        get {
            return _isFireworking;
        }

        set {
            _isFireworking = value;
        }
    }

    public int FillBlockCount {
        get {
            return _fillBlockCount;
        }

        set {
            _fillBlockCount = value;
        }
    }

    public int ValidBlockSpaceCount {
        get {
            return _validBlockSpaceCount;
        }

        set {
            _validBlockSpaceCount = value;
            GameSystem.Instance.ResultValidBlockSpaceCount = value;
        }
    }

    public bool BoostFirework {
        get {
            return _boostFirework;
        }

        set {
            _boostFirework = value;
        }
    }



    #endregion
}
