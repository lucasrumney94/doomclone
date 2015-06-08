using UnityEngine;
using System.Collections;

	// Use this for initialization
public class SkyboxCamera : MonoBehaviour {
		
		
		// set the main camera in the inspector
		public Camera MainCamera;
		
		// set the sky box camera in the inspector
		public Camera SkyCamera;
		
		// the additional rotation to add to the skybox
		// can be set during game play or in the inspector
		public Vector3 SkyBoxRotation;
		
		// Use this for initialization
		void Start()
		{
			if (SkyCamera.depth >= MainCamera.depth)
			{
				Debug.Log("Set skybox camera depth lower "+
				          " than main camera depth in inspector");
			}
			if (MainCamera.clearFlags != CameraClearFlags.Nothing)
			{
				Debug.Log("Main camera needs to be set to dont clear" +
				          "in the inspector");
			}
		}
		
		// if you need to rotate the skybox during gameplay
		// rotate the skybox independently of the main camera
		//public void SetSkyBoxRotation(Vector3 rotation)
		//    {
		//    this.SkyBoxRotation = rotation;
		//}
		
		// Update is called once per frame
		void Update()
		{
			SkyCamera.transform.position = MainCamera.transform.position;
			SkyCamera.transform.rotation = MainCamera.transform.rotation;
			//SkyCamera.transform.rotation *= SkyBoxRotation;//MainCamera.transform.rotation
			SkyCamera.transform.Rotate(SkyBoxRotation);
		}
	}

