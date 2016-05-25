using UnityEngine;
using System.Collections;

public class RotateMedals : MonoBehaviour {
	private float vel=40;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		gameObject.transform.Rotate (Vector3.up *vel* Time.deltaTime);
	
		if (vel>40)
		{
			vel=vel+100;
			if (vel > 4000) {
				Destroy (gameObject);
			}
		}
	}




	void OnTriggerEnter2D(Collider2D objeto)
	{
		if (vel == 40) {
			if (objeto.tag == "Player") {

				vel = 150;

			}
		}
	}


}
