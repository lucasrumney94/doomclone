using UnityEngine;
using System.Collections;

public class rocket : MonoBehaviour {
	// The fly speed (used by the weapon later)
	public float speed = 2000.0f;
	
	// explosion prefab (particles)
	public GameObject explosionPrefab;
	
	// find out when it hit something
	void OnCollisionEnter(Collision c) {
		// show an explosion
		// - transform.position because it should be
		//   where the rocket is
		// - Quaternion.identity because it should
		//   have the default rotation
		Instantiate(explosionPrefab,transform.position,Quaternion.identity);
		
		// destroy the rocket
		// note:
		//  Destroy(this) would just destroy the rocket
		//                script attached to it
		//  Destroy(gameObject) destroys the whole thing
		Destroy(gameObject);
	}
}