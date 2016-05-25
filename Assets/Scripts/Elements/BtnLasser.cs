using UnityEngine;
using System.Collections;

public class BtnLasser : MonoBehaviour {

	private GameObject LasserG;
	public float timelasser=0;
	private AudioSource audio;
	// Use this for initialization
	void Start () {
	
		LasserG=GameObject.Find("GrandLasser");
		audio= GetComponent<AudioSource> ();

	}
	
	// Update is called once per frame
	void Update () {
	
		if (timelasser>0)
		{
			timelasser = timelasser - Time.deltaTime;
			audio.pitch= 0.7f + 1/(1+timelasser);

			if (timelasser < 0) {
				timelasser = 0;
				LasserG.SetActive (true);
				audio.Stop ();
			}
		}
	}



	void OnTriggerEnter2D(Collider2D objeto)
	{

		if (objeto.tag == "Player") {

			timelasser = 5;
			LasserG.SetActive (false);

			audio.Play ();


		}
	}


	void OnTriggerExit2D(Collider2D objeto)
	{

		if (objeto.tag == "Player") {



		}
	}



}
