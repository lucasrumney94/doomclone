using UnityEngine;
using System.Collections;

public class ammoPickup : MonoBehaviour {	

	public string weapon = "pistol";
	public int amount;
	public bool isGun;
	public ammoInfo loot;

	void Start()
	{
		loot = new ammoInfo (weapon, amount, isGun);
	}


	void OnTriggerEnter(Collider c)
	{
		if (c.tag == "Player")
		{
			Debug.Log ("entered!");
			c.SendMessageUpwards ("changeAmmo", loot, SendMessageOptions.DontRequireReceiver);
			Destroy(this.gameObject);
		}
	}
}
