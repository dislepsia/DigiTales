using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CargarCodigosDesbloqueados : MonoBehaviour {

	public TMP_InputField entrada;
	public Text salida;
	public int pantalla = 0;

	public void Guardar () {

		if(entrada.text.Equals("6281"))
			PlayerPrefs.SetString ("Chanchitos", entrada.text + " ");

		if(entrada.text.Equals("7735"))
			PlayerPrefs.SetString ("Caperucita", entrada.text+ " ");

		if(entrada.text.Equals("4467"))
			PlayerPrefs.SetString ("Cenicienta", entrada.text+ " ");

		pantalla = 1;
	}

	void Start () {

		if(PlayerPrefs.GetString ("Chanchitos").Length > 0){
			salida.text += PlayerPrefs.GetString ("Chanchitos");
		}

			if(PlayerPrefs.GetString ("Caperucita").Length > 0){
			salida.text += PlayerPrefs.GetString ("Caperucita");
		}

		if(PlayerPrefs.GetString ("Cenicienta").Length > 0){
			salida.text += PlayerPrefs.GetString ("Cenicienta");
		}
	}

	//Habilitar botón para la carga de código sólo cuando haya 4 caracteres
	void Update(){

		if(pantalla.Equals(0)){

			string[] codigosHabilitados = GameObject.Find ("Text").GetComponent<Text> ().text.Split(' ');

			foreach(string codigo in codigosHabilitados){

				if (codigo.Equals("6281")){
					GameObject.Find ("ChanchitosButton").GetComponent<Button> ().interactable = true;
				}

				if (codigo.Equals ("7735")) {
					GameObject.Find ("CaperucitaButton").GetComponent<Button> ().interactable = true;
				} 

				if (codigo.Equals ("4467")) {
					GameObject.Find ("CenicientaButton").GetComponent<Button> ().interactable = true;
				} 

			}


		}
		else
			pantalla = 2;

		if (pantalla.Equals(1)){

			Button cargaCodigo = GameObject.Find ("CargarButton").GetComponent<Button> ();

			if(entrada.text.Length>3){
				cargaCodigo.interactable = true;
			}
			else{
				cargaCodigo.interactable = false;
			}

		}

	}
}
