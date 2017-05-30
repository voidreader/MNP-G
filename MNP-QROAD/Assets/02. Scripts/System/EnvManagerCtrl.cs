using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvManagerCtrl : MonoBehaviour {

    public static EnvManagerCtrl Instance = null;

    /* 환경설정 점검 사항
     * 
     * AndroidManifest
     * Assets\Plugins\Android\AN_Res\res\values ids.xml
     * Facebook 세팅 
     * 유니티 애즈, 구글 애드몹, 접속 주소, 버전, package 명  
     */

    public string liveServerURL = string.Empty;
    public string testServerURL = string.Empty;

    public string googleAdMobAOS = string.Empty;
    public string googleAdMobIOS = string.Empty;

    public string unityAdsAOS = string.Empty;
    public string unityAdsIOS = string.Empty;


    public string GP_LEADERBOARD_ID = string.Empty;

    public string GP_ACHIEVEMENT_ID1 = string.Empty; // 고양이 10마리 
    public string GP_ACHIEVEMENT_ID2 = string.Empty; // 30마리
    public string GP_ACHIEVEMENT_ID3 = string.Empty; // 50마리
    public string GP_ACHIEVEMENT_ID4 = string.Empty; // 70마리
    public string GP_ACHIEVEMENT_ID5 = string.Empty; // 100마리

    public string IOS_ACHIEVEMENT_ID1 = "mnp.collection.10"; // 고양이 10마리 
    public string IOS_ACHIEVEMENT_ID2 = "mnp.collection.30"; // 30마리
    public string IOS_ACHIEVEMENT_ID3 = "mnp.collection.50"; // 50마리
    public string IOS_ACHIEVEMENT_ID4 = "mnp.collection.70"; // 70마리
    public string IOS_ACHIEVEMENT_ID5 = "mnp.collection.100"; // 100마리


    #region MNP-G

    // live URL : http://ec2-13-124-50-170.ap-northeast-2.compute.amazonaws.com:8124/
    // TEST URL : http://ec2-54-193-101-29.us-west-1.compute.amazonaws.com:8124/

    // admob google ca-app-pub-6442004984942241/9558349615
    // admob ios ca-app-pub-6442004984942241/4988549215

    // unity ads google 1394443
    // unity ads iOS 1394442

    //public string GP_LEADERBOARD_ID = "CgkIgcy5lKECEAIQAA";

    //public string ACHIEVEMENT_ID1 = "CgkIgcy5lKECEAIQAQ"; // 고양이 10마리 
    //public string ACHIEVEMENT_ID2 = "CgkIgcy5lKECEAIQAg"; // 30마리
    //public string ACHIEVEMENT_ID3 = "CgkIgcy5lKECEAIQAw"; // 50마리
    //public string ACHIEVEMENT_ID4 = "CgkIgcy5lKECEAIQBA"; // 70마리
    //public string ACHIEVEMENT_ID5 = "CgkIgcy5lKECEAIQBQ"; // 100마리

    #endregion

    #region MNP-Q

    // live URL : http://ec2-13-124-50-170.ap-northeast-2.compute.amazonaws.com:8124/
    // TEST URL : http://ec2-54-193-101-29.us-west-1.compute.amazonaws.com:8124/

    // admob google ca-app-pub-7276185723803254/9144810526
    // admob ios ca-app-pub-7276185723803254/7528476528

    // unity ads google 1394443
    // unity ads iOS 1394442 

    //public string GP_LEADERBOARD_ID = "CgkIgcy5lKECEAIQAA";
    //public string ACHIEVEMENT_ID1 = "CgkIgcy5lKECEAIQAQ"; // 고양이 10마리 
    //public string ACHIEVEMENT_ID2 = "CgkIgcy5lKECEAIQAg"; // 30마리
    //public string ACHIEVEMENT_ID3 = "CgkIgcy5lKECEAIQAw"; // 50마리
    //public string ACHIEVEMENT_ID4 = "CgkIgcy5lKECEAIQBA"; // 70마리
    //public string ACHIEVEMENT_ID5 = "CgkIgcy5lKECEAIQBQ"; // 100마리


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
