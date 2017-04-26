using UnityEngine;
using System.Collections;
using SimpleJSON;
using PathologicalGames;

public class RankRewardColumnCtrl : MonoBehaviour {

    [SerializeField] string _debugNode;
    [SerializeField] RewardObjectCtrl[] _arrRewardObjects;

    [SerializeField] UILabel _lblText;

    [SerializeField] int _maxRank;
    [SerializeField] int _minRank;

    JSONNode _rewardNode;

	
    public void SetColumn(JSONNode pNode) {

        InitColumn();

        _debugNode = pNode.ToString();

        _maxRank = pNode["maxrank"].AsInt;
        _minRank = pNode["minrank"].AsInt;

        _rewardNode = pNode["reward"];

        for(int i=0; i<_rewardNode.Count; i++) {
            _arrRewardObjects[i].SetRewardInfo(_rewardNode[i]);
        }

        // Text 처리
        // GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L4334).Replace("[n]", pRank.ToString());

        if (_maxRank == _minRank) {
            _lblText.text = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3215).Replace("[n]", _maxRank.ToString());
        } else if (_maxRank != _minRank && _minRank != 0) {
            _lblText.text = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3215).Replace("[n]", _maxRank.ToString()) 
                + " ~ \n"
                + GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3215).Replace("[n]", _minRank.ToString());
        }
        else {
            _lblText.text = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3215).Replace("[n]", _maxRank.ToString()) + " ~ \n";
        }


    }

    private void InitColumn() {

        _debugNode = string.Empty;

        for(int i=0; i<_arrRewardObjects.Length; i++) {
            _arrRewardObjects[i].gameObject.SetActive(false);
        }
    }

    void OnSpawned() {
        this.transform.localScale = GameSystem.Instance.BaseScale;
    }

}
