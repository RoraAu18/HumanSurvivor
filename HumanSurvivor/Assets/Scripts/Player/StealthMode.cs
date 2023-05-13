using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealthMode : MonoBehaviour
{
    public AIPlayerController playerController;
    public CharacterController cc;
    public bool stealth;
    private bool oldStealth;
    void Start()
    {
        TryGetComponent(out cc);
        TryGetComponent(out playerController);
        oldStealth = false;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            stealth = true;
        }
        else
        {
            stealth = false;
        }
        oldStealth = stealth;
    }
}
