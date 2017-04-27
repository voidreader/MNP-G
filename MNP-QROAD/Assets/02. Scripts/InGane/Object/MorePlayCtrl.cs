using UnityEngine;
using System.Collections;
using DG.Tweening;

public class MorePlayCtrl : MonoBehaviour {


    [SerializeField] UILabel _lblCurrentScoreValue;
    [SerializeField] UILabel _lblBestScoreValue;

    [SerializeField] UILabel _lblPlayerGemValue;


    /// <summary>
    /// 10초 더 화면 오픈 
    /// </summary>
    public void SetMorePlayWindow() {

        this.gameObject.SetActive(true);

        _lblBestScoreValue.transform.DOKill();
        _lblBestScoreValue.transform.localScale = GameSystem.Instance.BaseScale;
        _lblBestScoreValue.transform.DOScale(1.2f, 0.3f).SetLoops(-1, LoopType.Yoyo);


        _lblBestScoreValue.text = GameSystem.Instance.GetNumberToString(GameSystem.Instance.UserBestScore);
        _lblCurrentScoreValue.text = GameSystem.Instance.GetNumberToString(GameSystem.Instance.InGameTotalScore);
        _lblPlayerGemValue.text = GameSystem.Instance.GetNumberToString(GameSystem.Instance.UserGem);

    }


}
