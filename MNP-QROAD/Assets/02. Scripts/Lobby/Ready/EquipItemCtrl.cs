using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;
using DG.Tweening;

public class EquipItemCtrl : MonoBehaviour {

	[SerializeField] private UISprite spItem;


    [SerializeField] UILabel _lblPrice; // 원래 가격
    [SerializeField] UILabel _lblBargainPrice; // 할인가격
    [SerializeField] int _price;

    [SerializeField] GameObject _bargainInfo; // 할인된 경우 표시 


	[SerializeField] private bool isClicked = false;

	[SerializeField] private GameObject spFillColumn;
    
	[SerializeField] private int equipItemIndex;


    [SerializeField]
    UISprite _itemBox; // 아이템 박스 Sprite
    [SerializeField]
    GameObject _saleArrow;

    [SerializeField] GameObject _saleInfo;
    [SerializeField] GameObject _defaultPriceInfo;

    [SerializeField]
    bool _isLock;

    [SerializeField] UISprite _lockSprite;
    [SerializeField] SparkLightCtrl _sparkLight;


    /// <summary>
    /// 아이템 장착 
    /// </summary>
    public void ClickItem() {

        // Lock 상태 체크
        if(_lockSprite.gameObject.activeSelf) {
            LobbyCtrl.Instance.OpenInfoPopUp(PopMessageType.BoostItemLock);
            return;
        }

        if(spItem.spriteName == PuzzleConstBox.listEquipItemClip[3]) {
            return;
        }


		if (!isClicked) { // 체크 처리 

			if (GameSystem.Instance.UserGold - GameSystem.Instance.CheckedBoostItemsPrice < Price) {
                //AndroidMessage.Create("Product Purchase Failed", result.response.ToString() + " " + result.message);
                //AndroidMessage.Create("아이템 장착 실패", "골드가 충분하지 않습니다.", "확인");
                LobbyCtrl.Instance.OpenInfoPopUp(PopMessageType.ShortageGoldForItem);
                return;
            }

            GameSystem.Instance.CheckedBoostItemsPrice += Price;

            LobbyCtrl.Instance.UpdateMoney();

            // 체크 표시 
            spFillColumn.gameObject.SetActive(true);

            // ICON 칼라로 변경
            spItem.spriteName = PuzzleConstBox.listColoredEquipItemClip[EquipItemIndex];


            // 아이템 정보를 Add
            GameSystem.Instance.ListEquipItemID.Add(equipItemIndex);

            // 문구 변경.
            ReadyGroupCtrl.Instance.SetEquipItemText(equipItemIndex);

            isClicked = true;




        } else { // 체크 해제 
			spFillColumn.gameObject.SetActive (false);

            GameSystem.Instance.CheckedBoostItemsPrice -= Price;
            LobbyCtrl.Instance.UpdateMoney();

            // 흑백 칼라로 변경
            spItem.spriteName = PuzzleConstBox.listEquipItemClip[EquipItemIndex];


            // 아이템 정보를 Remove
            GameSystem.Instance.ListEquipItemID.Remove(equipItemIndex);

            // 문구 변경.
            ReadyGroupCtrl.Instance.SetEquipItemText(-1);

            isClicked = false;
        }

		

	}
    
    public void ReleaseItem() {
        spFillColumn.gameObject.SetActive(false);

        // 아이템 정보를 Remove
        GameSystem.Instance.ListEquipItemID.Remove(equipItemIndex);

        // 문구 변경.
        ReadyGroupCtrl.Instance.SetEquipItemText(-1);

        isClicked = false;
    }



