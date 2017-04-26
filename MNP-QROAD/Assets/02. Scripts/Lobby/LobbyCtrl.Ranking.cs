using UnityEngine;
using System.Collections;
using PathologicalGames;

public partial class LobbyCtrl : MonoBehaviour {

	/* 랭킹 컨트롤 */
	[SerializeField] MyRankCtrl _myRank;
	[SerializeField] UIGrid _grdRank;
	[SerializeField] RankRewardResultCtrl _rankRewardResult;


	public void OpenRanking() {
        //GameSystem.Instance.TestMessage ();

        Debug.Log(">>> OpenRanking");

        bigPopup.gameObject.SetActive(true);
        bigPopup.SetRanking();


        AdbrixManager.Instance.SendAdbrixInAppActivity(AdbrixManager.Instance.BUTTON_RANKING);
    }


    /// <summary>
    /// 랭킹 정보 요청
    /// </summary>
	public void RequestRanking() {
		RankingCtrl rankCtrl = FindObjectOfType(typeof(RankingCtrl)) as RankingCtrl;

		if (rankCtrl == null) {
			Invoke("RequestRanking", 1);
			return;
		}

        if (rankCtrl.RankType == RankingType.ScoreRank) {
            RequestRankData();
        }
        else if (rankCtrl.RankType == RankingType.FBRank) {
            if (Facebook.Unity.FB.IsLoggedIn)
                rankCtrl.SendMessage("LoadScores");
        }
        else if (rankCtrl.RankType == RankingType.AroundScoreRank) {
        }
	}


    public void RequestAroundRankData() {
        _myRank.gameObject.SetActive(false);
        GameSystem.Instance.Post2AroundRankList(SpawnAroundRankData);
    }

    private  void SpawnAroundRankData() {

        Debug.Log("★★★★ SpawnAroundRankData ");

        Transform rank;
        int myRankIndex = -1;

        int offsetY;
        
        pnlRankScrollView.clipOffset = Vector2.zero;
        pnlRankScrollView.transform.localPosition = new Vector3(0, 200, 0);
        //pnlRankScrollView.baseClipRegion.Set(0, -250, 640, 720);
        pnlRankScrollView.baseClipRegion = new Vector4(0, -250, 640, 720);
        pnlRankScrollView.clipOffset += Vector2.one;
        pnlRankScrollView.clipOffset -= Vector2.one;
        pnlRankScrollView.gameObject.GetComponent<UIScrollView>().ResetPosition();


        for (int i = 0; i < GameSystem.Instance.RankJSON["data"]["ranklist"].Count; i++) {

            rank = PoolManager.Pools["RankPool"].Spawn("RankPrefab", Vector3.zero, Quaternion.identity);
            rank.GetComponent<RankColumnCtrl>().SetAroundRankInfo(GameSystem.Instance.RankJSON["data"]["ranklist"][i]["mainneko"].AsInt
                                                            , GameSystem.Instance.RankJSON["data"]["ranklist"][i]["mainnekograde"].AsInt
                                                            , GameSystem.Instance.RankJSON["data"]["ranklist"][i]["highscore"].AsInt
                                                            , GameSystem.Instance.RankJSON["data"]["ranklist"][i]["rank"].AsInt
                                                            , GameSystem.Instance.RankJSON["data"]["ranklist"][i]["username"].Value
                                                            , GameSystem.Instance.RankJSON["data"]["ranklist"][i]["userdbkey"].AsInt);

            if (GameSystem.Instance.RankJSON["data"]["rank"].AsInt == GameSystem.Instance.RankJSON["data"]["ranklist"][i]["rank"].AsInt) {
                myRankIndex = i;
                rank.GetComponent<RankColumnCtrl>().SetMyAroundRank();
            }

            // position 지정하자. 
            rank.localScale = GameSystem.Instance.BaseScale;
            rank.localPosition = new Vector3(0, i * _grdRank.cellHeight * -1, 0);


        }

        offsetY = myRankIndex * -1 * 100;

        if (myRankIndex <= 3) {
            offsetY = 0;
        }
        else if (myRankIndex > 3 && myRankIndex <= 10) {
            offsetY = myRankIndex * -1 * 100;
        }
        else if (myRankIndex > 10 && myRankIndex <= 20) {
            offsetY = myRankIndex * -1 * 115;
        }
        else if (myRankIndex > 20 && myRankIndex <= 30) {
            offsetY = myRankIndex * -1 * 125;
        }
        else if (myRankIndex > 30 && myRankIndex <= 40) {
            offsetY = myRankIndex * -1 * 130;
        }
        else if (myRankIndex > 40 && myRankIndex <= 50) {
            offsetY = myRankIndex * -1 * 135;
        }
        else if (myRankIndex > 50 && myRankIndex <= 60) {
            offsetY = myRankIndex * -1 * 140;
        }
        else if (myRankIndex > 60 && myRankIndex <= 70) {
            offsetY = myRankIndex * -1 * 145;
        }
        else if (myRankIndex > 70 && myRankIndex <= 80) {
            offsetY = myRankIndex * -1 * 150;
        }
        else if (myRankIndex > 80 && myRankIndex <= 90) {
            offsetY = myRankIndex * -1 * 155;
        }
        else {
            offsetY = myRankIndex * -1 * 160;
        }


        pnlRankScrollView.clipOffset = new Vector2(0, offsetY);
        pnlRankScrollView.transform.localPosition = new Vector3(0, offsetY * -1 + 200, 0);


        GameSystem.Instance.OnOffWaitingRequestInLobby(false); // 모두 소환하고 푼다. 
    }


