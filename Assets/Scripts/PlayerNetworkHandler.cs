using UnityEngine;
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
	public void CmdSpawnWithAuthority(string prefabName , Vector3 position, Quaternion quaternion){
		Debug.Log ("Server receives RPC from client " + connectionToClient.connectionId.ToString ());
		Debug.Log ("prefab = " + prefabName.ToString ());

		GameObject newObject = Instantiate (spawnPrefabs[prefabName], position, quaternion) as GameObject;
		Debug.Log ("CP1");
		NetworkServer.SpawnWithClientAuthority (newObject, connectionToClient);
	}

	[Command]
	public void CmdSpawnWithAuthorityCallback(string prefabName , Vector3 position, Quaternion quaternion, NetworkInstanceId caller, string methodName){
		Debug.Log ("Server receives RPC from client " + connectionToClient.connectionId.ToString ());
		Debug.Log ("prefab = " + prefabName.ToString ());
		GameObject newObject = Instantiate (spawnPrefabs[prefabName], position, quaternion) as GameObject;
		Debug.Log ("CP1");
		NetworkServer.SpawnWithClientAuthority (newObject, connectionToClient);
		Debug.Log ("Server calling RPC with " + newObject.GetComponent<NetworkIdentity>().netId.ToString());
		NetworkServer.FindLocalObject (caller).SendMessage (methodName, newObject.GetComponent<NetworkIdentity> ().netId);
		//cb (newObject.GetComponent<NetworkIdentity>().netId);
	}

	[Command]
	public void CmdSetMinionDestination(NetworkInstanceId minionId, Vector3 destination){
		GameObject minion = NetworkServer.FindLocalObject (minionId);
		minion.GetComponent<NavMeshAgent> ().SetDestination (destination);
	}

	[Command]
	public void CmdSpawnMinion(NetworkInstanceId spawnPointId, NetworkInstanceId targetId, string minionName){
		GameObject spawnPoint = NetworkServer.FindLocalObject(spawnPointId);
		GameObject newMinion = Instantiate (spawnPrefabs[minionName], spawnPoint.transform.position, Quaternion.identity) as GameObject;
		GameObject target = NetworkServer.FindLocalObject (targetId);
		newMinion.GetComponent<NavMeshAgent> ().SetDestination(target.transform.position);
		spawnPoint.GetComponent<SpawnPoint> ().RegisterMinion (newMinion);
		newMinion.GetComponent<Minion> ().owner = spawnPoint.GetComponent<SpawnPoint> ().owner;
		//TODO set owner

		NetworkServer.SpawnWithClientAuthority (newMinion, connectionToClient);

		//spawnPoint.RegisterMinion (newMinion);
	}

		
}
