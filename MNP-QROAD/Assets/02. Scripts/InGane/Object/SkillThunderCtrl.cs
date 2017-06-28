using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using PathologicalGames;
using System;


public class SkillThunderCtrl : MonoBehaviour {

    BlockCtrl _target;
    int _moveCount;
    Action<SkillThunderCtrl> ActionRequestTarget = delegate { };


    /// <summary>
    /// 번개 타겟 설정, 이동 
    /// </summary>
    /// <param name="pBC"></param>
    /// <param name="pActionRequestTarget"></param>
    public void SetThunderTarget(BlockCtrl pBC, Action<SkillThunderCtrl> pActionRequestTarget) {

        this.transform.DOKill();
            

        // 타켓이 잡히면 이동한다. 
        _target = pBC;
        ActionRequestTarget = pActionRequestTarget;

        this.transform.DOMove(_target.transform.position, 1f).OnComplete(OnCompleteMove);
        this.transform.DOLocalRotate(new Vector3(0, 0, 720), 1, RotateMode.FastBeyond360).SetEase(Ease.Linear);

        
    }


    /// <summary>
    /// 이동 완료 
    /// </summary>
    void OnCompleteMove() {
        _moveCount--;


        // 대상 타겟이 '여전히' 스페셜 블록인지 체크한다.
        if (_target.IsFishGrill || _target.IsStone || _target.IsCookie) {
            DestroySpecialBlock(_target);
            DespawnItself();
            return;
        }
        else { // 움직이는 사이에 스페셜 블록이 '아니게' 된 경우. 

            if(_moveCount > 0) { // 3번까지만 허용한다.
                // InGameCtrl 에게서 다시 요청 
                ActionRequestTarget(this);
                ActionRequestTarget = delegate { };
            }
        }
    }




    /// <summary>
    /// 스페셜 블록의 파괴 처리 
    /// </summary>
    /// <param name="pBC"></param>
    void DestroySpecialBlock(BlockCtrl pBC) {

        if (pBC.IsFishGrill)
            pBC.GrillFish();
        else if (pBC.IsStone)
            pBC.BreakStone();
        else if (pBC.IsCookie)
            pBC.DestroyCookie();

    }

    public void DespawnItself() {
        PoolManager.Pools[PuzzleConstBox.objectPool].Despawn(this.transform);
    }

    void OnDespawned() {
        this.transform.DOKill();
    }

    void OnSpawned() {
        this.transform.localScale = GameSystem.Instance.BaseScale;
        _moveCount = 3;
    }
    

}
