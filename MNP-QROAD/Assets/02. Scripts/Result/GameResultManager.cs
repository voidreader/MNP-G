using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PathologicalGames;
using DG.Tweening;
using SimpleJSON;

public class GameResultManager : MonoBehaviour {

    static GameResultManager _instance = null;

    bool _endResultShow = false;

    [SerializeField]
    GameObject _lock;

    JSONNode _stageNode, _userStageNode;

    [SerializeField]
    string _debugStageNode;

    [SerializeField]
    private GameObject _resultForm;

    [SerializeField] StageMissionResultEffect _resultEffect;


    [SerializeField] private Transform objNewRecord;

    [SerializeField] UISprite _clearGradeSprite;
    [SerializeField] UILabel _clearGradeLabel;
    [SerializeField] YellowLightCtrl _clearGradeEffect;

    // 미션 정보
    [SerializeField]
    StageMissionColCtrl[] _arrStageMissionCols = new StageMissionColCtrl[4];
    

    [SerializeField] GameObject _btnRestart;
    [SerializeField] GameObject _btnNextStage;
    [SerializeField] GameObject _btnHome;

    // 수치
    [SerializeField] private UILabel lblTotalScore; // Total Score
    
    [SerializeField] private UILabel lblMaxCombo; // Max Combo
    [SerializeField] private UILabel lblCoin; // Coin 
    [SerializeField] private UILabel lblStage; // Stage
    [SerializeField] private UILabel lblNekoBonus; // 네코 보너스 
    [SerializeField] private UILabel lblBadgeBonus; // 뱃지 보너스 

    [SerializeField] private UILabel lblChub; // 고등어
    [SerializeField] private UILabel lblTicket; // 티켓

    [SerializeField] UILabel lblMaxComboBonus; // 날아가는 콤보 보너스 
    
    [SerializeField] UILabel lblFlyingBadgeBonus; // 날아가는  보너스 

    // Plus 수치 
    
    [SerializeField] UISprite spPlusCoinIcon;
    [SerializeField] private UILabel lblPlusCoin;
    [SerializeField] UISprite spScoreIcon;
    [SerializeField] private UILabel lblScorePercent;



    // 사운드 
    [SerializeField] AudioSource resultAudioSource;
    [SerializeField] AudioSource scoreAudioSource;

    [SerializeField] AudioClip _clipResultFormPopUp;
    [SerializeField] AudioClip _clipStageMissionCol;
    [SerializeField] AudioClip _clipStageMissionClear;
    [SerializeField] AudioClip _clipStageMissionFail;



    // Panel
    [SerializeField]
    UIPanel _lobbyPanel;

    [SerializeField] GameObject _resultButtons;

    UIButton[] _arrButtons;


    Vector3 _initResultFormPos = new Vector3(0, -100, 0);



    public static GameResultManager Instance {
        get {
            if (_instance == null) {
                _instance = FindObjectOfType(typeof(GameResultManager)) as GameResultManager;

                if (_instance == null) {
                    Debug.Log("GameResultManager Init Error");
                    return null;
                }
            }

            return _instance;
        }
    }


    // Use this for initialization
    void Start () {
	
	}
	
    /// <summary>
    /// 게임결과 화면 종료 처리 
    /// </summary>
    public void ClearResult() {
        

        _lobbyPanel.gameObject.SetActive(true);
        _lock.gameObject.SetActive(false);

        _resultForm.SendMessage("CloseSelf");


        CheckRatePopup();

    }

    #region 평가팝업 관련 

    /// <summary>
    /// 평가 팝업 오픈 
    /// </summary>
    private void CheckRatePopup() {

        Debug.Log("▶▶▶ CheckRatePopup firstWeekRate ::  " + GameSystem.Instance.FirstWeekPlay.ToString());
        Debug.Log("▶▶▶ CheckRatePopup UpdateBestScoreCount ::  " + GameSystem.Instance.UpdateBestScoreCount.ToString());
        Debug.Log("▶▶▶ CheckRatePopup GetRate ::  " + GameSystem.Instance.GetRate().ToString());

        // 이미 평가를 했거나 거절한 경우는 리턴 
        if (GameSystem.Instance.GetRate())
            return;

        // 조건 체크 (매주 첫번째 플레이거나, 최고 스코어를 3회 갱신 )
        if(!GameSystem.Instance.FirstWeekPlay && GameSystem.Instance.DtSyncTime.DayOfWeek == System.DayOfWeek.Tuesday) {
            GameSystem.Instance.FirstWeekPlay = true;
            GameSystem.Instance.OpenRatePopUp(); // 평가 팝업창 오픈 
            return;
        }

        if(GameSystem.Instance.UpdateBestScoreCount == 3) {
            
            GameSystem.Instance.UpdateBestScoreCount = 4;
            GameSystem.Instance.OpenRatePopUp(); // 평가 팝업창 오픈 
            return;

        }

    }

