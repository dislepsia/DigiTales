using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using KKSpeech;

public class RecordingCanvas : MonoBehaviour {

	public Button startRecordingButton;
	public Text resultText;

	public Text coincidencia;
	public Text contador;
	public Text resultTextSpeech;
	public Text resultErrores;

	private string textoEscena;
	private string[] palabrasEscena = null;
	private string textoSpeech;
	private string[] palabrasSpeech = null;

	int cantPalabrasEscena = 0;
	int cantPalabrasSpeech = 0;
	int i = 0;
	int j = 0;
	int n = 0;
	int salir = 0;
	int count = 0;

	public GameObject player;



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

			textoEscena = resultText.text;
			palabrasEscena = textoEscena.Split(' ');
			cantPalabrasEscena = palabrasEscena.Length;

			//animator = GetComponent<Animator>();
			//animator.Play("PlayerRun2");


		} else {
			//animator.Play("PlayerRun2");
			resultErrores.text = "Sorry, but this device doesn't support speech recognition";
			startRecordingButton.enabled = false;
		}

	}

	public void OnFinalResult(string result) {
		//resultText.text = result;
	}

	public void OnPartialResult(string result) {
		
		palabrasSpeech = result.Split(' ');
		cantPalabrasSpeech = palabrasSpeech.Length;

		for (i = n; i < cantPalabrasSpeech; i++)
		{
			for (j = n; j < cantPalabrasEscena; j++)
			{
				if (palabrasSpeech [i].ToString () == palabrasEscena [j].ToString ())
				{
					/*if (palabrasEscena [j].ToString ().Trim() == "correr")
					{
						contador.text = "correr";
						player.SendMessage("UpdateState", "PlayerRun2"); 

					}*/
					resultTextSpeech.text = resultTextSpeech.text + palabrasSpeech [i].ToString () + " ";
					n++;
					salir = 1;

					
					break;

				}
			}
			if(salir == 1)
			{
				salir = 0;
				break;
			}
		}

		coincidencia.text = result;
		//count++;
		//contador.text = count.ToString();
		//resultText.text = result;
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
