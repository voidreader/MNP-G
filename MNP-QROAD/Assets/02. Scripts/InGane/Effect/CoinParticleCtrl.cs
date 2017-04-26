using UnityEngine;
using System.Collections;
using PathologicalGames;
using DG.Tweening;

public class CoinParticleCtrl : MonoBehaviour {

	private Vector3 destPos = new Vector3(-2.95f, 5.8f, 0);

	// Use this for initialization
	void Start () {
	
	}


	public void OnSpawned() {
		this.transform.DOMove (destPos, Random.Range (0.3f, 0.8f)).OnComplete(OnCompleteMove);
	}
	
	public void OnCompleteMove() {
		PoolManager.Pools [PuzzleConstBox.objectPool].Despawn (this.transform);
		
		// Coin FX 발동시킨다. 
		InUICtrl.Instance.PlayCoinFx (); // 이 메소드에서 코인 증가 처리 
	}

}
