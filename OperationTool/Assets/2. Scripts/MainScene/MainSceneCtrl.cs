using UnityEngine;
using System.Collections;

public class MainSceneCtrl : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	public void GoMailScene() {
        Application.LoadLevel("MailScene");
    }
}
