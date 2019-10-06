using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioRespuestaOk : MonoBehaviour {

	public AudioClip respuestaOk;
	public AudioSource audioSource;

	void Start () {
		audioSource.clip = respuestaOk;
	}
}
