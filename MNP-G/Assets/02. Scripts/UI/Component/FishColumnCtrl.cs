using UnityEngine;
using System.Collections;
using DG.Tweening;

public class FishColumnCtrl : MonoBehaviour {

    [SerializeField] int _index; // 컬럼 위치 (0~5) 6개 

    [SerializeField] GameObject _activeSet;
	[SerializeField] UISprite _fish;
	[SerializeField] UILabel _lblQ;
    [SerializeField] UIButton _btnRemove;

    [SerializeField]
    bool _isReceiveColumn; // 주는쪽인지 받는쪽인지 체크 


    // 생선량 
	[SerializeField] int _quantity;


    // 현재 설정 창 
	PlayerOwnNekoCtrl _neko;
    NekoFeedCtrl _base;

    int _frameCount = 10;

    bool _isPressed;
    int _pressedFrameCount = 0;

	// Use this for initialization
	void Start () {
    }

    void Update() {
        /*
        if (Time.frameCount % _frameCount == 0 && GetInput(0) && UICamera.hoveredObject != null) {
            // 주는쪽에서만 동작 
            if (UICamera.hoveredObject == _activeSet) {
                FeedFish();
                if (_frameCount > 4)
                    _frameCount--;
            }
        }
        */

        if (_isPressed)
            _pressedFrameCount += 2;

        if(_isPressed && Time.frameCount % _frameCount == 0 && GetInput(0) && _pressedFrameCount >= 30) {
            FeedFish();
            if (_frameCount > 4)
                _frameCount--;
        }

        if(GetInputUp(0)) {
            _isPressed = false;
            _pressedFrameCount = 0;
            _frameCount = 10;
        }

    }

    public void OnPressbyEvent() {

        if (GameSystem.Instance.LocalTutorialStep == 5) {
            return;
        }

        _isPressed = true;
    }

    public void OnReleaseByEvent() {
        _isPressed = false;
        _pressedFrameCount = 0;
    }

    /// <summary>
    /// 생선을 Add 하는 컬럼 초기화 (Warehouse)
    /// </summary>
    /// <param name="pIndex"></param>
    /// <param name="pNeko"></param>
    /// <param name="pBase"></param>
	public void SetFishColumn(int pIndex, PlayerOwnNekoCtrl pNeko, NekoFeedCtrl pBase) {


        _isReceiveColumn = false;

        _index = pIndex;
		_neko = pNeko;
		_base = pBase;

		switch (_index) {
		case 0:
			_fish.spriteName = "ico_fish_go";
			_quantity = GameSystem.Instance.UserChub;

			break;
		case 1:
			_fish.spriteName = "ico_fish_tuna";
			_quantity = GameSystem.Instance.UserTuna;
             break;

		case 2:
			_fish.spriteName = "ico_fish_salmon";
			_quantity = GameSystem.Instance.UserSalmon;
             break;

		case 3:
			_fish.spriteName = "ico_fish_gocan";
			_quantity = 0;
			break;
		case 4:

			_fish.spriteName = "ico_fish_tunacan";
			_quantity = 0;
			break;
		case 5:
			_fish.spriteName = "ico_fish_salmoncan";
			_quantity = 0;
			break;
		}



        _btnRemove.gameObject.SetActive(false);
        _lblQ.text = _quantity.ToString();
	}

    /// <summary>
    /// 받는쪽 컬럼 초기화
    /// </summary>
    /// <param name="pIndex"></param>
    /// <param name="pNeko"></param>
    /// <param name="pBase"></param>
    public void SetRecieveColumn(int pIndex, PlayerOwnNekoCtrl pNeko, NekoFeedCtrl pBase) {

        _isReceiveColumn = true;

        _index = pIndex;
        _neko = pNeko;
        _base = pBase;

        switch (_index) {
            case 0:
                _fish.spriteName = "ico_fish_go";
                
                break;
            case 1:
                _fish.spriteName = "ico_fish_tuna";
                
                break;
            case 2:
                _fish.spriteName = "ico_fish_salmon";
                
                break;
            case 3:
                _fish.spriteName = "ico_fish_gocan";
                
                break;
            case 4:

                _fish.spriteName = "ico_fish_tunacan";
                
                break;
            case 5:
                _fish.spriteName = "ico_fish_salmoncan";
                
                break;
        }

        _quantity = 0;

        _activeSet.SetActive(false);
        _btnRemove.gameObject.SetActive(false);
        _lblQ.text = _quantity.ToString();

    }

    /// <summary>
    /// 생선 주기(생선컬럼터치 - 주는쪽 )
    /// </summary>
    public void FeedFish() {

        // 받는쪽 컬럼이면 하나씩 뺀다. 
        if(_isReceiveColumn) {
            MinusFeedFish();
            return;
        }

        //Debug.Log("FeedFish index :: " + _index);

        if(_base.Neko.IsMaxGrade) {
            LobbyCtrl.Instance.OpenInfoPopUp(PopMessageType.CantGrowNeko);
            return;
        }

        
		if (_quantity <= 0)
			return;

        // 게이지가 가득차면 더이상 줄 수없음. 
        if (_base.IsMaxBead)
            return;

        _fish.transform.DOKill();
        _fish.transform.localScale = GameSystem.Instance.BaseScale;
		_fish.transform.DOPunchScale (new Vector3 (1.1f, 1.1f, 1), 0.2f, 2).OnComplete (OnCompletePunch);


        switch (_index) {
            case 0:
                GameSystem.Instance.FeedChub++; // 실제 전송데이터 증가 
                _base.AddFish(1); // 게이지 처리용 정보 처리 
                break;
            case 1:
                GameSystem.Instance.FeedTuna++;
                _base.AddFish(3);
                break;

            case 2:
                GameSystem.Instance.FeedSalmon++;
                _base.AddFish(5);
                break;
        }

        // 개수 차감, 실제 먹임 
        _quantity--;
        _lblQ.text = _quantity.ToString();

        // Recieve 컬럼으로 전달 
        _base.ArrRecieveFishColumn[_index].ReceiveFeedFish();
        
        LobbyCtrl.Instance.PlayPossitive();
    }



