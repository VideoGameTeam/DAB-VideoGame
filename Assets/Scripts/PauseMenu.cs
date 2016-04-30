using UnityEngine;
using System.Collections;
using UnityEngine.UI; // Required when Using UI elements.


public class PauseMenu : MonoBehaviour {
	public GameObject objectpause;
	public GameObject objectOptions;
	public GameObject objectExit;


	public Text TextDificult;

	//public Slider SliVol;
	public Slider SliVol;
	public Text TextValorVol;


	public Slider Slilight;
	//public Light MainlLight;
	public Text TextValorLight;

	private Light MainlLight;

	// Use this for initialization
	void Start () {
		MainlLight =(Light)  FindObjectOfType (typeof(Light));
		loadstate ();
					
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape) && Time.timeScale == 1) {
			objectpause.SetActive (true);
			objectExit.SetActive (false);
			objectOptions.SetActive (false);

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

	}

	public void PressBtnPause()
	{
		//objectpause.SetActive (true);
		//Time.timeScale = 0;
		//TEST
		Gamestate.EstadoJuego.health--;
		//print (Gamestate.EstadoJuego.health.ToString ());
		GameObject.Find ("PlayerStatus").SendMessage ("UpdateScreen"); 

	}


	public void PressBtnContinue()
	{
		objectpause.SetActive (false);
		Time.timeScale = 1;
	}

	public void PressBtnOptions()
	{
		objectpause.SetActive (false);
		objectOptions.SetActive (true);

	}
	public void PressBtnExit()
	{
		objectpause.SetActive (false);
		objectExit.SetActive (true);
	
	}



	public void PressBtnCANCEL()
	{
		objectpause.SetActive (true);
		objectOptions.SetActive (false);
		objectExit.SetActive (false);
		loadstate ();


	}
	//BTN RETURN == FUNCTION CANCEL

	public void PressBtnExEXIT()
	{
		objectpause.SetActive (false);
		objectExit.SetActive (false);
		Time.timeScale = 1;
		Application.LoadLevel ("StartGame");

	}



	public void PressBtnOpcOK()
	{
		objectpause.SetActive (false);
		objectOptions.SetActive (false);
		Time.timeScale = 1;
		//Function Guardar Parametros

		Gamestate.EstadoJuego.VolumeSet = SliVol.value;
		Gamestate.EstadoJuego.LightSet= Slilight.value;
		Gamestate.EstadoJuego.SaveOptions ();
		loadstate ();
			

	}

	void loadstate(){
	
		//OptionsStatus
		SliVol.value = Gamestate.EstadoJuego.VolumeSet;
		Slilight.value = Gamestate.EstadoJuego.LightSet;

		TextValorVol.text = Mathf.Round( SliVol.value*100).ToString();
		TextValorLight.text = Mathf.Round(Slilight.value).ToString();
		AudioListener.volume = SliVol.value;
			print(Gamestate.EstadoJuego.LightSet.ToString());
		MainlLight.intensity = Gamestate.EstadoJuego.LightSet/14;

		switch (Mathf.FloorToInt(Gamestate.EstadoJuego.Dificult)) {
		case 0:
			TextDificult.text = "FACIL";
			break;
		case 1:
			TextDificult.text = "MEDIO";
			break;
		case 2:
			TextDificult.text = "DIFICIL";
			break;

		}

		//TextDificult.text=Gamestate.EstadoJuego.Dificult;
		//Gamestatus
	
	}



	public void updateVol()
	{
		
		TextValorVol.text = Mathf.Round( SliVol.value*100).ToString();
		AudioListener.volume = SliVol.value;

	}


	public void updateLight()
	{
		TextValorLight.text = Mathf.Round(Slilight.value).ToString();
		MainlLight.intensity = Slilight.value / 14;

	}



}
