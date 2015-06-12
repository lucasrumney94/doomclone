using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//public class Weapon 
//{
//	string name;
//	float damage;
//	float fireRate;
//	
//	
//	int pellets;
//	float spread;
//}




public class playerWeapons : MonoBehaviour {

	Dictionary<string,float>  weaponDamages = new Dictionary<string, float>();


	Dictionary<int,string> Weapons = new Dictionary<int, string>();
	Dictionary<string,bool> hasWeapon = new Dictionary<string,bool> ();

	[Range(.5f,2f)]
	public float fireHeight;

	[Range(0,3)]
	public int currentWeapon;
	public int numberOfWeapons;
	public float shotRange;
	public float scanSpread = 1f;
	public float scanAccuracy = 20; // keep low, its a for loop (this is the number of rays it will draw between -scanSpread and scanSpread)
	//public float scanHeight = 1.2f; //lowerbound/scanHeight, upperBound*scanHeight (>1 is a high scan affinity, <1 scans lower)


	Dictionary<string, int> Ammo = new Dictionary<string, int>();
	Dictionary<string, int> maxAmmo = new Dictionary<string, int>();

	[Header("pistol")]
	public float pistolDamage;
	public float pistolFireRate = 2f;

	[Header("shotgun")]

	public float shotgunDamage;
	public float shotgunFireRate = .5f;
	public int shotgunPellets = 8;
	public float shotgunSpread = 3f;

	[Header("machinegun")]
	public float machinegunDamage = 1f;
	public float machinegunFireRate = 5f;
	
	[Header("rocketlauncher")]
	public float rocketlauncherDamage = 4f;
	public float rocketlauncherFireRate = .2f;
	public GameObject rocketPrefab;
	public float rocketSpeed = 1.0f;

	
	[Header("ammo")]
	public int pistolAmmo = 30;
	public int shotgunAmmo = 20;
	public int machinegunAmmo = 200;
	public int rocketlauncherAmmo = 6;
	
	[Header("max Ammo")]
	public int pistolMaxAmmo = 500;
	public int shotgunMaxAmmo = 50;
	public int machinegunMaxAmmo = 500;
	public int rocketlauncherMaxAmmo = 15;


	[Space(30)]
	public float rocketLaunchOffset=1f; //forward along z


	private Vector3 fireHeight3; // for all guns
	private Vector3 bullet3;//for shotgun spread
	private Vector3 launcher3; //firing origin for launchers 


	private RaycastHit scanHit;
	private GameObject target;
	private bool targetFound;

	private bool canFire = true;
	private bool fired = false;


	//public Weapon pistol;



