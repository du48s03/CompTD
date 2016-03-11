using UnityEngine;
using System.Collections;
using System;

public class CameraDrag : MonoBehaviour
{
	public float dragSpeed = 2;
	public Camera mainCam;
	private Vector3 dragOrigin;
	private Vector3 camOrigin;
	private bool dragging = false;


	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			//RaycastHit origin;
			RaycastHit[] hits = Physics.RaycastAll(mainCam.ScreenPointToRay (Input.mousePosition));
			for (int i = 0; i < hits.Length; i++) {
				if (hits [i].transform.gameObject.tag == "base") {
					dragOrigin = hits [i].point;
				}
			}
			dragOrigin.y = 0;
			camOrigin =  mainCam.transform.position;
			dragging = true;
			return;
		}

		if (dragging) {
			//Vector3 pos = mainCam.ScreenToViewportPoint (Input.mousePosition );
			mainCam.transform.position = camOrigin;
			Vector3 dragEnd = new Vector3();
			RaycastHit[] hits = Physics.RaycastAll(mainCam.ScreenPointToRay (Input.mousePosition));
			for (int i = 0; i < hits.Length; i++) {
				if (hits [i].transform.gameObject.tag == "base")
					dragEnd = hits [i].point;
			}
			dragEnd.y = 0;
			Vector3 move  = dragOrigin - dragEnd;
			mainCam.transform.Translate (move, Space.World);
			//dragOrigin = dragEnd;
		}

		if (Input.GetMouseButtonUp (0)) {
			dragging = false;
		}
	}
		
}