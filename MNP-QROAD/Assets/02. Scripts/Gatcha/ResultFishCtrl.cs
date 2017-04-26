using UnityEngine;
using System.Collections;
using PathologicalGames;
using DG.Tweening;

public class ResultFishCtrl : MonoBehaviour {

    [SerializeField] UISprite _fish;
    [SerializeField] UILabel _text;
        
    void OnSpawned() {
        this.transform.localScale = GameSystem.Instance.BaseScale;
    }


	public void SetResultFish(FishType pType, int pCount) {
        GameSystem.Instance.SetFishSprite(_fish, pType);

        if(pType == FishType.Chub) {
            _text.text = GameSystem.Instance.GetLocalizeText(4320).Replace("[n]", pCount.ToString());
        } else if (pType == FishType.Tuna) {
            _text.text = GameSystem.Instance.GetLocalizeText(4320).Replace("[n]", pCount.ToString());
        } else if (pType == FishType.Salmon) {
            _text.text = GameSystem.Instance.GetLocalizeText(4320).Replace("[n]", pCount.ToString());
        }
    }

    public void Focus() {
        _fish.transform.DOShakeScale(0.2f, 1, 5, 30);
        _text.transform.DOShakeScale(0.2f, 1, 5, 30);

        LobbyCtrl.Instance.PlayNekoRewardGet();
    }
}
