using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PathologicalGames;
using DG.Tweening;

public class BlockCtrl : MonoBehaviour {

	public int blockID = -1;
	public int itemID = -1;

    public BlockState currentState = BlockState.None;
    public BlockState preState = BlockState.None;


	[SerializeField] tk2dSprite spBlock; // 블록 본체
	[SerializeField] Transform trBlockSprite; // 블록 Transform 
	[SerializeField] tk2dSpriteAnimator _spCoverBlock; // 블록을 덮고 있는 특수 효과 Sprite 
    [SerializeField] tk2dSpriteAnimator _spUpperEffect;
    [SerializeField] tk2dSpriteAnimator spBlockNavigator; // 네비게이션 효과
    [SerializeField] tk2dSpriteAnimator _spUpperTile; // 상위 타일(쿠키, 바위)

    [SerializeField] GameObject bombFX; // 폭탄 연기 효과 

    [SerializeField] tk2dSprite _spriteTile; // 타일 
    string _originTileSprite; // 타일 복원용도


	public tk2dSpriteAnimator _verticalLine = null; // 라인 이펙트 
    public tk2dSpriteAnimator _horizontalLine = null; // 라인 이펙트 

    public int posX, posY;
	public BlockPos blockPos = new BlockPos ();

	// 매칭 체크 변수 
	public bool isMatchingChecked = false;


	private Vector3 pos;
	private Vector3 smokePos;
    private Vector3 localPos;

    private float _rollDice;

    private int _originDestroyClipType = 2;
    private int _destroyClipType = 2;

    [SerializeField]
    bool _isCookie = false;

    [SerializeField]
    bool _isStone = false;
    bool _isBreakingStone = false; // 이중 파괴를 막기 위해서 사용

    [SerializeField]
    bool _isMovementBlock = false;

    #region Fish Grill 변수 

    [SerializeField] bool _isFishGrill = false;
    List<string> _listGrillOrder = new List<string>(); 
    [SerializeField] int _fishGrillCount = 0; // 굽기 카운트(최초에 3으로 설정)

    #endregion

    #region readonly Vector

    readonly Vector3 _baseScale = new Vector3(1, 1, 1);
    readonly Vector3 _HFlipScale = new Vector3(-1, 1, 1);
    readonly Vector3 _VFlipScale = new Vector3(1, -1, 1);
    readonly Vector3 _HVFlipScale = new Vector3(-1, -1, 1);

    public bool IsCookie {
        get {
            return _isCookie;
        }

        set {
            _isCookie = value;
        }
    }

    public bool IsStone {
        get {
            return _isStone;
        }

        set {
            _isStone = value;
        }
    }

    public bool IsFishGrill {
        get {
            return _isFishGrill;
        }

        set {
            _isFishGrill = value;
        }
    }

    public bool IsMovementBlock {
        get {
            return _isMovementBlock;
        }

        set {
            _isMovementBlock = value;
        }
    }

    #endregion

    /* 
     * 주요 메소드 
     * ① OnCompleteBlockDestroy 블록 매칭 및 파괴 애니메이션
     * 
     * 
     */

    #region Awake, Start
    void Awake() {
        _verticalLine.AnimationCompleted += LineCompleteDelegate;
        _horizontalLine.AnimationCompleted += LineCompleteDelegate;

        _spUpperTile.AnimationCompleted += EffectCompleteDelegate; // 쿠키에서 사용 

        _spCoverBlock.AnimationCompleted += EffectCompleteDelegate; // 바위블록 파괴에서 사용 
        _spUpperEffect.AnimationCompleted += EffectCompleteDelegate; // 그릴에서 사용 

        

    }

	// Use this for initialization
	void Start () {
		pos = this.transform.position;
	}
    #endregion


    #region 블록 초기화 및 상태 설정 

    /// <summary>
    /// 블록타입(id) 설정
    /// </summary>
    /// <param name="id">Identifier.</param>
    public void SetBlockType(int id) {
        blockID = id;
        SetBlockSprite();
    }

    /// <summary>
    /// 블록 등장확률 처리 
    /// </summary>
    /// <returns></returns>
    private int GetRandomBlockID() {
        return Random.Range(0, InGameCtrl.Instance.ColorCount);
   }

    /// <summary>
    /// 초기화 로직 SetState에서 사용
    /// </summary>
    private void InitBlockCtrl() {


        spBlock.transform.DOKill();
        spBlock.transform.localScale = GameSystem.Instance.BaseScale;

        // 블록 커버 
        if(preState != BlockState.Stone && preState != BlockState.StrongStone) {
            _spCoverBlock.gameObject.SetActive(false);
            _spCoverBlock.transform.localPosition = Vector3.zero;
        }



        bombFX.SetActive(false);
        spBlockNavigator.gameObject.SetActive(false);

        IsStone = false;

        // Upper Tile 초기화 
        if (!IsCookie)
            _spUpperTile.gameObject.SetActive(false);


    }

