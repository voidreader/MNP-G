using UnityEngine;
using System.Collections;

public class NekoRewardGiftCtrl : MonoBehaviour {

	private Transform nekoTarget;
	private Camera worldCam = null;
	private Camera guiCam = null;
	
	private bool isInit = false;
	private Vector3 pos = Vector3.zero;  // hp가 위치할 좌표
	private Vector3 targetPos = Vector3.zero; // 타겟의 월드좌표 


	// Update is called once per frame
	void Update () {
		if (!isInit)
			return;
		
		targetPos = nekoTarget.position;
		//targetPos.x = targetPos.x - 0.35f;
		targetPos.y = targetPos.y + 0.8f;
		
		//타겟의 포지션을 월드좌표에서 ViewPort좌표로 변환하고 다시 ViewPort좌표를 NGUI월드좌표로 변환합니다.
		//pos = guiCam.ViewportToWorldPoint (worldCam.WorldToViewportPoint (nekoTarget.transform.position));
		pos = guiCam.ViewportToWorldPoint (worldCam.WorldToViewportPoint (targetPos));
		pos.z = 0;
		
		transform.position = pos;
	}

	public void SetTargetNeko(Transform pTarget) {


		this.gameObject.SetActive (true);
		nekoTarget = pTarget;
		worldCam = NGUITools.FindCameraForLayer (pTarget.gameObject.layer);
		guiCam = NGUITools.FindCameraForLayer (this.gameObject.layer);

		isInit = true;
	}

	public void HideSprite() {
		isInit = false;
		this.gameObject.SetActive (false);
	}
}
