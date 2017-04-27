using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PathologicalGames;
using SimpleJSON;
using DG.Tweening;

public partial class LobbyCtrl : MonoBehaviour {


    [SerializeField] UIPanel pnlMissionScrollView; // 미션 ScrollView 

    [SerializeField] GameObject _heartShop; // 하트샵 
	[SerializeField] GameObject _jewelShop; // 젬샵 
	[SerializeField] GameObject _goldShop; // 코인샵 
	[SerializeField] GameObject _goldPurchaseConfirm; // 골드 구매 확인창 

    [SerializeField] SKUMasterCtrl _skuMaster; // 인앱 상품 관리 

    // 데이터 이전 정보 
    [SerializeField] CodeInfoCtrl _codeInfo;
    [SerializeField] CodeInputCtrl _codeInput;

    [SerializeField] GameObject _objNewMission; // 미션 버튼의 New 표시 

    MissionController _missionController = null;

    // 정렬된 미션 (달성 → 미달성 → 보상 받음)
    [SerializeField] List<JSONNode> _listSortedMission = new List<JSONNode>();
    MissionColumnCtrl _finalDailyMission;
    MissionColumnCtrl _finalWeeklyMission;

    // 미션 완료 연출 용도 
    [SerializeField] UISprite _throwingIcon;
    [SerializeField] Transform _topCoinIcon;
    [SerializeField] Transform _topGemIcon;
    [SerializeField] WhiteLightCtrl _colorfulLightInTop;

    [SerializeField]
    MissionClearDirctCtrl _missionClearDirect;

    #region 미션


    /// <summary>
    /// 완료 미션 체크 
    /// </summary>
    public void UpdateMissionNew() {


        // Unlock 상태
        if (!GameSystem.Instance.CheckStateMissionUnlock()) {
            _objNewMission.SetActive(false);
            return;
        }



        if (GameSystem.Instance.CheckCompletedMission()) {
            _objNewMission.SetActive(true);
        }
        else {
            _objNewMission.SetActive(false);
        } 


    }

    /// <summary>
    /// 미션 화면 오픈 
    /// </summary>
    public void OpenMission() {
        if(_btnMission.normalSprite == PuzzleConstBox.spriteLockIcon) {
            OpenInfoPopUp(PopMessageType.Lock);
            return;
        }


        AdbrixManager.Instance.SendAdbrixInAppActivity(AdbrixManager.Instance.BUTTON_MISSION);

        Debug.Log(">>> OpenMission");
        GameSystem.Instance.Post2SyncTime(this.RequestUserMission);
        /*
        bigPopup.gameObject.SetActive(true);
        bigPopup.SetMission();
        */
    }

    /// <summary>
    ///  사용자 미션 정보 조회 
    /// </summary>
    private void RequestUserMission() {
        GameSystem.Instance.Post2UserMission(this.OpenMissionWindow);
    }

    private void OpenMissionWindow() {
        bigPopup.gameObject.SetActive(true);
        bigPopup.SetMission();
    }

    public void SpawnMissions() {
        if(_missionController == null) {
            _missionController = FindObjectOfType(typeof(MissionController)) as MissionController;
        }

        //_missionController.SetTabButton();

        if (_missionController.IsDaily) {
            SpawnDailyMissions();
        }
        else {
            SpawnWeeklyMissions();
        }

    }

    /// <summary>
    /// 미션 정렬 처리 
    /// </summary>
    /// <param name="pNode"></param>
    private void SortMission(JSONNode pNode) {
        _listSortedMission.Clear();

        for (int i = 0; i < pNode.Count; i++) {
            _listSortedMission.Add(pNode[i]);
        }


        // 순서대로 넣는다. 달성상태(progress:1), 미달성(progress:0), 완료(progress:2)
        /*
        for (int i = 0; i < pNode.Count; i++) {

            if (pNode[i]["progress"].AsInt == 1)
                _listSortedMission.Add(pNode[i]);
        }

        for (int i = 0; i < pNode.Count; i++) {

            if (pNode[i]["progress"].AsInt == 0)
                _listSortedMission.Add(pNode[i]);
        }

        for (int i = 0; i < pNode.Count; i++) {

            if (pNode[i]["progress"].AsInt == 2)
                _listSortedMission.Add(pNode[i]);
        }
        */
    }

