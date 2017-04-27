using UnityEngine;
using System.Collections;
using PathologicalGames;
using CodeStage.AntiCheat.ObscuredTypes;
using DG.Tweening;

public partial class InGameCtrl : MonoBehaviour {

	/* 콤보 변수 */
	public ObscuredFloat comboTime = 2f;
	public ObscuredFloat fromComboTime = 0f;
	
	/* 타이머 변수 */
	public ObscuredFloat playTime = 0;
	[SerializeField] int playTimeValue = 0;

    float _playtimeFromZero = 0; // 0부터 시작하는 플레이타임 


	private bool isTenSecAlert = false; // 타임오버 경고 표시 


	// Timer
	[SerializeField] UILabel lblInGameTimer = null;
    [SerializeField] bool _isOnNavigator = false;
    

    [SerializeField]
    GameObject _pausePopup;


    



    IEnumerator InGameTimer() {
		
		do {
			yield return null;

            if (_pausePopup.activeSelf)
                continue;

			if(timerLock)
				continue;

			fromComboTime += Time.deltaTime;
			
			// 콤보 초기화 (No Miss Time 추가)
			if(fromComboTime > comboTime && InUICtrl.Instance.comboCnt != 0 
                && currentState == GameState.Play) {
				InUICtrl.Instance.ResetCombo();
                //GameSystem.Instance.IngameMissCount++; // 시간이 초과해도 Miss Count를 증가시킨다.


            }

            PlaytimeFromZero += Time.deltaTime; //0부터 시작하는 플레이 타임(미션에서 사용)

            playTime -= Time.deltaTime;
			playTimeValue = (int)playTime;
			lblInGameTimer.text = playTimeValue.ToString();


			if(playTime < 10 && !isTenSecAlert){
				isTenSecAlert = true;
				// 5초 경고 표시 시작 
				OnAlertStage();
			}




            if (playTime <= 0) {

                playTime = 0;
                playTimeValue = 0;
                lblInGameTimer.text = playTimeValue.ToString();

                isPlaying = false;
            }

            CheckNoInputTime();
			
		} while (isPlaying); // 플레이 중.. 



        // 시간 끝나서 플레이 종료 
        yield return null;

        

        // 타임오버 효과 
        OffAlertStage ();

        // 네이게이터 오프 
        OffNavigator();

        // Time Over 처리 
        this.preState = this.currentState;

        if (IsBossStage || IsRescueStage) {

            // 실패와 성공으로 분기되야 한다.
            if (GameSystem.Instance.InGameDestroyUFO > 0 || GameSystem.Instance.InGameRescueNeko > 0)
                InUICtrl.Instance.ClearGame();
            else
                InUICtrl.Instance.FailGame();

            // Fail

        }
        else {
            InUICtrl.Instance.TimeOver();
        }



        yield return new WaitForSeconds(0.5f);
        SetTimeOverBlock();
        

    }


    /// <summary>
    /// 게임 Ready 연출 시작 
    /// </summary>
    /// <returns></returns>
	IEnumerator Ready() {


		// 장착 아이템이 있으면 1초 늦게 시작한다. 
		if (_equipItemCount > 0) 
			yield return new WaitForSeconds (1);


        while (IsShowingSkillAndIcon)
            yield return new WaitForSeconds(0.1f);

        // 준비 과정 종료 되면 Ready-Go 텍스트 처리 
        InUICtrl.Instance.OnReadySprite ();
		
		do {
			yield return null;
			
			// 새로운 상태가 입력되면, break.
			if(_isNewState) 
				break;
			
			
			
		} while (!_isNewState);
	}
	
	
	
	IEnumerator Play() {
		
    	yield return null;

        // 최초 플레이 처리 
        if (!isFirstPlay) {


            #region 스테이지별 Tip 

            OpenGameTip();

            while (_puzzleTip.GetGameTipActive()) {
                yield return new WaitForSeconds(0.1f);
            }

            #endregion

            InUICtrl.Instance.IsPossiblePause = true; // 게임이 시작된 후에 pause를 할 수 있음.

			isFirstPlay = true;
			isPlaying = true;

			StartCoroutine(GenerateStage());

			while(!isSpawned) {
				yield return null;
			}

			updateLock = false;

			// 타이머 동작 시작
			StartCoroutine(InGameTimer());

			// BGM 플레이
			InSoundManager.Instance.PlayBGM(true);




        }

		// 이미 시작한 상태에서 게임이 종료되면 플레이 종료 
		if (isFirstPlay && !isPlaying) {
			yield break;
		}


        SetBlockFace();

        do {
			yield return null;
			
			// 새로운 상태가 입력되면, break.
			if(_isNewState) 
				break;
			
			
			
		} while (!_isNewState);
	}





