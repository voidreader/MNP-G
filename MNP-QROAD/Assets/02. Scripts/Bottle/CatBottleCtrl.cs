using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PathologicalGames;


public class CatBottleCtrl : MonoBehaviour {

    [SerializeField]
    List<UISprite> _listBottleCat = new List<UISprite>();


	// Use this for initialization
	void Start () {
	
	}

	
    /// <summary>
    /// 
    /// </summary>
	public void InitBottle() {

        /*
        for(int i=0; i<GameSystem.Instance.UserNeko.Count; i++) {
            if (_listBottleCat.Count <= i)
                return;

            GameSystem.Instance.SetNekoSprite(_listBottleCat[i], GameSystem.Instance.UserNeko[i]["tid"].AsInt, GameSystem.Instance.UserNeko[i]["star"].AsInt);
            _listBottleCat[i].gameObject.SetActive(true);
        }
        */

    }


    void OnSpawned() {
        this.transform.localScale = GameSystem.Instance.BaseScale;
    }
}
