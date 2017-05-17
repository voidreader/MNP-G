using UnityEngine;
using System.Collections;

public class WindowManagerCtrl : MonoBehaviour {

    /* 단순 팝업 컨트롤 */

	static WindowManagerCtrl _instance = null;

	// Window Controller 
	[SerializeField] CollectionMasterCtrl _collectionMaster;
	[SerializeField] GameObject _objGatchaConfirm; // 가챠 확인 창 
    [SerializeField] NoticeListCtrl _objNoticeList; // 공지사항
    [SerializeField] NoticeDetailCtrl _objNoticeDetail;
	[SerializeField] GameObject _objAttendance; // 출석체크 
    [SerializeField] GatchaProductDetailCtrl _objGatchaProductDetail;
    [SerializeField] PackageDetailCtrl _objPackageDetail;
    [SerializeField] HelpRankCtrl _objHelpRank;
    [SerializeField] GameObject _objCodeIssue;

    [SerializeField] GameObject _objInviteWindow;
    [SerializeField] GameObject _objInviteExplain;
    [SerializeField] GameObject _objEventPage;
	
	
    [SerializeField] BigPopupCtrl _objBigPopup;
    
    [SerializeField] NekoGiftResultCtrl _objNekoGiftResult;
    [SerializeField] UserGemInfoCtrl _objUserGemInfo;
    [SerializeField] SimpleNickCtrl _objNickInfo;
    [SerializeField] RankRewardResultCtrl _objLineInviteResult; // 라인 초대 결과 

	[SerializeField] FishGatchCtrl _fishGatcha;

	[SerializeField] PackageCtrl[] _arrPackages;

    [SerializeField] UIButton _btnWanted;
    [SerializeField] GameObject _newNekoPage;

    [SerializeField] RecordInfoCtrl _recordWindow;

	[SerializeField] SNSNekoCtrl _snsUploader;

    [SerializeField] SpecialPackageCtrl _specialPackage;
    [SerializeField] GameObject _btnSpecialPackage;

    [SerializeField] PopUpShareBottleCtrl _shareBottle;



    public static WindowManagerCtrl Instance {
		get {
			if(_instance == null) {
				_instance = FindObjectOfType(typeof(WindowManagerCtrl)) as WindowManagerCtrl;
				
				if(_instance == null) {
					Debug.Log("WindowManagerCtrl Init Error");
					return null;
				}
			}
			
			return _instance;
		}
	}

    #region Properties

    public FishGatchCtrl FishGatcha {
        get {
            return _fishGatcha;
        }

        set {
            _fishGatcha = value;
        }
    }

    public GameObject ObjGatchaConfirm {
        get {
            return _objGatchaConfirm;
        }

        set {
            _objGatchaConfirm = value;
        }
    }

    public NoticeListCtrl ObjNoticeList {
        get {
            return _objNoticeList;
        }

        set {
            _objNoticeList = value;
        }
    }

    public NoticeDetailCtrl ObjNoticeDetail {
        get {
            return _objNoticeDetail;
        }

        set {
            _objNoticeDetail = value;
        }
    }

    public CollectionMasterCtrl CollectionMaster {
        get {
            return _collectionMaster;
        }

        set {
            _collectionMaster = value;
        }
    }

    #endregion

    void Start() {

        //CheckRankRewardButton ();

        //CheckPackage ();


        CheckSpecialPackageExists();

    }


    public void OpenNekoGiftResult(string pType, int pValue) {
        _objNekoGiftResult.SetResult(pType,pValue);
    }

    public void OpenShareBonusResult(string pType, int pValue) {
        _objNekoGiftResult.SetShareResult(pType, pValue);
    }



	public void TestCall() {

	}

	

	

	/// <summary>
	/// 출석 체크 
	/// </summary>
	public void OpenAttendance() {
		_objAttendance.SetActive (true);
		_objAttendance.GetComponent<AttendanceCtrl> ().InitAttendance ();
	}

