using UnityEngine;
using Facebook.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

#region enum



#endregion

public class MNPFacebookCtrl : MonoBehaviour {

    static MNPFacebookCtrl _instance = null;
    public List<string> fb_scopes_list = new List<string>();
    public List<string> fb_scopes_publish = new List<string>();
    private FB_UserInfo _userInfo = null;



    // Actions 
    public static event Action OnCompleteLoginWithPublishAction = delegate { };
    public static event Action OnCompleteLoadFriend = delegate { };
    public static event Action OnAppScoresRequestCompleteAction = delegate { };
    public static event Action OnInvitableFriendsDataRequestCompleteAction = delegate { };
    public static event Action OnPostingCompleteAction = delegate { };


    private string _UserId = string.Empty;
    private string _AccessToken = string.Empty;
    List<string> _permissions = new List<string>();

    [SerializeField] private int _lastSubmitedScore = 0;
    private int _submitedScore = 0;

    [SerializeField] private Dictionary<string, FB_UserInfo> _friends = new Dictionary<string, FB_UserInfo>();
    [SerializeField] private Dictionary<string, FB_UserInfo> _invitableFriends = new Dictionary<string, FB_UserInfo>();

    private Dictionary<string, FB_Score> _userScores = new Dictionary<string, FB_Score>();
    [SerializeField] private Dictionary<string, FB_Score> _appScores = new Dictionary<string, FB_Score>();

    [SerializeField] List<string> _targetUsers = new List<string>();

    public static MNPFacebookCtrl Instance {
        get {
            if (_instance == null) {
                _instance = FindObjectOfType(typeof(MNPFacebookCtrl)) as MNPFacebookCtrl;

                if (_instance == null) {
                    Debug.Log("MNPFacebookCtrl Init Error");
                    return null;
                }
            }

            return _instance;
        }
    }


    #region Properties 
    public string UserId {
        get {
            return _UserId;
        }

        set {
            _UserId = value;
        }
    }

    public string AccessToken {
        get {
            return _AccessToken;
        }

        set {
            _AccessToken = value;
        }
    }

    public FB_UserInfo UserInfo {
        get {
            return _userInfo;
        }

        set {
            _userInfo = value;
        }
    }

    public Dictionary<string, FB_UserInfo> Friends {
        get {
            return _friends;
        }

        set {
            _friends = value;
        }
    }

    public Dictionary<string, FB_UserInfo> InvitableFriends {
        get {
            return _invitableFriends;
        }

        set {
            _invitableFriends = value;
        }
    }

    public List<FB_UserInfo> InvitableFriendList {
        get {
            if (_invitableFriends == null) {
                return null;
            }

            List<FB_UserInfo> flist = new List<FB_UserInfo>();
            foreach (KeyValuePair<string, FB_UserInfo> item in _invitableFriends) {
                flist.Add(item.Value);
            }

            return flist;
        }


    }


    public List<FB_UserInfo> FriendsList {
        get {
            if (_friends == null) {
                return null;
            }

            List<FB_UserInfo> flist = new List<FB_UserInfo>();
            foreach (KeyValuePair<string, FB_UserInfo> item in _friends) {
                flist.Add(item.Value);
            }

            return flist;
        }
    }

    public Dictionary<string, FB_Score> AppScores {
        get {
            return _appScores;
        }

        set {
            _appScores = value;
        }
    }

    public List<string> TargetUsers {
        get {
            return _targetUsers;
        }

        set {
            _targetUsers = value;
        }
    }

    #endregion

    void Awake() {
        DontDestroyOnLoad(this.gameObject);
        
    }

    // Use this for initialization
    void Start () {
        fb_scopes_list.Clear();
        fb_scopes_publish.Clear();

        fb_scopes_list.Add("email");
        fb_scopes_list.Add("public_profile");
        fb_scopes_list.Add("user_friends");
        //fb_scopes_list.Add("user_games_activity");
        //fb_scopes_list.Add("publish_actions");


        fb_scopes_publish.Add("publish_actions");

        InitFB();
    }

