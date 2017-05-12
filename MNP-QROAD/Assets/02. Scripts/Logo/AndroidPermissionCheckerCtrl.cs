using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AndroidPermissionCheckerCtrl : MonoBehaviour {

    bool _isChecked = false;

    [SerializeField] UIButton _btnCheck;
    [SerializeField] GameObject _lblErr;

    bool _isGranted = false;

    public static event Action OnCompleteGrant = delegate { };


    // Use this for initialization
    void Start () {
		
	}

    public void OnCheck() {
        if (_btnCheck.normalSprite == "c-none") { // 체크되지 않은 상태에서  체크로 변경 
            _btnCheck.normalSprite = "c-in";
            _isChecked = true;
        }
        else {
            _btnCheck.normalSprite = "c-none";
            _isChecked = false;
        }
    }


    /// <summary>
    /// 권한 요청 체커 오픈 
    /// </summary>
    public void OpenChecker(Action pComplete) {
        this.gameObject.SetActive(true);

        OnCompleteGrant = delegate { };
        OnCompleteGrant += pComplete;

        _btnCheck.normalSprite = "c-none";
        _isChecked = false;
        _isGranted = false;
        _lblErr.SetActive(false);
    }


    /// <summary>
    /// 권한 요청 
    /// </summary>
    public void RequestPermission() {

        if(!_isChecked) { // 체크되지 않은 경우. 
            _lblErr.SetActive(true);
            return;
        }
        else {
            _lblErr.SetActive(false);
        }

        Debug.Log("★★ RequestPermission ");

        // 퍼미션 요청 
        PermissionsManager.ActionPermissionsRequestCompleted += PermissionsManager_ActionPermissionsRequestCompleted;
        PermissionsManager.Instance.RequestPermissions(AN_Permission.WRITE_EXTERNAL_STORAGE, AN_Permission.READ_EXTERNAL_STORAGE);

    }

    private void PermissionsManager_ActionPermissionsRequestCompleted(AN_GrantPermissionsResult obj) {

        Debug.Log("★★ PermissionsManager_ActionPermissionsRequestCompleted Start ");

        PermissionsManager.ActionPermissionsRequestCompleted -= PermissionsManager_ActionPermissionsRequestCompleted;

        _isGranted = true; // 권한을 받았다고 치고, 체크 

        foreach (KeyValuePair<AN_Permission, AN_PermissionState> pair in obj.RequestedPermissionsState) {
            Debug.Log(pair.Key.ToString() + " / " + pair.Value.ToString());

            if(pair.Value == AN_PermissionState.PERMISSION_DENIED) {
                _isGranted = false;
            }
        }


        // 종료 하고 Action 실행 
        if (_isGranted) {

            Debug.Log("★★ PermissionsManager_ActionPermissionsRequestCompleted Granted!!!! ");

            OnCompleteGrant();
            
            this.gameObject.SetActive(false); 
        }
        else {
            _lblErr.SetActive(true);
        }
        
    } 
}
