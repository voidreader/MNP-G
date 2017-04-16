using UnityEngine;
using System.Collections;
using DG.Tweening;


public class MissionClearDirctCtrl : MonoBehaviour {


    public Transform _base;
    public UILabel _lblValue;
    public UILabel _lblInfo;
    public UISprite _icon;

    string _infoText = string.Empty;

    void OnEnable() {
        _base.DOKill();
        _base.localEulerAngles = Vector3.zero;


        _base.DOLocalRotate(new Vector3(0, 0, -360), 2, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
    }

    /// <summary>
    /// 
    /// </summary>
    public void SetMissionDirect(string pType, int pValue) {

        this.gameObject.SetActive(true);

        if(pType == "gem") {
            _icon.spriteName = "top-dia";
            _infoText = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3226) + " ";
        }
        else if(pType == "coin") {
            _icon.spriteName = "top-coin";
            _infoText = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3225) + " ";
        }

        _infoText += GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3253);

        _lblValue.text = "x" + pValue.ToString();
        _lblInfo.text = _infoText.Replace("[n]", pValue.ToString());


        Invoke("OffDirecting", 1.5f);
    
    }


    /// <summary>
    /// 
    /// </summary>
    private void OffDirecting() {
        this.gameObject.SetActive(false);
    }
}
