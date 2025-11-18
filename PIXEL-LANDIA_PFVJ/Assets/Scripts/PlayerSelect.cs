using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelect : MonoBehaviour
{
    public bool enableSelectCharacter;
    public enum Player { Frog, VirtualGuy, PinkMan, MaskDude };
    public Player playerSelected;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public RuntimeAnimatorController[] playerController;
    public Sprite[] playersRenderer;

    void Start()
    {
        if (!enableSelectCharacter)
        {
            ChangePlayerInMenu();
        }
        else
        {
            switch (playerSelected)
            {
                case Player.Frog:
                    spriteRenderer.sprite = playersRenderer[0];
                    animator.runtimeAnimatorController = playerController[0];
                    break;
                case Player.PinkMan:
                    spriteRenderer.sprite = playersRenderer[1];
                    animator.runtimeAnimatorController = playerController[1];
                    break;
                case Player.VirtualGuy:
                    spriteRenderer.sprite = playersRenderer[2];
                    animator.runtimeAnimatorController = playerController[2];
                    break;
                case Player.MaskDude:
                    spriteRenderer.sprite = playersRenderer[3];
                    animator.runtimeAnimatorController = playerController[3];
                    break;
                default:
                    break;
            }
        }
    }

    public void ChangePlayerInMenu()
    {
        switch (PlayerPrefs.GetString("PlayerSelected"))
        {
            case "Frog":
                spriteRenderer.sprite = playersRenderer[0];
                animator.runtimeAnimatorController = playerController[0];
                break;
            case "PinkMan":
                spriteRenderer.sprite = playersRenderer[1];
                animator.runtimeAnimatorController = playerController[1];
                break;
            case "VirtualGuy":
                spriteRenderer.sprite = playersRenderer[2];
                animator.runtimeAnimatorController = playerController[2];
                break;
            case "MaskDude":
                spriteRenderer.sprite = playersRenderer[3];
                animator.runtimeAnimatorController = playerController[3];
                break;
            default:
                break;
        }
    }
}
