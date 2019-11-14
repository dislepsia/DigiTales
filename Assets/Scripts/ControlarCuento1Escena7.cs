using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using KKSpeech;
using UnityEngine.SceneManagement;

public class ControlarCuento1Escena7 : MonoBehaviour {

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
	int efectoParallax = 0;
	public float parallaxSpeed = 0.12f;

	//variables de sonidos
	public AudioClip suspenso;
	private AudioSource ambienteBosque;
	public GameObject bosqueInv; //objeto para controlar escena

	public GameObject player; //objeto para controlar animacion de personaje
	public GameObject bosque; //objeto para controlar escena

	public GameObject fantasma; //objeto para controlar escena

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
	bool coroutineStarted2 = true;


	string modoVibracion = string.Empty; 

	int cambiarTexto = 0;

	bool textoCompleto = false;

	string fraseEscena = string.Empty;

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
		textoEscena = sceneText.text = "de pronto luego de varios relámpagos";
		palabrasEscena = textoEscena.Split(' ');
		cantPalabrasEscena = palabrasEscena.Length;

		//para q se reproduzca mas rapido, es sonido ya esta asignado
		ambienteBosque = GetComponent<AudioSource> ();						
		ambienteBosque.clip = suspenso;
			

		//iniciar objetos
		player.SetActive(true);
		bosque.SetActive(true);

		player.gameObject.GetComponent<Animator>().Play("PlayerRun");
		efectoParallax = 1;

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
				if (string.Equals (palabrasSpeech [i].ToString ().Trim(), palabrasEscena [i].ToString ().Trim()))
				{
					//activar animacion segun palabra
					switch (palabrasSpeech [i].ToString ().Trim())
					{	
					case "luego":							
					if(palabraspintadas==i)
					{
						PintarPalabra (palabrasSpeech [i].ToString ());
						coroutineStarted2 = false;	
					}			
						break;

					case "relámpagos":	
						if(palabraspintadas==i)
						{
						textoCompleto = true;
						DesactivarEscucha ();
						PintarPalabra (palabrasSpeech [i].ToString ());

						coroutineStarted1 = "una tenebrosa sombra surge";//para freezar contenedor				
						}
							break;

					case "sombra":							
							if(palabraspintadas==i)
							{
						PintarPalabra (palabrasSpeech [i].ToString ());
						coroutineStarted2 = false;	
						fantasma.SetActive(true);
						ambienteBosque.Play ();	

						player.gameObject.GetComponent<Animator> ().Play ("PlayerIdle");
						efectoParallax = 0;	
							}
						break;

					case "surge":
								if(palabraspintadas==i)
								{
						textoCompleto = true;
						DesactivarEscucha ();
						PintarPalabra (palabrasSpeech [i].ToString ());
						fraseEscena = coroutineStarted1 = "la niña cae desmayada";//para freezar contenedor		
								}
						break;

					case "cae":		
									if(palabraspintadas==i)
									{
						PintarPalabra (palabrasSpeech [i].ToString ());
						player.gameObject.GetComponent<Animator> ().Play ("PlayerDie");

									}
							

						break;

					case "desmayada":	
										if(palabraspintadas==i)
										{
						textoCompleto = true;
						DesactivarEscucha ();
						fantasma.gameObject.GetComponent<Animator> ().enabled =true;
						fantasma.gameObject.GetComponent<Animator>().Play("Fantasma");
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
			switch (palabrasSpeech [i].ToString ().Trim())
			{
			case "luego":
				if(n == 0 && nroContenedor==0)
				{
					Pintar (palabrasSpeech [i].ToString ().Trim());		
					coroutineStarted2 = false;	
				}
				break;
			
			case "relámpagos":
				if(n == 1 && nroContenedor==0)
				{
					textoCompleto = true;		
					DesactivarEscucha ();
					Pintar (palabrasSpeech [i].ToString ().Trim());
					nroContenedor=1;
					coroutineStarted1 = "una tenebrosa sombra surge";//para freezar contenedor		
				}
				break;

			case "sombra":
				if(n == 0 && nroContenedor==1)
				{
					coroutineStarted2 = false;	
					fantasma.SetActive(true);
					ambienteBosque.Play ();	
					Pintar (palabrasSpeech [i].ToString ().Trim());		
					player.gameObject.GetComponent<Animator> ().Play ("PlayerIdle");
					efectoParallax = 0;	
				}
				break;
			case "surge":					
				if(n == 1 && nroContenedor==1)
				{
					textoCompleto = true;
					DesactivarEscucha ();
					Pintar (palabrasSpeech [i].ToString ().Trim());		
					nroContenedor=2;
					fraseEscena = coroutineStarted1 = "la niña cae desmayada";//para freezar contenedor

				}
				break;
			case "cae":
				if(n == 0 && nroContenedor==2)
				{
					player.gameObject.GetComponent<Animator> ().Play ("PlayerDie");
					Pintar (palabrasSpeech [i].ToString ().Trim());		

				}
				break;
			case "desmayada":
				if(n == 1 && nroContenedor==2)
				{
					textoCompleto = true;
					DesactivarEscucha ();
					fantasma.gameObject.GetComponent<Animator> ().enabled =true;
					fantasma.gameObject.GetComponent<Animator>().Play("Fantasma");
					Pintar (palabrasSpeech [i].ToString ().Trim());		
					coroutineStarted = false;//para freezar ejecucion

				}
				break;

				default:					
					break;
			}		
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

		coroutineStarted2 = false;	
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

			if(fraseEscena == "la niña cae desmayada")
			{
				
				player.gameObject.GetComponent<Animator> ().Play ("PlayerIdle");	

			}
				else
				{
		player.gameObject.GetComponent<Animator> ().Play ("PlayerRun");
		efectoParallax = 1;
			fantasma.SetActive(false);
				}
			contenedorError.SetActive (true);
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
	else
	{
		RawImage bosqueImagen = bosque.GetComponent<RawImage> ();
		bosqueImagen.uvRect = new Rect(bosqueImagen.uvRect.x , 0f, 1f, 1f);
	}

	if (!coroutineStarted)
			StartCoroutine (EsperarSegundos (1f));

	if (!string.IsNullOrEmpty(coroutineStarted1))			
			StartCoroutine (RetrasarContenedor (0.5f, coroutineStarted1));	
		
		if (!coroutineStarted2)
			StartCoroutine (EfectoRelampago ());
	}  

	IEnumerator EfectoRelampago()
	{
		coroutineStarted2 = true;

		for (int z = 0; z < 4; z++)
		{
			bosqueInv.SetActive (true);
			yield return new WaitForSeconds(0.1f);
			bosqueInv.SetActive (false);
			yield return new WaitForSeconds(0.1f);
		}

	}  


	IEnumerator EsperarSegundos(float seconds)
	{
		coroutineStarted = true;
		yield return new WaitForSeconds(seconds);

		StartCoroutine (SpriteShapeOut());
		StopCoroutine ("SpriteShapeOut");

	SceneManager.LoadScene("Cuento1Escena7Bis");
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
