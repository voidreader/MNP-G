using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NGUIPlayEffectCtrl : MonoBehaviour {

    [SerializeField] UISpriteAnimation _spriteAnimation;
    [SerializeField] UISprite _sprite;

    [SerializeField] UIAtlas _InGameAtlas;

    /// <summary>
    /// 위치 지정 플레이 
    /// </summary>
    /// <param name="pEffectID"></param>
    /// <param name="pPos"></param>
    public void PlayPos(NGUIEffectType pType, Vector3 pPos) {

        this.gameObject.SetActive(true);

        this.transform.position = pPos;

        switch(pType) {
            case NGUIEffectType.InGameWhiteLight:
                _sprite.atlas = _InGameAtlas;
                _spriteAnimation.namePrefix = "mission-fx";
                _spriteAnimation.framesPerSecond = 15;
                _spriteAnimation.loop = false;
                _sprite.MakePixelPerfect();
                break;


            default:
                Debug.Log("No Type");
                this.gameObject.SetActive(false);
                return;
        }




        _spriteAnimation.Play();
    }

    IEnumerator CheckingPlay() {
        while (_spriteAnimation.isPlaying) {
            yield return new WaitForSeconds(0.1f);
        }

        this.gameObject.SetActive(false);
    }
}
