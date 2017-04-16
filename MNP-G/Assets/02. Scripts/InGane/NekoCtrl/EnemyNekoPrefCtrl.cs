using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using PathologicalGames;
using Google2u;


public class EnemyNekoPrefCtrl : MonoBehaviour {

    [SerializeField] private NekoAppearCtrl spNeko;



    [SerializeField]
    private bool isHitting = false; // Enemy Neko가 타격중인지 확인 여부 
    [SerializeField]
    private bool isAlive = false;

    bool _isDestroyedCage = false;

    public int nekoID;
    
        


    // HP
    private NekoFollowHP nekoHPBar;
    private BossHPCtrl bossHPBar;
    [SerializeField] bool _isImmune; // 체력 닳지 않음 여부 


    private int _currentCoinReward;
    private int _currentCoinValue;

    [SerializeField] private float maxHP;
    [SerializeField] private float currentHP;

    float _halfHP;

    [SerializeField] private int minCoinReward;
    [SerializeField] private int maxCoinReward;

    Transform _rope;


    private int criticalX = 1;

    // Vectors
    private Vector3 variableDustPos;

    private Vector3 _initLocalPos;


    [SerializeField]
    bool _isBoss = false;

    


    // Use this for initialization
    void Start() {
        //spNekoHitFX.AnimationCompleted += AnimationCompleteDelegate;
       
    }


    /// <summary>
    /// Raises the trigger enter event.
    /// </summary>
    /// <param name="pCol">P col.</param>
    void OnTriggerEnter(Collider pCol) {

        if (pCol.tag != PuzzleConstBox.tagFoot && pCol.tag != PuzzleConstBox.tagBigFoot && pCol.tag != PuzzleConstBox.tagFireworkBolt)
            return;

        if(pCol.tag == PuzzleConstBox.tagFireworkBolt) {
            this.HitEnemyNeko(300, true);

            InGameCtrl.Instance.DoFirework();

            return;
        }

        criticalX = 1;

        if (pCol.tag == PuzzleConstBox.tagBigFoot)
            criticalX = 4;

        this.HitEnemyNeko(GameSystem.Instance.BlockAttackPower * criticalX, true);


    }


    public void SetCloneNeko(int pNekoID) {

        this.transform.localPosition = new Vector3(0, 3, 0);
        nekoID = pNekoID;
        spNeko.InitRescueNekoCloneAppear(this.gameObject.transform, nekoID);
    }

    /// <summary>
    /// 구출 고양이 세팅 
    /// </summary>
    /// <param name="pNekoID">P neko I.</param>
    /// <param name="pNekoFollowHP">P neko follow H.</param>
    public void SetRescueNeko(int pNekoID, int pHP, NekoFollowHP pNekoFollowHP) {

        // 살아있음 처리 
        isAlive = true;
        IsBoss = false;


        // 구출 스테이지에서만 HP가 줄어들도록 설정한다.
        _isImmune = true;
        if (InGameCtrl.Instance.IsRescueStage)
            _isImmune = false;

        if (pHP == 0) {
            pHP = 1;
            _isImmune = true;
        }


        _isDestroyedCage = false;

        nekoID = pNekoID;

        // 외형 설정 
        spNeko.InitRescueNekoAppear(this.gameObject.transform, nekoID);


        // 고양이 고유값 설정 
        maxHP = pHP;
        minCoinReward = EnemyNekoManager.Instance.GetCoinMin(GameSystem.Instance.PlayStage);
        maxCoinReward = EnemyNekoManager.Instance.GetCoinMax(GameSystem.Instance.PlayStage);

        _halfHP = currentHP / 2;
        currentHP = maxHP;

        nekoHPBar = pNekoFollowHP;
        nekoHPBar.InitNekoFollowHP(this.gameObject, false);
        nekoHPBar.SetNekoHP(maxHP, currentHP);

        if (_isImmune)
            nekoHPBar.gameObject.SetActive(false);

        //this.transform.localPosition = new Vector3(-0.2f, 0.5f, 0); // 위치 초기화 
        
        // this.transform.DOLocalMoveY(this.transform.localPosition.y + 0.2f, 1).SetLoops(-1, LoopType.Yoyo); // 무브먼트
        EnemyNekoManager.Instance.MoveRope();

        _rope = EnemyNekoManager.Instance.Rope.transform;
        _initLocalPos = _rope.localPosition;

    }


    /// <summary>
    /// 고양이 비활성화 
    /// </summary>
    public void InactiveNeko() {
        spNeko.gameObject.SetActive(false);
        this.gameObject.SetActive(false);
    }




