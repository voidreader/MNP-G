using UnityEngine;
using System.Collections;
using PathologicalGames;

public enum RankingType {

    ScoreRank, // 1~100위 + 사용자 랭킹 
    FBRank, // FB 친구 랭킹 
    AroundScoreRank // 사용자 주변 랭킹 

}

public class RankingCtrl : MonoBehaviour {

    [SerializeField] UIButton btnAroundScore; // 사용자 주변 랭킹 
	[SerializeField] UIButton btnScore; // 일반 스코어 랭킹 
	[SerializeField] UIButton btnFBScore; // 페이스북 친구 랭킹 

    [SerializeField] GameObject _playerRank;

    [SerializeField]
    UIButton btnFBLogin;

    [SerializeField]
    UILabel _lblBottom;


    [SerializeField]
    RankingType _rankType;

    public RankingType RankType {
        get {
            return _rankType;
        }

        set {
            _rankType = value;
            SetTabButton();
        }
    }


    /// <summary>
    /// 랭킹 화면 초기화 
    /// </summary>
    public void InitRanking() {

        this.gameObject.SetActive(true);

        _playerRank.SetActive(false);

        RankType = RankingType.ScoreRank;
    }

    public void LoginFacebook() {

        // delegate 등록 
        MNPFacebookCtrl.OnCompleteLoadFriend += LoadScores;
        MNPFacebookCtrl.OnAppScoresRequestCompleteAction += LobbyCtrl.Instance.SpawnFBRankData;
        MNPFacebookCtrl.Instance.LoginFB();


    }

    private void LoadScores() {
        btnFBLogin.gameObject.SetActive(false);

        GameSystem.Instance.OnOffWaitingRequestInLobby(true);

        MNPFacebookCtrl.OnAppScoresRequestCompleteAction += LobbyCtrl.Instance.SpawnFBRankData;
        MNPFacebookCtrl.Instance.LoadAppScores();

        // 스코어에 따른 랭킹 Prefab 생성 
        Debug.Log("AppScores Count :: " + MNPFacebookCtrl.Instance.AppScores.Count);
    }


    /// <summary>
    /// 랭킹화면 탭버튼 수정 
    /// </summary>
	public void SetTabButton() {


        // int rankingTheme = GameSystem.Instance.EnvInitJSON["rankingtheme"].AsInt;
        int minRankingStage = GameSystem.Instance.EnvInitJSON["minrankingstage"].AsInt; 
        int maxRankingStage = GameSystem.Instance.EnvInitJSON["maxrankingstage"].AsInt;

        _lblBottom.text = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3254);
        _lblBottom.text = _lblBottom.text.Replace("[n1]", minRankingStage.ToString());
        _lblBottom.text = _lblBottom.text.Replace("[n2]", maxRankingStage.ToString());

        _lblBottom.text = _lblBottom.text + "\n" +  GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3089);

        if (RankType == RankingType.ScoreRank) {
            btnScore.normalSprite = "common_btn_tap_red";
            btnFBScore.normalSprite = "common_btn_tap_gray";
            btnAroundScore.normalSprite = "common_btn_tap_gray";
            btnFBLogin.gameObject.SetActive(false);
        }
        else if (RankType == RankingType.FBRank) {
            if (Facebook.Unity.FB.IsLoggedIn) {
                // LoadAppScroes
                btnFBLogin.gameObject.SetActive(false);
                // 스코어 조회 
            }
            else {
                btnFBLogin.gameObject.SetActive(true);
            }

            btnFBScore.normalSprite = "common_btn_tap_red";
            btnScore.normalSprite = "common_btn_tap_gray";
            btnAroundScore.normalSprite = "common_btn_tap_gray";
        }
        else if (RankType == RankingType.AroundScoreRank) {
            btnScore.normalSprite = "common_btn_tap_gray";
            btnFBScore.normalSprite = "common_btn_tap_gray";
            btnAroundScore.normalSprite = "common_btn_tap_red";

            btnFBLogin.gameObject.SetActive(false);
        }

	}

    /// <summary>
    /// 일반 스코어 랭킹 탭 
    /// </summary>
	public void OnClickScoreRank() {

        if (RankType == RankingType.ScoreRank)
            return;

        RankType = RankingType.ScoreRank;

		PoolManager.Pools ["RankPool"].DespawnAll ();
		LobbyCtrl.Instance.RequestRankData ();
	}

    /// <summary>
    /// 페이스북 스코어 랭킹 탭 
    /// </summary>
	public void OnClickFBScoreRank() {
        if (RankType == RankingType.FBRank)
            return;


        RankType = RankingType.FBRank;

        PoolManager.Pools ["RankPool"].DespawnAll ();

        if(Facebook.Unity.FB.IsLoggedIn) {
            LoadScores();
        }
	}

    public void OnClickAroundScoreRank() {
        if (RankType == RankingType.AroundScoreRank)
            return;

        RankType = RankingType.AroundScoreRank;

        PoolManager.Pools["RankPool"].DespawnAll();
        LobbyCtrl.Instance.RequestAroundRankData();
    }


    /// <summary>
    /// 물음표 버튼 클릭 
    /// </summary>
	public void OnClickQS() {
        WindowManagerCtrl.Instance.OpenHelpRank();
	}

}
