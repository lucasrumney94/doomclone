using UnityEngine;
using System.Collections;

public class playerHealth : MonoBehaviour {

	public float health=100f;
	public float armor=100f;
	public float armorEfficiency=80;//from 0 to 100, percent of the damage that it takes 
	public bool isAlive = true;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Debug.Log (health);
		//Debug.Log (armor);
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
			armor = 0.0f;
			health -= damage;
		}


		


			//Debug.Log (health);
		if (health <= 0.0f) 
		{
			Debug.Log ("You died!");
			isAlive = false;
		}
	}

	void changeHealth(float amount)
	{
		health += amount;
		if (health > 100)
			health = 100;
	}

	void changeArmor(float amount)
	{
		armor += amount;
		if (armor > 100)
			armor = 100;
	}

}
