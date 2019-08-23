using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using KKSpeech;
using UnityEngine.SceneManagement;

public class ControlarCuento1Escena5 : MonoBehaviour {

	public Button startRecordingButton;

	public Text sceneText; //texto propio de la escena
	public Text resultTextSpeech; //texto reconocido por voz

	public Text resultErrores; //visualizar error

	//variables para trabajar sceneText
	private string textoEscena = string.Empty; 
	private string[] palabrasEscena = null; 

	//variables para trabajar result(reconocimiento parcial de voz)
	private string[] palabrasSpeech = null;
	int cantPalabrasSpeech = 0;

	//variables para efectoParallax
	int efectoParallax = 0;
	public float parallaxSpeed = 0.12f;

	//variables de sonidos
	//public AudioClip grito;
	private AudioSource ambienteBosque;

	public GameObject player; //objeto para controlar animacion de personaje
	public GameObject bosque; //objeto para controlar escena

	private string[] palabrasClave = null; 

	int k=0;
	int j=0;

	public Animator circuloNegro;
	public Animator microfono;

	int contadorUsing=0;
	int contadorUsing2=0;
	int contadorUsing3=0;

	bool coroutineStarted = true;//para freezar ejecucion

    void Start() { 
		Screen.orientation = ScreenOrientation.Landscape;

		if (SpeechRecognizer.ExistsOnDevice()) {
			SpeechRecognizerListener listener = GameObject.FindObjectOfType<SpeechRecognizerListener>();
			listener.onAuthorizationStatusFetched.AddListener(OnAuthorizationStatusFetched);
			listener.onAvailabilityChanged.AddListener(OnAvailabilityChange);
			listener.onErrorDuringRecording.AddListener(OnError);
			listener.onErrorOnStartRecording.AddListener(OnError);
			listener.onFinalResults.AddListener(OnFinalResult);
			listener.onPartialResults.AddListener(OnPartialResult);
			listener.onEndOfSpeech.AddListener(OnEndOfSpeech);
			startRecordingButton.enabled = false;
			SpeechRecognizer.RequestAccess();

			//obtengo cantidad de palabras de escena actual
			textoEscena = sceneText.text;
			palabrasEscena = textoEscena.Split(' ');

			//para q se reproduzca mas rapido, es sonido ya esta asignado
			//ambienteBosque = GetComponent<AudioSource> ();						
			//ambienteBosque.clip = grito;

			//palabras clave
			palabrasClave = new string[3]{"adrenalina","varios","ágilmente"};

			//iniciar objetos
			player.SetActive(true);
			bosque.SetActive(true);
			//player.SendMessage ("UpdateState", "PlayerRun");
			player.gameObject.GetComponent<Animator>().Play("PlayerRun");
			efectoParallax = 1;

		} else {			
			resultErrores.text = "Sorry, but this device doesn't support speech recognition";
			startRecordingButton.enabled = false;
		}



	}

	/*RESULTADO FINAL DEL RECONOCIMIENTO DE VOZ*/
	public void OnFinalResult(string result) {		
		ReiniciarValoresEscena();
	}

	/*RESULTADO PARCIAL DEL RECONOCIMIENTO DE VOZ*/
	public void OnPartialResult(string result) {

		//obtengo cantidad de palabras de reconocimiento parcial de voz
		palabrasSpeech = result.ToLower().Split(' ');
		cantPalabrasSpeech = palabrasSpeech.Length;

////////////////////////////////////////////*COLOREO DE ORACION DE LA ESCENA*//*PALABRA-POR-PALABRA*////////////////////////////////////////////
			/*for (i = n; i < cantPalabrasSpeech; i++)
			{
				if (string.Equals (palabrasSpeech [i].ToString ().Trim(), palabrasEscena [i].ToString ().Trim()))
				{
					//activar animacion segun palabra
					switch (palabrasSpeech [i].ToString ().Trim())
					{
						case "bosque":
							bosque.SetActive(true);
							break;
						case "nena":
							player.SetActive(true);
							break;
						case "temerosa":
							SceneManager.LoadScene("RelatarCuento");
							player.SetActive(true);
							bosque.SetActive(true);
							break;
								
						default:					
							break;
					}

					resultTextSpeech.text = resultTextSpeech.text + palabrasSpeech [i].ToString () + " "; //coloreo
					n++; //para no tener en cuenta palabra coloreada en el bucle

					break;
				}
			else 
				resultErrores.text = "Palabra no reconocida";
			}*/


////////////////////////////////////////////*COLOREO DE ORACION DE LA ESCENA*//*POR-PALABRA-CLAVE*////////////////////////////////////////////
		/*if (string.Equals (palabrasSpeech [cantPalabrasSpeech-1].ToString ().Trim(), palabrasClave [k].ToString ().Trim()))
		{			
			//activar animacion segun palabra
			switch (palabrasSpeech [cantPalabrasSpeech-1].ToString ().Trim())
			{
				case "bosque":
					bosque.SetActive(true);
					break;
				case "nena":
					player.SetActive(true);
					break;
				case "temerosa":
					StartCoroutine (SpriteFadeOut());					
					break;

				default:					
					break;
			}

			while(!string.Equals (palabrasEscena [j].ToString (), palabrasClave [k].ToString ().Trim()))
			{
				resultTextSpeech.text = resultTextSpeech.text + palabrasEscena [j].ToString () + " "; //coloreo
				j++;					
			}
			resultTextSpeech.text = resultTextSpeech.text + palabrasEscena [j].ToString () + " "; //coloreo
			j++;
			k++;

		else 
			resultErrores.text = "Palabra no reconocida";
		}
		*/


////////////////////////////////////////////*COLOREO DE ORACION DE LA ESCENA*//*POR-PALABRA-CLAVE(PSEUDO-REAL-TIME)*////////////////////////////////////////////					
			//activar animacion segun palabra
		switch (palabrasSpeech [cantPalabrasSpeech-1].ToString ().Trim())
		{
			case "adrenalina":	
				StartCoroutine(UsingYield());
				StopCoroutine ("UsingYield");
				break;
			case "varios":	
				StartCoroutine(UsingYield2());
				StopCoroutine ("UsingYield2");
				break;
			case "ágilmente":
				StartCoroutine(UsingYield3());
				StopCoroutine ("UsingYield3");
				coroutineStarted = false;
				break;

			default:					
				break;
		}	
	}

