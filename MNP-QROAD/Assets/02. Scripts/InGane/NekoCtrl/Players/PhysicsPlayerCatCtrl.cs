using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using PathologicalGames;

public class PhysicsPlayerCatCtrl : MonoBehaviour {

    [SerializeField] NekoAppearCtrl _appear; // 외형 UI (NGUI)
    [SerializeField] NekoFollowHP _skillBar;

    [SerializeField] int _nekoid;
    [SerializeField] int _grade;
    [SerializeField] int _index;
    [SerializeField] Vector3 _initPos;
    [SerializeField] float _skillValue; // 흡수되는 수치 
    [SerializeField] float _skillMax;

    [SerializeField] float _currentSkillValue = 0;

    
    [SerializeField] Transform _textSpecialSkill;
    readonly Vector3 _posLeftSpecialSkillText = new Vector3(-500, 185, 0);
    readonly Vector3 _posRightSpecialSkillText = new Vector3(500, 185, 0);



    public void HideCat() {
        this.gameObject.SetActive(false);
        _appear.gameObject.SetActive(false);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other) {
        if (other.tag != PuzzleConstBox.tagFoot && other.tag != PuzzleConstBox.tagBigFoot && other.tag != PuzzleConstBox.tagFireworkBolt)
            return;

        
        if(other.CompareTag(PuzzleConstBox.tagBigFoot)) {
            FillSkillBar(_skillValue * 2); // 폭탄으로 생성된 과일은 2배다.
        }
        else {
            FillSkillBar(_skillValue);
        }
        
    }

    /// <summary>
    /// 게이지 차오르기 
    /// </summary>
    /// <param name="pSkill"></param>
    public void FillSkillBar(float pSkillV) {


        _currentSkillValue += pSkillV;
        _skillBar.SetNekoSkillBar(_skillMax, _currentSkillValue);

        // 스킬발동 
        if(_currentSkillValue >= _skillMax) {
            _skillBar.SetNekoSkillBar(_skillMax, 0); // 스킬바 초기화 

            DoSkill();

        }

    }

    /// <summary>
    /// 고양이 초기화 
    /// </summary>
    /// <param name="pNekoID"></param>
    /// <param name="pGrade"></param>
    /// <param name="pSkillMax"></param>
    public void InitPlayerCat(int pNekoID, int pGrade, float pSkillValue, float pSkillMax, int pIndex) {

        this.gameObject.SetActive(true);



        this.transform.localPosition = PlayerCatManagerCtrl.Instance.InitPosCats[pIndex];
        _initPos = this.transform.localPosition;

        _nekoid = pNekoID;
        _grade = pGrade;
        _index = pIndex;
        _skillMax = pSkillMax;
        _skillValue = pSkillValue;

        _currentSkillValue = 0;

        _appear.InitNekoAppear(this.transform, pNekoID, pGrade);


        _skillBar.InitTopPlayerCatFollowBar(this.gameObject);
        _skillBar.SetNekoSkillBar(_skillMax, 0, pIndex);

        // 둥둥.
        FloatCat();


    }

    /// <summary>
    /// 플레이어 고양이 무브 처리 
    /// </summary>
    void FloatCat() {
        this.transform.DOLocalMoveY(5.2f, 1).SetLoops(-1, LoopType.Yoyo);
    }

    /// <summary>
    /// 스킬 발동
    /// </summary>
    void DoSkill() {
        this.transform.DOKill();

        // 효과
        PoolManager.Pools[PuzzleConstBox.objectPool].Spawn(PuzzleConstBox.prefabDust, this.transform.position, Quaternion.identity).GetComponent<DustCtrl>().PlayColorfulLight();

        // 이얍! 소리 
        InSoundManager.Instance.PlaySpecialSkill();

        // 점프 
        this.transform.DOLocalJump(_initPos, 1, 1, 0.5f).OnComplete(FloatCat);

        // 게이지 초기화 
        _currentSkillValue = 0;
        _skillBar.SetNekoSkillBar(_skillMax, _currentSkillValue);


        // 스킬 발동 
        InGameCtrl.Instance.CheckActiveSkill(_nekoid, _index);

        if(InGameCtrl.Instance.ListNekoPassive[_index].HasActiveSkill)
            ShowSpecialSkillText();

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


    public Vector3 GetCurrentPosition() {
        return this.transform.position;
    }
}
