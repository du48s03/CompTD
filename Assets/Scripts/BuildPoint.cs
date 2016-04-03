using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityTest;

public class BuildPoint : NetworkBehaviour {

	public Player owner = null;
	public int GoalAffinity = 3;
	private int affinity = 0;
	//The affinity is positive if the host player has more controll over the point. 


	public void Score(Player player){
		if (!isServer)
			return;
		if (player.isLocalPlayer)//if the player is the host player
			affinity++;
		else
			affinity--;
		if (affinity == GoalAffinity || affinity == -GoalAffinity) {
			SetOwner (player);
		}
		RpcSetAffinity (affinity);
		//Debug.Log ("Player Score!: affinity = " + affinity.ToString ());
	}

	public void SetOwner(Player player){
		owner = player;
		RpcSetOwner (player.GetComponent<NetworkIdentity> ().netId);
		//Debug.Log ("Player has controlled the build point!");
	}

	public bool ownedByLocalPlayer(){
		Player localPlayer = Player.GetLocalPlayer();
		return (owner && owner == localPlayer);
	}

	[ClientRpc]
	void RpcSetAffinity(int aff){
		affinity = aff;
		int isHost = isServer ? 1 : -1;
		Color color = new Color(1f, 1f, 1f, 1f);
		if (affinity * isHost <= 0) {
			color.r = 1f;
			color.b = 1f + (float)isHost * (float)affinity / (float)GoalAffinity;
			color.g = 1f + (float)isHost * (float)affinity / (float)GoalAffinity;
		}
		if (affinity * isHost >= 0) {
			color.b = 1f;
			color.r = 1f - (float)isHost * (float)affinity / (float)GoalAffinity;
			color.g = 1f - (float)isHost * (float)affinity / (float)GoalAffinity;
		}
		Debug.Log ("affinity on the client side set to " + affinity.ToString ());
		Debug.Log (color.ToString ());
		transform.FindChild ("OuterRing").GetComponent<MeshRenderer> ().material.color = color;
	}

	[ClientRpc]
	void RpcSetOwner(NetworkInstanceId playerID){
		foreach (Player player in FindObjectsOfType<Player>()) {
			if (player.GetComponent<NetworkIdentity> ().netId == playerID) {
				owner = player;
				if (player.isLocalPlayer) {
					//TODO: display a sound effect maybe?
				} else {
					//TODO: display a sound effect maybe?
				}
			}
		}
	}


}
