using UnityEngine;
using System.Collections;
using PathologicalGames;
using SimpleJSON;

public class OwnCatCtrl : MonoBehaviour {

    // Controls
    [SerializeField] UISprite _frame; // frame 
    [SerializeField] UISprite _spGrade; // 등급
    [SerializeField] UILabel _lblName;

    [SerializeField] UILabel lblLevel; // 레벨
    [SerializeField] private UISprite _spNeko;
    [SerializeField] UISprite _spMedal;


    [SerializeField] private GameObject _spUsing;
    [SerializeField] private bool isSelected = false;

    

    // 고양이 정보 
    [SerializeField] int _id;
    int _level;
    int _grade;
    int _bead;
    int _power;
    int _dbkey;

    int _maxGrade; // 성장가능한 최고등급 

    [SerializeField] bool _isMaxGrade = false; // 고양이가 최종 등급 상태인지 체크 
    [SerializeField] bool _isMaxLevel = false; // 만렙여부

    JSONNode _info;
    [SerializeField] string _debugInfo = string.Empty;

    // 현재 준비화면 장착 여부
    [SerializeField] bool _isEquipped = false;
   
    

    #region Properties 

    public string CharacterSpriteName {
        get {
            return this._spNeko.spriteName;
        }
    }

    public UIAtlas CharacterAtlas {
        get {
            return this._spNeko.atlas;
        }
    }


    public bool IsMaxGrade {
        get {
            return _isMaxGrade;
        }

        set {
            _isMaxGrade = value;
        }
    }

    public bool IsMaxLevel {
        get {
            return _isMaxLevel;
        }

        set {
            _isMaxLevel = value;
        }
    }

    public int Id {
        get {
            return _id;
        }

        set {
            _id = value;
        }
    }

    public int Level {
        get {
            return _level;
        }

        set {
            _level = value;
        }
    }

    public int Grade {
        get {
            return _grade;
        }

        set {
            _grade = value;
        }
    }

    public int Bead {
        get {
            return _bead;
        }

        set {
            _bead = value;
        }
    }

    public int Power {
        get {
            return _power;
        }

        set {
            _power = value;
        }
    }

    public int Dbkey {
        get {
            return _dbkey;
        }

        set {
            _dbkey = value;
        }
    }

    public int MaxGrade {
        get {
            return _maxGrade;
        }

        set {
            _maxGrade = value;
        }
    }

    public bool IsEquipped {
        get {
            return _isEquipped;
        }

        set {
            _isEquipped = value;
        }
    }

    #endregion

    // Use this for initialization
    void Start () {
	}

    /// <summary>
    /// 캐릭터 정보 설정 
    /// </summary>
    /// <param name="pNode"></param>
    public void SetCharacterInfo(JSONNode pNode) {

        // 고양이 JSON 세팅 
        _info = pNode;
        _debugInfo = _info.ToString();

        // 기본 크기로 조정 
        this.transform.localScale = GameSystem.Instance.BaseScale;


        SetNoSelectFrameSprite();

        _spUsing.SetActive(false);

        Id = _info["tid"].AsInt;
        Level = _info["level"].AsInt;
        Grade = _info["star"].AsInt;
        Bead = _info["bead"].AsInt;
        Dbkey = _info["dbkey"].AsInt;

        // 성장가능한 최고 등급 정보
        MaxGrade = int.Parse(GameSystem.Instance.NekoInfo.Rows[GameSystem.Instance.GetNekoRowID(Id)]._max_grade);
        Power = GameSystem.Instance.GetNekoPurePower(Grade, Level); // 파워 


        lblLevel.text = "Lv." + Level.ToString();
        _spGrade.spriteName = GameSystem.Instance.GetNekoGradeSprite(Grade);

        _lblName.text = GameSystem.Instance.GetNekoName(Id, Grade);
        

        // 최종 등급인지 체크 
        if (Grade == 5 || Grade == MaxGrade) {
            IsMaxGrade = true;

        }
        else {
            IsMaxGrade = false;

        }


        // 만렙 체크
        IsMaxLevel = false;
        switch (Grade) {
            case 1:
                if (Level >= 30)
                    IsMaxLevel = true;

                break;
            case 2:
                if (Level >= 35)
                    IsMaxLevel = true;

                break;
            case 3:
                if (Level >= 40)
                    IsMaxLevel = true;

                break;
            case 4:
                if (Level >= 45)
                    IsMaxLevel = true;

                break;

            case 5:
                if (Level >= 50)
                    IsMaxLevel = true;

                break;
        }


        // Sprite 및 외향 설정
        GameSystem.Instance.SetNekoSprite(_spNeko, Id, Grade);

        // 메달 설정 
        if (GameSystem.Instance.GetNekoMedalType(Id, Grade, Level) == NekoMedal.none)
            _spMedal.gameObject.SetActive(false);
        else if (GameSystem.Instance.GetNekoMedalType(Id, Grade, Level) == NekoMedal.bronze) {
            _spMedal.gameObject.SetActive(true);
            _spMedal.spriteName = PuzzleConstBox.spriteSmallBronzeBadge;
        }
        else if (GameSystem.Instance.GetNekoMedalType(Id, Grade, Level) == NekoMedal.silver) {
            _spMedal.gameObject.SetActive(true);
            _spMedal.spriteName = PuzzleConstBox.spriteSmallSilverBadge;
        }
        else if (GameSystem.Instance.GetNekoMedalType(Id, Grade, Level) == NekoMedal.gold) {
            _spMedal.gameObject.SetActive(true);
            _spMedal.spriteName = PuzzleConstBox.spriteSmallGoldBadge;
        }

        // 장착여부 
        IsEquipped = GameSystem.Instance.ListEquipNekoID.Contains(Id);

        // 준비창에서 열린 경우 장착여부에 따른 장착 사인 활성화
        if(LobbyCtrl.Instance.IsReadyCharacterList && IsEquipped) {
            _spUsing.SetActive(true);
        }
        else {
            _spUsing.SetActive(false);
        }

        // 오브젝트 활성화 
        this.gameObject.SetActive(true);

    } // end of SetCharacterInfo


