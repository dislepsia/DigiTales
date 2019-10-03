using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSound : MonoBehaviour {

	private static BackgroundSound instance = null;
	public static BackgroundSound Instance {
		get {
			return instance; 
		}
	}
	

	void Awake () {
		if (instance != null && instance != this) {
			Destroy (this.gameObject);
			return;
		} else {
			instance = this;
		}

		DontDestroyOnLoad (this.gameObject);
	}
}
