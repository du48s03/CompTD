using UnityEngine;
using System.Collections;

public class BuildButton : MonoBehaviour {

	public GameObject buildPoint;
	public GameObject buildingPrefab;

	public void OnClick(){
		BuildPoint buildPointController = buildPoint.GetComponent<BuildPoint> ();
		buildPointController.owner.GetComponent<PlayerNetworkHandler> ().CmdSpawnTower (buildPointController.netId, buildingPrefab.name);
	}

}
