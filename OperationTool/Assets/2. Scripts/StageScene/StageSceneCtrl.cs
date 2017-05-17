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

    public GameObject _lblErr;


    public void FindID() {
        JSONNode node = JSON.Parse("{}");
        _lblErr.SetActive(false);

        if (string.IsNullOrEmpty(_iptFindID.value))
            return;

        node["id"] = _iptFindID.value;

        WWWHelper.Instance.Post2WithJSON("request_findid", OnFinishedFindID, node);
    }
        

    public void ProceedUserStage() {

    }


    public void InitUserStage() {

    }


    
    private void OnFinishedFindID(HTTPRequest request, HTTPResponse response) {

        JSONNode result = JSON.Parse(response.DataAsText);

        Debug.Log("★★ OnFinishedFindID :: " + result.ToString());
        
        result = result["data"];

        if(result["id"].AsInt <= 0) {

            // ID 없음
            _lblErr.SetActive(true);
            return;
        }

        _findingID = result["id"].AsInt;
        _name = result["name"];
        _currentStage = result["stage"].AsInt;

        _lblUserID.text = _findingID.ToString() + " : ";
        _lblUserName.text = _name;
        _lblUserStage.text = _currentStage.ToString();

    }
}
