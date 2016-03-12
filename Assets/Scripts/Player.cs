using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	private Focusable focus;
	private Vector3 startPosition;

	void OnStart(){
		Debug.Log ("Start");
	}

	void Update(){
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
					setFocus (hit.transform.GetComponent<Focusable>());
				}
			}
			//startPosition = null;
		}
	}

	private void setFocus(Focusable target){
		if(focus != null)
			focus.OnLoseFocus(this);
		focus = target;
		if(focus != null)
			target.OnGainFocus(this);
	}
}
