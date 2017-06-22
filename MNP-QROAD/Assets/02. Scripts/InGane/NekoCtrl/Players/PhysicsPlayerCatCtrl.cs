using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PhysicsPlayerCatCtrl : MonoBehaviour {

    [SerializeField] NekoAppearCtrl _appear; // 외형 UI (NGUI)
    [SerializeField] NekoFollowHP _hpBar;

    [SerializeField] int _nekoid;
    [SerializeField] int _grade;
    [SerializeField] float _skillMax;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other) {
        
    }

    /// <summary>
    /// 고양이 초기화 
    /// </summary>
    /// <param name="pNekoID"></param>
    /// <param name="pGrade"></param>
    /// <param name="pSkillMax"></param>
    public void InitPlayerCat(int pNekoID, int pGrade, float pSkillMax, int pIndex) {

        _nekoid = pNekoID;
        _grade = pGrade;
        _skillMax = pSkillMax;

        _appear.InitNekoAppear(this.transform, pNekoID, pGrade);




    }


}