    /// <summary>
    /// 보스 고양이 세팅 
    /// </summary>
    /// <param name="pNekoID"></param>
    /// <param name="pHP"></param>
    /// <param name="pNekoFollowHP"></param>
    public void SetBossNeko(int pNekoID, int pHP, BossHPCtrl pBossHP) {

        // 살아있음 처리 
        isAlive = true;
        IsBoss = true;

        // 보스 스테이지에서만 HP가 줄어들도록 설정한다.
        _isImmune = true;
        if (InGameCtrl.Instance.IsBossStage)
            _isImmune = false;

        if (pHP == 0) {
            pHP = 1;
            _isImmune = true;
        }

        nekoID = pNekoID;

        // 외형 설정 
        spNeko.InitBossNekoAppear(this.gameObject.transform, nekoID);


        // 고양이 고유값 설정 
        maxHP = pHP;
        minCoinReward = EnemyNekoManager.Instance.GetCoinMin(GameSystem.Instance.PlayStage);
        maxCoinReward = EnemyNekoManager.Instance.GetCoinMax(GameSystem.Instance.PlayStage);
        


        currentHP = maxHP;

        
        bossHPBar = pBossHP;
        bossHPBar.gameObject.SetActive(true);
        bossHPBar.SetNekoHP(maxHP, currentHP);

        if(InGameCtrl.Instance.IsBossStage) {
            _initLocalPos = this.transform.localPosition;
            this.transform.DOLocalMoveY(this.transform.localPosition.y + 0.2f, 1).SetLoops(-1, LoopType.Yoyo);
        }

        


        // 보스는 처음에 collider를 비활성화
        //this.GetComponent<BoxCollider>().enabled = false;
    }

    /// <summary>
    ///  옆으로 빼놓기.
    /// </summary>
    public void SetSideNeko() {
        this.transform.localPosition = new Vector3(5, 4, 0);
    }





    public void OnSpawned() {

        this.transform.rotation = Quaternion.identity;
        this.transform.localScale = GameSystem.Instance.BaseScale;
     
    }

    public void OnDespawned() {
        this.transform.DOKill();
    }





    /// <summary>
    /// 반파된 케이지로 변경 처리 
    /// </summary>
    void SetDestroyedCage() {

        Debug.Log("★ SetDestroyedCage");

        _isDestroyedCage = true;


        //spNeko
        spNeko.SetHalfCage();
    }



    /// <summary>
    /// Gets the current neko position.
    /// </summary>
    /// <returns>The current neko position.</returns>
    public Vector3 GetCurrentNekoPosition() {
        return this.transform.position;
    }

    #region Hitting 


    /// <summary>
    /// Enemy 네코 타격 처리 
    /// </summary>
    /// <param name="pDamage">P damage.</param>
    public void HitEnemyNeko(float pDamage, bool isKillEnemyNeko) {
        HitEnemyNeko(pDamage, isKillEnemyNeko, false);
    }

    public void HitEnemyNeko(float pDamage, bool isKillEnemyNeko, bool isHitFXPlay) {
        if (!isAlive)
            return;


        // 점수 효과
        PoolManager.Pools["UI"].Spawn("EnemyNekoHitText", this.transform.position, Quaternion.identity).GetComponent<EnemyNekoHitTextCtrl>().SetTarget(this.gameObject, pDamage);

        RewardChub();


        // HP 감소 
        if (!_isImmune) {
            currentHP -= pDamage;

            if(IsBoss)
                bossHPBar.SetNekoHP(maxHP, currentHP);
            else
                nekoHPBar.SetNekoHP(maxHP, currentHP);


        }

        if(!IsBoss && currentHP < _halfHP && !_isDestroyedCage ) {
            SetDestroyedCage();
        }


        // 공격중일때는 흔들지 않음. 
        if (!isHitting) {

            if(IsBoss) 
                this.transform.DOPunchPosition(new Vector3(0.3f, 0.3f, 0), 0.2f, 10, 1).OnStart(OnStartHitting).OnComplete(OnCompleteHitting).OnKill(OnCompleteHitting);
            else
                _rope.DOPunchPosition(new Vector3(0.3f, 0.3f, 0), 0.2f, 10, 1).OnStart(OnStartHitting).OnComplete(OnCompleteHitting).OnKill(OnCompleteHitting);

            // Hit FX 
            //if (isHitFXPlay && !spNekoHitFX.IsPlaying(EnemyNekoManager.Instance.ClipNekoBigHitFX)) {
            if (isHitFXPlay ) {
                PlayBigHitFX();
            }
        }


        // HP가 0 미만이 되면 
        if (currentHP <= 0 && isKillEnemyNeko) {

            RescueNeko();

            //KillEnemyNeko();
            //EnemyNekoManager.Instance.SetNextNeko();
        }
    }


