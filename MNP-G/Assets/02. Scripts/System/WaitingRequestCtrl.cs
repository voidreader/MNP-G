using UnityEngine;
using System.Collections;
using DG.Tweening;

public class WaitingRequestCtrl : MonoBehaviour {

	[SerializeField] Transform _waiting;

	// Use this for initialization
	void Start () {
		
	}
	
	void OnEnable() {
		_waiting.DORotate (new Vector3 (0, 0, -720), 2, RotateMode.FastBeyond360).SetEase (Ease.Linear).SetLoops (-1, LoopType.Restart);
	}
}
