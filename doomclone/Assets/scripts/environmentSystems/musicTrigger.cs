using UnityEngine;
using System.Collections;

public class musicTrigger : MonoBehaviour {

	public int changeSongTo;

	private bool tripped = false;


	void OnTriggerEnter(Collider c)
	{
		if (c.tag == "Player" && !tripped)
		{
			GameObject.FindGameObjectWithTag("MusicController").GetComponent<musicController>().changeTo(changeSongTo);
			tripped = true;
			Destroy(this.gameObject);
		}
	}
}
