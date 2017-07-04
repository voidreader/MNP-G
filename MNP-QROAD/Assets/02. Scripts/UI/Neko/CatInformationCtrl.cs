using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Google2u;
using SimpleJSON;

public class CatInformationCtrl : MonoBehaviour {


    static CatInformationCtrl _instance = null;

    #region 고양이 정보 
    MNP_NekoSkill _NekoSkill = MNP_NekoSkill.Instance; // 네코 스킬 정보 
    MNP_NekoSkillValue _SkillValue = MNP_NekoSkillValue.Instance; // 네코 스킬 값

    [SerializeField] OwnCatCtrl _neko = null;

    [SerializeField] UISprite _spNekoSprite;
    [SerializeField] UILabel _lblNekoLevel;
    [SerializeField] UILabel _lblNekoPower;
    [SerializeField] UILabel _lblNekoName;
    [SerializeField] UISprite _spNekoGrade;
    [SerializeField] UIProgressBar _barNekoBead;
    [SerializeField] UILabel _lblNekoBeadValue;
    [SerializeField] UISprite _spNekoMedal;

    [SerializeField] GameObject[] _arrNekoSkillGroup;
    [SerializeField] UISprite[] _arrNekoSkillType; // 고양이 스킬 타입
    [SerializeField] UILabel[] _arrNekoSkillLabel; // 고양이 스킬 설명 

    [SerializeField] bool _isMainCat = false;
    [SerializeField] GameObject _lblNoSelect;

    int _currentBead;
    int _maxBead;
    int _grade;
    int _level;
    string _gradeText;
    private float _barValue; // Bar Value 
    private int _power;


    JSONNode _nekoNode;
    NekoData _nekoStructData;

    #endregion


    int _maxSixNekoListIndex = -1;
    int _currentSixNekoIndex = -1;

    [SerializeField] bool _isGradeOrder = false;
    [SerializeField] bool _isShowingSkillInfo = false;

    #region Object

    [SerializeField] UIButton _btnSort;
    [SerializeField] UILabel _lblSort;

    [SerializeField] GameObject _btnConfirm;
    [SerializeField] GameObject _btnRelease;

    [SerializeField] GameObject _btnFeed;
    [SerializeField] GameObject _btnLevelUp;

    [SerializeField] GameObject _objNekoGradeName;
    [SerializeField] GameObject _spNekoShadow;
    [SerializeField] GameObject _btnSkillInfo;
    [SerializeField] GameObject _spSmallSkillSign;
    [SerializeField] GameObject _spBigSkillSign;

    [SerializeField] Transform _trTopMove;
    [SerializeField] GameObject _spMain1;
    [SerializeField] GameObject _spMain2;


    Vector3 _posNekoInit = new Vector3(0, 175, 0);
    float _NekoFloatMoveY = 190;
    Vector3 _topOriginPos = new Vector3(141, 402, 0);


    [SerializeField]
    UICenterOnChild _SixNekoOnCenter;
    [SerializeField] GameObject _currentCenterObject = null;

    [SerializeField] GameObject _btnLeftSixNekoList;
    [SerializeField] GameObject _btnRightSixNekoList;
    [SerializeField] UILabel _lblSixNekoList;

    #endregion

    #region 부속 화면 

    [SerializeField] NekoLevelUpConfirmCtrl _nekoLevelUpConfirmWindow; // 레벨업 
    [SerializeField] NekoFeedCtrl _feedWindow; // 생선주기 화면 
    [SerializeField] NekoInfoCtrl _infoWindow; // 고양이 스토리

    #endregion

    #region 화면 내 이펙트 관련 변수
    [SerializeField] GameObject _effectGroup;
    [SerializeField] UISprite _effectText;
    [SerializeField] Transform _effectAura;
    [SerializeField] List<UISpriteAnimation> _listNekoFrameEffect = new List<UISpriteAnimation>();
    #endregion


    #region 프로퍼티

