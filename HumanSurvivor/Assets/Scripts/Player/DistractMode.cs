using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistractMode : MonoBehaviour
{
    
    public AIPlayerController playerController;
    public CharacterController cc;
    public bool distract;
    private bool oldDistract;
    public Transform posForDistract;
    void Start()
    {
        TryGetComponent(out cc);
        TryGetComponent(out playerController);
        oldDistract = false;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            distract = true;
            GameManager.OnlyInstance.OnDistractMode();
            posForDistract.position = transform.position;
        }
        else
        {
            distract = false;
        }
        oldDistract = distract;
    }
}
