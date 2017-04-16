
using UnityEngine;
using System.Collections;

public class InputCtrl : MonoBehaviour {
	
	static InputCtrl _instance = null; 
	
	[SerializeField]
	public Camera _camera = null;
	[SerializeField]
	public Camera uiCamera = null;
	
	/* Raycast hit 용도 */
	private Ray ray;
	private RaycastHit hit; 
	private LayerMask mask;
	private GameObject hitBlock;
    BlockCtrl _blockCtrl;

    [SerializeField] float _noInputTime;

	
	
	public static InputCtrl Instance {
		get {
			if(_instance == null) {
				_instance = FindObjectOfType(typeof(InputCtrl)) as InputCtrl;
				
				if(_instance == null) {
					//Debug.Log("InputCtrl Init Error");
					return null;
				}
			}
			
			return _instance;
		}
	}


    #region Properties

    public float NoInputTime {
        get {
            return _noInputTime;
        }

        set {
            _noInputTime = value;
        }
    }

    #endregion



    // Update is called once per frame
    void Update () {


        // 네비게이터가 없을때 미입력시간 증가 
        if(!InGameCtrl.Instance.IsOnNavigator 
            && (InGameCtrl.Instance.currentState == GameState.Play ) 
            && !InGameCtrl.Instance.ConfirmExit.activeSelf
            && !InUICtrl.Instance.pausePopup.activeSelf
            ) {

            _noInputTime += Time.deltaTime;

        }
            

		
		// Pause Control
		if(Input.GetKeyDown(KeyCode.Escape)) {
			
            // 10초 더 팝업이 띄워진 상태에서는 동작하지 않음.
            if(InUICtrl.Instance.MorePlayCtrl.gameObject.activeSelf) {
                return;
            }

            if(InGameCtrl.Instance.ConfirmExit.activeSelf) {
                InGameCtrl.Instance.ConfirmExit.SendMessage("Close");
                return;
            }

			if(InUICtrl.Instance.pausePopup.activeSelf) {
				InUICtrl.Instance.Resume();
			} else {
				InUICtrl.Instance.PopPause();
			}
		}
		
		
		if (InGameCtrl.Instance.updateLock) 
			return;
		
		// 팝업창이 띄워져 있을때는 입력받지 않음. 
		if (InUICtrl.Instance.IsActivePausePopup)
			return;
		
		// 플레이중이 아닐떄는 입력받지 않음 
		if (!InGameCtrl.Instance.IsPlaying) {
			return;
		}
		
		
		if(GetInputDown ()) {
			
			//Debug.Log("▶ GetInputDown #1");
			
			ray = _camera.ScreenPointToRay(GetInputPosition());
			
			mask = 1 << LayerMask.NameToLayer("Block");
			
			if(Physics.Raycast(ray, out hit, 20f, mask)) {

				hitBlock = hit.transform.gameObject;
				
				if(hitBlock.CompareTag("Block") && InGameCtrl.Instance.currentState == GameState.Play) {

                    _blockCtrl = hitBlock.GetComponent<BlockCtrl>();

                    if (_blockCtrl.currentState == BlockState.Inactive)
                        return;

                    hitBlock.SendMessage("TouchBlock");
                    InitNoInputTime(); // 미입력 시간 초기화
                }
				
				return;
			}
			
			ray = uiCamera.ScreenPointToRay(GetInputPosition());
			mask = 1 << LayerMask.NameToLayer("UI");
			

			
		} // GetInputDown 종료 
	}



    private void InitNoInputTime() {
        NoInputTime = 0;

        if(InGameCtrl.Instance.IsOnNavigator)
            InGameCtrl.Instance.SendMessage("OffNavigator");

    }
	
	#region INPUT_METHOD
	#if UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_WEBPLAYER || UNITY_WEBGL
	public static Vector2 GetInputPosition(int touchPoint = 0)
	{
		return Input.mousePosition;
	}
	
	public static  bool GetInputDown(int inputNum = 0)
	{
		return Input.GetMouseButtonDown(inputNum);
	}
	
	public static bool GetInputUp(int inputNum = 0)
	{
		return Input.GetMouseButtonUp(inputNum);
	}
	
	public static bool GetInput(int inputNum = 0)
	{
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
		return (Input.touches[inputNum].phase == TouchPhase.Ended);
	}
	
	public static bool GetInput(int inputNum = 0)
	{
		return (Input.touches[inputNum].phase == TouchPhase.Stationary);
	}
	#endif
	#endregion
}