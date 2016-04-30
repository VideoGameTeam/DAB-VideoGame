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

	public GameObject PanGOver;
	public GameObject PopSave;

	public Text TxtGameover;

	private float timeh=0;
	private float timeOver=0;
	public float deltat=0;

	// Use this for initialization
	void Start () {
		UpdateScreen ();
		deltat=Time.deltaTime;
	}
	
	// Update is called once per frame
	void Update () {
	//	deltat=Time.deltaTime;
		if (Gamestate.EstadoJuego.health >= 1 && Gamestate.EstadoJuego.health <= 20) {
		
			if (timeh>=(5-Gamestate.EstadoJuego.Dificult)) {
				timeh = 0;
				Gamestate.EstadoJuego.health--;
				SliHealth.value = Gamestate.EstadoJuego.health;
			}
			else
			{
				timeh=timeh + deltat;
			}

		} else if (Gamestate.EstadoJuego.health <= 0) {
			Gamestate.EstadoJuego.health=0;
			Time.timeScale = 0;

			if (timeOver>=(10)) {
				timeOver = 0;
				PanGOver.SetActive (false);
				Time.timeScale = 1;
				TolastCheckpoin ();
			}
			else
			{
				PanGOver.SetActive (true);
				timeOver=timeOver + deltat;
				TxtGameover.text = (10 - Mathf.FloorToInt (timeOver)).ToString();
			}



		}

	}


	public void UpdateScreen(){


		SliHealth.value = Gamestate.EstadoJuego.health;
		if (Gamestate.EstadoJuego.health >= 1 && Gamestate.EstadoJuego.health <= 20) {
			timeh = Time.time + 5-Gamestate.EstadoJuego.Dificult;
		}
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

	}

	public void TolastCheckpoin ()
	{
		Gamestate.EstadoJuego.defaultValGame ();
		UpdateScreen ();
	}

	public void FinishLevel()
	{
		Gamestate.EstadoJuego.health=100;
		Gamestate.EstadoJuego.GameLevel++;
		Time.timeScale = 0;
		PopSave.SetActive (true);
		//Gamestate.EstadoJuego.defaultValGame ();
		//UpdateScreen ();
	}


}
