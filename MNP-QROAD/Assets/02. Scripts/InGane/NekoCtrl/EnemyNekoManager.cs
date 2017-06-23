using UnityEngine;
using System.Collections;
using PathologicalGames;
using DG.Tweening;
using Google2u;
using SimpleJSON;

/// <summary>
/// 게임은 3가지 종류로 나뉜다.
/// 1. 일반 미션 
/// 2. 구출 미션
/// 3. 보스 미션
/// 
/// 일반 미션에서는 모든 HP가 비활성화 된다.
/// 구출 미션에서는 구출 대상 고양이의 HP만 활성화된다. 
/// 보스 미션에서는 보스 고양이만 등장한다. 
/// </summary>
public class EnemyNekoManager : MonoBehaviour {
	
	
	static EnemyNekoManager _instance = null;
    JSONNode _currentStageNode;
    

    [SerializeField] private EnemyNekoPrefCtrl _rescueNeko; // 구출 대상 고양이
    [SerializeField] private EnemyNekoPrefCtrl _rescueCloneNeko; // 구출 대상 고양이

    [SerializeField] private EnemyNekoPrefCtrl _bossNeko; // 보스 고양이 

    [SerializeField] GameObject _bossNekoSpeech;
    [SerializeField] GameObject _rescueNekoSpeech;

    [SerializeField] UILabel _lblRescueNekoSpeech;
    [SerializeField] UILabel _lblBossNekoSpeech;


    [SerializeField] private NekoFollowHP nekoHPBar = null;
    [SerializeField] private BossHPCtrl bossHPBar = null;

        [SerializeField] GameObject _bossFireLeft;
    [SerializeField] GameObject _bossFireRight;

    [SerializeField]
    GameObject _rope;

    
	
    [SerializeField]
    tk2dSpriteAnimator _splashScreen;

    
    
	
	// clips 
	private readonly string clipNekoAttackFX = "ClipWhiteLight"; // FX Clip
	private readonly string clipNekoBigHitFX = "ClipWhiteLight";
    readonly string APPEAR_NEKO_ID = "appearnekoid";
    readonly string APPEAR_NEKO_HP = "appearnekohp";

    private Vector3 centerPos = new Vector3(0, 4.4f, 0);
	
	
	private int nekoDice; // Neko Spawn 생성용도 
    private int nekoDice2; // Neko Spawn 생성용도 
    private int nekoSpawnNumber; // Neko Spawn 생성용도 

    public static EnemyNekoManager Instance {
		get {
			if(_instance == null) {
				_instance = FindObjectOfType(typeof(EnemyNekoManager)) as EnemyNekoManager;
				
				if(_instance == null) {
					Debug.Log("EnemyNekoManager Init Error");
					return null;
				}
			}
			
			return _instance;
		}
	}
	
	void Awake() {
        
		//enemyNekoSpawnIndx = 32;
	}
	
	// Use this for initialization
	void Start () {
        // bossHPBar.gameObject.SetActive(false);


        SetBossFire(false);

    }
	

    public void SetInactiveManager() {
        Rope.SetActive(false);
        BossNeko.gameObject.SetActive(false);
        bossHPBar.gameObject.SetActive(false);
    }

