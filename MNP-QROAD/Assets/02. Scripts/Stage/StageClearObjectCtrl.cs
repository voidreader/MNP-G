using UnityEngine;
using System.Collections;
using DG.Tweening;

public class StageClearObjectCtrl : MonoBehaviour {

    public UILabel _lblStage;
    public UILabel _lblClearStage;

    public UISprite _sprite;
    public int _stage;
    StageBaseCtrl _base;


    UISpriteAnimation _animation;

    
    /// <summary>
    /// Current Stage 표시 Sprite 설정 (Theme마다 다르다)
    /// </summary>
    /// <param name="pSprite"></param>
    public void SetCurrentStageSprite(string pSprite) {
        this.GetComponent<UIButton>().normalSprite = pSprite;
    }

    public void SetStageLabel(int pStage, StageBaseCtrl pBase) {

        _lblClearStage.gameObject.SetActive(false);

        _stage = pStage;
        _lblStage.text = pStage.ToString();
        _lblClearStage.text = pStage.ToString();

        _base = pBase;
             
    }

    

    /// <summary>
    /// 스테이지의 클리어 상태 처리 (Bronze, Silver, Gold ,Dia)
    /// </summary>
    public void SetStageClearProgress(int pState) {

        this.gameObject.SetActive(true);

        InitClearStageAppear(pState);


    }

    /// <summary>
    /// 상태에 따른 외향 초기화 
    /// </summary>
    /// <param name="pState"></param>
    void InitClearStageAppear(int pState) {

        this._lblStage.gameObject.SetActive(false);
        this._lblClearStage.gameObject.SetActive(true);

        switch (pState) {
            case 0:
                this.gameObject.SetActive(false);
                break;

            case 1:
                _sprite.spriteName = PuzzleConstBox.spriteBronzeClear;
                _lblClearStage.color = PuzzleConstBox.colorBronzeClear;
                break;

            case 2:
                _sprite.spriteName = PuzzleConstBox.spriteSilverClear;
                _lblClearStage.color = PuzzleConstBox.colorSilverClear;
                break;

            case 3:
                _sprite.spriteName = PuzzleConstBox.spriteGoldClear;
                _lblClearStage.color = PuzzleConstBox.colorGoldClear;
                break;

            case 4:
                _sprite.spriteName = PuzzleConstBox.spriteDiaClear;
                _lblClearStage.color = PuzzleConstBox.colorDiaClear;
                break;
        }
    }

    /// <summary>
    /// 클리어 연출
    /// </summary>
    public void SetCleatEffect(int pState) {

        InitClearStageAppear(pState);

        this.gameObject.SetActive(true);
        this._lblClearStage.gameObject.SetActive(false);

        StartCoroutine(ClearingEffect());
    }


    IEnumerator ClearingEffect() {

        
        this.GetComponent<UISprite>().enabled = false;
        _lblStage.gameObject.SetActive(false);

        yield return new WaitForSeconds(1);

        // 클리어 마크 등장. 
        this.transform.localScale = new Vector3(2, 2, 2);
        this.GetComponent<UISprite>().enabled = true;
        _lblClearStage.gameObject.SetActive(true);

        this.transform.DOScale(1, 0.3f).OnComplete(OnCompleteClearingEffect);

        // 사운드 
       
    }

    private void OnCompleteClearingEffect() {

        Debug.Log("★OnCompleteClearingEffect");

        // 클리어 위치에 폭파 연출 및 글자 연출 
        _base.OnStageClearBreakEffect(this.transform.localPosition);



        // Sprite 애니메이션 연출
        /*
        _animation = this.gameObject.AddComponent<UISpriteAnimation>();

        _animation.loop = false;
        
        _animation.framesPerSecond = 15;
        _animation.snap = false;
        _animation.namePrefix = "stage-clear-wing-f";
        _animation.Play();
        */
    }

    

}