    #region 페이스북 로그인 처리 
    /// <summary>
    /// 페이스북 로그인 
    /// </summary>
    public void LoginFB() {

        FB.LogInWithReadPermissions(fb_scopes_list.ToArray(), AuthCallback);
    }

    public void LoginPublishActions() {
        Debug.Log(">>>>>> LoginPublishActions");
        FB.LogInWithPublishPermissions(fb_scopes_publish.ToArray(), AuthPublishCallBack);
    }

    private void AuthPublishCallBack(ILoginResult result) {
        Debug.Log("AuthPublishCallBack :: " + result.RawResult.ToString());

        if (FB.IsLoggedIn) {
            // AccessToken class will have session details
            var aToken = Facebook.Unity.AccessToken.CurrentAccessToken;
            // Print current access token's User ID
            Debug.Log(aToken.UserId);

            UserId = aToken.UserId;
            GameSystem.Instance.FacebookID = _UserId;

            // Print current access token's granted permissions
            foreach (string perm in aToken.Permissions) {
                if(!_permissions.Contains(perm))
                    _permissions.Add(perm);
            }


            OnCompleteLoginWithPublishAction();
            OnCompleteLoginWithPublishAction = delegate { };

        }
        else {
            Debug.Log("User cancelled login");
        }
    }

    private void AuthCallback(ILoginResult result) {

        Debug.Log("◆◆◆ MNP Facebook AuthCallback :: " + result.RawResult.ToString());

        if (FB.IsLoggedIn) {
            // AccessToken class will have session details
            var aToken = Facebook.Unity.AccessToken.CurrentAccessToken;
            // Print current access token's User ID
            Debug.Log(aToken.UserId);

            UserId = aToken.UserId;
            GameSystem.Instance.FacebookID = _UserId;

            // Print current access token's granted permissions
            _permissions.Clear();
            foreach (string perm in aToken.Permissions) {
                Debug.Log(perm);
                _permissions.Add(perm);
            }

            //Invoke("LoginPublishActions", 0.5f);
            

            LoadUserData();

            #if UNITY_WEBGL

                    if (TitleCtrl.Instance != null) {
                            TitleCtrl.Instance.OnCanvasInit();
                    }

            #endif


            if (LobbyCtrl.Instance != null) {
                LobbyCtrl.Instance._objOptionGroup.SendMessage("RefreshFBGoogleState");
            }

            OnCompleteLoginWithPublishAction();
            OnCompleteLoginWithPublishAction = delegate { };

        }
        else {
            Debug.Log("User cancelled login");
        }
    }

    #endregion



    #region 스코어 처리 

    /// <summary>
    /// 친구들의 스코어 정보 조회 
    /// </summary>
    public void LoadAppScores() {

        Debug.Log("LoadAppScore :: " + FB.AppId);
        FB.API("/" + FB.AppId + "/scores", HttpMethod.GET, OnAppScoresComplete);
    }



    private void OnAppScoresComplete(IGraphResult result) {
        if(!CheckSucceedResult(result)) {
            Debug.Log("OnAppScoresComplete Failed");
            GameSystem.Instance.OnOffWaitingRequestInLobby(false);
            return;
        }


        Debug.Log("OnAppScoresComplete OK :: " + result.RawResult);
        _appScores.Clear();

        Dictionary<string, object> JSON = ANMiniJSON.Json.Deserialize(result.RawResult) as Dictionary<string, object>;
        List<object> data = JSON["data"] as List<object>;

        foreach (object row in data) {
            FB_Score score = new FB_Score();
            Dictionary<string, object> dataRow = row as Dictionary<string, object>;

            if (dataRow.ContainsKey("user")) {

                Dictionary<string, object> userInfo = dataRow["user"] as Dictionary<string, object>;

                if (userInfo.ContainsKey("id")) {
                    score.UserId = System.Convert.ToString(userInfo["id"]);
                }

                if (userInfo.ContainsKey("name")) {
                    score.UserName = System.Convert.ToString(userInfo["name"]);
                }


            }


            if (dataRow.ContainsKey("score")) {
                score.value = System.Convert.ToInt32(dataRow["score"]);
            }

            score.AppId = FB.AppId;


            /*
            if (dataRow.ContainsKey("application")) {
                Dictionary<string, object> AppInfo = dataRow["application"] as Dictionary<string, object>;

                if (AppInfo.ContainsKey("id")) {
                    score.AppId = System.Convert.ToString(AppInfo["id"]);
                }

                if (AppInfo.ContainsKey("name")) {
                    score.AppName = System.Convert.ToString(AppInfo["name"]);
                }

            }
            */


            AddToAppScores(score);
        }


        OnAppScoresRequestCompleteAction();
        OnAppScoresRequestCompleteAction = delegate { };
    }

