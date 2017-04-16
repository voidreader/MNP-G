using UnityEngine;
using System.Collections;


public class GatchaConfirmCtrl : MonoBehaviour {




    [SerializeField]
    GameObject _objSimplePopup; // 공용 메세지 팝업 

    [SerializeField] GameObject _gatchaGroup;
    [SerializeField] GameObject _packageGroup;

    readonly string _redSprite = "common_btn_tap_red";
    readonly string _graySprite = "common_btn_tap_gray";

    [SerializeField] UIButton _btnGatchaGroup;
    [SerializeField] UIButton _btnPackageGroup;

    [SerializeField] PackageCtrl[] _packages;

    // Banners
    [SerializeField] UITexture _specialSmallBanner;
    [SerializeField] UITexture _freeSmallBanner;
    [SerializeField] UITexture _fishSmallBanner;


    [SerializeField]
    GameObject _freeCraneGroup;
    [SerializeField] UISprite _freeCraneSign;
    [SerializeField] UILabel _freeCraneSignValue;
    [SerializeField] UILabel _lblFreeCraneInfo;

    // 할인 마크 
    [SerializeField] UISprite _specialMultiGatchaBargainMark = null;
    


    [SerializeField] UILabel _singleSpecialGatchaPrice;
    [SerializeField] UILabel _multiSpecialGatchaPrice;

    [SerializeField] UIPanel _packageScrollView;
    [SerializeField] UIPanel _craneScrollView;


    


    // Use this for initialization
    void Start() {
    }

    void OnEnable() {

        /*
        // 오리지널 가격과, 변경 가격을 비교하여 할인된 금액이 제시되면, 할인 마크 표시 
        if (PuzzleConstBox.originalMultiGatchaPrice > GameSystem.Instance.SpecialMultiGatchaPrice) {
            _specialMultiGatchaBargainMark.gameObject.SetActive(true);
            _specialMultiGatchaBargainMark.spriteName = "star3-one";
        }
        else {
            _specialMultiGatchaBargainMark.gameObject.SetActive(true);
            _specialMultiGatchaBargainMark.spriteName = "star3-one";
        }

        if (PuzzleConstBox.originalMultiFishingPrice > GameSystem.Instance.SpecialMultiFishingPrice) {
            _fishingMultiBargainMark.SetActive(true);
        }
        else {
            _fishingMultiBargainMark.SetActive(false);
        }

        _singleSpecialGatchaPrice.text = GameSystem.Instance.GetNumberToString(GameSystem.Instance.SpecialSingleGatchaPrice);
        _multiSpecialGatchaPrice.text = GameSystem.Instance.GetNumberToString(GameSystem.Instance.SpecialMultiGatchaPrice);

        */

    }


