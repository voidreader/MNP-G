using UnityEngine;
using System.Collections;

public class SparkLightCtrl : MonoBehaviour {

    [SerializeField] UISpriteAnimation _sprite;

    public void PlayCurrentPos() {
        this.gameObject.SetActive(true);
        _sprite.Play();

        StartCoroutine(CheckingPlay());
    }

    public void Play(Vector3 pPos) {
        this.gameObject.SetActive(true);
        this.transform.localPosition = pPos;

        _sprite.Play();

        StartCoroutine(CheckingPlay());
    }

    IEnumerator CheckingPlay() {
        while (_sprite.isPlaying) {
            yield return new WaitForSeconds(0.1f);
        }

        this.gameObject.SetActive(false);

    }
}
