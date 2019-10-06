using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioRespuestaError : MonoBehaviour {

	public AudioClip respuestaError;

	public AudioSource audioSource;

	void Start () {
		audioSource.clip = respuestaError;
	}
}
