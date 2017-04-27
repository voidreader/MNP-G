using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System.IO;

public partial class GameSystem : MonoBehaviour {


    #region Facebook 

	private List<string> _listSendHeartHistory = new List<string>();
	private string _heartFriendFacebookID = null;







    /// <summary>
    /// Sends the facebook heart.
    /// </summary>
    /// <param name="pFriendID">P friend I.</param>
    public void SendFacebookHeart(string pFriendID) {
		//SPFacebook.Instance.SendGift ("하트 선물", "밋치리네코 팝 하트 선물이 도착했어요", _fbHeartID, "", new string[]{pFriendID});

		Debug.Log (">>> SendFacebookHeart friendID :: " + pFriendID);

		_heartFriendFacebookID = pFriendID;


        Post2SendHeart (pFriendID);

        //SPFacebook.Instance.SendInvite("밋치리네코팝", "Play with me", "", new string[] { pFriendID });
        //LoadInvitableFriends();
    }


    void OnAppRequestCompleteAction(FB_AppRequestResult result) {


        //Debug.Log("Original Facebook Responce: " + result.ToString());
        Debug.Log("Original Facebook Responce: " + result.RawData.ToString());

        if (result.IsSucceeded) {
            Debug.Log("App request succeeded");
            Debug.Log("ReuqetsId: " + result.ReuqestId);
            foreach (string UserId in result.Recipients) {
                Debug.Log(UserId);
            }
            
        }
        else {
            Debug.Log("App request has failed");
        }
    }



    private void SaveHeartSentHistory() {
		ES2.Save (_listSendHeartHistory, "HeartSentList");
	}

	public List<string> LoadHeartSentHistory() {
		if (ES2.Exists ("HeartSentList")) {
			return ES2.LoadList<string> ("HeartSentList");
		} else {
			return null;
		}

	}





	/// <summary>
	/// 페이스북 하트 보내기 다음 리셋 시간 표시
	/// </summary>
	private void SaveNextResetTime() {
		string _resettime = _resetSendHeartTime.Month.ToString () + _resetSendHeartTime.Day.ToString ();
		_listSendHeartHistory = LoadHeartSentHistory();
		if (_listSendHeartHistory == null) {
			_listSendHeartHistory = new List<string>();
		}

		Debug.Log("◆◆◆◆◆ SaveNextResetTime :: " + _resettime);

		// 이미 같은 값을 가지고 있다면 return
		if (ES2.Exists ("ResetSendHeartTime") && ES2.Load<string> ("ResetSendHeartTime").Equals (_resettime)) {
			return;
		}

		// 기존에 있던 값보다 현재 시간이 더 크다면, 친구 리스트를 초기화 하고 저장 
		if (LoadNextResetTimeTick () <= _syncTime) {
			Debug.Log("◆◆◆◆◆ Reset Sent History ");
			_listSendHeartHistory.Clear ();
			SaveHeartSentHistory ();
		} 


		ES2.Save<string> (_resetSendHeartTime.Month.ToString () + _resetSendHeartTime.Day.ToString (), "ResetSendHeartTime");
		ES2.Save<long> (_resetSendHeartTime.Ticks, "ResetSendHeartTimeTick"); // 실제 틱 
	}

	public long LoadNextResetTimeTick() {
		if (ES2.Exists ("ResetSendHeartTimeTick")) {
			return ES2.Load<long> ("ResetSendHeartTimeTick");
		} else {
			return 0;
		}


	}




	private void OnFocusChanged(bool focus) {
		if (!focus)  {                                                                                  
			// pause the game - we will need to hide                                             
			Time.timeScale = 0;                                                                  
		} else  {                                                                                       
			// start the game back up - we're getting focus again                                
			Time.timeScale = 1;                                                                  
		}  
	}



#endregion



}
