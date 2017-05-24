using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;

public class HeartShopCtrl : MonoBehaviour {

	[SerializeField] UILabel _lblGem;
    [SerializeField] GameObject _max;
    [SerializeField] UIButton _btnFree;

    [SerializeField] UILabel _lblFreeChargeButton;

    int _heartCount = 0;

    int getHeart;


    [SerializeField]
    UISprite[] arrHearts;

    [SerializeField]
    UILabel _lblHeartCount;


    /// <summary>
    /// 하트 개수 업데이트
    /// </summary>
    public void UpdateHearts() {

        _lblHeartCount.text = string.Empty;
        _heartCount = GameSystem.Instance.HeartCount;

        

        if(GameSystem.Instance.Remainheartcharge <= 0) {
            _btnFree.normalSprite = "gray-bodan";
            _lblFreeChargeButton.text = "Free";
        }
        else {
            _btnFree.normalSprite = "pop-reds";
            _lblFreeChargeButton.text = "Free(" + GameSystem.Instance.Remainheartcharge + ")";
        }

        for (int i = 0; i < 5; i++) {
            arrHearts[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < _heartCount; i++) {

            if (i >= arrHearts.Length)
                break;

            arrHearts[i].gameObject.SetActive(true);
        }

        if (_heartCount >= 5) {
            _max.SetActive(true);

            if(_heartCount > 5) {
                _lblHeartCount.text = "+" + (_heartCount - 5).ToString();
            }

        }
        else {
            _max.SetActive(false);
        }

        

    }


    /// <summary>
    /// 하트샵 오픈 
    /// </summary>
    public void SetHeartShop() {

        this.gameObject.SetActive(true);

        // 하트가 5개가 넘은 경우
        if (GameSystem.Instance.HeartCount >= 5) {
            _lblGem.text = "0";
            _btnFree.normalSprite = "gray-bodan";
        }
        else {
            getHeart = 5 - GameSystem.Instance.HeartCount;
            _lblGem.text = (getHeart * 10).ToString();
            _btnFree.normalSprite = "pop-reds";
        }

        UpdateHearts();
    }


	public void ShowHeartAd() {

		if (GameSystem.Instance.HeartCount >= 5) {

			if(LobbyCtrl.Instance != null) 
				LobbyCtrl.Instance.OpenInfoPopUp(PopMessageType.HeartPurChaseButFull);

			return;
		}

        
        GameSystem.Instance.CheckAd(AdsType.HeartAds);
        

	}



	public void ExchangeHeart() {


        if (GameSystem.Instance.HeartCount >= 5) {

            if (LobbyCtrl.Instance != null)
                LobbyCtrl.Instance.OpenInfoPopUp(PopMessageType.HeartPurChaseButFull);

            return;
        }

        if (LobbyCtrl.Instance != null) 
			LobbyCtrl.Instance.ExchangeHeart ();
	}




    public void OnClickFriend() {
        LobbyCtrl.Instance.SendMessage("SetInviteTab");
    }


}