    /// <summary>
    /// 스테이지 네코 설정 
    /// </summary>
    public void InitStageRescueNeko() {

        // 현재 스테이지의 기준정보 노드 
        CurrentStageNode = GameSystem.Instance.GetCurrentStageNode();


        // 보스전 세팅 
        if (InGameCtrl.Instance.GetBossStage()) {

            Debug.Log("★ InitStageRescueNeko Boss Stage");

            // 보스 고양이만 활성화 한다.
            BossNeko.SetBossNeko(CurrentStageNode[APPEAR_NEKO_ID].AsInt, CurrentStageNode[APPEAR_NEKO_HP].AsInt, bossHPBar);
            
            
            RescueNeko.InactiveNeko();
            RescueCloneNeko.InactiveNeko();

            Rope.gameObject.SetActive(false);

            SetBossFire(true);
        }
        else {

            Debug.Log("★ InitStageRescueNeko Not Boss Stage");

            InitRope();

            RescueNeko.SetRescueNeko(CurrentStageNode[APPEAR_NEKO_ID].AsInt, CurrentStageNode[APPEAR_NEKO_HP].AsInt, nekoHPBar);
            RescueCloneNeko.SetCloneNeko(CurrentStageNode[APPEAR_NEKO_ID].AsInt);

            //BossNeko.SetBossNeko(_stageOrder.Rows[GetStageRowID(GameSystem.Instance.PlayStage)]._boss_id, _stageOrder.Rows[GetStageRowID(GameSystem.Instance.PlayStage)]._boss_HP, bossHPBar);
            BossNeko.InactiveNeko();
            bossHPBar.gameObject.SetActive(false);
            //BossNeko.SetSideNeko();
        }




        //_stageNekoGroup.DOLocalMoveY(0.3f, 1).SetLoops(-1, LoopType.Yoyo);
    }


    /// <summary>
    /// 구출한 네코 말풍선 띄우기 
    /// </summary>
    public void OnRescueNekoSpeech() {

        Rope.gameObject.SetActive(false);
        RescueNeko.InactiveNeko();

        StartCoroutine(ShowingRescueNekoSpeech());

    }

    IEnumerator ShowingRescueNekoSpeech() {

        this._rescueNekoSpeech.SetActive(true);
        _lblRescueNekoSpeech.text = GameSystem.Instance.GetLocalizeText(MNP_Localize.rowIds.L3435);


        if (InGameCtrl.Instance.IsRescueStage) {
            // 게임 플레이 종료 
            InGameCtrl.Instance.StopPlaying();
        }

        yield return new WaitForSeconds(2);

        this._rescueNekoSpeech.SetActive(false);




    }



    /// <summary>
    ///  보스네코 말풍선 띄우기 
    /// </summary>
    public void OnBossNekoSpeech() {

        StartCoroutine(ShowingBossNekoSpeech());

    }

    IEnumerator ShowingBossNekoSpeech() {

        this._bossNekoSpeech.SetActive(true);

        _lblBossNekoSpeech.text = GameSystem.Instance.GetLocalizeText(MNP_Localize.rowIds.L3436);

        yield return new WaitForSeconds(2);

        this._bossNekoSpeech.SetActive(false);

        // 게임 플레이 종료 
        InGameCtrl.Instance.StopPlaying();
    }


    /// <summary>
    /// Gets the current neko position.
    /// </summary>
    /// <returns>The current neko position.</returns>
    public Vector3 GetCurrentNekoPosition() {
		return CurrentEnemyNeko.GetCurrentNekoPosition ();
	}
	
	

	
	/// <summary>
	/// Splash 스크린 효과 
	/// </summary>
	public void BlinkWhiteScreen() {
        //_splashScreen.gameObject.SetActive(true);
        //_splashScreen.Play();
        //_splashScreen.AnimationCompleted = AnimationCompleteDelegate;
    }

    private void AnimationCompleteDelegate(tk2dSpriteAnimator pSprite, tk2dSpriteAnimationClip pClip) {
        pSprite.gameObject.SetActive(false);
    }

        
	
	/// <summary>
	/// 화이트 스크린 효과 
	/// </summary>
	public void BlinkShortWhiteScreen() {
        //_splashScreen.gameObject.SetActive(true);
        //_splashScreen.Play();
        //_splashScreen.AnimationCompleted = AnimationCompleteDelegate;
    }
	

