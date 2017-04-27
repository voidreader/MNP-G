using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using SimpleJSON;
using Google2u;

public class ReadyGroupCtrl : MonoBehaviour {


    static ReadyGroupCtrl _instance = null;

    [SerializeField]
    int _stage;

    [SerializeField] 
    UILabel _lblTitle;


    [SerializeField]
    private int _selectedEquipNekoIndex = -1;

    [SerializeField] PopUpgradeCtrl _upgradePopUp;

    [SerializeField] GameObject _EquipNekoSet;

    [SerializeField] EquipItemCtrl[] _arrEquipItem = new EquipItemCtrl[4]; // 부스트 아이템 
    [SerializeField] EquipNekoCtrl[] _arrEquipNeko = new EquipNekoCtrl[3]; // 고양이 
    [SerializeField] UILabel _lblMissionTitle;
    [SerializeField] StageMissionColCtrl[] _arrStageMissionCols = new StageMissionColCtrl[4];

    

    
    [SerializeField] UILabel lblEquipItem;
    [SerializeField] UIButton _startBtn;

    [SerializeField]
    bool _isTouchLock = false;


    // Power Level
    [SerializeField] UILabel _lblPowerLevel;
    [SerializeField] UILabel _lblPowerValue;
    [SerializeField] UILabel _lblPowerUpgrade;
    [SerializeField] UISprite _spriteFruit;


    // Equip Bonus
    [SerializeField] EquipBonusCtrl[] _arrEquipBonus;


    Vector3 _equipBonusOnePos = new Vector3(0, 385, 0);
    Vector3[] _equipBonusTwoPos = { new Vector3(80, 385, 0), new Vector3(-80, 385, 0) };
    Vector3[] _equipBonusThreePos = { new Vector3(0, 385, 0), new Vector3(-150, 385, 0), new Vector3(150, 385, 0) };
    Vector3[] _equipBonusFourPos = { new Vector3(-225, 385, 0), new Vector3(-75, 385, 0), new Vector3(75, 385, 0), new Vector3(225, 385, 0) };
    Vector3[] _equipBonusFivePos = { new Vector3(-225, 385, 0), new Vector3(-75, 385, 0), new Vector3(75, 385, 0), new Vector3(225, 385, 0), new Vector3(240, -340, 0) };

    [SerializeField]
    GameObject _signHotTime;


        // 네코 패시브 정보
    [SerializeField] List<MyNekoPassiveCtrl> _listNekoPassive = new List<MyNekoPassiveCtrl>();
    [SerializeField]
    int _passvieCount = 0;

    bool _onStartingGame = false;

    JSONNode _currentStage;
    [SerializeField]
    string _debugCurrentStage;


    public static ReadyGroupCtrl Instance {
        get {
            if (_instance == null) {
                _instance = FindObjectOfType(typeof(ReadyGroupCtrl)) as ReadyGroupCtrl;

                if (_instance == null) {
                    Debug.Log("ReadyGroupCtrl Init Error");
                    return null;
                }
            }

            return _instance;
        }
    }

    /// <summary>
    /// 게임 시작 효과 처리 
    /// </summary>
    public bool OnStartingGame {
        get {
            return _onStartingGame;
        }

        set {
            _onStartingGame = value;
        }
    }

    #region Properties 

    public bool IsTouchLock {
        get {
            return _isTouchLock;
        }

        set {
            _isTouchLock = value;
        }
    }

    public int SelectedEquipNekoIndex {
        get {
            return _selectedEquipNekoIndex;
        }

        set {
            _selectedEquipNekoIndex = value;
        }
    }

    #endregion

    // Use this for initialization
    void Start () {
        //UpdateEquipNeko();
    }


    #region Equip Bonus 



    /// <summary>
    /// 고양이 장착 보너스 처리 
    /// </summary>
    void InitEquipBonus() {

        for(int i=0; i<_arrEquipBonus.Length; i++) {
            _arrEquipBonus[i].gameObject.SetActive(false);
        }
    }


