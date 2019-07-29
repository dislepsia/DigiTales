using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeValueChange : MonoBehaviour {

	private AudioSource audioSrc;
	private float volumenBarra;

	void Start(){
		audioSrc = GetComponent<AudioSource> ();
	}

	void Update(){
		audioSrc.volume = volumenBarra;
	}

	public void SetVolume(float vol){
		audioSrc.volume = vol;
	}
}
