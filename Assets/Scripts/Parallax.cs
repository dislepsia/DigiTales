using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour {

	float scrollSpeed = -10f;
	Vector2 startPos;

	// Use this for initialization
	void Start () {
		startPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		float newPos=0f;


			newPos = Mathf.Repeat(Time.time * scrollSpeed, 200);

		transform.position = startPos + Vector2.right * newPos;
		Debug.Log ("Verdadero: " +transform.position);
	}
}
