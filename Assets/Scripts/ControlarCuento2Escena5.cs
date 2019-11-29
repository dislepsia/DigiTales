using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using KKSpeech;
using UnityEngine.SceneManagement;

public class ControlarCuento2Escena5 : MonoBehaviour 
{
	public Button startRecordingButton;
	public Button stopRecordingButton;
	bool stopRecording = false;

	public Text sceneText; 
	public Text resultTextSpeech; 

	public Text resultErrores; 

	private string textoEscena = string.Empty; 
	private string[] palabrasEscena = null; 
	int cantPalabrasEscena = 0;

	private string[] palabrasSpeech = null;
	int cantPalabrasSpeech = 0;

	private AudioSource ambienteBosque;
	public AudioClip LoboMalo;
	public AudioClip CerditoChillido;
	public AudioClip GolpePuerta;

	public GameObject chanchoGranjero; 
	public GameObject chanchoCarpintero;
	public GameObject chanchaArquitecta;
	public GameObject lobo;
	public GameObject lobo2;

	public GameObject bosque;
	public GameObject polvoLobo;

	public GameObject casaPaja; 
	public GameObject casaMadera;
	public GameObject casaLadrillo;

	int i=0;
	int n=0;
	int k=0;
	int palabraspintadas=0;
	int nroContenedor=0;

	public Animator imagenNegra;
	public Animator microfono;

	public GameObject contenedor;
	public GameObject contenedorError;

	bool coroutineStarted = true;
	string coroutineStarted1 = string.Empty;
	bool coroutineStartedCasas = true;

	string modoVibracion = string.Empty; 

	bool textoCompleto = false;

	string escenaNro = string.Empty; 

	bool hayLobo = false;


    void Start() 
	{ 
		Screen.orientation = ScreenOrientation.Landscape;

		modoVibracion = PlayerPrefs.GetString ("ModoVibracion");
	
		SpeechRecognizerListener listener = GameObject.FindObjectOfType<SpeechRecognizerListener>();
		listener.onErrorDuringRecording.AddListener(OnError);			
		listener.onFinalResults.AddListener(OnFinalResult);
		if(PlayerPrefs.GetString ("ModoReconocimiento") == "0")
			listener.onPartialResults.AddListener(OnPartialResult);
		else if(PlayerPrefs.GetString ("ModoReconocimiento") == "1")			
			listener.onPartialResults.AddListener(OnPartialResultPalabraClave);

		textoEscena = sceneText.text = "un lobo feroz aparece";
		palabrasEscena = textoEscena.Split(' ');
		cantPalabrasEscena = palabrasEscena.Length;

		ambienteBosque = GetComponent<AudioSource> ();						
		ambienteBosque.clip = LoboMalo;

		bosque.SetActive(true);

		ActivarEscucha ();
		imagenNegra.Play("FadeIN");
	}

	public void OnFinalResult(string result) 
	{		
		if (!stopRecording)
			ReiniciarValoresEscena ();
		else
			stopRecording = false;
	}


