using UnityEngine;
using System.Collections;

public class ScrollSlices : VRTK.VRTK_InteractableObject {
	private SteamVR_Controller.Device controller;
	private SteamVR_TrackedObject trackedObj;

	public Renderer rend;
	public Renderer childRend;
	public int XDim = 585;
	public int YDim = 585;
	public int columns = 14;
	public int rows = 13;
	public int frames = 182;
	public float sliceOffsetRange = 9.78f;

	private int offset = 0;

	private float textureOffsetX;
	private float textureOffsetY;

	private bool scrollActive = false;

	public GameObject childSlice;

	private float pastzPosition = 0.0f;

	// Use this for initialization
	void Start () {

		rend = GetComponent<Renderer>();
		textureOffsetX = 1.0f / columns;
		textureOffsetY = 1.0f / rows;

		if (childSlice != null) {
			childRend = childSlice.GetComponent<Renderer> ();
		}
	}

	public override void Grabbed(GameObject usingObject)
	{
		base.Grabbed(usingObject);
		scrollActive = true;

		trackedObj = usingObject.GetComponent<SteamVR_TrackedObject> ();
		controller = SteamVR_Controller.Input((int)trackedObj.index);
	}

	public override void Ungrabbed(GameObject usingObject)
	{
		base.Ungrabbed(usingObject);
		scrollActive = false;
	}


	// Update is called once per frame
	void Update () {
		if (scrollActive) {
			float zPosition = transform.localPosition.z;
			offset = (int)Mathf.Round ((zPosition / sliceOffsetRange) * frames);
			int yCount = (int)Mathf.Floor (offset / columns);
			int xCount = offset % columns;

			rend.material.SetTextureOffset ("_MainTex", new Vector2 (xCount * textureOffsetX, -yCount * textureOffsetY));

			if (childSlice != null) {
				childRend.material.SetTextureOffset ("_MainTex", new Vector2 (xCount * textureOffsetX, -yCount * textureOffsetY));
			}

			if (controller != null) {
				controller.TriggerHapticPulse ((ushort)Mathf.FloorToInt(Mathf.Abs (zPosition - pastzPosition) * 1000));
			}

			pastzPosition = zPosition;
		} 
	}
}
