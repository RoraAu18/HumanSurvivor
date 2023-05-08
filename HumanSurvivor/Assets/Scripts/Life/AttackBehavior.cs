using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehavior : MonoBehaviour
{
    public float damageAmount = 10;

    //This code will call the method in Damage controller where the object will encounter damage
    public void Attack(DamageController receiver)
    {
        receiver.GetDamage(damageAmount);
    }
}
