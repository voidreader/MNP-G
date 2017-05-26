using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PathologicalGames;

public partial class InGameCtrl : MonoBehaviour {


	private int matchBlockCount = 0; // 1회 터치에 매치된 블록 카운트
	private int loopMatchCount = 0; // 터치 매치 블록 카운트를 체크할때 Loop 문에서 사용되는 임시 변수 

	public List<BlockCtrl> listAroundBlock = new List<BlockCtrl>();
	private List<BlockCtrl> listMatchBlockSeq = new List<BlockCtrl> ();
    private List<BlockCtrl> _listMatchBlockID = new List<BlockCtrl>();
    List<BlockCtrl> _listSuperLineBlocks = new List<BlockCtrl>();


    #region Stone, Cookie, Fish 튀김 
    [SerializeField]
    List<BlockCtrl> _listDestroyStoneBlocks = new List<BlockCtrl>();
    List<BlockCtrl> _listFishGrillBlocks = new List<BlockCtrl>();
    List<BlockCtrl> _listFireworkBlocks = new List<BlockCtrl>();

    int _initStoneBlockCount = 0;
    int _initFishGrillCount = 0;
    int _totalFishGrillCount = 0;
    int _tmpCookieRemainCount = 0;
    int _tmpStoneRemainCount = 0;
    #endregion



    // DestroyMatchedBlock 에서 사용 
    private int pX; // 기준 위치의 x 좌표 
	private int pY; // 기준 위치의 y 좌표 
	private BlockPos targetBlockPos;

    bool _isBigLine = false; // 보드 라인 크기 
	private bool isUpMatch = false;
	private bool isDownMatch = false;
	private bool isLeftMatch = false;
	private bool isRightMatch = false;


    // Navigation
    List<BlockCtrl> _listNaviTouchPos = new List<BlockCtrl>();
    List<BlockCtrl> _listNaviTarget = new List<BlockCtrl>();
    List<BlockCtrl> _listNaviAround = new List<BlockCtrl>();



    // HitItem 
    public List<BlockCtrl> listBombRangeBlock = new List<BlockCtrl> (); // 폭탄 범위 블록 모음 
    List<BlockCtrl> _listBombRangeAllBlock = new List<BlockCtrl>();
    List<BlockCtrl> _listCopyMoveTileBlock = new List<BlockCtrl>();

    int _blockDestoryCount;

    public int InitStoneBlockCount {
        get {
            return _initStoneBlockCount;
        }

        set {
            _initStoneBlockCount = value;
        }
    }

    public int InitFishGrillCount {
        get {
            return _initFishGrillCount;
        }

        set {
            _initFishGrillCount = value;
        }
    }

    public int TotalFishGrillCount {
        get {
            return _totalFishGrillCount;
        }

        set {
            _totalFishGrillCount = value;
        }
    }

    #region Item Block Touch 

    /// <summary>
    /// Item 블록 터치
    /// </summary>
    /// <param name="pBC">P B.</param>
    public void HitItem(BlockCtrl pBC) {


		if (pBC.itemID == 0) {
			DoBombArea (pBC);
		}
		else {
			DoBombColor(pBC);
		}


	}


    /// <summary>
    /// 범위 폭탄 발동 
    /// </summary>
    /// <param name="pBC"></param>
	private void DoBombArea(BlockCtrl pBC) {

		//EnemyNekoManager.Instance.MoveEnemyNekoCenterPos();


		// 3x3 범위의 블록 제거 
		listBombRangeBlock.Clear ();
        _listBombRangeAllBlock.Clear();

        // (세로위치, 가로위치).. 맨날 햇갈린다. 
        for (int i=-2; i<=2; i++) {
			
			if(pBC.posX+i < 0 || pBC.posX+i >= GameSystem.Instance.Height) // 범위를 벗어나는 경우 continue;
				continue;
			
			for(int j=-2; j<=2; j++) {
				
				if(pBC.posY+j < 0 || pBC.posY+j >= GameSystem.Instance.Width)
					continue;
				
				if(i==0 && j==0 ) // 기준좌표는 포함하지 않음. 
					continue;
				
                // 여기는 Idle 상태만 수집 
				if(fieldBlocks[pBC.posX+i, pBC.posY+j].currentState == BlockState.Idle)
					listBombRangeBlock.Add(fieldBlocks[pBC.posX+i, pBC.posY+j]);

                // 모든 Block 수집 
                _listBombRangeAllBlock.Add(fieldBlocks[pBC.posX + i, pBC.posY + j]);


            }
		}
		

		pBC.DestroyItemBlock();


        StartCoroutine (RaiseBombEffect (pBC.transform.position));
        //StartCoroutine (DestroyBombRangeBlock (listBombRangeBlock));
        //RaiseBombEffectAll(pBC.transform.position);
        DestroyBombRangeBlockAll(pBC.transform, listBombRangeBlock);

        // 범위 내 모든 쿠키를 제거 
        for (int i = 0; i < _listBombRangeAllBlock.Count; i++) {
            _listBombRangeAllBlock[i].DestroyCookie();
            _listBombRangeAllBlock[i].BreakStone();
            _listBombRangeAllBlock[i].GrillFish();
        }
    }


	/// <summary>
	/// Do the color of the bomb.
	/// </summary>
	/// <param name="pBC">P B.</param>
	private void DoBombColor(BlockCtrl pBC) {

		//EnemyNekoManager.Instance.MoveEnemyNekoCenterPos ();

		// 동일 타입의 블록 모드 제거 
		listBombRangeBlock.Clear ();
		for(int i=0; i<GameSystem.Instance.Height; i++) {
			for( int j=0; j<GameSystem.Instance.Width; j++) {
				if(fieldBlocks[i,j].currentState == BlockState.Idle && fieldBlocks[i,j].blockID == pBC.blockID)
					listBombRangeBlock.Add(fieldBlocks[i,j]);
			}
		}
		
		pBC.DestroyItemBlock();

        //StartCoroutine (DestroyBombRangeBlock (listBombRangeBlock));

        StartCoroutine(RaiseBombEffect(pBC.transform.position));
        DestroyBombRangeBlockAll(pBC.transform, listBombRangeBlock);


        // 스톤, 그릴, 폭죽
        ProceedAroundSpecialBlocks(listBombRangeBlock, false);
        
    }


