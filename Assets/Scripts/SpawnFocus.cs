using UnityEngine;
using System.Collections;


public class SpawnFocus : Focusable {
	public GameObject menuPrefab;

	private GameObject menu;

	public override void OnGainFocus(Player gameManager){
		if (menu != null)
			return;
		menu = Instantiate (menuPrefab);
		HintBubble billBoard = menu.GetComponent<HintBubble> ();
		billBoard.owner = this.gameObject;
		billBoard.camera = Camera.main;
		Debug.Log ("Show menu");
	}

	public override void OnLoseFocus(Player gameManager){
		if (menu == null)
			return;
		Destroy (menu);
		menu = null;
		Debug.Log ("Hide menu");
	}
}
