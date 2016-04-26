using UnityEngine;
using System.Collections;
using UnityEngine.UI; // Required when Using UI elements.


public class MainMenu : MonoBehaviour {
	public GameObject MenuPpal;
	public GameObject MainOption;
	public GameObject MainExit;
	public GameObject Credits;

	//public Slider SliVol;
	public Slider SliVol;
	public Text TextValorVol;


	public Slider Slilight;
	//public Light MainlLight;
	public Text TextValorLight;

	public Slider SliDificult;
	public 	Text TxtValDificult;



	// Use this for initialization
	void Start () {

		loadstate ();
	
	}
	
	// Update is called once per frame
	void Update () {
		/*
		if (Input.GetKeyDown (KeyCode.Escape) && Time.timeScale == 1) {
			MenuPpal.SetActive (false);
			MainExit.SetActive (true);
			//objectOptions.SetActive (false);
			Time.timeScale = 0;

		} 
		else if(Input.GetKeyDown (KeyCode.Escape) && Time.timeScale == 0) {
			if (objectExit.activeSelf||objectOptions.activeSelf) {
				objectExit.SetActive (false);
				objectOptions.SetActive (false);
				objectpause.SetActive (true);

			}
			else {
				objectpause.SetActive (false);
				Time.timeScale = 1;
			}

		}
	*/
	}

	public void PressBtnNewGame()
	{
		Time.timeScale = 1;
		Application.LoadLevel ("InGame");

	}


	public void PressBtnMenuOpt()
	{
		MenuPpal.SetActive (false);
		MainOption.SetActive (true);
		//objectExit.SetActive (false);

	}


	public void PressBtnMenuExit()
	{
		MenuPpal.SetActive (false);
		MainExit.SetActive (true);
		//objectExit.SetActive (false);

	}

	public void PressBtnMenuCANCEL()
	{
		MenuPpal.SetActive (true);
		MainOption.SetActive (false);
		MainExit.SetActive (false);
		Credits.SetActive (false);
		//objectExit.SetActive (false);
		Time.timeScale = 1;
		loadstate ();

	}


	public void PressBtnMenuExExit()
	{
		//Time.timeScale = 0;
		Application.Quit();


	}


	public void PressBtnMenuOptOK()
	{

		MenuPpal.SetActive (true);
		MainOption.SetActive (false);
		///GUARDAR CAMBIOS

		Gamestate.EstadoJuego.VolumeSet = SliVol.value;
		Gamestate.EstadoJuego.LightSet= Slilight.value;
		Gamestate.EstadoJuego.Dificult=Mathf.FloorToInt( SliDificult.value);
		Gamestate.EstadoJuego.SaveValue ();

		loadstate ();

	}

	public void PressBtnMenuCredits()
	{

		MenuPpal.SetActive (false);
		MainOption.SetActive (false);
		Credits.SetActive (true);
		///GUARDAR CAMBIOS
		/// 
		Time.timeScale = 0;

	
	}


	void loadstate()
	{

		SliVol.value = Gamestate.EstadoJuego.VolumeSet;
		Slilight.value = Gamestate.EstadoJuego.LightSet;
		SliDificult.value = Gamestate.EstadoJuego.Dificult;
		updateDificult ();

	TextValorVol.text = Mathf.Round( SliVol.value*100).ToString();
	TextValorLight.text = Mathf.Round(Slilight.value).ToString();
	AudioListener.volume = SliVol.value;

	}

	public void updateVol()
	{

		TextValorVol.text = Mathf.Round( SliVol.value*100).ToString();
		AudioListener.volume = SliVol.value;

	}


	public void updateLight()
	{
		TextValorLight.text = Mathf.Round(Slilight.value).ToString();

	}

	public void updateDificult()
	{
		switch (Mathf.FloorToInt(SliDificult.value)) {
		case 0:
			TxtValDificult.text = "FACIL";
			break;
		case 1:
			TxtValDificult.text = "MEDIO";
			break;
		case 2:
			TxtValDificult.text = "DIFICIL";
			break;

		}





	}







}
