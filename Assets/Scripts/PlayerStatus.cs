using UnityEngine;
using System.Collections;

using UnityEngine.UI; // Required when Using UI elements.



public class PlayerStatus : MonoBehaviour {


	//public static PlayerStatus StatePlayer;

	public Slider SliHealth;
	public Slider Slimana;

	public Text TextAdmo;



	// Use this for initialization
	void Start () {
		UpdateScreen ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void UpdateScreen(){

			//SliHealth = GameObject.FindGameObjectWithTag("Player");
			//SliHealth = GameObject.Find("SliderHealt");
			//SliHealth = GameObject.Find("SliderMana");
			//playerStatus = playerGO.GetComponent<PlayerStatus> ();

			SliHealth.value = Gamestate.EstadoJuego.health;
			Slimana.value = Gamestate.EstadoJuego.mana;

	}
}
