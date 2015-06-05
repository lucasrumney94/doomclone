﻿using UnityEngine;
using System.Collections;

public class lockCursor : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		Cursor.lockState = CursorLockMode.Confined;
		Cursor.lockState = CursorLockMode.Locked;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKey(KeyCode.Escape))
		{
		    Cursor.lockState= CursorLockMode.None;
		}
	}
}
