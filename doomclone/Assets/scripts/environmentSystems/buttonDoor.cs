using UnityEngine;
using System.Collections;

public class buttonDoor : MonoBehaviour {
	
	public float doorHeight; //How far the door will move
	public float doorSpeed;
	
	private Vector3 originalPosition;
	public bool openMe;


	
	void Start()
	{
		originalPosition = transform.position;
	}
	
	void Update()
	{
		if (openMe && transform.position.y - originalPosition.y < doorHeight) {
			//Debug.Log ("buttonDoor was activated!!");
			transform.position += new Vector3 (0, doorSpeed, 0);
		} 
		else 
		{
			openMe = false;
		}

	}

	
}
