using UnityEngine;
using System.Collections;
using DG.Tweening;
using SimpleJSON;

public class SkillBarCtrl : MonoBehaviour {
    [SerializeField]
    int _barIndx = -1;
    [SerializeField]
    int _nekoID = -1;
    [SerializeField]
    int _nekoGrade = -1;

    [SerializeField]
    int _fullValue = 30;
    [SerializeField]
    int _currentValue = 0;

    [SerializeField]
    UIProgressBar _circleProgress;

    [SerializeField]
    Transform _autoText;

    Vector3 _posAutoText = new Vector3(0, 50, 0);

    
    // Effect 
    [SerializeField]
    WhiteLightCtrl _fullFX = null;

    [SerializeField]
    UISprite _miniHead;

    JSONNode _nekoSkillNode;
    [SerializeField]
    int _specialAttackGrade;

    [SerializeField] Transform _textSpecialSkill;
    readonly Vector3 _posLeftSpecialSkillText = new Vector3(-500, 185, 0);
    readonly Vector3 _posRightSpecialSkillText = new Vector3(500, 185, 0);

    #region Properties 

    public int BarIndx {
        get {
            return _barIndx;
        }

        set {
            _barIndx = value;
        }
    }

    public int NekoID {
        get {
            return _nekoID;
        }

        set {
            _nekoID = value;
        }
    }

    public int NekoGrade {
        get {
            return _nekoGrade;
        }

        set {
            _nekoGrade = value;
        }
    }

    public int CurrentValue {
        get {
            return _currentValue;
        }

        set {
            _currentValue = value;
        }
    }

    public int FullValue {
        get {
            return _fullValue;
        }

        set {
            _fullValue = value;
        }
    }

    #endregion

    // Use this for initialization
    void Start() {

    }




    /// <summary>
    /// 게임내 Index 설정
    /// </summary>
    /// <param name="pIndx"></param>
    public void SetIndex(int pIndx) {
        _barIndx = pIndx;
    }


    /// <summary>
    /// 네코 스킬 바 설정 
    /// </summary>
    /// <param name="pNekoID"></param>
    /// <param name="pNekoGrade"></param>
    public void SetNekoSkillBar(int pNekoID, int pNekoGrade) {

        if (pNekoID < 0)
            return;

        NekoID = pNekoID;
        NekoGrade = pNekoGrade;

        GameSystem.Instance.SetNekoMiniSprite(_miniHead, NekoID, NekoGrade);



        _nekoSkillNode = GameSystem.Instance.GetNekoNodeByID(NekoID);
        /*
        _specialAttackGrade = _nekoSkillNode["specialattack"].AsInt;

        switch (_specialAttackGrade) {
            case 1:
                FullValue = Random.Range(20, 40);
                break;
            case 2:
                FullValue = Random.Range(30, 50);
                break;
            case 3:
                FullValue = Random.Range(40, 60);
                break;
            case 4:
                FullValue = Random.Range(50, 70);
                break;
            case 5:
                FullValue = Random.Range(60, 80);
                break;
            default:
                FullValue = Random.Range(30, 50);
                break;
        }
        */

        FullValue = InGameCtrl.Instance.SkillInvokeBlockCount;


        if(InGameCtrl.Instance.BoostSpecialAttack) {
            FullValue -= 5;
        }




        InitCircleProgress();

        
        _fullFX.gameObject.SetActive(false);
    }


    /// <summary>
    /// 일정 값으로 고정 
    /// </summary>
    public void SetValue(int pValue) {
        _currentValue = pValue;
        _circleProgress.value = (float)(_currentValue / FullValue);
    }



    /// <summary>
    /// 블록 1개당 1씩 증가.
    /// </summary>
    /// <param name="pValue"></param>
    public void AddValue(int pValue) {

        if (NekoID < 0)
            return;

        if (_currentValue >= FullValue)
            return;

        _currentValue += pValue;

        //_circleProgress.value = (float)(_currentValue / FullValue);
        _circleProgress.value = (float)_currentValue / (float)FullValue;


        if (_currentValue >= FullValue) {
            UseSkill();
        }
    }


    /// <summary>
    /// 스킬 작동!
    /// </summary>
    public void UseSkill() {

        if (NekoID < 0)
            return;


        //_fullFX.gameObject.SetActive(true);
        _fullFX.Play();


        InSoundManager.Instance.PlaySingleAttackReady();

        InitCircleProgress();

        // 캐릭터 소환 
        InGameCtrl.Instance.SpawnMyNeko(this, BarIndx);
        

        _autoText.localPosition = _posAutoText;
        _autoText.gameObject.SetActive(true);
        _autoText.DOLocalMoveY(80, 0.5f).OnComplete(OnCompleteAutoText);

        InUICtrl.Instance.SetMinusMissionCount(SpecialMissionType.specialAttack);


        if (InGameCtrl.Instance.ListNekoPassive[_barIndx].HasActiveSkill)
            ShowSpecialSkillText();

    }
    

    void OnCompleteAutoText() {
        _autoText.gameObject.SetActive(false);
    }

    /// <summary>
    /// Circle 초기화
    /// </summary>
    private void InitCircleProgress() {
        _currentValue = 0;
        _circleProgress.value = 0;
    }


    void ShowSpecialSkillText() {
        _textSpecialSkill.localPosition = _posLeftSpecialSkillText;
        _textSpecialSkill.gameObject.SetActive(true);
        _textSpecialSkill.DOLocalMoveX(0, 0.2f).OnComplete(OnCompleteTextMove);

    }


    void OnCompleteTextMove() {
        _textSpecialSkill.DOLocalMoveX(500, 0.2f).SetDelay(0.4f).OnComplete(OnCompleteShowSpecialSkillTextRight);
    }

    void OnCompleteShowSpecialSkillTextRight() {
        _textSpecialSkill.gameObject.SetActive(false);
    }



}
