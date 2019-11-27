using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PuzzleControl : MonoBehaviour {

	[SerializeField]
	private Transform[] pictures;
	[SerializeField]
	private Transform[] winText;

	public static bool youWin;

	bool coroutineStarted = true;

	void Start () {
		youWin = false;
	}

	void Update () {
		if(
			pictures[0].rotation.z.Equals(0) &&
			pictures[1].rotation.z.Equals(0) &&
			pictures[2].rotation.z.Equals(0) &&
			pictures[3].rotation.z.Equals(0) &&
			pictures[4].rotation.z.Equals(0) &&
			pictures[5].rotation.z.Equals(0) &&
			pictures[6].rotation.z.Equals(0) &&
			pictures[7].rotation.z.Equals(0) &&
			pictures[8].rotation.z.Equals(0) &&
			pictures[9].rotation.z.Equals(0) &&
			pictures[10].rotation.z.Equals(0) &&
			pictures[11].rotation.z.Equals(0) &&
			pictures[12].rotation.z.Equals(0)) {

			youWin = true;
			GameObject.Find ("ArmadoOk").GetComponent<TextMeshProUGUI> ().enabled = true;
			AudioSource respuestaOk = GameObject.Find ("AudioRespuestaOk").GetComponent<AudioSource> ();
			respuestaOk.Play ();
			StartCoroutine (EsperarSegundos (3));
		}
	}

	IEnumerator EsperarSegundos(int seconds){
		coroutineStarted = true;
		yield return new WaitForSeconds (seconds);

		StartCoroutine (SpriteFadeOut ());
		StopCoroutine ("SpriteFadeOut");
		SceneManager.LoadScene ("MiniJuego-NenaTemerosa-Modo");
	}

	IEnumerator SpriteFadeOut()	{		
		yield return new WaitForSeconds(1f);
	}
}
