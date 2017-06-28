using UnityEngine;
using System.Collections;
using SimpleJSON;

public class MyNekoPassiveCtrl : MonoBehaviour {

	public int NekoID = -1;
	
    JSONNode _nekoNode;
    NekoData _nekoData;

	bool _isNoneSkill = false;
	bool _isCoinPlus = false;  // 코인 획득량 증가 
	bool _isScorePlus = false; // 점수 획득량 증가
	bool _isGameTimePlus = false; // 게임시간 증가
	bool _isPowerPlus = false; // 네코 공격력 증가 
    bool _isStartBomb = false; 

    /*
    [SerializeField]
    int _nekoBombAppearBonus = 0; // 폭탄게이지 감소 보너스 
    [SerializeField]
    int _nekoSkillInvokeBonus = 0; //네코 스킬 발동 감소 보너스
    */


    private bool _isBombAppearBonus = false; // 폭탄 아이템 게이지 상승 획득
    private bool _isSkillInvokeBonus = false; // 네코 스킬 게이지 상승
    private bool _isRandomBombDrop = false; // 랜덤 폭탄 드롭 
    private bool _isTimePlusActive = false; // 타임증가 액티브
    private bool _isYellowBombDrop = false;
    private bool _isBlueBombDrop = false;
    private bool _isRedBombDrop = false;
    private bool _isBlackBombDrop = false;

    // 추가 액티브 2017.06
    bool _isRemoveSpecialBlock = false; // 스페셜 블록 제거 
    bool _isAccelBombCreate = false; // 폭탄 생성 가속화 
    bool _isChangeBoardBlockColor = false; // 블록 색상 동일하게 변경 
    bool _isRemoveSomeLine = false; // 라인 제거 

    bool _hasActiveSkill = false;

    // 실제 증가 수치 
    public float CoinPlus = 0;
	public float ScorePlus = 0;
	public float GameTimePlus = 0;
	public float PowerPlus = 0;

    int _startBombCount;
       
    [SerializeField] private int _bombAppearPlus = 0;
    [SerializeField] private int _nekoSkillInvokePlus = 0;
    [SerializeField] private int _randomBombDropCount = 0;
    [SerializeField] private float _timeActivePlus = 0;
    [SerializeField] private int _yellowBombDropCount = 0;
    [SerializeField] private int _blueBombDropCount = 0;
    [SerializeField] private int _redBombDropCount = 0;
    [SerializeField] private int _blackBombDropCount = 0;

    [SerializeField] int _removeSpecialBlockCount = 0;
    [SerializeField] int _accelBombCreate = 0;
    [SerializeField] int _removeSomeLine = 0;



    private void InitPassiveData() {
        IsScorePlus = false;
        IsCoinPlus = false;
        IsPowerPlus = false;
        IsGameTimePlus = false;
        IsStartBomb = false;
        IsBombAppearBonus = false;
        IsSkillInvokeBonus = false;
        IsRandomBombDrop = false;
        IsTimePlusActive = false;
        IsYellowBombDrop = false;
        IsBlueBombDrop = false;
        IsRedBombDrop = false;
        IsBlackBombDrop = false;

        IsRemoveSomeLine = false;
        IsChangeBoardBlockColor = false;
        IsAccelBombCreate = false;
        IsRemoveSpecialBlock = false;

        HasActiveSkill = false;

        ScorePlus = 0;
        CoinPlus = 0;
        GameTimePlus = 0;
        PowerPlus = 0;
        StartBombCount = 0;
        BombAppearPlus = 0;
        NekoSkillInvokePlus = 0;
        RandomBombDropCount = 0;
        TimeActivePlus = 0;
        YellowBombDropCount = 0;
        BlueBombDropCount = 0;
        RedBombDropCount = 0;
        BlackBombDropCount = 0;

        RemoveSpecialBlockCount = 0;
        AccelBombCreate = 0;
        RemoveSomeLine = 0;
    }