    private int GetStageRowID(int pStage) {
        switch(pStage) {
            case 1: return (int)MNP_NekoGameOrder.rowIds.Stage1;
            case 2: return (int)MNP_NekoGameOrder.rowIds.Stage2;
            case 3: return (int)MNP_NekoGameOrder.rowIds.Stage3;
            case 4: return (int)MNP_NekoGameOrder.rowIds.Stage4;
            case 5: return (int)MNP_NekoGameOrder.rowIds.Stage5;
            case 6: return (int)MNP_NekoGameOrder.rowIds.Stage6;
            case 7: return (int)MNP_NekoGameOrder.rowIds.Stage7;
            case 8: return (int)MNP_NekoGameOrder.rowIds.Stage8;
            case 9: return (int)MNP_NekoGameOrder.rowIds.Stage9;
            case 10: return (int)MNP_NekoGameOrder.rowIds.Stage10;
            case 11: return (int)MNP_NekoGameOrder.rowIds.Stage11;
            case 12: return (int)MNP_NekoGameOrder.rowIds.Stage12;
            case 13: return (int)MNP_NekoGameOrder.rowIds.Stage13;
            case 14: return (int)MNP_NekoGameOrder.rowIds.Stage14;
            case 15: return (int)MNP_NekoGameOrder.rowIds.Stage15;
            case 16: return (int)MNP_NekoGameOrder.rowIds.Stage16;
            case 17: return (int)MNP_NekoGameOrder.rowIds.Stage17;
            case 18: return (int)MNP_NekoGameOrder.rowIds.Stage18;
            case 19: return (int)MNP_NekoGameOrder.rowIds.Stage19;
            case 20: return (int)MNP_NekoGameOrder.rowIds.Stage20;
            case 21: return (int)MNP_NekoGameOrder.rowIds.Stage21;
            case 22: return (int)MNP_NekoGameOrder.rowIds.Stage22;
            case 23: return (int)MNP_NekoGameOrder.rowIds.Stage23;
            case 24: return (int)MNP_NekoGameOrder.rowIds.Stage24;
            case 25: return (int)MNP_NekoGameOrder.rowIds.Stage25;
            case 26: return (int)MNP_NekoGameOrder.rowIds.Stage26;
            case 27: return (int)MNP_NekoGameOrder.rowIds.Stage27;
            case 28: return (int)MNP_NekoGameOrder.rowIds.Stage28;
            case 29: return (int)MNP_NekoGameOrder.rowIds.Stage29;
            case 30: return (int)MNP_NekoGameOrder.rowIds.Stage30;
            case 31: return (int)MNP_NekoGameOrder.rowIds.Stage31;
            case 32: return (int)MNP_NekoGameOrder.rowIds.Stage32;
            case 33: return (int)MNP_NekoGameOrder.rowIds.Stage33;
            case 34: return (int)MNP_NekoGameOrder.rowIds.Stage34;
            case 35: return (int)MNP_NekoGameOrder.rowIds.Stage35;
            case 36: return (int)MNP_NekoGameOrder.rowIds.Stage36;
            case 37: return (int)MNP_NekoGameOrder.rowIds.Stage37;
            case 38: return (int)MNP_NekoGameOrder.rowIds.Stage38;
            case 39: return (int)MNP_NekoGameOrder.rowIds.Stage39;
            case 40: return (int)MNP_NekoGameOrder.rowIds.Stage40;
            case 41: return (int)MNP_NekoGameOrder.rowIds.Stage41;
            case 42: return (int)MNP_NekoGameOrder.rowIds.Stage42;
            case 43: return (int)MNP_NekoGameOrder.rowIds.Stage43;
            case 44: return (int)MNP_NekoGameOrder.rowIds.Stage44;
            case 45: return (int)MNP_NekoGameOrder.rowIds.Stage45;
            case 46: return (int)MNP_NekoGameOrder.rowIds.Stage46;
            case 47: return (int)MNP_NekoGameOrder.rowIds.Stage47;
            case 48: return (int)MNP_NekoGameOrder.rowIds.Stage48;
            case 49: return (int)MNP_NekoGameOrder.rowIds.Stage49;
            case 50: return (int)MNP_NekoGameOrder.rowIds.Stage50;
            case 51: return (int)MNP_NekoGameOrder.rowIds.Stage51;
            case 52: return (int)MNP_NekoGameOrder.rowIds.Stage52;
            case 53: return (int)MNP_NekoGameOrder.rowIds.Stage53;
            case 54: return (int)MNP_NekoGameOrder.rowIds.Stage54;
            case 55: return (int)MNP_NekoGameOrder.rowIds.Stage55;
            case 56: return (int)MNP_NekoGameOrder.rowIds.Stage56;
            case 57: return (int)MNP_NekoGameOrder.rowIds.Stage57;
            case 58: return (int)MNP_NekoGameOrder.rowIds.Stage58;
            case 59: return (int)MNP_NekoGameOrder.rowIds.Stage59;
            case 60: return (int)MNP_NekoGameOrder.rowIds.Stage60;
            case 61: return (int)MNP_NekoGameOrder.rowIds.Stage61;
            case 62: return (int)MNP_NekoGameOrder.rowIds.Stage62;
            case 63: return (int)MNP_NekoGameOrder.rowIds.Stage63;
            case 64: return (int)MNP_NekoGameOrder.rowIds.Stage64;
            case 65: return (int)MNP_NekoGameOrder.rowIds.Stage65;
            case 66: return (int)MNP_NekoGameOrder.rowIds.Stage66;
            case 67: return (int)MNP_NekoGameOrder.rowIds.Stage67;
            case 68: return (int)MNP_NekoGameOrder.rowIds.Stage68;
            case 69: return (int)MNP_NekoGameOrder.rowIds.Stage69;
            case 70: return (int)MNP_NekoGameOrder.rowIds.Stage70;
            case 71: return (int)MNP_NekoGameOrder.rowIds.Stage71;
            case 72: return (int)MNP_NekoGameOrder.rowIds.Stage72;
            case 73: return (int)MNP_NekoGameOrder.rowIds.Stage73;
            case 74: return (int)MNP_NekoGameOrder.rowIds.Stage74;
            case 75: return (int)MNP_NekoGameOrder.rowIds.Stage75;
            case 76: return (int)MNP_NekoGameOrder.rowIds.Stage76;
            case 77: return (int)MNP_NekoGameOrder.rowIds.Stage77;
            case 78: return (int)MNP_NekoGameOrder.rowIds.Stage78;

            default: return (int)MNP_NekoGameOrder.rowIds.Stage1;


        }
    }

