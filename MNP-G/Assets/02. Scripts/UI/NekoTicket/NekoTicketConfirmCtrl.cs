using UnityEngine;
using System;
using System.Collections;

public class NekoTicketConfirmCtrl : MonoBehaviour {

    [SerializeField]
    GameObject _fishGroup;

    [SerializeField]
    UISprite _spNekoSprite;
    [SerializeField]
    UILabel _lblNekoGrade;

    [SerializeField]
    UILabel _lblText;

    [SerializeField]
    int _nekoID;
    [SerializeField]
    int _nekoGrade;


    string _gradeStar;
    string _nekoName;

    [SerializeField]
    bool _isOwn = false;

    event Action<int, int> OnYes;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="pNekoID"></param>
    /// <param name="pNekoGrade"></param>
    /// <param name="pGradeStar"></param>
    /// <param name="pNekoName"></param>
    /// <param name="pSprite"></param>
    public void OpenNekoTicketExchangeConfirm(int pNekoID, int pNekoGrade, string pGradeStar, string pNekoName, UISprite pSprite, Action<int, int> pCallback) {

        this.gameObject.SetActive(true);

        OnYes = delegate { };
        OnYes += pCallback;

        _isOwn = false;
        _spNekoSprite.atlas = pSprite.atlas;
        _spNekoSprite.spriteName = pSprite.spriteName;

        _fishGroup.SetActive(false);


        _nekoID = pNekoID;
        _nekoGrade = pNekoGrade;
        _gradeStar = pGradeStar;
        _nekoName = pNekoName;

        _lblNekoGrade.text = _gradeStar;


        // 소유 여부 체크 
        if (GameSystem.Instance.FindUserNekoData(_nekoID) >= 0) {
            _isOwn = true;
        }

        // 이미 고양이를 소유한 경우와 소유하지 않은 경우로 나뉜다. 
        if(_isOwn) {
            _lblText.text = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L4245);

            // 생선표시 
            _fishGroup.SetActive(true);
        }
        else {

            _lblText.text = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L4246).Replace("[n]", _nekoName);

        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void OnClickYes() {
        this.SendMessage("CloseSelf");
        OnYes(_nekoID, _nekoGrade);
    }
}
