using UnityEngine;
using System.Collections;
using Facebook.Unity;

public class SNSNekoCtrl : MonoBehaviour {

    [SerializeField]
    GameObject objWaitingRequest = null;

    [SerializeField]
    GameObject _btnGroup;

    
    private Texture2D _tex; // 텍스쳐 

    [SerializeField]
    UISprite _nekoSprite;

    [SerializeField] UISprite _bgSprite;

    [SerializeField] UILabel _lblNekoStar;
    [SerializeField] UILabel _lblNekoUpperName;
    [SerializeField] UILabel _lblNekoNameGreen;
    [SerializeField] UILabel _lblNekoNameBlue;

    int _nekoGrade;
    string _nekoStar;


    string _adoptText = string.Empty;

    string _wantedText = string.Empty;

    readonly string _bgSprite1 = "sn1";
    readonly string _bgSprite3 = "sn3";


    string _shareText = string.Empty;

    [SerializeField]
    bool _isProcessing = false;


    void OnDisable() {
        _isProcessing = false;
    }


    private void InitScreen() {
        _lblNekoStar.text = "";
        _lblNekoUpperName.text = "";
        _lblNekoNameGreen.text = "";
        _lblNekoNameBlue.text = "";

        //_adoptText = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L4234);
        //_wantedText = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L4235);
        _adoptText = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L4234) + " " + GameSystem.Instance.UrlOnelink + " " + GameSystem.Instance.UrlHP;
        _wantedText = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L4235) + " " + GameSystem.Instance.UrlOnelink + " " + GameSystem.Instance.UrlHP;


        _btnGroup.SetActive(false);

    }


    /// <summary>
    /// Wanted에서 Social 공유 
    /// </summary>
    /// <param name="pNekoSprite"></param>
    /// <param name="pNekoName"></param>
    /// <param name="pNekoCatchCount"></param>
    /// <param name="pType"></param>
    public void CaptureWantedNeko(UISprite pNekoSprite, string pNekoName, string pNekoCatchCount, SocialType pType) {

        if (_isProcessing)
            return;

        InitScreen();

        _bgSprite.spriteName = _bgSprite3;

        _lblNekoUpperName.text = pNekoName;
        _lblNekoNameBlue.text = pNekoCatchCount;

        _nekoSprite.atlas = pNekoSprite.atlas;
        _nekoSprite.spriteName = pNekoSprite.spriteName;


        this.gameObject.SetActive(true);

        // 스크린 캡쳐 시작 
        StartCoroutine(TakeWantedScreenShot(pType));
    }


    public void CaptureAdoptNeko(int pNekoID, int pNekoGrade, SocialType pType) {

        if (_isProcessing)
            return;

        InitScreen();
        this.gameObject.SetActive(true);


        _bgSprite.spriteName = _bgSprite1;

        _nekoStar = string.Empty;

        _nekoGrade = pNekoGrade;
        for(int i=0; i<_nekoGrade; i++) {
            _nekoStar += "*";
        }

        _lblNekoNameGreen.text = GameSystem.Instance.GetNekoName(pNekoID, pNekoGrade);
        _lblNekoStar.text = _nekoStar;

        GameSystem.Instance.SetNekoSprite(_nekoSprite, pNekoID, pNekoGrade);


        // 스크린 캡쳐 시작 
        StartCoroutine(TakeAdoptScreenShot(pType));
    }


    /// <summary>
    /// 고양이 뽑기 스크린샷
    /// </summary>
    /// <param name="pType"></param>
    /// <returns></returns>
    IEnumerator TakeAdoptScreenShot(SocialType pType) {
        _isProcessing = true;

        yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(0.1f);

        string type = string.Empty;

        Debug.Log("!!! TakeAdoptScreenShot :: " + Screen.height);


        //_tex = new Texture2D(720, 917, TextureFormat.RGB24, false);
        _tex = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);

        yield return new WaitForEndOfFrame();

        _tex.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        _tex.Apply();

        
        

        if (pType == SocialType.FB) {
            type = "facebook.katana";
        }
        else if (pType == SocialType.twiiter) {
            type = "twi";
        }


        // Lock 객체 활성화
        this.objWaitingRequest.SetActive(true);