	// Use this for initialization
	void Start () 
	{
		StartCoroutine("fireWait");

		weaponDamages.Add ("pistol",pistolDamage);
		weaponDamages.Add ("shotgun", shotgunDamage);
		weaponDamages.Add ("machinegun", machinegunDamage);
		weaponDamages.Add ("rocketlauncher", rocketlauncherDamage);

		Weapons.Add (0, "pistol");
		Weapons.Add (1, "shotgun");
		Weapons.Add (2, "machinegun");
		Weapons.Add (3, "rocketlauncher");

		Ammo.Add ("pistol", pistolAmmo);
		Ammo.Add ("shotgun", shotgunAmmo);
		Ammo.Add ("machinegun", machinegunAmmo);
		Ammo.Add ("rocketlauncher", rocketlauncherAmmo);

		maxAmmo.Add ("pistol", pistolMaxAmmo);
		maxAmmo.Add ("shotgun", shotgunMaxAmmo);
		maxAmmo.Add ("machinegun", machinegunMaxAmmo);
		maxAmmo.Add ("rocketlauncher", rocketlauncherMaxAmmo);

		hasWeapon.Add ("pistol", true);
		hasWeapon.Add ("shotgun", true);
		hasWeapon.Add ("machinegun", true);
		hasWeapon.Add ("rocketlauncher", true);


		checkMaxAmmo ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Debug.Log (currentWeapon);
		//Debug.Log (Weapons[currentWeapon]);

		//Debug.Log (Ammo [Weapons[currentWeapon]]);
		//Debug.Log (hasWeapon ["shotgun"]);
		if (target != null)
			//Debug.Log (target.name);


		if (Input.GetButtonDown ("SwitchWeapon1")&& hasWeapon["pistol"]) 
		{
			switchWeapon (0);
		}
		if (Input.GetButtonDown ("SwitchWeapon2") && hasWeapon["shotgun"])
			switchWeapon (1);
		if (Input.GetButtonDown ("SwitchWeapon3")&& hasWeapon["machinegun"])
			switchWeapon (2);
		if (Input.GetButtonDown ("SwitchWeapon4")&& hasWeapon["rocketlauncher"])
			switchWeapon (3);


		if (Input.GetButtonDown ("SwitchWeaponForward")) /////////////////////Needs to be reworked to function with guns you don't have yet.
			switchWeapon ((currentWeapon+1)%numberOfWeapons);

		if (Input.GetButtonDown ("SwitchWeaponBackward"))
		{
			if (currentWeapon == 0)
			{
				currentWeapon = numberOfWeapons;
			}
			switchWeapon ((currentWeapon-1));
		}


		if (Input.GetButton("Fire1") && canFire) 
		{	
			if (Ammo [Weapons [currentWeapon]] != 0)
			{

				fired = true;
				fire(currentWeapon);
				canFire = false;
			
				fireWait ();
			}
		}

	}
	void fire(int currentWeapon)
	{
		target = null; //reset target every shot

		fireHeight3 = transform.position;
		fireHeight3.y += fireHeight;

		for (float i = -scanSpread; i < scanSpread; i+=(1/scanAccuracy)) //SCAN FOR LOWEST Y LEVEL ENEMY
		{
			bullet3 = Vector3.forward;
			bullet3.y = Vector3.forward.y + i;
			//print(bullet3.y);
			bullet3 = transform.TransformDirection(bullet3);


			if (Physics.Raycast(fireHeight3,bullet3,out scanHit,shotRange))
			{
				if (scanHit.collider.gameObject.tag == "Enemy")
				{
					target = scanHit.collider.gameObject;
					break;
				}
				//Debug.DrawLine(fireHeight3,scanHit.point, Color.green);
			}
		}
		if (target == null) 
			targetFound = false;
		else 
			targetFound = true;



		if (canFire)
			changeAmmo (new ammoInfo(Weapons[currentWeapon], -1,false));

		if (Weapons[currentWeapon]== "pistol" && canFire)
		{
			if (targetFound == false)
			{
				if (Physics.Raycast(fireHeight3,transform.TransformDirection(Vector3.forward),out scanHit,shotRange))
				{
					scanHit.collider.SendMessageUpwards("applyDamage",weaponDamages[Weapons[currentWeapon]],SendMessageOptions.DontRequireReceiver);
					Debug.DrawLine(fireHeight3,scanHit.point, Color.blue);
				}
			}
			if (targetFound == true)
			{
				Debug.Log ("pistol target found");
				if (Physics.Raycast(fireHeight3,target.transform.position-fireHeight3,out scanHit,shotRange))
				{
					scanHit.collider.SendMessageUpwards("applyDamage",weaponDamages[Weapons[currentWeapon]],SendMessageOptions.DontRequireReceiver);
					Debug.DrawLine(fireHeight3,scanHit.point, Color.cyan);
				}
			}

		}

		if (Weapons[currentWeapon] == "shotgun" && canFire) 
		{
			for (int i = 0; i< shotgunPellets; i++)
			{
				// old random code
				//bullet3 = transform.TransformDirection(Vector3.forward);
				//float randomX = Random.Range(-shotgunSpread,shotgunSpread);
				//bullet3.x += randomX;
				//
				//new rand code
				float randomX = Random.Range(-shotgunSpread,shotgunSpread);


				if (targetFound == false)
				{

					bullet3 = Vector3.forward;
					bullet3.x = Vector3.forward.x + randomX;
					bullet3 = transform.TransformDirection(bullet3);
					//Debug.DrawRay(fireHeight3,bullet3, Color.magenta);

					if (Physics.Raycast(fireHeight3,bullet3,out scanHit,shotRange))
					{
						scanHit.collider.SendMessageUpwards("applyDamage",weaponDamages[Weapons[currentWeapon]],SendMessageOptions.DontRequireReceiver);
						Debug.DrawLine(fireHeight3,scanHit.point, Color.green);
					}
				}

				if (targetFound == true)
				{
					bullet3 = transform.InverseTransformDirection(target.transform.position - fireHeight3);
					bullet3.x = Vector3.forward.x + 8*randomX;
					bullet3 = transform.TransformDirection(bullet3);

					Debug.DrawRay(fireHeight3,bullet3,Color.green);
					if (Physics.Raycast(fireHeight3,bullet3,out scanHit,shotRange))
					{
						scanHit.collider.SendMessageUpwards("applyDamage",weaponDamages[Weapons[currentWeapon]],SendMessageOptions.DontRequireReceiver);
						Debug.DrawLine(fireHeight3,scanHit.point,Color.red);
					}
				}
			}
		}

		if (Weapons[currentWeapon]== "machinegun" && canFire)
		{

			if (targetFound == false)
			{
				if (Physics.Raycast(fireHeight3,transform.TransformDirection(Vector3.forward),out scanHit,shotRange))
				{
					scanHit.collider.SendMessageUpwards("applyDamage",weaponDamages[Weapons[currentWeapon]],SendMessageOptions.DontRequireReceiver);
					Debug.DrawLine(fireHeight3,scanHit.point, Color.blue);
				}
			}
			if (targetFound == true)
			{
				if (Physics.Raycast(fireHeight3,target.transform.position-fireHeight3,out scanHit,shotRange))
				{
					scanHit.collider.SendMessageUpwards("applyDamage",weaponDamages[Weapons[currentWeapon]],SendMessageOptions.DontRequireReceiver);
					Debug.DrawLine(fireHeight3,scanHit.point, Color.cyan);
				}
			}
		}

		if (Weapons [currentWeapon] == "rocketlauncher" && canFire) 
		{
			if (targetFound == false)
			{
				launcher3 = fireHeight3 + rocketLaunchOffset*transform.TransformDirection(Vector3.forward);
				GameObject g = (GameObject)Instantiate(rocketPrefab,
				                                       launcher3,
				                                       transform.rotation);
				float force = g.GetComponent<rocket>().speed;
				g.GetComponent<Rigidbody>().AddForce(g.transform.forward * force);

			}
			if (targetFound == true)
			{
				launcher3 = fireHeight3 + rocketLaunchOffset*transform.TransformDirection(Vector3.forward);
			
				GameObject g = (GameObject)Instantiate(rocketPrefab,
				                                       launcher3,
				                                       transform.rotation);
				float force = g.GetComponent<rocket>().speed;
				g.GetComponent<Rigidbody>().AddForce((target.transform.position-transform.position).normalized * force);
			
			}
			
			

		}

	}

