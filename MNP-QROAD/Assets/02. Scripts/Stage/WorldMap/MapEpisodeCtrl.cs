using UnityEngine;
using System.Collections;
using SimpleJSON;

public class MapEpisodeCtrl : MonoBehaviour {

    [SerializeField] UILabel _lblEpisode;
    [SerializeField] GameObject _objClear;
    [SerializeField] UILabel _lblStarCount;
    [SerializeField] GameObject _objStarCounter;

    [SerializeField] UISprite _spEpisode;
    [SerializeField] UIButton _btnEpisode;
    JSONNode _userStageNode;

    [SerializeField] int _episodeID = 0;
    [SerializeField] int _firstStage = 0;
    [SerializeField] int _lastStage = 0;
    [SerializeField] int _star = 0;
    [SerializeField] bool _isClear = false;
    [SerializeField] bool _isOver33Stars = false;

    [SerializeField] string _spriteName = string.Empty;

    WorldMapCtrl _worldMapbase = null;

    /// <summary>
    /// 맵 세팅 
    /// </summary>
    /// <param name="pEpisode"></param>
    public void SetEpisode(int pEpisode, WorldMapCtrl pWorldMapbase) {

        this.transform.localScale = GameSystem.Instance.BaseScale;
        this.gameObject.SetActive(true);

        _userStageNode = GameSystem.Instance.UserStageJSON["stagelist"];
        _worldMapbase = pWorldMapbase;

        _episodeID = pEpisode;
        _lblEpisode.text = _episodeID.ToString();


        // 이전 에피소드에서 별 33개를 획득하지 못한 경우 
        if(!_worldMapbase.GetPreviousEpisodeClear(_episodeID)) {

            _objClear.SetActive(false);
            _objStarCounter.SetActive(false);

            _spEpisode.spriteName = "ep-" + _episodeID.ToString() + "g";
            _btnEpisode.normalSprite = _spEpisode.spriteName;
            return;
        }

        _spriteName = "ep-" + _episodeID.ToString();
        _spEpisode.spriteName = _spriteName;
        _btnEpisode.normalSprite = _spEpisode.spriteName;

        // 에피소드의 첫번째, 마지막 스테이지 찾기 
        _firstStage = (pEpisode - 1) * 13 + 1;
        _lastStage = pEpisode * 13;

        // clear 여부 체크 및 star 
        _star = 0;
        _isClear = true;
        _isOver33Stars = false;
        for (int i= _firstStage-1 ; i<_lastStage; i++) {
            _star += _userStageNode[i]["state"].AsInt;

            if(_userStageNode[i]["state"].AsInt == 0) { // 클리어하지 못한 곳이 있는지 체크 
                _isClear = false; // 클리어 여부 
            }
        }

        if(_isClear && _star >= 33) {
            _isOver33Stars = true; // 다음 테마 이동 가능 여부 
        }


        // 변수 할당
        if(_isClear)
            _objClear.SetActive(true);

        
        _objStarCounter.SetActive(true);
        _lblStarCount.text = _star.ToString() + "/39";

        
        
    }

    /// <summary>
    /// 
    /// </summary>
    public bool GetEpisodeClear() {
        return _isOver33Stars;
    }

    public void OnClick() {
        StageMasterCtrl.Instance.MoveTheme(_episodeID);
    }


}
