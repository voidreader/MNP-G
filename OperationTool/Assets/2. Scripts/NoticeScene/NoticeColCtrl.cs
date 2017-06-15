using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathologicalGames;
using SimpleJSON;

public class NoticeColCtrl : MonoBehaviour {

    /* 배너 내용 */
    public string _stat = string.Empty;
    public string _no = string.Empty;
    public string _bannerURL = string.Empty;
    public string _text = string.Empty;
    public string _action = string.Empty;
    public string _smallTitle = string.Empty;
    public string _locale = string.Empty;
    public string _key = string.Empty;

    // Controls 
    public UIPopupList _listStat;
    public UIPopupList _listLocale;
    public UIPopupList _listAction;
    public UIInput _iptNo;
    public UIInput _iptURL;
    public UIInput _iptSmallTitle;

    JSONNode _masterNode;

    /// <summary>
    /// 
    /// </summary>
    public void SaveModifiedInfo() {


        // 상태가 변한 경우에만 
        if (_stat == "-")
            return;

        _masterNode = NoticeSceneCtrl.Instance.NodeResultList;


        // key값을 찾아 변경 
        for(int i =0; i<_masterNode.Count;i++ ) {
            if (_masterNode["key"] != _key)
                continue;

            _masterNode["stat"] = _stat;
            _masterNode["no"] = _no;
            _masterNode["_locale"] = _locale;
            _masterNode["smallbannerurl"] = _bannerURL;
            _masterNode["bannertext"] = _text;
            _masterNode["action"] = _action;
            _masterNode["updatedate"] = _smallTitle;
        }
    }

    /// <summary>
    /// 
    /// 공지사항 정보 세팅 
    /// </summary>
    /// <param name="pNode"></param>
    /// <param name="pPos"></param>
    public void SetNoticeCol(JSONNode pNode, int pPos) {

        _stat = "-";
        _no = pNode["no"];
        _locale = pNode["locale"];
        _bannerURL = pNode["smallbannerurl"];
        _text = pNode["bannertext"];
        _action = pNode["action"];
        _smallTitle = pNode["updatedate"];

        _key = pNode["key"];

        _listStat.value = _stat;
        _listAction.value = _action;
        _listLocale.value = _locale;

        _iptNo.value = _no;
        _iptSmallTitle.value = _smallTitle;
        _iptURL.value = _bannerURL;

        // 위치 지정
        this.transform.localScale = new Vector3(1, 1, 1);
        this.transform.localPosition = new Vector3(0, pPos * -1 * 60, 0);
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="pText"></param>
    public void SetNoticeText(string pText) {
        if (_text == pText)
            return;



        _text = pText;

        // 상태값 변경 
        SetStat("U");
    }


    /// <summary>
    /// 삭제 
    /// </summary>
    public void SetDelete() {
        this.SetStat("D");
    }

    /// <summary>
    /// 상태값 설정 
    /// </summary>
    /// <param name="pStat"></param>
    public void SetStat(string pStat) {
        _stat = pStat;
        _listStat.value = _stat;
    }
    



}
