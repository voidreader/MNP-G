using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using PathologicalGames;
using DG.Tweening;

public class BottleManager : MonoBehaviour {


    static BottleManager _instance = null;

    [SerializeField]
    List<NekoAppearCtrl> _listBottleNekoAppear = new List<NekoAppearCtrl>();

    [SerializeField]
    List<BottleNekoCtrl> _listBottleNeko = new List<BottleNekoCtrl>();

    GameObject _catBottle = null;

    [SerializeField] Transform _btnWakeUp;
    [SerializeField] Transform _timer;

    [SerializeField] GameObject _screen;

    [SerializeField] UILabel _lblMins;
    [SerializeField] UILabel _lblSecs;

    [SerializeField] GameObject _bottle;
    [SerializeField] GameObject _bottleUI;
    [SerializeField] Transform _bottomUI;
    [SerializeField] UIProgressBar _progressTimer;


    [SerializeField] GameObject _bottleBG;

    System.TimeSpan _nextWakeUpTime;

    bool _onBottleUI = false;


    [SerializeField] AudioSource _srcBottleAudio;
    [SerializeField] AudioClip _clipWakeUpRing;

    readonly string BOTTLE_POOL = "BottlePool";
    readonly string APPEAR_POOL = "NekoAppear";
    readonly string BOTTLE_NEKO = "BottleNeko";
    readonly string APPEAR_NEKO = "NekoAppear";
    readonly string WAKEUPTIME = "wakeuptime";

    #region Properties 

    public static BottleManager Instance {
        get {
            if (_instance == null) {
                _instance = FindObjectOfType(typeof(BottleManager)) as BottleManager;

                if (_instance == null) {
                    // Debug.Log("LobbyCtrl Init Error");
                    return null;
                }
            }

            return _instance;
        }
    }

    public List<NekoAppearCtrl> ListBottleNekoAppear {
        get {
            return _listBottleNekoAppear;
        }

        set {
            _listBottleNekoAppear = value;
        }
    }

    public List<BottleNekoCtrl> ListBottleNeko {
        get {
            return _listBottleNeko;
        }

        set {
            _listBottleNeko = value;
        }
    }

    #endregion



    void Update() {

        
        if (Time.frameCount % 20 == 0) {
            UpdateWakeUpTime();
        }
        
    }

    void UpdateWakeUpTime() {

        if (!_bottleUI.activeSelf)
            return;

        _nextWakeUpTime = GameSystem.Instance.GetRemainWakeUpTimeTick();


        if(_nextWakeUpTime == System.TimeSpan.Zero || GameSystem.Instance.IsRequestingWakeUp) {
            _timer.gameObject.SetActive(false);
            OnSleepUI();
            return;
        }

        // Timer 관련 업데이트 
        UpdateTimer();

    }


    void UpdateTimer() {
        _timer.gameObject.SetActive(true);
        _lblMins.text = _nextWakeUpTime.Minutes.ToString();
        _lblSecs.text = _nextWakeUpTime.Seconds.ToString();
    }

    /// <summary>
    /// Sleep 상태 체크 
    /// </summary>
    /// <returns></returns>
    bool CheckSleep() {
        _nextWakeUpTime = GameSystem.Instance.GetRemainWakeUpTimeTick();


        if (_nextWakeUpTime == System.TimeSpan.Zero || GameSystem.Instance.IsRequestingWakeUp) {
            return true; // 잠든 상태 
        }

        return false;
    }

    /// <summary>
    ///  깨우기 
    /// </summary>
    public void WakeUp() {
        GameSystem.Instance.Post2WakeUp(GameSystem.Instance.UserJSON["wakeuptime"]);

        SoundPlayWakeUpRing();

        WakeUpNekos();
        
    }


    public void WakeUpCallback() {
        this._screen.SetActive(false);
        this._btnWakeUp.gameObject.SetActive(false);
    }
    
    void OnSleepUI() {
        //Debug.Log("★ OnSleepUI");

        if (_btnWakeUp.gameObject.activeSelf)
            return;

        _screen.SetActive(true);

        _btnWakeUp.gameObject.SetActive(true);
        _btnWakeUp.localPosition = new Vector3(0, -100, 0);
        _btnWakeUp.DOLocalMoveY(-20, 0.5f);

        _timer.gameObject.SetActive(false);
    }

    /// <summary>
    /// 
    /// </summary>
    void InitUI() {
        _bottleUI.SetActive(false);
    }

    /// <summary>
    /// 
    /// </summary>
    public void InitBottle() {

        InitUI();
        SpawnBottleNeko();
    }

