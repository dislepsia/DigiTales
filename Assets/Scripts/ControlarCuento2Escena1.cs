using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using KKSpeech;
using UnityEngine.SceneManagement;

public class ControlarCuento2Escena1 : MonoBehaviour 
{
	//GAMEOBJECTS PARA CONTROL DEL MICROFONO
	public Button startRecordingButton; //CONTROL-BUTTON MICROFONO INACTIVO
	public Button stopRecordingButton; //CONTROL-BUTTON PARA DESACTIVAR ESCUCHA MANUALMENTE
	bool stopRecording = false; //MANEJA DESACTIVACION MANUAL DE ESCUCHA 

	//GAMEOBJECTS PARA CONTROL DE RECONOCIMIENTO
	public Text sceneText; //CONTROL-TEXT DEL CONTENEDOR DE LA ESCENA ACTIVA
	public Text resultTextSpeech; //CONTROL-TEXT RECONOCIDO POR VOZ

	public Text resultErrores; //VARIABLE PARA VISUALIZAR ERROR(TESTING)

	//VARIABLES PARA MANEJAR TEXTO DE LA ESCENA ACTIVA
	private string textoEscena = string.Empty; //TEXTO DE LA ESCENA
	private string[] palabrasEscena = null; //ARRAY DE PALABRAS DE LA ESCENA 
	int cantPalabrasEscena = 0; //CANTIDAD DE PALABRAS DE LA ESCENA

	//VARIABLES PARA MANEJAR RECONOCIMIENTO DE VOZ DE LA ESCENA ACTIVA
	private string[] palabrasSpeech = null; //ARRAY DE PALABRAS RECONOCIDAS
	int cantPalabrasSpeech = 0; //CANTIDAD DE PALABRAS RECONOCIDAS

	//GAMEOBJECTS PARA MANEJAR SONIDOS
	private AudioSource ambienteBosque; //SONIDO DE FONDO
	public AudioClip CerditoOink; //CLIP DE CERDITO
	public AudioClip CerditoFeliz; //CLIP DE CERDITO

	//GAMEOBJECTS PARA MANEJAR PERSONAJES Y SUS ANIMACIONES
	public GameObject chanchoGranjero; 
	public GameObject chanchoCarpintero;
	public GameObject chanchaArquitecta;
	public GameObject bosque; 

	//GAMEOBJECTS PARA MANEJAR ANIMACIONES
	public GameObject polvo;
	public GameObject polvo1;
	public GameObject polvo2;

	//VARIABLES PARA CONTROL DE COLOREO
	int i=0; 
	int n=0; 
	int k=0; 
	int palabraspintadas=0; //CONTROLA INDICE DE PALABRA A PINTAR (ORDEN)
	int nroContenedor=0; //CONTROLA NRO DE CONTENEDOR ACTIVO

	//GAMEOBJECTS PARA MANEJAR ANIMACIONES
	public Animator imagenNegra; //CONTROLA EFECTO DE INICIO Y FIN DE ESCENA
	public Animator microfono; //CONTROLA EFECTO DE MICROFONO ACTIVO

	//GAMEOBJECTS PARA MANEJAR CONTENEDORES DE TEXTO
	public GameObject contenedor; //CONTROLA CONTENEDOR ACTIVO
	public GameObject contenedorError; //CONTROLA CONTENEDOR DE ERROR DE ESCUCHA

	//VARIABLES PARA CONTROL DE TIEMPOS
	bool coroutineStarted = true; //DETIENE LEVEMENTE LA EJECUCION AL FINALIZAR LA ESCENA
	string coroutineStarted1 = string.Empty; //DETIENE LEVEMENTE LA EJECUCION AL CAMBIAR DE CONTENEDOR

	string modoVibracion = string.Empty; //VARIABLE PARA CONTROL DE VIBRACION

	bool textoCompleto = false;//VARIABLE PARA CONTROLAR TEXTO RECONOCIDO TOTALMENTE