    /// <summary>
    /// 캐릭터 정보 업데이트 
    /// </summary>
    public void UpdateInfo() {

        for(int i=0; i<GameSystem.Instance.UserNeko.Count; i++) {
            if(GameSystem.Instance.UserNeko[i]["tid"].AsInt == Id) {
                SetCharacterInfo(GameSystem.Instance.UserNeko[i]);
            }
        }
    }


    /// <summary>
    /// 캐릭터 장착
    /// </summary>
    public void EquipCharacter() {

        // 혹시 현재 캐릭터가 다른 슬롯에 장착되어 있다면, 자동해제 
        if(GameSystem.Instance.ListEquipNekoID.Contains(Id)) {

            RemoveEquipSign();

        } // 다른 슬롯 체크 종료 

        // ReadyGroup에게 정보 전달 
        ReadyGroupCtrl.Instance.SetNekoEquipOnCurrentSlot(this);

        // 장착 표시 
        IsEquipped = true;
        _spUsing.SetActive(true);

        // 캐릭터 리스트 종료
        LobbyCtrl.Instance.CloseCharacterList();
    }


    /// <summary>
    /// 캐릭터 해제 
    /// </summary>
    public void ReleaseCharacter() {
        RemoveEquipSign();

        ReadyGroupCtrl.Instance.SetNekoEquipOnCurrentSlot(null);
        SetNoSelectFrameSprite();

        ReadyGroupCtrl.Instance.RefreshEquipBonus();
        ReadyGroupCtrl.Instance.RefreshEquipNeko();
    }

    



    /// <summary>
    /// 비활성화
    /// </summary>
    public void SetDisable() {
        this.gameObject.SetActive(false);
    }



    /// <summary>
    /// 
    /// </summary>
    public void OnClick() {

        if(GameSystem.Instance.SelectNeko != null) 
            GameSystem.Instance.SelectNeko.SendMessage("SetNoSelectFrameSprite");

        SetSelectFrameSprite();
        

        // 선택창으로 연결 
        NekoSelectBigPopCtrl.Instance.SetCurrentNeko(this);

    }


    /// <summary>
    /// 장착 사인 제거 
    /// </summary>
    void RemoveEquipSign() {
        _isEquipped = false;
        _spUsing.SetActive(false);

        // 리스트내의 동일한 ID는 -1로 처리
        for (int i = 0; i < GameSystem.Instance.ListEquipNekoID.Count; i++) {
            if (GameSystem.Instance.ListEquipNekoID[i] == Id) {
                GameSystem.Instance.ListEquipNekoID[i] = -1;
            }
        }
    }

    private void SetSelectFrameSprite() {
        // _frame.spriteName = PuzzleConstBox.spriteFrameNekoGreen;
        _frame.spriteName = "new-card-in";

        _lblName.color = PuzzleConstBox.colorSelectedNekoName;
        _lblName.effectColor = PuzzleConstBox.colorSelectedNekoNameShadow;
        lblLevel.color = PuzzleConstBox.colorSelectedNekoLevel;
    }

    private void SetNoSelectFrameSprite() {
        //_frame.spriteName = PuzzleConstBox.spriteFrameNekoYellow;
        _frame.spriteName = "new-card-standard";

        _lblName.color = PuzzleConstBox.colorUnSelectedNekoName;
        _lblName.effectColor = PuzzleConstBox.colorUnSelectedNekoNameShadow;
        lblLevel.color = PuzzleConstBox.colorUnSelectedNekoLevel;

    }
}
