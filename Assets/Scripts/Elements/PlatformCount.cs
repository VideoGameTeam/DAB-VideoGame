using UnityEngine;
using System.Collections;

public class PlatformCount : MonoBehaviour {

	public Puzzle platform;
	public int PlatformID=0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	
	}



	void OnTriggerEnter2D(Collider2D objeto)
	{

		if (objeto.tag == "Player") {

			platform.plat=PlatformID;
			gameObject.GetComponent<Rigidbody2D>().gravityScale = 6;	

		}
	}


	void OnTriggerExit2D(Collider2D objeto)
	{

		if (objeto.tag == "Player") {

			gameObject.GetComponent<Rigidbody2D>().gravityScale = 3;	

		}
	}



}