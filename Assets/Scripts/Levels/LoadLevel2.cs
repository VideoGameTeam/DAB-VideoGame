﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadLevel2 : MonoBehaviour {
	void OnTriggerExit2D(Collider2D objeto){
		GameObject.Find ("PlayerStatus").SendMessage ("FinishLevel");
	}
}
