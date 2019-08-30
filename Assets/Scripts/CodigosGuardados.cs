using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class CodigosGuardados : MonoBehaviour {

	public static string codigosHabilitados = "";
	private string rutaArchivo;

	void Awake () {
		Debug.Log (Application.persistentDataPath);
		rutaArchivo = Application.persistentDataPath + "/datos.dat";
	}

	void Start () {
		Cargar ();
	}

	public void Guardar () {
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (rutaArchivo);

		GuardarDatos codigos = new GuardarDatos ();
		codigos.habilitados = codigos.ToString() + "-";

		bf.Serialize (file, codigos);
		file.Close();
		Debug.Log ("CodigosHabilitados: "+codigosHabilitados);
		Debug.Log ("Guardar:" +codigosHabilitados);
	}

	// Update is called once per frame
	void Cargar () {
		if(File.Exists(rutaArchivo)){

		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Open (rutaArchivo, FileMode.Open);

		GuardarDatos codigos = (GuardarDatos)bf.Deserialize (file);
		codigosHabilitados= codigos.habilitados;
		file.Close();
		Debug.Log ("Cargar:"+codigosHabilitados);
		}
	}
}

[Serializable]
class GuardarDatos{

	public string habilitados;

}