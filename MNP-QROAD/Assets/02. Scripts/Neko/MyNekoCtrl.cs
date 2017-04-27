using UnityEngine;
using System.Collections;
using PathologicalGames;
using DG.Tweening;

public class MyNekoCtrl : MonoBehaviour {

	// neko ID 
	[SerializeField]
	private int myNekoID = -1;
    private string _namePrefix;

    [SerializeField] private NekoAppearCtrl mainSprite;

    [SerializeField] private float hitPower = 0;
    [SerializeField] private float specialAttackPower = 0;
	[SerializeField] private int level = 1;
	[SerializeField] private int grade = 1;
	private int nekoDataIndex = -1;


	private Vector3 originPos = new Vector3(-6, 1 ,0);
	private Vector3 destPos = new Vector3(-6, 0 ,0);
	private readonly Vector3[] tripleDestPos = new Vector3[3] {new Vector3(6, 7, 0), new Vector3(-6,7,0), new Vector3(-6,7,0)};

	private readonly Vector3 rushPos = new Vector3(0, 4.2f, 0);
	private Vector3 variableDustPos;
	private string idleClip = null;




	// Use this for initialization
	void Start () {

	}

	void OnSpawned() {

	}

	/// <summary>
	/// Sets my neko.
	/// </summary>
	/// <param name="pNekoID">P neko I.</param>
	public void SetMyNeko(int pNekoID, int pGrade) {
        myNekoID = pNekoID;
        grade = pGrade;


        mainSprite = InUICtrl.Instance.PlayerAppear;

        mainSprite.InitMyNekoAppearByTID(this.gameObject.transform,  myNekoID);



        // index 부터 찾는다. 
        nekoDataIndex = GameSystem.Instance.FindUserNekoData(myNekoID);

        // neko Data Index가 -1 인 경우 오류 처리 
        if (nekoDataIndex < 0) {

            Debug.Log("Can't Find nekoDataIndex");

            GameSystem.Instance.SetSystemMessage("エラー", "予期せぬエラーが発生しました。\n以下のコードをお問い合わせフォームよりお伝えください。\nERROR: 000000", "quit");

        }

        // 레벨을 찾아서 hitPower 세팅 
        level = GameSystem.Instance.UserNeko[nekoDataIndex]["level"].AsInt;
        hitPower = 100; // 기본 파워  , 등급에 따른 재계산 


        // 네코 파워
        hitPower = GameSystem.Instance.GetNekoInGamePower(myNekoID);
        specialAttackPower = hitPower ; // 100배 설정으로 변경 



    }


    private void LockCombo() {

		//InGameCtrl.Instance.timerLock = true;
		InGameCtrl.Instance.fromComboTime = -1;
	}

	/*
	private void ReleaseCombo() {

		if (isTutorial)
			return;


		InGameCtrl.Instance.timerLock = false;
		InGameCtrl.Instance.fromComboTime = 0;
	}
	*/


	#region Triple Attack Rush 
	/// <summary>
	/// 트리플 어택 러쉬. indx에 따라 공격 경로가 다르다 
	/// </summary>
	/// <param name="pIndx">P indx.</param>
	public void TripleRush(int pIndx) {

		

		InUICtrl.Instance.SetComboCnt(1);
		LockCombo ();

		this.transform.DOMove (rushPos, 0.5f).SetEase (Ease.InQuart).OnComplete(()=>OnCompleteTripleRushStep1(pIndx));
	}

	private void OnCompleteTripleRushStep1(int pIndx) {

		if (Debug.isDebugBuild) {
			Debug.Log("!! OnCompleteTripleRushStep1 #1");
		}

		InSoundManager.Instance.PlayPlayerTripleAttackVoice ();

		// destPos로 이동 
		this.transform.DOMove (tripleDestPos[pIndx], 0.3f).SetEase (Ease.InBack).OnComplete(OnCompleteTripleRushStep2);
		
		// Sound 처리 
		InSoundManager.Instance.PlayPlayerNekoHit ();


		// 히트시마다 Sun Effect 
		//EnemyNekoManager.Instance.PlaySunEffect (); 
		EnemyNekoManager.Instance.BlinkShortWhiteScreen ();


		// 검은 먼지 이펙트 
		StartCoroutine (MakeRushBlackDust ());

		// 마지막 공격만 네코를 쓰러뜨린다. (3단 공격은 1.5배)
		if (pIndx == 2) {
			//EnemyNekoManager.Instance.ReleaseEnemyNekoPos(); // 움직임 해제 
			//SpawnBonusGem(true);
			EnemyNekoManager.Instance.CurrentEnemyNeko.HitEnemyNeko (specialAttackPower + (specialAttackPower * 0.5f),true,true);

		} else {
			EnemyNekoManager.Instance.CurrentEnemyNeko.HitEnemyNeko (specialAttackPower + (specialAttackPower * 0.5f) ,true,true); // 2016.07 3단 공격의 설정 변경 
		}

		//Damage 합산처리
		GameSystem.Instance.FloatInGameDamage += specialAttackPower;

		// 흔들기 
		this.transform.DOShakePosition (0.1f, 2, 20, 30);

		if (Debug.isDebugBuild) {
			Debug.Log("!! OnCompleteTripleRushStep1 #2");
		}



	}

