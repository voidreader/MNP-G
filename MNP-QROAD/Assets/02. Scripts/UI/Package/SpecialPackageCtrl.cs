using UnityEngine;
using System.Collections;

public class SpecialPackageCtrl : MonoBehaviour {

    [SerializeField]
    UILabel _lblPrice;

    string sku = string.Empty;

    public void SetPackageDetail(string pID, string pPrice) {

        Debug.Log("★ SetPackageDetail pID:: " + pID);
        Debug.Log("★ SetPackageDetail pPrice:: " + pID);
        sku = pID;

        this.gameObject.SetActive(true);
        _lblPrice.text = pPrice;

    }

    public void Buy() {
        GameSystem.Instance.PurchaseInAppPackage(sku, PuzzleConstBox.packageSpecial);
    }
}
