using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class InGameMissionIconCtrl : MonoBehaviour {
    [SerializeField] UISprite _icon;
    [SerializeField] UILabel _lblValue;
    [SerializeField] int _goalCount;

    [SerializeField] UIFont _redFont;
    [SerializeField] UIFont _greenFont;

    [SerializeField] NGUIPlayEffectCtrl _clearEffect;

    [SerializeField] SpecialMissionType _missionType;
    int _index = -1;
    bool _isClear = false;
    bool _isFixedNumber = false;

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

        if (!_isFixedNumber)
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

        if(!_isFixedNumber)
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
                _icon.spriteName = "mission-bronz";
                break;

            case StageClearType.silver:
                _icon.spriteName = "mission-silver";
                break;

            case StageClearType.gold:
                _icon.spriteName = "mission-gold";
                break;
        }

        // 사운드 재생 
        InSoundManager.Instance.PlayInGameStageClear();


        // 이펙트 플레이
        _clearEffect.PlayPos(NGUIEffectType.InGameWhiteLight, this.transform.position);
        _icon.transform.DOLocalJump(this.transform.localPosition, 30, 1, 0.5f);



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
            _isFixedNumber = true;
        }
        else {
            _lblValue.bitmapFont = _redFont;
            _isFixedNumber = false;
        }

        switch(pType) {
            case SpecialMissionType.block:
                _icon.spriteName = "ico-block";
                break;
            case SpecialMissionType.cookie:
                _icon.spriteName = "ico-kuki";
                break;
            case SpecialMissionType.stone:
                _icon.spriteName = "ico-stone";
                break;
            case SpecialMissionType.grill:
                _icon.spriteName = "ico-fish";
                break;

            case SpecialMissionType.move:
                _icon.spriteName = "ico-neko-home2";
                break;

            case SpecialMissionType.match3:
                _icon.spriteName = "ico-block-3";
                break;
            case SpecialMissionType.match4:
                _icon.spriteName = "ico-block-4";
                break;
            case SpecialMissionType.great:
                _icon.spriteName = "ico-block-great";
                break;
            case SpecialMissionType.perfect:
                _icon.spriteName = "ico-block-perfect";
                break;
            case SpecialMissionType.bomb:
                _icon.spriteName = "ico-bomb-block";
                break;

            case SpecialMissionType.coin:
                _icon.spriteName = "ico-coin";
                break;
            case SpecialMissionType.combo:
                _icon.spriteName = "ico-combo";
                break;
            case SpecialMissionType.score:
                _icon.spriteName = "ico-score";
                break;

            case SpecialMissionType.specialAttack:
                _icon.spriteName = "ico-special-skill";
                break;

            case SpecialMissionType.basic:
                _icon.spriteName = "ico-stage";
                break;

            default:
                _icon.spriteName = "010-i-base-top";
                break;

        }
    }
}