    private void AddToAppScores(FB_Score score) {

        

        if (_appScores.ContainsKey(score.UserId)) {
            _appScores[score.UserId] = score;
        }
        else {
            _appScores.Add(score.UserId, score);
        }

        if (_userScores.ContainsKey(score.AppId)) {
            _userScores[score.AppId] = score;
        }
        else {
            _userScores.Add(score.AppId, score);
        }
    }


    // 사용자의 스코어 정보 조회 
    public void LoadPlayerScores() {
        Debug.Log("LoadPlayerScores");
        FB.API("/" + UserId + "/scores", HttpMethod.GET, OnLoaPlayrScoresComplete);
    }


    private void OnLoaPlayrScoresComplete(IGraphResult result) {


        Debug.Log("OnLoaPlayrScoresComplete :: " + result.RawResult);

        if (!CheckSucceedResult(result)) {
            Debug.Log("OnLoaPlayrScoresComplete Fail");
            return;
        }


        Debug.Log("OnLoaPlayrScoresComplete OK");

        Dictionary<string, object> JSON = ANMiniJSON.Json.Deserialize(result.RawResult) as Dictionary<string, object>;
        List<object> data = JSON["data"] as List<object>;

        foreach (object row in data) {
            FB_Score score = new FB_Score();
            Dictionary<string, object> dataRow = row as Dictionary<string, object>;

            Dictionary<string, object> userInfo = dataRow["user"] as Dictionary<string, object>;

            score.UserId = System.Convert.ToString(userInfo["id"]);
            score.UserName = System.Convert.ToString(userInfo["name"]);


            score.value = System.Convert.ToInt32(dataRow["score"]);

            // 최대값만 할당.
            if (_lastSubmitedScore < score.value)
                _lastSubmitedScore = score.value;
           

            //Debug.Log("▶OnLoaPlayrScoresComplete  LastSubmitedScore :: " + _lastSubmitedScore);

            //Dictionary<string, object> AppInfo = dataRow["application"] as Dictionary<string, object>;

            score.AppId = FB.AppId;
            //score.AppName = System.Convert.ToString(AppInfo["name"]);


            AddToUserScores(score);

        }


    }