    /// <summary>
    /// 고양이 장착 보너스 Refresh
    /// </summary>
    public void RefreshEquipBonus() {

        return;

        _passvieCount = 0;
        GameSystem.Instance.InitInGameAccquireInfo();

        for (int i = 0; i < GameSystem.Instance.ListEquipNekoID.Count; i++) {
            _listNekoPassive[i].SetNekoPassive(GameSystem.Instance.ListEquipNekoID[i]);
        }

        // 스킬 합산 처리 
        for (int i = 0; i < _listNekoPassive.Count; i++) {

            if (_listNekoPassive[i].IsScorePlus) { // 스코어 상승 

                if(GameSystem.Instance.NekoScorePercent == 0)
                    _passvieCount++;

                GameSystem.Instance.NekoScorePercent += _listNekoPassive[i].ScorePlus;
                
            }

            if (_listNekoPassive[i].IsCoinPlus) { // 획득 코인 상승 


                if (GameSystem.Instance.NekoCoinPercent == 0)
                    _passvieCount++;

                GameSystem.Instance.NekoCoinPercent += _listNekoPassive[i].CoinPlus;
            }

            if (_listNekoPassive[i].IsPowerPlus) { // 파워 상승 

                if (GameSystem.Instance.NekoPowerPlus == 0)
                    _passvieCount++;

                GameSystem.Instance.NekoPowerPlus += _listNekoPassive[i].PowerPlus;
            }

            if (_listNekoPassive[i].IsGameTimePlus) { // 게임 플레이 시간 상승 

                if (GameSystem.Instance.NekoGameTimePlus == 0)
                    _passvieCount++;

                GameSystem.Instance.NekoGameTimePlus += _listNekoPassive[i].GameTimePlus;
            }

            if (_listNekoPassive[i].IsStartBomb) { // 시작 폭탄

                if (GameSystem.Instance.NekoStartBombCount == 0)
                    _passvieCount++;

                GameSystem.Instance.NekoStartBombCount += _listNekoPassive[i].StartBombCount;
            }


            // 폭탄 아이템 게이지 상승(합산 처리 X)
            if (_listNekoPassive[i].IsBombAppearBonus) {

                if (GameSystem.Instance.NekoBombAppearBonus == 0)
                    _passvieCount++;

                GameSystem.Instance.NekoBombAppearBonus += _listNekoPassive[i].BombAppearPlus;
            }

            // 네코 스킬 게이지 획득 상승(합산 처리 X)
            if (_listNekoPassive[i].IsSkillInvokeBonus) {

                if (GameSystem.Instance.NekoSkillInvokeBonus == 0)
                    _passvieCount++;

                GameSystem.Instance.NekoSkillInvokeBonus += _listNekoPassive[i].NekoSkillInvokePlus;
            }
        }


        // passvie Count 만큼 루프 
        for(int i= 0; i<_passvieCount; i++) {

            // 순차적으로 체크 하고 continue; 
            if(GameSystem.Instance.NekoScorePercent > 0) {
                _arrEquipBonus[i].SetEquipBonus(1, GameSystem.Instance.NekoScorePercent);

                GameSystem.Instance.NekoScorePercent = 0;

                continue;
            }
            
            if(GameSystem.Instance.NekoCoinPercent > 0) {
                _arrEquipBonus[i].SetEquipBonus(2, GameSystem.Instance.NekoCoinPercent);
                GameSystem.Instance.NekoCoinPercent = 0;
                continue;
            }

            if (GameSystem.Instance.NekoPowerPlus > 0) {
                _arrEquipBonus[i].SetEquipBonus(3, GameSystem.Instance.NekoPowerPlus);
                GameSystem.Instance.NekoPowerPlus = 0;
                continue;
            }

            if(GameSystem.Instance.NekoGameTimePlus > 0) {
                _arrEquipBonus[i].SetEquipBonus(4, GameSystem.Instance.NekoGameTimePlus);
                GameSystem.Instance.NekoGameTimePlus = 0;
                continue;
            }

            if(GameSystem.Instance.NekoStartBombCount > 0) {
                _arrEquipBonus[i].SetEquipBonus(5, GameSystem.Instance.NekoStartBombCount);
                GameSystem.Instance.NekoStartBombCount = 0;
                continue;
            }

            if (GameSystem.Instance.NekoBombAppearBonus > 0) {
                _arrEquipBonus[i].SetEquipBonus(6, GameSystem.Instance.NekoBombAppearBonus);
                GameSystem.Instance.NekoBombAppearBonus = 0;
                continue;
            }

            if (GameSystem.Instance.NekoSkillInvokeBonus > 0) {
                _arrEquipBonus[i].SetEquipBonus(7, GameSystem.Instance.NekoSkillInvokeBonus);
                GameSystem.Instance.NekoSkillInvokeBonus = 0;
                continue;
            }

        }

        // 위치 지정 
        if(_passvieCount == 1) {
            _arrEquipBonus[0].SetPosition(_equipBonusOnePos);
        }
        else if (_passvieCount == 2) {
            _arrEquipBonus[0].SetPosition(_equipBonusTwoPos[0]);
            _arrEquipBonus[1].SetPosition(_equipBonusTwoPos[1]);
        }
        else if (_passvieCount == 3) {
            _arrEquipBonus[0].SetPosition(_equipBonusThreePos[0]);
            _arrEquipBonus[1].SetPosition(_equipBonusThreePos[1]);
            _arrEquipBonus[2].SetPosition(_equipBonusThreePos[2]);
        }
        else if (_passvieCount == 4) {
            _arrEquipBonus[0].SetPosition(_equipBonusFourPos[0]);
            _arrEquipBonus[1].SetPosition(_equipBonusFourPos[1]);
            _arrEquipBonus[2].SetPosition(_equipBonusFourPos[2]);
            _arrEquipBonus[3].SetPosition(_equipBonusFourPos[3]);
        }
        else if (_passvieCount == 5) {
            _arrEquipBonus[0].SetPosition(_equipBonusFivePos[0]);
            _arrEquipBonus[1].SetPosition(_equipBonusFivePos[1]);
            _arrEquipBonus[2].SetPosition(_equipBonusFivePos[2]);
            _arrEquipBonus[3].SetPosition(_equipBonusFivePos[3]);
            _arrEquipBonus[4].SetPosition(_equipBonusFivePos[4]);
        }


    }
    #endregion


