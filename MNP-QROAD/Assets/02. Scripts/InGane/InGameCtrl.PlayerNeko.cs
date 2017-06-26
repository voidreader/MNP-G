﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PathologicalGames;


public partial class InGameCtrl : MonoBehaviour {


	// 플레이어 네코 게이지의 처리를 담당한다. 



	// MyNeko 관련 처리 
	private Vector3 originPos = new Vector3(-6, 1 ,0); // 최초 생성위치 
	private Vector3[] tripleOriginPos = new Vector3[3] {new Vector3(-6,1,0), new Vector3(6,1,0), new Vector3(6,7,0)};
	private int[] tripleNekoID = new int[3];
	private MyNekoCtrl spawnedMyNeko = null;

    List<BlockCtrl> _listRemoveLineBlock = new List<BlockCtrl>();
    List<BlockCtrl> _listSpecialTargetBlock = new List<BlockCtrl>();

    bool _isActiveSkillBars = false;

    public bool IsActiveSkillBars {
        get {
            return _isActiveSkillBars;
        }

        set {
            _isActiveSkillBars = value;
        }
    }

    /// <summary>
    /// 플레이어 네코 스킬 게이지 초기화
    /// </summary>
    private void InitPlayerNekoBars() {
        IsActiveSkillBars = true;
        InUICtrl.Instance.InitSkillBars();
    }

    void SetInactiveNekoBars() {
        IsActiveSkillBars = false;
        InUICtrl.Instance.SetInactiveSkillBars();
    }



    /// <summary>
    /// 소환!
    /// </summary>
    /// <param name="pNeko"></param>
    public void SpawnMyNeko(SkillBarCtrl pNeko, int pEquipIndex) {
        spawnedMyNeko = PoolManager.Pools[PuzzleConstBox.myNekoPool].Spawn(PuzzleConstBox.prefabMyNeko, originPos, Quaternion.identity).GetComponent<MyNekoCtrl>();
        spawnedMyNeko.SetMyNeko(pNeko.NekoID, pNeko.NekoGrade);

        AddNekoDamageInfo(pNeko.NekoID, (int)spawnedMyNeko.GetHitPower());


        // 적 네코를 화면 중앙으로 이동. 
        //EnemyNekoManager.Instance.MoveEnemyNekoCenterPos();

        // 적 네코를 향하여 돌진 
        spawnedMyNeko.Rush();

        GameSystem.Instance.IngameSpecialAttackCount++;


        // 액티브 스킬 체크 
        CheckActiveSkill(pNeko.NekoID, pEquipIndex);
    }


    /// <summary>
    /// 액티브 스킬 체크 
    /// </summary>
    /// <param name="pEquipIndex"></param>
    public void CheckActiveSkill(int pNekoID, int pEquipIndex) {


        #region 폭탄드롭 관련 4개의 액티브 스킬 처리 
        if(InGameCtrl.Instance.ListNekoPassive[pEquipIndex].IsRandomBombDrop) {
            InGameCtrl.Instance.DropPassiveBomb(InGameCtrl.Instance.ListNekoPassive[pEquipIndex].RandomBombDropCount, BombType.Random);
        }

        if (InGameCtrl.Instance.ListNekoPassive[pEquipIndex].IsYellowBombDrop) {
            InGameCtrl.Instance.DropPassiveBomb(InGameCtrl.Instance.ListNekoPassive[pEquipIndex].YellowBombDropCount, BombType.Yellow);
        }

        if (InGameCtrl.Instance.ListNekoPassive[pEquipIndex].IsBlueBombDrop) {
            InGameCtrl.Instance.DropPassiveBomb(InGameCtrl.Instance.ListNekoPassive[pEquipIndex].BlueBombDropCount, BombType.Blue);
         }

        if (InGameCtrl.Instance.ListNekoPassive[pEquipIndex].IsRedBombDrop) {
            InGameCtrl.Instance.DropPassiveBomb(InGameCtrl.Instance.ListNekoPassive[pEquipIndex].RedBombDropCount, BombType.Red);
         }

        if (InGameCtrl.Instance.ListNekoPassive[pEquipIndex].IsBlackBombDrop) {
            InGameCtrl.Instance.DropPassiveBomb(InGameCtrl.Instance.ListNekoPassive[pEquipIndex].BlackBombDropCount, BombType.Black);
         }


        #endregion

        if (InGameCtrl.Instance.IsBonusTime)
            return;

        // 게임시간 증가 액티브 
        if (InGameCtrl.Instance.ListNekoPassive[pEquipIndex].IsTimePlusActive) {
            Debug.Log("Active Skill Game Time Active");
            PlusGameTimeActive(InGameCtrl.Instance.ListNekoPassive[pEquipIndex].TimeActivePlus);
        }

        if(ListNekoPassive[pEquipIndex].IsRemoveSpecialBlock) {

        }


        if (ListNekoPassive[pEquipIndex].IsRemoveSomeLine) {
            RemoveLines(ListNekoPassive[pEquipIndex].RemoveSomeLine);
        }

        if (ListNekoPassive[pEquipIndex].IsChangeBoardBlockColor) {
            ChangeAllIdleBlockColorToSame();
        }

        if (ListNekoPassive[pEquipIndex].IsAccelBombCreate) {
            AccelerateBombCreate(ListNekoPassive[pEquipIndex].AccelBombCreate);
        }

    }

