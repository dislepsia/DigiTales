using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlarEscena : MonoBehaviour
{    
	string LevelName = string.Empty;
    
    void Start()
    {
        LevelName = Application.loadedLevelName;

		switch (LevelName)
		{
		case "NewMenu":
			Screen.orientation = ScreenOrientation.Portrait;
			break;

		case "Creditos":
			Screen.orientation = ScreenOrientation.Portrait;
			break;

		case "NewListadoCuentos":
			Screen.orientation = ScreenOrientation.Portrait;
			break;

		case "Cuento1Escena1":
			Screen.orientation = ScreenOrientation.Landscape;
			break;

		case "Cuento1Escena2":
			Screen.orientation = ScreenOrientation.Landscape;
			break;

		case "Cuento1Escena3":
			Screen.orientation = ScreenOrientation.Landscape;
			break;

		case "Cuento1Escena4":
			Screen.orientation = ScreenOrientation.Landscape;
			break;

		case "Cuento1Escena5":
			Screen.orientation = ScreenOrientation.Landscape;
			break;

		case "Opciones":
			Screen.orientation = ScreenOrientation.Portrait;
			break;

		default:					
			break;
		}        
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
