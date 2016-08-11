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
			GameObject vrCameraEyeLight = GameObject.Find ("[CameraRig]/Camera (eye)/Spotlight");
			SteamVR_Render steamVR = GameObject.Find ("[SteamVR]").GetComponent<SteamVR_Render>();
			if (!insideEndoscope) {
				vrCameraEye.GetComponent<Camera> ().cullingMask = 1 << LayerMask.NameToLayer ("SeenByEndoOnly");
				vrCameraRig.transform.position = new Vector3(endoCameraObject.transform.position.x, endoCameraObject.transform.position.y - 0.04f, endoCameraObject.transform.position.z);
				vrCameraRig.transform.localScale = new Vector3 (0.1f, 0.1f, 0.1f);

				steamVR.trackingSpace = Valve.VR.ETrackingUniverseOrigin.TrackingUniverseSeated;
				vrCameraEyeLight.GetComponent<Light> ().enabled = true;

				insideEndoscope = true;
			} else {
				vrCameraEye.GetComponent<Camera> ().cullingMask |= -1;
				vrCameraEye.GetComponent<Camera> ().cullingMask &= ~(1 << LayerMask.NameToLayer ("SeenByEndoOnly"));
				vrCameraRig.transform.position = new Vector3(0,0,0);
				vrCameraRig.transform.localScale = new Vector3 (1, 1, 1);

				steamVR.trackingSpace = Valve.VR.ETrackingUniverseOrigin.TrackingUniverseStanding;
				vrCameraEyeLight.GetComponent<Light> ().enabled = false;

				insideEndoscope = false;
			}
		}
		if (insideEndoscope){
			GameObject vrCameraRig = GameObject.Find ("[CameraRig]");
			if (Input.GetKeyDown(KeyCode.DownArrow)) {
				vrCameraRig.transform.position = new Vector3(vrCameraRig.transform.position.x, vrCameraRig.transform.position.y - 0.01f, vrCameraRig.transform.position.z);
			}
			else if (Input.GetKeyDown(KeyCode.UpArrow)) {
				vrCameraRig.transform.position = new Vector3(vrCameraRig.transform.position.x, vrCameraRig.transform.position.y + 0.01f, vrCameraRig.transform.position.z);	
			}
		}
	}
}
