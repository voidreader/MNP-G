using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using PathologicalGames;
using DG.Tweening;
using CodeStage.AntiCheat.ObscuredTypes;

public class InUICtrl : MonoBehaviour {

    static InUICtrl _instance = null;



    private bool _isActivePause = false; //  Pause 팝업 활성화 여부 
    private bool _playerNekoBarLock = false; // 플레이어 네코 스킬 게이지 Lock 여부 

    [SerializeField] private Camera uiCamera;
    [SerializeField] private Camera mainCamera;


    [SerializeField] NekoAppearCtrl[] arrPlayerNekoAppear;
    int playerNekoAppearIndex = 0;


    private int _matchCount;
    private bool _isPossiblePause = false; // pause를 할 수 있음 .

    // UI
    [SerializeField] InGameStageInfoCtrl _stageMissionInfo;


    public GameObject pausePopup = null; // pause 팝업창 
    [SerializeField] GameObject spReady = null; // Ready 이미지 
    [SerializeField] GameObject spGo = null; // Go 이미지 

    // TimeOver
    [SerializeField] private UISprite spGameOver = null;
    [SerializeField] private GameObject timeOverObj = null;
    [SerializeField] GameObject bonusTimeObj;
    [SerializeField] GameObject spClear = null;
    [SerializeField] GameObject spFail = null;



    // PlayerNeko (MyNeko) 게이지 
    [SerializeField] SkillBarCtrl[] _arrSkillBars; // Editor에서 세팅 



    // 코인 처리
    [SerializeField] UISpriteAnimation coinSprite;
    [SerializeField] UILabel lblCoin = null;
    [SerializeField] ObscuredInt inGameGotCoin = 0; // 게임에서 획득한 코인 
    [SerializeField] UISpriteAnimation coinHot; // 코인 핫타임

    // 일반 배경
    [SerializeField] GameObject normalGroup = null;
    [SerializeField] tk2dSprite _bgTop; // 상단 배경 (Theme마다 변경된다) 




    // 콤보
    [SerializeField] Transform comboUI; // UI Group
    [SerializeField] UILabel lblCombo; // 레이블 
    public ObscuredInt comboCnt = 0; // 콤보 카운트


    // Score & Damage
    float _comboBonusScore = 0.01f; // 콤보 보너스 스코어
    [SerializeField] UILabel lblScore = null;

    [SerializeField] UILabel _lblBlockCount;



    #region 스코어 관련 
    [SerializeField] List<YellowLightCtrl> _listScoreLight;
    [SerializeField] private ObscuredInt calcScore = 0;
    private ObscuredInt comboAddValue = 0;

    private ObscuredInt gameScore = 0;
    public ObscuredInt gotGem = 0;

    int _blueBlockCount = 0;
    int _yellowBlockCount = 0;
    int _redBlockCount = 0;

    #endregion

    // 아이템 관련 변수 
    ObscuredFloat itemBarCnt = 0;
    ObscuredFloat itemBarValue = 0;

    int currentItemIndx = 0;
    [SerializeField] UIProgressBar itemBar = null;
    [SerializeField] UISprite spItemHead = null; // 아이템 헤더 

    [SerializeField] UIProgressBar _smallItemBar;
    [SerializeField] UIProgressBar _bigItemBar;

    [SerializeField] UISprite _bigItemHead;
    [SerializeField] UISprite _smallItemHead;

    private Vector3 originHeadPos = Vector3.zero;
    private BlockCtrl pickItemBlock = null;
    private Vector3 pickItemBlockPos = Vector3.zero;

    #region 미션 타일 관련 변수 

    // 미션 타일 
    [SerializeField] UISprite _missionTileSprite1;
    [SerializeField] UILabel _missionTileValue1;

    [SerializeField] UISprite _missionTileSprite2;
    [SerializeField] UILabel _missionTileValue2;

    [SerializeField] UILabel _cookieMissionValue;
    [SerializeField] UILabel _stoneMissionValue;
    [SerializeField] UILabel _fishMissionValue;
    [SerializeField] UILabel _moveMissionValue;

    readonly string COOKIE_TILE = "003-ck-tile-standard";
    readonly string STONE_TILE = "004-stone-block-1";
    readonly string FISH_TILE = "fish-c-clear";
    readonly string MOVE_TILE = "colorfull-top";

    int _missionTileIndex = 0;

    #endregion

    // 장착 아이템 관련 변수

    public InGameEquipItemCtrl[] arrEquipItem = new InGameEquipItemCtrl[3];


    #region 10초 더 플레이 화면 관련 변수 
    [SerializeField] MorePlayCtrl _morePlayCtrl;
    bool _isMorePlayOpened = false;

    [SerializeField] Transform _morePlayEffectTransform;

    #endregion

    #region  보드 클리어 관련 변수
    [SerializeField] Transform _textPerfectClear;
    [SerializeField] Transform _textGreatClear;
    [SerializeField] Transform _textPerfectSec;

    readonly Vector3 _posLeftGreatClear = new Vector3(-500, 192, 0);
    readonly Vector3 _posRightGreatClear = new Vector3(500, 192, 0);

    readonly Vector3 _posLeftPerfectClear = new Vector3(-500, 185, 0);
    readonly Vector3 _posRightPerfectClear = new Vector3(500, 185, 0);

    readonly Vector3 _posPerfectSec = new Vector3(0, 185, 0);

    #endregion


