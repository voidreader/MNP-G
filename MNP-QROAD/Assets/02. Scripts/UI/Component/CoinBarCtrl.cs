using UnityEngine;
using System.Collections;
using DG.Tweening;

public class CoinBarCtrl : MonoBehaviour {

    [SerializeField] UISprite _icon;
    [SerializeField] UILabel _lblValue;
    [SerializeField] int _value;
    [SerializeField] GameObject _bargainMark;

    private bool _isInit = false;

    void Awake() {

        _value = 0;
        _lblValue.text = GameSystem.Instance.GetNumberToString(_value);

        // 할인 마크 표시 
        if(PuzzleConstBox.listCoinShopOriginalPrices[0] < GameSystem.Instance.ListCoinShopPrice[0]) {
            _bargainMark.SetActive(true);
        }
        else {
            _bargainMark.SetActive(false);
        }

    }

    public void SetCoinBar(int pValue) {
        if (pValue == _value)
            return;


        if (LobbyCtrl.Instance != null && _isInit) {
            LobbyCtrl.Instance.PlayNekoRewardGet();
        }

        OnCompleteScale();
        _icon.transform.DOShakeScale(0.5f, 1, 10, 90).OnComplete(OnCompleteScale);

        _value = pValue;
        _lblValue.text = GameSystem.Instance.GetNumberToString(_value);

        if (!_isInit)
            _isInit = true;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="pValue"></param>
    public void SetValue(int pValue) {
        _value = pValue;
        _lblValue.text = GameSystem.Instance.GetNumberToString(_value);
    }

    private void OnCompleteScale() {
        _icon.transform.DOKill();
        _icon.transform.localScale = PuzzleConstBox.baseScale;
    }

    public void AddCoin(int pValue) {
    }

}
