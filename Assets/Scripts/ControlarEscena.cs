using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlarEscena : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string LevelName = Application.loadedLevelName;
        if (LevelName == "Menu") {
            Screen.orientation = ScreenOrientation.Portrait;
        }
        else if (LevelName == "Creditos")
        {
            Screen.orientation = ScreenOrientation.Portrait;
        }
        else if (LevelName == "ListadoCuentos")
        {
            Screen.orientation = ScreenOrientation.Portrait;
        }
        else if (LevelName == "RelatarCuento2")
        {
            Screen.orientation = ScreenOrientation.Landscape;
        }
        else if (LevelName == "RelatarCuento")
        {
            Screen.orientation = ScreenOrientation.Landscape;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }    
}
