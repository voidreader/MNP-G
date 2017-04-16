using UnityEngine;
using System.Collections;

public class NekoBonusInfoCtrl : MonoBehaviour {

    [SerializeField] UILabel _lblText;
    string _text;

	// Use this for initialization
	void Start () {
	
	}
	
    /// <summary>
    /// 
    /// </summary>
	public void SetNekoBonusInfo() {

        this.gameObject.SetActive(true);

        _text = GameSystem.Instance.GetLocalizeText("3222");
        _lblText.text = _text.Replace("[n]", GameSystem.Instance.Remainnekogift.ToString());
    }

    public void OpenNekoAd() {
        this.SendMessage("CloseSelf");
        LobbyCtrl.Instance.SendMessage("GetAdsNekoGift");
    }

    public void OpenNekoNoAd() {
        this.SendMessage("CloseSelf");
        LobbyCtrl.Instance.SendMessage("DelayedGetNekoFreeGift");
    }

}
