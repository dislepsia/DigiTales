using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using KKSpeech;
using UnityEngine.UI;

public class Inicio : MonoBehaviour {

	public Text resultErrores;
	public GameObject contenedor;

	void Start()
	{	
		Screen.orientation = ScreenOrientation.Portrait;	
		StartCoroutine (SpriteShapeOut());
		StopCoroutine ("SpriteShapeOut");

		//Inicializo modo relato
		if (!PlayerPrefs.HasKey ("ModoReconocimiento")) {
			PlayerPrefs.SetInt ("ModoReconocimiento", 0);
		} 

		if (!PlayerPrefs.HasKey ("ModoVibracion")) {
			PlayerPrefs.SetInt ("ModoVibracion", 1);
		} 

		PlayerPrefs.Save ();
	}  

	IEnumerator SpriteShapeOut()
	{	
		SystemLanguage lenguaje = Application.systemLanguage;

		yield return new WaitForSeconds(3f);

		//COMPRUEBA COMPATIBILIDAD DE DISPOSITIVO
		if (SpeechRecognizer.ExistsOnDevice()) {
			if (lenguaje.ToString() == "Spanish") 
				SceneManager.LoadScene("NewMenu");
			else {			
				resultErrores.text = "DEBE HABILITAR EL IDIOMA ESPAÑOL EN SU DISPOSITIVO";
				contenedor.SetActive (true);
			}
		} else {			
			resultErrores.text = "SU DISPOSITIVO NO ES COMPATIBLE CON LA APLICACION";
			contenedor.SetActive (true);
		}

	}
}
