using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CargarImagenDependiendoDeLetra : MonoBehaviour {

	public static CargarImagenDependiendoDeLetra objetoEleccion = new CargarImagenDependiendoDeLetra();
	public string letra;

	public void LetraF () {
		CargarImagenDependiendoDeLetra.objetoEleccion.letra = "fantasma";
	}

	public void LetraL () {
		CargarImagenDependiendoDeLetra.objetoEleccion.letra = "lechuza";
	}

	public void LetraC () {
		CargarImagenDependiendoDeLetra.objetoEleccion.letra = "castillo";
	}

	public void LetraP () {
		CargarImagenDependiendoDeLetra.objetoEleccion.letra = "pegaso";
	}

	public void LetraT () {
		CargarImagenDependiendoDeLetra.objetoEleccion.letra = "tormenta";
	}

	public void LetraR () {
		CargarImagenDependiendoDeLetra.objetoEleccion.letra = "rama";
	}

	public void LetraV () {
		CargarImagenDependiendoDeLetra.objetoEleccion.letra = "vestido";
	}
}