    /// <summary>
    /// 블록 상태 설정 
    /// </summary>
    /// <param name="bs"></param>
    /// <param name="pDestroyClipType"></param>
    public void SetState(BlockState bs, int pDestroyClipType) {
        _destroyClipType = pDestroyClipType;
        this.SetState(bs);
    }

    /// <summary>
    /// 블록 상태 설정 
    /// </summary>
    /// <param name="bs">Bs.</param>
    public void SetState(BlockState bs) {

        // 이전 상태를 저장한다.
        preState = currentState;

        // 초기화 로직 
        InitBlockCtrl();

        // 상태 설정 
        currentState = bs;

        if (currentState == BlockState.None) {

            // 현 블록을 listEmpty 에 추가한다.
            if (!IsMovementBlock)
                InGameCtrl.Instance.listEmptyBlock.Add(this);

            isMatchingChecked = false;
            spBlock.gameObject.SetActive(false);
            _spriteTile.gameObject.SetActive(true);

        }
        else if (currentState == BlockState.Idle) { // Idle 상태 (블록 생성)
            spBlock.transform.DOKill();
            spBlock.transform.localScale = GameSystem.Instance.BaseScale;

            // list Empty 리스트에서 제거 
            if (!InGameCtrl.Instance.listEmptyBlock.Remove(this)) {
                //Debug.Log ("▶ Fail listEmptyBlock Remove ");
            }

            this.SetBlockType(GetRandomBlockID()); // 임의의 블록 ID 설정 

            spBlock.gameObject.SetActive(true);

            // 블록이 작은 크기에서 본래 크기로 커지는 효과 
            //trBlockSprite.localScale = PuzzleConstBox.minBlockScale;
            trBlockSprite.localScale = Vector3.zero;
            trBlockSprite.DOScale(1, 0.35f).SetEase(Ease.InOutElastic);

            //blockSprite.Play (clipIdle);


        }
        else if (currentState == BlockState.Dead) { // 블록 매칭 파괴 

            // 블록 파괴 처리 
            OnCompleteBlockDestroy(_destroyClipType);

        }
        else if (currentState == BlockState.DeadFromItem) { // 주변 폭탄으로 인한 블록 파괴

            spBlock.gameObject.SetActive(false);

            // 블록 파괴 처리 
            OnCompleteBlockDestroy(_originDestroyClipType);


        }
        else if (currentState == BlockState.Item) {

            spBlock.transform.DOKill();
            spBlock.transform.localScale = GameSystem.Instance.BaseScale;

            // list Empty 리스트에서 제거 
            if (!InGameCtrl.Instance.listEmptyBlock.Remove(this)) {
            }

            bombFX.SetActive(true);
            trBlockSprite.transform.DOScale(new Vector3(1.1f, 1.1f, 1), 0.4f).SetLoops(-1, LoopType.Yoyo);

        }
        else if (currentState == BlockState.NoneFromItem) {

            // 현 블록을 listEmpty 에 추가한다.
            if (!IsMovementBlock)
                InGameCtrl.Instance.listEmptyBlock.Add(this);

            isMatchingChecked = false;
            spBlock.gameObject.SetActive(false);


            currentState = BlockState.None; // setState를 태우지 않음 . 
        }
        else if (currentState == BlockState.ItemBlockDead) { // 아이템 블록 파괴 

            spBlock.gameObject.SetActive(false);
            OnCompleteItemBlockDestroy();

            //blockSprite.Play (PuzzleConstBox.clipItemBlockDestroy);

        }
        else if (currentState == BlockState.Inactive) {
            spBlock.gameObject.SetActive(false);
            _spriteTile.gameObject.SetActive(false);

            if (InGameCtrl.Instance.listEmptyBlock.Contains(this))
                InGameCtrl.Instance.listEmptyBlock.Remove(this);

        }
        else if (currentState == BlockState.StrongStone) {

            // 최초 바위화 시킬때, EmptyBlock에서 제거 
            InGameCtrl.Instance.listEmptyBlock.Remove(this);

            IsStone = true;
            //_spUpperTile.gameObject.SetActive(true);
            //_spUpperTile.Play(PuzzleConstBox.clipStrongStone);
            spBlock.gameObject.SetActive(true);
            spBlock.SetSprite(PuzzleConstBox.spriteStrongStone);

        }
        else if (currentState == BlockState.Stone) {

            IsStone = true;
            //_spUpperTile.gameObject.SetActive(true);
            //_spUpperTile.Play(PuzzleConstBox.clipStone);
            spBlock.gameObject.SetActive(true);
            spBlock.SetSprite(PuzzleConstBox.spriteStone);
        }
        else if (currentState == BlockState.FishGrill) { // Fish 굽기 첫 설정 

            // EmptyBlock 에서 제거 
            InGameCtrl.Instance.listEmptyBlock.Remove(this);

            IsFishGrill = true;
            _fishGrillCount = 3; // 3으로 초기화
            _listGrillOrder.Clear();

            // 타일을 변경. 
            _spUpperTile.gameObject.SetActive(true);
            _spUpperTile.Play(PuzzleConstBox.clipFishGrillTile); // 타일을 굽기 타일로 변경 
            spBlock.gameObject.SetActive(true);
            spBlock.SetSprite(PuzzleConstBox.spriteOriginGrill); // 블록 스프라이트 변경 

            // Idle Effect 출현
            _spUpperEffect.gameObject.SetActive(true);
            _spUpperEffect.Play(PuzzleConstBox.clipFishGrillIdleEffect);

        }
        else if(currentState == BlockState.FireworkCap) {

            // EmptyBlock에서 제거
            InGameCtrl.Instance.listEmptyBlock.Remove(this);

            _spUpperTile.gameObject.SetActive(true);
            _spUpperTile.Play(PuzzleConstBox.clipBlockFireworkBase); 

            // 블록 스프라이트는 제거 
            spBlock.gameObject.SetActive(false);

            _spCoverBlock.gameObject.SetActive(true);
            _spCoverBlock.transform.localPosition = new Vector3(0, 0.21f, 0);

            _spCoverBlock.Play(PuzzleConstBox.clipBlockFireworkCap);
        }
        else if( currentState == BlockState.Firework) {

            // 뚜겅제거
            SpawnStoneBreakUpperEffect();
            isMatchingChecked = false;


            _spUpperTile.gameObject.SetActive(true);
            _spUpperTile.Play(PuzzleConstBox.clipBlockFireworkBase);

            // 폭죽 등장 
            _spCoverBlock.gameObject.SetActive(true);
            _spCoverBlock.transform.localPosition = new Vector3(0, 0.21f, 0);
            _spCoverBlock.Play(PuzzleConstBox.clipBlockFirework); 
        }

    }

