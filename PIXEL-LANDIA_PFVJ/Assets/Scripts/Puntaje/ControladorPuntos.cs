using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorPuntos : MonoBehaviour
{
    public static ControladorPuntos instance;
    [SerializeField] private float cantidadPuntos;

    private void Awake()
    {
        if (ControladorPuntos.instance == null)
        {
            ControladorPuntos.instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void SumarPuntos(float puntos)
    {
        cantidadPuntos += puntos;
    }

    public float GetPuntos()
    {
        return cantidadPuntos;
    }
}
