using System.Collections;
using System.Collections.Generic;
using BestHTTP;
using SimpleJSON;
using UnityEngine;

public class StageSceneCtrl : MonoBehaviour {

    public int _findingID = 0; // 찾을 아이디
    public int _findedID = 0; // 찾은 아이디 
    public string _name;
    public int _currentStage = 0;

    public MessageBoxCtrl _messageBox;

    public UIInput _iptFindID;
    public UIInput _iptStage;
    public UILabel _lblUserID;
    public UILabel _lblUserName;
    public UILabel _lblUserStage;

    public UILabel _lblResult;

    public GameObject _lblErr;


    void InitResult() {
        _lblErr.SetActive(false);
        _lblResult.gameObject.SetActive(false);
    }

    public void FindID() {
        JSONNode node = JSON.Parse("{}");

        InitResult();

        if (string.IsNullOrEmpty(_iptFindID.value))
            return;

        node["id"] = _iptFindID.value;

        WWWHelper.Instance.Post2WithJSON("request_findid", OnFinishedFindID, node);
    }



    #region ~까지 스테이지 클리어 
    public void ProceedAllUserStage() {
        InitResult();

        if (_findingID <= 0)
            return;

        if (string.IsNullOrEmpty(_iptStage.value))
            return;

        //node["id"] = _iptFindID.value;
        //WWWHelper.Instance.Post2WithJSON("request_findid", OnFinishedFindID, node);
        string msg = _findingID + " : " + _name + " 유저의 스테이지를 [" + _iptStage.value + "]까지 클리어 시키겠습니까?";

        _messageBox.OpenMessageBox(PostProceedAllUserStage, msg);
    }

    void PostProceedAllUserStage() {
        JSONNode node = JSON.Parse("{}");
        node["id"] = _findingID.ToString();
        node["stage"] = _iptStage.value;

        WWWHelper.Instance.Post2WithJSON("request_updatealluserstage", OnFinishedInitUserStage, node);
    }

    #endregion



    public void ProceedUserStage() {
        InitResult();

        if (_findingID <= 0)
            return;

        if (string.IsNullOrEmpty(_iptStage.value))
            return;

        //node["id"] = _iptFindID.value;
        //WWWHelper.Instance.Post2WithJSON("request_findid", OnFinishedFindID, node);
        string msg = _findingID + " : " + _name + " 유저의 스테이지 [" + _iptStage.value + "]을 클리어 하겠습니까?";

        _messageBox.OpenMessageBox(PostProceedUserStage, msg);
    }

    void PostProceedUserStage() {
        JSONNode node = JSON.Parse("{}");
        node["id"] = _findingID.ToString();
        node["stage"] = _iptStage.value;

        WWWHelper.Instance.Post2WithJSON("request_updateuserstage", OnFinishedInitUserStage, node);
    }




    #region 유저 스테이지 초기화 
    public void InitUserStage() {


        InitResult();

        if (_findingID <= 0)
            return;

        //node["id"] = _iptFindID.value;
        //WWWHelper.Instance.Post2WithJSON("request_findid", OnFinishedFindID, node);
        string msg = _findingID + " : " + _name + " 유저의 스테이지 진행 정보를 모두 초기화 시키겠습니까?";

        _messageBox.OpenMessageBox(PostInitUserStage, msg);
    }
    
    void PostInitUserStage() {
        JSONNode node = JSON.Parse("{}");
        node["id"] = _findingID.ToString();
        WWWHelper.Instance.Post2WithJSON("request_inituserstage", OnFinishedInitUserStage, node);
    }

    private void OnFinishedInitUserStage(HTTPRequest request, HTTPResponse response) {

        JSONNode result = JSON.Parse(response.DataAsText);
        Debug.Log("★★ OnFinishedInitUserStage :: " + result.ToString());
        result = result["data"];


        // ID 전달 
        OnFinishedFindID(request, response);


        _lblResult.text = "처리가 완료되었습니다.";
        _lblResult.gameObject.SetActive(true);



    }
    #endregion




    private void OnFinishedProceedAllUserStage(HTTPRequest request, HTTPResponse response) {

        JSONNode result = JSON.Parse(response.DataAsText);

        Debug.Log("★★ OnFinishedProceedUserStage :: " + result.ToString());

        result = result["data"];

    }


    private void OnFinishedProceedUserStage(HTTPRequest request, HTTPResponse response) {

        JSONNode result = JSON.Parse(response.DataAsText);

        Debug.Log("★★ OnFinishedProceedUserStage :: " + result.ToString());

        result = result["data"];

    }




    private void OnFinishedFindID(HTTPRequest request, HTTPResponse response) {

        JSONNode result = JSON.Parse(response.DataAsText);

        Debug.Log("★★ OnFinishedFindID :: " + result.ToString());
        
        result = result["data"];

        if(result["id"].AsInt <= 0) {

            // ID 없음
            _lblErr.SetActive(true);

            _findingID = 0;
            _name = string.Empty;
            _currentStage = 0;

            _lblUserID.text = string.Empty;
            _lblUserName.text = string.Empty;
            _lblUserStage.text = string.Empty;

            return;
        }

        _lblErr.SetActive(false);

        _findingID = result["id"].AsInt;
        _name = result["name"];
        _currentStage = result["stage"].AsInt;

        _lblUserID.text = "ID : " + _findingID.ToString();
        _lblUserName.text = "NAME : " +_name;
        _lblUserStage.text = "현재 스테이지 : " +_currentStage.ToString();

    }
}
