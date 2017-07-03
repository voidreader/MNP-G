using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using PathologicalGames;


public class StageMasterCtrl : MonoBehaviour {

    static StageMasterCtrl _instance = null;

    readonly string STAGE_POOL = "Stages";
    readonly string BOTTLE_PREFAB = "CatBottleUI";
    readonly int STAGE_GAP = 960;
    //readonly string SPRITE_MOVE_BOTTLE_BTN = "main_btn_glass";
    //readonly string SPRITE_MOVE_PRE_CENTER = "main_btn_back";


    StageBaseCtrl _spawnedTheme;
    CatBottleCtrl _spawnedCatBottle;

    [SerializeField] UIAtlas _stageAtlasBG1;
    [SerializeField] UIAtlas _stageAtlasBG2;

    [SerializeField] UIAtlas _stageAtlasObject1;
    [SerializeField] UIAtlas _stageAtlasObject2;


    [SerializeField]
    List<StageBaseCtrl> _listThemes = new List<StageBaseCtrl>(); // 전체 스테이지 

    [SerializeField] StageClearCommentCtrl _themeClearComment;

    [SerializeField]
    UIPanel _stagePanel;

    [SerializeField]
    UICenterOnChild _stageCenterOnChild;

    [SerializeField] UIButton _btnMoveBottle;
    [SerializeField] GameObject _preCenterObject;
    [SerializeField] GameObject _currentCenterObject;


    [SerializeField] GameObject _btnLeftEpisodeMove;
    [SerializeField] GameObject _btnRightEpisodeMove;


    // 결과화면에서 다시하기 혹은 다음 스테이지를 실행했을때
    // 연출 도중 타 스테이지의 진입을 방지하기 위한 변수 
    bool _isLockedByLoadReplayOrNextStage = false;

    // 다시하기 용도
    public static event Action<int> OnCompleteStageClearDirect = delegate { };

    #region readonly

    readonly string _theme2BoardSeatSprite = "cow-sp-in";
    readonly string _theme2NotBoardSeatSprite = "cow-standard";
    readonly int CLEAR_STARS = 33;
    #endregion

    public static StageMasterCtrl Instance {
        get {
            if (_instance == null) {
                _instance = FindObjectOfType(typeof(StageMasterCtrl)) as StageMasterCtrl;

                if (_instance == null) {
                    // Debug.Log("LobbyCtrl Init Error");
                    return null;
                }
            }

            return _instance;
        }

    }

    #region Properties 
    public string Theme2BoardSeatSprite {
        get {
            return _theme2BoardSeatSprite;
        }
    }

    public string Theme2NotBoardSeatSprite {
        get {
            return _theme2NotBoardSeatSprite;
        }
    }

    public UICenterOnChild StageCenterOnChild {
        get {
            return _stageCenterOnChild;
        }

        set {
            _stageCenterOnChild = value;
        }
    }

    public List<StageBaseCtrl> ListThemes {
        get {
            return _listThemes;
        }

        set {
            _listThemes = value;
        }
    }

    public bool IsLockedByLoadReplayOrNextStage {
        get {
            return _isLockedByLoadReplayOrNextStage;
        }

        set {
            _isLockedByLoadReplayOrNextStage = value;
        }
    }

    #endregion

    // Use this for initialization
    void Start () {

        _stageCenterOnChild.onCenter = OnCenter;
        

    }

    void OnCenter(GameObject pObj) {

        if (pObj == null)
            return;


        // 중복실행을 막는다. 
        if (pObj == _currentCenterObject)
            return;

        Debug.Log("OnCenter :: " + pObj.ToString());
        _currentCenterObject = pObj;

        if(pObj.GetComponent<CatBottleCtrl>() != null ) {
            // 유리병 Center의 경우
            _btnLeftEpisodeMove.SetActive(false);
            _btnRightEpisodeMove.SetActive(true);
        }
        else {
            // 나머지는 스테이지 
            if(pObj.GetComponent<StageBaseCtrl>().ThemeID >= GameSystem.Instance.StageMasterJSON.Count) {
                _btnLeftEpisodeMove.SetActive(true);
                _btnRightEpisodeMove.SetActive(false);
            } 
            else {
                _btnLeftEpisodeMove.SetActive(true);
                _btnRightEpisodeMove.SetActive(true);
            }
        }

    }


