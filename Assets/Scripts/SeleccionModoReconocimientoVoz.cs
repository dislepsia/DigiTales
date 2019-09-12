using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeleccionModoReconocimientoVoz : MonoBehaviour {

	//public int modo;
	//public static SeleccionModoReconocimientoVoz voz = new SeleccionModoReconocimientoVoz();

	public void Dropdown_IndexChange(int indice)
    {
		//modo = indice;
		PlayerPrefs.SetString ("ModoReconocimiento", indice.ToString());


		if(PlayerPrefs.GetString ("ModoVibracion").Equals("0"))
			Debug.Log("Modo: Tiempo Real");
		else
			Debug.Log("Modo: Palabras Clave");
    }
}
