using UnityEngine;
using System.Collections;
using PathologicalGames;

public class DustCtrl : MonoBehaviour {

    public tk2dSpriteAnimator dustSprite = null;
    public tk2dSprite sprite = null;


    private Vector3 baseScale = new Vector3(1.2f, 1.2f, 1);
    private Vector3 twiceScale = new Vector3(2, 2, 1);
    private Vector3 blackScale = new Vector3(1.5f, 1.5f, 1);

    private readonly string _clipWhite = "ClipDustWhite";
    private readonly string _clipBlack = "ClipDustBlack";
    readonly string _clipAbsorb = "ClipWhiteLightAbsorb";
    readonly string _clipColorfulLight = "ClipColofulLight";

    // Use this for initialization
    void Start() {
        dustSprite.AnimationCompleted += CompleteDelegate;
    }

    public void CompleteDelegate(tk2dSpriteAnimator pSprite, tk2dSpriteAnimationClip pClip) {
        PoolManager.Pools[PuzzleConstBox.objectPool].Despawn(this.transform);
    }

    public void OnSpawned() {
        sprite.scale = baseScale;
    }

    public void Play(bool isWhiteDust) {

        if (isWhiteDust) {
            //dustSprite.Play (PuzzleConstBox.clipDustWhite);
            dustSprite.Play();
        } else {
            sprite.scale = twiceScale;
            //dustSprite.Play (PuzzleConstBox.clipDustBlack);
            dustSprite.Play();
        }

    }

    /// <summary>
    /// 일반 흰색 먼지 효과
    /// </summary>
    public void PlayDustWhite() {
        sprite.scale = baseScale;
        dustSprite.Play(_clipWhite);
    }


    /// <summary>
    /// 
    /// </summary>
    public void PlayWhiteLightAbsorb() {
        sprite.scale = baseScale;
        dustSprite.Play(_clipAbsorb);
    }

    /// <summary>
    /// 
    /// </summary>
    public void PlayColorfulLight() {
        sprite.scale = baseScale;
        dustSprite.Play(_clipColorfulLight);
    }


    /// <summary>
    /// 두배 크기의 흰색 먼지 효과
    /// </summary>
    public void PlayLargeDustWhite() {
        sprite.scale = twiceScale;
        dustSprite.Play(_clipWhite);
    }

    public void PlayDustBlack() {
        sprite.scale = twiceScale;
        dustSprite.Play(_clipBlack);
    }


    
}
