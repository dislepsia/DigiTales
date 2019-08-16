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
        SceneManager.LoadScene("NewCreditos");
    }

    public void RelatarCuento()
    {
        SceneManager.LoadScene("RelatarCuento2");        
    }

    public void ListadoCuentos()
    {
		
        SceneManager.LoadScene("NewListadoCuentos");
		Screen.orientation = ScreenOrientation.Portrait;
    }
		
	public void Opciones()
	{
		SceneManager.LoadScene("Opciones");
	}

	public void Descargar()
	{
		SceneManager.LoadScene("CargaDeCodigo");
	}

	public void Salir()
	{
		SceneManager.LoadScene("Salir");
	}
}

