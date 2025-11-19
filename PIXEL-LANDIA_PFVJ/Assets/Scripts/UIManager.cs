using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject optionsPanel;

    //EL MÉTODO DE DETECCIÓN DE TECLA(NUEVO)
    void Update()
    {
        // Detecta si la tecla ESCAPE acaba de ser presionada
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Llama a la función que maneja el panel de opciones
            ToggleOptionsPanel();
        }
    }

    // EL MÉTODO QUE ALTERNA EL PANEL (NUEVO)
    public void ToggleOptionsPanel()
    {
        // Si el panel de opciones está ACTIVO, lo cerramos.
        if (optionsPanel.activeSelf)
        {
            Return();
        }
        // Si está INACTIVO, lo abrimos.
        else
        {
            OptionsPanel();
        }
    }

    public void OptionsPanel()
    {
       Time.timeScale = 0;
        optionsPanel.SetActive(true);
    }

    public void Return()
    {
        Time.timeScale = 1;
        optionsPanel.SetActive(false);
    }

    public void AnotherOptions()
    {
        //sound
        //graphics
    }

    public void  GoMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu-Niveles");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
