using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LetraElegida : MonoBehaviour {

	public Image imgA;
	public Image imgB;

	void Start () {
		//GameObject.Find ("TextLetra").GetComponent<Text> ().text = 
		if (CargarImagenDependiendoDeLetra.objetoEleccion.letra.Equals ("árbol")) {
			imgA.enabled = true;
		}

		if (CargarImagenDependiendoDeLetra.objetoEleccion.letra.Equals ("barco")) {
			imgB.enabled = true;
		}
	}
		
}
