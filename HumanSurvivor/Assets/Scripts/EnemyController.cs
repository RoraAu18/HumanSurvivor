using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, iLifeController
{
    public Rigidbody rbody;
    public DamageController damageReceived;
    //public CapsuleCollider collid;
    
    // Start is called before the first frame update
    void Start()
    {
        TryGetComponent(out rbody);
        TryGetComponent(out damageReceived);
    }
        
    // Update is called once per frame
    void Update()
    {

    }

    /*
    public void IsDamaaged()
    {
        //trial
        var height = collid.height;
        transform.position += new Vector3(0,1,0) * height/2;
    }
    */
    public void OnDamage()
    {
        rbody.AddForce(Vector3.up * 3000);
        Debug.Log("estoy entrando a get damage");
    }

    public void OnDeath()
    {
        if (damageReceived.isDead)
        {
            gameObject.SetActive(false);
        }
    }
}
