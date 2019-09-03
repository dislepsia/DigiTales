using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Dropdown : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler {

	public RectTransform contenedor = null;
	public bool isOpen;

	// Use this for initialization
	void Start () {
		contenedor = transform.Find ("Contenedor").GetComponent<RectTransform> ();
		isOpen = false;
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 scale = contenedor.localScale;
		scale.y = Mathf.Lerp (scale.y, isOpen ? 1 : 0, Time.deltaTime * 12);
		contenedor.localScale = scale;
	}

	#region IPointerEnterHandler implementation

	public void OnPointerEnter (PointerEventData eventData)
	{
		isOpen = true;
	}

	#endregion

	#region IPointerExitHandler implementation

	public void OnPointerExit (PointerEventData eventData)
	{
		isOpen = false;
	}

	#endregion
}
