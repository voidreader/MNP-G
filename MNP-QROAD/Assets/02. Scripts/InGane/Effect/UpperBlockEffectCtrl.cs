using UnityEngine;
using System.Collections;
using PathologicalGames;

public class UpperBlockEffectCtrl : MonoBehaviour {

    [SerializeField] tk2dSpriteAnimator _animator;
    readonly string _clipBlockDestory = "ClipBlockDestroy";

    // clipBlockDestroy

    void Start() {
        _animator.AnimationCompleted += EffectCompleteDelegate;
    }

    /// <summary>
    /// 
    /// </summary>
    public void PlayStoneDestory() {
        InSoundManager.Instance.PlayIceBlockBreak();
        _animator.Play(PuzzleConstBox.clipStoneBreak);
    }

    public void PlayBlockDestroy() {
        _animator.Play(_clipBlockDestory);
    }


    void EffectCompleteDelegate(tk2dSpriteAnimator pSprite, tk2dSpriteAnimationClip pClip) {
        PoolManager.Pools["Blocks"].Despawn(this.transform);
    }
        
}
