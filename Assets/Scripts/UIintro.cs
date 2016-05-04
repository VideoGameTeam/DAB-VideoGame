using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIintro : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			SceneManager.LoadScene("Level_"+ Gamestate.EstadoJuego.GameLevel);
		} 

	}

	public void PressBtnSkip()
	{
		SceneManager.LoadScene("Level_"+ Gamestate.EstadoJuego.GameLevel);
	}
}
