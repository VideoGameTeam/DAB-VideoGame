using UnityEngine;
using System.Collections;
using UnityEngine.UI; // Required when Using UI elements.


public class PauseMenu : MonoBehaviour {
	public GameObject objectpause;
	public GameObject objectOptions;
	public GameObject objectExit;




	//public Slider SliVol;
	public Slider SliVol;
	public Text TextValorVol;


	public Slider Slilight;
	//public Light MainlLight;
	public Text TextValorLight;



	public float VolumeSet;
	public float LightSet;



	// Use this for initialization
	void Start () {
		///MainlLight= GetComponent<Light>();

		/////CARGAR LUZ Y VOLUME
		/// 
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape) && Time.timeScale == 1) {
			objectpause.SetActive (true);
			objectExit.SetActive (false);
			objectOptions.SetActive (false);

			Time.timeScale = 0;

		} 
		else if(Input.GetKeyDown (KeyCode.Escape) && Time.timeScale == 0) {
			if (objectExit.activeSelf||objectOptions.activeSelf) {
				objectExit.SetActive (false);
				objectOptions.SetActive (false);
				objectpause.SetActive (true);

			}
			else {
				objectpause.SetActive (false);
				Time.timeScale = 1;
			}

		}

	}

	public void PressBtnPause()
	{
		objectpause.SetActive (true);
		Time.timeScale = 0;

	}


	public void PressBtnContinue()
	{
		objectpause.SetActive (false);
		Time.timeScale = 1;
	}

	public void PressBtnOptions()
	{
		objectpause.SetActive (false);
		objectOptions.SetActive (true);

	}
	public void PressBtnExit()
	{
		objectpause.SetActive (false);
		objectExit.SetActive (true);
	
	}



	public void PressBtnCANCEL()
	{
		objectpause.SetActive (true);
		objectOptions.SetActive (false);
		objectExit.SetActive (false);

	}
	public void PressBtnOpcOK()
	{
		objectpause.SetActive (false);
		objectOptions.SetActive (false);
		Time.timeScale = 1;
		//Function Guardar Parametros
	}

	public void PressBtnOpcEasy()
	{
		//Function Parametros
	}
	public void PressBtnOpcMedium()
	{
		//Function Parametros
	}
	public void PressBtnOpcHard()
	{
		//Function Parametros
	}

	//BTN RETURN == FUNCTION CANCEL

	public void PressBtnExEXIT()
	{
		objectpause.SetActive (false);
		objectExit.SetActive (false);
		Time.timeScale = 1;
		Application.LoadLevel ("StartGame");

	}

	public void updateVol()
	{
		VolumeSet= SliVol.value;
		TextValorVol.text = Mathf.Round( SliVol.value*100).ToString();


		AudioListener.volume = SliVol.value;

	}


	public void updateLight()
	{
		
		LightSet= Slilight.value;
		TextValorLight.text = Mathf.Round(Slilight.value).ToString();
	///	MainlLight.intensity = LightSet / 16;

		/*GameObject lightGameObject = new GameObject("MainlLight");
        Light lightComp = lightGameObject.AddComponent<Light>();
        lightComp.intensity = LightSet/16;
        lightGameObject.transform.position = new Vector3(0, 5, 0);
*/
	}




}
