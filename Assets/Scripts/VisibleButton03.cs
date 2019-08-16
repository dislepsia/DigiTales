using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {

	public GameObject button;

	public void visibleButton(){

		if (button != null)
			button.SetActive (true);
	}
}