    /// <summary>
    /// 스테이지 게임 설정 (준비화면 오픈)
    /// </summary>
    public void SetStageGame(int pStage) {
        _stage = pStage;

        _lblTitle.text = "Stage 「" + _stage + "」";

        // 플레이 스테이지 설정
        GameSystem.Instance.PlayStage = _stage;
        _currentStage = GameSystem.Instance.GetStageNode(pStage);
        _debugCurrentStage = _currentStage.ToString();

        
        for(int i=0; i<_arrStageMissionCols.Length; i++) {
            _arrStageMissionCols[i].SetDisable();
        }

        /* 스테이지 정보 세팅 */


        // 미션 정보
        _lblMissionTitle.text = GameSystem.Instance.GetLocalizeText(MNP_Localize.rowIds.L4339) + "(" + _currentStage["time"].AsInt.ToString() + GameSystem.Instance.GetLocalizeText(MNP_Localize.rowIds.L3412) + ")";
        _arrStageMissionCols[0].SetMissionInfo(_currentStage["questid1"].AsInt, _currentStage["questvalue1"].AsInt);
        _arrStageMissionCols[1].SetMissionInfo(_currentStage["questid2"].AsInt, _currentStage["questvalue2"].AsInt);
        _arrStageMissionCols[2].SetMissionInfo(_currentStage["questid3"].AsInt, _currentStage["questvalue3"].AsInt);
        _arrStageMissionCols[3].SetMissionInfo(_currentStage["questid4"].AsInt, _currentStage["questvalue4"].AsInt);

        SetNormalGame();
    }

