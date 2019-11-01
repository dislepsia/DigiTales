using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CuentoElegido : MonoBehaviour {

	void Start () {

		switch (CargarPantallaDeCuento.objetoEleccion.cuento) {

		case "nena":
			SceneManager.LoadScene("Cuento1Escena1");
			break;

		case "chanchitos":
			SceneManager.LoadScene("Cuento2Escena1");
			break;
		}
	}
}
