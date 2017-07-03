using UnityEngine;
using System.Collections;

public class NekoInfoCtrl : MonoBehaviour {

    [SerializeField]
    UISprite _nekoSprite;

    [SerializeField]
    UILabel _nekoName;

    [SerializeField]
    UILabel _nekoDetail;

    int _nekoID, _nekoGrade;

    [SerializeField] GameObject _btnMain;
    [SerializeField] GameObject _lblMain;


    public void SetNekoInfo(int pNekoID, int pNekoGrade) {

        _nekoID = pNekoID;
        _nekoGrade = pNekoGrade;

        //_nekoSprite.gameObject.SetActive(true);
        GameSystem.Instance.SetNekoSprite(_nekoSprite, pNekoID, pNekoGrade);


        // 이름, 설명 세팅 
        _nekoName.text = GameSystem.Instance.GetNekoName(pNekoID, pNekoGrade);
        _nekoDetail.text = GameSystem.Instance.GetNekoDetail(pNekoID, pNekoGrade);

        this.gameObject.SetActive(true);
    }

    public void ShareFB() {
        WindowManagerCtrl.Instance.ShareNekoFB(_nekoID, _nekoGrade);

        this.GetComponent<LobbyCommonUICtrl>().CloseSelf();
    }


    /// <summary>
    /// 메인 고양이 설정 
    /// </summary>
    /// <param name="pFlag"></param>
    public void CheckMain(bool pFlag) {
        if(pFlag) {
            _btnMain.gameObject.SetActive(false);
            _lblMain.gameObject.SetActive(true);
        }
        else {
            _btnMain.gameObject.SetActive(true);
            _lblMain.gameObject.SetActive(false);
        }
    }

}
