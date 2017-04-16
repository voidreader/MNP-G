using UnityEngine;
using System.Collections;
using DG.Tweening;
using SimpleJSON;

public class GNekoTenCtrl : MonoBehaviour {

    [SerializeField] UISprite _nekoSprite;
    [SerializeField] UIButton _btn;
    [SerializeField] UISprite _baseSprite;

    [SerializeField]
    UILabel _lblGrade;
    [SerializeField]
    Transform _aura;

    [SerializeField]
    int _tempStar;

    NekoGatchaCtrl _base;
    int _nekoIndex;

    bool _isFusion = false;

	// Use this for initialization
	void Start () {
	
	}
	
    /// <summary>
    /// 
    /// </summary>
    /// <param name="pIndex"></param>
    /// <param name="pNekoID"></param>
    /// <param name="pGrade"></param>
	public void SetNeko(int pIndex, int pNekoID, int pGrade, NekoGatchaCtrl pBase) {
        GameSystem.Instance.SetNekoSprite(_nekoSprite, pNekoID, pGrade);
        _btn.normalSprite = _nekoSprite.spriteName;
        _base = pBase;

        _nekoIndex = pIndex;
        _tempStar = pGrade;

        _baseSprite.spriteName = "cat-base-br";
    }



    public void SetNeko(int pIndex, JSONNode pNode, NekoGatchaCtrl pBase) {

        _lblGrade.gameObject.SetActive(false);
        _aura.gameObject.SetActive(false);

        _tempStar = pNode["star"].AsInt;


        // 소유한 네코와 비교해서, 신규 등급으로 적용 
        if (pNode["isFusion"].AsInt == 0) {
            // 신규네코. 
            _baseSprite.spriteName = "cat-base-tr";
            _isFusion = false;
        }
        else { // 퓨전의 경우 체크

            _isFusion = true;
            _baseSprite.spriteName = "cat-base-br";

        }

        // Sprite 처리 
        GameSystem.Instance.SetNekoSprite(_nekoSprite, pNode["tid"].AsInt, _tempStar);
        _btn.normalSprite = _nekoSprite.spriteName;

        _base = pBase;
        _nekoIndex = pIndex;

    }

    /// <summary>
    /// 마지막 네코 강조 처리 
    /// </summary>
    public void SetLastNeko() {
        _lblGrade.text = GameSystem.Instance.GetNekoGradeText(_tempStar);
        _lblGrade.gameObject.SetActive(true);
        _lblGrade.transform.DOKill();
        _lblGrade.transform.localPosition = new Vector3(0, 70, 0);
        _lblGrade.transform.DOLocalJump(_lblGrade.transform.localPosition, 50, 1, 0.5f);

        _aura.gameObject.SetActive(true);
        _aura.DOKill();
        _aura.localEulerAngles = Vector3.zero;
        _aura.DOLocalRotate(new Vector3(0, 0, 360), 2, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);


        LobbyCtrl.Instance.PlayEffect(SoundConstBox.acStarJump);
    }

    public void OnClickNeko() {
        _base.SetMainNeko(_nekoIndex);
    }
}
