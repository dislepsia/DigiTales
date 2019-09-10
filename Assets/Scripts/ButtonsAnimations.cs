using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsAnimations : MonoBehaviour {

	[Range(0.25f,5f)]
	public float scaleSpeed = 0.4f;
	public AnimationCurve aCurve;
	private Transform _transform;
	private float step;
	private float objScale;

	// Use this for initialization
	public void Start () {
		_transform = this.transform;
	}
	
	// Update is called once per frame
	public void Update () {
		step += scaleSpeed * Time.deltaTime;
		objScale = aCurve.Evaluate (step);
		_transform.localScale = new Vector2 (objScale, objScale);
		if (step >= 1) {
			step = 0;
		}
	}
}
