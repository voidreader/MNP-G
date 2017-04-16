using UnityEngine;
using System.Collections;

public class OptionSoundPushCtrl : MonoBehaviour {


  	[SerializeField] UIButton uibtnBGM;
	[SerializeField] UIButton uibtnEffect;
    [SerializeField] UIButton uibtnHeartPush;
    [SerializeField] UIButton uibtnFreeCranePush;
    [SerializeField] UIButton uibtnRemotePush;

    [SerializeField] UIButton uibtnPuzzleTipPush;

    [SerializeField] GameObject _lblBGMOn;
    [SerializeField] GameObject _lblBGMOff;
    [SerializeField] GameObject _lblSEOn;
    [SerializeField] GameObject _lblSEOff;
    [SerializeField] GameObject _lblHeartPushOn;
    [SerializeField] GameObject _lblHeartPushOff;
    [SerializeField] GameObject _lblFreeCranePushOn;
    [SerializeField] GameObject _lblFreeCranePushOff;
    [SerializeField] GameObject _lblRemotePushOn;
    [SerializeField] GameObject _lblRemotePushOff;

    [SerializeField] GameObject _lblPuzzleTipOn;
    [SerializeField] GameObject _lblPuzzleTipOff;


    private bool _optionBGM = true;
    private bool _optionEffect = true;

    private bool _optionHeartFull = true;
    private bool _optionFreeCrane = true;
    private bool _optionRemotePush = true;
    private bool _optionPuzzleTip = false;

    string _btnOn = "option_btn_red";
    string _btnOff = "option_btn_blue";

    /// <summary>
    /// 
    /// </summary>
	public void SetSetting() {

        this.gameObject.SetActive(true);

        _optionBGM = GameSystem.Instance.OptionBGMPlay;
        _optionEffect = GameSystem.Instance.OptionSoundPlay;
        _optionHeartFull = GameSystem.Instance.OptionHeartPushUse;
        _optionFreeCrane = GameSystem.Instance.OptionFreeCranePush;
        _optionRemotePush = GameSystem.Instance.OptionRemotePush;
        _optionPuzzleTip = GameSystem.Instance.OptionPuzzleTip;

        if (_optionBGM) {

            _lblBGMOn.SetActive(true);
            _lblBGMOff.SetActive(false);
            uibtnBGM.normalSprite = _btnOn;
        }
        else {
            _lblBGMOn.SetActive(false);
            _lblBGMOff.SetActive(true);

            uibtnBGM.normalSprite = _btnOff;
        }

        if (_optionEffect) {

            _lblSEOn.SetActive(true);
            _lblSEOff.SetActive(false);
            uibtnEffect.normalSprite = _btnOn;

        }
        else {
            _lblSEOn.SetActive(false);
            _lblSEOff.SetActive(true);
            uibtnEffect.normalSprite = _btnOff;
        }

        if (_optionHeartFull) {

            _lblHeartPushOn.SetActive(true);
            _lblHeartPushOff.SetActive(false);
            uibtnHeartPush.normalSprite = _btnOn;
        }
        else {
            _lblHeartPushOn.SetActive(false);
            _lblHeartPushOff.SetActive(true);
            uibtnHeartPush.normalSprite = _btnOff;
        }


        if (_optionFreeCrane) {
            _lblFreeCranePushOn.SetActive(true);
            _lblFreeCranePushOff.SetActive(false);
            uibtnFreeCranePush.normalSprite = _btnOn;
        }
        else {
            _lblFreeCranePushOn.SetActive(false);
            _lblFreeCranePushOff.SetActive(true);
            uibtnFreeCranePush.normalSprite = _btnOff;
        }

        if (_optionRemotePush) {
            _lblRemotePushOn.SetActive(true);
            _lblRemotePushOff.SetActive(false);
            uibtnRemotePush.normalSprite = _btnOn;
        }
        else {
            _lblRemotePushOn.SetActive(false);
            _lblRemotePushOff.SetActive(true);
            uibtnRemotePush.normalSprite = _btnOff;
        }


        if (_optionPuzzleTip) {
            _lblPuzzleTipOn.SetActive(true);
            _lblPuzzleTipOff.SetActive(false);
            uibtnPuzzleTipPush.normalSprite = _btnOn;
        }
        else {
            _lblPuzzleTipOn.SetActive(false);
            _lblPuzzleTipOff.SetActive(true);
            uibtnPuzzleTipPush.normalSprite = _btnOff;
        }

    }

    public void ClickPuzzleTip() {
        _optionPuzzleTip = !_optionPuzzleTip;

        if (_optionPuzzleTip) {
            _lblPuzzleTipOn.SetActive(true);
            _lblPuzzleTipOff.SetActive(false);
            uibtnPuzzleTipPush.normalSprite = _btnOn;
        }
        else {
            _lblPuzzleTipOn.SetActive(false);
            _lblPuzzleTipOff.SetActive(true);
            uibtnPuzzleTipPush.normalSprite = _btnOff;
        }

        GameSystem.Instance.SavePuzzleTipOption(_optionPuzzleTip);

    }

    /// <summary>
    /// 배경음악 버튼 클릭 
    /// </summary>
    public void ClickBGMSound() {

        _optionBGM = !_optionBGM;



        if (_optionBGM) {

            _lblBGMOn.SetActive(true);
            _lblBGMOff.SetActive(false);
            uibtnBGM.normalSprite = _btnOn;
        }
        else {

            _lblBGMOn.SetActive(false);
            _lblBGMOff.SetActive(true);
            uibtnBGM.normalSprite = _btnOff;
        }

        GameSystem.Instance.SaveBGMSoundOption(_optionBGM);
    }


    /// <summary>
    /// 효과음 버튼 클릭
    /// </summary>
    public void ClickEffectSound() {
        _optionEffect = !_optionEffect; // 바꿔주고 

        if (_optionEffect) {

            _lblSEOn.SetActive(true);
            _lblSEOff.SetActive(false);
            uibtnEffect.normalSprite = _btnOn;
        }
        else {


            _lblSEOn.SetActive(false);
            _lblSEOff.SetActive(true);
            uibtnEffect.normalSprite = _btnOff;
        }


        GameSystem.Instance.SaveEffectSoundOption(_optionEffect);

    }


    /// <summary>
    /// Push Option
    /// </summary>
    public void ClickNekoRewardPush() {
        _optionHeartFull = !_optionHeartFull;


        if (_optionHeartFull) {

            _lblHeartPushOn.SetActive(true);
            _lblHeartPushOff.SetActive(false);
            uibtnHeartPush.normalSprite = _btnOn;
        }
        else {
            _lblHeartPushOn.SetActive(false);
            _lblHeartPushOff.SetActive(true);
            uibtnHeartPush.normalSprite = _btnOff;
        }

        GameSystem.Instance.SaveHeartPushOption(_optionHeartFull);
    }


    public void ClickRemotePush() {
        _optionRemotePush = !_optionRemotePush;
        GameSystem.Instance.SaveRemotePushOption(_optionRemotePush);

        SetSetting();
    }


    public void ClickFreeCranePush() {
        _optionFreeCrane = !_optionFreeCrane;
        GameSystem.Instance.SaveFreeCranePushOption(_optionFreeCrane);

        SetSetting();
    }

}
