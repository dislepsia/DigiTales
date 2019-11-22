using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using UnityEngine.SceneManagement;

public class Vibracion : MonoBehaviour {

	public void Dropdown_IndexChange(int indice)
	{
		PlayerPrefs.SetInt ("ModoVibracion", indice);
		PlayerPrefs.Save ();

		if (PlayerPrefs.GetInt ("ModoVibracion") == 0)
			Vibrate ();
	}

	public void Vibrate(){
		Handheld.Vibrate ();
	}

	void Start(){

		string nombreEscena = SceneManager.GetActiveScene().name;

		if (nombreEscena.Equals ("Opciones")) {

			if (PlayerPrefs.HasKey ("ModoVibracion")) {
				if (PlayerPrefs.GetInt ("ModoVibracion") == 0) {
					GameObject.Find ("DropdownVibracion").GetComponent<TMP_Dropdown> ().value = 0;
				} else {
					GameObject.Find ("DropdownVibracion").GetComponent<TMP_Dropdown> ().value = 1;
				}
			} 

			if (!PlayerPrefs.HasKey ("ModoVibracion")){
				PlayerPrefs.SetInt ("ModoVibracion", 0);
				PlayerPrefs.Save ();
			}
		}
	}
		
	void Update(){
		
		string nombreEscena = SceneManager.GetActiveScene().name;

		if (nombreEscena.Equals ("Opciones")) {

			if (PlayerPrefs.GetInt ("ModoVibracion") == 0) {
				GameObject.Find ("Icono-Vibrar-Si").GetComponent<Image> ().enabled = true;
				GameObject.Find ("Icono-Vibrar-No").GetComponent<Image> ().enabled = false;

			} else {
				GameObject.Find ("Icono-Vibrar-Si").GetComponent<Image> ().enabled = false;
				GameObject.Find ("Icono-Vibrar-No").GetComponent<Image> ().enabled = true;
			}
		}
	}

}
