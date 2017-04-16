using UnityEngine;
using System.Collections;

public class GatchaNekoCtrl : MonoBehaviour {

    [SerializeField] private NekoAppearCtrl _appear; // 외향(NGUI)
    [SerializeField] int _nekoID;


    public void SetGatchaNeko(int pNekoID, int pGrade) {

        if(_appear == null) {
            _appear = LobbyCtrl.Instance.GatchaNekoAppear;
        }

        _nekoID = pNekoID;
        _appear.InitNekoAppear(this.transform, _nekoID, pGrade, false);
    }


    public void SetVisible(bool pValue) {

        Debug.Log("GatchaNeko SetVisible :: " + pValue.ToString());

        if(_appear == null) {
            _appear = LobbyCtrl.Instance.GatchaNekoAppear;
        }

        _appear.gameObject.SetActive(pValue);

        this.gameObject.SetActive(pValue);
    }
}