    IEnumerator RaiseBombEffect(Vector3 pRaisePos) {


		// 지정된 위치에서 첫 이펙트 Raise
		//PoolManager.Pools [PuzzleConstBox.objectPool].Spawn (PuzzleConstBox.prefabBombBoard, pRaisePos, Quaternion.identity);
		PoolManager.Pools [PuzzleConstBox.objectPool].Spawn (GameSystem.Instance.particleCartoonFight, pRaisePos, Quaternion.identity);
		InSoundManager.Instance.PlayBombBoard();


        // 대형 파편 생성
        SpawnBigFruit(pRaisePos);

		// 지정된 위치에서 이펙트 발동

        // 사운드 
        for (int i = 0; i < 4; i++) {
            yield return new WaitForSeconds(0.08f);
            InSoundManager.Instance.PlayBombBoard();

            PoolManager.Pools[PuzzleConstBox.objectPool].Spawn(GameSystem.Instance.particleCartoonFight, PuzzleConstBox.listBombBoard[i], Quaternion.identity);
        }

        yield return new WaitForSeconds (0.1f);

		//EnemyNekoManager.Instance.CurrentEnemyNeko.EndCenterEffect ();

		// 스테이지 체크를 한다.
		if(!CheckStageMatch()) {

            SetBoardClearResult();

            RemoveAllIdleBlocks();
            StartCoroutine(RespawnStageBlock());
        }

        CheckMoveRoadBlocks();


        // 특수 미션 UI값 처리 
        GetSpecialMissionBlockCountUI(out _tmpCookieRemainCount, out _tmpStoneRemainCount);
    }



    /// <summary>
    /// 
    /// </summary>
    /// <param name="pRaisePos"></param>
	private void SpawnBigFruit(Vector3 pRaisePos) {

		// 효과음
		InSoundManager.Instance.PlayBombAfter ();


        PoolManager.Pools[PuzzleConstBox.objectPool].Spawn(
            PuzzleConstBox.prefabMiniFoot, pRaisePos, Quaternion.identity).GetComponent<MiniFootCtrl>().SetBigFruit();


    }




    /// <summary>
    /// 시간 차없는 블록 파괴 
    /// </summary>
    /// <param name="pList"></param>
    private void DestroyBombRangeBlockAll(Transform pBC, List<BlockCtrl> pList) {

        

        for (int i = 0; i < pList.Count; i++) {
            pList[i].SetState(BlockState.DeadFromItem);
        }

        InUICtrl.Instance.SetComboCnt(1);

        // Item Bar 
        InUICtrl.Instance.AddItemBarValue(pList.Count);

        InUICtrl.Instance.AddBombDestroyScore(pBC.transform, pList.Count);

        GameSystem.Instance.IngameBlockCount += pList.Count;

        //폭탄 파괴시에는 블록카운트 추가 
        for(int i =0; i < pList.Count; i++ ) {
            if(pList[i].blockID == 0) {
                GameSystem.Instance.MatchedBlueBlock++;
            }
            else if (pList[i].blockID == 1) {
                GameSystem.Instance.MatchedYellowBlock++;
            }
            else if (pList[i].blockID == 2) {
                
                GameSystem.Instance.MatchedRedBlock++;

            }
        }
    }


    #endregion

    #region Lock 처리 

    /// <summary>
    /// Sets the in game lock.
    /// </summary>
    /// <param name="pValue">If set to <c>true</c> p value.</param>
    public void SetInGameLock(bool pValue) {
		updateLock = pValue;
		timerLock = pValue;

		
	}

	public void SetInputLock(bool pValue) {
		updateLock = pValue;
		
	}

    #endregion




    /// <summary>
    /// 터치 체크 
    /// </summary>
    /// <param name="blockPos">Block position.</param>
    public void HitCheck(BlockPos blockPos, BlockCtrl pHitBlock) {

		// 터치 지점 주변 블록 체크 
		matchBlockCount = CheckAroundBlocks (blockPos, true);
		
		if(matchBlockCount > 1) { // Match 
			// 매칭 블록 처리 
			DestroyMatchedBlock(blockPos, pHitBlock, matchBlockCount); 
			// 매칭 블록 포인트 처리 
			SetMatchBlockPoint(matchBlockCount);


            if (!CheckStageMatch()) {
                //Debug.Log("♠♠♠ No Match In this Stage!");
                SetBoardClearResult();
                RemoveAllIdleBlocks();
                StartCoroutine(RespawnStageBlock());
            }

            CheckMoveRoadBlocks();


            // 특수 미션 UI값 처리 
            GetSpecialMissionBlockCountUI(out _tmpCookieRemainCount, out _tmpStoneRemainCount);


        } else { // Miss

            InUICtrl.Instance.MissCombo();
            GameSystem.Instance.IngameMissCount++; // Miss Count ++

            PopMissText(blockPos); // Miss 띄우기 
            PopMissPenalty(blockPos); // Miss 패널티 띄우기


            pHitBlock.PlayTouchSpotEffect(); // Touch 효과 
		}

	}

    /// <summary>
    /// 스테이지 매칭 체크 
    /// </summary>
    /// <returns></returns>
	public bool CheckStageMatch() {

        // 리스포닝 중에는 return true
        if (IsRespawningBoard)
            return true;


		for (int i=0; i<GameSystem.Instance.Height; i++) {
			for (int j=0; j<GameSystem.Instance.Width; j++) {

				if(fieldBlocks[i,j].currentState != BlockState.None)
					continue;

				// 체크 
				if(CheckAroundBlocks(fieldBlocks[i,j].blockPos, false) > 1) {
					return true; // 매칭 있음 
				} else {
					continue;
				}
			}
		}

		return false;
	}


    #region Move Mission 처리 


