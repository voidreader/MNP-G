using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvManagerCtrl : MonoBehaviour {

    public static EnvManagerCtrl Instance = null;

    /* 환경설정 점검 사항
     * 
     * AndroidManifest
     * Facebook 세팅 
     * 유니티 애즈, 구글 애드몹, 접속 주소, 버전, package 명  
     */

    public string liveServerURL = string.Empty;
    public string testServerURL = string.Empty;

    public string googleAdMobAOS = string.Empty;
    public string googleAdMobIOS = string.Empty;

    public string unityAdsAOS = string.Empty;
    public string unityAdsIOS = string.Empty;

    #region MNP-G

    // admob google ca-app-pub-6442004984942241/9558349615
    // admob ios ca-app-pub-6442004984942241/4988549215

    // unity ads google 1394443
    // unity ads iOS 1394442

    #endregion

    #region MNP-Q

    // live URL : http://ec2-13-124-50-170.ap-northeast-2.compute.amazonaws.com:8124/
    // TEST URL : http://ec2-54-193-101-29.us-west-1.compute.amazonaws.com:8124/

    // admob google ca-app-pub-7276185723803254/9144810526
    // admob ios ca-app-pub-7276185723803254/7528476528

    // unity ads google 1394443
    // unity ads iOS 1394442 

    #endregion


    void Awake() {
        if (Instance == null)
            Instance = this;

        DontDestroyOnLoad(this);
    }

    // Use this for initialization
    void Start () {


    }


}
