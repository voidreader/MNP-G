using UnityEngine;
using DG.Tweening;

public class StageClearCommentCtrl : MonoBehaviour {

    [SerializeField] UISprite _spRewardIcon;
    [SerializeField] UILabel _lblRewardValue;

    [SerializeField] UILabel _lblComment;

    [SerializeField]
    int _currentTheme = 0;

    int _stars;

    [SerializeField]
    bool _onMove = false;



    /// <summary>
    /// 
    /// </summary>
    /// <param name="pTheme"></param>
    /// <param name="pStars"></param>
    public void SetThemeClearComment(int pTheme, int pStars) {

        this.transform.localPosition = new Vector3(-700, 0, 0);
        this.gameObject.SetActive(true);
        _onMove = false;
        _currentTheme = pTheme;
        _stars = pStars;

        _spRewardIcon.gameObject.SetActive(true);
        _lblRewardValue.gameObject.SetActive(true);

        _spRewardIcon.spriteName = PuzzleConstBox.spriteUIGemMark;
        _lblRewardValue.text = "x10";

        //_lblComment.text = "[7D4700FF]테마 『[n]』을 클리어 했다냥!\n클리어 선물은 메일함을 열어보라냥! [-]";
        _lblComment.text = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3106);
        _lblComment.text = _lblComment.text.Replace("[n]", _currentTheme.ToString());

        if (pTheme > 1 && pStars < 33) {
            _lblComment.text += "\n";
            //_lblComment.text += "[FF6E00FF]하지만 다음 테마로 가기 위해서는 별을 33개 이상 모아야 한다냥~~[-]";
            _lblComment.text += GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3107);

        }

        this.transform.DOLocalMoveX(0, 0.3f).OnComplete(OnCompleteMove);

    }

    /// <summary>
    /// 다음 테마로 이동!
    /// </summary>
    public void SetNextThemeOpenComment(int pTheme) {

        this.transform.localPosition = new Vector3(-700, 0, 0);
        this.gameObject.SetActive(true);
        _onMove = false;

        _stars = 33;

        _spRewardIcon.gameObject.SetActive(false);
        _lblRewardValue.gameObject.SetActive(false);


        _currentTheme = pTheme;
        //_lblComment.text = "[7D4700FF]테마 『[n]』에서 별을 33개 이상 모았냥!\n그러면 다음 테마로 진행한다냥![-]";
        _lblComment.text = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3108);
        _lblComment.text = _lblComment.text.Replace("[n]", _currentTheme.ToString());

        if(_currentTheme >= StageMasterCtrl.Instance.ListThemes.Count) {
            _lblComment.text += "\n\n";
            //_lblComment.text += "[FF6E00FF]다음 테마는 업데이트를 기다려 달라냥! [-]";
            _lblComment.text += GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3109);
            _stars = 0;
        }

        this.transform.DOLocalMoveX(0, 0.3f).OnComplete(OnCompleteMove);
    }

    

    void OnCompleteMove() {
        _onMove = true;
    }


    public void OnClick() {
        if (!_onMove)
            return;


        // 별이 33개 이상 모은 경우 다음 테마로 이동시킨다. 
        if(_stars >= 33)
            StageMasterCtrl.Instance.MoveTheme(_currentTheme + 1);

        this.gameObject.SetActive(false);
    }

    


}
