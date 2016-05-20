using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}



	void OnTriggerEnter2D(Collider2D objeto)
	{

		if (objeto.tag == "Player") {

			Gamestate.EstadoJuego.lastcheck [0]= objeto.gameObject.transform.position.x;
			Gamestate.EstadoJuego.lastcheck [1]= objeto.gameObject.transform.position.y+2* objeto.gameObject.transform.localScale.y;
			Gamestate.EstadoJuego.lastcheck [2]= objeto.gameObject.transform.position.z;
		//	print("Player OK");

				//GameObject.Find ("Backtxt").GetComponent<RawImage>().enabled = true;



		}

	}


}
