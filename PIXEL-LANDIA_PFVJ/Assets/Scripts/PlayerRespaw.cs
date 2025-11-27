using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerRespaw : MonoBehaviour
{
    // Variables públicas
    public GameObject[] hearts;  // Array de GameObjects de corazones (UI)
    public Animator animator;    // Referencia al componente Animator del jugador

    // Variables privadas
    private int life;
    private float checkPointPositionX, checkPointPositionY;

    void Start()
    {
        // Inicializa la vida con el número de corazones en el array
        life = hearts.Length;

        // Carga la posición del Checkpoint si existe
        if (PlayerPrefs.GetFloat("CheckPointPositionX") != 0)
        {
            // Mueve al jugador al último checkpoint guardado
            transform.position = new Vector2(PlayerPrefs.GetFloat("CheckPointPositionX"), PlayerPrefs.GetFloat("CheckPointPositionY"));
        }
    }

    // Método que se llama cuando el jugador recibe daño
    public void PlayerDamaged()
    {
        // Evita procesar daño si el jugador ya está muerto
        if (life <= 0) return;

        // 1. Reducir vida
        life--;

        // 2. Actualizar UI y comprobar el estado de vida
        UpdateLifeState();
    }

    private void UpdateLifeState()
    {
        // Reproduce la animación de golpe
        if (animator != null)
        {
            animator.Play("Hit");
        }

        // Si la vida es mayor o igual a 0, desactiva el corazón correspondiente
        if (life >= 0 && life < hearts.Length)
        {
            // El índice 'life' actual apunta al corazón que se acaba de perder
            hearts[life].SetActive(false);
        }

        // 3. Comprobar Derrota
        if (life <= 0)
        {
            HandlePlayerDefeat();
        }
    }

    private void HandlePlayerDefeat()
    {
        // ** INTEGRACIÓN CLAVE CON UIManager **

        // 1. Llama a la instancia única de UIManager
        if (UIManager.Instance != null)
        {
            // 2. Muestra el panel de derrota (pausa el juego y activa la UI de derrota)
            UIManager.Instance.ShowDefeatPanel();
        }
        else
        {
            // Esto es un fallback si UIManager no existe o no tiene el Singleton
            Debug.LogError("UIManager.Instance no se encontró. No se puede mostrar la pantalla de derrota.");
            // En un entorno de desarrollo, podrías recargar aquí
            // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        // Opcional: Desactivar los controles o el Collider del jugador
        // GetComponent<Collider2D>().enabled = false;
    }

    // Método para guardar la posición del checkpoint
    public void ReachedCheckPoint(float x, float y)
    {
        PlayerPrefs.SetFloat("CheckPointPositionX", x);
        PlayerPrefs.SetFloat("CheckPointPositionY", y);
        PlayerPrefs.Save(); // Guarda los datos inmediatamente
    }
}