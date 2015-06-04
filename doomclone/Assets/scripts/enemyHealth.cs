using UnityEngine;
using System.Collections;

public class enemyHealth : MonoBehaviour {

	public float health; 
	

	void applyDamage(float damage)
	{
		health -= damage;
		//Debug.Log (health);
		if (health <= 0.0f) 
		{
			destroyMe();
		}
	}
	void destroyMe()
	{
		//Destroy the Movement Script
		Destroy (GetComponent<enemyMovement>());
		Destroy (GetComponent<BoxCollider> ());
		Destroy (GetComponent<CharacterController> ());
		//Destroy the Shooting Script

		//Destroy(gameObject);
	}

}
