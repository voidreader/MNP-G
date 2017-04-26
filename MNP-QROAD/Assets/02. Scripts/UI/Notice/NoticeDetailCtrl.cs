using UnityEngine;
using System.Collections;
using SimpleJSON;

public class NoticeDetailCtrl : MonoBehaviour {


    [SerializeField] UILabel _lblContent;
    string _content;

    [SerializeField] UITexture _bigBanner;

    [SerializeField]
    UIPanel _scrollView;

    [SerializeField]
    string _actionType = "";

    JSONNode _noticeNode;

    [SerializeField]
    string _debugNode;

    [SerializeField] GameObject _btnClose;


    public void SetDetail(int pIndex) {

        this.gameObject.SetActive(true);
        InitScrollView();

        if (GameSystem.Instance.NoticeBannerInitJSON.Count > pIndex && GameSystem.Instance.ArrNoticeSmallTextures[pIndex] != null ) {


            _noticeNode = GameSystem.Instance.NoticeBannerInitJSON[pIndex];
            _debugNode = _noticeNode.ToString();

            _bigBanner.mainTexture = GameSystem.Instance.ArrNoticeSmallTextures[pIndex];
            _content = GameSystem.Instance.NoticeBannerInitJSON[pIndex]["bannertext"].Value;
            _lblContent.text = _content;

            _actionType = _noticeNode["action"].Value;

            // 액션 타입에 따른 추가 처리 
            if(_actionType == "event") {
                
                SetEventAction();
            }
            else {
                
                SetNoticeAction();
            }

        }

    }

    /// <summary>
    /// Event 용도 상세창 설정 
    /// </summary>
    private void SetEventAction() {
        _btnClose.SetActive(false);
    }

    /// <summary>
    /// 공지 용도 상세창 설정 
    /// </summary>
    private void SetNoticeAction() {
        _btnClose.SetActive(true);
    }


    private void InitScrollView() {
        _scrollView.gameObject.GetComponent<UIScrollView>().ResetPosition();
        _scrollView.clipOffset = Vector2.zero;
        _scrollView.transform.localPosition = new Vector3(0, 0, 0);
    }

    /// <summary>
    /// 확인 버튼 클릭 
    /// </summary>
    public void OnClickConfirm() {
        if(_actionType == "event") {
            Debug.Log("OpenEventPage");
            //WindowManagerCtrl.Instance.OpenEventPageOnly(); // 8월 17일 업데이트 이후 사용하지 않도록 처리
        }
    }
}
