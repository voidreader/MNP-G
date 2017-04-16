using UnityEngine;
using System;
using System.Collections;
using PathologicalGames;
using DG.Tweening;
using SimpleJSON;


/// <summary>
/// 13개의 스테이지로 이루어진 큰 스테이지 
/// </summary>
public class StageBaseCtrl : MonoBehaviour {


    Action<int> OnSparkCallBack;

    [SerializeField]
    int _themeID = 0;

    [SerializeField] Transform _childDragTransform;
    [SerializeField] StageLockObjectCtrl _stageLock;

    [SerializeField] UISprite _mainBG;
    [SerializeField] UISprite _fenceSprite;
    [SerializeField] UISprite _finalSpotSprite;

    [SerializeField] UISprite _bgLeft;
    [SerializeField] UISprite _bgRight;

    [SerializeField] WhiteLightCtrl _sparkLight; // 스파크 이펙트 (하단에서 사용)
    [SerializeField] WhiteLightCtrl _colorfulSparkLight; // 스파크 이펙트 (상단에서 사용)
    [SerializeField] UILabel _lblStarCount; // 별 카운터 

    [SerializeField] int _firstStage = 0; // 현재 테마의 첫번째 스테이지 
    [SerializeField] int _lastStage = 0; // 현재 테마의 마지막 스테이지 

    [SerializeField] UISprite[] _arrCurrentThemeNeko;
    [SerializeField] Transform[] _arrCurrentThemeSeat;

    #region Theme 1
    public Transform _ferrisWheel;

    [SerializeField] Transform _objWheelCircle;
    public Transform _objBone;
    public Transform[] _arrObjWheelSeats;
    public UISprite[] _arrWheelSeatNeko;

    #endregion

    #region Theme 2

    [SerializeField]
    GameObject _theme2;

    [SerializeField] Transform[] _arrTheme2Seats;
    [SerializeField] UISprite[] _arrTheme2Neko;



    #endregion

    #region Theme 3

    [SerializeField]
    GameObject _theme3;
    [SerializeField] UISprite[] _arrTheme3Neko;
    [SerializeField] GameObject[] _arrTheme3Spot;

    Transform _theme3LeftSmallLight;
    Transform _theme3RightSmallLight;
    Transform _theme3LeftBigLight;
    Transform _theme3RightBigLight;

    UISpriteAnimation _theme3LeftSpot;
    UISpriteAnimation _theme3CenterSpot;
    UISpriteAnimation _theme3RightSpot;


    Vector3 _theme3LeftJumpPos = new Vector3(-65, 115, 0);
    Vector3 _theme3RightJumpPos = new Vector3(65, 115, 0);

    Vector3 _themeGroundLeftJumpPos = new Vector3(-100, 60, 0);
    Vector3 _themeGroundRightJumpPos = new Vector3(100, 60, 0);

    Vector3 _themeBottomLeftJumpPos = new Vector3(-65, 50);
    Vector3 _themeBottomRightJumpPos = new Vector3(65, 50);


    #endregion

    #region Theme 4
    [SerializeField]
    GameObject _theme4;

    [SerializeField] UISprite[] _arrTheme4Neko;
    [SerializeField] GameObject[] _arrTheme4Spot;

    Transform _theme4SwingBase;
    Transform _theme4SwingRope;

    #endregion

    #region Theme5
    [SerializeField] GameObject _theme5;
    [SerializeField] UISprite[] _arrTheme5Neko;
    [SerializeField] GameObject[] _arrTheme5Spot;
    Transform _theme5Wheel1;
    Transform _theme5Wheel2;
    #endregion

    #region Theme 6
    [SerializeField] GameObject _theme6;
    [SerializeField] UISprite[] _arrTheme6Neko;
    [SerializeField] GameObject[] _arrTheme6Spot;

    Transform _theme6BottomLight;
    Transform _theme6TopLight;
    Transform _theme6SideLightLeft;
    Transform _theme6SideLightRight;
    Transform _theme6Screen;

    Transform _theme6LeftLight;
    Transform _theme6RightLight;

    #endregion

    #region Theme 7

    [SerializeField] GameObject _theme7;
    [SerializeField] UISprite[] _arrTheme7Neko;

    Transform _theme7Center;
    Transform _theme7CenterTimer;

    Transform _theme7LeftPump;
    Transform _theme7LeftPumpHead;
    Transform _theme7RightPump;
    Transform _theme7RightPumpHead;

    Transform[] _theme7ChainWheels = new Transform[10];
    [SerializeField] GameObject[] _theme7Candy;

    #endregion

    #region Theme 8
    [SerializeField] GameObject _theme8;
    [SerializeField] UISprite[] _arrTheme8Neko;

    Transform _theme8LeftLight;
    Transform _theme8RightLight;
    Transform _theme8CenterLight;
    Transform _theme8Thunder;

    //Transform _theme8Rockets;
    GameObject[] _theme8Rockets;

    #endregion

    #region Theme 9
    [SerializeField] GameObject _theme9;
    [SerializeField] UISprite[] _arrTheme9Neko;

    Transform _theme9UFO;
    GameObject[] _arrTheme9NekoCap;

    #endregion


    #region Theme 10
    [SerializeField] GameObject _theme10;
    [SerializeField] UISprite[] _arrTheme10Neko;

    Transform _theme10Handle; // 중앙 핸들

    #endregion



    [SerializeField] int[] _arrStageMiniNekoIds = new int[4]; // 구출 대상 네코 ID 모음 
    [SerializeField] string[] _arrStageMiniNekoSprites = new string[4]; // 구출 대상 네코 Sprite 모음 


    [SerializeField] int _stageBossNekoID = 0;

    // 스테이지 클리어 마크
    [SerializeField] StageClearObjectCtrl[] _arrStageClearObject;

    // 스테이지 미니 네코 
    [SerializeField] StageMiniHeadCtrl[] _arrStageMiniHeads;


    [SerializeField] UISpriteAnimation _clearBreakEffect; // 클리어 폭파 이펙트
    [SerializeField] Transform _clearTopText; // 클리어 텍스트 

    [SerializeField]
    StageClearObjectCtrl _currentStageObject = null;

    [SerializeField]
    Transform _bossPosition; // 보스 위치 



    [SerializeField] bool _isLock = false;

    JSONNode _themeNode;
    // int _tempIndex = 0;


    bool _isProcessingThemeClearing = false;

    // CallBack 스테이지 클리어 연출 
    Action<int> _OnCompleteDirectingClear = delegate { };

    #region Properties 

    public StageClearObjectCtrl[] ArrStageClearObject {
        get {
            return _arrStageClearObject;
        }

        set {
            _arrStageClearObject = value;
        }
    }

    public StageMiniHeadCtrl[] ArrStageMiniHeads {
        get {
            return _arrStageMiniHeads;
        }

        set {
            _arrStageMiniHeads = value;
        }
    }

    public bool IsProcessingThemeClearing {
        get {
            return _isProcessingThemeClearing;
        }

        set {
            _isProcessingThemeClearing = value;
        }
    }

    public int ThemeID {
        get {
            return _themeID;
        }

        set {
            _themeID = value;
        }
    }

