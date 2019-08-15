using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using KKSpeech;
using UnityEngine.SceneManagement;

public class RecordingCanvas : MonoBehaviour {

	public Button startRecordingButton;

	public Text sceneText; //oracion propia de la escena

	//public Text coincidencia; //coincidencia de palabra en escena y pronunciada
	//public Text cantAccesos; //cantidad de veces que se accede a metodo OnPartialResult
	public Text resultTextSpeech; //texto reconocido por voz
	public Text resultErrores; //visualizar error

	//variables para trabajar sceneText
	private string textoEscena = string.Empty; 
	private string[] palabrasEscena = null; 
	int cantPalabrasEscena = 0; 

	//variables para trabajar result(reconocimiento parcial de voz)
	private string[] palabrasSpeech = null;
	int cantPalabrasSpeech = 0;

	//variables para controlar coloreo
	int i = 0;
	int n = 0;

	//variables para efectoParallax
	int efectoParallax = 0;
	public float parallaxSpeed = 0.12f;

	//variables de sonidos
	public AudioClip buho;
	public AudioClip grito;
	public AudioClip trueno;
	private AudioSource ambienteBosque;

	//int count = 0;

	public GameObject player; //objeto para controlar animacion de personaje
	public GameObject bosque; //objeto para controlar escena

	string LevelName = string.Empty;
	private string[] palabrasClave = null; 
	int cantPalabrasClave = 0;
	int k=0;
	int j=0;


	public Animator imagenNegra;
	public Animator circuloNegro;
	public Animator microfono;

	int contadorUsing=0;
	int contadorUsing2=0;
	int contadorUsing3=0;


