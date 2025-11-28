using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FruitManager : MonoBehaviour
{

    public Text levelCleared; // Texto "Nivel Completado"
    public GameObject transition; // Objeto de transición (opcional, si aún lo quieres usar)

    // UI de contadores
    public Text totalFruits;
    public Text fruitsCollected;

    private int totalFruitsInLevel;

    public void Start()
    {

        totalFruitsInLevel = transform.childCount;
    }

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
 
                Invoke("CallVictoryPanel", 1f);
            }
            else
            {
                Debug.LogError("ERROR: UIManager.Instance no encontrado. La escena no avanzará.");
                
            }

        }
    }

    void CallVictoryPanel()
    {

        UIManager.Instance.ShowVictoryPanel();
    }

}