using UnityEngine;
using System.Collections;

public class SpawnPoint : MonoBehaviour {

	public Player owner;

	private GameObject prefab;
	private ArrayList minions = new ArrayList();

	public IEnumerator NewMinion(GameObject prefab){
		this.prefab = prefab;
		yield return StartCoroutine (owner.GetComponent<Player>().SelectTarget (receiveTarget));
	}

	public void receiveTarget(GameObject target){
		GameObject newMinion = Instantiate (prefab, transform.position, Quaternion.identity) as GameObject;
		Debug.Log ("received target" + target.transform.position.ToString ());
		newMinion.GetComponent<NavMeshAgent> ().SetDestination (target.transform.position);
		RegisterMinion (newMinion);

	}

	public void RegisterMinion(GameObject newMinion){
		minions.Add (newMinion);
		Debug.Log ("A minion is registered");
	}
}