    private void AddToUserScores(FB_Score score) {
        if (_userScores.ContainsKey(score.AppId)) {
            _userScores[score.AppId] = score;
        }
        else {
            _userScores.Add(score.AppId, score);
        }


        if (_appScores.ContainsKey(score.UserId)) {
            _appScores[score.UserId] = score;
        }
        else {
            _appScores.Add(score.UserId, score);
        }

    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public bool CheckPublishAction() {
        return _permissions.Contains("publish_actions");
    }

    public void SubmitScore(int score) {

        Debug.Log("FB SubmitScore :: " + score.ToString());

        if (!_permissions.Contains("user_games_activity")) {
            Debug.Log("No permission user_games_activity");
            return;
        }

        // 더 높은 점수일때만 갱신
        Debug.Log("▶ SubmitScore score/lastsubmitedscore :: " + score.ToString() + "/" + _lastSubmitedScore.ToString());
        if (score > _lastSubmitedScore) {
            _submitedScore = score;
            FB.API("/" + UserId + "/scores?score=" + score, HttpMethod.POST, OnScoreSubmited);
            
        } else {
            Debug.Log("Score is lower then previous data");
        }

    }


    private void OnScoreSubmited(IGraphResult result) {

        Debug.Log("OnScoreSubmited :: " + result.RawResult);

        if (!CheckSucceedResult(result)) {
            Debug.Log("OnScoreSubmited fail");
            return;
        }

        

        Debug.Log("OnScoreSubmited OK :: " + result.RawResult);
        _lastSubmitedScore = _submitedScore;

        if(result.RawResult.Contains("true")) { 

            FB_Score score = new FB_Score();
            score.AppId = FB.AppId;
            score.UserId = UserId;
            score.value = _lastSubmitedScore;

            if (_appScores.ContainsKey(UserId)) {
                _appScores[UserId].value = _lastSubmitedScore;
            }
            else {
                _appScores.Add(score.UserId, score);
            }


            if (_userScores.ContainsKey(FB.AppId)) {
                _userScores[FB.AppId].value = _lastSubmitedScore;
            }
            else {
                _userScores.Add(FB.AppId, score);
            }
        
        }


        LoadPlayerScores();
    }

    #endregion



    #region 사용자 정보 조회 
    /// <summary>
    /// 사용자 정보 조회 
    /// </summary>
    public void LoadUserData() {

        Debug.Log("MNPFB Load User Data");

        if(FB.IsLoggedIn) {
            FB.API("/me?fields=id,birthday,name,first_name,last_name,link,email,locale,location,gender", HttpMethod.GET, UserDataCallBack);
        } else {
            Debug.LogWarning("Auth user before loadin data, fail event generated");
        }
    }


    private void UserDataCallBack(IGraphResult result) {



        if(!CheckSucceedResult(result)) {
            Debug.Log("UserDataCallBack fail");
        }

        Debug.Log("UserDataCallBack OK");

        _userInfo = new FB_UserInfo(result.RawResult);

        GameSystem.Instance.Post2FBLink();

        LoadFrientdsInfo(50);
    }

    #endregion


    #region 사용자 친구 정보 조회 


    public void LoadInvitableFrientdsInfo(int limit) {
        if(FB.IsLoggedIn) {
            //FB.API("/me?fields=invitable_friends.limit(" + limit.ToString() + ").fields(first_name,id,last_name,name,link,locale,location,picture.type(square))", HttpMethod.GET, InvitableFriendsDataCallBack);
            FB.API("/me?fields=invitable_friends.limit(" + limit.ToString() + ").fields(first_name,id,last_name,name,link,picture.type(square))", HttpMethod.GET, InvitableFriendsDataCallBack);
        } else {
            Debug.LogWarning("LoadInvitableFrientdsInfo Auth user before loadin data, fail event generated");
        }
    }

    /// <summary>
    /// 친구 정보 조회 
    /// </summary>
    /// <param name="limit"></param>
    public void LoadFrientdsInfo(int limit) {

        if (FB.IsLoggedIn) {

            FB.API("/me?fields=friends.limit(" + limit.ToString() + ").fields(first_name,id,last_name,name,link,locale,location)", HttpMethod.GET, FriendsDataCallBack);

        }
        else {
            Debug.LogWarning("Auth user before loadin data, fail event generated");
        }
    }

    private void FriendsDataCallBack(IGraphResult result) {
        if (!CheckSucceedResult(result)) {
            Debug.Log("FriendsDataCallBack fail");
            OnCompleteLoadFriend = delegate { };
            return;
        }

        Debug.Log("FriendsDataCallBack OK");

        ParseFriendsData(result.RawResult);


        // 플레이어 스코어 정보 조회
        LoadPlayerScores();

        LoadInvitableFrientdsInfo(50);

        OnCompleteLoadFriend();
        OnCompleteLoadFriend = delegate { };
    }



    private void InvitableFriendsDataCallBack(IGraphResult result) {

        if (!CheckSucceedResult(result)) {
            //Debug.Log(result.Error);
            Debug.Log("InvitableFriendsDataCallBack fail");
            OnInvitableFriendsDataRequestCompleteAction = delegate { };
            return;
        }

        Debug.Log("★ InvitableFriendsDataCallBack :: " + result.RawResult);

        ParseInvitableFriendsData(result.RawResult);
        

        OnInvitableFriendsDataRequestCompleteAction();
        OnInvitableFriendsDataRequestCompleteAction = delegate { };
    }


    public void ParseFriendsData(string data) {
        ParseFriendsFromJson(data, _friends);
    }

    public void ParseInvitableFriendsData(string data) {

        Debug.Log("★ ParseInvitableFriendsData");

        //ParseFriendsFromJson(data, _invitableFriends, true);
        JSONNode node = SimpleJSON.JSON.Parse(data);
        node = node["invitable_friends"];

        InvitableFriends.Clear();
        for (int i=0; i<node["data"].Count;i++) {
            FB_UserInfo user = new FB_UserInfo(node["data"][i]);
            InvitableFriends.Add(user.Id, user);
        }

    }

    


    private void ParseFriendsFromJson(string data, Dictionary<string, FB_UserInfo> friends, bool invitable = false) {
        Debug.Log("ParceFriendsData");
        Debug.Log(data);

        try {
            
            IDictionary JSON = ANMiniJSON.Json.Deserialize(data) as IDictionary;
            IDictionary f = invitable ? JSON["invitable_friends"] as IDictionary : JSON["friends"] as IDictionary;
            IList flist = f["data"] as IList;

            if(invitable) {
                InvitableFriends.Clear();
                for (int i = 0; i < flist.Count; i++) {
                    FB_UserInfo user = new FB_UserInfo(flist[i] as IDictionary);
                    InvitableFriends.Add(user.Id, user);
                }
            }
            else {
                Friends.Clear();
                for (int i = 0; i < flist.Count; i++) {
                    FB_UserInfo user = new FB_UserInfo(flist[i] as IDictionary);
                    Friends.Add(user.Id, user);
                }
            }



        }
        catch (System.Exception ex) {
            Debug.LogWarning("Parceing Friends Data failed");
            Debug.LogWarning(ex.Message);
        }
    }

    #endregion

    #region Post

    /// <summary>
    /// 
    /// </summary>
    /// <param name="pTitle"></param>
    /// <param name="pMessage"></param>
    /// <param name="pImage"></param>
    public void PostImage(string pTitle, string pMessage, Texture2D pImage) {
        
        
        Debug.Log(">>>> FB PostImage");

        byte[] imageBytes = pImage.EncodeToPNG();

        WWWForm wwwForm = new WWWForm();
        // wwwForm.AddField("caption", pMessage);
        wwwForm.AddBinaryData("image", imageBytes, "picture.png");
        wwwForm.AddField("name", pMessage);

        FB.API("me/photos", HttpMethod.POST, PostCallback_Internal, wwwForm);
    }

    /// <summary>
    /// 
    /// </summary>
    public void PostFeed(string pMessage) {
        Debug.Log(">>>> FB PostImage");

        WWWForm wwwForm = new WWWForm();
        
        wwwForm.AddField("link", "http://onelink.to/jb842h");
        wwwForm.AddField("message", pMessage);

        FB.API("me/feed", HttpMethod.POST, PostCallback_Internal, wwwForm);
    }


    private void PostCallback_Internal(IGraphResult result) {

        //GameSystem.Instance.OnOffWaitingRequestInLobby(false);
        Debug.Log("★PostCallback_Internal #1 :: " + result.Error);
        Debug.Log("★PostCallback_Internal #2 :: " + result.RawResult);

        /*
        SNSNekoCtrl snsNekoUploader = FindObjectOfType(typeof(SNSNekoCtrl)) as SNSNekoCtrl;

        if(snsNekoUploader != null) {
            snsNekoUploader.gameObject.SetActive(false); // 비활성화 처리 
        }
        */

        OnPostingCompleteAction();
        OnPostingCompleteAction = delegate { };


        if (!CheckSucceedResult(result)) {
            Debug.Log("PostCallback_Internal fail");

#if UNITY_ANDROID
            AndroidNotificationManager.Instance.ShowToastNotification(GameSystem.Instance.GetLocalizeText(3058), 3);
#endif

            return;
        }

        Debug.Log("FriendsDataCallBack OK");
#if UNITY_ANDROID
        AndroidNotificationManager.Instance.ShowToastNotification(GameSystem.Instance.GetLocalizeText(3057), 3);
#endif
    }


    #endregion

    public void SendInvite() {

        Debug.Log("SendInvite! :: " + _targetUsers.Count);

        /*
        if (_targetUsers.Count == 0)
            return;
        */

        //FB.AppRequest("Come play this great game!", OGActionType.ASKFOR, null, _targetUsers.ToArray(), "", "Invite", InviteCallBack);
        FB.AppRequest(GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L4238)
            , _targetUsers, null, null, 50, ""
            , GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L4239), InviteCallBack);
        //FB.AppRequest("Come play this great game!", null, null, _targetUsers.ToArray(), "", "Invite!", InviteCallBack);

    }

    private void InviteCallBack(IAppRequestResult result) {
        Debug.Log(">>>> InviteCallBack :: " + result.RawResult);

        

        if(result.ToString().Contains("cancelled")) {

        } 
        else {
            // 친구초대 주간미션 
            GameSystem.Instance.CheckMissionProgress(MissionType.Week, 1, 1);

            

            JSONNode node = JSON.Parse(result.RawResult);
            

            GameSystem.Instance.Post2InviteFriend(node["to"].Value.Split(',').Length); // 초대 사용자 만큼 패킷전송
        }


    }


    private bool CheckSucceedResult(IGraphResult result) {
        if (string.IsNullOrEmpty(result.Error) && !string.IsNullOrEmpty(result.RawResult))
            return true;

        Debug.Log(result.Error);
        return false;
    }


    #region 페이스북 초기화
    public void InitFB() {

        Debug.Log("Init FB");

        if (!FB.IsInitialized) {
            FB.Init(InitCallback, OnHideUnity);
        }
        else {
            FB.ActivateApp();
        }
    }


    /// <summary>
    /// Facebook Init Callback
    /// </summary>
    private void InitCallback() {

        if (FB.IsInitialized) {
            Debug.Log("▶▶▶▶Good Initialize FB in MNP");
            FB.ActivateApp();

            if(FB.IsLoggedIn && Facebook.Unity.AccessToken.CurrentAccessToken != null) {
                _AccessToken = Facebook.Unity.AccessToken.CurrentAccessToken.TokenString;
                _UserId = Facebook.Unity.AccessToken.CurrentAccessToken.UserId;
                GameSystem.Instance.FacebookID = _UserId;

                // Print current access token's granted permissions
                _permissions.Clear();
                foreach (string perm in Facebook.Unity.AccessToken.CurrentAccessToken.Permissions) {
                    Debug.Log(perm);
                    _permissions.Add(perm);
                }

                // 로그인 된경우 사용자 정보 조회
                LoadUserData();
            }


            #if UNITY_WEBGL

                    if (TitleCtrl.Instance != null) {
                            TitleCtrl.Instance.OnCanvasInit();
                    }

            #endif



        }
        else {
            Debug.Log("Failed to Initialize the Facebook SDK");
        }
    }

    private void OnHideUnity(bool isGameShown) {
        /*
        if (!isGameShown) {
            // Pause the game - we will need to hide
            Time.timeScale = 0;
        }
        else {
            // Resume the game - we're getting focus again
            Time.timeScale = 1;
        }
        */



    }
#endregion

}
