using UnityEngine;
using System.Collections;

public class dontCollideWithEnemyLedgeColliders : MonoBehaviour {

	private GameObject[] projectiles;

	// Use this for initialization
	void Update () 
	{
		projectiles = GameObject.FindGameObjectsWithTag("EnemyLedgeCollider");
		//Debug.Log(projectiles);
		foreach (GameObject i in projectiles)
		{
			if (this.gameObject.GetComponent<Collider>() != null)
				Physics.IgnoreCollision(this.gameObject.GetComponent<Collider>(),i.GetComponent<Collider>());
		}
	}
	
}
