using UnityEngine;
using System.Collections;
using SimpleJSON;
using PathologicalGames;

public class CollectionCtrl : MonoBehaviour {

    JSONNode _upperThemeNode;
    JSONNode _lowerThemeNode;

    CollectionMasterCtrl _master;

    [SerializeField]
    int _currentUserStage = 0;

    [SerializeField] int _upperThemeID;
	[SerializeField] UISprite[] _arrUpperThemeSeat;
    [SerializeField] UISprite[] _arrUpperThemeLight;
    [SerializeField] UISprite[] _arrUpperThemeNeko;
    
    [SerializeField] UISprite _upperThemeBossSeat;
    [SerializeField] UISprite _upperThemeBossNeko;
    [SerializeField] UISprite _upperThemeBossLight;

    [SerializeField] GameObject _upperThemeButton;

    [SerializeField] int _lowerThemeID;
    [SerializeField] UISprite[] _arrLowerThemeSeat;
    [SerializeField] UISprite[] _arrLowerThemeLight;
    [SerializeField] UISprite[] _arrLowerThemeNeko;
    
    [SerializeField] UISprite _lowerThemeBossSeat;
    [SerializeField] UISprite _lowerThemeBossNeko;

    [SerializeField] UISprite _lowerThemeBossLight;

    [SerializeField] GameObject _lowerThemeButton;


    void OnSpawned() {
        this.transform.localScale = GameSystem.Instance.BaseScale;
    }

    /// <summary>
    /// 초기화 
    /// </summary>
    /// <param name="pUpperNode"></param>
    /// <param name="pLowerNode"></param>
    public void InitCollection(JSONNode pUpperNode, JSONNode pLowerNode, CollectionMasterCtrl pMaster) {
        _upperThemeNode = pUpperNode;
        _lowerThemeNode = pLowerNode;

        _currentUserStage = GameSystem.Instance.UserCurrentStage;

        _upperThemeID = _upperThemeNode["masterid"].AsInt;
        _lowerThemeID = _lowerThemeNode["masterid"].AsInt;


        InitObject();

        // 상하단 테마 초기화 
        InitUpperTheme();
        InitLowerTheme();

        _master = pMaster;
    }