	public void OnPartialResult(string result) 
	{
		if(!stopRecording)
		{
			palabrasSpeech = result.ToLower().Split(' ');
			cantPalabrasSpeech = palabrasSpeech.Length;
		
			for (i = n; i < cantPalabrasSpeech && cantPalabrasSpeech <= cantPalabrasEscena; i++)
			{
				if (string.Equals (palabrasSpeech [i].ToString ().Trim(), palabrasEscena [i].ToString ().Trim()))
				{						
					switch (palabrasSpeech [i].ToString ().Trim())
					{
						case "lobo":
							if(palabraspintadas==i)
							{								
								PintarPalabra (palabrasSpeech [i].ToString ());
								if(modoVibracion == "0")
									Handheld.Vibrate ();
								ambienteBosque.Play ();	
								if(hayLobo==false)
								{
									polvoLobo.SetActive (true);
									hayLobo = true;
								}
							}
							break;
						case "aparece":
							if(palabraspintadas==i)
							{								
								textoCompleto = true;
								DesactivarEscucha ();
								PintarPalabra (palabrasSpeech [i].ToString ());
								ambienteBosque.clip = CerditoChillido;
								escenaNro=coroutineStarted1 = "los cerditos corren aterrados";
							}
							break;
						case "cerditos":
							if(palabraspintadas==i)
							{						
								PintarPalabra (palabrasSpeech [i].ToString ());
								ambienteBosque.Play ();		

								chanchoGranjero.gameObject.GetComponent<Animator> ().enabled =true;
								chanchoGranjero.gameObject.GetComponent<Animator>().Play("ChanchoHuye");						
								chanchoCarpintero.gameObject.GetComponent<Animator> ().enabled =true;
								chanchoCarpintero.gameObject.GetComponent<Animator>().Play("ChanchoHuye1");						
								chanchaArquitecta.gameObject.GetComponent<Animator> ().enabled =true;
								chanchaArquitecta.gameObject.GetComponent<Animator>().Play("ChanchoHuye2");
							}
							break;
						case "aterrados":
							if(palabraspintadas==i)
							{
								textoCompleto = true;
								DesactivarEscucha ();
								PintarPalabra (palabrasSpeech [i].ToString ());
								lobo2.SetActive (false);
								ambienteBosque.clip = GolpePuerta;
								coroutineStarted1 = "y se refugian en sus casas";
							}
							break;
						case "refugian":
							if(palabraspintadas==i)
							{
								PintarPalabra (palabrasSpeech [i].ToString ());
							}
							break;
						case "casas":
							if(palabraspintadas==i)
							{
								textoCompleto = true;
								DesactivarEscucha ();
								PintarPalabra (palabrasSpeech [i].ToString ());
								coroutineStartedCasas= false;
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

	public void OnPartialResultPalabraClave(string result) 
	{
		if(!stopRecording)
		{
			palabrasSpeech = result.ToLower().Split(' ');
			cantPalabrasSpeech = palabrasSpeech.Length;
		
			for (i = k; i < cantPalabrasSpeech && cantPalabrasSpeech <= cantPalabrasEscena; i++)
			{
				switch (palabrasSpeech [i].ToString ().Trim())
				{				
					case "lobo":
						if(n == 0  && nroContenedor==0)
						{
							Pintar (palabrasSpeech [i].ToString ().Trim ());
							if(modoVibracion == "0")
								Handheld.Vibrate ();
							ambienteBosque.Play ();	
							if(hayLobo==false)
							{
								polvoLobo.SetActive (true);
								hayLobo = true;
							}
						}
						break;
					case "aparece":					
						if(n == 1  && nroContenedor==0)
						{						
							textoCompleto = true;
							DesactivarEscucha ();
							Pintar (palabrasSpeech [i].ToString ().Trim ());
							nroContenedor=1;					
							ambienteBosque.clip = CerditoChillido;
							escenaNro=coroutineStarted1 = "los cerditos corren aterrados";
						}
						break;
					case "cerditos":
						if(n == 0   && nroContenedor==1)
						{
							Pintar (palabrasSpeech [i].ToString ().Trim ());					
							ambienteBosque.Play ();	

							chanchoGranjero.gameObject.GetComponent<Animator> ().enabled =true;
							chanchoGranjero.gameObject.GetComponent<Animator>().Play("ChanchoHuye");
							
							chanchoCarpintero.gameObject.GetComponent<Animator> ().enabled =true;
							chanchoCarpintero.gameObject.GetComponent<Animator>().Play("ChanchoHuye1");
						
							chanchaArquitecta.gameObject.GetComponent<Animator> ().enabled =true;
							chanchaArquitecta.gameObject.GetComponent<Animator>().Play("ChanchoHuye2");						
						}
						break;
					case "aterrados":					
						if(n == 1   && nroContenedor==1)
						{
							textoCompleto = true;
							DesactivarEscucha ();
							Pintar (palabrasSpeech [i].ToString ().Trim ());
							lobo2.SetActive (false);
							nroContenedor=2;
							ambienteBosque.clip = GolpePuerta;
							coroutineStarted1 = "y se refugian en sus casas";						
						}
						break;
					case "refugian":					
						if(n == 0  && nroContenedor==2)
						{
							Pintar (palabrasSpeech [i].ToString ().Trim ());						
						}
						break;
					case "casas":					
						if(n == 1   && nroContenedor==2)
						{
							textoCompleto = true;
							DesactivarEscucha ();
							Pintar (palabrasSpeech [i].ToString ().Trim ());
							coroutineStartedCasas= false;
						}
						break;
					default:					
						break;
				}
			}
		}
	}

	public void OnError(string error) 
	{
		if(!stopRecording)
		{
			DesactivarEscucha();
			contenedorError.SetActive (true);
		}
		else
			stopRecording=false;
	}

	public void OnStartRecordingPressed()
	{
		if (SpeechRecognizer.IsRecording()) 
			DesactivarEscucha ();
		 else 			
			ActivarEscucha ();		
	}

	public void PintarPalabra(string palabra)
	{
		resultTextSpeech.text = resultTextSpeech.text + palabra + " "; 	
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

		contenedor.SetActive (true);
		resultTextSpeech.text = string.Empty;

		textoCompleto = false;

		if(escenaNro != string.Empty)
		{
			escenaNro = string.Empty;
			lobo.SetActive (false);
			lobo2.SetActive (true);
			chanchoGranjero.SetActive (true);
			chanchoCarpintero.SetActive (true);
			chanchaArquitecta.SetActive (true);
		}

		ActivarEscucha();
	}

	void Pintar(string palabraClave)
	{		
		n++;
		while (!string.Equals (palabrasEscena [k].ToString (), palabraClave))
		{
			resultTextSpeech.text = resultTextSpeech.text + palabrasEscena [k].ToString () + " "; 
				k++;
		}

		resultTextSpeech.text = resultTextSpeech.text + palabrasEscena [k].ToString () + " "; 
		k++;
	}  


	public void ReiniciarValoresEscena() 
	{	
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


	void Update()
	{
		if (!coroutineStarted)
		StartCoroutine (EsperarSegundos (0.5f));

		if (!string.IsNullOrEmpty(coroutineStarted1))	
				StartCoroutine (RetrasarContenedor (0.5f, coroutineStarted1));

		if (!coroutineStartedCasas)
			StartCoroutine (ChanchosCasas (1f));	
	}  


	IEnumerator ChanchosCasas(float seconds)
	{
		coroutineStartedCasas = true;

		casaPaja.SetActive (true);
		chanchoGranjero.SetActive (true);
		chanchoGranjero.gameObject.GetComponent<Animator> ().enabled =true;
		chanchoGranjero.gameObject.GetComponent<Animator>().Play("ChanchoCasa");
		ambienteBosque.Play ();

		yield return new WaitForSeconds(seconds);
		casaPaja.SetActive (false);
		casaMadera.SetActive (true);
		chanchoCarpintero.SetActive (true);
		chanchoCarpintero.gameObject.GetComponent<Animator> ().enabled =true;
		chanchoCarpintero.gameObject.GetComponent<Animator>().Play("ChanchoCasa1");
		ambienteBosque.Play ();

		yield return new WaitForSeconds(seconds);
		casaMadera.SetActive (false);
		casaLadrillo.SetActive (true);
		chanchaArquitecta.SetActive (true);
		chanchaArquitecta.gameObject.GetComponent<Animator> ().enabled =true;
		chanchaArquitecta.gameObject.GetComponent<Animator>().Play("ChanchoCasa2");
		ambienteBosque.Play ();

		yield return new WaitForSeconds(seconds);
		casaLadrillo.SetActive (false);
		coroutineStarted = false;
	}

	IEnumerator EsperarSegundos(float seconds)
	{
		coroutineStarted = true;
		yield return new WaitForSeconds(seconds);

		StartCoroutine (SpriteFadeOut());
		StopCoroutine ("SpriteFadeOut");

		SceneManager.LoadScene("Cuento2Escena5bis");
	}

	IEnumerator SpriteFadeOut()
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

	public void ActivarEscucha() 
	{	
		startRecordingButton.gameObject.SetActive(false);
		stopRecordingButton.gameObject.SetActive(true);
		microfono.gameObject.SetActive(true);
		SpeechRecognizer.StartRecording(true);

		contenedorError.SetActive (false);
	}

	public void DesactivarEscucha()
	{	
		SpeechRecognizer.StopIfRecording ();
		startRecordingButton.gameObject.SetActive(true);
		stopRecordingButton.gameObject.SetActive(false);
		microfono.gameObject.SetActive(false);
	}

	public void Vibrar()
	{
		Handheld.Vibrate ();
	}

	public void BotonVolver() 
	{	
		DesactivarEscucha();
		Screen.orientation = ScreenOrientation.Portrait;
		SceneManager.LoadScene("MiniJuego-NenaTemerosa-Modo");  
	}

	public void ReiniciarValoresStopEscucha() 
	{	
		resultTextSpeech.text = string.Empty;

		i=0;
		n=0;
		k=0;
		palabraspintadas = 0;

		DesactivarEscucha ();
	}

	public void BotonPararEscucha()
	{	
		stopRecording = true;	
		ReiniciarValoresStopEscucha();
	}
}
