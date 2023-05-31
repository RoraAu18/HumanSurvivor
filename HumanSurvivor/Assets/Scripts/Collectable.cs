using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public ColisionController colisionController;
    private Collider myCollider;
    public ObjectsType myObjectype;

    void Start()
    {
        TryGetComponent<ColisionController>(out colisionController);
        TryGetComponent<Collider>(out myCollider);
        colisionController.collisionEnter += ColletObject;
    }

    void ColletObject(Collider collectableObj)
    {
        if (collectableObj.TryGetComponent<AIPlayerController>(out _))
        {
            gameObject.SetActive(false);
            GameManager.OnlyInstance.AddItemCollected(myObjectype);
        }
    }


}
