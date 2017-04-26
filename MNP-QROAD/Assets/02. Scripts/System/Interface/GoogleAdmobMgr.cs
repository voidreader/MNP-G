using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;

public class GoogleAdmobMgr : MonoBehaviour {


    static GoogleAdmobMgr _instance = null;
    BannerView _bottomBannerView;

    void Awake() {

        DontDestroyOnLoad(this.gameObject);

    }

    // Use this for initialization
    void Start() {
        RequestBanner();
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

    void RequestBanner() {
#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
        string adUnitId = "ca-app-pub-7276185723803254/9144810526";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-7276185723803254/7528476528";
#else
        string adUnitId = "unexpected_platform";
#endif

        // Create a 320x50 banner at the top of the screen.
        BottomBannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the banner with the request.
        BottomBannerView.LoadAd(request);
        BottomBannerView.OnAdLoaded += BottomBannerView_OnAdLoaded;
        BottomBannerView.OnAdFailedToLoad += BottomBannerView_OnAdFailedToLoad;

    }

    private void BottomBannerView_OnAdFailedToLoad(object sender, AdFailedToLoadEventArgs e) {
        Debug.Log("★ Admob banner load fail");
    }

    private void BottomBannerView_OnAdLoaded(object sender, System.EventArgs e) {

        Debug.Log("★ Admob banner loaded");
        //throw new System.NotImplementedException();
    }
}
