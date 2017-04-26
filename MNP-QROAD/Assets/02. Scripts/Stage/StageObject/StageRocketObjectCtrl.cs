using UnityEngine;
using System.Collections;
using DG.Tweening;

public class StageRocketObjectCtrl : MonoBehaviour {

    [SerializeField]
    GameObject _fire;


    public void OnFire() {
        StartCoroutine(DoingFire());

        // 수직으로 올라가기. 
        this.transform.DOLocalMoveY(this.transform.localPosition.y + 400, Random.Range(1.5f, 3f)).SetEase(Ease.InQuart).OnComplete(OnComppleteMove);
    }

    void OnComppleteMove() {
        StopCoroutine(DoingFire());
        this.gameObject.SetActive(false);
    }
        

    IEnumerator DoingFire() {
        while (true) {
            _fire.SetActive(!_fire.activeSelf);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
