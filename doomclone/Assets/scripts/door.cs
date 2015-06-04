using UnityEngine;
using System.Collections;

public class door : MonoBehaviour {

	public float doorHeight; //How far the door will move
	public float doorSpeed;
	public float doorCloseRange;
	public Transform player;

	private Vector3 originalPosition;
	private bool openMe;
	private bool closeMe;



	void Start()
	{
		originalPosition = transform.position;
		StartCoroutine("closeDetect");
	}

	void Update()
	{
		if (openMe && transform.position.y - originalPosition.y < doorHeight) {
			transform.position += new Vector3 (0, doorSpeed, 0);
		} 
		else 
		{
			openMe = false;
		}
		if (!openMe && closeMe && transform.position.y - originalPosition.y > 0) {
			transform.position -= new Vector3 (0, doorSpeed, 0);
		} 
		else 
		{
			closeMe = false;
		}
		//transform.position = new Vector3 (Mathf.PingPong (Time.time, doorWidth), transform.position.y, transform.position.z);
	}



	void interact()
	{
		openMe = true;
	}

	IEnumerator closeDetect()
	{
		for (;;) //always
		{ 
			if (Vector3.Distance(transform.position,player.position)> doorCloseRange)
			{	
				closeMe = true;
			}
			//Debug.Log(closeMe);
			yield return new WaitForSeconds(.2f);
		}
	}

}
