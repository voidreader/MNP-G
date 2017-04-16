using UnityEngine;
using System.Collections;

public class PopUpShareBottleCtrl : MonoBehaviour {

    [SerializeField] UILabel _lblMessage;

    

    /// <summary>
    /// 화면 오픈 
    /// </summary>
    /// <param name="pCheckBonus"></param>
    public void OpenMainShare(bool pCheckBonus) {

        this.gameObject.SetActive(true);

        _lblMessage.text = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L4166);

        if (pCheckBonus)
            _lblMessage.text = _lblMessage.text + "\n" +GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L4167);

    }


    public void ShareFB() {
        

    }

    public void ShareTW() {

    }

	
}
