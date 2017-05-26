using UnityEngine;
using System.Collections;
using DG.Tweening;

public class FirstNickCtrl : MonoBehaviour {

    [SerializeField]
    UIInput _inputNick;

    [SerializeField]
    bool _isChecked = false;

    [SerializeField] UIButton _btnCheck;

    [SerializeField]
    UILabel _lblError;

    bool _isSending = false;

    // Use this for initialization
    void Start () {
	
	}

    void OnEnable() {

        if (GameSystem.Instance.UserName.ToUpper() == "UNNAMED" || GameSystem.Instance.UserName.ToUpper() == "GUEST")
            _inputNick.value = GameSystem.Instance.GetLocalizeText("4205");
        else
            _inputNick.value = GameSystem.Instance.UserName;


        _isSending = false;
    }

    public void Confirm() {

        Debug.Log("NickName :: " + _inputNick.value);

        _lblError.gameObject.SetActive(false);

        if(_inputNick.value == GameSystem.Instance.GetLocalizeText("4205")
            || _inputNick.value.ToUpper() == "UNNAMED" 
            || _inputNick.value.ToUpper() == "GUEST"
            || string.IsNullOrEmpty(_inputNick.value)) {


            _lblError.text = GameSystem.Instance.GetLocalizeText("4203");
            _lblError.gameObject.SetActive(true);
            return;
        }


        if(!_isChecked) {
            _lblError.text = GameSystem.Instance.GetLocalizeText("4204");
            _lblError.gameObject.SetActive(true);
            return;
        }


        Debug.Log("New Nick Name ::" + _inputNick.value);
        //GameSystem.Instance.UserName = _inputNick.value;

        if (_isSending)
            return;

        _isSending = true;
        GameSystem.Instance.Post2RequestNickName(_inputNick.value);
        
    }

    /// <summary>
    /// 
    /// </summary>
    private void OnCompleteNickName() {

        _isSending = false;

        Debug.Log("OnCompleteNickName");

        // 게임시작 
        TitleCtrl.Instance.StartGame();
        this.transform.DOScale(0, 0.3f).SetEase(Ease.InBack);
    }

    /// <summary>
    /// 닉네임 변경 실패
    /// </summary>
    private void OnCompleteFalse() {
        _isSending = false;

        _lblError.gameObject.SetActive(true);
        _lblError.text = GameSystem.Instance.GetLocalizeText("4206");
    }


    /// <summary>
    /// 체크 클릭 
    /// </summary>
    public void OnClickCheck() {
        _isChecked = !_isChecked;

        if (_isChecked)
            _btnCheck.normalSprite = "c-in";
        else
            _btnCheck.normalSprite = "c-none";
    }

    /// <summary>
    /// 이용약관 클릭 
    /// </summary>
    public void OnClickURL() {
        //Application.OpenURL("https://partners.nanoo.so/game/bbs/view/1/424697?code=538eaceb469d24570207033e341a1c17"); 
        Application.OpenURL(GameSystem.Instance.UrlTerm);
    }
}
