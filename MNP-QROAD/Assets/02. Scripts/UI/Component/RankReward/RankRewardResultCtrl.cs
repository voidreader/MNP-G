using UnityEngine;
using System.Collections;
using SimpleJSON;


/// <summary>
/// 라인 초대 보상, 주간랭킹 보상 정보 에서 사용 
/// </summary>
public class RankRewardResultCtrl : MonoBehaviour {

    [SerializeField]
    string _debugNode;

    JSONNode _resultNode;


    [SerializeField]
    RewardObjectCtrl[] _arrRewardObjects;

    [SerializeField]
    UILabel _lblText;

    [SerializeField]
    int _lastRank;

    [SerializeField]
    int _rewardCount = 0;

    // LineInvte
    [SerializeField] UILabel _lblLineGemValue;
    [SerializeField] UILabel _lblLineNekoName;
    [SerializeField] UILabel _lblLineNekoGrade;
    [SerializeField] UISprite _spLineNeko;


    // Use this for initialization
    void Start () {
	
	}
	


    /// <summary>
    /// 라인 초대 보상
    /// </summary>
    /// <param name="pNode"></param>
    public void InitLineInvite(JSONNode pNode) {

        this.gameObject.SetActive(true);
        _lblText.text = GameSystem.Instance.GetLocalizeText(4233);

        //getgem: 300,
        //getnekoid: 105,
        //getnekograde: 3

        GameSystem.Instance.SetNekoSprite(_spLineNeko, pNode["getnekoid"].AsInt, pNode["getnekograde"].AsInt);
        _lblLineGemValue.text = GameSystem.Instance.GetNumberToString(pNode["getgem"].AsInt);
        _lblLineNekoName.text = GameSystem.Instance.GetNekoName(pNode["getnekoid"].AsInt, pNode["getnekograde"].AsInt);
        _lblLineNekoGrade.text = GameSystem.Instance.GetNekoGradeText(pNode["getnekograde"].AsInt);


    }


    /// <summary>
    /// 주간랭킹 보상 
    /// </summary>
    /// <param name="pNode"></param>
    public void InitResult(JSONNode pNode) {

        this.gameObject.SetActive(true);

        
        _rewardCount = 0;
        _lastRank = 0;


        _debugNode = string.Empty;
        _debugNode = pNode.ToString();

        _resultNode = pNode["data"];

        for (int i = 0; i < _arrRewardObjects.Length; i++) {
            _arrRewardObjects[i].gameObject.SetActive(false);
        }

        _lastRank = _resultNode["rank"].AsInt;
        _lblText.text = GameSystem.Instance.GetLocalizeText(3216);
        _lblText.text = _lblText.text.Replace("[n]", GameSystem.Instance.GetNumberToString(_lastRank));

        _rewardCount = _resultNode["reward"].Count;

        if(_rewardCount == 1) {
            _arrRewardObjects[0].transform.localPosition = Vector3.zero;
            _arrRewardObjects[0].gameObject.SetActive(true);
        } else if(_rewardCount == 2) {
            _arrRewardObjects[0].transform.localPosition = new Vector3(-80, 0, 0);
            _arrRewardObjects[0].gameObject.SetActive(true);
            _arrRewardObjects[1].transform.localPosition = new Vector3(80, 0, 0);
            _arrRewardObjects[1].gameObject.SetActive(true);
        } else if(_rewardCount == 3) {
            _arrRewardObjects[0].transform.localPosition = new Vector3(-180, 0, 0);
            _arrRewardObjects[0].gameObject.SetActive(true);
            _arrRewardObjects[1].transform.localPosition = new Vector3(0, 0, 0);
            _arrRewardObjects[1].gameObject.SetActive(true);
            _arrRewardObjects[2].transform.localPosition = new Vector3(180, 0, 0);
            _arrRewardObjects[2].gameObject.SetActive(true);
        }

        // 세팅
        for(int i=0; i<_resultNode["reward"].Count; i++) {

            _arrRewardObjects[i].SetRewardInfo(_resultNode["reward"][i]);
        }

    }

    public void OnClosing() {
        
        // 메일함을 읽어야한다.
        GameSystem.Instance.Post2CheckNewMail(); // 메일 조회 
    }
	
}
