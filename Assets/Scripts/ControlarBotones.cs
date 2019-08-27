using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlarBotones : MonoBehaviour
{
    public void ExitGame()
    {
        Application.Quit();
    }

    public void MenuPrincipal()
    {
		Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("NewMenu");
    }

    public void Creditos()
    {
		Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("NewCreditos");
    }

    public void RelatarCuento()
    {
        SceneManager.LoadScene("Cuento1Escena1");        
    }

	public void MiniJuegoNenaTemerosaLetras()
	{
		SceneManager.LoadScene("MiniJuego-NenaTemerosa-Letras");        
	}

	public void MiniJuegoNenaTemerosaModo()
	{
		Screen.orientation = ScreenOrientation.Portrait;
		SceneManager.LoadScene("MiniJuego-NenaTemerosa-Modo");        
	}

	public void MiniJuegoNenaTemerosa()
	{
		SceneManager.LoadScene("MiniJuego-NenaTemerosa");        
	}

    public void ListadoCuentos()
    {		
		Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("NewListadoCuentos");
    }
		
	public void Opciones()
	{
		Screen.orientation = ScreenOrientation.Portrait;
		SceneManager.LoadScene("Opciones");
	}

	public void Descargar()
	{
		Screen.orientation = ScreenOrientation.Portrait;
		SceneManager.LoadScene("CargaDeCodigo");
	}

	public void Salir()
	{
		Screen.orientation = ScreenOrientation.Portrait;
		SceneManager.LoadScene("Salir");
	}
}

