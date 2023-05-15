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
        enemy = GetComponentInParent<EnemyAIContoller>();
        TryGetComponent(out animator);
        enemy.onEnemyStateChange += OnStateChange;
    }
    void OnStateChange(EnemyStates states)
    {
        Debug.Log("entering anim");
        switch (states )
        {
            case EnemyStates.Idle:
                animator.SetTrigger("Idle");
                break;            
            case EnemyStates.Distracted:
                animator.SetTrigger("Distracted");
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

        }
    } 

}
