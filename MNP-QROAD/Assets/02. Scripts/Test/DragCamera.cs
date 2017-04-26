using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class DragCamera : MonoBehaviour
{
	public float dragSpeed = 0.5f;
	private Vector3 dragOrigin;

	public Vector3 pos;
	public Vector3 move;
	
	public bool cameraDragging = true;
	
	public float outerLeft = 0f;
	public float outerRight = 14.4f;


	public int robbyIndex = 0;
	public List<Vector3> listRobbyPos = new List<Vector3>();


	void Start() {
		listRobbyPos.Add (new Vector3 (0, 0, -10));
		listRobbyPos.Add (new Vector3 (7.2f, 0, -10));
		listRobbyPos.Add (new Vector3 (14.4f, 0, -10));
	}

	
	void Update()
	{
		
		//Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

		/*
		float left = Screen.width * 0.2f;
		float right = Screen.width - (Screen.width * 0.2f);
		
		if(mousePosition.x < left)
		{
			cameraDragging = true;
		}
		else if(mousePosition.x > right)
		{
			cameraDragging = true;
		}
		*/
		
		/*
		if (cameraDragging) {
			
			if (Input.GetMouseButtonDown(0))
			{
				dragOrigin = Input.mousePosition;
				return;
			}
			
			if (!Input.GetMouseButton(0)) return;
			
			Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
			Vector3 move = new Vector3(pos.x * dragSpeed, 0, 0);
			
			if (move.x > 0f)
			{
				if(this.transform.position.x < outerRight)
				{
					transform.Translate(move, Space.World);
				}
			}
			else{
				if(this.transform.position.x > outerLeft)
				{
					transform.Translate(move, Space.World);
				}
			}
		}
		*/


		if (Input.GetMouseButtonDown (0)) {
			dragOrigin = Input.mousePosition;
			cameraDragging = true;
			return;
		}

		if (!Input.GetMouseButton (0))
			return;

		pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
		move = new Vector3(pos.x * dragSpeed, 0, 0);

		if (cameraDragging) {
			// 카메라 우측이동 
			if (move.x < 0) {

				if (robbyIndex + 1 >= listRobbyPos.Count) 
					return;
			
				robbyIndex++;
			
				this.transform.DOMove (listRobbyPos [robbyIndex], 0.5f);
				cameraDragging = false;


			} else if(move.x > 0){
				if (robbyIndex <= 0)
					return;
			
				robbyIndex--;
			
				this.transform.DOMove (listRobbyPos [robbyIndex], 0.5f);
				cameraDragging = false;

			} else {
				return;
			}

		}


		if (Input.GetMouseButtonUp (0)) {
			pos = Vector3.zero;
			move = Vector3.zero;
			cameraDragging = false;
		}

	}
	
	
}