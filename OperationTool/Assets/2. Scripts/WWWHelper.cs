using UnityEngine;
using System.Collections;
using SimpleJSON;
using BestHTTP;
using System.Text;

public class WWWHelper : MonoBehaviour {

    static WWWHelper _instance = null;

    JSONNode _dataForm;
    public string _requestURL = string.Empty;
    //ec2-13-124-50-170.ap-northeast-2.compute.amazonaws.com
    string _data = string.Empty;

    string _id = string.Empty;
    string _pwd = string.Empty;


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
    /// URL 세팅 
    /// </summary>
    /// <param name="url"></param>
    public void SetURL(string url, string id, string pwd) {
        _requestURL = url;
        _id = id;
        _pwd = pwd;
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
        _dataForm["data"]["id"] = _id;
        

        switch (requestID) {

            case "request_login":
                _dataForm["data"]["pwd"] = _pwd;
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
                    _dataForm["data"]["checked"].AsInt = 0;
                else
                    _dataForm["data"]["checked"].AsInt = 1;

                _dataForm["data"]["message"] = pNode["message"];

                break;
        }


        _data = _dataForm.ToString();

        Debug.Log(">>> Post URL :: " + _requestURL);
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
