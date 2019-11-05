using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackCatcher : MonoBehaviour {

	void Update () {
		if (Application.platform == RuntimePlatform.Android) {
			if (Input.GetKeyUp (KeyCode.Escape)) {
				ControlarBotones.Salir_Atras_Boton_Cel();
			}
		}
	}
}
