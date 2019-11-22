using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SeleccionModoReconocimientoVoz : MonoBehaviour {

	public void Dropdown_IndexChange(int indice)
    {
		PlayerPrefs.SetInt ("ModoReconocimiento", indice);
		PlayerPrefs.Save ();

		if(PlayerPrefs.GetInt ("ModoReconocimiento") == 0)
			Debug.Log("Modo: Tiempo Real-ValorIndice: " +indice.ToString());
		else
			Debug.Log("Modo: Palabras Clave-ValorIndice: " +indice.ToString());
    }

	void Start(){
		Debug.Log("Start");
		if (PlayerPrefs.HasKey ("ModoReconocimiento")) {
			Debug.Log("Entro HasKey");
			if(PlayerPrefs.GetInt ("ModoReconocimiento") == 0) {
				GameObject.Find ("DropdownReconocimiento").GetComponent<TMP_Dropdown> ().value = 0;
				Debug.Log("Modo 0");
			} else {
				Debug.Log("Modo 1");
				GameObject.Find ("DropdownReconocimiento").GetComponent<TMP_Dropdown> ().value = 1;
			}
		} 

		if (!PlayerPrefs.HasKey ("ModoReconocimiento")) {
			Debug.Log("HasKey En falso");
			PlayerPrefs.SetInt ("ModoReconocimiento", 0);
			PlayerPrefs.Save ();
			Debug.Log("SetHaskey");
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
