using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Sprites;

public class ControlBrightness : MonoBehaviour {

	public Color[] colores;
	public Queue<Color> colaColor = new Queue<Color>();
	public SpriteRenderer spriteRenderer;

	void Start(){

		spriteRenderer = GetComponent<SpriteRenderer> ();

		foreach (Color c in colores)
			colaColor.Enqueue(c);
	}

	void Update(){

		if (Input.GetKeyDown (KeyCode.A))
			Invoke ("NextColor",0);
	}

	public void NextColor(){

		Color c = colaColor.Dequeue();
		spriteRenderer.color = c;
		colaColor.Enqueue (c);
	}
}
