using UnityEngine;
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
