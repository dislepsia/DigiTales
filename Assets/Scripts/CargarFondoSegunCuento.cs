using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CargarFondoSegunCuento : MonoBehaviour {

	void Start () {
		switch (CargarPantallaDeCuento.objetoEleccion.cuento) {

		case "nena":
			GameObject.Find ("PanelNena").GetComponent<Image> ().enabled = true;
			break;

		case "chanchitos":
			GameObject.Find ("PanelChanchito").GetComponent<Image> ().enabled = true;
			break;
		}  
	}
}