    private System.DateTime _pauseStartTime;
    private System.TimeSpan _timeGap;



    public static InUICtrl Instance {
        get {
            if (_instance == null) {
                _instance = FindObjectOfType(typeof(InUICtrl)) as InUICtrl;

                if (_instance == null) {
                    //Debug.Log("InUICtrl Init Error");
                    return null;
                }
            }

            return _instance;
        }
    }


    // Use this for initialization
    void Start() {
        

        lblScore.gameObject.SetActive(true);

        if (GameSystem.Instance.IsHotTime) {
            coinHot.gameObject.SetActive(true);
            coinHot.Play();
        }

        spReady.SetActive(false);
        spGo.SetActive(false);

        // 상단 배경 처리
        SetTopSpriteTheme();
    }


    #region 스테이지별 UI 세팅 

    public void InitStageUI() {
        SetItemBarUIType();
    }


    /// <summary>
    /// 스테이지 종류에 따른 아이템 게이지 타입 세팅 
    /// </summary>
    private void SetItemBarUIType() {
        if (InGameCtrl.Instance.IsCookieMission || InGameCtrl.Instance.IsStoneMission || InGameCtrl.Instance.IsFishMission || InGameCtrl.Instance.IsMoveMission) {

            itemBar = _smallItemBar;
            spItemHead = _smallItemHead;

            // 반대쪽 오브젝트는 감춘다.
            _bigItemHead.gameObject.SetActive(false);
            _bigItemBar.gameObject.SetActive(false);

            // 특수 미션에 따라서, UI 처리 
            if(InGameCtrl.Instance.IsCookieMission) { // 쿠키 미션 

                if(_missionTileIndex == 0) {
                    _missionTileSprite1.spriteName = COOKIE_TILE;
                    _cookieMissionValue = _missionTileValue1;
                }
                else {
                    _missionTileSprite2.spriteName = COOKIE_TILE;
                    _cookieMissionValue = _missionTileValue2;
                }

                _cookieMissionValue.text = InGameCtrl.Instance.GetRemainCookieBlocks().ToString();
                _missionTileIndex++;
            }

            if(InGameCtrl.Instance.IsStoneMission) { // 바위 미션 
                if (_missionTileIndex == 0) {
                    _missionTileSprite1.spriteName = STONE_TILE; ;
                    _stoneMissionValue = _missionTileValue1;
                }
                else {
                    _missionTileSprite2.spriteName = STONE_TILE;
                    _stoneMissionValue = _missionTileValue2;
                }

                _stoneMissionValue.text = InGameCtrl.Instance.InitStoneBlockCount.ToString();
                _missionTileIndex++;
            }

            if (InGameCtrl.Instance.IsFishMission) { // 생선굽기 미션 
                if (_missionTileIndex == 0) {
                    _missionTileSprite1.spriteName = FISH_TILE; ;
                    _fishMissionValue = _missionTileValue1;
                }
                else {
                    _missionTileSprite2.spriteName = FISH_TILE;
                    _fishMissionValue = _missionTileValue2;
                }

                _fishMissionValue.text = InGameCtrl.Instance.InitFishGrillCount.ToString();
                _missionTileIndex++;
            }

            if (InGameCtrl.Instance.IsMoveMission) { // 이동 미션 
                if (_missionTileIndex == 0) {
                    _missionTileSprite1.spriteName = MOVE_TILE; ;
                    _moveMissionValue = _missionTileValue1;
                }
                else {
                    _missionTileSprite2.spriteName = MOVE_TILE;
                    _moveMissionValue = _missionTileValue2;
                }

                _moveMissionValue.text = InGameCtrl.Instance.MoveMissionCount.ToString();
                _missionTileIndex++;
            }



            // 특수미션이 한개면 2번째 UI는 감춘다. 
            if (_missionTileIndex == 1) {
                _missionTileSprite2.gameObject.SetActive(false);
                _missionTileValue2.gameObject.SetActive(false);
            }
                

        }
        else { // 일반 스테이지 

            itemBar = _bigItemBar;
            spItemHead = _bigItemHead;

            // 반대쪽 오브젝트는 감춘다.
            _smallItemHead.gameObject.SetActive(false);
            _smallItemBar.gameObject.SetActive(false);

            _missionTileSprite1.gameObject.SetActive(false);
            _missionTileSprite2.gameObject.SetActive(false);

        }

        itemBar.gameObject.SetActive(true);
        spItemHead.gameObject.SetActive(true);

        originHeadPos = spItemHead.transform.localPosition;
    }

    /// <summary>
    /// 테마에 맞는 배경 상단 스프라이트 처리 
    /// </summary>
    private void SetTopSpriteTheme() {

        
        SimpleJSON.JSONNode stagenode = GameSystem.Instance.StageDetailJSON[GameSystem.Instance.PlayStage - 1];
        int theme = stagenode["theme"].AsInt;

        if (InGameCtrl.Instance.GetBossStage()) {
            _bgTop.SetSprite("001-top-bg-boss1");
            return;
        }

        Debug.Log("★ SetTopSpriteTheme theme :: " + theme);

        switch(theme) {
            case 1:
                _bgTop.SetSprite("001-top-bg-1");
                break;

            case 2:
                _bgTop.SetSprite("001-top-bg-2");
                break;

            case 3:
                _bgTop.SetSprite("001-top-bg-3");
                break;

            case 4:
                _bgTop.SetSprite("001-top-bg-4");
                break;

            case 5:
                _bgTop.SetSprite("001-top-bg-5");
                break;

            case 6:
                _bgTop.SetSprite("001-top-bg-6");
                break;

            case 7:
                _bgTop.SetSprite("001-top-bg-7");
                break;

            case 8:
                _bgTop.SetSprite("001-top-bg-8");
                break;

            case 9:
                _bgTop.SetSprite("001-top-bg-9");
                break;

            case 10:
                _bgTop.SetSprite("001-top-bg-10");
                break;

            default:
                _bgTop.SetSprite("001-top-bg-1");
                break;

        }
    }