	/*METODO DE INICIO DE ESCENA*/
    void Start() 
	{ 
		Screen.orientation = ScreenOrientation.Landscape; //ROTO PANTALLA

		modoVibracion = PlayerPrefs.GetString ("ModoVibracion"); //SETEO MODO DE VIBRACION SELECCIONADO EN OPCIONES

		//INSTANCIO OBJETO DE RECONOCIMIENTO DE VOZ
		SpeechRecognizerListener listener = GameObject.FindObjectOfType<SpeechRecognizerListener>();			
		listener.onErrorDuringRecording.AddListener(OnError); //RESETEA ESCUCHA AL CABO DE UNOS SEGUNDOS DE INACTIVIDAD
		listener.onFinalResults.AddListener(OnFinalResult); //METODO QUE CAPTURA RESULTADO FINAL DE RECONOCIMIENTO

		//SETEO MODO DE RECONOCIMIENTO SELECCIONADO EN OPCIONES
		if(PlayerPrefs.GetString ("ModoReconocimiento") == "0")
			listener.onPartialResults.AddListener(OnPartialResult); //METODO QUE CAPTURA RESULTADO PARCIAL DE RECONOCIMIENTO
		else if(PlayerPrefs.GetString ("ModoReconocimiento") == "1")			
			listener.onPartialResults.AddListener(OnPartialResultPalabraClave); //METODO QUE CAPTURA RESULTADO PARCIAL DE RECONOCIMIENTO


		//VARIABLES PARA MANEJAR TEXTO DE LA ESCENA ACTIVA
		textoEscena = sceneText.text = "érase una vez en un bosque";
		palabrasEscena = textoEscena.Split(' ');
		cantPalabrasEscena = palabrasEscena.Length;

		//GAMEOBJECTS PARA MANEJAR SONIDOS
		ambienteBosque = GetComponent<AudioSource> ();						
		ambienteBosque.clip = CerditoOink;

		ActivarEscucha (); //ACTIVO ESCUCHA
		imagenNegra.Play("FadeIN"); //ACTIVO ANIMACION FADE
	}


	/*RESULTADO FINAL DEL RECONOCIMIENTO DE VOZ*/
	public void OnFinalResult(string result) 
	{		
		if (!stopRecording)
			ReiniciarValoresEscena ();
		else
			stopRecording = false;
	}

