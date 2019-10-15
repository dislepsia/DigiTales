using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EfectoTronco : MonoBehaviour {

	public GameObject bosque; //objeto para controlar escena
	public GameObject player; //objeto para controlar animacion de personaje
	public GameObject lluvia; //objeto para controlar animacion de personaje
	public GameObject fantasma; //objeto para controlar animacion de personaje
	public GameObject pegaso; //objeto para controlar animacion de personaje
	public GameObject humo; //objeto para controlar animacion de personaje

	public AudioClip suspenso;
	public AudioClip chirrido;
	private AudioSource ambienteBosque;

	//public AudioClip puerta;
	//private AudioSource ambienteBosque;

	// Use this for initialization
	void Start () {		
		//ambienteBosque = GetComponent<AudioSource> ();						
		//ambienteBosque.clip = puerta;
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

	void Idle()
	{	
		player.gameObject.GetComponent<Animator>().Play("Player2Idle");
	}

	void Lluvia()
	{	
		lluvia.SetActive (true);
	}

	void Fantasma()
	{	
		fantasma.SetActive (true);
		ambienteBosque = GetComponent<AudioSource> ();						
		ambienteBosque.clip = suspenso;
		ambienteBosque.Play ();

	}

	void Humo()
	{	
		pegaso.SetActive (false);
		humo.SetActive (true);
		humo.gameObject.GetComponent<Animator>().Play("Humo");


	}

	void CasaBosque()
	{	
		player.gameObject.GetComponent<Animator>().Play("PlayerIdle");
		humo.gameObject.GetComponent<Animator>().Play("Humo");

	}

	void EntraCasa()
	{	
		
		player.SetActive (false);
		//ambienteBosque.Play ();

	}

	void PuertaCasa()
	{	

		ambienteBosque = GetComponent<AudioSource> ();						
		ambienteBosque.clip = chirrido;
		ambienteBosque.Play ();
	}





	// Update is called once per frame
	void Update () {
		
	}
}
