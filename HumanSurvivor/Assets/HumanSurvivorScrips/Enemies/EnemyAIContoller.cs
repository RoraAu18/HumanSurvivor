using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIContoller : MonoBehaviour
{
    [SerializeField]
    public SMNode mainNode;
    [SerializeField]
    SMContext context;
    Rigidbody rb;
    void Start()
    {
        TryGetComponent(out rb);
    }

    // Update is called once per frame
    void Update()
    {
        mainNode.Run(context);
    }
}
