using UnityEngine;
using System.Collections;



public class ShowText : MonoBehaviour {

	public string text;

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player") {
			Gamestate.EstadoJuego.ChangeMsg (text, 6);
			Object.Destroy (this.gameObject);
		}
	}
}
