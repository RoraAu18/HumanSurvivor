using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealthMode : MonoBehaviour
{
    public AIPlayerController playerController;
    public CharacterController cc;
    [SerializeField]
    GameObject effect;
    public bool stealth;
    private bool oldStealth;
    void Start()
    {
        TryGetComponent(out cc);
        TryGetComponent(out playerController);
        effect.gameObject.SetActive(false);
        oldStealth = false;
    }

    void Update()
    {
        if (GameManager.OnlyInstance.gameStates == GameStates.GameOver) return;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            effect.gameObject.SetActive(true);
            playerController.onStealhMode = true;
            stealth = true;
        }
        else
        {
            effect.gameObject.SetActive(false);
            playerController.onStealhMode = false;
            stealth = false;
        }
        oldStealth = stealth;
    }
}

