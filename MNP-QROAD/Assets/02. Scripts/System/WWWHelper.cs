using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using SimpleJSON;
using BestHTTP;
using CodeStage.AntiCheat.ObscuredTypes;
using Google2u;

public class WWWHelper : MonoBehaviour {

	static WWWHelper _instance = null;

    MNP_HTTP _httpProtocol = MNP_HTTP.Instance;
    

    [SerializeField]
    bool _isTestMode = false;

	// 기초 dataset 
	//[SerializeField] TextAsset _text; // 기초  폼을 조회할 json text asset 
	[SerializeField] ObscuredString _data; // 실제 서버로 날릴 string data 
	[SerializeField] JSONNode _dataForm; // 기초 폼 JsonNode

	[SerializeField] ObscuredString _token;
	[SerializeField] ObscuredInt _dbkey;
    [SerializeField] int _root = 0;

	
    [SerializeField] string _deviceuniqueID;

    /* 결제 사용 */
	[SerializeField] string _currentSKU = null;
    [SerializeField] string _packageSKU = null;
    [SerializeField] string _currentPackage = null;

    [SerializeField] string _sendMissionType;
    [SerializeField] int _sendMissionID;
    [SerializeField] MissionColumnCtrl _sendMission;

    [SerializeField] int _preItemAdsID = -1;
    [SerializeField] int _itemAdsID = 0;

    HTTPRequest _previousRequest = null;

    string _pushDeviceToken = "";

    [SerializeField]
    int _adsID = 0;

	private Hashtable _header = new Hashtable();
	private string _requestURL = null;

	private ObscuredString _preData;
	private string _preRequestURL;
	private string _preRequestID;

    private string _url = "baseURL"; // live server

    //string _baseURL = "baseURL"; // 라이브 서버 
    //string _testURL = "backURL";  // 테스트 서버 

    [SerializeField] string _baseURL = string.Empty;
    [SerializeField] string _testURL = string.Empty;

    string _requestedNickName = "";
    readonly string _parsingText = "{}";

	public float timer = 0;
	// public float timeout = 10;
	public bool failed = false;

	// 이벤트 델리게이터 
	//public delegate void HttpRequestDelegate(string requestID, WWW www);
	//public event HttpRequestDelegate OnHttpRequest = null;


	public static WWWHelper Instance {
		get {
			if(_instance == null) {
				_instance = FindObjectOfType(typeof(WWWHelper)) as WWWHelper;
				
				if(_instance == null) {
					Debug.Log("WWWHelper Init Error");
					return null;
				}
			}
			
			return _instance;
		}
	}

	void Awake() {
		DontDestroyOnLoad (this.gameObject);
	}

	// Use this for initialization
	void Start () {
        //_text = Resources.Load("JSON/UserForm") as TextAsset;
        //_dataForm = JSON.Parse(_text.ToString());
        _dataForm = JSON.Parse("{}");
		_header.Add("Content-Type", "application/json; charset=UTF-8");


        _baseURL = EnvManagerCtrl.Instance.liveServerURL;
        _testURL = EnvManagerCtrl.Instance.testServerURL;

        _url = _baseURL;

        if (IsTestMode)
            _url = _testURL;

        
    }


    /// <summary>
    /// Connect Server 주소 설정 
    /// </summary>
    /// <param name="pLiveServer"></param>
    public void SetConnectServerURL(bool pLiveServer) {


        //_baseURL = EnvManagerCtrl.Instance.liveServerURL;
        //_testURL = EnvManagerCtrl.Instance.testServerURL;

        if (pLiveServer) {
            _url = _baseURL;
        }
        else {
            _url = _testURL;
            _isTestMode = true;
        }

        // 무조건 테스트 서버 접속 
        if(IsTestMode) {
            _url = _testURL;
        }


        Debug.Log("★SetConnectServerURL :: " + _url);
    }

	/// <summary>
	/// BEST HTTP
	/// </summary>
	/// <param name="requestID">Request I.</param>
	/// <param name="pCallback">P callback.</param>
	public void Post2(string requestID, OnRequestFinishedDelegate pCallback) {
		Post2 (requestID, pCallback, -1);
	}

