using UnityEngine;
using System.Collections;

public class MailSceneCtrl : MonoBehaviour {


    public UIPopupList _listMailType;
    public UIPopupList _listGiftType;
    public UIPopupList _listWhen;
    public UIPopupList _listExpire;

    public MessageBoxCtrl _messageBox;

    [SerializeField] string _mailValue;
    [SerializeField] string _giftValue;
    [SerializeField] string _whenValue;
    [SerializeField] string _expireValue;

    [SerializeField] int _mailtype;
    [SerializeField] int _gifttype;
    [SerializeField] int _when;
    [SerializeField] int _expire;

    // Use this for initialization
    void Start () {

        Init();
	}

    void Init() {
        _mailtype = -1;
        _gifttype = -1;
        _when = -1;
        _expire = -1;
    }


    /// <summary>
    /// 메일 보내기 
    /// </summary>
    public void SendToEveryone() {
        Debug.Log("★ Send to everyone!");
    }

    public void ConfirmMail() {

        string msg = string.Empty;

        

        msg += "전체메일 발송전 확인사항 입니다";
        msg += "\n";
        msg += "메일형태 : " + _mailValue + "\n";
        msg += "재화 : " + _giftValue + "\n";
        msg += "발송시간 : " + _whenValue + "\n";
        msg += "만료시간 : " + _expireValue + "\n";


        _messageBox.OpenMessageBox(SendToEveryone, msg);
    }

	
    public void OnValueChangedMailType() {
        Debug.Log("Value :: " + _listMailType.value);
        _mailValue = _listMailType.value;

        switch (_listMailType.value) {
            case "Push Bonus":
                _mailtype = 17;
                break;

            case "Event Bonus":
                _mailtype = 50;
                break;

            case "Test Bonus":
                _mailtype = 50;
                break;
        }
        
    }

    public void OnValueChangedGiftType() {
        Debug.Log("Value :: " + _listGiftType.value);
        _giftValue = _listGiftType.value;

        switch (_listGiftType.value) {
            case "Coin":
                _gifttype = 0;
                break;

            case "Heart":
                _gifttype = 1;
                break;

            case "Gem":
                _gifttype = 2;
                break;

            case "Fish1":
                _gifttype = 5;
                break;

            case "Fish2":
                _gifttype = 6;
                break;

            case "Fish3":
                _gifttype = 7;
                break;

            case "Neko":
                _gifttype = 8;
                break;


            case "Freeticket":
                _gifttype = 9;
                break;

            case "Rareticket":
                _gifttype = 10;
                break;

            case "Rainbowticket":
                _gifttype = 11;
                break;

        }
    }

    public void OnValueChangedWhen() {
        Debug.Log("Value :: " + _listWhen.value);
        _whenValue = _listWhen.value;

        switch (_listWhen.value) {
            case "Now!":
                _when = 0;
                break;

            case "+5 Min":
                _when = 5;
                break;

            case "+10 Min":
                _when = 10;
                break;

            case "+1 Hour":
                _when = 60;
                break;

            case "+2 Hour":
                _when = 120;
                break;

            case "+3 Hour":
                _when = 180;
                break;

            case "+1 Day":
                _when = 1440;
                break;

            case "+2 Day":
                _when = 2880;
                break;

            case "+3 Day":
                _when = 4320;
                break;
        }

    }

    public void OnValueChangedExpired() {
        Debug.Log("Value :: " + _listExpire.value);
        _expireValue = _listExpire.value;

        switch (_listExpire.value) {
            case "+1 Day":
                _expire = 0;
                break;

            case "+2 Day":
                _expire = 5;
                break;

            case "+30 Day":
                _expire = 10;
                break;

            case "+365 Day":
                _expire = 60;
                break;

        }
    }
}
