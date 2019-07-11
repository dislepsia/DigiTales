using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using KKSpeech;
using UnityEngine.SceneManagement;

public class RecordingCanvas : MonoBehaviour {

	public Button startRecordingButton;
	public Text sceneText; //oracion propia de la escena

	public Text coincidencia; //
	public Text cantAccesos; //cantidad de veces que se accede a metodo OnPartialResult
	public Text resultTextSpeech; //texto reconocido por voz
	public Text resultErrores; //visualizar error

	//variables para trabajar sceneText
	private string textoEscena; 
	private string[] palabrasEscena = null; 
	int cantPalabrasEscena = 0; 

	//variables para trabajar result(reconocimiento parcial de voz)
	private string[] palabrasSpeech = null;
	int cantPalabrasSpeech = 0;

	//variables para controlar coloreo
	int i = 0;
	int j = 0;
	int n = 0;
	int palabraReconocida = 0;

	//int count = 0;

	public GameObject player; //objeto para controlar animacion de personaje
    public GameObject bosque; //objeto para controlar escena

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

		} else {			
			resultErrores.text = "Sorry, but this device doesn't support speech recognition";
			startRecordingButton.enabled = false;
		}

	}

	/*RESULTADO FINAL DEL RECONOCIMIENTO DE VOZ*/
	public void OnFinalResult(string result) {
		//resultText.text = result;
	}

	/*RESULTADO PARCIAL DEL RECONOCIMIENTO DE VOZ*/
	public void OnPartialResult(string result) {

		//obtengo cantidad de palabras de reconocimiento parcial de voz
		palabrasSpeech = result.Split(' ');
		cantPalabrasSpeech = palabrasSpeech.Length;

		/*COLOREO DE ORACION DE LA ESCENA*/
		/*for (i = n; i < cantPalabrasSpeech; i++)
		{
			for (j = n; j < cantPalabrasEscena; j++)
			{
				if (palabrasSpeech [i].ToString () == palabrasEscena [j].ToString ())
				{
					//activar animacion segun palabra
					if (palabrasEscena [j].ToString ().Trim() == "correr")
						player.SendMessage("UpdateState", "PlayerRun");
					
					resultTextSpeech.text = resultTextSpeech.text + palabrasSpeech [i].ToString () + " "; //coloreo
					n++; //para no tener en cuenta palabra coloreada en el bucle
					palabraReconocida = 1; //para terminar bucleo por palabra encontrada
					
					break;
				}
			}
			if(palabraReconocida == 1)
			{
				palabraReconocida = 0;
				break;
			}
		}*/

		/*COLOREO DE ORACION DE LA ESCENA*/
		for (i = n; i < cantPalabrasSpeech; i++)
		{
			for (j = n; j < cantPalabrasEscena; j++)
			{
				if (palabrasSpeech [i].ToString () == palabrasEscena [j].ToString () && i == j)
				{
                    //activar animacion segun palabra
                    if (palabrasEscena[j].ToString().Trim() == "bosque")
                        bosque.SetActive(true);

                    //activar animacion segun palabra
                    if (palabrasEscena[j].ToString().Trim() == "niña")
                        player.SetActive(true);

                    //activar animacion segun palabra
                    if (palabrasEscena[j].ToString().Trim() == "temerosa")
                    {
                        SceneManager.LoadScene("RelatarCuento");
                        player.SetActive(true);
                        bosque.SetActive(true);
                    }

                        //activar animacion segun palabra
                     if (palabrasEscena [j].ToString ().Trim() == "correr")
						player.SendMessage("UpdateState", "PlayerRun");
                     

					resultTextSpeech.text = resultTextSpeech.text + palabrasSpeech [i].ToString () + " "; //coloreo
					n++; //para no tener en cuenta palabra coloreada en el bucle
					palabraReconocida = 1; //para terminar bucleo por palabra encontrada

					break;
				}
			}
			if(palabraReconocida == 1)
			{
				palabraReconocida = 0;
				resultErrores.text = "OK";
				break;
			}
			else 
				resultErrores.text = "Palabra no reconocida";
		}


		//coincidencia.text = result;
		//count++;
		//cantAccesos.text = count.ToString();
	}

	public void OnAvailabilityChange(bool available) {
		startRecordingButton.enabled = available;
		if (!available) {
			resultErrores.text = "Speech Recognition not available";
		} else {
			resultErrores.text = "Say something :-)";
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
		startRecordingButton.GetComponentInChildren<Text>().text = "Start Recording";
	}

	public void OnError(string error) {
		Debug.LogError(error);
		resultErrores.text = "Something went wrong... Try again! \n [" + error + "]";
		startRecordingButton.GetComponentInChildren<Text>().text = "Start Recording";
	}

	public void OnStartRecordingPressed() {
		if (SpeechRecognizer.IsRecording()) {
			SpeechRecognizer.StopIfRecording();
			startRecordingButton.GetComponentInChildren<Text>().text = "Start Recording";
		} else {
			SpeechRecognizer.StartRecording(true);
			startRecordingButton.GetComponentInChildren<Text>().text = "Stop Recording";
			resultErrores.text = "Say something :-)";
		}
	}
}
