using UnityEngine;
using System.Collections;
using DG.Tweening;
using SimpleJSON;

public class NekoGatchaCtrl : MonoBehaviour {

    [SerializeField] Camera mainCamera;
    private Ray ray;
    [SerializeField] private RaycastHit hit;
    [SerializeField] private LayerMask mask;
    
    [SerializeField] bool _inputLock = false;
    
    [SerializeField] SNSNekoCtrl _snsUpload;


    [SerializeField] GameObject _machine;
    [SerializeField] GameObject _ticketBG;

    [SerializeField] UIButton _touchArea;
    [SerializeField] UIButton _touchArea2;

    [SerializeField] Transform _tweezer; 
    [SerializeField] Transform _joyStick;

    [SerializeField] tk2dSpriteAnimator _tweezerLight; // 집게 라이트 애니메이션 
    [SerializeField] tk2dSpriteAnimator _tweezerAnimator;  // 집게 자체의 애니메이션 
    [SerializeField] private tk2dSprite _fadeScreen;  // Fade Screen

    [SerializeField] tk2dSpriteAnimator[] _leftLights;
    [SerializeField] tk2dSpriteAnimator[] _rightLights;

    [SerializeField] tk2dSpriteAnimator _number1; 
    [SerializeField] tk2dSpriteAnimator _number2;
    [SerializeField] tk2dSpriteAnimator _pushBtn;

    [SerializeField] UILabel _lblNekoName;
    [SerializeField] UILabel _lblNekoDetail;

    [SerializeField] UIButton _btnFB;
    [SerializeField] UIButton _btnTW;
    [SerializeField] UIButton _btnConfirm;
    


    [SerializeField] GatchaNekoCtrl _gatchaNeko; // 뽑은 고양이.
    [SerializeField] GameObject _nekoBall; // 고양이 묶음

    [SerializeField] int _gatchNekoID;
	[SerializeField] int _gatchaNekoLevel;
	[SerializeField] int _gatchaNekoStar;
    [SerializeField] int _preGatchaNekoStar; // 동일 고양이의 이전 등급 
    [SerializeField] string _gatchaNekoName; // 뽑기로 뽑은 고양이 이름 
    [SerializeField] bool _newMainNeko = false; // 신규 네코여부 


    [SerializeField] FishType _fishType;
    [SerializeField] int _fishValue;


    // 결과창 객체 
    [SerializeField] GameObject _resultUI;
    [SerializeField] Transform _resultCircleFX;
    
    

    [SerializeField] UISprite _resultNekoSprite;
    [SerializeField] UILabel _lblExtraInfo;
    

    [SerializeField] Transform[] arrResultStars; // 등급 별
    private Vector3[] arrGradeStarPos = new Vector3[] { new Vector3(-120, 0, 0), new Vector3(-60f, 0, 0), new Vector3(0, 0, 0), new Vector3(60, 0, 0), new Vector3(120, 0, 0) };

    
    

    // 등급 Star 위치 
    Vector3[] _oneStarPos = new Vector3[] { Vector3.zero };
    Vector3[] _twoStarPos = new Vector3[] { new Vector3(-50, 0, 0), new Vector3(50, 0, 0) };
    Vector3[] _threeStarPos = new Vector3[] { new Vector3(-100, 0, 0), new Vector3(0, 0, 0), new Vector3(100, 0, 0) };
    Vector3[] _fourStarPos = new Vector3[] { new Vector3(-150, 0, 0), new Vector3(-50, 0, 0), new Vector3(50, 0, 0), new Vector3(150, 0, 0) };
    Vector3[] _fiveStarPos = new Vector3[] { new Vector3(-200, 0, 0), new Vector3(-100, 0, 0), new Vector3(0, 0, 0), new Vector3(100, 0, 0), new Vector3(200, 0, 0) };




    [SerializeField] YellowLightCtrl[] _arrYellowLights;
    [SerializeField] WhiteLightCtrl[] _arrWhiteLights;


    [SerializeField] UISprite[] _arrMachineTenNeko;
    [SerializeField] GNekoTenCtrl[] _arrResultTenNeko;
    [SerializeField] Transform _tenFirstRow;
    [SerializeField] Transform _tenSecondRow;


    
    


    [SerializeField] private bool _isPicking = false; // 집는 행위중인 경우 
    private bool _isFusion = false; // 퓨전인지 단순 획득인지 체크 
    private bool _isShowReulst = false; //결과창 여부 
    private bool _isSinglePick = false; // 1회 뽑기 여부 


    // Moving
    private bool isLeftMoving;
	[SerializeField] Vector2 startPos, deltaPos, previousDeltaPos, nowPos;
	[SerializeField] bool isDragged;
	[SerializeField] float dragAccuracy = 50f;
	[SerializeField] float stickSpeed;
	[SerializeField] float tweezerSpeed;

    readonly string _clipTweezerLight = "TweezerLight";

    // ClipStickFloor,
    readonly string _layer = "Default";

    bool _isTicketGacha = false;
    JSONNode _ticketNode;

    #region 사운드 클립 

