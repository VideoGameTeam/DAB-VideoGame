using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour {

    private MovieTexture movie;
    private Renderer render;
    private AudioSource audioSource;

    // Use this for initialization
    void Start() {
        render = GetComponent<Renderer>();
        movie = (MovieTexture)render.material.mainTexture;
        audioSource = GetComponent<AudioSource>();
        movie.Play();
        audioSource.Play();

    }
	
	// Update is called once per frame
	void Update () {
		if( movie.isPlaying == false ){
			SceneManager.LoadScene("Level_"+ Gamestate.EstadoJuego.GameLevel);	
		}
	}
}
