﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using KKSpeech;

public class ControlarJuegoAprendizaje : MonoBehaviour {

	public Button startRecordingButton;
	public GameObject sceneText;
	public Text resultErrores; //visualizar error

	//variables para trabajar result(reconocimiento parcial de voz)
	private string[] palabrasSpeech = null;

	public Animator microfono;
	public Animator imagenNegra;

	bool coroutineStarted = true;//para freezar ejecucion

	void Start() { 
		SpeechRecognizerListener listener = GameObject.FindObjectOfType<SpeechRecognizerListener>();
		listener.onErrorDuringRecording.AddListener(OnError);//NECESARIO, CUANDO ESTA ACTIVO POR MAS DE 5S TIRA ERROR PARA RESETEARSE LA ESCUCHA
		listener.onFinalResults.AddListener(OnFinalResult);
		listener.onPartialResults.AddListener(OnPartialResult);
		ActivarEscucha ();
	}

	/*RESULTADO FINAL DEL RECONOCIMIENTO DE VOZ*/
	public void OnFinalResult(string result) {		
		//ReiniciarValoresEscena();
		palabrasSpeech = result.ToLower().Split(' ');
		AudioSource respuestaOk;
		switch (palabrasSpeech [0].ToString ().Trim ()) {

		case "árbol":
			if(CargarImagenDependiendoDeLetra.objetoEleccion.letra.Equals("árbol"))
				respuesta ("A", true);
			break;

		case "lechuza":
			if(CargarImagenDependiendoDeLetra.objetoEleccion.letra.Equals("lechuza"))
				respuesta ("L",true);
			break;

		case "castillo":
			if (CargarImagenDependiendoDeLetra.objetoEleccion.letra.Equals ("castillo")) {
				//respuesta ("C",true);					
				DesactivarEscucha ();
				GameObject.Find ("Image-C").GetComponent<Image> ().enabled = true;
				respuestaOk = GameObject.Find ("AudioRespuestaOk").GetComponent<AudioSource> ();
				GameObject.Find ("ImagenPanel").GetComponent<Image> ().color = UnityEngine.Color.green;
				respuestaOk.Play ();
				sceneText.SetActive (true);
				coroutineStarted = false;
			}
			break;

		case "durazno":
			if(CargarImagenDependiendoDeLetra.objetoEleccion.letra.Equals("durazno"))
				respuesta ("D",true);
			else
				respuesta (false);
			break;

		case "tormenta":
			if (CargarImagenDependiendoDeLetra.objetoEleccion.letra.Equals ("castillo")) {
				DesactivarEscucha ();
				GameObject.Find ("Image-T").GetComponent<Image> ().enabled = true;
				respuestaOk = GameObject.Find ("AudioRespuestaOk").GetComponent<AudioSource> ();
				GameObject.Find ("ImagenPanel").GetComponent<Image> ().color = UnityEngine.Color.green;
				respuestaOk.Play ();
				sceneText.SetActive (true);
				coroutineStarted = false;
			}
			break;

		case "rama":
			if(CargarImagenDependiendoDeLetra.objetoEleccion.letra.Equals("rama"))
				respuesta ("R", true);
			break;

		case "vestido":
			if(CargarImagenDependiendoDeLetra.objetoEleccion.letra.Equals("vestido"))
				respuesta ("V",true);
			break;

		default:
			//respuesta (false);
			break;
		}			
	}

	/*RESULTADO PARCIAL DEL RECONOCIMIENTO DE VOZ*/
	public void OnPartialResult(string result) {
	}
		
