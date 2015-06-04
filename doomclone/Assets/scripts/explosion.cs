using UnityEngine;
using System.Collections;

public class explosion : MonoBehaviour {

	public float explosiveDamage;
	public float radius = 2.0f;
	private int i = 0;

	private bool switchForStay = false;//this is the boolean flag so that the explosion enters  

	// Use this for initialization
	void Start () 
	{
		Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
		int j = 0;
		while (j < hitColliders.Length) 
		{
			hitColliders[j].SendMessageUpwards("applyDamage",explosiveDamage,SendMessageOptions.DontRequireReceiver);
			Debug.Log (hitColliders[j].name);
			j++;
		}
	}

	void Update()
	{
		i++;
		if (i>50)
		{
			Destroy(this.gameObject);
		}
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
}
