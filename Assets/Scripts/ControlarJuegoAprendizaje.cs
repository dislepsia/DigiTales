using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using KKSpeech;

public class ControlarJuegoAprendizaje : MonoBehaviour {

	public Button startRecordingButton;
	public GameObject sceneText;
	public Text resultErrores; 
	public Image ErrorPanelTexto;
	public Image ErrorPanelImagen;

	private string[] palabrasSpeech = null;

	public Animator microfono;
	public Animator imagenNegra;

	bool coroutineStarted = true;

	void Start() { 
		SpeechRecognizerListener listener = GameObject.FindObjectOfType<SpeechRecognizerListener>();
		listener.onErrorDuringRecording.AddListener(OnError);//NECESARIO, CUANDO ESTA ACTIVO POR MAS DE 5S TIRA ERROR PARA RESETEARSE LA ESCUCHA
		listener.onFinalResults.AddListener(OnFinalResult);
		listener.onPartialResults.AddListener(OnPartialResult);
		ActivarEscucha ();
	}
		
	public void OnPartialResult(string result) {		
		
		palabrasSpeech = result.ToLower().Split(' ');
		string reconocimiento = palabrasSpeech [0].ToString ().Trim ();

		switch (reconocimiento) {

		case "fantasma":
			if (CargarImagenDependiendoDeLetra.objetoEleccion.letra.Equals ("fantasma"))
				rtaOkImagen ("F");
			else
				rtaErrorImagen ();
			break;

		case "lechuza":
			if (CargarImagenDependiendoDeLetra.objetoEleccion.letra.Equals ("lechuza"))
				rtaOkPalabra ("L");
			else
				rtaErrorPalabra ();
			break;

		case "castillo":
			if (CargarImagenDependiendoDeLetra.objetoEleccion.letra.Equals ("castillo")) 
				rtaOkImagen ("C");
			else
				rtaErrorImagen ();
			break;

		case "pegaso":
			if (CargarImagenDependiendoDeLetra.objetoEleccion.letra.Equals ("pegaso"))
				rtaOkPalabra ("P");
			else
				rtaErrorPalabra ();
			break;

		case "tormenta":
			if (CargarImagenDependiendoDeLetra.objetoEleccion.letra.Equals ("tormenta"))
				rtaOkImagen ("T");
			else
				rtaErrorImagen ();
			break;

		case "rama":
			if (CargarImagenDependiendoDeLetra.objetoEleccion.letra.Equals ("rama"))
				rtaOkPalabra ("R");
			else
				rtaErrorPalabra ();
			break;

		case "vestido":
			if (CargarImagenDependiendoDeLetra.objetoEleccion.letra.Equals ("vestido"))
				rtaOkPalabra ("V");
			else
				rtaErrorPalabra ();
			break;
		
		default:
			ErrorNoDisponible ();
			break;
		}
	}

	public void OnFinalResult(string result) {
	}

	void ErrorNoDisponible(){
		if (CargarImagenDependiendoDeLetra.objetoEleccion.letra.Equals ("fantasma") ||
		   CargarImagenDependiendoDeLetra.objetoEleccion.letra.Equals ("castillo") ||
		   CargarImagenDependiendoDeLetra.objetoEleccion.letra.Equals ("tormenta")) {
			rtaErrorImagen ();
		}
		else
			rtaErrorPalabra ();
	}

	void rtaOkPalabra(string respuesta){

		DesactivarEscucha ();

		//Imagen
		GameObject.Find ("RespuestaText-" + respuesta).GetComponent<TextMeshProUGUI> ().enabled = true;
		//Panel
		GameObject.Find ("RespuestaPanel").GetComponent<Image> ().color = UnityEngine.Color.green;
		//Audio
		AudioSource respuestaOk = GameObject.Find ("AudioRespuestaOk").GetComponent<AudioSource> ();
		respuestaOk.Play ();

		sceneText.SetActive (true);
		coroutineStarted = true;//para freezar ejecucion	
	}

	void rtaErrorPalabra(){
		
		DesactivarEscucha ();

		ErrorPanelTexto.enabled = true;
		GameObject.Find ("RespuestaText-Error-Panel-Texto").GetComponent<Image> ().enabled = true;

		AudioSource respuestaError = GameObject.Find ("AudioRespuestaError").GetComponent<AudioSource> ();
		respuestaError.Play ();

		sceneText.SetActive (true);
		coroutineStarted = true;
	}

	void rtaOkImagen(string respuesta){

		DesactivarEscucha ();

		GameObject.Find ("Image-" + respuesta).GetComponent<Image> ().enabled = true;
		GameObject.Find ("ImagenPanel").GetComponent<Image> ().color = UnityEngine.Color.green;

		AudioSource respuestaOk = GameObject.Find ("AudioRespuestaOk").GetComponent<AudioSource> ();
		respuestaOk.Play ();

		sceneText.SetActive (true);
		coroutineStarted = true;
	}

	void rtaErrorImagen(){

		DesactivarEscucha ();

		ErrorPanelImagen.enabled = true;
		GameObject.Find ("RespuestaText-Error-Panel-Imagen").GetComponent<Image> ().enabled = true;
		GameObject.Find ("ImagenPanel").GetComponent<Image> ().color = UnityEngine.Color.red;

		AudioSource respuestaError = GameObject.Find ("AudioRespuestaError").GetComponent<AudioSource> ();
		respuestaError.Play ();

		sceneText.SetActive (true);
		coroutineStarted = true;
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
