using UnityEngine;
using System.Collections;

public class OptionSubPopCtrl : MonoBehaviour {

    [SerializeField]
    GameObject _groupLang;

    void InitGroup() {
        _groupLang.SetActive(false);
    }

	public void OpenLanguageList() {
        this.gameObject.SetActive(true);

        _groupLang.SetActive(true);
    }



    #region 버튼 기능 모음

    public void ChangeLangToEnglish() {
        LobbyCtrl.Instance.OpenInfoPopUp(PopMessageType.ChangeLang, OnCompleteChangeEnglish);
    }

    void OnCompleteChangeEnglish() {
        GameSystem.Instance.GameLanguage = SystemLanguage.English;
        SaveLanguage(SystemLanguage.English);
    }


    public void ChangeLangToKorean() {
        LobbyCtrl.Instance.OpenInfoPopUp(PopMessageType.ChangeLang, OnCompleteChangeKorean);
    }

    void OnCompleteChangeKorean() {
        GameSystem.Instance.GameLanguage = SystemLanguage.Korean;
        SaveLanguage(SystemLanguage.Korean);
    }


    /// <summary>
    /// 언어 정보 저장 
    /// </summary>
    /// <param name="pLang"></param>
    void SaveLanguage(SystemLanguage pLang) {
        ES2.Save<SystemLanguage>(pLang, PuzzleConstBox.ES_Language);

        // 배너를 다시 받기 위해서 정보를 초기화 
        GameSystem.Instance.InitLocalDataVersion();


        LoadTitleScene();
    }

    /// <summary>
    /// 타이틀 씬으로 이동 
    /// </summary>
    void LoadTitleScene() {
        Fader.Instance.FadeIn(0.5f).LoadLevel("SceneTitle").FadeOut(1f);
    }

    #endregion


}