    public void MoveRope() {
        Rope.transform.DOLocalMoveY(Rope.transform.localPosition.y + 0.2f, 1).SetLoops(-1, LoopType.Yoyo); // 무브먼트
    }

    public void PullRope() {
        Rope.transform.DOLocalMoveY(7, 0.4f).SetEase(Ease.InBack).OnComplete(OnCompletePullRope);
    }

    void OnCompletePullRope() {
        Rope.gameObject.SetActive(false);
        RescueNeko.InactiveNeko();
    }

    void InitRope() {
        Rope.transform.localPosition = new Vector3(0, 3.55f, 0);
    }

    
    public int GetCoinMin(int pStage) {
        return 10;
    }

    public int GetCoinMax(int pStage) {
        return 20;
    }

    public void SetBossFire(bool pFalg) {
        _bossFireLeft.SetActive(pFalg);
        _bossFireRight.SetActive(pFalg);
    }

    #region Properties 

    public string ClipNekoAttackFX {
		get {
			return clipNekoAttackFX;
		}
	}
	
	public string ClipNekoBigHitFX {
		get {
			return clipNekoBigHitFX;
		}
	}
	
	
	
	public EnemyNekoPrefCtrl RescueNeko {
		get {
			return _rescueNeko;
		}
		set {
			this._rescueNeko = value;
		}
	}

    public EnemyNekoPrefCtrl BossNeko {
        get {
            return _bossNeko;
        }

        set {
            _bossNeko = value;
        }
    }

    public EnemyNekoPrefCtrl CurrentEnemyNeko {
        get {
            if (InGameCtrl.Instance.IsBossStage)
                return _bossNeko;
            else
                return _rescueNeko;
        }
    }

    public GameObject Rope {
        get {
            return _rope;
        }

        set {
            _rope = value;
        }
    }

    public EnemyNekoPrefCtrl RescueCloneNeko {
        get {
            return _rescueCloneNeko;
        }

        set {
            _rescueCloneNeko = value;
        }
    }

    public JSONNode CurrentStageNode {
        get {
            return _currentStageNode;
        }

        set {
            _currentStageNode = value;
        }
    }



    #endregion
}
