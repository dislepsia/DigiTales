﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using KKSpeech;
using UnityEngine.SceneManagement;

public class ControlarCuento1Escena8 : MonoBehaviour {

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
	public AudioClip relincho;
	public AudioClip aleteo;
	private AudioSource ambienteBosque;


	public GameObject player; //objeto para controlar animacion de personaje
	public GameObject bosque; //objeto para controlar escena

	public GameObject pegaso; //objeto para controlar escena

	int i=0;
	int n=0;

	public Animator circuloNegro;
	public Animator microfono;
	//public Animator troncoEfecto;

	public GameObject contenedor;

	bool coroutineStarted = true;//para freezar ejecucion
	string coroutineStarted1 = string.Empty;//para freezar contenedor


	string modoRelato = string.Empty; 
	string modoVibracion = string.Empty; 

	int cambiarTexto = 0;

	bool textoCompleto = false;


    void Start() { 
		Screen.orientation = ScreenOrientation.Landscape;
		modoRelato = PlayerPrefs.GetString ("ModoReconocimiento");
		modoVibracion = PlayerPrefs.GetString ("ModoVibracion");


		SpeechRecognizerListener listener = GameObject.FindObjectOfType<SpeechRecognizerListener>();		
		listener.onErrorDuringRecording.AddListener(OnError);			
		listener.onFinalResults.AddListener(OnFinalResult);
		listener.onPartialResults.AddListener(OnPartialResult);


		//obtengo cantidad de palabras de escena actual
		textoEscena = sceneText.text = "con temor abre los ojos";
		palabrasEscena = textoEscena.Split(' ');

		//para q se reproduzca mas rapido, es sonido ya esta asignado
		ambienteBosque = GetComponent<AudioSource> ();						
		ambienteBosque.clip = relincho;
			

		//iniciar objetos
		player.SetActive(true);
		bosque.SetActive(true);


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
		//resultErrores.text = result.ToLower() + " " + cantPalabrasSpeech + palabrasSpeech [0].ToString ().Trim() + " ";

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
					case "abre":							
						
						PintarPalabra (palabrasSpeech [i].ToString ());

									
						break;

					case "ojos":							
						textoCompleto = true;
						DesactivarEscucha ();
						PintarPalabra (palabrasSpeech [i].ToString ());
						player.gameObject.GetComponent<Animator> ().enabled =true;
						player.gameObject.GetComponent<Animator>().Play("PlayerIdleInv");

						coroutineStarted1 = "al parecer la sombra es un pegaso";//para freezar contenedor				
						break;

					case "sombra":							

						PintarPalabra (palabrasSpeech [i].ToString ());

						break;

					case "pegaso":							
						textoCompleto = true;
						DesactivarEscucha ();
						PintarPalabra (palabrasSpeech [i].ToString ());
						pegaso.gameObject.GetComponent<Animator> ().enabled = true;
						pegaso.gameObject.GetComponent<Animator> ().Play ("PegasoEspera");
						ambienteBosque.Play ();
						coroutineStarted1 = "el cual parte rápidamente";//para freezar contenedor		

						break;

					case "parte":		
						PintarPalabra (palabrasSpeech [i].ToString ());
						ambienteBosque.clip = aleteo;
									

						break;

					case "rápidamente":							
						textoCompleto = true;
						DesactivarEscucha ();
						pegaso.gameObject.GetComponent<Animator>().Play("PegasoVuelo");
						ambienteBosque.Play ();
						coroutineStarted = false;//para freezar ejecucion
						PintarPalabra (palabrasSpeech [i].ToString ());				
						break;

						default:	
						PintarPalabra (palabrasSpeech [i].ToString ());
							break;
					}		

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
			case "luego":
				if(Pintar ("luego", 0))
				{
					
				}
				break;
			
			case "relámpagos":
				if(Pintar ("relámpagos", 1))
				{
					textoCompleto = true;		
					DesactivarEscucha ();
					coroutineStarted1 = "una tenebrosa sombra surge";//para freezar contenedor		
				}
				break;

			case "sombra":
				if(Pintar ("sombra", 0))
				{
					

					ambienteBosque.Play ();	

					player.gameObject.GetComponent<Animator> ().Play ("PlayerIdle");

				}
				break;
			case "surge":					
				if(Pintar ("surge", 1))
				{
					textoCompleto = true;
					DesactivarEscucha ();



				}
				break;
			case "cae":
				if(Pintar ("cae", 0))
				{
					player.gameObject.GetComponent<Animator> ().Play ("PlayerDie");


				}
				break;
			case "desmayada":
				if(Pintar ("desmayada", 1))
				{
					textoCompleto = true;
					DesactivarEscucha ();
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

public void OnError(string error) {
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
	
	textoCompleto = false;
	ActivarEscucha();			
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

	if (!coroutineStarted)
		StartCoroutine (EsperarSegundos (3));

	if (!string.IsNullOrEmpty(coroutineStarted1))			
		StartCoroutine (RetrasarContenedor (1, coroutineStarted1));	
		

	}  


	IEnumerator EsperarSegundos(int seconds)
	{
		coroutineStarted = true;
		yield return new WaitForSeconds(seconds);

		StartCoroutine (SpriteShapeOut());
		StopCoroutine ("SpriteShapeOut");

	SceneManager.LoadScene("Cuento1Escena7Bis");
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