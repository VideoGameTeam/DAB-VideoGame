using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
	public GameObject MenuPpal;
	public GameObject MainOption;
	public GameObject MainExit;


	// Use this for initialization
	void Start () {
	
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
		//objectExit.SetActive (false);

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
		

	}


}