    /// <summary>
    /// 이동미션이 완료되었을때 처리 
    /// </summary>
    void ClearMoveMission() {

        Debug.Log("★★★ Just cleared move mission");

        // 이동 타일 제거 
        for(int i=0; i< _listMoveTiles.Count; i++ ) {
            _listMoveTiles[i].gameObject.SetActive(false);
        }


        _moveTileStartBlock.IsMovementBlock = false;
        _moveTileStartBlock.SetState(BlockState.None);

        for(int i=0; i<ListMoveTilesBlock.Count; i++) {
            ListMoveTilesBlock[i].IsMovementBlock = false;
            ListMoveTilesBlock[i].SetState(BlockState.None); // 블록 활성화 
        }
        
        ListMoveTilesBlock.Clear(); // 클리어 
    }


    /// <summary>
    /// 이동 미션에서의 경로상 블록 체크 
    /// </summary>
    void CheckMoveRoadBlocks() {

        if (!IsMoveMission)
            return;

        if (MoveMissionCount <= 0)
            return;

        // 무빙 처리중에는 return
        if (IsProcessingMove)
            return;



        for(int i=0; i<ListMoveTilesBlock.Count;i++) {

            // 경로를 가로막고 있는 것이 있으면 return
            if (ListMoveTilesBlock[i].currentState == BlockState.Idle
                || ListMoveTilesBlock[i].IsFishGrill
                || ListMoveTilesBlock[i].IsStone
                || ListMoveTilesBlock[i].currentState == BlockState.Item)

                return;
        }


        StartCoroutine(MovingCat());
        
    }

    /// <summary>
    /// 이동 경로의 블록 생성
    /// </summary>
    void SpawnMovingBlocks() {
        int spawnCount = ListMoveTilesBlock.Count / 2;
        int copyIndex = 0;


        // Copy
        _listCopyMoveTileBlock.Clear();
        for (int i = 0; i < ListMoveTilesBlock.Count; i++) {
            _listCopyMoveTileBlock.Add(ListMoveTilesBlock[i]);
        }

        Debug.Log("★ SpawnMovingBlocks SpawnCount, copycount :: " + spawnCount + "/" + _listCopyMoveTileBlock.Count);

        // 카운트 만큼 생성 
        for (int i = 0; i < spawnCount; i++) {
            copyIndex = Random.Range(0, _listCopyMoveTileBlock.Count);


            // 생성위치에 폭탄이 있으면 생성되지 않도록 변경 
            if (_listCopyMoveTileBlock[copyIndex].currentState == BlockState.Item)
                continue; 

            if(MoveDefenceType == 0)
                _listCopyMoveTileBlock[copyIndex].SetState(BlockState.Idle);
            else if(MoveDefenceType == 1)
                _listCopyMoveTileBlock[copyIndex].SetState(BlockState.StrongStone);
            else { // 혼합
                if(Random.Range(0,2) % 2 == 0)
                    _listCopyMoveTileBlock[copyIndex].SetState(BlockState.Idle);
                else
                    _listCopyMoveTileBlock[copyIndex].SetState(BlockState.StrongStone);
            }


            _listCopyMoveTileBlock.RemoveAt(copyIndex);
        }
    }

    IEnumerator MovingCat() {
      

        Debug.Log("★★ MovingCat ★★");

        IsProcessingMove = true;
        // 감소 
        MoveMissionCount--;



        // 첫 점프
        _currentMovingCat.SetJumping(ListMoveTiles[1].transform.position);

        yield return new WaitForSeconds(0.15f);

        // 인덱스 1부터 ~ count-1 까지 
        for(int i=1; i<ListMoveTiles.Count-1;i++) {

            ListMoveTiles[i].PushTile();

            if(i+1 == ListMoveTiles.Count-1) {
                _currentMovingCat.SetLastJumping(ListMoveTiles[i + 1].transform.position);
            }
            else {
                _currentMovingCat.SetJumping(ListMoveTiles[i + 1].transform.position);
            }

            

            yield return new WaitForSeconds(0.15f);
        }

        SpawnMovingBlocks();


        // UI 처리
        InUICtrl.Instance.SetMoveMissionValue(MoveMissionCount);


        IsProcessingMove = false;

        if(MoveMissionCount <= 0) {
            ClearMoveMission();
            yield break;

        }

        SetMovingCatInStartPosition();

    }

    /// <summary>
    /// Idle 세팅
    /// </summary>
    void SetMovingCatInStartPosition() {
        _currentMovingCat = PoolManager.Pools["Blocks"].Spawn(PuzzleConstBox.prefabMoveingCat, ListMoveTiles[0].transform.position, Quaternion.identity).GetComponent<MovingCatCtrl>();
        _currentMovingCat.SetIdle();
    }

    #endregion

    /// <summary>
    /// 모든 Idle 블록을 제거(보드 클리어) 
    /// </summary>
    private void RemoveAllIdleBlocks() {

        // 이동 미션일 경우 다르게 처리 
        if(IsMoveMission) {

            for (int i = 0; i < GameSystem.Instance.Height; i++) {
                for (int j = 0; j < GameSystem.Instance.Width; j++) {

                    // 이동경로의 블록은 파괴하지 않는다.
                    if (fieldBlocks[i, j].currentState == BlockState.Idle && !ListMoveTilesBlock.Contains(fieldBlocks[i,j])) {
                        fieldBlocks[i, j].RemoveBlock();
                    }
                }
            }

            return;
        }


        // 일반 스테이지 
		for (int i=0; i<GameSystem.Instance.Height; i++) {
			for (int j=0; j<GameSystem.Instance.Width; j++) {
				
				if(fieldBlocks[i,j].currentState == BlockState.Idle) {
					fieldBlocks[i,j].RemoveBlock();
				}
			}
		}
	}




	/// <summary>
	/// 매치 블록 개수를 사용하여 게임 내 필요한 포인트 증가 
	/// </summary>
	/// <param name="pMatchBlockCount">P match block count.</param>
	private void SetMatchBlockPoint(int pMatchBlockCount) {

		// 스코어 처리 
		//InUICtrl.Instance.AddMatchScore(pMatchBlockCount);

		// Item Bar 처리 
		InUICtrl.Instance.AddItemBarValue (pMatchBlockCount);
	}


