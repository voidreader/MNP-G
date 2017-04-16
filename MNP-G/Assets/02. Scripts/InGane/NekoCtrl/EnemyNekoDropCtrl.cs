using UnityEngine;
using System.Collections;
using PathologicalGames;
using DG.Tweening;

public class EnemyNekoDropCtrl : MonoBehaviour {

	[SerializeField] Transform trDrop;
	[SerializeField] tk2dSprite spDrop;
	private GameObject target;
	private bool isFirstDrop = false;
	private int index;

	public void SetTargetPos(Vector3 pTargetPos, bool pFirstFlag, GameObject pTarget) {

		index = Random.Range (0, 11);
		isFirstDrop = pFirstFlag;
		target = pTarget;

		if (index <= 1) {
			spDrop.SetSprite (PuzzleConstBox.listEnemyNekoAttackDrop [2]);
		} else if (index > 1 && index <= 4) {
			spDrop.SetSprite (PuzzleConstBox.listEnemyNekoAttackDrop [1]);
		} else {
			spDrop.SetSprite (PuzzleConstBox.listEnemyNekoAttackDrop [0]);
		}

		trDrop.DOMove (pTargetPos, 0.5f).OnComplete(OnCompleteMove);
	}

	private void OnCompleteMove() {
		// 첫번째 방울에 닿으면 아이스로 처리 
		/*
		if(isFirstDrop)
			target.SendMessage ("SetBlockCover", BlockCover.Ice);
		*/

		PoolManager.Pools [PuzzleConstBox.objectPool].Despawn (this.transform);
	}
}
