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
        SceneManager.LoadScene("Menu");
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
        SceneManager.LoadScene("ListadoCuentos");
    }

    public void Cuento()
    {
        SceneManager.LoadScene("Cuento");
    }
}

