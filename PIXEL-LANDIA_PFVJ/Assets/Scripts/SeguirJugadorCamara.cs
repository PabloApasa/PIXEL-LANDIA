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
        PlayerMove Player = FindFirstObjectByType<PlayerMove>();

        if (Player == null)
        {
            Debug.LogWarning("No se encontró al jugador.");
            return;
        }
        
        Transform jugadorTransform = Player.transform;
        cinemachineCamera.Follow = jugadorTransform;
    }
}
