using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LetraElegida : MonoBehaviour {

	public Image imgA;
	public Image imgB;
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

		case "buho":
			imgB.enabled = true;
			break;

		case "castillo":
			imgC.enabled = true;
			break;

		case "durazno":
			imgD.enabled = true;
			break;

		case "tormenta":
			GameObject.Find ("Titulo").GetComponent<TextMeshProUGUI> ().text = "Que palabra es?...";
			GameObject.Find ("RespuestaText-T").GetComponent<TextMeshProUGUI> ().enabled = true;
			break;

		case "rama":
			imgR.enabled = true;
			break;

		case "vestido":
			imgV.enabled = true;
			break;
		}
	}
}
