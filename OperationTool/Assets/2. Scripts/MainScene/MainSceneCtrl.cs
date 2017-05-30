using UnityEngine;
using System.Collections;
using BestHTTP;
using SimpleJSON;

public class MainSceneCtrl : MonoBehaviour {


    static MainSceneCtrl _instance = null;

    public UILabel _lblResult;
    public UIInput _iptURL;
    public UIInput _iptAccount;
    public UIInput _iptPWD;


    [SerializeField] string _id;
    [SerializeField] string _url;



    public static MainSceneCtrl Instance {
        get {
            if (_instance == null) {
                _instance = FindObjectOfType(typeof(MainSceneCtrl)) as MainSceneCtrl;

                if (_instance == null) {
                    Debug.Log("MainSceneCtrl Init Error");
                    return null;
                }
            }

            return _instance;
        }
    }

    #region Properties 

    public string Id {
        get {
            return _id;
        }

        set {
            _id = value;
        }
    }

    public string Url {
        get {
            return _url;
        }

        set {
            _url = value;
        }
    }

    #endregion

    // Use this for initialization
    void Start () {
        DontDestroyOnLoad(this.gameObject);
	}
	
	public void GoMailScene() {
        Application.LoadLevel("MailScene");
    }

    public void GoMaintenanceScene() {
        Application.LoadLevel("MaintenanceScene");
    }

    public void GoStageScene() {
        Application.LoadLevel("StageScene");
    }


    #region HTTP

    public void ConnectServer() {

        if (!CheckValidateConnect())
            return;

        // 접속 
        WWWHelper.Instance.SetURL(_iptURL.value, _iptAccount.value, _iptPWD.value);
        WWWHelper.Instance.Post2WithJSON("request_login", OnFinishedLogin, null);

    }

    private void OnFinishedLogin(HTTPRequest request, HTTPResponse response) {

        JSONNode result = JSON.Parse(response.DataAsText);

        Debug.Log(">>> request.State :: " + request.State.ToString());

        if (request.State == HTTPRequestStates.ConnectionTimedOut || request.State == HTTPRequestStates.TimedOut
            || request.State == HTTPRequestStates.Error) {


            if (!string.IsNullOrEmpty(request.Exception.Message)) {
                Debug.Log("Request Exception :: " + request.Exception.Message);
            }

        }

        Debug.Log("★★ OnFinishedLogin :: " + result.ToString());

        result = result["data"];

    }


    /// <summary>
    /// 접속정보 유효성 체크 
    /// </summary>
    /// <returns></returns>
    bool CheckValidateConnect() {


        _lblResult.gameObject.SetActive(false);


        if (string.IsNullOrEmpty(_iptURL.value)) {
            _lblResult.gameObject.SetActive(true);
            _lblResult.text = "접속 URL이 입력되지 않았습니다.";
            return false;
        }

        if (string.IsNullOrEmpty(_iptAccount.value)) {
            _lblResult.gameObject.SetActive(true);
            _lblResult.text = "ID가 입력되지 않았습니다.";
            return false;
        }


        if (string.IsNullOrEmpty(_iptPWD.value)) {
            _lblResult.gameObject.SetActive(true);
            _lblResult.text = "패스워드가 입력되지 않았습니다.";
            return false;
        }

        return true;

    }

    


    #endregion
}