	public void OnAvailabilityChange(bool available) {
		startRecordingButton.enabled = available;
		if (!available) {
			resultErrores.text = "Speech Recognition not available";
		} else {
			//resultErrores.text = "Say something :-)";
		}
	}

	public void OnAuthorizationStatusFetched(AuthorizationStatus status) {
		switch (status) {
		case AuthorizationStatus.Authorized:
			startRecordingButton.enabled = true;
			break;
		default:
			startRecordingButton.enabled = false;
			resultErrores.text = "Cannot use Speech Recognition, authorization status is " + status;
			break;
		}
	}

	public void OnEndOfSpeech() {
		startRecordingButton.GetComponentInChildren<Text>().text = "";
	}

	public void OnError(string error) {
		Debug.LogError(error);
		//resultErrores.text = "Something went wrong... Try again! \n [" + error + "]";
		startRecordingButton.GetComponentInChildren<Text>().text = "";

		startRecordingButton.gameObject.SetActive(true);
		microfono.gameObject.SetActive(false);
	}

	public void OnStartRecordingPressed() {
		if (SpeechRecognizer.IsRecording()) {
			SpeechRecognizer.StopIfRecording();
			startRecordingButton.GetComponentInChildren<Text>().text = "";

			startRecordingButton.gameObject.SetActive(true);
			microfono.gameObject.SetActive(false);
		} else {			
			startRecordingButton.gameObject.SetActive(false);
			microfono.gameObject.SetActive(true);
			SpeechRecognizer.StartRecording(true);
			startRecordingButton.GetComponentInChildren<Text>().text = "";
			//resultErrores.text = "Say something :-)";
		}
	}

	public void ReiniciarValoresEscena() {		
		resultTextSpeech.text = string.Empty;

		j = 0;
		k = 0;

		contadorUsing = 0;
		contadorUsing2 = 0;
		contadorUsing3 = 0;

		startRecordingButton.gameObject.SetActive(true);
		microfono.gameObject.SetActive(false);
	}

	// Update is called once per frame
	void Update()
	{
		if (efectoParallax == 1)
		{
			float finalSpeed= parallaxSpeed * Time.deltaTime;
			RawImage bosqueImagen = bosque.GetComponent<RawImage> ();				
			bosqueImagen.uvRect = new Rect(bosqueImagen.uvRect.x + finalSpeed , 0f, 1f, 1f);
		}

		if (!coroutineStarted)
			StartCoroutine (EsperarSegundos (3));
	}  


	IEnumerator EsperarSegundos(int seconds)
	{
		coroutineStarted = true;
		yield return new WaitForSeconds(seconds);

		StartCoroutine (SpriteShapeOut());
		StopCoroutine ("SpriteShapeOut");

		SceneManager.LoadScene("NewMenu");
	}


	IEnumerator UsingYield()
	{
		contadorUsing ++;
		if (contadorUsing == 1)
		{				
			while(!string.Equals (palabrasEscena [j].ToString (), palabrasClave [k].ToString ().Trim()))
			{
				resultTextSpeech.text = resultTextSpeech.text + palabrasEscena [j].ToString () + " "; //coloreo
				j++;	
				yield return new WaitForSeconds(0.03f);
			}

			resultTextSpeech.text = resultTextSpeech.text + palabrasEscena [j].ToString () + " "; //coloreo
			j++;
			k++;
		}
	}

	IEnumerator UsingYield2()
	{
		contadorUsing2 ++;
		if (contadorUsing2 == 1)
		{	
			while(!string.Equals (palabrasEscena [j].ToString (), palabrasClave [k].ToString ().Trim()))
			{
				resultTextSpeech.text = resultTextSpeech.text + palabrasEscena [j].ToString () + " "; //coloreo
				j++;	
				yield return new WaitForSeconds(0.03f);
			}

			resultTextSpeech.text = resultTextSpeech.text + palabrasEscena [j].ToString () + " "; //coloreo
			j++;
			k++;
		}
	}

	IEnumerator UsingYield3()
	{
		contadorUsing3 ++;
		if (contadorUsing3 == 1)
		{	
			while(!string.Equals (palabrasEscena [j].ToString (), palabrasClave [k].ToString ().Trim()))
			{
				resultTextSpeech.text = resultTextSpeech.text + palabrasEscena [j].ToString () + " "; //coloreo
				j++;	
				yield return new WaitForSeconds(0.03f);
			}

			resultTextSpeech.text = resultTextSpeech.text + palabrasEscena [j].ToString () + " "; //coloreo
			j++;
			k++;
		}
	}

	IEnumerator SpriteShapeOut()
	{		
		circuloNegro.SetTrigger ("end");
		yield return new WaitForSeconds(1f);
	}
}
