using UnityEngine;
using System.Collections;
using DG.Tweening;

public class TutorialHandCtrl : MonoBehaviour {

	[SerializeField] UISprite _hand;
    [SerializeField] Vector3 _targetPos;

    private Vector3 _originPos;
    private Vector3 _movePos;

    Camera worldCam = null;
    Camera guiCam = null;

    // localPosition의 x좌표가 200을 넘길 경우 반전이동으로 처리 
    Vector3 _rightMovePos = new Vector3(100, 100, 0);
    Vector3 _leftMovePos = new Vector3(-100, 100, 0);

    Vector3 _rightOriginPos = new Vector3(80, 80, 0);
    Vector3 _leftOriginPos = new Vector3(-80, 80, 0);

    [SerializeField] Vector3 _debugLocalPos;


    private Vector3 _worldTargetPos = Vector3.zero; // 타겟의 월드좌표 

    public void SetDisable() {
        this.gameObject.SetActive(false);
    }

    public void SetEnable(Vector3 pPos) {
        _hand.transform.DOKill();
        _hand.transform.localPosition = _rightOriginPos;

        _targetPos = pPos;
        this.gameObject.transform.localPosition = _targetPos;
        this.gameObject.SetActive(true);

        SetFlipMovement();

        _hand.transform.DOLocalMove(_movePos, 1).SetLoops(-1, LoopType.Restart);

    }


    public void SetTargetPos(Transform pTarget) {


        _hand.transform.DOKill();
        _hand.transform.localPosition = _rightOriginPos;

        worldCam = NGUITools.FindCameraForLayer(pTarget.gameObject.layer);
        guiCam = NGUITools.FindCameraForLayer(this.gameObject.layer);

        _worldTargetPos = pTarget.transform.position;
        //_worldTargetPos.x = _worldTargetPos.x + 0.5f;
        //_worldTargetPos.y = _worldTargetPos.y + 0.5f;

        _targetPos = guiCam.ViewportToWorldPoint(worldCam.WorldToViewportPoint(_worldTargetPos));
        _targetPos.z = 0;



        this.gameObject.transform.position = _targetPos;
        this.gameObject.SetActive(true);


        SetFlipMovement();

        _hand.transform.DOLocalMove(_movePos, 1).SetLoops(-1, LoopType.Restart);
    }

    /// <summary>
    /// Flip, 이동 좌표 처리 
    /// </summary>
    private void SetFlipMovement() {

        // 튜토리얼 내에서만 동작하도록 처리 
        /*
        if(TutorialCtrl.Instance == null) {
            _movePos = _rightMovePos;
            _hand.transform.localPosition = _rightOriginPos;
            _hand.flip = UIBasicSprite.Flip.Nothing;
            return;
        }
        */


        _debugLocalPos = this.transform.localPosition;
            

        if (this.transform.localPosition.x > 200) {
            _hand.transform.localPosition = _leftOriginPos;
            _movePos = _leftMovePos;
            _hand.flip = UIBasicSprite.Flip.Horizontally;
        }
        else {
            _hand.transform.localPosition = _rightOriginPos;
            _movePos = _rightMovePos;
            _hand.flip = UIBasicSprite.Flip.Nothing;
        }

    }
}
