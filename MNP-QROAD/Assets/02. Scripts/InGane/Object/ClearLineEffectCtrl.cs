using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathologicalGames;

public class ClearLineEffectCtrl : MonoBehaviour {

    [SerializeField] tk2dSpriteAnimator _clip;

    Vector3 horizontalRot = Vector3.zero;
    Vector3 verticalRot = new Vector3(0, 0, 90);

    private void Start() {
        _clip.AnimationCompleted += LineCompleteDelegate;
    }



    /// <summary>
    /// 생성 후 플레이 
    /// </summary>
    /// <param name="pPos"></param>
    /// <param name="pIsVertical"></param>
    public void SetSpawnPos(Vector3 pPos, bool pIsVertical) {

        if (pIsVertical)
            this.transform.localEulerAngles = verticalRot;
        else
            this.transform.localEulerAngles = horizontalRot;

        this.transform.position = pPos;

        _clip.Play();

    }


    public void LineCompleteDelegate(tk2dSpriteAnimator pSprite, tk2dSpriteAnimationClip pClip) {
        PoolManager.Pools[PuzzleConstBox.objectPool].Despawn(this.transform);
    }


}