    public void SetNekoPassive(int pNekoID) {

        InitPassiveData();


        NekoID = pNekoID;

		if (NekoID < 0) {
			IsNoneSkill = true; // 아무것도 스킬이 없다. 
			return;
		}


        _nekoNode = GameSystem.Instance.GetNekoNodeByID(NekoID);
        _nekoData = GameSystem.Instance.GetNekoData(_nekoNode);


        
        SetSkillInfo(_nekoData.skillid1, _nekoData.skillvalue1);
        SetSkillInfo(_nekoData.skillid2, _nekoData.skillvalue2);
        SetSkillInfo(_nekoData.skillid3, _nekoData.skillvalue3);
        SetSkillInfo(_nekoData.skillid4, _nekoData.skillvalue4);
        
    }


    /// <summary>
    /// 스킬 정보 세팅 
    /// </summary>
    /// <param name="pSkillType"></param>
    /// <param name="pValue"></param>
    private void SetSkillInfo(int pSkillID, float pValue) {


        if (pSkillID <= 0)
            return;

        if (pSkillID >= 8)
            HasActiveSkill = true;


        switch(pSkillID) {

            case 1: // Score Bonus Passive 
                IsScorePlus = true;
                ScorePlus += pValue;
                break;

            case 2:
                IsCoinPlus = true;
                CoinPlus += pValue;
                break;

            case 3:
                IsPowerPlus = true;
                PowerPlus += pValue;
                break;

            case 4:
                IsGameTimePlus = true;
                GameTimePlus += pValue;
                break;

            case 5: // 시작 폭탄 
                IsStartBomb = true;
                StartBombCount += (int)pValue;
                break;

            case 6: // 폭탄 생성에 필요한 블록 감소 
                IsBombAppearBonus = true;
                BombAppearPlus += (int)pValue;
                break;

            case 7: // 스킬 발동에 필요한 블록 감소 
                IsSkillInvokeBonus = true;
                NekoSkillInvokePlus += (int)pValue;
                break;

            case 8: // 랜덤 폭탄 
                IsRandomBombDrop = true;
                RandomBombDropCount = (int)pValue;
                break;

            case 9: // 빨강 폭탄 
                IsRedBombDrop = true;
                RedBombDropCount = (int)pValue;
                break;

            case 10: // 파랑 폭탄 
                IsBlueBombDrop = true;
                BlueBombDropCount = (int)pValue;
                break;

            case 11: // 노랑 폭탄 
                IsYellowBombDrop = true;
                YellowBombDropCount = (int)pValue;
                break;

            case 12: // 시간 추가 액티브 
                IsTimePlusActive = true;
                TimeActivePlus = pValue;
                break;

            case 13: // 특수 블록을 n개 제거
                IsRemoveSpecialBlock = true;
                RemoveSpecialBlockCount = (int)pValue;
                break;

            case 14: // 폭탄 생성을 가속화 
                IsAccelBombCreate = true;
                AccelBombCreate = (int)pValue;
                break;

            case 15: // 블록의 색상을 동일하게 변경 
                IsChangeBoardBlockColor = true;

                break;

            case 16: // 임의의 n개의 라인을 제거
                IsRemoveSomeLine = true;
                RemoveSomeLine = (int)pValue;
                break;
                

        }

    }



    #region Properties

    public bool IsRandomBombDrop {
        get {
            return _isRandomBombDrop;
        }

        set {
            _isRandomBombDrop = value;
        }
    }

 



    public int RandomBombDropCount {
        get {
            return _randomBombDropCount;
        }

        set {
            _randomBombDropCount = value;
        }
    }









    public float TimeActivePlus {
        get {
            return _timeActivePlus;
        }

        set {
            _timeActivePlus = value;
        }
    }

    public int YellowBombDropCount {
        get {
            return _yellowBombDropCount;
        }

        set {
            _yellowBombDropCount = value;
        }
    }

    public int BlueBombDropCount {
        get {
            return _blueBombDropCount;
        }

        set {
            _blueBombDropCount = value;
        }
    }

