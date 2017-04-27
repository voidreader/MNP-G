using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttendanceCtrl : MonoBehaviour {

	[SerializeField] int _totalday = 0;
    [SerializeField] int _eventTotalday = 0;
    [SerializeField] acCtrl[] _arrColumns;

    [SerializeField]
    EventAttendanceCtrl _eventAttendance;

    [SerializeField] GameObject _generalAttendance;

    

    /// <summary>
    /// 출석체크 창 초기화 
    /// </summary>
    public void InitAttendance() {

        _totalday = GameSystem.Instance.AttendanceJSON["data"]["totalday"].AsInt;
        _eventTotalday = GameSystem.Instance.AttendanceJSON["data"]["eventtotalday"].AsInt;

        _generalAttendance.SetActive(true);

        Debug.Log(">>> InitAttendance :: " + GameSystem.Instance.AttendanceInitJSON[0]["type"].Value + " / " + GameSystem.Instance.AttendanceInitJSON[0]["value"].AsInt);

        for(int i=0; i<GameSystem.Instance.AttendanceInitJSON.Count; i++) {

            if(i+1 < _totalday) {
                _arrColumns[i].SetAttendanceColumn(i + 1, GameSystem.Instance.AttendanceInitJSON[i]["value"].AsInt, GameSystem.Instance.AttendanceInitJSON[i]["type"].Value, true);
            } 
            else {
                _arrColumns[i].SetAttendanceColumn(i + 1, GameSystem.Instance.AttendanceInitJSON[i]["value"].AsInt, GameSystem.Instance.AttendanceInitJSON[i]["type"].Value, false);
            }

            if(i+1 == _totalday) {
                // 연출 
                _arrColumns[i].SetToday();
            }
        }
	}

    /// <summary>
    /// 
    /// </summary>
    public void OpenAttendanceCheck() {

        this.gameObject.SetActive(true);

        _totalday = GameSystem.Instance.AttendanceJSON["data"]["totalday"].AsInt;
        _eventTotalday = GameSystem.Instance.AttendanceJSON["data"]["eventtotalday"].AsInt;

        _eventAttendance.gameObject.SetActive(false);
        _generalAttendance.SetActive(true);

        //Debug.Log(">>> InitAttendance :: " + GameSystem.Instance.AttendanceInitJSON[0]["type"].Value + " / " + GameSystem.Instance.AttendanceInitJSON[0]["value"].AsInt);

        for (int i = 0; i < GameSystem.Instance.AttendanceInitJSON.Count; i++) {
            if (i < _totalday) {
                _arrColumns[i].SetAttendanceColumn(i + 1, GameSystem.Instance.AttendanceInitJSON[i]["value"].AsInt, GameSystem.Instance.AttendanceInitJSON[i]["type"].Value, true);
            }
            else {
                _arrColumns[i].SetAttendanceColumn(i + 1, GameSystem.Instance.AttendanceInitJSON[i]["value"].AsInt, GameSystem.Instance.AttendanceInitJSON[i]["type"].Value, false);
            }
        }

    }

    /// <summary>
    /// 이벤트 출석체크  창 열기 (단순 열기)
    /// </summary>
    public void OpenEventAttendanceSimple() {
        _generalAttendance.SetActive(false);
        _eventAttendance.OpenEventAttendance();
    }


    /// <summary>
    /// 출석체크 종료 
    /// </summary>
    public void CloseGeneralAttendance() {

        // 일반 출첵 종료 
        _generalAttendance.SendMessage("CloseSelf");

        if(LobbyCtrl.Instance._objOptionGroup.gameObject.activeSelf) {
            this.gameObject.SetActive(false);
            return;
        }

        // 이벤트 출첵 체크 
        if (_eventTotalday > 0 && _eventTotalday <= 7) {
            _eventAttendance.InitEventAttendance();
            return;
        }

        
        LobbyCtrl.Instance.SendMessage("CheckNotice");
        this.gameObject.SetActive(false);

    }

    /// <summary>
    /// 이벤트 출석체크 종료 
    /// </summary>
    public void CloseEventAttendance() {
        _eventAttendance.SendMessage("CloseSelf");

        if(!GameSystem.Instance.IsEnterLobbyCompleted) {
            LobbyCtrl.Instance.SendMessage("CheckNotice");
        }

        this.gameObject.SetActive(false);
    }
	

}