    //Sound
    [SerializeField] AudioSource _gatchaAudioSrc;
    [SerializeField] AudioSource _stickAudioSrc;
	[SerializeField] AudioClip _clipTweezerUpDown;
	[SerializeField] AudioClip _clipTweezerLeftRight;
	[SerializeField] AudioClip _clipButton;
	[SerializeField] AudioClip _clipTweezerGrab;
	[SerializeField] AudioClip _clipPickNewNeko;
	[SerializeField] AudioClip _clipGatchaResult;
    [SerializeField] AudioClip _clipPunchTweezer;


    public bool InputLock {
        get {
            return _inputLock;
        }

        set {
            _inputLock = value;
        }
    }

    public bool IsTicketGacha {
        get {
            return _isTicketGacha;
        }

        set {
            _isTicketGacha = value;
        }
    }

    public JSONNode TicketNode {
        get {
            return _ticketNode;
        }

        set {
            _ticketNode = value;
        }
    }

    #endregion


    void Start() {
        //mainCamera = NGUITools.FindCameraForLayer(this.gameObject.layer);
        _tweezerAnimator.AnimationCompleted = AnimationCompleteDelegate;



        //InitializeGatcha();
    }

    

    /// <summary>
    /// 가챠 시작 (초기화)
    /// </summary>
    public void InitializeGatcha(bool pSinglePick) {

        this._machine.SetActive(true);
        _ticketBG.SetActive(false);



        _touchArea.gameObject.SetActive(false);
        _touchArea2.gameObject.SetActive(false);

        // Row 초기화 
        _tenFirstRow.localPosition = new Vector3(800, 0, 0);
        _tenSecondRow.localPosition = new Vector3(-800, -135, 0);


        _number1.gameObject.SetActive(true); // 1반짝임 
        _number1.Play();

        _number2.gameObject.SetActive(false);


        // 기계 불빛 효과 추가 
        TurnOnMachineLight();

        _isPicking = false;
        _isShowReulst = false;
        _isSinglePick = pSinglePick;

        InitFadeScreen();

        OnMovingFx(false);

        _tweezer.localPosition = new Vector3(0, 3.7f, 0); // 집게발 위치 초기화

        IsTicketGacha = false;
        SetGatchaNeko(0, true); // 뽑은 고양이 세팅 


        // BGM Play
        if(GameSystem.Instance.OptionBGMPlay)
            LobbyCtrl.Instance.PlayGatchaBGM();
    }

    private void InitFadeScreen() {
        _fadeScreen.gameObject.SetActive(false);
    }

    private void InitStars() {
        for(int i=0; i< arrResultStars.Length; i++) {
            arrResultStars[i].gameObject.SetActive(false);
            arrResultStars[i].transform.DOKill();
        }
    }

    /// <summary>
    /// 10회 뽑기 세팅 
    /// </summary>
    private void SetNekoPack() {
        _nekoBall.SetActive(false);
        _gatchaNeko.SetVisible(false);

    }


    /// <summary>
    /// 티켓 네코 설정
    /// </summary>
    /// <param name="pNode"></param>
    public void SetTicketNeko(JSONNode pNode) {

        _ticketNode = pNode;

        this.gameObject.SetActive(true);
          

        this._machine.SetActive(false);
        InitFadeScreen();

        _touchArea.gameObject.SetActive(false);
        _touchArea2.gameObject.SetActive(false);


        // Row 초기화 
        _tenFirstRow.localPosition = new Vector3(800, 0, 0);
        _tenSecondRow.localPosition = new Vector3(-800, -135, 0);




        _gatchNekoID = pNode["tid"].AsInt;
        _gatchaNekoLevel = 1;
        _gatchaNekoStar = pNode["star"].AsInt;
        _preGatchaNekoStar = _gatchaNekoStar;

        if(pNode["isFusion"].AsInt == 0) {
            _isFusion = false;
            _newMainNeko = true;
        }
        else {
            _isFusion = true;
            _newMainNeko = false;
        }

        // 네코 네임, 외양 설정 
        _gatchaNekoName = GameSystem.Instance.GetNekoName(_gatchNekoID, _gatchaNekoStar);

        _gatchaNeko.SetGatchaNeko(_gatchNekoID, _gatchaNekoStar);
        _gatchaNeko.SetVisible(false);

        IsTicketGacha = true;

        ShowSingleResult();
        
    }

