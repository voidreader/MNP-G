using UnityEngine;
using System.Collections;
using SimpleJSON;

public class GatchaProductDetailCtrl : MonoBehaviour {
    
    
    [SerializeField] string _title;
    [SerializeField] string _content;

    [SerializeField] UITexture _bigBanner;
    [SerializeField] UILabel _lblBigContent;

    [SerializeField] UITexture _smallBanner;
    [SerializeField] UILabel _lblSmallContent;

    [SerializeField] UIPanel _scrollView;

    [SerializeField] GameObject _bigGroup;
    [SerializeField] GameObject _smallGroup;

    JSONNode _node;

	// Use this for initialization
	void Start () {
	
	}
	
	public void SetProductDetail(GatchaProductType pType) {

        this.gameObject.SetActive(true);

        InitScrollView();

        

        if (pType == GatchaProductType.Special) {
            _node = GetProductNode("special");

            _bigGroup.SetActive(true);
            _smallGroup.SetActive(false);

            _bigBanner.mainTexture = GameSystem.Instance.SpecialSmallBanner;
            _content = _node["bannertext"];
            _lblBigContent.text = _content;

        } 
        else if (pType == GatchaProductType.Free) {
            _node = GetProductNode("free");
            //_banner.mainTexture = GameSystem.Instance.FreeSmallBanner;
        }
        else if (pType == GatchaProductType.Fish) {
            _node = GetProductNode("fish");

            _bigGroup.SetActive(false);
            _smallGroup.SetActive(true);

            _smallBanner.mainTexture = GameSystem.Instance.FishSmallBanner;
            _content = _node["bannertext"];
            _lblSmallContent.text = _content;
        }

        
        

    }

    private void InitScrollView() {
        _scrollView.gameObject.GetComponent<UIScrollView>().ResetPosition();
        _scrollView.clipOffset = Vector2.zero;
        _scrollView.transform.localPosition = new Vector3(0, 21, 0);
    }


    private JSONNode GetProductNode(string pType) {
        for(int i=0; i<GameSystem.Instance.GatchaBannerInitJSON.Count; i++) {

            if (GameSystem.Instance.GatchaBannerInitJSON[i]["type"].Value == pType)
                return GameSystem.Instance.GatchaBannerInitJSON[i];

        }

        return null;
    }


}
