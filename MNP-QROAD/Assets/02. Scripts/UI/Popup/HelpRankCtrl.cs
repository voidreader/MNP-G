using UnityEngine;
using System.Collections;
using PathologicalGames;

public class HelpRankCtrl : MonoBehaviour {


    [SerializeField]
    UIPanel _scrollView;


    Transform _column;

    public void SetScoreRank() {
        this.gameObject.SetActive(true);

        InitScrollView();

        SpawnRewardColumns();
	}

    private void SpawnRewardColumns() {

        for(int i=0; i<GameSystem.Instance.RankRewardInitJSON.Count;i++) {
            _column = PoolManager.Pools["RankRewardPool"].Spawn("RankRewardPrefab", Vector3.zero, Quaternion.identity);
            _column.GetComponent<RankRewardColumnCtrl>().SetColumn(GameSystem.Instance.RankRewardInitJSON[i]);

            _column.localScale = GameSystem.Instance.BaseScale;
            _column.localPosition = new Vector3(0, i * _scrollView.GetComponent<UIGrid>().cellHeight * -1, 0);
        }

        

        //PoolManager.Pools["RankRewardPool"].Spawn()
    }


    private void InitScrollView() {
        _scrollView.gameObject.GetComponent<UIScrollView>().ResetPosition();
        _scrollView.clipOffset = Vector2.zero;
        _scrollView.transform.localPosition = new Vector3(0, 290, 0);
    }

    public void CloseHelp() {
        
        this.SendMessage("CloseSelf");
    }

    void OnDisable() {
        PoolManager.Pools["RankRewardPool"].DespawnAll();
    }

	

}
