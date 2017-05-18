using UnityEngine;
using System.Collections;
using DG.Tweening;
using PathologicalGames;

public class MovingCatCtrl : MonoBehaviour {


    [SerializeField] Transform _head;
    [SerializeField] Transform _body;

    Vector3 _rightRot = new Vector3(0, 0, 4);
    Vector3 _leftRot = new Vector3(0, 0, -4);


    /// <summary>
    /// Idle 상태 설정 
    /// </summary>
    public void SetIdle() {
        _head.DOKill();
        _body.DOKill();


        //this.transform.DOJump(this.transform.position, this.transform.position.y + 0.5f, 1, 0.3f).OnComplete(OnCompleteIdleJumping);
        this.transform.DOJump(this.transform.position, 0.5f, 1, 0.3f).OnComplete(OnCompleteIdleJumping);

    }

    void OnCompleteIdleJumping() {
        _head.DOKill();
        _body.DOKill();
        _head.localEulerAngles = _leftRot;
        _head.DOLocalRotate(_rightRot, 0.5f, RotateMode.Fast).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);

        // 안녕!
    }

    /// <summary>
    /// 점핑 
    /// </summary>
    /// <param name="pPos"></param>
    public void SetJumping(Vector3 pPos) {
        _head.DOKill();
        //this.transform.DOJump(pPos, this.transform.position.y + 0.3f, 1, 0.15f);
        this.transform.DOJump(pPos, 0.3f, 1, 0.15f);
        InSoundManager.Instance.PlayMovingJump();
    }

    public void SetLastJumping(Vector3 pPos) {
        _head.DOKill();
        //this.transform.DOJump(pPos, this.transform.position.y + 0.3f, 1, 0.15f).OnComplete(OnCompleteJumping);
        this.transform.DOJump(pPos, 0.3f, 1, 0.15f).OnComplete(OnCompleteJumping);
        InSoundManager.Instance.PlayMovingJump();
    }


    void OnCompleteJumping() {

        // 고마워 띄우기

        this.transform.DOLocalMoveY(this.transform.localPosition.y + 1.5f, 1);
        InSoundManager.Instance.PlayMovingJumpLast();

        // 먼지 이펙트
        PoolManager.Pools[PuzzleConstBox.objectPool].Spawn(PuzzleConstBox.prefabDust, this.transform.position, Quaternion.identity).GetComponent<DustCtrl>().PlayDustWhite();

        Invoke("DelayedDespawn", 1);
    }

    void DelayedDespawn() {
        PoolManager.Pools["Blocks"].Despawn(this.transform);
    }

    void OnSpawned() {

        this.transform.localScale = GameSystem.Instance.BaseScale;

        _head.DOKill();
        _body.DOKill();

        // 머리 위치 조정 
        _head.localPosition = new Vector3(0, 0.7f, 0);
        _head.localEulerAngles = Vector3.zero;

    }
}
