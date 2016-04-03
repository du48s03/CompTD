using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Tower : NetworkBehaviour {

	[SyncVar]
	private uint ownerId;

	public static GameObject Instantiate(Object original, Vector3 position, Quaternion quaternion, NetworkInstanceId ownerId){
		GameObject newTower = Instantiate (original, position, quaternion) as GameObject;
		newTower.GetComponent<Tower> ().ownerId = ownerId.Value;
		return newTower;
	}


	public override void OnStartClient ()
	{
		base.OnStartClient ();
		Debug.Log ("Tower owner ID = " + ownerId.ToString ());
		if (Player.GetPlayerWithNetID (ownerId) == null)
			Debug.Log ("Can't find the plaer with the ID");
		else
			Debug.Log ("TowerOwner = " + Player.GetPlayerWithNetID (ownerId).ToString ());
		Color towerColor = Player.GetLocalPlayer() == Player.GetPlayerWithNetID(ownerId) ? Color.blue : Color.red;
		transform.Find ("TowerBase").GetComponent<MeshRenderer> ().material.color = towerColor;
		transform.Find ("TowerBase/TowerBaseSecond/TowerBody/TowerTop").GetComponent<MeshRenderer> ().material.color = towerColor;
	}

}