    /// <summary>
    /// 바위 미션 값 세팅
    /// </summary>
    /// <param name="pValue"></param>
    public void SetStoneMissionValue(int pValue) {
       _stoneMissionValue.text = pValue.ToString();
    }

    /// <summary>
    /// 쿠키 미션 값 세팅 
    /// </summary>
    /// <param name="pValue"></param>
    public void SetCookieMissionValue(int pValue) {
        _cookieMissionValue.text = pValue.ToString();
    }

    /// <summary>
    /// 생선굽기 값 세팅 
    /// </summary>
    /// <param name="pValue"></param>
    public void SetFishMissionValue(int pValue) {
        _fishMissionValue.text = pValue.ToString();
    }

    public void SetMoveMissionValue(int pValue) {
        _moveMissionValue.text = pValue.ToString();
    }

    #endregion

    #region 스테이지 미션 


    /// <summary>
    /// 스테이지 미션 
    /// </summary>
    public void ShowStageMission() {
        StartCoroutine(ShowingStageMission());
    }

    IEnumerator ShowingStageMission() {

        // 스테이지 미션 팝업을 오픈합니다.
        _stageMissionInfo.SetIngameMissionInfo(GameSystem.Instance.PlayStage);

        // UI 설정
        InitStageUI();

        yield return new WaitForSeconds(2);

        _stageMissionInfo.EnableButton();
    }

    #endregion

    #region 패시브, 아이템 정보 표시


    /// <summary>
    /// 부스트 아이템 효과 시작
    /// </summary>
    public void ShowEquipItem() {
        StartCoroutine (ShowingEquipItem ());
	}

	IEnumerator ShowingEquipItem() {


        InGameCtrl.Instance.IsShowingSkillAndIcon = true;

        while (_stageMissionInfo.gameObject.activeSelf) {
            yield return new WaitForSeconds(0.1f);
        }

        yield return null;

        if (InGameCtrl.Instance._equipItemCount == 0) {
            InGameCtrl.Instance.IsShowingSkillAndIcon = false;
            yield break;
        }


        // 장착 부스트 아이템의 On 처리 
        if (InGameCtrl.Instance.BoostPlayTime) {
            arrEquipItem[0].SetActiveEquipItem();
            InGameCtrl.Instance.AddGameTime(5);
            yield return new WaitForSeconds(0.2f);
        }

        if (InGameCtrl.Instance.BoostBombCreate) {
            arrEquipItem[1].SetActiveEquipItem();
            yield return new WaitForSeconds(0.2f);
        }

        if (InGameCtrl.Instance.BoostSpecialAttack) {
            arrEquipItem[2].SetActiveEquipItem();
            yield return new WaitForSeconds(0.2f);
        }


        InGameCtrl.Instance.IsShowingSkillAndIcon = false;


    }

	

    #endregion




    #region  장착 아이템 

    public void InitEquipItem() {
		for(int i=0;i<arrEquipItem.Length;i++) {
			arrEquipItem[i].Init(i);
		}
	}





	public void InactiveEquipItem(int pIndex) {
		arrEquipItem [pIndex].InactiveEquipItem ();
	}



	#endregion 



	#region UI 동작 
	
	/// <summary>
	/// Ready 이미지 등장 
	/// </summary>
	public void OnReadySprite() {
        spReady.transform.localPosition = new Vector3(0, -800, 0);
		spReady.SetActive (true);


        spReady.transform.DOLocalMoveY (0, 1f, false).SetEase (Ease.InOutBack).OnComplete(EndReadySprite);
		
		InSoundManager.Instance.PlayReady ();
	}
	
	private void EndReadySprite() {
		spReady.transform.DOLocalMoveY(800, 0.5f, false).SetEase (Ease.InOutBack).SetDelay(0.2f);
		
		
		StartCoroutine (OnGoSprite ());
	}
	
	IEnumerator OnGoSprite() {
		yield return new WaitForSeconds(0.2f);

        spGo.transform.localPosition = Vector3.zero;
        spGo.transform.localScale = new Vector3(2, 2, 2);

        spGo.SetActive (true); // Go 이미지 등장
		spGo.transform.DOScale (GameSystem.Instance.BaseScale, 0.5f).SetEase (Ease.OutCubic).OnComplete (EndReadyGo);
		
		// 사운드 처리
		InSoundManager.Instance.PlayGo ();
	}
	
	private void EndReadyGo() {
		spGo.SetActive (false);
		spReady.SetActive (false);

        InGameCtrl.Instance.SetState(GameState.Play);
	}

	// 팝업창 오픈 
	public void PopPause() {

        if (!IsPossiblePause)
            return;

		_isActivePause = true; 
		InGameCtrl.Instance.SetInGameLock (true);
		pausePopup.SetActive (true);

		_pauseStartTime = System.DateTime.Now;

	}

