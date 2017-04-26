using UnityEngine;
using System.Collections;

public class SKUCtrl : MonoBehaviour {

	[SerializeField] string _sku;
	[SerializeField] string _price;
	[SerializeField] UILabel _lblPrice;
	[SerializeField] UILabel _lblGem;
	[SerializeField] int _gemCount;

	// Use this for initialization
	void Start () {
	
	}

	public string SKU {
		get {
			return this._sku;
		} 
		set {
			this._sku = value;
		}
	}

	public void SetSKUInfo(string pSKU, string pPrice) {
		_sku = pSKU;
		_price = pPrice;
		_lblPrice.text = _price;

        if (_sku.Contains("g1")) {
            _gemCount = 300;
        }
        else if (_sku.Contains("g2")) {
            _gemCount = 530;
        }
        else if (_sku.Contains("g3")) {
            _gemCount = 1100;
        }
        else if (_sku.Contains("g4")) {
            _gemCount = 3400;
        }
        else if (_sku.Contains("g5")) {
            _gemCount = 5800;
        }
        

		_lblGem.text = _gemCount.ToString ("#,###");
	}

    public void Purchase() {

        LobbyCtrl.Instance.Purchase(_sku);

    }


}
