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
	public int Medicine;

	public int GameLevel;
	public int Checkpoint;
	public int Admo;
	public bool Trident;
	public int Points;
	public int UserLevel;



	private float HealthMod=0;
	private float ManaMod=0;

	void Awake()
	{
		if (EstadoJuego==null)
		{	
			EstadoJuego = this;
			DontDestroyOnLoad (gameObject);

			filename= Application.persistentDataPath +"/datadab.dat";
			LoadOptions ();
			defaultValGame ();

		}
		else if(EstadoJuego!=this)
		{
			Destroy(gameObject);
		}
			

	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (HealthMod > 0) {
			health++;
			HealthMod--;
			if (health > 100) {
				health = 100;
				HealthMod = 0;
			}
			GameObject.Find ("PlayerStatus").SendMessage ("UpdateScreen");
		} else if (HealthMod < 0) {
			health--;
			HealthMod++;
			if (health <0) {
				health = 0;
				HealthMod = 0;
			}
			GameObject.Find ("PlayerStatus").SendMessage ("UpdateScreen");
		}
		if (ManaMod > 0) {
			mana++;
			ManaMod--;
			if (mana> 100) {
				mana= 100;
				ManaMod = 0;
			}
			GameObject.Find ("PlayerStatus").SendMessage ("UpdateScreen");
		} else if (ManaMod < 0) {
			mana--;
			ManaMod++;
			if (mana<0) {
				mana = 0;
				ManaMod = 0;
			}
			GameObject.Find ("PlayerStatus").SendMessage ("UpdateScreen");
		}


	}

	public void ChangeHealth(float value)
	{
		HealthMod = value;
	}


	public void ChangeMana(float value)
	{
		ManaMod = value;
	}

	public void FindSavefile()
	{
		Savegamepath= Application.persistentDataPath +"/Savegame_"+NumberSavegame+".dat";

		if (File.Exists (Savegamepath)==false) {
			NumberSavegame = 0;
		}
	}

	public void SaveGame()
	{
		if (NumberSavegame != 0) {
			Savegamepath = Application.persistentDataPath + "/Savegame_" + NumberSavegame + ".dat";

			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Create (Savegamepath);

			datasavegame data = new datasavegame (Dificult,health,mana, Medicine, Admo, Trident, Points,GameLevel,Checkpoint,UserLevel);
			data.Dificult = Dificult;
			data.health = health;
			data.mana = mana;

			data.Medicine = Medicine;
			data.Admo= Admo;
			data.Trident=Trident;
			data.Points=Points;

			data.GameLevel=GameLevel;
			data.Checkpoint=Checkpoint;
			data.UserLevel=UserLevel;

			bf.Serialize (file, data);

			file.Close ();
			//void (true);
		} else {
			print ("Partida Invalida");
			//return(false);
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

			Medicine= data.Medicine;
			Admo=data.Admo;
			Trident=data.Trident;
			Points=data.Points;

			GameLevel = data.GameLevel;
			Checkpoint=data.Checkpoint;
			UserLevel=data.UserLevel;


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
		mana=50+15*(2-Dificult);
		Medicine = 2-Dificult;
		GameLevel=1;
		Checkpoint=1;
		Admo=50*(2-Dificult);
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
	public int Medicine;
	public int Admo;
	public bool Trident;
	public int Points;

	public int GameLevel;
	public int Checkpoint;
	public int UserLevel;


	public datasavegame(int Dificult,float health, float mana, int Medicine, int Admo, bool Trident, int Points, int GameLevel,	int Checkpoint, int UserLevel)
	{
		//Variables Savegame
		this.Dificult=Dificult;
		this.health = health;
		this.mana = mana;
		this.Medicine = Medicine;
		this.Admo= Admo;
		this.Trident=Trident;
		this.Points=Points;

		this.GameLevel=GameLevel;
		this.Checkpoint=Checkpoint;
		this.UserLevel=UserLevel;


	}


}