	/// <summary>
    /// 매치된 블록 제거 처리 (이펙트 라인 )
    /// </summary>
    /// <param name="pHitBlockPos"></param>
    /// <param name="pHitBlock"></param>
    /// <param name="pMatchCount"></param>
	public void DestroyMatchedBlock(BlockPos pHitBlockPos, BlockCtrl pHitBlock, int pMatchCount = 2) {


		isRightMatch = false;
		isLeftMatch = false;
		isUpMatch = false;
		isDownMatch = false;

        // 4개 매치일때 빅 매치 기능 추가 (일직선상으로 모두 파괴)
        _isBigLine = false;
        _listSuperLineBlocks.Clear(); // 대상 블록 리스트 

        if (pMatchCount > 3) 
            _isBigLine = true;


        // 빙고 3개 한번에 처리시 횟수 증가 
        if (pMatchCount == 3) {
            GameSystem.Instance.IngameMatchThreeCount++;
        }
        else if (pMatchCount == 4) {
            GameSystem.Instance.IngameMatchFourCount++;
        }



        foreach (BlockCtrl targetBlockCtrl in listAroundBlock) {
			
			// 매치되는 블록 사이의 블록에 라인 이펙트 추가 
			if(targetBlockCtrl.isMatchingChecked) {
				
				targetBlockPos = targetBlockCtrl.GetBlockPos();
				
				// lineEffect 
				//y+ 방향으로 라인이펙트 Play
				if(targetBlockPos.x == pHitBlockPos.x && targetBlockPos.y > pHitBlockPos.y) {

					for(int i=pHitBlockPos.y+1; i <= targetBlockPos.y; i++) {
                        fieldBlocks[pHitBlockPos.x, i].PlayHorizontalLineEffect(_isBigLine);
                    }
					
					isRightMatch = true;

                    if(_isBigLine) {

                        // targetBlock Y+ (width) 오른쪽 방향으로 뻗어나가도록 처리
                         for(int w = targetBlockPos.y + 1; w < GameSystem.Instance.Width; w++) {

                            if (CheckBlockingLine(fieldBlocks[pHitBlockPos.x, w]))
                                break;

                            if (fieldBlocks[pHitBlockPos.x, w].currentState == BlockState.Idle)
                                _listSuperLineBlocks.Add(fieldBlocks[pHitBlockPos.x, w]); // ADD

                            
                            fieldBlocks[pHitBlockPos.x, w].PlayHorizontalLineEffect(_isBigLine); // 라인 플레이
                        }

                    }


				}
				// y-- 방향으로 play 
				else if(targetBlockPos.x == pHitBlockPos.x && targetBlockPos.y < pHitBlockPos.y) {

					for(int i=pHitBlockPos.y-1; i >= targetBlockPos.y; i--) {
                        fieldBlocks[pHitBlockPos.x, i].PlayHorizontalLineEffect(_isBigLine);
					}

					isLeftMatch = true;


                    if (_isBigLine) {
                        // targetBlock Y- (width) 왼쪽 방향으로 뻗어나가도록 처리
                        for (int w = targetBlockPos.y - 1; w >= 0; w--) {

                            if (CheckBlockingLine(fieldBlocks[pHitBlockPos.x, w]))
                                break; 

                            if (fieldBlocks[pHitBlockPos.x, w].currentState == BlockState.Idle)
                                _listSuperLineBlocks.Add(fieldBlocks[pHitBlockPos.x, w]); // ADD

                            fieldBlocks[pHitBlockPos.x, w].PlayHorizontalLineEffect(_isBigLine); // 라인 플레이 
                        }
                    }

                }
				// x+ 방향으로 play 
				else if(targetBlockPos.x > pHitBlockPos.x && targetBlockPos.y == pHitBlockPos.y) {

					for(int i=pHitBlockPos.x+1; i <= targetBlockPos.x; i++) {
                        fieldBlocks[i, pHitBlockPos.y].PlayVerticalLineEffect(_isBigLine);
                    }

					isDownMatch= true;

                    if (_isBigLine) {
                        // targetBlock X+ (height) 아래 방향으로 뻗어나가도록 처리
                        for (int h = targetBlockPos.x + 1; h < GameSystem.Instance.Height; h++) {


                            if (CheckBlockingLine(fieldBlocks[h, pHitBlockPos.y]))
                                break;

                            if (fieldBlocks[h, pHitBlockPos.y].currentState == BlockState.Idle)
                                _listSuperLineBlocks.Add(fieldBlocks[h, pHitBlockPos.y]); // ADD

                            fieldBlocks[h, pHitBlockPos.y].PlayVerticalLineEffect(_isBigLine); // 라인 플레이 
                        }
                    }


                }
				// x-방향으로 play 
				else if(targetBlockPos.x < pHitBlockPos.x && targetBlockPos.y == pHitBlockPos.y) {

                    for (int i=pHitBlockPos.x-1; i >= targetBlockPos.x; i--) {
                        fieldBlocks[i, pHitBlockPos.y].PlayVerticalLineEffect(_isBigLine);
					}
					
					isUpMatch = true;

                    if (_isBigLine) {
                        // targetBlock X+ (height) 아래 방향으로 뻗어나가도록 처리
                        for (int h = targetBlockPos.x - 1; h >=0; h--) {

                            if (CheckBlockingLine(fieldBlocks[h, pHitBlockPos.y]))
                                break;

                            if (fieldBlocks[h, pHitBlockPos.y].currentState == BlockState.Idle)
                                _listSuperLineBlocks.Add(fieldBlocks[h, pHitBlockPos.y]); // ADD

                            fieldBlocks[h, pHitBlockPos.y].PlayVerticalLineEffect(_isBigLine); // 라인 플레이 
                        }
                    }

                }
				
			}
			
		} // end of foreach

        // 터치 블록 이펙트 처리 #1
        fieldBlocks[pHitBlockPos.x, pHitBlockPos.y].PlayTouchSpotEffect();

        // 터치 블록 이펙트 처리 #2
        if (isLeftMatch && isRightMatch && isUpMatch && isDownMatch) {
            fieldBlocks[pHitBlockPos.x, pHitBlockPos.y].PlayHorizontalLineEffect(_isBigLine);
            fieldBlocks[pHitBlockPos.x, pHitBlockPos.y].PlayVerticalLineEffect(_isBigLine);
        }
        else if (!isLeftMatch && isRightMatch && isUpMatch && isDownMatch)
            fieldBlocks[pHitBlockPos.x, pHitBlockPos.y].PlayUpDownRightLineEffect(_isBigLine);

        else if (isLeftMatch && isRightMatch && !isUpMatch && !isDownMatch)
            fieldBlocks[pHitBlockPos.x, pHitBlockPos.y].PlayHorizontalLineEffect(_isBigLine);

        else if (!isLeftMatch && isRightMatch && isUpMatch && !isDownMatch)
            fieldBlocks[pHitBlockPos.x, pHitBlockPos.y].PlayRightUpLineEffect(_isBigLine);

        else if (isLeftMatch && !isRightMatch && isUpMatch && !isDownMatch)
            fieldBlocks[pHitBlockPos.x, pHitBlockPos.y].PlayLeftUpLineEffect(_isBigLine);

        else if (!isLeftMatch && isRightMatch && !isUpMatch && isDownMatch)
            fieldBlocks[pHitBlockPos.x, pHitBlockPos.y].PlayRightDownLineEffect(_isBigLine);

        else if (!isLeftMatch && !isRightMatch && isUpMatch && isDownMatch)
            fieldBlocks[pHitBlockPos.x, pHitBlockPos.y].PlayVerticalLineEffect(_isBigLine);

        else if (isLeftMatch && !isRightMatch && !isUpMatch && isDownMatch)
            fieldBlocks[pHitBlockPos.x, pHitBlockPos.y].PlayLeftDownLineEffect(_isBigLine);

        else if (isLeftMatch && isRightMatch && !isUpMatch && isDownMatch)
            fieldBlocks[pHitBlockPos.x, pHitBlockPos.y].PlayLeftRightDownLineEffect(_isBigLine);

        else if (isLeftMatch && !isRightMatch && isUpMatch && isDownMatch)
            fieldBlocks[pHitBlockPos.x, pHitBlockPos.y].PlayUpDownLeftLineEffect(_isBigLine);

        else if (isLeftMatch && isRightMatch && isUpMatch && !isDownMatch)
            fieldBlocks[pHitBlockPos.x, pHitBlockPos.y].PlayLeftRightUpLineEffect(_isBigLine);


        // 추가된 블록을 listAroundBlock에 추가 
        if(_isBigLine) {
            for(int i =0; i<_listSuperLineBlocks.Count; i++) {

                _listSuperLineBlocks[i].isMatchingChecked = true;

                listAroundBlock.Add(_listSuperLineBlocks[i]);
            }
        }

        // 실제 파괴 처리 
        DestroyMathBlockAtSameTime (listAroundBlock, pHitBlock, matchBlockCount);
		
		
		// 터치스코어 변수 재계산 후 점수 처리 
		// 사운드 재생 
		if (matchBlockCount == 2) {
			//InGameConstBox.soundCtrl.PlayMatchSound(0);
			InSoundManager.Instance.PlayMatchSound(0);
		}
		else if(matchBlockCount == 3) {
			InSoundManager.Instance.PlayMatchSound(1);
		}
		else if(matchBlockCount == 4) {
			InSoundManager.Instance.PlayMatchSound(2);
		}
		else {
			Debug.Log("▶ DestroyMatchedBlock 오류");
		}
		
	}