    #endregion

    #region 블록 특수 처리 (생선굽기, 쿠키, 바위, 이동)


    /// <summary>
    /// 폭죽 블록 설정 
    /// </summary>
    public void SetFirework() {
        if (!(currentState == BlockState.None))
            return;

        this.SetState(BlockState.Firework);

    }

    /// <summary>
    /// 타일 생선굽기
    /// </summary>
    public void SetFishGrill() {
        if (!(currentState == BlockState.None))
            return;

        this.SetState(BlockState.FishGrill);


    }

    /// <summary>
    /// 타일 쿠키화 
    /// </summary>
    public void SetCookie() {

        if (! (currentState == BlockState.None))
            return;

        IsCookie = true;


        // 타일을 변경. 
        _spUpperTile.gameObject.SetActive(true);
        _spUpperTile.Play(PuzzleConstBox.clipCookie);
    }

    /// <summary>
    /// 이동 경로 처리 
    /// </summary>
    public void SetMoveLine() {
        IsMovementBlock = true;

        if (InGameCtrl.Instance.listEmptyBlock.Contains(this))
            InGameCtrl.Instance.listEmptyBlock.Remove(this);
    }


    /// <summary>
    /// 폭죽 터뜨리기 
    /// </summary>
    public void PopFirework() {
        if (currentState != BlockState.FireworkCap && currentState != BlockState.Firework)
            return;

        if(currentState == BlockState.FireworkCap) {
            SpawnStoneBreakUpperEffect();
            this.SetState(BlockState.Firework);
            return;
        }

        if (currentState == BlockState.Firework) {
            this.SetState(BlockState.None);
            SpwanFireworkBolts();
            return;
        }

    }

    #endregion

    #region 생선 굽기 동작 처리 

    /// <summary>
    /// 생선 굽기 
    /// </summary>
    public void GrillFish() {

        if (!IsFishGrill)
            return;

        if (_fishGrillCount <= 0) // 0으로 내려갔으면 더이상 명령 수행하지 않음 .
            return;


        if (_fishGrillCount == 3) {
            _listGrillOrder.Add(PuzzleConstBox.orderFirstGrill);
        }
        else if(_fishGrillCount ==2)
            _listGrillOrder.Add(PuzzleConstBox.orderSecondGrill);
        else if(_fishGrillCount == 1)
            _listGrillOrder.Add(PuzzleConstBox.orderLastGrill);

        // 바로 숫자를 차감한다.
        _fishGrillCount--;


        // 생선 굽기는 애니메이션이 끊기지 않게 하기 위해서  FIFO 기법을 사용한다.
        PlayGrillOrder(); // 명령 수행 

    }