    #endregion


    /// <summary>
    /// 게임 결과 화면 보이기 시작 
    /// </summary>
    public void BeginShowingResult() {
        _lobbyPanel.gameObject.SetActive(false); // 로비패널 비활성화 처리 
        //LobbyCtrl.Instance.SendMessage("DisableTopLobbyUI");

        DisableTopButton();

        ShowResultForm();
    }


    /// <summary>
    /// 로비로 돌아가기 
    /// </summary>
    public void LoadLobby() {

        if (_endResultShow)
            return;

        _endResultShow = true;
        GameSystem.Instance.IsOnGameResult = false;
        GameSystem.Instance.ListEquipItemID.Clear();
        ClearResult();
        


        LobbyCtrl.Instance.InitializeLobby();
        

        StopResultBGM();
    }

    /// <summary>
    /// 재시작 버튼 
    /// </summary>
    public void LoadReplay() {
        if (_endResultShow)
            return;

        _endResultShow = true;
        GameSystem.Instance.IsOnGameResult = false;
        GameSystem.Instance.ListEquipItemID.Clear();
        ClearResult();


        LobbyCtrl.Instance.InitializeLobby(true);
        // 마지막에 플레이했던 스테이지가 업데이트 되었을 경우에는 연출을 보고, 레디창을 띄운다.
        // 마지막에 플레이했던 스테이지가 업데이트 되지 않은 경우에는 바로 레디창 오픈 
        
        if(StageMasterCtrl.Instance.GetLastStageUpdated()) {
            StageMasterCtrl.OnCompleteStageClearDirect += LobbyCtrl.Instance.OpenReady;

        }
        else {
            LobbyCtrl.Instance.OpenReady(GameSystem.Instance.PlayStage);
        }
        

        StopResultBGM();
    }


    // 다음 스테이지 열기 
    public void LoadNextStage() {
        if (_endResultShow)
            return;

        _endResultShow = true;
        GameSystem.Instance.IsOnGameResult = false;
        GameSystem.Instance.ListEquipItemID.Clear();
        ClearResult();


        LobbyCtrl.Instance.InitializeLobby(true);

        // 스테이지 클리어 상황 업데이트가 이루어진 경우에는 연출을 보고 준비화면 오픈 
        if (StageMasterCtrl.Instance.GetLastStageUpdated()) {
            // 플레이 스테이지를 1 증가 시킨다.
            GameSystem.Instance.PlayStage++;
            StageMasterCtrl.OnCompleteStageClearDirect += LobbyCtrl.Instance.OpenReady;
        }
        else {

            GameSystem.Instance.PlayStage++;
            LobbyCtrl.Instance.OpenReady(GameSystem.Instance.PlayStage);

        }

        StopResultBGM();

    }
    
    private void DisableTopButton() {

        _arrButtons = GameObject.FindObjectsOfType<UIButton>();

        for (int i = 0; i < _arrButtons.Length; i++) {
            if (_arrButtons[i].name == "btnPlus")
                _arrButtons[i].enabled = false;

        }
    }

    private void EnableTopButton() {

        _arrButtons = GameObject.FindObjectsOfType<UIButton>();

        for (int i = 0; i < _arrButtons.Length; i++) {
            if (_arrButtons[i].name == "btnPlus")
                _arrButtons[i].enabled = true;

        }
    }


    #region 결과 Form 


