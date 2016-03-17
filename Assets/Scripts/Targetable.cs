using UnityEngine;
using System.Collections;

public class Targetable : MonoBehaviour {
	public virtual bool IsTargetable(GameObject player){
		if (GetComponent<BuildPoint> ()) {
			return GetComponent<BuildPoint> ().owner == null;
		} else if (GetComponent<Base> ()) {
			return GetComponent<Base> ().owner != player;
		}
		return true;
	}
}
