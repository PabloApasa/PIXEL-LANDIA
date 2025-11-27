using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FruitManager : MonoBehaviour
{
    // Las referencias de UI pueden mantenerse, pero se recomienda que el UIManager las gestione.
    // Aquí las mantenemos para no romper tu UI actual.
    public Text levelCleared; // Texto "Nivel Completado"
    public GameObject transition; // Objeto de transición (opcional, si aún lo quieres usar)

    // UI de contadores
    public Text totalFruits;
    public Text fruitsCollected;

    private int totalFruitsInLevel;

    public void Start()
    {
        // Cuenta los hijos (frutas) al inicio del nivel
        totalFruitsInLevel = transform.childCount;
    }

    // NOTA: Es más eficiente llamar a AllFruitsCollected() solo cuando se recoge una fruta,
    // en lugar de en cada frame (Update), pero mantendremos tu estructura actual por ahora.
    private void Update()
    {
        // Actualiza el contador de frutas recolectadas (transform.childCount es el restante)
        totalFruits.text = totalFruitsInLevel.ToString();
        fruitsCollected.text = (totalFruitsInLevel - transform.childCount).ToString();

        // Comprueba la condición de victoria en cada frame
        AllFruitsCollected();
    }

    public void AllFruitsCollected()
    {
        if (transform.childCount == 0)
        {
            // --- CÓDIGO REFRACTORIZADO PARA INTEGRACIÓN ---

            Debug.Log("Todas las frutas recolectadas. Activando victoria.");

            // 1. Mostrar la UI de "Nivel Completado" inmediatamente (si lo deseas)
            if (levelCleared != null)
            {
                levelCleared.gameObject.SetActive(true);
            }
            if (transition != null)
            {
                transition.SetActive(true);
            }

            // 2. Detener la ejecución de este Update/Script para evitar llamadas repetidas
            enabled = false;

            // 3. LLAMADA CLAVE: Delegar el control de la victoria al UIManager
            if (UIManager.Instance != null)
            {
                // UIManager.ShowVictoryPanel() contiene la lógica:
                // - Si es el último nivel: Muestra el Panel de Victoria.
                // - Si NO es el último: Carga la siguiente escena directamente.

                // Usamos Invoke para darle tiempo a que la transición visual ocurra (1 segundo)
                Invoke("CallVictoryPanel", 1f);
            }
            else
            {
                Debug.LogError("ERROR: UIManager.Instance no encontrado. La escena no avanzará.");
                // Opcional: Fallback a SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            // ---------------------------------------------
        }
    }

    void CallVictoryPanel()
    {
        // Se llama después del retraso (Invoke)
        UIManager.Instance.ShowVictoryPanel();
    }

    // El método ChangeScene() y su lógica de carga de escena ya NO son necesarios aquí.
    // Ahora todo se gestiona desde UIManager.ShowVictoryPanel().
}