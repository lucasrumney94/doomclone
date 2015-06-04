using UnityEngine;
using System.Collections;

public class ammoInfo
{
	public string weapon;
	public int bullets;
	public bool isGun;
	
	public ammoInfo(string weapon, int bullets, bool isGun)
	{
		this.weapon = weapon;
		this.bullets = bullets;
		this.isGun = isGun;
	}


}
