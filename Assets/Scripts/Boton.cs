using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boton : MonoBehaviour
{
    public void ExitGame()
    {
        Application.Quit();
    }

    public void MenuPrincipal()
    {
        SceneManager.LoadScene("NewMenu");
		Screen.orientation = ScreenOrientation.Portrait;
    }

    public void Creditos()
    {
        SceneManager.LoadScene("Creditos");
    }

    public void RelatarCuento()
    {
        SceneManager.LoadScene("RelatarCuento2");        
    }

    public void ListadoCuentos()
    {
		
        SceneManager.LoadScene("NewListadoCuentos");
    }

    public void Cuento()
    {
        SceneManager.LoadScene("Cuento");
    }
}

