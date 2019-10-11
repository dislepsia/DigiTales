using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LetraElegida : MonoBehaviour {

	public Image imgF;
	public Image imgC;
	public Image imgP;
	public Image imgL;
	public Image imgT;
	public Image imgR;
	public Image imgV;

	void Start () {

		switch (CargarImagenDependiendoDeLetra.objetoEleccion.letra) {

		case "fantasma":
			palabraImagen ("F");
			break;

		case "lechuza":
			imgL.enabled = true;
			break;

		case "castillo":
			palabraImagen ("C");
			break;

		case "pegaso":
			imgP.enabled = true;
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
