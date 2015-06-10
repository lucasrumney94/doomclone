using UnityEngine;
using System.Collections;

public class lightLevelDoor : MonoBehaviour {
	
	
	
	private RaycastHit lightCheck;
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Physics.Raycast(transform.position,transform.TransformDirection(Vector3.down),out lightCheck,2))
		{
			this.gameObject.layer = lightCheck.collider.gameObject.layer;
		}
	}
}
