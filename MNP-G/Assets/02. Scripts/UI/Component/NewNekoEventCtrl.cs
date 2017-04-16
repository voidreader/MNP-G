using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;


public class NewNekoEventCtrl : MonoBehaviour {


    [SerializeField]
    UIButton _eventButton;
    List<string> _listImages = new List<string>();

    [SerializeField] UISpriteAnimation _lighting;

    int _imageIndex = 0;


	// Use this for initialization
	void Start () {
        _listImages.Add("vent-151");
        _listImages.Add("vent-152");
        _listImages.Add("vent-153");
        _listImages.Add("vent-154");
        _listImages.Add("vent-155");



		CheckEventNekoExists ();
    }

	public void CheckEventNekoExists() {

        if (!this.gameObject.activeSelf)
            return;

		// 151~5
		if (GameSystem.Instance.FindUserNekoData (151) >= 0
		    && GameSystem.Instance.FindUserNekoData (152) >= 0
		    && GameSystem.Instance.FindUserNekoData (153) >= 0
		    && GameSystem.Instance.FindUserNekoData (154) >= 0
		    && GameSystem.Instance.FindUserNekoData (155) >= 0) {

			this.gameObject.SetActive (false);
		} else {
			this.gameObject.SetActive (true);
		}
	}


    public void Change() {

        this.transform.DOKill();
        this.transform.localScale = GameSystem.Instance.BaseScale;
        this.transform.DOScale(1.2f, 0.5f).SetLoops(-1, LoopType.Yoyo);
        //this.transform.localEulerAngles = Vector3.zero;


        StartCoroutine(Changing());
        //StartCoroutine(Lighting());
    }

    IEnumerator Changing() {

        while(true) {
            yield return new WaitForSeconds(3);
            _imageIndex++;

            if (_imageIndex >= _listImages.Count)
                _imageIndex = 0;

            _eventButton.normalSprite = _listImages[_imageIndex];
            _lighting.gameObject.SetActive(true);
            _lighting.Play();

        }

    }

    IEnumerator Lighting() {

        //yield return new WaitForSeconds(1);

        _lighting.gameObject.SetActive(true);
        _lighting.Play();

        while(true) {
            yield return new WaitForSeconds(0.1f);

            if(!_lighting.isPlaying) {
                _lighting.gameObject.SetActive(false);
            }
            else {
                continue;
            }

            yield return new WaitForSeconds(3);

            _lighting.gameObject.SetActive(true);
            _lighting.Play();

        }


    }

    /// <summary>
    /// 이벤트창 오픈 
    /// </summary>
    public void OpenNewNekoMission() {
        WindowManagerCtrl.Instance.OpenNewNekoPage();
        AdbrixManager.Instance.SendAdbrixInAppActivity(AdbrixManager.Instance.BUTTON_NEWFIVENEKO);
    }
	
	
}
