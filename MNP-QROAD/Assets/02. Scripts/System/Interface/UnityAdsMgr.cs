using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;

public class UnityAdsMgr : MonoBehaviour {


    // Serialize private fields
    [SerializeField]
    string iosGameId;
    [SerializeField]
    string androidGameId;
    [SerializeField]
    bool enableTestMode = true;

    [SerializeField]
    AdsType CurrentAdsType;
    string zone = "rewardedVideo";

    static UnityAdsMgr _instance = null;

    public static UnityAdsMgr Instance {

        get {
            if (_instance == null) {
                _instance = FindObjectOfType(typeof(UnityAdsMgr)) as UnityAdsMgr;

                if (_instance == null) {
                    Debug.Log("UnityAdsMgr Init Error");
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
    IEnumerator Start() {
        string gameId = null;

#if UNITY_IOS // If build platform is set to iOS...
        gameId = iosGameId;
#elif UNITY_ANDROID // Else if build platform is set to Android...
        gameId = androidGameId;
#endif



        
        if (Advertisement.isSupported) { // If runtime platform is supported...
            Advertisement.Initialize(gameId, enableTestMode); // ...initialize.
        }
        


        // Wait until Unity Ads is initialized,
        //  and the default ad placement is ready.

        
        while (!Advertisement.isInitialized || !Advertisement.IsReady()) {
            yield return new WaitForSeconds(0.5f);
        }
        

        yield return null;
      

    }



    /// <summary>
    /// 동영상 광고 시청
    /// </summary>
    /// <param name="pType"></param>
    public void ShowAd(AdsType pType) {
        
        
        CurrentAdsType = pType;

        if (Application.internetReachability == NetworkReachability.NotReachable) {
            Debug.Log("Network is not enable");
        }

        if (Advertisement.IsReady()) {

            ShowOptions options = new ShowOptions();
            options.resultCallback = this.HandleShowResult;

            // Show 하는 순간 AdsID 처리
            GameSystem.Instance.AdsID++;
            WWWHelper.Instance.AdsID = GameSystem.Instance.AdsID;


            LobbyCtrl.Instance.MuteLobbySound();
            Advertisement.Show(zone, options);
        }
        else {

            LobbyCtrl.Instance.OpenInfoPopUp(PopMessageType.AdShortage); //광고 소진 팝업

        }
        
        
    }


    
    /// <summary>
    /// 동영상 시청 callback
    /// </summary>
    /// <param name="result"></param>
    /// 
    
    private void HandleShowResult(ShowResult result) {

        LobbyCtrl.Instance.OffMuteSound();

        switch (result) {
            case ShowResult.Finished:
                Debug.Log("Video completed. User rewarded");


                // 유저 보상 처리 
                GameSystem.Instance.OnAdvertisementFinished(CurrentAdsType);

                break;
            case ShowResult.Skipped:
                Debug.LogWarning("Video was skipped.");
                break;
            case ShowResult.Failed:
                Debug.LogError("Video failed to show.");
                break;
        }
    }
    


}
