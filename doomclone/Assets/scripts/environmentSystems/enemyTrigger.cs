using UnityEngine;
using System.Collections;

public class enemyTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void OnTriggerEnter(Collider c)
	{
		if (c.tag == "Player")
		{
//			Debug.Log ("in collider!");
			//Physics.IgnoreCollision(c.collider, GetComponent<Collider>());
			foreach (Transform child in transform)
			{
				child.GetComponent<Collider>().SendMessageUpwards("setAwake",true,SendMessageOptions.DontRequireReceiver);
			}
			//this.gameObject.SetActive(false);
		}
	}
}
