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
        if (GameManager.OnlyInstance.gameStates == GameStates.GameOver) return;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            playerController.onStealhMode = true;
            stealth = true;
        }
        else
        {
            playerController.onStealhMode = false;
            stealth = false;
        }
        oldStealth = stealth;
    }
}