    public int RedBombDropCount {
        get {
            return _redBombDropCount;
        }

        set {
            _redBombDropCount = value;
        }
    }

    public int BlackBombDropCount {
        get {
            return _blackBombDropCount;
        }

        set {
            _blackBombDropCount = value;
        }
    }



    public bool IsTimePlusActive {
        get {
            return _isTimePlusActive;
        }

        set {
            _isTimePlusActive = value;
        }
    }

    public bool IsYellowBombDrop {
        get {
            return _isYellowBombDrop;
        }

        set {
            _isYellowBombDrop = value;
        }
    }

    public bool IsBlueBombDrop {
        get {
            return _isBlueBombDrop;
        }

        set {
            _isBlueBombDrop = value;
        }
    }

    public bool IsRedBombDrop {
        get {
            return _isRedBombDrop;
        }

        set {
            _isRedBombDrop = value;
        }
    }

    public bool IsBlackBombDrop {
        get {
            return _isBlackBombDrop;
        }

        set {
            _isBlackBombDrop = value;
        }
    }

    public bool IsNoneSkill {
        get {
            return _isNoneSkill;
        }

        set {
            _isNoneSkill = value;
        }
    }

    public bool IsCoinPlus {
        get {
            return _isCoinPlus;
        }

        set {
            _isCoinPlus = value;
        }
    }

    public bool IsScorePlus {
        get {
            return _isScorePlus;
        }

        set {
            _isScorePlus = value;
        }
    }

    public bool IsGameTimePlus {
        get {
            return _isGameTimePlus;
        }

        set {
            _isGameTimePlus = value;
        }
    }

    public bool IsPowerPlus {
        get {
            return _isPowerPlus;
        }

        set {
            _isPowerPlus = value;
        }
    }

    public bool IsBombAppearBonus {
        get {
            return _isBombAppearBonus;
        }

        set {
            _isBombAppearBonus = value;
        }
    }

    public bool IsSkillInvokeBonus {
        get {
            return _isSkillInvokeBonus;
        }

        set {
            _isSkillInvokeBonus = value;
        }
    }

    public int BombAppearPlus {
        get {
            return _bombAppearPlus;
        }

        set {
            _bombAppearPlus = value;
        }
    }

    public int NekoSkillInvokePlus {
        get {
            return _nekoSkillInvokePlus;
        }

        set {
            _nekoSkillInvokePlus = value;
        }
    }

    public bool IsStartBomb {
        get {
            return _isStartBomb;
        }

        set {
            _isStartBomb = value;
        }
    }

    public int StartBombCount {
        get {
            return _startBombCount;
        }

        set {
            _startBombCount = value;
        }
    }

    public bool IsRemoveSpecialBlock {
        get {
            return _isRemoveSpecialBlock;
        }

        set {
            _isRemoveSpecialBlock = value;
        }
    }

    public bool IsAccelBombCreate {
        get {
            return _isAccelBombCreate;
        }

        set {
            _isAccelBombCreate = value;
        }
    }

    public bool IsChangeBoardBlockColor {
        get {
            return _isChangeBoardBlockColor;
        }

        set {
            _isChangeBoardBlockColor = value;
        }
    }

    public bool IsRemoveSomeLine {
        get {
            return _isRemoveSomeLine;
        }

        set {
            _isRemoveSomeLine = value;
        }
    }

    public int RemoveSpecialBlockCount {
        get {
            return _removeSpecialBlockCount;
        }

        set {
            _removeSpecialBlockCount = value;
        }
    }

    public int AccelBombCreate {
        get {
            return _accelBombCreate;
        }

        set {
            _accelBombCreate = value;
        }
    }

    public int RemoveSomeLine {
        get {
            return _removeSomeLine;
        }

        set {
            _removeSomeLine = value;
        }
    }

    public bool HasActiveSkill {
        get {
            return _hasActiveSkill;
        }

        set {
            _hasActiveSkill = value;
        }
    }



    #endregion


}