	// 팝업창 종료 
	public void Resume() {
		_isActivePause = false; 
		InGameCtrl.Instance.fromComboTime -= 0.5f;
		InGameCtrl.Instance.SetInGameLock (false);
		pausePopup.SendMessage ("Close");

		//pause 시간 계산
		_timeGap = System.DateTime.Now - _pauseStartTime;
		GameSystem.Instance.InGamePauseTime += _timeGap.Seconds;
		//_timeGap.
	}
	#endregion

	#region Item 관련
	/// <summary>
	/// 아이템 바에 값을 더한다. 
	/// </summary>
	/// <param name="num">Number.</param>
	public void AddItemBarValue(int num) {


        itemBarCnt += num;
        itemBarValue = itemBarCnt / (float)InGameCtrl.Instance.BombAppearBlockCount;
        itemBar.value = itemBarValue;

		
		if (itemBarCnt >= InGameCtrl.Instance.BombAppearBlockCount) { // 아이템 소환 
			SeqSummonItem();
		}
	}
	
	public void StartSeqSummonItem() {
		//StartCoroutine (SeqSummonItem ());
		SeqSummonItem ();
	}
	
	
	private void SeqSummonItem() {

		
		// Item Bar 초기화 및 Tween
		//itemBar.transform.DOScale (new Vector3 (1.2f, 1.2f, 1), 0.2f);
		itemBar.transform.DOShakeScale(0.8f, 0.5f, 5, 30);
		itemBarCnt = 0;
		itemBarValue = 0;
        itemBar.value = 0;
		
		// Header 이동 
		SummonItem ();
	}
	
	private void SummonItem() {
		pickItemBlock = InGameCtrl.Instance.GetEmptyBlock (); // 빈공간을 할당받는다. 
		//pickItemBlock.SetBlockLock (true);
		pickItemBlock.SetItemBlock (currentItemIndx);
		
		//spItemFloor.transform.position = pickItemBlock.transform.position;
		pickItemBlockPos = pickItemBlock.transform.position;
		pickItemBlockPos = uiCamera.ViewportToWorldPoint (mainCamera.WorldToViewportPoint (pickItemBlockPos));
		
		spItemHead.transform.DOMove (pickItemBlockPos, 0.2f).OnComplete (OnCompleteThrowItemHead);
		//spItemHead.transform.DOMove (pickItemBlockPos, 0.2f);
	}

	public void SpecificItem(int pItemIndex) {
		pickItemBlock = InGameCtrl.Instance.GetEmptyBlock (); // 빈공간을 할당받는다. 
		pickItemBlock.SetItemBlock (pItemIndex);

	}
	
	private void OnCompleteThrowItemHead() {
		
		InSoundManager.Instance.PlayBombDrop ();

        //spItemFloor.gameObject.SetActive (true);
        //spItemHead.transform.DOScale (new Vector3(1.8f, 1.8f, 1), 0.3f).OnComplete(OnCompleteArrive);
        OnCompleteArrive();

    }
	
	private void OnCompleteDoFadeIn() {
		//spItemFloor.DOFade (0, 0.2f).OnComplete(OnCompleteDoFadeOut); // 레이드 아웃 처리 
	}
	
	private void OnCompleteDoFadeOut() {
		//spItemFloor.gameObject.SetActive (false);
	}
	
	
	private void OnCompleteArrive() {
		//spItemFloor.gameObject.SetActive (false);
		//spItemHead.transform.DOScale(new Vector3(1.5f, 1.5f, 1), 0.2f).OnComplete(OnCompleteDelivery);
        OnCompleteDelivery();

    }
	
	private void OnCompleteDelivery() {
		// 아이템 헤더 이동이 완료되면 블록을 바꿔치기. 
		//pickItemBlock.SetBlockLock (false);
		//pickItemBlock.SetItemBlock (currentItemIndx);
		
		spItemHead.transform.localPosition = originHeadPos;
		spItemHead.transform.localScale = GameSystem.Instance.BaseScale;
		
		// 등장 아이템 설정 
		SetItemHead ();
	}
	
	/// <summary>
	/// 등장 아이템 설정 
	/// </summary>
	public void SetItemHead() {

		currentItemIndx = UnityEngine.Random.Range (0, 100);

        // 별폭탄 등장 확률 하향
        if (currentItemIndx < 10)
            currentItemIndx = 0;
        else {
            currentItemIndx = UnityEngine.Random.Range(1, 4);
        }

		//spItemHead.SetSprite (PuzzleConstBox.listItemHeadClip [currentItemIndx]);
		spItemHead.spriteName = PuzzleConstBox.listItemHeadClip [currentItemIndx];
	}

	#endregion

	#region Combo, Score 처리 

    private void PlayScoreAbsorbLight() {
        StartCoroutine(PlayingScoreLight());
    }

    IEnumerator PlayingScoreLight() {

        for(int i=0; i<_listScoreLight.Count; i++) {
            _listScoreLight[i].Play();

            yield return new WaitForSeconds(0.2f);
        }
    }




