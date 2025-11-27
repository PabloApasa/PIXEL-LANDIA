using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveJoystck : MonoBehaviour
{
    private float horizontalMove = 0f;
    private float verticalMove = 0f;
    public Joystick joystick;
    public float runSpeedHorizontal = 2;
    public float runSpeed = 2;
    public float jumpSpeed = 3;
    public float doubleJumpSpeed = 2.5f;
    private bool canDoubleJump;
    Rigidbody2D rb2D;

   
    public SpriteRenderer spriteRenderer;

    public Animator animator;

    public GameObject dustLeft;
    public GameObject dustRight;
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (horizontalMove > 0)
        {
            spriteRenderer.flipX = false;
            animator.SetBool("Run", true);

            if (CheckGround.isGrounded == true)
            {
                dustLeft.SetActive(true);
                dustRight.SetActive(false);
            }

        }
        else if (horizontalMove < 0)
        {
            spriteRenderer.flipX = true;
            animator.SetBool("Run", true);

            if (CheckGround.isGrounded == true)
            {
                dustLeft.SetActive(false);
                dustRight.SetActive(true);
            }
        }
        else
        {
            animator.SetBool("Run", false);

            dustLeft.SetActive(false);
            dustRight.SetActive(false);
        }

        if (CheckGround.isGrounded == false)
        {
            animator.SetBool("Jump", true);
            animator.SetBool("Run", false);
            dustLeft.SetActive(false);
            dustRight.SetActive(false);
        }
        if (CheckGround.isGrounded == true)
        {
            animator.SetBool("Jump", false);
            animator.SetBool("DoubleJump", false);
            animator.SetBool("Falling", false);
        }
        if (rb2D.linearVelocity.y < 0)
        {
            animator.SetBool("Falling", true);

        }
        else if (rb2D.linearVelocity.y >= 0)
        {
            animator.SetBool("Falling", false);
        }
    }
    void FixedUpdate()
    {
        horizontalMove = joystick.Horizontal * runSpeedHorizontal;
        transform.position += new Vector3(horizontalMove, 0, 0) * Time.deltaTime * runSpeed;
    }

    public void Jump()
    {
        if (CheckGround.isGrounded)
        {
            canDoubleJump = true;
            rb2D.linearVelocity = new Vector2(rb2D.linearVelocity.x, jumpSpeed);
        }
        else
        {


            if (canDoubleJump)
            {
                animator.SetBool("DoubleJump", true);
                rb2D.linearVelocity = new Vector2(rb2D.linearVelocity.x, doubleJumpSpeed);
                canDoubleJump = false;
            }

        }
    }
}
