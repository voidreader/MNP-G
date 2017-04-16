using UnityEngine;
using System.Collections;
using PathologicalGames;
using DG.Tweening;

public class FireworkBoltCtrl : MonoBehaviour {

    float _spawnTime;
    Vector3 _fisrtMovePos;
    bool _isInited = false;


    private Vector3 targetPos = Vector3.zero; // Target Position 
    private Vector3 disVec = Vector3.zero;


    void Update() {

        if (!_isInited)
            return;

        targetPos = EnemyNekoManager.Instance.GetCurrentNekoPosition();

        disVec = (targetPos - transform.position).normalized;
        //transform.position += disVec * Time.deltaTime * Random.Range (PuzzleConstBox.minFootSpeed, PuzzleConstBox.maxFootSpeed);
        transform.position += disVec * Time.deltaTime * 12;
    }

    void OnSpawned() {

        _isInited = false;

        _spawnTime = Random.Range(0.2f, 0.8f);
        _fisrtMovePos = this.transform.position;
        _fisrtMovePos = new Vector3(_fisrtMovePos.x + Random.Range(-0.8f, 0.8f), _fisrtMovePos.y + Random.Range(-0.8f, 0.8f), _fisrtMovePos.z);

        // 소환되면 랜덤한 방향과 회전을 시작한다
        this.transform.DOLocalRotate(new Vector3(0, 0, -720), _spawnTime, RotateMode.FastBeyond360);
        this.transform.DOMove(_fisrtMovePos, _spawnTime).OnComplete(OnCompleteFirstMove);

        InSoundManager.Instance.PlayShootFirework();
    }

    void OnCompleteFirstMove() {
        this.transform.DOKill();

        _isInited = true;

        InSoundManager.Instance.PlayFireworkTail();
    }


    /// <summary>
    /// Raises the trigger enter event.
    /// 충돌될때, 파티클 이펙트 구현 
    /// </summary>
    /// <param name="pCol">P col.</param>
    void OnTriggerEnter(Collider pCol) {

        if (pCol.tag != PuzzleConstBox.tagEnemyNeko && pCol.tag != PuzzleConstBox.tagCloneEnemyNeko)
            return;

        if (PoolManager.Pools[PuzzleConstBox.objectPool].IsSpawned(this.transform)) {

            PoolManager.Pools[PuzzleConstBox.objectPool].Despawn(this.transform);
            InSoundManager.Instance.PlayHitFirework();
            PoolManager.Pools[PuzzleConstBox.objectPool].Spawn(PuzzleConstBox.prefabDust, this.transform.position, Quaternion.identity).GetComponent<DustCtrl>().PlayDustWhite();
            SpawnFragmentHit();
        }
    }


    private void SpawnFragmentHit() {
        for (int i = 0; i < 4; i++) {
            PoolManager.Pools[PuzzleConstBox.objectPool].Spawn(PuzzleConstBox.prefabFragmentHit, EnemyNekoManager.Instance.GetCurrentNekoPosition(), Quaternion.identity);
        }
    }


}


