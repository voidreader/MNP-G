using UnityEngine;
using System.Collections;

public class YellowLightCtrl : MonoBehaviour {

    [SerializeField] UISpriteAnimation _yellowSprite;

    int _randomScale;

    public void PlayRandomScale(Vector3 pPos) {
        this.gameObject.SetActive(true);
        _yellowSprite.transform.localPosition = pPos;

        // 크기 
        _randomScale = Random.Range(300, 500);
        _yellowSprite.GetComponent<UIWidget>().width = _randomScale;
        _yellowSprite.GetComponent<UIWidget>().height = _randomScale;

        _yellowSprite.Play();




        StartCoroutine(CheckingPlay());
    }

    public void Play() {
        this.gameObject.SetActive(true);
        _yellowSprite.Play();

        StartCoroutine(CheckingPlay());
    }

    IEnumerator CheckingPlay() {
        while(_yellowSprite.isPlaying) {
            yield return new WaitForSeconds(0.1f);
        }

        this.gameObject.SetActive(false);

    }
	
	
}
