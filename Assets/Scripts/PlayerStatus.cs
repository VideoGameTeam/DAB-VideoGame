using UnityEngine;
using System.Collections;

using UnityEngine.UI; // Required when Using UI elements.



public class PlayerStatus : MonoBehaviour {


	//public static PlayerStatus StatePlayer;

	public Slider SliHealth;
	public Slider Slimana;
	public Text TextAdmo;
	public GameObject trident;
	public GameObject gun;
	public GameObject Bot1;
	public GameObject Bot2;
	public GameObject Bot3;



	// Use this for initialization
	void Start () {
		UpdateScreen ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Gamestate.EstadoJuego.health >= 1 && Gamestate.EstadoJuego.health <= 20) {
			Gamestate.EstadoJuego.health--;
			SliHealth.value = Gamestate.EstadoJuego.health;
			//UpdateScreen ();
		} else if (Gamestate.EstadoJuego.health <= 0) {
			Gamestate.EstadoJuego.health=0;
			Time.timeScale = 0;
			Gameover ();
		}

	}

	public void Gameover(){
	}

	public void UpdateScreen(){

			//SliHealth = GameObject.FindGameObjectWithTag("Player");
			//SliHealth = GameObject.Find("SliderHealt");
			//SliHealth = GameObject.Find("SliderMana");
			//playerStatus = playerGO.GetComponent<PlayerStatus> ();

		SliHealth.value = Gamestate.EstadoJuego.health;
		Slimana.value = Gamestate.EstadoJuego.mana;

		if (Gamestate.EstadoJuego.Admo <= 0) {
			Gamestate.EstadoJuego.Admo = 0;
		}
		TextAdmo.text= Gamestate.EstadoJuego.Admo.ToString();

		if (Gamestate.EstadoJuego.Trident) {
			gun.SetActive (false);
			trident.SetActive (true);
		}
		else {
			trident.SetActive (false);
			gun.SetActive (true);
		}

		//Botiquines

		switch (Gamestate.EstadoJuego.Medicine) {
		case 0:
			Bot1.SetActive (false);
			Bot2.SetActive (false);
			Bot3.SetActive (false);
			break;
		case 1:
			Bot1.SetActive (true);
			Bot2.SetActive (false);
			Bot3.SetActive (false);
			break;
		case 2:
			Bot1.SetActive (true);
			Bot2.SetActive (true);
			Bot3.SetActive (false);
			break;

		case 3:
			Bot1.SetActive (true);
			Bot2.SetActive (true);
			Bot3.SetActive (true);
			break;

		}
		/*
		public int Dificult;
		public float health;
		public float mana;
		public int GameLevel;
		public int Checkpoint;
		public int Admo;
		public bool Trident;
		public int Points;
		public int UserLevel;
*/
	}
}
