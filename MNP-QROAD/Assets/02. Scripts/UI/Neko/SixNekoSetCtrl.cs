using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathologicalGames;

public class SixNekoSetCtrl : MonoBehaviour {

    [SerializeField] List<OwnCatCtrl> _listSixCat;
    [SerializeField] int _id = -1;
    int _tmpInt;

    public int Id {
        get {
            return _id;
        }

        set {
            _id = value;
        }
    }

    public void SetPos(int pPos) {

        Id = pPos;
        this.transform.localPosition = new Vector3(pPos * 660, -160, 0);

    }
	
    /// <summary>
    /// 리스트 초기화 
    /// </summary>
    /// <param name="pMin"></param>
    /// <param name="pMax"></param>
    public void InitList(int pMin, int pMax) {
        _tmpInt = 0;

        this.gameObject.SetActive(true);

        // 정렬된 고양이 캐릭터 Count. 
        /*
        for (int i = 0; i < GameSystem.Instance.ListSortUserNeko.Count; i++) {
            ListCharacterList[i].SetCharacterInfo(GameSystem.Instance.ListSortUserNeko[i]);
        }
        */

        // 
        for (int i=pMin; i<=pMax; i++) {
            _listSixCat[_tmpInt].SetCharacterInfo(GameSystem.Instance.ListSortUserNeko[i]);
            _listSixCat[_tmpInt].gameObject.SetActive(true);
            _tmpInt++;
        }
    }

    public void SetDisable() {
        this.gameObject.SetActive(false);
    }

    public bool CheckExistsNekoByID(int pNekoID) {

        for(int i=0; i<_listSixCat.Count; i++) {

            if (!_listSixCat[i].gameObject.activeSelf)
                continue;

            if (_listSixCat[i].Id == pNekoID)
                return true;
        }

        return false;
    }


    void OnSpawned() {

        this.transform.localScale = GameSystem.Instance.BaseScale;

        for(int i=0; i<_listSixCat.Count; i++) {
            _listSixCat[i].gameObject.SetActive(false);
            

        }
    }
	

}
