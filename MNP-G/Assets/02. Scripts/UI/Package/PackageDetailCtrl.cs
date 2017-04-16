using UnityEngine;
using System;
using System.Collections;
using SimpleJSON;

public class PackageDetailCtrl : MonoBehaviour {

    [SerializeField]
    UITexture _banner;
    [SerializeField]
    string _title;
    [SerializeField]
    string _content;

    [SerializeField] UILabel _lblContent;
    [SerializeField] UILabel _lblPrice;
    [SerializeField]
    UIPanel _scrollView;

    JSONNode _node;

    string _price;
    [SerializeField] int _id;

    public static event Action OnClickBuyButton = delegate { };

    // Use this for initialization
    void Start () {
	
	}

    public void SetPackageDetail(int pID, string pPrice) {

        Debug.Log("★ SetPackageDetail :: " + pID);
        _id = pID;
        this.gameObject.SetActive(true);
        InitScrollView();

        // ID가 index
        if (GameSystem.Instance.ArrPackageSmallTextures[pID] != null)
            _banner.mainTexture = GameSystem.Instance.ArrPackageSmallTextures[pID];

        if(GameSystem.Instance.PackageBannerInitJSON != null && GameSystem.Instance.PackageBannerInitJSON.Count > pID) {
            //bannertext
            _content = GameSystem.Instance.PackageBannerInitJSON[pID]["bannertext"].Value;
            _lblContent.text = _content;
        }

        _price = pPrice;
        _lblPrice.text = _price;

    }

    public void OnClickBuy() {

        Debug.Log("★ OnClickBuy");

        OnClickBuyButton();
    }

    void OnDisable() {
        OnClickBuyButton = delegate { };
    }



    private void InitScrollView() {
        _scrollView.gameObject.GetComponent<UIScrollView>().ResetPosition();
        _scrollView.clipOffset = Vector2.zero;
        _scrollView.transform.localPosition = new Vector3(0, -330, 0);
    }

}
