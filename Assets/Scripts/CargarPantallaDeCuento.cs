using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CargarPantallaDeCuento : MonoBehaviour {

	public static CargarPantallaDeCuento objetoEleccion = new CargarPantallaDeCuento();
	public string cuento;

	public void Nena () {
		CargarPantallaDeCuento.objetoEleccion.cuento = "nena";
	}

	public void Chanchitos () {
		CargarPantallaDeCuento.objetoEleccion.cuento = "chanchitos";
	}

	public void Caperucita () {
		CargarPantallaDeCuento.objetoEleccion.cuento = "caperucita";
	}

	public void Cenicienta () {
		CargarPantallaDeCuento.objetoEleccion.cuento = "cenicienta";
	}
}