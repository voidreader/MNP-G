using UnityEngine;
using System.Collections;
using Facebook.Unity;
using SWorker;

public class HeartRequestPopup : MonoBehaviour {


    

    [SerializeField]
    GameObject _innerBanners;

    string _message = string.Empty;

    [SerializeField]
    bool _isHeartTab = true;

    [SerializeField] UIButton _heartTab;
    [SerializeField] UIButton _inviteTab;

    [SerializeField] GameObject _bottom;

    public bool IsHeartTab {
        get {
            return _isHeartTab;
        }

        set {
            _isHeartTab = value;
        }
    }

    public void EnableFBButton(bool pValue) {
        //_fbLogin.gameObject.SetActive(pValue);



        _innerBanners.SetActive(pValue); // 
    }


    public void LoginFacebook() {
        MNPFacebookCtrl.OnCompleteLoadFriend += LobbyCtrl.Instance.SpawnHeartFriendList;
        MNPFacebookCtrl.OnCompleteLoginWithPublishAction += LobbyCtrl.Instance.GetFacebookLinkReward;
        MNPFacebookCtrl.Instance.LoginFB();
        
    }



    public void OnCompleteLogin() {
        //_fbLogin.gameObject.SetActive(false);
        //_innerBanners.SetActive(false);
    }

    void OnEnable() {

        if (FB.IsLoggedIn) {
            _innerBanners.SetActive(false);
        }
        else {
            
            _innerBanners.SetActive(true);
        }

        // 메세지 수신 유저 초기확
        MNPFacebookCtrl.Instance.TargetUsers.Clear();

        if(IsHeartTab) {
            _heartTab.normalSprite = PuzzleConstBox.spriteRedTab;
            _inviteTab.normalSprite = PuzzleConstBox.spriteGrayTab;
            _bottom.gameObject.SetActive(false);
        }
        else {
            _heartTab.normalSprite = PuzzleConstBox.spriteGrayTab;
            _inviteTab.normalSprite = PuzzleConstBox.spriteRedTab;


            if(FB.IsLoggedIn)
                _bottom.gameObject.SetActive(true);
            else
                _bottom.gameObject.SetActive(false);

        }
    }

    void OnDisable() {
        _innerBanners.SetActive(false);
    }


    void Start() {
        _message = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L4262) + GameSystem.Instance.UrlOnelink;
            
    }

    public void OnClickHeartTab() {

        if (LobbyCtrl.Instance.GetWaingRequestEnable())
            return;

        if (_isHeartTab)
            return;

        if (!FB.IsLoggedIn)
            return;

        _isHeartTab = true;

        _heartTab.normalSprite = PuzzleConstBox.spriteRedTab;
        _inviteTab.normalSprite = PuzzleConstBox.spriteGrayTab;
        _bottom.gameObject.SetActive(false);

        LobbyCtrl.Instance.SpawnHeartFriendList();

    }


    public void OnClickInviteTab() {

        if (LobbyCtrl.Instance.GetWaingRequestEnable())
            return;

        if (!_isHeartTab)
            return;

        if (!FB.IsLoggedIn)
            return;

        _isHeartTab = false;

        _heartTab.normalSprite = PuzzleConstBox.spriteGrayTab;
        _inviteTab.normalSprite = PuzzleConstBox.spriteRedTab;
        _bottom.gameObject.SetActive(true);

        LobbyCtrl.Instance.SpawnHeartFriendList();
    }

    public void SendInvite() {

        /*
        if (MNPFacebookCtrl.Instance.TargetUsers.Count == 0) {
            this.SendMessage("CloseSelf");
            return;
        }
        */


        MNPFacebookCtrl.Instance.SendInvite();

    }


    public void OpenTwitterInvite() {
        SocialWorker.PostTwitter(_message, null, null, onTwitterResult);
        // SocialWorker.PostTwitter()

    }

    public void OpenLineInvite() {

#if UNITY_ANDROID 
        SocialWorker.PostLine(_message, null, onResult);

#elif UNITY_IOS

        string url = "http://line.me/R/msg/text/?" + WWW.EscapeURL(_message, System.Text.Encoding.UTF8);
        Application.OpenURL(url);

#endif


        // 라인 초대를 실행하려고 한 경우, 추가 보상 
        GameSystem.Instance.Post2LineInviteReward();

    }

    private void onResult(SocialWorkerResult obj) {
        if (obj == SocialWorkerResult.Success) {
            Debug.Log("Post Success");
        }
        else {
            Debug.Log("Post Fail");
        }
    }

    private void onTwitterResult(SocialWorkerResult obj) {
        if (obj == SocialWorkerResult.Success) {
            Debug.Log("Post Success");
        }
        else {
            Debug.Log("Post Fail");


            string url = "https://twitter.com/intent/tweet?text=" + WWW.EscapeURL(_message, System.Text.Encoding.UTF8);
            Application.OpenURL(url);
        }
    }


    public void OpenExplain() {
        WindowManagerCtrl.Instance.OpenInviteExplain();
    }

    public void OnClose() {
        GameSystem.Instance.Post2CheckNewMailUnder();
    }

}
