using UnityEngine;
using System.Collections;
using PathologicalGames;
using DG.Tweening;

public class BlockColorScoreCtrl : MonoBehaviour {


    [SerializeField]
    private Camera worldCam = null;
    [SerializeField]
    private Camera guiCam = null;
    [SerializeField]
    private UILabel lblHitText = null;

    private GameObject target;
    private Vector3 targetPos;
    private Vector3 textPos;

    int _currentScore = 0;

    [SerializeField] UIFont _blueFont;
    [SerializeField] UIFont _redFont;
    [SerializeField] UIFont _yellowFont;

    [SerializeField] UISprite _spX;

    // Use this for initialization
    void Start () {
        if (worldCam == null) {

            worldCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
            guiCam = NGUITools.FindCameraForLayer(this.gameObject.layer);
            
        }
    }





    /// <summary>
    /// 타겟 처리 
    /// </summary>
    /// <param name="pObj"></param>
    /// <param name="pBlockID"></param>
    /// <param name="pScore"></param>
    public void SetTarget(GameObject pObj, int pBlockID, int pScore, int pX) {

        this.gameObject.SetActive(true);
        this.transform.localScale = GameSystem.Instance.BaseScale;

        //Debug.Log ("▶ SetTarget ::" + pDamage);
        target = pObj;

        // 위치 처리 
        SetTextPos();

        // 스코어 
        _currentScore = pScore;


        // 레이블 처리 
        lblHitText.text = string.Format("{0:n0}", _currentScore);

        if (pBlockID == 0)
            lblHitText.bitmapFont = _blueFont;
        else if (pBlockID == 1)
            lblHitText.bitmapFont = _yellowFont;
        else
            lblHitText.bitmapFont = _redFont;



        if (pX == 3) {
            _spX.gameObject.SetActive(true);
            _spX.spriteName = PuzzleConstBox.spriteX2;
        }
        else if (pX == 4) {
            _spX.gameObject.SetActive(true);
            _spX.spriteName = PuzzleConstBox.spriteX3;
        }
        else {
            _spX.gameObject.SetActive(false);
        }

        // 점수 추가 
        InUICtrl.Instance.AddScore(_currentScore);

        this.transform.DOLocalMoveY(this.transform.localPosition.y + 20, 0.4f).OnComplete(OnCompleteMove);
    }

    private void SetTextPos() {
        targetPos = target.transform.position;

        if (worldCam == null) {

            worldCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
            guiCam = NGUITools.FindCameraForLayer(this.gameObject.layer);
            
        }


        // 추가 위치 조정 
        targetPos.y = targetPos.y + 0.5f;
        

        textPos = guiCam.ViewportToWorldPoint(worldCam.WorldToViewportPoint(targetPos));
        textPos.z = 0;

        this.transform.position = textPos;


    }




    private void OnCompleteMove() {
        PoolManager.Pools[PuzzleConstBox.inGameUIPool].Despawn(this.transform);
    }
}