    /// <summary>
    /// 점수 계산 처리 (블록 컬러 정보 포함)
    /// </summary>
    /// <param name="pMatchList"></param>
    public void AddMatchScoreWithBlockColor(List<BlockCtrl> pMatchList, BlockCtrl pHitBlock) {
        
        _matchCount = pMatchList.Count;


        _blueBlockCount = 0;
        _yellowBlockCount = 0;
        _redBlockCount = 0;

        for (int i = 0; i < pMatchList.Count; i++) {
            if (pMatchList[i].blockID == 0)
                _blueBlockCount++;
            else if (pMatchList[i].blockID == 1)
                _yellowBlockCount++;
            else if (pMatchList[i].blockID == 2)
                _redBlockCount++;
        }

        GameSystem.Instance.IngameBlockCount += _matchCount;

        GameSystem.Instance.MatchedBlueBlock += _blueBlockCount;
        GameSystem.Instance.MatchedYellowBlock += _yellowBlockCount;
        GameSystem.Instance.MatchedRedBlock += _redBlockCount;

        if(_matchCount <= 2)
            calcScore = 100;
        else if(_matchCount == 3)
            calcScore = 200;
        else
            calcScore = 300;

        _comboBonusScore = GetComboBonus();

        // 콤보에 따른 추가 가산 
        calcScore = Mathf.RoundToInt(calcScore + (calcScore * comboCnt * _comboBonusScore));


        for(int i=0; i<pMatchList.Count; i++) {
            pMatchList[i].OnScore(calcScore, pMatchList.Count);
        }

        //gameScore += calcScore;
        //lblScore.text = gameScore.ToString("#,###");

        SetBlockCount();
    }

    float GetComboBonus() {
        if (comboCnt < 10)
            _comboBonusScore = 0.01f;
        else if (comboCnt >= 10 && comboCnt < 20)
            _comboBonusScore = 0.02f;
        else if (comboCnt >= 20 && comboCnt < 30)
            _comboBonusScore = 0.03f;
        else if (comboCnt >= 30 && comboCnt < 40)
            _comboBonusScore = 0.04f;
        else if (comboCnt >= 40)
            _comboBonusScore = 0.05f;

        return _comboBonusScore;
    }

    /// <summary>
    /// 신버전 폭탄 스코어 처리 
    /// </summary>
    /// <param name="pCount"></param>
    public void AddBombDestroyScore(Transform pTR,  int pCount) {
        calcScore = 1000;

        if (pCount == 0)
            pCount = 1;


        calcScore = Mathf.RoundToInt( (pCount * 100) + (pCount * 100 * GetComboBonus()));


        gameScore += calcScore;

        lblScore.text = gameScore.ToString("#,###");

        SetBlockCount();
    }







    /// <summary>
    /// 스코어에 합산하기. 
    /// </summary>
    /// <param name="pScore"></param>
    public void AddScore(int pScore) {
		gameScore += pScore;
		lblScore.text = string.Format("{0:n0}", gameScore);

        SetBlockCount();
    }

    public void SetBlockCount() {
        _lblBlockCount.text = GameSystem.Instance.IngameBlockCount.ToString();
    }

    /// <summary>
    /// 스코어 레이블 흔들기 
    /// </summary>
	public void ShakeScore() {
		lblScore.transform.DOShakeScale (0.5f, 1, 10, 90).OnComplete (OnCompleteShakeScore);
	}

	private void OnCompleteShakeScore() {
		lblScore.transform.localScale = PuzzleConstBox.baseScale;
	}






    /// <summary>
    /// 파라매터 만큼 콤보 카운트를 증가시킨다. 
    /// </summary>
    /// <param name="pValue">P value.</param>
    public void SetComboCnt(int pValue) {

        // 보너스 타임에는 사용하지 않는다. 
		if (InGameCtrl.Instance.IsBonusTime) 
			return;


		InitComboTime (); // 콤보 시간 0으로. 

        // 콤보를 할때마다 TotalCombo를 증가
        GameSystem.Instance.InGameTotalCombo++;

		comboCnt += pValue;

        // 5배수 마다 콤보 유지시간 감소 
        if (comboCnt % 10 == 0) 
            InGameCtrl.Instance.MinusComboTime();





        comboUI.gameObject.SetActive(true);
        comboUI.DOKill();
        comboUI.localScale = PuzzleConstBox.baseScale;
        comboUI.DOPunchScale(new Vector3(1.2f, 1.2f, 1), 0.2f, 2).OnComplete(OnCompleteComboPunchScale);


		// 레이블 세팅 
		lblCombo.text = comboCnt.ToString ();


	}





	

	private void OnCompleteComboPunchScale(){
		comboUI.transform.localScale = GameSystem.Instance.BaseScale;
	}

	
	public void ResetCombo() {

        //Debug.Log(">> ResetCombo");

		InGameCtrl.Instance.RestoreComboTime (); // 콤보시간 복원처리 
		
		// RestCombo 전에 MaxCombo 체크해서 세팅 
		if (GameSystem.Instance.InGameMaxCombo < comboCnt) {
			GameSystem.Instance.InGameMaxCombo = comboCnt;
		}


        comboCnt = 0;
		
		
		comboUI.gameObject.SetActive (false); // 콤보 숫자 표시 비활성화 
		comboUI.transform.localScale = GameSystem.Instance.BaseScale;
	}

