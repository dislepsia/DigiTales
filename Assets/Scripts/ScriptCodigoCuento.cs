using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScriptCodigoCuento : MonoBehaviour {

	public void Update(){
		//Transferencia del código ingresado a la escena del listado de cuentos
		HabilitarCuento.objetoHabilitar.codigo = GameObject.Find ("TextMeshPro - InputField").GetComponent<TMP_InputField> ().text;
	}
}