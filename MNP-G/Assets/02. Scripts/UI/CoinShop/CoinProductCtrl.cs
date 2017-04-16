using UnityEngine;
using System.Collections;

public class CoinProductCtrl : MonoBehaviour {

    [SerializeField] int _productIndex = -1;

    [SerializeField] int _originalPrice = 0;
    [SerializeField] int _bargainPrice = 0;

    [SerializeField] UILabel _lblOriginalPrice = null;
    [SerializeField] UILabel _lblBargainPrice = null;

    [SerializeField] GameObject _groupBargain;

    Vector3 _originalPricePos = new Vector3(-40, 0, 0);
    Vector3 _movedOriginalPricePos = new Vector3(-50, 20, 0);

    void OnEnable() {
        if (_productIndex < 0)
            return;

        _originalPrice = PuzzleConstBox.listCoinShopOriginalPrices[_productIndex];
        _bargainPrice = GameSystem.Instance.ListCoinShopPrice[_productIndex];

        _lblOriginalPrice.text = GameSystem.Instance.GetNumberToString(_originalPrice);
        _lblBargainPrice.text = GameSystem.Instance.GetNumberToString(_bargainPrice);


        // 할인여부에 따른 이동, 활성, 비활성화 처리 
        if (_originalPrice < _bargainPrice) {
            _groupBargain.SetActive(true);
            _lblOriginalPrice.transform.localPosition = _movedOriginalPricePos;
        }
        else {
            _groupBargain.SetActive(false);
            _lblOriginalPrice.transform.localPosition = _originalPricePos;
        }


    }
	
	
}