    public void SpawnDailyMissions() {

        Transform spawned;

        PoolManager.Pools["MissionPool"].DespawnAll();
        GameSystem.Instance.CheckMissionDay();

        //Debug.Log("SpawnDailyMission :: " + GameSystem.Instance.MissionDayJSON.ToString());

        // 미션 정렬 처리 
        SortMission(GameSystem.Instance.MissionDayJSON);

        // Scroll View 위치 초기화 
        pnlMissionScrollView.gameObject.GetComponent<UIScrollView>().ResetPosition();
        pnlMissionScrollView.clipOffset = Vector2.zero;
        pnlMissionScrollView.transform.localPosition = new Vector3(0, 260, 0);

        for(int i=0; i<_listSortedMission.Count;i++) {
            spawned = PoolManager.Pools["MissionPool"].Spawn("MissionColumn", Vector3.zero, Quaternion.identity);
            spawned.GetComponent<MissionColumnCtrl>().SetMissionInfo(_listSortedMission[i], MissionType.Day);

            // 위치 정해주기
            spawned.localPosition = new Vector3(0, i * pnlMissionScrollView.GetComponent<UIGrid>().cellHeight * -1, 0);
        }

        /*
        for (int i = 0; i < GameSystem.Instance.MissionDayJSON.Count; i++) {
            spawned = PoolManager.Pools["MissionPool"].Spawn("MissionColumn", Vector3.zero, Quaternion.identity);
            spawned.GetComponent<MissionColumnCtrl>().SetMissionInfo(GameSystem.Instance.MissionDayJSON[i], MissionType.Day);

            spawned.localPosition = new Vector3(0, i * pnlMissionScrollView.GetComponent<UIGrid>().cellHeight * -1, 0);
        }
        */
    }


    /// <summary>
    /// 주간 미션 리스트 생성
    /// </summary>
    public void SpawnWeeklyMissions() {

        Transform spawned;

        PoolManager.Pools["MissionPool"].DespawnAll();
        GameSystem.Instance.CheckMissionDay();


        Debug.Log("SpawnDailyMission :: " + GameSystem.Instance.MissionWeekJSON.ToString());


        // 미션 정렬 처리 
        SortMission(GameSystem.Instance.MissionWeekJSON);

        pnlMissionScrollView.gameObject.GetComponent<UIScrollView>().ResetPosition();
        pnlMissionScrollView.clipOffset = Vector2.zero;
        pnlMissionScrollView.transform.localPosition = new Vector3(0, 260, 0);


        for (int i = 0; i < _listSortedMission.Count; i++) {
            spawned = PoolManager.Pools["MissionPool"].Spawn("MissionColumn", Vector3.zero, Quaternion.identity);
            spawned.GetComponent<MissionColumnCtrl>().SetMissionInfo(_listSortedMission[i], MissionType.Week);

            spawned.localPosition = new Vector3(0, i * pnlMissionScrollView.GetComponent<UIGrid>().cellHeight * -1, 0);
        }

        /*
        for (int i = 0; i < GameSystem.Instance.MissionWeekJSON.Count; i++) {
            spawned = PoolManager.Pools["MissionPool"].Spawn("MissionColumn", Vector3.zero, Quaternion.identity);
            spawned.GetComponent<MissionColumnCtrl>().SetMissionInfo(GameSystem.Instance.MissionWeekJSON[i], MissionType.Week);

            spawned.localPosition = new Vector3(0, i * pnlMissionScrollView.GetComponent<UIGrid>().cellHeight * -1, 0);
        }
        */
    }


    #endregion



    #region Shop Control

    public void OpenJewelShop() {

        // Test
        //AdbrixManager.Instance.SendAdbrixInAppPurchasing("diamond_100");


#if UNITY_WEBGL
        OpenInfoPopUp(PopMessageType.NoFunc);
        return;
#endif

        _jewelShop.SetActive (true);
	}

	public void OpenGoldShop() {
		_goldShop.SetActive (true);
	}

	public void OpenHeartShop() {

    	// 개수 세팅 필요 
		_heartShop.GetComponent<HeartShopCtrl> ().SetHeartShop ();
	}

	public void RefreshHeartShop() {
		_heartShop.GetComponent<HeartShopCtrl> ().SetHeartShop ();
	}

	public void ExchangeHeart() {

		if (GameSystem.Instance.HeartCount >= 5) {
			OpenInfoPopUp(PopMessageType.HeartPurChaseButFull);
			return;
		}

		GameSystem.Instance.Post2HeartCharge ();
		CloseStack ();
	}