    /// <summary>
    /// 미스시에 발생하는 콤보 클리어 처리 
    /// </summary>
    public void MissCombo() {
        InGameCtrl.Instance.RestoreComboTime(); // 콤보시간 복원처리

        // RestCombo 전에 MaxCombo 체크해서 세팅 
        if (GameSystem.Instance.InGameMaxCombo < comboCnt) {
            GameSystem.Instance.InGameMaxCombo = comboCnt;
        }


        comboCnt = 0;
        comboUI.gameObject.SetActive(false); // 콤보 숫자 표시 비활성화 
        comboUI.transform.localScale = GameSystem.Instance.BaseScale;
    }


	private void InitComboTime() {
		InGameCtrl.Instance.fromComboTime = 0;
	}

	#endregion

	#region Coin 처리 

	/// <summary>
	/// 코입 흡수 효과 
	/// </summary>
	public void PlayCoinFx() {
		PlayCoinFx (1);
	}

	public void PlayCoinFx(int pValue) {

        if (!coinSprite.isPlaying) {
            coinSprite.Play();
            InSoundManager.Instance.PlayCoinAbsorb();
        }
		
		// Coin Add
		// 핫타임 체크(50% 증가)
		if (GameSystem.Instance.IsHotTime) {
            inGameGotCoin += pValue * 2;
		} else {
			inGameGotCoin += pValue;
		}


		lblCoin.text = inGameGotCoin.ToString ();
	}

	public void AddGem(int pValue) {
		gotGem += pValue;
	}
    #endregion

    #region 플레이어 장착 네코 게이지(Bar) 
    /// <summary>
    ///  플레이어 캐릭터 스킬 게이지 초기화 
    /// </summary>
    public void InitSkillBars() {


        int nekograde;
        int nekoID;
        float nekoPower;

        GameSystem.Instance.ListNekoDamageInfo = new List<NekoDamageInfo>();
        NekoDamageInfo nekoDamage;

        // 초기화 
        for (int i = 0; i < _arrSkillBars.Length; i++) {
            _arrSkillBars[i].SetIndex(i);


            if (GameSystem.Instance.ListEquipNekoID[i] < 0) {
                _arrSkillBars[i].gameObject.SetActive(false);
            }
            else {
                nekoID = GameSystem.Instance.ListEquipNekoID[i];
                nekograde = GameSystem.Instance.FindNekoStarByNekoID(nekoID);
                nekoPower = GameSystem.Instance.GetNekoInGamePower(nekoID);

                _arrSkillBars[i].SetNekoSkillBar(nekoID, nekograde);

                nekoDamage = new NekoDamageInfo();
                nekoDamage.nekoID = nekoID;
                nekoDamage.nekoStar = nekograde;
                nekoDamage.damage = 0;
                GameSystem.Instance.ListNekoDamageInfo.Add(nekoDamage);

            }
        }
    }



    /// <summary>
    /// 스킬 바 채우기 
    /// </summary>
    public void FillSkillBar(int pBlockID) {

        // blue, yellow, red의 순서 

        if (GameSystem.Instance.ListEquipNekoID[pBlockID] < 0)
            return;



        _arrSkillBars[pBlockID].AddValue(1);
    }


    #endregion

    #region 타임오버, 클리어 

    /// <summary>
    /// 클리어 처리 (보스 몹 처리)
    /// </summary>
    public void ClearGame() {
        Debug.Log("★★★ Time Over");

        // 게임내 입력을 막는다. 
        InGameCtrl.Instance.SetInGameLock(true);


        // 코루틴으로 변경 
        StartCoroutine(ProcessingClearGame());
    }


    IEnumerator ProcessingClearGame() {
        yield return new WaitForSeconds(0.2f);

        // 사운드 플레이 
        InSoundManager.Instance.PlayClear();
        InSoundManager.Instance.StopAlertSound();

        // 클리어 오브젝트 오픈 
        spClear.SetActive(true);
        spClear.transform.DOScale(1, 1).OnComplete(GameOver);

        InSoundManager.Instance.PlayVoiceClear();
    }

    /// <summary>
    /// 게임 클리어 실패 
    /// </summary>
    public void FailGame() {
        Debug.Log("★★★ FailGame");

        // 게임내 입력을 막는다. 
        InGameCtrl.Instance.SetInGameLock(true);


        // 코루틴으로 변경 
        StartCoroutine(ProcessingFailGame());
    }


    IEnumerator ProcessingFailGame() {
        yield return new WaitForSeconds(0.2f);

        // 사운드 플레이 
        InSoundManager.Instance.PlayStageFail();
        InSoundManager.Instance.StopAlertSound();

        // 클리어 오브젝트 오픈 
        spFail.SetActive(true);
        spFail.transform.DOScale(1, 1).OnComplete(GameOver);

        // InSoundManager.Instance.PlayVoiceClear();
    }


    /// <summary>
    /// 타임 오버 처리 
    /// </summary>
    public void TimeOver() {

        Debug.Log("★★★ Time Over");

        // 게임내 입력을 막는다. 
		InGameCtrl.Instance.SetInGameLock (true);


        // 코루틴으로 변경 
        StartCoroutine(WaitingTimeOver());

    }

