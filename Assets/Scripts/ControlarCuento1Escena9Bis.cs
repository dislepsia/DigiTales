using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using KKSpeech;
using UnityEngine.SceneManagement;

public class ControlarCuento1Escena9Bis : MonoBehaviour {

	public Button startRecordingButton;
	public Button stopRecordingButton;

	bool stopRecording = false;

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

	//variables para efectoParallax

	//variables de sonidos

	private AudioSource ambienteBosque;

	public GameObject bosque; //objeto para controlar escena

	int i=0;
	int n=0;
	int k=0;
	int palabraspintadas=0;
	int nroContenedor=0;

	public Animator imagenNegra;
	public Animator microfono;
	//public Animator troncoEfecto;

	public GameObject contenedor;
	public GameObject contenedorError;

	bool coroutineStarted = true;//para freezar ejecucion
	string coroutineStarted1 = string.Empty;//para freezar contenedor



	string modoVibracion = string.Empty; 

	int cambiarTexto = 0;

	bool textoCompleto = false;



    void Start() { 
		Screen.orientation = ScreenOrientation.Landscape;

		modoVibracion = PlayerPrefs.GetString ("ModoVibracion");


		SpeechRecognizerListener listener = GameObject.FindObjectOfType<SpeechRecognizerListener>();		
		listener.onErrorDuringRecording.AddListener(OnError);			
		listener.onFinalResults.AddListener(OnFinalResult);
		if(PlayerPrefs.GetString ("ModoReconocimiento") == "0")
			listener.onPartialResults.AddListener(OnPartialResult);
		else
			listener.onPartialResults.AddListener(OnPartialResultPalabraClave);


		//obtengo cantidad de palabras de escena actual
		textoEscena = sceneText.text = "esta historia continuará";
		palabrasEscena = textoEscena.Split(' ');
		cantPalabrasEscena = palabrasEscena.Length;

		//para q se reproduzca mas rapido, es sonido ya esta asignado
		ambienteBosque = GetComponent<AudioSource> ();						

			

		//iniciar objetos

		bosque.SetActive(true);




		ActivarEscucha ();
		imagenNegra.Play("FadeIN");
	}

	/*RESULTADO FINAL DEL RECONOCIMIENTO DE VOZ*/
	public void OnFinalResult(string result) {		
		if (!stopRecording)
			ReiniciarValoresEscena ();
		else
			stopRecording = false;
	}

	/*RESULTADO PARCIAL DEL RECONOCIMIENTO DE VOZ*/
	public void OnPartialResult(string result) {
		if(!stopRecording)
		{
		//obtengo cantidad de palabras de reconocimiento parcial de voz
		palabrasSpeech = result.ToLower().Split(' ');

		cantPalabrasSpeech = palabrasSpeech.Length;
		//resultErrores.text = result.ToLower() + " " + cantPalabrasSpeech + palabrasSpeech [0].ToString ().Trim() + " ";


////////////////////////////////////////////*COLOREO DE ORACION DE LA ESCENA*//*PALABRA-POR-PALABRA*////////////////////////////////////////////
		for (i = n; i < cantPalabrasSpeech && cantPalabrasSpeech <= cantPalabrasEscena; i++)
		{
				if (string.Equals (palabrasSpeech [i].ToString ().Trim(), palabrasEscena [i].ToString ().Trim()) )
				{
					//activar animacion segun palabra
					switch (palabrasSpeech [i].ToString ().Trim())
					{	


					case "continuará":							
						textoCompleto = true;
						DesactivarEscucha ();

						coroutineStarted = false;//para freezar ejecucion
						PintarPalabra (palabrasSpeech [i].ToString ());				
						break;

					

						default:	
						PintarPalabra (palabrasSpeech [i].ToString ());
							break;
					}		

			}	
				}			
		}
		}
	public void OnPartialResultPalabraClave(string result) {
			if(!stopRecording)
			{
		//obtengo cantidad de palabras de reconocimiento parcial de voz
		palabrasSpeech = result.ToLower().Split(' ');
		cantPalabrasSpeech = palabrasSpeech.Length;
////////////////////////////////////////////*COLOREO DE ORACION DE LA ESCENA*//*POR-PALABRA-CLAVE*////////////////////////////////////////////
		//resultErrores.text = result.ToLower() + " " + cantPalabrasSpeech + palabrasSpeech [0].ToString ().Trim() + " " ;
		for (i = k; i < cantPalabrasSpeech && cantPalabrasSpeech <= cantPalabrasEscena; i++)
		{		
		//activar animacion segun palabra
			switch (palabrasSpeech [cantPalabrasSpeech-1].ToString ().Trim())
			{

			
			case "continuará":
			if(n == 0 && nroContenedor==0)
				{
					textoCompleto = true;
					DesactivarEscucha ();
					Pintar (palabrasSpeech [i].ToString ().Trim());		
					coroutineStarted = false;//para freezar ejecucion		
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
		}*/
	}
	}
public void OnError(string error) {
		if(!stopRecording)
		{
			DesactivarEscucha();
			contenedorError.SetActive (true);
		}
		else
			stopRecording=false;
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
			stopRecordingButton.gameObject.SetActive(false);
		microfono.gameObject.SetActive(false);
			contenedorError.SetActive (true);

	}
}

	// Update is called once per frame
	void Update()
	{


	if (!coroutineStarted)
			StartCoroutine (EsperarSegundos (0.5f));

	if (!string.IsNullOrEmpty(coroutineStarted1))			
			StartCoroutine (RetrasarContenedor (0.5f, coroutineStarted1));	
		

	}  




	IEnumerator EsperarSegundos(float seconds)
	{
		coroutineStarted = true;
		yield return new WaitForSeconds(seconds);

		StartCoroutine (SpriteShapeOut());
		StopCoroutine ("SpriteShapeOut");

		SceneManager.LoadScene("MiniJuego-NenaTemerosa-Modo");
	}

	IEnumerator SpriteShapeOut()
	{		
		imagenNegra.Play("FadeOUT");
		yield return new WaitForSeconds(0.5f);
	}

IEnumerator RetrasarContenedor(float seconds, string frase)
{		
	coroutineStarted1 = string.Empty;
	yield return new WaitForSeconds(seconds);

	CambiarTexto(frase);
}

public void ActivarEscucha() {	
	startRecordingButton.gameObject.SetActive(false);
		stopRecordingButton.gameObject.SetActive(true);
	microfono.gameObject.SetActive(true);
	SpeechRecognizer.StartRecording(true);
		contenedorError.SetActive (false);
}

public void DesactivarEscucha() {	
	SpeechRecognizer.StopIfRecording ();
	startRecordingButton.gameObject.SetActive(true);
		stopRecordingButton.gameObject.SetActive(false);
	microfono.gameObject.SetActive(false);
}

public void Vibrar(){
	Handheld.Vibrate ();
}

	public void BotonVolver() {	

		DesactivarEscucha();
		Screen.orientation = ScreenOrientation.Portrait;
		SceneManager.LoadScene("MiniJuego-NenaTemerosa-Modo");  
	}

	public void ReiniciarValoresStopEscucha() {	

		resultTextSpeech.text = string.Empty;

		i=0;
		n=0;
		k=0;
		palabraspintadas = 0;


		DesactivarEscucha ();



	}

	public void BotonPararEscucha() {	
		stopRecording = true;	
		ReiniciarValoresStopEscucha();

	}
}
