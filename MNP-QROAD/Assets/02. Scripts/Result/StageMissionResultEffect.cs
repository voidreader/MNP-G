using UnityEngine;
using System.Collections;
using DG.Tweening;

public class StageMissionResultEffect : MonoBehaviour {

    public GameObject _clear;
    public GameObject _fail;


    [SerializeField] Transform _cloud;
    [SerializeField] Transform _failText;

    [SerializeField] Transform _aura;
    [SerializeField] Transform _ClearText;

    public void SetFail() {
        this.gameObject.SetActive(true);
        _fail.SetActive(true);
        _clear.SetActive(false);

        _cloud.DOLocalMoveY(140, 0.5f).SetLoops(-1, LoopType.Yoyo);
        _failText.DOLocalMoveY(270, 0.5f).SetLoops(-1, LoopType.Yoyo);
    }

    public void SetClear() {
        this.gameObject.SetActive(true);
        _fail.SetActive(false);
        _clear.SetActive(true);


        _aura.DOLocalRotate(new Vector3(0, 0, 360), 1, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
        _ClearText.DOScale(1.1f, 0.5f).SetLoops(-1, LoopType.Yoyo);
    }
}
