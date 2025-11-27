using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class FruitCollected : MonoBehaviour
{
    [SerializeField] private float cantidadPuntos;
    [SerializeField] private Puntaje puntaje;

    public AudioSource clip;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GetComponent<SpriteRenderer>().enabled = false;
            gameObject.transform.GetChild(0).gameObject.SetActive(true);


            Destroy(gameObject, 0.5f);

            puntaje.SumarPuntos(cantidadPuntos);
            ControladorPuntos.instance.SumarPuntos(cantidadPuntos);

            clip.Play();
        }
    }
}
