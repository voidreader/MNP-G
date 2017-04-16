using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Google2u;
using SimpleJSON;

public class NekoSelectBigPopCtrl : MonoBehaviour {

    static NekoSelectBigPopCtrl _instance = null;

    MNP_NekoSkill _NekoSkill = MNP_NekoSkill.Instance; // 네코 스킬 정보 
    MNP_NekoSkillValue _SkillValue = MNP_NekoSkillValue.Instance; // 네코 스킬 값

    [SerializeField] GameObject _noInfoGroup;
    [SerializeField] GameObject _infoGroup;
    

	[SerializeField] UIButton btnSort;
	[SerializeField] UISprite spChecker;

	[SerializeField] bool _isGradeOrder = false;

    [SerializeField] PlayerOwnNekoCtrl _neko = null;

    [SerializeField] GameObject btnConfirm;
    [SerializeField] GameObject btnRelease;

    [SerializeField]
    NekoLevelUpConfirmCtrl _nekoLevelUpConfirmWindow;

    

    [SerializeField]
    NekoFeedCtrl _feedWindow;

    
    [SerializeField] NekoInfoCtrl _infoWindow; // 네코 설명
    


    /* 선택 고양이 정보 */
    [SerializeField] UISprite _nekoSprite;
    [SerializeField] UILabel _nekoGrade;
    [SerializeField] UILabel _nekoLevel;
    [SerializeField] UILabel _nekoPower;
    [SerializeField] UILabel _nekoName;
    [SerializeField] UILabel _nekoStar;
    [SerializeField] UISprite _nekoGradeBarStar;
    
    [SerializeField] UIProgressBar _nekoBeadBar;
    [SerializeField] UISprite _nekoBadge;


    // 스킬 관련 변수 
    [SerializeField]
    UILabel _lblNekoSkillInfo;
    JSONNode _nekoNode;
    NekoData _nekoStructData;
    

    int _currentBead;
    int _maxBead;
    

    [SerializeField] int _star;
    [SerializeField] string _grade;
    [SerializeField] int _level;
    private float _barValue; // Bar Value 
    private int _power;

    Vector3 _originRot = new Vector3(0, 0, -10);
    Vector3 _highPos = new Vector3(0, 200, 0);


    [SerializeField] UIButton _btnMainNeko;
    [SerializeField] UILabel _lblMainNeko;

    [SerializeField] GameObject _effectGroup;
    [SerializeField] UISprite _effectText;
    [SerializeField] Transform _effectAura;
    [SerializeField] List<UISpriteAnimation> _listNekoFrameEffect = new List<UISpriteAnimation>();


    public static NekoSelectBigPopCtrl Instance {

        get {
            if (_instance == null) {
                _instance = FindObjectOfType(typeof(NekoSelectBigPopCtrl)) as NekoSelectBigPopCtrl;

                if (_instance == null) {
                    Debug.Log("NekoSelectBigPopCtrl Init Error");
                    return null;
                }
            }

            return _instance;
        }

    }



    #region 초기화

    /// <summary>
    /// 활성활 될때 초기화 
    /// </summary>
    void OnEnable() {
        SetActiveNeko(false);


        GameSystem.Instance.PreviousSelectNekoID = -1;
        GameSystem.Instance.SelectNeko = null;
    }


    private void SetActiveNeko(bool pValue) {

        _nekoBadge.gameObject.SetActive(false);


        if (!pValue) {
            Neko = null;
        }

        _nekoSprite.gameObject.SetActive(pValue);
        _nekoGrade.gameObject.SetActive(pValue);
        _nekoLevel.gameObject.SetActive(pValue);
        _nekoPower.gameObject.SetActive(pValue);
        _nekoBeadBar.gameObject.SetActive(pValue);
        _nekoStar.gameObject.SetActive(pValue);
        _nekoBeadBar.gameObject.SetActive(pValue);
        

        if (!pValue) {
            SetNoneSkill();
            btnConfirm.SetActive(false);
            btnRelease.SetActive(false);
            
        }

        SetInfoGroup(pValue);

    }

    /// <summary>
    /// 스킬 초기화
    /// </summary>
    private void SetNoneSkill() {
        _lblNekoSkillInfo.text = string.Empty;
        
    }

    private void SetInfoGroup(bool pValue) {
        if(pValue) {
            _infoGroup.SetActive(true);
            _noInfoGroup.SetActive(false);
        } else {
            _infoGroup.SetActive(false);
            _noInfoGroup.SetActive(true);
        }
    }

