using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

public class Navegador
{
    private IWebDriver navegador;

    public Navegador()
    {
        ChromeOptions opciones = new ChromeOptions();

        //esto es para que no se abra la ventana del navegador sobre la seguridad de la contraseña y el autocompletado, para que no interfiera con la automatización
        //este es el error que tanto me costó ver. :/ ya que el metodo BUscarPorId no encontraba el elemento y me tiraba error, y era porque estaba tapado por la ventana de seguridad del navegador.

        opciones.AddUserProfilePreference("credentials_enable_service", false);
        opciones.AddUserProfilePreference("profile.password_manager_enabled", false);
        opciones.AddUserProfilePreference("profile.password_leak_detection_enabled", false);
        opciones.AddExcludedArgument("enable-automation");
        opciones.AddAdditionalChromeOption("useAutomationExtension", false);

        var prefs = new Dictionary<string, object>
        {
            { "profile.default_content_setting_values.notifications", 2 }, // 2 = Bloquear
            { "profile.default_content_setting_values.geolocation", 2 },   // Bloquear ubicación
            { "profile.managed_default_content_settings.popups", 2 }       // Bloquear pop-ups
        };
        opciones.AddUserProfilePreference("prefs", prefs);

        navegador = new ChromeDriver(opciones);
    }

    public void IrA(string url)
    {
        navegador.Navigate().GoToUrl(url);
    }

    public IWebElement BuscarPorId(string id)
    {
        return navegador.FindElement(By.Id(id));
    }

    public void Escribir(IWebElement elemento, string texto)
    {
        elemento.SendKeys(texto);
    }

    public void HacerClick(IWebElement elemento)
    {
        elemento.Click();
    }
    public IReadOnlyCollection<IWebElement> BuscarPorClase(string clase)
    {
        return navegador.FindElements(By.ClassName(clase));
    }

    public IWebElement BuscarUnoPorClase(string clase)
    {
        return navegador.FindElement(By.ClassName(clase));
    }
    public string ObtenerUrl()
    {
        return navegador.Url;
    }




}