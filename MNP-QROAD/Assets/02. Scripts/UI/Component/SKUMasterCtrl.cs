using UnityEngine;
using System.Collections;

public class SKUMasterCtrl : MonoBehaviour {

	[SerializeField] SKUCtrl[] _arrSKUCtrl;

     // Use this for initialization
    void Start () {
	
	}

    public void OpenURL() {
        Application.OpenURL("https://mnpop.x-legend.co.jp/sp/faq/");
    }

	public void SetSKUs() {

#if UNITY_ANDROID

        foreach (GoogleProductTemplate p in AndroidInAppPurchaseManager.Client.Inventory.Products) {
            
            if(p.SKU.Contains("t0")) {
                _arrSKUCtrl[0].SetSKUInfo(p.SKU, p.LocalizedPrice);
            }
            else if(p.SKU.Contains("g1")) {
                _arrSKUCtrl[1].SetSKUInfo(p.SKU, p.LocalizedPrice);
            }
            else if (p.SKU.Contains("g2")) {
                _arrSKUCtrl[2].SetSKUInfo(p.SKU, p.LocalizedPrice);
            }
            else if (p.SKU.Contains("g3")) {
                _arrSKUCtrl[3].SetSKUInfo(p.SKU, p.LocalizedPrice);
            }
            else if (p.SKU.Contains("g4")) { 
                _arrSKUCtrl[4].SetSKUInfo(p.SKU, p.LocalizedPrice);
            }
            else if (p.SKU.Contains("g5")) {
                _arrSKUCtrl[5].SetSKUInfo(p.SKU, p.LocalizedPrice);
            }

        }
#elif UNITY_IOS
		Debug.Log("SetSKUs in IOS");

        foreach (IOSProductTemplate tpl in IOSInAppPurchaseManager.Instance.Products) {
            if (tpl.Id.Contains("t0")) {
                _arrSKUCtrl[0].SetSKUInfo(tpl.Id, tpl.LocalizedPrice);
            }
            else if (tpl.Id.Contains("g1")) {
                _arrSKUCtrl[1].SetSKUInfo(tpl.Id, tpl.LocalizedPrice);
            }
            else if (tpl.Id.Contains("g2")) {
                _arrSKUCtrl[2].SetSKUInfo(tpl.Id, tpl.LocalizedPrice);
            }
            else if (tpl.Id.Contains("g3")) {
                _arrSKUCtrl[3].SetSKUInfo(tpl.Id, tpl.LocalizedPrice);
            }
            else if (tpl.Id.Contains("g4")) {
                _arrSKUCtrl[4].SetSKUInfo(tpl.Id, tpl.LocalizedPrice);
            }
        else if (tpl.Id.Contains("g5")) {
                _arrSKUCtrl[5].SetSKUInfo(tpl.Id, tpl.LocalizedPrice);
            }
        }
#endif


    }

}
