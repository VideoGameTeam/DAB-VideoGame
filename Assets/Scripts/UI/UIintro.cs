using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIintro : MonoBehaviour {

	private GameObject video;
	public GameObject Loading;
	// Use this for initialization
	void Start () {

		video = GameObject.Find ("Video");
		//Loading = GameObject.Find ("TextContinue");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			loadLevel ();
		} 

	}

	public void PressBtnSkip()
	{
		loadLevel ();
	}

	void loadLevel()
	{
		video.SetActive (false);
		SceneManager.LoadScene("Level_"+ Gamestate.EstadoJuego.GameLevel);
		Loading.SetActive (true);

	}
}
