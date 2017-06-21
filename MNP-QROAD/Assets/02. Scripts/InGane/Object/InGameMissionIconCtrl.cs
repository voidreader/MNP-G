using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMissionIconCtrl : MonoBehaviour {
    [SerializeField] UISprite _icon;
    [SerializeField] UILabel _lblValue;
    [SerializeField] int _goalCount;

    [SerializeField] UIFont _redFont;
    [SerializeField] UIFont _greenFont;


    [SerializeField] SpecialMissionType _missionType;
    int _index = -1;
    bool _isClear = false;

    public SpecialMissionType MissionType {
        get {
            return _missionType;
        }

        set {
            _missionType = value;
        }
    }

    public bool IsClear {
        get {
            return _isClear;
        }

        set {
            _isClear = value;
        }
    }


    /// <summary>
    /// 목표치 감소 
    /// </summary>
    public void MinusCount() {

        if (_goalCount <= 0)
            return;

        if (IsClear)
            return;

        _goalCount--;
        _lblValue.text = GameSystem.Instance.GetNumberToString(_goalCount);

        // Clear 처리 
        if (_goalCount <= 0) {
            IsClear = true;
            InUICtrl.Instance.ClearMissionIcon(_index);
        }

    }

    public void MinusCount(int pCount) {
        if (_goalCount <= 0)
            return;

        if (IsClear)
            return;

        _goalCount -= pCount;
        _lblValue.text = GameSystem.Instance.GetNumberToString(_goalCount);


        // Clear 처리 
        if (_goalCount <= 0) {
            IsClear = true;
            InUICtrl.Instance.ClearMissionIcon(_index);
        }

    }

    public void CompareGoalCount(int pCount) {

        if (IsClear)
            return;

        if (pCount >= _goalCount) {
            IsClear = true;
            InUICtrl.Instance.ClearMissionIcon(_index);
        }
    }



    /// <summary>
    /// 미션 클리어 처리 
    /// </summary>
    /// <param name="pType"></param>
    public void ClearMission(StageClearType pType) {
        _lblValue.gameObject.SetActive(false);

        switch(pType) {
            case StageClearType.bronze:
                _icon.spriteName = "stage-clear-wing-bronz";
                break;

            case StageClearType.silver:
                _icon.spriteName = "stage-clear-wing-silver";
                break;

            case StageClearType.gold:
                _icon.spriteName = "stage-clear-wing";
                break;
        }

        // 사운드 재생 
        InSoundManager.Instance.PlayInGameStageClear();
        
    }

    /// <summary>
    /// 인게임 미션 정보 세팅 
    /// </summary>
    /// <param name="pType"></param>
    /// <param name="pCount"></param>
    public void SetInGameMissionIcon(SpecialMissionType pType, int pCount, int pIndex) {

        _isClear = false;
        _index = pIndex;

        _goalCount = pCount;
        MissionType = pType;

        _lblValue.gameObject.SetActive(true);
        _lblValue.text = GameSystem.Instance.GetNumberToString(_goalCount);

        if (pType == SpecialMissionType.basic) {
            _goalCount = 1;
            _lblValue.text = _goalCount.ToString();
        }

        // 스코어는 레이블 감춘다. 
        if(pType == SpecialMissionType.score || pType == SpecialMissionType.combo) { // 고정레이블은 그린폰트를 준다. 
            _lblValue.bitmapFont = _greenFont;
        }
        else {
            _lblValue.bitmapFont = _redFont;
        }

        switch(pType) {
            case SpecialMissionType.block:
                _icon.spriteName = "blocks";
                break;
            case SpecialMissionType.cookie:
                _icon.spriteName = "003-ck-tile-standard";
                break;
            case SpecialMissionType.stone:
                _icon.spriteName = "004-stone-block-1";
                break;
            case SpecialMissionType.grill:
                _icon.spriteName = "fish-c-clear";
                break;

            case SpecialMissionType.move:
                _icon.spriteName = "colorful-top";
                break;

            default:
                _icon.spriteName = "010-i-base-top";
                break;

        }
    }
}
