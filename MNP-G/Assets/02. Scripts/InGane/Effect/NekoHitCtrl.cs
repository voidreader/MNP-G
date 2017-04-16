using UnityEngine;
using System.Collections;
using DG.Tweening;
using PathologicalGames;

/// <summary>
/// Neko 타격시, 생성되는 별 파편 
/// </summary>
public class NekoHitCtrl : MonoBehaviour {

	[SerializeField]
	private tk2dSpriteAnimator spriteAnimator = null;
	[SerializeField]
	private tk2dSprite sprite = null;


	private string clipMyNekoHit = "HitStarNoAnimation";
	private Vector3 destPos = Vector3.zero;
	private Vector3 twiceScale = new Vector3(2,2,1);
	private Vector3 baseScale = new Vector3(1,1,1);
	private int randomNumber;

	// Use this for initialization
	void Start () {

	}
	
	public void SetHitType(NekoHitType pNekoHitType) {

		if(pNekoHitType == NekoHitType.MyNekoHit) {

			sprite.scale = twiceScale;
			spriteAnimator.Play(clipMyNekoHit);
			SpreadMyNekoHit();
		}

	}

	/// <summary>
	/// MyNekoHit Type 별 파편 퍼뜨리기 
	/// </summary>
	private void SpreadMyNekoHit() {

		// 좌우를 정한다. 
		randomNumber = Random.Range (0, 2);

		if (randomNumber % 2 == 0)
			destPos.x = Random.Range (-10f, -4f);
		else 
			destPos.x = Random.Range (4f, 10f);

		// 위 아래를 정한다. 
		randomNumber = Random.Range (0, 2);
		
		if (randomNumber % 2 == 0)
			destPos.y = -8;
		else 
			destPos.y = 8;

		destPos.z = 0;

		this.transform.DOMove (destPos, 1f).SetEase (Ease.OutCubic).OnComplete(OnCompleteMove);
	}

	private void OnCompleteMove() {
		PoolManager.Pools [PuzzleConstBox.objectPool].Despawn (this.transform);
	}

	private void OnSpawned() {
		sprite.scale = baseScale;
	}
}

