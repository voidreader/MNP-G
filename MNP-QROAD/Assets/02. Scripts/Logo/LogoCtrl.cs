using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Advertisements;
using IgaworksUnityAOS;


public class LogoCtrl : MonoBehaviour {

    [SerializeField]
    GameObject _logo;

    [SerializeField]
    GameObject _logo2;

    [SerializeField]
    GameObject _warning;

    [SerializeField]
    float _screenRatio;

    bool _waiting = false;

    void Awake() {

        /* 운영체제별 해상도 처리 */
        _screenRatio = (float)Screen.width / (float)Screen.height;
        //Debug.Log("▶▶▶▶ Resolution Width / Heigh , Ratio :: " + Screen.width + " / " + Screen.height + ", " + _screenRatio);


#if UNITY_IOS

        //iPad 일때는 해상도 조정을 하지 않음. 
        ISN_Device device = ISN_Device.CurrentDevice;
        if (!device.Model.Contains("iPad")) {
            Screen.SetResolution(Screen.height / 16 * 9, Screen.height, true);
        }

#else

        //안드로이드 해상도 처리 


        // 3:4 비율은 SetResolution을 사용하지 않고 좌우 검은 Letterbox 처리 
        if (_screenRatio > 0.7f) {

            //Debug.Log("▶▶▶ Android 3:4 ");

            return;
        } else {

            //Debug.Log("▶▶▶ Android SetResolution ");

            if (Screen.height / Screen.width == 2) {
                Screen.SetResolution(Screen.width, Screen.width / 9 * 16, true);
            }
            else {
                Screen.SetResolution(Screen.height / 16 * 9, Screen.height, true);
            }

            
            // Screen.SetResolution(Screen.width, Screen.width / 9 * 16, true);
            
        }

#endif


    }


    // Use this for initialization
    void Start () {


        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        _waiting = false;

        //ScissorCtrl.Instance.UpdateResolution();

        PuzzleConstBox.Initialize();

		//LoadLobby ();
		StartCoroutine (Waiting ());

        AdbrixManager.Instance.SendAdbrixNewUserFunnel(AdbrixManager.Instance.APP_EXCUTE);

	}



    IEnumerator Waiting() {
        Debug.Log(">>> Waiting Start");


#if UNITY_ANDROID



        /*
        GoogleCloudMessageService.Instance.Init();
        GoogleCloudMessageService.Instance.RgisterDevice();
        */

#elif UNITY_IOS
        ISN_RemoteNotificationsController.Instance.RegisterForRemoteNotifications((ISN_RemoteNotificationsRegistrationResult result) => {
            if (result.IsSucceeded) {
                Debug.Log("DeviceId: " + result.Token.DeviceId);
                WWWHelper.Instance.PushDeviceToken = result.Token.TokenString;
            }
            else {
                Debug.Log("Error: " + result.Error.Code + " / " + result.Error.Message);
            }
        });
        
        GameSystem.Instance.CancelAllLocalNotification();
        
#endif





        //Debug.Log(">>> Waiting Start #1");

        _logo2.SetActive(true);
        

        yield return new WaitForSeconds(1.8f);


        _logo2.SetActive(false);
        _logo.SetActive(true);

        yield return new WaitForSeconds(1.8f);

        _logo.SetActive(false);
        _warning.gameObject.SetActive(true);

        yield return new WaitForSeconds(2);


        //Debug.Log(">>> Waiting Start #2");

        LoadTitleScene();
    }


	private void LoadLobby() {
		GameSystem.Instance.LoadLobbyScene ();
	}

	private void LoadTitleScene() {
		GameSystem.Instance.LoadTitleScene ();
        
	}






}