#if UNITY_ANDROID


        if (pType == SocialType.twiiter) { // 트위터 
            AndroidSocialGate.OnShareIntentCallback += HandleOnShareIntentCallback;
            AndroidSocialGate.StartShareIntent(_adoptText, _adoptText, _tex, type);
        }
        else { // FB

            
            this.objWaitingRequest.SetActive(false);
            _btnGroup.SetActive(true);
            _shareText = _adoptText;
            yield break;
            

            //AndroidSocialGate.OnShareIntentCallback += HandleOnShareIntentCallback;
            //AndroidSocialGate.StartShareIntent(_adoptText, _adoptText, _tex, type);

            /*
            MNPFacebookCtrl.OnPostingCompleteAction += OnCompletePosingFB;
            MNPFacebookCtrl.Instance.PostImage(_adoptText, _adoptText, _tex);
            */
        }


#elif UNITY_IOS
        if (pType == SocialType.twiiter) {
            IOSSocialManager.OnTwitterPostResult += OnTwiiterPostResult;
            IOSSocialManager.Instance.TwitterPost(_adoptText, null, _tex);
        }
        else if (pType == SocialType.FB) {
            this.objWaitingRequest.SetActive(false);
            _btnGroup.SetActive(true);
            _shareText = _adoptText;
            yield break;

            // IOSSocialManager.OnFacebookPostResult += OnFacebookPostResult;
            // IOSSocialManager.Instance.FacebookPost(_adoptText, null, _tex);
        }
        
#endif
        _isProcessing = false;


        StartCoroutine(LockScreen());

    }


    /// <summary>
    /// 기념관 스크린샷 
    /// </summary>
    /// <param name="pType"></param>
    /// <returns></returns>
    IEnumerator TakeWantedScreenShot(SocialType pType) {
      

        _isProcessing = true;

        yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(0.1f);

        string type = string.Empty;

        Debug.Log("!!! TakwWanted! :: " + Screen.height);


        //_tex = new Texture2D(720, 917, TextureFormat.RGB24, false);
        _tex = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);

        yield return new WaitForEndOfFrame();

        _tex.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        _tex.Apply();




        if (pType == SocialType.FB) {
            type = "facebook.katana";
        }
        else if (pType == SocialType.twiiter) {
            type = "twi";
        }


        // Lock 객체 활성화
        this.objWaitingRequest.SetActive(true);

#if UNITY_ANDROID

        if (pType == SocialType.twiiter) { // 트위터 
            AndroidSocialGate.OnShareIntentCallback += HandleOnShareIntentCallback;
            AndroidSocialGate.StartShareIntent(_wantedText, _wantedText, _tex, type);
        }
        else { // FB


            this.objWaitingRequest.SetActive(false);
            _btnGroup.SetActive(true);
            _shareText = _wantedText;

            yield break;

            //AndroidSocialGate.OnShareIntentCallback += HandleOnShareIntentCallback;
            //AndroidSocialGate.StartShareIntent(_wantedText, _wantedText, _tex, type);
            /*
            MNPFacebookCtrl.OnPostingCompleteAction += OnCompletePosingFB;
            MNPFacebookCtrl.Instance.PostImage(_wantedText, _wantedText, _tex);
            */
        }

        //AndroidNotificationManager.Instance.ShowToastNotification("Uploadを開始します。しばらくお待ちください。", 5);


#elif UNITY_IOS
        if (pType == SocialType.twiiter) {
            IOSSocialManager.OnTwitterPostResult += OnTwiiterPostResult;
            IOSSocialManager.Instance.TwitterPost(_wantedText, null, _tex);
        }
        else if (pType == SocialType.FB) {
            this.objWaitingRequest.SetActive(false);
            _btnGroup.SetActive(true);
            _shareText = _wantedText;

            yield break;

        /*
            IOSSocialManager.OnFacebookPostResult += OnFacebookPostResult;
            IOSSocialManager.Instance.FacebookPost(_wantedText, null, _tex);
        */
        }
        
