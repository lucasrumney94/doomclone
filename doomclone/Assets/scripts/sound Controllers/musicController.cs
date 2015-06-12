using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class musicController : MonoBehaviour {
	
	public AudioClip[] music;

	public int defaultSong = 0; 




	private int currentSong;
	private AudioSource musicSource;




	// Use this for initialization
	void Start () 
	{
		musicSource = this.gameObject.GetComponent<AudioSource>();

		musicSource.clip = music[defaultSong];

		musicSource.Play();

	}
	
	// Update is called once per frame
	void Update () 
	{

		if (!musicSource.isPlaying)
		{
			changeTo(0);
		}


	}

	public void changeTo(int song)
	{	
		musicSource.clip = music[song];

		musicSource.Play();
	}


}
