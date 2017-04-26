using UnityEngine;
using System;
using System.Collections;
using DG.Tweening;

public class NekoEvolAlertCtrl : MonoBehaviour {

    [SerializeField] UISprite _currentNekoSprite;
    [SerializeField] UISprite _evolNekoSprite;

    [SerializeField] UILabel _currentNekoName;
    [SerializeField] UILabel _evolNekoName;

    [SerializeField] Transform _arrow;

    Vector3 _arrowOriginPos = new Vector3(0, 35, 0);
    Vector3 _originRot = new Vector3(0, 0, -10);

    public static event Action OnCompleteClickOK = delegate { };

    /// <summary>
    /// 
    /// </summary>
    /// <param name="pNekoID"></param>
    /// <param name="pCurrnetGrade"></param>
    /// <param name="pNextGrade"></param>
    public void SetFinalEvolutionAlert(int pNekoID, int pCurrnetGrade, int pNextGrade) {

        this.gameObject.SetActive(true);

        // 네코 설정 
        GameSystem.Instance.SetNekoSprite(_currentNekoSprite, pNekoID, pCurrnetGrade);
        GameSystem.Instance.SetNekoSprite(_evolNekoSprite, pNekoID, pNextGrade);

        _currentNekoName.text = GameSystem.Instance.GetNekoName(pNekoID, pCurrnetGrade);
        _evolNekoName.text = GameSystem.Instance.GetNekoName(pNekoID, pNextGrade);

        _arrow.DOKill();
        _arrow.localPosition = _arrowOriginPos;
        _arrow.DOLocalMoveX(10, 0.5f).SetLoops(-1, LoopType.Restart);


        _evolNekoSprite.transform.DOKill();
        _evolNekoSprite.transform.localEulerAngles = _originRot;
        _evolNekoSprite.transform.DOLocalRotate(new Vector3(0, 0, 10), 0.5f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
    }

    public void OnClickOK() {

        OnCompleteClickOK();
        OnCompleteClickOK = delegate { };

        this.SendMessage("CloseSelf");
    }

    public void OnClickCancel() {
        this.SendMessage("CloseSelf");
    }
	
}

