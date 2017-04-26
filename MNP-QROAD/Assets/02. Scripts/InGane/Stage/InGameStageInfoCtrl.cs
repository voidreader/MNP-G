using UnityEngine;
using System.Collections;
using DG.Tweening;
using SimpleJSON;

public class InGameStageInfoCtrl : MonoBehaviour {


    [SerializeField]
    StageMissionColCtrl[] _arrMissionInfos;

    

    [SerializeField]
    Transform _window;

    JSONNode stageNode;
    JSONNode userStageNode;

    


    public void EnableButton() {
        this.GetComponent<UIButton>().enabled = true;
    }

    public void OffMissionInfo() {

        _window.DOScale(0, 0.3f).SetEase(Ease.InBack).OnComplete(OnCompleteOffMission);

        InSoundManager.Instance.PlayStageMissionClose();

    }

    private void OnCompleteOffMission() {
        this.gameObject.SetActive(false);
    }

    /// <summary>
    /// 인게임 시작시점에 미션 정보 호출 
    /// 특수 미션 설정값을 할당한다.
    /// </summary>
    /// <param name="pStage"></param>
    public void SetIngameMissionInfo(int pStage) {


        Debug.Log("☆★ SetIngameMissionInfo :: " + pStage);

        // JSON NODE 가져오기
        for (int i = 0; i < GameSystem.Instance.StageDetailJSON.Count; i++) {

            if (GameSystem.Instance.StageDetailJSON[i]["stageid"].AsInt == pStage) {
                stageNode = GameSystem.Instance.StageDetailJSON[i];
                break;
            }
        }

        // JSON NODE 가져오기
        for (int i = 0; i < GameSystem.Instance.UserStageJSON["stagelist"].Count; i++) {

            if (GameSystem.Instance.UserStageJSON["stagelist"][i]["stageid"].AsInt == pStage) {
                userStageNode = GameSystem.Instance.UserStageJSON["stagelist"][i];
                break;
            }
        }

        //Debug.Log("☆★ SetIngameMissionInfo  stageNode:: " + stageNode.ToString());
        //Debug.Log("☆★ SetIngameMissionInfo  userStageNode:: " + userStageNode.ToString());



        #region StageMissionCount 몇개? 
        if (stageNode["questid1"].AsInt > 0) {
            GameSystem.Instance.InGameStageMissionCount++;

        }

        if (stageNode["questid2"].AsInt > 0) {
            GameSystem.Instance.InGameStageMissionCount++;
        }

        if (stageNode["questid3"].AsInt > 0) {
            GameSystem.Instance.InGameStageMissionCount++;
        }

        if (stageNode["questid4"].AsInt > 0) {
            GameSystem.Instance.InGameStageMissionCount++;
        }

        #endregion


        #region 특수 미션 체크 
        // 쿠키
        if(stageNode["questid1"].AsInt == 12 || stageNode["questid2"].AsInt == 12 || stageNode["questid3"].AsInt == 12 || stageNode["questid4"].AsInt == 12) {
            InGameCtrl.Instance.IsCookieMission = true;
        }

        // 바위
        if (stageNode["questid1"].AsInt == 15 || stageNode["questid2"].AsInt == 15 || stageNode["questid3"].AsInt == 15 || stageNode["questid4"].AsInt == 15) {
            InGameCtrl.Instance.IsStoneMission = true;
        }

        // 보스
        if (stageNode["questid1"].AsInt == 11 || stageNode["questid2"].AsInt == 11 || stageNode["questid3"].AsInt == 11 || stageNode["questid4"].AsInt == 11) {
            InGameCtrl.Instance.IsBossStage = true;
        }

        // 구출
        if (stageNode["questid1"].AsInt == 10 || stageNode["questid2"].AsInt == 10 || stageNode["questid3"].AsInt == 10 || stageNode["questid4"].AsInt == 10) {
            InGameCtrl.Instance.IsRescueStage = true;
        }

        // 생선튀김
        if (stageNode["questid1"].AsInt == 22 || stageNode["questid2"].AsInt == 22 || stageNode["questid3"].AsInt == 22 || stageNode["questid4"].AsInt == 22) {
            InGameCtrl.Instance.IsFishMission = true;
        }

        // 이동미션
        if (stageNode["questid1"].AsInt == 23 || stageNode["questid2"].AsInt == 23 || stageNode["questid3"].AsInt == 23 || stageNode["questid4"].AsInt == 23) {
            InGameCtrl.Instance.IsMoveMission = true;
        }

        #endregion

        // 이미 다이어 레벨 클리어한 스테이지의 경우는 미션창을 띄우지 않음 
        if (userStageNode["state"].AsInt >= 4) {
            return;
        }



        this.gameObject.SetActive(true);
        this.GetComponent<UIButton>().enabled = false;

        // 사운드
        InSoundManager.Instance.PlayStageMissionPopUp();

        _window.localScale = Vector3.zero;
        _window.DOScale(1, 0.5f).SetEase(Ease.OutBack);




        for (int i = 0; i < _arrMissionInfos.Length; i++) {
            _arrMissionInfos[i].gameObject.SetActive(false);
        }




        // 위치 조정 
        if (GameSystem.Instance.InGameStageMissionCount == 1) {
            _arrMissionInfos[0].transform.localPosition = new Vector3(-210, -45, 0);
        }
        else if (GameSystem.Instance.InGameStageMissionCount == 2) {
            _arrMissionInfos[0].transform.localPosition = new Vector3(-210, 20, 0);
            _arrMissionInfos[1].transform.localPosition = new Vector3(-210, -120, 0);
        }
        else if (GameSystem.Instance.InGameStageMissionCount == 3) {
            _arrMissionInfos[0].transform.localPosition = new Vector3(-210, 50, 0);
            _arrMissionInfos[1].transform.localPosition = new Vector3(-210, -50, 0);
            _arrMissionInfos[2].transform.localPosition = new Vector3(-210, -150, 0);
        }
        else if (GameSystem.Instance.InGameStageMissionCount == 4) {
            _arrMissionInfos[0].transform.localPosition = new Vector3(-210, 80, 0);
            _arrMissionInfos[1].transform.localPosition = new Vector3(-210, -10, 0);
            _arrMissionInfos[2].transform.localPosition = new Vector3(-210, -100, 0);
            _arrMissionInfos[3].transform.localPosition = new Vector3(-210, -190, 0);
        }


        StartCoroutine(PlacingStageMissionCols());

    }

