using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class NekoEvolutionCtrl : MonoBehaviour {

    
    [SerializeField] UIButton _collider;
    [SerializeField] Transform _title;
    [SerializeField] Transform _aura;
    [SerializeField] Transform _spotlight;
    [SerializeField] UISprite _nekoSprite;
    [SerializeField] GameObject _whiteSplash;

    [SerializeField] Transform[] _arrStars = new Transform[5]; // 별 등급 
    [SerializeField] UISpriteAnimation[] _arrStarEffects = new UISpriteAnimation[5]; // 별 효과

    [SerializeField] YellowLightCtrl[] _arrYellowLights;

    //[SerializeField] UILabel _lblInfo;

    int _previousGrade;
    int _currentGrade;
    int _nekoID;

    bool _isFinalEvolution = false; // 5성 고양이의 진화인지 체크 


    Vector3[] _oneStarPos = new Vector3[] { Vector3.zero };
    Vector3[] _twoStarPos = new Vector3[] { new Vector3(-50, 0, 0), new Vector3(50, 0, 0) };
    Vector3[] _threeStarPos = new Vector3[] { new Vector3(-100, 0, 0), new Vector3(0, 0, 0), new Vector3(100, 0, 0) };
    Vector3[] _fourStarPos = new Vector3[] { new Vector3(-150, 0, 0), new Vector3(-50, 0, 0), new Vector3(50, 0, 0), new Vector3(150, 0, 0) };
    Vector3[] _fiveStarPos = new Vector3[] { new Vector3(-200, 0, 0), new Vector3(-100, 0, 0), new Vector3(0, 0, 0), new Vector3(100, 0, 0), new Vector3(200, 0, 0) };

    [SerializeField] float _jumpDelayTime = 0.5f;

    // Use this for initialization
    void Start () {
        //Init();
    }

    void Update() {

        /*
        if (Input.GetKeyDown(KeyCode.A)) {
            SetNekoEvolution(9, 4, 5);
        }
        */
    }
        
    void OnEnable() {
        StopBGM();
    }

    void OnDisable() {

        

        ResumeBGM();
        StopEffect();
        LobbyCtrl.Instance.SendMessage("EnableTopLobbyUI");
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="pNekoID"></param>
    /// <param name="pPreviousGrade"></param>
    /// <param name="pCurrentGrade"></param>
    public void SetNekoEvolution(int pNekoID, int pPreviousGrade, int pCurrentGrade) {


        LobbyCtrl.Instance.SendMessage("DisableTopLobbyUI");
            

        this.gameObject.SetActive(true);

		//GameSystem.Instance.CheckLobbyBingoQuest (35); // Neko Rank Up  Bingo

        _isFinalEvolution = false;

        if(pPreviousGrade == 4 && pCurrentGrade == 5) {
            _isFinalEvolution = true;
        }


        _nekoID = pNekoID;
        _previousGrade = pPreviousGrade;
        _currentGrade = pCurrentGrade;

        // 네코 스프라이트 설정 
        GameSystem.Instance.SetNekoSprite(_nekoSprite, _nekoID, _previousGrade);


        if (_isFinalEvolution)
            PlayFinalEvolution();
        else
            PlayGeneralEvolution();




    }
	

    /// <summary>
    /// 일반 진화 연출 플레이 
    /// </summary>
    private void  PlayGeneralEvolution() {
        Init();

        PlayTitleAura();

        StartCoroutine(PlayingGeneralEvolution());



    }

    /// <summary>
    /// 최종 진화 연출 플레이 
    /// </summary>
    private void PlayFinalEvolution() {

        Init();

        PlayTitleAura();

        StartCoroutine(PlayingFinalEvolution());
    }

    private void PlayTitleAura() {
        _title.gameObject.SetActive(true);
        _title.localScale = new Vector3(2, 2, 1);
        _title.DOScale(1, 0.5f);

        _aura.gameObject.SetActive(true);
        _aura.localScale = Vector3.zero;
        _aura.DOScale(1, 0.5f).OnComplete(OnCompleteAuraScale);

    }

    /// <summary>
    /// 네코 세팅 
    /// </summary>
    private void SetNeko() {
        _nekoSprite.gameObject.SetActive(true);
        _nekoSprite.transform.localPosition = new Vector3(0, 100, 0);

        // 점프
        _nekoSprite.transform.DOLocalJump(_nekoSprite.transform.localPosition, 250, 1, _jumpDelayTime);

        PlaySound(SoundConstBox.acNekoJump);
    }

    /// <summary>
    /// 5성 고양이 세팅 
    /// </summary>
    private void SetFinalNeko() {
        GameSystem.Instance.SetNekoSprite(_nekoSprite, _nekoID, _currentGrade);

        _nekoSprite.gameObject.SetActive(true);
        _nekoSprite.transform.localPosition = new Vector3(0, 100, 0);

        // 점프
        _nekoSprite.transform.DOLocalJump(_nekoSprite.transform.localPosition, 250, 1, _jumpDelayTime);
        PlaySound(SoundConstBox.acNekoJump);
    }


    IEnumerator PlayingFinalEvolution() {
        yield return new WaitForSeconds(0.5f);

        Debug.Log("PlayingFinalEvolution");

        //Spotlight
        _spotlight.gameObject.SetActive(true);
        _spotlight.DOScaleX(1, 0.5f);

        yield return new WaitForSeconds(0.5f);

        SetNeko();

        // 별 (이전 등급)
        SetSimpleGradeStar(_previousGrade);

        // 흔들어준다
        yield return new WaitForSeconds(_jumpDelayTime);

        //_nekoSprite.transform.DOShakePosition(2, 2f, 20, 90).OnComplete(OnCompleteShakePosition);
        _nekoSprite.transform.DOPunchPosition(new Vector3(20, 10, 0), 2, 15, 0.8f).OnComplete(OnCompleteShakePosition);
        _nekoSprite.transform.DOScale(0, 1.8f);
        PlaySound(SoundConstBox.acShrinkNeko);

        yield return new WaitForSeconds(2);

        // Splash
        _whiteSplash.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        _whiteSplash.SetActive(false);

        // Spotlight 축소
        _spotlight.DOScaleX(0, 0.5f);

        // new Neko!
        SetFinalNeko();

        // star effect 플레이 
        PlayStarEffect(_previousGrade);

        yield return new WaitForSeconds(0.3f);

        DisableStarEffect();

        // 현 등급 처리
        SetGradeStar(_currentGrade);

        PlayEvolBGM();

        // Yellow Light 이펙트 
        for (int i = 0; i < _arrYellowLights.Length; i++) {
            Vector3 randomPos = new Vector3(Random.Range(-300, 300), Random.Range(-140, 400), 0);
            _arrYellowLights[i].PlayRandomScale(randomPos);

            yield return new WaitForSeconds(0.4f);
        }


        _collider.enabled = true;

        UpdateReadyGroup();


    }


    IEnumerator PlayingGeneralEvolution() {
        yield return new WaitForSeconds(0.5f);

        Debug.Log("PlayingGeneralEvolution");

        // 네코 세팅
        SetNeko();

        // 별 (이전 등급)
        SetSimpleGradeStar(_previousGrade);

        yield return new WaitForSeconds(_jumpDelayTime * 2);

        // star effect 플레이 
        PlayStarEffect(_previousGrade);

        yield return new WaitForSeconds(0.3f);

        DisableStarEffect();



        // 현 등급 처리
        SetGradeStar(_currentGrade);
        PlayEvolBGM();

        // Yellow Light 이펙트 
        for (int i=0; i<_arrYellowLights.Length; i++) {
            Vector3 randomPos = new Vector3(Random.Range(-300, 300), Random.Range(-140, 400), 0);
            _arrYellowLights[i].PlayRandomScale(randomPos);

            yield return new WaitForSeconds(0.4f);
        }

        _collider.enabled = true;

        UpdateReadyGroup();
    }


    private void OnCompleteShakePosition() {
        _nekoSprite.transform.localPosition = new Vector3(0, 100, 0);
        _nekoSprite.transform.localScale = new Vector3(1, 1, 1);
    }

    private void UpdateReadyGroup () {
        if (ReadyGroupCtrl.Instance != null) {
            ReadyGroupCtrl.Instance.UpdateEquipNeko();
        }
    }

    /// <summary>
    /// 별 이펙트 플레이 
    /// </summary>
    /// <param name="pGrade"></param>
    private void PlayStarEffect(int pGrade) {

        for(int i=0; i<pGrade; i++) {
            _arrStarEffects[i].gameObject.SetActive(true);
            _arrStarEffects[i].Play();
        }

    }

    /// <summary>
    /// 
    /// </summary>
    private void DisableStarEffect() {
        for(int i=0; i<_arrStarEffects.Length; i++) {
            _arrStarEffects[i].gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name=""></param>
    private void SetGradeStar(int pGrade) {
        //_lblInfo.gameObject.SetActive(true);
        StartCoroutine(SettingStars(pGrade));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="pGrade"></param>
    /// <returns></returns>
    IEnumerator SettingStars(int pGrade) {

        // 위치 세팅 
        SetSimpleGradeStar(pGrade);

        // invisible 
        for (int i=0; i<_arrStars.Length; i++) {
            _arrStars[i].gameObject.SetActive(false);
        }



        for(int i=0; i<pGrade; i++) {
            _arrStars[i].gameObject.SetActive(true);
            _arrStars[i].transform.DOLocalJump(_arrStars[i].transform.localPosition, 100, 1, 0.5f);
            PlaySound(SoundConstBox.acStarJump);
            yield return new WaitForSeconds(0.2f);
        }
    }
       


    /// <summary>
    /// 단순 별처리 
    /// </summary>
    /// <param name="pGrade"></param>
    private void SetSimpleGradeStar(int pGrade) {

        Vector3[] arrPos;

        if (pGrade == 1)
            arrPos = _oneStarPos;
        else if (pGrade == 2)
            arrPos = _twoStarPos;
        else if (pGrade == 3)
            arrPos = _threeStarPos;
        else if (pGrade == 4)
            arrPos = _fourStarPos;
        else
            arrPos = _fiveStarPos;

        for (int i=0; i<pGrade; i++) {
            _arrStars[i].transform.localPosition = arrPos[i];
            _arrStars[i].gameObject.SetActive(true);
            _arrStarEffects[i].transform.localPosition = arrPos[i];
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void OnCompleteAuraScale() {
        _aura.DOLocalRotate(new Vector3(0, 0, 360), 2f, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
    }

    /// <summary>
    /// 초기화 
    /// </summary>
    private void Init() {
        _aura.DOKill();

        _collider.enabled = false;
        //_lblInfo.gameObject.SetActive(false);
        _whiteSplash.SetActive(false);
        _spotlight.gameObject.SetActive(false);
        _title.gameObject.SetActive(false);
        _aura.gameObject.SetActive(false);

        _title.transform.localScale = Vector3.zero;
        _aura.transform.localScale = Vector3.zero;
        _aura.transform.localEulerAngles = Vector3.zero;

        _spotlight.transform.localScale = new Vector3(0, 1, 1);

        _nekoSprite.gameObject.SetActive(false);

        for(int i=0; i<_arrStarEffects.Length; i++) {
            _arrStarEffects[i].gameObject.SetActive(false);
        }

        for(int i=0; i<_arrStars.Length;i++) {
            _arrStars[i].gameObject.SetActive(false);
        }

        for(int i=0; i<_arrYellowLights.Length; i++) {
            _arrYellowLights[i].gameObject.SetActive(false);
        }

    }



    // 로비 배경화면 BGM Pause, Resume
    private void StopBGM() {
        LobbyCtrl.Instance.SendMessage("PauseBGM");
    }

    private void ResumeBGM() {
        LobbyCtrl.Instance.SendMessage("ResumeBGM");
    }

    private void StopEffect() {
        LobbyCtrl.Instance.SendMessage("StopEffect");
    }
    
    private void PlayEvolBGM() {
        LobbyCtrl.Instance.PlayEffect(SoundConstBox.acNekoEvolBGM);
    }

    /// <summary>
    /// 사운드 플레이 
    /// </summary>
    /// <param name="pAC"></param>
    private void PlaySound(AudioClip pAC) {
        LobbyCtrl.Instance.PlayEffect(pAC);
    }
}
