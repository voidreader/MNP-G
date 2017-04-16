using UnityEngine;
using System.Collections;

public class NoticeListCtrl : MonoBehaviour {

    [SerializeField]
    UIPanel _scrollView;

    [SerializeField]
    GameObject _popup;

    [SerializeField] bool _isChecked = false;
    [SerializeField] UIButton _btnCheck;

    [SerializeField] GameObject _eventCover; // event 내용이 있을때, 앞에 씌우는 커버 

    bool _hasEvent = false; // Event 배너 포함 여부 

    // Use this for initialization
    void Start () {
	
	}

    void OnDisable() {
        DisableList();
    }
	
    public void EnableNoticeList() {

        //GameSystem.Instance.OnOffWaitingRequestInLobby(true);

        _popup.SetActive(false);
        _eventCover.SetActive(false);
        

        // 날짜 비교체크 
        if(GameSystem.Instance.LoadCurrentDayOfYear() == GameSystem.Instance.DtSyncTime.DayOfYear) {
            _isChecked = true;
        }

        this.gameObject.SetActive(true);
        InitScrollView();

        SetCheckButton();
        //Invoke("SpawnList", 0.4f);

        SpawnList();
        
    }

    private void SetCheckButton() {
        if (!_isChecked)
            _btnCheck.normalSprite = "c-none";
        else
            _btnCheck.normalSprite = "c-in";
    }

    /// <summary>
    /// 리스트 활성화 
    /// </summary>
    private void SpawnList() {
        _hasEvent = false;

        for (int i=0; i<GameSystem.Instance.NoticeBannerInitJSON.Count;i++) {

            if(GameSystem.Instance.NoticeBannerInitJSON[i]["action"].Value == "event" ||
                GameSystem.Instance.NoticeBannerInitJSON[i]["action"].Value == "trespass" ||
                GameSystem.Instance.NoticeBannerInitJSON[i]["action"].Value == "help") {
                _hasEvent = true;
                break;
            }
        }

        //GameSystem.Instance.OnOffWaitingRequestInLobby(false);
        StartCoroutine(Showing());

      }

    IEnumerator Showing() {

        if (_hasEvent && !LobbyCtrl.Instance.BigPopup.gameObject.activeSelf) {
            _eventCover.SetActive(true); // 이벤트 커버 씌우기. 

            // event 팝업을 오픈
            for (int i = 0; i < GameSystem.Instance.NoticeBannerInitJSON.Count; i++) {
                if (GameSystem.Instance.NoticeBannerInitJSON[i]["action"].Value == "event" ||
                    GameSystem.Instance.NoticeBannerInitJSON[i]["action"].Value == "trespass" ||
                    GameSystem.Instance.NoticeBannerInitJSON[i]["action"].Value == "help") {

                    yield return new WaitForSeconds(0.2f);

                    WindowManagerCtrl.Instance.OpenNoticeDetail(i);

                    yield return new WaitForSeconds(0.2f);


                    // 창이 떠있는 동안 대기.
                    while (WindowManagerCtrl.Instance.ObjNoticeDetail.gameObject.activeSelf) {
                        yield return new WaitForSeconds(0.1f);
                    }
                }
            }
        }


        _eventCover.SetActive(false);

        // Popup enable
        _popup.SetActive(true);


        // 배너 표현 
        for (int i = 0; i < GameSystem.Instance.NoticeBannerInitJSON.Count; i++) {
            LobbyCtrl.Instance.ListNoticeSmallBanner[i].gameObject.SetActive(true);
            LobbyCtrl.Instance.ListNoticeSmallBanner[i].SetBanner(i);
        }
        _scrollView.GetComponent<UIGrid>().Reposition();
    }


    /// <summary>
    /// Event 배너를 팝업 시키기. 
    /// </summary>
    /// <returns></returns>
    IEnumerator ShowingEventList() {

        _eventCover.SetActive(true); // 이벤트 커버 씌우기. 

        // WindowManagerCtrl.Instance.OpenNoticeDetail(_id);


        for (int i = 0; i < GameSystem.Instance.NoticeBannerInitJSON.Count; i++) {
            if(GameSystem.Instance.NoticeBannerInitJSON[i]["action"].Value == "event") {

                yield return new WaitForSeconds(0.2f);

                WindowManagerCtrl.Instance.OpenNoticeDetail(i);

                yield return new WaitForSeconds(0.2f);


                // 창이 떠있는 동안 대기.
                while(WindowManagerCtrl.Instance.ObjNoticeDetail.gameObject.activeSelf) {
                    yield return new WaitForSeconds(0.1f);
                }
            }
        }

        _eventCover.SetActive(false);

    }


    private void DisableList() {
        LobbyCtrl.Instance.SendMessage("ClearNoticeSmallBanner");
    }


    /// <summary>
    /// 스크롤 뷰 초기화 
    /// </summary>
    private void InitScrollView() {
        _scrollView.gameObject.GetComponent<UIScrollView>().ResetPosition();
        _scrollView.clipOffset = Vector2.zero;
        _scrollView.transform.localPosition = new Vector3(0, 200, 0);
    }


    public void OnClickCheck() {
        // c-none , c-in
        _isChecked = !_isChecked;

        SetCheckButton();

        // 오늘 하루 열지 않음 
        if (_isChecked) {
            GameSystem.Instance.SaveCurrentDayOfYear();
            //this.SendMessage("CloseSelf");
        }
        else {
            GameSystem.Instance.InitCurrnetDayOfYear();
        }


    }
}