#endif

        _isProcessing = false;

        StartCoroutine(LockScreen());
       
    }


    /// <summary>
    /// 안드로이드 포스팅 
    /// </summary>
    public void PostFBImage() {

        // 로그인 되지 않았으면 로그인 처리 
        if(!FB.IsLoggedIn) {
            MNPFacebookCtrl.OnCompleteLoginWithPublishAction += PostFBImage;
            // MNPFacebookCtrl.Instance.LoginPublishActions();
            MNPFacebookCtrl.Instance.LoginFB();
            return;
        }


        if(!MNPFacebookCtrl.Instance.CheckPublishAction()) {
            MNPFacebookCtrl.OnCompleteLoginWithPublishAction += PostFBImage;
            MNPFacebookCtrl.Instance.LoginPublishActions();
            return;
        }

        // AndroidNotificationManager.Instance.ShowToastNotification(GameSystem.Instance.GetLocalizeText(3058), 3);

        //MNPFacebookCtrl.OnPostingCompleteAction += OnCompletePosingFB;
        MNPFacebookCtrl.Instance.PostImage(_shareText, _shareText, _tex);

        // 올리는것처럼 보여준다. 
        this.objWaitingRequest.SetActive(true);
        StartCoroutine(UnlockScreen());
    }

    IEnumerator UnlockScreen() {
        // 5초후 자동으로 풀어준다. 
        yield return new WaitForSeconds(2);
        this.objWaitingRequest.SetActive(false);
        this.gameObject.SetActive(false);
    }

    public void CancelUpload() {
        this.objWaitingRequest.SetActive(false);
        this.gameObject.SetActive(false);
    }


    IEnumerator LockScreen() {

        // 5초후 자동으로 풀어준다. 
        yield return new WaitForSeconds(5);

        this.objWaitingRequest.SetActive(false);
    }


    /// <summary>
    /// AndroidSocialGate.OnShareIntentCallback 콜백
    /// </summary>
    /// <param name="status"></param>
    /// <param name="package"></param>
    void HandleOnShareIntentCallback(bool status, string package) {
        AndroidSocialGate.OnShareIntentCallback -= HandleOnShareIntentCallback;
        Debug.Log("[HandleOnShareIntentCallback] " + status.ToString() + " " + package);


        if(status) {
            // LobbyCtrl.Instance.OpenInfoPopUp(PopMessageType.UploadComplete);
        } else {
            // LobbyCtrl.Instance.OpenInfoPopUp(PopMessageType.UploadFail);
        }

        this.objWaitingRequest.SetActive(false);
        this.gameObject.SetActive(false);
    }

    /// <summary>
    /// Facebook Posing 콜백 
    /// </summary>
    void OnCompletePosingFB() {

        Debug.Log("OnCompletePosintgFB");

        this.objWaitingRequest.SetActive(false);
        this.gameObject.SetActive(false);
    }


    void OnFacebookPostResult(SA.Common.Models.Result result) {

        Debug.Log("OnFacebookPostResult");
        IOSSocialManager.OnFacebookPostResult -= OnFacebookPostResult;

        if (result.IsSucceeded) {
            LobbyCtrl.Instance.OpenInfoPopUp(PopMessageType.UploadComplete);
        }
        else {
            LobbyCtrl.Instance.OpenInfoPopUp(PopMessageType.UploadFail);
        }

        this.objWaitingRequest.SetActive(false);
        this.gameObject.SetActive(false);
    }


    void OnTwiiterPostResult(SA.Common.Models.Result result) {
        
        Debug.Log("OnTwiiterPostResult");
        IOSSocialManager.OnTwitterPostResult -= OnTwiiterPostResult;

        if(result.IsSucceeded) {
            LobbyCtrl.Instance.OpenInfoPopUp(PopMessageType.UploadComplete);
        }
        else {
            LobbyCtrl.Instance.OpenInfoPopUp(PopMessageType.UploadFail);
        }

        this.objWaitingRequest.SetActive(false);
        this.gameObject.SetActive(false);
    }

}
