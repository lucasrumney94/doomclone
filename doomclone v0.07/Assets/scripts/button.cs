using UnityEngine;
using System.Collections;

public class button : MonoBehaviour {

	public secretDoor Door;
	

	// Update is called once per frame
	void interact() 
	{
		Door.openMe = true;
	}
}
