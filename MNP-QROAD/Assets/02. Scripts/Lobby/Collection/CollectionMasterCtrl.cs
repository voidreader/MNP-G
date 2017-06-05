using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PathologicalGames;

public class CollectionMasterCtrl : MonoBehaviour {

    Transform _spawnedTR;

    [SerializeField]
    UISprite _selectedNeko;
    [SerializeField]
    UILabel _lblNekoName;
    [SerializeField]
    UILabel _lblNekoInfo;

    [SerializeField] UIButton _btnRight;
    [SerializeField] UIButton _btnLeft;
    [SerializeField]
    UICenterOnChild _centerOnChild;

    [SerializeField]
    GameObject _centerObject;

    List<GameObject> _listCollections = new List<GameObject>();
    int _tmpIndex;

    void OnEnable() {

        InitUserCollection();

    }

    void Update() {
        if ( (_centerOnChild.centeredObject == _centerObject) && _centerObject != null)
            return;

        // 센터 오브젝트가 변경되었을때만, 
        _tmpIndex = FindCollectionIndex(_centerOnChild.centeredObject);
        _btnLeft.gameObject.SetActive(false);
        _btnRight.gameObject.SetActive(false);
        _centerObject = _centerOnChild.centeredObject;

        if (_tmpIndex < 0)
            return;

        if (_tmpIndex > 0)
            _btnLeft.gameObject.SetActive(true);

        if (_tmpIndex + 1 < _listCollections.Count)
            _btnRight.gameObject.SetActive(true);

    }

    #region 이동 
    public void MoveRight() {
        int currentIndex = FindCollectionIndex(_centerObject);
        _centerOnChild.CenterOn(_listCollections[currentIndex + 1].transform);
    }

    public void MoveLeft() {
        int currentIndex = FindCollectionIndex(_centerObject);
        _centerOnChild.CenterOn(_listCollections[currentIndex - 1].transform);
    }
    #endregion

    int FindCollectionIndex(GameObject pCollection) {
        for(int i=0; i<_listCollections.Count; i++) {
            if (_listCollections[i] == pCollection)
                return i;
        }

        return -1;

    }

    /// <summary>
    /// 초기화 
    /// </summary>
    void InitUserCollection() {


        int index = 0;
        int i = 0;

        _selectedNeko.gameObject.SetActive(false);
        _lblNekoName.gameObject.SetActive(false);
        _lblNekoInfo.gameObject.SetActive(false);
        _centerObject = null;

        PoolManager.Pools[PuzzleConstBox.poolCollection].DespawnAll();
        _listCollections.Clear();

        while (index < GameSystem.Instance.StageMasterJSON.Count) {
            _spawnedTR = PoolManager.Pools[PuzzleConstBox.poolCollection].Spawn(PuzzleConstBox.prefabCollection, Vector3.zero, Quaternion.identity);

            _spawnedTR.localPosition = new Vector3(720 * i, 0, 0); // 위치 지정 
            _spawnedTR.GetComponent<CollectionCtrl>().InitCollection(GameSystem.Instance.StageMasterJSON[index], GameSystem.Instance.StageMasterJSON[index + 1], this);

            i++;
            index += 2; // 인덱스는 2씩증가한다. 

            _listCollections.Add(_spawnedTR.gameObject);
        }
    }


    public void CloseCollection() {
        PoolManager.Pools[PuzzleConstBox.poolCollection].DespawnAll();

        _btnLeft.gameObject.SetActive(false);
        _btnRight.gameObject.SetActive(false);
        this.gameObject.SetActive(false);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="pNekoID"></param>
    public void SetSelectNeko(int pNekoID) {

        Debug.Log("★★★ SetSelectNeko ::" + pNekoID);

        _selectedNeko.gameObject.SetActive(true);
        _lblNekoName.gameObject.SetActive(true);
        _lblNekoInfo.gameObject.SetActive(true);

        GameSystem.Instance.SetNekoSpriteByID(_selectedNeko, pNekoID);
        _selectedNeko.width = 200;
        _selectedNeko.height = 200;

        _lblNekoName.text = GameSystem.Instance.GetNekoName(pNekoID, 3);
        _lblNekoInfo.text = GameSystem.Instance.GetNekoDetail(pNekoID, 3);
    }
}

