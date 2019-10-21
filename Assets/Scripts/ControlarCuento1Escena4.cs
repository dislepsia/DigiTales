using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using KKSpeech;
using UnityEngine.SceneManagement;

public class ControlarCuento1Escena4 : MonoBehaviour {

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

	//variables para vibrar
	//bool vibrar = true;

	//variables de sonidos
	public AudioClip grito;
	public AudioClip viento;
	private AudioSource ambienteBosque;

	public GameObject player; //objeto para controlar animacion de personaje
	public GameObject bosque; //objeto para controlar escena

	int i=0;
	int n=0;

	public Animator circuloNegro;
	public Animator microfono;

	public GameObject contenedor;

	bool coroutineStarted = true;//para freezar ejecucion
	string coroutineStarted1 = string.Empty;//para freezar contenedor
	bool coroutineStarted2 = true;

	bool coroutineStarted4 = true;


	string modoVibracion = string.Empty; 

	int cambiarTexto = 0;

	bool textoCompleto = false;

    void Start() { 
		Screen.orientation = ScreenOrientation.Landscape;

		modoVibracion = PlayerPrefs.GetString ("ModoVibracion");

		//if (SpeechRecognizer.ExistsOnDevice()) {
			SpeechRecognizerListener listener = GameObject.FindObjectOfType<SpeechRecognizerListener>();
			//listener.onAuthorizationStatusFetched.AddListener(OnAuthorizationStatusFetched);
			//listener.onAvailabilityChanged.AddListener(OnAvailabilityChange);
			listener.onErrorDuringRecording.AddListener(OnError);
			//listener.onErrorOnStartRecording.AddListener(OnError);
			listener.onFinalResults.AddListener(OnFinalResult);
		if(PlayerPrefs.GetString ("ModoReconocimiento") == "0")
			listener.onPartialResults.AddListener(OnPartialResult);
		else
			listener.onPartialResults.AddListener(OnPartialResultPalabraClave);
			//listener.onEndOfSpeech.AddListener(OnEndOfSpeech);
			//startRecordingButton.enabled = false;
			//SpeechRecognizer.RequestAccess();

			//obtengo cantidad de palabras de escena actual
		textoEscena = sceneText.text = "justo detrás de ella";
			palabrasEscena = textoEscena.Split(' ');

			//para q se reproduzca mas rapido, es sonido ya esta asignado
			ambienteBosque = GetComponent<AudioSource> ();						
			ambienteBosque.clip = grito;


			//iniciar objetos
			player.SetActive(true);
			bosque.SetActive(true);

		//} else {			
			//resultErrores.text = "Sorry, but this device doesn't support speech recognition";
			//startRecordingButton.enabled = false;
		//}

		ActivarEscucha ();

	}

	/*RESULTADO FINAL DEL RECONOCIMIENTO DE VOZ*/
	public void OnFinalResult(string result) {		
		ReiniciarValoresEscena();
	}

