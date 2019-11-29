using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using KKSpeech;
using UnityEngine.SceneManagement;

public class ControlarCuento1Escena5 : MonoBehaviour 
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

	int efectoParallax = 0;
	public float parallaxSpeed = 0.12f;

	public AudioClip viento;
	private AudioSource ambienteBosque;

	public GameObject player; 
	public GameObject bosque; 

	int i=0;
	int n=0;
	int k=0;
	int palabraspintadas=0;
	int nroContenedor=0;

	public Animator imagenNegra;
	public Animator microfono;
	public Animator troncoEfecto;

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
		else
			listener.onPartialResults.AddListener(OnPartialResultPalabraClave);

		textoEscena = sceneText.text = "a causa del viento";
		palabrasEscena = textoEscena.Split(' ');
		cantPalabrasEscena = palabrasEscena.Length;
					
		ambienteBosque = GetComponent<AudioSource> ();						
		ambienteBosque.clip = viento;
		ambienteBosque.Play ();		
					
		player.SetActive(true);
		bosque.SetActive(true);
		player.gameObject.GetComponent<Animator>().Play("PlayerRun");
		efectoParallax = 1;

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
						case "viento":	
							if(palabraspintadas==i)
							{
								textoCompleto = true;
								DesactivarEscucha ();
								PintarPalabra (palabrasSpeech [i].ToString ());
								coroutineStarted1 = "una rama cae al suelo";			
							}
							break;
						case "cae":	
							if(palabraspintadas==i)
							{
								PintarPalabra (palabrasSpeech [i].ToString ());
								troncoEfecto.gameObject.SetActive (true);	
								player.gameObject.GetComponent<Animator> ().Play ("PlayerIdle");
								efectoParallax = 0;	
							}
							break;	
						case "suelo":	
							if(palabraspintadas==i)
							{
								textoCompleto = true;
								DesactivarEscucha ();
								PintarPalabra (palabrasSpeech [i].ToString ());
								coroutineStarted1 = "bloqueando el camino";		
							}
							break;
						case "camino":	
							if(palabraspintadas==i)
							{
								textoCompleto = true;
								DesactivarEscucha ();								
								PintarPalabra (palabrasSpeech [i].ToString ());	
								coroutineStarted = false;
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
					case "causa":
						if(n == 0 && nroContenedor==0)
							Pintar (palabrasSpeech [i].ToString ().Trim());				
						break;
					case "viento":
						if(n == 1 && nroContenedor==0)
						{
							textoCompleto = true;		
							DesactivarEscucha ();
							Pintar (palabrasSpeech [i].ToString ().Trim());	
							nroContenedor = 1;
							coroutineStarted1 = "una rama cae al suelo";	
						}
						break;
					case "cae":	
						if(n == 0 && nroContenedor==1)
						{
							troncoEfecto.gameObject.SetActive(true);
							player.gameObject.GetComponent<Animator>().Play("PlayerIdle");
							efectoParallax = 0;	
							Pintar (palabrasSpeech [i].ToString ().Trim());	
						}
						break;	
					case "suelo":
						if(n == 1 && nroContenedor==1)
						{
							textoCompleto = true;		
							DesactivarEscucha ();
							Pintar (palabrasSpeech [i].ToString ().Trim());	
							nroContenedor = 2;
							coroutineStarted1 = "bloqueando el camino";	
						}
						break;
					case "bloqueando":
						if(n == 0 && nroContenedor==2)
							Pintar (palabrasSpeech [i].ToString ().Trim());				
						break;
					case "camino":					
						if(n == 1 && nroContenedor==2)
						{
							textoCompleto = true;
							DesactivarEscucha ();
							Pintar (palabrasSpeech [i].ToString ().Trim());	
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
		k = 0;
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

			troncoEfecto.gameObject.SetActive(false);
			player.gameObject.GetComponent<Animator> ().Play ("PlayerRun");
			efectoParallax = 1;
			contenedorError.SetActive (true);
		}
	}


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
		StartCoroutine (EsperarSegundos (0.5f));

		if (!string.IsNullOrEmpty(coroutineStarted1))			
			StartCoroutine (RetrasarContenedor (0.5f, coroutineStarted1));

		if (!ambienteBosque.isPlaying)
			ambienteBosque.Play ();		
	}  


	IEnumerator EsperarSegundos(float seconds)
	{
		coroutineStarted = true;
		yield return new WaitForSeconds(seconds);

		StartCoroutine (SpriteShapeOut());
		StopCoroutine ("SpriteShapeOut");

		SceneManager.LoadScene("Cuento1Escena6");
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
