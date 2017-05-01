using UnityEngine;
using System.Collections;
using SimpleJSON;
using BestHTTP;
using System.Text;

public class WWWHelper : MonoBehaviour {

    static WWWHelper _instance = null;

    JSONNode _dataForm;
    public string _requestURL = "http://ec2-13-124-50-170.ap-northeast-2.compute.amazonaws.com:7120/op";
    // public string _router = string.Empty;
    string _data = string.Empty;


    public static WWWHelper Instance {
        get {
            if (_instance == null) {
                _instance = FindObjectOfType(typeof(WWWHelper)) as WWWHelper;

                if (_instance == null) {
                    Debug.Log("WWWHelper Init Error");
                    return null;
                }
            }

            return _instance;
        }
    }

    void Start() {
        DontDestroyOnLoad(this.gameObject);
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="requestID"></param>
    /// <param name="pCallback"></param>
    /// <param name="pNode"></param>
    public void Post2WithJSON(string requestID, OnRequestFinishedDelegate pCallback, JSONNode pNode) {
        _dataForm = JSON.Parse("{}");

        
        _dataForm["cmd"] = requestID;

        switch (requestID) {
            case "request_checkundermaintenance":
                break;


            case "request_updatemissionprogress":
                _dataForm["data"]["missionid"].AsInt = pNode["tid"].AsInt;
                _dataForm["data"]["missiontype"] = pNode["missiontype"].Value;
                _dataForm["data"]["current"].AsInt = pNode["current"].AsInt;
                _dataForm["data"]["progress"].AsInt = pNode["progress"].AsInt;
                break;


            case "request_powerupgrade":
                _dataForm["data"]["nextlevel"].AsInt = pNode["nextlevel"].AsInt;
                _dataForm["data"]["price"].AsInt = pNode["price"].AsInt;
                _dataForm["data"]["pricetype"] = pNode["pricetype"].Value;

                break;
        }


        _data = _dataForm.ToString();
        Debug.Log(">>> Post _data :: " + _data);

        HTTPRequest request = new HTTPRequest(new System.Uri(_requestURL), HTTPMethods.Post, pCallback);
        request.SetHeader("Content-Type", "application/json; charset=UTF-8");
        request.RawData = Encoding.UTF8.GetBytes(_data);
        request.Tag = pNode;

        request.ConnectTimeout = System.TimeSpan.FromSeconds(15);
        request.Timeout = System.TimeSpan.FromSeconds(30);

        

        request.Send();
    }
}
