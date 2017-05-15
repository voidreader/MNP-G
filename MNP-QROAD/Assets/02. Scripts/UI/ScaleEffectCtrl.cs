using UnityEngine;
using System.Collections;
using DG.Tweening;

public class ScaleEffectCtrl : MonoBehaviour {

    public float _scaleSize = 0;
	
	void OnEnable() {

        if (_scaleSize <= 0)
            _scaleSize = 1.08f;

		this.transform.DOKill ();
        this.transform.localScale = GameSystem.Instance.BaseScale;
		this.transform.DOScale (_scaleSize, 0.5f).SetLoops (-1, LoopType.Yoyo);

	}
}
