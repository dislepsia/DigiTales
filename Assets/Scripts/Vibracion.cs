using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Vibracion : MonoBehaviour {

	//public static string modo = 1;
	//public static bool bandera = true;

	public void Dropdown_IndexChange(int indice)
	{
		//modo = indice;
		//bandera = false;
		PlayerPrefs.SetString ("ModoVibracion", indice.ToString());

		if(PlayerPrefs.GetString("ModoVibracion").Equals("0")){
			Vibrate ();
		}
	}

	public void Vibrate(){
		Handheld.Vibrate ();
	}

	void Start(){
		//if (bandera.Equals (false)) {
		//	GameObject.Find ("DropdownVibracion").GetComponent<Dropdown> ().value = int.Parse(PlayerPrefs.GetString ("ModoVibracion"));	
		//}
	}

	void Update(){
		//modo = GameObject.Find ("DropdownVibracion").GetComponent<Dropdown> ();
		//modo.value = int.Parse (PlayerPrefs.GetString ("ModoVibracion").ToString ());
	}
}