    void OnEnable() {

        //if(GameSystem.Instance.ListEquipNekoID.Count <)
        CheckCoinHotTime();

        InitEquipBonus();

        

        for (int i = 0; i < 3; i++) {
            _arrEquipNeko[i].InitEquipNekoIndex(i); // 인덱스 설정 

            if(GameSystem.Instance.ListEquipNekoID.Count < i+1) {
                _arrEquipNeko[i].SetNekoInfo(-1);
            }
            else {
                _arrEquipNeko[i].SetNekoInfo(GameSystem.Instance.ListEquipNekoID[i]);
            }
        }
       

        for (int i = 0; i < 4; i++) {
            _arrEquipItem[i].SetEquipItem(i);
        }

        RefreshPower();
        RefreshEquipBonus();
        CheckReadyUnlock();
    }




    void OnDisable() {

        if (OnStartingGame)
            return;

        try {
            for (int i = 0; i < 4; i++) {
                if (_arrEquipItem[i].IsClicked)
                    _arrEquipItem[i].ClickItem();
            }
        }
        catch(System.Exception ex) {
            Debug.Log(ex.StackTrace);
        }
    }


    /// <summary>
    /// 부스트 아이템 텍스트 설정 
    /// </summary>
    /// <param name="pItemIndex">P item index.</param>
    public void SetEquipItemText(int pItemIndex) {

        if (pItemIndex == 0) {
            // 플레이 시간 5초 추가 
            lblEquipItem.text = GameSystem.Instance.GetLocalizeText(MNP_Localize.rowIds.L3071);

        }
        else if (pItemIndex == 1) {
            // 폭탄블록 생성에 필요한 블록 감소 
            lblEquipItem.text = GameSystem.Instance.GetLocalizeText(MNP_Localize.rowIds.L3072);
        }
        else if (pItemIndex == 2) {
            // 고양이 스페셜 어택 블록 감소 
            lblEquipItem.text = GameSystem.Instance.GetLocalizeText(MNP_Localize.rowIds.L3073);
        }
        else if (pItemIndex == 3) {
            // 게임시작과 함께 피버가 발생 
            lblEquipItem.text = GameSystem.Instance.GetLocalizeText(MNP_Localize.rowIds.L3074);
        }
        else {
            lblEquipItem.text = GameSystem.Instance.GetLocalizeText(MNP_Localize.rowIds.L3037);
        }
    }



    /// <summary>
    /// 고양이 장착 정보 세팅 
    /// </summary>
    private void SetEquipNekoInfo() {

        GameSystem.Instance.CheckEquipNekoInfo();

        for (int i = 0; i < 3; i++) {
            _arrEquipNeko[i].SetNekoInfo(GameSystem.Instance.ListEquipNekoID[i]);
        }
    }

    /// <summary>
    /// 레디창의 장착 고양이에 대한 정보 갱신
    /// </summary>
    public void UpdateEquipNeko() {
        for (int i = 0; i < 3; i++) {
            _arrEquipNeko[i].SetNekoInfo(_arrEquipNeko[i].NekoID);
        }
    }



    /// <summary>
    /// 장착 고양이 리프레시 
    /// </summary>
    public void RefreshEquipNeko() {
        for (int i = 0; i < GameSystem.Instance.ListEquipNekoID.Count; i++) {
            _arrEquipNeko[i].SetNekoInfo(GameSystem.Instance.ListEquipNekoID[i]);
        }
    }



    public void SetNormalGame() {
		_EquipNekoSet.gameObject.SetActive (true);
		
	}


    /// <summary>
    /// 체크 핫타임 
    /// </summary>
    private void CheckCoinHotTime() {

        _signHotTime.SetActive(GameSystem.Instance.IsHotTime);

 
    }


    /// <summary>
    /// 아이템 언락 처리 
    /// </summary>
    public void UnlockItems() {
        for (int i = 0; i < _arrEquipItem.Length; i++) {
            _arrEquipItem[i].UnlockItem();
        }
    }



