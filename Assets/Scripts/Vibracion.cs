using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Vibracion : MonoBehaviour {

	public void Dropdown_IndexChange(int indice)
	{
		PlayerPrefs.SetString ("ModoVibracion", indice.ToString());
		//GameObject.Find ("DropdownVibracion").GetComponent<Dropdown> ().value = indice;

		if(PlayerPrefs.GetString ("ModoVibracion").Equals("0")){
			Debug.Log ("Clases-Vibrar: ON (" + indice + ")");
			Vibrate ();
		}
			else{
			Debug.Log ("Clases-Vibrar: OFF (" + indice + ")");
		}	
	}

	public void Vibrate(){
		Handheld.Vibrate ();
	}

	void Start(){
		//GameObject.Find ("DropdownVibracion").GetComponent<Dropdown> ().value = 
			//int.Parse(PlayerPrefs.GetString ("ModoVibracion").ToString());
	}

	void Update(){
		//GameObject.Find ("DropdownVibracion").GetComponent<Dropdown> ().value = 
			//int.Parse(PlayerPrefs.GetString ("ModoVibracion").ToString());
	}
}
