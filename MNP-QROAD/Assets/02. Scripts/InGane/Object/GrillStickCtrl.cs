using UnityEngine;
using System.Collections;
using PathologicalGames;
using DG.Tweening;
    

public class GrillStickCtrl : MonoBehaviour {

    Vector3 _pos;
    Vector3 _destPos = new Vector3(3.2f, 3.65f, 0);

	public void SetGrillStick(Vector3 pPos) {
        _pos = new Vector3(pPos.x + 0.21f, pPos.y + 0.56f, 0);
        this.transform.localPosition = _pos;

        // 이동
        this.transform.DOLocalMove(_destPos, 0.5f).OnComplete(OnCompleteMove).SetEase(Ease.InBack);
        InGameCtrl.Instance.TotalFishGrillCount--;

        Invoke("DespawnForce", 0.3f);
    }

    void DespawnForce() {

        this.transform.DOKill();
        OnCompleteMove();

    }

    void OnCompleteMove() {
        PoolManager.Pools[PuzzleConstBox.objectPool].Despawn(this.transform);
        
    }
}
