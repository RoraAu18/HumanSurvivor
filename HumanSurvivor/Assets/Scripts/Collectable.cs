using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public ColisionController colisionController;
    public Collider myCollider;
    void Start()
    {
        TryGetComponent<ColisionController>(out colisionController);
        TryGetComponent<Collider>(out myCollider);
        colisionController.collisionEnter += colletObject;
    }

    void colletObject(Collider collectableObj)
    {
        if (collectableObj.TryGetComponent<AIPlayerController>(out _))
        {
            gameObject.SetActive(false);
            GameManager.OnlyInstance.AddItemCollected(1);
        }
    }


}