    IEnumerator WaitingTimeOver() {

        yield return new WaitForSeconds(0.1f);

        // 사운드 플레이 
        InSoundManager.Instance.PlayTimeOut();
        InSoundManager.Instance.StopAlertSound();

        // 타임오버 오브젝트 오픈 
        timeOverObj.SetActive(true);
        spGameOver.transform.DOScale(1, 1).OnComplete(BeforeGameOver);
    }


	
    /// <summary>
    /// 타임오버 오브젝트 완료시 처리(게임오버) 
    /// </summary>
	private void BeforeGameOver() {
        Debug.Log("★★★ BeforeGameOver");

        timeOverObj.SetActive(false);

        // 테스트
        /*
        InGameCtrl.Instance.ReadyResultScene();
        MorePlayCtrl.SetMorePlayWindow();
        return;
        */

        /*
        if (CheckMorePlayPossible()) {

            _isMorePlayOpened = true;

            Debug.Log("Set More Play!");
            // 10초 더 플레이 오픈 
            MorePlayCtrl.SetMorePlayWindow();
            return;
        }
        */

        // 조건이 맞지 않으면 GameOver로 진행 
        GameOver();
    }

    /// <summary>
    /// 게임오버 처리 (보너스 타임과 종료로 분기)
    /// </summary>
    public void GameOver() {


        Debug.Log("★★★ GameOver");
        MorePlayCtrl.gameObject.SetActive(false);




        // 보너스 타임 처리 
        CheckBonusTimePossible();
    }

    /// <summary>
    /// 보너스 타임 체크 
    /// </summary>
    private void CheckBonusTimePossible() {
        if (InGameCtrl.Instance.GetBonusTimePossible()) {
            Invoke("BonusTime", 1f);
            return;
        }
        else {
            Invoke("OnTimeOver", 1f);
            return;
        }

    }

    /// <summary>
    /// 10초 더 플레이 승락.
    /// </summary>
    public void DoMorePlay() {
        GameSystem.Instance.Post2MorePlay(CallBackMorePlay);
    }

    /// <summary>
    /// Action 처리 DoMorePlay의 콜백
    /// </summary>
    /// <param name="pFlag"></param>
    private void CallBackMorePlay(bool pFlag) {

        Debug.Log("★★★ OnMorePlay :: " + pFlag);

        this.MorePlayCtrl.gameObject.SetActive(false); // inactive 

        if (!pFlag) {
            GameOver();
            return;
        }

        // 10초 더 플레이의 연출 
        PlayEffectMorePlay(); 

    }

    #region 10초 더 플레이 Yes 처리. 이펙트
    private void PlayEffectMorePlay() {

        // 10초 플레이 연출중에 일시정지 창을 띄울 수 없도록 처리
        _isPossiblePause = false;


        // 거대 보석이 올라가고, 돌아간다. 
        _morePlayEffectTransform.localPosition = new Vector3(0, -900, 0);
        _morePlayEffectTransform.gameObject.SetActive(true);

        InSoundManager.Instance.PlayTenSecAdd(); // 사운드 플레이 

        _morePlayEffectTransform.DOLocalMoveY(500, 0.5f);

        _morePlayEffectTransform.DOScale(0, 0.5f).SetDelay(0.7f).SetEase(Ease.InOutElastic).OnComplete(OnCompleteMorePlayEffect);
    }

    private void OnCompleteMorePlayEffect() {

        InSoundManager.Instance.PlayAbsorbTime(); // 사운드 플레이 

        // 10초 증가 처리 
        InGameCtrl.Instance.Add10SecToPlayTime();

    }

    #endregion


    /// <summary>
    /// 10초 더 플레이 가능 여부 체크 
    /// 다음의 조건이 만족해야 한다. 
    /// 1. 그 주의 베스트 스코어가 존재할 것. 
    /// 2. 사용자 보석이 50개 이상 있을 것.
    /// 3. 최종 스코어가 최고점수의 70~99% 사이에 도달할것.
    /// 4. 1회 플레이에 1번만 표시 
    /// </summary>
    /// <returns>10초 더 플레이 오픈 여부 </returns>
    private bool CheckMorePlayPossible() {

        float divideValue;

        // 점수 계산 
        InGameCtrl.Instance.ReadyResultScene();

        if (_isMorePlayOpened)
            return false;

        if (GameSystem.Instance.UserBestScore == 0) {
            Debug.Log("★ CheckMorePlayPossible UserBestScore is Zero ");
            return false;
        }

        if (GameSystem.Instance.UserGem < 50) {
            Debug.Log("★ CheckMorePlayPossible UserGem < 50 ");
            return false;
        }


        divideValue = (float)GameSystem.Instance.InGameTotalScore / (float)GameSystem.Instance.UserBestScore;
        Debug.Log("★ CheckMorePlayPossible  divideValue :: " + divideValue);

        if (divideValue < 0.7f) {
            Debug.Log("★ CheckMorePlayPossible  < 0.7f");
            return false;
        }

        if (GameSystem.Instance.InGameTotalScore >= GameSystem.Instance.UserBestScore) {
            Debug.Log("★ CheckMorePlayPossible  >= 1");
            return false;
        }

        return true;
    }





    /// <summary>
    /// 보너스 타임 처리 
    /// </summary>
	private void BonusTime() {

		timeOverObj.SetActive (false);


        spClear.SetActive(false);
        spFail.SetActive(false);

		// Sprite 올려주고 ,
		bonusTimeObj.SetActive (true);
		bonusTimeObj.transform.localScale = new Vector3 (2, 2, 1);
		bonusTimeObj.transform.DOScale (1, 0.5f);

		InGameCtrl.Instance.StartBonusTime ();

	}

	public void OnCompleteBonusTimeScale() {
		//bonusTimeObj.transform.localScale = PuzzleConstBox.baseScale;
		bonusTimeObj.SetActive (false);
	}
	
	
	
