using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CargarCodigosDesbloqueados : MonoBehaviour {

	public TMP_InputField entrada;
	public Text salida;
	string codigosAnteriores = "";

	public void Guardar () {
		PlayerPrefs.SetString ("CodigoCuento", entrada.text);
	}
	
	// Update is called once per frame
	void Start () {
		salida.text = PlayerPrefs.GetString ("CodigoCuento");

	}
		
}