    /// <summary>
    /// 결과 창 오픈
    /// </summary>
    private void ShowResultForm() {
        Debug.Log("▶  ShowResultForm");

        EnableTopButton();

        // 사운드 플레이
        PlayResultBGM();

        _lock.SetActive(true);
        _resultForm.SetActive(true);
        _resultButtons.SetActive(false);

        _resultForm.transform.localPosition = _initResultFormPos;
        _resultForm.transform.DOLocalMoveY(0, 0.5f);
        PlayResultFormPopUp(); // 사운드



        InitStageMissions();

        // 수치 처리 
        SetResultNumbers();

        // 버튼 설정

    }


    /// <summary>
    /// 스테이지 미션 정보 세팅 
    /// </summary>
    private void InitStageMissions() {
        _stageNode = GameSystem.Instance.StageDetailJSON[GameSystem.Instance.PlayStage -1];
        _userStageNode = GameSystem.Instance.UserStageJSON["stagelist"][GameSystem.Instance.PlayStage - 1];

        _debugStageNode = _stageNode.ToString();


        // 미션 정보 초기화 
        for(int i=0; i<_arrStageMissionCols.Length; i++) {

            // CloneUserStageJSON 사용
            _arrStageMissionCols[i].InitInResult(_stageNode["questid" + (i + 1).ToString()].AsInt, _stageNode["questvalue" + (i + 1).ToString()].AsInt, GameSystem.Instance.CloneUserStageJSON["progress" + (i + 1).ToString()].AsInt);
        }

        // 위치 조정 
        switch(GameSystem.Instance.InGameStageMissionCount) {
            case 1:
                _arrStageMissionCols[0].transform.localPosition = new Vector3(-230, 0, 0);
                break;
            case 2:
                _arrStageMissionCols[0].transform.localPosition = new Vector3(-230, 80, 0);
                _arrStageMissionCols[1].transform.localPosition = new Vector3(-230, -80, 0);
                break;
            case 3:
                _arrStageMissionCols[0].transform.localPosition = new Vector3(-230, 100, 0);
                _arrStageMissionCols[1].transform.localPosition = new Vector3(-230, 0, 0);
                _arrStageMissionCols[2].transform.localPosition = new Vector3(-230, -100, 0);
                break;
            case 4:
                _arrStageMissionCols[0].transform.localPosition = new Vector3(-230, 120, 0);
                _arrStageMissionCols[1].transform.localPosition = new Vector3(-230, 40, 0);
                _arrStageMissionCols[2].transform.localPosition = new Vector3(-230, -40, 0);
                _arrStageMissionCols[3].transform.localPosition = new Vector3(-230, -120, 0);
                break;
        }

    }

    /// <summary>
    /// ResultForm 수치 처리
    /// </summary>
    private void SetResultNumbers() {

        lblFlyingBadgeBonus.gameObject.SetActive(false);
        lblMaxComboBonus.gameObject.SetActive(false);

        spScoreIcon.gameObject.SetActive(false);
        lblScorePercent.gameObject.SetActive(false);

        // 뱃지 보너스
        lblBadgeBonus.text = System.Math.Round(GameSystem.Instance.UserNekoBadgeBonus, 3).ToString() + "%";

        // 최대 콤보
        lblMaxCombo.text = GameSystem.Instance.GetNumberToString(GameSystem.Instance.InGameMaxCombo);

        // 코인 
        lblCoin.text = GameSystem.Instance.GetNumberToString(GameSystem.Instance.InGameCoin);

		// 티켓 (-270, 125, -70)
        lblTicket.text = GameSystem.Instance.GetNumberToString(GameSystem.Instance.InGameTicket);

        // 고등어 
        lblChub.text = GameSystem.Instance.GetNumberToString(GameSystem.Instance.InGameChub);

        // 스테이지 
        //lblStage.text = GameSystem.Instance.InGameStage.ToString();
        lblStage.text = "STAGE " + GameSystem.Instance.PlayStage.ToString();
             

        // 최종 스코어 초기화 
        lblTotalScore.text = "0"; // 0으로 처리하고 
        StartCoroutine(PlusResultValue()); // 코루틴 시작 


        if (GameSystem.Instance.InGameTotalScore > GameSystem.Instance.UserPreBestScore) {

            Debug.Log("> >>>> > >> UpdateBestScoreCount Update");
            GameSystem.Instance.UpdateBestScoreCount = GameSystem.Instance.UpdateBestScoreCount + 1; // 베스트 스코어 갱신횟수 저장 
        }
        else {
            // 미갱신 횟수 증가 
            GameSystem.Instance.SaveESvalueInt(PuzzleConstBox.ES_NotRenewBestScore, GameSystem.Instance.LoadESvalueInt(PuzzleConstBox.ES_NotRenewBestScore) + 1);
            Debug.Log("▶ Can't Renew Best Score!");
            
        }

    }


