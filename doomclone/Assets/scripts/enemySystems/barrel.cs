using UnityEngine;
using System.Collections;

public class barrel : MonoBehaviour {

	public GameObject explosionPrefab;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	void applyDamage()
	{
		Instantiate(explosionPrefab,transform.position,Quaternion.identity);
		Destroy(this.gameObject);
	}
}
