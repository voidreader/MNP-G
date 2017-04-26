using UnityEngine;
using System.Collections;
using SimpleJSON;
using DG.Tweening;

public class BingoColumnCtrl : MonoBehaviour {


	[SerializeField] int _colID;
	[SerializeField] int _colGoalValue;
	[SerializeField] int _colCurrentValue;
	[SerializeField] string _colState;
    [SerializeField] string _info;

    [SerializeField] GameObject _selectCover;
    [SerializeField] UISprite _spSelectCover;
    [SerializeField] GameObject _colBase;
	[SerializeField] UIButton _colSprite;
	[SerializeField] UILabel _idLabel;

    #region Font
    [SerializeField] UIFont _boldGreenFont;
	[SerializeField] UIFont _greenFont;
    [SerializeField] UIFont _boldBlueFont;
	[SerializeField] UIFont _blueFont;
    [SerializeField] UIFont _boldRedFont;
	[SerializeField] UIFont _redFont;

    [SerializeField] UIFont _boldOrangeFont;
	[SerializeField] UIFont _orangeFont;
    [SerializeField] UIFont _boldPurpleFont;
	[SerializeField] UIFont _purpleFont;


    [SerializeField] UIFont _boldAfricotFont;
	[SerializeField] UIFont _africotFont;

    #endregion

    [SerializeField] bool _isSelected = false;
	[SerializeField] bool _isChecked = false;

	[SerializeField] SparkLightCtrl _splashEffect;
    [SerializeField] SparkLightCtrl _blinkEffect;


    string _spriteFill;
    string _spriteEmpty;
    string _spriteSelect;

    UIFont _boldFont;
    UIFont _font;

    JSONNode _originalNode;


	public void SetBingoCol(JSONNode pNode, string pColor) {
		_splashEffect.gameObject.SetActive (false);
        _blinkEffect.gameObject.SetActive(false);
		_selectCover.gameObject.SetActive (false);
        _colBase.SetActive(true);

        _originalNode = pNode;

        _colID = pNode ["id"].AsInt;
		_colGoalValue = pNode ["questvalue"].AsInt; 
		_colCurrentValue = pNode ["current"].AsInt;
		_colState = pNode ["state"].Value;
        _info = pNode["info"].Value;

		_isChecked = pNode ["checked"].AsBool;

        _idLabel.text = _colID.ToString();

        switch(pColor) {
            case "green":
                _spriteFill = PuzzleConstBox.spriteGreenBingoFill;
                _spriteEmpty = PuzzleConstBox.spriteGreenBingoEmpty;
                _spriteSelect = PuzzleConstBox.spriteGreenBingoSelect;
                _boldFont = _boldGreenFont;
                _font = _greenFont;
                break;
            case "blue":
                _spriteFill = PuzzleConstBox.spriteBlueBingoFill;
                _spriteEmpty = PuzzleConstBox.spriteBlueBingoEmpty;
                _spriteSelect = PuzzleConstBox.spriteBlueBingoSelect;
                _boldFont = _boldBlueFont;
                _font = _blueFont;
                break;
            case "red":
                _spriteFill = PuzzleConstBox.spriteRedBingoFill;
                _spriteEmpty = PuzzleConstBox.spriteRedBingoEmpty;
                _spriteSelect = PuzzleConstBox.spriteRedBingoSelect;
                _boldFont = _boldRedFont;
                _font = _redFont;
                break;

            case "orange":
                _spriteFill = PuzzleConstBox.spriteOrangeBingoFill;
                _spriteEmpty = PuzzleConstBox.spriteOrangeBingoEmpty;
                _spriteSelect = PuzzleConstBox.spriteOrangeBingoSelect;
                _boldFont = _boldOrangeFont;
                _font = _orangeFont;
                break;

            case "purple":
                _spriteFill = PuzzleConstBox.spritePurpleBingoFill;
                _spriteEmpty = PuzzleConstBox.spritePurpleBingoEmpty;
                _spriteSelect = PuzzleConstBox.spritePurpleBingoSelect;
                _boldFont = _boldPurpleFont;
                _font = _purpleFont;
                break;

            case "black":
                _spriteFill = PuzzleConstBox.spriteBlackBingoFill;
                _spriteEmpty = PuzzleConstBox.spriteBlackBingoEmpty;
                _spriteSelect = PuzzleConstBox.spriteBlackBingoSelect;
                _boldFont = BingoMasterCtrl.Instance._boldBlackFont;
                _font = BingoMasterCtrl.Instance._blackFont;
                break;

            case "africot":
                _spriteFill = PuzzleConstBox.spriteAfricotBingoFill;
                _spriteEmpty = PuzzleConstBox.spriteAfricotBingoEmpty;
                _spriteSelect = PuzzleConstBox.spriteAfricotBingoSelect;
                _boldFont = BingoMasterCtrl.Instance._boldAfricotFont;
                _font = BingoMasterCtrl.Instance._AfricotFont;
                break;

        }

        _spSelectCover.spriteName = _spriteSelect;

        // 상태에 따른 겉모습 수정 
        if (_colState.Equals("fill") && _isChecked) {
            _colSprite.normalSprite = _spriteFill;
            _idLabel.bitmapFont = _font;
        }
        else {
            _colSprite.normalSprite = _spriteEmpty;
            _idLabel.bitmapFont = _boldFont;
        }



	}