    /// <summary>
    /// Crane Shop 초기화
    /// </summary>
    public void InitCraneShop() {

        //_freeGatchaButton.text = "こり[n]回".Replace("[n]", GameSystem.Instance.Remainfreegacha.ToString());
        
        InitSmallBanners();

        if (GameSystem.Instance.Remainfreegacha <= 0) {
            _freeCraneGroup.SetActive(false);
            _lblFreeCraneInfo.gameObject.SetActive(true);
            _lblFreeCraneInfo.text = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3111);
        }
        else {

            _freeCraneGroup.SetActive(true);
            _lblFreeCraneInfo.gameObject.SetActive(false);

            switch (GameSystem.Instance.Remainfreegacha) {
                case 1:
                    _freeCraneSignValue.text = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L4363).Replace("[n]", "1");
                    break;
                case 2:
                    _freeCraneSignValue.text = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L4363).Replace("[n]", "2");
                    break;
                case 3:
                    _freeCraneSignValue.text = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L4363).Replace("[n]", "3");
                    break;
            }


        }


        InitCraneScrollView();
    }

    private void InitCraneScrollView() {
        _craneScrollView.GetComponent<UIScrollView>().ResetPosition();
        _craneScrollView.clipOffset = Vector2.zero;
        _craneScrollView.transform.localPosition = new Vector3(0, 200, 0);
    }

    /// <summary>
    /// 
    /// </summary>
    private void InitSmallBanners() {
        if (GameSystem.Instance.SpecialSmallBanner != null)
            _specialSmallBanner.mainTexture = GameSystem.Instance.SpecialSmallBanner;

        /*
        if(GameSystem.Instance.FreeSmallBanner != null)
            _freeSmallBanner.mainTexture = GameSystem.Instance.FreeSmallBanner;
        */

        if (GameSystem.Instance.FishSmallBanner != null)
            _fishSmallBanner.mainTexture = GameSystem.Instance.FishSmallBanner;


        // 패키지  처리 
        for(int i =0; i<GameSystem.Instance.ArrPackageSmallTextures.Length; i++) {
            if (GameSystem.Instance.ArrPackageSmallTextures[i] != null) {
                //_arrPackageBanners[i].mainTexture = GameSystem.Instance.ArrPackageSmallTextures[i];
                _packages[i].SetBannerTexture(GameSystem.Instance.ArrPackageSmallTextures[i]);
            }
        }

    }

    public void RefreshPackageAdopted() {
        for(int i =0; i<_packages.Length; i++) {
            if(_packages[i].gameObject.activeSelf) {
                _packages[i].CheckExistsPackage();
            }
        }
    }





    /// <summary>
    /// 가챠 스크린 오픈 
    /// </summary>
    public void OpenGatcha(bool pIsFree) {
		LobbyCtrl.Instance.OpenGatchaScreen (pIsFree);
		this.gameObject.GetComponent<LobbyCommonUICtrl> ().CloseSelf ();
	}



    public void OpenGatcha() {
        Debug.Log(">>> OpenGatcha");
        LobbyCtrl.Instance.OpenGatchaScreen(GameSystem.Instance.IsFreeGatcha);
        //LobbyCtrl.Instance.CloseStack();
        this.gameObject.GetComponent<LobbyCommonUICtrl>().CloseSelf();
    }

    /// <summary>
    /// 스페셜 뽑기 1회 
    /// </summary>
	public void OpenSpecialGatcha() {
		GameSystem.Instance.IsFreeGatcha = false;
        GameSystem.Instance.GatchaCount = 1;


        // 튜토리얼 처리 
        if(GameSystem.Instance.TutorialStage == 0 && GameSystem.Instance.LocalTutorialStep == 0) {
            LobbyCtrl.Instance.OpenTutorialGatchaScreen();
            this.gameObject.GetComponent<LobbyCommonUICtrl>().CloseSelf();
            return;
        }


        _objSimplePopup.SetActive(true);
        _objSimplePopup.GetComponent<SimplePopupCtrl>().OpenMessagePopupCallback(PopMessageType.GatchaConfirm, OpenGatcha);
    }


    /// <summary>
    /// 스페셜 뽑기 10회
    /// </summary>
    public void OpenSpecialGatchaTen() {
        GameSystem.Instance.IsFreeGatcha = false;
        GameSystem.Instance.GatchaCount = 10;

        _objSimplePopup.SetActive(true);
        _objSimplePopup.GetComponent<SimplePopupCtrl>().OpenMessagePopupCallback(PopMessageType.ConfirmSpecialGatchaTen, OpenGatcha);
    }

	public void OpenEventGatcha() {

		GameSystem.Instance.IsFreeGatcha = false;
		LobbyCtrl.Instance.OpenEventGatcha (3);
		this.gameObject.GetComponent<LobbyCommonUICtrl> ().CloseSelf ();
	}


    public void PostSingleFishGatcha() {
        //GameSystem.Instance.Post2FishGatcha(1);
        LobbyCtrl.Instance.OpenInfoPopUp(PopMessageType.ConfirmFishGatchaOne);
    }

	public void PostMultiFishGatcha() {

        LobbyCtrl.Instance.OpenInfoPopUp(PopMessageType.ConfirmFishGatchaTen);

		//GameSystem.Instance.Post2FishGatcha (10);
	}

	public void OpenFishGatcha() {
		WindowManagerCtrl.Instance.OpenFishBand ();
		this.gameObject.GetComponent<LobbyCommonUICtrl> ().CloseSelf ();
	}

    /// <summary>
    /// 프리 크레인(뽑기) 열기  준비 창 오픈 
    /// </summary>
    public void OpenFreeCrane() {

        GameSystem.Instance.IsFreeGatcha = true;
        GameSystem.Instance.Post2RequestAdsRemainFreeGatcha(); // 횟수 체크를 호출한다. 콜백에 의해서 아래 두 문구 중 하나가 실행 

        //LobbyCtrl.Instance.OpenInfoPopUp(PopMessageType.ReadyToFreeGatcha); // 확인 팝업 
        //LobbyCtrl.Instance.OpenInfoPopUp(PopMessageType.NoRemainFreeGatcha); // 남은 기회가 없음. 
    }


    /// <summary>
    /// 프리 크레인의 광고 보기
    /// </summary>
    public void ShowFreeCraneAd() {
        GameSystem.Instance.ShowAd(AdsType.GatchaAds);
    }

    /// <summary>
    /// 프리 크레인 열기. 
    /// </summary>
    public void OpenFreeCraneScreen() {

        // PopMessageType.ReadyToFreeGatcha 에서 연결된다.

        LobbyCtrl.Instance.OpenGatchaScreen(true);
        this.gameObject.GetComponent<LobbyCommonUICtrl>().CloseSelf();
    }


    #region Tab Control 
    public void OnClickGatchaGroup() {
        _gatchaGroup.SetActive(true);
        _packageGroup.SetActive(false);

        _btnGatchaGroup.normalSprite = _redSprite;
        _btnPackageGroup.normalSprite = _graySprite;

        InitCraneScrollView();
    }

    public void OnClickPackageGroup() {

        Debug.Log("OnClickPackageGroup");

        _gatchaGroup.SetActive(false);
        _packageGroup.SetActive(true);


        _btnGatchaGroup.normalSprite = _graySprite;
        _btnPackageGroup.normalSprite = _redSprite;

        InitPackageScrollView();

#if UNITY_IOS

        foreach (IOSProductTemplate tpl in IOSInAppPurchaseManager.Instance.Products) {
            if (tpl.Id == GameSystem.Instance.StartPackProductID_ios) {
                // 600 패키지가 존재할때, 초기화
                for (int i = 0; i < GameSystem.Instance.PackageBannerInitJSON.Count; i++) {
                    _packages[i].SetPackageInfo(tpl.Id, tpl.LocalizedPrice, i, GameSystem.Instance.PackageBannerInitJSON[i]["packagename"].Value);
                }
            }
        }


#elif UNITY_ANDROID
        foreach (GoogleProductTemplate p in AndroidInAppPurchaseManager.Client.Inventory.Products) {
            if (p.SKU == GameSystem.Instance.StartPackProductID_android) {

                // 600 패키지가 존재할때, 초기화
                for (int i = 0; i < GameSystem.Instance.PackageBannerInitJSON.Count; i++) {
                    _packages[i].SetPackageInfo(p.SKU, p.LocalizedPrice, i, GameSystem.Instance.PackageBannerInitJSON[i]["packagename"].Value);
                }

            }
        }
#endif


    }

    #endregion


    private void InitPackageScrollView() {

        for(int i =0; i<_packages.Length; i++) {
            _packages[i].gameObject.SetActive(false);
        }

        _packageScrollView.gameObject.GetComponent<UIScrollView>().ResetPosition();
        _packageScrollView.clipOffset = Vector2.zero;
        _packageScrollView.transform.localPosition = new Vector3(0, 70, 0);

        _packageScrollView.GetComponent<UIGrid>().Reposition();
    }



}
