using UnityEngine;
using System.Collections;
using DG.Tweening;

public class TitlePopupCtrl : MonoBehaviour {

	void Awake() {
		SetDisable ();
	}
	
	private void Open() {
		
		this.transform.DOScale (1, 0.3f).SetEase (Ease.OutBack).OnComplete(OnCompleteOpen);
	}
	
	// 취소(백버튼)을 통한 종료 
	public void Close() {
		
		this.transform.DOScale (0, 0.3f).SetEase (Ease.InBack).OnComplete (SetDisable);
	}
	
	// 자가 종료 (X버튼 등)
	public void CloseSelf() {

		this.transform.DOScale (0, 0.3f).SetEase (Ease.InBack).OnComplete (SetDisable);
	}
	
	public void DoBeforeClose() {
		

	}
	
	private void OnCompleteOpen() {
		
	}
	
	
	/// <summary>
	/// Object Disable
	/// </summary>
	void SetDisable() {
		
		this.transform.position = Vector3.zero; // 위치를 기본 위치로 옮겨놓는다. 
		this.transform.localPosition = Vector3.zero;
		this.transform.localScale = Vector3.zero;
		this.gameObject.SetActive (false);
	}
}
