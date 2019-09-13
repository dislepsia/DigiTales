using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Inicio : MonoBehaviour {


	void Start()
	{
		StartCoroutine (SpriteShapeOut());
		StopCoroutine ("SpriteShapeOut");

		//inicializo modo relato
		PlayerPrefs.SetString ("ModoReconocimiento", "0");
	}  

	IEnumerator SpriteShapeOut()
	{	
		yield return new WaitForSeconds(3f);	
		SceneManager.LoadScene("NewMenu");
	}
}
