using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class Vibracion : MonoBehaviour {

	public void Dropdown_IndexChange(int indice)
	{
		PlayerPrefs.SetString ("ModoVibracion", indice.ToString());
		PlayerPrefs.Save ();

		if (PlayerPrefs.GetString ("ModoVibracion").Equals ("0"))
			Vibrate ();
	}

	public void Vibrate(){
		Handheld.Vibrate ();
	}

	void Start(){
		if (PlayerPrefs.GetString ("ModoVibracion").Equals ("0")) {
			GameObject.Find ("DropdownVibracion").GetComponent<TMP_Dropdown> ().value = 0;
		} else {
			GameObject.Find ("DropdownVibracion").GetComponent<TMP_Dropdown> ().value = 1;
		}
	}
		
	void Update(){
		if (PlayerPrefs.GetString ("ModoVibracion").Equals ("0")) {
			GameObject.Find ("Icono-Vibrar-Si").GetComponent<Image> ().enabled = true;
			GameObject.Find ("Icono-Vibrar-No").GetComponent<Image> ().enabled = false;

		} else {
			GameObject.Find ("Icono-Vibrar-Si").GetComponent<Image> ().enabled = false;
			GameObject.Find ("Icono-Vibrar-No").GetComponent<Image> ().enabled = true;
		}
	}

}