	void respuesta(string respuesta, bool audio){
		DesactivarEscucha ();

		if (audio.Equals (true)) {

			GameObject.Find ("RespuestaText-A").GetComponent<TextMeshProUGUI> ().enabled = false;
			GameObject.Find ("RespuestaText-L").GetComponent<TextMeshProUGUI> ().enabled = false;
			GameObject.Find ("RespuestaText-C").GetComponent<TextMeshProUGUI> ().enabled = false;
			GameObject.Find ("RespuestaText-D").GetComponent<TextMeshProUGUI> ().enabled = false;
			GameObject.Find ("RespuestaText-R").GetComponent<TextMeshProUGUI> ().enabled = false;
			GameObject.Find ("RespuestaText-V").GetComponent<TextMeshProUGUI> ().enabled = false;
			GameObject.Find ("RespuestaText-T").GetComponent<TextMeshProUGUI> ().enabled = false;

			GameObject.Find ("RespuestaText-" + respuesta).GetComponent<TextMeshProUGUI> ().enabled = true;

			GameObject.Find ("RespuestaPanel").GetComponent<Image> ().color = UnityEngine.Color.green;
			AudioSource respuestaOk = GameObject.Find ("AudioRespuestaOk").GetComponent<AudioSource> ();
			respuestaOk.Play ();
		} 

		sceneText.SetActive (true);
		coroutineStarted = false;//para freezar ejecucion	
	}

	void respuesta(bool respuesta){
		DesactivarEscucha ();
		GameObject.Find ("RespuestaText-Error-Panel-Texto").GetComponent<Image> ().enabled = true;
		GameObject.Find ("RespuestaText-Error-Panel-Imagen").GetComponent<Image> ().enabled = true;

		GameObject.Find ("Imagen-A").GetComponent<Image> ().enabled = false;
		GameObject.Find ("Imagen-L").GetComponent<Image> ().enabled = false;
		GameObject.Find ("Imagen-C").GetComponent<Image> ().enabled = false;
		GameObject.Find ("Imagen-D").GetComponent<Image> ().enabled = false;
		GameObject.Find ("Imagen-R").GetComponent<Image> ().enabled = false;
		GameObject.Find ("Imagen-T").GetComponent<Image> ().enabled = false;
		GameObject.Find ("Imagen-V").GetComponent<Image> ().enabled = false;

		GameObject.Find ("RespuestaText-A").GetComponent<TextMeshProUGUI> ().enabled = false;
		GameObject.Find ("RespuestaText-L").GetComponent<TextMeshProUGUI> ().enabled = false;
		GameObject.Find ("RespuestaText-C").GetComponent<TextMeshProUGUI> ().enabled = false;
		GameObject.Find ("RespuestaText-D").GetComponent<TextMeshProUGUI> ().enabled = false;
		GameObject.Find ("RespuestaText-R").GetComponent<TextMeshProUGUI> ().enabled = false;
		GameObject.Find ("RespuestaText-V").GetComponent<TextMeshProUGUI> ().enabled = false;
		GameObject.Find ("RespuestaText-T").GetComponent<TextMeshProUGUI> ().enabled = false;

		AudioSource respuestaError = GameObject.Find ("AudioRespuestaError").GetComponent<AudioSource> ();
		respuestaError.Play ();
		sceneText.SetActive (true);
		coroutineStarted = false;
	}

	public void OnError(string error) {
		DesactivarEscucha();
	}

	public void OnStartRecordingPressed() {
		if (SpeechRecognizer.IsRecording()) {
			DesactivarEscucha();
		} else {			
			ActivarEscucha ();
		}
	}

	public void ReiniciarValoresEscena() {			
			startRecordingButton.gameObject.SetActive(true);
			microfono.gameObject.SetActive(false);	
	}
			
	void Update(){
		if (!coroutineStarted)
			StartCoroutine (EsperarSegundos (2));
	}  
		
	IEnumerator EsperarSegundos(int seconds){
			coroutineStarted = true;
			yield return new WaitForSeconds (seconds);

			StartCoroutine (SpriteFadeOut ());
			StopCoroutine ("SpriteFadeOut");
		SceneManager.LoadScene ("MiniJuego-NenaTemerosa-Letras");
	}

	IEnumerator SpriteFadeOut()	{		
		imagenNegra.SetTrigger ("end");
		yield return new WaitForSeconds(1f);
	}

	public void ActivarEscucha() {	
		startRecordingButton.gameObject.SetActive(false);
		microfono.gameObject.SetActive(true);
		SpeechRecognizer.StartRecording(true);
	}

	public void DesactivarEscucha() {	
		SpeechRecognizer.StopIfRecording ();
		startRecordingButton.gameObject.SetActive(true);
		microfono.gameObject.SetActive(false);
	}

	public void Vibrar(){
			Handheld.Vibrate ();
		}
}
