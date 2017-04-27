using UnityEngine;
using System.Collections;
using DG.Tweening;

public class GatchaNekoReturnCtrl : MonoBehaviour {

    [SerializeField] GameObject _group;
    [SerializeField] GameObject _stars;
    [SerializeField] UISprite _fishSprite;
    [SerializeField] UILabel _lblFishValue;
    [SerializeField] UISprite _miniNekoSprite;
    [SerializeField] UILabel _lblPreGrade;


    [SerializeField] Transform _bigNew;
    [SerializeField] Transform _smallNew;

    [SerializeField] Transform _arrow;


    void OnEnable() {
        _group.SetActive(false);
        _stars.SetActive(false);

        _bigNew.gameObject.SetActive(false);
        _smallNew.gameObject.SetActive(false);
    }


    /// <summary>
    /// 신규 네코를 표시 (new 마크)
    /// </summary>
    /// <param name="pIsMain"></param>
    public void SetNewNekoMark(bool pIsMain) {
        if(pIsMain) {
            _bigNew.gameObject.SetActive(true);
            _smallNew.gameObject.SetActive(false);

            _bigNew.DOKill();
            _bigNew.localScale = GameSystem.Instance.BaseScale;
            _bigNew.DOScale(new Vector3(1.1f, 1.1f, 1), 0.5f).SetLoops(-1, LoopType.Yoyo);
        }
        else {
            /*
            _smallNew.gameObject.SetActive(true);
            _bigNew.gameObject.SetActive(false);

            _smallNew.DOKill();
            _smallNew.localScale = GameSystem.Instance.BaseScale;
            _smallNew.DOScale(new Vector3(1.1f, 1.1f, 1), 0.5f).SetLoops(-1, LoopType.Yoyo);
            */
        }
    }

    /// <summary>
    /// 추가 정보 표현 
    /// </summary>
    /// <param name="pFish"></param>
    /// <param name="pNekoID"></param>
    /// <param name="pSacrificeGrade"></param>
    /// <param name="pFishValue"></param>
    /// <param name="pMain"></param>
    public void SetExtraInfo(FishType pFish, int pNekoID, int pSacrificeGrade, int pFishValue, bool pMain) {
        _group.SetActive(true);

        SetFish(pFish);
        _lblFishValue.text = "+" + pFishValue.ToString();
        GameSystem.Instance.SetNekoSprite(_miniNekoSprite, pNekoID, pSacrificeGrade);
        _lblPreGrade.text = GameSystem.Instance.GetNekoGradeText(pSacrificeGrade);

        // Main의 여부에 따라서 Arrow의 위치 변경 
        if (!pMain) // 서브의 위치 
            _arrow.localPosition = Vector3.zero;
        else { // 메인에 위치 
            _arrow.localPosition = new Vector3(65, 100, 0);

        }
    }

    /*
    /// <summary>
    /// 
    /// </summary>
    /// <param name="pFish"></param>
    /// <param name="pNekoID"></param>
    /// <param name="pNekoGrade"></param>
    /// <param name="pFishValue"></param>
    public void SetExtraInfo(FishType pFish, int pNekoID, int pNekoGrade, int pFishValue, int pPreGrade) {

        _group.SetActive(true);
        //_stars.SetActive(false);

        SetFish(pFish);
        _lblFishValue.text = "+" + pFishValue.ToString();
        GameSystem.Instance.SetNekoSprite(_miniNekoSprite, pNekoID, pNekoGrade);

        _lblPreGrade.text = GameSystem.Instance.GetNekoGradeText(pPreGrade);

    }
    */

    public void EnableStars() {

        Debug.Log("MainNeko EnableStars");

        _stars.SetActive(true);
    }

    


    public void SetFish(FishType pFish) {

        _fishSprite.transform.localScale = GameSystem.Instance.BaseScale;
        _fishSprite.gameObject.SetActive(true);

        if(pFish == FishType.Chub) {
            _fishSprite.spriteName = "fish-go-b";
        }
        else if (pFish == FishType.Tuna) {
            _fishSprite.spriteName = "fish-tuna-b";
        }
        else if (pFish == FishType.Salmon) {
            _fishSprite.spriteName = "fish-salmon-b";
        }


        _fishSprite.transform.DOScale(1.2f, 0.5f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);

    }
}
