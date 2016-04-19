using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {
	public GameObject objetpause;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape) && Time.timeScale == 1) {
			objetpause.SetActive (true);
			Time.timeScale = 0;

		} 
		else if(Input.GetKeyDown (KeyCode.Escape) && Time.timeScale == 0) {
			objetpause.SetActive (false);
			Time.timeScale = 1;

		}
			
	
	}
}
