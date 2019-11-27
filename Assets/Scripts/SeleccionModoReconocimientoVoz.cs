﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SeleccionModoReconocimientoVoz : MonoBehaviour {

	public void Dropdown_IndexChange(int indice)
	{
		PlayerPrefs.SetInt ("ModoReconocimiento", indice);
		PlayerPrefs.Save ();
	}

	void Start(){

		if (PlayerPrefs.HasKey ("ModoReconocimiento")) {

			if(PlayerPrefs.GetInt ("ModoReconocimiento") == 0) {
				GameObject.Find ("DropdownReconocimiento").GetComponent<TMP_Dropdown> ().value = 0;

			} else {

				GameObject.Find ("DropdownReconocimiento").GetComponent<TMP_Dropdown> ().value = 1;
			}
		} 

		if (!PlayerPrefs.HasKey ("ModoReconocimiento")) {
			PlayerPrefs.SetInt ("ModoReconocimiento", 0);
			PlayerPrefs.Save ();
		}
	}

	void Update(){
		if(PlayerPrefs.GetInt ("ModoReconocimiento") == 0) {
			GameObject.Find ("Icono-TipoRelato-TiempoReal").GetComponent<Image> ().enabled = true;
			GameObject.Find ("Icono-TipoRelato-PalabrasClave").GetComponent<Image> ().enabled = false;

		} else {
			GameObject.Find ("Icono-TipoRelato-TiempoReal").GetComponent<Image> ().enabled = false;
			GameObject.Find ("Icono-TipoRelato-PalabrasClave").GetComponent<Image> ().enabled = true;
		}
	}
}