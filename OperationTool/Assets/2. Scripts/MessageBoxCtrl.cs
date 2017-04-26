using UnityEngine;
using System.Collections;
using System;

public class MessageBoxCtrl : MonoBehaviour {

    Action OnConfirm;

    string _msg = String.Empty;

    public UILabel _lblMessage;

	// Use this for initialization
	void Start () {
	
	}
	
    void InitMessageBox() {
        this.gameObject.SetActive(true);
        OnConfirm = delegate { };
    }

	public void OpenMessageBox(Action pConfirm, string pMessage) {
        InitMessageBox();

        OnConfirm += pConfirm;

        _msg = pMessage;
        _lblMessage.text = _msg;
    }


    public void Confirm() {
        OnConfirm();
        Close();
    }
        


    public void Close() {
        this.gameObject.SetActive(false);
    }
}
