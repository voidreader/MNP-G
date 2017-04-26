using UnityEngine;
using System.Collections;
using SimpleJSON;
using DG.Tweening;

public class BingoMissionInfoCtrl : MonoBehaviour {

	[SerializeField] Transform _goldClear;
	[SerializeField] Transform _greenClear;

	[SerializeField] UILabel _lblID;
    
    [SerializeField] UILabel _lblInfo;
    [SerializeField] UILabel _lblProgress;

    [SerializeField] UISprite _rewardIcon;
    [SerializeField] UILabel _lblRewardValue;

    Vector3 _leftMissionInfoPos = new Vector3(200, 380, 0);
    Vector3 _originMissionInfoPos = new Vector3(0, 380, 0); // 미션 정보 위치
    string _rewardtype;


    readonly string _greenBoldCode = "[00216b]";
    readonly string _greenCode = "[004f02]";
    readonly string _redBoldCode = "[6d2e00]";
    readonly string _redCode = "[841500]";
    readonly string _blueBoldCode = "[8001c2]";
    readonly string _blueCode = "[00216b]";
    readonly string _purpleBoldCode = "[b85301]";
    readonly string _purpleCode = "[683276]";
    readonly string _orangeBoldCode = "[385bf2]";
    readonly string _orangeCode = "[a75b24]";

    readonly string _blackBoldCode = "[2b2b2b]";
    readonly string _blackCode = "[595959]";

    readonly string _africotBoldCode = "[7c5a13]";
    readonly string _africotCode = "[313e89]";