    void Start() { 
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
			textoEscena = sceneText.text;
			palabrasEscena = textoEscena.Split(' ');
			cantPalabrasEscena = palabrasEscena.Length;

			LevelName = Application.loadedLevelName;

			ambienteBosque = GetComponent<AudioSource> ();
			/*ambienteBosque.clip = buho;
			ambienteBosque.Play ();*/

			if (LevelName == "RelatarCuento2") 
			{
				palabrasClave = new string[3]{"bosque","niña","misteriosa"};
				cantPalabrasClave = 3;
			}else if (LevelName == "RelatarCuento")
			{				
				ambienteBosque.clip = trueno;//para q se reproduzca mas rapido, es sonido ya esta asignado
				palabrasClave = new string[3]{"tormenta","inevitable","oscuridad"};
				cantPalabrasClave = 3;

				player.SetActive(true);
				bosque.SetActive(true);
			}else if (LevelName == "RelatarCuento3")
			{				
				ambienteBosque.clip = buho;//para q se reproduzca mas rapido, es sonido ya esta asignado
				palabrasClave = new string[3]{"comenzó","búho","cantar"};
				cantPalabrasClave = 3;

				player.SetActive(true);
				bosque.SetActive(true);
			}
			else if (LevelName == "RelatarCuento4")
			{
				ambienteBosque.clip = grito;//para q se reproduzca mas rapido, es sonido ya esta asignado
				palabrasClave = new string[3]{"grito","entonces","correr"};
				cantPalabrasClave = 3;

				player.SetActive(true);
				bosque.SetActive(true);
			}else if (LevelName == "RelatarCuento5")
			{
				
				palabrasClave = new string[3]{"adrenalina","varios","agilmente"};
				cantPalabrasClave = 3;

				player.SetActive(true);
				bosque.SetActive(true);

				player.SendMessage ("UpdateState", "PlayerRun");
				efectoParallax = 1;
			}






			//coincidencia.text = coincidencia.text + " " + cantPalabrasEscena.ToString()+ " " + cantPalabrasSpeech.ToString()+ " " + i.ToString()+ " " + n.ToString();

		} else {			
			resultErrores.text = "Sorry, but this device doesn't support speech recognition";
			startRecordingButton.enabled = false;
		}



	}

	/*RESULTADO FINAL DEL RECONOCIMIENTO DE VOZ*/
	public void OnFinalResult(string result) {
		//resultText.text = result;
		ReiniciarValoresEscena();
	}

	/*RESULTADO PARCIAL DEL RECONOCIMIENTO DE VOZ*/
	public void OnPartialResult(string result) {

		//obtengo cantidad de palabras de reconocimiento parcial de voz
		palabrasSpeech = result.ToLower().Split(' ');
		cantPalabrasSpeech = palabrasSpeech.Length;

////////////////////////////////////////////*COLOREO DE ORACION DE LA ESCENA*//*PALABRA-POR-PALABRA*////////////////////////////////////////////
			/*for (i = n; i < cantPalabrasSpeech; i++)
			{
				if (string.Equals (palabrasSpeech [i].ToString ().Trim(), palabrasEscena [i].ToString ().Trim()))
				{
					//activar animacion segun palabra
					switch (palabrasSpeech [i].ToString ().Trim())
					{
						case "bosque":
							bosque.SetActive(true);
							break;
						case "nena":
							player.SetActive(true);
							break;
						case "temerosa":
							SceneManager.LoadScene("RelatarCuento");
							player.SetActive(true);
							bosque.SetActive(true);
							break;
						case "correr":
							player.SendMessage ("UpdateState", "PlayerRun");
							efectoParallax = 1;
							break;
						case "grito":					
							//ambienteBosque.clip = grito;
							ambienteBosque.Play ();
							//Handheld.Vibrate();
							break;
						case "se":					
							//ambienteBosque.clip = grito;
							//ambienteBosque.Play ();
							Handheld.Vibrate();//vibracion en proxima palabra para que se reproduzca casi simultaneamente
							break;				
						default:					
							break;
					}

					resultTextSpeech.text = resultTextSpeech.text + palabrasSpeech [i].ToString () + " "; //coloreo
					n++; //para no tener en cuenta palabra coloreada en el bucle

					//resultErrores.text = "OK";

					break;
				}
			//else 
				//resultErrores.text = "Palabra no reconocida";
			}*/


////////////////////////////////////////////*COLOREO DE ORACION DE LA ESCENA*//*POR-PALABRA-CLAVE*////////////////////////////////////////////
		/*if (string.Equals (palabrasSpeech [cantPalabrasSpeech-1].ToString ().Trim(), palabrasClave [k].ToString ().Trim()))
		{			
			//activar animacion segun palabra
			switch (palabrasSpeech [cantPalabrasSpeech-1].ToString ().Trim())
			{
				case "bosque":
					bosque.SetActive(true);
					break;
				case "nena":
					player.SetActive(true);
					break;
				case "temerosa":
					StartCoroutine (SpriteFadeOut());					
					break;
				case "correr":
					player.SendMessage ("UpdateState", "PlayerRun");
					efectoParallax = 1;
					StartCoroutine (SpriteShapeOut());
					break;
				case "grito":					
					//ambienteBosque.clip = grito;
					ambienteBosque.Play ();
					//Handheld.Vibrate();
					break;
				case "se":					
					//ambienteBosque.clip = grito;
					//ambienteBosque.Play ();
					Handheld.Vibrate();//vibracion en proxima palabra para que se reproduzca casi simultaneamente
					break;
				default:					
					break;
			}
			//StartCoroutine(UsingYield(0.5f));

			while(!string.Equals (palabrasEscena [j].ToString (), palabrasClave [k].ToString ().Trim()))
			{
				resultTextSpeech.text = resultTextSpeech.text + palabrasEscena [j].ToString () + " "; //coloreo
				j++;					
			}

			resultTextSpeech.text = resultTextSpeech.text + palabrasEscena [j].ToString () + " "; //coloreo
			j++;
			k++;


		//else 
			//resultErrores.text = "Palabra no reconocida";
		}
		*/


////////////////////////////////////////////*COLOREO DE ORACION DE LA ESCENA*//*POR-PALABRA-CLAVE(PSEUDO-REAL-TIME)*////////////////////////////////////////////					
			//activar animacion segun palabra
			switch (palabrasSpeech [cantPalabrasSpeech-1].ToString ().Trim())
			{
		case "bosque":
				bosque.SetActive (true);

				StartCoroutine (UsingYield ());
				StopCoroutine ("UsingYield");
				break;
			case "niña":
				player.SetActive(true);
				
				StartCoroutine(UsingYield2());
				StopCoroutine ("UsingYield2");
				break;
			case "misteriosa":
				
				
				StartCoroutine(UsingYield3());
				StopCoroutine ("UsingYield3");

			StartCoroutine(esperar());
			StopCoroutine ("esperar");

			StartCoroutine (SpriteFadeOut());
			StopCoroutine ("SpriteFadeOut");

			SceneManager.LoadScene("RelatarCuento");
				break;



		case "tormenta":
			ambienteBosque.Play ();

			StartCoroutine(UsingYield2());
			StopCoroutine ("UsingYield2");
			break;
		case "inevitable":	

			StartCoroutine (UsingYield ());
			StopCoroutine ("UsingYield");
			break;
		
		case "oscuridad":
			

			StartCoroutine(UsingYield3());
			StopCoroutine ("UsingYield3");

			StartCoroutine(esperar());
			StopCoroutine ("esperar");

			StartCoroutine (SpriteShapeOut());
			StopCoroutine ("SpriteShapeOut");

			SceneManager.LoadScene("RelatarCuento3");
			break;






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

			StartCoroutine(esperar());
			StopCoroutine ("esperar");

			StartCoroutine (SpriteShapeOut());
			StopCoroutine ("SpriteShapeOut");

			SceneManager.LoadScene("RelatarCuento4");
			break;







			case "grito":					
				//ambienteBosque.clip = grito;
				ambienteBosque.Play ();
				Handheld.Vibrate();

				StartCoroutine(UsingYield());
				StopCoroutine ("UsingYield");
				break;
			case "entonces":					
				//ambienteBosque.clip = grito;
				//ambienteBosque.Play ();
				//Handheld.Vibrate();//vibracion en proxima palabra para que se reproduzca casi simultaneamente

				StartCoroutine(UsingYield2());
				StopCoroutine ("UsingYield2");
				break;
			case "correr":
				player.SendMessage ("UpdateState", "PlayerRun");
				efectoParallax = 1;
				

				StartCoroutine(UsingYield3());
				StopCoroutine ("UsingYield3");

			StartCoroutine(esperar());
			StopCoroutine ("esperar");

			StartCoroutine (SpriteShapeOut());

			SceneManager.LoadScene("RelatarCuento5");
				break;



		case "adrenalina":			

			StartCoroutine(UsingYield());
			StopCoroutine ("UsingYield");
			break;
		case "varios":			

			StartCoroutine(UsingYield2());
			StopCoroutine ("UsingYield2");
			break;
		case "agilmente":
			


			StartCoroutine(UsingYield3());
			StopCoroutine ("UsingYield3");

			StartCoroutine(esperar());
			StopCoroutine ("esperar");

			StartCoroutine (SpriteShapeOut());

			break;




			default:					
				break;
			}

		//coincidencia.text = coincidencia.text + result + " ";
		//count++;
		//cantAccesos.text = count.ToString();
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

	public void ReiniciarValoresEscena() {		
		//resultTextSpeech.text = string.Empty;
		//resultErrores.text = string.Empty;
		i = 0;
		n = 0;

		j = 0;
		k = 0;

		contadorUsing = 0;
		contadorUsing2 = 0;
		contadorUsing3 = 0;

		startRecordingButton.gameObject.SetActive(true);
		microfono.gameObject.SetActive(false);
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


	}  

	IEnumerator UsingYield()
	{
		contadorUsing ++;
		if (contadorUsing == 1)
		{	
			
			while(!string.Equals (palabrasEscena [j].ToString (), palabrasClave [k].ToString ().Trim()))
			{
				resultTextSpeech.text = resultTextSpeech.text + palabrasEscena [j].ToString () + " "; //coloreo
				j++;	
				yield return new WaitForSeconds(0.03f);
			}

			resultTextSpeech.text = resultTextSpeech.text + palabrasEscena [j].ToString () + " "; //coloreo
			j++;
			k++;


		}
	}

	IEnumerator UsingYield2()
	{
		contadorUsing2 ++;
		if (contadorUsing2 == 1)
		{	

			while(!string.Equals (palabrasEscena [j].ToString (), palabrasClave [k].ToString ().Trim()))
			{
				resultTextSpeech.text = resultTextSpeech.text + palabrasEscena [j].ToString () + " "; //coloreo
				j++;	
				yield return new WaitForSeconds(0.03f);
			}

			resultTextSpeech.text = resultTextSpeech.text + palabrasEscena [j].ToString () + " "; //coloreo
			j++;
			k++;


		}
	}

	IEnumerator UsingYield3()
	{
		contadorUsing3 ++;
		if (contadorUsing3 == 1)
		{	

			while(!string.Equals (palabrasEscena [j].ToString (), palabrasClave [k].ToString ().Trim()))
			{
				resultTextSpeech.text = resultTextSpeech.text + palabrasEscena [j].ToString () + " "; //coloreo
				j++;	
				yield return new WaitForSeconds(0.03f);
			}

			resultTextSpeech.text = resultTextSpeech.text + palabrasEscena [j].ToString () + " "; //coloreo
			j++;
			k++;


		}
	}

	IEnumerator SpriteFadeOut()
	{		
		imagenNegra.SetTrigger ("end");
		yield return new WaitForSeconds(1f);



	}

	IEnumerator SpriteShapeOut()
	{		
		circuloNegro.SetTrigger ("end");
		yield return new WaitForSeconds(1f);
	}


	IEnumerator esperar()
	{		
		
		yield return new WaitForSeconds(4f);



	}
}
