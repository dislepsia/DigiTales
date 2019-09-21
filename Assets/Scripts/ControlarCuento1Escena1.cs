using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using KKSpeech;
using UnityEngine.SceneManagement;

public class ControlarCuento1Escena1 : MonoBehaviour {

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
	private AudioSource ambienteBosque;

	public GameObject player; //objeto para controlar animacion de personaje
	public GameObject bosque; //objeto para controlar escena

	int i=0;
	int n=0;


	public Animator imagenNegra;
	public Animator microfono;

	public GameObject contenedor;

	bool coroutineStarted = true;//para freezar ejecucion
	string coroutineStarted1 = string.Empty;//para freezar contenedor

	string modoRelato = string.Empty; 

	int cambiarTexto = 0;

	bool textoCompleto = false;

    void Start() { 
		Screen.orientation = ScreenOrientation.Landscape;
		modoRelato = PlayerPrefs.GetString ("ModoReconocimiento");

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
			textoEscena = sceneText.text = "había una vez";
			palabrasEscena = textoEscena.Split(' ');

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

		if (modoRelato == "0")
		{
////////////////////////////////////////////*COLOREO DE ORACION DE LA ESCENA*//*PALABRA-POR-PALABRA*////////////////////////////////////////////
			for (i = n; i < cantPalabrasSpeech; i++)
			{
				if (string.Equals (palabrasSpeech [i].ToString ().Trim(), palabrasEscena [i].ToString ().Trim()) && n == i)
					{
						//activar animacion segun palabra
						switch (palabrasSpeech [i].ToString ().Trim())
						{
							case "vez":
								//textoCompleto = true;
								PintarPalabra (palabrasSpeech [i].ToString ());
								coroutineStarted1 = "en un bosque oscuro";//para freezar contenedor	
								//textoCompleto = false;
								break;
							case "bosque":
								bosque.SetActive (true);
								PintarPalabra (palabrasSpeech [i].ToString ());
								break;
							case "oscuro":
								//textoCompleto = true;
								PintarPalabra (palabrasSpeech [i].ToString ());
								coroutineStarted1 = "una niña vestida de rojo";//para freezar contenedor
								//textoCompleto = false;
								break;
							case "niña":
								player.SetActive (true);	
								PintarPalabra (palabrasSpeech [i].ToString ());
								break;
							case "rojo":	
								//textoCompleto = true;
								coroutineStarted = false;//para freezar ejecucion
								PintarPalabra (palabrasSpeech [i].ToString ());
								//textoCompleto = false;
								break;

							default:	
								PintarPalabra (palabrasSpeech [i].ToString ());
								break;
						}

						//resultTextSpeech.text = resultTextSpeech.text + palabrasSpeech [i].ToString () + " "; //coloreo
						//n++; //para no tener en cuenta palabra coloreada en el bucle

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
				case "vez":
				if(Pintar ("vez", 0))
					CambiarTexto("en un bosque oscuro");
					break;
				case "bosque":					
					if(Pintar ("bosque", 0))
						bosque.SetActive (true);
					break;
				case "oscuro":
					if(Pintar ("oscuro", 1))
					CambiarTexto("una niña vestida de rojo");
				break;
				case "niña":					
					if(Pintar ("niña", 0))
						player.SetActive(true);
					break;
				case "rojo":					
					if(Pintar ("rojo", 1))
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
			case "bosque":
				bosque.SetActive (true);
				StartCoroutine (UsingYield ());
				StopCoroutine ("UsingYield");
				break;
			case "niña":
				player.SetActive (true);
				StartCoroutine (UsingYield2 ());
				StopCoroutine ("UsingYield2");
				break;
			case "misteriosa":
				StartCoroutine(UsingYield3());
				StopCoroutine ("UsingYield3");				
				coroutineStarted = false;//para freezar ejecucion
				break;

				default:					
					break;
		}	*/
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

	public void PintarPalabra(string palabra)
	{
		resultTextSpeech.text = resultTextSpeech.text + palabra + " "; //coloreo
		n++; //para no tener en cuenta palabra coloreada en el bucle
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
		OnStartRecordingPressed ();//activa escucha
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
		//if(!textoCompleto)
		//{
			resultTextSpeech.text = string.Empty;

			i=0;
			n=0;

			startRecordingButton.gameObject.SetActive(true);
			microfono.gameObject.SetActive(false);
		//}
	}

	// Update is called once per frame
	void Update()
	{
		if (!coroutineStarted)
			StartCoroutine (EsperarSegundos (1));

		if (!string.IsNullOrEmpty(coroutineStarted1))			
			StartCoroutine (RetrasarContenedor (1, coroutineStarted1));
	}  


	IEnumerator EsperarSegundos(int seconds)
	{
		coroutineStarted = true;
		yield return new WaitForSeconds(seconds);

		StartCoroutine (SpriteFadeOut());
		StopCoroutine ("SpriteFadeOut");

		SceneManager.LoadScene("Cuento1Escena2");
	}

	IEnumerator SpriteFadeOut()
	{		
		imagenNegra.SetTrigger ("end");
		yield return new WaitForSeconds(1f);
	}

	IEnumerator RetrasarContenedor(int seconds, string frase)
	{		
		coroutineStarted1 = string.Empty;
		yield return new WaitForSeconds(seconds);

		CambiarTexto(frase);
	}



}
