using UnityEngine;
using System.Collections;
/// <summary>
/// Elevator.
/// 
/// This script will tell an elevator to move up or down if the player is on it or looking at it. 
/// </summary>


public class elevator : MonoBehaviour {

	public float activate_distance;
	public float elevator_delay;
	public float elevatorSpeed;
	public float elevatorHeight;
	//public bool activated;
	public bool up = false;
	public Transform player;
 

	private RaycastHit onHit;
	private bool changeState;
	private Vector3 upPosition;
	private Vector3 downPosition;

	// Use this for initialization
	void Start () 
	{
		if (up) 
		{
			upPosition = transform.position;
			downPosition = transform.position - new Vector3 (0, elevatorHeight,0);
		} 
		else 
		{
			upPosition = transform.position + new Vector3 (0, elevatorHeight,0);
			downPosition = transform.position;
		}

		StartCoroutine("playerDetect");
		changeState = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Debug.Log (changeState);
		if (changeState == true) 
		{
			if (up && transform.position.y > downPosition.y) 
			{
				transform.position -= new Vector3 (0, elevatorSpeed, 0);
			}

			if (!up && transform.position.y < upPosition.y) //if down
			{ 
				transform.position += new Vector3 (0, elevatorSpeed, 0);
			}


			
			if (transform.position.y <= downPosition.y)
			{
				changeState = false;
				up = false;
			}
			if (transform.position.y >= upPosition.y)
			{
				changeState = false;
				up = true;
			}




			//else 
			//{
				//up = true;
				//changeState = false;
			//}
		}
	}

	void changeMyState()
	{
		//Debug.Log ("function run");
		changeState = true;
	}

	IEnumerator playerDetect()
	{
		for (;;) //always
		{ 
		if (Physics.Raycast(player.position,transform.TransformDirection(Vector3.down),out onHit,activate_distance))
			{
				//Debug.Log("down hit");
				//changeState = true;
				onHit.collider.SendMessageUpwards("changeMyState",SendMessageOptions.DontRequireReceiver);
				//Debug.DrawLine(transform.position,onHit.point, Color.red);
			}
			if (Physics.Raycast(player.position,transform.TransformDirection(player.forward),out onHit,activate_distance))
			{
				//Debug.Log("front hit");
				//changeState = true;
				onHit.collider.SendMessageUpwards("changeMyState",SendMessageOptions.DontRequireReceiver);
				//Debug.DrawLine(transform.position,onHit.point, Color.red);
			}
			//Debug.Log(closeMe);
			yield return new WaitForSeconds(elevator_delay);
		}
	}
}