	/*RESULTADO PARCIAL DEL RECONOCIMIENTO DE VOZ*/
	public void OnPartialResult(string result) {

		//obtengo cantidad de palabras de reconocimiento parcial de voz
		palabrasSpeech = result.ToLower().Split(' ');

		//resultErrores.text = result.ToLower() + " " + cantPalabrasSpeech + palabrasSpeech [0].ToString ().Trim() + " ";


////////////////////////////////////////////*COLOREO DE ORACION DE LA ESCENA*//*PALABRA-POR-PALABRA*////////////////////////////////////////////
			
				if (string.Equals (palabrasSpeech [i].ToString ().Trim(), palabrasEscena [i].ToString ().Trim()) )
				{
					//activar animacion segun palabra
					switch (palabrasSpeech [i].ToString ().Trim())
					{
					case "ella":
						textoCompleto = true;
						DesactivarEscucha ();
						PintarPalabra (palabrasSpeech [i].ToString ());
						coroutineStarted1 = "un grito desesperado se oye";//para freezar contenedor				
						break;
					case "grito":	
							
						PintarPalabra (palabrasSpeech [i].ToString ());
						coroutineStarted4 = false;								
						break;	
					
					case "oye":
						textoCompleto = true;
						DesactivarEscucha ();
						PintarPalabra (palabrasSpeech [i].ToString ());
						ambienteBosque.clip = viento;
						coroutineStarted1 = "entonces comenzó a correr";//para freezar contenedor				
						break;

					case "correr":							
						textoCompleto = true;
						DesactivarEscucha ();
						player.gameObject.GetComponent<Animator>().Play("PlayerRun");
						efectoParallax = 1;
						ambienteBosque.Play ();		
						coroutineStarted = false;//para freezar ejecucion
						PintarPalabra (palabrasSpeech [i].ToString ());				
						break;


						default:	
						PintarPalabra (palabrasSpeech [i].ToString ());
							break;
					}

					//resultTextSpeech.text = resultTextSpeech.text + palabrasSpeech [i].ToString () + " "; //coloreo
					//n++; //para no tener en cuenta palabra coloreada en el bucle

					
				}			
			
		}
	public void OnPartialResultPalabraClave(string result) {

		//obtengo cantidad de palabras de reconocimiento parcial de voz
		palabrasSpeech = result.ToLower().Split(' ');
		cantPalabrasSpeech = palabrasSpeech.Length;
////////////////////////////////////////////*COLOREO DE ORACION DE LA ESCENA*//*POR-PALABRA-CLAVE*////////////////////////////////////////////
			//activar animacion segun palabra
			switch (palabrasSpeech [cantPalabrasSpeech-1].ToString ().Trim())
			{
			case "detrás":
				if(n == 0)
					Pintar (palabrasSpeech [cantPalabrasSpeech - 1].ToString ().Trim ());				
				break;
			case "ella":
				if(n == 1)
				{
					textoCompleto = true;		
					DesactivarEscucha ();
					Pintar (palabrasSpeech [cantPalabrasSpeech - 1].ToString ().Trim ());
					coroutineStarted1 = "un grito desesperado se oye";//para freezar contenedor	
				}
				break;
			case "grito":	
				if(n == 0)
				{
					Pintar (palabrasSpeech [cantPalabrasSpeech - 1].ToString ().Trim ());
					coroutineStarted4 = false;
				}
				break;				
			case "oye":
				if(n == 1)
				{
					textoCompleto = true;		
					DesactivarEscucha ();
					Pintar (palabrasSpeech [cantPalabrasSpeech - 1].ToString ().Trim ());
					ambienteBosque.clip = viento;
					coroutineStarted1 = "entonces comenzó a correr";//para freezar contenedor	
				}	
				break;
			case "comenzó":
				if(n == 0)
					Pintar (palabrasSpeech [cantPalabrasSpeech - 1].ToString ().Trim ());				
				break;
			case "correr":					
				if(n == 1)
				{
					textoCompleto = true;
					DesactivarEscucha ();
					Pintar (palabrasSpeech [cantPalabrasSpeech - 1].ToString ().Trim ());
					player.gameObject.GetComponent<Animator>().Play("PlayerRun");
					efectoParallax = 1;	
					ambienteBosque.Play ();		
					coroutineStarted = false;//para freezar ejecucion	

					//resultTextSpeech.text = string.Empty;
					//textoCompleto = false;
				}
				break;

				default:					
					break;
			}

////////////////////////////////////////////*COLOREO DE ORACION DE LA ESCENA*//*POR-PALABRA-CLAVE(PSEUDO-REAL-TIME)*////////////////////////////////////////////					
			//activar animacion segun palabra
		/*switch (palabrasSpeech [cantPalabrasSpeech-1].ToString ().Trim())
		{
			case "grito":					
				ambienteBosque.Play ();
				Handheld.Vibrate();
				StartCoroutine(UsingYield());
				StopCoroutine ("UsingYield");
				break;
			case "entonces":				
				StartCoroutine(UsingYield2());
				StopCoroutine ("UsingYield2");
				break;
		case "correr":
			//player.SendMessage ("UpdateState", "PlayerRun");
			    player.gameObject.GetComponent<Animator>().Play("PlayerRun");
				efectoParallax = 1;
				StartCoroutine(UsingYield3());
				StopCoroutine ("UsingYield3");
				coroutineStarted = false;				
				break;

			default:					
				break;
		}*/
	}

