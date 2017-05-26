using UnityEngine;
using System.Collections;
using SWorker;
using System;
using Facebook.Unity;

public class InviteWindowCtrl : MonoBehaviour {


    string _message = string.Empty;
        
    void Start() {
        _message = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L4250);
    }




    public void LoginFacebook() {

        if (FB.IsLoggedIn) {
            LobbyCtrl.Instance.OpenHeartRequest();
        }
        else {
            MNPFacebookCtrl.OnCompleteLoadFriend += LobbyCtrl.Instance.SpawnHeartFriendList;
            MNPFacebookCtrl.OnCompleteLoginWithPublishAction += LobbyCtrl.Instance.GetFacebookLinkReward;
            MNPFacebookCtrl.Instance.LoginFB();
        }



    }


    public void OpenTwitterInvite() {

        //AndroidSocialGate.OnShareIntentCallback += AndroidSocialGate_OnShareIntentCallback;
        //AndroidSocialGate.StartShareIntent("#mitchirinekopop", _message, "twi");
        
        SocialWorker.PostTwitter(_message, null, null, onTwitterResult);

    }

    private void AndroidSocialGate_OnShareIntentCallback(bool arg1, string arg2) {

        AndroidSocialGate.OnShareIntentCallback -= AndroidSocialGate_OnShareIntentCallback;

        if(!arg1) {
            string url = "http://twitter.com/intent/tweet?text=" + _message;
        }
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


    public void OpenMailInvite() {
        SocialWorker.PostMail(_message, null, onResult);
    }


    public void OpenExplain() {
        WindowManagerCtrl.Instance.OpenInviteExplain();
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
            // string url = "http://twitter.com/intent/tweet?text=" + _message;
            Application.OpenURL(url);
        }
    }


    public void OnClose() {
        GameSystem.Instance.Post2CheckNewMailUnder();
    }
}
