using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathologicalGames;
using BestHTTP;
using SimpleJSON;

public class NoticeSceneCtrl : MonoBehaviour {

    private List<NoticeColCtrl> listCols = new List<NoticeColCtrl>();
    JSONNode _nodeResultList;
    static NoticeSceneCtrl _instance = null;

    public static NoticeSceneCtrl Instance {
        get {
            if (_instance == null) {
                _instance = FindObjectOfType(typeof(NoticeSceneCtrl)) as NoticeSceneCtrl;

                if (_instance == null) {
                    Debug.Log("NoticeSceneCtrl Init Error");
                    return null;
                }
            }

            return _instance;
        }
    }

    public List<NoticeColCtrl> ListCols {
        get {
            return listCols;
        }

        set {
            listCols = value;
        }
    }

    public JSONNode NodeResultList {
        get {
            return _nodeResultList;
        }

        set {
            _nodeResultList = value;
        }
    }

    void Start() {
    }



    /// <summary>
    /// 편집된 리스트 저장 
    /// </summary>
    public void SaveNoticeList() {
        for(int i=0;i<ListCols.Count; i++) {
            ListCols[i].SaveModifiedInfo();
        }

        // 다했으면 전송
        WWWHelper.Instance.Post2WithJSON("request_savenotice", OnFinishedRequestNoticeList, NodeResultList);
    }

    /// <summary>
    /// 
    /// </summary>
    public void RequestNoticeList() {

        JSONNode node = JSON.Parse("{}");
        ListCols.Clear();
        
        // 접속 
        WWWHelper.Instance.Post2WithJSON("request_noticelist", OnFinishedRequestNoticeList, node);
    }



    private void OnFinishedRequestNoticeList(HTTPRequest request, HTTPResponse response) {

        NodeResultList = JSON.Parse(response.DataAsText);

        Debug.Log(">>> request.State :: " + request.State.ToString());

        if (request.State == HTTPRequestStates.ConnectionTimedOut || request.State == HTTPRequestStates.TimedOut
            || request.State == HTTPRequestStates.Error) {

            if (!string.IsNullOrEmpty(request.Exception.Message)) {
                Debug.Log("Request Exception :: " + request.Exception.Message);
            }

        }

        Debug.Log("★★ request_noticelist :: " + NodeResultList.ToString());

        if (NodeResultList["result"].AsInt != 0) {
            return;
        }


        NodeResultList = NodeResultList["data"]["list"];

        // 개수만큼 Spawn 및 세팅 
        for(int i=0; i< NodeResultList.Count; i++) {
            ListCols.Add(PoolManager.Pools["notice"].Spawn("NoticeCol", Vector3.zero, Quaternion.identity).GetComponent<NoticeColCtrl>());
            ListCols[i].SetNoticeCol(NodeResultList[i], i);
        }
    }

}
