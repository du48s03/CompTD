using UnityEngine;
using System.Collections;

public class SpawnMenu : MonoBehaviour {
	public GameObject spawnPoint;

	/*
	 * 
	 * 
	 * 
	 */
	public static GameObject Instantiate(Object original, Vector3 position, Quaternion quaternion, GameObject spawnPoint){
		GameObject newSpawnMenu = Instantiate (original, position, quaternion) as GameObject;
		newSpawnMenu.GetComponent<SpawnMenu> ().spawnPoint = spawnPoint;
		newSpawnMenu.GetComponent<HintBubble> ().owner = spawnPoint;
		newSpawnMenu.GetComponent<HintBubble> ().camera = Camera.main;
		foreach (SpawnButton child in newSpawnMenu.transform.GetComponentsInChildren<SpawnButton>()) {
			child.spawnPoint = spawnPoint;
		}
		return newSpawnMenu;
	}

	public void setSpawnPoint(GameObject sp){
		spawnPoint = sp;
		foreach (SpawnButton child in transform.GetComponentsInChildren<SpawnButton>()) {
			child.spawnPoint = sp;
		}

	}
}
