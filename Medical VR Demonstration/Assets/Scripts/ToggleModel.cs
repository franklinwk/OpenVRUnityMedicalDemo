using UnityEngine;
using System.Collections;

public class ToggleModel : VRTK.VRTK_InteractableObject {

	public GameObject[] thingsToHide;
	public GameObject hideLabel;
	public GameObject showLabel;

	// Use this for initialization
	void Start () {
		base.Start ();
	}

	public override void StartUsing(GameObject usingObject) {
		base.StartUsing(usingObject);
		foreach (GameObject model in thingsToHide) {
			model.GetComponent<MeshRenderer> ().enabled = false;
		}
		hideLabel.GetComponent<MeshRenderer> ().enabled = false;
		showLabel.GetComponent<MeshRenderer> ().enabled = true;
	}

	public override void StopUsing(GameObject usingObject) {
		base.StopUsing(usingObject);
		foreach (GameObject model in thingsToHide) {
			model.GetComponent<MeshRenderer> ().enabled = true;
		}
		hideLabel.GetComponent<MeshRenderer> ().enabled = true;
		showLabel.GetComponent<MeshRenderer> ().enabled = false;
	}
}
