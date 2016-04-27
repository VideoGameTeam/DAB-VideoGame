using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

using UnityEngine.UI; // Required when Using UI elements.


public class Gamestate : MonoBehaviour {

	public static Gamestate EstadoJuego;
	private string filename;

	public float VolumeSet;
	public float LightSet;
	public int Dificult=1;
	//Variabnles Ingame
	public float health;
	public float mana;




	void Awake()
	{
		filename= Application.persistentDataPath +"/datadab.dat";
		print (filename);

		if (EstadoJuego==null)
		{	
			EstadoJuego = this;
			DontDestroyOnLoad (gameObject);
		}
		else if(EstadoJuego!=this)
		{
			Destroy(gameObject);

		}
	}
	// Use this for initialization
	void Start () {

		LoadValue ();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void SaveValue()
	{
		BinaryFormatter bf =new BinaryFormatter();
		FileStream file=File.Create(filename);

		datatosave data = new datatosave(VolumeSet,LightSet);
		data.VolumeSet = VolumeSet;
		data.LightSet = LightSet;

		bf.Serialize (file, data);

		file.Close ();
	}

	public void LoadValue()
	{
		if (File.Exists (filename)) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (filename, FileMode.Open);

			datatosave data = (datatosave)bf.Deserialize (file);

			VolumeSet = data.VolumeSet;
			LightSet = data.LightSet;


			bf.Serialize (file, data);

			file.Close ();
		} else
		{
			//Valores por defecto
			LightSet=8;
			VolumeSet = 1;

			SaveValue ();

		}
	}



}

[Serializable]
class datatosave{

	public float VolumeSet;
	public float LightSet;


	public datatosave(float VolumeSet,float LightSet)
	{
		this.VolumeSet=VolumeSet;
		this.LightSet = LightSet;

	}

}