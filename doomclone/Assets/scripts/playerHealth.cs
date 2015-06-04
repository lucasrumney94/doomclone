using UnityEngine;
using System.Collections;

public class playerHealth : MonoBehaviour {

	public float health=100f;
	public float armor=100f;
	public float armorEfficiency=80;//from 0 to 100, percent of the damage that it takes 

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown("space"))
		{
			applyDamage(10f);
		}
	}

	void applyDamage(float damage)
	{
		if (armor > 0) 
		{
			armor -= damage * (armorEfficiency/100);
			health -= damage * ((100 - armorEfficiency)/100);
		}
		if (armor <= 0) 
		{
			health -= damage;
		}


		Debug.Log (health);
		Debug.Log (armor);


			//Debug.Log (health);
		if (health <= 0.0f) 
		{
			//you died, reload from last checkpoint menu
		}
	}
}
