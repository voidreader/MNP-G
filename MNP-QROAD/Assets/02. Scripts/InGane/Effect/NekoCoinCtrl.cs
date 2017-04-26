using UnityEngine;
using System.Collections;
using DG.Tweening;
using PathologicalGames;

public class NekoCoinCtrl : MonoBehaviour {

	public int _value;

	public void OnSpawned() {

		// 임의의 방향으로 AddForce
		this.GetComponent<Rigidbody>().velocity = Vector3.zero;
		this.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-500,500), Random.Range(800,1200), 0));

		_value = 4;

		Invoke ("MoveCoin", 1.5f);

	}

	public void SetValue(int pValue) {
		_value = pValue;
	}

	private void MoveCoin() {
		this.transform.DOMove (GameSystem.Instance.CoinDestPos, Random.Range (0.3f, 0.6f)).OnComplete(OnCompleteMove);
	}

	public void OnCompleteMove() {
		PoolManager.Pools [PuzzleConstBox.objectPool].Despawn (this.transform);
		
		// Coin FX 발동시킨다. 
		InUICtrl.Instance.PlayCoinFx (_value);
	}
}
