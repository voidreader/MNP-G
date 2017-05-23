using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;

public class GoogleAdmobMgr : MonoBehaviour {


    static GoogleAdmobMgr _instance = null;
    BannerView _bottomBannerView;
    AdRequest request;

    bool _isBannerRequested = false;

    void Awake() {

        DontDestroyOnLoad(this.gameObject);

    }

    // Use this for initialization
    void Start() {
        // RequestBanner();
    }

    public static GoogleAdmobMgr Instance {

        get {
            if (_instance == null) {
                _instance = FindObjectOfType(typeof(GoogleAdmobMgr)) as GoogleAdmobMgr;

                if (_instance == null) {
                    Debug.Log("GoogleAdmobMgr Init Error");
                    return null;
                }
            }

            return _instance;
        }


    }

    public BannerView BottomBannerView {
        get {
            return _bottomBannerView;
        }

        set {
            _bottomBannerView = value;
        }
    }

    public bool IsBannerRequested {
        get {
            return _isBannerRequested;
        }

        set {
            _isBannerRequested = value;
        }
    }

    public void RequestBanner() {

        if (IsBannerRequested)
            return;

#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
        string adUnitId = "ca-app-pub-7276185723803254/9144810526";
#elif UNITY_IOS
        string adUnitId = "ca-app-pub-7276185723803254/7528476528";
#else
        string adUnitId = "unexpected_platform";
#endif

        try {
            // Create a 320x50 banner at the top of the screen.
            AdSize newSize = new AdSize(320, 38);
            Debug.Log(">>>> BottomBannerView Create adUnitId : " + adUnitId);
            //BottomBannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);
            BottomBannerView = new BannerView(adUnitId, newSize, AdPosition.Bottom);

            // Create an empty ad request.
            Debug.Log(">>>> Request Create");
            request = new AdRequest.Builder().Build();
            // Load the banner with the request.
            Debug.Log(">>>> LoadAd BottomBannerView");
            BottomBannerView.LoadAd(request);

            Debug.Log(">>>> Connect Event");
            BottomBannerView.OnAdLoaded += BottomBannerView_OnAdLoaded;
            BottomBannerView.OnAdFailedToLoad += BottomBannerView_OnAdFailedToLoad;
            BottomBannerView.OnAdLeavingApplication += BottomBannerView_OnAdLeavingApplication;
        }
        catch(System.Exception e) {
            Debug.Log(">>> StackTrace ::" + e.StackTrace);
            Debug.Log(">>>> LoadAd BottomBannerView");
            return;
        }


    }

    private void BottomBannerView_OnAdLeavingApplication(object sender, System.EventArgs e) {
        Debug.Log("★★★★★ RequestBanner Exception");
    }

    /// <summary>
    /// 
    /// </summary>
    public void ReloadBannerView() {
        IsBannerRequested = false;

        BottomBannerView.LoadAd(request);
        BottomBannerView.OnAdLoaded += BottomBannerView_OnAdLoaded;
        BottomBannerView.OnAdFailedToLoad += BottomBannerView_OnAdFailedToLoad;
    }

    private void BottomBannerView_OnAdFailedToLoad(object sender, AdFailedToLoadEventArgs e) {
        Debug.Log("★ Admob banner load fail");
        IsBannerRequested = false;

        BottomBannerView.OnAdFailedToLoad -= BottomBannerView_OnAdFailedToLoad;
    }

    private void BottomBannerView_OnAdLoaded(object sender, System.EventArgs e) {

        Debug.Log("★ Admob banner loaded");
        IsBannerRequested = true;
        BottomBannerView.OnAdLoaded -= BottomBannerView_OnAdLoaded;
    }
}