    /// <summary>
    /// 그릴 오더 수행 
    /// </summary>
    void PlayGrillOrder() {
        if (_listGrillOrder.Count == 0)
            return;

        if (_listGrillOrder.Count > 1) // 2개 이상의 명령이 들어가있다면 return. (Animation Completed에서 호출해준다.)
            return;

        /* orderLastGrill의 실행이 완료되어야 하나의 굽기가 완료된것으로 한다. */

        spBlock.gameObject.SetActive(false);
        _spUpperEffect.gameObject.SetActive(false);

        // 무조건 첫번째 명령어만 실행한다.
        if (_listGrillOrder[0] == PuzzleConstBox.orderFirstGrill) {
            
            _spCoverBlock.gameObject.SetActive(true);
            _spCoverBlock.Play(PuzzleConstBox.clipFirstFishGrill); // AnimationCompleted 에서 추가 처리 
        }
        else if(_listGrillOrder[0] == PuzzleConstBox.orderSecondGrill) { // 두번째 명령어 
            _spCoverBlock.gameObject.SetActive(true);
            _spCoverBlock.Play(PuzzleConstBox.clipSecondFishGrill); // AnimationCompleted 에서 추가 처리 
        }
        else { // 마지막 명령어
            
            // 젓가락 등장!?
            PoolManager.Pools[PuzzleConstBox.objectPool].Spawn("GrillStick", Vector3.zero, Quaternion.identity).GetComponent<GrillStickCtrl>().SetGrillStick(this.transform.localPosition);


            InSoundManager.Instance.PlayFishStick();

            // UI 설정
            InUICtrl.Instance.SetMinusMissionCount(SpecialMissionType.grill);
            
            // invoke 시킨다. 
            Invoke("InvokedExitGrill", 0.5f);
        }
    }

    void InvokedExitGrill() {
        IsFishGrill = false;
        this.SetState(BlockState.None); // None으로 변경 
    }

    #endregion

    #region 돌 깨기
    /// <summary>
    /// 돌 깨기
    /// </summary>
    public void BreakStone() {
        if (!IsStone)
            return;

        if (_isBreakingStone)
            return;

        _spCoverBlock.gameObject.SetActive(true);
        _spCoverBlock.Play(PuzzleConstBox.clipStoneBreak);

        if (currentState == BlockState.StrongStone)
            SetState(BlockState.Stone);
        else if(currentState == BlockState.Stone) {
            SetState(BlockState.None);

            // UI 설정 
            InUICtrl.Instance.SetMinusMissionCount(SpecialMissionType.stone);
        }

        InSoundManager.Instance.PlayIceBlockBreak();
    }

    #endregion
    
    #region 네비게이터 설정 

    /// <summary>
    /// 네비게이터 타겟으로 설정 
    /// </summary>
    public void SetNaviTarget() {
        spBlock.SetSprite(PuzzleConstBox.listShockBlockSprite[blockID]);
        spBlockNavigator.gameObject.SetActive(true);
        spBlockNavigator.Play();
    }

    public void OffNavigator() {
        spBlockNavigator.gameObject.SetActive(false);
        spBlock.SetSprite(PuzzleConstBox.listBlockSprite[blockID]);
    }
    #endregion

    #region Spawning 코인, 파편 등 

    /// <summary>
    /// Coin Particle 생성 (스폰되면 알아서 이동함) 랜덤하게 10분의 1확률로 생성되도록 처리 
    /// </summary>
    private void SpawnCoinParticle() {

		// 백분율로 처리한다 
		if (Random.Range (0, 100) < 10)
			PoolManager.Pools [PuzzleConstBox.objectPool].Spawn (PuzzleConstBox.prefabCoinParticle, this.transform.position, Quaternion.identity);
	}



	/// <summary>
	/// Foot 파편 생성 
	/// </summary>
	private void SpawnFootFragment() {


        if(InGameCtrl.Instance.IsBossStage || InGameCtrl.Instance.IsRescueStage) {
            PoolManager.Pools[PuzzleConstBox.objectPool].Spawn(
            PuzzleConstBox.prefabFootFrag, this.transform.position, Quaternion.identity).GetComponent<FootFragCtrl>().SetTargetPos();
            return;
        }



        // 각 블록 ID 별로 고양이가 장착되어있는지 체크
        if (blockID < 3 && GameSystem.Instance.ListEquipNekoID[blockID] < 0 )
            return;

		PoolManager.Pools [PuzzleConstBox.objectPool].Spawn (
			PuzzleConstBox.prefabFootFrag, this.transform.position, Quaternion.identity).GetComponent<FootFragCtrl> ().SetTargetPos (blockID);
	}


    /// <summary>
    /// 이펙트 생성 
    /// </summary>
    void SpawnStoneBreakUpperEffect() {
        PoolManager.Pools["Blocks"].Spawn(PuzzleConstBox.prefabUpperBlockEffect, this.transform.position, Quaternion.identity).GetComponent<UpperBlockEffectCtrl>().PlayStoneDestory();
    }

    /// <summary>
    /// 폭죽 탄환 소환 
    /// </summary>
    void SpwanFireworkBolts() {
        PoolManager.Pools["Blocks"].Spawn(PuzzleConstBox.prefabUpperBlockEffect, this.transform.position, Quaternion.identity).GetComponent<UpperBlockEffectCtrl>().PlayBlockDestroy();

        // 탄환소환 
        StartCoroutine(SpawningFireworkBolt());

    }