    public Transform ChildDragTransform {
        get {
            return _childDragTransform;
        }

        set {
            _childDragTransform = value;
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

    public int FirstStage {
        get {
            return _firstStage;
        }

        set {
            _firstStage = value;
        }
    }

    public int LastStage {
        get {
            return _lastStage;
        }

        set {
            _lastStage = value;
        }
    }

    #endregion


    // Use this for initialization
    void Start() {

    }

    #region Init Theme

    /// <summary>
    /// 테마 초기화 
    /// </summary>
    /// <param name="pIndex"></param>
    public void InitTheme(int pIndex, int pJSONIndex) {

        ThemeID = GameSystem.Instance.StageMasterJSON[pJSONIndex]["masterid"].AsInt;

        // 테마 배경 및 Sprite 설정 
        _mainBG.atlas = StageMasterCtrl.Instance.GetThemeBGAtlas(ThemeID);
        _bgLeft.atlas = StageMasterCtrl.Instance.GetThemeBGAtlas(ThemeID);
        _bgRight.atlas = StageMasterCtrl.Instance.GetThemeBGAtlas(ThemeID);

        _fenceSprite.atlas = StageMasterCtrl.Instance.GetThemeObjectAtlas(ThemeID);


        _mainBG.spriteName = GameSystem.Instance.StageMasterJSON[pJSONIndex]["sprite_mainbg"].Value;
        _fenceSprite.spriteName = GameSystem.Instance.StageMasterJSON[pJSONIndex]["fence_sprite"].Value;
        _finalSpotSprite.spriteName = GameSystem.Instance.StageMasterJSON[pJSONIndex]["final_sprite"].Value;
        _currentStageObject.SetCurrentStageSprite(GameSystem.Instance.StageMasterJSON[pJSONIndex]["current_sprite"].Value);


        _bgLeft.spriteName = GameSystem.Instance.StageMasterJSON[pJSONIndex]["bg_left"].Value;
        _bgRight.spriteName = GameSystem.Instance.StageMasterJSON[pJSONIndex]["bg_right"].Value;


        // Theme의 최초 스테이지는 index * 13
        // Theme의 마지막 스테이지는 index * 13 + 13
        FirstStage = pJSONIndex * 13 + 1;
        LastStage = pJSONIndex * 13 + 13;

        // 스타 카운터 초기화 
        _lblStarCount.text = "0/39";
        _lblStarCount.text = StageMasterCtrl.Instance.GetThemeStars(ThemeID).ToString() + "/39";


        //_fenceSprite.MakePixelPerfect();

        // 현재 스테이지 표시 및 클리어 마크 초기화
        _currentStageObject.gameObject.SetActive(false);

        for (int i = 0; i < ArrStageClearObject.Length; i++) {
            ArrStageClearObject[i].gameObject.SetActive(false);
        }


        // 구출 대상 네코, 보스 네코 세팅 
        _arrStageMiniNekoIds[0] = GameSystem.Instance.StageMasterJSON[pJSONIndex]["stage_neko1"].AsInt;
        _arrStageMiniNekoIds[1] = GameSystem.Instance.StageMasterJSON[pJSONIndex]["stage_neko2"].AsInt;
        _arrStageMiniNekoIds[2] = GameSystem.Instance.StageMasterJSON[pJSONIndex]["stage_neko3"].AsInt;
        _arrStageMiniNekoIds[3] = GameSystem.Instance.StageMasterJSON[pJSONIndex]["stage_neko4"].AsInt;

        _arrStageMiniNekoSprites[0] = GameSystem.Instance.StageMasterJSON[pJSONIndex]["stage_neko1_sprite"];
        _arrStageMiniNekoSprites[1] = GameSystem.Instance.StageMasterJSON[pJSONIndex]["stage_neko2_sprite"];
        _arrStageMiniNekoSprites[2] = GameSystem.Instance.StageMasterJSON[pJSONIndex]["stage_neko3_sprite"];
        _arrStageMiniNekoSprites[3] = GameSystem.Instance.StageMasterJSON[pJSONIndex]["stage_neko4_sprite"];

        // 테마 보스 
        _stageBossNekoID = GameSystem.Instance.StageMasterJSON[pJSONIndex]["stage_boss"].AsInt;


        // 스테이지에 위치하는 미니 네코 설정 
        for (int i = 0; i < _arrStageMiniHeads.Length; i++) {
            _arrStageMiniHeads[i].SetMiniSpriteByID(_arrStageMiniNekoIds[i], _arrStageMiniNekoSprites[i]);
        }


        // 클리어 마크 레이블 처리 
        for (int i = 0; i < _arrStageClearObject.Length; i++) {
            _arrStageClearObject[i].SetStageLabel(FirstStage + i, this);
        }


        // 보스 마크
        _bossPosition.gameObject.SetActive(true);
        _bossPosition.DOKill();
        _bossPosition.DOScale(1.1f, 0.5f).SetLoops(-1, LoopType.Yoyo);



        InitThemeProgress(); // 진척도 설정 



        CheckThemeLockStatus();

    }



    /// <summary>
    /// 각 테마별 진척도에 따른 상태 처리 
    /// </summary>
    /// <param name="pClearCount"></param>
    void InitThemeProgress() {

        int clearCount = GetClearCount();

        switch (ThemeID) {
            case 1:
                Debug.Log("★★ Theme 1 ClearCount :: " + clearCount);
                Set1stThemeProgress(clearCount);
                break;

            case 2:
                Set2stThemeProgress(clearCount);
                break;

            case 3:
                Set3rdThemeProgress(clearCount);
                break;

            case 4:
                Set4thThemeProgress(clearCount);
                break;

            case 5:
                Set5thThemeProgress(clearCount);
                break;

            case 6:
                Set6thThemeProgress(clearCount);
                break;


            case 7:
                Set7thThemeProgress(clearCount);
                break;

            case 8:
                Set8thThemeProgress(clearCount);
                break;

            case 9:
                Set9thThemeProgress(clearCount);
                break;

            case 10:
                Set10thThemeProgress(clearCount);
                break;


        }
    }

    void OnSpawned() {
        this.transform.localScale = GameSystem.Instance.BaseScale;

        _ferrisWheel.gameObject.SetActive(false); // Theme1
        _theme2.gameObject.SetActive(false);
        _theme3.gameObject.SetActive(false);
        _theme4.gameObject.SetActive(false);
        _theme5.gameObject.SetActive(false);
        _theme6.gameObject.SetActive(false);
        _theme7.gameObject.SetActive(false);
        _theme8.gameObject.SetActive(false);
        _theme9.gameObject.SetActive(false);
        _theme10.gameObject.SetActive(false);


    }



    /// <summary>
    /// 테마의 잠금상태 여부 체크 
    /// </summary>
    public void CheckThemeLockStatus() {
        IsLock = StageMasterCtrl.Instance.GetCurrentThemeLockStatus(ThemeID);
        _stageLock.gameObject.SetActive(IsLock);
    }


    /// <summary>
    /// 각 테마에 클리어 스테이지 개수 구하기.
    /// </summary>
    /// <returns></returns>
    int GetClearCount() {

        int clearCount = 0;
        int lastPlayStage = GameSystem.Instance.UserStageJSON["laststage"].AsInt;

        for (int i = FirstStage - 1; i < LastStage; i++) {
            if (GameSystem.Instance.UserStageJSON["stagelist"][i]["state"].AsInt > 0)
                clearCount++;
        }
        return clearCount;
    }

    #endregion





    /// <summary>
    ///  현재 스테이지 설정 
    /// </summary>
    /// <param name="pStage"></param>
    public void SetCurrentStage(int pStage) {

        // 1~13, 14~26, 

        int stageIndex = 0;

        if (pStage <= 13)
            stageIndex = pStage - 1;
        else { // 임시
            stageIndex = pStage - (13 * (ThemeID - 1)) - 1;
        }


        Debug.Log("★★ SetCurrentStage :: " + stageIndex);

        _currentStageObject.SetStageLabel(pStage, this);
        _currentStageObject.transform.localPosition = PuzzleConstBox.listCurrentStagePos[stageIndex];
        _currentStageObject.gameObject.SetActive(true);
    }


    #region 스테이지 클리어 연출 처리 SetThemeSpecialProgressEffect, SetClearEffect

    /// <summary>
    /// 테마의 3,6,9,11,12번째 스테이지에 대한 특수 연출 처리 
    /// </summary>
    /// <param name="pIndex"></param>
    void SetThemeSpecialProgressEffect(int pIndex) {

        IsProcessingThemeClearing = true;


        Debug.Log("★ SetThemeProgressEffect ThemeID :: " + ThemeID);
        Debug.Log("★ SetThemeProgressEffect pIndex :: " + pIndex);

        // OnSparkCallBack 은 각 테마마다 다르게 설정된다. 
        _sparkLight.PlayWorldPos(_arrStageMiniHeads[pIndex].transform.position, OnSparkCallBack

            , pIndex);
        _arrStageMiniHeads[pIndex].gameObject.SetActive(false);

    }


    /// <summary>
    /// 게임 플레이 후 플레이 스테이지 클리어 
    /// </summary>
    /// <param name="pStage">마지막으로 플레이했던 스테이지 </param>
    public void SetClearEffect(int pStage, Action<int> pCallback) {

        // 테마에서 몇번째 스테이지인지 구하기.
        int index = pStage - FirstStage;
        int pindex = 0;

        _OnCompleteDirectingClear = pCallback;

        Debug.Log("★★ SetClearEffect " + index);

        // 3,6,9,12,13 스테이지에 대해서, 첫 클리어에 대한 특수 처리 시작 
        if ((index == 2 || index == 5 || index == 8 || index == 11)
            && GameSystem.Instance.UserStageJSON["prestate"].AsInt == 0) {

            // 구출 고양이 Index 
            if (index == 2)
                pindex = 0;
            else if (index == 5)
                pindex = 1;
            else if (index == 8)
                pindex = 2;
            else if (index == 11)
                pindex = 3;

            SetThemeSpecialProgressEffect(pindex);
        }


        // 연출 종료 후 진행 (Clear Mark, 보스 아이콘, 현재 스테이지 등 처리)
        StartCoroutine(WaitingThemeClearing(index));
    }

    IEnumerator WaitingThemeClearing(int pIndex) {

        // 이전 연출(고양이 구출)이 종료될때 까지 기다린다.
        while (IsProcessingThemeClearing) {
            yield return new WaitForSeconds(0.1f);
        }


        // 현재 스테이지를 알려주는 마크 제거 
        _currentStageObject.gameObject.SetActive(false);

        // 스테이지 클리어 효과 시작 
        ArrStageClearObject[pIndex].SetCleatEffect(GameSystem.Instance.UserStageJSON["laststate"].AsInt);


        // 마지막 스테이지의 경우 Boss Positino 비활성화
        if (pIndex >= 12) {
            _bossPosition.gameObject.SetActive(false);
        }

        // 마지막 플레이 했던 스테이지 정보를 갱신
        GameSystem.Instance.UpdateLastPlayStage();

        // 현재 스테이지 표시 
        StageMasterCtrl.Instance.SetCurrentStageFromMaster();


        yield return new WaitForSeconds(2);

        // Callback, 별 체크 및 다음 Theme Open여부를 체크 
        _OnCompleteDirectingClear(ThemeID);
    }


    /// <summary>
    /// 클리어 스파크 이펙트 후 진행 처리 
    /// </summary>
    /// <param name="pIndex"></param>
    void OnCompleteCurrentThemeSparkCallBack(int pIndex) {
        _arrStageMiniHeads[pIndex].PlayClearJump();

        // 미니네코의 위치를 알맞는 Seat로이동, 딜레이 처리 
        _arrStageMiniHeads[pIndex].transform.DOMove(_arrCurrentThemeSeat[pIndex].transform.position, 1.5f).OnComplete(() => OnCompleteCurrentThemeMiniHeadMove(pIndex)).SetDelay(0.6f);
    }


    /// <summary>
    /// 미니헤드 날아가고 나서 처리 
    /// </summary>
    /// <param name="pIndex"></param>
    void OnCompleteCurrentThemeMiniHeadMove(int pIndex) {
        _arrStageMiniHeads[pIndex].gameObject.SetActive(false);

        // 날아간 위치에서 펑~
        _colorfulSparkLight.PlayWorldPos(_arrCurrentThemeSeat[pIndex].transform.position);

        // 테마마다 동작이 다른 부분.
        _arrStageMiniHeads[pIndex].gameObject.SetActive(false);

        // 날아간 위치에서 펑~
        _colorfulSparkLight.PlayWorldPos(_arrCurrentThemeSeat[pIndex].transform.position);

        // 고양이 출현!
        _arrCurrentThemeNeko[pIndex].gameObject.SetActive(true);
        GameSystem.Instance.SetNekoSpriteByID(_arrCurrentThemeNeko[pIndex], _arrStageMiniNekoIds[pIndex]);

        // 테마마다 추가 효과 
        switch (ThemeID) {
            case 3:
                _arrTheme3Spot[pIndex].SetActive(true); // 각 장소의 특수효과 연출을 위해 Active.
                SetPlaying3rdThemeEach(pIndex);

                break;
            case 4:
            case 5:
            case 6:
            case 7:
            case 8:
                if (pIndex >= 3)
                    PlayingTheme(ThemeID);
                break;

            case 9:

                if (pIndex <= 3) {
                    _arrTheme9NekoCap[pIndex].SetActive(true);
                    _arrTheme9NekoCap[pIndex].GetComponent<UISpriteAnimation>().Play();
                }
                break;
        }


        IsProcessingThemeClearing = false;
    }

    #endregion


    /// <summary>
    /// 준비 화면 오픈 
    /// </summary>
    /// <param name="pStage"></param>
    public void OpenReady(int pStage) {

        if (IsLock) {
            LobbyCtrl.Instance.OpenInfoPopUp(PopMessageType.ThemeLock);
            return;
        }

        LobbyCtrl.Instance.OpenReady(pStage);
    }



    #region 폭파 이펙트 사용 

    /// <summary>
    /// 폭파 이펙트 사용 
    /// </summary>
    /// <param name="pLocalPos"></param>
    public void OnStageClearBreakEffect(Vector3 pLocalPos) {
        _clearBreakEffect.transform.localPosition = pLocalPos;

        _clearBreakEffect.gameObject.SetActive(true);
        _clearBreakEffect.Play();

        StartCoroutine(PlayingClearBreakEffect(pLocalPos));
    }


    IEnumerator PlayingClearBreakEffect(Vector3 pLocalPos) {
        while (_clearBreakEffect.isPlaying) {
            yield return new WaitForSeconds(0.02f);
        }

        _clearBreakEffect.gameObject.SetActive(false);

        // 클리어 텍스트 연출 
        OnStageClearTextTop(pLocalPos);

    }


    /// <summary>
    /// 클리어 텍스트 연출 
    /// </summary>
    /// <param name="pLocalPos"></param>
    public void OnStageClearTextTop(Vector3 pLocalPos) {

        //기준 위치보다 살짝 위쪽으로 조정 
        _clearTopText.transform.localPosition = new Vector3(pLocalPos.x, pLocalPos.y + 20, pLocalPos.z);
        _clearTopText.gameObject.SetActive(true);
        _clearTopText.DOLocalMoveY(pLocalPos.y + 60, 2).OnComplete(OnCompleteStageClearText);

        // 사운드 
        LobbyCtrl.Instance.SoundStageClearMark();

    }

    private void OnCompleteStageClearText() {
        _clearTopText.gameObject.SetActive(false);
    }

    #endregion


    /// <summary>
    /// 스테이지의 클리어 마크 처리 
    /// </summary>
    /// <param name="pClearCount"></param>
    private void SetClearMarks(int pClearCount) {

        //Debug.Log("★ pClearCount ::" + pClearCount);

        if (pClearCount <= 0)
            return;


        //Debug.Log("★ SetClearMarks UserStageJSON :: " + GameSystem.Instance.UserStageJSON["stagelist"].ToString());

        for (int i = 0; i < pClearCount; i++) {
            // 대상 스테이지의 진척도에 따른 다른 형태의 마크를 표시한다.
            _arrStageClearObject[i].SetStageClearProgress(GameSystem.Instance.UserStageJSON["stagelist"][(ThemeID - 1) * 13 + i]["state"].AsInt);
        }

        // 13개 모두 클리어 했으면, 보스 마크 감추기 
        if (pClearCount >= 13)
            _bossPosition.gameObject.SetActive(false);

    }

    /// <summary>
    /// 스테이지 스타 카운터 세팅 
    /// </summary>
    /// <param name="pStars"></param>
    public void SetStageStarCounter(int pStars) {
        _lblStarCount.text = pStars.ToString() + "/39";
    }

    #region 스테이지 테마 마다 특수 효과 


    IEnumerator PlayingOnCenter() {

        int index = 0;

        while (true) {

            // 중심에 본인이 없으면 멈춘다. 
            if (StageMasterCtrl.Instance.StageCenterOnChild.centeredObject != this.gameObject)
                yield return new WaitForSeconds(1);


            if (ThemeID == 7) {
                #region Theme7
                _theme7Candy[index].gameObject.SetActive(true);
                _theme7Candy[index].GetComponent<UISprite>().spriteName = PuzzleConstBox.listTheme7Candy[UnityEngine.Random.Range(0, 3)];
                _theme7Candy[index].transform.position = _theme7LeftPumpHead.position;
                _theme7Candy[index].GetComponent<Rigidbody>().velocity = Vector3.zero;
                _theme7Candy[index].GetComponent<Rigidbody>().AddForce(new Vector3(UnityEngine.Random.Range(-50, 50), UnityEngine.Random.Range(80, 120), 0));

                index++;

                if (index >= _theme7Candy.Length)
                    index = 0;

                yield return new WaitForSeconds(0.1f);

                _theme7Candy[index].gameObject.SetActive(true);
                _theme7Candy[index].GetComponent<UISprite>().spriteName = PuzzleConstBox.listTheme7Candy[UnityEngine.Random.Range(0, 3)];
                _theme7Candy[index].transform.position = _theme7RightPumpHead.position;
                _theme7Candy[index].GetComponent<Rigidbody>().velocity = Vector3.zero;
                _theme7Candy[index].GetComponent<Rigidbody>().AddForce(new Vector3(UnityEngine.Random.Range(-50, 50), UnityEngine.Random.Range(80, 120), 0));

                index++;

                if (index >= _theme7Candy.Length)
                    index = 0;


                yield return new WaitForSeconds(0.1f);

                #endregion

            }
            else if (ThemeID == 8) {

                //_theme8Thunder.gameObject.SetActive(!_theme8Thunder.gameObject.activeSelf);
                _theme8Rockets[index].transform.localPosition = PuzzleConstBox.listTheme8RocketStartPos[UnityEngine.Random.Range(0, 6)];
                _theme8Rockets[index].gameObject.SetActive(true);
                _theme8Rockets[index++].GetComponent<StageRocketObjectCtrl>().OnFire();

                if (index >= _theme8Rockets.Length)
                    index = 0;


                // 로켓 소환. 
                yield return new WaitForSeconds(1);
            }


        } // end of while 

    }

    /// <summary>
    /// 완료된 테마의 연출 시작 
    /// </summary>
    private void PlayingTheme(int pTheme) {

        switch (pTheme) {
            case 1:
                break;

            case 2:

                // 상하 바운드.
                for (int i = 0; i < _arrTheme2Seats.Length; i++) {
                    _arrTheme2Seats[i].transform.DOLocalMoveY(150, 2).SetLoops(-1, LoopType.Yoyo).SetDelay(UnityEngine.Random.Range(0f, 3f));
                }

                break;

            case 3:
                #region Theme3

                // 여기는 spot 상태를 체크해서 순차적으로 동작 
                if (_arrTheme3Spot[0].activeSelf) {
                    SetPlaying3rdThemeEach(0);
                }

                if (_arrTheme3Spot[1].activeSelf) {
                    SetPlaying3rdThemeEach(1);
                }

                if (_arrTheme3Spot[2].activeSelf) {
                    SetPlaying3rdThemeEach(2);
                }

                if (_arrTheme3Spot[3].activeSelf) {
                    SetPlaying3rdThemeEach(3);
                }
                #endregion
                break;

            case 4:
                #region Theme4
                _isSwingLeft = true;
                _theme4SwingRope.DOLocalRotate(new Vector3(0, 0, -30), 1, RotateMode.Fast).OnComplete(OnCompleteTheme4Swing).SetEase(Ease.OutSine);
                _theme4SwingBase.DOLocalRotate(new Vector3(0, 0, 30), 1, RotateMode.Fast).SetEase(Ease.OutSine); ;


                for (int i = 0; i < _arrTheme4Neko.Length; i++) {
                    _arrTheme4Neko[i].transform.DOLocalRotate(new Vector3(0, 0, 30), 1, RotateMode.Fast).SetEase(Ease.OutSine); ;
                }
                #endregion
                break;

            case 5:
                #region Theme5
                _theme5Wheel1.DOLocalRotate(new Vector3(0, 0, 720), 6, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);
                _theme5Wheel2.DOLocalRotate(new Vector3(0, 0, -720), 6, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);
                _arrTheme5Neko[3].transform.localEulerAngles = new Vector3(0, 0, -15);
                _arrTheme5Neko[3].transform.DOLocalRotate(new Vector3(0, 0, 15), 1, RotateMode.Fast).SetLoops(-1, LoopType.Yoyo);


                for (int i = 0; i < 3; i++) {
                    _arrTheme5Neko[i].transform.localEulerAngles = new Vector3(0, 0, -5);
                    _arrTheme5Neko[i].transform.DOLocalRotate(new Vector3(0, 0, 5), 1).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
                }
                #endregion
                break;

            case 6:
                #region Theme6
                _theme6BottomLight.gameObject.SetActive(true);
                _theme6TopLight.gameObject.SetActive(true);

                _theme6LeftLight.gameObject.SetActive(true);
                _theme6LeftLight.GetComponent<UISpriteAnimation>().Play();
                _theme6RightLight.gameObject.SetActive(true);
                _theme6RightLight.GetComponent<UISpriteAnimation>().Play();

                _theme6Screen.gameObject.SetActive(true);
                _theme6Screen.GetComponent<UISpriteAnimation>().Play();

                _theme6SideLightLeft.gameObject.SetActive(true);
                _theme6SideLightLeft.GetComponent<UISpriteAnimation>().Play();

                _theme6SideLightRight.gameObject.SetActive(true);
                _theme6SideLightRight.GetComponent<UISpriteAnimation>().Play();

                // 점프
                _arrTheme6Neko[0].transform.DOLocalJump(_arrTheme6Neko[0].transform.localPosition, 120, 1, 0.6f).SetLoops(-1, LoopType.Restart);
                _arrTheme6Neko[1].transform.DOLocalJump(_arrTheme6Neko[1].transform.localPosition, 280, 1, 0.6f).SetLoops(-1, LoopType.Restart);
                _arrTheme6Neko[2].transform.DOLocalJump(_arrTheme6Neko[2].transform.localPosition, 280, 1, 0.6f).SetLoops(-1, LoopType.Restart);

                _arrTheme6Neko[3].transform.DOLocalJump(_arrTheme6Neko[3].transform.localPosition, 350, 1, 0.6f).SetLoops(-1, LoopType.Restart);

                //_arrTheme6Neko[3].transform.localEulerAngles = new Vector3(0, 0, 5);
                //_arrTheme6Neko[3].transform.DOLocalRotate(new Vector3(0, 0, -5), 1).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
                #endregion

                break;

            case 7:
                #region Theme7

                _theme7CenterTimer.DOLocalRotate(new Vector3(0, 0, 720), 6, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);

                // Pumping 
                _theme7LeftPumpHead.DOLocalMoveY(_theme7LeftPumpHead.localPosition.y + 10, 0.2f).SetLoops(-1, LoopType.Yoyo);
                _theme7RightPumpHead.DOLocalMoveY(_theme7LeftPumpHead.localPosition.y + 10, 0.2f).SetLoops(-1, LoopType.Yoyo);


                // Jumping 
                _arrTheme7Neko[1].transform.DOLocalJump(_arrTheme7Neko[1].transform.localPosition, 250, 1, 0.6f).SetLoops(-1, LoopType.Restart);
                _arrTheme7Neko[2].transform.DOLocalJump(_arrTheme7Neko[2].transform.localPosition, 250, 1, 0.6f).SetLoops(-1, LoopType.Restart);
                _arrTheme7Neko[3].transform.DOLocalJump(_arrTheme7Neko[3].transform.localPosition, 500, 1, 0.6f).SetLoops(-1, LoopType.Restart);

                _arrTheme7Neko[0].transform.localPosition = _themeBottomRightJumpPos;
                _arrTheme7Neko[0].transform.DOLocalJump(_themeBottomLeftJumpPos, 145, 1, 1.5f).OnComplete(() => OnCompleteBottomCenterNekoJump(_arrTheme7Neko[0]));

                StartCoroutine(PlayingOnCenter());
                #endregion
                break;

            case 8:
                #region Theme8
                // 8자 비행 
                _arrTheme8Neko[3].transform.localPosition = new Vector3(-250, 400, 0);

                Sequence theme8Seq = DOTween.Sequence();
                theme8Seq.Append(_arrTheme8Neko[3].transform.DOLocalMove(new Vector3(-280, 320, 0), 0.5f).SetEase(Ease.Linear));
                theme8Seq.Append(_arrTheme8Neko[3].transform.DOLocalMove(new Vector3(-250, 240, 0), 0.5f).SetEase(Ease.Linear));
                theme8Seq.Append(_arrTheme8Neko[3].transform.DOLocalMove(new Vector3(250, 400, 0), 1f).SetEase(Ease.Linear));
                theme8Seq.Append(_arrTheme8Neko[3].transform.DOLocalMove(new Vector3(280, 320, 0), 0.5f).SetEase(Ease.Linear));
                theme8Seq.Append(_arrTheme8Neko[3].transform.DOLocalMove(new Vector3(250, 240, 0), 0.5f).SetEase(Ease.Linear));
                theme8Seq.Append(_arrTheme8Neko[3].transform.DOLocalMove(new Vector3(-250, 400, 0), 1f).SetEase(Ease.Linear));
                theme8Seq.SetLoops(-1, LoopType.Restart);
                theme8Seq.PrependInterval(0);
                theme8Seq.Play();


                // 코루틴 실행 
                StartCoroutine(PlayingOnCenter());

                #endregion
                break;

            case 9:
                #region Theme9
                _theme9UFO.DORotate(new Vector3(0, 0, -720), 24, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
                //_objBone.DORotate(new Vector3(0, 0, 720), 24, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);

                for (int i = 0; i < _arrTheme9Neko.Length; i++) {
                    _arrTheme9Neko[i].transform.DOLocalRotate(new Vector3(0, 0, 360), 12, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
                }
                #endregion

                break;

            case 10:

                _theme10Handle.DORotate(new Vector3(0, 0, -70), 3, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);

                break;
        }
    }


    #region PlayingTheme Callback 

    bool _isSwingLeft = true;
    float _swingDelayTime = 0.5f;
    void OnCompleteTheme4Swing() {
        if (_isSwingLeft) {
            _isSwingLeft = false;
            _theme4SwingRope.DOLocalRotate(new Vector3(0, 0, 30), 2, RotateMode.Fast).OnComplete(OnCompleteTheme4Swing).SetEase(Ease.OutSine);
            _theme4SwingBase.DOLocalRotate(new Vector3(0, 0, -30), 2, RotateMode.Fast).SetEase(Ease.OutSine);


            for (int i = 0; i < _arrTheme4Neko.Length; i++) {
                _arrTheme4Neko[i].transform.DOLocalRotate(new Vector3(0, 0, -30), 2, RotateMode.Fast).SetEase(Ease.OutSine);
            }

        }
        else {
            _isSwingLeft = true;
            _theme4SwingRope.DOLocalRotate(new Vector3(0, 0, -30), 2, RotateMode.Fast).OnComplete(OnCompleteTheme4Swing).SetEase(Ease.OutSine);
            _theme4SwingBase.DOLocalRotate(new Vector3(0, 0, 30), 2, RotateMode.Fast).SetEase(Ease.OutSine);


            for (int i = 0; i < _arrTheme4Neko.Length; i++) {
                _arrTheme4Neko[i].transform.DOLocalRotate(new Vector3(0, 0, 30), 2, RotateMode.Fast).SetEase(Ease.OutSine);
            }


        }
    }


    #endregion

    #region 스테이지별 예외 사항 연출 

    void OnCompleteTheme3NekoJump() {
        if (_arrTheme3Neko[3].flip == UIBasicSprite.Flip.Nothing) {
            _arrTheme3Neko[3].flip = UIBasicSprite.Flip.Horizontally;
            _arrTheme3Neko[3].transform.DOLocalJump(_theme3RightJumpPos, 145, 1, 2).OnComplete(OnCompleteTheme3NekoJump);
        }
        else {
            _arrTheme3Neko[3].flip = UIBasicSprite.Flip.Nothing;
            _arrTheme3Neko[3].transform.DOLocalJump(_theme3LeftJumpPos, 145, 1, 2).OnComplete(OnCompleteTheme3NekoJump);
        }
    }


    void OnCompleteTheme10NekoJump() {
        if (_arrTheme10Neko[2].flip == UIBasicSprite.Flip.Nothing) {
            _arrTheme10Neko[2].flip = UIBasicSprite.Flip.Horizontally;
            _arrTheme10Neko[2].transform.DOLocalJump(_themeGroundRightJumpPos, 145, 1, 2).OnComplete(OnCompleteTheme10NekoJump);
        }
        else {
            _arrTheme10Neko[2].flip = UIBasicSprite.Flip.Nothing;
            _arrTheme10Neko[2].transform.DOLocalJump(_themeGroundLeftJumpPos, 145, 1, 2).OnComplete(OnCompleteTheme10NekoJump);
        }
    }


    void OnCompleteBottomCenterNekoJump(UISprite pNeko) {
        if (pNeko.flip == UIBasicSprite.Flip.Nothing) {
            pNeko.flip = UIBasicSprite.Flip.Horizontally;
            pNeko.transform.DOLocalJump(_themeBottomRightJumpPos, 145, 1, 2).OnComplete(() => OnCompleteBottomCenterNekoJump(pNeko));
        }
        else {
            pNeko.flip = UIBasicSprite.Flip.Nothing;
            pNeko.transform.DOLocalJump(_themeBottomLeftJumpPos, 145, 1, 2).OnComplete(() => OnCompleteBottomCenterNekoJump(pNeko));
        }
    }

    void OnCompleteNekoFlip(UISprite pNeko) {
        if (pNeko.flip == UIBasicSprite.Flip.Nothing) {
            pNeko.flip = UIBasicSprite.Flip.Horizontally;
        }
        else {
            pNeko.flip = UIBasicSprite.Flip.Nothing;
        }
    }

    #endregion

    #endregion

    #region 스테이지 진행사항에 따른 설정 처리 

    #region 첫번째 Theme 

    /// <summary>
    /// 클리어 스테이지에 따른 진척도 설정 
    /// </summary>
    /// <param name="pClearCount"></param>
    public void Set1stThemeProgress(int pClearCount) {

        _ferrisWheel.gameObject.SetActive(true);

        _arrCurrentThemeNeko = _arrWheelSeatNeko;
        _arrCurrentThemeSeat = _arrObjWheelSeats;

        // Spark Callback.
        OnSparkCallBack = OnComplete1stThemeSparkCallBack;

        // 스테이지 클리어 마크 표시 
        SetClearMarks(pClearCount);

        if (pClearCount >= 3) {
            _arrStageMiniHeads[0].gameObject.SetActive(false);
            _arrObjWheelSeats[0].GetComponent<UISprite>().spriteName = "sky-1";
            _arrWheelSeatNeko[0].gameObject.SetActive(true);
            GameSystem.Instance.SetNekoSpriteByID(_arrWheelSeatNeko[0], _arrStageMiniNekoIds[0]);
        }

        if (pClearCount >= 6) {
            _arrStageMiniHeads[1].gameObject.SetActive(false);
            _arrObjWheelSeats[1].GetComponent<UISprite>().spriteName = "sky-2";
            _arrWheelSeatNeko[1].gameObject.SetActive(true);
            GameSystem.Instance.SetNekoSpriteByID(_arrWheelSeatNeko[1], _arrStageMiniNekoIds[1]);
        }

        if (pClearCount >= 9) {
            _arrStageMiniHeads[2].gameObject.SetActive(false);
            _arrObjWheelSeats[2].GetComponent<UISprite>().spriteName = "sky-3";
            _arrWheelSeatNeko[2].gameObject.SetActive(true);
            GameSystem.Instance.SetNekoSpriteByID(_arrWheelSeatNeko[2], _arrStageMiniNekoIds[2]);
        }

        if (pClearCount >= 12) {
            _arrStageMiniHeads[3].gameObject.SetActive(false);
            _arrObjWheelSeats[3].GetComponent<UISprite>().spriteName = "sky-4"; // 좌석 변경 
            _arrWheelSeatNeko[3].gameObject.SetActive(true);
            GameSystem.Instance.SetNekoSpriteByID(_arrWheelSeatNeko[3], _arrStageMiniNekoIds[3]);
            RollingWheel();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void RollingWheel() {
        _objWheelCircle.DORotate(new Vector3(0, 0, -720), 24, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
        _objBone.DORotate(new Vector3(0, 0, 720), 24, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);

        for (int i = 0; i < _arrObjWheelSeats.Length; i++) {
            _arrObjWheelSeats[i].DOLocalRotate(new Vector3(0, 0, 360), 12, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
        }
    }

    /// <summary>
    /// 스파크 후 Callback
    /// </summary>
    /// <param name="pIndex"></param>
    private void OnComplete1stThemeSparkCallBack(int pIndex) {


        //_arrStageMiniHeads[pIndex].gameObject.SetActive(true);
        _arrStageMiniHeads[pIndex].PlayClearJump();

        // 미니 네코의 위치를 관람차 첫번째 위치로 이동(딜레이 처리)
        _arrStageMiniHeads[pIndex].transform.DOMove(_arrObjWheelSeats[pIndex].transform.position, 1.5f).OnComplete(() => OnComplete1stThemeMiniHeadMove(pIndex)).SetDelay(0.6f);
    }


    /// <summary>
    /// 구출 완료 장소로 이동 
    /// </summary>
    /// <param name="pIndex"></param>
    private void OnComplete1stThemeMiniHeadMove(int pIndex) {


        _arrStageMiniHeads[pIndex].gameObject.SetActive(false);

        // 날아간 위치에서 펑~
        _colorfulSparkLight.PlayWorldPos(_arrObjWheelSeats[pIndex].transform.position);


        // 좌석 변경 
        _arrObjWheelSeats[pIndex].GetComponent<UISprite>().spriteName = "sky-" + (pIndex + 1).ToString();

        // 네코 탑승
        _arrWheelSeatNeko[pIndex].gameObject.SetActive(true);
        GameSystem.Instance.SetNekoSpriteByID(_arrWheelSeatNeko[pIndex], _arrStageMiniNekoIds[pIndex]);


        IsProcessingThemeClearing = false; // 테마 연출 종료 



    }

    #endregion

    #region 두번째 Theme. Theme2



    /// <summary>
    /// 클리어 스테이지에 따른 진척도 설정 
    /// </summary>
    /// <param name="pClearCount"></param>
    private void Set2stThemeProgress(int pClearCount) {

        _theme2.gameObject.SetActive(true);


        _arrCurrentThemeNeko = _arrTheme2Neko;
        _arrCurrentThemeSeat = _arrTheme2Seats;

        // Spark Callback.
        OnSparkCallBack = OnComplete2stThemeSparkCallBack;


        SetClearMarks(pClearCount);
        UISprite seatSprite;

        if (pClearCount >= 3) {
            _arrStageMiniHeads[0].gameObject.SetActive(false);

            seatSprite = _arrTheme2Seats[0].GetComponent<UISprite>();
            seatSprite.spriteName = StageMasterCtrl.Instance.Theme2BoardSeatSprite;
            seatSprite.gameObject.SetActive(true);
            seatSprite.MakePixelPerfect();

            GameSystem.Instance.SetNekoSpriteByID(_arrTheme2Neko[0], _arrStageMiniNekoIds[0]);
        }

        if (pClearCount >= 6) {
            _arrStageMiniHeads[1].gameObject.SetActive(false);

            seatSprite = _arrTheme2Seats[1].GetComponent<UISprite>();
            seatSprite.spriteName = StageMasterCtrl.Instance.Theme2BoardSeatSprite;
            seatSprite.gameObject.SetActive(true);
            seatSprite.MakePixelPerfect();


            GameSystem.Instance.SetNekoSpriteByID(_arrTheme2Neko[1], _arrStageMiniNekoIds[1]);
        }

        if (pClearCount >= 9) {
            _arrStageMiniHeads[2].gameObject.SetActive(false);

            seatSprite = _arrTheme2Seats[2].GetComponent<UISprite>();
            seatSprite.spriteName = StageMasterCtrl.Instance.Theme2BoardSeatSprite;
            seatSprite.gameObject.SetActive(true);
            seatSprite.MakePixelPerfect();

            GameSystem.Instance.SetNekoSpriteByID(_arrTheme2Neko[2], _arrStageMiniNekoIds[2]);
        }

        if (pClearCount >= 12) {
            _arrStageMiniHeads[3].gameObject.SetActive(false);

            seatSprite = _arrTheme2Seats[3].GetComponent<UISprite>();
            seatSprite.spriteName = StageMasterCtrl.Instance.Theme2BoardSeatSprite;
            seatSprite.gameObject.SetActive(true);
            seatSprite.MakePixelPerfect();

            GameSystem.Instance.SetNekoSpriteByID(_arrTheme2Neko[3], _arrStageMiniNekoIds[3]);
            PlayingTheme(ThemeID);
        }
    }


    /// <summary>
    /// 첫번째 테마 특수 효과 
    /// </summary>
    /// <param name="pIndex"></param>
    public void Set2stThemeClearEffect(int pIndex) {

        IsProcessingThemeClearing = true;

        Debug.Log("★★ Set2stThemeClearEffect :: " + pIndex);

        _sparkLight.PlayWorldPos(_arrStageMiniHeads[pIndex].transform.position, OnComplete2stThemeSparkCallBack, pIndex);
        _arrStageMiniHeads[pIndex].gameObject.SetActive(false);


    }

    /// <summary>
    /// 스파크 후 Callback
    /// </summary>
    /// <param name="pIndex"></param>
    private void OnComplete2stThemeSparkCallBack(int pIndex) {


        //_arrStageMiniHeads[pIndex].gameObject.SetActive(true);
        _arrStageMiniHeads[pIndex].PlayClearJump();

        // 미니 네코의 위치를 관람차 첫번째 위치로 이동(딜레이 처리)
        _arrStageMiniHeads[pIndex].transform.DOMove(_arrTheme2Seats[pIndex].transform.position, 1.5f).OnComplete(() => OnComplete2stThemeMiniHeadMove(pIndex)).SetDelay(0.6f);
    }


    /// <summary>
    /// 구출 완료 장소로 이동 
    /// </summary>
    /// <param name="pIndex"></param>
    private void OnComplete2stThemeMiniHeadMove(int pIndex) {


        _arrStageMiniHeads[pIndex].gameObject.SetActive(false);

        // 날아간 위치에서 펑~
        _colorfulSparkLight.PlayWorldPos(_arrTheme2Seats[pIndex].transform.position);


        // 좌석 변경 
        _arrTheme2Seats[pIndex].GetComponent<UISprite>().spriteName = StageMasterCtrl.Instance.Theme2BoardSeatSprite;
        _arrTheme2Seats[pIndex].GetComponent<UISprite>().MakePixelPerfect();

        // 네코 탑승
        _arrTheme2Seats[pIndex].gameObject.SetActive(true);
        GameSystem.Instance.SetNekoSpriteByID(_arrTheme2Neko[pIndex], _arrStageMiniNekoIds[pIndex]);


        IsProcessingThemeClearing = false; // 테마 연출 종료 
    }

    #endregion

    #region Theme3


    /// <summary>
    /// 각 스팟이 활성화 될때, 추가 처리 
    /// </summary>
    /// <param name="pIndex"></param>
    void SetPlaying3rdThemeEach(int pIndex) {
        switch (pIndex) {
            case 0:
                _theme3LeftSmallLight.DOLocalRotate(new Vector3(0, 0, -10), 2).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
                _theme3RightSmallLight.DOLocalRotate(new Vector3(0, 0, 10), 2).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);

                _arrTheme3Neko[0].transform.DOLocalMoveY(440, 2).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
                break;

            case 1:

                _arrTheme3Neko[1].transform.localEulerAngles = new Vector3(0, 0, -5);
                _arrTheme3Neko[1].transform.DOLocalRotate(new Vector3(0, 0, 5), 2).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);

                _theme3LeftBigLight.DOLocalRotate(new Vector3(0, 0, -10), 2).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
                _theme3LeftSpot.Play();
                break;
            case 2:

                _arrTheme3Neko[2].transform.localEulerAngles = new Vector3(0, 0, -5);
                _arrTheme3Neko[2].transform.DOLocalRotate(new Vector3(0, 0, 5), 2).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);

                _theme3RightBigLight.DOLocalRotate(new Vector3(0, 0, 10), 2).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
                _theme3RightSpot.Play();
                break;
            case 3:
                _theme3CenterSpot.Play();
                _arrTheme3Neko[3].transform.localPosition = _theme3RightJumpPos;

                //Debug.Log("★★★★★ Jumping!");
                _arrTheme3Neko[3].transform.DOLocalJump(_theme3LeftJumpPos, 145, 1, 1.5f).OnComplete(OnCompleteTheme3NekoJump);
                break;
        }
    }



    void Set3rdThemeProgressDetail(int pClearCount) {
        if (pClearCount >= 3) {
            _arrStageMiniHeads[0].gameObject.SetActive(false);
            _arrTheme3Spot[0].SetActive(true);
            GameSystem.Instance.SetNekoSpriteByID(_arrTheme3Neko[0], _arrStageMiniNekoIds[0]);
        }

        if (pClearCount >= 6) {
            _arrStageMiniHeads[1].gameObject.SetActive(false);

            _arrTheme3Spot[1].SetActive(true);
            GameSystem.Instance.SetNekoSpriteByID(_arrTheme3Neko[1], _arrStageMiniNekoIds[1]);
        }

        if (pClearCount >= 9) {
            _arrStageMiniHeads[2].gameObject.SetActive(false);

            _arrTheme3Spot[2].SetActive(true);

            GameSystem.Instance.SetNekoSpriteByID(_arrTheme3Neko[2], _arrStageMiniNekoIds[2]);
        }

        if (pClearCount >= 12) {
            _arrStageMiniHeads[3].gameObject.SetActive(false);

            _arrTheme3Spot[3].SetActive(true);

            GameSystem.Instance.SetNekoSpriteByID(_arrTheme3Neko[3], _arrStageMiniNekoIds[3]);

        }

        PlayingTheme(ThemeID);
    }


    public void Set3rdThemeProgress(int pClearCount) {

        _theme3.gameObject.SetActive(true);


        _arrCurrentThemeNeko = _arrTheme3Neko;
        _arrCurrentThemeSeat = new Transform[_arrTheme3Neko.Length];

        for (int i = 0; i < _arrCurrentThemeSeat.Length; i++) {
            _arrCurrentThemeSeat[i] = _arrTheme3Neko[i].transform;
        }

        // Spark Callback.
        OnSparkCallBack = OnCompleteCurrentThemeSparkCallBack;

        for (int i = 0; i < _arrTheme3Spot.Length; i++) {
            _arrTheme3Spot[i].SetActive(true);
        }

        // Find 
        _theme3LeftBigLight = GameObject.Find("Theme3LeftBigLight").transform;
        _theme3RightBigLight = GameObject.Find("Theme3RightBigLight").transform;
        _theme3LeftSmallLight = GameObject.Find("Theme3LeftSmallLightSpot").transform;
        _theme3RightSmallLight = GameObject.Find("Theme3RightSmallLightSpot").transform;

        _theme3CenterSpot = GameObject.Find("Theme3Center").GetComponent<UISpriteAnimation>();
        _theme3LeftSpot = GameObject.Find("Theme3Left").GetComponent<UISpriteAnimation>();
        _theme3RightSpot = GameObject.Find("Theme3Right").GetComponent<UISpriteAnimation>();

        // Find 하고 다시 감추기
        for (int i = 0; i < _arrTheme3Spot.Length; i++) {
            _arrTheme3Spot[i].SetActive(false);
        }


        SetClearMarks(pClearCount);
        Set3rdThemeProgressDetail(pClearCount);

    }

    #endregion

    #region Theme4

    /// <summary>
    /// 4번째 테마 진척도 설정 
    /// </summary>
    /// <param name="pClearCount"></param>
    void Set4thThemeProgress(int pClearCount) {
        _theme4.SetActive(true);


        _arrCurrentThemeNeko = _arrTheme4Neko;
        _arrCurrentThemeSeat = new Transform[_arrTheme4Spot.Length];

        for (int i = 0; i < _arrCurrentThemeSeat.Length; i++) {
            _arrCurrentThemeSeat[i] = _arrTheme4Spot[i].transform;
        }


        OnSparkCallBack = OnCompleteCurrentThemeSparkCallBack;

        for (int i = 0; i < _arrTheme4Spot.Length; i++) {
            _arrTheme4Spot[i].SetActive(true);
        }

        _theme4SwingBase = GameObject.Find("Theme4SwingBase").transform;
        _theme4SwingRope = GameObject.Find("Theme4SwingRope").transform;

        SetClearMarks(pClearCount);
        Set4thThemeProgressDetail(pClearCount);
    }


    /// <summary>
    /// 4번째 테마 디테일 설정 
    /// </summary>
    /// <param name="pClearCount"></param>
    void Set4thThemeProgressDetail(int pClearCount) {

        if (pClearCount >= 3) {
            _arrStageMiniHeads[0].gameObject.SetActive(false);
            GameSystem.Instance.SetNekoSpriteByID(_arrCurrentThemeNeko[0], _arrStageMiniNekoIds[0]);
        }

        if (pClearCount >= 6) {
            _arrStageMiniHeads[1].gameObject.SetActive(false);
            GameSystem.Instance.SetNekoSpriteByID(_arrCurrentThemeNeko[1], _arrStageMiniNekoIds[1]);
        }

        if (pClearCount >= 9) {
            _arrStageMiniHeads[2].gameObject.SetActive(false);
            GameSystem.Instance.SetNekoSpriteByID(_arrCurrentThemeNeko[2], _arrStageMiniNekoIds[2]);
        }

        if (pClearCount >= 12) {
            _arrStageMiniHeads[3].gameObject.SetActive(false);
            GameSystem.Instance.SetNekoSpriteByID(_arrCurrentThemeNeko[3], _arrStageMiniNekoIds[3]);
            PlayingTheme(ThemeID);
        }

    }

    #endregion

    #region Theme5

    void Set5thThemeProgress(int pClearCount) {
        _theme5.SetActive(true);

        _arrCurrentThemeNeko = _arrTheme5Neko;
        _arrCurrentThemeSeat = new Transform[_arrTheme5Spot.Length];

        for (int i = 0; i < _arrCurrentThemeSeat.Length; i++) {
            _arrCurrentThemeSeat[i] = _arrTheme5Spot[i].transform;
        }


        OnSparkCallBack = OnCompleteCurrentThemeSparkCallBack;


        for (int i = 0; i < _arrTheme5Spot.Length; i++) {
            _arrTheme5Spot[i].SetActive(true);
        }

        // Theme 5 오브젝트 Find 
        _theme5Wheel1 = GameObject.Find("Theme5Wheel1").transform;
        _theme5Wheel2 = GameObject.Find("Theme5Wheel2").transform;

        SetClearMarks(pClearCount);
        Set5ThemeProgressDetail(pClearCount);

    }

    void Set5ThemeProgressDetail(int pClearCount) {
        if (pClearCount >= 3) {
            _arrStageMiniHeads[0].gameObject.SetActive(false);
            GameSystem.Instance.SetNekoSpriteByID(_arrCurrentThemeNeko[0], _arrStageMiniNekoIds[0]);
        }

        if (pClearCount >= 6) {
            _arrStageMiniHeads[1].gameObject.SetActive(false);
            GameSystem.Instance.SetNekoSpriteByID(_arrCurrentThemeNeko[1], _arrStageMiniNekoIds[1]);
        }

        if (pClearCount >= 9) {
            _arrStageMiniHeads[2].gameObject.SetActive(false);
            GameSystem.Instance.SetNekoSpriteByID(_arrCurrentThemeNeko[2], _arrStageMiniNekoIds[2]);
        }

        if (pClearCount >= 12) {
            _arrStageMiniHeads[3].gameObject.SetActive(false);
            GameSystem.Instance.SetNekoSpriteByID(_arrCurrentThemeNeko[3], _arrStageMiniNekoIds[3]);
            PlayingTheme(ThemeID);
        }
    }


    #endregion

    #region Theme 6


    void Set6thThemeProgress(int pClearCount) {
        _theme6.SetActive(true);

        _arrCurrentThemeNeko = _arrTheme6Neko;
        _arrCurrentThemeSeat = new Transform[_arrTheme6Spot.Length];

        for (int i = 0; i < _arrCurrentThemeSeat.Length; i++) {
            _arrCurrentThemeSeat[i] = _arrTheme6Spot[i].transform;
        }


        OnSparkCallBack = OnCompleteCurrentThemeSparkCallBack;


        for (int i = 0; i < _arrTheme6Spot.Length; i++) {
            _arrTheme6Spot[i].SetActive(true);
        }

        // Theme 6 오브젝트 Find 
        _theme6BottomLight = GameObject.Find("Theme6BottomLight").transform;
        _theme6TopLight = GameObject.Find("Theme6TopLight").transform;
        _theme6SideLightLeft = GameObject.Find("ThemeSideLightLeft").transform;
        _theme6SideLightRight = GameObject.Find("ThemeSideLightRight").transform;
        _theme6Screen = GameObject.Find("Theme6Screen").transform;
        _theme6LeftLight = GameObject.Find("Theme6LeftLight").transform;
        _theme6RightLight = GameObject.Find("Theme6RightLight").transform;


        _theme6BottomLight.gameObject.SetActive(false);
        _theme6TopLight.gameObject.SetActive(false);
        _theme6SideLightLeft.gameObject.SetActive(false);
        _theme6SideLightRight.gameObject.SetActive(false);
        _theme6Screen.gameObject.SetActive(false);
        _theme6LeftLight.gameObject.SetActive(false);
        _theme6RightLight.gameObject.SetActive(false);

        SetClearMarks(pClearCount);
        Set6ThemeProgressDetail(pClearCount);

    }

    void Set6ThemeProgressDetail(int pClearCount) {
        if (pClearCount >= 3) {
            _arrStageMiniHeads[0].gameObject.SetActive(false);
            GameSystem.Instance.SetNekoSpriteByID(_arrCurrentThemeNeko[0], _arrStageMiniNekoIds[0]);
        }

        if (pClearCount >= 6) {
            _arrStageMiniHeads[1].gameObject.SetActive(false);
            GameSystem.Instance.SetNekoSpriteByID(_arrCurrentThemeNeko[1], _arrStageMiniNekoIds[1]);
        }

        if (pClearCount >= 9) {
            _arrStageMiniHeads[2].gameObject.SetActive(false);
            GameSystem.Instance.SetNekoSpriteByID(_arrCurrentThemeNeko[2], _arrStageMiniNekoIds[2]);
        }

        if (pClearCount >= 12) {
            _arrStageMiniHeads[3].gameObject.SetActive(false);
            GameSystem.Instance.SetNekoSpriteByID(_arrCurrentThemeNeko[3], _arrStageMiniNekoIds[3]);
            PlayingTheme(ThemeID);
        }
    }


    #endregion

    #region Theme 7

    void Set7thThemeProgress(int pClearCount) {
        _theme7.SetActive(true);

        _arrCurrentThemeNeko = _arrTheme7Neko;
        _arrCurrentThemeSeat = new Transform[_arrTheme7Neko.Length];

        for (int i = 0; i < _arrCurrentThemeSeat.Length; i++) {
            _arrCurrentThemeSeat[i] = _arrTheme7Neko[i].transform;
        }


        OnSparkCallBack = OnCompleteCurrentThemeSparkCallBack;


        for (int i = 0; i < _arrTheme7Neko.Length; i++) {
            _arrTheme7Neko[i].gameObject.SetActive(true);
        }

        // Theme 7 오브젝트 Find 
        _theme7Center = GameObject.Find("Theme7Center").transform;
        _theme7CenterTimer = GameObject.Find("Theme7CenterTimer").transform;
        _theme7LeftPump = GameObject.Find("Theme7LeftPump").transform;
        _theme7LeftPumpHead = GameObject.Find("Theme7LeftPumpHead").transform;
        _theme7RightPump = GameObject.Find("Theme7RightPump").transform;
        _theme7RightPumpHead = GameObject.Find("Theme7RightPumpHead").transform;

        _theme7ChainWheels[0] = GameObject.Find("Theme7ChainWheel1").transform;
        _theme7ChainWheels[1] = GameObject.Find("Theme7ChainWheel2").transform;
        _theme7ChainWheels[2] = GameObject.Find("Theme7ChainWheel3").transform;
        _theme7ChainWheels[3] = GameObject.Find("Theme7ChainWheel4").transform;
        _theme7ChainWheels[4] = GameObject.Find("Theme7ChainWheel5").transform;
        _theme7ChainWheels[5] = GameObject.Find("Theme7ChainWheel6").transform;
        _theme7ChainWheels[6] = GameObject.Find("Theme7ChainWheel7").transform;
        _theme7ChainWheels[7] = GameObject.Find("Theme7ChainWheel8").transform;
        _theme7ChainWheels[8] = GameObject.Find("Theme7ChainWheel9").transform;
        _theme7ChainWheels[9] = GameObject.Find("Theme7ChainWheel10").transform;

        // 캔디들. 
        _theme7Candy = GameObject.FindGameObjectsWithTag("Theme7Candy");

        for (int i = 0; i < _theme7Candy.Length; i++) {
            _theme7Candy[i].SetActive(false);
        }


        _theme7Center.gameObject.SetActive(false);

        _theme7LeftPump.gameObject.SetActive(false);

        _theme7RightPump.gameObject.SetActive(false);



        // 바퀴 회전 
        for (int i = 0; i < _theme7ChainWheels.Length; i++) {
            //_theme7ChainWheels[i].gameObject.SetActive(false);
        }




        SetClearMarks(pClearCount);
        Set7ThemeProgressDetail(pClearCount);

    }

    void Set7ThemeProgressDetail(int pClearCount) {
        if (pClearCount >= 3) {
            _arrStageMiniHeads[0].gameObject.SetActive(false);
            GameSystem.Instance.SetNekoSpriteByID(_arrCurrentThemeNeko[0], _arrStageMiniNekoIds[0]);
        }

        if (pClearCount >= 6) {
            _arrStageMiniHeads[1].gameObject.SetActive(false);
            GameSystem.Instance.SetNekoSpriteByID(_arrCurrentThemeNeko[1], _arrStageMiniNekoIds[1]);
            _theme7LeftPump.gameObject.SetActive(true);
        }

        if (pClearCount >= 9) {
            _arrStageMiniHeads[2].gameObject.SetActive(false);
            GameSystem.Instance.SetNekoSpriteByID(_arrCurrentThemeNeko[2], _arrStageMiniNekoIds[2]);
            _theme7RightPump.gameObject.SetActive(true);
        }

        if (pClearCount >= 12) {
            _arrStageMiniHeads[3].gameObject.SetActive(false);
            GameSystem.Instance.SetNekoSpriteByID(_arrCurrentThemeNeko[3], _arrStageMiniNekoIds[3]);
            _theme7Center.gameObject.SetActive(true);
            PlayingTheme(ThemeID);
        }
    }

    #endregion

    #region Theme 8

    void Set8thThemeProgress(int pClearCount) {
        _theme8.SetActive(true);

        _arrCurrentThemeNeko = _arrTheme8Neko;
        _arrCurrentThemeSeat = new Transform[_arrTheme8Neko.Length];

        for (int i = 0; i < _arrCurrentThemeSeat.Length; i++) {
            _arrCurrentThemeSeat[i] = _arrTheme8Neko[i].transform;
        }


        for (int i = 0; i < _arrTheme8Neko.Length; i++) {
            _arrTheme8Neko[i].gameObject.SetActive(true);
        }

        OnSparkCallBack = OnCompleteCurrentThemeSparkCallBack;


        // Theme 8 오브젝트 Find 

        _theme8CenterLight = GameObject.Find("Theme8CenterLight").transform;
        _theme8LeftLight = GameObject.Find("Theme8LeftLight").transform;
        _theme8RightLight = GameObject.Find("Theme8RightLight").transform;
        _theme8Thunder = GameObject.Find("Theme8Thunder").transform;

        // 로켓
        _theme8Rockets = GameObject.FindGameObjectsWithTag("Theme8Rocket");

        for (int i = 0; i < _theme8Rockets.Length; i++) {
            _theme8Rockets[i].SetActive(false);
        }


        _theme8CenterLight.gameObject.SetActive(false);
        _theme8LeftLight.gameObject.SetActive(false);
        _theme8RightLight.gameObject.SetActive(false);

        _theme8Thunder.GetComponent<StageThunderObjectCtrl>().OnThunder();

        SetClearMarks(pClearCount);
        Set8ThemeProgressDetail(pClearCount);

    }

    void Set8ThemeProgressDetail(int pClearCount) {

        if (pClearCount >= 3) {
            _arrStageMiniHeads[0].gameObject.SetActive(false);
            GameSystem.Instance.SetNekoSpriteByID(_arrCurrentThemeNeko[0], _arrStageMiniNekoIds[0]);

            _theme8LeftLight.gameObject.SetActive(true);
            _theme8LeftLight.GetComponent<UISpriteAnimation>().Play();

            // 고양이 왔다갔다 처리 
            _arrTheme8Neko[0].flip = UIBasicSprite.Flip.Horizontally;
            _arrTheme8Neko[0].transform.localPosition = new Vector3(-320, 70, 0);
            _arrTheme8Neko[0].transform.DOLocalMoveX(-40, UnityEngine.Random.Range(2, 3)).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo).OnStepComplete(() => OnCompleteNekoFlip(_arrTheme8Neko[0])); ;
        }

        if (pClearCount >= 6) {
            _arrStageMiniHeads[1].gameObject.SetActive(false);
            GameSystem.Instance.SetNekoSpriteByID(_arrCurrentThemeNeko[1], _arrStageMiniNekoIds[1]);

            _theme8RightLight.gameObject.SetActive(true);
            _theme8RightLight.GetComponent<UISpriteAnimation>().Play();

            // 고양이 왔다갔다 처리 
            _arrTheme8Neko[1].transform.localPosition = new Vector3(320, 70, 0);
            _arrTheme8Neko[1].transform.DOLocalMoveX(40, UnityEngine.Random.Range(2, 3)).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo).OnStepComplete(() => OnCompleteNekoFlip(_arrTheme8Neko[1]));

        }

        if (pClearCount >= 9) {
            _arrStageMiniHeads[2].gameObject.SetActive(false);
            GameSystem.Instance.SetNekoSpriteByID(_arrCurrentThemeNeko[2], _arrStageMiniNekoIds[2]);

            _theme8CenterLight.gameObject.SetActive(true);
            _theme8CenterLight.GetComponent<UISpriteAnimation>().Play();

            _arrTheme8Neko[2].flip = UIBasicSprite.Flip.Horizontally;
            _arrTheme8Neko[2].transform.localPosition = new Vector3(-100, 130, 0);
            _arrTheme8Neko[2].transform.DOLocalMoveX(100, UnityEngine.Random.Range(1.5f, 3)).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo).OnStepComplete(() => OnCompleteNekoFlip(_arrTheme8Neko[2]));
        }

        if (pClearCount >= 12) {
            _arrStageMiniHeads[3].gameObject.SetActive(false);
            GameSystem.Instance.SetNekoSpriteByID(_arrCurrentThemeNeko[3], _arrStageMiniNekoIds[3]);

            PlayingTheme(ThemeID);
        }
    }

    #endregion

    #region Theme 9

    void Set9thThemeProgress(int pClearCount) {
        _theme9.SetActive(true);

        _arrCurrentThemeNeko = _arrTheme9Neko;
        _arrCurrentThemeSeat = new Transform[_arrTheme9Neko.Length];

        for (int i = 0; i < _arrCurrentThemeSeat.Length; i++) {
            _arrCurrentThemeSeat[i] = _arrTheme9Neko[i].transform;
        }


        for (int i = 0; i < _arrTheme9Neko.Length; i++) {
            _arrTheme9Neko[i].gameObject.SetActive(true);
        }

        OnSparkCallBack = OnCompleteCurrentThemeSparkCallBack;


        // Theme 8 오브젝트 Find 
        _theme9UFO = GameObject.Find("Theme9UFO").transform;
        _arrTheme9NekoCap = new GameObject[4];

        _arrTheme9NekoCap[0] = GameObject.Find("Theme9Neko1Cap");
        _arrTheme9NekoCap[1] = GameObject.Find("Theme9Neko2Cap");
        _arrTheme9NekoCap[2] = GameObject.Find("Theme9Neko3Cap");
        _arrTheme9NekoCap[3] = GameObject.Find("Theme9Neko4Cap");


        for (int i = 0; i < _arrTheme9NekoCap.Length; i++) {
            _arrTheme9NekoCap[i].SetActive(false);
        }



        SetClearMarks(pClearCount);
        Set9ThemeProgressDetail(pClearCount);

    }


    void Set9ThemeProgressDetail(int pClearCount) {

        if (pClearCount >= 3) {
            _arrStageMiniHeads[0].gameObject.SetActive(false);
            GameSystem.Instance.SetNekoSpriteByID(_arrCurrentThemeNeko[0], _arrStageMiniNekoIds[0]);

            _arrTheme9NekoCap[0].SetActive(true);
            _arrTheme9NekoCap[0].GetComponent<UISpriteAnimation>().Play();

        }

        if (pClearCount >= 6) {
            _arrStageMiniHeads[1].gameObject.SetActive(false);
            GameSystem.Instance.SetNekoSpriteByID(_arrCurrentThemeNeko[1], _arrStageMiniNekoIds[1]);

            _arrTheme9NekoCap[1].SetActive(true);
            _arrTheme9NekoCap[1].GetComponent<UISpriteAnimation>().Play();
        }

        if (pClearCount >= 9) {
            _arrStageMiniHeads[2].gameObject.SetActive(false);
            GameSystem.Instance.SetNekoSpriteByID(_arrCurrentThemeNeko[2], _arrStageMiniNekoIds[2]);

            _arrTheme9NekoCap[2].SetActive(true);
            _arrTheme9NekoCap[2].GetComponent<UISpriteAnimation>().Play();

        }

        if (pClearCount >= 12) {
            _arrStageMiniHeads[3].gameObject.SetActive(false);
            GameSystem.Instance.SetNekoSpriteByID(_arrCurrentThemeNeko[3], _arrStageMiniNekoIds[3]);


            _arrTheme9NekoCap[3].SetActive(true);
            _arrTheme9NekoCap[3].GetComponent<UISpriteAnimation>().Play();

            PlayingTheme(ThemeID);
        }
    }
    #endregion

    #region Theme 10

    void Set10thThemeProgress(int pClearCount) {
        _theme10.SetActive(true);

        _arrCurrentThemeNeko = _arrTheme10Neko;
        _arrCurrentThemeSeat = new Transform[_arrTheme10Neko.Length];

        for (int i = 0; i < _arrCurrentThemeSeat.Length; i++) {
            _arrCurrentThemeSeat[i] = _arrTheme10Neko[i].transform;
        }


        for (int i = 0; i < _arrTheme10Neko.Length; i++) {
            _arrTheme10Neko[i].gameObject.SetActive(true);
        }

        OnSparkCallBack = OnCompleteCurrentThemeSparkCallBack;


        // Theme 10 오브젝트 Find 
        _theme10Handle = GameObject.Find("Theme10Handle").transform;

        SetClearMarks(pClearCount);
        Set10ThemeProgressDetail(pClearCount);

    }


    void Set10ThemeProgressDetail(int pClearCount) {

        if (pClearCount >= 3) {
            _arrStageMiniHeads[0].gameObject.SetActive(false);
            GameSystem.Instance.SetNekoSpriteByID(_arrCurrentThemeNeko[0], _arrStageMiniNekoIds[0]);

            _arrTheme10Neko[0].transform.DOLocalMoveY(200, UnityEngine.Random.Range(2.0f, 3.0f)).SetLoops(-1, LoopType.Yoyo);
        }

        if (pClearCount >= 6) {
            _arrStageMiniHeads[1].gameObject.SetActive(false);
            GameSystem.Instance.SetNekoSpriteByID(_arrCurrentThemeNeko[1], _arrStageMiniNekoIds[1]);

            _arrTheme10Neko[1].transform.DOLocalMoveY(200, UnityEngine.Random.Range(2.0f, 3.0f)).SetLoops(-1, LoopType.Yoyo);
        }

        if (pClearCount >= 9) {
            _arrStageMiniHeads[2].gameObject.SetActive(false);
            GameSystem.Instance.SetNekoSpriteByID(_arrCurrentThemeNeko[2], _arrStageMiniNekoIds[2]);

            _arrTheme10Neko[2].flip = UIBasicSprite.Flip.Horizontally;
            _arrTheme10Neko[2].transform.localPosition = _themeGroundLeftJumpPos;
            _arrTheme10Neko[2].transform.DOLocalJump(_themeGroundRightJumpPos, 145, 1, 1.5f).OnComplete(OnCompleteTheme10NekoJump);
        }

        if (pClearCount >= 12) {
            _arrStageMiniHeads[3].gameObject.SetActive(false);
            GameSystem.Instance.SetNekoSpriteByID(_arrCurrentThemeNeko[3], _arrStageMiniNekoIds[3]);
            PlayingTheme(ThemeID);
        }
    }

    #endregion 

    #endregion
}
