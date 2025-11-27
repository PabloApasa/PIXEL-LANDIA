using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpDamage : MonoBehaviour
{
    public Collider2D collider2D;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public GameObject destroyParticle;
    public float jumpForce = 2.5f;
    public int lifes = 2;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().linearVelocity = (Vector2.up * jumpForce);
            LosseLifeAndHit();
            
        }
    }

    public void LosseLifeAndHit()
    {
        lifes--;
        animator.Play("Hit");
        CheckLife();
    }

    public void CheckLife()
    {
        if (lifes == 0)
        {
            destroyParticle.SetActive(true);
            spriteRenderer.enabled = false;
            Invoke("EmenyDie", 0.2f);
        }
    }

    public void EmenyDie()
    {
        Destroy(gameObject);
    }
}
