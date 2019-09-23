using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using KKSpeech;
using UnityEngine.UI;

public class Inicio : MonoBehaviour {

	public Text resultErrores; //visualizar error

	void Start()
	{		
		StartCoroutine (SpriteShapeOut());
		StopCoroutine ("SpriteShapeOut");

		//inicializo modo relato
		PlayerPrefs.SetString ("ModoReconocimiento", "0");		
		PlayerPrefs.SetString ("ModoVibracion", "1");	
	}  

	IEnumerator SpriteShapeOut()
	{	
		yield return new WaitForSeconds(3f);

		//COMPRUEBA COMPATIBILIDAD DE DISPOSITIVO
		if (SpeechRecognizer.ExistsOnDevice()) {
			SceneManager.LoadScene("NewMenu");
		} else {			
			resultErrores.text = "SU DISPOSITIVO NO ES COMPATIBLE CON LA APLICACION";
		}

	}
}
