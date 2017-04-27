using UnityEngine;
using System.Collections;
using PathologicalGames;

public class FragmentHitCtrl : MonoBehaviour {

	[SerializeField] private tk2dSprite spFragment;

	private Vector3 rightForce = new Vector3(100, 250, 0);
	private Vector3 leftForce = new Vector3(-100, 250, 0);
	[SerializeField] Rigidbody rigid;


	// Use this for initialization
	void Start () {

	}


	void OnSpawned() {
		SetFragmentHit ();
	}


	private void SetFragmentHit() {

		// 양쪽의 방향으로 addforce 
		spFragment.SetSprite (PuzzleConstBox.listFragmentNekoHitClip [Random.Range (0, PuzzleConstBox.listFragmentNekoHitClip.Count)]);

		rigid.velocity = Vector3.zero;


		if (Random.Range (0, 2) % 2 == 0) {
			rigid.AddForce (rightForce);
		} else {
			rigid.AddForce (leftForce);
		}

		PoolManager.Pools [PuzzleConstBox.objectPool].Despawn (this.transform, 0.8f);

	}

}