	private void CloseGoldPurchaseConfirm() {
        Debug.Log(">>> CloseGoldPurchaseConfirm");
		_goldPurchaseConfirm.SendMessage ("CloseSelf");
	}

#endregion

    #region 보석 구매 
    public void Purchase (string sku) {
        GameSystem.Instance.PurchaseInApp(sku);
    }


#endregion

    #region 골드 Exchagne
	public void ExchangeGoldZero(){
		_goldPurchaseConfirm.SetActive (true);
		_goldPurchaseConfirm.GetComponent<GoldPurchaseConfirmCtrl> ().SetExchangeIndex (0);
	}

	public void ExchangeGoldOne(){
		_goldPurchaseConfirm.SetActive (true);
		_goldPurchaseConfirm.GetComponent<GoldPurchaseConfirmCtrl> ().SetExchangeIndex (1);
	}

	public void ExchangeGoldTwo(){
		_goldPurchaseConfirm.SetActive (true);
		_goldPurchaseConfirm.GetComponent<GoldPurchaseConfirmCtrl> ().SetExchangeIndex (2);
	}

	public void ExchangeGoldThree(){
		_goldPurchaseConfirm.SetActive (true);
		_goldPurchaseConfirm.GetComponent<GoldPurchaseConfirmCtrl> ().SetExchangeIndex (3);
	}

	public void ExchangeGoldFour(){
		_goldPurchaseConfirm.SetActive (true);
		_goldPurchaseConfirm.GetComponent<GoldPurchaseConfirmCtrl> ().SetExchangeIndex (4);
	}
    #endregion


    #region 데이터 이전 관련

    public void IssueCode() {

        
        if(!string.IsNullOrEmpty(GameSystem.Instance.DataCode)) {
            Debug.Log("IssueCode has code");
            OpenCodeInfo(GameSystem.Instance.DataCode, GameSystem.Instance.DataCodeExpiredTime, true);
        }else { // 코드 정보가 없으면 발급 처리 
            Debug.Log("IssueCode no code");
            GameSystem.Instance.Post2CodeIssue();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="pCode"></param>
    /// <param name="pExpiredTime"></param>
    /// <param name="pHasCode"></param>
    public void OpenCodeInfo(string pCode, long pExpiredTime, bool pHasCode) {

        _codeInfo.SetCodeInfo(pCode, pExpiredTime, pHasCode);

    }


    public void OpenCodeInput() {
        _codeInput.gameObject.SetActive(true);
    }
    #endregion



    /// <summary>
    /// 
    /// </summary>
    /// <param name="pType"></param>
    /// <param name="pPos"></param>
    public void ThrowIcon(string pType, Vector3 pPos) {
        _throwingIcon.spriteName = pType;
        _throwingIcon.transform.position = pPos;

        if(pType.IndexOf("coin") >= 0) {
            _throwingIcon.transform.DOMove(PosTopCoinIcon, 1.5f).OnComplete(PlayTopCoinColorfulLight);
        }
        else {
            _throwingIcon.transform.DOMove(PosTopGemIcon, 1.5f).OnComplete(PlayTopGemColorfulLight);
        }

    }

/// <summary>
/// 미션 클리어 연출 
/// </summary>
/// <param name="pType"></param>
/// <param name="pValue"></param>
    public void PlayMissionClearDircet(string pType, int pValue) {
        _missionClearDirect.SetMissionDirect(pType, pValue);
    }

    void PlayTopCoinColorfulLight() {
        _colorfulLightInTop.PlayWorldPos(PosTopCoinIcon);
        UpdateTopInformation();
    }

    void PlayTopGemColorfulLight() {
        _colorfulLightInTop.PlayWorldPos(PosTopGemIcon);
        UpdateTopInformation();
    }


    public Vector3 PosTopCoinIcon {
        get {
            return _topCoinIcon.transform.position;
        }
    }

    public Vector3 PosTopGemIcon {
        get {
            return _topGemIcon.transform.position;
        }
    }


    public SKUMasterCtrl SkuMaster {
        get {
            return _skuMaster;
        }

        set {
            _skuMaster = value;
        }
    }

    public MissionColumnCtrl FinalDailyMission {
        get {
            return _finalDailyMission;
        }

        set {
            _finalDailyMission = value;
        }
    }

    public MissionColumnCtrl FinalWeeklyMission {
        get {
            return _finalWeeklyMission;
        }

        set {
            _finalWeeklyMission = value;
        }
    }
}
