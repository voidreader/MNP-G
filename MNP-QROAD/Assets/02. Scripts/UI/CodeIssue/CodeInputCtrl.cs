using UnityEngine;
using System.Collections;

public class CodeInputCtrl : MonoBehaviour {



    [SerializeField] UIInput _input;
    [SerializeField] UILabel _lblInput;

	// Use this for initialization
	void Start () {
	
	}

    void OnEnable() {
        _lblInput.text = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3099);
        //_input.value = GameSystem.Instance.GetLocalizeText(3099);
        
    }
	
	public void SendCode() {

        GameSystem.Instance.Post2CodeInput(_input.value.ToUpper());

    }

    public void OnClickInput() {

        Debug.Log("OnClickInput");

        if (string.IsNullOrEmpty(_input.value))
            return;

        if (_input.value == GameSystem.Instance.GetLocalizeText(3099)) {
            _input.value = "";
        }
    }

}
