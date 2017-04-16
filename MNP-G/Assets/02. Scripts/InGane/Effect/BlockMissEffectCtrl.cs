using UnityEngine;
using System.Collections;
using PathologicalGames;
using DG.Tweening;

public class BlockMissEffectCtrl : MonoBehaviour {

	[SerializeField] private tk2dSprite _sprite;

    readonly string _spriteMissPenalty = "text-miss-m1sec";
	private string _spMissText = "002-miss";
	private bool isShieldUsed = false;



    float _showTime = 0.3f;


	
	public void OnSpawned() {
        this.gameObject.SetActive(false);
	}


    /// <summary>
    /// Miss Text 연출 
    /// </summary>
    public void SetMissText() {

        this.gameObject.SetActive(true);
        this.transform.localScale = GameSystem.Instance.BaseScale;
        _sprite.SetSprite(_spMissText);
        _showTime = 0.3f;


        this.transform.DOLocalMoveY(this.transform.position.y + 1, _showTime).SetEase(Ease.Linear).OnComplete(OnCompleteMove);

        // Sound Play
        InSoundManager.Instance.PlayMatchSound(3);
    }

    // Miss Penalty Text 연출 
    public void SetMissPenalty() {

        this.gameObject.SetActive(true);
        this.transform.localScale = GameSystem.Instance.BaseScale;
        _sprite.SetSprite(_spriteMissPenalty);
        _showTime = 0.5f;


        this.transform.DOLocalMoveY(this.transform.position.y + 1, _showTime + 0.2f).SetEase(Ease.Linear).OnComplete(OnCompleteMove);

        InGameCtrl.Instance.MinusGameTime(1);

        // Sound Play
        //InSoundManager.Instance.PlayMatchSound(3);
        
    }

    
	public void OnCompleteMove() {
		PoolManager.Pools [PuzzleConstBox.objectPool].Despawn (this.transform);
	}


}
