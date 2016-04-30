using UnityEngine;
using System.Collections;
using UnityEngine.UI; // Required when Using UI elements.
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
	public GameObject MenuPpal;
	public GameObject MainOption;
	public GameObject MainExit;
	public GameObject MainLoadPart;
	public GameObject Credits;

	public Text PreviewPart;

	public Slider SliVol;
	public Text TextValorVol;
	public Slider Slilight;
	public Text TextValorLight;
	public Slider SliDificult;
	public 	Text TxtValDificult;

	private Light MainlLight;

	void Awake()
	{
		MainlLight = (Light)FindObjectOfType (typeof(Light));
	
		loadstate ();

	}
	void Start () {


		loadstate ();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PressBtnNewGame()
	{
		Gamestate.EstadoJuego.defaultValGame ();
		Time.timeScale = 1;
		SceneManager.LoadScene("Level_1");
		//Application.LoadLevel ("Level_1");
	}


	public void PressBtnMenuLoadPart()
	{
		Gamestate.EstadoJuego.LastDificult=Gamestate.EstadoJuego.Dificult;
		MenuPpal.SetActive (false);
		MainLoadPart.SetActive (true);

		//Primer Partida
		Gamestate.EstadoJuego.NumberSavegame = 1;
		Gamestate.EstadoJuego.LoadGame ();
		UpdatePreview ();

	}

	public void PressBtnMenuOpt()
	{
		MenuPpal.SetActive (false);
		MainOption.SetActive (true);

		Gamestate.EstadoJuego.LastDificult=Gamestate.EstadoJuego.Dificult;
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
		MainLoadPart.SetActive (false);
		Credits.SetActive (false);
		//objectExit.SetActive (false);
		Time.timeScale = 1;
		loadstate ();
		Gamestate.EstadoJuego.defaultValGame();

	}


	public void PressBtnMenuExExit()
	{
		Application.Quit();
	}


	public void PressBtnMenuOptOK()
	{

		MenuPpal.SetActive (true);
		MainOption.SetActive (false);
		///GUARDAR CAMBIOS

		Gamestate.EstadoJuego.VolumeSet = SliVol.value;
		Gamestate.EstadoJuego.LightSet= Slilight.value;
		Gamestate.EstadoJuego.Dificult=Mathf.FloorToInt(SliDificult.value);
		Gamestate.EstadoJuego.LastDificult=Gamestate.EstadoJuego.Dificult;

		Gamestate.EstadoJuego.SaveOptions();

		loadstate ();

	}

	public void PressBtnMenuPreviewPart()
	{
		//Cargar Savegame Seleccionado
	}

	public void PressBtnMenuStartPart()
	{
		SceneManager.LoadScene("Level_"+ Gamestate.EstadoJuego.GameLevel);
		//Application.LoadLevel ("Level_"+ Gamestate.EstadoJuego.GameLevel);
		//Application.LoadLevel ("InGame");

	}

	public void PressBtnMenuCredits()
	{

		MenuPpal.SetActive (false);
		MainOption.SetActive (false);
		Credits.SetActive (true);

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

		//	print(Gamestate.EstadoJuego.LightSet.ToString());
		MainlLight.intensity = Gamestate.EstadoJuego.LightSet/14;

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

	public void GuadarJuego3()
	{
		Gamestate.EstadoJuego.NumberSavegame = 3;

		Gamestate.EstadoJuego.health = 70;
		Gamestate.EstadoJuego.mana = 60;
		Gamestate.EstadoJuego.Dificult= 2;

		Gamestate.EstadoJuego.Medicine = 3;
		Gamestate.EstadoJuego.Admo= 333;
		Gamestate.EstadoJuego.Trident=false;
		Gamestate.EstadoJuego.Points=3333;

		Gamestate.EstadoJuego.GameLevel=3;
		Gamestate.EstadoJuego.Checkpoint=3;
		Gamestate.EstadoJuego.UserLevel=3;

		Gamestate.EstadoJuego.SaveGame ();

	}


	public void PreviewGame1()
	{
		Gamestate.EstadoJuego.NumberSavegame = 1;
		Gamestate.EstadoJuego.LoadGame ();
		UpdatePreview ();

	}
	public void PreviewGame2()
	{
		Gamestate.EstadoJuego.NumberSavegame = 2;
		Gamestate.EstadoJuego.LoadGame ();
		UpdatePreview ();

	}

	public void PreviewGame3()
	{
		Gamestate.EstadoJuego.NumberSavegame = 3;
		Gamestate.EstadoJuego.LoadGame ();
		UpdatePreview ();

	}

	public void UpdatePreview()
	{
		if (Gamestate.EstadoJuego.NumberSavegame != 0) {
			PreviewPart.text = "Partida " + Gamestate.EstadoJuego.NumberSavegame + "\nDificult: " + Gamestate.EstadoJuego.Dificult + ";   Nivel: " + Gamestate.EstadoJuego.GameLevel + ";   Player Rank: " + Gamestate.EstadoJuego.UserLevel;
		}else
		{
			PreviewPart.text = "No Existe Partida Guardada en este Slot";
		}
	}

}