    /// <summary>
    /// 준비화면의 Unlock 체크 
    /// </summary>
    public void CheckReadyUnlock() {
        // 잠금 해제 체크(부스트 아이템 스테이지 7)
        if (GameSystem.Instance.UserCurrentStage >= 7 && GameSystem.Instance.CheckItemUnlockProceed()) {
            LobbyCtrl.Instance.CheckItemUnlockProceed();
            return;
        }

        if (GameSystem.Instance.UserCurrentStage >= 5 && GameSystem.Instance.CheckPassiveUnlockProceed()) {
            //CheckPassiveUnlockProceed();
            return;
        }
    }



    /// <summary>
    /// Starts the game.
    /// </summary>
    public void StartGame() {

        

        // 장착 네코 체크 (일반게임만)
        if (_arrEquipNeko[0].NekoID < 0 && _arrEquipNeko[1].NekoID < 0 && _arrEquipNeko[2].NekoID < 0) {
            LobbyCtrl.Instance.OpenInfoPopUp(PopMessageType.EquipNekoNeed);
            return;
        }

        GameSystem.Instance.ListEquipNekoID.Clear();

        // 세팅한 고양이를 시스템  정보에 입력
        for (int i = 0; i < _arrEquipNeko.Length; i++) {

            if(GameSystem.Instance.ListEquipNekoID.Contains(_arrEquipNeko[i].NekoID)) {
                GameSystem.Instance.ListEquipNekoID.Add(-1);
            }
            else {
                GameSystem.Instance.ListEquipNekoID.Add(_arrEquipNeko[i].NekoID);
            }
        }


        if (IsTouchLock)
            return;

        // 호출전에 부스트 아이템 장착 체킹 한번더. 
        for (int i = 0; i < _arrEquipItem.Length; i++) {

            // FillColumn이 체크되지 않은경우는 리스트에서 제거 
            if (!_arrEquipItem[i].GetFillColumnActive()) {
                GameSystem.Instance.ListEquipItemID.Remove(_arrEquipItem[i].EquipItemIndex);
            }
        }


        


        // 튜토리얼의 경우에는 하트 소비를 하지 않음.
        if (GameSystem.Instance.TutorialComplete == 0) {
            GameSystem.Instance.DoGameStart();
            IsTouchLock = true;
        }
        else {
            GameSystem.Instance.Post2GameStart();
        }

    }



    /// <summary>
    /// 준비창에서 선택된 아이템들의 코인 값.
    /// </summary>
    /// <returns></returns>
    public int GetSelectedEquipItemCoinValue() {

        int value = 0;

        for (int i = 0; i < 3; i++) {
            if (_arrEquipItem[i].IsClicked)
                value += _arrEquipItem[i].Price;
        }

        return value;
    }


    /// <summary>
    /// 장착 네코의 ID 구하기 
    /// </summary>
    /// <param name="pIndex"></param>
    /// <returns></returns>
    public int GetEquipedNekoID() {
        return _arrEquipNeko[SelectedEquipNekoIndex].NekoID;
    }



    /// <summary>
    /// 선택한 네코를 장착
    /// </summary>
    /// <param name="pIndex"></param>
    /// <param name="pNeko"></param>
    public void SetNekoEquip(int pIndex, OwnCatCtrl pNeko) {
        _arrEquipNeko[pIndex].SetNekoInfo(pNeko);
    }

    public void SetNekoEquipOnCurrentSlot(OwnCatCtrl pNeko) {
        _arrEquipNeko[SelectedEquipNekoIndex].SetNekoInfo(pNeko);
    }



    /// <summary>
    /// 업그레이드 팝업 열기
    /// </summary>
    public void OpenUpgradePopUp() {
        _upgradePopUp.gameObject.SetActive(true);
    }


    /// <summary>
    /// 파워 정보 Refresh
    /// </summary>
    public void RefreshPower() {
        _lblPowerLevel.text = GameSystem.Instance.UserPowerLevel.ToString();
        _lblPowerValue.text = GameSystem.Instance.BlockAttackPower.ToString();

        _lblPowerUpgrade.text = "(" + GameSystem.Instance.UserPowerLevel.ToString() + "/10)";

        _spriteFruit.spriteName = PuzzleConstBox.listFruitClip[GameSystem.Instance.UserPowerLevel - 1];
    }
}
