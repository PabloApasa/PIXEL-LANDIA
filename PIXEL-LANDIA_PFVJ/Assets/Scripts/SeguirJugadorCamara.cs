using Unity.Cinemachine;
using UnityEngine;

public class SeguirJugadorCamara : MonoBehaviour
{
    [SerializeField] private CinemachineCamera cinemachineCamera;

    private void Start()
    {
        SeguirJugador();
    }

    private void SeguirJugador()
    {
        escriptdelmoviminetojugador jugador = FindFirstObjectByType<scriptdelmovimientodeljugador>();

        if (jugador == null)
        {
            Debug.LogWarning("No se encontró al jugador.");
            return;
        }
        
        Transform jugadorTransform = jugador.transform;
        cinemachineCamera.Follow = jugadorTransform;
    }
}
