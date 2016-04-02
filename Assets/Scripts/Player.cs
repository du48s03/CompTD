using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {

	private GameObject focus;
	private Vector3 startPosition;
	private bool targetSelecting = false;
	public delegate void callbackSelectTarget (GameObject target);
	public GameObject spawnPointPrefab;

	void Update(){
		if (!isLocalPlayer)
			return;
		if (targetSelecting)
			return;
		transform.position += new Vector3 (Input.GetAxisRaw ("Horizontal"), 0f, Input.GetAxisRaw ("Vertical")) * 0.2f;
		if (Input.GetKeyDown (KeyCode.Space))
			//GetComponent<PlayerNetworkHandler>().CmdSpawnWithAuthority(spawnPointPrefab.name, transform.position, Quaternion.identity);		
			GetComponent<PlayerNetworkHandler>().CmdSpawnSpawnPoint(transform.position, Quaternion.identity, GetComponent<NetworkIdentity>().netId);
		if (Input.GetMouseButtonDown (0)) {
			startPosition = Input.mousePosition;
		}
		if (Input.GetMouseButtonUp (0)) {
			Vector3 endPosition = Input.mousePosition;
			if (startPosition == endPosition) {
				RaycastHit hit;
				bool hasHit = Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit);
				if (!hasHit|| hit.transform.GetComponent<Focusable> () == null) {
					setFocus (null);
				} else {
					setFocus (hit.transform.gameObject);
				}
			}
		}
	}

	private void setFocus(GameObject target){
		if(focus != null)
			focus.GetComponent<Focusable>().OnLoseFocus(this);
		focus = target;
		if(focus != null)
			target.GetComponent<Focusable>().OnGainFocus(this);
	}

	public IEnumerator SelectTarget(callbackSelectTarget callback){
		targetSelecting = true;
		Vector3 startPosition = Vector3.back;
		GameObject prev_focus = focus;
		setFocus (null);
		//Debug.Log ("Start Selecting Target");
		while (true) {
			//Debug.Log ("Selecting target....");
			if (Input.GetMouseButtonDown (0)) {
				startPosition = Input.mousePosition;
				yield return null;
			}
			if (Input.GetMouseButtonUp (0)) {
				Vector3 endPosition = Input.mousePosition;
				if (endPosition == startPosition) {
					RaycastHit hit;
					if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit)) {
						GameObject target = hit.transform.gameObject;
						if (target.GetComponent<Targetable> () && target.GetComponent<Targetable> ().IsTargetable (gameObject)) {
							//Debug.Log ("Selected legal target");
							targetSelecting = false;
							callback (target);
							setFocus (prev_focus);
							break;
						}
					}
				}
			}
			yield return null;
		}
		//Debug.Log ("Leaving selecting target");
	}
}
