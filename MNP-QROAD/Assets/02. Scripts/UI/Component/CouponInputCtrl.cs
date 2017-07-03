using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CouponInputCtrl : MonoBehaviour {


    [SerializeField] UIInput inputCoupon;
    [SerializeField] UILabel _lblInput;
    [SerializeField] string _couponInfo;


    void OnEnable() {
        _lblInput.text = GameSystem.Instance.GetLocalizeText(3115);
        //_input.value = GameSystem.Instance.GetLocalizeText(3099);

    }

    public void OpenCouponInput() {

        inputCoupon.value = string.Empty;
        
        this.gameObject.SetActive(true);
    }

    public void SendCoupon() {

        Debug.Log(">>> SendCoupon ipt value :: " + inputCoupon.value);

        _couponInfo = string.Empty;


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
