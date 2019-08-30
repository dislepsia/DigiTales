using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using KKSpeech;
using UnityEngine.SceneManagement;

public class ControlarCuento1Escena2 : MonoBehaviour {

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

	//variables de sonidos
	public AudioClip trueno;
	private AudioSource ambienteBosque;

	public GameObject player; //objeto para controlar animacion de personaje
	public GameObject bosque; //objeto para controlar escena

	int i=0;
	int n=0;

	public Animator circuloNegro;
	public Animator microfono;

	bool coroutineStarted = true;//para freezar ejecucion

	int modoRelato=0;

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
			ambienteBosque = GetComponent<AudioSource> ();						
			ambienteBosque.clip = trueno;

			//iniciar objetos
			player.SetActive(true);
			bosque.SetActive(true);

		} else {			
			resultErrores.text = "Sorry, but this device doesn't support speech recognition";
			startRecordingButton.enabled = false;
		}

		OnStartRecordingPressed ();

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
		//resultErrores.text = result.ToLower() + " " + cantPalabrasSpeech + palabrasSpeech [0].ToString ().Trim() + " " ;

		if (modoRelato == 0)
		{
////////////////////////////////////////////*COLOREO DE ORACION DE LA ESCENA*//*PALABRA-POR-PALABRA*////////////////////////////////////////////
			for (i = n; i < cantPalabrasSpeech; i++)
			{
				if (string.Equals (palabrasSpeech [i].ToString ().Trim(), palabrasEscena [i].ToString ().Trim()) && n == i)
				{
					//activar animacion segun palabra
					switch (palabrasSpeech [i].ToString ().Trim())
					{
						case "tormenta":
							ambienteBosque.Play ();							
							break;						
						case "rápidamente":							
							coroutineStarted = false;
							break;

						default:					
							break;
					}

					resultTextSpeech.text = resultTextSpeech.text + palabrasSpeech [i].ToString () + " "; //coloreo
					n++; //para no tener en cuenta palabra coloreada en el bucle

					break;
				}			
			}
		}
		else
		{
////////////////////////////////////////////*COLOREO DE ORACION DE LA ESCENA*//*POR-PALABRA-CLAVE*////////////////////////////////////////////				
			//activar animacion segun palabra
			switch (palabrasSpeech [cantPalabrasSpeech-1].ToString ().Trim())
			{
				case "tormenta":
					if(Pintar ("tormenta", 0))
						ambienteBosque.Play ();											
					break;						
				case "rápidamente":		
					if(Pintar ("rápidamente", 1))
					{
						coroutineStarted = false;//para freezar ejecucion	
						SpeechRecognizer.StopIfRecording();
					}
					break;

				default:					
					break;
			}
		}
////////////////////////////////////////////*COLOREO DE ORACION DE LA ESCENA*//*POR-PALABRA-CLAVE(PSEUDO-REAL-TIME)*////////////////////////////////////////////					
			//activar animacion segun palabra
		/*switch (palabrasSpeech [cantPalabrasSpeech-1].ToString ().Trim())
		{
			case "tormenta":
				ambienteBosque.Play ();
				StartCoroutine(UsingYield2());
				StopCoroutine ("UsingYield2");
				break;
			case "inevitable":	
				StartCoroutine (UsingYield ());
				StopCoroutine ("UsingYield");
				break;			
			case "oscuridad":
				StartCoroutine(UsingYield3());
				StopCoroutine ("UsingYield3");
				coroutineStarted = false;
				break;

				default:					
					break;
		}*/	
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

	bool Pintar(string palabraClave, int nroPalabraClave)
	{
		if (n == nroPalabraClave) {	
			n++;
			while (!string.Equals (palabrasEscena [i].ToString (), palabraClave)) {
				resultTextSpeech.text = resultTextSpeech.text + palabrasEscena [i].ToString () + " "; //coloreo
				i++;					
			}
			resultTextSpeech.text = resultTextSpeech.text + palabrasEscena [i].ToString () + " "; //coloreo
			i++;
			return true;
		} else
			return false;
	}  

	public void ReiniciarValoresEscena() {		
		resultTextSpeech.text = string.Empty;

		i = 0;
		n = 0;

		startRecordingButton.gameObject.SetActive(true);
		microfono.gameObject.SetActive(false);
	}

	// Update is called once per frame
	void Update()
	{
		if (!coroutineStarted)
			StartCoroutine (EsperarSegundos (3));
	}  


	IEnumerator EsperarSegundos(int seconds)
	{
		coroutineStarted = true;
		yield return new WaitForSeconds(seconds);

		StartCoroutine (SpriteShapeOut());
		StopCoroutine ("SpriteShapeOut");

		SceneManager.LoadScene("Cuento1Escena3");
	}

	IEnumerator SpriteShapeOut()
	{		
		circuloNegro.SetTrigger ("end");
		yield return new WaitForSeconds(1f);
	}
}
