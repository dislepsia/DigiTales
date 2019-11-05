using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour {

	public AudioMixer audioMixer;

	public void SetVolume(float volume){
		audioMixer.SetFloat("volume",volume);
		PlayerPrefs.SetFloat ("Volume", volume);

		//string nombreEscena = SceneManager.GetActiveScene().name;

		//if (nombreEscena.Equals ("Opciones")) {
		
			volume = (-1) * volume;

			if (volume > 55 && volume < 80) {
				GameObject.Find ("Icono-Sonido-Bajo").GetComponent<Image> ().enabled = true;
				GameObject.Find ("Icono-Sonido-Medio").GetComponent<Image> ().enabled = false;
				GameObject.Find ("Icono-Sonido-Alto").GetComponent<Image> ().enabled = false;
			}

			if (volume > 25 && volume < 55) {
				GameObject.Find ("Icono-Sonido-Bajo").GetComponent<Image> ().enabled = false;
				GameObject.Find ("Icono-Sonido-Medio").GetComponent<Image> ().enabled = true;
				GameObject.Find ("Icono-Sonido-Alto").GetComponent<Image> ().enabled = false;
			}

			if (volume > 0 && volume < 25) {
				GameObject.Find ("Icono-Sonido-Bajo").GetComponent<Image> ().enabled = false;
				GameObject.Find ("Icono-Sonido-Medio").GetComponent<Image> ().enabled = false;
				GameObject.Find ("Icono-Sonido-Alto").GetComponent<Image> ().enabled = true;
			}

		//}
	}

	public void Update(){
		string nombreEscena = SceneManager.GetActiveScene().name;
		if (nombreEscena.Equals ("Opciones")) {
			Slider volumen = GameObject.Find ("BarraSonido").GetComponent<Slider> ();
			volumen.value = PlayerPrefs.GetFloat ("Volume");
		} else {
			audioMixer.SetFloat("volume",PlayerPrefs.GetFloat ("Volume"));
			//Debug.Log ("Volumen tiene valor? " + PlayerPrefs.GetFloat ("Volume"));
		}
	}
}