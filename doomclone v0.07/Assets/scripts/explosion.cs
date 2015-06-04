using UnityEngine;
using System.Collections;

public class explosion : MonoBehaviour {

	public float explosiveDamage;
	private int i = 0;

	// Use this for initialization
	void Start () 
	{

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
		c.collider.SendMessageUpwards("applyDamage",explosiveDamage, SendMessageOptions.DontRequireReceiver);
	}

}
