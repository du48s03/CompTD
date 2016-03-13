using UnityEngine;
using System.Collections;

public class SpawnMenu : MonoBehaviour {
	public GameObject spawnPoint;

	public void setSpawnPoint(GameObject sp){
		spawnPoint = sp;
		foreach (SpawnButton child in transform.GetComponentsInChildren<SpawnButton>()) {
			child.spawnPoint = sp;
		}
	}
}
