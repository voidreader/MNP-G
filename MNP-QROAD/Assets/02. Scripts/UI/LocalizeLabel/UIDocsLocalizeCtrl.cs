using UnityEngine;
using System.Collections;



public class UIDocsLocalizeCtrl : MonoBehaviour {

	public string _textID;
	public UILabel _targetLabel = null;
	[SerializeField] string _text = null;


	void Awake() {
		GetLabelComponent ();
		GetText ();
	}


	void OnEnable() {
		GetLabelComponent ();
		GetText ();
	}

	private void GetLabelComponent() {
		if (_targetLabel == null) {
			_targetLabel = this.gameObject.GetComponent<UILabel>();
		}


        
    }

	private void GetText() {

        if (string.IsNullOrEmpty(_textID))
            return;

		//Debug.Log ("▶ GetText #1 ");
		_text = GameSystem.Instance.GetLocalizeText(_textID);
		_targetLabel.text = _text;


        /*
        if (GameSystem.Instance.GameLanguage == SystemLanguage.Thai) {
            _targetLabel.trueTypeFont = GameSystem.Instance.ThaiFont;
        }
        else {
            _targetLabel.trueTypeFont = GameSystem.Instance.ThaiFont;
        }
        */

    }

}
