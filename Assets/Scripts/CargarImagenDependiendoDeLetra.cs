using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CargarImagenDependiendoDeLetra : MonoBehaviour {

	public static CargarImagenDependiendoDeLetra objetoEleccion = new CargarImagenDependiendoDeLetra();
	public string letra;

	public void LetraA () {
		CargarImagenDependiendoDeLetra.objetoEleccion.letra = "árbol";
	}

	public void LetraB () {
		CargarImagenDependiendoDeLetra.objetoEleccion.letra = "barco";
	}

	public void LetraC () {
		CargarImagenDependiendoDeLetra.objetoEleccion.letra = "castillo";
	}

	public void LetraD () {
		CargarImagenDependiendoDeLetra.objetoEleccion.letra = "durazno";
	}

	public void LetraE () {
		CargarImagenDependiendoDeLetra.objetoEleccion.letra = "elefante";
	}
}
