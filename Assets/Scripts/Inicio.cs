using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using KKSpeech;
using UnityEngine.UI;

//CONTROLA VARIABLES DE INICIO Y COMPATIBILIDAD
public class Inicio : MonoBehaviour 
{
	public Text resultErrores;
	public GameObject contenedor;

	void Start()
	{	
		Screen.orientation = ScreenOrientation.Portrait;	
		StartCoroutine (SpriteShapeOut());
		StopCoroutine ("SpriteShapeOut");

		//INICIALIZO MODO RELATO
		if (!PlayerPrefs.HasKey ("ModoReconocimiento")) 
		{
			PlayerPrefs.SetInt ("ModoReconocimiento", 0);
		} 

		//INICIALIZO VIBRACION
		if (!PlayerPrefs.HasKey ("ModoVibracion")) 
		{
			PlayerPrefs.SetInt ("ModoVibracion", 1);
		} 

		PlayerPrefs.Save ();
	}  

	IEnumerator SpriteShapeOut()
	{	
		SystemLanguage lenguaje = Application.systemLanguage;

		yield return new WaitForSeconds(3f);

		//COMPRUEBA COMPATIBILIDAD DE DISPOSITIVO
		if (SpeechRecognizer.ExistsOnDevice()) 
		{
			//VERIFICA LENGUAJE ACTIVO DEL DISPOSITIVO
			if (lenguaje.ToString() == "Spanish") 
				SceneManager.LoadScene("NewMenu");
			else 
			{			
				resultErrores.text = "DEBE HABILITAR EL IDIOMA ESPAÑOL EN SU DISPOSITIVO";
				contenedor.SetActive (true);
			}
		} 
		else 
		{			
			resultErrores.text = "SU DISPOSITIVO NO ES COMPATIBLE CON LA APLICACION";
			contenedor.SetActive (true);
		}
	}
}
