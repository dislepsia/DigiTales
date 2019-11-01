using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundSoundPause : MonoBehaviour {

	public static int marca = 0;

	void Start () {
		string nombreEscena = SceneManager.GetActiveScene().name;
		if (
			nombreEscena.Equals ("Cuento1Escena1") ||
			nombreEscena.Equals ("Cuento1Escena2") ||
			nombreEscena.Equals ("Cuento1Escena3") ||
			nombreEscena.Equals ("Cuento1Escena4") ||
			nombreEscena.Equals ("Cuento1Escena5") ||
			nombreEscena.Equals ("Cuento1Escena6") ||
			nombreEscena.Equals ("Cuento2Escena1")) {

			BackgroundSound.Instance.gameObject.GetComponent<AudioSource> ().Pause ();
			marca = 1;
			//Debug.Log ("Pause: " + nombreEscena);
		}

		if (nombreEscena.Equals ("MiniJuego-NenaTemerosa-Modo")) {
			//Debug.Log ("Valor Marca: " + marca);
			if (marca.Equals(1)) {
				BackgroundSound.Instance.gameObject.GetComponent<AudioSource> ().Play ();
				//Debug.Log ("Play: " + nombreEscena);
			} 
		}
	}
}