    /// <summary>
    /// 각 네코 패시브, 보너스에 따른 점수 증가 처리 
    /// </summary>
    /// <returns>The result value.</returns>
    IEnumerator PlusResultValue() {

        int score = GameSystem.Instance.InGameScore;

        


        #region 미션 처리 


        // 이번 퍼즐에서의 진척도를 보여준다. 
        for (int i=0; i<GameSystem.Instance.InGameStageMissionCount; i++) {

            _arrStageMissionCols[i].SetStageMissionInResult( GameSystem.Instance.CloneUserStageJSON["progress" + (i+1).ToString()].AsInt);
            PlayStageMissionCol();

            yield return new WaitForSeconds(0.2f);

        }


        yield return new WaitForSeconds(1);

        #endregion

        // 기본 스코어 처리 
        StartCoroutine(IncreaseTotalScore());

        // 여기부터 각 변수 처리 
        yield return new WaitForSeconds(1.5f);

        #region 코인 
        if (GameSystem.Instance.NekoCoinPercent > 0) {
            spPlusCoinIcon.gameObject.SetActive(true);
            spPlusCoinIcon.transform.DOScale(1.2f, 0.5f).SetLoops(-1, LoopType.Yoyo);
            lblPlusCoin.text = "+" + GameSystem.Instance.NekoCoinPercent.ToString() + "%";

            lblCoin.text = GameSystem.Instance.GetNumberToString(GameSystem.Instance.InGameTotalCoin);

            if(_resultForm.activeSelf)
                PlayAbsorbCoin();
        }

        yield return new WaitForSeconds(0.2f);
        #endregion

        #region 네코 스코어 증가 패시브 
        score = score + GameSystem.Instance.InGamePlusScore;
        lblTotalScore.text = GameSystem.Instance.GetNumberToString(score);
        if (GameSystem.Instance.NekoScorePercent > 0) {
            spScoreIcon.gameObject.SetActive(true);
            lblScorePercent.gameObject.SetActive(true);
            lblScorePercent.text = "+" + GameSystem.Instance.NekoScorePercent.ToString() + "%";
            spScoreIcon.transform.DOScale(1.2f, 0.5f).SetLoops(-1, LoopType.Yoyo);
            if (_resultForm.activeSelf)
                PlayAbsorbCoin();
            yield return new WaitForSeconds(0.2f);
        }
        #endregion

        #region 최대 콤보 보너스 

        lblMaxComboBonus.text = "+" + GameSystem.Instance.GetNumberToString(GameSystem.Instance.InGameMaxCombo * 1000);
        lblMaxComboBonus.gameObject.SetActive(true);
        lblMaxComboBonus.transform.localPosition = new Vector3(260, -175, 0);
        lblMaxComboBonus.transform.DOKill();
        lblMaxComboBonus.transform.localScale = GameSystem.Instance.BaseScale;
        lblMaxComboBonus.transform.DOScale(1.2f, 0.2f).SetLoops(-1, LoopType.Yoyo);
        lblMaxComboBonus.transform.DOLocalMove(new Vector3(0, 380, 0), 0.5f).SetEase(Ease.InBack).OnComplete(OnCompleteMaxComboBonusMove);
        lblTotalScore.transform.DOShakeScale(0.5f, 1, 10, 90).OnComplete(OnCompleteShakeScore).SetDelay(0.5f);

        yield return new WaitForSeconds(0.5f);

        score += (GameSystem.Instance.InGameMaxCombo * 1000);
        lblTotalScore.text = GameSystem.Instance.GetNumberToString(score);

        yield return new WaitForSeconds(0.2f);
        #endregion

        #region 네코 뱃지 보너스
        if(GameSystem.Instance.UserNekoBadgeBonus > 0) {
            lblFlyingBadgeBonus.text = lblBadgeBonus.text;
            lblFlyingBadgeBonus.gameObject.SetActive(true);
            lblFlyingBadgeBonus.transform.localPosition = new Vector3(-260, -175, 0);
            lblFlyingBadgeBonus.transform.DOKill();
            lblFlyingBadgeBonus.transform.localScale = GameSystem.Instance.BaseScale;
            lblFlyingBadgeBonus.transform.DOScale(1.2f, 0.2f).SetLoops(-1, LoopType.Yoyo);
            lblFlyingBadgeBonus.transform.DOLocalMove(new Vector3(0, 380, 0), 0.5f).SetEase(Ease.InBack).OnComplete(OnCompleteFlyingBadgeBonus);
            lblTotalScore.transform.DOShakeScale(0.5f, 1, 10, 90).OnComplete(OnCompleteShakeScore).SetDelay(0.5f);

            yield return new WaitForSeconds(0.5f);

            score += GameSystem.Instance.InGameBadgeBonusScore;
            lblTotalScore.text = GameSystem.Instance.GetNumberToString(GameSystem.Instance.InGameTotalScore);
            yield return new WaitForSeconds(0.2f);
        }
        

        #endregion



        // Best 스코어 처리 
        /*
        if (GameSystem.Instance.InGameTotalScore > GameSystem.Instance.UserPreBestScore) {

            Debug.Log("> >>>> > >> Update Best Score");

            // 스프라이트 오브젝트 처리 
            objNewRecord.gameObject.SetActive(true);
            objNewRecord.DOScale(new Vector3(0.8f, 0.8f, 1), 1).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);

            // 최고스코어 갱신 사운드 재생 

            Invoke("PlayBestScore", 1f);

        }
        else {
            LobbyCtrl.Instance.CheckUnlockNekoLevelUpTip();
        }
        */

        // 최종 스테이지 성공 여부 
        if(GameSystem.Instance.InGameStageClear) {
            _resultEffect.SetClear();
        }
        else {
            _resultEffect.SetFail();
        }


        

        yield return new WaitForSeconds(1);

        if(GameSystem.Instance.InGameStageClear) {
            _clearGradeSprite.transform.localScale = new Vector3(2, 2, 2);
            _clearGradeSprite.gameObject.SetActive(true);
            _clearGradeSprite.transform.DOScale(1, 0.3f);

            _clearGradeLabel.text = GameSystem.Instance.UserStageJSON["laststage"].AsInt.ToString();

            if (GameSystem.Instance.UserStageJSON["laststate"].AsInt == 1) {
                _clearGradeSprite.spriteName = PuzzleConstBox.spriteBronzeClear;
                _clearGradeLabel.color = PuzzleConstBox.colorBronzeClear;
            }
            else if (GameSystem.Instance.UserStageJSON["laststate"].AsInt == 2) {
                _clearGradeSprite.spriteName = PuzzleConstBox.spriteSilverClear;
                _clearGradeLabel.color = PuzzleConstBox.colorSilverClear;
            }
            else if (GameSystem.Instance.UserStageJSON["laststate"].AsInt == 3) {
                _clearGradeSprite.spriteName = PuzzleConstBox.spriteGoldClear;
                _clearGradeLabel.color = PuzzleConstBox.colorGoldClear;
            }
            else if (GameSystem.Instance.UserStageJSON["laststate"].AsInt == 4) {
                _clearGradeSprite.spriteName = PuzzleConstBox.spriteDiaClear;
                _clearGradeLabel.color = PuzzleConstBox.colorDiaClear;
            }


            yield return new WaitForSeconds(0.3f);

            _clearGradeEffect.Play();

            yield return new WaitForSeconds(0.5f);
        }


        LobbyCtrl.Instance.CheckUnlockNekoLevelUpTip();


        _resultButtons.SetActive(true);
        //버튼 활성화 체크

        // 현재 진행 스테이지가 마지막으로 플레이했던 스테이지랑 같거나 크면 
        if(GameSystem.Instance.UserCurrentStage <= GameSystem.Instance.UserStageJSON["laststage"].AsInt) {
            _btnNextStage.gameObject.SetActive(false);  // 다음 스테이지 비활성화 
        }

        // 보스 스테이지 일때는 다음 스테이지 비활성화 
        if(GameSystem.Instance.PlayStage % 13 == 0) {
            _btnNextStage.gameObject.SetActive(false);
        }

        // 튜토리얼 진행 중에는 비활성화 
        if(GameSystem.Instance.TutorialComplete == 0) {
            _btnNextStage.SetActive(false);
            _btnRestart.SetActive(false);
        }

        
        

        // 빙고 체크 추가 
        if (BingoMasterCtrl.Instance.CheckExistsUncheckedFill()) {
            StopResultBGM();
            LobbyCtrl.Instance.OpenBingo();
        }

        //MNPFacebookCtrl.Instance.SubmitScore(GameSystem.Instance.InGameTotalScore);

    }


