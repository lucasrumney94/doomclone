using UnityEngine;
using System.Collections;

public class dontCollidewithPlayer : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		Physics.IgnoreCollision(this.gameObject.GetComponent<Collider>(),GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>());
	}

}