    private void OnStartHitting() {
        isHitting = true;
    }

    private void OnCompleteHitting() {
        isHitting = false;

        if (IsBoss)
            this.transform.localPosition = _initLocalPos;
        else
            _rope.localPosition = _initLocalPos;
    }


    private void PlayBigHitFX() {
        //spNekoHitFX.gameObject.SetActive(true);
        //spNekoHitFX.Play(EnemyNekoManager.Instance.ClipNekoBigHitFX);

        // 
        StartCoroutine(SpurtStarFragment());

    }

    IEnumerator SpurtStarFragment() {
        for (int i = 0; i < 8; i++) {
            PoolManager.Pools[PuzzleConstBox.objectPool].Spawn(PuzzleConstBox.prefabFragmentHit, GetCurrentNekoPosition(), Quaternion.identity);
            yield return new WaitForSeconds(0.05f);
        }
    }


    /// <summary>
    /// 현재 Enemy Neko Kill 처리 
    /// </summary>
    public void RescueNeko() {

        // Win Neko Add 처리
        //GameSystem.Instance.ListIngameWinNekoInfo.Add(this.nekoID);

        isAlive = false; // 살아있지 않음. 
        
        this.transform.DOKill(); // 움직임 제거 


        if(IsBoss) {

            // 게이지 비활성화
            bossHPBar.HideNekoHP();

            // 중앙으로 이동 
            this.transform.DOLocalMove(new Vector3(0, 0.5f, 0), 1).OnComplete(OnCompleteBossNekoMove);

            // 보스 킬 달성 처리 
            GameSystem.Instance.InGameDestroyUFO++;

        }
        else {
            EnemyNekoManager.Instance.PullRope(); // 로프를 당긴다. 
            
            spNeko.SetHideCage();
            nekoHPBar.gameObject.SetActive(false);

            // 클론 고양이를 움직인다.
            EnemyNekoManager.Instance.RescueCloneNeko.transform.DOLocalMove(new Vector3(-0.75f, 0.25f, 0), 1).SetDelay(0.5f).OnComplete(OnCompleteRescueNeko);

            GameSystem.Instance.InGameRescueNeko++;
        }

        RewardNekoKillBonus();
        RewardNekoCoin();
    }


    /// <summary>
    /// 
    /// </summary>
    void OnCompleteBossNekoMove() {
        // 떨림 
        this.transform.DOLocalMoveY(this.transform.localPosition.y + 0.1f, 0.2f).SetLoops(-1, LoopType.Yoyo);

        // 땀방울 표시
        spNeko.OnNekoDrop();

        // 보스 말풍선 띄운다.
        EnemyNekoManager.Instance.OnBossNekoSpeech();

        StartCoroutine(KillingBossNeko());
    }

    IEnumerator KillingBossNeko() {

        // 2초 지연 
        yield return new WaitForSeconds(2);

        // 회전 처리 , 날아감. 
        this.transform.DORotate(new Vector3(0, 0, 720), 1, RotateMode.FastBeyond360).SetEase(Ease.Linear);
        this.transform.DOScale(0, 2); // 크기 작아지게.

        Vector3 idleMovingPos = this.transform.position;
        idleMovingPos.x = idleMovingPos.x + 5;
        idleMovingPos.y = idleMovingPos.y + 5;


        this.transform.DOMove(idleMovingPos, 2).OnComplete(OnCompleteKillNeko);


        // 사운드 처리
        InSoundManager.Instance.PlayEnemyNekoKill();
    }


    /// <summary>
    /// 네코 구출 완료!
    /// </summary>
    void OnCompleteRescueNeko() {
        // this.transform.DOLocalMoveY(this.transform.localPosition.y + 0.2f, 1).SetLoops(-1, LoopType.Yoyo);

        // 고마워 말풍선 띄운다.
        EnemyNekoManager.Instance.OnRescueNekoSpeech();
    }

    private void OnCompleteKillNeko() {
        spNeko.KillNekoAppear();
        
    }


    /// <summary>
    /// Kill 마다 스코어 보상 
    /// </summary>
    private void RewardNekoKillBonus() {


        // 킬 보너스 추가 
        //InUICtrl.Instance.SetNekoKillBonus(KillBonusScore);
        
    }

