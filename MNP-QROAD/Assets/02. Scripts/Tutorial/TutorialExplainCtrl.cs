using UnityEngine;
using System.Collections;
using DG.Tweening;

public class TutorialExplainCtrl : MonoBehaviour {

	[SerializeField] Transform _trNeko;

	[SerializeField] string _text;
	[SerializeField] UILabel _lblSmall;

	[SerializeField] UISprite _spPanel;
	
    [SerializeField] GameObject _touchMark;
    [SerializeField] Transform _touchArrow;


    [SerializeField]
    bool _hasTouchMark = true;

	private Vector3 lowerPos = new Vector3(0,-460,0);
	private Vector3 upperPos = new Vector3(0,-250,0);

    private Vector3 originArrowPos = new Vector3(-20, 25, 0);

    [SerializeField]
    AudioSource _audioSource;

	// Use this for initialization
	void Start () {
	
	}


	void OnEnable() {
        

	}

    private void SetMoving() {
        _trNeko.DOKill();
        
        _trNeko.localScale = GameSystem.Instance.BaseScale;
        //Enable 시에 말랑말랑
        _trNeko.DOScale(1.1f, 1).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);

        if (_hasTouchMark) {
            _touchMark.gameObject.SetActive(true);
            _touchArrow.DOKill();
            _touchArrow.localPosition = originArrowPos;
            _touchArrow.DOLocalMoveX(0, 0.5f).SetLoops(-1, LoopType.Yoyo);
        } else {
            _touchMark.gameObject.SetActive(false);
        }

    }


    /// <summary>
    /// 설명창 하단으로 아웃 처리 
    /// </summary>
    public void SetOutExplain() {
        this.transform.DOLocalMoveY(-1100, 0.5f).SetDelay(1);
    }

    public void SetExplainText(int pLocalizeID, bool pIsBig, bool pIsUpper=false, bool pHasTouchMark=true) {

        this.gameObject.SetActive(true);

        _hasTouchMark = pHasTouchMark;

        SetMoving();


        if (pIsUpper) {
			this.transform.localPosition = upperPos;
		} else {
			this.transform.localPosition = lowerPos;
		}

		//_text = GameSystem.Instance.DocsLocalize.get<string> (pLocalizeID.ToString(), "content");
        _text = GameSystem.Instance.GetLocalizeText(pLocalizeID.ToString());


        _lblSmall.gameObject.SetActive(true);


		_lblSmall.text = _text;
	}

    public void SetNekoEnable(bool pFlag) {
        _trNeko.gameObject.SetActive(pFlag);
    }


	public void SetLobbyExplainText(int pLocalizeID, bool pUpperFlag) {

        SetMoving();

        if (pUpperFlag)
		    this.transform.localPosition = new Vector3 (0, -15, 0);
        else
            this.transform.localPosition = new Vector3(0, -500, 0);

        //_text = GameSystem.Instance.DocsLocalize.get<string> (pLocalizeID.ToString(), "content");
        _text = GameSystem.Instance.GetLocalizeText(pLocalizeID.ToString());

        _lblSmall.gameObject.SetActive(true);

		_lblSmall.text = _text;
		
	}
	
	public void OnCompleteClick() {

        _audioSource.Play();

        if (InGameCtrl.Instance != null) {
            InGameCtrl.Instance.IsWaitingExplain = false;
            this.gameObject.SetActive(false);
            return;
        }


        if(LobbyCtrl.Instance != null) {
            LobbyCtrl.Instance.IsWaitingTab = false;
            this.gameObject.SetActive(false);
            return;
        }

		if (GameSystem.Instance.TutorialStage < 10) {
			
		}
		else { 
			if(LobbyCtrl.Instance != null)
				LobbyCtrl.Instance.IsWaitingTab = false;
		}


    }
}
