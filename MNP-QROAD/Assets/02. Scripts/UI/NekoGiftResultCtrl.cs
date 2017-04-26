using UnityEngine;
using System.Collections;

public class NekoGiftResultCtrl : MonoBehaviour {

    [SerializeField]
    UILabel _value;

    [SerializeField]
    UISprite _giftIcon;

    [SerializeField]
    UILabel _lblMessage;

    


    /// <summary>
    /// 공유하기 보너스 결과 
    /// </summary>
    /// <param name="pType"></param>
    /// <param name="pValue"></param>
    public void SetShareResult(string pType, int pValue) {

        this.gameObject.SetActive(true);
        _value.text = "x" + GameSystem.Instance.GetNumberToString(pValue);

        _lblMessage.text = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L4260);

        SetIcon(pType);

    }

    /// <summary>
    /// 밋치리보너스 결과 처리 
    /// </summary>
    /// <param name="pType"></param>
    /// <param name="pValue"></param>
    public void SetResult(string pType, int pValue) {

        this.gameObject.SetActive(true);
        _value.text = "x" + GameSystem.Instance.GetNumberToString(pValue);

        _lblMessage.text = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L4106);

        SetIcon(pType);
    }


    private void SetIcon(string pType) {

        switch (pType) {
            case "coin":
                _giftIcon.spriteName = PuzzleConstBox.spriteUICoinMark;
                break;
            case "gem":
                _giftIcon.spriteName = PuzzleConstBox.spriteUIDiaMark;
                break;
            case "chub":
                _giftIcon.spriteName = "i-k";
                break;
            case "tuna":
                _giftIcon.spriteName = "i-t";
                break;
            case "salmon":
                _giftIcon.spriteName = "i-ss";
                break;
            case "freeticket":
                _giftIcon.spriteName = PuzzleConstBox.spriteUIFreeTicket;
                break;

            case "rareticket":
                _giftIcon.spriteName = PuzzleConstBox.spriteUIRareTicket;
                break;
			case "rainbowticket":
				_giftIcon.spriteName = PuzzleConstBox.spriteUIRainbowTicket;
				break;
        }
    }


    /// <summary>
    /// 닫기 버튼
    /// </summary>
    public void OnClosing() {
        LobbyCtrl.Instance.UpdateTopInformation();
    }

    public void Confirm() {
        LobbyCtrl.Instance.UpdateTopInformation();
        this.SendMessage("CloseSelf");
    }
}
