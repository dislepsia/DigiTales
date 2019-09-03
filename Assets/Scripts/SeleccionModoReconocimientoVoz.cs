using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeleccionModoReconocimientoVoz : MonoBehaviour {

	public static int index;

	public void Dropdown_IndexChange(int indice)
    {
		index = indice;

		if(indice == 0)
			Debug.Log("Modo: Tiempo Real");
		else
			Debug.Log("Modo: Palabras Clave");
    }
}