	/*RESULTADO PARCIAL DEL RECONOCIMIENTO DE VOZ*//*TIEMPO-REAL*/
	public void OnPartialResult(string result) 
	{
		//SI EL RECONOCIMIENTO NO SE DESACTIVO MANUALMENTE
		if(!stopRecording)
		{
			palabrasSpeech = result.ToLower().Split(' '); //OBTENGO ARRAY DE PALABRAS DE RECONOCIMIENTO PARCIAL DE VOZ
		    cantPalabrasSpeech = palabrasSpeech.Length; //OBTENGO CANTIDAD DE PALABRAS DE RECONOCIMIENTO PARCIAL DE VOZ
			//resultErrores.text = result.ToLower() + " " + cantPalabrasSpeech + palabrasSpeech [0].ToString ().Trim() + " "; //TESTING

			//INICIO COLOREO
			for (i = n; i < cantPalabrasSpeech && cantPalabrasSpeech <= cantPalabrasEscena; i++)
			{
				//SI LA PALABRA DE LA ESCENA ES LA PALABRA RECONOCIDA Y SUS POSICIONES
				if (string.Equals (palabrasSpeech [i].ToString ().Trim(), palabrasEscena [i].ToString ().Trim()))
				{
					//SEGUN PALABRA RECONOCIDA
					switch (palabrasSpeech [i].ToString ().Trim())
					{
						case "bosque":
							//SI LA POSICION DE LA PALABRA DE LA ESCENA ES IGUAL A LA RECONOCIDA
							if(palabraspintadas==i)
							{
								textoCompleto = true; //TEXTO DE CONTENEDOR RECONOCIDO
								DesactivarEscucha (); 
								PintarPalabra (palabrasSpeech [i].ToString ());
								bosque.SetActive (true); //ACTIVO ANIMACION DE BOSQUE
								coroutineStarted1 = "tres pequeños cerditos rosados"; //RETRASO SIGUIENTE CONTENEDOR Y CAMBIO TEXTO	
							}
							break;
						case "tres":
							if(palabraspintadas==i)
							{								
								PintarPalabra (palabrasSpeech [i].ToString ());
								//ACTIVO ANIMACIONES DE CHANCHITOS
								polvo.SetActive (true);
								polvo1.SetActive (true);
								polvo2.SetActive (true);
								ambienteBosque.Play ();	//ACTIVO SONIDO CHANCHITOS
							}
							break;
						case "rosados":
							if(palabraspintadas==i)
							{
								textoCompleto = true;
								DesactivarEscucha ();
								PintarPalabra (palabrasSpeech [i].ToString ());
								ambienteBosque.clip = CerditoFeliz; //INSTANCIO OTRO SONIDO
								coroutineStarted1 = "ellos vivían muy felices";
							}
							break;
						case "vivían":
							if(palabraspintadas==i)
							{
								//ACTIVO COMPONENTE ANIMATOR DE GAMEOBJECTS DE LOS CHANCHITOS
								chanchoGranjero.gameObject.GetComponent<Animator> ().enabled = true;
								chanchoCarpintero.gameObject.GetComponent<Animator> ().enabled = true;
								chanchaArquitecta.gameObject.GetComponent<Animator> ().enabled = true;
								//ACTIVOS ANIMACIONES DE CHANCHITOS
								chanchoGranjero.gameObject.GetComponent<Animator>().Play("ChanchoBaila");
								chanchoCarpintero.gameObject.GetComponent<Animator>().Play("ChanchoBaila1");
								chanchaArquitecta.gameObject.GetComponent<Animator>().Play("ChanchoBaila2");

								ambienteBosque.Play ();	//ACTIVO SONIDO CHANCHITOS
								PintarPalabra (palabrasSpeech [i].ToString ());
							}
							break;
						case "felices":
							if(palabraspintadas==i)
							{
								textoCompleto = true;
								DesactivarEscucha ();
								PintarPalabra (palabrasSpeech [i].ToString ());
								coroutineStarted = false; //RETRASO SIGUIENTE ESCENA	
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

	/*RESULTADO PARCIAL DEL RECONOCIMIENTO DE VOZ*//*PALABRA-CLAVE*/
	public void OnPartialResultPalabraClave(string result) 
	{
		if(!stopRecording)
		{
			palabrasSpeech = result.ToLower().Split(' ');
			cantPalabrasSpeech = palabrasSpeech.Length;
			//resultErrores.text = result.ToLower() + " " + cantPalabrasSpeech + palabrasSpeech [0].ToString ().Trim() + " ";

			for (i = k; i < cantPalabrasSpeech && cantPalabrasSpeech <= cantPalabrasEscena; i++)
			{
				switch (palabrasSpeech [i].ToString ().Trim())
				{				
					case "vez":
						//SI ES LA PRIMERA PALABRA CLAVE Y EL PRIMER CONTENEDOR
						if(n == 0  && nroContenedor==0)
						{
							Pintar (palabrasSpeech [i].ToString ().Trim ());	
						}
						break;
					case "bosque":					
						if(n == 1  && nroContenedor==0)
						{						
							textoCompleto = true;
							DesactivarEscucha ();
							Pintar (palabrasSpeech [i].ToString ().Trim ());
							bosque.SetActive (true);
							nroContenedor=1; //CAMBIO A SIGUIENTE CONTENEDOR
							coroutineStarted1 = "tres pequeños cerditos rosados";
						}
						break;
					case "tres":
						if(n == 0   && nroContenedor==1)
						{
							Pintar (palabrasSpeech [i].ToString ().Trim ());	
							polvo.SetActive (true);
							polvo1.SetActive (true);
							polvo2.SetActive (true);						
							ambienteBosque.Play ();	
						}
						break;
					case "rosados":					
						if(n == 1   && nroContenedor==1)
						{
							textoCompleto = true;
							DesactivarEscucha ();
							Pintar (palabrasSpeech [i].ToString ().Trim ());
							ambienteBosque.clip = CerditoFeliz;
							nroContenedor=2;
							coroutineStarted1 = "ellos vivían muy felices";					
						}
						break;
					case "vivían":					
						if(n == 0  && nroContenedor==2)
						{
							chanchoGranjero.gameObject.GetComponent<Animator> ().enabled = true;
							chanchoCarpintero.gameObject.GetComponent<Animator> ().enabled = true;
							chanchaArquitecta.gameObject.GetComponent<Animator> ().enabled = true;

							chanchoGranjero.gameObject.GetComponent<Animator>().Play("ChanchoBaila");
							chanchoCarpintero.gameObject.GetComponent<Animator>().Play("ChanchoBaila1");
							chanchaArquitecta.gameObject.GetComponent<Animator>().Play("ChanchoBaila2");

							ambienteBosque.Play ();	
							Pintar (palabrasSpeech [i].ToString ().Trim ());						
						}
						break;
					case "felices":					
						if(n == 1   && nroContenedor==2)
						{
							textoCompleto = true;
							DesactivarEscucha ();
							Pintar (palabrasSpeech [i].ToString ().Trim ());
							coroutineStarted = false;
						}
						break;
					default:					
						break;
				}
			}
		}
	}

	/*SIN RECONOCIMIENTO POR VARIOS SEGUNDOS*/
	public void OnError(string error) 
	{
		//SI EL RECONOCIMIENTO NO SE DESACTIVO MANUALMENTE
		if(!stopRecording)
		{
			DesactivarEscucha();
			contenedorError.SetActive (true); //MUESTRO ERROR
		}
		else
			stopRecording=false;
	}

	/*COMIENZA RECONOCIMIENTO DE VOZ*/
	public void OnStartRecordingPressed() 
	{
		//SI ESTA ACTIVA LA ESCUCHA
		if (SpeechRecognizer.IsRecording()) 
			DesactivarEscucha ();		 
		else 					
			ActivarEscucha ();		
	}

	/*COLOREA EN TIEMPO-REAL*/
	public void PintarPalabra(string palabra)
	{
		resultTextSpeech.text = resultTextSpeech.text + palabra + " "; 	
		n++; //PARA COMENZAR ITERACION LUEGO DE PALABRA RECONOCIDA
		palabraspintadas++; //INCREMENTA CANTIDAD DE PALABRAS PINTADAS
	}

	/*CAMBIA TEXTO DEL CONTENEDOR*/
	public void CambiarTexto(string textoNuevo)
	{	
		contenedor.SetActive (false); //OCULTO CONTENEDOR LEIDO

		//RESETEO VARIABLES DE COLOREO
		i = 0;
		n = 0;
		k = 0;
		palabraspintadas = 0;

		//PREPARO VARIABLES DE NUEVO CONTENEDOR DE ESCENA
		textoEscena = sceneText.text = textoNuevo;
		palabrasEscena = textoEscena.Split (' ');
		cantPalabrasEscena = palabrasEscena.Length;

		contenedor.SetActive (true); //ANIMACION CONTENEDOR NUEVO
		resultTextSpeech.text = string.Empty; //BORRO EL TEXTO RECONOCIDO PARA NUEVO CONTENEDOR

		textoCompleto = false; //TEXTO NO COMPLETO PARA NUEVO CONTENEDOR

		ActivarEscucha();
	}


	/*COLOREA EN PALABRA-CLAVE*/
	void Pintar(string palabraClave)
	{		
		n++; //CONTROLA ORDEN DE COLOREO DE PALABRA-CLAVE

		//MIENTRAS NO SEA LA PALABRA CLAVE 
		while (!string.Equals (palabrasEscena [k].ToString (), palabraClave)) 
		{
			resultTextSpeech.text = resultTextSpeech.text + palabrasEscena [k].ToString () + " "; //COLOREA
			k++; //INCREMENTO INDICE PARA COLOREO DE PALABRA-CLAVE
		}

		//COLOREA PALABRA CLAVE
		resultTextSpeech.text = resultTextSpeech.text + palabrasEscena [k].ToString () + " "; 	
		k++;
	}  

	/*REINICIO VALORES AL DESACTIVAR ESCUCHA*/
	public void ReiniciarValoresEscena() 
	{	
		//SI EL TEXTO NO SE COMPLETO
		if(!textoCompleto)
		{
			resultTextSpeech.text = string.Empty; //BLANQUEO CONTROL-TEXT DE RECONOCIMIENTO

			//RESETEO VARIABLES DE COLOREO
			i = 0;
			n = 0;
			k = 0;
			palabraspintadas = 0;

			startRecordingButton.gameObject.SetActive(true); //CONTROL-BUTTON MICROFONO ACTIVO
			stopRecordingButton.gameObject.SetActive(false); //CONTROL-BUTTON DESACTIVAR ESCUCHA MANUALMENTE INACTIVO
			microfono.gameObject.SetActive(false); //ANIMACION MICROFONO ACTIVO
			contenedorError.SetActive (true); //MUESTRO ERROR DE RECONOCIMIENTO
		}
	}

	/*METODO LLAMADO POR CADA FRAME*/
	void Update()
	{
		//SI TERMINO ESCENA
		if (!coroutineStarted)
			StartCoroutine (EsperarSegundos (0.5f));

		//SI HAY OTRO CONTENEDOR
		if (!string.IsNullOrEmpty(coroutineStarted1))	
			StartCoroutine (RetrasarContenedor (0.5f, coroutineStarted1));	
	}  

	/*METODO QUE RETRASA EJECUCION*/
	IEnumerator EsperarSegundos(float seconds)
	{
		coroutineStarted = true;
		yield return new WaitForSeconds(seconds); //SEGUNDOS A RETRASAR EJECUCION

		StartCoroutine (SpriteFadeOut()); //INICIO COROUTINE 
		StopCoroutine ("SpriteFadeOut"); //FINALIZO COROUTINE

		SceneManager.LoadScene("Cuento2Escena2"); //INICIO ESCENA SIGUIENTE
	}

	/*METODO PARA EFECTO FADE DE FIN DE ESCENA*/
	IEnumerator SpriteFadeOut()
	{	
		imagenNegra.Play("FadeOUT");
		yield return new WaitForSeconds(0.5f);
	}

	/*METODO QUE RETRASA CONTENEDOR*/
	IEnumerator RetrasarContenedor(float seconds, string frase)
	{		
		coroutineStarted1 = string.Empty;
		yield return new WaitForSeconds(seconds);

		CambiarTexto(frase); //PREPARO NUEVO CONTENEDOR
	}

	/*METODO QUE ACTIVA ESCUCHA*/
	public void ActivarEscucha() 
	{	
		startRecordingButton.gameObject.SetActive(false);
		stopRecordingButton.gameObject.SetActive(true);
		microfono.gameObject.SetActive(true);

		SpeechRecognizer.StartRecording(true); //EL OBJETO INSTANCIADO COMIENZA LA ESCUCHA

		contenedorError.SetActive (false);
	}

	/*METODO QUE DESACTIVA ESCUCHA*/
	public void DesactivarEscucha() 
	{	
		SpeechRecognizer.StopIfRecording (); //EL OBJETO INSTANCIADO DESACTIVA LA ESCUCHA SI ES Q ESTA ACTIVA

		startRecordingButton.gameObject.SetActive(true);
		stopRecordingButton.gameObject.SetActive(false);
		microfono.gameObject.SetActive(false);
	}

	/*METODO QUE ACTIVA VIBRACION*/
	public void Vibrar()
	{
		Handheld.Vibrate ();
	}

	/*METODO QUE CONTROLA BOTON VOLVER*/
	public void BotonVolver() 
	{	
		DesactivarEscucha();
		Screen.orientation = ScreenOrientation.Portrait;
		SceneManager.LoadScene("MiniJuego-NenaTemerosa-Modo");  
	}

	/*METODO QUE REINICIA VALORES DE ESCENA SI LA ESCUCHA FUE DESACTIVADA MANUALMENTE*/
	public void ReiniciarValoresStopEscucha() 
	{	
		resultTextSpeech.text = string.Empty;

		i = 0;
		n = 0;
		k = 0;
		palabraspintadas = 0;

		DesactivarEscucha ();
	}

	/*METODO QUE CONTROLA BOTON INVISIBLE QUE DESACTIVA LA ESCUCHA MANUALMENTE*/
	public void BotonPararEscucha() 
	{	
		stopRecording = true;	
		ReiniciarValoresStopEscucha();
	}
}
