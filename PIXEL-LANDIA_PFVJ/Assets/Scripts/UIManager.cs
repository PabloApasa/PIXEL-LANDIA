 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    
    public static UIManager Instance;

   
    [Header("UI Panels")]
    public GameObject optionsPanel;   
    public GameObject defeatPanel;    
    public GameObject victoryPanel;   

    public Text victoryScoreText; 

    [Header("Escena y Audio")]
    public AudioSource clip;       
    public string nextSceneName;     
    public string finalSceneName = "NombreDeTuUltimoNivel"; 

 
    private bool isGamePaused = false;

    private void Awake()
    {
        // Implementación del Singleton
        if (Instance == null)
        {
            Instance = this;
            
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            
            if (!defeatPanel.activeSelf && !victoryPanel.activeSelf)
            {
                ToggleOptionsPanel();
            }
        }
    }



    public void ToggleOptionsPanel()
    {
        isGamePaused = !isGamePaused; // Invertir el estado

        if (isGamePaused)
        {
            OptionsPanel();
        }
        else
        {
            Return();
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
        isGamePaused = false;
    }

  
    public void ShowVictoryPanel()
    {
        Time.timeScale = 0; 
        optionsPanel.SetActive(false); 

        string currentScene = SceneManager.GetActiveScene().name;

      
        if (currentScene == finalSceneName)
        {
            
            if (ControladorPuntos.instance != null && victoryScoreText != null)
            {
                float finalScore = ControladorPuntos.instance.GetPuntos();

                
                victoryScoreText.text = "Puntuación Final: " + finalScore.ToString("F0");
            }

            
            victoryPanel.SetActive(true);
        }
        else
        {
            
            LoadNextLevel();
        }
    }

    public void ShowDefeatPanel()
    {
        Time.timeScale = 0; 
        optionsPanel.SetActive(false); 
        defeatPanel.SetActive(true); 
        isGamePaused = true;
    }

   
    public void LoadNextLevel()
    {
        Time.timeScale = 1; 
        SceneManager.LoadScene(nextSceneName);
    }

 
    public void GoMainMenu()
    {
        Time.timeScale = 1; 
        SceneManager.LoadScene("Menu-Niveles");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PlaySoundButton()
    {
        if (clip != null)
        {
            clip.Play();
        }
    }

    // Método para recargar la escena actual
    public void RestartCurrentLevel()
    {

        Time.timeScale = 1;

      
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

   
        SceneManager.LoadScene(currentSceneIndex);

    }
}