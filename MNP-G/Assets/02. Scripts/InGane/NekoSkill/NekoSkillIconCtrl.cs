using UnityEngine;
using System.Collections;
using DG.Tweening;

public class NekoSkillIconCtrl : MonoBehaviour {


    [SerializeField]
    UISprite _iconSprite;

    [SerializeField]
    UILabel _iconLabel;

    [SerializeField]
    string _value;

    [SerializeField]
    NekoSkillType _skillType;

    [SerializeField]
    Vector3 _destPos;


    [SerializeField]
    WhiteLightCtrl _colorfulLight;

    private void SetIcon(NekoSkillType pType, float pValue) {

        _iconLabel.gameObject.SetActive(false);

        _skillType = pType;

        switch (_skillType) {
            case NekoSkillType.score_passive:
                _iconSprite.spriteName = PuzzleConstBox.listNekoSkillSprite[0];
                _destPos = PuzzleConstBox.listNekoSkillDestPos[0];
                _value = "+" + pValue.ToString() + "%";
                break;
            case NekoSkillType.coin_passive:
                _iconSprite.spriteName = PuzzleConstBox.listNekoSkillSprite[1];
                _destPos = PuzzleConstBox.listNekoSkillDestPos[1];
                _value = "+" + pValue.ToString() + "%";
                break;
            case NekoSkillType.time_passive:
                _iconSprite.spriteName = PuzzleConstBox.listNekoSkillSprite[2];
                _destPos = PuzzleConstBox.listNekoSkillDestPos[2];
                _value = "+" + pValue.ToString();
                break;
            case NekoSkillType.fevertime_passive:
                _iconSprite.spriteName = PuzzleConstBox.listNekoSkillSprite[3];
                _destPos = PuzzleConstBox.listNekoSkillDestPos[3];
                _value = "+" + pValue.ToString();
                break;
            case NekoSkillType.power_passive:
                _iconSprite.spriteName = PuzzleConstBox.listNekoSkillSprite[4];
                _destPos = PuzzleConstBox.listNekoSkillDestPos[4];
                _value = "+" + pValue.ToString() + "%";
                break;
            case NekoSkillType.yellowblock_score_passive:
                _iconSprite.spriteName = PuzzleConstBox.listNekoSkillSprite[5];
                _destPos = PuzzleConstBox.listNekoSkillDestPos[5];
                _value = "+" + pValue.ToString() + "%";
                break;
            case NekoSkillType.blueblock_score_passive:
                _iconSprite.spriteName = PuzzleConstBox.listNekoSkillSprite[6];
                _destPos = PuzzleConstBox.listNekoSkillDestPos[6];
                _value = "+" + pValue.ToString() + "%";
                break;
            case NekoSkillType.redblock_score_passive:
                _iconSprite.spriteName = PuzzleConstBox.listNekoSkillSprite[7];
                _destPos = PuzzleConstBox.listNekoSkillDestPos[7];
                _value = "+" + pValue.ToString() + "%";
                break;

            case NekoSkillType.yellowblock_appear_passive:
                _iconSprite.spriteName = PuzzleConstBox.listNekoSkillSprite[8];
                _destPos = PuzzleConstBox.listNekoSkillDestPos[8];
                _value = "+" + pValue.ToString() + "%";
                break;
            case NekoSkillType.blueblock_appear_passive:
                _iconSprite.spriteName = PuzzleConstBox.listNekoSkillSprite[9];
                _destPos = PuzzleConstBox.listNekoSkillDestPos[9];
                _value = "+" + pValue.ToString() + "%";
                break;
            case NekoSkillType.redblock_appear_passive:
                _iconSprite.spriteName = PuzzleConstBox.listNekoSkillSprite[10];
                _destPos = PuzzleConstBox.listNekoSkillDestPos[10];
                _value = "+" + pValue.ToString() + "%";
                break;
            case NekoSkillType.bomb_appear_passive:
                _iconSprite.spriteName = PuzzleConstBox.listNekoSkillSprite[11];
                _destPos = PuzzleConstBox.listNekoSkillDestPos[11];
                _value = "";
                break;
            case NekoSkillType.nekoskill_appear_passive:
                _iconSprite.spriteName = PuzzleConstBox.listNekoSkillSprite[12];
                _destPos = PuzzleConstBox.listNekoSkillDestPos[12];
                _value = "";
                break;
            case NekoSkillType.userexp_passive:
                _iconSprite.spriteName = PuzzleConstBox.listNekoSkillSprite[13];
                _destPos = PuzzleConstBox.listNekoSkillDestPos[13];
                _value = "+" + pValue.ToString() + "%";
                break;
            case NekoSkillType.random_bomb_active:
                _iconSprite.spriteName = PuzzleConstBox.listNekoSkillSprite[14];
                _destPos = PuzzleConstBox.listNekoSkillDestPos[14];
                _value = "+" + pValue.ToString();
                break;
            case NekoSkillType.combo_maintain_active:
                _iconSprite.spriteName = PuzzleConstBox.listNekoSkillSprite[15];
                _destPos = PuzzleConstBox.listNekoSkillDestPos[15];
                _value = "+" + pValue.ToString();
                break;
            case NekoSkillType.fever_raise_active:
                _iconSprite.spriteName = PuzzleConstBox.listNekoSkillSprite[16];
                _destPos = PuzzleConstBox.listNekoSkillDestPos[16];
                _value = "+" + pValue.ToString();
                break;
            case NekoSkillType.time_active:
                _iconSprite.spriteName = PuzzleConstBox.listNekoSkillSprite[17];
                _destPos = PuzzleConstBox.listNekoSkillDestPos[17];
                _value = "+" + pValue.ToString();
                break;
            case NekoSkillType.yellow_bomb_active:
                _iconSprite.spriteName = PuzzleConstBox.listNekoSkillSprite[18];
                _destPos = PuzzleConstBox.listNekoSkillDestPos[18];
                _value = "+" + pValue.ToString();
                break;

            case NekoSkillType.blue_bomb_active:
                _iconSprite.spriteName = PuzzleConstBox.listNekoSkillSprite[19];
                _destPos = PuzzleConstBox.listNekoSkillDestPos[19];
                _value = "+" + pValue.ToString();
                break;
            case NekoSkillType.red_bomb_active:
                _iconSprite.spriteName = PuzzleConstBox.listNekoSkillSprite[20];
                _destPos = PuzzleConstBox.listNekoSkillDestPos[20];
                _value = "+" + pValue.ToString();
                break;
            case NekoSkillType.black_bomb_active:
                _iconSprite.spriteName = PuzzleConstBox.listNekoSkillSprite[21];
                _destPos = PuzzleConstBox.listNekoSkillDestPos[21];
                _value = "+" + pValue.ToString();
                break;
        }

        _iconLabel.text = _value;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="pType"></param>
    /// <param name="pValue"></param>
    public void SetSkillIcon(NekoSkillType pType, float pValue) {
        SetIcon(pType, pValue);
    }

    public void SetSkillIcon(NekoSkillType pType, int pValue) {
        SetIcon(pType, pValue);

    }

    #region 패시브 스킬 

    /// <summary>
    /// 패시브 스킬의 게임 시작 연출시 위치 지정 
    /// </summary>
    /// <param name="pInitPos"></param>
    public void SetInitPos(Vector3 pInitPos) {
        InSoundManager.Instance.PlayPassivePing();
        this.gameObject.SetActive(true);
        this._iconLabel.gameObject.SetActive(false);
        

        this.transform.localScale = Vector3.zero;
        this.transform.localPosition = pInitPos;

        this.transform.DOScale(1, 1f).SetEase(Ease.OutBack).OnComplete(OnCompleteScale).SetDelay(0.5f);
    }

    private void OnCompleteScale() {
        // destPos 로 이동
        this.transform.DOLocalMove(_destPos, 0.5f).SetEase(Ease.OutBounce).OnComplete(OnCompleteMove);
    }

    private void OnCompleteMove() {

        this._iconLabel.gameObject.SetActive(true);
        this.transform.DOScale(0, 1.3f).SetEase(Ease.InBounce);

        _colorfulLight.Play();

    }

    #endregion


    #region 액티브 스킬 처리 

    public void SetActivePos(NekoSkillType pType, float pValue) {
        _skillType = pType;
        this.gameObject.SetActive(true);
        this._iconLabel.gameObject.SetActive(true);
        this.transform.localScale = new Vector3(1, 1, 1);
        _value = "+" + pValue.ToString();
        this._iconLabel.text = _value;

        if (_skillType == NekoSkillType.time_active) { // 타임 증가 
            this._iconSprite.spriteName = PuzzleConstBox.listNekoSkillSprite[17];

            // 좌에서 우로 이동 
            this.transform.localPosition = new Vector3(-700, PuzzleConstBox.listNekoSkillDestPos[17].y, 0);

            this.transform.DOLocalMoveX(PuzzleConstBox.listNekoSkillDestPos[17].x, 0.3f).OnComplete(OnCompleteFirstActiveMove);
        }
        else if (_skillType == NekoSkillType.fever_raise_active) { // 콤보 상승 
            this._iconSprite.spriteName = PuzzleConstBox.listNekoSkillSprite[16];
            // 좌에서 우로 이동 
            this.transform.localPosition = new Vector3(-700, PuzzleConstBox.listNekoSkillDestPos[16].y, 0);
            this.transform.DOLocalMoveX(PuzzleConstBox.listNekoSkillDestPos[16].x, 0.3f).OnComplete(OnCompleteFirstActiveMove);
        }
        else if (_skillType == NekoSkillType.combo_maintain_active) {

            this._iconSprite.spriteName = PuzzleConstBox.listNekoSkillSprite[15];

            // 좌에서 우로 이동 
            this.transform.localPosition = new Vector3(-700, PuzzleConstBox.listNekoSkillDestPos[15].y, 0);
            this.transform.DOLocalMoveX(PuzzleConstBox.listNekoSkillDestPos[15].x, 0.3f).OnComplete(() => OnCompleteNoMissTimeMove(pValue));
        }
        else if (_skillType == NekoSkillType.black_bomb_active || _skillType == NekoSkillType.yellow_bomb_active
            || _skillType == NekoSkillType.red_bomb_active || _skillType == NekoSkillType.blue_bomb_active
            || _skillType == NekoSkillType.random_bomb_active) {

            this._iconLabel.text = "";

            if (_skillType == NekoSkillType.yellow_bomb_active)
                this._iconSprite.spriteName = PuzzleConstBox.listNekoSkillSprite[18];
            if (_skillType == NekoSkillType.blue_bomb_active)
                this._iconSprite.spriteName = PuzzleConstBox.listNekoSkillSprite[19];
            if (_skillType == NekoSkillType.red_bomb_active)
                this._iconSprite.spriteName = PuzzleConstBox.listNekoSkillSprite[20];
            if (_skillType == NekoSkillType.black_bomb_active)
                this._iconSprite.spriteName = PuzzleConstBox.listNekoSkillSprite[21];
            if (_skillType == NekoSkillType.random_bomb_active)
                this._iconSprite.spriteName = PuzzleConstBox.listNekoSkillSprite[14];


            // 좌에서 우로 이동 
            this.transform.localPosition = new Vector3(-700, PuzzleConstBox.listNekoSkillDestPos[21].y, 0);
            this.transform.DOLocalMoveX(PuzzleConstBox.listNekoSkillDestPos[21].x, 0.3f).OnComplete(OnCompleteFirstActiveMove);
        }
        


    }

    private void OnCompleteFirstActiveMove() {
        this._iconLabel.gameObject.SetActive(true);
        this.transform.DOLocalMoveX(800, 0.3f).SetDelay(0.5f);
    }

    private void OnCompleteNoMissTimeMove(float pValue) {
        this._iconLabel.gameObject.SetActive(true);
        this.transform.DOLocalMoveX(800, 0.3f).SetDelay(pValue);
    }

    #endregion



}