    /// <summary>
    /// Kill 마다 Coin 보상 
    /// </summary>
    private void RewardNekoCoin() {

        // NekoCoin 소환 
        _currentCoinReward = Random.Range(minCoinReward, maxCoinReward + 1);
        _currentCoinValue = 2; // 코인 1개당 코인 증가량 

        if (_currentCoinReward < 1) {
            return;
        }

        // 효과음 재생
        //InSoundManager.Instance.PlayEnemySpreadGold (_currentCoinReward);


        // 효과음과 객체관리를 위해 개수를 제한한다. 
        if (_currentCoinReward > 20 && _currentCoinReward < 40) {
            _currentCoinReward += _currentCoinReward % 4; // 나머지만큼을 더해주고.

            _currentCoinReward = _currentCoinReward / 4;
            _currentCoinValue *= 4; // 화폐량은 2를 곱한다. 
        }
        else if (_currentCoinReward >= 40 && _currentCoinReward < 100) {
            _currentCoinReward -= _currentCoinReward % 8;
            _currentCoinReward = _currentCoinReward / 8;
            _currentCoinValue *= 8; // 4를 곱해준다. 
        }
        else if (_currentCoinReward >= 100) {
            _currentCoinReward -= _currentCoinReward % 20;
            _currentCoinReward = _currentCoinReward / 20;

            _currentCoinValue *= 20;
        }

        for (int i = 0; i < _currentCoinReward; i++) {
            // 소환하면서 value 세팅 
            PoolManager.Pools[PuzzleConstBox.objectPool].Spawn("NekoCoinPrefab", this.transform.position, Quaternion.identity).GetComponent<NekoCoinCtrl>().SetValue(_currentCoinValue);
        }


        // 생선 
        //RewardChub();
    }



    /// <summary>
    /// 생선 드롭 , 티켓 드롭 
    /// </summary>
    private void RewardChub() {
        //10 분의 1 확률 (Chub 보상)
        if (Random.Range(0, 100) < 1)
            PoolManager.Pools[PuzzleConstBox.objectPool].Spawn("ChubRewardPrefab", this.transform.position, Quaternion.identity).GetComponent<ChubRewardCtrl>().SpawnChub();

		// 5% 확률 Ticket 보상 (이벤트 기간에만 활성화)
		/*
        if (Random.Range(0, 100) < 5) {
            PoolManager.Pools[PuzzleConstBox.objectPool].Spawn("ChubRewardPrefab", this.transform.position, Quaternion.identity).GetComponent<ChubRewardCtrl>().SpawnTicket();
        }
        */
    }



    public void MakeBlackDustOnce() {
        MakeBlackDustOnce(false);
    }

    public void MakeBlackDustOnce(bool alwaysFlag) {
        variableDustPos = GetCurrentNekoPosition();
        //variableDustPos = GetNekoHitArea();
        variableDustPos.Set(Random.Range(variableDustPos.x - 0.3f, variableDustPos.x + 0.5f)
                            , Random.Range(variableDustPos.y - 0.2f, variableDustPos.y + 0.5f)
                            , variableDustPos.z);

        //PoolManager.Pools[PuzzleConstBox.objectPool].Spawn(PuzzleConstBox.prefabDust, variableDustPos, Quaternion.identity).SendMessage("Play", false);
        if (Random.Range(0, 3) == 1) {
            //PoolManager.Pools[PuzzleConstBox.objectPool].Spawn(GameSystem.Instance.particleSmokePuff, variableDustPos, Quaternion.identity);
            PoolManager.Pools[PuzzleConstBox.objectPool].Spawn(PuzzleConstBox.prefabDust, variableDustPos, Quaternion.identity).GetComponent<DustCtrl>().PlayDustWhite();
        }
    }





    #endregion


    /// <summary>
    /// Animations the complete delegate.
    /// </summary>
    /// <param name="pSprite">P sprite.</param>
    /// <param name="pClip">P clip.</param>
    public void AnimationCompleteDelegate(tk2dSpriteAnimator pSprite, tk2dSpriteAnimationClip pClip) {
        if (pClip.name == EnemyNekoManager.Instance.ClipNekoAttackFX) {
            pSprite.gameObject.SetActive(false);
        }
        else if (pClip.name == EnemyNekoManager.Instance.ClipNekoBigHitFX) {
            pSprite.gameObject.SetActive(false);
        }
        /*
		else if (pClip.name == EnemyNekoManager.Instance.ClipNekoThunder) {
			pSprite.gameObject.SetActive (false);
		}
		*/
    }


    #region Properties 

    


    public bool IsBoss {
        get {
            return _isBoss;
        }

        set {
            _isBoss = value;
        }
    }


    #endregion
}
