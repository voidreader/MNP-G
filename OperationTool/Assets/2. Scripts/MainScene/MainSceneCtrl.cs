using UnityEngine;
using System.Collections;
using BestHTTP;
using SimpleJSON;

public class MainSceneCtrl : MonoBehaviour {


    static MainSceneCtrl _instance = null;

    public static MainSceneCtrl Instance {
        get {
            if (_instance == null) {
                _instance = FindObjectOfType(typeof(MainSceneCtrl)) as MainSceneCtrl;

                if (_instance == null) {
                    Debug.Log("MainSceneCtrl Init Error");
                    return null;
                }
            }

            return _instance;
        }
    }



    // Use this for initialization
    void Start () {
        DontDestroyOnLoad(this.gameObject);
	}
	
	public void GoMailScene() {
        Application.LoadLevel("MailScene");
    }

    public void GoMaintenanceScene() {
        Application.LoadLevel("MaintenanceScene");
    }

    public void GoStageScene() {
        Application.LoadLevel("StageScene");
    }


    #region HTTP




    #endregion
}
