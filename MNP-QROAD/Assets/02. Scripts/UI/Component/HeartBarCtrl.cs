using UnityEngine;
using System.Collections;

public class HeartBarCtrl : MonoBehaviour {

    [SerializeField]
    UILabel lblHeartTime;


    [SerializeField]
    UILabel lblHeartValue;

    [SerializeField]
    GameObject _max;

    int _heartCount = 0;

    public void UpdateHearts(int pHeartCount) {

        Debug.Log("▶ UpdateHearts pHeartCount : " + pHeartCount);

        UpdateHeartTime();

        _heartCount = pHeartCount;


        lblHeartValue.text = _heartCount.ToString();

        if (_heartCount >= 5) {
            _max.SetActive(true);
            // lblHeartValue.gameObject.SetActive(false);
        }
        else {
            _max.SetActive(false);
            // lblHeartValue.gameObject.SetActive(true);
        }

    }

    public void UpdateHeartTime() {
        lblHeartTime.text = GameSystem.Instance.GetRemainTakeHeartTimeTick();
    }
}
