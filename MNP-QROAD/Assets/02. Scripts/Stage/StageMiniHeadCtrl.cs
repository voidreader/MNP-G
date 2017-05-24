using UnityEngine;
using DG.Tweening;

public class StageMiniHeadCtrl : MonoBehaviour {

    [SerializeField] UISprite _miniSprite;
    [SerializeField] int _nekoID;

    [SerializeField] Transform _drop;

    [SerializeField]
    Vector3 _destPos;
    

    /// <summary>
    /// 스테이지 구출대상 미니사이즈 세팅 
    /// </summary>
    /// <param name="pID"></param>
    /// <param name="pSprite"></param>
    public void SetMiniSpriteByID(int pID, string pSprite) {

        _nekoID = pID;

        _miniSprite.spriteName = pSprite;
           
       
        //_miniSprite.transform.DOLocalRotate(new Vector3(0, 0, 10), 0.5f, RotateMode.Fast).SetLoops(-1, LoopType.Yoyo);
    }


    public void PlayClearJump() {

        this.gameObject.SetActive(true);

        // 스프라이트 변경 
        GameSystem.Instance.SetNekoMiniSpriteByID(_miniSprite, _nekoID);
        _miniSprite.MakePixelPerfect();

        _destPos = this.transform.localPosition;


        // Jump
        this.transform.DOLocalJump(_destPos, 50, 1, 0.6f);

    }
    
	
}