    IEnumerator SpawningFireworkBolt() {
        for(int i=0; i<3; i++) {

            PoolManager.Pools[PuzzleConstBox.objectPool].Spawn(PuzzleConstBox.prefabFireworkBolt, this.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.2f);
        }
    }
    #endregion


    /// <summary>
    /// 블록 아이템화
    /// </summary>
    /// <param name="pItemID">P item I.</param>
    public void SetItemBlock(int pItemID) {

		this.SetState (BlockState.Item);

		this.transform.localScale = Vector3.zero;
        this.transform.DOScale(1, 0.2f).SetDelay(0.3f);

		itemID = pItemID;

		spBlock.SetSprite (PuzzleConstBox.listItemBlockSprite [itemID]);

		// green blue red
		// 컬러 폭탄의 경우 id 세팅 
		if (itemID == 1)
			blockID = 0;
		else if (itemID == 2)
			blockID = 1;
		else if (itemID == 3)
			blockID = 2;

		spBlock.gameObject.SetActive(true);
	}


    /// <summary>
    /// 블록 랜덤 폭탄화 
    /// </summary>
    public void SetRandomBombBlock() {
        this.SetState(BlockState.Item);

        this.transform.localScale = Vector3.zero;
        this.transform.DOScale(1, 0.2f).SetDelay(0.3f);

        itemID = Random.Range(0, 4);
        // 컬러 폭탄의 경우 id 세팅 
        if (itemID == 1)
            blockID = 0;
        else if (itemID == 2)
            blockID = 1;
        else if (itemID == 3)
            blockID = 2;

        spBlock.SetSprite(PuzzleConstBox.listItemBlockSprite[itemID]);
        spBlock.gameObject.SetActive(true);
    }


	private GameState GetGameState() {
		return InGameCtrl.Instance.currentState;
	}


    /// <summary>
    /// 블록 파괴 처리, 파편 이동 경로 지정
    /// </summary>
    /// <param name="pVec">P vec.</param>
    public void DestroyBlock(int pDestroyClipType = 2) {
		this.SetState (BlockState.Dead, pDestroyClipType);
	}

    /// <summary>
    /// 보드에서 매치될 수 없는 블록인 경우 Destroy 가 아니라 remove로 처리한다. 
    /// </summary>
    public void RemoveBlock() {
        spBlock.transform.DOScale(Vector3.zero, 0.15f).OnComplete(OnCompleteRemove).SetEase(Ease.InBack);
    }

    void OnCompleteRemove() {
        this.SetState(BlockState.None);
        spBlock.transform.localScale = GameSystem.Instance.BaseScale;
    }

    /// <summary>
    /// 아이템 블록 파괴 
    /// </summary>
	public void DestroyItemBlock(int pDestroyClipType = 2) {
		this.SetState (BlockState.ItemBlockDead, pDestroyClipType);

        // 빙고 폭탄 블록 사용 횟수
        GameSystem.Instance.IngameBombCount++;

        InUICtrl.Instance.SetMinusMissionCount(SpecialMissionType.bomb);
        
        switch(itemID) {

            case 0:
                GameSystem.Instance.IngameBlackBombCount++;
                break;
            case 1:
                GameSystem.Instance.IngameBlueBombCount++;
                break;
            case 2:
                GameSystem.Instance.IngameYellowBombCount++;
                break;
            case 3:
                GameSystem.Instance.IngameRedBombCount++;
                break;
        }
    }



    public void JumpBlock() {
        localPos = this.transform.localPosition;
          
        this.transform.DOLocalJump(localPos, 0.2f, 1, 1f);
    }



    #region Block 별 스코어 처리

    /// <summary>
    ///  스코어 띄우기 
    /// </summary>
    /// <param name="pScore"></param>
    public void OnScore(int pScore, int pCount) {
        PoolManager.Pools[PuzzleConstBox.inGameUIPool].Spawn(PuzzleConstBox.prefabBlockColorScore, Vector3.zero, Quaternion.identity)
            .GetComponent<BlockColorScoreCtrl>().SetTarget(this.gameObject, blockID, pScore, pCount);
    }

    #endregion

    #region 터치 처리 

    /// <summary>
    /// Touchs the block.
    /// </summary>
    public void TouchBlock() {
		

		// Idle 상태이거나 ItemSpot(아이템 생성위치)의 경우 터치 불가 
		if(currentState == BlockState.Idle)
			return;
		

		if (currentState == BlockState.Item) {
			HitItem(this);

		} else {
			HitCheck (GetBlockPos ());
		}
	}

	private void HitItem(BlockCtrl pBC) {
		InGameCtrl.Instance.HitItem (pBC);
	}

