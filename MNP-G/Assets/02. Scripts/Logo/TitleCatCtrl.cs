using UnityEngine;
using System.Collections;
using DG.Tweening;

public class TitleCatCtrl : MonoBehaviour {

	//private Sequence mySeq;
	private float jumpPower;
	private float jumpTime;
	private float originPosY;
	private Vector3 initPos;

	void Start() {
		initPos = this.transform.localPosition;
	}

	private void Jump() {
        initPos = this.transform.localPosition;

        this.transform.localPosition = initPos;

		jumpTime = Random.Range (0.2f, 0.4f);
		jumpPower = Random.Range (0.1f, 0.3f);
		
		originPosY = this.transform.localPosition.y;
		
		Sequence mySeq = DOTween.Sequence ();
		//mySeq.Append (this.transform.DOMoveY (originPosY+1f, jumpTime).SetEase (Ease.InBack));
		//mySeq.Append (this.transform.DOMoveY (originPosY, jumpTime).SetEase (Ease.OutBounce));

		mySeq.Append (this.transform.DOLocalMoveY (originPosY+jumpPower, jumpTime).SetEase (Ease.OutCubic));
		mySeq.Append (this.transform.DOLocalMoveY (originPosY, jumpTime).SetEase (Ease.InCubic));
		mySeq.Play ().SetLoops (-1);
	}
}
