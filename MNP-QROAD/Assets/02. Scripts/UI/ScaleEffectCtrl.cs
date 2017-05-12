using UnityEngine;
using System.Collections;
using DG.Tweening;

public class ScaleEffectCtrl : MonoBehaviour {

	
	void OnEnable() {

		this.transform.DOKill ();
        this.transform.localScale = GameSystem.Instance.BaseScale;
		this.transform.DOScale (1.08f, 0.5f).SetLoops (-1, LoopType.Yoyo);

	}
}
