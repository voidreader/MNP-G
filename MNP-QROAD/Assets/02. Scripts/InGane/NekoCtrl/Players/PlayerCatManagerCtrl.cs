using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCatManagerCtrl : MonoBehaviour {
    static PlayerCatManagerCtrl _instance = null;

    [SerializeField] List<PhysicsPlayerCatCtrl> _listPhysicsPlayerCats;

    #region Properties 

    public static PlayerCatManagerCtrl Instance {
        get {
            if (_instance == null) {
                _instance = FindObjectOfType(typeof(PlayerCatManagerCtrl)) as PlayerCatManagerCtrl;

                if (_instance == null) {
                    Debug.Log("PlayerCatManagerCtrl Init Error");
                    return null;
                }
            }

            return _instance;
        }
    }

    #endregion



    /// <summary>
    /// 화면 상단의 플레이어 장착 고양이 초기화 
    /// 일반 스테에지에서만 사용한다.
    /// 보스 스테이지, 구출 스테이지에서는 호출 되면 안된다. 
    /// </summary>
    public void InitPlayerCats() {

        int id, grade;
        float skillmax;

        for( int i =0; i<_listPhysicsPlayerCats.Count;i++) {
            
            if(GameSystem.Instance.ListEquipNekoID[i] < 0) { // 장착중이지 않은 경우는 보여주지 않음 
                _listPhysicsPlayerCats[i].HideCat();
            }
            else {
                id = GameSystem.Instance.ListEquipNekoID[i];
                grade = GameSystem.Instance.FindNekoStarByNekoID(id);

                // 최종 skillmax 값.
                skillmax = InGameCtrl.Instance.SkillInvokeBlockCount;

                _listPhysicsPlayerCats[i].InitPlayerCat(id, grade, InGameCtrl.Instance.SkillInvokeValue, skillmax, i);
            }
        }
    }


    /// <summary>
    /// 
    /// </summary>
    public void SetInactiveManager() {
        for(int i=0; i<_listPhysicsPlayerCats.Count; i++) {
            _listPhysicsPlayerCats[i].HideCat();
        }
    }

    public Vector3 GetTargetCatPos(int pIndex) {
        return _listPhysicsPlayerCats[pIndex].GetCurrentPosition();
    }

}