	private void HitCheck(BlockPos blockPos) {
		InGameCtrl.Instance.HitCheck(blockPos, this);
	}

	public void ShakeScaleBlock() {

        // 블록 표정 변화 
        spBlock.SetSprite(PuzzleConstBox.listMissBlockSprite[blockID]); // sprite 설정 
        this.transform.DOShakeScale (0.2f, 1.5f, 30).OnComplete(OnCompleteShakeScaleBlock);
	}
	private void OnCompleteShakeScaleBlock() {
        // 원상복구 
        SetBlockSprite();
        this.transform.localScale = GameSystem.Instance.BaseScale;
	}

    #endregion



    private void SetBlockSprite() {

        if (currentState != BlockState.Idle)
            return;

        spBlock.SetSprite(PuzzleConstBox.listBlockSprite[blockID]);
    }

    /// <summary>
    /// 타임오버 블록 
    /// </summary>
    public void SetTimeoverBlock() {

        if (currentState != BlockState.Idle)
            return;

        spBlock.SetSprite(PuzzleConstBox.timeoverBlockSprite);
    }


    /// <summary>
    /// 게임플레이 피버/노멀에 따른 블록 표정 변화 
    /// </summary>
    public void SetGamePlayBlock() {
        SetBlockSprite();
    }

    #region 라인 이펙트 플레이 


    /// <summary>
    /// 라인 이펙트 플레이 
    /// </summary>
    /// <param name="clip">Clip.</param>
    public void PlayVerticalLineEffect(bool pBig) {
		this._verticalLine.gameObject.SetActive (true);

        if(pBig) {
            _verticalLine.Play(PuzzleConstBox.clipBigVerticalLine);
        }
        else {
            _verticalLine.Play(PuzzleConstBox.clipVerticalLine);
        }
	}

    public void PlayHorizontalLineEffect(bool pBig) {
        this._horizontalLine.gameObject.SetActive(true);
        _horizontalLine.transform.localEulerAngles = Vector3.zero;

        _horizontalLine.Sprite.scale = _baseScale;

        if (pBig) {
            _horizontalLine.Play(PuzzleConstBox.clipBigHorizontalLine);
        }
        else {
            _horizontalLine.Play(PuzzleConstBox.clipHorizontalLine);
        }
    }

    /// <summary>
    /// 'ㄴ' 자 라인 
    /// </summary>
    /// <param name="pBig"></param>
    public void PlayRightUpLineEffect(bool pBig) {
        this._horizontalLine.gameObject.SetActive(true);
        _horizontalLine.transform.localEulerAngles = Vector3.zero;

        // 회전 처리 
        _horizontalLine.Sprite.scale = _baseScale;

        if (pBig) {
            _horizontalLine.Play(PuzzleConstBox.clipBigCurveLine);
        }
        else {
            _horizontalLine.Play(PuzzleConstBox.clipCurveLine);
        }
    }

    public void PlayLeftUpLineEffect(bool pBig) {
        this._horizontalLine.gameObject.SetActive(true);
        _horizontalLine.transform.localEulerAngles = Vector3.zero;

        // 회전 처리 
        _horizontalLine.Sprite.scale = _HFlipScale;

        if (pBig) {
            _horizontalLine.Play(PuzzleConstBox.clipBigCurveLine);
        }
        else {
            _horizontalLine.Play(PuzzleConstBox.clipCurveLine);
        }
    }

    public void PlayRightDownLineEffect(bool pBig) {
        this._horizontalLine.gameObject.SetActive(true);
        _horizontalLine.transform.localEulerAngles = Vector3.zero;

        // 회전 처리 
        _horizontalLine.Sprite.scale = _VFlipScale ;

        if (pBig) {
            _horizontalLine.Play(PuzzleConstBox.clipBigCurveLine);
        }
        else {
            _horizontalLine.Play(PuzzleConstBox.clipCurveLine);
        }
    }


    public void PlayLeftDownLineEffect(bool pBig) {
        this._horizontalLine.gameObject.SetActive(true);
        _horizontalLine.transform.localEulerAngles = Vector3.zero;

        // 회전 처리 
        _horizontalLine.Sprite.scale = _HVFlipScale;

        if (pBig) {
            _horizontalLine.Play(PuzzleConstBox.clipBigCurveLine);
        }
        else {
            _horizontalLine.Play(PuzzleConstBox.clipCurveLine);
        }
    }


    public void PlayLeftRightDownLineEffect(bool pBig) {
        this._horizontalLine.gameObject.SetActive(true);

        // 회전 처리 
        _horizontalLine.Sprite.scale = _baseScale;
        _horizontalLine.transform.localEulerAngles = Vector3.zero;

        if (pBig) {
            _horizontalLine.Play(PuzzleConstBox.clipBigTLine);
        }
        else {
            _horizontalLine.Play(PuzzleConstBox.clipTLine);
        }

    }

