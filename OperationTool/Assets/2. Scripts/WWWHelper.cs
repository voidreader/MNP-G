using UnityEngine;
using System.Collections;
using SimpleJSON;
using BestHTTP;
using System.Text;

public class WWWHelper : MonoBehaviour {

    static WWWHelper _instance = null;

    JSONNode _dataForm;
    private string requestURL = string.Empty;
    //ec2-13-124-50-170.ap-northeast-2.compute.amazonaws.com
    string _data = string.Empty;

    string _id = string.Empty;
    string _pwd = string.Empty;

    bool _isConnected = false;


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

    public string Id {
        get {
            return _id;
        }

        set {
            _id = value;
        }
    }

    public string Pwd {
        get {
            return _pwd;
        }

        set {
            _pwd = value;
        }
    }

    public string RequestURL {
        get {
            return requestURL;
        }

        set {
            requestURL = value;
        }
    }

    public bool IsConnected {
        get {
            return _isConnected;
        }

        set {
            _isConnected = value;
        }
    }

    void Start() {
        DontDestroyOnLoad(this.gameObject);
    }

    /// <summary>
    /// URL 세팅 
    /// </summary>
    /// <param name="url"></param>
    public void SetURL(string url, string id, string pwd) {
        RequestURL = url;
        Id = id;
        Pwd = pwd;

        
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
        _dataForm["data"]["id"] = Id;
        

        switch (requestID) {

            case "request_login":
                _dataForm["data"]["pwd"] = Pwd;
                break;


            case "request_findid":
                _dataForm["data"]["userdbkey"] = pNode["id"];
                break;


            case "request_updatealluserstage":
                _dataForm["data"]["userdbkey"] = pNode["id"];
                _dataForm["data"]["stage"] = pNode["stage"];
                break;

            case "request_updateuserstage":
                _dataForm["data"]["userdbkey"] = pNode["id"];
                _dataForm["data"]["stage"] = pNode["stage"];
                break;

            case "request_inituserstage":
                _dataForm["data"]["userdbkey"] = pNode["id"];
                break;


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

            case "request_setmaintenancesimple":

                if(pNode["checked"].AsBool)
                    _dataForm["data"]["checked"].AsInt = 1;
                else
                    _dataForm["data"]["checked"].AsInt = 0;

                _dataForm["data"]["message"] = pNode["message"];

                break;

            case "request_sendmailtoeveryone":
                _dataForm["data"]["mailtype"].AsInt = pNode["mailtype"].AsInt;
                _dataForm["data"]["gifttype"].AsInt = pNode["gifttype"].AsInt;
                _dataForm["data"]["when"].AsInt = pNode["when"].AsInt;
                _dataForm["data"]["expire"].AsInt = pNode["expire"].AsInt;
                _dataForm["data"]["quantity"].AsInt = pNode["quantity"].AsInt;

                break;
        }


        _data = _dataForm.ToString();

        Debug.Log(">>> Post URL :: " + RequestURL);
        Debug.Log(">>> Post _data :: " + _data);

        HTTPRequest request = new HTTPRequest(new System.Uri(RequestURL), HTTPMethods.Post, pCallback);
        request.SetHeader("Content-Type", "application/json; charset=UTF-8");
        request.RawData = Encoding.UTF8.GetBytes(_data);
        request.Tag = pNode;

        request.ConnectTimeout = System.TimeSpan.FromSeconds(15);
        request.Timeout = System.TimeSpan.FromSeconds(30);

        

        request.Send();
    }
}
