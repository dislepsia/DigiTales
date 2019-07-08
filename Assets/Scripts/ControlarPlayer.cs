using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlarPlayer : MonoBehaviour
{
	private Animator animator; // variable para controlar animacion del player

	 

	void Start()
	{
		animator = GetComponent<Animator>(); //se vincula componente animator a variable privada creada

	}


	void Update()
	{
		
	}

	public void UpdateState(string state = null)
	{
		if (state != null)
		{
			animator.Play(state); //activa animacion que recibe como parametro
		}
	}


}
