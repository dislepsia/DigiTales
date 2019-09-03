using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ArmarJson : MonoBehaviour {

	string filePath;
	string jsonString;

	void Awake () {
		filePath = Application.dataPath + "/CodigosGuardados.json";
		jsonString = File.ReadAllText (filePath);
		Codigo lista = JsonUtility.FromJson<Codigo> (jsonString);
		print (lista);
	}

	void Update () {
		
	}
}

[System.Serializable]
public class Codigo{
	public string chanchitos;
	public string caperucita;
	public string blancanieves;
	public string rapuncel;
}