    /// <summary>
    /// 네코 스킬 체크 
    /// </summary>
    private void CheckSkill() {

         _nekoNode =  GameSystem.Instance.GetNekoNodeByID(Neko.NekoID);

        Debug.Log("Check Skill _nekoNode ::" + _nekoNode.ToString());

        _nekoStructData = GameSystem.Instance.GetNekoData(_nekoNode);


        _lblNekoSkillInfo.text = string.Empty;
        string nekoSkill = string.Empty;


        Debug.Log("Check Skill SkillCount ::" + _nekoStructData.skillCount);

        for (int i=0; i<_nekoStructData.skillCount; i++) {

            if (i > 0)
                nekoSkill += "\n";

            nekoSkill += "●";
            nekoSkill += _nekoStructData.listSkillInfo[i];
        }

        _lblNekoSkillInfo.text = nekoSkill;
        

    }

    
    #endregion


    /// <summary>
    /// 네코 설명 보기 
    /// </summary>
    public void OpenInfoWindow() {
        if (Neko == null) {
            LobbyCtrl.Instance.OpenInfoPopUp(PopMessageType.EquipNekoNeed);
            return;
        }

        _infoWindow.SetNekoInfo(Neko.NekoID, Neko._grade);
    }

    private void OnCompleteAppear() {
        _nekoStar.gameObject.SetActive(true);
            
    }


    /// <summary>
    /// 네코 아이디를 미리 선택 처리 
    /// </summary>
    /// <param name="pNekoID"></param>
    public void SetNekoByNekoID(int pNekoID) {

        if (pNekoID < 0)
            return;

        for(int i=0; i < LobbyCtrl.Instance.ListPlayerNekoSelection.Count; i++) {
            if(LobbyCtrl.Instance.ListPlayerNekoSelection[i].NekoID == pNekoID) {
                SetCurrentNeko(LobbyCtrl.Instance.ListPlayerNekoSelection[i]);
            }
        }
    }

