using UnityEngine;
using System.Collections;

public class PackageCtrl : MonoBehaviour {

	public bool _isAdopted = false;
    [SerializeField] UITexture _bannerTexture;
	[SerializeField] GameObject _adoptedObject;

	[SerializeField] string _sku;
	[SerializeField] string _price;
	[SerializeField] UILabel _lblPrice;

    [SerializeField]
    int _bannerID;

    [SerializeField] string _packageName;


	public void SetAdopted() {
		_isAdopted = true;
		_adoptedObject.SetActive (_isAdopted);
	}

	public bool GetAdopted() {
		return _isAdopted;
	}


    /// <summary>
    /// 배너 텍스쳐 설정 
    /// </summary>
    /// <param name="pTex"></param>
    public void SetBannerTexture(Texture2D pTex) {
        _bannerTexture.mainTexture = pTex;
    }

    /// <summary>
    /// 패키지 정보 세팅 
    /// </summary>
    /// <param name="pRealSKU"></param>
    /// <param name="pPrice"></param>
    /// <param name="pBannerID"></param>
    /// <param name="pPackName"></param>
    public void SetPackageInfo(string pRealSKU, string pPrice, int pBannerID, string pPackName) {

        this.gameObject.SetActive(true);

        _bannerID = pBannerID;
        _packageName = pPackName;

        _sku = pRealSKU;
        _price = pPrice;

        CheckExistsPackage();

    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public bool CheckExistsPackage() {

        string packID1, packID2;

        if (_packageName == PuzzleConstBox.packageHoney600) {
            packID1 = GameSystem.Instance.StartPackProductID_android;
            packID2 = GameSystem.Instance.StartPackProductID_ios;
        }
        else if (_packageName == PuzzleConstBox.packageSummer600) { // 여름 팩 
            packID1 = GameSystem.Instance._600Pack2_android;
            packID2 = GameSystem.Instance._600Pack2_ios;
        }
        else if (_packageName == PuzzleConstBox.packageMoon600) { // 달맞이 팩
            packID1 = GameSystem.Instance._600Pack3_android;
            packID2 = GameSystem.Instance._600Pack3_ios;
        }
        else if (_packageName == PuzzleConstBox.packageMusic600) { // 달맞이 팩
            packID1 = GameSystem.Instance._600Pack4_android;
            packID2 = GameSystem.Instance._600Pack4_ios;
        }
        else if (_packageName == PuzzleConstBox.packageBoots600) { // 빨간모자 팩
            packID1 = GameSystem.Instance._600Pack5_android;
            packID2 = GameSystem.Instance._600Pack5_ios;
        }
        else if (_packageName == PuzzleConstBox.packageMaple600) { // 단풍 팩
            packID1 = GameSystem.Instance._600Pack6_android;
            packID2 = GameSystem.Instance._600Pack6_ios;
        }
        else if (_packageName == PuzzleConstBox.packageSecret600) { // 비밀 팩
            packID1 = GameSystem.Instance._600Pack7_android;
            packID2 = GameSystem.Instance._600Pack7_ios;
        }
        else if (_packageName == PuzzleConstBox.packageSanta600) { // 
            packID1 = GameSystem.Instance._600Pack8_android;
            packID2 = GameSystem.Instance._600Pack8_ios;
        }
        else if (_packageName == PuzzleConstBox.packageFish600) { // 
            packID1 = GameSystem.Instance._600Pack9_android;
            packID2 = GameSystem.Instance._600Pack9_ios;
        }
        else if (_packageName == PuzzleConstBox.packageSpecial) { // 
            packID1 = "mn_pkg_02";
            packID2 = "mn_pkg_02";
        }

        else {
            packID1 = "XXX";
            packID2 = "XXX";
        }

        for (int i = 0; i < GameSystem.Instance.UserDataJSON["data"]["packagelist"].Count; i++) {

            if (GameSystem.Instance.UserDataJSON["data"]["packagelist"][i].Value == packID1
                || GameSystem.Instance.UserDataJSON["data"]["packagelist"][i].Value == packID2) {
                SetAdopted();
                return true;
            }

        }
        return false;
    }

    public void BuyPackage() {

        Debug.Log("BuyPackage" + ":" + _sku);
       

        if(!_isAdopted) {
            Debug.Log("BuyPackage #2" + ":" + _sku);
            Debug.Log("BuyPackage #2" + ":" + _packageName);
            //GameSystem.Instance.PurchaseInApp(_sku);
            GameSystem.Instance.PurchaseInAppPackage(_sku, _packageName);
        }
       
    }

    public void OpenDetail() {


        if(_isAdopted) {

            LobbyCtrl.Instance.OpenInfoPopUp(PopMessageType.AlreadStartPackage);
            return;
        }



        PackageDetailCtrl.OnClickBuyButton += BuyPackage;
        WindowManagerCtrl.Instance.OpenPackageDetail(_bannerID, _price);
    }

	
}
