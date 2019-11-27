using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Experimental.UIElements;

//CONTROLA BOTONES DE LA APP
public class ControlarBotones : MonoBehaviour
{
    public void ExitGame()
    {
        Application.Quit();
    }

	//METODO ASOCIADO A BOTON
    public void MenuPrincipal()
    {
		Screen.orientation = ScreenOrientation.Portrait; //ROTA PANTALLA
        SceneManager.LoadScene("NewMenu"); //CARGA ESCENA MENU
    }

    public void Creditos()
    {
		Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("NewCreditos");
    }

    public void RelatarCuento()
    {
		switch (CargarPantallaDeCuento.objetoEleccion.cuento) 
		{
			case "nena":
				SceneManager.LoadScene("Cuento1Escena1");
				break;

			case "chanchitos":
				SceneManager.LoadScene("Cuento2Escena1");
				break;
		}
    }

	public void MiniJuegoNenaTemerosaLetras()
	{
		Screen.orientation = ScreenOrientation.Portrait;

		switch (CargarPantallaDeCuento.objetoEleccion.cuento)
		{
			case "nena":
				SceneManager.LoadScene("MiniJuego-NenaTemerosa-Letras");    
				break;

			case "chanchitos":
				SceneManager.LoadScene("MiniJuego-Chanchitos");    
				break;
		}  
	}

	public void MiniJuegoNenaTemerosaModo()
	{
		Screen.orientation = ScreenOrientation.Portrait;
		SceneManager.LoadScene("MiniJuego-NenaTemerosa-Modo");        
	}

	public void MiniJuegoNenaTemerosa()
	{
		Screen.orientation = ScreenOrientation.Portrait;
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

	public static void Salir_Atras_Boton_Cel()
	{
		Screen.orientation = ScreenOrientation.Portrait;
		SceneManager.LoadScene("Salir");
	}
}