    public void PlayLeftRightUpLineEffect(bool pBig) {
        this._horizontalLine.gameObject.SetActive(true);
        _horizontalLine.transform.localEulerAngles = Vector3.zero;

        // 회전 처리 
        _horizontalLine.Sprite.scale = _VFlipScale;

        if (pBig) {
            _horizontalLine.Play(PuzzleConstBox.clipBigTLine);
        }
        else {
            _horizontalLine.Play(PuzzleConstBox.clipTLine);
        }

    }

    public void PlayUpDownRightLineEffect(bool pBig) {
        this._horizontalLine.gameObject.SetActive(true);
        _horizontalLine.transform.localEulerAngles = new Vector3(0, 0, 90);

        // 회전 처리 
        _horizontalLine.Sprite.scale = _baseScale;

        if (pBig) {
            _horizontalLine.Play(PuzzleConstBox.clipBigTLine);
        }
        else {
            _horizontalLine.Play(PuzzleConstBox.clipTLine);
        }

    }

    public void PlayUpDownLeftLineEffect(bool pBig) {
        this._horizontalLine.gameObject.SetActive(true);
        _horizontalLine.transform.localEulerAngles = new Vector3(0, 0, 90);

        // 회전 처리 
        _horizontalLine.Sprite.scale = _VFlipScale;

        if (pBig) {
            _horizontalLine.Play(PuzzleConstBox.clipBigTLine);
        }
        else {
            _horizontalLine.Play(PuzzleConstBox.clipTLine);
        }

    }



    /// <summary>
    /// 터치 타일 효과 
    /// </summary>
    public void PlayTouchSpotEffect() {
        _spriteTile.transform.DOKill();
        _spriteTile.transform.localScale = GameSystem.Instance.BaseScale;
        //_spriteTile.SetSprite(PuzzleConstBox.spriteTouchTile);

        // 올록볼록
        _spriteTile.transform.DOScale(0.7f, 0.1f).SetLoops(2, LoopType.Yoyo).OnComplete(OnCompleteTileSpotEffect);

    }

    void OnCompleteTileSpotEffect() {
        //_spriteTile.SetSprite(_originTileSprite);
        _spriteTile.transform.localScale = GameSystem.Instance.BaseScale;
    }


    /// <summary>
    /// 네비 타일 효과
    /// </summary>
    public void OnNaviTile() {
        _spriteTile.transform.DOKill();
        _spriteTile.transform.localScale = GameSystem.Instance.BaseScale;

        _spriteTile.SetSprite(PuzzleConstBox.spriteTouchTile);
        _spriteTile.transform.DOScale(0.9f, 0.2f).SetLoops(-1, LoopType.Yoyo);
    }

    /// <summary>
    /// 네비 타일 효과 제거 
    /// </summary>
    public void OffNaviTile() {
        _spriteTile.transform.DOKill();
        _spriteTile.transform.localScale = GameSystem.Instance.BaseScale;
        _spriteTile.SetSprite(_originTileSprite);
    }

    #endregion


    /// <summary>
    /// 플레이어 네코 게이지에 포인트를 더한다. 
    /// </summary>
    /// <param name="pBlockID">P block I.</param>
    private void SetPlayerNekoPoint(int pBlockID) {

		InUICtrl.Instance.FillSkillBar(pBlockID);

	}



	/// <summary>
	/// 블록 매칭 처리 (파괴 처리)
	/// </summary>
	private void OnCompleteBlockDestroy(int pMatchType = 2) {

        PoolManager.Pools[PuzzleConstBox.objectPool].Spawn(PuzzleConstBox.prefabBlockDestroyEffect, pos, Quaternion.identity).GetComponent<BlockDestroyEffectCtrl>().Play(blockID, pMatchType);

		SpawnCoinParticle (); // Coin Particle 생성 
		SpawnFootFragment (); // 발바닥 생성 

		// 고양이 스킬 게이지 상승 
		SetPlayerNekoPoint (blockID);

		// 블록 상태 처리 
		this.SetState (BlockState.None);

        // Cookie 타일 처리 
        DestroyCookie();

  
    }


    /// <summary>
    /// 쿠키 제거
    /// </summary>
    public void DestroyCookie() {
        if (!IsCookie)
            return;

        IsCookie = false;

        // UI 설정 
        InUICtrl.Instance.SetMinusMissionCount(SpecialMissionType.cookie);

        _spUpperTile.Play(PuzzleConstBox.clipCookieBreak);

    }

    
    /// <summary>
    /// 아이템 블록 당사자 파괴 처리 
    /// </summary>
    private void OnCompleteItemBlockDestroy(){

		// 리젠 없고, None 상태로의 변경을 다른 방식으로 한다.
		this.SetState (BlockState.NoneFromItem);

        DestroyCookie();

	}

