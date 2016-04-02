using UnityEngine;
using UnityEngine.Networking;
using System.Collections;


public class Minion : NetworkBehaviour {

	public Player owner; 

	//this should only be called on the server side, with the spawnable prefab object ;
	public virtual GameObject Instantiate(Vector3 pos, Quaternion rot, Player owner){
		GameObject newobj = Instantiate (gameObject, pos, rot) as GameObject;
		newobj.GetComponent<Minion> ().owner = owner;
		if(isServer)
			Debug.Log ("owner = "+this.owner.ToString ());
		return newobj;
	}
		

	public override void OnStartAuthority ()
	{
		base.OnStartAuthority ();
		GetComponent<MeshRenderer> ().material.color = Color.red;
	}
		
	void OnTriggerEnter(Collider col){
		if (!isServer || !col)
			return;
		if (col.gameObject.GetComponent<BuildPoint> () && !col.gameObject.GetComponent<BuildPoint>().owner) {
			Debug.Log ("Detect build point");
			col.gameObject.GetComponent<BuildPoint> ().Score (owner);
			Destroy (gameObject);
		}
	}
}