    /// <summary>
    /// 점수 올라가는 효과
    /// </summary>
    /// <returns></returns>
    IEnumerator IncreaseTotalScore() {

        Debug.Log(">>> IncreaseTotalScore");

        int totalScore = 0;

        // InGameScore 까지 주르륵 올라감 
        if (_resultForm.activeSelf)
            PlayIncreaseScoreSound(); // 사운드 처리 

        for (int i = 0; i < 20; i++) {
            totalScore += (int)(GameSystem.Instance.InGameScore * 0.05f);
            lblTotalScore.text = GameSystem.Instance.GetNumberToString(totalScore);

            yield return new WaitForSeconds(0.06f);
        }

        StopScoreSound(); // 사운드 종료 

        lblTotalScore.text = GameSystem.Instance.GetNumberToString(GameSystem.Instance.InGameScore);
        totalScore = GameSystem.Instance.InGameScore;
    }



    private void OnCompleteFlyingBadgeBonus() {
        lblFlyingBadgeBonus.gameObject.SetActive(false);


        if (_resultForm.activeSelf)
            PlayAbsorbCoin();
    }

  

    private void OnCompleteMaxComboBonusMove() {

        lblMaxComboBonus.gameObject.SetActive(false);


        if (_resultForm.activeSelf)
            PlayAbsorbCoin();
    }

