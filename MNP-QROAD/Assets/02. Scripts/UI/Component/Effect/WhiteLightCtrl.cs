using UnityEngine;
using System;
using System.Collections;

public class WhiteLightCtrl : MonoBehaviour {

    [SerializeField]
    UISpriteAnimation _whiteSprite;

    int _randomScale;

    

    public void PlayRandomScale(Vector3 pPos) {
        this.gameObject.SetActive(true);
        _whiteSprite.transform.localPosition = pPos;

        // 크기 
        _randomScale = UnityEngine.Random.Range(350, 500);
        _whiteSprite.GetComponent<UIWidget>().width = _randomScale;
        _whiteSprite.GetComponent<UIWidget>().height = _randomScale;

        _whiteSprite.Play();


        StartCoroutine(CheckingPlay());
    }

    public void PlayLocalPos(Vector3 pPos) {
        this.gameObject.SetActive(true);
        _whiteSprite.transform.localPosition = pPos;

        _whiteSprite.Play();

        StartCoroutine(CheckingPlay());
    }


    public void PlayWorldPos(Vector3 pPos) {
        this.gameObject.SetActive(true);
        _whiteSprite.transform.position = pPos;

        _whiteSprite.Play();

        StartCoroutine(CheckingPlay());
    }


    public void PlayWorldPos(Vector3 pPos, Action<int> pCallback, int pIndex) {
        this.gameObject.SetActive(true);
        _whiteSprite.transform.position = pPos;

        _whiteSprite.Play();

        StartCoroutine(CheckingPlayWithCallback(pCallback, pIndex));
    }

    public void Play() {
        this.gameObject.SetActive(true);
        _whiteSprite.GetComponent<UIWidget>().width = 232;
        _whiteSprite.GetComponent<UIWidget>().height = 232;
        _whiteSprite.Play();

        StartCoroutine(CheckingPlay());
    }

    IEnumerator CheckingPlay() {
        while (_whiteSprite.isPlaying) {
            yield return new WaitForSeconds(0.1f);
        }

        this.gameObject.SetActive(false);

    }

    IEnumerator CheckingPlayWithCallback(Action<int> pCallback, int pIndex) {
        while (_whiteSprite.isPlaying) {
            yield return new WaitForSeconds(0.1f);
        }

        this.gameObject.SetActive(false);

        pCallback(pIndex);

    }

}
