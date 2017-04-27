using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PathologicalGames;

public class HeartRequestCtrl : MonoBehaviour {

	[SerializeField] UILabel lblName;
	[SerializeField] UILabel lblTime;
	[SerializeField] UISprite spTime;

	[SerializeField] UIButton btnSend;
    [SerializeField] UIButton btnCheck;

	[SerializeField] UILabel lblButton;

	[SerializeField] UITexture spPic;

	[SerializeField] private string friendID;
	private List<string> _listSentHis = null;
	[SerializeField] bool isPossibleSend = false;
	private FB_UserInfo _facebookUserInfo;

    [SerializeField] bool _isChecked = false;

	// Use this for initialization
	void Start () {
	
	}
	

	void OnSpawned() {
		// 첫 로드시에는 false로 로드. 
		spTime.gameObject.SetActive (false);
		lblTime.gameObject.SetActive (false);
	}

	private void OnProfileImageLoaded(FB_UserInfo fbuser) {
		spPic.mainTexture = fbuser.GetProfileImage (FB_ProfileImageSize.square);
		fbuser.OnProfileImageLoaded -= OnProfileImageLoaded;
	}

	/// <summary>
	/// Sets the heart request.
	/// </summary>
	/// <param name="friend">Friend.</param>
	public void SetHeartRequest(FB_UserInfo friend, bool pIsHeart) {
        _isChecked = false;


        _facebookUserInfo = friend;

        // 사진 정보 조회 
        _facebookUserInfo.OnProfileImageLoaded += OnProfileImageLoaded;
        _facebookUserInfo.LoadProfileImage(FB_ProfileImageSize.square);


		this.transform.localScale = GameSystem.Instance.BaseScale;
		this.gameObject.SetActive (true);
		lblName.text = _facebookUserInfo.Name;
		friendID = _facebookUserInfo.Id;


		// 보낸 리스트 체크 , 버튼 설정 
		spTime.gameObject.SetActive (false);
		lblTime.gameObject.SetActive (false); 
		btnSend.normalSprite = "common_btn_yellow";
		lblButton.text = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3078);
		isPossibleSend = true;

        


        _listSentHis = GameSystem.Instance.LoadHeartSentHistory ();

		for(int i=0; i<_listSentHis.Count; i++) {
			
			// 보낸리스트에 있으면 시간과 버튼 변경 처리 
			if(_listSentHis[i].Equals(this.friendID)) {
				
				btnSend.normalSprite = "option_btn_gray";
                lblButton.text = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3068);
				isPossibleSend = false;
				
				spTime.gameObject.SetActive (true);
				lblTime.gameObject.SetActive (true);
				
				System.TimeSpan ts = new System.TimeSpan(GameSystem.Instance.LoadNextResetTimeTick() - GameSystem.Instance.SyncTime);
				
				lblTime.text = string.Format (GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3102), ts.Hours, ts.Minutes);
				
				break;
			}
			
		}

        /* heart , invite 분기 처리 */
        if(pIsHeart) {
            btnCheck.gameObject.SetActive(false);
            btnSend.gameObject.SetActive(true);
            
        } else {
            btnCheck.gameObject.SetActive(true);
            btnSend.gameObject.SetActive(false);
            lblTime.gameObject.SetActive(false);
            


            if(_isChecked) {
                btnCheck.normalSprite = PuzzleConstBox.spriteChecked;
                MNPFacebookCtrl.Instance.TargetUsers.Add(friendID);
            } else {
                btnCheck.normalSprite = PuzzleConstBox.spriteUnchecked;
            }

            // add
            //MNPFacebookCtrl.Instance.TargetUsers.Add(friendID);
        }


	}

    /// <summary>
    /// 체크버튼 
    /// </summary>
    public void OnClickChecker() {
        _isChecked = !_isChecked;

        if (_isChecked) {
            btnCheck.normalSprite = PuzzleConstBox.spriteChecked;
            MNPFacebookCtrl.Instance.TargetUsers.Add(friendID);
        }
        else {
            btnCheck.normalSprite = PuzzleConstBox.spriteUnchecked;
            MNPFacebookCtrl.Instance.TargetUsers.Remove(friendID);
        }

    }


    /// <summary>
    /// 하트 보내기 
    /// </summary>
	public void SendHeart() {

		if (!isPossibleSend) {
			return;
		}

		GameSystem.Instance.CurrentHeartRequest = this;
		GameSystem.Instance.SendFacebookHeart (friendID);
	}



	public void Receiving() {
		_listSentHis = GameSystem.Instance.LoadHeartSentHistory ();
		
		if (_listSentHis == null)
			return;
		

		for(int i=0; i<_listSentHis.Count; i++) {
			
			// 보낸리스트에 있으면 시간과 버튼 변경 처리 
			if(_listSentHis[i].Equals(this.friendID)) {
				
				//Debug.Log ("!!!! Receiving #2");
				
				btnSend.normalSprite = "option_btn_gray";
				lblButton.text = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3068);
                isPossibleSend = false;
				
				spTime.gameObject.SetActive (true);
				lblTime.gameObject.SetActive (true);
				
				System.TimeSpan ts = new System.TimeSpan(GameSystem.Instance.LoadNextResetTimeTick() - GameSystem.Instance.SyncTime);
				
				lblTime.text = string.Format (GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3102), ts.Hours, ts.Minutes);
				
				break;
			}
			
		}
	}
}