	public void Post2(string requestID, OnRequestFinishedDelegate pCallback, int pParam) {
        //_dataForm = JSON.Parse(_text.ToString());
        _dataForm = JSON.Parse(_parsingText);

        _requestURL = _url + _httpProtocol.Rows[(int)MNP_HTTP.rowIds.url].GetStringData(requestID);
        //_requestURL = _httpProtocol.Rows[(int)MNP_HTTP.rowIds.url].GetStringData(_url) + _httpProtocol.Rows[(int)MNP_HTTP.rowIds.url].GetStringData(requestID);

        Debug.Log(">>> Post _requestURL :: " + _requestURL);

        _dataForm["cmd"] = requestID;

        if (!"request_login".Equals(requestID) && !"request_dataversion".Equals(requestID) && !"request_gamedata".Equals(requestID) && !"request_findaccount".Equals(requestID)) {
            _dataForm["data"]["token"] = _token.ToString();
            _dataForm["data"]["userdbkey"].AsInt = _dbkey;
        }


        switch (requestID) {


            case "request_rescuerewardupdate":
                _dataForm["data"]["themeid"].AsInt = pParam;
                break;

            case "request_takethemepay":

                _dataForm["data"]["themeid"].AsInt = pParam;

                break;



            case "request_gamedata":
                for (int i = 0; i < GameSystem.Instance.RequestGameData.Count; i++) {
                    _dataForm["data"]["gamedata"][-1] = GameSystem.Instance.RequestGameData[i];
                }

                // 언어 정보 추가 
                _dataForm["data"]["lang"] = GameSystem.Instance.GameLanguage.ToString();

                break;

            case "request_findaccount":

                _dataForm["data"]["userid"] = GameSystem.Instance.UserID;
                _dataForm["data"]["deviceid"] = GameSystem.Instance.DeviceID;
                _dataForm["data"]["platform"] = GameSystem.Instance.Platform;
                _dataForm["data"]["facebookid"] = GameSystem.Instance.FacebookID;
                _dataForm["data"]["ver"] = GameSystem.Instance.GameVersion;


#if UNITY_IOS
                _dataForm["data"]["devicetoken"] = PushDeviceToken;
                _dataForm["data"]["uuid"] = SystemInfo.deviceUniqueIdentifier;
#else
                _dataForm["data"]["devicetoken"] = "None";
                _dataForm["data"]["uuid"] = GameSystem.Instance.JavaAndroidID;
#endif

                break;


            case "request_login":
                _dataForm["data"]["userdbkey"].AsInt = _dbkey;
                _dataForm["data"]["root"].AsInt = _root;
                _dataForm["data"]["ver"] = GameSystem.Instance.GameVersion;

                _dataForm["data"]["lang"] = GameSystem.Instance.GameLanguage.ToString();

                break;

                
            case "request_userrecord": // 사용자 실적 정보 조회
                _dataForm["data"]["targetdbkey"].AsInt = pParam;
                break;



            case "request_updateuserbingo":
                _dataForm["data"]["currentbingoid"].AsInt = pParam ;
                break;

            case "request_itemads":
                if(GameSystem.Instance.AdsID != WWWHelper.Instance.AdsID) {
                    print("★★★ Ads ID not equal!!!!");
                    return;
                }

                break;


            case "request_facebookinvite":
                _dataForm["data"]["friend"].AsInt = pParam;
                break;

            case "request_clearbingo":
                for (int i = 0; i < GameSystem.Instance.ListClearedBingoCols.Count; i++) {
                    _dataForm["data"]["clearbingo"][-1].AsInt = GameSystem.Instance.ListClearedBingoCols[i];
                }

                for (int i = 0; i < GameSystem.Instance.ListClearedBingoLines.Count; i++) {
                    _dataForm["data"]["clearline"][-1].AsInt = GameSystem.Instance.ListClearedBingoLines[i];
                }
                break;

            case "request_gameresult": // 게임결과 전송 

                _dataForm["data"]["exp"].AsInt = 0;
                _dataForm["data"]["combo"].AsInt = GameSystem.Instance.InGameMaxCombo;
                _dataForm["data"]["score"].AsInt = GameSystem.Instance.InGameTotalScore; // 최종 스코어 
                _dataForm["data"]["blocks"].AsInt = GameSystem.Instance.MatchedBlueBlock + GameSystem.Instance.MatchedRedBlock + GameSystem.Instance.MatchedYellowBlock; // 소거 블록 수 
                _dataForm["data"]["getgold"].AsInt = GameSystem.Instance.InGameTotalCoin; // 최종 코인 
                _dataForm["data"]["getgem"].AsInt = GameSystem.Instance.InGameDiamond;
                _dataForm["data"]["getticket"].AsInt = GameSystem.Instance.InGameTicket;

                _dataForm["data"]["damage"].AsInt = GameSystem.Instance.InGameDamage; // 데미지

                // 생선류 
                _dataForm["data"]["getchub"].AsInt = GameSystem.Instance.InGameChub;
                _dataForm["data"]["gettuna"].AsInt = GameSystem.Instance.InGameTuna;
                _dataForm["data"]["getsalmon"].AsInt = GameSystem.Instance.InGameSalmon;


                // 스테이지 미션 정보
                JSONNode currentUserStage = GameSystem.Instance.UserStageJSON["stagelist"][GameSystem.Instance.PlayStage - 1]; // 유저 스테이지 정보 

                // 현재 플레이한 스테이지에 대한 정보 
                _dataForm["data"]["stage"].AsInt = GameSystem.Instance.PlayStage;
                _dataForm["data"]["prestate"].AsInt = currentUserStage["state"].AsInt; // 플레이하기 전의 달성도. 

                // 이번 스테이지에서 달성한 진척도 정보
                _dataForm["data"]["state"].AsInt = GameSystem.Instance.CloneUserStageJSON["state"].AsInt;
                _dataForm["data"]["progress1"].AsInt = GameSystem.Instance.CloneUserStageJSON["progress1"].AsInt;
                _dataForm["data"]["progress2"].AsInt = GameSystem.Instance.CloneUserStageJSON["progress2"].AsInt;
                _dataForm["data"]["progress3"].AsInt = GameSystem.Instance.CloneUserStageJSON["progress3"].AsInt;
                _dataForm["data"]["progress4"].AsInt = GameSystem.Instance.CloneUserStageJSON["progress4"].AsInt;


                // 사용한 네코 정보
                for (int i = 0; i < GameSystem.Instance.ListEquipNekoID.Count; i++) {
                    if (GameSystem.Instance.ListEquipNekoID[i] != -1) {
                        _dataForm["data"]["useneko"][-1].AsInt = GameSystem.Instance.ListEquipNekoID[i];
                    }
                }

                for(int i=0; i<GameSystem.Instance.ListClearedBingoCols.Count; i++) {
                    _dataForm["data"]["clearbingo"][-1].AsInt = GameSystem.Instance.ListClearedBingoCols[i];
                }

                for (int i = 0; i < GameSystem.Instance.ListClearedBingoLines.Count; i++) {
                    _dataForm["data"]["clearline"][-1].AsInt = GameSystem.Instance.ListClearedBingoLines[i];
                }

                Debug.Log("▶ post2 request_gameresult Done");

                break;

            case "request_gamestart":

                bool checkExists = false;


                // EquipItem
                for (int i = 0; i < GameSystem.Instance.ListEquipItemID.Count; i++) {

                    checkExists = false;

                    for(int j=0; j<_dataForm["data"]["useitem"].Count; j++) {
                        if (_dataForm["data"]["useitem"][j].AsInt == GameSystem.Instance.ListEquipItemID[i]) {
                            checkExists = true;
                            break;
                        }
                    }


                    if(!checkExists)
                        _dataForm["data"]["useitem"][-1].AsInt = GameSystem.Instance.ListEquipItemID[i];
                }

                // EquipNeko
                for(int i=0; i< GameSystem.Instance.ListEquipNekoID.Count; i++) {
                    _dataForm["data"]["neko"][i]["nekoid"].AsInt = GameSystem.Instance.ListEquipNekoID[i];
                    if (_dataForm["data"]["neko"][i]["nekoid"].AsInt >= 0)
                        _dataForm["data"]["neko"][i]["grade"].AsInt = GameSystem.Instance.FindNekoStarByNekoID(GameSystem.Instance.ListEquipNekoID[i]);
                    else
                        _dataForm["data"]["neko"][i]["grade"].AsInt = -1;
                }


                Debug.Log("▶ post2 request_gamestart Done");

                break;



            case "request_nickname":
                _dataForm["data"]["nickname"] = GameSystem.Instance.UserName;
                break;

            case "request_nekoupgrade":
                _dataForm["data"]["nekodbkey"].AsInt = GameSystem.Instance.UpgradeNekoDBKey;
                _dataForm["data"]["nextlevel"].AsInt = pParam; // 다음레벨 
                break;

            case "request_mainneko":
                _dataForm["data"]["nekodbkey"].AsInt = GameSystem.Instance.UpgradeNekoDBKey;
                break;

            case "request_eatfish":

                _dataForm["data"]["nekodbkey"].AsInt = GameSystem.Instance.UpgradeNekoDBKey;
                _dataForm["data"]["fishes"][0].AsInt = GameSystem.Instance.FeedChub;
                _dataForm["data"]["fishes"][1].AsInt = GameSystem.Instance.FeedTuna;
                _dataForm["data"]["fishes"][2].AsInt = GameSystem.Instance.FeedSalmon;


                break;

            case "request_gacha":

                if (GameSystem.Instance.IsFreeGatcha && GameSystem.Instance.AdsID != WWWHelper.Instance.AdsID) {
                    print("★★★ request_gacha Ads ID not equal!!!!");
                    return;
                }

                _dataForm["data"]["isfree"].AsBool = GameSystem.Instance.IsFreeGatcha;

                if (GameSystem.Instance.IsFreeGatcha)
                    GameSystem.Instance.GatchaCount = 1;

                _dataForm["data"]["event"].AsInt = pParam;
                _dataForm["data"]["count"].AsInt = GameSystem.Instance.GatchaCount;

                break;


            case "request_fishgacha":

                _dataForm["data"]["count"].AsInt = pParam;
                GameSystem.Instance.FishGatchaCount = pParam;

                break;


            case "request_facebooklink":
                _dataForm["data"]["facebookid"] = MNPFacebookCtrl.Instance.UserId;
                break;

            case "request_viewads":
                _dataForm["data"]["adsplaytime"].AsInt = Random.Range(25, 46);

                break;

            case "request_nekoreward":
                _dataForm["data"]["nekodbkey"].AsInt = pParam;
                break;

            case "request_wanted":
                _dataForm["data"]["nekotid"].AsInt = pParam;
                break;

            case "request_mailread":
                _dataForm["data"]["maildbkey"].AsInt = pParam;

                if(GameSystem.Instance.ReadMailColumn.ColumnClass == "rainbowticket") {
                    _dataForm["data"]["exchange_id"].AsInt = GameSystem.Instance.ReadMailColumn.ExchangeNekoID;
                    _dataForm["data"]["exchange_grade"].AsInt = GameSystem.Instance.ReadMailColumn.ExchangeNekoGrade;
                }

                break;

            // passive upgrade 
            case "request_gametimelevel":
            case "request_fevertimelevel":
            case "request_attackpowerlevel":

                _dataForm["cmd"] = "request_passiveupgrade";
                _dataForm["data"]["passiveskill"] = requestID.Replace("request_", "");

                Debug.Log("passiveskill :: " + _dataForm["data"]["passiveskill"]);

                // requestURL 변경 
                //_requestURL = _httpProtocol.Rows[(int)MNP_HTTP.rowIds.url].GetStringData(_url) + _httpProtocol.Rows[(int)MNP_HTTP.rowIds.url].GetStringData(_dataForm"cmd"]);
                _requestURL = _url + _httpProtocol.Rows[(int)MNP_HTTP.rowIds.url].GetStringData(_dataForm["cmd"]);

                requestID = _dataForm["cmd"];
                break;

            case "request_exchangegold":
                _dataForm["data"]["exchangeindex"].AsInt = pParam;
                break;

            case "request_nekogift":
                if (pParam <= 0) {
                    _dataForm["data"]["isads"].AsBool = false;
                    AdbrixManager.Instance.SendAdbrixInAppActivity(AdbrixManager.Instance.NEKO_BONUS_NOADS);
                }
                else {
                    _dataForm["data"]["isads"].AsBool = true;

                    AdbrixManager.Instance.SendAdbrixInAppActivity(AdbrixManager.Instance.NEKO_BONUS_ADS);

                    // 아이디가 다르면 리턴처리.
                    if (GameSystem.Instance.AdsID != WWWHelper.Instance.AdsID) {
                        print("★★★★ AdsID not equal!! request_nekogift");
                    }

                }
                break;

            case "request_missionreward":

                _dataForm["data"]["missiontype"] = SendMissionType;
                _dataForm["data"]["tid"].AsInt = SendMissionID;


                break;



            case "request_payload":

                /* payload를 획득할때 보내는 SKU는 패키지용도와 일반 경제 용도를 구분해서 전송해야 한다. */

                if (string.IsNullOrEmpty(_currentPackage)) { // 일반 결제 
                    _dataForm["data"]["product"] = _currentSKU;
                    _dataForm["data"]["productid"] = _currentSKU;
                }
                else { // 패키지 결제 (packageSKU를 사용)
                    _dataForm["data"]["product"] = _packageSKU;
                    _dataForm["data"]["productid"] = _packageSKU;
                }

                
                break;

            case "request_verifypayload":

#if UNITY_ANDROID
                _dataForm["data"]["payload"] = GameSystem.Instance.BillJSON["developerPayload"].Value;

#elif UNITY_IOS

			//_dataForm ["data"] ["payload"] = GameSystem.Instance.paylo
#endif
                break;

            case "request_purchase":

#if UNITY_IOS

			Debug.Log(">> IOS request_purchase");
			_dataForm["data"]["product"] = _currentSKU;
			_dataForm["data"]["productid"] = _currentSKU;
			_dataForm ["data"] ["receipt"] = GameSystem.Instance.RecieptIOS;

                if (string.IsNullOrEmpty(_currentPackage)) { // 일반 결제 
                    _dataForm["data"]["package"] = "";
                }
                else { // 패키지 결제 (packageSKU를 사용)
                    _dataForm["data"]["package"] = _packageSKU;
                }
            
#elif UNITY_ANDROID

                if (GameSystem.Instance.BillJSON != null)
                    _dataForm["data"]["productresult"] = GameSystem.Instance.BillJSON.ToString();
                else
                    _dataForm["data"]["productresult"] = GameSystem.Instance.SyncTimeData.ToString();


                if (string.IsNullOrEmpty(_currentPackage)) { // 일반 결제 
                    _dataForm["data"]["package"] = "";
                }
                else { // 패키지 결제 (packageSKU를 사용)
                    _dataForm["data"]["package"] = _packageSKU;
                }

#endif

                break;

        }

        _data = _dataForm.ToString();

        Debug.Log(">>> Post _data :: " + _data);
        //Debug.Log(">>> Post _requestURL :: " + _requestURL);

        HTTPRequest request = new HTTPRequest(new System.Uri(_requestURL), HTTPMethods.Post, pCallback);
        request.SetHeader("Content-Type", "application/json; charset=UTF-8");
        request.RawData = Encoding.UTF8.GetBytes(_data);
        
        request.ConnectTimeout = System.TimeSpan.FromSeconds(5);
        request.Timeout = System.TimeSpan.FromSeconds(20);
        
        _previousRequest = request;

        request.Send();
    }



