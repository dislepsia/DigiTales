using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using KKSpeech;
using TMPro;
using UnityEngine.SceneManagement;

public class ControlarJuegoAprendizaje : MonoBehaviour {

	public Button startRecordingButton;


	public GameObject sceneText;


	public Text resultErrores; //visualizar error

	//variables para trabajar sceneText
	private string textoEscena = string.Empty; 
	private string[] palabrasEscena = null; 

	//variables para trabajar result(reconocimiento parcial de voz)
	private string[] palabrasSpeech = null;
	int cantPalabrasSpeech = 0;

	//variables de sonidos
	private AudioSource ambienteBosque;



	public Animator microfono;
	public Animator imagenNegra;

	bool coroutineStarted = true;//para freezar ejecucion
	string coroutineStarted1 = string.Empty;//para freezar contenedor

	string modoVibracion = string.Empty; 

	void Start() { 
		modoVibracion = PlayerPrefs.GetString ("ModoVibracion");

		SpeechRecognizerListener listener = GameObject.FindObjectOfType<SpeechRecognizerListener>();
		listener.onErrorDuringRecording.AddListener(OnError);//NECESARIO, CUANDO ESTA ACTIVO POR MAS DE 5S TIRA ERROR PARA RESETEARSE LA ESCUCHA
		listener.onFinalResults.AddListener(OnFinalResult);
		listener.onPartialResults.AddListener(OnPartialResult);

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
		resultErrores.text = result.ToLower() + " " + cantPalabrasSpeech + palabrasSpeech [0].ToString ().Trim() + " " ;

		////////////////////////////////////////////*COLOREO DE ORACION DE LA ESCENA*//*POR-PALABRA-CLAVE*////////////////////////////////////////////				
		//activar animacion segun palabra
		switch (palabrasSpeech [0].ToString ().Trim())
		{
			case "árbol":									
					DesactivarEscucha ();
					//coroutineStarted1 = "en un bosque oscuro";//para freezar contenedor	
					sceneText.SetActive(true);
					coroutineStarted = false;//para freezar ejecucion					
				
				break;
			default:					
				break;
		}			
	}

public void OnError(string error) {
	DesactivarEscucha();
}

public void OnStartRecordingPressed() {
	if (SpeechRecognizer.IsRecording()) {
		DesactivarEscucha();
	} else {			
		ActivarEscucha ();
	}
}

public void ReiniciarValoresEscena() {			
		startRecordingButton.gameObject.SetActive(true);
		microfono.gameObject.SetActive(false);	
}

// Update is called once per frame
void Update()
{
	if (!coroutineStarted)
		StartCoroutine (EsperarSegundos (1));

	/*if (!string.IsNullOrEmpty(coroutineStarted1))			
		StartCoroutine (RetrasarContenedor (1, coroutineStarted1));*/
}  


IEnumerator EsperarSegundos(int seconds)
{
	coroutineStarted = true;
	yield return new WaitForSeconds(seconds);

	StartCoroutine (SpriteFadeOut());
	StopCoroutine ("SpriteFadeOut");

	SceneManager.LoadScene("newMenu");
}

IEnumerator SpriteFadeOut()
{		
	imagenNegra.SetTrigger ("end");
	yield return new WaitForSeconds(1f);
}

/*IEnumerator RetrasarContenedor(int seconds, string frase)
{		
	coroutineStarted1 = string.Empty;
	yield return new WaitForSeconds(seconds);

	CambiarTexto(frase);
}*/

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
