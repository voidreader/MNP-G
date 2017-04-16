using UnityEngine;
using System.Collections;
using PathologicalGames;


/// <summary>
/// 케릭터 선택창에서 나열되는 각 고양이 객체들의 스크립트 
/// </summary>
public class PlayerOwnNekoCtrl : MonoBehaviour {

	
	[SerializeField] UISprite _frame;
	[SerializeField] private int _id;
	[SerializeField] private int _usedIndex;

	[SerializeField] private UISprite spNeko;
    [SerializeField] UISprite _spMedal;
	

	[SerializeField] private UISprite spUsing;
	[SerializeField] private bool isSelected = false;
	[SerializeField] bool isOwned = false;
	public bool isForUpgrade = false; // 업그레이드 창에서 호출 된 경우 true.

	public int _level;
	public int _grade;
	public int _bead;
	public int _dbkey;
	public int _power;
    private int _maxGrade; // 성장가능한 최고 등급 

    [SerializeField] bool _isMaxGrade = false; // 고양이가 최종 등급 상태인지 체크 
    [SerializeField] bool _isMaxLevel = false; // 만렙여부

	private string _gradeText;

    readonly string _spriteNoSelect = "lvup_frm_neko_yellow";
    readonly string _spriteSelect = "lvup_frm_neko_green";

    [SerializeField] UILabel lblGrade;
	[SerializeField] UILabel lblLevel;

	

	public void SetUpgradeFlag(bool pFlag) {
		isForUpgrade = pFlag;
	}


	/// <summary>
	/// 준비창에서 세팅되는 플레이어 네코 정보 
	/// </summary>
	/// <param name="pID">P I.</param>
	/// <param name="pSprite">P sprite.</param>
	/// <param name="pShadowSprite">P shadow sprite.</param>
	public void SetPlayerOwnNeko(int pID) {

		//isForUpgrade = false;

		isOwned = false;
		spUsing.gameObject.SetActive (false);

		// 크기조정 
		this.transform.localScale = GameSystem.Instance.BaseScale;

		_id = pID;


		if (!isForUpgrade) {

			// 기존에 선택 여부 체크
			if (GameSystem.Instance.ListEquipNekoID.Contains (_id) && GameSystem.Instance.CheckUserNekoData (_id)) {
				isSelected = true;

				for (int i=0; i<GameSystem.Instance.ListEquipNekoID.Count; i++) {
					if (_id == GameSystem.Instance.ListEquipNekoID [i])
						_usedIndex = i;
				}
				spUsing.gameObject.SetActive (true);
			} else {
				isSelected = false;
			}
		}



		RefreshState ();
	}

	/// <summary>
	/// Upgrade 창에서 세팅되는 정보 
	/// </summary>
	/// <param name="pID">P I.</param>
	/// <param name="pSprite">P sprite.</param>
	/// <param name="pShadowSprite">P shadow sprite.</param>
	public void SetUpgradePlayerOwnNeko(int pID) {
		isForUpgrade = true;

		// 크기조정 
		this.transform.localScale = GameSystem.Instance.BaseScale;
		
		_id = pID;

        // 상태 조회 및 세팅 
		RefreshState ();
	}
	
	
	/// <summary>
	/// 고양이 정보 불러오기. 
	/// </summary>
	public void RefreshState() {

		

        SetNoSelectFrameSprite();

        if (GameSystem.Instance.UserNeko != null) {


            isOwned = false;
			
			lblGrade.gameObject.SetActive(false); // 등급
			spNeko.gameObject.SetActive(false);
			

			 // 소유권을 가지고 있는 확인
            for (int i = 0; i < GameSystem.Instance.UserNeko.Count; i++) {

                if (GameSystem.Instance.UserNeko[i]["tid"].AsInt == _id) {

                    isOwned = true;
					lblGrade.gameObject.SetActive(true); // 등급
					spNeko.gameObject.SetActive(true);


                    _level = GameSystem.Instance.UserNeko[i]["level"].AsInt;
                    _grade = GameSystem.Instance.UserNeko[i]["star"].AsInt;
                    _bead = GameSystem.Instance.UserNeko[i]["bead"].AsInt;
                    _dbkey = GameSystem.Instance.UserNeko[i]["dbkey"].AsInt;

                    _maxGrade = int.Parse(GameSystem.Instance.NekoInfo.Rows[GameSystem.Instance.GetNekoRowID(_id)]._max_grade);

                    _power = 100; // 기본 파워  , 등급에 따른 재계산 

                    if (_grade == 2)
                        _power = 150;
                    else if (_grade == 3)
                        _power = 250;
                    else if (_grade == 4)
                        _power = 400;
                    else if (_grade == 5)
                        _power = 600;

                    _power += (_level - 1) * 30;

					lblLevel.text = "Lv." +  _level.ToString();
					lblGrade.text = GetGradeText(_grade);

                    // 최종 등급인지 체크 
                    if(_grade == 5 || _grade == _maxGrade) {
                        IsMaxGrade = true;
                        lblGrade.bitmapFont = GameSystem.Instance.OrangeStarFont;
                        lblGrade.spacingX = -32;
                        lblGrade.fontSize = 11;
                    } else {
                        IsMaxGrade = false;
                        lblGrade.bitmapFont = GameSystem.Instance.NormalStarFont;
                        lblGrade.spacingX = -25;
                        lblGrade.fontSize = 17;
                    }


                    IsMaxLevel = false;
                    // 만렙인지 체크
                    switch(_grade) {
                        case 1:
                            if (_level >= 30)
                                IsMaxLevel = true;
                                
                            break;
                        case 2:
                            if (_level >= 35)
                                IsMaxLevel = true;

                            break;
                        case 3:
                            if (_level >= 40)
                                IsMaxLevel = true;

                            break;
                        case 4:
                            if (_level >= 45)
                                IsMaxLevel = true;

                            break;

                        case 5:
                            if (_level >= 50)
                                IsMaxLevel = true;

                            break;

                    }


                    GameSystem.Instance.SetNekoSprite(spNeko, NekoID, _grade);

                    // 메달 설정 
                    if (GameSystem.Instance.GetNekoMedalType(NekoID, _grade, _level) == NekoMedal.none)
                        _spMedal.gameObject.SetActive(false);
                    else if (GameSystem.Instance.GetNekoMedalType(NekoID, _grade, _level) == NekoMedal.bronze) {
                        _spMedal.gameObject.SetActive(true);
                        _spMedal.spriteName = PuzzleConstBox.spriteSmallBronzeBadge;
                    }
                    else if (GameSystem.Instance.GetNekoMedalType(NekoID, _grade, _level) == NekoMedal.silver) {
                        _spMedal.gameObject.SetActive(true);
                        _spMedal.spriteName = PuzzleConstBox.spriteSmallSilverBadge;
                    }
                    else if (GameSystem.Instance.GetNekoMedalType(NekoID, _grade, _level) == NekoMedal.gold) {
                        _spMedal.gameObject.SetActive(true);
                        _spMedal.spriteName = PuzzleConstBox.spriteSmallGoldBadge;
                    }



                    return;
				}
			}

		}
	}