    #region Mission Clear

    /// <summary>
    /// Plaies the clear motion.(컬럼 오픈)
    /// </summary>
    public void PlayClearMotion() {
        // 1. delay and scale 
        this.transform.DOKill();
        this.transform.localScale = GameSystem.Instance.BaseScale;
        this.transform.DOScale(1.2f, 0.2f).SetLoops(2, LoopType.Yoyo).SetDelay(0.1f).OnComplete(OnCompleteDoScaleInClearMotion);

		// 2. splash play and change sprite 

	}

	private void OnCompleteDoScaleInClearMotion() {

		// spash play 
		_splashEffect.PlayCurrentPos ();
        BingoMasterCtrl.Instance.PlayAudio(BingoMasterCtrl.Instance.AcColumnOpen);

        _colSprite.normalSprite = _spriteFill;

        _idLabel.bitmapFont = _font;

		_isChecked = true;
	}

    #endregion

    #region Line Clear

    public void PlayLineClearMotion() {

        this.transform.DOKill();
        this.transform.localScale = GameSystem.Instance.BaseScale;

        _blinkEffect.PlayCurrentPos();
        BingoMasterCtrl.Instance.PlayAudio(BingoMasterCtrl.Instance.AcBlink);


        if (_colBase.activeSelf) { // 이미 클리어된 미션의 경우는 패스 
            Invoke("PlaySplash", 0.5f);
        }

    }

    private void PlaySplash() {
        _splashEffect.PlayCurrentPos();
        //BingoMasterCtrl.Instance.PlayAudio(BingoMasterCtrl.Instance.AcColumnOpen);
        this.transform.DOScale(1.1f, 0.2f).SetLoops(2, LoopType.Yoyo).OnComplete(OnCompleteLineClearSplash);
    }

    private void OnCompleteLineClearSplash() {
        _colBase.SetActive(false); // Sprite 안보이게 처리 
    }

    #endregion



    /// <summary>
    /// 라인 클리어로 인해 공백 상태로 변경 
    /// </summary>
    public void SetClearState() {
        _colBase.gameObject.SetActive(false);
    }

    /// <summary>
    /// 컬럼 선택 
    /// </summary>
    public void SelectCol() {

        if(!_selectCover.activeSelf) {
            BingoMasterCtrl.Instance.ClearSelect();
            _selectCover.SetActive(true);
            BingoMasterCtrl.Instance.SelectedCol = this;
            BingoMasterCtrl.Instance.OpenMissionInfo(_originalNode);

            BingoMasterCtrl.Instance.PlayAudio(BingoMasterCtrl.Instance.AcPossitiveUI);
        }
        else {
            BingoMasterCtrl.Instance.SelectedCol = null;
            _selectCover.SetActive(false);
            BingoMasterCtrl.Instance.CloseMissionInfo();
            BingoMasterCtrl.Instance.PlayAudio(BingoMasterCtrl.Instance.AcPossitiveUI);
        }



    }

    /// <summary>
    /// 선택 해제 
    /// </summary>
    public void UnselectCol() {
        this._selectCover.SetActive(false);
    }
}
