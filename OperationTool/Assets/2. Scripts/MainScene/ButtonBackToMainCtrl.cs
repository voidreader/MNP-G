using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBackToMainCtrl : MonoBehaviour {

    public void BackToMain() {
        Application.LoadLevel("MainScene");
    }
}