    IEnumerator CheckingUI() {


        // 최초 실행시에, 유리병 고양이에 대한 물리처리를 한다. 
        if(CheckSleep()) {
            SleepNekos();
        }
        else {
            WakeUpNekos();
        }

        while(true) {
            // CenterObject 를 체크한다.

            yield return new WaitForSeconds(0.2f);

            //if(StageMasterCtrl.Instance.StageCenterOnChild.centeredObject == )
            if (_catBottle == null)
                continue;

            


            // Center에 위치했을때. 
            if(StageMasterCtrl.Instance.StageCenterOnChild.centeredObject == _catBottle) {


                if (_onBottleUI)
                    continue;

                // UI 등장
                _onBottleUI = true;

                // 하단 UI 내려가고, 
                _bottomUI.DOLocalMoveY(-850, 0.5f);

                _bottleUI.SetActive(true);

                if(CheckSleep()) { // 잠들었을때, 


                    if(!_screen.activeSelf) {
                        SleepNekos();
                    }

                    OnSleepUI();


                }
                else {

                    if(!_timer.gameObject.activeSelf) {
                        WakeUpNekos();
                    }

                    _timer.gameObject.SetActive(true);
                    _screen.SetActive(false);
                    _btnWakeUp.gameObject.SetActive(false);
                    _timer.localPosition = new Vector3(0, -850, 0);
                    _timer.DOLocalMoveY(-470, 0.5f).SetDelay(0.5f);
                    UpdateWakeUpTime();
                }


                continue;
            }
            else { // Center에 위치하지 않았을때. 

                if (!_onBottleUI)
                    continue;

                _onBottleUI = false;
                _bottleUI.SetActive(false);
                _bottomUI.DOLocalMoveY(-570, 0.5f);

            }

            

        }
    }


    /// <summary>
    /// 
    /// </summary>
    void WakeUpNekos() {
        // 병안에 들어있는 고양이 로직
        for (int i = 0; i < _listBottleNeko.Count; i++) {
            _listBottleNeko[i].WakeUpNeko();
        }
    }

    void SleepNekos() {
        // 병안에 들어있는 고양이 로직
        for (int i = 0; i < _listBottleNeko.Count; i++) {
            _listBottleNeko[i].SleepNeko();
        }
    }


    /// <summary>
    /// 병 고양이 소환 
    /// </summary>
    void SpawnBottleNeko() {

        PoolManager.Pools[BOTTLE_POOL].DespawnAll();
        PoolManager.Pools[APPEAR_POOL].DespawnAll();
        

        _listBottleNeko.Clear();
        _listBottleNekoAppear.Clear();

        StartCoroutine(Spawning());

    }

    IEnumerator Spawning() {

        _bottle.SetActive(true);
        _bottleBG.SetActive(true);

        for (int i = 0; i < GameSystem.Instance.UserNeko.Count; i++) {

            PoolManager.Pools[APPEAR_POOL].Spawn(APPEAR_NEKO, Vector3.zero, Quaternion.identity);

            if(GameSystem.Instance.UserNeko.Count >= 40) {
                PoolManager.Pools[BOTTLE_POOL].Spawn(BOTTLE_NEKO, new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 0.85f), 0), Quaternion.identity).GetComponent<BottleNekoCtrl>()
.SetLobbyNeko(GameSystem.Instance.UserNeko[i]["tid"].AsInt, GameSystem.Instance.UserNeko[i]["star"].AsInt, i, true);
            }
            else {
                PoolManager.Pools[BOTTLE_POOL].Spawn(BOTTLE_NEKO, new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 0.85f), 0), Quaternion.identity).GetComponent<BottleNekoCtrl>()
                .SetLobbyNeko(GameSystem.Instance.UserNeko[i]["tid"].AsInt, GameSystem.Instance.UserNeko[i]["star"].AsInt, i);
            }



            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(1);

        // cat Bottle을 찾는다.
        _catBottle = GameObject.FindGameObjectWithTag("CatBottle");

        StartCoroutine(CheckingUI());
    }



    /// <summary>
    /// 고양이 하나 추가하기.
    /// </summary>
    /// <param name="pNode"></param>
    public void SpawnSingleNeko(SimpleJSON.JSONNode pNode) {
        PoolManager.Pools[APPEAR_POOL].Spawn(APPEAR_NEKO, Vector3.zero, Quaternion.identity);
        PoolManager.Pools[BOTTLE_POOL].Spawn(BOTTLE_NEKO, new Vector3(Random.Range(-2f, 2f), 0.9f, 0), Quaternion.identity).GetComponent<BottleNekoCtrl>()
        .SetLobbyNeko(pNode["tid"].AsInt, pNode["star"].AsInt, _listBottleNekoAppear.Count-1);
    }


    /// <summary> 
    /// 유리병 숨기기 
    /// </summary>
    public void HideNBottleeko() {
        for(int i =0; i<ListBottleNekoAppear.Count;i++) {
            ListBottleNekoAppear[i].gameObject.SetActive(false);
        }

        _bottle.SetActive(false);

    }

    public void ShowBottleNeko() {
        for (int i = 0; i < ListBottleNekoAppear.Count; i++) {
            ListBottleNekoAppear[i].gameObject.SetActive(true);
        }

        _bottle.SetActive(true);
    }


    /// <summary>
    /// 유리병 공유 화면 오픈 
    /// </summary>
    public void OpenMainShare() {
        
        GameSystem.Instance.Post2CheckShareBonus();
    }

    #region Sound

    /// <summary>
    /// 
    /// </summary>
    public void SoundPlayWakeUpRing() {
        _srcBottleAudio.PlayOneShot(_clipWakeUpRing);
    }

    #endregion
}

