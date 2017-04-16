using UnityEngine;
using System.Collections;
using PathologicalGames;
using DG.Tweening;

public class BlockNavigatorCtrl : MonoBehaviour {

    [SerializeField]
    tk2dSpriteAnimator _innerCircle;

    [SerializeField]
    Transform _finger;

    [SerializeField]
    tk2dSprite _fingerSprite;

    [SerializeField]
    Vector3 _debugLocalPosition;

    Vector3 _originFingerPos = new Vector3(0.8f, 0.7f, 0);


    Vector3 _rightFingerPos = new Vector3(0.8f, 0.7f, 0);
    Vector3 _leftFingerPos = new Vector3(-0.8f, 0.7f, 0);

    Vector3 _rightMovePos = new Vector3(0.7f, 0.6f, 0);
    Vector3 _leftMovePos = new Vector3(-0.7f, 0.6f, 0);


    void OnSpawned() {
        _innerCircle.Play();
        _finger.DOKill();
        _finger.localPosition = _originFingerPos;

        _debugLocalPosition = this.transform.localPosition;



        SetFlipMovement();

        //_finger.DOLocalMove(new Vector3(0.7f, 0.6f, 0), 0.5f).SetLoops(-1, LoopType.Restart);
        
    }

    /// <summary>
    /// 기준 좌표를 중심으로 좌우 Flip move
    /// </summary>
    private void SetFlipMovement() {

        if (this.transform.localPosition.x > 2.5f) {
            _finger.localPosition = _leftFingerPos;
            _fingerSprite.scale = new Vector3(-1, 1, 1);
            _finger.DOLocalMove(_leftMovePos, 0.5f).SetLoops(-1, LoopType.Restart);
            return;
        }
        else {
            _finger.localPosition = _rightFingerPos;
            _fingerSprite.scale = new Vector3(1, 1, 1);
            _finger.DOLocalMove(_rightMovePos, 0.5f).SetLoops(-1, LoopType.Restart);
            return;
        }

    }
	
}