    /// <summary>
    /// 중심 이동 
    /// </summary>
    /// <param name="pStage"></param>
    private void SetCameraPos(int pStage) {

        int movepos = GetThemeIndex(pStage);


        //Debug.Log("★ SetCameraPos pStage  :: " + pStage);
        //Debug.Log("★ SetCameraPos GetThemeIndex  :: " + movepos);
        

        if(movepos < 0) {
            Debug.Log("★ SetCameraPos Out of Theme Index");
            movepos = 11;
        }

        // _stageCenterOnChild.CenterOn(ListThemes[GetThemeIndex(pStage)].transform);

        Debug.Log("★ SetCameraPos pos :: " + ((movepos + 1) * STAGE_GAP).ToString());

        _stagePanel.clipOffset += Vector2.one;
        _stagePanel.clipOffset -= Vector2.one;
        _stagePanel.gameObject.GetComponent<UIScrollView>().ResetPosition();

        _stagePanel.transform.localPosition = new Vector3((movepos + 1) * STAGE_GAP * -1, 0, 0);
        _stagePanel.clipOffset = new Vector2((movepos + 1) * STAGE_GAP, 0);

        _currentCenterObject = _listThemes[movepos].gameObject;
        

    }

    /// <summary>
    /// 스테이지 마스터 초기화 
    /// </summary>
    public void InitializeStageMaster() {

        ListThemes.Clear();


        // Cat Bottle 
        _spawnedCatBottle = PoolManager.Pools[STAGE_POOL].Spawn(BOTTLE_PREFAB, Vector3.zero, Quaternion.identity).GetComponent<CatBottleCtrl>();
        _spawnedCatBottle.InitBottle();

        BottleManager.Instance.InitBottle();

        Debug.Log("★★★ InitializeStage :: " + GameSystem.Instance.StageMasterJSON.Count);

        // 스테이지 마스터에 따른 테마 소환 
        for(int i =0; i<GameSystem.Instance.StageMasterJSON.Count; i++) {

            _spawnedTheme = PoolManager.Pools[STAGE_POOL].Spawn("StageBase", Vector3.zero, Quaternion.identity).GetComponent<StageBaseCtrl>();
            _spawnedTheme.transform.localPosition = new Vector3((i+1) * STAGE_GAP, 0, 0); // 해상도 만큼 가로 위치 설정 

            ListThemes.Add(_spawnedTheme);
            _spawnedTheme.InitTheme(GameSystem.Instance.StageMasterJSON[i]["masterid"].AsInt, i);
            
            
        }

        // 플레이하고 나서 로비 진입시에는 클리어와 현상 유지로 분기 
        if(GameSystem.Instance.UserStageJSON["laststage"].AsInt > 0) {

            Debug.Log("★ Init StageMaster , LastStage Played :: " + GameSystem.Instance.UserStageJSON["laststage"]);

            // 퍼즐 종료 후, 플레이 스테이지를 클리어 했다면 연출을 표시한다.
            if (GameSystem.Instance.InGameStageClear && GameSystem.Instance.InGameStageUp) {

                Debug.Log("★ Stage Clear!");

                SetCameraPos(GameSystem.Instance.UserStageJSON["laststage"].AsInt);
                DirectStageClear();

            }
            else {

                Debug.Log("★ Stage Fail!");

                SetCameraPos(GameSystem.Instance.UserStageJSON["laststage"].AsInt);
                SetCurrentStageFromMaster();
            }

        }
        else {

            Debug.Log("★ Init StageMaster First Init");

            // SetCameraPos(GameSystem.Instance.UserStageJSON["currentstage"].AsInt);
            //Invoke("MoveCurrentStageTheme", 0.3f);
            SetCameraPos(GameSystem.Instance.UserCurrentStage);
            SetCurrentStageFromMaster();
        }


        // 튜토리얼이 완료되지 않은 경우는 scroll view를 동작하지 않도록 처리
        if(GameSystem.Instance.TutorialComplete <2 ) {
            _stagePanel.GetComponent<UIScrollView>().enabled = false;
        }
        else {
            _stagePanel.GetComponent<UIScrollView>().enabled = true;
        }


    }

    /// <summary>
    /// 마지막으로 플레이 했던 스테이지의 업데이트 여부 
    /// </summary>
    /// <returns></returns>
    public bool GetLastStageUpdated() {
        if (GameSystem.Instance.UserStageJSON["laststage"].AsInt > 0
            && GameSystem.Instance.InGameStageClear 
            && GameSystem.Instance.InGameStageUp) {

            return true;

        }


        return false;
    }



