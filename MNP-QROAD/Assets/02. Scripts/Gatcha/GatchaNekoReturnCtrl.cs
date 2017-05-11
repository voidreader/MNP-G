using UnityEngine;
using System.Collections;
using DG.Tweening;

public class GatchaNekoReturnCtrl : MonoBehaviour {

    [SerializeField] GameObject _group;
    [SerializeField] GameObject _stars;
    [SerializeField] UISprite _fishSprite;
    [SerializeField] UILabel _lblFishValue;


    [SerializeField] GameObject _gemSprite;
    [SerializeField] UILabel _lblGemValue;

    
    


    [SerializeField] Transform _bigNew;
    

    [SerializeField] Transform _arrow;


    void OnEnable() {
        _group.SetActive(false);
        _stars.SetActive(false);

        _bigNew.gameObject.SetActive(false);
        
    }


    /// <summary>
    /// 신규 네코를 표시 (new 마크)
    /// </summary>
    /// <param name="pIsMain"></param>
    public void SetNewNekoMark(bool pIsMain) {
        if(pIsMain) {
            _bigNew.gameObject.SetActive(true);
            _bigNew.DOKill();
            _bigNew.localScale = GameSystem.Instance.BaseScale;
            _bigNew.DOScale(new Vector3(1.1f, 1.1f, 1), 0.5f).SetLoops(-1, LoopType.Yoyo);
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
    public void SetExtraInfo(FishType pFish, int pFishValue, string pGetType, int pGemValue) {
        _group.SetActive(true);
        _group.transform.DOKill();

        this._gemSprite.SetActive(false);

        SetFish(pFish);


        // 생선값 세팅 
        _lblFishValue.text = "+" + pFishValue.ToString();

        if(pGetType == "special") {
            _lblGemValue.text = "+" + pGemValue;
            this._gemSprite.SetActive(true);

        }

        _group.transform.DOScale(1.1f, 0.5f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);

    }


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


        //_fishSprite.transform.DOScale(1.1f, 0.5f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
        

    }
}
