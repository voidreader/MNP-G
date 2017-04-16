using UnityEngine;
using System.Collections;
using DG.Tweening;

public class FreeCraneIconCtrl : MonoBehaviour {

    
    [SerializeField] UISprite _sign;

    public void Play() {

        
        if (this.gameObject.activeSelf) {
            SetSign();
            return;
        }


        this.gameObject.SetActive(true);

        SetSign();
        _sign.transform.DOKill();
        _sign.transform.localScale = GameSystem.Instance.BaseScale;
        _sign.transform.DOScale(1.1f, 0.5f).SetLoops(-1, LoopType.Yoyo);

        

    }

    public void SetSign() {
        /*
        switch(GameSystem.Instance.Remainfreegacha) {
            case 1:
                _sign.spriteName = "nokori-1";
                break;

            case 2:
                _sign.spriteName = "nokori-2";
                break;

                
            case 3:
                _sign.spriteName = "nokori-3";
                break;

        }
        */
    }



    /// <summary>
    /// 
    /// </summary>
    public void OpenFreeCrane() {
        WindowManagerCtrl.Instance.OpenGatchaConfirm(true);
    }


}
