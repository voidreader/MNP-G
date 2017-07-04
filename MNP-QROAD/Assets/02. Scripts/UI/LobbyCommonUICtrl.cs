using UnityEngine;
using System.Collections;
using DG.Tweening;
using PathologicalGames;

public class LobbyCommonUICtrl : MonoBehaviour {

    bool _isAnimation = false;


    /// <summary>
    /// 시작하자마자 Disable 상태로 처리 
    /// </summary>
    void Awake() {

        // UI 최적화 테스트 진행중 
        if (this.CompareTag("Attendance") || this.CompareTag("DontNeedSetDisable") || this.CompareTag("BigPopup") || this.CompareTag("NekoGiftResult") || this.CompareTag("ReadyGroup")) {
			Debug.Log(">>>>> Awake");
			SetPosition();
			return;
		}

        if(this.CompareTag("ResultAreaCamera")) {
            Debug.Log(">>>>> Awake");
            SetResultFormPosition();
            return;
        }

		if(!this.CompareTag("NoDisable"))
			SetDisable ();


	}
	
	void OnEnable() {

		/*
		if (this.CompareTag ("Attendance")) {
			Debug.Log(">>>>> Attendance OnEnable");
		}
		*/

		Open ();
	}
	
	
	private void Open() {

        _isAnimation = true;

        // Push
        LobbyCtrl.Instance.PushPopup (this);

		this.transform.DOScale (1, 0.15f).SetEase (Ease.OutBack).OnComplete(OnCompleteOpen);
	}

	// 취소(백버튼)을 통한 종료 
	public void Close() {

        if (_isAnimation) {
            // 다시 입력 
            LobbyCtrl.Instance.PushPopup(this);
            return;
        }

		DoBeforeClose ();

		//this.transform.DOScale (0, 0.3f).SetEase (Ease.InBack).OnComplete (SetDisable);
		SetClose ();
	}

	// 자가 종료 (X버튼 등)
	public void CloseSelf() {

        if (_isAnimation)
            return;


        LobbyCtrl.Instance.PopPopup ();

		DoBeforeClose ();

		//this.transform.DOScale (0, 0.3f).SetEase (Ease.InBack).OnComplete (SetDisable);
		SetClose ();
	}

	public void DoBeforeClose() {


        if (this.CompareTag("BigPopup")) {
            this.GetComponent<BigPopupCtrl>().OnClosing();
            return;
        }

        if (this.CompareTag("PlayerOwnNeko")) {
            LobbyCtrl.Instance.ClearCharacterList();
        } else if (this.CompareTag("HeartRequestGroup")) {
            LobbyCtrl.Instance.ClearHeartRequestPool();
        } else if (this.CompareTag("NekoGiftResult")) {

        }
        else if (this.CompareTag("DontNeedSetDisable")) {

            if (this.gameObject.GetComponent<CatInformationCtrl>() != null) {
                LobbyCtrl.Instance.ClearCharacterList();
            }

        }

	}

	private void OnCompleteOpen() {

        _isAnimation = false; // 애니메이션 종료 

        if (this.CompareTag("BigPopup")) {
            this.GetComponent<BigPopupCtrl>().OnCompleteOpen();
            return;
        }

        // Neko Select 
        if (this.CompareTag ("PlayerOwnNeko")) {

            // LobbyCtrl.Instance.SpawnCharacterList(LobbyCtrl.Instance.IsReadyCharacterList);

		} else if (this.CompareTag ("HeartRequestGroup")) {
			LobbyCtrl.Instance.SpawnHeartFriendList ();
		}


	}

	private void OnCompleteClose() {

		if (this.CompareTag ("Attendance")) {
            LobbyCtrl.Instance.SendMessage("CheckNotice");
		}
        else if (this.CompareTag("ReadyGroup")) {
            LobbyCtrl.Instance.CheckProceedUnlock();
        }
	}


	void SetPosition() {
		this.transform.position = Vector3.zero; // 위치를 기본 위치로 옮겨놓는다. 
		this.transform.localPosition = Vector3.zero;
		this.transform.localScale = Vector3.zero;
	}

    void SetResultFormPosition() {
        this.transform.position = Vector3.zero; // 위치를 기본 위치로 옮겨놓는다. 
        this.transform.localPosition = new Vector3(0, -80, 0);
        this.transform.localScale = Vector3.zero;

        
    }


    /// <summary>
    /// Object Disable
    /// </summary>
    void SetDisable() {

		SetPosition ();

		this.gameObject.SetActive (false);

        _isAnimation = false; // 애니메이션 종료 
    }

	void SetClose() {

		SetPosition ();

		this.gameObject.SetActive (false);
		
		OnCompleteClose ();


        _isAnimation = false; // 애니메이션 종료 

    }

}
