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
        SceneManager.LoadScene("NewMenu");
    }

    public void Creditos()
    {
        SceneManager.LoadScene("NewCreditos");
    }

    public void RelatarCuento()
    {
        SceneManager.LoadScene("Cuento1Escena1");        
    }

    public void ListadoCuentos()
    {		
        SceneManager.LoadScene("NewListadoCuentos");
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

