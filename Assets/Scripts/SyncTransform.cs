using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class SyncTransform : NetworkBehaviour {
	
	[SerializeField][SyncVar]private Vector3 SyncPosition;
	[SerializeField][SyncVar]private Quaternion SyncRotation;


	public float lerpRate = 15f;
	public float positionDelta = 0.2f;
	public float rotationDelta = 0.1f;

	void FixedUpdate(){
		if (isServer){
			//Debug.Log ("Server updating SyncPosition");
			//Debug.Log ("SyncPosition = " + SyncPosition.ToString ());
			//Debug.Log ("SyncRotation = " + SyncRotation.ToString ());
			if ((SyncPosition - transform.position).magnitude > positionDelta) {
				SyncPosition = transform.position;
			}
			if (Quaternion.Dot (SyncRotation, transform.localRotation) < 1f - rotationDelta) {
				SyncRotation = transform.localRotation;
			}
		}
		else {
			//Debug.Log ("Client updating Position");
			//Debug.Log ("SyncPosition = " + SyncPosition.ToString ());
			//Debug.Log ("SyncRotation = " + SyncRotation.ToString ());
			transform.position = Vector3.Lerp (transform.position, SyncPosition, lerpRate*Time.fixedDeltaTime);
			transform.localRotation = Quaternion.Slerp (transform.localRotation, SyncRotation, lerpRate*Time.fixedDeltaTime);
		}
	}

	[ClientRpc]
	public void RpcUpdateTransform(Vector3 pos, Quaternion rot){
		transform.position = Vector3.Lerp (transform.position, pos, lerpRate);
		transform.localRotation = Quaternion.Lerp (transform.localRotation, rot, lerpRate);
	}

}