    public static CatInformationCtrl Instance {

        get {
            if (_instance == null) {
                _instance = FindObjectOfType(typeof(CatInformationCtrl)) as CatInformationCtrl;

                if (_instance == null) {
                    Debug.Log("CatInformationCtrl Init Error");
                    return null;
                }
            }

            return _instance;
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

    public int Grade {
        get {
            return _grade;
        }

        set {
            _grade = value;
        }
    }

    public int Level {
        get {
            return _level;
        }

        set {
            _level = value;
        }
    }

    public float NekoGradeBarValue {
        get {
            return _barNekoBead.value;
        }
    }

    public string LevelText {
        get {
            return _lblNekoLevel.text;
        }
    }

    public string GradeText {
        get {
            return _gradeText;
        }

        set {
            _gradeText = value;
        }
    }

    public string BeadBarText {
        get {
            return _lblNekoBeadValue.text;
        }
    }

    public bool IsMainCat {
        get {
            return _isMainCat;
        }

        set {
            _isMainCat = value;
        }
    }


    #endregion

    private void OnEnable() {
        InitInformation();
    }

    /// <summary>
    /// 화면 오픈 
    /// </summary>
    public void OpenCatInformation() {
        this.gameObject.SetActive(true);
    }

    public void CloseCatInformation() {
        LobbyCtrl.Instance.ClearCharacterList();
        this.SendMessage("CloseSelf");
    }

    /// <summary>
    /// 화면 오픈 초기화 
    /// </summary>
    void InitInformation() {

        _SixNekoOnCenter.onCenter = OnCenter;

        GameSystem.Instance.SelectNeko = null;
        GameSystem.Instance.PreviousSelectNekoID = -1;

        _btnConfirm.SetActive(false);
        _btnRelease.SetActive(false);

        _spBigSkillSign.SetActive(false);
        _spSmallSkillSign.SetActive(false);

        // 고양이가 선택되지 않았을때, 필요없는 오브젝트 비활성화 
        SetActivateCatInfoObj(false);
        _lblNekoLevel.text = string.Empty;

        _trTopMove.localPosition = _topOriginPos;
        _isShowingSkillInfo = false;

        _isGradeOrder = GameSystem.Instance.LoadGradeOrder();

        
            
            


    }



    /// <summary>
    /// 고양이 선택 
    /// </summary>
    /// <param name="pNeko"></param>
    public void SetCatInfomation(OwnCatCtrl pNeko) {

        bool flagSameCat = false;

        if (pNeko == GameSystem.Instance.SelectNeko)
            flagSameCat = true;

        Neko = pNeko;
        GameSystem.Instance.SelectNeko = pNeko;

        // 이펙트 초기화
        InitLevelUpEffect();


        // 정보 처리 시작
        SetActivateCatInfoObj(true);
        SetOrderButton();

        // 고양이 등장 
        GameSystem.Instance.SetNekoSpriteWithButton(_spNekoSprite, Neko.Id, Neko.Grade);

        if (!flagSameCat) {
            _spNekoSprite.transform.DOKill();
            _spNekoSprite.transform.localPosition = _posNekoInit;
            _spNekoSprite.transform.localScale = Vector3.zero; // 크기가 0에서 커지게. 

            _spNekoSprite.transform.DOScale(1, 0.5f).OnComplete(OnCompleteNekoSpriteAppear);
        }

        


        _currentBead = Neko.Bead;
        Grade = Neko.Grade;
        GradeText = GameSystem.Instance.GetNekoGradeText(Grade);
        _power = Neko.Power;
        Level = Neko.Level;

        _lblNekoLevel.text = "Lv." + Level.ToString();
        _lblNekoPower.text = "Power : " + _power.ToString();
        _lblNekoName.text = GameSystem.Instance.GetNekoName(Neko.Id, Neko.Grade);
        _spNekoGrade.spriteName = GameSystem.Instance.GetNekoGradeSprite(Grade);

        // 등급 게이지 계산
        _maxBead = int.Parse(MNP_NekoBead.Instance.GetGenRow("grade" + Grade.ToString()).GetStringData("BeadStack")) - int.Parse(MNP_NekoBead.Instance.GetGenRow("grade" + Grade.ToString()).GetStringData("BeadMin")) + 1;
        _currentBead = _currentBead - int.Parse(MNP_NekoBead.Instance.GetGenRow("grade" + Grade.ToString()).GetStringData("BeadMin"));


        if (Grade >= 5) {
            _maxBead = int.Parse(MNP_NekoBead.Instance.GetGenRow("grade" + Grade.ToString()).GetStringData("BeadMin"));
            _currentBead = int.Parse(MNP_NekoBead.Instance.GetGenRow("grade" + Grade.ToString()).GetStringData("BeadMin"));
        }

        _lblNekoBeadValue.text = (_currentBead.ToString() + "/" + _maxBead.ToString());

        _barValue = (float)_currentBead / (float)_maxBead;
        _barNekoBead.value = _barValue;


        // 최대 레벨일때..
        if (Neko.IsMaxLevel) {

        }

        // 최고 등급일때..
        if (Neko.IsMaxGrade) {
            _barNekoBead.value = 1;
            _lblNekoBeadValue.text = "MAX"; // Max
        }

        #region 뱃지 (메달 처리)
        _spNekoMedal.gameObject.SetActive(false);
        switch (GameSystem.Instance.GetNekoMedalType(Neko.Id, Neko.Grade, Neko.Level)) {
            case NekoMedal.bronze:
                _spNekoMedal.gameObject.SetActive(true);
                _spNekoMedal.spriteName = PuzzleConstBox.spriteBigBronzeBadge;
                break;
            case NekoMedal.silver:
                _spNekoMedal.gameObject.SetActive(true);
                _spNekoMedal.spriteName = PuzzleConstBox.spriteBigSilverBadge;
                break;
            case NekoMedal.gold:
                _spNekoMedal.gameObject.SetActive(true);
                _spNekoMedal.spriteName = PuzzleConstBox.spriteBigGoldBadge;
                break;
        }
        #endregion

        // 고양이 스킬 정보 세팅 
        CheckSkill();

        // 버튼 오브젝트 처리 
        if (LobbyCtrl.Instance.IsReadyCharacterList) { // 준비화면에서 열린 경우. 
            if (Neko.IsEquipped) {
                _btnConfirm.SetActive(false);
                _btnRelease.SetActive(true);
            }
            else {
                _btnConfirm.SetActive(true);
                _btnRelease.SetActive(false);
            }
        }
        else { // 로비에서 열린 경우 
            _btnConfirm.SetActive(false);
            _btnRelease.SetActive(false);
        }

        // 메인 고양이 처리 
        if (_neko.Id == GameSystem.Instance.UserDataJSON["data"]["mainneko"].AsInt)
            IsMainCat = true;
        else
            IsMainCat = false;


        CheckMainCat(IsMainCat);
        
    }

    /// <summary>
    /// 메인 고양이 처리 
    /// </summary>
    public void CheckMainCat(bool pMainFlag) {

        _spMain1.SetActive(pMainFlag);
        _spMain2.SetActive(pMainFlag);

        // 
        if (_infoWindow.gameObject.activeSelf) {
            _infoWindow.CheckMain(pMainFlag);
        }

    }


    /// <summary>
    /// 고양이 정보 관련 오브젝트 활성&비활성화
    /// </summary>
    /// <param name="pFlag"></param>
    void SetActivateCatInfoObj(bool pFlag) {
        _spNekoShadow.SetActive(pFlag);
        
        _btnLevelUp.SetActive(pFlag);
        _btnFeed.SetActive(pFlag);

        _spNekoMedal.gameObject.SetActive(pFlag);
        _spNekoSprite.gameObject.SetActive(pFlag);
        _objNekoGradeName.SetActive(pFlag);


        _lblNoSelect.SetActive(!pFlag);

        if (!pFlag) {
            Neko = null;
        }

        if (_isShowingSkillInfo && pFlag)
            _spBigSkillSign.SetActive(pFlag);

        if(!_isShowingSkillInfo && pFlag)
            _spSmallSkillSign.SetActive(pFlag);
    }

    void OnCompleteNekoSpriteAppear() {
        _spNekoSprite.transform.DOLocalMoveY(180, 1).SetLoops(-1, LoopType.Yoyo);
    }


    /// <summary>
    /// 스킬 정보 오픈 
    /// </summary>
    public void OpenSkillInfo() {

        // 이미 스킬이 보여주고 있는 상태라면 종료 
        if (_isShowingSkillInfo)
            return;

        // 141 
        _trTopMove.DOLocalMoveX(-70, 0.5f).OnComplete(OnCompleteOpenSkill);

        _isShowingSkillInfo = true;
    }

    void OnCompleteOpenSkill() {
        _spSmallSkillSign.SetActive(false);
        _spBigSkillSign.SetActive(true);
    }

    /// <summary>
    /// 스킬 정보 닫기 
    /// </summary>
    public void CloseSkillInfo() {

        if (!_isShowingSkillInfo)
            return;

        _spBigSkillSign.SetActive(false);
        _trTopMove.DOLocalMoveX(141, 0.5f).OnComplete(OnCompleteCloseSkill);

        _isShowingSkillInfo = false;

    }

    void OnCompleteCloseSkill() {
        _spSmallSkillSign.SetActive(true);
        
    }


    /// <summary>
    /// 네코 스킬 체크 
    /// </summary>
    private void CheckSkill() {

        _nekoNode = GameSystem.Instance.GetNekoNodeByID(Neko.Id);
        _nekoStructData = GameSystem.Instance.GetNekoData(_nekoNode);

        Debug.Log("Check Skill SkillCount ::" + _nekoStructData.skillCount);

        for(int i=0; i<_arrNekoSkillGroup.Length; i++) {
            _arrNekoSkillGroup[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < _nekoStructData.skillCount; i++) {
            _arrNekoSkillGroup[i].gameObject.SetActive(true);


            _arrNekoSkillType[i].spriteName = "top-skill-text-passive";
            _arrNekoSkillLabel[i].text = _nekoStructData.listSkillInfo[i];

            if ( i == 0 ) {
                if (_nekoStructData.skillid1 > 7)
                    _arrNekoSkillType[i].spriteName = "top-skill-text-active";
            }
            else if (i == 1) {
                if (_nekoStructData.skillid2 > 7)
                    _arrNekoSkillType[i].spriteName = "top-skill-text-active";
            }
            else if (i== 2) {
                if (_nekoStructData.skillid3 > 7)
                    _arrNekoSkillType[i].spriteName = "top-skill-text-active";
            }
        }
    }


    /// <summary>
    /// 네코 아이디를 미리 선택 처리 
    /// </summary>
    /// <param name="pNekoID"></param>
    public void SetNekoByNekoID(int pNekoID) {

        if (pNekoID < 0)
            return;

        for (int i = 0; i < LobbyCtrl.Instance.ListCharacterList.Count; i++) {
            if (LobbyCtrl.Instance.ListCharacterList[i].Id == pNekoID) {
                SetCatInfomation(LobbyCtrl.Instance.ListCharacterList[i]);
            }
        }
    }

    void SetOrderButton() {
        if (!_isGradeOrder) {
            _btnSort.normalSprite = "le-base-off";
            _lblSort.color = PuzzleConstBox.colorSortUnChecked;
        }
        else {
            _btnSort.normalSprite = "le-base-on";
            _lblSort.color = PuzzleConstBox.colorSortChecked;
        }
    }

    /// <summary>
    /// 사용자 고양이 정렬 
    /// </summary>
    public void SortUserNeko() {

        if (GameSystem.Instance.SelectNeko != null)
            GameSystem.Instance.PreviousSelectNekoID = GameSystem.Instance.SelectNeko.Id;

        _isGradeOrder = !_isGradeOrder;

        if (_isGradeOrder) {
            GameSystem.Instance.SortUserNekoByBead();
        }
        else {
            GameSystem.Instance.SortUserNekoByGet();
        }

        SetOrderButton();
        
        GameSystem.Instance.SaveGradeOrder(_isGradeOrder);



        // LobbyCtrl.Instance.SpawnCharacterList(LobbyCtrl.Instance.IsReadyCharacterList);
        LobbyCtrl.Instance.SpawnCharacterListBySix(LobbyCtrl.Instance.IsReadyCharacterList);

        // 현재 네코의 정보를 선택처리 
        if (GameSystem.Instance.PreviousSelectNekoID >= 0) {
            //GameSystem.Instance.SelectNeko.SendMessage("SetSelectFrameSprite");
            for (int i = 0; i < LobbyCtrl.Instance.ListCharacterList.Count; i++) {

                if (GameSystem.Instance.PreviousSelectNekoID == LobbyCtrl.Instance.ListCharacterList[i].Id) {
                    LobbyCtrl.Instance.ListCharacterList[i].OnClick();
                }
            }
        }
    }


    /// <summary>
    /// 메인 고양이 설정 
    /// </summary>
    public void SetMainNeko() {
        GameSystem.Instance.UpgradeNekoDBKey = _neko.Dbkey;
        GameSystem.Instance.Post2MainNeko();
    }


    #region 캐릭터 리스트 관련 
    
    public void OnCenterForce(GameObject pObj) {

        _SixNekoOnCenter.CenterOn(pObj.transform);
    }

    public void OnCenter(GameObject pObj) {

        if (pObj == null)
            return;


        // 중복실행을 막는다. 
        if (pObj == _currentCenterObject)
            return;

        _btnRightSixNekoList.SetActive(true);
        _btnLeftSixNekoList.SetActive(true);


        _currentCenterObject = pObj;

        _maxSixNekoListIndex = GetMaxSixNekoIndex();
        _currentSixNekoIndex = _currentCenterObject.GetComponent<SixNekoSetCtrl>().Id;

        _lblSixNekoList.text  = (_currentSixNekoIndex + 1 ).ToString() + " / " + _maxSixNekoListIndex.ToString();

        // 좌우 버튼 처리
        if(_maxSixNekoListIndex -1 <= _currentSixNekoIndex) {
            _btnRightSixNekoList.SetActive(false);
        }

        if (_currentSixNekoIndex == 0)
            _btnLeftSixNekoList.SetActive(false);
    }

    int GetMaxSixNekoIndex() {

        int maxIndex = 0;

        for(int i=0; i<LobbyCtrl.Instance.ListSixNekoSetList.Count;i++) {
            if (LobbyCtrl.Instance.ListSixNekoSetList[i].Id < 0)
                break;

            maxIndex = LobbyCtrl.Instance.ListSixNekoSetList[i].Id;

        }

        return maxIndex + 1;
    }

    public void OnClickLeftSixNekoList() {
        _SixNekoOnCenter.CenterOn(LobbyCtrl.Instance.ListSixNekoSetList[_currentSixNekoIndex - 1].transform);
    }

    public void OnClickRightSixNekoList() {
        _SixNekoOnCenter.CenterOn(LobbyCtrl.Instance.ListSixNekoSetList[_currentSixNekoIndex + 1].transform);
    }


    #endregion


    #region 준비화면 연관 메소드 

    /// <summary>
    /// 선택한 고양이 장착
    /// </summary>
    public void EquipCurrentNeko() {

        // 튜토리얼 처리 
        if (GameSystem.Instance.LocalTutorialStep == 2) {
            Debug.Log(">> First Neko Select Done");
            LobbyCtrl.Instance.DisableAllButton();
            LobbyCtrl.Instance.EnableSomeButton("btnStart");
            LobbyCtrl.Instance.TeachingReady();
        }


        Neko.EquipCharacter();

    }

    /// <summary>
    /// 장착된 고양이 해제 
    /// </summary>
    public void ReleaseCurrentNeko() {
        Neko.ReleaseCharacter();
        SetActivateCatInfoObj(false);
    }


    #endregion

    #region 부속 화면 관련 메소드

    /// <summary>
    /// 네코 설명 보기 
    /// </summary>
    public void OpenInfoWindow() {
        if (Neko == null) {
            LobbyCtrl.Instance.OpenInfoPopUp(PopMessageType.EquipNekoNeed);
            return;
        }

        _infoWindow.SetNekoInfo(Neko.Id, Neko.Grade);
        _infoWindow.CheckMain(IsMainCat);
    }

    /// <summary>
    /// 생선주기 화면 오픈 
    /// </summary>
    public void OpenFeedWindow() {
        if (Neko == null) {
            LobbyCtrl.Instance.OpenInfoPopUp(PopMessageType.EquipNekoNeed);
            return;
        }

        if (Neko.IsMaxGrade) {
            // 선택한 고양이는 더이상 성장시킬 수 없다는 메세지
            LobbyCtrl.Instance.OpenInfoPopUp(PopMessageType.CantGrowNeko);
            return;
        }

        _feedWindow.OpenFeedWindow(this, Neko);
    }

    /// <summary>
    /// 레벨 업 화면 오픈
    /// </summary>
    public void OpenLevelupWindow() {

        if (Neko == null) {
            LobbyCtrl.Instance.OpenInfoPopUp(PopMessageType.EquipNekoNeed);
            return;
        }



        // 성장 맥스치 처리.
        if ((Grade == 1 && Level == 30) || (Grade == 2 && Level == 35) || (Grade == 3 && Level == 40)
            || (Grade == 4 && Level == 45) || (Grade == 5 && Level == 50)) {

            LobbyCtrl.Instance.OpenInfoPopUp(PopMessageType.NeedGradeUp);
            return;
        }

        _nekoLevelUpConfirmWindow.SetNekoLevelUp(Neko);

        //_nekoUpWindow.gameObject.SetActive(true);
        //_nekoUpWindow.SetNekoLevelUp(Neko);
    }

    #endregion

    #region 이펙트 관련 메소드

    /// <summary>
    /// 네코 레벨 업그레이드 완료
    /// </summary>
    public void OnCompleteUpgrade() {

        // Refresh 처리. 
        Neko.UpdateInfo();
        SetCatInfomation(Neko);

        if (GameSystem.Instance.LocalTutorialStep <= 4) {
            GameSystem.Instance.SaveLocalTutorialStep(5); // 5으로 저장 (고양이 레벨 업그레이드 완료)
        }

        Neko.SendMessage("SetSelectFrameSprite");

        RaiseLevelupEffect();
    }

    /// <summary>
    /// 생선먹이기 (진화 아님)
    /// </summary>
    public void OnCompleteFeedFish(bool pRaiseEffect) {

        // 생선창 닫음
        _feedWindow.SendMessage("CloseSelf");

        Neko.UpdateInfo();
        SetCatInfomation(Neko);

        if (pRaiseEffect)
            RaiseSmallFeedEffect();
    }


    private void RaiseSmallFeedEffect() {
        InitLevelUpEffect();
        StartCoroutine(DoingSmallFeedEffect());
    }




    private void RaiseLevelupEffect() {

        InitLevelUpEffect();
        StartCoroutine(DoingLevelUpEffect());
    }


    private void InitLevelUpEffect() {


        StopCoroutine(DoingLevelUpEffect());
        StopCoroutine(DoingSmallFeedEffect());

        _effectAura.gameObject.SetActive(false);
        _effectText.gameObject.SetActive(false);
        for (int i = 0; i < _listNekoFrameEffect.Count; i++) {
            _listNekoFrameEffect[i].gameObject.SetActive(false);
        }

        // 그룹 전체를 비활성화 
        _effectGroup.gameObject.SetActive(false);
    }


    IEnumerator DoingLevelUpEffect() {

        _effectGroup.gameObject.SetActive(true);

        //_nekoSprite.GetComponent<UISpriteAnimation>().Play();

        // 사운드 재생
        LobbyCtrl.Instance.PlayEffect(SoundConstBox.acNekoLevelUp);

        _effectText.spriteName = "level-up-mini";
        _effectText.MakePixelPerfect();
        _effectText.gameObject.SetActive(true);

        _effectText.transform.localPosition = new Vector3(0, 50, 0);
        _effectText.transform.DOKill();

        _effectAura.gameObject.SetActive(true);
        _effectAura.transform.localEulerAngles = Vector3.zero;
        _effectAura.transform.DOKill();


        _effectText.transform.DOLocalMoveY(140, 1).OnComplete(OnCompleteEffectTextMove);
        _effectAura.DOLocalRotate(new Vector3(0, 0, 360), 1f, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);

        for (int i = 0; i < _listNekoFrameEffect.Count; i++) {
            _listNekoFrameEffect[i].gameObject.SetActive(true);
            _listNekoFrameEffect[i].Play();
            yield return new WaitForSeconds(0.4f);
        }

        yield return new WaitForSeconds(1f);


        

        for (int i = 0; i < _listNekoFrameEffect.Count; i++) {
            _listNekoFrameEffect[i].gameObject.SetActive(false);
        }

        _effectAura.gameObject.SetActive(false);
        //_nekoSprite.GetComponent<UISpriteAnimation>().Pause(); 

    }

    IEnumerator DoingSmallFeedEffect() {

        _effectGroup.gameObject.SetActive(true);

        
        //_nekoSprite.GetComponent<UISpriteAnimation>().Play();

        // 사운드 재생
        LobbyCtrl.Instance.PlayEffect(SoundConstBox.acNekoLevelUp);

        _effectText.spriteName = "baebullor";
        _effectText.MakePixelPerfect();
        //_effectText.width = 258;
        //_effectText.height = 60;
        _effectText.gameObject.SetActive(true);
        _effectText.transform.localPosition = new Vector3(0, 50, 0);
        _effectText.transform.DOKill();

        _effectAura.gameObject.SetActive(true);
        _effectAura.transform.localEulerAngles = Vector3.zero;
        _effectAura.transform.DOKill();


        _effectText.transform.DOLocalMoveY(140, 1).OnComplete(OnCompleteEffectTextMove);
        _effectAura.DOLocalRotate(new Vector3(0, 0, 360), 1f, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);

        for (int i = 0; i < _listNekoFrameEffect.Count; i++) {
            _listNekoFrameEffect[i].gameObject.SetActive(true);
            _listNekoFrameEffect[i].Play();
            yield return new WaitForSeconds(0.4f);
        }

        yield return new WaitForSeconds(1f);


        

        for (int i = 0; i < _listNekoFrameEffect.Count; i++) {
            _listNekoFrameEffect[i].gameObject.SetActive(false);
        }

        _effectAura.gameObject.SetActive(false);
        //_nekoSprite.GetComponent<UISpriteAnimation>().Pause();

    }


    private void OnCompleteEffectTextMove() {
        _effectText.gameObject.SetActive(false);
    }


    #endregion


}