	IEnumerator fireWait()
	{
		for(;;)//always
		{
			if (fired)
			{
				if (Weapons[currentWeapon] == "pistol")
				{
					canFire = true;
					fired = false;
					yield return new WaitForSeconds(1/pistolFireRate);
				}
				if (Weapons[currentWeapon] == "shotgun") 
				{
					yield return new WaitForSeconds(1/shotgunFireRate);
					canFire = true;
					fired = false;
                }
				if (Weapons[currentWeapon] == "machinegun") 
				{
					yield return new WaitForSeconds(1/machinegunFireRate);
					canFire = true;
					fired = false;
				}
				if (Weapons[currentWeapon] == "rocketlauncher") 
				{
					yield return new WaitForSeconds(1/rocketlauncherFireRate);
					canFire = true;
					fired = false;
                }
			
			}
			yield return null;
		}
	}

	void switchWeapon(int weapon)
	{
		if (hasWeapon [Weapons[weapon]]) 
			currentWeapon = weapon;
	}

	void checkMaxAmmo()
	{
		for (int i = 0; i < numberOfWeapons; i++)
		{
			if (Ammo[Weapons[i]] > maxAmmo[Weapons[i]])
			{
				Ammo[Weapons[i]] = maxAmmo[Weapons[i]];
			}
		}
	}

	public void changeAmmo (ammoInfo a)
	{
		Ammo [a.weapon] += a.bullets;
		checkMaxAmmo ();
		if (a.isGun) 
		{
			hasWeapon[a.weapon] = true;
		}
	}

	void enemyScan()
	{

	}
}





