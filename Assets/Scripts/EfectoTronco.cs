using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EfectoTronco : MonoBehaviour {

	public GameObject bosque; //objeto para controlar escena
	public GameObject player; //objeto para controlar animacion de personaje

	// Use this for initialization
	void Start () {		
	}

	IEnumerator EfectoTemblor()
	{	
		/*coroutineStarted2 = true;

	troncoEfecto.gameObject.SetActive (true);
	yield return new WaitForSeconds(0.2f);*/

		RectTransform bosqueImagen = bosque.GetComponent<RectTransform> ();
		Vector3 myVector = new Vector3(bosqueImagen.position.x+0.5f, bosqueImagen.position.y, bosqueImagen.position.z);
		Vector3 myVector2 = new Vector3(bosqueImagen.position.x, bosqueImagen.position.y, bosqueImagen.position.z);
		for (int z = 0; z < 2; z++)
		{		
			bosqueImagen.position = myVector;
			yield return new WaitForSeconds(0.1f);
			bosqueImagen.position = myVector2;
			yield return new WaitForSeconds(0.1f);
		}

	}

	void Temblor()
	{	
		Handheld.Vibrate ();
	}

	void Correr()
	{	
		player.gameObject.GetComponent<Animator>().Play("PlayerRun");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
