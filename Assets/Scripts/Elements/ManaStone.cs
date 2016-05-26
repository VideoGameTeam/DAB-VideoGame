using UnityEngine;
using System.Collections;

public class ManaStone : MonoBehaviour {
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			Gamestate.EstadoJuego.Trident = true;
			Gamestate.EstadoJuego.mana += 30;
			if(Gamestate.EstadoJuego.mana>100){
				Gamestate.EstadoJuego.mana = 100;
			}

			Object.Destroy (this.gameObject);

		}
	}
}
