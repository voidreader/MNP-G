using UnityEngine;
using System.Collections;
using DG.Tweening;

public class InGameEquipItemCtrl : MonoBehaviour {

	// 객체 
	[SerializeField] GameObject _checker;
	[SerializeField] UISprite _spriteItem;


	//[SerializeField] tk2dSpriteAnimator spUseFx;
	//[SerializeField] Transform trFx;

	[SerializeField] int itemIndex;
	[SerializeField] bool isEquipped = false;


	// Use this for initialization
	void Start () {

		isEquipped = false;
	
		//spUseFx.AnimationCompleted += AnimationCompleteDelegate;
	}

	public void SetInGameEquipItem() {

        _checker.gameObject.SetActive(false);

		for(int i=0; i<GameSystem.Instance.ListEquipItemID.Count;i++) {
			if(itemIndex == GameSystem.Instance.ListEquipItemID[i]) {
				isEquipped = true; // 장착되어있음 .

                // 활성화 처리 
                _spriteItem.spriteName = PuzzleConstBox.listEquipBoostItemColorSprite[itemIndex];
                _checker.gameObject.SetActive(true);

                break;
			}
		}
	}

	/// <summary>
	/// 아이템 액티브 처리 
	/// </summary>
	public void SetActiveEquipItem() {

        
        _spriteItem.spriteName = PuzzleConstBox.listEquipBoostItemColorSprite[itemIndex];

        InSoundManager.Instance.PlayEquipitemPing();
	}





    /// <summary>
    /// 부스트 아이템 비활성화 
    /// </summary>
	public void InactiveEquipItem() {

        if (itemIndex == 0) {
            this.transform.DOKill();
            this.transform.localPosition = GameSystem.Instance.BaseScale;

            _spriteItem.spriteName = PuzzleConstBox.listEquipBoostItemDarkSprite[itemIndex];
        }
    }

	/// <summary>
	/// 초기화 
	/// </summary>
	/// <param name="pSpriteIndex">P sprite index.</param>
	public void Init(int pSpriteIndex) {
		itemIndex = pSpriteIndex;
        _spriteItem.spriteName = PuzzleConstBox.listEquipBoostItemDarkSprite[pSpriteIndex];
        
        this.transform.localScale = GameSystem.Instance.BaseScale;

        _checker.gameObject.SetActive(false);
    }


	
	
}