	/// <summary>
	/// 초기화 
	/// </summary>
	/// <param name="pIndex">P index.</param>
	public void SetEquipItem(int pIndex) {
		equipItemIndex = pIndex;
		spItem.spriteName = PuzzleConstBox.listEquipItemClip [pIndex];

        // 가격설정 
        switch(pIndex) {
            case 0:
                _lblPrice.text = GameSystem.Instance.GetNumberToString(PuzzleConstBox.originalBoostItemPrice); // 원 가격
                _lblBargainPrice.text = GameSystem.Instance.GetNumberToString(GameSystem.Instance.BoostShieldPrice); // 변경 가격 
                Price = GameSystem.Instance.BoostShieldPrice;
                break;
            case 1:
                _lblPrice.text = GameSystem.Instance.GetNumberToString(PuzzleConstBox.originalBoostItemPrice);
                _lblBargainPrice.text = GameSystem.Instance.GetNumberToString(GameSystem.Instance.BoostBombPrice);
                Price = GameSystem.Instance.BoostBombPrice;
                break;
            case 2:
                _lblPrice.text = GameSystem.Instance.GetNumberToString(PuzzleConstBox.originalBoostItemPrice);
                _lblBargainPrice.text = GameSystem.Instance.GetNumberToString(GameSystem.Instance.BoostCriticalPrice);
                Price = GameSystem.Instance.BoostCriticalPrice;
                break;
            case 3:
                //_lblPrice.text = "Free";
                //_price = 0;

                _lblPrice.text = GameSystem.Instance.GetNumberToString(PuzzleConstBox.originalBoostStartFever);
                _lblBargainPrice.text = GameSystem.Instance.GetNumberToString(GameSystem.Instance.BoostStartFeverPrice);
                Price = GameSystem.Instance.BoostStartFeverPrice;
                break;
        }


        // 잠금 처리 
        SetLockState(!GameSystem.Instance.CheckStateItemUnlock());

        if (_isLock)
            return;

        setBargainInfo();

    }

    /// <summary>
    /// 할인정보 세팅 
    /// </summary>
    private void setBargainInfo() {

        if (equipItemIndex < 3 && Price < PuzzleConstBox.originalBoostItemPrice) {
            _bargainInfo.SetActive(true);
            return;
        }

        if (equipItemIndex == 3 && Price < PuzzleConstBox.originalBoostStartFever) {
            _bargainInfo.SetActive(true);
            return;
        }


        
    }

	/// <summary>
	/// Sets the fill column.
	/// </summary>
	/// <param name="pFlag">If set to <c>true</c> p flag.</param>
	public void SetFillColumn(bool pFlag) {

        spFillColumn.gameObject.SetActive (pFlag);
        if(pFlag) {
            isClicked = true;
            
        } else {
            isClicked = false;
            
        }
	}

	public bool GetFillColumnActive() {
		return spFillColumn.activeSelf;
	}


    /// <summary>
    /// 
    /// </summary>
    /// <param name="pLock"></param>
    private void SetLockState(bool pLock) {

        _isLock = pLock;

        if(pLock) {
            _lockSprite.gameObject.SetActive(true);
            _bargainInfo.SetActive(false);
            _defaultPriceInfo.SetActive(false);
            spItem.gameObject.SetActive(false);
        }
        else {
            _lockSprite.gameObject.SetActive(false);
            _defaultPriceInfo.SetActive(true);
            spItem.gameObject.SetActive(true);
        }

        if (equipItemIndex == 3)
            _defaultPriceInfo.SetActive(false);
    }


    /// <summary>
    ///  Unlock 처리 
    /// </summary>
    public void UnlockItem() {
        StartCoroutine(UnlockingItem());
    }

    IEnumerator UnlockingItem() {
        _sparkLight.PlayCurrentPos();
        yield return new WaitForSeconds(0.3f);

        _lockSprite.gameObject.SetActive(false);
        _itemBox.transform.DOScale(1.1f, 0.3f).SetLoops(2, LoopType.Yoyo);

        _defaultPriceInfo.SetActive(true);
        _isLock = false;
        setBargainInfo();

        spItem.gameObject.SetActive(true);



        // 실제로 버튼을 활성화 하는것은 Lobby의 unlocking 에서 처리한다.
        //this.GetComponent<UIButton>().enabled = true;

        if (equipItemIndex == 3)
            _defaultPriceInfo.SetActive(false);

    }


	


	public int EquipItemIndex {
		get {
			return this.equipItemIndex;
		}
	}

    public bool IsClicked {
        get {
            return isClicked;
        }

        set {
            isClicked = value;
        }
    }

    public bool IsLock {
        get {
            return _isLock;
        }

        set {
            _isLock = value;
        }
    }

    public int Price {
        get {
            return _price;
        }

        set {
            _price = value;
        }
    }
}
