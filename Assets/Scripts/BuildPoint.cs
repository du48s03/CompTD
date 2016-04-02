using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class BuildPoint : NetworkBehaviour {

	public GameObject owner = null;
	public int GoalAffinity = 3;
	[SyncVar]
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
		//Debug.Log ("Player Score!: affinity = " + affinity.ToString ());
	}

	public void SetOwner(Player player){
		owner = player.gameObject;
		RpcSetOwner (player.GetComponent<NetworkIdentity> ().netId);
		//Debug.Log ("Player has controlled the build point!");
	}

	[ClientRpc]
	void RpcSetAffinity(int aff){
		affinity = aff;
		Material ringMaterial = transform.FindChild ("Outer Ring").GetComponent<Material> ();
	}

	[ClientRpc]
	void RpcSetOwner(NetworkInstanceId playerID){
		foreach (Player player in FindObjectsOfType<Player>()) {
			if (player.isLocalPlayer) {
				if (playerID == player.GetComponent<NetworkIdentity> ().netId) {
					//Debug.Log ("You have controlled the build point!");
				} else {
					//Debug.Log ("The enemy has controlled the build point!");
				}
			}
		}
	}
}
