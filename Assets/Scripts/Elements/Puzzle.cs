using UnityEngine;
using System.Collections;

public class Puzzle : MonoBehaviour {

	public float PosYInit;
	public GameObject Platform1;
	public GameObject Platform2;
	public GameObject Platform3;
	public GameObject Platform4;
	private bool btndown=false;

	// Use this for initialization
	void Start () {
		//PosInit = new float[3];

		PosYInit = gameObject.transform.position.y;
	}


	// Update is called once per frame
	void Update () {
		if (btndown && gameObject.transform.position.y>(PosYInit-0.12f)) {
			gameObject.transform.position = new Vector3 (gameObject.transform.position.x,( gameObject.transform.position.y-0.3f*Time.deltaTime), gameObject.transform.position.z);
		
		}

		if (btndown==false && gameObject.transform.position.y<PosYInit) {
			gameObject.transform.position = new Vector3 (gameObject.transform.position.x,( gameObject.transform.position.y+0.1f*Time.deltaTime), gameObject.transform.position.z);

		}

	
	}



	void OnTriggerEnter2D(Collider2D objeto)
	{

		if (objeto.tag == "Player") {

			btndown = true;


		}
	}

	void OnTriggerExit2D(Collider2D objeto)
	{

		if (objeto.tag == "Player") {

			btndown = false;


		}
	}





}
