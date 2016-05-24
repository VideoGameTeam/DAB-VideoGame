using UnityEngine;
using System.Collections;

public class BtnLasser : MonoBehaviour {

	private GameObject LasserG;
	public float timelasser=0;
	// Use this for initialization
	void Start () {
	
		LasserG=GameObject.Find("GrandLasser");

	}
	
	// Update is called once per frame
	void Update () {
	
		if (timelasser>0)
		{
			timelasser = timelasser - Time.deltaTime;

			if (timelasser < 0) {
				timelasser = 0;
				LasserG.SetActive (true);

			}
		}


	}



	void OnTriggerEnter2D(Collider2D objeto)
	{

		if (objeto.tag == "Player") {

			timelasser = 5;
			LasserG.SetActive (false);
		}
	}


	void OnTriggerExit2D(Collider2D objeto)
	{

		if (objeto.tag == "Player") {



		}
	}



}
