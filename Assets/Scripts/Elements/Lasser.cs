using UnityEngine;
using System.Collections;

public class Lasser : MonoBehaviour {

		//public GameObject Shot1;
		public GameObject Shot2;
		//public GameObject Wave;
		private float Disturbance;

		public int ShotType = 0;

		private GameObject NowShot;

		void Start () {
		//NowShot = null;
		//GameObject Bullet;

		//Bullet = Shot2;
		//Fire
		NowShot = (GameObject)Instantiate(Shot2, this.transform.position, this.transform.rotation);
		}

		void Update () {
			
	
			}

	/*
	void OnTriggerEnter2D(Collider2D objeto)
	{

		if (objeto.tag == "Player") {
			Gamestate.EstadoJuego.ChangeHealth (-30);
	//		anim.SetFloat ("Damage", 0.0F);
		}

	}

	void OnTriggerExit2D(Collider2D objeto)
	{

		if (objeto.tag == "Lasser") {
	//		anim.SetFloat ("Damage", 0.0F);
		}

	}
*/


		
	}




