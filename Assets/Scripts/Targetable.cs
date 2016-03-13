using UnityEngine;
using System.Collections;

public class Targetable : MonoBehaviour {
	public virtual bool IsTargetable(){
		if (GetComponent<BuildPoint> ()) {
			return GetComponent<BuildPoint> ().owner == null;
		}
		return true;
	}
}