    /// <summary>
    /// 현재 스테이지 표시 
    /// </summary>
    public void SetCurrentStageFromMaster() {

        //Debug.Log("★★ SetCurrentStage Theme Index:: " + GetThemeIndex(GameSystem.Instance.UserStageJSON["currentstage"].AsInt));

        if (GetThemeIndex(GameSystem.Instance.UserStageJSON["currentstage"].AsInt) < 0)
            return;

        if(GameSystem.Instance.UserCurrentStage > GameSystem.Instance.StageMasterJSON.Count * 2 * 13) {
            return;
        }

        // 현재 스테이지 정보 처리 
        ListThemes[GetThemeIndex(GameSystem.Instance.UserStageJSON["currentstage"].AsInt)].SetCurrentStage(GameSystem.Instance.UserStageJSON["currentstage"].AsInt);

    }


    /// <summary>
    /// 스테이지를 클리어 했을때, 연출
    /// </summary>
    public void DirectStageClear() {

        int playstage  = GameSystem.Instance.UserStageJSON["laststage"].AsInt;
        int themeIndex = 0;


        //테마 index 찾기 
        themeIndex = GetThemeIndex(playstage);
        ListThemes[themeIndex].SetClearEffect(playstage, CheckPlayThemeStars); // 방금 플레이한 스테이지를 클리어. 

    }


    #region Theme에 맞는 Index, Atlas Get

    /// <summary>
    /// ThemeIndex 구하기 
    /// </summary>
    /// <returns></returns>
    public int GetThemeIndex(int pStage) {

        if (pStage >= 1 && pStage <= 13)
            return 0;

        if (pStage > 13 && pStage <= 26)
            return 1;

        
        if (pStage > 26 && pStage <= 39)
            return 2;

       
        if (pStage > 39 && pStage <= 52)
            return 3;

        if (pStage > 52 && pStage <= 65)
            return 4;

        if (pStage > 65 && pStage <= 78)
            return 5;

        if (pStage > 78 && pStage <= 91)
            return 6;

        if (pStage > 91 && pStage <= 104)
            return 7;

        if (pStage > 104 && pStage <= 117)
            return 8;

        if (pStage > 117 && pStage <= 130)
            return 9;

        if (pStage > 130 && pStage <= 143)
            return 10;

        if (pStage > 143 && pStage <= 156)
            return 11;




        return -1;

    }


    /// <summary>
    /// 테마에 맞는 아틀라스 조회 
    /// </summary>
    /// <param name="pTheme"></param>
    /// <returns></returns>
    public UIAtlas GetThemeBGAtlas(int pTheme) {

        switch(pTheme) {
            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
            case 6:
                return _stageAtlasBG1;

            case 7:
            case 8:
            case 9:
            case 10:
            case 11:
            case 12:
                return _stageAtlasBG2;
        }

        return null;

    }

    public UIAtlas GetThemeObjectAtlas(int pTheme) {
        switch (pTheme) {
            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
            case 6:
                return _stageAtlasObject1;

            case 7:
            case 8:
            case 9:
            case 10:
            case 11:
            case 12:
                return _stageAtlasObject2;
        }

        return null;
    }

    #endregion


    /// <summary>
    /// 다음 테마로 넘어갈 자격이 생기면, 안내 메세지 오픈 
    /// </summary>
    /// <param name="pTheme"></param>
    public void OpenThemeClearComment(int pTheme, int pStars) {

        // 여기에 넘어왔다는 건, 테마의 스테이지는 일단 모두 클리어했다는 뜻. 별이 부족할 수도 있다.


        // 보상을 받은 적이 없는 경우
        if (!GetThemeClearPaidInfo(pTheme)) {

            // 서버 통신 (보상 요청)
            GameSystem.Instance.Post2RequestThemeClearPay(pTheme); // 보상 요청 (통신)
            LobbyCtrl.Instance.SendMessage("ForceMailNewSign");


            _themeClearComment.SetThemeClearComment(pTheme, pStars);

            if (pTheme >= ListThemes.Count)
                return;

            // 다음 테마의 Lock을 해제하도록.
            ListThemes[pTheme].CheckThemeLockStatus();

        }
        else { //  보상을 이미 받은 경우.

            // 별을 33개이상 모으지 않았으면 띄우지 않아야 한다.  
            if (pStars < CLEAR_STARS)
                return;

            // 보상을 받았고, 다음 테마가 존재하면서, Lock 상태라면, 다른 메세지를 띄우고 Move 시킨다.
            if (pTheme < ListThemes.Count && ListThemes[pTheme].IsLock) {

                _themeClearComment.SetNextThemeOpenComment(pTheme);

                // 다음 테마의 Lock 해제 
                ListThemes[pTheme].CheckThemeLockStatus();
            }
            else if(pTheme >= ListThemes.Count) { // 뒤에 테마가 아직 구현되지 않은 경우.

                // 로컬 정보 활용
                if(!GameSystem.Instance.LoadESvalueBool("ThemeClearComment" + pTheme)) {

                    _themeClearComment.SetNextThemeOpenComment(pTheme);

                    GameSystem.Instance.SaveESvalueBool("ThemeClearComment" + pTheme, true); // 로컬에 메세지를 띄웠다고 저장.
                }

            }
        }

    }

