using UnityEngine;
using System.Collections;
using PathologicalGames;
using SimpleJSON;
using DG.Tweening;

public class NoticeSmallBannerCtrl : MonoBehaviour {

    [SerializeField] UILabel _lblUpdated;
    [SerializeField] Transform _newSprtie;

    [SerializeField]
    UITexture _banner;

    [SerializeField]
    int _id;

    [SerializeField]
    string _action;

    JSONNode _node;

    [SerializeField] string _debugUpdateDate;
    System.DateTime _updatedDate;

    Vector3 _originLabelPos = new Vector3(-130, 150, 0);
    Vector3 _newLabelPos = new Vector3(-60, 150, 0);

    // Use this for initialization
    void Start () {
	
	}
	
    /// <summary>
    /// 배너 텍스트 설정 
    /// </summary>
    /// <param name="pIndex"></param>
    public void SetBanner(int pIndex) {

        //_lblUpdated.transform.DOKill();
        //_lblUpdated.transform.localScale = GameSystem.Instance.BaseScale;

        _newSprtie.gameObject.SetActive(false);
        _lblUpdated.transform.localPosition = _originLabelPos;

        _id = pIndex;

        if(GameSystem.Instance.ArrNoticeSmallTextures[pIndex] != null) {
            _banner.mainTexture = GameSystem.Instance.ArrNoticeSmallTextures[pIndex];
            _node = GameSystem.Instance.NoticeBannerInitJSON[pIndex];
            _debugUpdateDate = _node["updatedate"].Value;

            _action = _node["action"];
            
            if(_node["new"].AsBool) {
                //_debugUpdateDate = "[FFF55A]New[-] " + _debugUpdateDate;
                _newSprtie.gameObject.SetActive(true);
                _newSprtie.DOKill();
                _newSprtie.localScale = GameSystem.Instance.BaseScale;
                _newSprtie.DOScale(1.2f, 0.3f).SetLoops(-1, LoopType.Yoyo);

                _debugUpdateDate = "[FFF55A]" + _debugUpdateDate + "[-]";
                _lblUpdated.transform.localPosition = _newLabelPos;
            }
            
            

            _lblUpdated.text = _debugUpdateDate;

        }

    }

    void OnSpawned() {
        this.transform.localScale = GameSystem.Instance.BaseScale;
    }
	

    public void OnClickBanner() {


        if(_node["new"].AsBool) {
            GameSystem.Instance.NoticeBannerInitJSON[_id]["new"].AsBool = false; // 한번 봤으면 false 처리. 
            _debugUpdateDate = _node["updatedate"].Value;
            _lblUpdated.text = _debugUpdateDate;

            _lblUpdated.transform.localPosition = _originLabelPos;
            _newSprtie.gameObject.SetActive(false);


            GameSystem.Instance.SendMessage("SaveNoticeBannerJSON");
        }

		if (_node ["action"].Value.Equals("shop")) {
			LobbyCtrl.Instance.OpenGatchaConfirm ();
			return;
		}

        // OpenPackageWindow
        if (_node["action"].Value.Equals("pack")) {
            WindowManagerCtrl.Instance.OpenPackageWindow();
            return;
        }

        if (_node["action"].Value.Equals("trespass")) {
            WindowManagerCtrl.Instance.OpenEventPage();
            return;
        }

        if (_node["action"].Value.Equals("url")) {
            //WindowManagerCtrl.Instance.OpenEventPage();
            Application.OpenURL(_node["bannertext"].Value);
            return;
        }


        WindowManagerCtrl.Instance.OpenNoticeDetail(_id);

    }
}