    /// <summary>
    /// 생선을 받는쪽 컬럼 처리 
    /// </summary>
    public void ReceiveFeedFish() {

        _quantity++;

        switch (_index) {

            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
        }


        _lblQ.text = _quantity.ToString();
        _activeSet.SetActive(true);

        if (_quantity > 0)
            _btnRemove.gameObject.SetActive(true);


        // 튜토리얼 처리 
        if(GameSystem.Instance.LocalTutorialStep == 5) {
            // 하나를 받았으면, 컬럼을 Lock 처리 한다.
            _base.SetTutorialLock();
        }

    }

    /// <summary>
    /// 해당컬럼의 모든 생선 제거
    /// </summary>
    public void RemoveFeedFish() {
        // give Column에 받은 만큼 돌려주고 0으로 처리
        //_base.ArrGiveFishColumn[_index].add

        if (!_isReceiveColumn)
            return;

        // 수량만큼 fake 게이지도 처리해야한다. 
        //_base.MinusFish()    
        switch(_index) {
            case 0:
                _base.MinusFish(_quantity * 1);
                break;
            case 1:
                _base.MinusFish(_quantity * 3);
                break;
            case 2:
                _base.MinusFish(_quantity * 5);
                break;
        }

        _base.ArrGiveFishColumn[_index].ReturnFish(_quantity);

        _quantity = 0;
        _lblQ.text = _quantity.ToString();
        _activeSet.SetActive(false);

        LobbyCtrl.Instance.PlayNegative();
    }

    /// <summary>
    /// 1개씩 빼기.
    /// </summary>
    private void MinusFeedFish() {
        if (!_isReceiveColumn)
            return;

        if (_quantity <= 0)
            return;

        switch (_index) {
            case 0:
                _base.MinusFish(1);
                break;
            case 1:
                _base.MinusFish(3);
                break;
            case 2:
                _base.MinusFish(5);
                break;
        }

        _quantity--;
        _lblQ.text = _quantity.ToString();

        _base.ArrGiveFishColumn[_index].AddWareHouseFish();
        

        if (_quantity <= 0) {
            _activeSet.SetActive(false);
        }

        LobbyCtrl.Instance.PlayNegative();

    }


    /// <summary>
    /// 1개씩 증가 
    /// </summary>
    public void AddWareHouseFish() {
        _quantity++;
        _lblQ.text = _quantity.ToString();

        // 1개씩 감소시킨다. 
        switch (_index) {
            case 0:
                GameSystem.Instance.FeedChub--;
                break;
            case 1:
                GameSystem.Instance.FeedTuna--;
                break;
            case 2:
                GameSystem.Instance.FeedSalmon--;
                break;
        }
    }

    /// <summary>
    /// Recieve 쪽에서 도로 반환 
    /// </summary>
    public void ReturnFish(int pQ) {
        _quantity += pQ;
        _lblQ.text = _quantity.ToString();

        // 인덱스에 따른 생선 처리 (return 이니까 0으로 만든다)
        switch(_index) {
            case 0:
                GameSystem.Instance.FeedChub = 0;
                break;
            case 1:
                GameSystem.Instance.FeedTuna = 0;
                break;
            case 2:
                GameSystem.Instance.FeedSalmon = 0;
                break;
        }
    }

	private void OnCompletePunch() {
		this._fish.transform.localScale = PuzzleConstBox.baseScale;

        
	}

    void OnHover(bool isOver) {
        Debug.Log("OnHover :: " + isOver.ToString());
    }



    #region INPUT_METHOD
#if UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_WEBPLAYER || UNITY_WEBGL
    public static Vector2 GetInputPosition(int touchPoint = 0) {
        return Input.mousePosition;
    }

    public static bool GetInputDown(int inputNum = 0) {
        return Input.GetMouseButtonDown(inputNum);
    }

    public static bool GetInputUp(int inputNum = 0) {
        return Input.GetMouseButtonUp(inputNum);
    }

    public static bool GetInput(int inputNum = 0) {
        //Debug.Log("Get Input :: " +  inputNum);
        return Input.GetMouseButton(inputNum);

        
    }

#elif UNITY_ANDROID || UNITY_IPHONE
	
	public static Vector2 GetInputPosition(int touchPoint = 0)
	{
		return Input.touches[touchPoint].position;
		
	}
	
	public static bool GetInputDown(int inputNum = 0)
	{
		if(Input.touches.Length == 0)
			return false;

		return (Input.touches[inputNum].phase == TouchPhase.Began);
	}
	
	public static bool GetInputUp(int inputNum = 0)
	{
        if (Input.touchCount == 0)
                return false;

		return (Input.touches[inputNum].phase == TouchPhase.Ended);
	}
	
	public static bool GetInput(int inputNum = 0)
	{

        if (Input.touchCount == 0)
            return false;

		return (Input.touches[inputNum].phase == TouchPhase.Stationary);
	}
#endif
    #endregion

}