    #region 특수 블록제거 액티브 스킬 

    /// <summary>
    /// 특수 블록을 제거 
    /// </summary>
    /// <param name="pBlockCount"></param>
    void RemoveSpecialBlocks(int pBlockCount, int pIndex) {

        // 쿠키, 스톤, 생선굽기 미션에서만 동작.
        if (!IsCookieMission && !IsStoneMission && !IsFishMission )
            return;

        // 없애야 하는 특수 블록 개수만큼, 리스트로 받아온다. 
        _listSpecialTargetBlock = FindSpecialTargetBlock(pBlockCount);

        


        // 번개 소환 







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

    /// <summary>
    /// 타겟이 되는 블록을 선정 
    /// </summary>
    /// <returns></returns>
    List<BlockCtrl> FindSpecialTargetBlock(int pBlockCount) {

        List<BlockCtrl> listTargets = new List<BlockCtrl>();

        // 우선순위는 그릴-바위-쿠키의 순서
        if (IsFishMission) {
            for (int i = 0; i < GameSystem.Instance.Height; i++) {
                for (int j = 0; j < GameSystem.Instance.Width; j++) {
                    if (fieldBlocks[i, j].IsFishGrill && !fieldBlocks[i, j].isMatchingChecked) {
                        listTargets.Add(fieldBlocks[i, j]);
                    }
                }
            }
        }

        if (listTargets.Count >= pBlockCount)
            return listTargets;


        if (IsStoneMission) {
            for (int i = 0; i < GameSystem.Instance.Height; i++) {
                for (int j = 0; j < GameSystem.Instance.Width; j++) {
                    if (fieldBlocks[i, j].IsStone && !fieldBlocks[i, j].isMatchingChecked) {
                        listTargets.Add(fieldBlocks[i, j]);
                    }
                }
            }
        }


        if (listTargets.Count >= pBlockCount)
            return listTargets;


        // 쿠키 수집
        if (IsCookieMission) {

            for (int i = 0; i < GameSystem.Instance.Height; i++) {
                for (int j = 0; j < GameSystem.Instance.Width; j++) {
                    if (fieldBlocks[i, j].IsCookie && !fieldBlocks[i, j].isMatchingChecked) {
                        listTargets.Add(fieldBlocks[i, j]);
                    }
                }
            }
        }

        return listTargets;

    }

    #endregion




    /// <summary>
    /// 보드상의 일부 라인에서 일반 블록을 모두 제거 
    /// </summary>
    /// <param name="pLine"></param>
    void RemoveLines(int pLine) {

        int direction = UnityEngine.Random.Range(0, 2);
        int line1 = -1, line2 = -1;

        _listRemoveLineBlock.Clear();



        // 가로로 할것인지, 세로로 할것인지 고른다.
        // 0 이면 가로, i가 고정 

        if (direction == 0) {

            if (pLine == 1)
                line1 = UnityEngine.Random.Range(0, 9);
            else {
                line1 = UnityEngine.Random.Range(0, 5);
                line2 = UnityEngine.Random.Range(5, 9);
            }

            if (line1 >= 0) {
                for (int j = 0; j < 9; j++) {
                    if (fieldBlocks[line1, j].currentState == BlockState.Idle)
                        _listRemoveLineBlock.Add(fieldBlocks[line1, j]);
                }
            }

            if (line2 >= 0) { // 두개의 라인까지 없앨 수 있다.
                for (int j = 0; j < 9; j++) {
                    if (fieldBlocks[line2, j].currentState == BlockState.Idle)
                        _listRemoveLineBlock.Add(fieldBlocks[line2, j]);
                }
            }

        } // 가로 끝 
        else {
            // 1 이면 세로, j가 고정 
            if (pLine == 1)
                line1 = UnityEngine.Random.Range(0, 9);
            else {
                line1 = UnityEngine.Random.Range(0, 5);
                line2 = UnityEngine.Random.Range(5, 9);
            }


            if (line1 >= 0) {
                for (int i = 0; i < 9; i++) {
                    if (fieldBlocks[i, line1].currentState == BlockState.Idle)
                        _listRemoveLineBlock.Add(fieldBlocks[i, line1]);
                }
            }

            if (line2 >= 0) { // 두개의 라인까지 없앨 수 있다.
                for (int i = 0; i < 9; i++) {
                    if (fieldBlocks[i, line2].currentState == BlockState.Idle)
                        _listRemoveLineBlock.Add(fieldBlocks[i, line2]);
                }
            }

        }

        // 수집 완료. 처리시작 


        if (_listRemoveLineBlock.Count == 0)
            return;


        // 블록파괴 
        for (int i = 0; i < _listRemoveLineBlock.Count; i++) {
            _listRemoveLineBlock[i].SetState(BlockState.DeadFromItem);
        }

        InUICtrl.Instance.AddItemBarValue(_listRemoveLineBlock.Count);

        GameSystem.Instance.IngameBlockCount += _listRemoveLineBlock.Count;

        //폭탄 파괴시에는 블록카운트 추가 
        for (int i = 0; i < _listRemoveLineBlock.Count; i++) {
            if (_listRemoveLineBlock[i].blockID == 0) {
                GameSystem.Instance.MatchedBlueBlock++;
            }
            else if (_listRemoveLineBlock[i].blockID == 1) {
                GameSystem.Instance.MatchedYellowBlock++;
            }
            else if (_listRemoveLineBlock[i].blockID == 2) {
                GameSystem.Instance.MatchedRedBlock++;
            }
            else if (_listRemoveLineBlock[i].blockID == 3) {
                GameSystem.Instance.MatchedGreenBlock++;
            }

            // UI 처리 
            InUICtrl.Instance.SetMinusMissionCount(SpecialMissionType.block);
        }


        // 스톤, 그릴, 폭죽 등 특수블록 처리 
        ProceedAroundSpecialBlocks(_listRemoveLineBlock, false);
    }


    /// <summary>
    /// 수치만큼 폭탄 생성을 가속화
    /// </summary>
    /// <param name="pValue"></param>
    void AccelerateBombCreate(int pValue) {
        InUICtrl.Instance.AddItemBarValue(pValue);
    }

    /// <summary>
    /// 보드상의 모든 일반 블록을 같은 색으로 변경 
    /// </summary>
    void ChangeAllIdleBlockColorToSame() {

        int randomBlockID = UnityEngine.Random.Range(0, ColorCount);

        for (int i = 0; i < GameSystem.Instance.Height; i++) {
            for (int j = 0; j < GameSystem.Instance.Width; j++) {

                if (fieldBlocks[i, j].currentState == BlockState.Idle) {
                    fieldBlocks[i, j].SetBlockType(randomBlockID);
                }
            }
        }
    }


    /// <summary>
    /// 게임타임 증가 액티브 스킬
    /// </summary>
    /// <param name="pTime"></param>
    private void PlusGameTimeActive(float pTime) {
        // 값 증가 
        InGameCtrl.Instance.AddGameTime(pTime);

        // 연출 효과 
        //InUICtrl.Instance.ShowActiveSkill(NekoSkillType.time_active, pTime);
    }





	private void AddNekoDamageInfo(int pNekoID, int pDamage) {

		NekoDamageInfo nekoDamage;

		for(int i=0; i<GameSystem.Instance.ListNekoDamageInfo.Count; i++){

			if(GameSystem.Instance.ListNekoDamageInfo[i].nekoID == pNekoID) {

				nekoDamage = GameSystem.Instance.ListNekoDamageInfo[i];
				nekoDamage.damage += pDamage;
				GameSystem.Instance.ListNekoDamageInfo[i] = nekoDamage;
				//GameSystem.Instance.ListNekoDamageInfo[i].damage = pDamage + GameSystem.Instance.ListNekoDamageInfo[i].damage;
			}
		}
	}





}
