using UnityEngine;
using System.Collections;
using DG.Tweening;

public class EventPageCtrl : MonoBehaviour {

    [SerializeField] UIAtlas _comAtlas;

    Vector3 _originNekoPos = new Vector3(0, 100, 0);

    // 네코들 
    [SerializeField] Transform _watermelonNeko;
    [SerializeField] Transform _sunburnNeko;
    [SerializeField] Transform _watermelonAura;
    [SerializeField] Transform _sunburnAura;

    [SerializeField] UILabel _lblWaterMelonProgress;
    [SerializeField] UIProgressBar _progressWaterMelon;
    [SerializeField] UILabel _lblWaterMelonMissionDetail;

    [SerializeField] UIButton _btnWaterMelonReward;

    [SerializeField] UILabel _lblSunBurnProgress;
    [SerializeField] UIProgressBar _progressSunBurn;
    [SerializeField] UILabel _lblSunBurnMissionDetail;

    [SerializeField] UIButton _btnSunBurnReward;


    [SerializeField] UISprite _watermelonRewardIcon;
    [SerializeField] UISprite _sunburnRewardIcon;


    int _currentWaterMelonStepGoal = 0;
    int _currentSunBurnStepGoal = 0;

    [SerializeField] int _currentWaterMelonStep = 0;
    [SerializeField] int _currentSunBurnStep = 0;

    [SerializeField] int _currentWaterMelonCatch = 0;
    [SerializeField] int _currentSunBurnCatch = 0;


    [SerializeField] UILabel _lblWaterMelonComplete;
    [SerializeField] UILabel _lblSunBurnComplete;

    float _progressValue;

    // Use this for initialization
    void Start () {
	
	}

    void OnEnable() {

        _watermelonAura.DOKill();
        _watermelonAura.localEulerAngles = Vector3.zero;
        _watermelonAura.DORotate(new Vector3(0, 0, -720), 4, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);

        _sunburnAura.DOKill();
        _sunburnAura.localEulerAngles = Vector3.zero;
        _sunburnAura.DORotate(new Vector3(0, 0, -720), 4, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);

        InitEventPage();
    }

