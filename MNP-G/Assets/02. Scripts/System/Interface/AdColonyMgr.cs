using UnityEngine;
using System.Collections;

public class AdColonyMgr : MonoBehaviour {

    static AdColonyMgr _instance = null;

    AdsType CurrentAdsType;


    // App & Zone ID 
    public string appID_Android = string.Empty;
    public string appID_IOS = string.Empty;

    public string zoneID_Android = string.Empty;
    public string zoneID_IOS = string.Empty;

    public string zoneID = string.Empty;

    string appId = string.Empty;
    

    public static AdColonyMgr Instance {
        get {
            if (_instance == null) {
                _instance = FindObjectOfType(typeof(AdColonyMgr)) as AdColonyMgr;

                if (_instance == null) {
                    Debug.Log("Nothing" + _instance.ToString());
                    return null;
                }
            }
            return _instance;
        }
    }

    void Awake() {
        DontDestroyOnLoad(this.gameObject);
    }


    // Use this for initialization
    void Start () {
      

#if UNITY_ANDROID

        appId = appID_Android;
        zoneID = zoneID_Android;


#elif UNITY_IOS

        appId = appID_IOS;
        zoneID = zoneID_IOS;

#else
        return;

#endif




        Debug.Log("▶▶▶ AdColony Init ::" + appId.ToString() + " / " + zoneID);

        AdColony.Configure(GameSystem.Instance.GameVersion, appId, zoneID);
        AdColony.OnV4VCResult = OnV4VCFinished;
        
    }



    void OnV4VCFinished(bool ad_shown, string name, int amount) {
        Debug.Log("Ad OnV4VCFinished :: " + ad_shown);

        LobbyCtrl.Instance.OffMuteSound();

        Screen.orientation = ScreenOrientation.Portrait;

        if (ad_shown) {
            GameSystem.Instance.OnAdvertisementFinished(CurrentAdsType);
        }
    }


    public void ShowAd(AdsType pType) {
        CurrentAdsType = pType;



        // BGM 관련 이슈로 강제로 멈춤 처리 
        //LobbyCtrl.Instance.StopBGM();
        LobbyCtrl.Instance.MuteLobbySound();


        // Show 하는 순간 AdsID 처리
        GameSystem.Instance.AdsID++;
        WWWHelper.Instance.AdsID = GameSystem.Instance.AdsID;

        AdColony.ShowV4VC(false, zoneID);
        
    }

    public bool IsVideoAvailable() {

       
        return AdColony.IsV4VCAvailable(zoneID);

    }




}