    public void OpenEventAttendanceOnly() {
        _objAttendance.SetActive(true);
        _objAttendance.GetComponent<AttendanceCtrl>().OpenEventAttendanceSimple();
    }

	public void CheckAttendance() {
	}

    /// <summary>
    /// 라인 초대 결과 보상 
    /// </summary>
    /// <param name="pNode"></param>
	public void OpenLineInviteResult(SimpleJSON.JSONNode pNode) {
        _objLineInviteResult.InitLineInvite(pNode);
    }

	/// <summary>
	/// 뽑기 확인창 오픈 
	/// </summary>
	public void OpenGatchaConfirm(bool pSetCrane = false) {

		// 열기전에 일부 화면 닫기. 
		LobbyCtrl.Instance.CloseCharacterList ();
		

		ObjGatchaConfirm.SetActive (true);
		ObjGatchaConfirm.GetComponent<GatchaConfirmCtrl> ().InitCraneShop();

		if (pSetCrane) {
			ObjGatchaConfirm.GetComponent<GatchaConfirmCtrl> ().OnClickGatchaGroup ();
		}
	}

    public void OpenPackageWindow() {
        // 열기전에 일부 화면 닫기. 
        LobbyCtrl.Instance.CloseCharacterList();


        ObjGatchaConfirm.SetActive(true);
        ObjGatchaConfirm.GetComponent<GatchaConfirmCtrl>().InitCraneShop();

        ObjGatchaConfirm.GetComponent<GatchaConfirmCtrl>().OnClickPackageGroup();
    }

	public void CloseOpenGatchaConfirm() {
		ObjGatchaConfirm.GetComponent<LobbyCommonUICtrl> ().Close ();
	}

    public void RefreshPackageAdopted() {
        ObjGatchaConfirm.GetComponent<GatchaConfirmCtrl>().RefreshPackageAdopted();
    }

    #region Collection

    public void OpenCollectionMaster() {

        if (_btnWanted.normalSprite == PuzzleConstBox.spriteLockIcon) {
            LobbyCtrl.Instance.OpenInfoPopUp(PopMessageType.Lock);
            return;
        }


        GameSystem.Instance.Post2RescueHistory();
        
    }

    public void OpenCollectionMasterCallback() {
        CollectionMaster.gameObject.SetActive(true);
    }

    #endregion




    public void OpenFishBand() {
        LobbyCtrl.Instance.SendMessage("DisableTopLobbyUI");
        FishGatcha.InitializeFishing();
        

        //_objFishBand.InitFishBand ();
    }



    public void OpenHelpRank() {

        _objHelpRank.SetScoreRank();

    }


    public void OpenUserGemInfo(SimpleJSON.JSONNode pNode) {
        _objUserGemInfo.SetValue(pNode);
    }


    public void OpenSimpleNickInfo() {
        _objNickInfo.gameObject.SetActive(true);

    }

    /// <summary>
    /// 공지사항 오픈 
    /// </summary>
    public void OpenNoticeList() {
        _objNoticeList.EnableNoticeList();
    }

    public void OpenNoticeDetail(int pIndex) {
        _objNoticeDetail.SetDetail(pIndex);
    }

    #region 캐릭터 뽑기 디테일 정보 오픈 
    public void OpenGatchaProductDetailSpecial() {
        _objGatchaProductDetail.SetProductDetail(GatchaProductType.Special);
    }

    public void OpenGatchaProductDetailFish() {
        _objGatchaProductDetail.SetProductDetail(GatchaProductType.Fish);
    }

    public void OpenGatchaProductDetailFree() {
        _objGatchaProductDetail.SetProductDetail(GatchaProductType.Free);
    }


    public void OpenPackageDetail(int pID, string pPrice) {
        _objPackageDetail.SetPackageDetail(pID, pPrice); // 0번이 스타트 패키지 
    }

    #endregion

    public void OpenCodeIssue() {
        _objCodeIssue.SetActive(true);
    }


    public void OpenInviteWindow() {

        _objInviteWindow.SetActive(true);

    }

