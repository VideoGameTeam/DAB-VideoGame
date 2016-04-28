using UnityEngine;
using System.Collections;
using UnityEngine.UI; // Required when Using UI elements.


public class UpdateGame : MonoBehaviour {

	public Slider SliHealth;
	public Slider Slimana;

	public Text TextAdmo;


	// Use this for initialization
	void Start () {
		UpdateStatus ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void UpdateStatus()
	{
		SliHealth.value = Gamestate.EstadoJuego.health;
		Slimana.value = Gamestate.EstadoJuego.mana;

	}

}
