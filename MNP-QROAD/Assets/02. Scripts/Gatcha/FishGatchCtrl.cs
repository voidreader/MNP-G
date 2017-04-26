using UnityEngine;
using System.Collections;
using System.Collections.Generic;


using DG.Tweening;

public class FishGatchCtrl : MonoBehaviour {

    [SerializeField] Transform _ship;
    [SerializeField] Transform _mainCat;
    [SerializeField] UISprite _fishingRod; // 낚시대 

    [SerializeField] UISprite _fish; // 낚은 고기 
    [SerializeField] Transform _upperObject; //바구니랑 하니 

    [SerializeField] bool _isSingleFish = true;
    [SerializeField] Ease _ease = Ease.InOutBack;


    Vector3 _fishOriginPos = new Vector3(180, -445, 0);
    Vector3 _fishDestPos = new Vector3(-60, 300, 0);
    Vector3 _fishFocusPos = new Vector3(245, 500, 0);
    Vector3 _upperObjectInitPos = new Vector3(-50, 320, 0);
    Vector3 _shipInitPos = new Vector3(0, -290, 0);

    // 10마리 짜리 
    [SerializeField] List<ResultFishCtrl> _listResultFish = new List<ResultFishCtrl>(); // 결과창의 10마리의 ResultFishCtrl
    [SerializeField] List<UISprite> _multiFishes = new List<UISprite>(); // 낚시화면에서 보이는 메인 물고기 외의 9마리 고양이. 


    [SerializeField] ResultFishCtrl _singlePopupResultFish;
    

    [SerializeField] GameObject _resultSinglePopup;
    [SerializeField] GameObject _resultMultiPopup;

    [SerializeField] GameObject _resultAuraGroup;
    [SerializeField] Transform _resultAura;
    [SerializeField] GameObject _resultMainFishGroup;
    [SerializeField] UISprite _resultMainFish;
    //[SerializeField] UILabel _resultSingleValue;

    [SerializeField] UIButton _btnHook;


    [SerializeField] float _singleJumpSpeed = 1;
    [SerializeField] float _multiJumpSpeed = 0.3f;
    [SerializeField] float _jumpSpeed;

    // 바구니
    [SerializeField] Transform _bucket;
    



