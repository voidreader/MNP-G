using UnityEngine;
using System.Collections;
using PathologicalGames;
using DG.Tweening;

public class BlockDestroyEffectCtrl : MonoBehaviour {

	[SerializeField] tk2dSpriteAnimator _spriteAnimator;
	[SerializeField] tk2dSprite _sprite;
    [SerializeField] Transform _transform;

    readonly string _baseClip = "ClipBlockDestroy";
    
    private Vector3 twiceScale = new Vector3(1.5f, 1.5f, 1);

    void Awake() {
        _transform = this.transform;
    }

	// Use this for initialization
	void Start () {
        

        _spriteAnimator.AnimationCompleted += CompleteDelegate;
	}
	
	public void CompleteDelegate(tk2dSpriteAnimator pSprite, tk2dSpriteAnimationClip pClip) {
		PoolManager.Pools[PuzzleConstBox.objectPool].Despawn(this.transform);
	}

	public void OnSpawned() {
		//_sprite.Play ();
	}

	public void Play() {
		_sprite.scale = PuzzleConstBox.baseScale;
		_spriteAnimator.Play (_baseClip);
        
	}

    public void Play(int _blockID) {
        _sprite.scale = PuzzleConstBox.baseScale;
        _spriteAnimator.Play(PuzzleConstBox.listDestroyBlockClip[_blockID]);
    }


    /// <summary>
    /// 매치 개수에 따라 다르게 폭파 한다.
    /// </summary>
    /// <param name="pBlockID"></param>
    /// <param name="pType"></param>
    public void Play(int pBlockID, int pType) {
        _sprite.scale = PuzzleConstBox.baseScale;
        _transform.localScale = PuzzleConstBox.baseScale;

        // 크기 조절 
        _transform.DOKill();
        _transform.DOScale(1.3f, 0.2f).SetLoops(2, LoopType.Yoyo);

        if (pType == 2) {
            _spriteAnimator.Play(PuzzleConstBox.listDestroyBlockClip[pBlockID]); // 2개
        }
        else if(pType == 3) {
            _spriteAnimator.Play(PuzzleConstBox.listDestroyBlockClip2[pBlockID]); // 3개
        }
        else if (pType == 4) {
            _spriteAnimator.Play(PuzzleConstBox.listDestroyBlockClip3[pBlockID]); // 4개
        }
    }

    
	public void PlayFever() {
		_sprite.scale = twiceScale;
		_spriteAnimator.Play (_baseClip);
	}

    public void PlayFever(int _blockID) {
        _sprite.scale = twiceScale;
        _spriteAnimator.Play(PuzzleConstBox.listDestroyBlockClip[_blockID]);
    }


    public void PlayFever(int pBlockID, int pType) {
        _sprite.scale = twiceScale;

        if (pType == 2) {
            _spriteAnimator.Play(PuzzleConstBox.listDestroyBlockClip[pBlockID]);
        }
        else if (pType == 3) {
            _spriteAnimator.Play(PuzzleConstBox.listDestroyBlockClip2[pBlockID]);
        }
        else if (pType == 4) {
            _spriteAnimator.Play(PuzzleConstBox.listDestroyBlockClip3[pBlockID]);
        }
    }


}
