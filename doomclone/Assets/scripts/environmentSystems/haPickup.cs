using UnityEngine;
using System.Collections;

public class haPickup : MonoBehaviour {	
	
	public bool health;
	public bool armor;
	public int amount;

	
	void Start()
	{
	}
	
	
	void OnTriggerEnter(Collider c)
	{
		if (c.tag == "Player")
		{
			if (health)
			{
				c.SendMessageUpwards ("changeHealth", amount, SendMessageOptions.DontRequireReceiver);
			}
			if (armor)
			{
				c.SendMessageUpwards ("changeArmor", amount, SendMessageOptions.DontRequireReceiver);
			}
			Destroy(this.gameObject);
		}
	}
}