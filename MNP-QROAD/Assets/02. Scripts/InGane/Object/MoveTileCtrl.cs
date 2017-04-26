using UnityEngine;
using System.Collections;
using PathologicalGames;

public class MoveTileCtrl : MonoBehaviour {


    bool _isLastSpot;
    bool _isStartSpot;

    [SerializeField] tk2dSpriteAnimator _tile;

    #region Properties 
    public bool IsLastSpot {
        get {
            return _isLastSpot;
        }

        set {
            _isLastSpot = value;
        }
    }

    public bool IsStartSpot {
        get {
            return _isStartSpot;
        }

        set {
            _isStartSpot = value;
        }
    }
    #endregion


    void Awake() {

        _tile.AnimationCompleted += AnimationCompleteDelegate;
    }

    /// <summary>
    /// 시작지점
    /// </summary>
    public void SetStartSpot() {
        _tile.Play(PuzzleConstBox.clipMoveStart);

        IsStartSpot = true;
    }

    /// <summary>
    /// 종료지점
    /// </summary>
    public void SetLastSpot() {
        _tile.Play(PuzzleConstBox.clipMoveEnd);

        IsLastSpot = true;
    }

    public void PushTile() {
        _tile.Play(PuzzleConstBox.clipMoveTilePush);
           
    }

    void OnSpawned () {
        IsStartSpot = false;
        IsLastSpot = false;

        _tile.Play(PuzzleConstBox.clipMoveTile);
    }
        



    void AnimationCompleteDelegate(tk2dSpriteAnimator pSprite, tk2dSpriteAnimationClip pClip) {
        if (pClip.name == PuzzleConstBox.clipMoveTilePush)
            _tile.Play(PuzzleConstBox.clipMoveTile);
    }
}
