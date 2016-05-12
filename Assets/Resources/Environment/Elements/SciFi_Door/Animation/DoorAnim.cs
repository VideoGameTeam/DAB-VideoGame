using UnityEngine;
using System.Collections;

public class DoorAnim : MonoBehaviour {

	//private bool open=false;
	public Animation Animador;

	// Update is called once per frame
	void Update () {

	}


	void OnTriggerEnter2D(Collider2D objeto)
	{

		if (objeto.tag == "Player") {
			Animador.PlayQueued ("open");
		}

	}


	void OnTriggerExit2D(Collider2D objeto)
	{

	if (objeto.tag == "Player") {
			Animador.PlayQueued ("close");
		}
	
	

	}

}
