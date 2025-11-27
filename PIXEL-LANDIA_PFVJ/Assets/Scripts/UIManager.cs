 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // 1. Singleton Instance
    public static UIManager Instance;

    // 2. Referencias de UI y Sonido
    [Header("UI Panels")]
    public GameObject optionsPanel;   // Menú de Pausa
    public GameObject defeatPanel;    // Pantalla de Derrota
    public GameObject victoryPanel;   // Pantalla de Victoria (Solo final de juego)

    public Text victoryScoreText; //componente Text/textMeshPro para mostrar puntaje final

    [Header("Escena y Audio")]
    public AudioSource clip;        // Sonido de clic de botón
    public string nextSceneName;     // Nombre de la siguiente escena a cargar
    public string finalSceneName = "NombreDeTuUltimoNivel"; // *** ¡IMPORTANTE ASIGNAR! ***

    // 3. Variables de Estado
    private bool isGamePaused = false;

    private void Awake()
    {
        // Implementación del Singleton
        if (Instance == null)
        {
            Instance = this;
            // Opcional: Si el Canvas debe persistir en todas las escenas
            // DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        // Detección de tecla ESC para alternar el menú de pausa
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Solo alternamos si NO hay una pantalla de Derrota/Victoria activa
            if (!defeatPanel.activeSelf && !victoryPanel.activeSelf)
            {
                ToggleOptionsPanel();
            }
        }
    }

    // --- MÉTODOS DE PAUSA/OPCIONES ---

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

    // --- MÉTODOS DE ESTADO DE JUEGO (Derrota/Victoria) ---

    // 🏆 Se llama al alcanzar la meta del nivel.
    public void ShowVictoryPanel()
    {
        Time.timeScale = 0; // Pausa el juego
        optionsPanel.SetActive(false); // Asegura que el menú de pausa esté oculto

        string currentScene = SceneManager.GetActiveScene().name;

        // ** LÓGICA CLAVE: Comprueba si es el último nivel **
        if (currentScene == finalSceneName)
        {
            // 1.Obtener el puntaje total del Singleton
            if (ControladorPuntos.instance != null && victoryScoreText != null)
            {
                float finalScore = ControladorPuntos.instance.GetPuntos();

                // 2. Actualizar el componente de texto de la Victoria
                // Usa ToString("F0") para mostrar el número sin decimales (si es un número entero)
                victoryScoreText.text = "Puntuación Final: " + finalScore.ToString("F0");
            }

            // 3. Mostrar el panel de Victoria
            victoryPanel.SetActive(true);
        }
        else
        {
            // No es el nivel final, avanza directamente al siguiente nivel
            LoadNextLevel();
        }
    }

    // 💀 Se llama cuando el PlayerRespaw detecta que las vidas llegaron a 0.
    public void ShowDefeatPanel()
    {
        Time.timeScale = 0; // Pausa el juego
        optionsPanel.SetActive(false); // Asegura que el menú de pausa esté oculto
        defeatPanel.SetActive(true); // Muestra la pantalla de derrota
        // Desactiva la posibilidad de reabrir el menú de pausa
        isGamePaused = true;
    }

    // --- MÉTODOS DE NAVEGACIÓN ---

    // Lo llama el panel de Derrota o el código de Victoria si no es el último nivel.
    public void LoadNextLevel()
    {
        Time.timeScale = 1; // Reanuda el tiempo
        SceneManager.LoadScene(nextSceneName);
    }

    // Lo llama el panel de Derrota o el botón de Retorno al Menú.
    public void GoMainMenu()
    {
        Time.timeScale = 1; // Reanuda el tiempo
        // Asume que la escena de menú se llama "Menu-Niveles"
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
        // 1. Reanudar el tiempo (vital si el juego está pausado por la pantalla de derrota)
        Time.timeScale = 1;

        // 2. Obtener el índice de la escena activa
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // 3. Cargar la escena
        SceneManager.LoadScene(currentSceneIndex);

        // Opcional: Asegúrate de que el panel de derrota se oculte si es necesario
        // aunque al cargar la escena se reinicia toda la UI.
        // defeatPanel.SetActive(false); 
    }
}