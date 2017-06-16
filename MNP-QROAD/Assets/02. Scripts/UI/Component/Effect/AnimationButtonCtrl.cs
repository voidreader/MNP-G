using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AnimationButtonCtrl : MonoBehaviour {

    [SerializeField] string _id = string.Empty; // 어느 용도에 쓰이는지 알기 위해 사용 
    [SerializeField] UISpriteAnimation _spriteAnimation;
    [SerializeField] UIButton _btn;

    [SerializeField] GameObject _objUp;
    [SerializeField] UISprite _spriteUpEffect;
    [SerializeField] UILabel _lblUpCount;

    readonly string spriteDiamond = "top-dia";
    readonly string clipDiamond = "top-dia-f";
    readonly string spriteQ = "common_btn_question";

    void Init(string pID) {

        _id = pID;
        _objUp.SetActive(false);
        _objUp.transform.localPosition = new Vector3(0, 30, 0);

        
    }

    /// <summary>
    /// 다이아몬드 애니메이션 버튼 
    /// </summary>
    public void SetDiamondButton(string pID) {

        Init(pID);
        
        _btn.normalSprite = spriteDiamond;

        _spriteAnimation.enabled = true;
        _spriteAnimation.namePrefix = clipDiamond;
    }

    /// <summary>
    /// 일반 Q 버튼 
    /// </summary>
    public void SetGeneralQButton(string pID) {

        Init(pID);

        
        _spriteAnimation.enabled = false;


        // 일반 물음표 버튼
        _btn.normalSprite = spriteQ;
    }


    void OnClick() {

        if(_btn.normalSprite == spriteDiamond) {
            // 통신 필요 
            GameSystem.Instance.Post2Unlock("inactive_power_tip", OnClickDiamondAnimationEvent);
        }
        else {
            OpenTargetPopUp();
        }

    }


    /// <summary>
    /// 대상 버튼에 맞는 팝업 오픈 
    /// </summary>
    void OpenTargetPopUp() {
        switch(_id) {
            case "inactive_power_tip":

                //
                LobbyCtrl.Instance.GameTip.SetGameTip(TipType.PassivePower);

                break;
        }
    }


    /// <summary>
    /// 애니메이션 버튼 상태일때 터치시, 
    /// </summary>
    public void OnClickDiamondAnimationEvent() {
        _spriteUpEffect.spriteName = spriteDiamond;
        _lblUpCount.text = "+" + GameSystem.Instance.GetNumberToString(30); // 30으로 고정 

        _objUp.SetActive(true);
        _objUp.transform.DOLocalMoveY(60, 1).OnComplete(OnCompleteUpMove);
        _objUp.transform.DOScale(1.2f, 0.45f).SetLoops(2, LoopType.Yoyo);

        //LobbyCtrl.Instance.PlayNekoRewardGet();

    }


    void OnCompleteUpMove() {
        _objUp.SetActive(false);
        this.SetGeneralQButton(_id);

        OpenTargetPopUp();
    }



}
