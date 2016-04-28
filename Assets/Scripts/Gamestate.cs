using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

using UnityEngine.UI; // Required when Using UI elements.


public class Gamestate : MonoBehaviour {

	public static Gamestate EstadoJuego;
	private string filename;
	private string Savegamepath;
	public float VolumeSet;
	public float LightSet;
	public int LastDificult=1;
	//Variabnles Ingame
	//Las variables se modifican mediante la linea relacionada:
	//Gamestate.EstadoJuego.VolumeSet = Valor;

	public int NumberSavegame=0;

	public int Dificult;
	public float health;
	public float mana;
	public int GameLevel;
	public int Checkpoint;
	public int Admo;
	public bool Trident;
	public int Points;
	public int UserLevel;





	void Awake()
	{
		

		if (EstadoJuego==null)
		{	
			EstadoJuego = this;
			DontDestroyOnLoad (gameObject);

			filename= Application.persistentDataPath +"/datadab.dat";

		}
		else if(EstadoJuego!=this)
		{
			Destroy(gameObject);
		}
	}

	// Use this for initialization
	void Start () {

		LoadOptions ();
		defaultValGame ();
		//print ("Juego Initilised");
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void SaveGame()
	{
		if (NumberSavegame != 0) {
			Savegamepath = Application.persistentDataPath + "/Savegame_" + NumberSavegame + ".dat";

			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Create (Savegamepath);

			datasavegame data = new datasavegame (Dificult, health, mana);
			data.Dificult = Dificult;
			data.health = health;
			data.mana = mana;

			bf.Serialize (file, data);

			file.Close ();
		} else {
			print ("Partida Invalida");
		}
	}

	public void LoadGame()
	{
		Savegamepath= Application.persistentDataPath +"/Savegame_"+NumberSavegame+".dat";

		if (File.Exists (Savegamepath)) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Savegamepath, FileMode.Open);

			datasavegame data = (datasavegame)bf.Deserialize (file);

			Dificult = data.Dificult;
			health = data.health;
			mana = data.mana ;

			bf.Serialize (file, data);
			file.Close ();
		} else
		{
			NumberSavegame = 0;

			//No Existen Partidas Guardadas.

		}
	}


	public void SaveOptions()
	{
		BinaryFormatter bf =new BinaryFormatter();
		FileStream file=File.Create(filename);

		datatosave data = new datatosave(VolumeSet,LightSet);
		data.VolumeSet = VolumeSet;
		data.LightSet = LightSet;

		bf.Serialize (file, data);

		file.Close ();
	}

	public void LoadOptions()
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

			SaveOptions ();

		}
	}


	public void defaultValGame()
	{

		Dificult=LastDificult;
		health=100;
		mana=50;
		GameLevel=1;
		Checkpoint=1;
		Admo=20;
		Trident=false;
		Points=0;
		UserLevel=1;
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

		//Variables Savegame


	}
}
[Serializable]
class datasavegame{
	
	public int Dificult;
	public float health;
	public float mana;

	public datasavegame(int Dificult,float health, float mana)
	{
		this.Dificult=Dificult;
		this.health = health;
		this.mana = mana;

		//Variables Savegame


	}


}