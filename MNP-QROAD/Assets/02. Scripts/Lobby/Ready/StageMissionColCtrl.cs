using UnityEngine;
using System.Collections;
using Google2u;
using DG.Tweening;

public class StageMissionColCtrl : MonoBehaviour {

    [SerializeField]
    UILabel _lblMission;

    [SerializeField]
    int _current;


    [SerializeField]
    UIProgressBar _progressBar;

    [SerializeField]
    UILabel _lblProgress;

    [SerializeField]
    UISprite _spMission;

    [SerializeField]
    Transform _clear;

    [SerializeField]
    int _questID;
    [SerializeField]
    int _questValue;
    [SerializeField]
    float _currentBarValue;


    readonly string _bbcode = "[FFF221]";
    readonly string _bbcode2 = "[FFFFFF]";



    public void SetDisable() {
        this.gameObject.SetActive(false);
    }


    public void SetMissionInfo(int pMissionID, int pValue) {

        if (pMissionID <= 0) {
            SetDisable();
            return;
        }

        this.gameObject.SetActive(true);
        _lblMission.text = "● " + GameSystem.Instance.GetLocalizeText(GameSystem.Instance.GetStageMissionLocalID(pMissionID)).Replace("[n]", string.Format("{0:n0}", pValue));
        
    }


    /// <summary>
    /// 미션 정보 설정 
    /// </summary>
    /// <param name="pMissionType"></param>
    /// <param name="pValue"></param>
    public void SetMissionDetailInfo(int pMissionID, int pValue, int pCurrent) {
        this.gameObject.SetActive(true);

        _lblMission.text = GameSystem.Instance.GetLocalizeText(GameSystem.Instance.GetStageMissionLocalID(pMissionID)).Replace("[n]", string.Format("{0:n0}", pValue));

        // 12 (쿠키), 15 (바위) 미션에 대한 예외처리  
        if (pMissionID == 12) {
            SetCookieMissionInGameDetailInfo();
            return;
        }

        
        _lblProgress.text = string.Format("{0:n0}", pCurrent) + " / " + string.Format("{0:n0}", pValue);
        _progressBar.value = (float)pCurrent / (float)pValue;
    }




    /// <summary>
    /// 결과창의 미션 초기화 
    /// </summary>
    /// <param name="pMissionID"></param>
    /// <param name="pValue"></param>
    public void InitInResult(int pMissionID, int pQuestValue, int pCurrent) {

        _questID = pMissionID;

        if (_questID <= 0) {
            this.gameObject.SetActive(false);
            return;
        }

        this.gameObject.SetActive(true);
        _clear.gameObject.SetActive(false);

        _progressBar.value = 0;
        _spMission.spriteName = PuzzleConstBox.spriteStageMissionColor;

        // Text 설정
        _lblMission.text = _bbcode + GameSystem.Instance.GetLocalizeText(GameSystem.Instance.GetStageMissionLocalID(pMissionID)).Replace("[n]", string.Format("{0:n0}", pQuestValue)) + "[-]";

        // 미션 값 설정 
        _questValue = pQuestValue;
        _current = pCurrent;


        // 12 (쿠키), 15 (바위) 미션에 대한 예외처리  
        if (pMissionID == 12) {
            SetCookieMissionResultInit();
            return;
        }

        // 레이블 처리 
        _lblProgress.text = _bbcode + string.Format("{0:n0}", _current) + "/" + string.Format("{0:n0}", pQuestValue) + "[-]";
        _lblProgress.gameObject.SetActive(false);


    }

    /// <summary>
    ///  쿠키 미션 예외설정 
    /// </summary>
    void SetCookieMissionInGameDetailInfo() {

        // 강제적으로 current를 0으로, value는 액티브 블록 숫자로 변경한다. 
        _lblProgress.text = string.Format("{0:n0}", 0) + " / " + string.Format("{0:n0}", GameSystem.Instance.ResultValidBlockSpaceCount);
        _progressBar.value = 0;
    }

    /// <summary>
    /// 쿠키 미션 예외설정 (결과 화면 초기화)
    /// </summary>
    void SetCookieMissionResultInit() {
        _lblProgress.text = _bbcode + string.Format("{0:n0}", 0) + "/" + string.Format("{0:n0}", GameSystem.Instance.ResultValidBlockSpaceCount) + "[-]";
    }

    void SetCookieMissionResult() {

        // curret와 questvalue를 재설정 
        _current = GameSystem.Instance.ResultValidBlockSpaceCount - GameSystem.Instance.IngameRemainCookie; 
        _questValue = GameSystem.Instance.ResultValidBlockSpaceCount;
    }


    /// <summary>
    /// 결과창에서 미션에 대한 세팅 
    /// </summary>
    /// <param name="pMissionID"></param>
    /// <param name="pQuestValue"></param>
    /// <param name="pProgress"></param>
    public void SetStageMissionInResult(int pRealCurrent) {

        // 이번 퍼즐을 마치고 난 후의 진척도 세팅 
        _current = pRealCurrent;
        _currentBarValue = (float)_current / (float)_questValue;

        // 쿠키 미션 예외처리 
        if (_questID == 12)
            SetCookieMissionResult();

        if(_currentBarValue > 1) {
            _currentBarValue = 1;
        }

        StartCoroutine(Filling());
    }

    IEnumerator Filling() {

        float _divideValue = _currentBarValue / 20;

        if (_questValue > _current) { // 실패시에만 progress 정보를 먼저 보여준다.
            _lblProgress.gameObject.SetActive(true);
        }
        

        for(int i=0; i<20; i++) {
            _progressBar.value += _divideValue;
            yield return new WaitForSeconds(0.02f);
        }

        _progressBar.value = _currentBarValue;

        // 성공 , 실패 처리 
        if(_questValue <= _current) { // 성공

            _clear.transform.localScale = new Vector3(2, 2, 2);
            _clear.gameObject.SetActive(true);
            _clear.DOScale(1, 0.2f);
            _lblProgress.gameObject.SetActive(true);
            _lblProgress.text = _bbcode +  GameSystem.Instance.GetLocalizeText(MNP_Localize.rowIds.L3504) + "[-]";
        }
        else {
            SetFailInResult();
        }

    }

    private void SetFailInResult() {
        _spMission.spriteName = PuzzleConstBox.spriteStageMissionNoColor;
        _lblProgress.gameObject.SetActive(true);
        _lblProgress.text = _bbcode2 + GameSystem.Instance.GetLocalizeText(MNP_Localize.rowIds.L3505) + "[-]";
        _lblMission.text = _bbcode2 + GameSystem.Instance.GetLocalizeText(GameSystem.Instance.GetStageMissionLocalID(_questID)).Replace("[n]", string.Format("{0:n0}", _questValue)) + "[-]";
    }
}