	/*public void OnAvailabilityChange(bool available) {
		startRecordingButton.enabled = available;
		if (!available) {
			resultErrores.text = "Speech Recognition not available";
		} else {
			//resultErrores.text = "Say something :-)";
		}
	}*/

	/*public void OnAuthorizationStatusFetched(AuthorizationStatus status) {
		switch (status) {
		case AuthorizationStatus.Authorized:
			startRecordingButton.enabled = true;
			break;
		default:
			startRecordingButton.enabled = false;
			resultErrores.text = "Cannot use Speech Recognition, authorization status is " + status;
			break;
		}
	}*/

	/*public void OnEndOfSpeech() {
		startRecordingButton.GetComponentInChildren<Text>().text = "";
	}*/

public void OnError(string error) {
	//Debug.LogError(error);
	//resultErrores.text = "Something went wrong... Try again! \n [" + error + "]";
	//startRecordingButton.GetComponentInChildren<Text>().text = "";

	DesactivarEscucha();
}

public void OnStartRecordingPressed() {
	if (SpeechRecognizer.IsRecording()) {
		DesactivarEscucha ();
	} else {			
		ActivarEscucha ();
	}
}

public void PintarPalabra(string palabra)
{
	resultTextSpeech.text = resultTextSpeech.text + palabra + " "; //coloreo
	i++;
}

public void CambiarTexto(string textoNuevo)
{
	contenedor.SetActive (false);	
	i = 0;
	n = 0;
	textoEscena = sceneText.text = textoNuevo;
	palabrasEscena = textoEscena.Split (' ');

	contenedor.SetActive (true);//llama a otro contenedor de texto
	resultTextSpeech.text = string.Empty;//borra lo escuchado luego de llamar al otro contenedor
	//OnStartRecordingPressed ();//activa escucha

	textoCompleto = false;
	ActivarEscucha();
}

	void Pintar(string palabraClave)
	{
	
		
			n++;
			while (!string.Equals (palabrasEscena [i].ToString (), palabraClave)) {
				resultTextSpeech.text = resultTextSpeech.text + palabrasEscena [i].ToString () + " "; //coloreo
				i++;					
			}
			resultTextSpeech.text = resultTextSpeech.text + palabrasEscena [i].ToString () + " "; //coloreo
			i++;
		
	}  

public void ReiniciarValoresEscena() {	
	if(!textoCompleto)
	{
		resultTextSpeech.text = string.Empty;

		i=0;
		n=0;

		startRecordingButton.gameObject.SetActive(true);
		microfono.gameObject.SetActive(false);
	}
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
	
	if (!string.IsNullOrEmpty(coroutineStarted1))			
		StartCoroutine (RetrasarContenedor (1, coroutineStarted1));



	if (!coroutineStarted4)
		StartCoroutine (VibrarCelular ());
	}  


	IEnumerator EsperarSegundos(int seconds)
	{
		coroutineStarted = true;
		yield return new WaitForSeconds(seconds);

		StartCoroutine (SpriteShapeOut());
		StopCoroutine ("SpriteShapeOut");

		SceneManager.LoadScene("Cuento1Escena5");
	}



	IEnumerator SpriteShapeOut()
	{		
		circuloNegro.SetTrigger ("end");
		yield return new WaitForSeconds(1f);
	}

IEnumerator RetrasarContenedor(int seconds, string frase)
{		
	coroutineStarted1 = string.Empty;
	yield return new WaitForSeconds(seconds);

	CambiarTexto(frase);
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



IEnumerator VibrarCelular(){
	coroutineStarted4 = true;
	ambienteBosque.Play ();
	yield return new WaitForSeconds(1f);
	Handheld.Vibrate ();		

}
}
