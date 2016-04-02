using UnityEngine;
using UnityEngine.Networking;
using System.Collections;


public class Minion : NetworkBehaviour {

	public Player owner; 

	public virtual GameObject Instantiate(Vector3 pos, Quaternion rot, Player owner){
		GameObject newobj = Instantiate (gameObject, pos, rot) as GameObject;
		newobj.GetComponent<Minion> ().owner = owner;
		return newobj;
	}




	public override void OnStartAuthority ()
	{
		base.OnStartAuthority ();
		GetComponent<MeshRenderer> ().material.color = Color.red;
	}
		
}
