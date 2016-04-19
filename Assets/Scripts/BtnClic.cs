using UnityEngine;
using System.Collections;

public class BtnClic : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseEnter() {
		//Application.LoadLevel("SomeLevel");


	}

	public void PressBtnMenu()
	{
		print ("Hola");
		gameObject.SetActive (false);


	}


}