	private string GetGradeText(int _pStar) {

		_gradeText = "";

		for (int i=0; i<_pStar; i++) {
			_gradeText += "*";
		}

		return _gradeText;
	}

	public void OnClickOwnNeko() {

		// 소유하지 않은 컬럼은 선택할 수 없다. 
		if (!isOwned)
			return;


        SetSelectFrameSprite();
        SetNoSelectCurrentNeko();

        // 선택창으로 연결 
        NekoSelectBigPopCtrl.Instance.SetCurrentNeko(this);

    }

    private void SetSelectFrameSprite() {
        _frame.spriteName = _spriteSelect;
  
    }

    private void SetNoSelectFrameSprite() {
        _frame.spriteName = _spriteNoSelect;
    }

    private void SetNoSelectCurrentNeko() {
        if(GameSystem.Instance.SelectNeko != null && GameSystem.Instance.SelectNeko != this) {
            GameSystem.Instance.SelectNeko.SendMessage("SetNoSelectFrameSprite");
        }
    }



	/// <summary>
	/// 터치한 고양이를 선택 or 해제 한다. 
	/// </summary>
	/// <param name="isSelect">If set to <c>true</c> is select.</param>
	public void SelectCurrentNeko(bool isSelect) {


		// 선택
		if (isSelect) {

			// 이전에 선택했던 Player Neko 해제 
			for (int i=0; i <LobbyCtrl.Instance.ListPlayerNekoSelection.Count; i++) {

                if (LobbyCtrl.Instance.ListPlayerNekoSelection [i].NekoID == ReadyGroupCtrl.Instance.GetEquipedNekoID()) {
    				LobbyCtrl.Instance.ListPlayerNekoSelection [i].RemoveSelectedSign ();

                    for (int j = 0; j < GameSystem.Instance.ListEquipNekoID.Count; j++) {
                        if (GameSystem.Instance.ListEquipNekoID[j] == _id)
                            GameSystem.Instance.ListEquipNekoID[j] = -1; // 리스트내에 동일한 ID는 -1처리
                    }

                    break;
				}


			}

            ReadyGroupCtrl.Instance.SetNekoEquip(ReadyGroupCtrl.Instance.SelectedEquipNekoIndex, this);
			
			isSelected = true;
			_usedIndex = ReadyGroupCtrl.Instance.SelectedEquipNekoIndex;
			
			
			spUsing.gameObject.SetActive (true);
			
			LobbyCtrl.Instance.ClosePlayOwnNekoPanel ();

		} else {

			isSelected = false;

            for (int j = 0; j < GameSystem.Instance.ListEquipNekoID.Count; j++) {
                if (GameSystem.Instance.ListEquipNekoID[j] == _id)
                    GameSystem.Instance.ListEquipNekoID[j] = -1; // 리스트내에 동일한 ID는 -1처리
            }

            ReadyGroupCtrl.Instance.SetNekoEquip(ReadyGroupCtrl.Instance.SelectedEquipNekoIndex, null);
            spUsing.gameObject.SetActive(false);
            SetNoSelectFrameSprite();

        }

        if(ReadyGroupCtrl.Instance != null) {
            ReadyGroupCtrl.Instance.RefreshEquipBonus();
            ReadyGroupCtrl.Instance.RefreshEquipNeko();
        }
	}






    /// <summary>
    /// 선택 표시를 해제한다.
    /// </summary>
	public void RemoveSelectedSign() {
		isSelected = false;
		spUsing.gameObject.SetActive (false);
	}

	public string NekoSpriteName {
		get {
			return this.spNeko.spriteName;
		}
	}

    public UIAtlas NekoAtlas {
        get {
            return this.spNeko.atlas;
        }
    }

    

	public int NekoID {
		get {
			return this._id;
		}
	}

	public bool IsSelected {
		get {
			return this.isSelected;
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

    public int MaxGrade {
        get {
            return _maxGrade;
        }

        set {
            _maxGrade = value;
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
}
