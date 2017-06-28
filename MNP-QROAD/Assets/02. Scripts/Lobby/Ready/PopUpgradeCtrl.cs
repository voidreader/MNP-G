using UnityEngine;
using System.Collections;
using DG.Tweening;
using Google2u;

public class PopUpgradeCtrl : MonoBehaviour {

    readonly int MAX_LEVEL = 10;

    [SerializeField]
    int _currentPowerLevel = 0;

    // Lv1
    [SerializeField] GameObject _lv1Group;
    [SerializeField] UISprite _spriteLv1MyFruit;
    [SerializeField] UISprite _spriteLv1NextFruit;
    [SerializeField] UILabel _lblLv1Level;
    [SerializeField] UILabel _lblLv1Power;
    [SerializeField] UILabel _lblLv1Absorb;
    [SerializeField] UILabel _lblLv1PriceCoin;

    

    


    // Lv10
    [SerializeField] GameObject _lv5Group;
    [SerializeField] UISprite _spriteLv5MyFruit;
    [SerializeField] UISprite _spriteLv5Next1Fruit;
    [SerializeField] UISprite _spriteLv5Next2Fruit;
    [SerializeField] UISprite _spriteLv5Next3Fruit;
    [SerializeField] UISprite _spriteLv5Next4Fruit;
    [SerializeField] UISprite _spriteLv5Next5Fruit;
    [SerializeField] UILabel _lblLv5Level;
    [SerializeField] UILabel _lblLv5Power;
    [SerializeField] UILabel _lblLv5Absorb;

    [SerializeField] UILabel _lblLv5PriceCoin;
    [SerializeField] UILabel _lblLv5PriceGem;

    [SerializeField] SimplePopupCtrl _messagePopUp;


    [SerializeField] GameObject _btnLv1Upgrade;
    [SerializeField] GameObject _btnLv10CoinUpgrade;
    [SerializeField] GameObject _btnLv10GemUpgrade;

    [SerializeField] UILabel _lblLv1MaxComment;
    [SerializeField] UILabel _lblLv10MaxComment;


    MNP_PowerUpgradeCost _powerCost = MNP_PowerUpgradeCost.Instance;
    int _tempPrice;

    bool _isProcessing = false;

    int _upgradePrice = 0;
    string _upgradePriceType = string.Empty;


    int _skillPowerValue;
    int _skillAbsorbValue;

    #region Properties 
    public int UpgradePrice {
        get {
            return _upgradePrice;
        }

        set {
            _upgradePrice = value;
        }
    }
    #endregion

    void OnEnable() {
        SetPowerUpgradeStatus();
    }

    /// <summary>
    /// 업그레이드 상태 설정 
    /// </summary>
    public void SetPowerUpgradeStatus() {

        _lblLv10MaxComment.gameObject.SetActive(false);
        _lblLv1MaxComment.gameObject.SetActive(false);

        _currentPowerLevel = GameSystem.Instance.UserPowerLevel;

        if(_currentPowerLevel + 1 <= MAX_LEVEL) {
            _spriteLv1MyFruit.spriteName = PuzzleConstBox.listFruitClip[_currentPowerLevel - 1];
            _spriteLv1NextFruit.spriteName = PuzzleConstBox.listFruitClip[_currentPowerLevel]; // 다음 레벨 Sprite 
            _lblLv1PriceCoin.text = string.Format("{0:n0}", GetUpgradeCost(_currentPowerLevel, false));
            _lblLv1Level.text = "[ffdd00]Lv.[-]" + (_currentPowerLevel + 1).ToString();
            _lblLv1Power.text = "[ffdd00]Power: [-]" + ((_currentPowerLevel + 1) * 3).ToString();
            _lblLv1Absorb.text = "[ffdd00]" + GameSystem.Instance.GetLocalizeText(MNP_Localize.rowIds.L3121) + " [-]+" + ((_currentPowerLevel) * 3).ToString() + "%";
        }

        if (_currentPowerLevel + 5 <= MAX_LEVEL) {
            _spriteLv5MyFruit.spriteName = PuzzleConstBox.listFruitClip[_currentPowerLevel - 1];
            _spriteLv5Next1Fruit.spriteName = PuzzleConstBox.listFruitClip[_currentPowerLevel];
            _spriteLv5Next2Fruit.spriteName = PuzzleConstBox.listFruitClip[_currentPowerLevel + 1]; // 다음 레벨 Sprite 
            _spriteLv5Next3Fruit.spriteName = PuzzleConstBox.listFruitClip[_currentPowerLevel + 2]; // 다음 레벨 Sprite 
            _spriteLv5Next4Fruit.spriteName = PuzzleConstBox.listFruitClip[_currentPowerLevel + 3]; // 다음 레벨 Sprite 
            _spriteLv5Next5Fruit.spriteName = PuzzleConstBox.listFruitClip[_currentPowerLevel + 4]; // 다음 레벨 Sprite 



            _lblLv5PriceCoin.text = string.Format("{0:n0}", GetUpgradeCost(_currentPowerLevel, true));
            _lblLv5PriceGem.text = string.Format("{0:n0}", GetUpgradeGemCost(_currentPowerLevel));
            _lblLv5Level.text = "[ffdd00]Lv.[-]" + (_currentPowerLevel + 5).ToString();
            _lblLv5Power.text = "[ffdd00]Power: [-]" + ((_currentPowerLevel + 5) * 3).ToString();
            _lblLv5Absorb.text = "[ffdd00]" + GameSystem.Instance.GetLocalizeText(MNP_Localize.rowIds.L3121) + " [-]+" + ((_currentPowerLevel + 4) * 3).ToString() + "%";
        }




        // 5레벨 업그레이드가 최고 레벨을 초과하게 되는 경우 
        if (_currentPowerLevel + 5 > MAX_LEVEL) {
            _lv5Group.SetActive(false);

            _lblLv5Level.text = "[ffdd00]Lv.[-]" + (_currentPowerLevel).ToString();
            _lblLv5Power.text = "[ffdd00]Power: [-]" + ((_currentPowerLevel) * 3).ToString();
            _lblLv5Absorb.text = "[ffdd00]" + GameSystem.Instance.GetLocalizeText(MNP_Localize.rowIds.L3121) + " [-]+" + ((_currentPowerLevel-1) * 3).ToString() + "%";
            _lblLv10MaxComment.gameObject.SetActive(true);


        }

        if(_currentPowerLevel + 1 > MAX_LEVEL) {
            _lv1Group.SetActive(false);
            _lblLv1Level.text = "[ffdd00]Lv.[-]" + (_currentPowerLevel).ToString();
            _lblLv1Power.text = "[ffdd00]Power: [-]" + ((_currentPowerLevel) * 3).ToString();
            _lblLv1Absorb.text = "[ffdd00]" + GameSystem.Instance.GetLocalizeText(MNP_Localize.rowIds.L3121) + " [-]+" + ((_currentPowerLevel-1) * 3).ToString() + "%";

            _lblLv1MaxComment.gameObject.SetActive(true);
        }



    }