	public void OnTimeOver() {
		
		// Neko 이동 중지? 
		//EnemyNekoManager.Instance.DespawnAllEnemyNeko ();
		
		// 기존 timeover invisible
		//timeOverObj.SetActive (false);
		// Max Combo 처리 
		if (GameSystem.Instance.InGameMaxCombo < comboCnt) {
			GameSystem.Instance.InGameMaxCombo = comboCnt;
		}

		InGameCtrl.Instance.OnTimeOver(); // 최종 게임 내 결과 처리 

    }
	
    


	#region Properties
	

	
	public int GotCoin {
		get {
			return this.inGameGotCoin;
		} set {
			this.inGameGotCoin = value;
		}
	}
	
	public int GameScore {
		get {
			return this.gameScore;
		} set {
			this.gameScore = value;
		}
	}

	public bool IsActivePausePopup {
		get {
			return this._isActivePause;
		}
	}
	
    






    public NekoAppearCtrl PlayerAppear {
        get {

            if (playerNekoAppearIndex >= arrPlayerNekoAppear.Length)
                playerNekoAppearIndex = 0;

            return this.arrPlayerNekoAppear[playerNekoAppearIndex++];
        }
    }

    public bool PlayerNekoBarLock {
        get {
            return _playerNekoBarLock;
        }

        set {
            _playerNekoBarLock = value;
        }
    }

    public bool IsPossiblePause {
        get {
            return _isPossiblePause;
        }

        set {
            _isPossiblePause = value;
        }
    }

    public MorePlayCtrl MorePlayCtrl {
        get {
            return _morePlayCtrl;
        }

        set {
            _morePlayCtrl = value;
        }
    }





    #endregion


    #endregion

    #region 10 초 더 플레이의 준비처리 

    public void OnStartMorePlay() {
        spReady.gameObject.SetActive(true);
        spReady.transform.position = new Vector3(0, -10, 0);
        spReady.transform.DOMove(new Vector3(0, 0.4f, 0), 1f, false).SetEase(Ease.InOutBack).OnComplete(OnCompleteReadyObjectMove);

        InSoundManager.Instance.PlayReady();

    }

    private void OnCompleteReadyObjectMove() {
        spReady.transform.DOMove(new Vector3(0, 10, 0), 0.5f, false).SetEase(Ease.InOutBack).SetDelay(0.2f);

        Invoke("MoveGoSprite", 0.2f);
    }

    private void MoveGoSprite() {
        spGo.SetActive(true); // Go 이미지 등장
        spGo.transform.localScale = new Vector3(2, 2, 2);
        spGo.transform.DOScale(GameSystem.Instance.BaseScale, 0.5f).SetEase(Ease.OutCubic).OnComplete(OnCompleteMorePlayReady);

        // 사운드 처리
        InSoundManager.Instance.PlayGo();
    }

    private void OnCompleteMorePlayReady() {
        spGo.SetActive(false);
        spReady.SetActive(false);

        InGameCtrl.Instance.SendMessage("Start10SecMorePlay");
    }

    #endregion

    #region 보드 클리어 텍스트 연출

    /// <summary>
    /// Great 이미지 텍스트 연출 
    /// </summary>
    public void ShowGreatText() {
        _textGreatClear.localPosition = _posLeftGreatClear;
        _textGreatClear.gameObject.SetActive(true);
        _textGreatClear.DOLocalMoveX(0, 0.2f).OnComplete(OnCompleteShowGreatMoveCenter);
        InSoundManager.Instance.PlayVoiceGreat();
    }

    void OnCompleteShowGreatMoveCenter() {
        _textGreatClear.DOLocalMoveX(500, 0.2f).SetDelay(0.4f).OnComplete(OnCompleteShowGreatMoveRight);
        
    }

    void OnCompleteShowGreatMoveRight() {
        _textGreatClear.gameObject.SetActive(false);
    }


    /// <summary>
    /// Perfect 이미지 텍스트 연출 
    /// </summary>
    public void ShowPerfectText() {
        _textPerfectClear.localPosition = _posLeftPerfectClear;
        _textPerfectClear.gameObject.SetActive(true);
        _textPerfectClear.DOLocalMoveX(0, 0.2f).OnComplete(OnCompleteShowPefectMoveCenter);
        InSoundManager.Instance.PlayVoiceGreat();
    }

    void OnCompleteShowPefectMoveCenter() {
        _textPerfectClear.DOLocalMoveX(500, 0.2f).SetDelay(0.4f).OnComplete(OnCompleteShowPefectMoveRight).OnStart(OnStartShowPerfectMoveRight);
        InSoundManager.Instance.PlayVoicePerfect();
    }

    void OnStartShowPerfectMoveRight() {
        _textPerfectSec.localPosition = _posPerfectSec;
        _textPerfectSec.gameObject.SetActive(true);

        _textPerfectSec.DOLocalMoveY(200, 0.5f).OnComplete(OnCompleteShowPerfectSec); ;

        InGameCtrl.Instance.AddGameTime(2); //게임시간 2초 증가 
        InSoundManager.Instance.PlayPerfectBonusTime();

    }

    void OnCompleteShowPerfectSec() {
        _textPerfectSec.gameObject.SetActive(false);
    }

    void OnCompleteShowPefectMoveRight() {
        _textPerfectClear.gameObject.SetActive(false);
    }

    #endregion


}
