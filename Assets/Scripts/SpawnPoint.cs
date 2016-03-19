﻿using UnityEngine;
using UnityEngine.Networking;
using System.Collections;


public class SpawnPoint : NetworkBehaviour {

	public Player owner;

	private GameObject prefab;
	private GameObject target;

	[SyncVar]
	private ArrayList minions = new ArrayList();

	//Will be called for all the clients
	//hasAuthority is false for all the clients at this point
	public override void OnStartClient ()
	{
		base.OnStartClient ();
		GetComponent<CapsuleCollider> ().enabled = false;
		GetComponent<MeshRenderer> ().enabled = false;
	}
	//Will be called only for the clients with the authority, after OnStartClient is called. 
	//hasAuthority is set to true for the authoritative client at this point
	public override void OnStartAuthority ()
	{
		base.OnStartAuthority ();
		GetComponent<CapsuleCollider> ().enabled = true;
		GetComponent<MeshRenderer> ().enabled = true;
		//Player[] players = ClientScene.localPlayers;
		Player[] players = FindObjectsOfType<Player> ();
		for (int i = 0; i < players.Length; i++) {
			if (players [i].isLocalPlayer) {
				//Debug.Log ("Spawn Point found owner");
				owner = players [i];
				break;
			}
		}
	}

	public IEnumerator NewMinion(GameObject prefab){
		this.prefab = prefab;
		yield return StartCoroutine (owner.GetComponent<Player>().SelectTarget (receiveTarget));
	}

	public void receiveTarget(GameObject target){
		Debug.Log ("Client calling CmdSpawnWithAuthority");
		/*
		this.target = target;
		owner.GetComponent<PlayerNetworkHandler> ().CmdSpawnWithAuthorityCallback (
			prefab.name,
			transform.position,
			Quaternion.identity,
			netId,
			"RpcSpawnCallback"
		);
		*/

		owner.GetComponent<PlayerNetworkHandler> ().CmdSpawnMinion (
			GetComponent<NetworkIdentity> ().netId, 
			target.GetComponent<NetworkIdentity> ().netId, 
			prefab.name);
		
	}

	//Obsolete
	/*
	[ClientCallback]
	public void RpcSpawnCallback(NetworkInstanceId newMinion){
		owner.GetComponent<PlayerNetworkHandler> ().CmdSetMinionDestination (newMinion, target.transform.position);
		Debug.Log ("new minion spawned: " + newMinion.ToString ());
	}
	*/	
	public void RegisterMinion(GameObject newMinion){
		minions.Add (newMinion);
		Debug.Log ("Server: A minion is registered");
	}
}