    string _missionText = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3242);
    string _boldCode, _code;
    string _endCode = "[-]";

    string _missionInfoText;

	/// <summary>
	/// Inits the mission info.
	/// </summary>
	/// <param name="pNode">P node.</param>
	private void InitMissionInfo(JSONNode pNode) {
		_goldClear.gameObject.SetActive (false);
		_greenClear.gameObject.SetActive (false);
		_lblID.transform.localScale = GameSystem.Instance.BaseScale;

		_lblID.text = pNode["id"].Value;

        _missionText = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3242);
        //_lblMission.text = _boldCode + _missionText + _endCode;


        _missionInfoText = string.Empty;

        if(pNode["questgroup"].AsInt > 0) {
            _missionInfoText = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L4137).Replace("[g]", GameSystem.Instance.GetCatGroupName(pNode["questgroup"].AsInt));
            _missionInfoText = _missionInfoText + " " + GameSystem.Instance.GetLocalizeText(pNode["info"].Value);
        }
        else {
            _missionInfoText = GameSystem.Instance.GetLocalizeText(pNode["info"].Value);
        }

        _missionInfoText = _code + _missionInfoText + _endCode;

        _lblInfo.text = _missionInfoText;
        _lblInfo.text = _lblInfo.text.Replace("[n]", "[b]" + GameSystem.Instance.GetNumberToString(pNode["questvalue"].AsInt) + "[/b]");

        //_lblInfo.text = _code +  pNode["info"].Value.Replace("[n]", "[b]" + GameSystem.Instance.GetNumberToString(pNode["questvalue"].AsInt) + "[/b]") + _endCode;

        

        // quest 54(Daily Mission), 55(Weekly Mission)은 특수 처리
        if(pNode["questid"].AsInt == 54) { // Daily Mission
			pNode ["current"].AsInt = GameSystem.Instance.GetCompletedMissionForBingo (MissionType.Day);
			_lblProgress.text = _boldCode + GameSystem.Instance.GetNumberToString(pNode["current"].AsInt) + " / " + GameSystem.Instance.MissionDayInitJSON.Count.ToString() + _endCode;
        }
        else if(pNode["questid"].AsInt == 55) { // Weekly Mission
			pNode ["current"].AsInt = GameSystem.Instance.GetCompletedMissionForBingo (MissionType.Week);
			_lblProgress.text = _boldCode + GameSystem.Instance.GetNumberToString(pNode["current"].AsInt) + " / " + GameSystem.Instance.MissionWeekInitJSON.Count.ToString() + _endCode;
        }
        else {
            _lblProgress.text = _boldCode + GameSystem.Instance.GetNumberToString(pNode["current"].AsInt) + " / " + GameSystem.Instance.GetNumberToString(pNode["questvalue"].AsInt) + _endCode;
        }

        

        _rewardtype = pNode["rewardtype"];

        switch (_rewardtype) {
            case "gem":
                _rewardIcon.spriteName = PuzzleConstBox.spriteUIGemMark;
                break;
            case "coin":
                _rewardIcon.spriteName = PuzzleConstBox.spriteUIGoldMark;
                break;
            case "chub":
                _rewardIcon.spriteName = PuzzleConstBox.spriteUIChubMark;
                break;
            case "tuna":
                _rewardIcon.spriteName = PuzzleConstBox.spriteUITunaMark;
                break;
            case "salmon":
                _rewardIcon.spriteName = PuzzleConstBox.spriteUISalmonMark;
                break;

            case "freeticket":
                _rewardIcon.spriteName = PuzzleConstBox.spriteUIFreeTicket;
                break;
            case "rareticket":
                _rewardIcon.spriteName = PuzzleConstBox.spriteUIRareTicket;
                break;
			case "rainbowticket":
				_rewardIcon.spriteName = PuzzleConstBox.spriteUIRainbowTicket;
				break;
        }

        _lblRewardValue.text = _boldCode + " X " + GameSystem.Instance.GetNumberToString(pNode["rewardvalue1"].AsInt) + _endCode;


        if (pNode["state"].Value.Equals("fill")) {
            _lblProgress.gameObject.SetActive(false);
        }
        else {
            _lblProgress.gameObject.SetActive(true);
        }

    }

    public void SetMissionInfo(JSONNode pNode, string pColor) {

        this.gameObject.SetActive(true);
        this.transform.localPosition = _originMissionInfoPos;
		this.transform.DOKill ();


        SetColorInfo(pColor);


        InitMissionInfo (pNode);

		this.transform.DOLocalJump (_originMissionInfoPos, 500, 1, 0.5f);

		if (pNode ["state"].Value.Equals ("fill") && pNode ["checked"].AsBool) {
			_goldClear.gameObject.SetActive (true);
			_greenClear.gameObject.SetActive (true);
		}

    }

    private void SetColorInfo(string pColor) {
        // bbcode 처리 
        switch (pColor) {
            case "green":
                _boldCode = _greenBoldCode;
                _code = _greenCode;
                _lblID.bitmapFont = BingoMasterCtrl.Instance._boldGreenFont;
                break;
            case "red":
                _boldCode = _redBoldCode;
                _code = _redCode;
                _lblID.bitmapFont = BingoMasterCtrl.Instance._boldRedFont;
                break;
            case "blue":
                _boldCode = _blueBoldCode;
                _code = _blueCode;
                _lblID.bitmapFont = BingoMasterCtrl.Instance._boldBlueFont;
                break;

            case "orange":
                _boldCode = _orangeBoldCode;
                _code = _orangeCode;
                _lblID.bitmapFont = BingoMasterCtrl.Instance._boldOrangeFont;
                break;
            case "purple":
                _boldCode = _purpleBoldCode;
                _code = _purpleCode;
                _lblID.bitmapFont = BingoMasterCtrl.Instance._boldPurpleFont;
                break;
            case "black":
                _boldCode = _blackBoldCode;
                _code = _blackCode;
                _lblID.bitmapFont = BingoMasterCtrl.Instance._boldBlackFont;
                break;
            case "africot":
                _boldCode = _africotBoldCode;
                _code = _africotCode;
                _lblID.bitmapFont = BingoMasterCtrl.Instance._boldAfricotFont;
                break;


        }
    }


	/// <summary>
	/// Clears the mission.
	/// </summary>
	/// <param name="pNode">P node.</param>
	public void ClearMission(JSONNode pNode, string pColor) {
		this.gameObject.SetActive(true);

        SetColorInfo(pColor);

        this.transform.DOKill();
        this.transform.localPosition = _leftMissionInfoPos;
        this.transform.DOLocalMoveX(0, 0.2f);

        InitMissionInfo (pNode);

		_lblID.transform.DOScale (1.2f, 0.5f).SetLoops (2, LoopType.Yoyo);

		_goldClear.gameObject.SetActive (true);
		_goldClear.localScale = new Vector3 (1.3f, 1.3f, 1);
		_goldClear.DOScale (1, 0.5f).SetDelay (0.3f);

		_greenClear.gameObject.SetActive (true);
		_greenClear.localScale = new Vector3 (0.8f, 0.8f, 1);
		_greenClear.DOScale (1, 0.5f).SetDelay (0.1f).SetEase (Ease.OutElastic);
	}
}
