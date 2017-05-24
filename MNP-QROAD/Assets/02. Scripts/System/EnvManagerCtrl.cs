using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvManagerCtrl : MonoBehaviour {

    public static EnvManagerCtrl Instance = null;

    //  

    public string liveServerURL = string.Empty;
    public string testServerURL = string.Empty;


    void Awake() {
        if (Instance == null)
            Instance = this;

        DontDestroyOnLoad(this);
    }

    // Use this for initialization
    void Start () {


    }


}
