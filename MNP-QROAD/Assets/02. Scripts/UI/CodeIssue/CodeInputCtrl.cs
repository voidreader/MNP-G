using UnityEngine;
using System.Collections;

public class CodeInputCtrl : MonoBehaviour {



    [SerializeField] UIInput _input;

	// Use this for initialization
	void Start () {
	
	}

    void OnEnable() {
        _input.value = GameSystem.Instance.GetLocalizeText(3099);
    }
	
	public void SendCode() {

        GameSystem.Instance.Post2CodeInput(_input.value.ToUpper());

    }

    public void OnClickInput() {

        Debug.Log("OnClickInput");

        if (_input.value == GameSystem.Instance.GetLocalizeText(3099)) {
            _input.value = "";
        }
    }

}
