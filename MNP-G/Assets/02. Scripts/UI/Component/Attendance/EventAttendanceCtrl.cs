using UnityEngine;
using System.Collections;

public class EventAttendanceCtrl : MonoBehaviour {

    [SerializeField] int _eventTotalDay = 0;
	[SerializeField] EventAcCtrl[] _acColumns;


    public void InitEventAttendance() {

        this.gameObject.SetActive(true);

        _eventTotalDay = GameSystem.Instance.AttendanceJSON["data"]["eventtotalday"].AsInt;

        for(int i=0; i< _eventTotalDay-1; i++) {
            _acColumns[i].SetCover();
        }

        // 오늘자 달성 처리 
        _acColumns[_eventTotalDay - 1].SetToday();
    }


    // 단순 오픈 
    public void OpenEventAttendance() {

        this.gameObject.SetActive(true);
        _eventTotalDay = GameSystem.Instance.AttendanceJSON["data"]["eventtotalday"].AsInt;

        if(_eventTotalDay >= _acColumns.Length) {
            for (int i = 0; i < _acColumns.Length; i++) {
                _acColumns[i].SetCover();
            }
            return;
        }

        for (int i = 0; i < _eventTotalDay; i++) {
            _acColumns[i].SetCover();
        }
    }
}
