using UnityEngine;
using System.Collections;

public class BuildFocus : Focusable {

	public GameObject menuPrefab;
	private GameObject menu;

	public override void OnGainFocus (Player gameManager)
	{
		//TODO: show the affinity of the point
		if (menu)
			return;
		if (GetComponent<BuildPoint> ().ownedByLocalPlayer ()) {
			menu = BuildMenu.Instantiate (menuPrefab, gameObject.transform.position, Quaternion.identity, gameObject);
		}
	}


	public override void OnLoseFocus (Player gameManager)
	{
		if (!menu)
			return;
		Destroy (menu);
		menu = null;
	}

}