    /// <summary>
    /// 게임 팁 체크 및 오픈 
    /// </summary>
    void OpenGameTip() {

        /* 특정 스테이지에 입장하면 팁을 띄운다. */
        if (GameSystem.Instance.PlayStage == 1) {
            Debug.Log("★ Stage 1 Tutorial");
            _puzzleTip.SetGameTip(TipType.FisrtStage);
        }
        else if (GameSystem.Instance.PlayStage == 2) {
            Debug.Log("★ Stage 2 Tutorial");
            _puzzleTip.SetGameTip(TipType.BombTip);
        }
        else if (GameSystem.Instance.PlayStage == 3) {
            Debug.Log("★ Stage 3 Tutorial");
            _puzzleTip.SetGameTip(TipType.SpecialAttackTip);
        }
        else {

            // 최초의 특수 미션을 접했을때, 한번도 팁을 보지 않은 경우, 팁 화면 오픈 
            if(IsCookieMission && GameSystem.Instance.UserJSON[PuzzleConstBox.ES_StageCookieTip].AsInt > 0) {
                _puzzleTip.SetGameTip(TipType.CookieTip);
                GameSystem.Instance.Post2Unlock("stage_cookie_tip"); // DB 통신
            }
            else if(IsStoneMission && GameSystem.Instance.UserJSON[PuzzleConstBox.ES_StageStoneTip].AsInt > 0) {
                _puzzleTip.SetGameTip(TipType.StoneTip);
                GameSystem.Instance.Post2Unlock("stage_stone_tip");
            }
            else if (IsFishMission && GameSystem.Instance.UserJSON[PuzzleConstBox.ES_StageGrillTip].AsInt > 0) {
                _puzzleTip.SetGameTip(TipType.FishGrillTip);
                GameSystem.Instance.Post2Unlock("stage_grill_tip");
            }
            else if (IsMoveMission && GameSystem.Instance.UserJSON[PuzzleConstBox.ES_StageMoveTip].AsInt > 0) {
                _puzzleTip.SetGameTip(TipType.MoveMissionTip);
                GameSystem.Instance.Post2Unlock("stage_move_tip");
            }
        }


        /*
        if (GameSystem.Instance.PlayStage == 14) {
            Debug.Log("★ Stage 14 Tutorial");
            _puzzleTip.SetGameTip(TipType.CookieTip);
        }

        else if (GameSystem.Instance.PlayStage == 27) {
            Debug.Log("★ Stage 27 Tutorial");
            _puzzleTip.SetGameTip(TipType.StoneTip);
        }

        else if (GameSystem.Instance.PlayStage == 53) {
            Debug.Log("★ Stage 53 Tutorial");
            _puzzleTip.SetGameTip(TipType.FishGrillTip);
        }
        */
    }



    /// <summary>
    /// 10초 더 플레이 
    /// </summary>
    public void Add10SecToPlayTime() {
        Debug.Log("★★ AddMorePlayTime");

        playTime = 10;
        isTenSecAlert = false;
        lblInGameTimer.text = playTime.ToString();

        SetBlockFace(); // 블록 페이스 제거 
        OffAlertStage(); // 10초 알림 제거 

        // 레디 - 고 연출 추가
        InUICtrl.Instance.OnStartMorePlay();
    }


    /// <summary>
    /// 10초 더 플레이 시작!(시간이 흐름)
    /// </summary>
    private void Start10SecMorePlay() {
        // 락 해제 
        SetInGameLock(false);
        

        isPlaying = true;
        
        

        // 인게임 타이머 다시 시작 
        StartCoroutine(InGameTimer());

        // Pause Lock 해제
        InUICtrl.Instance.IsPossiblePause = true;
    }


	public void UpdateGameTime() {
		// 네코 패시브를 통해 시간을 증가해준다. 
		playTime += GameSystem.Instance.NekoGameTimePlus;
		playTimeValue = (int)playTime;
		lblInGameTimer.text = playTimeValue.ToString ();
	}


    /// <summary>
    /// Active Skill 효과로 게임 시간을 증가시킨다. 
    /// </summary>
    /// <param name="pAddTime"></param>
	public void AddGameTime(float pAddTime) {
		playTime += pAddTime;
		playTimeValue = (int)playTime;
		lblInGameTimer.text = playTimeValue.ToString ();

		lblInGameTimer.transform.DOShakeScale (0.5f, 1, 10, 90).OnComplete (OnCompleteShakeTimer);


	}


    /// <summary>
    /// 패널티로 게임 시간을 감소시킨다.
    /// </summary>
    /// <param name="pMinusTime"></param>
    public void MinusGameTime(float pMinusTime) {


        if (playTime <= 0) {
            return;
        }

        playTime -= pMinusTime;
        playTimeValue = (int)playTime;
        lblInGameTimer.text = playTimeValue.ToString();

        lblInGameTimer.transform.DOShakeScale(0.5f, 1, 10, 90).OnComplete(OnCompleteShakeTimer);
    }

	private void OnCompleteShakeTimer() {
		lblInGameTimer.transform.localScale = PuzzleConstBox.baseScale;
	}





    public bool IsOnNavigator {
        get {
            return _isOnNavigator;
        }

        set {
            _isOnNavigator = value;
        }
    }

    public float PlaytimeFromZero {
        get {
            return _playtimeFromZero;
        }

        set {
            _playtimeFromZero = value;
        }
    }
}
