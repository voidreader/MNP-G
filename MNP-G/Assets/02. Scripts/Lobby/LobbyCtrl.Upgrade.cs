using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PathologicalGames;

public partial class LobbyCtrl : MonoBehaviour {

	// Upgrade 창 제어 
	public bool IsSelectiveNekoPanel = false; // 성장 / 장착 창 제어 용도 

	// Selective 창 제어 
	[SerializeField] UIPanel pnlSelectiveNekoScrollView; // 네코 선택창 ScrollView Panel
	[SerializeField] UIPanel pnlRankScrollView; 
	

    [SerializeField] GameObject _newNekoAddedSign;




	// 업그레이드 고양이 
	public List<PlayerOwnNekoCtrl> _listUpgradeNeko = new List<PlayerOwnNekoCtrl>(); 

    /// <summary>
    /// 로비 버튼을 통해 열림 
    /// </summary>
	public void OpenUpgradGroup() {
		IsSelectiveNekoPanel = false;

		//_UpgradeGroup.gameObject.SetActive (true);
		objSelectNeko.SetActive (true);

        AdbrixManager.Instance.SendAdbrixInAppActivity(AdbrixManager.Instance.BUTTON_NEKOGROWTH);

        DisableNewNekoAddedSign();
    }

    void DisableNewNekoAddedSign() {
        _newNekoAddedSign.SetActive(false);
    }

    public void EnableNewNekoAddedSign() {
        _newNekoAddedSign.SetActive(true);
    }


	/// <summary>
	/// 준비창에서 열림 
	/// </summary>
	public void ShowPlayerOwnNekoPanel() {
		IsSelectiveNekoPanel = true;

		objSelectNeko.SetActive (true);

	
	}
	
	public void ClosePlayOwnNekoPanel() {

		if(objSelectNeko.activeSelf) 
			objSelectNeko.SendMessage ("CloseSelf");
		
	}



	#region 업그레이드 창 제어 
	
	/// <summary>
	/// 업그레이드 팝업창 호출 
	/// </summary>
	/// <param name="pAbilityIndex">P ability index.</param>
	public void OpenUpgradePopup(int pAbilityIndex, int pPrice) {

		// 골드가 부족한 경우 구매 팝업. 
		if (GameSystem.Instance.UserGold < pPrice) {
			//objPopupNeedCoin.gameObject.SetActive(true);

			OpenInfoPopUp(PopMessageType.GoldShortage);
			return;
		}
		
		
		//objPopupUpgrade.GetComponent<PopUpgradeCtrl> ().SetUpgradeInfo (pAbilityIndex);
		//objPopupUpgrade.SetActive (true);
	}


	
	#endregion


}
