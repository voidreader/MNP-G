using UnityEngine;
using System.Collections;
using DG.Tweening;


    

public class MoveHorizontalEffectCtrl : MonoBehaviour {

    [SerializeField]
    bool _isRightMove = true; // 에디터 지정 

    [SerializeField]
    float _posX; // 에디터 지정 

    void OnEnable() {




        // 스케일 조정과, 수평이동 
        this.transform.DOKill();
        this.transform.localScale = GameSystem.Instance.BaseScale;

        this.transform.DOScale(1.2f, 0.5f).SetLoops(-1, LoopType.Yoyo);

        /*
        if (_isRightMove) {
            this.transform.DOLocalMoveX(_posX + 5f, 0.5f).SetLoops(-1, LoopType.Yoyo);
        }
        else {
            this.transform.DOLocalMoveX(_posX - 5f, 0.5f).SetLoops(-1, LoopType.Yoyo);
        }
        */

    }


}