    /// <summary>
    ///  지정한 테마로 강제 이동
    /// </summary>
    /// <param name="pTargetTheme"></param>
    public void MoveTheme(int pTargetTheme) {

        Debug.Log("★☆★MoveTheme Target :: " + pTargetTheme);

        if (pTargetTheme-1 >= ListThemes.Count) {
            return;
        }

        _stageCenterOnChild.CenterOn(ListThemes[pTargetTheme - 1].transform);

    }



    void MoveCurrentStageTheme() {
        
        _stageCenterOnChild.CenterOn(ListThemes[GetThemeIndex(GameSystem.Instance.UserCurrentStage)].transform);
        //GetThemeIndex(GameSystem.Instance.UserStageJSON["laststage"].AsInt)
    }


    /// <summary>
    /// 이미 이전에 받은 보상이 있는지 체크 
    /// </summary>
    /// <param name="pTheme"></param>
    /// <returns></returns>
    bool GetThemeClearPaidInfo(int pTheme) {

        if (GameSystem.Instance.UserThemeJSON.AsArray == null || GameSystem.Instance.UserThemeJSON.AsArray.Count == 0)
            return false;

        for (int i=0; i<GameSystem.Instance.UserThemeJSON.Count;i++) {
            if(GameSystem.Instance.UserThemeJSON[i]["themeid"].AsInt == pTheme 
                && GameSystem.Instance.UserThemeJSON[i]["takepay"].AsInt > 0) {

                return true;
            }

        }


        return false;
    }

    #region Debug
    public int GetThemeStars(int pTheme) {

        

        int themeIndex = pTheme - 1; // 

        int minStageIndx = _listThemes[themeIndex].FirstStage - 1;
        int maxStageIndx = _listThemes[themeIndex].LastStage - 1;


        //Debug.Log("★ GetThemeStars Theme/minStageIndex/maxStageIndex :: " + pTheme + "/" + minStageIndx + "/" + maxStageIndx);

        int themeStars = 0;

        for (int i = minStageIndx; i <= maxStageIndx; i++) {
            themeStars += GameSystem.Instance.UserStageJSON["stagelist"][i]["state"].AsInt;
        }


        _listThemes[themeIndex].SetStageStarCounter(themeStars);


        return themeStars;
    }
    #endregion 

    /// <summary>
    /// 테마 별 개수 찾기 (클리어 연출 이후에 체크)
    /// DirectStageClear Callback
    /// </summary>
    /// <param name="pStage"></param>
    public void CheckPlayThemeStars(int pTheme) {
        
        int themeIndex = pTheme - 1; // 

        int minStageIndx = _listThemes[themeIndex].FirstStage - 1;
        int maxStageIndx = _listThemes[themeIndex].LastStage - 1;

        int themeStars = 0;


        GetThemeStars(pTheme);

        Invoke("CallOnCompleteStageClearDirect", 0.5f);


        // 0 1 2 3 4 5 6 7 8 9 10 11 12
        //UserStageJSON["stagelist"]
        // state 모두 더하기.
        for (int i=minStageIndx; i <= maxStageIndx; i++) {
 
            // 클리어하지 않은 스테이지가 있으면, 그냥 break;
            if (GameSystem.Instance.UserStageJSON["stagelist"][i]["state"].AsInt == 0) {
                themeStars = 0;
                break; 
            }


            themeStars += GameSystem.Instance.UserStageJSON["stagelist"][i]["state"].AsInt;
        }

        // 별 카운트 Update
        //_listThemes[themeIndex].SetStageStarCounter(themeStars);
        Debug.Log("★★ CheckPlayThemeStars themeStars :: " + themeStars);

        // 클이어하지 않은 스테이지가 있으면 종료 
        if (themeStars > 0) {
            OpenThemeClearComment(pTheme, themeStars);
        }

        // 모든 처리가 완료되었으면 플레이 관련 변수 초기화 
        GameSystem.Instance.InGameStageClear = false;
        GameSystem.Instance.InGameStageUp = false;
    }

