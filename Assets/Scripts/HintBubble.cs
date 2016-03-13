using UnityEngine;
using System.Collections;

public class HintBubble : MonoBehaviour {

	public GameObject owner;
	public Camera camera;
	public Vector3 offset;
	public float distanceToCamera = 10;
	
	// Update is called once per frame
	void FixedUpdate () {
		//Vector3 cameraloc = camera.transform.position;
		transform.LookAt (transform.position  + camera.transform.rotation*Vector3.forward
						, camera.transform.rotation * Vector3.up);
		Vector3 targetPoint = camera.WorldToScreenPoint (owner.transform.position) + offset;
		targetPoint.z =distanceToCamera;
		targetPoint = camera.ScreenToWorldPoint (targetPoint);

		transform.position = Vector3.Lerp(transform.position, targetPoint, 0.5f);
		return;
	}
}
