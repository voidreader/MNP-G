using UnityEngine;
using System.Collections;

public class SimpleNickCtrl : MonoBehaviour {


    [SerializeField] UIInput _inputNick;
    [SerializeField] UILabel _lblError;

	// Use this for initialization
	void Start () {
	
        
	}

    void OnEnable() {

        if (GameSystem.Instance.UserName.ToUpper() == "UNNAMED" || GameSystem.Instance.UserName.ToUpper() == "GUEST" || string.IsNullOrEmpty(GameSystem.Instance.UserName))
            _inputNick.value = GameSystem.Instance.GetLocalizeText(4205);
        else
            _inputNick.value = GameSystem.Instance.UserName;


        _lblError.gameObject.SetActive(false);

    }
	
	public void OnClickInput() {

        Debug.Log("OnClickInput");

        if(_inputNick.value == GameSystem.Instance.GetLocalizeText(4205)) {
            _inputNick.value = "";
        }
    }

    public void Confirm() {
        Debug.Log("New Nick Name ::" + _inputNick.value);
        //GameSystem.Instance.UserName = _inputNick.value;

        if (_inputNick.value == GameSystem.Instance.UserName)
            return;

        GameSystem.Instance.Post2RequestNickName(_inputNick.value);
        //this.SendMessage("CloseSelf");
    }


    /// <summary>
    /// 
    /// </summary>
    private void OnCompleteNickName() {

        // 게임시작 
        //TitleCtrl.Instance.StartGame();
        //this.transform.DOScale(0, 0.3f).SetEase(Ease.InBack);
    }

    /// <summary>
    /// 닉네임 변경 실패
    /// </summary>
    private void OnCompleteFalse() {
        //_lblError.gameObject.SetActive(true);
        //_lblError.text = GameSystem.Instance.GetLocalizeText("4206");

        _lblError.gameObject.SetActive(true);
        _lblError.text = GameSystem.Instance.GetLocalizeText("4206");
    }


    private void OnCompleteFalseExistsNickname() {

        _lblError.gameObject.SetActive(true);
        _lblError.text = GameSystem.Instance.GetLocalizeText("4264");
    }

    
}