	/// <summary>
	/// 파라매터 만큼 콤보 카운트를 증가시킨다. 
	/// </summary>
	/// <param name="comboCnt">Combo count.</param>
	private void SetComboCnt(int comboCnt) {
		InUICtrl.Instance.SetComboCnt (comboCnt);
	}




	/// <summary>
	/// Miss Text 띄우기 
	/// </summary>
	/// <param name="blockPos">Block position.</param>
	private void PopMissText(BlockPos blockPos) {
		PoolManager.Pools [PuzzleConstBox.objectPool].Spawn (PuzzleConstBox.prefabMissText, fieldBlocks[blockPos.x, blockPos.y].transform.position, Quaternion.identity).GetComponent<BlockMissEffectCtrl>()
            .SetMissText();
	}


    /// <summary>
    /// Miss로 인한 패널티 띄우기
    /// </summary>
    /// <param name="blockPos"></param>
    private void PopMissPenalty(BlockPos blockPos) {
        StartCoroutine(DelayedMissPenalty(blockPos));
    }

    IEnumerator DelayedMissPenalty(BlockPos blockPos) {
        yield return new WaitForSeconds(0.3f);

        PoolManager.Pools[PuzzleConstBox.objectPool].Spawn(PuzzleConstBox.prefabMissText, fieldBlocks[blockPos.x, blockPos.y].transform.position, Quaternion.identity).GetComponent<BlockMissEffectCtrl>()
            .SetMissPenalty();
    }
    



    /// <summary>
    /// 매칭 블록 처리 & 스코어 처리 추가  (2016.05)
    /// 2017.02 바위 블록 처리 추가 
    /// </summary>
    /// <param name="pList"></param>
	private void DestroyMathBlockAtSameTime(List<BlockCtrl> pList, BlockCtrl pHitBlock, int pMatchBlockCount = 2) {

        _listMatchBlockID.Clear();

        // 주변 특수블록 처리 
        ProceedAroundSpecialBlocks(pList, true);

        for (int i=0; i<pList.Count; i++) {
			if(pList[i].isMatchingChecked) {
                _listMatchBlockID.Add(pList[i]);
                pList[i].DestroyBlock(pMatchBlockCount);
			}
		}

        // 스코어 처리 
        InUICtrl.Instance.AddMatchScoreWithBlockColor(_listMatchBlockID, pHitBlock);


		// 콤보 처리
		SetComboCnt (1);
      
    }

    #region 특수 블록에 대한 처리 

    void ProceedAroundSpecialBlocks(List<BlockCtrl> pList, bool pMatchCheck) {

        AddAroundFireworkBlock(pList, pMatchCheck);
        PopFirework();

        AddAroundStoneBlock(pList, pMatchCheck);
        BraekStones();

        AddAroundGrillBlock(pList, pMatchCheck);
        GrillFish();
    }



