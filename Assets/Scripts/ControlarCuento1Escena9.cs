using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using KKSpeech;
using UnityEngine.SceneManagement;

public class ControlarCuento1Escena9 : MonoBehaviour {

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

	//variables para efectoParallax
	int efectoParallax = 0;
	public float parallaxSpeed = 0.06f;

	//variables de sonidos

	public AudioClip relincho;

	private AudioSource ambienteBosque;
	public GameObject bosqueInv; //objeto para controlar escena

	public GameObject player; //objeto para controlar animacion de personaje
	public GameObject bosque; //objeto para controlar escena


	public GameObject pegaso; //objeto para controlar escena

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

	bool coroutineStarted3 = true;


	string modoVibracion = string.Empty; 

	int cambiarTexto = 0;

	bool textoCompleto = false;

	public GameObject casa;

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
		fraseEscena = textoEscena = sceneText.text = "una casa abandonada aparece";
		palabrasEscena = textoEscena.Split(' ');
		cantPalabrasEscena = palabrasEscena.Length;

		//para q se reproduzca mas rapido, es sonido ya esta asignado
		ambienteBosque = GetComponent<AudioSource> ();						
		ambienteBosque.clip = relincho;
			




		//iniciar objetos
		player.SetActive(true);
		bosque.SetActive(true);

		player.gameObject.GetComponent<Animator>().Play("PlayerWalk");
		efectoParallax = 1;

		ActivarEscucha ();
		imagenNegra.Play("FadeIN");
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
		//resultErrores.text = result.ToLower() + " " + cantPalabrasSpeech + palabrasSpeech [0].ToString ().Trim() + " ";


////////////////////////////////////////////*COLOREO DE ORACION DE LA ESCENA*//*PALABRA-POR-PALABRA*////////////////////////////////////////////
		for (i = n; i < cantPalabrasSpeech && cantPalabrasSpeech <= cantPalabrasEscena; i++)
		{
				if (string.Equals (palabrasSpeech [i].ToString ().Trim(), palabrasEscena [i].ToString ().Trim()))
				{
					//activar animacion segun palabra
					switch (palabrasSpeech [i].ToString ().Trim())
					{	
					case "casa":							
					if(palabraspintadas==i)
					{
						PintarPalabra (palabrasSpeech [i].ToString ());
						casa.SetActive(true);
						coroutineStarted2 = false;	

						coroutineStarted3 = false;
					}
						break;

					case "aparece":	
						if(palabraspintadas==i)
						{
						textoCompleto = true;
						DesactivarEscucha ();
						PintarPalabra (palabrasSpeech [i].ToString ());

						fraseEscena = coroutineStarted1 = "sin dudarlo ingresa sigilosamente";//para freezar contenedor				
						}
							break;

					case "dudarlo":							
							if(palabraspintadas==i)
							{
						PintarPalabra (palabrasSpeech [i].ToString ());

						player.gameObject.GetComponent<Animator> ().Play ("PlayerWalk2");
						coroutineStarted2 = false;	
							}
							




						break;

					case "sigilosamente":	
								if(palabraspintadas==i)
								{
						textoCompleto = true;
						DesactivarEscucha ();
						PintarPalabra (palabrasSpeech [i].ToString ());
						//ambienteBosque.Play ();
						//player.SetActive(false);
						coroutineStarted1 = "pero al parecer no se encuentra sola";//para freezar contenedor
								}
						break;

					case "parecer":	
									if(palabraspintadas==i)
									{
						PintarPalabra (palabrasSpeech [i].ToString ());



									}

						break;

					case "sola":	
										if(palabraspintadas==i)
										{
						textoCompleto = true;
						DesactivarEscucha ();
						pegaso.SetActive(true);
						pegaso.gameObject.GetComponent<Animator> ().Play ("PegasoVuelo2");
						ambienteBosque.Play ();

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
	public void OnPartialResultPalabraClave(string result) {

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
			case "casa":
				if(n == 0 && nroContenedor==0)
				{
					casa.SetActive(true);
					Pintar (palabrasSpeech [i].ToString ().Trim());			
					coroutineStarted2 = false;	

					coroutineStarted3 = false;
				}
				break;
			
			case "aparece":
				if(n == 1 && nroContenedor==0)
				{
					textoCompleto = true;		
					DesactivarEscucha ();
					Pintar (palabrasSpeech [i].ToString ().Trim());		
					nroContenedor=1;
					fraseEscena = coroutineStarted1 = "sin dudarlo ingresa sigilosamente";//para freezar contenedor			
				}
				break;

			case "dudarlo":
				if(n == 0 && nroContenedor==1)
				{
					player.gameObject.GetComponent<Animator> ().Play ("PlayerWalk2");
					Pintar (palabrasSpeech [i].ToString ().Trim());			
					coroutineStarted2 = false;	
				}
				break;
			case "sigilosamente":					
				if(n == 1 && nroContenedor==1)
				{
					textoCompleto = true;
					DesactivarEscucha ();
					Pintar (palabrasSpeech [i].ToString ().Trim());		
					nroContenedor=2;
					coroutineStarted1 = "pero al parecer no se encuentra sola";//para freezar contenedor


				}
				break;
			case "parecer":
				if(n == 0 && nroContenedor==2)
				{
					Pintar (palabrasSpeech [i].ToString ().Trim());			
				}
				break;
			case "sola":
				if(n == 1 && nroContenedor==2)
				{
					textoCompleto = true;
					DesactivarEscucha ();
					pegaso.SetActive(true);
					pegaso.gameObject.GetComponent<Animator> ().Play ("PegasoVuelo2");
					ambienteBosque.Play ();
					Pintar (palabrasSpeech [i].ToString ().Trim());			
					coroutineStarted = false;//para freezar ejecucion

				}
				break;

				default:					
					break;
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
	DesactivarEscucha();
			contenedorError.SetActive (true);
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
		microfono.gameObject.SetActive(false);

			if(fraseEscena == "una casa abandonada aparece")
			{
			casa.SetActive(false);
			
			}
			else
			{

				player.gameObject.GetComponent<Animator> ().Play ("PlayerIdle");
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
			StartCoroutine (EsperarSegundos (4f));

	if (!string.IsNullOrEmpty(coroutineStarted1))			
			StartCoroutine (RetrasarContenedor (0.5f, coroutineStarted1));	
		
		if (!coroutineStarted2)
			StartCoroutine (EfectoRelampago ());

		//resultErrores.text = casa.GetComponent<RectTransform> ().position.x.ToString ().Trim() ;

		if (!coroutineStarted3)
		{
			StartCoroutine (RetrasarParallax (2));

		}




	}  

	IEnumerator RetrasarParallax(int seconds)
	{
		coroutineStarted3 = true;
		yield return new WaitForSeconds(seconds);

		player.gameObject.GetComponent<Animator> ().Play ("PlayerIdle");
		efectoParallax = 0;
	}

	IEnumerator EfectoRelampago()
	{
		coroutineStarted2 = true;

		for (int z = 0; z < 5; z++)
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

	SceneManager.LoadScene("Cuento1Escena9Bis");
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
	microfono.gameObject.SetActive(true);
	SpeechRecognizer.StartRecording(true);
		contenedorError.SetActive (false);
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
