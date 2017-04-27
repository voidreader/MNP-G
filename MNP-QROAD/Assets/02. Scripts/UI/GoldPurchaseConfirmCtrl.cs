using UnityEngine;
using System.Collections;
using Google2u;

public class GoldPurchaseConfirmCtrl : MonoBehaviour {

	[SerializeField] int _exchangeIndex;
	[SerializeField] string _gold;
	[SerializeField] string _gem;

	[SerializeField] UILabel _lblGold;
	[SerializeField] UILabel _lblGem;
	[SerializeField] UISprite _spGold;

	// Use this for initialization
	void Start () {
	
	}

	public void SetExchangeIndex(int pIndex) {
		_exchangeIndex = pIndex;

		//_gold = GameSystem.Instance.DocsGoldShop.get<int> (pIndex.ToString (), "gold").ToString ("#,###");
        _gem = string.Format("{0:n0}", MNP_CoinShop.Instance.Rows[pIndex]._gem); 
        _gold = GameSystem.Instance.GetNumberToString(GameSystem.Instance.ListCoinShopPrice[pIndex]);

		_lblGold.text = _gold;
		_lblGem.text = _gem;

		_spGold.spriteName = PuzzleConstBox.listGoldPurchaseConfirmSprite [_exchangeIndex];

	}

	public void ExchangeGold() {

        GameSystem.Instance.ExchangeGoldIndex = _exchangeIndex;
		GameSystem.Instance.Post2ExchangeGold (_exchangeIndex);

	}
	
}

