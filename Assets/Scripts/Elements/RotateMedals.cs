using UnityEngine;
using System.Collections;

public class RotateMedals : MonoBehaviour {
	public float vel=40;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		gameObject.transform.Rotate (Vector3.up *vel* Time.deltaTime);
	
		if (vel>40)
		{
			
			if (vel > 4000) {
				vel = vel + Time.deltaTime;
				if (vel>4053.5f)
				{
					Time.timeScale = 0;
				GameObject.Find ("PlayerStatus").SendMessage ("FinishLevel");
				Destroy (gameObject);
				}
				return;

			}
			vel=vel+100;
		}
	}




	void OnTriggerEnter2D(Collider2D objeto)
	{
		if (vel == 40) {
			Gamestate.EstadoJuego.GameLevel = 2;/// Solo requerido en Modo depuracion
			if (objeto.tag == "Player") {

				vel = 150;
				AudioSource audio=GetComponent<AudioSource> ();
				audio.Play ();
				Destroy (gameObject.GetComponent<Collider2D> ());// .isTrigger = false;




			}
		}
	}


}
