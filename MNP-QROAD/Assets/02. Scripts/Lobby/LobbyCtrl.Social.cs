using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PathologicalGames;
using Facebook.Unity;

public partial class LobbyCtrl : MonoBehaviour {


    // 하트 보내기 & 초대 창 
    [SerializeField] HeartRequestPopup _heartRequestPopup; 
    [SerializeField] GameObject _objHeartRequest;
    [SerializeField] List<HeartRequestCtrl> _listHeartFriend = new List<HeartRequestCtrl>();
    [SerializeField] UIGrid _grdHeartRequest;
    [SerializeField] UIPanel _svHeartRequest;


    public OptionCtrl _objOptionGroup;


    private void RefreshOptionState() {

        if (_objOptionGroup.gameObject.activeSelf) {
            _objOptionGroup.RefreshFBGoogleState();
        }
    }

    /// <summary>
    /// Logins the facebook.
    /// </summary>
    public void LoginFacebook() {

        if (Facebook.Unity.FB.IsLoggedIn) {
            Facebook.Unity.FB.LogOut();
            //GameSystem.Instance.IsFacebookLogin = false;
            Invoke("RefreshOptionState", 1f);
            return;
        }

        MNPFacebookCtrl.OnCompleteLoginWithPublishAction += GetFacebookLinkReward;
        MNPFacebookCtrl.Instance.LoginFB();


    }

    /// <summary>
    /// 페이스북 연동 보상 획득 
    /// </summary>
    public void GetFacebookLinkReward() {

        StartCoroutine(GettingFacebookLinkReward());
    }

    IEnumerator GettingFacebookLinkReward() {

        // 팝업 타이밍 때문에, 이전 Link 통신이 완료될때까지 대기 
        while(objWaitingRequest.activeSelf || GameSystem.Instance.FbLinkJSON == null) {
            yield return new WaitForSeconds(0.1f);
        }


        if (GameSystem.Instance.FbLinkJSON["getfacebooklink"].AsBool
            && !GameSystem.Instance.UserJSON["facebooklinkget"].AsBool) {

            //{"result":0,"error":"","data":{"getfacebooklink":true,"facebookid":"yrdy","reward":{"nekotid":152,"star":3,"message":"ふぇいすぶっく連動ありがとう！\nめーるにぷれぜんとを送ったよ！"}}}
            LobbyCtrl.Instance.OpenUpperInfoPopUp(PopMessageType.GetUserEventReward, GameSystem.Instance.FbLinkJSON["reward"].ToString());


            // 사용자 정보 갱신 
            GameSystem.Instance.UserJSON["facebooklinkget"].AsBool = true;
            GameSystem.Instance.UserDataJSON["data"]["facebooklinkget"].AsBool = true;
            GameSystem.Instance.Post2CheckNewMail();
        }
    }


    public void LogoutFacebook() {
        SPFacebook.Instance.Logout();
    }


    /// <summary>
    /// Loads the invitable friend.
    /// </summary>
    public void LoadInvitableFriend() {
        //SPFacebook.Instance.LoadInvitableFrientdsInfo (30);
        //SPFacebook.Instance.LoadFrientdsInfo (40);
        OpenHeartRequest();
    }

    /// <summary>
    /// Sets the friend list.
    /// </summary>
    public void SetFriendList() {

        HeartRequestCtrl heartRequestCtrl;

        _listHeartFriend.Clear();


        Debug.Log("▶SetFriendList");

        for (int i = 0; i < 50; i++) {
            heartRequestCtrl = PoolManager.Pools["HeartRequestPool"].Spawn("HeartRequestColumn").GetComponent<HeartRequestCtrl>();
            _listHeartFriend.Add(heartRequestCtrl);
        }

        ClearHeartRequestPool();
    }

