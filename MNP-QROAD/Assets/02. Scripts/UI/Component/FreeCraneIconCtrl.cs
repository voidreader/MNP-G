using UnityEngine;
using System.Collections;
using DG.Tweening;

public class FreeCraneIconCtrl : MonoBehaviour {

    
    [SerializeField] UISprite _sign;

    public void Play() {

        
        if (this.gameObject.activeSelf) {
            return;
        }


        this.gameObject.SetActive(true);

        /*
        _sign.transform.DOKill();
        _sign.transform.localScale = GameSystem.Instance.BaseScale;
        _sign.transform.DOScale(1.1f, 0.5f).SetLoops(-1, LoopType.Yoyo);
        */

        

    }

    /// <summary>
    /// 
    /// </summary>
    public void OpenFreeCrane() {
        WindowManagerCtrl.Instance.OpenGatchaConfirm(true);
    }


}