    /// <summary>
    /// 뽑은 고양이 정보를 미리 세팅한다.
    /// 이 정보를 바탕으로 결과값도 세팅한다.
    /// </summary>
    private void SetGatchaNeko(int pIndex, bool pNeedTenNekoColumn = false) {

        /* 
            2016.07 변경점 
            _gatchaNekoStar : 획득한 고양이 
            _preGatchaNekoStar : 기존에 갖고 있던 고양이

            두가지 변수를 구분한다. (퓨전일 경우에만 처리) 

            결과 패킷이 날아오면 (GameSystem.Instance.GatchaData) tid/star 정보가 기존에 갖고 있는 고양이 리스트에 있으면 _preGatchaNekoStar에 할당
            없다면, 신규 획득으로 간주하여 _gatchaNekoStar에 할당. 
        */


        _nekoBall.SetActive(false);

        _gatchNekoID = GameSystem.Instance.GatchaData["data"]["resultlist"][pIndex]["tid"].AsInt;
        _gatchaNekoLevel = 1;
        _gatchaNekoStar = GameSystem.Instance.GatchaData["data"]["resultlist"][pIndex]["star"].AsInt; //퓨전에서 재처리. 
        _preGatchaNekoStar = _gatchaNekoStar;

        if (GameSystem.Instance.GatchaData["data"]["resultlist"][pIndex]["isFusion"].AsInt == 0) {
            _isFusion = false;
            _newMainNeko = true; // 메인네코가 신규다.
        }
        else { // 퓨전 처리 
            _isFusion = true;

            //생선 설정
            if (GameSystem.Instance.GatchaData["data"]["resultlist"][pIndex]["fishtype"].Value == "chub") {
                _fishType = FishType.Chub;
            }
            else if (GameSystem.Instance.GatchaData["data"]["resultlist"][pIndex]["fishtype"].Value == "tuna") {
                _fishType = FishType.Tuna;
            }
            else if (GameSystem.Instance.GatchaData["data"]["resultlist"][pIndex]["fishtype"].Value == "salmon") {
                _fishType = FishType.Salmon;
            }

            _fishValue = GameSystem.Instance.GatchaData["data"]["resultlist"][pIndex]["fishvalue"].AsInt;
            

            // 메인 네코가 신규인지, sacrifice가 신규인지 판단 
            _newMainNeko = GameSystem.Instance.GetMatchNekoData(_gatchNekoID, _gatchaNekoStar);


            // 매칭되는 정보가 있으면 작은 고양이로 내려야 하기때문에, _preGatchaNekoStar로 설정 
            if(_newMainNeko) {
                _preGatchaNekoStar = _gatchaNekoStar;
                _gatchaNekoStar = GameSystem.Instance.GatchaData["data"]["resultlist"][pIndex]["origingrade"].AsInt; // sacrifice를 main으로 설정 
            }
            else { // 매칭되는 고양이 등급/아이디 정보가 없으면 신규로 생각한다. 
                _preGatchaNekoStar = GameSystem.Instance.GatchaData["data"]["resultlist"][pIndex]["origingrade"].AsInt; 
            }

            // _newMainNeko 변수 재활용 
            // _preGatchaNekoStar가 생선으로 변환된 등급인지 체크 
            _newMainNeko = true;
            if(_preGatchaNekoStar == GameSystem.Instance.GatchaData["data"]["resultlist"][pIndex]["origingrade"].AsInt) {
                _newMainNeko = false; // 동일 등급이 나온 경우는 Default로 Sub에 들어가도록 처리한다.
            }
            

        }

        // 네코 네임, 외양 설정 
        _gatchaNekoName = GameSystem.Instance.GetNekoName(_gatchNekoID, _gatchaNekoStar);

        _gatchaNeko.SetGatchaNeko(_gatchNekoID, _gatchaNekoStar);
        _gatchaNeko.SetVisible(false);


        // 나머지 9개의 네코 Sprite 세팅 (10회)
        if (!_isSinglePick && pNeedTenNekoColumn) {
            for(int i=1; i< GameSystem.Instance.GatchaData["data"]["resultlist"].Count; i++) {
                _arrMachineTenNeko[i - 1].gameObject.SetActive(false);
                _arrMachineTenNeko[i - 1].transform.localPosition = new Vector3(0, -150, 0);

                SetResultNekoSprite(_arrMachineTenNeko[i - 1], GameSystem.Instance.GatchaData["data"]["resultlist"][i - 1]);

                /*
                GameSystem.Instance.SetNekoSprite(_arrMachineTenNeko[i - 1]
                                                    , GameSystem.Instance.GatchaData["data"]["resultlist"][i - 1]["tid"].AsInt
                                                    , GameSystem.Instance.GatchaData["data"]["resultlist"][i - 1]["star"].AsInt);
                */

            }
        }

    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="pNode"></param>
    int _tempStar;
    private void SetResultNekoSprite(UISprite pSprite, JSONNode pNode) { 

        // 소유한 네코와 비교해서, 신규 등급으로 적용 
        if(pNode["isFusion"].AsInt == 0) {
            // 신규네코. 
            _tempStar = pNode["star"].AsInt;
        }
        else { // 퓨전의 경우 체크

            // 등급이 같을때
            if (pNode["star"].AsInt == pNode["origingrade"].AsInt) {
                // 등급이 동일한경우는 상관없다. 
                _tempStar = pNode["star"].AsInt;
            } 
            else { // 등급이 다를때. 등급 존재하는지 체크한다.
                if(GameSystem.Instance.GetMatchNekoData(pNode["tid"].AsInt, pNode["star"].AsInt)) {
                    _tempStar = pNode["origingrade"].AsInt;
                }
                else {
                    _tempStar = pNode["star"].AsInt;
                }
            }
        }

        // Sprite 처리 
        GameSystem.Instance.SetNekoSprite(pSprite, pNode["tid"].AsInt, _tempStar);

    }



    #region 좌우 라이트 처리 

    private void TurnOnMachineLight() {
        StartCoroutine(TurningOnMachineLight());
    }

    IEnumerator TurningOnMachineLight() {
        while(true) {
            for(int i=0; i<_leftLights.Length;i++) {
                _leftLights[i].gameObject.SetActive(true);
                _leftLights[i].Play();
                _leftLights[i].AnimationCompleted = LightCompleteDelegate;

                _rightLights[i].gameObject.SetActive(true);
                _rightLights[i].AnimationCompleted = LightCompleteDelegate;
                _rightLights[i].Play();

                yield return new WaitForSeconds(0.5f);
            }

            yield return new WaitForSeconds(1f);
        }
    }

    private void LightCompleteDelegate(tk2dSpriteAnimator pSprite, tk2dSpriteAnimationClip pClip) {
        pSprite.gameObject.SetActive(false);
    }

    private void FloorCompleteDelegate(tk2dSpriteAnimator pSprite, tk2dSpriteAnimationClip pClip) {
        
    }


    #endregion


    /// <summary>
    /// 선택한 고양이를 (10회) 메인으로 설정 
    /// </summary>
    /// <param name="pIndex"></param>
    public void SetMainNeko(int pIndex) {


        Debug.Log("SetMainNeko :: " + pIndex);

        SetGatchaNeko(pIndex);

        // 외형 설정 
        _resultNekoSprite.transform.DOKill();
        _resultNekoSprite.gameObject.SetActive(false);
        _resultNekoSprite.transform.localPosition = new Vector3(0, 470, 0); // 위치 초기화 
        _resultNekoSprite.gameObject.SetActive(true);

        StopCoroutine("ShowNekoGrade");
        InitStars();


        Vector3 destPos = new Vector3(0, 470, 0);

        _resultNekoSprite.transform.DOLocalJump(destPos, destPos.y + 100f, 1, 0.5f).OnComplete(OnCompleteMultiLocalJump); // 점핑 등장.
        GameSystem.Instance.SetNekoSprite(_resultNekoSprite, _gatchNekoID, _gatchaNekoStar);

        _lblNekoName.text = GameSystem.Instance.GetNekoName(_gatchNekoID, _gatchaNekoStar);
        _lblNekoDetail.text = GameSystem.Instance.GetNekoDetail(_gatchNekoID, _gatchaNekoStar);

        StartCoroutine(ShowNekoGrade(_gatchaNekoStar));
    }

    #region Update

    void Update() {


        if (InputLock)
            return;

        /*
        if(_isShowReulst) {
            if(Input.GetMouseButton(0)) {
                // 로비로 돌아간다. 
                LobbyCtrl.Instance.CloseGatchaScreen();
            }
        }
        */

        // 집는 행위를 했으면, 더이상 움직이지 못함.
        if (_isPicking)
            return;

        // 버튼 처리 
        if (Input.GetMouseButtonDown(0)) {

            ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            mask = 1 << LayerMask.NameToLayer(_layer);

            Debug.Log(">> GetMouseButtonDown");

            //  버튼 
            if (Physics.Raycast(ray, out hit, 10f, mask))  {

                Debug.Log("hit! :: " + hit.transform.gameObject.name);

                if (hit.transform.CompareTag("GatchaButton")) {
                    Debug.Log(">> Push Button Raycase");

                    _pushBtn.Play();

                    PlayTweezerButton();
                    PlayTweezerReady();

                    _number2.gameObject.SetActive(false);
                }

            }
        }



        // 조이스틱 처리 
        if (Input.GetMouseButton(0)) {

            // 터치한 객체가 조이스틱인지 체크 
            ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            mask = 1 << LayerMask.NameToLayer(_layer);

            //  스틱을 터치 중이 아니라면 return
            if (Physics.Raycast(ray, out hit, 10f, mask)) {

                if (!hit.transform.CompareTag("GatchaStick")) {
                    isDragged = false;
                    return;
                }

                // 불빛 처리 
                if (!_tweezerLight.IsPlaying(_clipTweezerLight)) {
                    OnMovingFx(true);
                }

                // 사운드 처리
                if(!_stickAudioSrc.isPlaying) {
                    PlayTweezerLeftRight();
                }

                if(!_number2.gameObject.activeSelf) {
                    _number2.gameObject.SetActive(true);
                    _number2.Play();

                    _number1.gameObject.SetActive(false);
                }
                


                isDragged = true;

                nowPos = Input.mousePosition;

                if (Input.GetMouseButtonDown(0)) {
                    startPos = nowPos;
                }

                previousDeltaPos = deltaPos;
                deltaPos = startPos - nowPos;

                // 두개의 deltaPos를 바탕으로 스틱의 기울기를 결정 
                if ((previousDeltaPos.x > deltaPos.x || deltaPos.x < 0)) {

                    // 플래그값을 변경하고 사운드를 한번만 재생 
                    if (isLeftMoving) {
                        isLeftMoving = false;
                        
                    }

                    // 집게 오른쪽 이동 
                    if (_tweezer.localPosition.x <= 2.3f)
                        _tweezer.Translate(Vector3.right * Time.deltaTime * tweezerSpeed);


                    // 스틱 오른쪽으로 기울여짐 (-)
                    if (_joyStick.localRotation.z < -0.2f)
                        return;

                    _joyStick.Rotate(0, 0, stickSpeed * Time.deltaTime * -1);




                }
                else if (previousDeltaPos.x < deltaPos.x || deltaPos.x > 0) {


                    // 플래그값을 변경하고 사운드를 한번만 재생 
                    if (!isLeftMoving) {
                        isLeftMoving = true;
                        
                    }

                    if (_tweezer.localPosition.x >= -2.3f)
                        _tweezer.Translate(Vector3.right * Time.deltaTime * tweezerSpeed * -1);


                    // 왼쪽으로 기울여짐 (+)
                    if (_joyStick.localRotation.z > 0.2f)
                        return;

                    _joyStick.Rotate(0, 0, stickSpeed * Time.deltaTime);


                }


            } // end of Physics 

        }
        else {
            isDragged = false;
        }
        // end of first GetMouseButton

        if (Input.GetMouseButtonUp(0)) {
            isDragged = false;
            _joyStick.localEulerAngles = Vector3.zero;


            StopTweezerLeftRight();

            if (!_isPicking) {
                OnMovingFx(false);
            }

        }
    }

    #endregion


    #region 일반 뽑기 

    /// <summary>
    /// 일반 뽑기 연출 
    /// </summary>
    private void OnGeneralCinematic() {

        // 사운드 플레이 
        PlayPickNewNeko();

        // 페이드 스크린 등장 (고양이는 가리면 안되기 때문에 tk2d 사용)         
        _fadeScreen.gameObject.SetActive(true);

        

        if(_isSinglePick)
            Invoke("ShowSingleResult", 1); 
        else
            Invoke("ShowMultiResult", 1);
    }

    #endregion

    #region 10회 뽑기 결과 화면 오픈 


    #endregion



    #region 뽑기 결과 화면 오픈

    private void ShowMultiResult() {
        Debug.Log(">> ShowMultiResult");

        // 효과음 재생 
        PlayGatchaResult();

        // 현 상태 그대로 UI 창을 띄운다. 
        InitResultUI();

        // SNS 버튼 설정 (튜토리얼에서 등장하지 않음)
        SetSNSButton();

        // 뽑힌 고양이 disable 
        _gatchaNeko.SetVisible(false);

        // 10마리 고양이 세팅
        for(int i=0; i<_arrResultTenNeko.Length; i++) {
            _arrResultTenNeko[i].SetNeko(i, GameSystem.Instance.GatchaData["data"]["resultlist"][i], this);
            /*
            _arrResultTenNeko[i].SetNeko(i, GameSystem.Instance.GatchaData["data"]["resultlist"][i]["tid"].AsInt
                                          , GameSystem.Instance.GatchaData["data"]["resultlist"][i]["star"].AsInt
                                          , this
                                          , GameSystem.Instance.GatchaData["data"]["resultlist"][i]["isFusion"].AsInt);
             */

            // 버튼 콤포넌트를 enable 시켜놓는다
            SetButtonComponent(_arrResultTenNeko[i].gameObject, false);

        }

        // 첫번째 고양이 외형 세팅 
        _lblExtraInfo.transform.localPosition = new Vector3(0, 80, 0);
        _resultNekoSprite.transform.localPosition = new Vector3(0, 470, 0);
        // Circle 이동 
        _resultCircleFX.transform.localPosition = new Vector3(0, 470, 0);

        Vector3 destPos = new Vector3(0, 470, 0);

        _resultNekoSprite.gameObject.SetActive(true);
        GameSystem.Instance.SetNekoSprite(_resultNekoSprite, _gatchNekoID, _gatchaNekoStar); // 네코 Sprite 세팅 
        _resultNekoSprite.transform.DOLocalJump(destPos, destPos.y + 100f, 1, 0.5f).OnComplete(OnCompleteSingleLocalJump); // 점핑 등장.

        _lblNekoName.text = GameSystem.Instance.GetNekoName(_gatchNekoID, _gatchaNekoStar);
        _lblNekoDetail.text = GameSystem.Instance.GetNekoDetail(_gatchNekoID, _gatchaNekoStar);

        StartCoroutine(DoingYellowEffect());

        // 효과 회전 
        _resultCircleFX.DOLocalRotate(new Vector3(0, 0, 720), 3, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);

        // Row 이동 
        _tenFirstRow.DOLocalMoveX(0, 0.5f);
        _tenSecondRow.DOLocalMoveX(0, 0.5f).OnComplete(OnCompleteMultiNekoRowMove);

        Invoke("EnableResultTenNekoButton", 0.5f);

        _touchArea.transform.localPosition = new Vector3(0, 470, 0);
        _touchArea.gameObject.SetActive(true);
        _touchArea2.gameObject.SetActive(true);
    }

    /// <summary>
    /// 
    /// </summary>
    private void OnCompleteMultiNekoRowMove() {
        // 마지막 네코 강조 처리 
        _arrResultTenNeko[_arrResultTenNeko.Length - 1].SetLastNeko();
    }

    private void EnableResultTenNekoButton() {

        
        for (int i = 0; i < _arrResultTenNeko.Length; i++) {
            SetButtonComponent(_arrResultTenNeko[i].gameObject, true);

        }

    }

    /// <summary>
    /// 1회 가챠 결과 보여주기
    /// </summary>
    private void ShowSingleResult() {

        Debug.Log(">> Show Single Result");

        // 효과음 재생 
        PlayGatchaResult();

        // 현 상태 그대로 UI 창을 띄운다. 
        InitResultUI();


        // SNS 버튼 설정 
        SetSNSButton();

        // 뽑힌 고양이 disable 
        _gatchaNeko.SetVisible(false);


        _lblExtraInfo.transform.localPosition = new Vector3(0, -200, 0);
        _resultNekoSprite.transform.localPosition = new Vector3(0, 270, 0);
        _resultCircleFX.transform.DOKill();
        _resultCircleFX.transform.localPosition = new Vector3(0, 270, 0);
        _resultCircleFX.transform.localEulerAngles = Vector3.zero;

        Vector3 destPos = new Vector3(0, 270, 0);

        _resultNekoSprite.gameObject.SetActive(true);
        InitStars();

        _resultNekoSprite.transform.DOLocalJump(destPos, destPos.y + 100, 1, 0.5f).OnComplete(OnCompleteSingleLocalJump); // 점핑 등장.
        GameSystem.Instance.SetNekoSprite(_resultNekoSprite, _gatchNekoID, _gatchaNekoStar);

        _lblNekoName.text = GameSystem.Instance.GetNekoName(_gatchNekoID, _gatchaNekoStar);
        _lblNekoDetail.text = GameSystem.Instance.GetNekoDetail(_gatchNekoID, _gatchaNekoStar);

        StartCoroutine(DoingWhiteEffect());

        // 효과 회전 
        _resultCircleFX.DOLocalRotate(new Vector3(0, 0, 720), 3, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);

        Invoke("EnableTouchArea", 1.5f);
        
    }

    private void EnableTouchArea() {
        _touchArea.gameObject.SetActive(true);
        _touchArea.transform.localPosition = new Vector3(0, 200, 0);
        _touchArea2.gameObject.SetActive(true);
    }

    /// <summary>
    /// 결과 UI 초기화 
    /// </summary>
    private void InitResultUI() {

        _resultUI.SetActive(true);

        // 각 이펙트 정보 초기화 
        for (int i = 0; i < _arrWhiteLights.Length; i++) {
            _arrWhiteLights[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < _arrYellowLights.Length; i++) {
            _arrYellowLights[i].gameObject.SetActive(false);
        }

        if (IsTicketGacha)
            _ticketBG.SetActive(true);
        else
            _ticketBG.SetActive(false);
    }




    IEnumerator DoingWhiteEffect() {
        for (int i = 0; i < _arrWhiteLights.Length; i++) {
            Vector3 randomPos = new Vector3(Random.Range(-300, 300), Random.Range(-140, 400), 0);
            _arrWhiteLights[i].PlayRandomScale(randomPos);

            yield return new WaitForSeconds(0.4f);
        }
    }

    IEnumerator DoingYellowEffect() {
        for (int i = 0; i < _arrYellowLights.Length; i++) {
            Vector3 randomPos = new Vector3(Random.Range(-300, 300), Random.Range(-140, 400), 0);
            _arrYellowLights[i].PlayRandomScale(randomPos);

            yield return new WaitForSeconds(0.4f);
        }
    }

    /// <summary>
    /// 1회 가챠의 등급 표시 후, 추가 정보 표시 
    /// </summary>
    private void OnCompleteSingleLocalJump() {
        // 퓨전 처리
        if (_isFusion) {
            //_resultNekoSprite.GetComponent<GatchaNekoReturnCtrl>().SetExtraInfo(_fishType, _gatchNekoID, _gatchaNekoStar, _fishValue, _preGatchaNekoStar);
            //_resultNekoSprite.GetComponent<GatchaNekoReturnCtrl>().SetExtraInfo(_fishType, _gatchNekoID, _preGatchaNekoStar, _fishValue, _newMainNeko);
        }
        else { // 퓨전이 아닌 경우는 무조건 'New'가 표시
            _resultNekoSprite.GetComponent<GatchaNekoReturnCtrl>().SetNewNekoMark(_newMainNeko);
        }

        // 등급 별 효과 
        StartCoroutine(ShowNekoGrade(_gatchaNekoStar));
    }

    /// <summary>
    /// 10회 가챠의 등급 표시 후, 추가 정보 표시 
    /// </summary>
    private void OnCompleteMultiLocalJump() {
        // 퓨전 처리
        if (_isFusion) {
            //_resultNekoSprite.GetComponent<GatchaNekoReturnCtrl>().SetExtraInfo(_fishType, _gatchNekoID, _gatchaNekoStar, _fishValue, _preGatchaNekoStar);
            //_resultNekoSprite.GetComponent<GatchaNekoReturnCtrl>().SetExtraInfo(_fishType, _gatchNekoID, _preGatchaNekoStar, _fishValue, _newMainNeko);
        }
        else {
            _resultNekoSprite.GetComponent<GatchaNekoReturnCtrl>().SetNewNekoMark(_newMainNeko);
        }

        

        // 등급 별 효과 
        ShowImediateNekoGrade(_gatchaNekoStar);
    }


    private void SetSNSButton() {
        // 튜토리얼 에서는 자랑하기를 없앤다.
        if (GameSystem.Instance.TutorialComplete != 2) {
            _btnFB.gameObject.SetActive(false);
            _btnTW.gameObject.SetActive(false);
            _btnConfirm.gameObject.SetActive(true);
        }
        else {
            _btnFB.gameObject.SetActive(true);
            _btnTW.gameObject.SetActive(true);
            _btnConfirm.gameObject.SetActive(false);
        }

    }



    /// <summary>
    /// 즉각적인 고양이 등급 표시 
    /// </summary>
    /// <param name="pStarCount"></param>
    private void ShowImediateNekoGrade(int pStarCount) {
        Debug.Log(">>>  ShowImediateNekoGrade pStarCount :: " + pStarCount);

        _resultNekoSprite.GetComponent<GatchaNekoReturnCtrl>().EnableStars();

        for (int i = 0; i < arrResultStars.Length; i++) {
            arrResultStars[i].gameObject.SetActive(false);
        }

        #region 위치 지정 
        Vector3[] arrPos;

        if (pStarCount == 1)
            arrPos = _oneStarPos;
        else if (pStarCount == 2)
            arrPos = _twoStarPos;
        else if (pStarCount == 3)
            arrPos = _threeStarPos;
        else if (pStarCount == 4)
            arrPos = _fourStarPos;
        else
            arrPos = _fiveStarPos;


        for (int i = 0; i < pStarCount; i++) {
            arrResultStars[i].transform.localPosition = arrPos[i];

        }

        for (int i = 0; i < pStarCount; i++) {
            arrResultStars[i].gameObject.SetActive(true);
            //arrResultStars[i].transform.DOLocalJump(arrResultStars[i].transform.localPosition, 100, 1, 0.5f);
        }

        #endregion


    }

    /// <summary>
    /// 별 표시 
    /// </summary>
    /// <param name="pStarCount"></param>
    /// <returns></returns>
    IEnumerator ShowNekoGrade(int pStarCount) {

        Debug.Log(">>>  ShowNekoGrade pStarCount :: " + pStarCount);

        _resultNekoSprite.GetComponent<GatchaNekoReturnCtrl>().EnableStars();

        for (int i = 0; i < arrResultStars.Length; i++) {
            arrResultStars[i].gameObject.SetActive(false);
        }


        #region 위치 지정 
        Vector3[] arrPos;

        if (pStarCount == 1)
            arrPos = _oneStarPos;
        else if (pStarCount == 2)
            arrPos = _twoStarPos;
        else if (pStarCount == 3)
            arrPos = _threeStarPos;
        else if (pStarCount == 4)
            arrPos = _fourStarPos;
        else
            arrPos = _fiveStarPos;

        
        for (int i = 0; i < pStarCount; i++) {
            arrResultStars[i].transform.localPosition = arrPos[i];
            
        }
        
        #endregion



        for (int i=0; i<pStarCount; i++) {
            arrResultStars[i].gameObject.SetActive(true);
            //arrResultStars[i].transform.DOLocalJump(arrResultStars[i].transform.localPosition, 100, 1, 0.5f);
            arrResultStars[i].transform.DOLocalJump(arrPos[i], 100, 1, 0.5f);
            //PlaySound(SoundConstBox.acStarJump);
            yield return new WaitForSeconds(0.1f);
        }

        _isShowReulst = true; // 결과창 처리 완료 
    }

    #endregion






    /// <summary>
    /// Tweezer 집게를 벌려 준비상태로 한다. 
    /// </summary>
    private void PlayTweezerReady() {
        _isPicking = true;

        //OnMovingFx(true);
        _tweezerAnimator.Play("TweezerForwardClip");
    }

    


    /// <summary>
    /// 집게를 아래로 내린다. 
    /// </summary>
    private void DownTweezer() {
        PlayTweezerUpDown();

        if(_isSinglePick)
            _tweezer.DOLocalMoveY(1.4f, 1).OnComplete(PlayTweezerDone);
        else
            _tweezer.DOLocalMoveY(1.2f, 1).OnComplete(PlayTweezerDone);
    }


    /// <summary>
    /// 집게를 위로 올린다.
    /// </summary>
    private void UpTweezer() {
        OnGatchaNekoSprite();

        PlayTweezerUpDown();
        _tweezer.DOLocalMoveY(4.5f, 1).OnComplete(OnCompleteUpTweezer);
    }

    /// <summary>
    /// 10회 뽑기에서 들어올리기. 
    /// </summary>
    private void UpMultiTweezer() {
        StartCoroutine(PunchingTweezer());
    }

    IEnumerator PunchingTweezer() {
        //_tweezer.DOPunchPosition(new Vector3(0, 1.22f, 0), 0.2f, 3, 0.3f);
        _tweezer.DOShakePosition(0.2f, 0.5f, 50, 3);
        PlayPunchTweezer();

        yield return new WaitForSeconds(1);

        //_tweezer.DOPunchPosition(new Vector3(0, 1.22f, 0), 0.2f, 3, 0.3f);
        _tweezer.DOShakePosition(0.3f, 0.6f, 50, 3);
        PlayPunchTweezer();

        yield return new WaitForSeconds(1);

        // 매우 흔들기
        _tweezer.DOPunchPosition(new Vector3(0, 1.25f, 0), 1f, 7, 0.5f);
        PlayPunchTweezer();

        yield return new WaitForSeconds(1f);

        OnGatchaNekoSprite();
        _tweezer.DOLocalMoveY(5f, 1.5f).SetEase(Ease.OutBounce);
        yield return new WaitForSeconds(0.1f);
        ThrowNekos();
        PlayFlyNeko();

        yield return new WaitForSeconds(2);

        OnCompletePicking();

    }

    private void ThrowNekos() {
        for(int i=0; i<_arrMachineTenNeko.Length;i++) {
            _arrMachineTenNeko[i].gameObject.SetActive(true);
            Vector3 dest = new Vector3(Random.Range(-750, 750), 850, 0);
            _arrMachineTenNeko[i].transform.DOLocalMove(dest, Random.Range(1, 1.5f));
        }

    }


    /// <summary>
    /// 획득 가챠 네코 등장 
    /// </summary>
    private void OnGatchaNekoSprite() {

        PlayTweezerGrab();

        _gatchaNeko.SetVisible(true);

        // 10마리 날리기? 
        if(!_isSinglePick) {

        }

    }


    /// <summary>
    /// 집게로 끌어오림이 종료되었음. 
    /// </summary>
    private void OnCompleteUpTweezer() {
        
        //OnFusionCenterCinematic ();
        OnCompletePicking();
    }

  


    private void PlayTweezerDone() {
        _tweezerAnimator.Play("TweezerBackwardClip");
    }


    /// <summary>
    /// 캐릭터 집기 완료.
    /// </summary>
    private void OnCompletePicking() {

        Debug.Log("OnCompletePicking");

        OnGeneralCinematic();

        
    }


    /// <summary>
    /// Animations the complete delegate.
    /// </summary>
    /// <param name="pSprite">P sprite.</param>
    /// <param name="pClip">P clip.</param>
    public void AnimationCompleteDelegate(tk2dSpriteAnimator pSprite, tk2dSpriteAnimationClip pClip) {

        // 집게 펴지기 완료되면 내려간다. 
        if (pClip.name.Equals("TweezerForwardClip")) {
            DownTweezer();
        }
        else if (pClip.name.Equals("TweezerBackwardClip")) {

            if (_isSinglePick) {
                UpTweezer();
            }
            else {
                Debug.Log("Multi Gatacha");
                UpMultiTweezer();
            }

        }

    }





    /// <summary>
    /// Raises the moving fx event.
    /// </summary>
    /// <param name="pFlag">If set to <c>true</c> p flag.</param>
    private void OnMovingFx(bool pFlag) {

        //_tweezerLight.gameObject.SetActive(pFlag);

        if (pFlag) {
            _tweezerLight.Play();
        }
        else {
            _tweezerLight.Stop();
  
        }

    }

    private void SetButtonComponent(GameObject pObj, bool pValue) {

        pObj.GetComponentInChildren<UIButton>().enabled = pValue;

    }


    #region SNS Upload

    public void OnClickTwiiter() {
        _snsUpload.CaptureAdoptNeko(_gatchNekoID, _gatchaNekoStar, SocialType.twiiter);
    }

    public void OnClickFB() {
        //GameSystem.Instance.OnOffWaitingRequestInLobby(true);
        _snsUpload.CaptureAdoptNeko(_gatchNekoID, _gatchaNekoStar, SocialType.FB);
    }


    #endregion

    #region 사운드 플레이 
    private void PlayTweezerGrab() {

        if (!GameSystem.Instance.OptionSoundPlay)
            return;

        _gatchaAudioSrc.PlayOneShot(_clipTweezerGrab);
    }

    private void PlayTweezerButton() {
        if (!GameSystem.Instance.OptionSoundPlay)
            return;

        _gatchaAudioSrc.PlayOneShot(_clipButton);
    }

    private void PlayTweezerUpDown() {
        if (!GameSystem.Instance.OptionSoundPlay)
            return;

        _gatchaAudioSrc.PlayOneShot(_clipTweezerUpDown);
    }

    private void PlayTweezerLeftRight() {
        if (!GameSystem.Instance.OptionSoundPlay)
            return;

        Debug.Log("PlayTweezerLeftRight");
        _stickAudioSrc.Play();
    }

    private void StopTweezerLeftRight() {
        if (!GameSystem.Instance.OptionSoundPlay)
            return;

        _stickAudioSrc.Stop();
    }

    private void PlayPickNewNeko() {

        if (!GameSystem.Instance.OptionSoundPlay)
            return;


        if (LobbyCtrl.Instance != null)
            LobbyCtrl.Instance.StopBGM();

        _gatchaAudioSrc.PlayOneShot(_clipPickNewNeko);
    }

    private void PlayGatchaResult() {
        if (!GameSystem.Instance.OptionSoundPlay)
            return;

        _gatchaAudioSrc.PlayOneShot(_clipGatchaResult);
    }

    private void PlayPunchTweezer() {
        if (!GameSystem.Instance.OptionSoundPlay)
            return;

        _stickAudioSrc.PlayOneShot(_clipPunchTweezer);
    }

    private void PlayFlyNeko() {
        if (!GameSystem.Instance.OptionSoundPlay)
            return;

        _stickAudioSrc.PlayOneShot(SoundConstBox.acEnemyNekoKill);
    }

    #endregion


}
