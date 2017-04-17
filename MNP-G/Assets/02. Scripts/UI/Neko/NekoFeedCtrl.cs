using UnityEngine;
using System.Collections;
using Google2u;

public class NekoFeedCtrl : MonoBehaviour {


    


    [SerializeField]
    SimplePopupCtrl _messagePopup;

    [SerializeField] NekoEvolAlertCtrl _nekoFinalEvolAlert;

    [SerializeField] FishColumnCtrl[] _arrGiveFishColumn; // 주는 컬럼 
    [SerializeField] FishColumnCtrl[] _arrRecieveFishColumn; // 받는 컬럼 


    OwnCatCtrl _neko;
    NekoSelectBigPopCtrl _nekoGrowthWindow;

    /* 선택 고양이 정보 */
    [SerializeField] UISprite _nekoSprite;
    [SerializeField] UILabel _nekoGrade; // 게이지에 사용되는 레이블. 5/100
    [SerializeField] UILabel _nekoLevel;
    [SerializeField] UILabel _nekoPower;
    [SerializeField] UILabel _nekoStar; // 등급 레이블 
    
    [SerializeField] UIProgressBar _nekoBeadBar; // 실제 BeadBar.
    [SerializeField] UIProgressBar _nekoFakeBeadBar; // 업그레이드를 표시하는 게이지 

    [SerializeField] int _feedFish = 0;
    [SerializeField] bool _isMaxBead = false; // 더이상 생선을 줄 수 없는 상태.
    
    [SerializeField] bool _isNextGradeMax = false; // 다음 단계가 마지막 등급.

    int _currentBead;
    int _maxBead;
    float _barValue; // Bar Value 




    private void InitFishColumns() {
        for (int i = 0; i < ArrGiveFishColumn.Length; i++) {
            ArrGiveFishColumn[i].SetFishColumn(i, _neko, this);
        }

        for (int i = 0; i < ArrRecieveFishColumn.Length; i++) {
            ArrRecieveFishColumn[i].SetRecieveColumn(i, _neko, this);
        }
    }


    public void OpenFeedWindow(NekoSelectBigPopCtrl pGrowWindow, OwnCatCtrl pNeko) {

        this.gameObject.SetActive(true);




        _neko = pNeko;
        _nekoGrowthWindow = pGrowWindow;



        // 기본정보 세팅
        SetInfo();


        // 튜토리얼 처리 
        if(GameSystem.Instance.LocalTutorialStep == 5) {
            // 참치 컬럼만 활성화 처리 
            LobbyCtrl.Instance.DisableAllButton();
            ArrGiveFishColumn[1].GetComponentInChildren<UIButton>().enabled = true;
        }
    }

    public void SetTutorialLock() {
        Debug.Log("SetTutorialLock");

        LobbyCtrl.Instance.DisableAllButton();
        // 확인버튼만. 
        LobbyCtrl.Instance.EnableSomeButton("btnFeed");
        LobbyCtrl.Instance.TutorialHand.SetEnable(new Vector3(180, -430, 0));

        LobbyCtrl.Instance.SetExplain(true);
    }

    private void SetInfo() {
        _feedFish = 0;
        IsMaxBead = false;
        _isNextGradeMax = false;

        InitFishColumns();
        GameSystem.Instance.InitFishFeed(); // 생선 값 초기화       

        _nekoBeadBar.value = _nekoGrowthWindow.NekoGradeBarValue;
        _nekoFakeBeadBar.value = _nekoGrowthWindow.NekoGradeBarValue;

        _nekoSprite.atlas = _neko.CharacterAtlas;
        _nekoSprite.spriteName = _neko.CharacterSpriteName;

        _nekoLevel.text = _nekoGrowthWindow.LevelText;
        _nekoPower.text = _neko.Power.ToString();
        _nekoGrade.text = _nekoGrowthWindow.GradeText;
        _nekoStar.text = _nekoGrowthWindow.StarText;

        _currentBead = _neko.Bead;

           
    }


    /// <summary>
    /// 생선먹이기 확정 처리
    /// </summary>
    public void ConfirmFeed() {
        if (_feedFish <= 0) { // 생선이 없는 경우는 없다는 팝업 오픈 
            _messagePopup.SetInfoMessage(PopMessageType.NoFish, null);
            return;
        }

        // 네코의 다음 등급이 5인 경우, AlertMessage 호출 
        // 2016.08 3~5등급의 모양이 동일하면 경고창을 띄우지 않는다 .
        if(IsMaxBead && _neko.Grade + 1 == 5 
            && (!GameSystem.Instance.GetNekoSpriteName(_neko.Id, _neko.Grade).Equals(GameSystem.Instance.GetNekoSpriteName(_neko.Id, _neko.Grade + 1)))) {
            NekoEvolAlertCtrl.OnCompleteClickOK += CheckConfirm;
            _nekoFinalEvolAlert.SetFinalEvolutionAlert(_neko.Id, _neko.Grade, _neko.Grade + 1);
            return;
        }
        else { // 그 외는 확인 메세지 호출 
            CheckConfirm();
        }
    }