    IEnumerator PlacingStageMissionCols() {
        if (stageNode["questid1"].AsInt > 0) {
            _arrMissionInfos[0].SetMissionDetailInfo(stageNode["questid1"].AsInt, stageNode["questvalue1"].AsInt, userStageNode["progress1"].AsInt);
            InSoundManager.Instance.PlayStageMissionCol();
            yield return new WaitForSeconds(0.2f);
        }

        if (stageNode["questid2"].AsInt > 0) {
            _arrMissionInfos[1].SetMissionDetailInfo(stageNode["questid2"].AsInt, stageNode["questvalue2"].AsInt, userStageNode["progress2"].AsInt);
            InSoundManager.Instance.PlayStageMissionCol();
            yield return new WaitForSeconds(0.2f);
        }

        if (stageNode["questid3"].AsInt > 0) {
            _arrMissionInfos[2].SetMissionDetailInfo(stageNode["questid3"].AsInt, stageNode["questvalue3"].AsInt, userStageNode["progress3"].AsInt);
            InSoundManager.Instance.PlayStageMissionCol();
            yield return new WaitForSeconds(0.2f);
        }

        if (stageNode["questid4"].AsInt > 0) {
            _arrMissionInfos[3].SetMissionDetailInfo(stageNode["questid4"].AsInt, stageNode["questvalue4"].AsInt, userStageNode["progress4"].AsInt);
            InSoundManager.Instance.PlayStageMissionCol();
            yield return new WaitForSeconds(0.2f);
        }
    }

}
