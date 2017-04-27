using UnityEngine;
using System.Collections;

public class TalkativeCatCtrl : MonoBehaviour {

    int index = 0;
    [SerializeField] GameObject _talking;
    [SerializeField] UILabel _lblTalk;
    [SerializeField] UISprite _cat;

    string text1 = "イベントちゅう！";
    string text2 = "たっぷするにゃ！";

    string _spriteNeko1 = "neko150-1";
    string _spriteNeko2 = "neko150-3";


    int _eventNoticeIndex = -1;

    // Use this for initialization
    void Start () {
        
	}

    public void Talk() {

        if(this.gameObject.activeSelf)
            StartCoroutine(Talking());
    }
	
	IEnumerator Talking() {


        while(true) {

            //_cat.spriteName = _spriteNeko1;
            _talking.SetActive(false);

            yield return new WaitForSeconds(3);

            _talking.SetActive(true);
            //_cat.spriteName = _spriteNeko2;
            if (index == 0)
                _lblTalk.text = text1;

            index++;

            yield return new WaitForSeconds(3);

            _talking.SetActive(false);
            //_cat.spriteName = _spriteNeko1;

            yield return new WaitForSeconds(3);


            _talking.SetActive(true);
            //_cat.spriteName = _spriteNeko2;
            if (index == 1)
                _lblTalk.text = text2;

            index--;

            yield return new WaitForSeconds(3);

        }


    }

    /// <summary>
    /// 
    /// </summary>
    public void OpenEventPage() {

        WindowManagerCtrl.Instance.OpenEventPageOnly();

        //WindowManagerCtrl.Instance.OpenNoticeDetail(_id);

        /*
        for(int i = 0; i<GameSystem.Instance.NoticeBannerInitJSON.Count;i++) {
            if (GameSystem.Instance.NoticeBannerInitJSON[i]["smallbannerurl"].Value == "http://pamobile.x-legend.co.jp/mnp/banner/crane_160815a.png") {
                _eventNoticeIndex = i;

                WindowManagerCtrl.Instance.OpenNoticeDetail(_eventNoticeIndex);


            }
        }
        */

    }
}
