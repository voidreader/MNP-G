using UnityEngine;
using System.Collections;
using DG.Tweening;

public class StageLockObjectCtrl : MonoBehaviour {


    public Transform _bigCircleOut;
    public Transform _bigCircleIn;

    public Transform _midCircleOut1;
    public Transform _midCircleOut2;
    public Transform _midCircleOut3;
    public Transform _midCircleOut4;
    public Transform _midCircleIn1;
    public Transform _midCircleIn2;
    public Transform _midCircleIn3;
    public Transform _midCircleIn4;


    public Transform _smallCircleOut1;
    public Transform _smallCircleOut2;
    public Transform _smallCircleOut3;
    public Transform _smallCircleOut4;

    public Transform _smallCircleIn1;
    public Transform _smallCircleIn2;
    public Transform _smallCircleIn3;
    public Transform _smallCircleIn4;


    Vector3 _rightVector = new Vector3(0, 0, -360);
    Vector3 _leftVector = new Vector3(0, 0, 360);
    float _rotateTime = 10;

    void OnEnable() {

        _bigCircleOut.DOLocalRotate(_rightVector, _rotateTime, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
        _bigCircleIn.DOLocalRotate(_leftVector, _rotateTime, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);

        _midCircleOut1.DOLocalRotate(_rightVector, _rotateTime, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
        _midCircleOut2.DOLocalRotate(_rightVector, _rotateTime, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
        _midCircleOut3.DOLocalRotate(_rightVector, _rotateTime, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
        _midCircleOut4.DOLocalRotate(_rightVector, _rotateTime, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);

        _midCircleIn1.DOLocalRotate(_leftVector, _rotateTime, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
        _midCircleIn2.DOLocalRotate(_leftVector, _rotateTime, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
        _midCircleIn3.DOLocalRotate(_leftVector, _rotateTime, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
        _midCircleIn4.DOLocalRotate(_leftVector, _rotateTime, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);


        _smallCircleOut1.DOLocalRotate(_rightVector, _rotateTime, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
        _smallCircleOut2.DOLocalRotate(_rightVector, _rotateTime, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
        _smallCircleOut3.DOLocalRotate(_rightVector, _rotateTime, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
        _smallCircleOut4.DOLocalRotate(_rightVector, _rotateTime, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);

        _smallCircleIn1.DOLocalRotate(_leftVector, _rotateTime, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
        _smallCircleIn2.DOLocalRotate(_leftVector, _rotateTime, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
        _smallCircleIn3.DOLocalRotate(_leftVector, _rotateTime, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
        _smallCircleIn4.DOLocalRotate(_leftVector, _rotateTime, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);

    }
}
