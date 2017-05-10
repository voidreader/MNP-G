using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathologicalGames;
using DG.Tweening;

public class WorldMapCtrl : MonoBehaviour {


    [SerializeField] List<MapEpisodeCtrl> _listEpisodes = new List<MapEpisodeCtrl>();
    [SerializeField] GameObject _locker;

    Vector3 _originpos = new Vector3(-600, 0, 0);
    Vector3 _onpos = new Vector3(-190, 0, 0);
   

    bool _isOn = false;

    public bool IsOn {
        get {
            return _isOn;
        }

        set {
            _isOn = value;
        }
    }

    void Start() {

    }


    /// <summary>
    /// 월드맵 초기화 
    /// </summary>
    public void InitWorldMap() {

        this.gameObject.SetActive(true);
        this.transform.localPosition = _originpos;

        // 기준정보상의 에피소드 개수만큼 활성화 

        _listEpisodes.Clear();

        for(int i=0; i<GameSystem.Instance.StageMasterJSON.Count; i++) {
            _listEpisodes.Add(PoolManager.Pools[PuzzleConstBox.worldMapPool].Spawn(PuzzleConstBox.prefabMapEpisode, Vector3.zero, Quaternion.identity).GetComponent<MapEpisodeCtrl>()); // add
        }

        // 리스트 순서대로 위치 설정 
        for(int i=0; i<_listEpisodes.Count; i++) {
            _listEpisodes[i].transform.localPosition = new Vector3(0, i * -330, 0);
            
        }

    }


    /// <summary>
    /// 월드맵 오픈 
    /// </summary>
	public void ShowWorldMap() {
        for (int i = 0; i < _listEpisodes.Count; i++) {
            //_listEpisodes[i].transform.localPosition = new Vector3(0, i * 330, 0);
            _listEpisodes[i].SetEpisode(i + 1, this); // 에피소드 정보 세팅 
        }

        _locker.SetActive(true);
        this.transform.DOLocalMoveX(-190, 0.3f);

        IsOn = true;
    }

    public void OffWorldMap() {
        _locker.SetActive(false);
        this.transform.DOLocalMoveX(-600, 0.3f);
        IsOn = false; 
    }


    /// <summary>
    ///  이전 에피소드의 클리어 (33개 별) 여부 
    /// </summary>
    /// <param name="pCurrentEpisode"></param>
    /// <returns></returns>
    public bool GetPreviousEpisodeClear(int pCurrentEpisode) {

        // 테마 1은 그냥 클리어 
        if (pCurrentEpisode == 1)
            return true;

        return _listEpisodes[pCurrentEpisode - 2].GetEpisodeClear();


        
    }
}
