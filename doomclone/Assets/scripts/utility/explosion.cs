using UnityEngine;
using System.Collections;

public class explosion : MonoBehaviour {

	public float explosiveDamage;
	public float radius = 2.0f;
	public float secondsTillDestroy = 2.0f;

	private bool destroyMe = false;

	private bool switchForStay = false;//this is the boolean flag so that the explosion enters  

	// Use this for initialization
	void Start () 
	{	
		StartCoroutine("destroyAfterSeconds");
		Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
		int j = 0;
		while (j < hitColliders.Length) 
		{
			hitColliders[j].SendMessageUpwards("applyDamage",explosiveDamage,SendMessageOptions.DontRequireReceiver);
			//Debug.Log (hitColliders[j].name);
			j++;
		}
	}

	void Update()
	{

		if (destroyMe)
			Destroy(this.gameObject);
	}

	void OnCollisionEnter(Collision c)
	{
//		if (switchForStay)
//		{
//			c.collider.SendMessageUpwards("applyDamage",explosiveDamage, SendMessageOptions.DontRequireReceiver);
//			switchForStay = false;
//			Physics.OverlapSphere
//		}
	}

	IEnumerator destroyAfterSeconds()
	{
		for(;;)//always
		{
			yield return new WaitForSeconds(secondsTillDestroy);
			destroyMe = true;
		}
	}
}
