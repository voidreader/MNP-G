using UnityEngine;
using System.Collections;
using DG.Tweening;

public class GTipCtrl : MonoBehaviour {

    public UISprite _spTip;
    public UILabel _lblTip;

    public GameObject _leftArrow;
    public GameObject _rightArrow;

    public int _totalPage = 0;
    public int _currentPage = 0;
    public TipType _tipType;

    public GameObject _btnClose;

    public Transform _frame;
    public GameObject _lock;
            
    public bool GetGameTipActive() {
        return _frame.gameObject.activeSelf;

    }

    /// <summary>
    /// 게임팁 설정 
    /// </summary>
    /// <param name="pType"></param>
    public void SetGameTip(TipType pType) {

        this.gameObject.SetActive(true);

        _lock.SetActive(true);
        _frame.transform.localPosition = new Vector3(0, 80, 0);
        _frame.gameObject.SetActive(true);
        _frame.DOLocalMoveY(-120, 0.5f);

        


        _currentPage = 0;
        _tipType = pType;
        _btnClose.SetActive(false);

        // 팁마다 설정된 페이지 처리 
        if (_tipType == TipType.FisrtStage) {
            _totalPage = 4;
        }
        else if(_tipType == TipType.NekoService) {
            _totalPage = 1;
        }
        else if(_tipType == TipType.AllPuzzleItem) {
            _totalPage = 1;
        }
        else if(_tipType == TipType.BombTip) {
            _totalPage = 2;
        }
        else if (_tipType == TipType.SpecialAttackTip) {
            _totalPage = 1;
        }
        else if (_tipType == TipType.CookieTip) {
            _totalPage = 1;
        }
        else if (_tipType == TipType.StoneTip) {
            _totalPage = 1;
        }
        else if (_tipType == TipType.FishGrillTip) {
            _totalPage = 1;
        }
        else if(_tipType == TipType.MoveMissionTip) {
            _totalPage = 2;
        }
        else if (_tipType == TipType.PassivePower) {
            _totalPage = 2;
        }

        SetPage(_currentPage);

    }

    public void SetNextPage() {
        _currentPage++;
        SetPage(_currentPage);
    }

    public void SetPreviousPage() {
        _currentPage--;
        SetPage(_currentPage);
    }

    /// <summary>
    /// 팁의 페이지 설정 
    /// </summary>
    /// <param name="pPage"></param>
    void SetPage(int pPage) {


        #region 방향키 
        _leftArrow.SetActive(true);
        _rightArrow.SetActive(true);

        if (pPage == 0) {
            _leftArrow.SetActive(false);
        }

        if(pPage + 1 >= _totalPage) {
            _rightArrow.SetActive(false);
        }

        #endregion

        #region FirstStage 게임 방식 팁 
        if(_tipType == TipType.FisrtStage) {
            switch(pPage) {
                case 0:
                    _spTip.spriteName = "tu-in-1";
                    _lblTip.text = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L2719);
                    CheckCloseButton(false);
                    break;
                case 1:
                    _spTip.spriteName = "tu-in-2";
                    _lblTip.text = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L2720);
                    CheckCloseButton(false);
                    break;
                case 2:
                    _spTip.spriteName = "tu-in-3";
                    _lblTip.text = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L2721);
                    CheckCloseButton(false);
                    break;
                case 3:
                    _spTip.spriteName = "tu-in-4";
                    _lblTip.text = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L2722);
                    CheckCloseButton(true);
                    break;
            }
        }
        #endregion
        #region 밋치리보너스 
        else if (_tipType == TipType.NekoService) {
            _spTip.spriteName = "tu-ui-present";
            _lblTip.text = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L2712);
            CheckCloseButton(true);
        }
        #endregion
        #region 부스트 아이템 
        else if (_tipType == TipType.AllPuzzleItem) {
            _spTip.spriteName = "tu-in-13";
            _lblTip.text = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L2700);
            CheckCloseButton(true);
        }
        #endregion
        else if (_tipType == TipType.BombTip) {
            switch (pPage) {
                case 0:
                    _spTip.spriteName = "tu-in-5";
                    _lblTip.text = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L2701);
                    CheckCloseButton(false);
                    break;
                case 1:
                    _spTip.spriteName = "tu-in-6";
                    _lblTip.text = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L2702);
                    CheckCloseButton(true);
                    break;
            }
        }
        else if (_tipType == TipType.SpecialAttackTip) {
            _spTip.spriteName = "tu-in-7";
            _lblTip.text = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L2703);
            CheckCloseButton(true);
        }
        else if (_tipType == TipType.CookieTip) {
            _spTip.spriteName = "tu-in-8";
            _lblTip.text = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L2704);
            CheckCloseButton(true);
        }
        else if (_tipType == TipType.StoneTip) {
            _spTip.spriteName = "tu-in-9-1";
            _lblTip.text = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L2705);
            CheckCloseButton(true);
        }
        else if (_tipType == TipType.FishGrillTip) {
            _spTip.spriteName = "tu-in-10";
            _lblTip.text = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L2706);
            CheckCloseButton(true);
        }
        else if (_tipType == TipType.MoveMissionTip) {
            switch (pPage) {
                case 0:
                    _spTip.spriteName = "tu-in-14";
                    _lblTip.text = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L2707);
                    CheckCloseButton(false);
                    break;
                case 1:
                    _spTip.spriteName = "tu-in-15";
                    _lblTip.text = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L2708);
                    CheckCloseButton(true);
                    break;
            }
        }
        else if (_tipType == TipType.PassivePower) {


            switch (pPage) {
                case 0:
                    _spTip.spriteName = "tu-in-17";
                    _lblTip.text = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L2725);
                    CheckCloseButton(false);
                    break;
                case 1:
                    _spTip.spriteName = "tu-in-16";
                    _lblTip.text = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L2726);
                    CheckCloseButton(true);
                    break;
            }

        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="pActive"></param>
    void CheckCloseButton(bool pActive) {

        // 이미 닫기 버튼이 활성화 되어있다면 return
        if (_btnClose.activeSelf)
            return;


        _btnClose.SetActive(pActive);
    }

    public void CloseTip() {

        // 튜토리얼 미완료 사용자가 처음으로 첫번째 스테이지에 입장했을때, 변수 설정
        if(_tipType == TipType.FisrtStage && GameSystem.Instance.UserJSON["tutorialcomplete"].AsInt == 0) {
            GameSystem.Instance.SaveLocalTutorialStep(4);
        }

        this.gameObject.SetActive(false);
        this._frame.gameObject.SetActive(false);
        _lock.gameObject.SetActive(false);

    }
}
