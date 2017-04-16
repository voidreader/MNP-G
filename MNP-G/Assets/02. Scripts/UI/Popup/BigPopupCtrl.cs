using UnityEngine;
using System.Collections;

public class BigPopupCtrl : MonoBehaviour {






    [SerializeField] UIPanel _mailBoxScrollView; // 메일 스크롤뷰
    [SerializeField] GameObject _optionGroup;
    [SerializeField] GameObject _rankCtrl;
    


    
    [SerializeField] GameObject _mailBG;
    [SerializeField] GameObject _optionBG;
    [SerializeField] GameObject _missionBG;

    [SerializeField] MissionController _missionControl;


    private void OffBGs() {
        
        _mailBG.SetActive(false);
        _optionBG.SetActive(false);
    }


    /// <summary>
    /// 랭킹창 오픈 
    /// </summary>
    public void SetRanking() {

        Debug.Log("SetRanking");
        InitPopup();


        _rankCtrl.GetComponent<RankingCtrl>().InitRanking();

        //OnCompleteOpen 에서 LobbyCtrl.Instance.RequestRanking(); 호출 

    }

    /// <summary>
    /// 메일함 팝업 오픈 
    /// </summary>
    public void SetMailBox() {

        InitPopup();

        _mailBG.SetActive(true);
    }

    /// <summary>
    /// 옵션창 오픈 
    /// </summary>
    public void SetOption() {
        InitPopup();

        _optionBG.SetActive(true);
    }

    public void SetMission() {
        InitPopup();

         _missionBG.SetActive(true);
        //_missionControl.SetTabButton();
        _missionControl.InitMissionController();
    }

    /// <summary>
    /// 완전히 열린 후 실행 
    /// </summary>
    public void OnCompleteOpen() {

        Debug.Log("OnCompleteOpen Big Popup");

        if (_mailBG.activeSelf) {
            LobbyCtrl.Instance.RequestMailBoxData();
            _mailBoxScrollView.gameObject.GetComponent<UIScrollView>().ResetPosition();
            _mailBoxScrollView.clipOffset = Vector2.zero;
            _mailBoxScrollView.transform.localPosition = new Vector3(0, 295, 0);
        } else if (_optionBG.activeSelf) {
            _optionGroup.SetActive(true);

        } else if (_rankCtrl.activeSelf) {
            LobbyCtrl.Instance.RequestRanking();
        }
        else if (_missionBG.activeSelf) {
            LobbyCtrl.Instance.SpawnMissions();
            
        }
    }

    /// <summary>
    /// 창이 닫힌 후 실행
    /// </summary>
    public void OnCompleteClose() {

    }

    /// <summary>
    /// 창이 닫히기 전에 실행
    /// </summary>
    public void OnClosing() {
        if (_mailBG.activeSelf) {
            LobbyCtrl.Instance.CloseMailBox();
        }
        else if (_optionBG.activeSelf) {
            _optionGroup.SetActive(false);
        }
        else if (_rankCtrl.activeSelf) {
            LobbyCtrl.Instance.CloseRanking();
        }
        else if (_missionBG.activeSelf) {
            DespawnMission();
            LobbyCtrl.Instance.UpdateMissionNew();
        }
        
    }




    /// <summary>
    /// 팝업 초기화
    /// </summary>
    private void InitPopup() {

        _optionGroup.SetActive(false);
        //_lblBottmText.gameObject.SetActive(false);
        _rankCtrl.SetActive(false);
        //_wantedObjs.SetActive(false);

        _missionBG.SetActive(false);

        OffBGs();
    }


    

    private void DespawnMission() {
        _missionControl.DespawnAll();
    }


}
