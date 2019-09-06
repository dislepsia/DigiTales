using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HabilitarCuento : MonoBehaviour {

	string cuento = "";
	GameObject objectToDisable;

	public static HabilitarCuento objetoHabilitar= new HabilitarCuento();
	public string codigo = null;

	//Habilitar todos los botones que fueron desbloqueados anteriormente (Teniendo ya el archivo)
	void Start () {
	}


	// Update is called once per frame
	void Update () {

		//foreach(string desbloquear in codigo)
		//{
		
			if(HabilitarCuento.objetoHabilitar.codigo !=null){

				cuento = HabilitarCuento.objetoHabilitar.codigo;

				if (cuento.Equals("6281")){
					GameObject.Find ("ChanchitosButton").GetComponent<Button> ().interactable = true;
				}

				if (cuento.Equals ("7735")) {
					GameObject.Find ("CaperucitaButton").GetComponent<Button> ().interactable = true;
				} 

				if (cuento.Equals ("4467")) {
					GameObject.Find ("CenicientaButton").GetComponent<Button> ().interactable = true;
				} 
			}
		}
	//}
}
