using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUDHandler : MonoBehaviour {

	private GameObject player;
	private float health;
	private Text healthText;
	private float armor;
	private Text armorText;
	private float currentAmmo;
	private Text currentAmmoText;
	private float Ammo1;
	private Text Ammo1Text;
	private float Ammo2;
	private Text Ammo2Text;
	private float Ammo3;
	private Text Ammo3Text;


	// Use this for initialization
	void Start () 
	{
		//Get values
		player = GameObject.FindGameObjectWithTag("Player");

		//get references to the HUD once
		healthText = GameObject.FindGameObjectWithTag("HealthValue").GetComponent<Text>();
		armorText = GameObject.FindGameObjectWithTag("ArmorValue").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		//get values
		health = player.GetComponent<playerHealth>().health;
		armor = player.GetComponent<playerHealth>().armor;


		//Update the values on the HUD

		healthText.text = health.ToString("n0");
		armorText.text = armor.ToString("n0");
	}
}