    /// <summary>
    /// 잉여 생선이 남아있는지 체크 
    /// </summary>
    private void CheckConfirm() {

        Debug.Log("▶▶▶ ConfirmFeed CheckConfirm");

        // 다음등급이 최고 등급인경우에, 숫자가 초과했다면 경고메세지.
        if (_isNextGradeMax) {
            _messagePopup.SetInfoMessage(PopMessageType.NekoGonnaMaxGrade, SendFish);
        }
        else {
            _messagePopup.SetInfoMessage(PopMessageType.ConfirmFeed, SendFish);
        }
    }


    /// <summary>
    /// 서버로 생선 먹이기 전송 
    /// </summary>
    private void SendFish() {
        GameSystem.Instance.CurrentSelectNeko = _neko;
        GameSystem.Instance.UpgradeNekoDBKey = _neko.Dbkey;
        GameSystem.Instance.Post2NekoFeedFish();
    }

    /// <summary>
    /// 생선 먹이고 난 후 Refresh()
    /// </summary>
    public void RefreshNekoInfo() {
        GameSystem.Instance.CurrentSelectNeko.UpdateInfo(); // 생선을 먹은 고양이 정보 업데이트 

        _nekoGrowthWindow.SetCurrentNeko(GameSystem.Instance.CurrentSelectNeko); // 성장 창 업데이트 
        SetInfo(); // 생선 창 업데이트 


    }

    /// <summary>
    /// 게이지 상승 처리 
    /// </summary>
    /// <param name="pQ"></param>
    public void AddFish(int pQ) {

        _feedFish += pQ;

        // 생선값을 더해서 처리.
        _currentBead = _neko.Bead + _feedFish;

        SetFakeBeadBar();

    }

    private void SetFakeBeadBar() {

        _maxBead = int.Parse(MNP_NekoBead.Instance.GetGenRow("grade" + _neko.Grade.ToString()).GetStringData("BeadStack")) - int.Parse(MNP_NekoBead.Instance.GetGenRow("grade" + _neko.Grade.ToString()).GetStringData("BeadMin")) + 1;
        _currentBead = _currentBead - int.Parse(MNP_NekoBead.Instance.GetGenRow("grade" + _neko.Grade.ToString()).GetStringData("BeadMin"));


        //_maxBead = GameSystem.Instance.DocsNekoBead.get<int>(_neko._grade.ToString(), "BeadStack") - GameSystem.Instance.DocsNekoBead.get<int>(_neko._grade.ToString(), "BeadMin") + 1;
        //_currentBead = _currentBead - GameSystem.Instance.DocsNekoBead.get<int>(_neko._grade.ToString(), "BeadMin");

        if (_currentBead >= _maxBead) {
            IsMaxBead = true;
        } else {
            IsMaxBead = false;
        }

        // 생선을 먹였을때 다음 등급이 최종 등급인지 체크 
        if(IsMaxBead && _neko.MaxGrade == _neko.Grade+1) {
            _isNextGradeMax = true;
        } else {
            _isNextGradeMax = false;
        }

        _nekoGrade.text = (_currentBead.ToString() + "/" + _maxBead.ToString());
        _barValue = (float)_currentBead / (float)_maxBead;

        _nekoFakeBeadBar.value = _barValue;
    }
  

    /// <summary>
    /// 게이지 감소 처리
    /// </summary>
    /// <param name="pQ"></param>
    public void MinusFish(int pQ) {

        _feedFish -= pQ;

        _currentBead = _neko.Bead + _feedFish;
      
        SetFakeBeadBar();
    }

    #region Properties
    public FishColumnCtrl[] ArrRecieveFishColumn {
        get {
            return _arrRecieveFishColumn;
        }

        set {
            _arrRecieveFishColumn = value;
        }
    }

    public FishColumnCtrl[] ArrGiveFishColumn {
        get {
            return _arrGiveFishColumn;
        }

        set {
            _arrGiveFishColumn = value;
        }
    }

    public bool IsMaxBead {
        get {
            return _isMaxBead;
        }

        set {
            _isMaxBead = value;
        }
    }

    public int FeedFish {
        get {
            return _feedFish;
        }

        set {
            _feedFish = value;
        }
    }

    public OwnCatCtrl Neko {
        get {
            return _neko;
        }

        set {
            _neko = value;
        }
    }
    #endregion
}
