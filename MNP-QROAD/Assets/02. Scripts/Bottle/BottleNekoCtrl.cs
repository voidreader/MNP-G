using UnityEngine;
using System.Collections;
using PathologicalGames;
using DG.Tweening;

public class BottleNekoCtrl : MonoBehaviour {

	public int id;

    private NekoAppearCtrl _appear; // 외향(NGUI)


	private Vector3 rightHScale = new Vector3(0,0,0);
	private Vector3 leftHScale = new Vector3(0,180,0);

	private SphereCollider scollider;
    [SerializeField] Rigidbody rigid;
	private Transform trNeko;
	private Vector3 distVec;
	private float movingRadius = 0.25f;
	private float idleRadius = 0.5f;

    [SerializeField] SphereCollider _sphere;

	// Use this for initialization
	void Start () {
	
		trNeko = this.transform;
		scollider = this.GetComponent<SphereCollider> ();
		rigid = this.GetComponent<Rigidbody> ();

	}
	

	void OnSpawned() {
        //this.transform.localScale = GameSystem.Instance.BaseScale;
	}



	/// <summary>
	/// 로비 네코 터치.
	/// </summary>
	public void HitLobbyNeko() {

        if (rigid.useGravity)
            return;

        rigid.AddForce(new Vector3(Random.Range(-600, 600), Random.Range(-600, 600), 0));
        LobbyCtrl.Instance.PlayLobbyNekoTouchSound();
    }


    // 고양이 깨우기 
    public void WakeUpNeko() {
        rigid.useGravity = false; // 중력 제거 

        rigid.AddForce(new Vector3(Random.Range(-600, 600), Random.Range(-600, 600), 0));
    }

    public void SleepNeko() {
        rigid.useGravity = true;
    }


    public void SetLobbyNeko(int pID, int pGrade, int pAppearIndex, bool pSmall = false) {
        // 리스트에 추가. 
        BottleManager.Instance.ListBottleNeko.Add(this);

        if(pSmall) {
            _appear = BottleManager.Instance.ListBottleNekoAppear[pAppearIndex].InitSmallNekoAppear(this.transform, pID, pGrade);
            _sphere.radius = 0.3f;
        }
        else {
            _appear = BottleManager.Instance.ListBottleNekoAppear[pAppearIndex].InitNekoAppear(this.transform, pID, pGrade);
            _sphere.radius = 0.4f;
        }


        // 랜덤하게 좌우를 바꾼다. 
        if (Random.Range(0, 2) == 0)
            this.transform.localRotation = Quaternion.Euler(leftHScale);
        else
            this.transform.localRotation = Quaternion.Euler(rightHScale);


        rigid.useGravity = true;
    }

    
}
