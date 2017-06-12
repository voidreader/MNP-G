using UnityEngine;
using System.Collections;
using System;

public class MessageBoxCtrl : MonoBehaviour {

    Action OnConfirm;
    Action OnCancel;

    string _msg = String.Empty;

    public UILabel _lblMessage;

	// Use this for initialization
	void Start () {
	
	}
	
    void InitMessageBox() {
        this.gameObject.SetActive(true);
        OnConfirm = delegate { };
        OnCancel = delegate { };
    }

	public void OpenMessageBox(Action pConfirm, string pMessage) {
        InitMessageBox();

        OnConfirm += pConfirm;

        _msg = pMessage;
        _lblMessage.text = _msg;
    }

    public void OpenMessageBox(Action pConfirm, Action pCancel, string pMessage) {
        InitMessageBox();

        OnConfirm += pConfirm;
        OnCancel += pCancel;

        _msg = pMessage;
        _lblMessage.text = _msg;
    }


    public void Confirm() {
        OnConfirm();
        Close();
    }

    public void Cancel() {
        OnCancel();
        Close();
    }
        


    public void Close() {
        this.gameObject.SetActive(false);
    }
}