    public void OpenInviteExplain() {
        _objInviteExplain.SetActive(true);
    }

    public void OpenEventPageOnly() {
        _objEventPage.SetActive(true);
    }

    public void OpenNewNekoPage() {
        _newNekoPage.SetActive(true);
    }


    /// <summary>
    /// 로비 말하는 고양이를 통해 입장 
    /// </summary>
    public void OpenEventPage() {

        Debug.Log("ES_EventTalkCatTouchCount :: " + GameSystem.Instance.LoadESvalueInt(PuzzleConstBox.ES_EventTalkCatTouchCount));

        // 최초는 help page로 이동 
        if(GameSystem.Instance.LoadESvalueInt(PuzzleConstBox.ES_EventTalkCatTouchCount) < 1) {
            OpenEventHelpPage();
        }
        else { // 2회부터 실제 페이지로 
            _objEventPage.SetActive(true);
        }

        GameSystem.Instance.SaveESvalueInt(PuzzleConstBox.ES_EventTalkCatTouchCount, GameSystem.Instance.LoadESvalueInt(PuzzleConstBox.ES_EventTalkCatTouchCount) + 1); // 이벤트 배너 터치 횟수를 설정 
    }

    private void OpenEventHelpPage() {
        for (int i = 0; i < GameSystem.Instance.NoticeBannerInitJSON.Count; i++) {
            if (GameSystem.Instance.NoticeBannerInitJSON[i]["action"].Value == "trespass") {
                //this.GetComponent<LobbyCommonUICtrl>().CloseSelf();
                OpenNoticeDetail(i);
            }
        }
    }


    /// <summary>
    /// 레코드 화면 오픈 
    /// </summary>
    /// <param name="pNode"></param>
    /// <param name="pMe"></param>
    public void OpenRecord(SimpleJSON.JSONNode pNode) {
        if (pNode["userdbkey"].AsInt == WWWHelper.Instance.UserDBKey)
            _recordWindow.SetRecord(pNode, true);
        else
            _recordWindow.SetRecord(pNode, false);

    }

    public void ShareNekoFB(int pID, int pGrade) {
        _snsUploader.CaptureAdoptNeko(pID, pGrade, SocialType.FB);
    }


    /// <summary>
    /// 
    /// </summary>
    public void CheckSpecialPackageExists() {

        //GameSystem.Instance.UserDataJSON["data"]["packagelist"][i]

        for (int i = 0; i < GameSystem.Instance.UserDataJSON["data"]["packagelist"].Count; i++) {

            if (GameSystem.Instance.UserDataJSON["data"]["packagelist"][i].Value == PuzzleConstBox.packageSpecial) {

                _btnSpecialPackage.SetActive(false);
                _specialPackage.gameObject.SetActive(false);
                return;

            }
        }

        _btnSpecialPackage.SetActive(true);

    }

    /// <summary>
    /// 스페셜 패키지 오픈 
    /// </summary>
    public void OpenSpecialPackage() {

#if UNITY_ANDROID

        foreach (GoogleProductTemplate p in AndroidInAppPurchaseManager.Client.Inventory.Products) {

            if (p.SKU.Contains(PuzzleConstBox.packageSpecial)) {
                _specialPackage.SetPackageDetail(p.SKU, p.LocalizedPrice);
                return;
            }
        }

#elif UNITY_IOS
            foreach (IOSProductTemplate tpl in IOSInAppPurchaseManager.Instance.Products) {
            if (tpl.Id.Contains("special_g")) {
                _specialPackage.SetPackageDetail(tpl.Id, tpl.LocalizedPrice);
                return;
            }
        }
#endif

        _specialPackage.SetPackageDetail("mn_pkg_02", "USD 0.99$");
    }


    /// <summary>
    /// 메인 공유 화면 오픈 
    /// </summary>
    /// <param name="pFlag"></param>
    public void OpenMainShareWithBonus() {
        _shareBottle.OpenMainShare(true);
    }

    public void OpenMainShare() {
        _shareBottle.OpenMainShare(false);
    }
}
