using UnityEngine;
using UnityEngine.Networking;
using System.Collections;


public class Minion : NetworkBehaviour {

	public Player owner;



	//TODO: the ownership is not set on the server side. 
	public override void OnStartAuthority ()
	{
		base.OnStartAuthority ();
		GetComponent<MeshRenderer> ().material.color = Color.red;
		//System.Collections.ArrayList players = ClientScene.localPlayers;
		/*
		PlayerNetworkHandler[] players = FindObjectsOfType<PlayerNetworkHandler> ();
		for (int i = 0; i < players.Length; i++) {
			//PlayerNetworkHandler player = (GameObject)players[i]
			//if (players[i].gameObject.GetComponent<PlayerNetworkHandler>().isLocalPlayer) {
			if(players[i].isLocalPlayer){
				owner = players [i].gameObject.GetComponent<Player>();
				break;
			}
		}
		*/
		//Debug.Log (" Minion ownership set to " + owner.GetComponent<NetworkIdentity> ().netId.ToString ());
	}
}
