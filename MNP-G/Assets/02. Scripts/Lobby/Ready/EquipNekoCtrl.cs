using UnityEngine;
using System.Collections;

public class EquipNekoCtrl : MonoBehaviour {

	[SerializeField] private int index;
	[SerializeField] private bool isSelected = false;

	[SerializeField] private UISprite spNeko;
	[SerializeField] private int nekoID;
	[SerializeField] private GameObject setEquipNeko;

    [SerializeField] GameObject _emptyInfo;

	// 객체 
	[SerializeField] UILabel lblGrade; // 등급 텍스트
	[SerializeField] UILabel lblLevel; // 레벨 텍스트


	[SerializeField] int _level;
	[SerializeField] int _grade;
	[SerializeField] string _star;
    string _spriteName;


    // Use this for initialization
    void Start () {
	
	}

	public void SetEquipNeko(int pIndex) {
		index = pIndex;

        //_emptyInfo.gameObject.SetActive(true);

        // ID에 따른 장착 네코 정보가 있는지 확인해서 세팅 .
    }




	/// <summary>
	/// 장착하는 플레이어 네코를 터치시.
	/// </summary>
	public void ClickEquipNeko() {

		// Clik.
		ReadyGroupCtrl.Instance.SelectedEquipNekoIndex = index;

		// 팝업창 띄우기 (LobbyCtrl.Upgrade)
		LobbyCtrl.Instance.ShowPlayerOwnNekoPanel ();

	}

	public void SetNekoInfo(int pNekoID) {

		// 0보다 작은 경우는 배치 되지 않음. 
		if (pNekoID < 0) {
			nekoID = -1;

            setEquipNeko.gameObject.SetActive(false);
            _emptyInfo.SetActive(true);

			return;
		}

		// 소유하지 않은 Neko 체크 
		//GameSystem.Instance.UserNekoData["data"]
		if (!GameSystem.Instance.CheckUserNekoData (pNekoID)) {

            _emptyInfo.SetActive(false);

            Debug.Log("▶▶ It's not user neko ");
			nekoID = -1;

			// GameSystem에 저장 
			GameSystem.Instance.SaveEquipNekoInfo (index, -1);

			// 해제 처리 
			setEquipNeko.gameObject.SetActive(false);
            _emptyInfo.SetActive(true);
            return;
		}

		setEquipNeko.gameObject.SetActive (true);
		nekoID = pNekoID;


        SearchNekoInfo(nekoID);

        spNeko.gameObject.SetActive(true);
        _emptyInfo.SetActive(false);
        GameSystem.Instance.SetNekoSprite(spNeko, nekoID, _grade);


	}

	/// <summary>
	/// 네코 아이디정보로 검색해서 필요한 정보를 할당 
	/// </summary>
	/// <param name="pNekoID">P neko I.</param>
	public void SearchNekoInfo(int pNekoID) {

        // 현재 선택한 Neko의 정보를 검색 
        for (int i = 0; i < GameSystem.Instance.UserNeko.Count; i++) {
            if (GameSystem.Instance.UserNeko[i]["tid"].AsInt == pNekoID) {

                _level = GameSystem.Instance.UserNeko[i]["level"].AsInt;
                _grade = GameSystem.Instance.UserNeko[i]["star"].AsInt;

                break;
            }
        }

        SetNekoDetailInfo();
    }

	public void SetNekoInfo(PlayerOwnNekoCtrl pSelectedNeko) {

		if (pSelectedNeko == null) {
			setEquipNeko.gameObject.SetActive(false);
			nekoID = -1;
			GameSystem.Instance.SaveEquipNekoInfo (index, nekoID);

            _emptyInfo.SetActive(true);
            return;
		}

		setEquipNeko.gameObject.SetActive (true);
        spNeko.atlas = pSelectedNeko.NekoAtlas;
        spNeko.spriteName = pSelectedNeko.NekoSpriteName;


		nekoID = pSelectedNeko.NekoID;


		_level = pSelectedNeko._level;
		_grade = pSelectedNeko._grade;

        _emptyInfo.SetActive(false);

        SetNekoDetailInfo ();

		// GameSystem에 저장 
		GameSystem.Instance.SaveEquipNekoInfo (index, nekoID);
	}


	private void SetNekoDetailInfo() {

		// Grade
		_star = "";
		for (int i=0; i<_grade; i++) {
			_star += "*";
		}
		lblGrade.text = _star;

		lblLevel.text = "Lv. "+ _level.ToString ();



        if(ReadyGroupCtrl.Instance != null) {
            //ReadyGroupCtrl.Instance.RefreshEquipBonus();
        }
     }

	public int NekoID {
		get {
			return this.nekoID;
		}
	}
	

}
