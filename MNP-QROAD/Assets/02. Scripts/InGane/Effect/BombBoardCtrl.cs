using UnityEngine;
using System.Collections;
using PathologicalGames;

public class BombBoardCtrl : MonoBehaviour {

	private tk2dSpriteAnimator spEffect = null;

	// Use this for initialization
	void Start () {
		if (spEffect == null) {
			spEffect = this.gameObject.GetComponent<tk2dSpriteAnimator>();
			spEffect.AnimationCompleted += AnimationCompleteDelegate;
		}
	}


	void OnSpawned() {

		if (spEffect == null) {
			spEffect = this.gameObject.GetComponent<tk2dSpriteAnimator>();
			spEffect.AnimationCompleted += AnimationCompleteDelegate;
		}

		spEffect.Play ();

		// 사운드 처리
		InSoundManager.Instance.PlayBombBoard ();
	}

	public void AnimationCompleteDelegate(tk2dSpriteAnimator pSprite, tk2dSpriteAnimationClip pClip) {
		PoolManager.Pools [PuzzleConstBox.objectPool].Despawn (this.transform);
	}

}