    public void Post2WithJSON(string requestID, OnRequestFinishedDelegate pCallback, JSONNode pNode) {
        //_dataForm = JSON.Parse(_text.ToString());
        
        _dataForm = JSON.Parse(_parsingText);
        
        //_requestURL = _httpProtocol.Rows[(int)MNP_HTTP.rowIds.url].GetStringData(_url) + _httpProtocol.Rows[(int)MNP_HTTP.rowIds.url].GetStringData(requestID);
        _requestURL = _url + _httpProtocol.Rows[(int)MNP_HTTP.rowIds.url].GetStringData(requestID);
        Debug.Log(">>> Post _requestURL :: " + _requestURL);

        _dataForm["cmd"] = requestID;

        if (!"request_login".Equals(requestID)) {
            _dataForm["data"]["token"] = _token.ToString();
            _dataForm["data"]["userdbkey"].AsInt = _dbkey;
        }

        switch (requestID) {
            case "request_updatebingoprogress":
                _dataForm["data"]["bingoid"].AsInt = pNode["bingoid"].AsInt;
                _dataForm["data"]["id"].AsInt = pNode["id"].AsInt;
                _dataForm["data"]["progress"].AsInt = pNode["current"].AsInt;
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

        request.ConnectTimeout = System.TimeSpan.FromSeconds(5);
        request.Timeout = System.TimeSpan.FromSeconds(20);

        _previousRequest = request;

        request.Send();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="requestID"></param>
    /// <param name="pCallback"></param>
    /// <param name="pParam"></param>
	public void Post2WithString(string requestID, OnRequestFinishedDelegate pCallback, string pParam) {

        //_dataForm = JSON.Parse(_text.ToString());
        _dataForm = JSON.Parse(_parsingText);
        //_requestURL = _httpProtocol.Rows[(int)MNP_HTTP.rowIds.url].GetStringData(_url) +  _httpProtocol.Rows[(int)MNP_HTTP.rowIds.url].GetStringData(requestID);
        _requestURL = _url + _httpProtocol.Rows[(int)MNP_HTTP.rowIds.url].GetStringData(requestID);

        Debug.Log(">>> Post _requestURL :: " + _requestURL);
		
		_dataForm["cmd"] = requestID;
		
		if (!"request_login".Equals(requestID)) {
			_dataForm["data"]["token"] = _token.ToString();
			_dataForm["data"]["userdbkey"].AsInt = _dbkey;
		}

		switch (requestID) {

            case "request_usecoupon":
                _dataForm["data"]["couponid"] = pParam;
                break;

            case "request_wakeup":
                _dataForm["data"]["wakeuptime"] = pParam;
                break;

            case "request_sendheart":
                _dataForm["data"]["facebookid"] = pParam;
                break;

            case "request_datatransfer":
                _dataForm["data"]["code"] = pParam;
                break;


            case "request_nickname":
                _dataForm["data"]["nickname"] = pParam;
                RequestedNickName = pParam;
                break;


            case "request_updateunlock":
                _dataForm["data"]["column"] = pParam;
                break;


            case "request_rewardtrespass":
                string[] arrParam = pParam.Split('|');

                _dataForm["data"]["eventtype"] = arrParam[0];
                _dataForm["data"]["rewardtype"] = arrParam[1];
                _dataForm["data"]["rewardvalue"] = arrParam[2];
                _dataForm["data"]["extrainfo"] = arrParam[3];
                _dataForm["data"]["nextstep"] = arrParam[4];


                break;

        }

		_data = _dataForm.ToString();
		Debug.Log(">>> Post _data :: " + _data);
		
		HTTPRequest request = new HTTPRequest(new System.Uri(_requestURL), HTTPMethods.Post, pCallback);
		request.SetHeader("Content-Type", "application/json; charset=UTF-8");
		request.RawData = Encoding.UTF8.GetBytes(_data);
        request.Tag = pParam;

        request.ConnectTimeout = System.TimeSpan.FromSeconds(5);
        request.Timeout = System.TimeSpan.FromSeconds(20);

        _previousRequest = request;

        request.Send();
	}


    /// <summary>
    /// 재시도 처리 
    /// </summary>
    public void PostPreviousRequest() {
        if (_previousRequest != null)
            _previousRequest.Send();
    }


    #region RichText
    public static string ToRichText_AddColor(string value, Color color) {
        return string.Format("<color={0}>{1}</color>", ToHexColor(color, false), value);
    }

    public static string ToHexColor(Color color, bool alphaChannel) {
        Color32 newColor = color;
        return string.Format("#{0}{1}{2}{3}",
                        alphaChannel ? newColor.a.ToString("X2") : string.Empty,
                        newColor.r.ToString("X2"),
                        newColor.g.ToString("X2"),
                        newColor.b.ToString("X2"));
    }
    #endregion

    #region Properties 

    public string HttpToken {
		get {
			return this._token;
		} 
		set {
			this._token = value;
		}
	}

	public int UserDBKey {
		get {
			return this._dbkey;
		}
		set {
			this._dbkey = value;
		}
	}


	public string CurrentSKU {
		get {
			return this._currentSKU;
		} set{
			this._currentSKU = value;
		}
	}

    public string SendMissionType {
        get {
            return _sendMissionType;
        }

        set {
            _sendMissionType = value;
        }
    }

    public int SendMissionID {
        get {
            return _sendMissionID;
        }

        set {
            _sendMissionID = value;
        }
    }

    public MissionColumnCtrl SendMission {
        get {
            return _sendMission;
        }

        set {
            _sendMission = value;
        }
    }

    public string PushDeviceToken {
        get {
            return _pushDeviceToken;
        }

        set {
            _pushDeviceToken = value;
        }
    }

    public string RequestedNickName {
        get {
            return _requestedNickName;
        }

        set {
            _requestedNickName = value;
        }
    }

    public int ItemAdsID {
        get {
            return _itemAdsID;
        }

        set {
            _itemAdsID = value;
        }
    }

    public int PreItemAdsID {
        get {
            return _preItemAdsID;
        }

        set {
            _preItemAdsID = value;
        }
    }

    public int AdsID {
        get {
            return _adsID;
        }

        set {
            _adsID = value;
        }
    }

    public string CurrentPackage {
        get {
            return _currentPackage;
        }

        set {
            _currentPackage = value;
        }
    }

    public string PackageSKU {
        get {
            return _packageSKU;
        }

        set {
            _packageSKU = value;
        }
    }

    public bool IsTestMode {
        get {
            return _isTestMode;
        }

        set {
            _isTestMode = value;
        }
    }

    public string DeviceuniqueID {
        get {
            return _deviceuniqueID;
        }

        set {
            _deviceuniqueID = value;
        }
    }

    public int Root {
        get {
            return _root;
        }

        set {
            _root = value;
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

}
