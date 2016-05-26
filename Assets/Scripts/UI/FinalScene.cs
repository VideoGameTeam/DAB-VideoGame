using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class FinalScene : MonoBehaviour {

	public GameObject Credits;


	// Use this for initialization
	void Start () {
		AudioListener.volume = Gamestate.EstadoJuego.VolumeSet;
	}
	
	// Update is called once per frame
	void Update () {
	
	}



	public void PressCredits()
	{

		//MenuPpal.SetActive (false);
		//MainOption.SetActive (false);
		Credits.SetActive (true);

		Time.timeScale = 0;

	}


	public void PressCancel()
	{

		Credits.SetActive (false);
		Time.timeScale = 1;

	}

	public void PressNewGame()
	{

		//Credits.SetActive (false);
		Time.timeScale = 1;
		SceneManager.LoadScene("StartGame");

	}

}
