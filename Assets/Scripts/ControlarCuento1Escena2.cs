using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using KKSpeech;
using UnityEngine.SceneManagement;

public class ControlarCuento1Escena2 : MonoBehaviour
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

	public AudioClip trueno;
	private AudioSource ambienteBosque;

	public GameObject player; 
	public GameObject bosque; 
	public GameObject bosqueInv; 

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
	bool coroutineStarted2 = true;

	string modoVibracion = string.Empty; 

	bool textoCompleto = false;


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

		textoEscena = sceneText.text = "arriba en el cielo";
		palabrasEscena = textoEscena.Split(' ');
		cantPalabrasEscena = palabrasEscena.Length;
			
		ambienteBosque = GetComponent<AudioSource> ();						
		ambienteBosque.clip = trueno;
			
		player.SetActive(true);
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
				if (string.Equals (palabrasSpeech [i].ToString ().Trim(), palabrasEscena [i].ToString ().Trim()) )
				{					
					switch (palabrasSpeech [i].ToString ().Trim())
					{
						case "cielo":
							if(palabraspintadas==i)
							{
								textoCompleto = true;
								DesactivarEscucha ();
								PintarPalabra (palabrasSpeech [i].ToString ());
								coroutineStarted1 = "la tormenta se acerca";
							}
							break;
						case "tormenta":
							if(palabraspintadas==i)
							{
								PintarPalabra (palabrasSpeech [i].ToString ());
								coroutineStarted2 = false;								
							}
							break;	
						case "acerca":
							if(palabraspintadas==i)
							{
								textoCompleto = true;
								DesactivarEscucha ();
								PintarPalabra (palabrasSpeech [i].ToString ());
								coroutineStarted1 = "con mucha fuerza";
							}
							break;
						case "fuerza":
							if(palabraspintadas==i)
							{
								textoCompleto = true;
								DesactivarEscucha ();
								coroutineStarted = false;
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


	public void OnPartialResultPalabraClave(string result)
	{
		if(!stopRecording)
		{
			palabrasSpeech = result.ToLower().Split(' ');
			cantPalabrasSpeech = palabrasSpeech.Length;
		
			for (i = k; i < cantPalabrasSpeech && cantPalabrasSpeech <= cantPalabrasEscena; i++)
			{			
				switch (palabrasSpeech [i].ToString ().Trim ())
				{
					case "arriba":
						if(n == 0 && nroContenedor==0)
							Pintar (palabrasSpeech [i].ToString ().Trim ());	
						break;
					case "cielo":
						if(n == 1 && nroContenedor==0)
						{
							textoCompleto = true;		
							DesactivarEscucha ();
							Pintar (palabrasSpeech [i].ToString ().Trim ());
							nroContenedor=1;
							coroutineStarted1 = "la tormenta se acerca";
						}
						break;
					case "tormenta":
						if(n == 0 && nroContenedor==1)
						{
							Pintar (palabrasSpeech [i].ToString ().Trim ());	
							coroutineStarted2 = false;
						}
						break;		
					case "acerca":
						if(n == 1 && nroContenedor==1)
						{
							textoCompleto = true;		
							DesactivarEscucha ();
							Pintar (palabrasSpeech [i].ToString ().Trim ());
							nroContenedor=2;
							coroutineStarted1 = "con mucha fuerza";	
						}
						break;
					case "con":
						if(n == 0 && nroContenedor==2)
							Pintar (palabrasSpeech [i].ToString ().Trim ());	
						break;
					case "fuerza":					
						if(n == 1 && nroContenedor==2)
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

		if (!coroutineStarted2)
			StartCoroutine (EfectoRelampago ());
	}  

	IEnumerator EfectoRelampago()
	{
		coroutineStarted2 = true;

		for (int z = 0; z < 2; z++)
		{
			bosqueInv.SetActive (true);
			yield return new WaitForSeconds(0.1f);
			bosqueInv.SetActive (false);
			yield return new WaitForSeconds(0.1f);
		}
		ambienteBosque.Play ();	
	}


	IEnumerator EsperarSegundos(float seconds)
	{
		coroutineStarted = true;
		yield return new WaitForSeconds(seconds);

		StartCoroutine (SpriteShapeOut());
		StopCoroutine ("SpriteShapeOut");

		SceneManager.LoadScene("Cuento1Escena3");
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
