using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinksContacto : MonoBehaviour
{
    public void InstagramDigiTales()
    {
        Application.OpenURL("https://www.instagram.com/digitalesapp/?hl=es-la");
    }
    public void InstagramNoli()
    {
        Application.OpenURL("https://www.instagram.com/_elnoli/?hl=es-la");
    }

    public void InstagramLeo()
    {
        Application.OpenURL("https://www.instagram.com/l30.design/?hl=es-la");
    }

	public void InstagramRemeras()
	{
		Application.OpenURL("https://www.instagram.com/dokremeras/?hl=es-la");
	}
    public void LinkedinAdrian()
    {
        Application.OpenURL("https://www.linkedin.com/in/adrian-lazarte-620371161/");
    }

    public void LinkedinFerra()
    {
        Application.OpenURL("https://www.linkedin.com/in/mart%C3%ADn-ferrarese-912b7238/");
    }

    public void LinkedinMatias()
    {
        Application.OpenURL("https://www.linkedin.com/in/martias-patrignoni-24288a192/");
    }

    public void CorreoDigiTales()
    {
        Application.OpenURL("https://accounts.google.com/ServiceLogin/identifier?service=mail&continue=https%3A%2F%2Fmail.google.com%2Fmail%2F&hl=es&flowName=GlifWebSignIn&flowEntry=AddSession");
    }
}