	/// <summary>
	/// Block Sprite Animation Complete Event
	/// </summary>
	/// <param name="pSprite">P sprite.</param>
	/// <param name="pClip">P clip.</param>
	public void blockCompleteDelegate(tk2dSpriteAnimator pSprite, tk2dSpriteAnimationClip pClip) {


		if (pClip.name == PuzzleConstBox.clipBlockDestroy) { // 블록 폭파 후 처리를 추가한다. 

			// Player Neko Bar 
			SetPlayerNekoPoint (blockID);

			this.SetState (BlockState.None);

		} else if (pClip.name == PuzzleConstBox.clipItemBlockDestroy){ // 폭탄 블록 처리 

			// 리젠도 없고, None 상태로 변경을 다른 방식으로 한다. 
			this.SetState(BlockState.NoneFromItem);

		}

	}
	
	public void LineCompleteDelegate(tk2dSpriteAnimator pSprite, tk2dSpriteAnimationClip pClip) {
		pSprite.gameObject.SetActive (false);
	}
	
	

	
    /// <summary>
    /// 
    /// </summary>
    /// <param name="pSprite"></param>
    /// <param name="pClip"></param>
	void EffectCompleteDelegate(tk2dSpriteAnimator pSprite, tk2dSpriteAnimationClip pClip) {
        if (pClip.name == PuzzleConstBox.clipCookieBreak) {
            IsCookie = false;
            // upperTile 제거
            _spUpperTile.gameObject.SetActive(false);
        }
        else if (pClip.name == PuzzleConstBox.clipStoneBreak) {

            _spCoverBlock.gameObject.SetActive(false);
        }
        else if (pClip.name == PuzzleConstBox.clipFirstFishGrill) { // 첫번째 굽기 종료시, 

            spBlock.gameObject.SetActive(true);
            spBlock.SetSprite(PuzzleConstBox.spriteFirstGrill); // 첫 굽기 Sprite로 변경 

            _spCoverBlock.gameObject.SetActive(false); // 애니메이션 종료 

            // order 제거
            _listGrillOrder.Remove(PuzzleConstBox.orderFirstGrill);

            // 연기 이펙트 
            _spUpperEffect.gameObject.SetActive(true);
            _spUpperEffect.Play(PuzzleConstBox.clipFishGrillSmokeEffect);

            PlayGrillOrder(); // 다시 Order 수행 

            InSoundManager.Instance.PlayFishGrilling();
        }
        else if (pClip.name == PuzzleConstBox.clipSecondFishGrill) { // 두번째 굽기 종료시, 

            spBlock.gameObject.SetActive(true);
            spBlock.SetSprite(PuzzleConstBox.spriteSecondGrill); // 첫 굽기 Sprite로 변경 

            _spCoverBlock.gameObject.SetActive(false); // 애니메이션 종료 

            // order 제거
            _listGrillOrder.Remove(PuzzleConstBox.orderSecondGrill);

            // 연기 이펙트 
            _spUpperEffect.gameObject.SetActive(true);
            _spUpperEffect.Play(PuzzleConstBox.clipFishGrillSmokeEffect);

            PlayGrillOrder(); // 다시 Order 수행 

            InSoundManager.Instance.PlayFishGrilling();
        }
        else if (pClip.name == PuzzleConstBox.clipFishGrillSmokeEffect) { // 그릴 연기 이펙트 종료시

            if (_fishGrillCount <= 0)
                _spUpperEffect.gameObject.SetActive(false);
            else
                _spUpperEffect.Play(PuzzleConstBox.clipFishGrillIdleEffect);
        }

    }
	




	/// <summary>
	/// 블록 위치 설정 
	/// </summary>
	/// <param name="pX">P x.</param>
	/// <param name="pY">P y.</param>
	public void SetBlockPos(int pX, int pY) {
		posX = pX;
		posY = pY;
		
		blockPos.x = posX;
		blockPos.y = posY;

        // 타일 sprite 설정 추가
        if (posY % 2 == 0 && posX % 2 == 0) {
            _spriteTile.SetSprite(PuzzleConstBox.spriteTile1);
            _originTileSprite = PuzzleConstBox.spriteTile1;
        }
        else if (posY % 2 == 0 && posX % 2 == 1) {
            _spriteTile.SetSprite(PuzzleConstBox.spriteTile2);
            _originTileSprite = PuzzleConstBox.spriteTile2;
        }
        else if (posY % 2 == 1 && posX % 2 == 0) {
            _spriteTile.SetSprite(PuzzleConstBox.spriteTile2);
            _originTileSprite = PuzzleConstBox.spriteTile2;
        }
        else if (posY % 2 == 1 && posX % 2 == 1) {
            _spriteTile.SetSprite(PuzzleConstBox.spriteTile1);
            _originTileSprite = PuzzleConstBox.spriteTile1;
        }


    }
	
	// Block 배열 인덱스 리턴 
	public BlockPos GetBlockPos() {
		return blockPos;
	}

	public void OnSpawned() {
		isMatchingChecked = false;
		

        //spBlockNavigator.gameObject.SetActive(false);
        
	}
	
}
