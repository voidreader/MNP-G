using UnityEngine;
using System.Collections;
using PathologicalGames;
using DG.Tweening;

public class UIPopupCtrl : MonoBehaviour {


	/// <summary>
	/// 시작하자마자 Disable 상태로 처리 
	/// </summary>
	void Awake() {
		if(!this.CompareTag("NoDisable"))
			SetDisable ();
	}

	void OnEnable() {
		Open ();
	}


	private void Open() {
		this.transform.DOScale (1, 0.15f).SetEase (Ease.OutBack).OnComplete(OnCompleteOpen);
	}

	public void Close() {


		this.transform.DOScale (0, 0.15f).SetEase (Ease.InBack).OnComplete (SetDisable);
	}

	private void OnCompleteOpen() {

	}


	/// <summary>
	/// Object Disable
	/// </summary>
	void SetDisable() {
		this.transform.position = Vector3.zero; // 위치를 기본 위치로 옮겨놓는다. 
		this.transform.localScale = Vector3.zero;
		this.gameObject.SetActive (false);
	}

	public void Resume() {
		InUICtrl.Instance.Resume ();
		//this.Close ();
	}


}