    /// <summary>
    /// 네코 선택 
    /// 네코를 선택하면 정보창을 세팅한다 .
    /// </summary>
    /// <param name="pNeko"></param>
    public void SetCurrentNeko(PlayerOwnNekoCtrl pNeko) {

        Debug.Log(">>> SetCurrentNeko >>> ");

        // 이펙트 초기화 
        InitLevelUpEffect();

        SetActiveNeko(true);
        _nekoStar.gameObject.SetActive(false);

        GameSystem.Instance.SelectNeko = pNeko;
        Neko = pNeko;

        // Sprite 처리 
        GameSystem.Instance.SetNekoSprite(_nekoSprite, pNeko.NekoID, pNeko._grade);
        _nekoSprite.transform.DOKill();
        _nekoSprite.transform.localPosition = _highPos;
        _nekoSprite.transform.localEulerAngles = _originRot;
        _nekoSprite.transform.DOLocalMoveY(0, 0.5f).SetEase(Ease.OutBounce).OnComplete(OnCompleteAppear);
        _nekoSprite.transform.DOLocalRotate(new Vector3(0, 0, 10), 0.5f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
        //_nekoSprite.atlas = Neko.NekoAtlas;
        //_nekoSprite.spriteName = Neko.NekoSpriteName;

        // 정보 처리 
        _currentBead = Neko._bead; // 구슬 
        Star = Neko._grade; // 등급 
        Level = Neko._level; // 레벨 

       _power = Neko._power;
        Grade = "";

        _nekoLevel.text = "Lv." + Level.ToString();

        // 2016.05 한계 레벨 추가
        switch(Star) {
            case 1:
                _nekoLevel.text = _nekoLevel.text + " / Lv.30";
                break;
            case 2:
                _nekoLevel.text = _nekoLevel.text + " / Lv.35";
                break;
            case 3:
                _nekoLevel.text = _nekoLevel.text + " / Lv.40";
                break;
            case 4:
                _nekoLevel.text = _nekoLevel.text + " / Lv.45";
                break;
            case 5:
                _nekoLevel.text = _nekoLevel.text + " / Lv.50";
                break;
        }

        // MAX 레벨인 경우는, 바꾼다.
        if(_neko.IsMaxLevel) {
            _nekoLevel.text = "MAX";
        }

        _nekoPower.text = _power.ToString();
        _nekoName.text = GameSystem.Instance.GetNekoName(_neko.NekoID, _neko._grade);


        for (int i = 0; i < Star; i++) {
            Grade += "*";
        }

        _nekoStar.text = Grade;

        //bead 계산 
        _maxBead = int.Parse(MNP_NekoBead.Instance.GetGenRow("grade" + Star.ToString()).GetStringData("BeadStack")) - int.Parse(MNP_NekoBead.Instance.GetGenRow("grade" + Star.ToString()).GetStringData("BeadMin")) + 1;
        _currentBead = _currentBead - int.Parse(MNP_NekoBead.Instance.GetGenRow("grade" + Star.ToString()).GetStringData("BeadMin"));


        if (Star >= 5) {
            _maxBead = int.Parse(MNP_NekoBead.Instance.GetGenRow("grade" + Star.ToString()).GetStringData("BeadMin"));
            _currentBead = int.Parse(MNP_NekoBead.Instance.GetGenRow("grade" + Star.ToString()).GetStringData("BeadMin"));
        }

        _nekoGrade.text = (_currentBead.ToString() + "/" + _maxBead.ToString());

        _barValue = (float)_currentBead / (float)_maxBead;
        _nekoBeadBar.value = _barValue;

        // 최고 등급 고양이의 경우는 MAX 처리 (2016.05)
        if (Neko.IsMaxGrade) {
            _nekoBeadBar.value = 1;
            _nekoGrade.text = "MAX"; // Max

            // Star Font를 교체한다
            _nekoStar.bitmapFont = GameSystem.Instance.OrangeStarFont;
            _nekoGradeBarStar.spriteName = PuzzleConstBox.spriteOrangeStarSprite;
        }
        else {
            _nekoStar.bitmapFont = GameSystem.Instance.NormalStarFont;
            _nekoGradeBarStar.spriteName = PuzzleConstBox.spriteNormalStarSprite;
        }

        _nekoBadge.gameObject.SetActive(false);

        // 뱃지(메달) 처리 
        if (GameSystem.Instance.GetNekoMedalType(_neko.NekoID, _neko._grade, _neko._level) == NekoMedal.bronze) {
            _nekoBadge.gameObject.SetActive(true);
            _nekoBadge.spriteName = PuzzleConstBox.spriteBigBronzeBadge;
        }
        else if (GameSystem.Instance.GetNekoMedalType(_neko.NekoID, _neko._grade, _neko._level) == NekoMedal.silver) {
            _nekoBadge.gameObject.SetActive(true);
            _nekoBadge.spriteName = PuzzleConstBox.spriteBigSilverBadge;
        }
        else if (GameSystem.Instance.GetNekoMedalType(_neko.NekoID, _neko._grade, _neko._level) == NekoMedal.gold) {
            _nekoBadge.gameObject.SetActive(true);
            _nekoBadge.spriteName = PuzzleConstBox.spriteBigGoldBadge;
        }


        // 스킬 체크 
        CheckSkill();


        // 버튼 체크
        if(Neko.isForUpgrade) { // 네코 성장 창 

            btnConfirm.SetActive(false);
            btnRelease.SetActive(false);

        }
        else { // 레디창에서 오픈 
            if(Neko.IsSelected) {
                btnConfirm.SetActive(false);
                btnRelease.SetActive(true);
            }
            else {
                btnConfirm.SetActive(true);
                btnRelease.SetActive(false);
            }
        } // 버튼 체크 종료 

        
        // 메인 네코 체크
        if(_neko.NekoID == GameSystem.Instance.UserDataJSON["data"]["mainneko"].AsInt) {
            _btnMainNeko.gameObject.SetActive(false);
            _lblMainNeko.gameObject.SetActive(true);
        } else {
            _btnMainNeko.gameObject.SetActive(true);
            _lblMainNeko.gameObject.SetActive(false);
        }
    }


    /// <summary>
    /// 현재 네코 선택 
    /// </summary>
    public void SelectCurrentNeko() {

        // 튜토리얼 처리 
        if (GameSystem.Instance.LocalTutorialStep == 2) {
            Debug.Log(">> First Neko Select Done");
            LobbyCtrl.Instance.DisableAllButton();
            LobbyCtrl.Instance.EnableSomeButton("btnStart");
            LobbyCtrl.Instance.TeachingReady();
        }


        //this.SendMessage("CloseSelf");
        Neko.SelectCurrentNeko(true);

    }

    /// <summary>
    /// 현재 네코 해제 
    /// </summary>
    public void ReleaseCurrentNeko() {
        //this.SendMessage("CloseSelf");
        Neko.SelectCurrentNeko(false);
        

        SetActiveNeko(false);

    }




    /// <summary>
    /// 사용자 고양이 정렬 
    /// </summary>
	public void SortUserNeko() {

        if(GameSystem.Instance.SelectNeko != null)
            GameSystem.Instance.PreviousSelectNekoID = GameSystem.Instance.SelectNeko.NekoID;

		if (!_isGradeOrder) {
			GameSystem.Instance.SortUserNekoByBead ();
			spChecker.gameObject.SetActive(true);
		} else {
			GameSystem.Instance.SortUserNekoByGet ();
			spChecker.gameObject.SetActive(false);
		}

		_isGradeOrder = !_isGradeOrder;
		GameSystem.Instance.SaveGradeOrder (_isGradeOrder);


        if (LobbyCtrl.Instance.IsSelectiveNekoPanel) {
            LobbyCtrl.Instance.SpawnFilteredSelectivePlayerOwnNeko();
        }
        else {
            LobbyCtrl.Instance.SpawnFilteredGrowthPlayerOwnNeko();
        }

        // 현재 네코의 정보를 선택처리 
        if (GameSystem.Instance.PreviousSelectNekoID >= 0) {
            //GameSystem.Instance.SelectNeko.SendMessage("SetSelectFrameSprite");
            for (int i = 0; i < LobbyCtrl.Instance.ListPlayerNekoSelection.Count; i++) {

                if (GameSystem.Instance.PreviousSelectNekoID == LobbyCtrl.Instance.ListPlayerNekoSelection[i].NekoID) {
                    LobbyCtrl.Instance.ListPlayerNekoSelection[i].OnClickOwnNeko();
                }
            }
        }
	}


    /// <summary>
    /// 생선주기 화면 오픈 
    /// </summary>
    public void OpenFeedWindow() {
        if (Neko == null) {
            LobbyCtrl.Instance.OpenInfoPopUp(PopMessageType.EquipNekoNeed);
            return;
        }

        if(Neko.IsMaxGrade) {
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

        if(Neko == null) {
            LobbyCtrl.Instance.OpenInfoPopUp(PopMessageType.EquipNekoNeed);
            return;
        }

        

        // 성장 맥스치 처리.
        if( (Star == 1 && _level == 30) || (Star == 2 && _level == 35) || (Star == 3 && _level == 40)
            || (Star == 4 && _level == 45) || (Star == 5 && _level == 50))  {
            
            LobbyCtrl.Instance.OpenInfoPopUp(PopMessageType.NeedGradeUp);
            return;
        }

        _nekoLevelUpConfirmWindow.SetNekoLevelUp(Neko);

        //_nekoUpWindow.gameObject.SetActive(true);
        //_nekoUpWindow.SetNekoLevelUp(Neko);
    }

    /// <summary>
    /// 네코 레벨 업그레이드 완료
    /// </summary>
    public void OnCompleteUpgrade() {

        // Refresh 처리. 
        Neko.RefreshState();
        SetCurrentNeko(Neko);

        if(GameSystem.Instance.LocalTutorialStep == 4) {
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

        Neko.RefreshState();
        SetCurrentNeko(Neko);

        if(pRaiseEffect)
            RaiseSmallFeedEffect();
    }

    public void SetMainNeko() {
        GameSystem.Instance.UpgradeNekoDBKey = _neko._dbkey;
        GameSystem.Instance.Post2MainNeko();
    }




    #region 이펙트 처리

    private void RaiseSmallFeedEffect() {
        InitLevelUpEffect();
        StartCoroutine(DoingSmallFeedEffect());
    }

    IEnumerator DoingSmallFeedEffect() {

        _effectGroup.gameObject.SetActive(true);

        _nekoStar.gameObject.SetActive(false);
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


        _nekoStar.gameObject.SetActive(true);

        for (int i = 0; i < _listNekoFrameEffect.Count; i++) {
            _listNekoFrameEffect[i].gameObject.SetActive(false);
        }

        _effectAura.gameObject.SetActive(false);
        //_nekoSprite.GetComponent<UISpriteAnimation>().Pause();

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

        _nekoStar.gameObject.SetActive(false);
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
        _effectAura.DOLocalRotate(new Vector3(0,0, 360), 1f, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);

        for (int i=0; i<_listNekoFrameEffect.Count; i++) {
            _listNekoFrameEffect[i].gameObject.SetActive(true);
            _listNekoFrameEffect[i].Play();
            yield return new WaitForSeconds(0.4f);
        }

        yield return new WaitForSeconds(1f);


        _nekoStar.gameObject.SetActive(true);

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



    #region Properties

    public string GradeBarText {
        get {
            return _nekoGrade.text;
        }
    }

    public int CurrentBead {
        get {
            return _currentBead;
        }
    }

    public float NekoGradeBarValue {
        get {
            return _nekoBeadBar.value;
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

    public string LevelText {
        get {
            return _nekoLevel.text;
        }
    }

    public string Grade {
        get {
            return _grade;
        }

        set {
            _grade = value;
        }
    }

    public int Star {
        get {
            return _star;
        }

        set {
            _star = value;
        }
    }

    public string StarText {
        get {
            return _nekoStar.text;
        }
    }


    public string GradeText {
        get {
            return _nekoGrade.text;
        }
    }

    public PlayerOwnNekoCtrl Neko {
        get {
            return _neko;
        }

        set {
            _neko = value;
        }
    }



    #endregion
}
