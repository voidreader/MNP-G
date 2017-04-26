using UnityEngine;
using System.Collections;

public class NewNekoRowCtrl : MonoBehaviour {

	[SerializeField] int _id;
    [SerializeField] int _findResult = -1;
    [SerializeField] GameObject _clearCover;


    void OnEnable() {
        SetComplete();
    }

    public void OpenPage() {

        switch (_id) {
            case 0:
                WindowManagerCtrl.Instance.OpenEventAttendanceOnly(); // OK
                break;
            case 1:
                //LobbyCtrl.Instance.OpenInfoPopUp(PopMessageType.CommingSoon);
                WindowManagerCtrl.Instance.OpenGatchaConfirm(true);
                break;
            case 2:
                //LobbyCtrl.Instance.OpenBingo(); // OK
                LobbyCtrl.Instance.OpenInfoPopUp(PopMessageType.BingoStart, LobbyCtrl.Instance.OpenBingo);
                break;
            case 3:
                //LobbyCtrl.Instance.OpenInfoPopUp(PopMessageType.CommingSoon);
                //LobbyCtrl.Instance.OpenInfoPopUp(PopMessageType.GetLevel10Reward);
                break;
            case 4:
				LobbyCtrl.Instance.OpenInfoPopUp(PopMessageType.GetFacebookLinkReward, LobbyCtrl.Instance.OpenHeartRequest);
                //Application.OpenURL("https://mobile.twitter.com/mitchiripop");
                break;
        }
    }

    public void SetComplete() {
        switch(_id) {
            case 0:
                _findResult = GameSystem.Instance.FindUserNekoData(153); 
                break;
            case 1:
                _findResult = GameSystem.Instance.FindUserNekoData(151);
                break;
		case 2:
			_findResult = GameSystem.Instance.FindUserNekoData (155);
            /*
			if (_findResult >= 0) {
				_clearCover.SetActive(true);
				return;
			}

			if (!BingoMasterCtrl.Instance.CheckExistsEmptyCol ()) {
				_findResult = 1;
			} else {
				_findResult = -1;
			}
            */

                break;
            case 3:
                _findResult = GameSystem.Instance.FindUserNekoData(154);
                break;
            case 4:
                _findResult = GameSystem.Instance.FindUserNekoData(152);
                break;
        }


        if(_findResult < 0) {
            _clearCover.SetActive(false);
        }
        else {
            _clearCover.SetActive(true);
        }
    }
}
