using UnityEngine;
using System.Collections;

public class button : MonoBehaviour {

	public buttonDoor Door;
	

	// Update is called once per frame
	void interact() 
	{
//		Debug.Log ("button was pressed!!");
		Door.openMe = true;
	}
}
