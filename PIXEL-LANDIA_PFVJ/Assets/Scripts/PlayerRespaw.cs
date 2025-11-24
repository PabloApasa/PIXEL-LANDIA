using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerRespaw : MonoBehaviour
{
    public GameObject[] hearts;
    private int life;

    private float checkPointPositionX, checkPointPositionY;

    public Animator animator;

    void Start()
    {
        life = hearts.Length;

        if (PlayerPrefs.GetFloat("CheckPointPositionX") != 0)
        {
            transform.position = new Vector2(PlayerPrefs.GetFloat("CheckPointPositionX"), PlayerPrefs.GetFloat("CheckPointPositionY"));
        }
    }

    private void ChechLife()
    {
        if (life < 1)
        {
            Destroy(hearts[0].gameObject);
            animator.Play("Hit");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else if (life < 2)
        {
            Destroy(hearts[1].gameObject);
            animator.Play("Hit");
        }
        else if (life < 3)
        {
            Destroy(hearts[2].gameObject);
            animator.Play("Hit");
        }
    }

    public void ReachedCheckPoint(float x, float y)
    {
        PlayerPrefs.SetFloat("CheckPointPositionX", x);
        PlayerPrefs.SetFloat("CheckPointPositionY", y);
    }
    public void PlayerDamaged()
    {
        life--;
        ChechLife(); 
    }
}
