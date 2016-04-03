﻿using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerNetworkHandler : NetworkBehaviour {

	public delegate void spawnCallback(NetworkInstanceId newObj);
	private System.Collections.Generic.Dictionary<string, GameObject> spawnPrefabs = new System.Collections.Generic.Dictionary<string, GameObject>();

	public override void OnStartLocalPlayer(){
		Debug.Log ("On start local player");
		System.Collections.Generic.List<GameObject> spawnables = FindObjectOfType<NetworkManager> ().spawnPrefabs;
		foreach (GameObject obj in spawnables) {
			spawnPrefabs [obj.name] = obj;
			Debug.Log (obj.name);
		}
		CmdInitServerPlayer ();
	}
		
	[Command]
	private void CmdInitServerPlayer(){
		Debug.Log ("Initializing player on the server side");
		System.Collections.Generic.List<GameObject> spawnables = FindObjectOfType<NetworkManager> ().spawnPrefabs;
		foreach (GameObject obj in spawnables) {
			spawnPrefabs [obj.name] = obj;
			Debug.Log (obj.name);
		}
	}

	[Command]
	public void CmdSpawnSpawnPoint(Vector3 position, Quaternion quaternion, NetworkInstanceId netId){
		//GameObject newPoint = Instantiate(spawnPrefabs["Spawn Point"], position, quaternion) as GameObject;
		Player owner = Player.GetPlayerWithNetID (netId.Value);
		GameObject newSpawnPoint = SpawnPoint.Instantiate(spawnPrefabs["Spawn Point"], position, quaternion, owner);
		//newPoint.GetComponent<SpawnPoint> ().owner = owner;

		NetworkServer.SpawnWithClientAuthority (newSpawnPoint, connectionToClient);

	}




	[Command]
	public void CmdSpawnWithAuthority(string prefabName , Vector3 position, Quaternion quaternion){
		//Debug.Log ("Server receives command from client " + connectionToClient.connectionId.ToString ());
		//Debug.Log ("prefab = " + prefabName.ToString ());

		GameObject newObject = Instantiate (spawnPrefabs[prefabName], position, quaternion) as GameObject;
		//Debug.Log ("CP1");
		NetworkServer.SpawnWithClientAuthority (newObject, connectionToClient);
	}

	[Command]
	public void CmdSpawnWithAuthorityCallback(string prefabName , Vector3 position, Quaternion quaternion, NetworkInstanceId caller, string methodName){
		//Debug.Log ("Server receives RPC from client " + connectionToClient.connectionId.ToString ());
		//Debug.Log ("prefab = " + prefabName.ToString ());
		GameObject newObject = Instantiate (spawnPrefabs[prefabName], position, quaternion) as GameObject;
		//Debug.Log ("CP1");
		NetworkServer.SpawnWithClientAuthority (newObject, connectionToClient);
		//Debug.Log ("Server calling RPC with " + newObject.GetComponent<NetworkIdentity>().netId.ToString());
		NetworkServer.FindLocalObject (caller).SendMessage (methodName, newObject.GetComponent<NetworkIdentity> ().netId);
		//cb (newObject.GetComponent<NetworkIdentity>().netId);
	}

	[Command]
	public void CmdSetMinionDestination(NetworkInstanceId minionId, Vector3 destination){
		GameObject minion = NetworkServer.FindLocalObject (minionId);
		minion.GetComponent<NavMeshAgent> ().SetDestination (destination);
	}


	//called from the client side to spawn a minion. 
	[Command]
	public void CmdSpawnMinion(NetworkInstanceId spawnPointId, NetworkInstanceId targetId, string minionName){
		GameObject spawnPoint = NetworkServer.FindLocalObject(spawnPointId);
		Debug.Log ("targetId = " + targetId.ToString ());
		GameObject newMinion = Minion.Instantiate(spawnPrefabs[minionName], spawnPoint.transform.position, Quaternion.identity, spawnPoint.GetComponent<SpawnPoint>().owner, targetId.Value);

		GameObject target = NetworkServer.FindLocalObject (targetId);
		newMinion.GetComponent<NavMeshAgent> ().SetDestination(target.transform.position);
		spawnPoint.GetComponent<SpawnPoint> ().RegisterMinion (newMinion);


		NetworkServer.SpawnWithClientAuthority (newMinion, connectionToClient);

		//spawnPoint.RegisterMinion (newMinion);
	}


	[Command]
	public void CmdSpawnTower(NetworkInstanceId buildPointId, string towerName){
		GameObject buildPoint = NetworkServer.FindLocalObject (buildPointId);
		Player owner = buildPoint.GetComponent<BuildPoint> ().owner;
		Debug.Log ("server: owner id = " + owner.netId);
		GameObject newTower = Tower.Instantiate (spawnPrefabs [towerName], buildPoint.transform.position, Quaternion.identity, owner.netId);
		buildPoint.GetComponent<BuildPoint> ().building = newTower.GetComponent<Tower> ().netId.Value;
		buildPoint.GetComponent<BuildPoint> ().hasBuilding = true;

		NetworkServer.SpawnWithClientAuthority (newTower, owner.gameObject);
	}

		
}
