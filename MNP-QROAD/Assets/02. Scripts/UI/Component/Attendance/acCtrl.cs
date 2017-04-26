using UnityEngine;
using System.Collections;
using DG.Tweening;


public class acCtrl : MonoBehaviour {

    [SerializeField]
    UISprite _spriteFrame;

    [SerializeField]
    UISprite _spriteCap;
    [SerializeField]
    UISprite _spriteRewardIcon;

    [SerializeField]
    UILabel _lblDay;

    [SerializeField]
    UILabel _lblValue;

    [SerializeField]
    int _value;

    [SerializeField]
    int _day;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="pDay"></param>
    /// <param name="pValue"></param>
    /// <param name="pType"></param>
    /// <param name="pComplete"></param>
    public void SetAttendanceColumn(int pDay, int pValue, string pType, bool pComplete) {
        _day = pDay;
        _value = pValue;

        SetRewardIcon(pType);

        if (pComplete)
            _spriteCap.gameObject.SetActive(true);
        else
            _spriteCap.gameObject.SetActive(false);


        if(pDay % 5 == 0) {
            _spriteFrame.spriteName = "ww-base";
        }else {
            _spriteFrame.spriteName = "ll-base";
        }

        _lblDay.text = _day.ToString();
        _lblValue.text = GameSystem.Instance.GetNumberToString(_value);
    }
	
	private void SetRewardIcon(string pType) {

        _spriteRewardIcon.width = 76;
        _spriteRewardIcon.height = 76;

        switch (pType) {
            case "coin":
                _spriteRewardIcon.spriteName = "i-coin";
                break;
            case "gem":
                // gem만 72
                _spriteRewardIcon.width = 72;
                _spriteRewardIcon.height = 72;
                _spriteRewardIcon.spriteName = "i-zam2";
                break;
            case "chub":
                _spriteRewardIcon.spriteName = "i-k";
                break;
            case "tuna":
                _spriteRewardIcon.spriteName = "i-t";
                break;
            case "salmon":
                _spriteRewardIcon.spriteName = "i-ss";
                break;
            case "freeticket":
                _spriteRewardIcon.spriteName = PuzzleConstBox.spriteUIFreeTicket;
                break;
            case "rareticket":
                _spriteRewardIcon.spriteName = PuzzleConstBox.spriteUIRareTicket;
                break;
			case "rainbowticket":
				_spriteRewardIcon.spriteName = PuzzleConstBox.spriteUIRainbowTicket;
				break;

        }
    }



    public void SetToday() {
        Invoke("SettingToday", 1);
    }

    private void SettingToday() {
        _spriteCap.gameObject.SetActive(true);
        _spriteCap.transform.localScale = new Vector3(2, 2, 1);
        _spriteCap.transform.DOScale(1, 0.5f);
    }

}
