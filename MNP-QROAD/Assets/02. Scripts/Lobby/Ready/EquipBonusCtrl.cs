using UnityEngine;
using System.Collections;

public class EquipBonusCtrl : MonoBehaviour {

	[SerializeField] UILabel _lblBonusTitle;
    [SerializeField] UILabel _lblBonusValue;

    [SerializeField] UILabel _lblBonusTitleShadow1;
    [SerializeField] UILabel _lblBonusTitleShadow2;


    /// <summary>
    /// 
    /// </summary>
    /// <param name="pSkillID"></param>
    /// <param name="pValue"></param>
    public void SetEquipBonus(int pSkillID, float  pValue) {

        this.gameObject.SetActive(true);

        _lblBonusTitle.text = GetBonusTitle(pSkillID);
        _lblBonusTitleShadow1.text = _lblBonusTitle.text;
        _lblBonusTitleShadow2.text = _lblBonusTitle.text;

        _lblBonusValue.text = "+" + string.Format("{0:n0}", pValue); 

        if (pSkillID == 1 || pSkillID == 2 || pSkillID == 3) {
            _lblBonusValue.text += "%";
        }

    }


    public void SetPosition(Vector3 pLocalPos) {
        this.transform.localPosition = pLocalPos;
    }

    private string GetBonusTitle(int pSkillID) {

        switch(pSkillID) {
            case 1:
                return GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3978);
            case 2:
                return GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3979);
            case 3:
                return GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3980);
            case 4:
                return GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3981);
            case 5:
                return GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3982);
            case 6:
                return GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3983);
            case 7:
                return GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3984);
                /*
            case 8:
                return GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3978);
            case 9:
                return GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3978);
            case 10:
                return GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3978);
            case 11:
                return GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3978);
            case 12:
                return GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3978);
                */

            default:
                return string.Empty;
        }

    }

}