    // Use this for initialization
    void Start() {
        //InitializeFishing(true);

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="pSingleFish"></param>
    public void InitializeFishing() {
        this.gameObject.SetActive(true);


        LobbyCtrl.Instance.PlayFishingBGM();

        _fish.gameObject.SetActive(false);
        _resultAuraGroup.SetActive(false);
        _resultMainFishGroup.SetActive(false);
        _resultSinglePopup.SetActive(false);
        _resultMultiPopup.SetActive(false);

        for(int i=0; i<_multiFishes.Count;i++) {
            _multiFishes[i].gameObject.SetActive(false);
        }


        // 멀티인지 싱글인지 체크 
        if (GameSystem.Instance.FishGachaJSON["fishlist"].Count > 1) {
            _isSingleFish = false;
        } else {
            _isSingleFish = true;
        }
            

        
        

        _btnHook.enabled = true;


        // 위치 초기화 
        _upperObject.DOKill();
        _ship.DOKill();
        _upperObject.localPosition = _upperObjectInitPos;
        _ship.localPosition = _shipInitPos;
       

        // 움직임 처리 
        _ship.DOLocalMoveY(-280, 0.3f).SetLoops(-1, LoopType.Yoyo);
        //_mainCat.DOLocalMoveY(165, 0.3f).SetLoops(-1, LoopType.Yoyo);
        _upperObject.DOLocalMoveY(_upperObject.transform.localPosition.y + 20, 0.5f).SetLoops(-1, LoopType.Yoyo);
    }

    private void OnCompleteMainCatJump() {
        //_mainCat.DOLocalMoveY(165, 0.3f).SetLoops(-1, LoopType.Yoyo);

        _fishingRod.spriteName = "f1";
        _fishingRod.transform.localPosition = new Vector3(97, -97, 0);
    }

    private void OnCompleteFishJump() {
        _fish.transform.DOKill();
    }


    /// <summary>
    /// 버튼 터치 
    /// </summary>
    public void Hook() {

        _btnHook.enabled = false;
        _jumpSpeed = _singleJumpSpeed;

        if(!_isSingleFish) {
            StartCoroutine(MultiHooking());
            return;
        }

        _mainCat.DOKill();
        _mainCat.DOLocalJump(_mainCat.transform.localPosition, 400, 1, _jumpSpeed).SetEase(_ease).OnComplete(OnCompleteMainCatJump);
        LobbyCtrl.Instance.PlayNekoRewardGet();

        // 낚시대 처리 
        _fishingRod.spriteName = "f3";
        _fishingRod.transform.localPosition = new Vector3(97, 60, 0);

        // 생선 Jump
        _fish.gameObject.SetActive(true);
        _fish.transform.localEulerAngles = Vector3.zero;
        //_resultSingleValue.text = "";


        GameSystem.Instance.SetFishSprite(_fish, GameSystem.Instance.FishGachaJSON["fishlist"][0]["type"].Value); // 첫번째 물고기로 처리 


        _fish.transform.localPosition = _fishOriginPos;
        _fish.transform.DOLocalJump(_fishDestPos, 200, 1, 1f).OnComplete(OnCompleteFirstFishJump);
        _fish.transform.DOLocalRotate(new Vector3(0, 0, 360), 0.5f, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);
        Invoke("OnCompleteFishInBucket", 1.5f);

    }


    IEnumerator MultiHooking() {

        _mainCat.DOKill();
        _mainCat.DOShakePosition(1.5f, 10f, 10, 10);

        yield return new WaitForSeconds(1.6f);

        // Jump
        _mainCat.DOKill();
        _mainCat.DOLocalJump(_mainCat.transform.localPosition, 400, 1, _jumpSpeed).SetEase(_ease).OnComplete(OnCompleteMainCatJump);

        // 낚시대 처리 
        _fishingRod.spriteName = "f3";
        _fishingRod.transform.localPosition = new Vector3(97, 60, 0);

        // 생선 Jump
        _fish.gameObject.SetActive(true);
        _fish.transform.localEulerAngles = Vector3.zero;
        //_resultSingleValue.text = "";




        GameSystem.Instance.SetFishSprite(_fish, GameSystem.Instance.FishGachaJSON["fishlist"][0]["type"].Value); // 첫번째 물고기로 처리 


        _fish.transform.localPosition = _fishOriginPos;
        _fish.transform.DOLocalJump(_fishDestPos, 200, 1, 1f).OnComplete(OnCompleteFirstFishJump);
        _fish.transform.DOLocalRotate(new Vector3(0, 0, 360), 0.5f, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);

        Invoke("OnCompleteFishInBucket", 1.5f);

        Debug.Log("★★ MultiHooking Count :: " + GameSystem.Instance.FishGachaJSON["fishlist"].Count);

        for (int i = 1; i < GameSystem.Instance.FishGachaJSON["fishlist"].Count; i++) {
            GameSystem.Instance.SetFishSprite(_multiFishes[i-1], GameSystem.Instance.FishGachaJSON["fishlist"][i]["type"].Value);

            // 날려버린다. 
            _multiFishes[i - 1].transform.localPosition = _fishOriginPos;
            Vector3 jumpPos = new Vector3(Random.Range(-320, 320), 800, 0);
            _multiFishes[i - 1].gameObject.SetActive(true);
            _multiFishes[i - 1].transform.DOLocalMove(jumpPos, Random.Range(0.3f,1.5f));
        }

    }

    private void OnCompleteFirstFishJump() {
        _fish.transform.DOKill();
        _fish.transform.localEulerAngles = Vector3.zero;

        _upperObject.transform.DOKill();

        // Bucket 흔들기
        _bucket.DOShakePosition(0.5f, 5, 10, 30);
    }


    private void OnCompleteFishInBucket() {

        Debug.Log(">>>> OnCompleteFishInBucket");
        _fish.transform.DOKill();
        _fish.transform.localPosition = _fishFocusPos;
        _fish.transform.DOLocalMoveY(_fishFocusPos.y + 50, 1.5f).OnComplete(OnCompleteSingleFocus);
       
    }

    /// <summary>
    /// 싱글 낚시, 포커스 종료후 결과 관련 동작 처리 
    /// </summary>
    private void OnCompleteSingleFocus() {
        _fish.gameObject.SetActive(false); // fish 비활성화

        if(_isSingleFish)
            StartCoroutine(ShowSingleResult());
        else
            StartCoroutine(ShowMultiResult());
    }

    IEnumerator ShowMultiResult() {

        LobbyCtrl.Instance.PlayEffect(SoundConstBox.acGatchaResult);

        _resultAuraGroup.SetActive(true);
        _resultAura.localScale = Vector3.zero;
        _resultAura.localEulerAngles = Vector3.zero;
        _resultAura.DOScale(1, 0.5f).OnComplete(OnCompleteAuraScale);

        yield return new WaitForSeconds(0.5f);


        // main Fish 등장
        _resultMainFishGroup.SetActive(true);
        GameSystem.Instance.SetFishSprite(_resultMainFish, GameSystem.Instance.FishGachaJSON["fishlist"][0]["type"].Value);

        //_resultNekoSprite.transform.DOLocalJump(destPos, destPos.y + 100f, 1, 1f).OnComplete(OnCompleteSingleLocalJump); // 점핑 등장.
        Vector3 jumpDestPos = Vector3.zero;
        _resultMainFish.transform.DOLocalJump(jumpDestPos, 100, 1, 0.5f);

        // multiFish 재활용
        for(int i=0; i<_multiFishes.Count;i++) {
            yield return new WaitForSeconds(0.2f);

            Vector3 initPos = new Vector3(Random.Range(-180, 180), 0, 0);
            _multiFishes[i].transform.localPosition = initPos;
            _multiFishes[i].transform.DOLocalJump(initPos, 100, 1, 0.5f);
            LobbyCtrl.Instance.PlayNekoRewardGet(); // 사운드 

        }

        yield return new WaitForSeconds(1);

        // 결과 화면 오픈 
        _resultMultiPopup.SetActive(true);

        int count = 0;
        string fish;
        FishType fishType = FishType.Chub;

        for (int i = 0; i < GameSystem.Instance.FishGachaJSON["fishlist"].Count; i++) {
            count = GameSystem.Instance.FishGachaJSON["fishlist"][i]["value"].AsInt;
            fish = GameSystem.Instance.FishGachaJSON["fishlist"][i]["type"].Value;

            if (fish == "chub") {
                fishType = FishType.Chub;
            }
            else if (fish == "tuna") {
                fishType = FishType.Tuna;
            }
            else if (fish == "salmon") {
                fishType = FishType.Salmon;
            }

            _listResultFish[i].SetResultFish(fishType, count);
        }


        LobbyCtrl.Instance.PlayNekoRewardGet(); // 사운드 
    }

    /// <summary>
    /// 단일 낚시 결과 시작 
    /// </summary>
    /// <returns></returns>
    IEnumerator ShowSingleResult() {

        LobbyCtrl.Instance.PlayEffect(SoundConstBox.acGatchaResult);

        _resultAuraGroup.SetActive(true);
        _resultAura.localScale = Vector3.zero;
        _resultAura.localEulerAngles = Vector3.zero;
        _resultAura.DOScale(1, 0.5f).OnComplete(OnCompleteAuraScale);

        yield return new WaitForSeconds(0.5f);

        // main Fish 등장
        _resultMainFishGroup.SetActive(true);
        GameSystem.Instance.SetFishSprite(_resultMainFish, GameSystem.Instance.FishGachaJSON["fishlist"][0]["type"].Value);

        //_resultNekoSprite.transform.DOLocalJump(destPos, destPos.y + 100f, 1, 1f).OnComplete(OnCompleteSingleLocalJump); // 점핑 등장.
        Vector3 jumpDestPos = Vector3.zero;
        _resultMainFish.transform.DOLocalJump(jumpDestPos, 100, 1, 0.5f);

        yield return new WaitForSeconds(0.5f);

        int count = 0;
        string fish;
        FishType fishType = FishType.Chub;

        count = GameSystem.Instance.FishGachaJSON["fishlist"][0]["value"].AsInt;
        //_resultSingleValue.text = "+" + count.ToString();

        fish = GameSystem.Instance.FishGachaJSON["fishlist"][0]["type"].Value;
        if (fish == "chub") {
            fishType = FishType.Chub;
        }
        else if (fish == "tuna") {
            fishType = FishType.Tuna;
        }
        else if (fish == "salmon") {
            fishType = FishType.Salmon;
        }

        yield return new WaitForSeconds(1);

        // 결과 팝업 오픈
        _resultSinglePopup.SetActive(true);
        _singlePopupResultFish.SetResultFish(fishType, count);
        LobbyCtrl.Instance.PlayNekoRewardGet(); // 사운드 
    }

    private void OnCompleteAuraScale() {
        // 회전 처리 
        _resultAura.DOLocalRotate(new Vector3(0, 0, 360), 2, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);


    }
    

   

    
    public void CloseFishGatcha() {
        LobbyCtrl.Instance.SendMessage("EnableTopLobbyUI");
        LobbyCtrl.Instance.PlayLobbyBGM();

        //_resultPopup.SendMessage("CloseSelf");
        this.gameObject.SetActive(false);
        LobbyCtrl.Instance.OpenGatchaConfirm();

    }

    
	
}
