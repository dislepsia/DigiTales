using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

using TMPro;
using UnityEngine.SceneManagement;
using KKSpeech;

public class ControlarJuegoAprendizaje : MonoBehaviour {

	public Button startRecordingButton;
	public GameObject sceneText;
	public Text resultErrores; //visualizar error

	//variables para trabajar result(reconocimiento parcial de voz)
	private string[] palabrasSpeech = null;
	int cantPalabrasSpeech = 0;

	//variables de sonidos
	private AudioSource ambienteBosque;

	public Animator microfono;
	public Animator imagenNegra;

	bool coroutineStarted = true;//para freezar ejecucion

	void Start() { 
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
					GameObject.Find ("RespuestaText-A").GetComponent<TextMeshProUGUI> ().enabled = true;
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
		
void Update()
{
	if (!coroutineStarted)
		StartCoroutine (EsperarSegundos (1));
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
