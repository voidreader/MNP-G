using UnityEngine;
using System.Collections;
using DG.Tweening;

public class GemBarCtrl : MonoBehaviour {

    [SerializeField]
    UISprite _icon;
    [SerializeField]
    UILabel _lblValue;
    [SerializeField]
    int _value;

    [SerializeField]
    GameObject _btnShop;
        

    private bool _isInit = false;


    void Awake() {

        _value = 0;
        _lblValue.text = GameSystem.Instance.GetNumberToString(_value);

#if !UNITY_IOS && !UNITY_ANDROID

        _btnShop.SetActive(false);

#endif

    }

    public void SetGemBar(int pValue) {
        if (pValue == _value)
            return;

        if (LobbyCtrl.Instance != null && _isInit) {
            LobbyCtrl.Instance.PlayNekoRewardGet();
        }

        _icon.transform.DOShakeScale(0.5f, 1, 10, 90).OnComplete(OnCompleteScale);

        _value = pValue;
        _lblValue.text = GameSystem.Instance.GetNumberToString(_value);

        if (!_isInit)
            _isInit = true;
    }

    private void OnCompleteScale() {
        _icon.transform.localScale = PuzzleConstBox.baseScale;
    }

    public void AddGem(int pValue) {
    }

}
