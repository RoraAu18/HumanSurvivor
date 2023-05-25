using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimatorController : MonoBehaviour
{
    EnemyAIContoller enemy;
    [SerializeField]
    Animator animator;
    void Start()
    {
        if (GameManager.OnlyInstance.gameStates == GameStates.GameOver) return;
        enemy = GetComponentInParent<EnemyAIContoller>();
        TryGetComponent(out animator);
        enemy.onEnemyStateChange += OnStateChange;
    }
    void OnStateChange(EnemyStates states)
    {
        Debug.Log("entering anim " + states);
        switch (states )
        {
            case EnemyStates.Idle:
                animator.SetTrigger("Idle");
                break;            
            case EnemyStates.Distracted:
                animator.SetTrigger("Distracted");
                break;
            case EnemyStates.Walking:
                animator.SetTrigger("Walking");
                break;
            case EnemyStates.Confused:
                animator.SetTrigger("Confused");
                break;
            case EnemyStates.Surprised:
                animator.SetTrigger("Surprised");
                break;            
            case EnemyStates.Running:
                animator.SetTrigger("Running");
                break;            
            case EnemyStates.CatchingPlayer:
                animator.SetTrigger("CatchingPlayer");
                break;
            case EnemyStates.Desperate:
                animator.SetTrigger("Desperate");
                break;
        }
    } 

}
