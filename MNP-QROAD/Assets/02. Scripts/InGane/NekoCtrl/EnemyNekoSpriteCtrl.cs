using UnityEngine;
using System.Collections;

public class EnemyNekoSpriteCtrl : MonoBehaviour {


    Camera _worldCam;
    Camera _uiCam;

    private bool _isInit = false;

    [SerializeField]
    private Transform _target;

    private Vector3 targetPos = Vector3.zero; // 타겟의 월드좌표 
    private Vector3 pos = Vector3.zero;  // 스프라이트가 위치할 좌표

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        /*
        if (!_isInit)
            return;
        */

        targetPos = _target.position;
        targetPos.x = targetPos.x - 0.35f;
        targetPos.y = targetPos.y + 0.7f;

        //타겟의 포지션을 월드좌표에서 ViewPort좌표로 변환하고 다시 ViewPort좌표를 NGUI월드좌표로 변환합니다.
        //pos = guiCam.ViewportToWorldPoint (worldCam.WorldToViewportPoint (nekoTarget.transform.position));
        pos = _uiCam.ViewportToWorldPoint(_worldCam.WorldToViewportPoint(targetPos));
        pos.z = 0;

        transform.position = pos;
    }


    public void InitPos(GameObject pTarget) {
        _target = pTarget.transform;

        _worldCam = NGUITools.FindCameraForLayer(_target.gameObject.layer);
        _uiCam = NGUITools.FindCameraForLayer(this.gameObject.layer);


        this.gameObject.SetActive(true);
        _isInit = true;
    }
}
