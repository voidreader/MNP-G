using UnityEngine;
using System.Collections;
using SimpleJSON;
using PathologicalGames;
using DG.Tweening;
using Google2u;



public class MissionColumnCtrl : MonoBehaviour {

    [SerializeField] MissionType _missionType; // 미셔타입
    [SerializeField] MissionState _missionState; // 상태 

    [SerializeField] UIButton _btnComplete;
    [SerializeField] UIProgressBar _progress;
    [SerializeField] UISprite _markComplete;

    [SerializeField] UILabel _lblContent;

    [SerializeField] UILabel _lblProgress;

    [SerializeField] UILabel _lblMissionOrder;

    [SerializeField]
    string _debugNode;

    [SerializeField] string _missionID;
    [SerializeField] int _tid;
    [SerializeField] string _contentID;

    [SerializeField] UILabel _lblRewardValue;
    [SerializeField] UISprite _rewardIcon;
    

    [SerializeField] float _barValue;

    [SerializeField] UISprite _base;
    [SerializeField] UISprite _cap;

    JSONNode _node;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="pNode"></param>
    public void SetMissionInfo(JSONNode pNode, MissionType pType) {

        _node = pNode;
        _debugNode = pNode.ToString();
        _missionType = pType;

        _tid = pNode["tid"].AsInt;

        if (pType == MissionType.Day)
            _missionID = "day" + pNode["tid"].Value;
        else if(pType == MissionType.Week)
            _missionID = "week" + pNode["tid"].Value;

        _lblMissionOrder.text = pNode["no"].AsInt.ToString();



        //_titleID = GameSystem.Instance.DocsMission.get<string>(_missionID, "title");
        //_contentID = GameSystem.Instance.DocsMission.get<string>(_missionID, "content");
        _contentID = MNP_Mission.Instance.GetRow(_missionID)._content;

        _lblContent.text  = GameSystem.Instance.GetLocalizeText(_contentID);

        _lblContent.text = _lblContent.text.Replace("[n]", GameSystem.Instance.GetNumberToString(pNode["missionCount"].AsInt));

        // reward 설정
		if (pNode ["rewardType"].Value == "gem")
			_rewardIcon.spriteName = "i-zam2";
		else if (pNode ["rewardType"].Value == "coin")
			_rewardIcon.spriteName = "main_ico_coin";
		else if (pNode ["rewardType"].Value == "chub")
			_rewardIcon.spriteName = "i-k";
		else if (pNode ["rewardType"].Value == "tuna")
			_rewardIcon.spriteName = "i-t";
		else if (pNode ["rewardType"].Value == "salmon")
			_rewardIcon.spriteName = "i-ss";
		else if (pNode ["rewardType"].Value == "freeticket")
			_rewardIcon.spriteName = PuzzleConstBox.spriteUIFreeTicket;
		else if (pNode ["rewardType"].Value == "rareticket")
			_rewardIcon.spriteName = PuzzleConstBox.spriteUIRareTicket;
		else if (pNode ["rewardType"].Value == "rainbowticket")
			_rewardIcon.spriteName = PuzzleConstBox.spriteUIRainbowTicket;


        
        _lblRewardValue.text = "x" + GameSystem.Instance.GetNumberToString(pNode["reward"].AsInt);

        if(_tid == 11) {

            pNode["current"].AsInt = GameSystem.Instance.GetCompletedMissionCount(pType);
            pNode["missionCount"].AsInt = GameSystem.Instance.GetMissionCount(pType);


            Debug.Log("★ Final Mission :: " + pNode["current"] + "/" + pNode["missionCount"]);
            if(pNode["progress"].AsInt < 2) {
                if (pNode["current"].AsInt >= pNode["missionCount"].AsInt) {
                    pNode["progress"].AsInt = 1;
                }
                else {
                    pNode["progress"].AsInt = 0;
                }
            }


            if (pType == MissionType.Day)
                LobbyCtrl.Instance.FinalDailyMission = this;
            else
                LobbyCtrl.Instance.FinalWeeklyMission = this;

        }

        _lblProgress.text = GameSystem.Instance.GetNumberToString(pNode["current"].AsInt) + " / " + GameSystem.Instance.GetNumberToString(pNode["missionCount"].AsInt);
       

        // 프로그레스바 설정
        _barValue = pNode["current"].AsFloat / pNode["missionCount"].AsFloat;
        _progress.value = _barValue;



        if (pNode["progress"].AsInt == 0)
            SetMissionState(MissionState.NotComplete);
        else if (pNode["progress"].AsInt == 1)
            SetMissionState(MissionState.WaitingComplete);
        else if (pNode["progress"].AsInt == 2)
            SetMissionState(MissionState.Complete);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="pType"></param>
    public void RefreshFinalMission(MissionType pType) {
        if (_tid != 11)
            return;

        _node["current"].AsInt = GameSystem.Instance.GetCompletedMissionCount(pType);
        _node["missionCount"].AsInt = GameSystem.Instance.GetMissionCount(pType);

        _lblProgress.text = GameSystem.Instance.GetNumberToString(_node["current"].AsInt) + " / " + GameSystem.Instance.GetNumberToString(_node["missionCount"].AsInt);
        _barValue = _node["current"].AsFloat / _node["missionCount"].AsFloat;
        _progress.value = _barValue;


        if(_node["current"].AsInt >= _node["missionCount"].AsInt && _node["progress"].AsInt == 0) {
            _node["progress"].AsInt = 1;
        }


        if (_node["progress"].AsInt == 0)
            SetMissionState(MissionState.NotComplete);
        else if (_node["progress"].AsInt == 1)
            SetMissionState(MissionState.WaitingComplete);
        else if (_node["progress"].AsInt == 2)
            SetMissionState(MissionState.Complete);

    }


    /// <summary>
    /// 미션 상태 지정 
    /// </summary>
    /// <param name="pstate"></param>
    public void SetMissionState(MissionState pstate) {
        _missionState = pstate;

        if(_missionState == MissionState.NotComplete) {
            _lblRewardValue.gameObject.SetActive(true);
            _rewardIcon.gameObject.SetActive(true);
            _lblRewardValue.gameObject.SetActive(true);

            _progress.gameObject.SetActive(true);
            _markComplete.gameObject.SetActive(false);
            _btnComplete.gameObject.SetActive(false);
            _cap.gameObject.SetActive(false);
         
        }
        else if(_missionState == MissionState.WaitingComplete) {

            _lblRewardValue.gameObject.SetActive(false);
            _rewardIcon.gameObject.SetActive(true);
            _lblRewardValue.gameObject.SetActive(true);

            _progress.gameObject.SetActive(false);
            _markComplete.gameObject.SetActive(false);
            _btnComplete.gameObject.SetActive(true);

            
            _cap.gameObject.SetActive(false);
         
        }
        else {
            _progress.gameObject.SetActive(false);

            _markComplete.gameObject.SetActive(true);
            _markComplete.transform.localScale = GameSystem.Instance.BaseScale;

            _btnComplete.gameObject.SetActive(true);

            
            _rewardIcon.gameObject.SetActive(true);
            _lblRewardValue.gameObject.SetActive(true);

            _cap.gameObject.SetActive(true);
            
        }
    }



    /// <summary>
    /// 완료버튼 클릭 
    /// </summary>
    public void OnClickComplete() {

        // Http Post
        GameSystem.Instance.Post2MissionReward(_missionType, _tid, this);
    }



    /// <summary>
    /// 통신 완료 후 미션 완료 처리 
    /// </summary>
    public void SetCompleteMission() {
        // 미션 완료 처리 
        GameSystem.Instance.SetCompleteMission(_missionType, _tid);

        // 컨트롤 invisible 
        /*
        _progress.gameObject.SetActive(false);
        _rewardIcon.gameObject.SetActive(false);
        _lblRewardValue.gameObject.SetActive(false);
        _btnComplete.gameObject.SetActive(false);
        */


        //_cap.gameObject.SetActive(true);
        //_base.spriteName = "base-lines-full";

        // 효과
        /*
        _markComplete.gameObject.SetActive(true);
        _markComplete.transform.localScale = new Vector3(2, 2, 1);
        _markComplete.transform.DOScale(1, 0.5f);
        */

        LobbyCtrl.Instance.PlayMissionClearDircet(_node["rewardType"].Value, _node["reward"].AsInt);


        Invoke("InvokedCompleteDirect", 2);
    }


    void InvokedCompleteDirect() {

        _cap.gameObject.SetActive(true);

        _markComplete.gameObject.SetActive(true);
        _markComplete.transform.localScale = new Vector3(2, 2, 1);
        _markComplete.transform.DOScale(1, 0.5f);

        // throwing icon 처리 
        //_throwingRewardIcon.transform.DOMove(LobbyCtrl.Instance.pos)


        

        if(_node["rewardType"].Value == "coin" ) {
            LobbyCtrl.Instance.ThrowIcon("top-coin", _rewardIcon.transform.position);
        }
        else {
            LobbyCtrl.Instance.ThrowIcon("top-dia", _rewardIcon.transform.position);
        }
    }

    void OnCompleteThrowTopCoin() {
        LobbyCtrl.Instance.SendMessage("PlayTopCoinColorfulLight");
        
    }

    void OnCompleteThrowGemCoin() {
        LobbyCtrl.Instance.SendMessage("PlayTopGemColorfulLight");

        
    }

    void OnSpawned() {
        this.transform.localScale = GameSystem.Instance.BaseScale;
    }
    
}
