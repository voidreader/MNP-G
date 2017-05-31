using UnityEngine;
using System.Collections;

public class GSceneTipCtrl : MonoBehaviour {

    public UILabel _lblTip;

    public static System.Action OnCompletePostStart = delegate { };

    /// <summary>
    /// 
    /// </summary>
    public void SetSceneTip() {
        this.gameObject.SetActive(true);

        _lblTip.text = GetTipText();

        Fader.Instance.FadeOut(0.5f);

        StartCoroutine(WaitingPost());

    }

    public void CloseSceneTip() {
        // Fader.Instance.FadeOut(0.5f);
        this.gameObject.SetActive(false);

    }

    string GetTipText() {
        int id = Random.Range(0, 9);

        switch(id) {

            case 0:
                return GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L4251);
            case 1:
                return GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L4252);
            case 2:
                return GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L4253);
            case 3:
                return GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L4254);
            case 4:
                return GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L4255);
            case 5:
                return GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L4256);
            case 6:
                return GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L4257);
            case 7:
                return GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L4258);
            case 8:
                return GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L4259);

            default:
                return GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L4251);
        }
    }

    IEnumerator WaitingPost() {
        yield return new WaitForSeconds(2);

        // 결과 통신 받을때까지 기다린다. 
        while (!ReadyGroupCtrl.Instance.OnStartingGame) {
            yield return new WaitForSeconds(0.1f);
        }

        OnCompletePostStart();
        OnCompletePostStart = delegate { };
    }
}
