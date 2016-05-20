using UnityEngine;
using System.Collections;



public class ShowText : MonoBehaviour {

	public string text;

	void OnTriggerEnter2D(Collider2D other)
	{
		Gamestate.EstadoJuego.ChangeMsg (text,5);
		Object.Destroy (this.gameObject);
	}
}
