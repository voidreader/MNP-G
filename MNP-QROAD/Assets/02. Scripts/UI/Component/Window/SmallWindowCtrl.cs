using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class SmallWindowCtrl : MonoBehaviour {

    Action OnComfirm = delegate { };

    [SerializeField] UILabel _lblMessage;

    public void OpenMessage(PopMessageType pType, Action pAction) {
        OnComfirm = delegate { };
        this.gameObject.SetActive(true);

        OnComfirm += pAction;

        if(pType == PopMessageType.RuntimePermissionDenied) {
            _lblMessage.text = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3512);
        }
            

    }

    public void Confirm() {
        OnComfirm();
        this.gameObject.SetActive(false);
    }


}
