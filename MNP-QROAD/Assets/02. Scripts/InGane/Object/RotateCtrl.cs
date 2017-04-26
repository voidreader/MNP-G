using UnityEngine;
using System.Collections;
using DG.Tweening;

public class RotateCtrl : MonoBehaviour {

	void OnEnable() {
		this.transform.DOKill ();
		this.transform.DORotate (new Vector3 (0, 0, 360), 3, RotateMode.FastBeyond360).SetEase (Ease.Linear).SetLoops (-1, LoopType.Restart);
	}

}
