using UnityEngine;
using System.Collections;

public class JumpToEndoscope : MonoBehaviour {

	private bool insideEndoscope = false;
	private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
	private SteamVR_TrackedObject trackedObj;

	// Use this for initialization
	void Start () {
		trackedObj = GetComponent<SteamVR_TrackedObject> ();
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("c") || controller.GetPressDown(Valve.VR.EVRButtonId.k_EButton_ApplicationMenu)) {
			GameObject endoCameraObject = GameObject.Find ("Endoscope/Endoscope/EndoscopeCamera");
			GameObject vrCameraRig = GameObject.Find ("[CameraRig]");
			GameObject vrCameraEye = GameObject.Find ("[CameraRig]/Camera (eye)");
			SteamVR_Render steamVR = GameObject.Find ("[SteamVR]").GetComponent<SteamVR_Render>();
			if (!insideEndoscope) {
				vrCameraEye.GetComponent<Camera> ().cullingMask = 1 << LayerMask.NameToLayer ("SeenByEndoOnly");
				vrCameraRig.transform.position = endoCameraObject.transform.position;
				vrCameraRig.transform.localScale = new Vector3 (0.1f, 0.1f, 0.1f);

				steamVR.trackingSpace = Valve.VR.ETrackingUniverseOrigin.TrackingUniverseSeated;

				insideEndoscope = true;
			} else {
				vrCameraEye.GetComponent<Camera> ().cullingMask |= -1;
				vrCameraEye.GetComponent<Camera> ().cullingMask &= ~(1 << LayerMask.NameToLayer ("SeenByEndoOnly"));
				vrCameraRig.transform.position = new Vector3(0,0,0);
				vrCameraRig.transform.localScale = new Vector3 (1, 1, 1);

				steamVR.trackingSpace = Valve.VR.ETrackingUniverseOrigin.TrackingUniverseStanding;

				insideEndoscope = false;
			}

		}
	}
}
