using UnityEngine;
using System.Collections;

public class billboard : MonoBehaviour
{
	private Camera m_Camera;

	private GameObject target;
	private Vector3 targetPoint;
	private Vector3 myPos; 


	void Start()
	{
		//m_Camera = Camera.main;
		target = GameObject.FindWithTag("Player");
		targetPoint = target.transform.position;
		targetPoint.y = this.gameObject.transform.position.y;

		myPos = Camera.main.transform.position;
	}
	void Update()
	{
		if (this.gameObject != null)
		{
			targetPoint = target.transform.position;
			targetPoint.y = this.gameObject.transform.position.y;
			////transform.LookAt(transform.position + m_Camera.transform.rotation * Vector3.back, m_Camera.transform.rotation * Vector3.up); //This Rotates them with the camera
			//transform.LookAt(targetPoint, Vector3.up);




			transform.rotation = Quaternion.LookRotation(this.gameObject.transform.position-targetPoint);
		}
		else
		{
			Destroy(this);
		}
	}
}