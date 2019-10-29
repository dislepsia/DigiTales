using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using KKSpeech;
using UnityEngine.SceneManagement;

public class ControlarCuento1Escena3 : MonoBehaviour {

	public Button startRecordingButton;

	public Text sceneText; //texto propio de la escena
	public Text resultTextSpeech; //texto reconocido por voz

	public Text resultErrores; //visualizar error

	//variables para trabajar sceneText
	private string textoEscena = string.Empty; 
	private string[] palabrasEscena = null; 
	int cantPalabrasEscena = 0;

	//variables para trabajar result(reconocimiento parcial de voz)
	private string[] palabrasSpeech = null;
	int cantPalabrasSpeech = 0;

	//variables de sonidos
	public AudioClip buho;
	private AudioSource ambienteBosque;

	public GameObject player; //objeto para controlar animacion de personaje
	public GameObject bosque; //objeto para controlar escena

	int i=0;
	int n=0;
	int k=0;
	int palabraspintadas=0;
	int nroContenedor=0;

	public Animator circuloNegro;
	public Animator microfono;
	public Animator buhoEfecto;

	public GameObject contenedor;

	bool coroutineStarted = true;//para freezar ejecucion
	string coroutineStarted1 = string.Empty;//para freezar contenedor
	bool coroutineStarted2 = true;


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
		else if(PlayerPrefs.GetString ("ModoReconocimiento") == "1")
			listener.onPartialResults.AddListener(OnPartialResultPalabraClave);
			//listener.onEndOfSpeech.AddListener(OnEndOfSpeech);
			//startRecordingButton.enabled = false;
			//SpeechRecognizer.RequestAccess();

			//obtengo cantidad de palabras de escena actual
			textoEscena = sceneText.text = "la noche comienza";
			palabrasEscena = textoEscena.Split(' ');
		cantPalabrasEscena = palabrasEscena.Length;

			//para q se reproduzca mas rapido, es sonido ya esta asignado
			ambienteBosque = GetComponent<AudioSource> ();						
			ambienteBosque.clip = buho;

			//iniciar objetos
			player.SetActive(true);
			bosque.SetActive(true);

