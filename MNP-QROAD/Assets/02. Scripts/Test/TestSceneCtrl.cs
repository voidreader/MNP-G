using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PathologicalGames;
using DG.Tweening;

public class TestSceneCtrl : MonoBehaviour {


	public UILabel lblTime;
	public UILabel lblTime2;

	public UILabel lblTick;
	public UILabel lblTick2;

	public System.DateTime dt;

	// Use this for initialization
	void Start () {

	}

	void Update() {
		if (Input.GetKeyDown (KeyCode.Z)) {

			// 시간 계산 
			dt = System.DateTime.Now.AddMinutes(1);
			lblTime.text = string.Format("{0:D2}:{1:D2}:{2:D2}", dt.Hour, dt.Minute, dt.Second);
			lblTick.text = dt.Ticks.ToString();
		}

		if(Input.GetKeyDown(KeyCode.X)) {


			System.TimeSpan ts = dt - System.DateTime.Now;
			lblTime2.text = string.Format("{0:D2}:{1:D2}:{2:D2}", ts.Hours, ts.Minutes, ts.Seconds);
			lblTick2.text = ts.Ticks.ToString();

			//System.TimeSpan ts2 = new System.TimeSpan(
		}
	}




}
