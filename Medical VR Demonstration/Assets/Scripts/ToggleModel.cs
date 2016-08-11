using UnityEngine;
using System.Collections;

public class ToggleModel : VRTK.VRTK_InteractableObject {

	public GameObject[] thingsToHide;
	public GameObject hideLabel;
	public GameObject showLabel;

	private bool hiddenStatus = false;

	// Use this for initialization
	void Start () {
		base.Start ();
	}

	public override void StartUsing(GameObject usingObject) {
		base.StartUsing(usingObject);
		hiddenStatus = !hiddenStatus;

		ToggleHide ();
	}

	public void ToggleHide() {
		if (hiddenStatus) {
			foreach (GameObject model in thingsToHide) {
				model.GetComponent<MeshRenderer> ().enabled = false;
			}
			hideLabel.GetComponent<MeshRenderer> ().enabled = false;
			showLabel.GetComponent<MeshRenderer> ().enabled = true;

			hiddenStatus = true;
		} else {
			foreach (GameObject model in thingsToHide) {
				model.GetComponent<MeshRenderer> ().enabled = true;
			}
			hideLabel.GetComponent<MeshRenderer> ().enabled = true;
			showLabel.GetComponent<MeshRenderer> ().enabled = false;

			hiddenStatus = false;
		}
	}
}