    public void ClearHeartRequestPool() {
        for (int i = 0; i < _listHeartFriend.Count; i++) {
            _listHeartFriend[i].gameObject.SetActive(false);
        }

        // WWWTextureLoader 삭제처리해준다.(프로필 파일 로드시 생성됨)
        /*
		Object[] arrTextureLoader;
		arrTextureLoader = GameObject.FindObjectsOfType (typeof(WWWTextureLoader));

		for(int i=0; i<arrTextureLoader.Length; i++) {
			//arrTextureLoader[i]
			Destroy(arrTextureLoader[i]);
		}
		*/

    }

    /// <summary>
    /// 하트 보내기 팝업을 초대  탭으로 변경처리
    /// </summary>
    private void SetInviteTab() {
        _heartRequestPopup.IsHeartTab = false;
        OpenHeartRequest();
    }

    /// <summary>
    /// 프렌드 리스트 활성화 
    /// </summary>
    public void SpawnHeartFriendList() {

        ClearHeartRequestPool();

        // 로그인 여부에 따른 버튼 세팅 
        if (!FB.IsLoggedIn) {
            _heartRequestPopup.EnableFBButton(true);
            return;
        }

        _heartRequestPopup.EnableFBButton(false);

        Debug.Log(">>> SpawnHeartFriendList");

        // Scroll View 초기화 
        _svHeartRequest.gameObject.GetComponent<UIScrollView>().ResetPosition();
        _svHeartRequest.clipOffset = Vector2.zero;
        _svHeartRequest.transform.localPosition = new Vector3(0, 250, 0);


        


        if (_listHeartFriend.Count == 0) {
            SetFriendList();
        }


        Debug.Log(">>>MNP Friend List Count :: " + MNPFacebookCtrl.Instance.Friends.Count + ">>>MNP Invitable Friend List Count :: " + MNPFacebookCtrl.Instance.InvitableFriends.Count);
        Debug.Log(">>>IsHeartTab :: " + _heartRequestPopup.IsHeartTab.ToString());


        if (_heartRequestPopup.IsHeartTab) { // 친구 리스트 
            for (int i = 0; i < MNPFacebookCtrl.Instance.Friends.Count; i++) {
                _listHeartFriend[i].SetHeartRequest(MNPFacebookCtrl.Instance.FriendsList[i], true);
            }
        }
        else {
            for (int i = 0; i < MNPFacebookCtrl.Instance.InvitableFriends.Count; i++) {
                _listHeartFriend[i].SetHeartRequest(MNPFacebookCtrl.Instance.InvitableFriendList[i], false);
            }
        }


        _grdHeartRequest.Reposition();
        _heartRequestPopup.OnCompleteLogin();


    }

    /// <summary>
    /// 하트 주고받기 창 오픈 
    /// </summary>
    public void OpenHeartRequest() {

        AdbrixManager.Instance.SendAdbrixInAppActivity(AdbrixManager.Instance.BUTTON_FRIEND);

        Debug.Log(">> OpenHeartRequest");
        //LoginFacebook();

        //SPFacebook.instance.AppRequest("Come play this great game!");
        _objHeartRequest.SetActive(true);
    }



    #region 메인 유리병 공유하기 

    public void PostMainFBFeed() {
        if (!FB.IsLoggedIn) {
            MNPFacebookCtrl.OnCompleteLoginWithPublishAction += GetFacebookLinkReward;
            MNPFacebookCtrl.OnCompleteLoginWithPublishAction += PostMainFBFeed;
            MNPFacebookCtrl.Instance.LoginFB();
            return;
        }

        if (!MNPFacebookCtrl.Instance.CheckPublishAction()) {
            MNPFacebookCtrl.OnCompleteLoginWithPublishAction += PostMainFBFeed;
            MNPFacebookCtrl.Instance.LoginPublishActions();
            return;
        }
        MNPFacebookCtrl.OnPostingCompleteAction += OnCompleteMainFBFeed;
        MNPFacebookCtrl.Instance.PostFeed(GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L4261));
    }

    void OnCompleteMainFBFeed() {

        // 통신 
        GameSystem.Instance.Post2GetShareBonus();


    }

    #endregion




}
