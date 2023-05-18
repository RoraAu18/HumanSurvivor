using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ConfusedCorroutine : MonoBehaviour
{
    public void Start()
    {
        
    }
    public IEnumerator ConfusedSeq(SMContext context, SMNodeStates states)
    {
        context.enemy.SetState(EnemyStates.Confused);
        yield return new WaitForSeconds(10);
        states = SMNodeStates.Succeed;
    }
}
