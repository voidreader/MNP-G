using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PathologicalGames;

public partial class LobbyCtrl : MonoBehaviour {

	//[SerializeField] GameObject _objMailBox;
	[SerializeField] GameObject _objMailNew;
	[SerializeField] UIPanel _pnlMailBox;
    
	[SerializeField] List<MailColumnCtrl> _listMailColumn = new List<MailColumnCtrl>(); // 메일 리스트 정보 

    Vector3 _mailBoxScrollViewPos = new Vector3(0, 295, 0);

    
    bool _hasCantReadMail = false; // 읽기 못한 메일이 있는 경우 (일괄읽기)
    bool _isProcessingReadAllMail = false;


    public void OpenMailBox() {
        // 오브젝트 활성화
        //_objMailBox.SetActive (true);
        bigPopup.gameObject.SetActive(true);
        bigPopup.SetMailBox();


        AdbrixManager.Instance.SendAdbrixInAppActivity(AdbrixManager.Instance.BUTTON_MAIL);
    }


    // 강제 new 설정
    public void ForceSetMailBoxNew() {
        GameSystem.Instance.HasNewMail = true;
        UpdateMailBoxNew();
    }

	/// <summary>
	/// 메일 박스 New 표시 처리 
	/// </summary>
	public void UpdateMailBoxNew() {

		if (GameSystem.Instance.HasNewMail) {
			_objMailNew.SetActive (true);
		} else {
			_objMailNew.SetActive (false);
		}
		 
	}


    /// <summary>
    /// 
    /// </summary>
    void ForceMailNewSign() {
        _objMailNew.SetActive(true);
    }


	/// <summary>
	/// 서버에 메일 데이터 요청 
	/// </summary>
	public void RequestMailBoxData() {
		GameSystem.Instance.Post2MailList ();
	}

	/// <summary>
	/// 메일 개수만큼 
	/// </summary>
	public void SpawnMailBoxColumns() {

        PoolManager.Pools["MailBoxPool"].DespawnAll();



        _listMailColumn.Clear (); // 리스트 클리어 

		for (int i=0; i<GameSystem.Instance.MailData["data"]["maildatas"].Count; i++) {
			// Spawn과 동시에 세팅 
			PoolManager.Pools["MailBoxPool"].Spawn("MailColumn").GetComponent<MailColumnCtrl>().SetMailColumn(i); // Set 시점에 ListMailColumn에 Add 

		}

        // 메일 정렬 처리
        SortMailBox();

        //_gridMailBox.Reposition ();
        SetPositionListMailBox ();
	}


    /// <summary>
    /// MailBox 정렬 (maildbkey의 역순)
    /// </summary>
    private void SortMailBox() {
		_listMailColumn.Sort(delegate (MailColumnCtrl mail1, MailColumnCtrl mail2) { return mail2.Maildbkey.CompareTo(mail1.Maildbkey); });
    }


	// 각 메일 컬럼 수동 정렬 
	public void SetPositionListMailBox() {

        // 위치 초기화 
        _pnlMailBox.GetComponent<UIScrollView>().ResetPosition();
        _pnlMailBox.clipOffset = Vector2.zero;
        _pnlMailBox.transform.localPosition = _mailBoxScrollViewPos;


        for (int i=0; i<_listMailColumn.Count; i++) {
			//_listMailColumn[i].transform.position = nguiCamera.ViewportToWorldPoint(camera.WorldToViewportPoint(new Vector3(0, i * _gridMailBox.cellHeight * -1, 0)));
			_listMailColumn[i].transform.localPosition = new Vector3(0, i * _pnlMailBox.GetComponent<UIGrid>().cellHeight * -1, 0);
		}
	}

	public void CloseMailBox() {

		PoolManager.Pools ["MailBoxPool"].DespawnAll ();

		// 메일이 하나도 없었던 경우에만, 메일 체크 
		if (_listMailColumn.Count == 0) {
			GameSystem.Instance.Post2CheckNewMail (); // 메일 체크 
		}
		else {
			GameSystem.Instance.HasNewMail = true;
			UpdateMailBoxNew();
		}

	}


    /// <summary>
    /// 메일 리스트 재정렬 
    /// </summary>
    /// <param name="pColumn"></param>
    public void ArrangeMailBox(MailColumnCtrl pColumn) {
        _listMailColumn.Remove(pColumn);
        this.SetPositionListMailBox();
    }


    /// <summary>
    /// 모든 메일 순차 읽기 시작.
    /// </summary>
    public void StartReadAllMail() {

        // 중복 실행을 막는다.
        if (IsProcessingReadAllMail)
            return;


        HasCantReadMail = false;
        IsProcessingReadAllMail = true;

        // Flag 값을 초기화 
        for (int i = 0; i < _listMailColumn.Count; i++) {
            _listMailColumn[i].MailCheckFlag = false;
        }

        ReadAllMail();
    }

    /// <summary>
    /// 모든 메일 읽기 2016.07
    /// </summary>
    public void ReadAllMail() {

        Debug.Log("★ ReadAllMail Start ");

        for (int i = 0; i < _listMailColumn.Count; i++) {
            if (_listMailColumn[i].MailCheckFlag)
                continue;

            // 하트가 가득차 있는 상황에서 하트가 있는 경우는 넘긴다.
            /*
            if(GameSystem.Instance.HeartCount >= 5 && _listMailColumn[i].ColumnClass == "heart") {
                _listMailColumn[i].MailCheckFlag = true;
                HasCantReadMail = true;
                continue;
            }
            */

            // Ticket은 일괄읽기에서 읽을 수 없음 
            if (_listMailColumn[i].ColumnClass.IndexOf("ticket") >= 0) {
                _listMailColumn[i].MailCheckFlag = true;
                HasCantReadMail = true;
                continue;
            }


            GameSystem.Instance.Post2AllMailRead(_listMailColumn[i].Maildbkey, _listMailColumn[i], OnCompleteReadAllMail);
            return;
        }


        Debug.Log("★ ReadAllMail End ");
        IsProcessingReadAllMail = false;

        // 처리 완료된 경우 팝업을 오픈. 
        if (HasCantReadMail)
            OpenInfoPopUp(PopMessageType.ExistsCantReadMail);

        // 메일리스트 리로드 
        RequestMailBoxData();

    }

    private void OnCompleteReadAllMail(Transform pMail) {

        // 정상적으로 읽은 메일만 처리 
        if (!pMail.GetComponent<MailColumnCtrl>().MailCheckFlag) {
            PoolManager.Pools["MailBoxPool"].Despawn(pMail);
            LobbyCtrl.Instance.ArrangeMailBox(pMail.GetComponent<MailColumnCtrl>());
        }


        // 0.5초의 간격을 두고 실행.
        Invoke("ReadAllMail", 0.5f);
    }


	public List<MailColumnCtrl> ListMailColumn {
		get {
			return this._listMailColumn;
		}
	}

    public bool HasCantReadMail {
        get {
            return _hasCantReadMail;
        }

        set {
            _hasCantReadMail = value;
        }
    }

    public bool IsProcessingReadAllMail {
        get {
            return _isProcessingReadAllMail;
        }

        set {
            _isProcessingReadAllMail = value;
        }
    }
}
