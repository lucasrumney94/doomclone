﻿using UnityEngine;
using System.Collections;

/// <summary>
/// Player interact.
/// This script is to be attached to the player to handle all raycasted interactions like door or elevators
/// </summary>

public class playerInteract : MonoBehaviour {

	public float interact_distance = 200.0f;
	public float forward_offset = 1.5f; 
	public float interact_height = 3.0f;



	private RaycastHit interHit; // the players raycast for interaction

	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetButton("Interact")) 
		{
			interactPressed();
		}
		//Debug.DrawRay(transform.position+transform.TransformVector(Vector3.forward*forward_offset)+(Vector3.up*interact_height),transform.TransformDirection(Vector3.forward), Color.blue);
	}
	void interactPressed()
	{
		if (Physics.Raycast(transform.position+transform.TransformVector(Vector3.forward*forward_offset)+(Vector3.up*interact_height),transform.TransformDirection(Vector3.forward),out interHit,interact_distance))
		{
			interHit.collider.SendMessageUpwards("interact",SendMessageOptions.DontRequireReceiver);

		}
	}

}
