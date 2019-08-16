using System.Collections;
using UnityEngine;

public class VisibleButton02 : MonoBehaviour {

	public string idButton;


	public void Update () {

		VisibleButton01 boton = GetComponent<VisibleButton01> ();
		idButton = boton.id;

		Debug.Log (idButton);
	}


}