    private void OnCompleteShakeScore() {
        lblTotalScore.transform.localScale = PuzzleConstBox.baseScale;
    }



    #endregion


    #region 사운드 플레이
    /// <summary>
    /// 병 등장 효과음 
    /// </summary>
    private void PlayResultBottle() {
        resultAudioSource.PlayOneShot(SoundConstBox.acResultBottle);
    }

    public void PlayBestScore() {
        if (_resultForm.activeSelf)
            resultAudioSource.PlayOneShot(SoundConstBox.acBestScore);
    }

    /// <summary>
    /// 결과네코 튕기는 소리 플레이
    /// </summary>
    public void PlayResultNekoPing() {

        resultAudioSource.PlayOneShot(SoundConstBox.acPing[Random.Range(0, SoundConstBox.acPing.Length)]);
    }

    /// <summary>
    ///  뚜껑 닫히는 소리 
    /// </summary>
    public void PlayCap() {
        resultAudioSource.PlayOneShot(SoundConstBox.acCap);
    }

    public void PlayResultBGM() {

        resultAudioSource.PlayOneShot(SoundConstBox.acResultFormBGM);
    }

    private void StopResultBGM() {
        resultAudioSource.Stop();
    }


    public void PlayAbsorbCoin() {
        resultAudioSource.PlayOneShot(SoundConstBox.acCoinAbsorb);
    }

    private void PlayIncreaseScoreSound() {
        scoreAudioSource.Play();
    }
    public void PlayResultFormPopUp() {
        resultAudioSource.PlayOneShot(_clipResultFormPopUp);
    }

    public void PlayStageMissionCol() {
        resultAudioSource.PlayOneShot(_clipStageMissionCol);
    }

    private void StopScoreSound() {
        scoreAudioSource.Stop();
    }


    public void PlayStageClear() {
        resultAudioSource.PlayOneShot(_clipStageMissionClear);
    }

    public void PlayStageFail() {
        resultAudioSource.PlayOneShot(_clipStageMissionFail);
    }


    #endregion

}