	private void OnCompleteTripleRushStep2() {
		PoolManager.Pools [PuzzleConstBox.myNekoPool].Despawn (this.transform);
		//PuzzleConstBox.nekoManager.GetCurrentNeko ().EndSpecialEffect ();

		//ReleaseCombo ();
	}

	#endregion






	#region Single Attack Rush 

	public void Rush() {

		InUICtrl.Instance.SetComboCnt(1);
		LockCombo ();

		this.transform.DOMove (rushPos, 0.5f).SetEase (Ease.InCubic).OnComplete(OnCompleteRushStep1);
		//EnemyNekoManager.Instance.PlayPlayerNekoAttackEffect ();




	}

	IEnumerator MakeRushBlackDust() {

		MakeNekoSpreadHitParticle ();

		for(int i=0; i<12; i++) {
			variableDustPos = EnemyNekoManager.Instance.GetCurrentNekoPosition();

			variableDustPos.Set(Random.Range(variableDustPos.x-1f, variableDustPos.x+1f) 
			                    ,Random.Range(variableDustPos.y-1f, variableDustPos.y+1f)
			                    ,variableDustPos.z);

			PoolManager.Pools[PuzzleConstBox.objectPool].Spawn(PuzzleConstBox.prefabDust, variableDustPos, Quaternion.identity).SendMessage("Play", false);



			yield return new WaitForSeconds(0.1f);
		}
	}


	private void MakeNekoSpreadHitParticle() {
		for (int i=0; i<10; i++) {
			
			PoolManager.Pools[PuzzleConstBox.objectPool].Spawn(PuzzleConstBox.prefabFragmentHit, EnemyNekoManager.Instance.GetCurrentNekoPosition(), Quaternion.identity);
		}
	}

	// 충돌! 
	private void OnCompleteRushStep1() {

		// 사운드 처리
		InSoundManager.Instance.PlayPlayerNekoWhip ();

		// Sun Effect
		EnemyNekoManager.Instance.BlinkWhiteScreen ();

		// 검은 먼지 이펙트 
		StartCoroutine (MakeRushBlackDust ());

		this.transform.DOShakePosition (0.4f, 3, 20, 30);
		this.transform.DOShakeRotation (0.5f, 30, 20, 30).OnComplete(OnCompleteRushStep2);

		// Enemy Neko Hit 처리 
		//EnemyNekoManager.Instance.ReleaseEnemyNekoPos(); // 움직임 해제 
		EnemyNekoManager.Instance.CurrentEnemyNeko.HitEnemyNeko (specialAttackPower ,true, true);
		//SpawnBonusGem (false);

		//Damage 합산처리
		GameSystem.Instance.FloatInGameDamage += specialAttackPower;

		// Sound 처리 
		InSoundManager.Instance.PlayPlayerNekoHit (); // Hit 사운드

		// 음성 
		InSoundManager.Instance.PlayPlayerNekoSingleAttackVoice ();

	}

	// 충돌 후 복귀 
	private void OnCompleteRushStep2() {

        
        if (this.transform.localEulerAngles.y == 0) {
            this.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else {
            this.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        // 1, 4.4f
        //this.transform.rotation = 
        this.transform.position = new Vector3 (1.8f, 4.5f, 0);
      

		this.transform.DOMove (destPos, 0.5f).SetEase (Ease.InCubic).OnComplete(OnCompleteRushStep3);

		// 종료처리
		//EnemyNekoManager.Instance.CurrentEnemyNeko.EndCenterEffect ();

	}

	// 위치 초기화 
	private void OnCompleteRushStep3() {

        if (this.transform.localEulerAngles.y == 0) {
            this.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else {
            this.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        PoolManager.Pools [PuzzleConstBox.myNekoPool].Despawn (this.transform);

		//ReleaseCombo ();

	}

	#endregion
	



	public float GetHitPower() {
		return hitPower;
	}

}