    /// <summary>
    /// 서버에 랭크 데이터 요청
    /// </summary>
    public void RequestRankData() {
		GameSystem.Instance.Post2RankList ();
	}



	/// <summary>
	/// 랭크 정보 수정 
	/// </summary>
	public void SpawnRankData() {
		Transform rank;

        pnlRankScrollView.gameObject.GetComponent<UIScrollView>().ResetPosition();
        pnlRankScrollView.clipOffset = Vector2.zero;
        pnlRankScrollView.transform.localPosition = new Vector3(0, 138, 0);
        pnlRankScrollView.baseClipRegion = new Vector4(0, -250, 640, 630);
        pnlRankScrollView.clipOffset += Vector2.one;
        pnlRankScrollView.clipOffset -= Vector2.one;

        // Player Rank 처리
        _myRank.gameObject.SetActive (true);
		_myRank.SetRankInfo (GameSystem.Instance.UserDataJSON ["data"] ["mainneko"].AsInt
                            , GameSystem.Instance.UserDataJSON["data"]["mainnekograde"].AsInt
                            , GameSystem.Instance.UserDataJSON ["data"] ["highscore"].AsInt
		                    , GameSystem.Instance.RankJSON ["data"] ["rank"].AsInt
		                    , GameSystem.Instance.UserDataJSON ["data"] ["nickname"].Value);


		for(int i=0; i<GameSystem.Instance.RankJSON["data"]["ranklist"].Count; i++) {

			rank = PoolManager.Pools["RankPool"].Spawn("RankPrefab", Vector3.zero, Quaternion.identity);
			rank.GetComponent<RankColumnCtrl>().SetRankInfo(GameSystem.Instance.RankJSON["data"]["ranklist"][i]["mainneko"].AsInt
                                                            ,GameSystem.Instance.RankJSON["data"]["ranklist"][i]["mainnekograde"].AsInt
			                                                ,GameSystem.Instance.RankJSON["data"]["ranklist"][i]["score"].AsInt
			                                                ,GameSystem.Instance.RankJSON["data"]["ranklist"][i]["rank"].AsInt
			                                                ,GameSystem.Instance.RankJSON["data"]["ranklist"][i]["name"].Value
                                                            , GameSystem.Instance.RankJSON["data"]["ranklist"][i]["userdbkey"].AsInt);

			// position 지정하자. 
			rank.localScale = GameSystem.Instance.BaseScale;
			rank.localPosition = new Vector3(0, i * _grdRank.cellHeight * -1, 0);

		}

        GameSystem.Instance.OnOffWaitingRequestInLobby(false); // 모두 소환하고 푼다. 


    }

    /// <summary>
    ///  페이스북 랭킹 컬럼 생성
    /// </summary>
    public void SpawnFBRankData() {

        Debug.Log(">> SpawnFBRankData");

        int i = 0;
        Transform rank;

        pnlRankScrollView.gameObject.GetComponent<UIScrollView>().ResetPosition();
        pnlRankScrollView.clipOffset = Vector2.zero;
        pnlRankScrollView.transform.localPosition = new Vector3(0, 138, 0);
        pnlRankScrollView.baseClipRegion = new Vector4(0, -250, 640, 630);
        pnlRankScrollView.clipOffset += Vector2.one;
        pnlRankScrollView.clipOffset -= Vector2.one;


        _myRank.gameObject.SetActive(true);

        foreach (System.Collections.Generic.KeyValuePair<string, FB_Score> temp in MNPFacebookCtrl.Instance.AppScores) {

            // 내 랭크 설정 
            if(temp.Value.UserId == MNPFacebookCtrl.Instance.UserId) {
                _myRank.SetFBRankInfo(temp.Value, i + 1);
            }

            rank = PoolManager.Pools["RankPool"].Spawn("RankPrefab", Vector3.zero, Quaternion.identity);

            // position 지정하자. 
            rank.localScale = GameSystem.Instance.BaseScale;
            rank.localPosition = new Vector3(0, i++ * _grdRank.cellHeight * -1, 0);

            rank.GetComponent<RankColumnCtrl>().SetFBRankInfo(temp.Value, i);

        }


        GameSystem.Instance.OnOffWaitingRequestInLobby(false); // 모두 소환하고 푼다. 

    }


	public void CloseRanking() {
		_myRank.gameObject.SetActive (false);
		PoolManager.Pools ["RankPool"].DespawnAll ();
	}

	private void RewardLastRank() {
		GameSystem.Instance.Post2ScoreRankReward ();
	}


	/// <summary>
	/// 스코어 랭킹 보상 창 오픈 
	/// </summary>
	public void OpenRankReward() {
        _rankRewardResult.InitResult(GameSystem.Instance.RankRewardJSON);
    }







	



}
