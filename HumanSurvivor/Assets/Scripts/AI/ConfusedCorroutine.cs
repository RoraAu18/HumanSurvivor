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
        yield return new WaitForSeconds(10);
        states = SMNodeStates.Succeed;
    }
}
