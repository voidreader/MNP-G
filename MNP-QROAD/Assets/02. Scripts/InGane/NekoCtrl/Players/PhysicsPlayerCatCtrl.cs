using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PhysicsPlayerCatCtrl : MonoBehaviour {

    [SerializeField] NekoAppearCtrl _appear; // 외형 UI (NGUI)
    [SerializeField] NekoFollowHP _skillBar;

    [SerializeField] int _nekoid;
    [SerializeField] int _grade;
    [SerializeField] float _skillValue; // 흡수되는 수치 
    [SerializeField] float _skillMax;

    [SerializeField] float _currentSkillValue = 0;


    public void HideCat() {
        this.gameObject.SetActive(false);
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
    void FillSkillBar(float pSkillV) {


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

        _nekoid = pNekoID;
        _grade = pGrade;
        _skillMax = pSkillMax;
        _skillValue = pSkillValue;

        _currentSkillValue = 0;

        _appear.InitNekoAppear(this.transform, pNekoID, pGrade);


        _skillBar.InitTopPlayerCatFollowBar(this.gameObject);
        _skillBar.SetNekoSkillBar(_skillMax, 0);

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

    }




    public Vector3 GetCurrentPosition() {
        return this.transform.position;
    }
}
