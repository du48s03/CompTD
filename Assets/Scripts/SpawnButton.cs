using UnityEngine;
using System.Collections;

public class SpawnButton : MonoBehaviour {

	public GameObject spawnPoint;
	public GameObject minionPrefab;

	public void OnClick(){
		StartCoroutine(spawnPoint.GetComponent<SpawnPoint>().NewMinion (minionPrefab));
	}

}
