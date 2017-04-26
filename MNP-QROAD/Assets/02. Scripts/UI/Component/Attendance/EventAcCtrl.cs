using UnityEngine;
using System.Collections;
using DG.Tweening;

public class EventAcCtrl : MonoBehaviour {


    [SerializeField] GameObject _cover;

    public void SetCover() {
        _cover.SetActive(true);
    }


    public void SetToday() {
        Invoke("SettingToday", 1);
    }

    private void SettingToday() {
        _cover.gameObject.SetActive(true);
        _cover.transform.localScale = new Vector3(2, 2, 1);
        _cover.transform.DOScale(1, 0.5f);
    }

}
