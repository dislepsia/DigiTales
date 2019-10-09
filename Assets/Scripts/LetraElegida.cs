using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LetraElegida : MonoBehaviour {

	public Image imgA;
	public Image imgL;
	public Image imgC;
	public Image imgD;
	public Image imgT;
	public Image imgR;
	public Image imgV;

	void Start () {

		switch (CargarImagenDependiendoDeLetra.objetoEleccion.letra) {

		case "árbol":
			imgA.enabled = true;
			break;

		case "lechuza":
			imgL.enabled = true;
			break;

		case "castillo":
			palabraImagen ("C");
			//imgC.enabled = true;
			break;

		case "durazno":
			imgD.enabled = true;
			break;

		case "tormenta":
			palabraImagen ("T");
			break;

		case "rama":
			imgR.enabled = true;
			break;

		case "vestido":
			imgV.enabled = true;
			break;
		}
	}

	void palabraImagen(string boton){
		GameObject.Find ("Titulo").GetComponent<TextMeshProUGUI> ().text = "Que palabra es?...";
		GameObject.Find ("RespuestaText-" + boton).GetComponent<TextMeshProUGUI> ().enabled = true;
	}
}