    /// <summary>
    /// 오브젝트 초기화 
    /// </summary>
    void InitObject() {
        for(int i=0; i<_arrUpperThemeLight.Length; i++) {
            _arrUpperThemeLight[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < _arrLowerThemeLight.Length; i++) {
            _arrUpperThemeLight[i].gameObject.SetActive(false);
        }

        for(int i=0; i<_arrUpperThemeSeat.Length; i++) {
            _arrUpperThemeSeat[i].spriteName = PuzzleConstBox.spriteBaseNekoSeat;
            _arrLowerThemeSeat[i].spriteName = PuzzleConstBox.spriteBaseNekoSeat;

            _arrUpperThemeSeat[i].GetComponent<UIButton>().enabled = false;
            _arrLowerThemeSeat[i].GetComponent<UIButton>().enabled = false;

            _arrUpperThemeNeko[i].spriteName = PuzzleConstBox.spriteBaseNekoFigure;
            _arrLowerThemeNeko[i].spriteName = PuzzleConstBox.spriteBaseNekoFigure;

            _arrUpperThemeNeko[i].MakePixelPerfect();
            _arrLowerThemeNeko[i].MakePixelPerfect();
        }


        _upperThemeBossLight.gameObject.SetActive(false);
        _upperThemeBossSeat.spriteName = PuzzleConstBox.spriteBaseBossSeat;
        // _upperThemeBossSeat.MakePixelPerfect();
        _upperThemeBossSeat.GetComponent<UIButton>().enabled = false;

        _lowerThemeBossLight.gameObject.SetActive(false);
        _lowerThemeBossSeat.spriteName = PuzzleConstBox.spriteBaseBossSeat;
        // _lowerThemeBossSeat.MakePixelPerfect();
        _lowerThemeBossSeat.GetComponent<UIButton>().enabled = false;

        _upperThemeButton.SetActive(false);
        _lowerThemeButton.SetActive(false);

    }


    /// <summary>
    /// 상단 테마 초기화
    /// </summary>
    void InitUpperTheme() {
        int loop = 0;
        bool isClearTheme = false;

        // 현재 스테이지가 테마의 13번째 스테이지보다 높은 경우, 모두 클리어 처리 
        if(_currentUserStage > _upperThemeID * 13) {
            loop = 4;
            isClearTheme = true;
        }
        else { 
            if(_currentUserStage > (_upperThemeID - 1 ) * 13 + 3) { // 테마의 3스테이지까지 클리어 한 경우 
                loop = 1;
            }

            if (_currentUserStage > (_upperThemeID - 1) * 13 + 6) { // 테마의 6스테이지까지 클리어 한 경우 
                loop = 2;
            }

            if (_currentUserStage > (_upperThemeID - 1) * 13 + 9) { // 테마의 9스테이지까지 클리어 한 경우 
                loop = 3;
            }

            if (_currentUserStage > (_upperThemeID - 1) * 13 + 12) { // 테마의 12스테이지까지 클리어 한 경우 
                loop = 4;
            }
        }


        // Loop 값 만큼 Seat와 Light, 네코 Sprite 처리 
        for(int i=0; i<loop; i++) {
            _arrUpperThemeSeat[i].spriteName = PuzzleConstBox.spriteSmallCollectionSeat;
            _arrUpperThemeLight[i].gameObject.SetActive(true);
            _arrUpperThemeSeat[i].GetComponent<UIButton>().enabled = true;

            GameSystem.Instance.SetNekoSpriteByID(_arrUpperThemeNeko[i], _upperThemeNode["stage_neko" + (i + 1).ToString()].AsInt);

            _arrUpperThemeNeko[i].width = 105;
            _arrUpperThemeNeko[i].height = 105;

        }

        if(isClearTheme) {
            _upperThemeBossLight.gameObject.SetActive(true);
            _upperThemeBossSeat.spriteName = PuzzleConstBox.spriteBigCollectionSeat;
            _upperThemeBossSeat.GetComponent<UIButton>().enabled = true;

            GameSystem.Instance.SetNekoSpriteByID(_upperThemeBossNeko, _upperThemeNode["stage_boss"].AsInt);

            _upperThemeBossNeko.width = 200;
            _upperThemeBossNeko.height = 200;

            _upperThemeButton.SetActive(true);

            // 보상 지급 내역 체크 
            if (CheckRewardReceived(_upperThemeID)) {
                _upperThemeButton.SetActive(false);
            }
        }


        


    }




    /// <summary>
    /// 하단 테마 초기화
    /// </summary>
    void InitLowerTheme() {
        int loop = 0;
        bool isClearTheme = false;

        // 현재 스테이지가 테마의 13번째 스테이지보다 높은 경우, 모두 클리어 처리 
        if (_currentUserStage > _lowerThemeID * 13) {
            loop = 4;
            isClearTheme = true;
        }
        else {
            if (_currentUserStage > (_lowerThemeID - 1) * 13 + 3) { // 테마의 3스테이지까지 클리어 한 경우 
                loop = 1;
            }

            if (_currentUserStage > (_lowerThemeID - 1) * 13 + 6) { // 테마의 6스테이지까지 클리어 한 경우 
                loop = 2;
            }

            if (_currentUserStage > (_lowerThemeID - 1) * 13 + 9) { // 테마의 9스테이지까지 클리어 한 경우 
                loop = 3;
            }

            if (_currentUserStage > (_lowerThemeID - 1) * 13 + 12) { // 테마의 12스테이지까지 클리어 한 경우 
                loop = 4;
            }
        }


        // Loop 값 만큼 Seat와 Light, 네코 Sprite 처리 
        for (int i = 0; i < loop; i++) {
            _arrLowerThemeSeat[i].spriteName = PuzzleConstBox.spriteSmallCollectionSeat;
            _arrLowerThemeSeat[i].GetComponent<UIButton>().enabled = true;
            _arrLowerThemeLight[i].gameObject.SetActive(true);

            GameSystem.Instance.SetNekoSpriteByID(_arrLowerThemeNeko[i], _lowerThemeNode["stage_neko" + (i + 1).ToString()].AsInt);

            _arrLowerThemeNeko[i].width = 105;
            _arrLowerThemeNeko[i].height = 105;

        }

        if (isClearTheme) {
            _lowerThemeBossLight.gameObject.SetActive(true);
            _lowerThemeBossSeat.spriteName = PuzzleConstBox.spriteBigCollectionSeat;
            _lowerThemeBossSeat.GetComponent<UIButton>().enabled = true;

            GameSystem.Instance.SetNekoSpriteByID(_lowerThemeBossNeko, _lowerThemeNode["stage_boss"].AsInt);

            _lowerThemeBossNeko.width = 200;
            _lowerThemeBossNeko.height = 200;

            _lowerThemeButton.SetActive(true);

            // 보상 지급 내역 체크 
            if (CheckRewardReceived(_lowerThemeID)) {
                _lowerThemeButton.SetActive(false);
            }
        }

    }


    #region 보상 지급 내역 체크
    bool CheckRewardReceived(int pThemeID) {

        for(int i=0; i<GameSystem.Instance.RescueHistory.Count; i++) {
            if(GameSystem.Instance.RescueHistory[i]["themeid"].AsInt == pThemeID) {

                if (GameSystem.Instance.RescueHistory[i]["takepay"].AsInt == 0)
                    return false;
                else
                    return true;
            }
        }

        return false;

    }
    #endregion

    #region 보상 지급 



    public void RewardUpperTheme() {
        GameSystem.Instance.Post2RescueReward(_upperThemeID, OnCompleteRewardedUpperTheme);
    }

    public void RewardLowerTheme() {
        GameSystem.Instance.Post2RescueReward(_lowerThemeID, OnCompleteRewardedLowerTheme);
    }

    //Callback
    void OnCompleteRewardedUpperTheme() {
        _upperThemeButton.SetActive(false);
        OpenRewardInfo();
    }

    void OnCompleteRewardedLowerTheme() {
        _lowerThemeButton.SetActive(false);
        OpenRewardInfo();

        
    }

    void OpenRewardInfo() {
        LobbyCtrl.Instance.OpenInfoPopUp(PopMessageType.RescueRewarded, "30");
        LobbyCtrl.Instance.ForceSetMailBoxNew();
    }


    #endregion

    #region 고양이 선택

    public void SelectUpperFirstNeko() {
        _master.SetSelectNeko(_upperThemeNode["stage_neko1"].AsInt);
    }

    public void SelectUpperSecondNeko() {
        _master.SetSelectNeko(_upperThemeNode["stage_neko2"].AsInt);
    }

    public void SelectUpperThirdNeko() {
        _master.SetSelectNeko(_upperThemeNode["stage_neko3"].AsInt);
    }

    public void SelectUpperFourthNeko() {
        _master.SetSelectNeko(_upperThemeNode["stage_neko4"].AsInt);
    }

    public void SelectUpperBossNeko() {
        _master.SetSelectNeko(_upperThemeNode["stage_boss"].AsInt);
    }


    public void SelectLowerFirstNeko() {
        _master.SetSelectNeko(_lowerThemeNode["stage_neko1"].AsInt);
    }

    public void SelectLowerSecondNeko() {
        _master.SetSelectNeko(_lowerThemeNode["stage_neko2"].AsInt);
    }

    public void SelectLowerThirdNeko() {
        _master.SetSelectNeko(_lowerThemeNode["stage_neko3"].AsInt);
    }

    public void SelectLowerFourthNeko() {
        _master.SetSelectNeko(_lowerThemeNode["stage_neko4"].AsInt);
    }

    public void SelectLowerBossNeko() {
        _master.SetSelectNeko(_lowerThemeNode["stage_boss"].AsInt);
    }



    #endregion




}
