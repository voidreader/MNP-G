using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CouponInputCtrl : MonoBehaviour {


    [SerializeField] UIInput inputCoupon;
    [SerializeField] string _couponInfo;


    public void OpenCouponInput() {
        this.gameObject.SetActive(true);
    }

    public void SendCoupon() {

        if (string.IsNullOrEmpty(inputCoupon.value)) {
            LobbyCtrl.Instance.OpenInfoPopUp(PopMessageType.CouponInput);
            return;
        }


        _couponInfo = inputCoupon.value.ToUpper();


        
        GameSystem.Instance.Post2UseCoupon(_couponInfo);

        // CloseSelf 하고 
        // this.gameObject.SendMessage("CloseSelf");
    }
}
