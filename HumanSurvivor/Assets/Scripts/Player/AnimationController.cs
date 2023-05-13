using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public Animator animator;
    public AIPlayerController player;

    void Start()
    {
        TryGetComponent(out animator);
        player = GetComponentInParent<AIPlayerController>();
        player.OnStatePlayerChange += ChangePlayerAnims;
    }

    //animator eyes config
      //idle happy
      //run squint
      //jump squint
      //stealth blink
      //distract trauma

    private void ChangePlayerAnims(PlayerStates states)
    {
        animator.SetBool("Run", false);

        switch (states)
        {
            case PlayerStates.idle:
                animator.SetTrigger("Idle");
                break;
            case PlayerStates.run:
                animator.SetBool("Run", true);
                break;
            case PlayerStates.jump:
                animator.SetTrigger("Jump");
                break;
            case PlayerStates.stealth:
                animator.SetTrigger("Stealth");
                break;
            case PlayerStates.distract:
                animator.SetTrigger("Distract");
                break;
        }
    }
}
