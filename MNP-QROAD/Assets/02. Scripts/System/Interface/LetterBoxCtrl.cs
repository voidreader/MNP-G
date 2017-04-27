using UnityEngine;
using System.Collections;

public class LetterBoxCtrl : MonoBehaviour {

    [SerializeField]
    tk2dTiledSprite _letterBox;

    void Start() {
        SetSize();
    }

	public void SetSize() {

        Debug.Log("SetSize!");
        _letterBox.dimensions.Set(Screen.width, Screen.height);
    }
}
