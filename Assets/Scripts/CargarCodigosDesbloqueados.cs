using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CargarCodigosDesbloqueados : MonoBehaviour {

	public TMP_InputField entrada;
	public Text salida;
	string[] codigosHabilitados = null;

	public void Guardar () {

		if(entrada.text.Equals("6281"))
			PlayerPrefs.SetString ("Chanchitos", entrada.text + " ");

		if(entrada.text.Equals("7735"))
			PlayerPrefs.SetString ("Caperucita", entrada.text+ " ");

		if(entrada.text.Equals("4467"))
			PlayerPrefs.SetString ("Cenicienta", entrada.text+ " ");
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

		string nombreEscena = SceneManager.GetActiveScene().name;

		//Cargo los códigos para aquellos cuentos que ya fueron ingresados
		if(nombreEscena.Equals("NewListadoCuentos")){
			//Debug.Log("Se cargo la escena: NewListadoCuentos");
		
			codigosHabilitados = GameObject.Find ("LabelCodigos").GetComponent<Text> ().text.Split(' ');
			int codigoErroneo = 0;


			foreach(string codigo in codigosHabilitados){

				if (codigo.Equals("6281")){
					GameObject.Find ("ChanchitosButton").GetComponent<Button> ().interactable = true;
					codigoErroneo = 1;
				}

				if (codigo.Equals ("7735")) {
					GameObject.Find ("CaperucitaButton").GetComponent<Button> ().interactable = true;
					codigoErroneo = 1;
				} 

				if (codigo.Equals ("4467")) {
					GameObject.Find ("CenicientaButton").GetComponent<Button> ().interactable = true;
					codigoErroneo = 1;
				} 

				/*if (codigoErroneo.Equals (1)) {
					contenedorError.SetActive (true);
					Debug.Log ("Entro, codigo erroneo->Mensaje");
				}*/
			}
		}

		//Habilito el botón de Cargar cuando el código ingresado sea de 4 caracteres
		if(nombreEscena.Equals("CargaDeCodigo")){
			
			Button cargaCodigo = GameObject.Find ("CargarButton").GetComponent<Button> ();

			if(entrada.text.Length > 3){
				cargaCodigo.interactable = true;
			}
			else{
				cargaCodigo.interactable = false;
			}
		}
	}
}