    /// <summary>
    /// 주변 그릴 블록 모으기 
    /// </summary>
    void AddAroundFireworkBlock(List<BlockCtrl> pList, bool pMatchCheck = true) {

        if (!IsBossStage && !IsRescueStage)
            return;

        _listFireworkBlocks.Clear();

        for (int i = 0; i < pList.Count; i++) {

            // 매칭 체크 하는 경우에 추가 
            if (pMatchCheck && !pList[i].isMatchingChecked)
                continue;

            // 상단 블록 체크
            if (pList[i].posX - 1 >= 0) {

                if(!_listFireworkBlocks.Contains(fieldBlocks[pList[i].posX - 1, pList[i].posY]))
                    _listFireworkBlocks.Add(fieldBlocks[pList[i].posX - 1, pList[i].posY]);
            }

            // 하단 블록 체크
            if (pList[i].posX + 1 < GameSystem.Instance.Height) {
                if (!_listFireworkBlocks.Contains(fieldBlocks[pList[i].posX + 1, pList[i].posY]))
                    _listFireworkBlocks.Add(fieldBlocks[pList[i].posX + 1, pList[i].posY]);
            }

            // 왼쪽 블록 체크
            if (pList[i].posY - 1 >= 0) {
                if (!_listFireworkBlocks.Contains(fieldBlocks[pList[i].posX, pList[i].posY - 1]))
                    _listFireworkBlocks.Add(fieldBlocks[pList[i].posX, pList[i].posY - 1]);
            }


            // 오른쪽 블록 체크
            if (pList[i].posY + 1 < GameSystem.Instance.Width) {
                if (!_listFireworkBlocks.Contains(fieldBlocks[pList[i].posX, pList[i].posY + 1]))
                    _listFireworkBlocks.Add(fieldBlocks[pList[i].posX, pList[i].posY + 1]);
            }
        }
    }



    /// <summary>
    /// 주변 그릴 블록 모으기 
    /// </summary>
    void AddAroundGrillBlock(List<BlockCtrl> pList, bool pMatchCheck = true) {

        if (!IsFishMission)
            return;

        _listFishGrillBlocks.Clear();

        for (int i = 0; i < pList.Count; i++) {

            // 매칭 체크 하는 경우에 추가 
            if (pMatchCheck && !pList[i].isMatchingChecked)
                continue;

            // 상단 블록 체크
            if (pList[i].posX - 1 >= 0) {
                _listFishGrillBlocks.Add(fieldBlocks[pList[i].posX - 1, pList[i].posY]);
            }

            // 하단 블록 체크
            if (pList[i].posX + 1 < GameSystem.Instance.Height) {
                _listFishGrillBlocks.Add(fieldBlocks[pList[i].posX + 1, pList[i].posY]);
            }

            // 왼쪽 블록 체크
            if (pList[i].posY - 1 >= 0) {
                _listFishGrillBlocks.Add(fieldBlocks[pList[i].posX, pList[i].posY - 1]);
            }


            // 오른쪽 블록 체크
            if (pList[i].posY + 1 < GameSystem.Instance.Width) {
                _listFishGrillBlocks.Add(fieldBlocks[pList[i].posX, pList[i].posY + 1]);
            }
        }
    }


