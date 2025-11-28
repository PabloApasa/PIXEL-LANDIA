using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerRespaw : MonoBehaviour
{
    // Variables públicas
    public GameObject[] hearts;  
    public Animator animator;    

    // Variables privadas
    private int life;
    private float checkPointPositionX, checkPointPositionY;

    void Start()
    {
        life = hearts.Length;

        
        if (PlayerPrefs.GetFloat("CheckPointPositionX") != 0)
        {
            transform.position = new Vector2(PlayerPrefs.GetFloat("CheckPointPositionX"), PlayerPrefs.GetFloat("CheckPointPositionY"));
        }
    }

   
    public void PlayerDamaged()
    {
        
        if (life <= 0) return;

        
        life--;

        
        UpdateLifeState();
    }

    private void UpdateLifeState()
    {
        
        if (animator != null)
        {
            animator.Play("Hit");
        }

        
        if (life >= 0 && life < hearts.Length)
        {
            
            hearts[life].SetActive(false);
        }

     
        if (life <= 0)
        {
            HandlePlayerDefeat();
        }
    }

    private void HandlePlayerDefeat()
    {
        
        if (UIManager.Instance != null)
        {
            
            UIManager.Instance.ShowDefeatPanel();
        }
        else
        {
           
            Debug.LogError("UIManager.Instance no se encontró. No se puede mostrar la pantalla de derrota.");
            
        }

       
    }

  
    public void ReachedCheckPoint(float x, float y)
    {
        PlayerPrefs.SetFloat("CheckPointPositionX", x);
        PlayerPrefs.SetFloat("CheckPointPositionY", y);
        PlayerPrefs.Save(); 
    }
}