    #region 레벨 1 업그레이드 


    /// <summary>
    /// 레벨 1 업그레이드 확인 
    /// </summary>
    public void UpgradeLv1Confirm() {
        _upgradePrice = GetUpgradeCost(_currentPowerLevel, false);
        _messagePopUp.SetConfirmMessage(PopMessageType.PowerUpgrade, string.Format("{0:n0}", _upgradePrice), UpgradeLv1Power);
    }



    /// <summary>
    /// 레벨 1 업그레이드 
    /// </summary>
    public void UpgradeLv1Power() {
        Debug.Log("★★★ UpgradeLv1Power");
        GameSystem.Instance.Post2PowerUpgrade(_currentPowerLevel + 1, _upgradePrice, "coin");
        this.SendMessage("CloseSelf");
    }

    #endregion


    #region 레벨 10 업그레이드

    /// <summary>
    /// 레벨 5 업그레이드 확인 
    /// </summary>
    public void UpgradeLv10CoinConfirm() {
        _upgradePrice = GetUpgradeCost(_currentPowerLevel, true);
        _upgradePriceType = "coin";
        _messagePopUp.SetConfirmMessage(PopMessageType.PowerUpgrade, string.Format("{0:n0}", _upgradePrice), UpgradeLv5Power);
    }


    /// <summary>
    /// 레벨 5 업그레이드 확인 
    /// </summary>
    public void UpgradeLv10GemConfirm() {
        _upgradePrice = GetUpgradeGemCost(_currentPowerLevel);
        _upgradePriceType = "gem";
        _messagePopUp.SetConfirmMessage(PopMessageType.PowerUpgradeWithGem, string.Format("{0:n0}", _upgradePrice), UpgradeLv5Power);

        
    }


    /// <summary>
    /// 레벨 5 업그레이드 
    /// </summary>
    public void UpgradeLv5Power() {
        Debug.Log("★★★ UpgradeLv5Power");

        GameSystem.Instance.Post2PowerUpgrade(_currentPowerLevel + 5, _upgradePrice, _upgradePriceType);
        this.SendMessage("CloseSelf");
    }


    #endregion


    /// <summary>
    /// 업그레이드 가격
    /// </summary>
    /// <param name="pCurrentLevel"></param>
    /// <param name="pLv5"></param>
    /// <returns></returns>
    private int GetUpgradeCost(int pCurrentLevel, bool pLv5) {

        if (pLv5) {

            _tempPrice = 0;
            for (int i = 0; i < 5; i++) {
                _tempPrice += _powerCost.Rows[pCurrentLevel - 1 + i]._cost;
            }

            // 10% 할인 처리 
            _tempPrice = (int)(_tempPrice * 0.9f);

            return _tempPrice;
        }

        else {
            return _powerCost.Rows[pCurrentLevel - 1]._cost;
        }
    }


    /// <summary>
    /// 업그레이드 Gem 가격  
    /// </summary>
    private int GetUpgradeGemCost(int pCurrentLevel) {
        return (int)(GetUpgradeCost(pCurrentLevel, true) * 0.01f);
    }

	
}