    /// <summary>
    /// 주변 스톤 블록 모으기 
    /// </summary>
    void AddAroundStoneBlock(List<BlockCtrl> pList, bool pMatchCheck = true) {

        /*
        if (!CheckStoneMission() && !IsMoveMission)
            return;
        */

        _listDestroyStoneBlocks.Clear();
        
        for(int i=0; i<pList.Count;i++) {

            // 매칭 체크 하는 경우에 추가 
            if(pMatchCheck && !pList[i].isMatchingChecked)
                continue;

            // 상단 블록 체크
            if (pList[i].posX - 1 >= 0) {

                if (!_listDestroyStoneBlocks.Contains(fieldBlocks[pList[i].posX - 1, pList[i].posY]))
                    _listDestroyStoneBlocks.Add(fieldBlocks[pList[i].posX - 1, pList[i].posY]);
            }

            // 하단 블록 체크
            if (pList[i].posX + 1 < GameSystem.Instance.Height) {

                if (!_listDestroyStoneBlocks.Contains(fieldBlocks[pList[i].posX + 1, pList[i].posY]))
                    _listDestroyStoneBlocks.Add(fieldBlocks[pList[i].posX + 1, pList[i].posY]);
            }

            // 왼쪽 블록 체크
            if (pList[i].posY - 1 >= 0) {
                

                if (!_listDestroyStoneBlocks.Contains(fieldBlocks[pList[i].posX, pList[i].posY - 1]))
                    _listDestroyStoneBlocks.Add(fieldBlocks[pList[i].posX, pList[i].posY - 1]);

            }


            // 오른쪽 블록 체크
            if (pList[i].posY + 1 < GameSystem.Instance.Width) {
                

                if (!_listDestroyStoneBlocks.Contains(fieldBlocks[pList[i].posX, pList[i].posY + 1]))
                    _listDestroyStoneBlocks.Add(fieldBlocks[pList[i].posX, pList[i].posY + 1]);
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    void BraekStones() {
        for(int i=0;i<_listDestroyStoneBlocks.Count;i++) {
            _listDestroyStoneBlocks[i].BreakStone();
        }
    }

    void GrillFish() {
        for (int i = 0; i < _listFishGrillBlocks.Count; i++) {
            _listFishGrillBlocks[i].GrillFish();
        }
    }

    void PopFirework() {
        for (int i = 0; i < _listFireworkBlocks.Count; i++) {
            _listFireworkBlocks[i].PopFirework();
        }
    }

    #endregion


    /// <summary>
    /// 터치 위치를 기준으로 블록 체킹을 할때, 체크 대상 블록(Idle) 인지 확인하는 메소드
    /// </summary>
    /// <returns><c>true</c>, if idle block check was validated, <c>false</c> otherwise.</returns>
    private bool ValidateIdleBlockCheck(BlockCtrl pBC) {

		// Idle 상태의 블록 return true;
		if (pBC.currentState == BlockState.Idle) {
			pBC.isMatchingChecked = false; // 매칭체크 변수를 false로 바꿔준다.
			return true;
		}

		return false;

	}


    /// <summary>
    /// 나아갈 수 없는 블록 라인인지 체크 
    /// </summary>
    /// <param name="pBlock"></param>
    /// <returns></returns>
    bool CheckBlockingLine(BlockCtrl pBlock) {
        if (pBlock.currentState == BlockState.Inactive
            || pBlock.currentState == BlockState.StrongStone
            || pBlock.currentState == BlockState.Stone
            || pBlock.currentState == BlockState.FishGrill
            || pBlock.currentState == BlockState.FireworkCap
            || pBlock.currentState == BlockState.Firework

            ) {

            return true;

        }

        return false;
    }

	/// <summary>
	/// 터치 위치를 기준으로 동서남북 체크 
	/// </summary>
	/// <returns>The around blocks.</returns>
	/// <param name="blockPos">Block position.</param>
	public int CheckAroundBlocks(BlockPos blockPos, bool bSetMatchFlag) {
		pX = blockPos.x;
		pY = blockPos.y;
		
		// 리스트 초기화 
		listAroundBlock.Clear ();


        // 매치 블록 카운트 초기화
        matchBlockCount = 0;

		
		// 1. up 방향 체크 
		for(int i=pX; i>=0; i--) {

            // 길이 막혔으면 대상 방향은 탐색을 종료한다. 
            if (CheckBlockingLine(fieldBlocks[i, pY]))
                break;

            // 유효한 블록의 경우만 리스트에 ADD 해주고 방향탐색을 종료 
			if(ValidateIdleBlockCheck(fieldBlocks[i,pY])) {
				listAroundBlock.Add(fieldBlocks[i,pY]);
				break; 
			}

		} // UP 방향 끝 
		
		// 2.Right 방향 체크 
		for (int i = pY; i < GameSystem.Instance.Width; i++) {


            if (CheckBlockingLine(fieldBlocks[pX, i]))
                break;

            if (ValidateIdleBlockCheck(fieldBlocks[pX, i])) {
				listAroundBlock.Add(fieldBlocks[pX, i]);
				break; 
			}

		}

		// Right 방향 끝. 
		
		// 3. Down 방향 
		for (int i = pX; i < GameSystem.Instance.Height; i++) {


            if (CheckBlockingLine(fieldBlocks[i, pY]))
                break;

            if (ValidateIdleBlockCheck(fieldBlocks[i,pY])) {
				listAroundBlock.Add(fieldBlocks[i,pY]);
				break; 
			}

		}
		// Down 방향 끝. 
		
		// 4. Left 방향 체크 
		for (int i = pY; i >= 0; i--) {

            if (CheckBlockingLine(fieldBlocks[pX, i]))
                break;

            if (ValidateIdleBlockCheck(fieldBlocks[pX, i])) {
				listAroundBlock.Add(fieldBlocks[pX, i]);
				break; 
			}

		}
		// Left 방향 끝. 
		

		// 5. 방향 체크 후, 동일 ID의 블록 확인 
		for (int i = 0; i < GameSystem.Instance.BlockTypeCount; i++) { // 방해블록 때문에 + 1 해준다. 
			
			loopMatchCount = 0;

			for(int j=0; j < listAroundBlock.Count; j++) {

				if(listAroundBlock[j].isMatchingChecked) {
					continue;
				}

				if(listAroundBlock[j].blockID == i) {
					loopMatchCount++;
				}
			}

			if(loopMatchCount >= 2) {
				foreach(BlockCtrl bc in listAroundBlock) {
					if(bc.blockID == i) {
						// setMatchFlag 가 true 때만 true 세팅 (필드에서 매칭 체크할때 사용을 위함 )
						if(bSetMatchFlag)
							bc.isMatchingChecked = true;
					}
				}

				if(loopMatchCount >= 2) {
					matchBlockCount += loopMatchCount;
				}
			}
		} // 동알 ID 블록 체크 끝 
		
		
		return matchBlockCount;
		
	}
	// End of CheckAroundBlocks ////////////////////////////////////////////////////////////////



    bool GetExistsIdleBlock() {

        for (int i = 0; i < GameSystem.Instance.Height; i++) {
            for (int j = 0; j < GameSystem.Instance.Width; j++) {

                if (fieldBlocks[i, j].currentState == BlockState.Idle)
                    return true;
            }
        }

        return false;

    }


    /// <summary>
    /// Gets the empty block.
    /// </summary>
    /// <returns>The empty block.</returns>
    public BlockCtrl GetEmptyBlock() {

	
		if (listEmptyBlock.Count == 0) {
			Debug.Log("▶▶ listEmpyBlock is Empty!");
		}

		return listEmptyBlock[Random.Range(0, listEmptyBlock.Count)];

	}



    /// <summary>
    /// UI에 할당할 스페셜 미션 블록 개수를 구한다.
    /// </summary>
    /// <param name="oCookie"></param>
    /// <param name="oStone"></param>
    void GetSpecialMissionBlockCountUI(out int oCookie, out int oStone) {

        oCookie = 0;
        oStone = 0;


        // 특수 미션이 아니면, 종료 
        if (!IsCookieMission && !IsStoneMission && !IsFishMission)
            return;


        for (int i = 0; i < GameSystem.Instance.Height; i++) {

            for (int j = 0; j < GameSystem.Instance.Width; j++) {

                if (fieldBlocks[i, j].IsCookie)
                    oCookie++;

                if (fieldBlocks[i, j].IsStone)
                    oStone++;
            }
        }

        if(IsCookieMission)
            InUICtrl.Instance.SetCookieMissionValue(oCookie);

        if(IsStoneMission)
            InUICtrl.Instance.SetStoneMissionValue(oStone);
            

    }

    /// <summary>
    /// 필드에 남은 바위 블록 체크 
    /// </summary>
    /// <returns></returns>
    int GetStoneBlockCount() {

        _tmpStoneRemainCount = 0;

        for (int i = 0; i < GameSystem.Instance.Height; i++) {
            for (int j = 0; j < GameSystem.Instance.Width; j++) {

                if (fieldBlocks[i, j].IsStone)
                    _tmpStoneRemainCount++;

            }
        }


        return _tmpStoneRemainCount;

    }


    /// <summary>
    /// 필드에 남아있는 쿠키의 개수를 구한다.
    /// </summary>
    /// <returns></returns>
    public int GetRemainCookieBlocks() {

        _tmpCookieRemainCount = 0;

        for (int i = 0; i < GameSystem.Instance.Height; i++) {

            for (int j = 0; j < GameSystem.Instance.Width; j++) {
                if (fieldBlocks[i, j].IsCookie)
                    _tmpCookieRemainCount++;
            }
        }

        return _tmpCookieRemainCount;
    }


    #region 네비게이터 용도. 랜덤한 매칭 가능 스팟 리턴 

    /// <summary>
    /// 랜덤한 매칭 가능한 스팟 리턴 
    /// </summary>
    /// <returns></returns>
    private void GetRandomMatchBlock() {

        // 리스트 클리어 
        _listNaviTarget.Clear();
        _listNaviTouchPos.Clear();

        for (int i = 0; i < GameSystem.Instance.Height; i++) {
            for (int j = 0; j < GameSystem.Instance.Width; j++) {

                if (fieldBlocks[i, j].currentState != BlockState.None)
                    continue;

                // 체크 
                GetNavigatorTargetBlocks(fieldBlocks[i, j].blockPos);

                if (_listNaviTarget.Count > 0) // 대상이 생기면 종료 
                    return;
                   
            }
        }

    }


    /// <summary>
    /// 네비게이터 대상 블록 찾기 
    /// </summary>
    /// <param name="blockPos"></param>
    private void GetNavigatorTargetBlocks(BlockPos blockPos) {
        pX = blockPos.x;
        pY = blockPos.y;

        // 리스트 초기화 
        _listNaviAround.Clear();


        // 매치 블록 카운트 초기화
        int naviLoopCount = 0;


        // 1. up 방향 체크 
        for (int i = pX; i >= 0; i--) {

            if (CheckBlockingLine(fieldBlocks[i, pY]))
                break;

            if (ValidateIdleBlockCheck(fieldBlocks[i, pY])) {
                _listNaviAround.Add(fieldBlocks[i, pY]);
                break;
            }

        } // UP 방향 끝 

        // 2.Right 방향 체크 
        for (int i = pY; i < GameSystem.Instance.Width; i++) {

            if (CheckBlockingLine(fieldBlocks[pX, i]))
                break;

            if (ValidateIdleBlockCheck(fieldBlocks[pX, i])) {
                _listNaviAround.Add(fieldBlocks[pX, i]);
                break;
            }

        }

        // Right 방향 끝. 

        // 3. Down 방향 
        for (int i = pX; i < GameSystem.Instance.Height; i++) {

            if (CheckBlockingLine(fieldBlocks[i, pY]))
                break;

            if (ValidateIdleBlockCheck(fieldBlocks[i, pY])) {
                _listNaviAround.Add(fieldBlocks[i, pY]);
                break;
            }

        }
        // Down 방향 끝. 

        // 4. Left 방향 체크 
        for (int i = pY; i >= 0; i--) {

            if (CheckBlockingLine(fieldBlocks[pX, i]))
                break;

            if (ValidateIdleBlockCheck(fieldBlocks[pX, i])) {
                _listNaviAround.Add(fieldBlocks[pX, i]);
                break;
            }

        }
        // Left 방향 끝. 


        // 5. 방향 체크 후, 동일 ID의 블록 확인 
        for (int i = 0; i < GameSystem.Instance.BlockTypeCount; i++) { 

            naviLoopCount = 0;

            for (int j = 0; j < _listNaviAround.Count; j++) {


                if(_listNaviTarget.Contains(_listNaviAround[j])) {
                    continue;
                }

                if (_listNaviAround[j].blockID == i) {
                    naviLoopCount++;
                }
            }

            if (naviLoopCount >= 2) {
                foreach (BlockCtrl bc in _listNaviAround) {
                    if (bc.blockID == i) {
                        // setMatchFlag 가 true 때만 true 세팅 (필드에서 매칭 체크할때 사용을 위함 )
                        _listNaviTarget.Add(bc);
                    }
                }

                
            }
        } // 동알 ID 블록 체크 끝 

        if (_listNaviTarget.Count < 2)
            return;

        // listNaviTarget의 블록을 체크해서 터치 지점을 추가 
        // 블록들이 일직선상에 위치하는지 체크 
        if(_listNaviTarget.Count > 2) { // 일직선상이 아님 
            _listNaviTouchPos.Add(fieldBlocks[pX, pY]); // 체크지점을 넣는다. 
        }
        else { // 일직선인지 체크 필요

            
            if(_listNaviTarget[0].posX == _listNaviTarget[1].posX) {   // 일직선, x좌표 동일
                //  두 블록사이의 모든 빈공간을 넣는다. 
                if(_listNaviTarget[0].posY < _listNaviTarget[1].posY) {
                    for(int i= _listNaviTarget[0].posY+1; i < _listNaviTarget[1].posY; i++) {
                        _listNaviTouchPos.Add(fieldBlocks[_listNaviTarget[0].posX, i]);
                    }
                }
                else {
                    for (int i = _listNaviTarget[1].posY +1; i < _listNaviTarget[0].posY; i++) {
                        _listNaviTouchPos.Add(fieldBlocks[_listNaviTarget[0].posX, i]);
                    }
                }

            }
            else if (_listNaviTarget[0].posY == _listNaviTarget[1].posY) { // // 일직선, y좌표 동일
                if (_listNaviTarget[0].posX < _listNaviTarget[1].posX) {
                    for (int i = _listNaviTarget[0].posX+1; i < _listNaviTarget[1].posX; i++) {
                        _listNaviTouchPos.Add(fieldBlocks[i,_listNaviTarget[0].posY]);
                    }
                }
                else {
                    for (int i = _listNaviTarget[1].posX+1; i < _listNaviTarget[0].posX; i++) {
                        _listNaviTouchPos.Add(fieldBlocks[i, _listNaviTarget[0].posY]);
                    }
                }
            }
            else { // 일직선 아님 
                _listNaviTouchPos.Add(fieldBlocks[pX, pY]); // 체크지점을 넣는다. 
            }
        }
    }

    #endregion

}