		//} else {			
		//	resultErrores.text = "Sorry, but this device doesn't support speech recognition";
		//	startRecordingButton.enabled = false;
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
		cantPalabrasSpeech = palabrasSpeech.Length;
		resultErrores.text = result.ToLower() + " " + cantPalabrasSpeech + palabrasSpeech [0].ToString ().Trim() + " ";


////////////////////////////////////////////*COLOREO DE ORACION DE LA ESCENA*//*PALABRA-POR-PALABRA*////////////////////////////////////////////
		for (i = n; i < cantPalabrasSpeech && cantPalabrasSpeech <= cantPalabrasEscena; i++)
		{
				if (string.Equals (palabrasSpeech [i].ToString ().Trim(), palabrasEscena [i].ToString ().Trim()) )
				{
					//activar animacion segun palabra
					switch (palabrasSpeech [i].ToString ().Trim())
					{
					case "comienza":
					if(palabraspintadas==i)
					{
						textoCompleto = true;
						DesactivarEscucha ();
						PintarPalabra (palabrasSpeech [i].ToString ());
						coroutineStarted1 = "entonces aparece el búho";//para freezar contenedor
					}
						break;
					case "aparece":	
						if(palabraspintadas==i)
						{
						PintarPalabra (palabrasSpeech [i].ToString ());
							ambienteBosque.Play ();	
						}
							break;	
					case "búho":
							if(palabraspintadas==i)
							{
						textoCompleto = true;
						DesactivarEscucha ();
						PintarPalabra (palabrasSpeech [i].ToString ());
						coroutineStarted1 = "con su particular cantar";//para freezar contenedor
							}
						break;
					case "particular":
								if(palabraspintadas==i)
								{
						PintarPalabra (palabrasSpeech [i].ToString ());
						buhoEfecto.gameObject.SetActive(true);		
								}
						break;	
						case "cantar":	
									if(palabraspintadas==i)
									{
						textoCompleto = true;
						DesactivarEscucha ();
						coroutineStarted = false;//para freezar ejecucion
						PintarPalabra (palabrasSpeech [i].ToString ());		
									}
							break;

						default:	
										if(palabraspintadas==i)
										{
						PintarPalabra (palabrasSpeech [i].ToString ());
										}
							break;
					}

					//resultTextSpeech.text = resultTextSpeech.text + palabrasSpeech [i].ToString () + " "; //coloreo
					//n++; //para no tener en cuenta palabra coloreada en el bucle

					
				}
		}
			
		}
	public void OnPartialResultPalabraClave(string result) {

		//obtengo cantidad de palabras de reconocimiento parcial de voz
		palabrasSpeech = result.ToLower().Split(' ');
		cantPalabrasSpeech = palabrasSpeech.Length;
////////////////////////////////////////////*COLOREO DE ORACION DE LA ESCENA*//*POR-PALABRA-CLAVE*////////////////////////////////////////////
		resultErrores.text = result.ToLower() + " " + cantPalabrasSpeech + palabrasSpeech [0].ToString ().Trim() + " " ;
		for (i = k; i < cantPalabrasSpeech && cantPalabrasSpeech <= cantPalabrasEscena; i++)
		{	
		//activar animacion segun palabra
			switch (palabrasSpeech [i].ToString ().Trim())
			{
			case "noche":
				if(n == 0 && nroContenedor==0)
					Pintar (palabrasSpeech [i].ToString ().Trim());				
			break;
			case "comienza":
				if(n == 1 && nroContenedor==0)
				{
					textoCompleto = true;		
					DesactivarEscucha ();
					Pintar (palabrasSpeech [i].ToString ().Trim());	
					nroContenedor=1;
				coroutineStarted1 = "entonces aparece el búho";//para freezar contenedor	
				}
				break;
			case "aparece":	
				if(n == 0 && nroContenedor==1)
			{
					Pintar (palabrasSpeech [i].ToString ().Trim());	
				ambienteBosque.Play ();		
			}
					break;		
			case "búho":
				if(n == 1 && nroContenedor==1)
				{
					textoCompleto = true;		
					DesactivarEscucha ();
					Pintar (palabrasSpeech [i].ToString ().Trim());	
					nroContenedor=2;
				coroutineStarted1 = "con su particular cantar";//para freezar contenedor	
				}
				break;
			case "particular":	
				if(n == 0 && nroContenedor==2)
			{
					Pintar (palabrasSpeech [i].ToString ().Trim());	
				buhoEfecto.gameObject.SetActive(true);
			}
				break;	

			case "cantar":					
				if(n == 1 && nroContenedor==2)
				{
					textoCompleto = true;
					DesactivarEscucha ();
					Pintar (palabrasSpeech [i].ToString ().Trim());	
					coroutineStarted = false;//para freezar ejecucion	

					//resultTextSpeech.text = string.Empty;
					//textoCompleto = false;
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
			case "comenzó":	
				ambienteBosque.Play ();
				StartCoroutine (UsingYield ());
				StopCoroutine ("UsingYield");
				break;
			case "búho":	
				StartCoroutine(UsingYield2());
				StopCoroutine ("UsingYield2");
				break;
			case "cantar":
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
	//i++;
	n++;
	palabraspintadas++;
}

public void CambiarTexto(string textoNuevo)
{
	contenedor.SetActive (false);	
	i = 0;
	n = 0;
	k=0;
	palabraspintadas = 0;
	textoEscena = sceneText.text = textoNuevo;
	palabrasEscena = textoEscena.Split (' ');
	cantPalabrasEscena = palabrasEscena.Length;

	contenedor.SetActive (true);//llama a otro contenedor de texto
	resultTextSpeech.text = string.Empty;//borra lo escuchado luego de llamar al otro contenedor
	//OnStartRecordingPressed ();//activa escucha

	textoCompleto = false;
	ActivarEscucha();
}

void Pintar(string palabraClave)
{		
	n++;//controla orden de coloreo de palabra clave
	while (!string.Equals (palabrasEscena [k].ToString (), palabraClave)) {
		resultTextSpeech.text = resultTextSpeech.text + palabrasEscena [k].ToString () + " "; //coloreo
		//i++;	
		k++;
	}
	resultTextSpeech.text = resultTextSpeech.text + palabrasEscena [k].ToString () + " "; //coloreo
	//i++;	
	k++;		
}  

public void ReiniciarValoresEscena() {	
	if(!textoCompleto)
	{
		resultTextSpeech.text = string.Empty;

		i=0;
		n=0;
		k=0;
		palabraspintadas = 0;
		startRecordingButton.gameObject.SetActive(true);
		microfono.gameObject.SetActive(false);

		buhoEfecto.gameObject.SetActive(false);
	}
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

		StartCoroutine (SpriteShapeOut());
		StopCoroutine ("SpriteShapeOut");

		SceneManager.LoadScene("Cuento1Escena4");
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

public void Vibrar(){
	Handheld.Vibrate ();
}
}
