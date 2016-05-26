using UnityEngine;
using System.Collections;

public class CheckPointOrbe : MonoBehaviour {

	private bool Checkp = false;

	private float velrotate=700; 

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Checkp) {

			gameObject.transform.Rotate(Vector3.down *velrotate* Time.deltaTime);
			velrotate = velrotate + 20; 
			if (velrotate > 2000) {
				
					Destroy (gameObject);

			}
		}
			
	
	}




	void OnTriggerEnter2D(Collider2D objeto)
	{

		if (objeto.tag == "Player") {

			Gamestate.EstadoJuego.lastcheck [0]= objeto.gameObject.transform.position.x;
			Gamestate.EstadoJuego.lastcheck [1]= objeto.gameObject.transform.position.y+2* objeto.gameObject.transform.localScale.y;
			Gamestate.EstadoJuego.lastcheck [2]= objeto.gameObject.transform.position.z;

			Checkp = true;

			AudioSource audio=GetComponent<AudioSource> ();
			audio.Play ();


		}

	}

}
