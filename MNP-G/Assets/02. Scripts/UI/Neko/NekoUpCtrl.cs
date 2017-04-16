using UnityEngine;
using System.Collections;

public class NekoUpCtrl : MonoBehaviour {

    PlayerOwnNekoCtrl _currentNeko;
    [SerializeField] UISprite _neko;

    // 네코 레벨업 변수 
    [SerializeField] GameObject _levelUpSet; 
    
    [SerializeField] UILabel _lblCurrentLevel;
    [SerializeField] UILabel _lblResultLevel;

    [SerializeField] UIButton _lblUpgrade;
    [SerializeField] UIButton _lblCancel;

    [SerializeField]
    UILabel _lblExplain;

    [SerializeField]
    int _nextPower;

    [SerializeField]
    int _nextlevel;

    

    [SerializeField]
    int _upgradePrice;

    

    

    bool _doingUpgrade = false; // 반복실행을 막는 변수

    public bool DoingUpgrade {
        get {
            return _doingUpgrade;
        }

        set {
            _doingUpgrade = value;
        }
    }

    public void SetNekoLevelUp(PlayerOwnNekoCtrl _pNeko, int pLevel) {

        this.gameObject.SetActive(true);

        DoingUpgrade = false;

        _currentNeko = _pNeko;
        _neko.atlas = _pNeko.NekoAtlas;
        _neko.spriteName = _pNeko.NekoSpriteName;

        _levelUpSet.SetActive(true);
        _lblUpgrade.gameObject.SetActive(true);
        _lblCancel.gameObject.SetActive(true);

        

        if (pLevel == 1) {
            _nextlevel = _pNeko._level + 1;
            _nextPower = _pNeko._power + 30;
        }
        else { // 레벨 10 증가 시키기. 
            // max 상한선을 체크해서 계산
            
            switch(_pNeko._grade) {
                case 1:

                    if(_pNeko._level + 10 > 30) {
                        _nextlevel = 30;
                    }
                    else {
                        _nextlevel = _pNeko._level + 10;
                    }

                    break;
                case 2:

                    if (_pNeko._level + 10 > 35) {
                        _nextlevel = 35;
                    }
                    else {
                        _nextlevel = _pNeko._level + 10;
                    }

                    break;
                case 3:

                    if (_pNeko._level + 10 > 40) {
                        _nextlevel = 40;
                    }
                    else {
                        _nextlevel = _pNeko._level + 10;
                    }

                    break;
                case 4:
                    if (_pNeko._level + 10 > 45) {
                        _nextlevel = 45;
                    }
                    else {
                        _nextlevel = _pNeko._level + 10;
                    }

                    break;
                case 5:
                    if (_pNeko._level + 10 > 50) {
                        _nextlevel = 50;
                    }
                    else {
                        _nextlevel = _pNeko._level + 10;
                    }

                    break;
            } // end of switch

            _nextPower = _pNeko._power + (_nextlevel - _pNeko._level) * 30;

        }


        _lblCurrentLevel.text = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L4312) + " " + _pNeko._level.ToString() + "\n" + GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L4110)+ " " + _pNeko._power.ToString();

        _lblResultLevel.text = "[ffef46]" + GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L4312) + " " + _nextlevel.ToString() + "\n" + GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L4110) + " " + _nextPower.ToString() +"[-]";

        _upgradePrice = 0;

        for(int i=_pNeko._level; i< _nextlevel; i++) {
            _upgradePrice += GameSystem.Instance.GetNekoUpgradeCost(i);
        }

        
        _lblExplain.text = _lblExplain.text.Replace("[n]", GameSystem.Instance.GetNumberToString(_upgradePrice));
    }


    /// <summary>
    /// 네코 업그레이드 
    /// </summary>
    public void UpgradeNeko() {

        if (DoingUpgrade)
            return;

        // 코인 부족한 경우 팝업 메세지 뜨고 종료 
        if (GameSystem.Instance.UserGold < _upgradePrice) {
            LobbyCtrl.Instance.OpenInfoPopUp(PopMessageType.GoldShortage);
            return;
        }

        DoingUpgrade = true;


        GameSystem.Instance.UpgradeNekoDBKey = _currentNeko._dbkey;
        GameSystem.Instance.Post2NekoUpgrade(_nextlevel);
    }

    public void OnCompleteUpgrade() {
        DoingUpgrade = false;
    }

    
}
