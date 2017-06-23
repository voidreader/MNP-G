using UnityEngine;
using System.Collections;
using PathologicalGames;
using DG.Tweening;

public class FootFragCtrl : MonoBehaviour {

	private Vector3 targetPos = Vector3.zero; // Target Position 
	private Vector3 disVec = Vector3.zero;
	private bool isTargetSet = false;

	public tk2dSprite spFootFrag = null;


    bool _isBig = false;
    readonly Vector3 BIG_SCALE = new Vector3(2,2,2);

    bool _isAbsorbFruit = false;
    int _blockID = -1;

    public bool IsBig {
        get {
            return _isBig;
        }

        set {
            _isBig = value;
        }
    }

    void Update() {
		if (!isTargetSet)
			return;

        if(_isAbsorbFruit) {
            targetPos = PlayerCatManagerCtrl.Instance.GetTargetCatPos(_blockID);
        }
        else {
            targetPos = EnemyNekoManager.Instance.GetCurrentNekoPosition();
        }

		

		disVec = (targetPos - transform.position).normalized;
		//transform.position += disVec * Time.deltaTime * Random.Range (PuzzleConstBox.minFootSpeed, PuzzleConstBox.maxFootSpeed);
		transform.position += disVec * Time.deltaTime * 7;
        

    }


    /// <summary>
    /// 스프라이트 설정 및 타겟으로 이동 시작 
    /// </summary>
	public void SetTargetPos() {
        IsBig = false;

        spFootFrag.SetSprite (PuzzleConstBox.listFruitClip [GameSystem.Instance.UserPowerLevel -1]);
        spFootFrag.scale = GameSystem.Instance.BaseScale;
        isTargetSet = true;

        // 빙글빙글 
        this.transform.DOKill();
        this.transform.DOLocalRotate(new Vector3(0, 0, 360), 0.5f, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart);
        
    }


    /// <summary>
    /// 흡수용 과일 파편 
    /// </summary>
    /// <param name="pBlockID"></param>
    public void SetTargetPos(int pBlockID) {
        IsBig = false;

        _isAbsorbFruit = true;
        _blockID = pBlockID;

        spFootFrag.SetSprite(PuzzleConstBox.listFruitClip[GameSystem.Instance.UserPowerLevel - 1]);
        spFootFrag.scale = GameSystem.Instance.BaseScale;
        isTargetSet = true;

        // 빙글빙글 
        this.transform.DOKill();
        this.transform.DOLocalRotate(new Vector3(0, 0, 360), 0.5f, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart);
    }


    /// <summary>
    ///  큰 과일 설정
    /// </summary>
    public void SetBigFruit() {
        IsBig = true;

        spFootFrag.SetSprite(PuzzleConstBox.listFruitClip[GameSystem.Instance.UserPowerLevel - 1]);
        spFootFrag.scale = BIG_SCALE;
        isTargetSet = true;

        // 빙글빙글 
        this.transform.DOKill();
        this.transform.DOLocalRotate(new Vector3(0, 0, 360), 0.5f, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart);
    }

	private void OnCompleteMove() {
		//PoolManager.Pools [PuzzleConstBox.objectPool].Despawn (this.transform);
	}


	/// <summary>
	/// Raises the trigger enter event.
	/// 충돌될때, 파티클 이펙트 구현 
	/// </summary>
	/// <param name="pCol">P col.</param>
	void OnTriggerEnter(Collider pCol) {


        if (!pCol.CompareTag(PuzzleConstBox.tagEnemyNeko) && !pCol.CompareTag(PuzzleConstBox.tagCloneEnemyNeko)
            && !pCol.CompareTag(PuzzleConstBox.tagInGamePlayerTopCat))
            return;

        // 상단 플레이어 고양이와 충돌한 경우 
        if(pCol.CompareTag(PuzzleConstBox.tagInGamePlayerTopCat)) {

            if (PoolManager.Pools[PuzzleConstBox.objectPool].IsSpawned(this.transform)) {

                PoolManager.Pools[PuzzleConstBox.objectPool].Despawn(this.transform);

                InSoundManager.Instance.PlayAbsorbFruit(); // 사운드 플레이 

                // 효과
                PoolManager.Pools[PuzzleConstBox.objectPool].Spawn(PuzzleConstBox.prefabDust, this.transform.position, Quaternion.identity).GetComponent<DustCtrl>().PlayDustWhite();
                //SpawnFragmentHit();
            }

            return;
        }


        // 클리어 이후에 발생하는 과일 제거 용도 
        if(pCol.tag == PuzzleConstBox.tagCloneEnemyNeko) {
            if (PoolManager.Pools[PuzzleConstBox.objectPool].IsSpawned(this.transform)) {
                PoolManager.Pools[PuzzleConstBox.objectPool].Despawn(this.transform);
            }
        }



        if (PoolManager.Pools[PuzzleConstBox.objectPool].IsSpawned(this.transform)) {

            PoolManager.Pools[PuzzleConstBox.objectPool].Despawn(this.transform);

            InSoundManager.Instance.PlayNekoSmallHitSound(); // 작은 히트 사운드
            

            PoolManager.Pools[PuzzleConstBox.objectPool].Spawn(PuzzleConstBox.prefabDust, this.transform.position, Quaternion.identity).GetComponent<DustCtrl>().PlayDustWhite();


            SpawnFragmentHit();
		}
	}


	private void SpawnFragmentHit() {
		for (int i=0; i<4; i++) {
			PoolManager.Pools[PuzzleConstBox.objectPool].Spawn(PuzzleConstBox.prefabFragmentHit, EnemyNekoManager.Instance.GetCurrentNekoPosition(), Quaternion.identity);
		}
	}

	void OnDespawned() {
		isTargetSet = false;

        this.transform.DOKill();
    }

}
