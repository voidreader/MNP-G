using UnityEngine;
using System.Collections;
using SimpleJSON;
using BestHTTP;

public class MailSceneCtrl : MonoBehaviour {


    public UIPopupList _listMailType;
    public UIPopupList _listGiftType;
    public UIPopupList _listWhen;
    public UIPopupList _listExpire;
    public UIInput _iptQuantity;
    public UILabel _lblResult;

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

        _iptQuantity.value = "0";

        _lblResult.gameObject.SetActive(false);
            
    }


    /// <summary>
    /// 메일 보내기 
    /// </summary>
    public void SendToEveryone() {
        Debug.Log("★ Send to everyone!");


        JSONNode node = JSON.Parse("{}");

        node["mailtype"].AsInt = _mailtype;
        node["gifttype"].AsInt = _gifttype;
        node["when"].AsInt = _when;
        node["expire"].AsInt = _expire;


        node["quantity"].AsInt = int.Parse(_iptQuantity.value);


        WWWHelper.Instance.Post2WithJSON("request_sendmailtoeveryone", OnFinishedMailToEveryone, node);

    }

    private void OnFinishedMailToEveryone(HTTPRequest request, HTTPResponse response) {

        JSONNode result = JSON.Parse(response.DataAsText);

        Debug.Log(">>> request.State :: " + request.State.ToString());

        if (request.State == HTTPRequestStates.ConnectionTimedOut || request.State == HTTPRequestStates.TimedOut
            || request.State == HTTPRequestStates.Error) {


            if (!string.IsNullOrEmpty(request.Exception.Message)) {
                Debug.Log("Request Exception :: " + request.Exception.Message);
            }

        }

        Debug.Log("★★ OnFinishedMailToEveryone :: " + result.ToString());

        if (result["result"].AsInt != 0) {
            SetResult("메일 발송 예약이 잘못되었습니다. 확인 바랍니다.");
            return;
        }

        SetResult("처리가 완료되었습니다.");


    }


    void SetResult(string pMsg) {
        _lblResult.gameObject.SetActive(true);
        _lblResult.text = pMsg;
    }

    public void ConfirmMail() {

        string msg = string.Empty;
        _lblResult.gameObject.SetActive(false);

        if(_mailtype < 0) {
            SetResult("메일형태가 입력되지 않았습니다.");
            return;
        }

        if (_gifttype < 0) {
            SetResult("재화형태가 입력되지 않았습니다.");
            return;
        }

        if (_when < 0) {
            SetResult("발송시점이 입력되지 않았습니다.");
            return;
        }

        if (_expire < 0) {
            SetResult("만료일자가 입력되지 않았습니다.");
            return;
        }

        // 유효성 체크
        if (_iptQuantity.value == "0" || string.IsNullOrEmpty(_iptQuantity.value)) {
            SetResult("수량이 입력되지 않았습니다.");
            return;
        }

             
        

        msg += "전체메일 발송전 확인사항 입니다";
        msg += "\n\n";
        msg += "메일형태 : " + _mailValue + "\n";
        msg += "재화 : " + _giftValue + "\n";
        msg += "수량 : " + _iptQuantity.value + "\n";
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

        // 초로 계산한다. 
        switch (_listWhen.value) {
            case "Now!":
                _when = 0;
                break;

            case "+5 Min":
                _when = 300;
                break;

            case "+10 Min":
                _when = 600;
                break;

            case "+1 Hour":
                _when = 3600;
                break;

            case "+2 Hour":
                _when = 7200;
                break;

            case "+3 Hour":
                _when = 10800;
                break;

            case "+1 Day":
                _when = 86400;
                break;

            case "+2 Day":
                _when = 172800;
                break;

            case "+3 Day":
                _when = 259200;
                break;
        }

    }

    public void OnValueChangedExpired() {
        Debug.Log("Value :: " + _listExpire.value);
        _expireValue = _listExpire.value;

        // 일자로 계산한다. 
        switch (_listExpire.value) {
            case "+1 Day":
                _expire = 1;
                break;

            case "+2 Day":
                _expire = 2;
                break;

            case "+30 Day":
                _expire = 30;
                break;

            case "+365 Day":
                _expire = 365;
                break;

        }
    }
}
