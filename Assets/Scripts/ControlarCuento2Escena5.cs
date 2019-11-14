using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using KKSpeech;
using UnityEngine.SceneManagement;

public class ControlarCuento2Escena5 : MonoBehaviour {

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

	//variables de sonidos
	private AudioSource ambienteBosque;

	public GameObject chanchoGranjero; //objeto para controlar animacion de personaje
	public GameObject chanchoCarpintero;
	public GameObject chanchaArquitecta;
	public GameObject lobo;
	public GameObject lobo2;

	public GameObject bosque; //objeto para controlar escena

	public GameObject polvoLobo;

	public GameObject casaPaja; //objeto para controlar animacion de personaje
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

	bool coroutineStarted = true;//para freezar ejecucion
	string coroutineStarted1 = string.Empty;//para freezar contenedor

	bool coroutineStartedChanchos = true;


	bool coroutineStartedCasas = true;


	string modoVibracion = string.Empty; 

	int cambiarTexto = 0;

	bool textoCompleto = false;

	bool chanchosPuff = false;

	string escenaNro = string.Empty; 

    void Start() { 
		Screen.orientation = ScreenOrientation.Landscape;

		modoVibracion = PlayerPrefs.GetString ("ModoVibracion");

		//if (SpeechRecognizer.ExistsOnDevice()) {
			SpeechRecognizerListener listener = GameObject.FindObjectOfType<SpeechRecognizerListener>();
			//listener.onAuthorizationStatusFetched.AddListener(OnAuthorizationStatusFetched);//NO NECESARIO PARA ANDROID(YA DECLARADO EN MANIFEST)
			//listener.onAvailabilityChanged.AddListener(OnAvailabilityChange);//NO NECESARIO, SOLO IOS
			listener.onErrorDuringRecording.AddListener(OnError);//NECESARIO, CUANDO ESTA ACTIVO POR MAS DE 5S TIRA ERROR PARA RESETEARSE LA ESCUCHA
			//listener.onErrorOnStartRecording.AddListener(OnError);//NO NECESARIO, NO ME INTERESA ERROR
			listener.onFinalResults.AddListener(OnFinalResult);
		if(PlayerPrefs.GetString ("ModoReconocimiento") == "0")
			listener.onPartialResults.AddListener(OnPartialResult);
		else if(PlayerPrefs.GetString ("ModoReconocimiento") == "1")			
			listener.onPartialResults.AddListener(OnPartialResultPalabraClave);
			//listener.onEndOfSpeech.AddListener(OnEndOfSpeech);//NO NECESARIO, SE LLAMA ANTES DE ONFINALRESULT
			//startRecordingButton.enabled = false;
			//SpeechRecognizer.RequestAccess();//NO NECESARIO PARA ANDROID(YA DECLARADO EN MANIFEST)

			//obtengo cantidad de palabras de escena actual
			textoEscena = sceneText.text = "un lobo feroz aparece";
			palabrasEscena = textoEscena.Split(' ');
		cantPalabrasEscena = palabrasEscena.Length;

		//} else {			
			//resultErrores.text = "Sorry, but this device doesn't support speech recognition";
			//startRecordingButton.enabled = false;
		//}

		bosque.SetActive(true);
		//OnStartRecordingPressed ();
		//DesactivarEscucha ();
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
		//resultErrores.text = result.ToLower() + " " + cantPalabrasSpeech + palabrasSpeech [0].ToString ().Trim() + " " ;


////////////////////////////////////////////*COLOREO DE ORACION DE LA ESCENA*//*PALABRA-POR-PALABRA*////////////////////////////////////////////
		for (i = n; i < cantPalabrasSpeech && cantPalabrasSpeech <= cantPalabrasEscena; i++)
			{
			if (string.Equals (palabrasSpeech [i].ToString ().Trim(), palabrasEscena [i].ToString ().Trim()) /*&& n == i*/ )
					{
						//activar animacion segun palabra
						switch (palabrasSpeech [i].ToString ().Trim())
						{
				case "lobo":
					if(palabraspintadas==i)
					{
								
								PintarPalabra (palabrasSpeech [i].ToString ());
						polvoLobo.SetActive (true);

								
					}
								break;
				case "aparece":
					if(palabraspintadas==i)
					{
								
						textoCompleto = true;
						DesactivarEscucha ();
						PintarPalabra (palabrasSpeech [i].ToString ());

						escenaNro=coroutineStarted1 = "los cerditos corren aterrados";//para freezar contenedor


					}
								break;

				case "cerditos":
					if(palabraspintadas==i)
					{
						
						PintarPalabra (palabrasSpeech [i].ToString ());
						//lobo2.SetActive (true);
						//chanchoGranjero.SetActive (true);
						chanchoGranjero.gameObject.GetComponent<Animator> ().enabled =true;
						chanchoGranjero.gameObject.GetComponent<Animator>().Play("ChanchoHuye");
						//chanchoCarpintero.SetActive (true);
						chanchoCarpintero.gameObject.GetComponent<Animator> ().enabled =true;
						chanchoCarpintero.gameObject.GetComponent<Animator>().Play("ChanchoHuye1");
						//chanchaArquitecta.SetActive (true);
						chanchaArquitecta.gameObject.GetComponent<Animator> ().enabled =true;
						chanchaArquitecta.gameObject.GetComponent<Animator>().Play("ChanchoHuye2");





					}
					break;

				case "aterrados":
					if(palabraspintadas==i)
					{textoCompleto = true;
						DesactivarEscucha ();

						PintarPalabra (palabrasSpeech [i].ToString ());

						lobo2.SetActive (false);
						coroutineStarted1 = "y se refugian en sus casas";//para freezar contenedor



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

						//resultTextSpeech.text = resultTextSpeech.text + palabrasSpeech [i].ToString () + " "; //coloreo
						//n++; //para no tener en cuenta palabra coloreada en el bucle

						
				/*break;*/
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

		//resultErrores.text = result.ToLower() + " " + cantPalabrasSpeech + palabrasSpeech [0].ToString ().Trim() + " " ;
		for (i = k; i < cantPalabrasSpeech && cantPalabrasSpeech <= cantPalabrasEscena; i++)
		{
////////////////////////////////////////////*COLOREO DE ORACION DE LA ESCENA*//*POR-PALABRA-CLAVE*////////////////////////////////////////////				
			//activar animacion segun palabra
			//resultErrores.text = resultErrores.text + i + " " + k + " " + "/ ";
			switch (palabrasSpeech [i].ToString ().Trim())
			{
				
				case "lobo":
				if(n == 0  && nroContenedor==0)
					{
					Pintar (palabrasSpeech [i].ToString ().Trim ());	
					polvoLobo.SetActive (true);
					}
					break;
				case "aparece":					
				if(n == 1  && nroContenedor==0)
					{
						
					textoCompleto = true;
					DesactivarEscucha ();
					Pintar (palabrasSpeech [i].ToString ().Trim ());

					nroContenedor=1;
					//lobo.SetActive (false);
					escenaNro=coroutineStarted1 = "los cerditos corren aterrados";//para freezar contenedor
					}
					break;
			case "cerditos":
				if(n == 0   && nroContenedor==1)
					{
					Pintar (palabrasSpeech [i].ToString ().Trim ());	


					//lobo2.SetActive (true);
					//chanchoGranjero.SetActive (true);
					chanchoGranjero.gameObject.GetComponent<Animator> ().enabled =true;
					chanchoGranjero.gameObject.GetComponent<Animator>().Play("ChanchoHuye");
					//chanchoCarpintero.SetActive (true);
					chanchoCarpintero.gameObject.GetComponent<Animator> ().enabled =true;
					chanchoCarpintero.gameObject.GetComponent<Animator>().Play("ChanchoHuye1");
					//chanchaArquitecta.SetActive (true);
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
					coroutineStarted1 = "y se refugian en sus casas";//para freezar contenedor
						
					}
					break;
			case "refugian":					
				if(n == 0  && nroContenedor==2)
					{

					casaPaja.SetActive (true);
					chanchoGranjero.SetActive (true);
					chanchoGranjero.gameObject.GetComponent<Animator> ().enabled =true;
					chanchoGranjero.gameObject.GetComponent<Animator>().Play("ChanchoCasa");
					casaMadera.SetActive (true);
					chanchoCarpintero.SetActive (true);
					chanchoCarpintero.gameObject.GetComponent<Animator> ().enabled =true;
					chanchoCarpintero.gameObject.GetComponent<Animator>().Play("ChanchoCasa1");
					casaLadrillo.SetActive (true);
					chanchaArquitecta.SetActive (true);
					chanchaArquitecta.gameObject.GetComponent<Animator> ().enabled =true;
					chanchaArquitecta.gameObject.GetComponent<Animator>().Play("ChanchoCasa2");
				

						Pintar (palabrasSpeech [i].ToString ().Trim ());
						
					}
					break;
			case "casas":					
				if(n == 1   && nroContenedor==2)
				{
					textoCompleto = true;
					DesactivarEscucha ();
					Pintar (palabrasSpeech [i].ToString ().Trim ());



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
			case "bosque":
				bosque.SetActive (true);
				StartCoroutine (UsingYield ());
				StopCoroutine ("UsingYield");
				break;
			case "niña":
				player.SetActive (true);
				StartCoroutine (UsingYield2 ());
				StopCoroutine ("UsingYield2");
				break;
			case "misteriosa":
				StartCoroutine(UsingYield3());
				StopCoroutine ("UsingYield3");				
				coroutineStarted = false;//para freezar ejecucion
				break;

				default:					
					break;
		}	*/
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

	yield return new WaitForSeconds(seconds);
	casaPaja.SetActive (false);
	casaMadera.SetActive (true);
	chanchoCarpintero.SetActive (true);
	chanchoCarpintero.gameObject.GetComponent<Animator> ().enabled =true;
	chanchoCarpintero.gameObject.GetComponent<Animator>().Play("ChanchoCasa1");
	yield return new WaitForSeconds(seconds);
	casaMadera.SetActive (false);
	casaLadrillo.SetActive (true);
	chanchaArquitecta.SetActive (true);
	chanchaArquitecta.gameObject.GetComponent<Animator> ().enabled =true;
	chanchaArquitecta.gameObject.GetComponent<Animator>().Play("ChanchoCasa2");
	yield return new WaitForSeconds(seconds);
	casaLadrillo.SetActive (false);
	coroutineStarted = false;//para freezar ejecucion
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
	//player.gameObject.GetComponent<Animator>().Play("PlayerRun");
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