    /// <summary>
    /// 페이지 초기화 
    /// </summary>
    private void InitEventPage() {


        _currentWaterMelonStep = GameSystem.Instance.LoadESvalueInt(PuzzleConstBox.ES_EventWaterMelonStep);
        _currentSunBurnStep = GameSystem.Instance.LoadESvalueInt(PuzzleConstBox.ES_EventSunBurnStep);

        
        _currentWaterMelonCatch = GameSystem.Instance.LoadESvalueInt(PuzzleConstBox.ES_EventWaterMelonCatch);
        _currentSunBurnCatch = GameSystem.Instance.LoadESvalueInt(PuzzleConstBox.ES_EventSunBurnCatch);

        _progressWaterMelon.gameObject.SetActive(true);
        _progressSunBurn.gameObject.SetActive(true);


        // 목표치 설정 
        //_currentWaterMelonStepGoal = GameSystem.Instance.DocsTrepassEvent.get<int>("watermelon" + _currentWaterMelonStep.ToString(), "catchgoal");
        //_currentSunBurnStepGoal = GameSystem.Instance.DocsTrepassEvent.get<int>("sunburn" + _currentSunBurnStep.ToString(), "catchgoal");
        int.TryParse(GetTrespassEventInfo("watermelon" + _currentWaterMelonStep.ToString(), "catchgoal"), out _currentWaterMelonStepGoal);
        int.TryParse(GetTrespassEventInfo("sunburn" + _currentSunBurnStep.ToString(), "catchgoal"), out _currentSunBurnStepGoal);


        // 프로그레스바 레이블 설정 
        _lblWaterMelonProgress.text = _currentWaterMelonCatch + "/" + _currentWaterMelonStepGoal;
        _lblSunBurnProgress.text = _currentSunBurnCatch + "/" + _currentSunBurnStepGoal;

        // 프로그레스바 값 설정
        _progressValue = (float)_currentWaterMelonCatch / (float)_currentWaterMelonStepGoal;
        _progressWaterMelon.value = _progressValue;
        _progressValue = (float)_currentSunBurnCatch / (float)_currentSunBurnStepGoal;
        _progressSunBurn.value = _progressValue;


        // 목표 달성시, 보상 버튼이 등장 
        if(_currentWaterMelonCatch >= _currentWaterMelonStepGoal) {
            _btnWaterMelonReward.gameObject.SetActive(true);
        }
        else {
            _btnWaterMelonReward.gameObject.SetActive(false);
        }

        if (_currentSunBurnCatch >= _currentSunBurnStepGoal) {
            _btnSunBurnReward.gameObject.SetActive(true);
        }
        else {
            _btnSunBurnReward.gameObject.SetActive(false);
        }

        // 미션 디테일 설정 
        SetRewardIcon(_watermelonRewardIcon, _lblWaterMelonMissionDetail, "watermelon" + _currentWaterMelonStep.ToString(), _currentWaterMelonStep);
        SetRewardIcon(_sunburnRewardIcon, _lblSunBurnMissionDetail, "sunburn" + _currentSunBurnStep.ToString(), _currentSunBurnStep);


        // 최종 완결 처리 
        if (_currentWaterMelonStep > 20) {
            _btnWaterMelonReward.gameObject.SetActive(false);
            _watermelonRewardIcon.gameObject.SetActive(false);
            _lblWaterMelonMissionDetail.gameObject.SetActive(false);
            _progressWaterMelon.gameObject.SetActive(false);

            _lblWaterMelonComplete.gameObject.SetActive(true);
        }


        if (_currentSunBurnStep > 20) {
            _btnSunBurnReward.gameObject.SetActive(false);
            _sunburnRewardIcon.gameObject.SetActive(false);
            _lblSunBurnMissionDetail.gameObject.SetActive(false);
            _progressSunBurn.gameObject.SetActive(false);

            _lblSunBurnComplete.gameObject.SetActive(true);

        }

    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="pID"></param>
    /// <param name="pCol"></param>
    private string GetTrespassEventInfo(string pID, string pCol) {

        for(int i=0; i<GameSystem.Instance.TrespassRewardJSON.Count; i++) {

            // 행을 맞추고, col값을 리턴 
            if(GameSystem.Instance.TrespassRewardJSON[i]["id"].Value.Equals(pID)) {
                return GameSystem.Instance.TrespassRewardJSON[i][pCol].Value;
            }

        }

        return string.Empty;
    }


    private void SetRewardIcon(UISprite pSprite, UILabel pLabel, string pType, int pStep) {

        if (pStep > 20)
            return;

        string rewardtype = GetTrespassEventInfo(pType, "rewardtype");
        string rewardvalue = GetTrespassEventInfo(pType, "rewardvalue");
        string extrainfo = GetTrespassEventInfo(pType, "extrainfo");

        //string extrainfo = GameSystem.Instance.DocsTrepassEvent.get<string>(pType, "extrainfo");

        pLabel.text = GameSystem.Instance.GetNumberToString(int.Parse(rewardvalue));

        switch (rewardtype) {

            case "gem":
                pSprite.atlas = _comAtlas;
                pSprite.spriteName = "i-zam2";
                break;
            case "coin":
                pSprite.atlas = _comAtlas;
                pSprite.spriteName = PuzzleConstBox.spriteUIGoldMark;
                break;
            case "chub":
                pSprite.atlas = _comAtlas;
                pSprite.spriteName = PuzzleConstBox.spriteUIChubMark;
                break;
            case "tuna":
                pSprite.atlas = _comAtlas;
                pSprite.spriteName = PuzzleConstBox.spriteUITunaMark;
                break;
            case "salmon":
                pSprite.atlas = _comAtlas;
                pSprite.spriteName = PuzzleConstBox.spriteUISalmonMark;
                break;

            case "freeticket":
                pSprite.atlas = _comAtlas;
                pSprite.spriteName = PuzzleConstBox.spriteUIFreeTicket;
                break;
            case "rareticket":
                pSprite.atlas = _comAtlas;
                pSprite.spriteName = PuzzleConstBox.spriteUIRareTicket;
                break;
            case "rainbowticket":
                pSprite.atlas = _comAtlas;
                pSprite.spriteName = PuzzleConstBox.spriteUIRainbowTicket;
                break;


            case "neko":
                GameSystem.Instance.SetNekoSprite(pSprite, int.Parse(rewardvalue), int.Parse(extrainfo));
                pLabel.text = GameSystem.Instance.GetNekoName(int.Parse(rewardvalue), int.Parse(extrainfo));
                break;

        }
        

        


    }


    /// <summary>
    /// 초기화 
    /// </summary>
    public void OnClickResetMission() {
        
        GameSystem.Instance.SaveESvalueInt(PuzzleConstBox.ES_EventSunBurnCatch, 5000);
        GameSystem.Instance.SaveESvalueInt(PuzzleConstBox.ES_EventWaterMelonCatch, 5000);
        //GameSystem.Instance.SaveESvalueInt(PuzzleConstBox.ES_EventSunBurnStep, 0);
        //GameSystem.Instance.SaveESvalueInt(PuzzleConstBox.ES_EventWaterMelonStep, 0);

        InitEventPage();
    }

    public void OnClickRewardWaterMelon() {
        if (_currentWaterMelonCatch < _currentWaterMelonStepGoal) {
            return;
        }

        

        // 보상 요청 처리. 
        // 파라매터 생성 
        string param;

        //GetTrespassEventInfo("watermelon" + _currentWaterMelonStep.ToString(), "rewardtype")

        param = "watermelon" + _currentWaterMelonStep.ToString() + "|"
            + GetTrespassEventInfo("watermelon" + _currentWaterMelonStep.ToString(), "rewardtype") + "|"
            + GetTrespassEventInfo("watermelon" + _currentWaterMelonStep.ToString(), "rewardvalue") + "|"
            + GetTrespassEventInfo("watermelon" + _currentWaterMelonStep.ToString(), "extrainfo") + "|"
            + (_currentWaterMelonStep + 1).ToString();

        /*
        param = "watermelon" + _currentWaterMelonStep.ToString() + "|"
            + GameSystem.Instance.DocsTrepassEvent.get<string>("watermelon" + _currentWaterMelonStep.ToString(), "rewardtype") + "|"
            + GameSystem.Instance.DocsTrepassEvent.get<string>("watermelon" + _currentWaterMelonStep.ToString(), "rewardvalue") + "|"
            + GameSystem.Instance.DocsTrepassEvent.get<string>("watermelon" + _currentWaterMelonStep.ToString(), "extrainfo") + "|"
            + (_currentWaterMelonStep+1).ToString();
        */

        // 통신 처리 
        GameSystem.Instance.Post2Trespass(param, this.OnCompleteRewardWaterMelon);

    }

    /// <summary>
    /// 미션 보상 콜백 
    /// </summary>
    private void OnCompleteRewardWaterMelon() {
        // 초과한 수치는 이어받을 수 있도록 처리
        _currentWaterMelonStep++;
        GameSystem.Instance.SaveESvalueInt(PuzzleConstBox.ES_EventWaterMelonCatch, _currentWaterMelonCatch - _currentWaterMelonStepGoal);
        GameSystem.Instance.SaveESvalueInt(PuzzleConstBox.ES_EventWaterMelonStep, _currentWaterMelonStep);

        InitEventPage();

        LobbyCtrl.Instance.PlayEffect(SoundConstBox.acNekoLevelUp);

        /*
        _watermelonAura.DOKill();
        _watermelonAura.localEulerAngles = Vector3.zero;
        _watermelonAura.DOLocalRotate(new Vector3(0, 0, -360), 1, RotateMode.FastBeyond360).SetEase(Ease.Linear);
        */

        _watermelonNeko.DOKill();
        _watermelonNeko.localPosition = _originNekoPos;
        _watermelonNeko.DOLocalJump(_watermelonNeko.localPosition, _watermelonNeko.localPosition.y + 100, 1, 0.5f);

        LobbyCtrl.Instance.ForceSetMailBoxNew();

    }


    public void OnClickRewardSunBurn() {
        if (_currentSunBurnCatch < _currentSunBurnStepGoal) {
            return;
        }

        
        
        // 보상 요청 처리. 
        // 파라매터 생성 
        string param;

        param = "sunburn" + _currentSunBurnStep.ToString() + "|"
            + GetTrespassEventInfo("sunburn" + _currentSunBurnStep.ToString(), "rewardtype") + "|"
            + GetTrespassEventInfo("sunburn" + _currentSunBurnStep.ToString(), "rewardvalue") + "|"
            + GetTrespassEventInfo("sunburn" + _currentSunBurnStep.ToString(), "extrainfo") + "|"
            + (_currentSunBurnStep+1).ToString();


        // 통신 처리 
        GameSystem.Instance.Post2Trespass(param, this.OnCompleteRewardSunburn);


    }


    /// <summary>
    /// 미션 보상 콜백 
    /// </summary>
    private void OnCompleteRewardSunburn() {
        _currentSunBurnStep++;
        GameSystem.Instance.SaveESvalueInt(PuzzleConstBox.ES_EventSunBurnCatch, _currentSunBurnCatch - _currentSunBurnStepGoal);
        GameSystem.Instance.SaveESvalueInt(PuzzleConstBox.ES_EventSunBurnStep, _currentSunBurnStep);

        InitEventPage();


        LobbyCtrl.Instance.PlayEffect(SoundConstBox.acNekoLevelUp);

        /*
        _sunburnNeko.DOKill();
        _sunburnNeko.localEulerAngles = Vector3.zero;
        _sunburnNeko.DOLocalRotate(new Vector3(0, 0, -360), 1f, RotateMode.FastBeyond360).SetEase(Ease.Linear);
        */

        _sunburnNeko.DOKill();
        _sunburnNeko.localPosition = _originNekoPos;
        _sunburnNeko.DOLocalJump(_sunburnNeko.localPosition, _sunburnNeko.localPosition.y + 100, 1, 0.5f);

        LobbyCtrl.Instance.ForceSetMailBoxNew();

    }


    /// <summary>
    /// 
    /// </summary>
    public void OpenHelpPage() {
        for(int i =0; i<GameSystem.Instance.NoticeBannerInitJSON.Count; i++) {
            if(GameSystem.Instance.NoticeBannerInitJSON[i]["action"].Value == "trespass") {
                this.GetComponent<LobbyCommonUICtrl>().CloseSelf();
                WindowManagerCtrl.Instance.OpenNoticeDetail(i);
            }
        }
    }
}
