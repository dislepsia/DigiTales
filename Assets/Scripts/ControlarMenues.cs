using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlarMenues : MonoBehaviour
{    
	string LevelName = string.Empty;
    
    void Start()
    {
		Screen.orientation = ScreenOrientation.Portrait;
    }
	    
    void Update()
    {
		if (LevelName == "Salir") 
		{
			if (Input.GetKeyDown (KeyCode.Escape))
				Application.Quit ();
		}
    }    
}
