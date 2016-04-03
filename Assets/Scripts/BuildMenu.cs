using UnityEngine;
using System.Collections;

public class BuildMenu : MonoBehaviour {

	private GameObject buildPoint;

	public static GameObject Instantiate(Object original, Vector3 position, Quaternion quaternion, GameObject buildPoint){
		GameObject menu = Instantiate (original, position, quaternion) as GameObject;
		menu.GetComponent<HintBubble> ().owner = buildPoint;
		menu.GetComponent<HintBubble> ().camera = Camera.main;
		menu.GetComponent<BuildMenu> ().buildPoint = buildPoint;
		foreach (BuildButton button in menu.GetComponentsInChildren<BuildButton>()) {
			button.buildPoint = buildPoint;
		}
		return menu;
	}
		
}
