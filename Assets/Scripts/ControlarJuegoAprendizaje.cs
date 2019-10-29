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
		//OnFinalResult	
	}

	public void OnFinalResult(string result) {
		palabrasSpeech = result.ToLower().Split(' ');
		string reconocimiento = palabrasSpeech [0].ToString ().Trim ();
		int bandera = 0;
		int marca = 1;

		GameObject.Find ("Resultado").GetComponent<Text> ().text = reconocimiento;

		switch (reconocimiento) {

		case "castillo":
			if (CargarImagenDependiendoDeLetra.objetoEleccion.letra.Equals ("castillo")) {
				bandera = -1;
				rtaOkImagen ("C");
			}

			bandera++;

			if (CargarImagenDependiendoDeLetra.objetoEleccion.letra.Equals ("lechuza") ||
				CargarImagenDependiendoDeLetra.objetoEleccion.letra.Equals ("pegaso") ||
				CargarImagenDependiendoDeLetra.objetoEleccion.letra.Equals ("rama") ||
				CargarImagenDependiendoDeLetra.objetoEleccion.letra.Equals ("vestido")) {
				rtaErrorPalabra ();
			} 

			bandera++;

			if (bandera.Equals (2)) {
				rtaErrorImagen ();
			}

			bandera = 0;
			marca = 0;
			break;

		case "fantasma":
			if (CargarImagenDependiendoDeLetra.objetoEleccion.letra.Equals ("fantasma")) {
				bandera = -1;
				rtaOkImagen ("F");
			}

			bandera++;

			if (CargarImagenDependiendoDeLetra.objetoEleccion.letra.Equals ("lechuza") ||
				CargarImagenDependiendoDeLetra.objetoEleccion.letra.Equals ("pegaso") ||
				CargarImagenDependiendoDeLetra.objetoEleccion.letra.Equals ("rama") ||
				CargarImagenDependiendoDeLetra.objetoEleccion.letra.Equals ("vestido")) {
				rtaErrorPalabra ();
			}

			bandera++;

			if(bandera.Equals(2)) {
				rtaErrorImagen ();
			}

			bandera = 0;
			marca = 0;
			break;

		case "lechuza":
			if (CargarImagenDependiendoDeLetra.objetoEleccion.letra.Equals ("lechuza")) {
				bandera = -1;
				rtaOkPalabra ("L");
			}

			bandera++;

			if (CargarImagenDependiendoDeLetra.objetoEleccion.letra.Equals ("castillo") ||
				CargarImagenDependiendoDeLetra.objetoEleccion.letra.Equals ("fantasma")||
				CargarImagenDependiendoDeLetra.objetoEleccion.letra.Equals ("tormenta")) {
				rtaErrorImagen ();
			} 

			bandera++;

			if(bandera.Equals(2)) {
				rtaErrorPalabra ();
			}

			bandera = 0;
			marca = 0;
			break;

		case "pegaso":
			if (CargarImagenDependiendoDeLetra.objetoEleccion.letra.Equals ("pegaso")) {
				bandera = -1;
				rtaOkPalabra ("P");
			}

			bandera++;

			if (CargarImagenDependiendoDeLetra.objetoEleccion.letra.Equals ("castillo") ||
				CargarImagenDependiendoDeLetra.objetoEleccion.letra.Equals ("fantasma") ||
				CargarImagenDependiendoDeLetra.objetoEleccion.letra.Equals ("tormenta")) {
				rtaErrorImagen ();
			} 

			bandera++;

			if(bandera.Equals(2)) {
				rtaErrorPalabra ();
			}

			bandera = 0;
			marca = 0;
			break;

		case "tormenta":
			if (CargarImagenDependiendoDeLetra.objetoEleccion.letra.Equals ("tormenta")) {
				bandera = -1;
				rtaOkImagen ("T");
			}

			bandera++;

			if (CargarImagenDependiendoDeLetra.objetoEleccion.letra.Equals ("lechuza") ||
				CargarImagenDependiendoDeLetra.objetoEleccion.letra.Equals ("pegaso") ||
				CargarImagenDependiendoDeLetra.objetoEleccion.letra.Equals ("rama") ||
				CargarImagenDependiendoDeLetra.objetoEleccion.letra.Equals ("vestido")) {
				rtaErrorPalabra ();
			} 

			bandera++;

			if(bandera.Equals(2)) {
				rtaErrorImagen ();
			}

			bandera = 0;
			marca = 0;
			break;

		case "rama":
			if (CargarImagenDependiendoDeLetra.objetoEleccion.letra.Equals ("rama")) {
				bandera = -1;
				rtaOkPalabra ("R");
			}

			bandera++;

			if (CargarImagenDependiendoDeLetra.objetoEleccion.letra.Equals ("castillo") ||
				CargarImagenDependiendoDeLetra.objetoEleccion.letra.Equals ("fantasma") ||
				CargarImagenDependiendoDeLetra.objetoEleccion.letra.Equals ("tormenta")) {
				rtaErrorImagen ();
			} 

			bandera++;

			if(bandera.Equals(2)) {
				rtaErrorPalabra ();
			}

			bandera = 0;
			marca = 0;
			break;

		case "vestido":
			if (CargarImagenDependiendoDeLetra.objetoEleccion.letra.Equals ("vestido")) {
				bandera = -1;
				rtaOkPalabra ("V");
			}

			bandera++;

			if (CargarImagenDependiendoDeLetra.objetoEleccion.letra.Equals ("castillo") ||
				CargarImagenDependiendoDeLetra.objetoEleccion.letra.Equals ("fantasma") ||
				CargarImagenDependiendoDeLetra.objetoEleccion.letra.Equals ("tormenta")) {
				rtaErrorImagen ();
			}

			bandera++;

			if(bandera.Equals(2)) {
				rtaErrorPalabra ();
			}

			bandera = 0;
			marca = 0;
			break;
		}

		ErrorNoDisponible (marca);
	}

	void ErrorNoDisponible(int marca){

		if(marca == 1){
			if (CargarImagenDependiendoDeLetra.objetoEleccion.letra.Equals ("fantasma") ||
				CargarImagenDependiendoDeLetra.objetoEleccion.letra.Equals ("castillo") ||
				CargarImagenDependiendoDeLetra.objetoEleccion.letra.Equals ("tormenta")) {
				rtaErrorImagen ();
			}

			if (CargarImagenDependiendoDeLetra.objetoEleccion.letra.Equals ("lechuza") ||
				CargarImagenDependiendoDeLetra.objetoEleccion.letra.Equals ("pegaso") ||
				CargarImagenDependiendoDeLetra.objetoEleccion.letra.Equals ("rama") ||
				CargarImagenDependiendoDeLetra.objetoEleccion.letra.Equals ("vestido")) {
				rtaErrorPalabra ();
			}
		}
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
		coroutineStarted = false;//para freezar ejecucion	
	}

	void rtaErrorPalabra(){
		
		DesactivarEscucha ();

		ErrorPanelTexto.enabled = true;
		GameObject.Find ("RespuestaText-Error-Panel-Texto").GetComponent<Image> ().enabled = true;

		AudioSource respuestaError = GameObject.Find ("AudioRespuestaIncorrecta").GetComponent<AudioSource> ();
		respuestaError.Play ();

		sceneText.SetActive (true);
		coroutineStarted = false;
	}

	void rtaOkImagen(string respuesta){

		DesactivarEscucha ();

		GameObject.Find ("Image-" + respuesta).GetComponent<Image> ().enabled = true;
		GameObject.Find ("ImagenPanel").GetComponent<Image> ().color = UnityEngine.Color.green;

		AudioSource respuestaOk = GameObject.Find ("AudioRespuestaOk").GetComponent<AudioSource> ();
		respuestaOk.Play ();

		sceneText.SetActive (true);
		coroutineStarted = false;
	}

	void rtaErrorImagen(){

		DesactivarEscucha ();

		ErrorPanelImagen.enabled = true;
		GameObject.Find ("RespuestaText-Error-Panel-Imagen").GetComponent<Image> ().enabled = true;
		GameObject.Find ("ImagenPanel").GetComponent<Image> ().color = UnityEngine.Color.red;

		AudioSource respuestaError = GameObject.Find ("AudioRespuestaIncorrecta").GetComponent<AudioSource> ();
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
