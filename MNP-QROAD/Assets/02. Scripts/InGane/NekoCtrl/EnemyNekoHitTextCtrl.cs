using UnityEngine;
using System.Collections;
using PathologicalGames;
using DG.Tweening;


public class EnemyNekoHitTextCtrl : MonoBehaviour {

	[SerializeField] private Camera worldCam = null;
	[SerializeField] private Camera guiCam = null;
	[SerializeField] private UILabel lblHitText = null;

	private GameObject target;
	private Vector3 targetPos;
	private Vector3 textPos;
	private bool isInit = false;

	private float randomPosX;
	private float randomPosY;
	//private UILabel lblHitText = null;




	// Use this for initialization
	void Start () {

		if (worldCam == null) {

			worldCam = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera> ();
			guiCam = NGUITools.FindCameraForLayer (this.gameObject.layer);
			lblHitText = this.gameObject.GetComponent<UILabel> ();
		}
	}

	public void OnSpawned() {
		isInit = false;	
		this.transform.localScale = GameSystem.Instance.BaseScale;

		if (worldCam == null) {
			worldCam = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera> ();
			guiCam = NGUITools.FindCameraForLayer (this.gameObject.layer);
			lblHitText = this.gameObject.GetComponent<UILabel> ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (!isInit)
			return;

		SetTextPos ();
	}

	public void SetBigTarget(GameObject pObj, float pDamage) {
		this.transform.localScale = new Vector3 (4, 4, 1);
		SetTarget (pObj, pDamage,2);
	}

	public void SetTarget(GameObject pObj, float pDamage) {
		SetTarget (pObj, pDamage, 1);
	}

	public void SetTarget(GameObject pObj, float pDamage, float pScaleTime) {

		//Debug.Log ("▶ SetTarget ::" + pDamage);


		randomPosX = Random.Range (-0.2f, 0.2f);
		randomPosY = Random.Range (-0.2f, 0.2f);

		target = pObj;

		SetTextPos ();

		isInit = true;

		this.transform.DOScale (0, pScaleTime).OnComplete(OnCompleteScale);

        // 점수 추가 
        InUICtrl.Instance.AddScore (GetScoreValue (pDamage));

        // Label 처리 
        lblHitText.text = GetScoreValue(pDamage).ToString();
        
        

	}

	private void SetTextPos() {
		targetPos = target.transform.position;
		targetPos.x = targetPos.x + randomPosX;
		targetPos.y = targetPos.y + randomPosY;

		// 추가 위치 조정 
		targetPos.y = targetPos.y + 0.5f;
		targetPos.x = targetPos.x + 0.5f;

		textPos = guiCam.ViewportToWorldPoint(worldCam.WorldToViewportPoint(targetPos));
		textPos.z = 0;

		this.transform.position = textPos;


	}

	private int GetScoreValue(float pDamage) {
        /*
		if (pDamage < 10) {
			return 100 + (Random.Range(50,100) * InUICtrl.Instance.comboCnt);
		} else if (pDamage >= 10 && pDamage < 100) {
			return 1000 + (Random.Range(50,100) *  InUICtrl.Instance.comboCnt);
		} else {
			return 5000 + (Random.Range(50,100) * InUICtrl.Instance.comboCnt);
		}
        */

        return Mathf.RoundToInt(pDamage);


	}


	private void OnCompleteScale() {
		PoolManager.Pools ["UI"].Despawn (this.transform);
	}


}