    /// <summary>
    /// 
    /// </summary>
    void CallOnCompleteStageClearDirect() {

        // 최대 스테이지 (임시로 넣는다)
        if(GameSystem.Instance.PlayStage + 1 > 156) {
            IsLockedByLoadReplayOrNextStage = false;
            OnCompleteStageClearDirect = delegate { };
            return;
        }

        OnCompleteStageClearDirect(GameSystem.Instance.PlayStage); // 다음 스테이지를 연다. 
        OnCompleteStageClearDirect = delegate { };
    }



    /// <summary>
    /// 주어진 테마의 Lock 상태 여부 (이전 테마의 별 갯수를 구한다.)
    /// </summary>
    /// <param name="pThemeID"></param>
    /// <returns></returns>
    public bool GetCurrentThemeLockStatus(int pThemeID) {

        // 테마 1은 Lock 없다. 무조건 false
        if (pThemeID <= 1)
            return false;

        int themeIndex = pThemeID - 2; // 이전 테마의 별 갯수를 세어야 하기 때문에 -2를 한다. 

        int minStageIndx = _listThemes[themeIndex].FirstStage - 1;
        int maxStageIndx = _listThemes[themeIndex].LastStage - 1;

        int themeStars = 0;

        // 0 1 2 3 4 5 6 7 8 9 10 11 12
        //UserStageJSON["stagelist"]
        // state 모두 더하기.
        for (int i = minStageIndx; i <= maxStageIndx; i++) {

            // 스테이지 중 클리어되지 않은 스테이지가 있으면 무조건 true(잠금)
            if (GameSystem.Instance.UserStageJSON["stagelist"][i]["state"].AsInt == 0)
                return true;

            themeStars += GameSystem.Instance.UserStageJSON["stagelist"][i]["state"].AsInt;
        }

        // 테마 2의 경우, 테마1를 모두 클리어했다면 Lock이 해제된다. 
        if(pThemeID == 2) {
            return false;
        }
        else {
            if (themeStars < 33) // 별이 33개 미만이면 Lock 
                return true;
            else
                return false; 
        }

    }


    #region 버튼 이동 


    /// <summary>
    /// 유리병으로 이동 
    /// </summary>
    public void MoveToBottle() {

        if (GameSystem.Instance.TutorialComplete < 2)
            return;

        StageCenterOnChild.CenterOn(_spawnedCatBottle.transform);

        /*
        if (_btnMoveBottle.normalSprite == SPRITE_MOVE_BOTTLE_BTN) {
            _btnMoveBottle.normalSprite = SPRITE_MOVE_PRE_CENTER;
            _preCenterObject = StageCenterOnChild.centeredObject;
            StageCenterOnChild.CenterOn(_spawnedCatBottle.transform);
        }
        else { // 되돌아가기
            _btnMoveBottle.normalSprite = SPRITE_MOVE_BOTTLE_BTN;
            StageCenterOnChild.CenterOn(_preCenterObject.transform);
        }
        */
        
    }


    public void MoveRightEpisode() {

        if (GameSystem.Instance.TutorialComplete < 2)
            return;


        if (_currentCenterObject == null)
            _currentCenterObject = StageCenterOnChild.centeredObject;

        // 유리병이 Center에 와있는 경우 
        if (_currentCenterObject.GetComponent<CatBottleCtrl>() != null) {
            MoveTheme(1);
        }
        else {
            // 나머지는 스테이지 
            if (_currentCenterObject.GetComponent<StageBaseCtrl>().ThemeID >= GameSystem.Instance.StageMasterJSON.Count) {
                return;
            }


            MoveTheme(_currentCenterObject.GetComponent<StageBaseCtrl>().ThemeID + 1);
        }
    }


    public void MoveLeftEpisode() {

        if (GameSystem.Instance.TutorialComplete < 2)
            return;

        if (_currentCenterObject == null)
            _currentCenterObject = StageCenterOnChild.centeredObject;

        if (_currentCenterObject.GetComponent<StageBaseCtrl>().ThemeID == 1)
            MoveToBottle();
        else {
            MoveTheme(_currentCenterObject.GetComponent<StageBaseCtrl>().ThemeID - 1);
        }

    }

    #endregion

}
