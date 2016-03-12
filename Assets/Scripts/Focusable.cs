using UnityEngine;
using System.Collections;

public abstract class Focusable : MonoBehaviour {
	public abstract void OnGainFocus (Player gameManager);
	public abstract void OnLoseFocus (Player gameManager);
}
