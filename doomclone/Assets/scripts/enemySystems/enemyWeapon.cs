using UnityEngine;
using System.Collections;

public class enemyWeapon : MonoBehaviour {


	public bool isMelee;
	public bool isHitScan;
	public bool hasShotgun;
	public bool isProjectile;

	public float lowDamage=1.0f;
	public float highDamage=5.0f;

	public float lowFireRate=1.0f;
	public float highFireRate=3.0f;

	public float shotRange=200f;
	public float meleeRange=1f;

	public int shotgunPellets = 8;
	public float shotgunSpread = .6f;


	public GameObject rocketPrefab;



	//randomly generated within range, real damage/fireRate
	private float damage;
	private float fireRate;

	//Bools for firing logic
	private bool canFire = true;
	private bool fired = false;

	private RaycastHit scanHit; // for hitscan weapons

	private Vector3 bullet3; //for shotgun spread
	private Vector3 launcher3; //for projectile launching
	public float launcher3Offset = 1.0f; 

	private bool isAwake;


	private GameObject target;

	// Use this for initialization
	void Start () 
	{
		StartCoroutine("fireWait");
		target = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (this.gameObject.GetComponent<enemyMovement> ().awake == 1)
			isAwake = true;
		else 
			isAwake = false;

		if (isAwake)
		{
			//Debug.Log ("enemy is awake in the Weapon script!");
			if (canFire && isHitScan) 
			{	
				fired = true;
				fire ();
				canFire = false;
				fireWait ();
			}
			if (canFire && isProjectile) 
			{	
				fired = true;
				fire ();
				canFire = false;
				fireWait ();
			}
			if (canFire && isMelee) 
			{
				fired = true;
				melee ();
				canFire = false;
				fireWait ();
			}
		}

	}

	void fire()
	{
		//Debug.Log ("enemy fire function called!");
		randomDamage();
		randomFireRate();
		if (hasShotgun) 
		{
			for (int i = 0; i< shotgunPellets; i++)
			{
				float randomX = Random.Range(-shotgunSpread,shotgunSpread);
				bullet3 = transform.InverseTransformDirection(target.transform.position - transform.position);
				bullet3.x = bullet3.x + 8*randomX;
				bullet3 = transform.TransformDirection(bullet3);
				
				Debug.DrawRay(transform.position,bullet3,Color.yellow);
				if (Physics.Raycast(transform.position,bullet3,out scanHit,shotRange))
				{
					if (scanHit.collider.tag == "Player") //I could disable this and make them able to hit each other! another if statement would be needed to keep them from shooting at walls
					{
						scanHit.collider.SendMessageUpwards("applyDamage",damage,SendMessageOptions.DontRequireReceiver);
						Debug.DrawLine(transform.position,scanHit.point,Color.red);
					}
				}
			}
		} 
		else if (isProjectile)
		{
			if (Physics.Raycast (transform.position, target.transform.position - transform.position, out scanHit, shotRange)) 
			{
				if (scanHit.collider.tag == "Player") 
				{
					Debug.Log("rocket Fired!");
					launcher3 = transform.position + launcher3Offset*((target.transform.position - transform.position).normalized);
					
					GameObject g = (GameObject)Instantiate(rocketPrefab,
					                                       launcher3,
					                                       transform.rotation);
					float force = g.GetComponent<rocket>().speed;
					g.GetComponent<Rigidbody>().AddForce((target.transform.position-transform.position).normalized * force);
				}
			}
		}
		else 
		{
			if (Physics.Raycast (transform.position, target.transform.position - transform.position, out scanHit, shotRange)) 
			{
				//Debug.Log ("enemy fire raycast performed!");
				//Debug.Log (scanHit.collider.tag);
				//Debug.DrawRay(transform.position,transform.TransformDirection(Vector3.back), Color.yellow);
				if (scanHit.collider.tag == "Player") 
				{
					//Debug.Log ("enemy fire raycast hit Player!");
					scanHit.collider.SendMessageUpwards ("applyDamage", damage, SendMessageOptions.DontRequireReceiver);
					//Debug.DrawLine (transform.position, scanHit.point, Color.green);
				}
			}
		}
	}

	void melee()
	{
		randomDamage();
		randomFireRate();
		if (Physics.Raycast (transform.position, target.transform.position-transform.position, out scanHit, meleeRange)) 
		{
			if (scanHit.collider.tag == "Player")
			{
				//Debug.Log("melee hit!");
				scanHit.collider.SendMessageUpwards("applyDamage",damage,SendMessageOptions.DontRequireReceiver);
				Debug.DrawLine(transform.position,scanHit.point, Color.green);
			}
		}
	}


	IEnumerator fireWait()
	{
		for(;;)//always
		{
			if (fired) //has fired!
			{
				canFire = true; //can fire again
				fired = false; //hasn't fired
				yield return new WaitForSeconds(1/fireRate); 
			}
			yield return null;
		}
	}


	void randomDamage()
	{
		damage = Random.Range (lowDamage, highDamage);
	}


	void randomFireRate()
	{
		fireRate = Random.Range (lowFireRate, highFireRate);
	}



}
