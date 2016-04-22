using UnityEngine;
using System.Collections;
using UnityEngine.UI; // Required when Using UI elements.


public class ChangeLigth : MonoBehaviour {
	public Slider SliLuz;
	public Light MainlLight;

	// Use this for initialization
	void Start () {
		MainlLight= GetComponent<Light>();
	
	}
	
	// Update is called once per frame
	void Update () {
	
		MainlLight.intensity = SliLuz.value/14;

	}
}
